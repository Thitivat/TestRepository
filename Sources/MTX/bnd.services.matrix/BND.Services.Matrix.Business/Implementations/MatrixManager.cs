using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;

using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Matrix.Business.Extensions;
using BND.Services.Matrix.Business.FiveDegrees.CashAccount;
using BND.Services.Matrix.Business.FiveDegrees.CenterService;
using BND.Services.Matrix.Business.FiveDegrees.PaymentService;
using BND.Services.Matrix.Business.FiveDegrees.PortfolioService;
using BND.Services.Matrix.Business.FiveDegrees.ProductBuilder;
using BND.Services.Matrix.Business.Interfaces;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Enums;

using IdentityModel.Client;

using AccountCloseResultsItem = BND.Services.Matrix.Entities.AccountCloseResultsItem;
using BucketItem = BND.Services.Matrix.Entities.BucketItem;
using ClosingPaymentItem = BND.Services.Matrix.Entities.ClosingPaymentItem;
using Currencies = BND.Services.Matrix.Business.FiveDegrees.CashAccount.Currencies;
using FeeHandlings = BND.Services.Matrix.Business.FiveDegrees.CashAccount.FeeHandlings;
using IdentificationStandards = BND.Services.Matrix.Business.FiveDegrees.PortfolioService.IdentificationStandards;
using IntervalTimeUnit = BND.Services.Matrix.Business.FiveDegrees.CashAccount.IntervalTimeUnit;
using PaymentBucketItem = BND.Services.Matrix.Entities.PaymentBucketItem;
using PaymentSources = BND.Services.Matrix.Business.FiveDegrees.CashAccount.PaymentSources;
using PaymentTypes = BND.Services.Matrix.Business.FiveDegrees.CashAccount.PaymentTypes;
using ServiceItem = BND.Services.Matrix.Business.FiveDegrees.CashAccount.ServiceItem;
using TaxExemptStates = BND.Services.Matrix.Business.FiveDegrees.CashAccount.TaxExemptStates;

namespace BND.Services.Matrix.Business.Implementations
{
    /// <summary>
    /// The matrix manager <see langword="class"/>
    /// </summary>
    public class MatrixManager : IMatrixManager
    {
        /// <summary>
        /// The class type name
        /// </summary>
        private readonly string _typeName;

        /// <summary>
        /// The type of <see cref="EnumErrorCodes"/>.
        /// </summary>
        private readonly Type _enumErrorCodesType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixManager"/> class.
        /// </summary>
        public MatrixManager()
        {
            _typeName = GetType().FullName;
            _enumErrorCodesType = typeof(EnumErrorCodes);
        }

        /// <summary>
        /// This method creates a savings account (This method requires access authentication)
        /// </summary>
        /// <param name="savingsFree">
        /// The <see cref="SavingsFree"/> entity
        /// </param>
        public void CreateSavingsAccount(SavingsFree savingsFree)
        {
            if (savingsFree == null)
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.SavingsNull,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;

            var centerId = GetCenter(accessToken);
            var departmentId = GetDepartment(accessToken, centerId, savingsFree.DepartmentName);
            var unitId = GetUnit(accessToken, centerId, departmentId, savingsFree.UnitName);
            var savingsProductId = GetSavingsProduct(accessToken, centerId);

            var portfolio = new PortfolioItem()
                                {
                                    ExternalPortfolioId = savingsProductId,
                                    UnitId = unitId
                                };
            var service = new ServiceItem()
            {
                AccountNumber = savingsFree.Iban.Trim(),
                Currency = ConfigurationManager.AppSettings[Constants.MatrixCurrency].ParseEnum<Currencies>(),
                FeeHandling = FeeHandlings.Service,
                LinkedAccountNumber =
                    string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[Constants.MatrixLinkedAccountNumber])
                        ? null
                        : ConfigurationManager.AppSettings[Constants.MatrixLinkedAccountNumber],
                NominatedAccountNumber = savingsFree.NominatedIban.Trim(),
                ProductId = savingsProductId,
                StatementInterval = new FiveDegrees.CashAccount.IntervalItem()
                                        {
                                            TimeUnit = ConfigurationManager.AppSettings[Constants.MatrixStatementInterval].ParseEnum<IntervalTimeUnit>(),
                                            Value = int.Parse(ConfigurationManager.AppSettings[Constants.MatrixStatementIntervalValue])
                                        },
                TaxExemptState = ConfigurationManager.AppSettings[Constants.MatrixTaxExemptState].ParseEnum<TaxExemptStates>()
            };

            var cashAccountService = new CashAccountBSL();
            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));
            var account = cashAccountService.AccountOpen(Guid.NewGuid().ToString(), centerId, centerId != 0, portfolio, service);

            ValidateResultCode(account.ResultCode, account.ErrorMessage);
        }

        /// <summary>
        /// Closes an account.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="closingPaymentItem"> The <see cref="Entities.ClosingPaymentItem"/> </param>
        /// <returns>
        /// The <see cref="AccountCloseResultsItem"/>.
        /// </returns>
        public AccountCloseResultsItem CloseAccount(string iban, ClosingPaymentItem closingPaymentItem)
        {
            if (string.IsNullOrWhiteSpace(iban))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.IBANNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            if (closingPaymentItem == null)
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.ClosingPaymentItemIsNull,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);
            var cashAccountService = new CashAccountBSL();
            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var result = cashAccountService.AccountClose(Guid.NewGuid().ToString(), centerId, true, iban, closingPaymentItem.ToMatrixModel());
            ValidateResultCode(result.ResultCode, result.ErrorMessage);

            return result.Data == null ? null : result.Data.ToEntity();
        }

        /// <summary>
        /// The block saving accounts.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool BlockSavingAccounts(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.IBANNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);

            var cashAccountService = new CashAccountBSL();
            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var result = cashAccountService.ChangeStateToBlockWithdrawals(Guid.NewGuid().ToString(), centerId, centerId != 0, iban);

            ValidateResultCode(result.ResultCode, result.ErrorMessage);

            return result.Data;
        }

        /// <summary>
        /// The unblock saving accounts.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        public void UnblockSavingAccounts(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.IBANNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;

            var centerId = GetCenter(accessToken);
            var cashAccountService = new CashAccountBSL();
            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));
            var res = cashAccountService.ChangeStateToOpen(Guid.NewGuid().ToString(), centerId, centerId != 0, iban.Trim());
            ValidateResultCode(res.ResultCode, res.ErrorMessage);
        }

        /// <summary>
        /// The get accrued inerest.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <param name="valueDate">
        /// The value date.
        /// </param>
        /// <param name="calculateTaxAction">
        /// The calculate tax action.
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public decimal GetAccruedInterest(string iban, DateTime valueDate, bool calculateTaxAction)
        {
            if (string.IsNullOrWhiteSpace(iban))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.IBANNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            if (valueDate.IsDefault())
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.NotValidValueDate,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;

            var centerId = GetCenter(accessToken);
            var cashAccountService = new CashAccountBSL();
            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));
            var res = cashAccountService.GetAccruedInterest(
                Guid.NewGuid().ToString(),
                centerId,
                centerId != 0,
                iban.Trim(),
                valueDate,
                true,
                calculateTaxAction,
                true);

            ValidateResultCode(res.ResultCode, res.ErrorMessage);

            return res.Data;
        }

        /// <summary>
        /// The get overview balance.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <param name="valueDate">
        /// The value date.
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public decimal GetBalanceOverview(string iban, DateTime valueDate)
        {
            if (string.IsNullOrWhiteSpace(iban))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.IBANNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            if (valueDate.IsDefault())
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.NoDateProvided,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);
            var cashAccountService = new CashAccountBSL();
            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));
            var balance = cashAccountService.GetBalance(Guid.NewGuid().ToString(), centerId, centerId != 0, iban.Trim(), valueDate, true);

            ValidateResultCode(balance.ResultCode, balance.ErrorMessage);

            return balance.Data;
        }

        /// <summary>
        /// Gets an overview of the transactions for specified <paramref name="iban"/> between dates specified
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate"> The end date. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is <see cref="Entities.MovementItem"/> .
        /// </returns>
        public List<Entities.MovementItem> GetMovements(string iban, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (string.IsNullOrWhiteSpace(iban))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.IBANNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            if (startDate == null && endDate == null)
            {
                throw CreateException(
                   HttpStatusCode.BadRequest,
                   Resources.Business.MatrixManager.DateNotEntered,
                   EnumErrorCodes.BusinessLayerError);
            }

            if (startDate > endDate)
            {
                throw CreateException(
                   HttpStatusCode.BadRequest,
                   Resources.Business.MatrixManager.EndDateMustBeBigger,
                   EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);

            var cashAccountService = new CashAccountBSL();

            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var dateTo = endDate.HasValue ? endDate.Value.AddDays(1).AddMilliseconds(-1) : (DateTime?)null;

            var movementsResult = cashAccountService.GetMovements(Guid.NewGuid().ToString(), centerId, true, iban.Trim(), startDate, true, dateTo, endDate.HasValue);

            ValidateResultCode(movementsResult.ResultCode, movementsResult.ErrorMessage);

            return movementsResult.Data == null || movementsResult.Data.Payments == null ? null : movementsResult.Data.Payments.ToEntities();
        }

        /// <summary>
        /// The get interest rate override.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="options"> The options. </param>
        /// <returns>
        /// The <see cref="List{GetInterestRateOverrideItem}"/>.
        /// </returns>
        public List<InterestRate> GetInterestRate(string iban, InterestRateOptions options)
        {
            if (string.IsNullOrWhiteSpace(iban))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.IBANNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            if (options == null)
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Common.NotValidOptions,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            int centerId = GetCenter(accessToken);

            var cashAccountService = new CashAccountBSL();
            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));
            var interestRates = cashAccountService.GetInterestRateOverrides(
                Guid.NewGuid().ToString(),
                centerId,
                centerId != 0,
                iban.Trim(),
                options.FromDate,
                options.FromDate.HasValue,
                options.EndOverrideDate,
                options.EndOverrideDate.HasValue);

            ValidateResultCode(interestRates.ResultCode, interestRates.ErrorMessage);

            return interestRates.Data == null ? null : interestRates.Data.ToEntities();
        }

        /// <summary>
        /// The get overview savings account.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <returns>
        /// The <see cref="AccountOverview"/>.
        /// </returns>
        public AccountOverview GetAccountOverview(string iban, DateTime valueDate)
        {
            if (string.IsNullOrWhiteSpace(iban))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.IBANNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            if (valueDate.IsDefault())
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.NotValidValueDate,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            int centerId = GetCenter(accessToken);

            var cashAccountService = new CashAccountBSL();
            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var cashAccountOverviewItem = cashAccountService.GetCashAccountOverview(
                Guid.NewGuid().ToString(),
                centerId,
                centerId != 0,
                iban.Trim(),
                valueDate,
                true);

            ValidateResultCode(cashAccountOverviewItem.ResultCode, cashAccountOverviewItem.ErrorMessage);

            return cashAccountOverviewItem.Data == null ? null : cashAccountOverviewItem.Data.ToEntity();
        }

        /// <summary>
        /// The sweep.
        /// </summary>
        /// <param name="sweep">
        /// The sweep entity.
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public decimal Sweep(Sweep sweep)
        {
            if (sweep == null)
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.EmptySweepEntity,
                    EnumErrorCodes.BusinessLayerError);
            }

            if (sweep.Source.Equals(sweep.Destination, StringComparison.InvariantCultureIgnoreCase))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.AccountsAreSame,
                    EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(sweep.Destination) || string.IsNullOrWhiteSpace(sweep.Source))
            {
                throw CreateException(
                HttpStatusCode.BadRequest,
                Resources.Business.MatrixManager.UnspecifiedAccount,
                EnumErrorCodes.BusinessLayerError);
            }

            if (sweep.ValueDate == null || sweep.ValueDate == default(DateTime))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.NoDateProvided,
                    EnumErrorCodes.BusinessLayerError);
            }

            if (sweep.Amount == 0M)
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.ZeroAmount,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);

            var cashAccountService = new CashAccountBSL();
            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var result = cashAccountService.AccountSweep(
                Guid.NewGuid().ToString(),
                centerId,
                centerId != 0,
                new AccountSweepItem()
                    {
                        Amount = sweep.Amount,
                        ValueDate = sweep.ValueDate,
                        SourceAccount = sweep.Source,
                        DestinationAccount = sweep.Destination,
                        Reference = sweep.Description,
                        Clarification = sweep.Description,
                        PaymentType = PaymentTypes.Principal,
                        PaymentSource = PaymentSources.SystemInternal,
                        ClearingDate = sweep.ValueDate,
                        ClearingDateSpecified = true,
                        InterestDate = sweep.ValueDate,
                        InterestDateSpecified = true,
                        ExchangeRate = 1M,
                        Currency = ConfigurationManager.AppSettings[Constants.MatrixCurrency]
                    });

            ValidateResultCode(result.ResultCode, result.ErrorMessage);

            return result.Data;
        }

        /// <summary>
        /// The create outgoing payment.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <param name="payment">
        /// The payment.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CreateOutgoingPayment(string iban, Payment payment)
        {
            ValidatePayment(payment, iban);

            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);

            var cashAccountService = new CashAccountBSL();
            cashAccountService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var result = cashAccountService.OutgoingPayment(Guid.NewGuid().ToString(), centerId, centerId != 0, payment.SourceIBAN, payment.ToMatrixModel());

            ValidateResultCode(result.ResultCode, result.ErrorMessage);

            return result.Data == null ? null : result.Data.PaymentBucketId;
        }

        /// <summary>
        /// The get system accounts.
        /// </summary>
        /// <returns>
        /// The <see cref="List{SystemAccount}"/>.
        /// </returns>
        public List<SystemAccount> GetSystemAccounts()
        {
            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);

            var portfolioService = new PortfolioServiceBSL();
            portfolioService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var filter = new GetServicesFilterParams()
            {
                ShowInternalServices = false,
                ShowInternalServicesSpecified = true,
            };

            var services = portfolioService.GetServices(Guid.NewGuid().ToString(), centerId, centerId != 0, filter);
            ValidateResultCode(services.ResultCode, services.ErrorMessage);

            var sysAccounts = ConfigurationManager.AppSettings[Constants.MatrixSysAccounts].Split(new[] { ',' },
                StringSplitOptions.RemoveEmptyEntries);
            var accounts = new List<SystemAccount>();

            foreach (var sysAccount in sysAccounts)
            {
                accounts.AddRange(
                    services.Data.Where(
                        s =>
                            s.ServiceIdentifications.Length > 0 &&
                            s.ServiceIdentifications[0].AccountNumber.StartsWith(
                                sysAccount.Trim(),
                                StringComparison.InvariantCultureIgnoreCase))
                    .ToList()
                    .ToEntities());
            }

            foreach (var account in accounts)
            {
                var accountInfo =
                    portfolioService.GetService(
                        Guid.NewGuid().ToString(),
                        centerId,
                        centerId != 0,
                        new GetServiceFilterParams()
                        {
                            AccountNumber = account.AccountNumbers.First().Key
                        });

                ValidateResultCode(accountInfo.ResultCode, accountInfo.ErrorMessage);
                account.AddAccountInfo(accountInfo.Data);
            }

            return accounts;
        }

        /// <summary>
        /// Gets messages from Matrix.
        /// </summary>
        /// <param name="filters"> The <see cref="MessageFilters"/>. </param>
        /// <returns>
        /// The <see cref="List{T}"/> where T is of type <see cref="Entities.Message"/>.
        /// </returns>
        public List<Message> GetMessages(MessageFilters filters)
        {
            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);

            var paymentService = new PaymentServiceBSL();
            paymentService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var bulkFilterItem = new BulkFilterItem();

            var messageIdItemResult = paymentService.GetMessageIdData(Guid.NewGuid().ToString(), centerId, true);
            ValidateResultCode(messageIdItemResult.ResultCode, messageIdItemResult.ErrorMessage);

            if (filters != null)
            {
                if (filters.To == null && filters.From == null)
                {
                    throw CreateException(
                       HttpStatusCode.BadRequest,
                       Resources.Business.MatrixManager.DateNotEntered,
                       EnumErrorCodes.BusinessLayerError);
                }

                if (filters.From > filters.To)
                {
                    throw CreateException(
                       HttpStatusCode.BadRequest,
                       Resources.Business.MatrixManager.EndDateMustBeBigger,
                       EnumErrorCodes.BusinessLayerError);
                }

                bulkFilterItem.To = filters.To.HasValue ? filters.To.Value.AddDays(1).AddMilliseconds(-1) : (DateTime?)null;
                bulkFilterItem.From = filters.From;

                var directionsFilter = filters.Direction.HasValue
                                           ? filters.Direction.Value.ToValues().Select(filter => filter.ToString().ParseEnum<DirectionLevels>())
                                                    .ToList()
                                           : null;
                var statusesFilter = filters.Status.HasValue
                                         ? filters.Status.Value.ToValues().Select(filter => filter.ToString().ParseEnum<MessageStatuses>()).ToList()
                                         : null;
                var typesFilter = filters.Type.HasValue
                                      ? filters.Type.Value.ToValues().Select(filter => filter.ToString().ParseEnum<MessageTypes>()).ToList()
                                      : null;

                var messagesResult = paymentService.GetMessages(Guid.NewGuid().ToString(), centerId, true, bulkFilterItem);
                ValidateResultCode(messagesResult.ResultCode, messagesResult.ErrorMessage);

                var messagesToReturn = new List<Message>();

                if (messagesResult.Data != null)
                {
                    messagesToReturn.AddRange(
                        messagesResult.Data.Where(
                            x =>
                            (directionsFilter == null || (x.Direction.HasValue && directionsFilter.Contains(x.Direction.Value)))
                            && (statusesFilter == null || statusesFilter.Contains(x.MessageStatus))
                            && (typesFilter == null || (x.MessageType.HasValue && typesFilter.Contains(x.MessageType.Value)))).ToEntities(
                                messageIdItemResult.Data.OperationalCurrency.ToString()));
                }

                return messagesToReturn;
            }

            throw CreateException(
                   HttpStatusCode.BadRequest,
                   Resources.Business.MatrixManager.DateNotEntered,
                   EnumErrorCodes.BusinessLayerError);

            // var messagesResult = paymentService.GetMessages(Guid.NewGuid().ToString(), centerId, true, bulkFilterItem);
            // ValidateResultCode(messagesResult.ResultCode, messagesResult.ErrorMessage);

            // return messagesResult.Data == null ? null : messagesResult.Data.ToEntities(messageIdItemResult.Data.OperationalCurrency.ToString());
        }

        /// <summary>
        /// Gets a message's details from Matrix.
        /// </summary>
        /// <param name="id"> The message id.  </param>
        /// <param name="msgType"> The <see cref="EnumMessageTypes"/> </param>
        /// <returns>
        /// The <see cref="Entities.MessageDetail"/>.
        /// </returns>
        public MessageDetail GetMessageDetails(int id, EnumMessageTypes msgType)
        {
            if (!Enum.IsDefined(typeof(EnumMessageTypes), msgType))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.InvalidMessageType,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);
            var paymentService = new PaymentServiceBSL();
            paymentService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var messageIdItemResult = paymentService.GetMessageIdData(Guid.NewGuid().ToString(), centerId, true);
            ValidateResultCode(messageIdItemResult.ResultCode, messageIdItemResult.ErrorMessage);

            var messageDetailResult = paymentService.GetMessageDetails(Guid.NewGuid().ToString(), centerId, true, id, true, msgType.ToString().ParseEnum<MessageTypes>(), true);
            ValidateResultCode(messageDetailResult.ResultCode, messageDetailResult.ErrorMessage);

            if (messageDetailResult.Data == null)
            {
                throw CreateException(HttpStatusCode.NotFound, string.Format("Message with id '{0}' was not found", id), EnumErrorCodes.BusinessLayerError);
            }

            return messageDetailResult.Data.ToEntity(messageIdItemResult.Data.OperationalCurrency.ToString());
        }

        /// <summary>
        /// The get buckets.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        /// <param name="fields">
        /// The fields.
        /// </param>
        /// <returns>
        /// The <see cref="Entities.BucketItem"/>.
        /// </returns>
        public List<BucketItem> GetBuckets(BucketItemFilters filters, BucketExtraFields fields)
        {
            var buckets = new List<BucketItem>();

            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);
            var paymentsService = new PaymentServiceBSL();
            paymentsService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var bucketFilters = new BucketFilterItem();

            if (filters == null)
            {
                throw CreateException(
                  HttpStatusCode.BadRequest,
                  Resources.Business.MatrixManager.DateNotEntered,
                  EnumErrorCodes.BusinessLayerError);
            }

            if (filters.To == null && filters.From == null)
            {
                throw CreateException(
                   HttpStatusCode.BadRequest,
                   Resources.Business.MatrixManager.DateNotEntered,
                   EnumErrorCodes.BusinessLayerError);
            }

            if (filters.From > filters.To)
            {
                throw CreateException(
                   HttpStatusCode.BadRequest,
                   Resources.Business.MatrixManager.EndDateMustBeBigger,
                   EnumErrorCodes.BusinessLayerError);
            }

            bucketFilters.From = filters.From;
            bucketFilters.To = filters.To.HasValue ? filters.To.Value.AddDays(1).AddMilliseconds(-1) : (DateTime?)null;

            var operationsFilter = filters.Operation.HasValue
                                       ? filters.Operation.Value.ToValues().Select(filter => filter.ToString().ParseEnum<BankOperationCodes>())
                                                .ToList()
                                       : null;
            var statusesFilter = filters.Status.HasValue
                                     ? filters.Status.Value.ToValues().Select(filter => filter.ToString().ParseEnum<PaymentBucketStatuses>())
                                              .ToList()
                                     : null;
            var sourcesFilter = filters.Source.HasValue
                                    ? filters.Source.Value.ToValues().Select(
                                        filter => filter.ToString().ParseEnum<FiveDegrees.PaymentService.PaymentSources>()).ToList()
                                    : null;
            var typesFilter = filters.Type.HasValue
                                  ? filters.Type.Value.ToValues().Select(filter => filter.ToString().ParseEnum<PaymentBucketTypes>()).ToList()
                                  : null;

            var bucketItems = paymentsService.GetBucketsByFilter(Guid.NewGuid().ToString(), centerId, centerId != 0, bucketFilters);
            ValidateResultCode(bucketItems.ResultCode, bucketItems.ErrorMessage);

            if (!bucketItems.Data.IsNullOrEmpty())
            {
                buckets.AddRange(bucketItems.Data.Where(
                        x =>
                        (operationsFilter == null || operationsFilter.Contains(x.OperationCode))
                        && (statusesFilter == null || statusesFilter.Contains(x.GroupStatus))
                        && (sourcesFilter == null || sourcesFilter.Contains(x.Source))
                        && (typesFilter == null || typesFilter.Contains(x.BucketType))).ToEntities());

                var bucketIds = new List<string>();

                if (fields != null && fields.Fields.ToValues().Contains(EnumBucketExtraField.PaymentBucketDetails))
                {
                    bucketIds.AddRange(buckets.Select(bucket => bucket.Id));

                    bucketItems = paymentsService.GetBuckets(
                        Guid.NewGuid().ToString(),
                        centerId,
                        centerId != 0,
                        bucketIds.ToArray());

                    return bucketItems.Data.ToEntities();
                }
            }

            return buckets;
        }

        /// <summary>
        /// The get bucket.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Entities.BucketItem"/>.
        /// </returns>
        public Entities.BucketItem GetBucket(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.IdNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            int centerId = GetCenter(accessToken);
            var paymentsService = new PaymentServiceBSL();
            paymentsService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));
            var bucketItem = paymentsService.GetBucket(Guid.NewGuid().ToString(), centerId, centerId != 0, id);
            ValidateResultCode(bucketItem.ResultCode, bucketItem.ErrorMessage);

            if (bucketItem.Data == null)
            {
                throw CreateException(HttpStatusCode.NotFound, string.Format("Bucket with id '{0}' was not found", id), EnumErrorCodes.BusinessLayerError);
            }

            return bucketItem.Data.ToEntity();
        }

        /// <summary>
        /// the return payment.
        /// </summary>
        /// <param name="returnPayment">
        /// The return payment
        /// </param>
        /// <returns></returns>
        public bool ReturnPayment(ReturnPayment returnPayment)
        {

            if (returnPayment == null)
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.PaymentNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);
            var paymentsService = new PaymentServiceBSL();
            paymentsService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));
            var bucketItem = paymentsService.ReturnPayment(Guid.NewGuid().ToString(), centerId, centerId != 0, returnPayment.MovementId, returnPayment.MovementId != 0, returnPayment.SepaReturnCode.ParseEnum<SepaReturnCodes>(), returnPayment.SepaReturnCode!=null, returnPayment.Message);
            ValidateResultCode(bucketItem.ResultCode, bucketItem.ErrorMessage);

            return bucketItem.Data;
        }

        /// <summary>
        /// Creates a return outgoing bucket
        /// </summary>
        /// <param name="returnBucketItem">
        /// The return bucket
        /// </param>
        /// <returns></returns>
        public string CreateOutgoingReturnBucket(ReturnBucketItem returnBucketItem)
        {
            if (returnBucketItem == null)
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.NullReturnBucket,
                    EnumErrorCodes.BusinessLayerError);
            }

            var accessToken = GetToken().AccessToken;
            var centerId = GetCenter(accessToken);

            var paymentsService = new PaymentServiceBSL();
            paymentsService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var outgoingBucketItem = paymentsService.CreateOutgoingReturnBucket(Guid.NewGuid().ToString(), centerId, centerId != 0, returnBucketItem.ToMatrixModel());

            ValidateResultCode(outgoingBucketItem.ResultCode, outgoingBucketItem.ErrorMessage);

            return outgoingBucketItem.Data;
        }

        /// <summary>
        /// Retrieves a token
        /// </summary>
        /// <returns>
        /// The <see cref="TokenResponse"/>.
        /// </returns>
        private static TokenResponse GetToken()
        {
            bool ignoreSslErrors;
            bool.TryParse(ConfigurationManager.AppSettings[Constants.MatrixAuthIgnoreTokenSSL], out ignoreSslErrors);

            if (ignoreSslErrors)
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            }

            var oauth2Client = new TokenClient(ConfigurationManager.AppSettings[Constants.MatrixAuthServiceBaseUrl] + ConfigurationManager.AppSettings[Constants.MatrixAuthTokenEndpoint], ConfigurationManager.AppSettings[Constants.MatrixAuthClientId], ConfigurationManager.AppSettings[Constants.MatrixAuthClientSecret]);
            var tokenResponse = oauth2Client.RequestResourceOwnerPasswordAsync(
                ConfigurationManager.AppSettings[Constants.MatrixAuthUserName],
                ConfigurationManager.AppSettings[Constants.MatrixAuthPassword],
                ConfigurationManager.AppSettings[Constants.MatrixAuthMatrixScope]).Result;

            return tokenResponse;
        }

        /// <summary>
        /// The validate result code.
        /// </summary>
        /// <param name="resultCode">
        /// The result code.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        private void ValidateResultCode(int resultCode, string errorMessage)
        {
            if (resultCode != 0)
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    string.Format("ResultCode: {0}, Error Message: {1}", resultCode, errorMessage),
                    EnumErrorCodes.BusinessLayerError);
            }
        }

        /// <summary>
        /// The validate payment.
        /// </summary>
        /// <param name="payment"> The payment. </param>
        /// <param name="iban"> The iban. </param>
        private void ValidatePayment(Payment payment, string iban)
        {
            if (payment == null)
            {
                throw CreateException(
                    HttpStatusCode.BadRequest,
                    Resources.Business.MatrixManager.PaymentNotGiven,
                    EnumErrorCodes.BusinessLayerError);
            }

            if (iban != payment.SourceIBAN)
            {
                throw CreateException(
                   HttpStatusCode.BadRequest,
                   Resources.Business.MatrixManager.IbanNotConsistentWithSourceIban,
                   EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.SourceIBAN))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.IBANNotGiven,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.CounterpartyIBAN))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.CounterpartyIBANShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.CounterpartyBIC))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.CounterpartyBicShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (payment.Amount == 0)
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.AmountShouldNotBeZero,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (payment.ValueDate == null || payment.ValueDate.IsDefault())
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.NotValidValueDate,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (payment.DebtorDetails == null)
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.DebtorShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.DebtorDetails.Name))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.DebtorsNameShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.DebtorDetails.Street))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.DebtorsStreetShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.DebtorDetails.Postcode))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.DebtorsPostcodeShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.DebtorDetails.City))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.DebtorsCityShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (payment.CreditorDetails == null)
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.CreditorShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.CreditorDetails.Name))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.CreditorsNameShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.CreditorDetails.Street))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.CreditorsStreetShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.CreditorDetails.Postcode))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.CreditorsPostcodeShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }

            if (string.IsNullOrWhiteSpace(payment.CreditorDetails.City))
            {
                throw CreateException(
                      HttpStatusCode.BadRequest,
                      Resources.Business.MatrixManager.CreditorsCityShouldNotBeNull,
                      EnumErrorCodes.BusinessLayerError);
            }
        }

        /// <summary>
        /// Gets the center id
        /// </summary>
        /// <param name="accessToken"> The access token. </param>
        /// <returns>
        /// The center id
        /// </returns>
        private int GetCenter(string accessToken)
        {
            var centerName = ConfigurationManager.AppSettings[Constants.MatrixCenterName];
            var centerService = new CenterServiceFacade();
            centerService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));
            var centers = centerService.GetCenters(Guid.NewGuid().ToString());

            ValidateResultCode(centers.ResultCode, centers.ErrorMessage);

            var center = centers.Data.FirstOrDefault(c => c.Name.Equals(centerName, StringComparison.InvariantCultureIgnoreCase));

            if (center == null)
            {
                throw CreateException(
                    HttpStatusCode.InternalServerError,
                    Resources.Business.MatrixManager.CenterNameWasNotFound,
                    EnumErrorCodes.BusinessLayerError);
            }

            return center.CenterId;
        }

        /// <summary>
        /// Gets the savings product id.
        /// </summary>
        /// <param name="accessToken"> The access token. </param>
        /// <param name="centerId"> The center id. </param>
        /// <returns>
        /// The product id.
        /// </returns>
        private int GetSavingsProduct(string accessToken, int centerId)
        {
            var savingsProductName = ConfigurationManager.AppSettings[Constants.MatrixSavingsProductName];
            var productBuilder = new ProductBuilderBSL();
            productBuilder.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var products = productBuilder.GetProducts(Guid.NewGuid().ToString(), centerId, centerId != 0);

            ValidateResultCode(products.ResultCode, products.ErrorMessage);

            var product = products.Data.FirstOrDefault(p => p.Name.Equals(savingsProductName, StringComparison.InvariantCultureIgnoreCase));

            if (product == null)
            {
                throw CreateException(
                    HttpStatusCode.InternalServerError,
                    Resources.Business.MatrixManager.SavingsProductNotFound,
                    EnumErrorCodes.BusinessLayerError);
            }

            return product.Id;
        }

        /// <summary>
        /// Gets the department id
        /// </summary>
        /// <param name="accessToken"> The access token. </param>
        /// <param name="centerId"> The center id. </param>
        /// <param name="departmentName"> The department name. </param>
        /// <returns>
        /// The department id.
        /// </returns>
        private int GetDepartment(string accessToken, int centerId, string departmentName)
        {
            var centerService = new CenterServiceFacade();

            centerService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var departmentsResult = centerService.GetDepartments(Guid.NewGuid().ToString(), centerId, true);

            ValidateResultCode(departmentsResult.ResultCode, departmentsResult.ErrorMessage);

            var department = departmentsResult.Data.FirstOrDefault(x => x.Name.Equals(departmentName, StringComparison.InvariantCultureIgnoreCase));

            if (department == null)
            {
                throw CreateException(
                    HttpStatusCode.NotFound,
                    Resources.Business.MatrixManager.DepartmentWasNotFound,
                    EnumErrorCodes.BusinessLayerError);
            }

            return department.DepartmentId;
        }

        /// <summary>
        /// Gets the unit id
        /// </summary>
        /// <param name="accessToken"> The access token. </param>
        /// <param name="centerId"> The center id. </param>
        /// <param name="departmentId"> The department id. </param>
        /// <param name="unitName"> The unit name. </param>
        /// <returns>
        /// The unit id
        /// </returns>
        private int GetUnit(string accessToken, int centerId, int departmentId, string unitName)
        {
            var centerService = new CenterServiceFacade();

            centerService.SetRequestHeader("Authorization", string.Format("Bearer {0}", accessToken));

            var unitsResult = centerService.GetUnits(Guid.NewGuid().ToString(), centerId, true, departmentId, true);

            ValidateResultCode(unitsResult.ResultCode, unitsResult.ErrorMessage);

            var unit = unitsResult.Data.FirstOrDefault(x => x.Name.Equals(unitName, StringComparison.InvariantCultureIgnoreCase));

            if (unit == null)
            {
                throw CreateException(
                    HttpStatusCode.NotFound,
                    Resources.Business.MatrixManager.UnitWasNotFound,
                    EnumErrorCodes.BusinessLayerError);
            }

            return unit.UnitId;
        }

        /// <summary>
        /// Creates a custom business layer exception
        /// </summary>
        /// <param name="httpStatusCode"> The <see cref="HttpStatusCode"/> of the error  </param>
        /// <param name="message"> The error message  </param>
        /// <param name="code"> The error code.  </param>
        /// <param name="important"> The important. </param>
        /// <returns>
        /// The <see cref="BusinessLayerException"/>
        /// </returns>
        private BusinessLayerException CreateException(HttpStatusCode httpStatusCode, string message, EnumErrorCodes code, bool important = true)
        {
            var stackTrace = new StackTrace();

            // Get calling method name
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;

            return new BusinessLayerException(
                new Error()
                {
                    Code = (int)code,
                    Title = typeof(BusinessLayerException).FullName,
                    Source = string.Format(Resources.Common.ErrorSourceInfo, callingMethod, _typeName),
                    StatusCode = ((int)httpStatusCode).ToString(CultureInfo.InvariantCulture),
                    StatusCodeDescription = httpStatusCode.GetDescription(),
                    Message = message
                },
                _enumErrorCodesType,
                important);
        }

        /*  private  CheckForResult(op res)
        {
            if (check == true)
            {
                throw CreateException(
                    HttpStatusCode.InternalServerError,
                    string.Format("ResultCode: {0}, Error Message: {1}", res.ResultCode, res.ErrorMessage),
                    EnumErrorCodes.BusinessLayerError);
            }
        }*/
    }
}
