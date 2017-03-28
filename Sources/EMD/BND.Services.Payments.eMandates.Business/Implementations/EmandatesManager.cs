using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using BND.Services.Payments.eMandates.Business.Exceptions;
using BND.Services.Payments.eMandates.Business.Interfaces;
using BND.Services.Payments.eMandates.Business.Models;
using BND.Services.Payments.eMandates.Business.Utilities;
using BND.Services.Payments.eMandates.Domain.Interfaces;
using BND.Services.Payments.eMandates.Entities;
using BND.Services.Payments.eMandates.Enums;
using EnumSequenceType = BND.Services.Payments.eMandates.Business.Models.EnumSequenceType;
using ModelDb = BND.Services.Payments.eMandates.Models;
using LogMsg = BND.Services.Payments.eMandates.Resources.Business.Logger;

//using eMandates.Merchant.Library.Misc;
//using SequenceType = eMandates.Merchant.Library.SequenceType;

namespace BND.Services.Payments.eMandates.Business.Implementations
{
    public class EMandatesManager : IEMandatesManager
    {
        /// <summary>
        /// The _e mandates client
        /// </summary>
        private IEMandatesClient _eMandatesClient;

        private IDirectoryRepository _directoryRepository;

        private ITransactionRepository _transactionRepository;

        private ISettingRepository _settingRepository;

        private ILogger _logger;

        public EMandatesManager(IEMandatesClient eMandatesClient, 
            IDirectoryRepository directoryRepository, 
            ITransactionRepository transactionRepository, 
            ISettingRepository settingRepository,
            ILogger logger)
        {
            _eMandatesClient = eMandatesClient;
            _directoryRepository = directoryRepository;
            _transactionRepository = transactionRepository;
            _settingRepository = settingRepository;
            _logger = logger;
        }

        public Entities.DirectoryModel GetDirectory()
        {
            try
            {
                ModelDb.Directory directory = _directoryRepository.Get();
                int dayCheckPeriod = Convert.ToInt32(_settingRepository.GetValueByKey("DayCheckPeriod"));

                if (directory == null || (DateTime.Now - directory.LastDirectoryRequestDateTimestamp).Days >= dayCheckPeriod)
                {
                    DirectoryResponseModel response = _eMandatesClient.SendDirectoryRequest();

                    if (response.IsError)
                    {
                        throw new eMandateOperationException(response.Error.ErrorCode, String.Format("ErrorCode:{0}\nConsumerMessage:{1}\nConsumerMessage:{2}\nErrorDetails:{3}\nSuggestedAction:{4}",
                                                                            response.Error.ErrorCode,
                                                                            response.Error.ErrorMessage,
                                                                            response.Error.ConsumerMessage,
                                                                            response.Error.ErrorDetails,                                                                            
                                                                            response.Error.SuggestedAction));
                    }

                    // Creates directory object from response
                    var newDirectory = new ModelDb.Directory
                    {
                        DirectoryDateTimestamp = response.DirectoryDateTimestamp,
                        LastDirectoryRequestDateTimestamp = DateTime.Now,
                        DebtorBanks = response.DebtorBanks.Select(s=> new ModelDb.DebtorBank
                        {
                            DebtorBankCountry = s.DebtorBankCountry,
                            DebtorBankId = s.DebtorBankId,
                            DebtorBankName = s.DebtorBankName                            
                        }).ToList(), 
                        RawMessage = new ModelDb.RawMessage
                        {
                            Message = response.RawMessage                            
                        }
                    };

                    // insert new directory to database
                    _directoryRepository.UpdateDirectory(newDirectory);

                    // logging
                    _logger.Info("Directories have been updated successfully.");

                    // get the lastest one from database.
                    directory = _directoryRepository.Get();
                }

                // Create the directory entity from model
                Entities.DirectoryModel result = new Entities.DirectoryModel()
                {
                    DebtorBanks = directory.DebtorBanks.Select(s => new DebtorBank()
                    {
                        DebtorBankCountry = s.DebtorBankCountry,
                        DebtorBankId = s.DebtorBankId,
                        DebtorBankName = s.DebtorBankName
                    }).ToList()
                };
                _logger.Info("Get Directory has been successfully.");
                return result;
            }
            catch (eMandateOperationException ex)
            {
                // log error
                _logger.Error(ex, ex.ErrorCode);

                throw ex;
            }
            catch (Exception ex)
            {
                // log error
                _logger.Error(ex, LogMsg.GetDirectoryErrorMessageId);

                throw new eMandateOperationException(LogMsg.GetDirectoryErrorMessageId, ex.Message, ex);
            }
        }

        public TransactionResponseModel CreateTransaction(NewTransactionModel newTransaction)
        {
            TransactionResponseModel result;

            // Validate NewTransactionRequest
            try
            {
                ValidateNewTransactionModel(newTransaction); // todo
            }
            catch (Exception ex)
            {
                throw new ValidationException("There was a validation exception. Check inner exception for details.", ex);
            }

            try
            {
                // Generate entranceCode
                string entranceCode = EntranceCode.Generate();

                // Create transaction request
                NewMandateRequestModel nmr = new NewMandateRequestModel();
                nmr.DebtorBankId = newTransaction.DebtorBankId;
                nmr.DebtorReference = newTransaction.DebtorReference; // nullable
                nmr.EMandateId = newTransaction.EMandateId;
                nmr.EMandateReason = newTransaction.EMandateReason; // nullable
                nmr.EntranceCode = entranceCode;
                nmr.ExpirationPeriod = newTransaction.ExpirationPeriod;
                nmr.Language = newTransaction.Language;
                nmr.MaxAmount = newTransaction.MaxAmount;
                nmr.MessageId = _eMandatesClient.GenerateMessageId();
                nmr.PurchaseId = newTransaction.PurchaseId; // nullable

                // TODO make sure below part works ok
                EnumSequenceType st;
                EnumSequenceType.TryParse(newTransaction.SequenceType, true, out st);
                nmr.SequenceType = st;

                // Call eMandates service (coreCommunicator)
                NewMandateResponseModel resp = _eMandatesClient.SendTransactionRequest(nmr);

                // create response and return
                if (resp == null)
                {
                    // handle error

                    // log

                    // throw
                    throw new Exception("Error while creating new emandate.");
                } 

                if (resp.IsError)
                {
                     throw new Exception(string.Format("Error while creating new emandate. \nCode: {0}\nMessage: {1} \nDetails: {2}", 
                         resp.Error.ErrorCode,
                         resp.Error.ErrorMessage,
                         resp.Error.ErrorDetails));
                }

                // add transaction to the DB
                ModelDb.Transaction transaction = new ModelDb.Transaction()
                {
                    SequenceType = nmr.SequenceType.ToString(),
                    IssuerAuthenticationUrl = resp.IssuerAuthenticationUrl,
                    TransactionCreateDateTimestamp = resp.TransactionCreateDateTimestamp,
                    RawMessage = new ModelDb.RawMessage()
                    {
                        Message = resp.RawMessage
                    },
                    EntranceCode = entranceCode,
                    DebtorReference = nmr.DebtorReference,
                    DebtorBankID = nmr.DebtorBankId,
                    Language = nmr.Language,
                    MaxAmount = nmr.MaxAmount,
                    ExpirationPeriod = nmr.ExpirationPeriod.HasValue ? (long)nmr.ExpirationPeriod.Value.TotalSeconds : (long?)null, // todo
                    EMandateID = nmr.EMandateId,
                    EMandateReason = nmr.EMandateReason,
                    IsSystemFail = false, //todo
                    MerchantReturnUrl = "", //todo
                    MessageID = nmr.MessageId,
                    PurchaseID = nmr.PurchaseId,
                    TransactionID = resp.TransactionId,
                    TransactionType = null, //todo
                    OriginalIban = null//

                };
                _transactionRepository.Insert(transaction);

                // Prepare response
                result = new TransactionResponseModel();
                result.TransactionId = resp.TransactionId;
                result.IssuerAuthenticationUrl = resp.IssuerAuthenticationUrl;
                result.TransactionRequestDateTimeStamp = resp.TransactionCreateDateTimestamp;
                
            }
            catch (Exception ex)
            {
                // log

                // throw
                throw;
            }

           return result;
        }

        private void ValidateNewTransactionModel(NewTransactionModel newTransaction)
        {
            // not null
            if (newTransaction == null)
            {
                throw new ArgumentNullException("newTransaction");
            }

            // DebtorBankId
            if (String.IsNullOrWhiteSpace(newTransaction.DebtorBankId))
            {
                throw new ArgumentException("newTransaction.DebtorBankId");
            }

            Regex rgx = new Regex(@"^([a-zA-Z]){4}([a-zA-Z]){2}([0-9a-zA-Z]){2}([0-9a-zA-Z]{3})?$");
            if (!rgx.IsMatch(newTransaction.DebtorBankId))
            {
                throw new ArgumentException("newTransaction.DebtorBankId");
            }

            // EMandateId
            if (String.IsNullOrWhiteSpace(newTransaction.EMandateId))
            {
                throw new ArgumentException("newTransaction.EMandateId");
            }

            // Language
            if (String.IsNullOrWhiteSpace(newTransaction.Language))
            {
                throw new ArgumentException("newTransaction.Language");
            }

            // SequenceType
            if (String.IsNullOrWhiteSpace(newTransaction.SequenceType))
            {
                throw new ArgumentException("newTransaction.SequenceType");
            }
        }

        public EnumQueryStatus GetTransactionStatus(string transactionId)
        {
            // Set default status to Open. This will be updated in the next step.
            EnumQueryStatus resultStatus = EnumQueryStatus.Open; // todo make sure we can start with this status

            // check transaction Id
            if (String.IsNullOrWhiteSpace(transactionId))
            {
                throw new ValidationException("There was a validation exception. Check inner exception for details.", new ArgumentNullException("transactionId"));
            }

            // Get status from Db
            ModelDb.Transaction transaction = _transactionRepository.GetTransactionWithLatestStatus(transactionId);

            if (transaction == null)
            {
                // throw exception cannot find transaction
                throw new Exception(String.Format("Transaction with id:{0} doesnt exist.", transactionId));
            }

            if (transaction.TransactionStatusHistories.Any())
            {
                if (!Enum.TryParse(transaction.TransactionStatusHistories.First().Status,
                        out resultStatus))
                {
                    // Cannot parse, throw exception
                    throw new Exception(String.Format("Unknown status ({0})", transaction.TransactionStatusHistories.First().Status));
                }
            }

            if (resultStatus == EnumQueryStatus.Open || resultStatus == EnumQueryStatus.Pending) // If status is open or pending, continue
            {
                // todo 
                //ValidateTransactionAttempt(transaction);

                // get from emandate 
                StatusResponseModel srm = _eMandatesClient.SendStatusRequest(transactionId);

                if (srm == null)
                {
                    throw new Exception("Error while requsting status for transaction: " + transactionId);
                }

                if (!srm.IsError)
                {
                    resultStatus = srm.Status;
                    bool isSystemFail = false;
                    

                    // If status is Success, Failure, Expired or Canceled, update in Db and return
                    switch (srm.Status)
                    {
                        // status Success, Failure, Expired or Canceled - update in Db and return
                            // For success add to raw response
                        case EnumQueryStatus.Success:
                            // TODO save to warehouse
                        case EnumQueryStatus.Failure:
                        case EnumQueryStatus.Expired:
                        case EnumQueryStatus.Cancelled:
                            break;
                        case EnumQueryStatus.Open:
                            // status open if over expiry time (30 mins, set system failure and return
                            if (IsExpired(transaction, 30*60))
                            {
                                isSystemFail = true;
                            }
                            break;
                        case EnumQueryStatus.Pending:
                            // status pending, if over expiry time (7 days) set system failure and return
                            if (IsExpired(transaction, 60 * 60 * 24 * 7))
                            {
                                isSystemFail = true;
                            }
                            break;
                        default:
                            break;
                    }

                    _transactionRepository.UpdateStatus(isSystemFail, new ModelDb.TransactionStatusHistory()
                    {
                        Status = srm.Status.ToString(),
                        RawMessage = 
                            !string.IsNullOrWhiteSpace(srm.RawMessage) ? new ModelDb.RawMessage(){ Message = srm.RawMessage} : null,
                        StatusDateTimestamp =  srm.StatusDateTimestamp,
                        TransactionID = transactionId
                       
                    });
                }
                else // If error, handle  // If not error, check status. 
                {
                    throw new Exception(string.Format("Error while getting status for eMandate. \nCode: {0}\nMessage: {1} \nDetails: {2}", 
                         srm.Error.ErrorCode,
                         srm.Error.ErrorMessage,
                         srm.Error.ErrorDetails));
                }
                
            }

            return resultStatus;;
        }

        private bool IsExpired(ModelDb.Transaction transaction, int seconds)
        {
            return DateTime.Now.Subtract(transaction.TransactionCreateDateTimestamp).Seconds > seconds;
        }

        private void ValidateTransactionAttempt(ModelDb.Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
