
using eMandates.Merchant.Library;
using NUnit.Framework;

namespace BND.Services.Payments.eMandates.UnitTests.Business
{
    [TestFixture]
    public class EMandatesClientUnitTest
    {
        #region [GetDirectory]
        [Test]
        public void GetDirectories()
        {
        }
        #endregion [GetDirectory]

        #region [CreateNewTransaction]
        [Test]
        public void SendTransactionRequest()
        {
            CoreCommunicator cc = new CoreCommunicator(ConfigurationFactory.GetCoreCommunicator());

            //CoreCommunicator cc = new CoreCommunicator(ConfigurationFactory.GetCoreCommunicator());

            NewMandateRequest nmr = new NewMandateRequest();

            nmr.DebtorBankId = "ABNANL2A";
            nmr.DebtorReference = "567890"; // nullable
            nmr.EMandateId = "4567890";
            nmr.EMandateReason = "My reason lol"; // nullable
            nmr.EntranceCode = "34567890";
            nmr.ExpirationPeriod = null;
            nmr.Language = "nl";
            nmr.MaxAmount = null;
            nmr.MessageId = "23456789";
            nmr.PurchaseId = "1234567890"; // nullable
            nmr.SequenceType = SequenceType.Ooff;


            var resp = cc.NewMandate(nmr);
        }
        #endregion [CreateNewTransaction]
    }
}
