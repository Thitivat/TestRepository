using Bndb.Kyc.SanctionLists.Parsers;
using Bndb.Kyc.SanctionLists.Parsers.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Bndb.Kyc.SanctionLists.Parsers.EuParserTest
{
    [TestClass]
    public class EuParserTest
    {
        #region [Fields]
        private string tempXml;
        private string tempXmlWrong;
        private string _globalDateTimeFormat="yyyy-MM-dd";

        #endregion

        [TestInitialize]
        public void Initialize()
        {
            tempXml = System.IO.Path.GetTempFileName();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Properties.Resources.global);
            xml.Save(tempXml);
            xml = null;

            tempXmlWrong = System.IO.Path.GetTempFileName();
            xml = new XmlDocument();
            xml.LoadXml(Properties.Resources.global_wrong_nodata);
            xml.Save(tempXmlWrong);
            xml = null;
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(tempXml))
            {
                File.Delete(tempXml);
            }

            if (File.Exists(tempXmlWrong))
            {
                File.Delete(tempXmlWrong);
            }
        }

        [TestMethod]
        public void Test_Parse_Success()
        {
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, tempXml);

            Assert.IsNotNull(parser);
            Assert.IsNotNull(parser.Entities);
            Assert.AreEqual(2440, parser.Entities.Count);
        }

        [TestMethod]
        public void Test_Parse_Success_Fsd()
        {
            string tempFsdXml = System.IO.Path.GetTempFileName();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Properties.Resources.global_fsd);
            xml.Save(tempFsdXml);
            xml = null;

            EuParser parser = EuParser.Parse(tempXml, tempFsdXml);

            PrivateObject po = new PrivateObject(parser);
            Assert.IsNotNull(po.GetField("_globalFsdXml"));
            Assert.IsNotNull(parser);
            Assert.IsNotNull(parser.Entities);
            Assert.AreEqual(2440, parser.Entities.Count);
        }

        [TestMethod]
        public void Test_Parse_Success_Xml()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Properties.Resources.global);
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, xml);
            PrivateObject po = new PrivateObject(parser);
            Assert.IsNotNull(po.GetField("_entities"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_Parse_Fail_NoData()
        {
            // if the xml file if wrong root format or didn't have data EuParser will throw an exception.            
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, tempXmlWrong);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Parse_Fail_NullXmlData()
        {
            XmlDocument doc = null;
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, doc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_GetElement_Exception()
        {
           // prepare data  
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, tempXml);
            string elementNode = "pdf_link";

            PrivateObject po = new PrivateObject(parser);

            XmlDocument globalXml = po.GetField("_globalXml") as XmlDocument;
            XmlNodeList entityList = globalXml.SelectNodes("/WHOLE/ENTITY");
            
            // call Private Method and inject data
            po.Invoke("GetElement", entityList.Item(0), elementNode, true);              
        }

        [TestMethod]
        public void Test_GetElement_Null()
        {
            // prepare data
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, tempXml);
            string elementNode = "";

            PrivateObject po = new PrivateObject(parser);

            XmlDocument globalXml = po.GetField("_globalXml") as XmlDocument;
            XmlNodeList entityList = globalXml.SelectNodes("/WHOLE/ENTITY");
            
            string result = null;

            // call Private Method and inject data
            result = po.Invoke("GetElement", entityList.Item(0), elementNode, false) as string;

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Parse_Exception()
        {
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, String.Empty);
        }

        [TestMethod]
        public void Test_Private_ExtractIdentificationFromTag_Success()
        {
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, tempXml);
            PrivateObject po = new PrivateObject(parser);
            string tagData = "<NUMBER>AD001095 (passport-National passport) ((passport))</NUMBER><NUMBER>AD001159 (other-Other identification number) </NUMBER><NUMBER>33301/862 (other-Other identification number) (issued: 17 october 1997 expires: 1 october 2005)</NUMBER><NUMBER>27082171 (passport-National passport) ((usa passport (issued 21.6.1992 in amman, jordan))(usa passport (issued 21.6.1992 in amman, jordan))passport no (usa) (issued 21.6.1992 in amman, jordan))</NUMBER><NUMBER>F654645 (passport-National passport) [known to be expired](passport issued on 30.4.2005, expired on 7.3.2010, issue date in hijri calendar 24/06/1426, expiry date in hijri calendar 21/03/1431)</NUMBER>";
            string number = string.Empty;
            string identificationType = string.Empty;

            // set argument with put parameter.
            var args = new object[] { tagData, number, identificationType };

            po.Invoke("ExtractIdentificationFromTag", args);

            Assert.AreNotEqual(String.Empty, args[1]);
            Assert.AreNotEqual(String.Empty, args[2]);
        }

        [TestMethod]
        public void Test_Private_ExtractIdentificationFromTag_Fail_NullTagData()
        {
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, tempXml);
            PrivateObject po = new PrivateObject(parser);

            string tagData = null;
            string number = string.Empty;
            string identificationType = string.Empty;

            // set argument with put parameter.
            var args = new object[] { tagData, number, identificationType };

            po.Invoke("ExtractIdentificationFromTag", args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Private_GetAttribute_Fail_RequireData()
        {
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, tempXml);
            string elementNode = "test";

            PrivateObject po = new PrivateObject(parser);

            XmlNodeList entityList = ((XmlDocument)po.GetField("_globalXml")).SelectNodes("/WHOLE/ENTITY");

            // call Private Method and inject data
            po.Invoke("GetAttribute", entityList.Item(0), elementNode, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Private_GetDateTimeUtc_Fail_WrongDateTimeFormat()
        {
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, tempXml);
            string dateTimeString = "test";
            string fieldName = "test";

            PrivateObject po = new PrivateObject(parser);

            // call Private Method and inject data
            po.Invoke("GetDateTimeUtc", dateTimeString, fieldName, true);
        }

        [TestMethod]
        public void Test_Private_GetDateTimeUtc_Fail_EmptyDateTimeFormat()
        {
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, tempXml);
            string dateTimeString = "";
            string fieldName = "test";

            PrivateObject po = new PrivateObject(parser);

            // call Private Method and inject data
            po.Invoke("GetDateTimeUtc", dateTimeString, fieldName, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Private_ParseContactInfo_Fail_NoNodeName()
        {
            EuParser parser = EuParser.Parse(_globalDateTimeFormat, tempXml);

            PrivateObject po = new PrivateObject(parser);

            XmlDocument globalXml = po.GetField("_globalXml") as XmlDocument;
            XmlNodeList entityList = globalXml.SelectNodes("/WHOLE/ENTITY");

            List<EuContactInfo> result = null;

            // call Private Method and inject data
            result = po.Invoke("ParseContactInfo", entityList.Item(0)) as List<EuContactInfo>;

        }

        //[TestMethod]
        //public void Test_Private_ParseContactInfo_Success()
        //{
        //    EuParser parser = EuParser.Parse(tempXml);

        //    PrivateObject po = new PrivateObject(parser);

        //    XmlDocument globalXml = po.GetField("_globalXml") as XmlDocument;
        //    XmlNodeList entityList = globalXml.SelectNodes("/WHOLE/ENTITY");

        //    List<EuContactInfo> result = null;

        //    // call Private Method and inject data
        //    result = po.Invoke("ParseContactInfo", entityList.Item(0)) as List<EuContactInfo>;

        //}
    }
}
