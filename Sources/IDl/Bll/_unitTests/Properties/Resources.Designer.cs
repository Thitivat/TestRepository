﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BND.Services.Payments.iDeal.Bll.Tests.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BND.Services.Payments.iDeal.Bll.Tests.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://abnamro-test.ideal-payment.de/ideal/iDEALv3.
        /// </summary>
        internal static string ACQUIRER_URL {
            get {
                return ResourceManager.GetString("ACQUIRER_URL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;DirectoryRes xmlns=&quot;http://www.idealdesk.com/ideal/messages/mer-acq/3.3.1&quot;
        ///version=&quot;3.3.1&quot;&gt;
        ///  &lt;createDateTimestamp&gt;2008-11-14T09:30:47.0Z&lt;/createDateTimestamp&gt;
        ///  &lt;Acquirer&gt;
        ///    &lt;acquirerID&gt;0001&lt;/acquirerID&gt;
        ///  &lt;/Acquirer&gt;
        ///  &lt;Directory&gt;
        ///    &lt;directoryDateTimestamp&gt;2004-11-10T10:15:12.145Z&lt;/directoryDateTimestamp&gt;
        ///    &lt;Country&gt;
        ///      &lt;countryNames&gt;Nederland&lt;/countryNames&gt;
        ///      &lt;Issuer&gt;
        ///        &lt;issuerID&gt;ABNANL2AXXX&lt;/issuerID&gt;
        ///        &lt;issuerName&gt;ABN AMRO Ba [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DirectoryResponse {
            get {
                return ResourceManager.GetString("DirectoryResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;AcquirerErrorRes xmlns=&quot;http://www.idealdesk.com/ideal/messages/mer-acq/3.3.1&quot;
        ///version=&quot;3.3.1&quot;&gt;
        ///  &lt;createDateTimestamp&gt;2008-11-14T09:30:47.0Z&lt;/createDateTimestamp&gt;
        ///  &lt;Error&gt;
        ///    &lt;errorCode&gt;SO1100&lt;/errorCode&gt;
        ///    &lt;errorMessage&gt;Issuer not available&lt;/errorMessage&gt;
        ///    &lt;errorDetail&gt;System generating error: Rabobank&lt;/errorDetail&gt;
        ///    &lt;consumerMessage&gt;
        ///      De geselecteerde iDEAL bank is momenteel niet beschikbaar i.v.m. onderhoud tot naar verwachting 31-12-2010 0 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ErrorResponse {
            get {
                return ResourceManager.GetString("ErrorResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;AcquirerStatusRes
        ///    xmlns=&quot;http://www.idealdesk.com/ideal/messages/mer-acq/3.3.1&quot;
        ///    xmlns:ns2=&quot;http://www.w3.org/2000/09/xmldsig#&quot; version=&quot;3.3.1&quot;&gt;
        ///  &lt;createDateTimestamp&gt;2016-05-19T10:42:37.623Z&lt;/createDateTimestamp&gt;
        ///  &lt;Acquirer&gt;
        ///    &lt;acquirerID&gt;0030&lt;/acquirerID&gt;
        ///  &lt;/Acquirer&gt;
        ///  &lt;Transaction&gt;
        ///    &lt;transactionID&gt;0030000041871818&lt;/transactionID&gt;
        ///    &lt;status&gt;Cancelled&lt;/status&gt;
        ///    &lt;statusDateTimestamp&gt;2016-05-19T12:42:37.597Z&lt;/statusDateTimestamp&gt;
        ///    &lt;c [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StatusFailResponse {
            get {
                return ResourceManager.GetString("StatusFailResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;AcquirerStatusRes
        ///    xmlns=&quot;http://www.idealdesk.com/ideal/messages/mer-acq/3.3.1&quot;
        ///    xmlns:ns2=&quot;http://www.w3.org/2000/09/xmldsig#&quot; version=&quot;3.3.1&quot;&gt;
        ///  &lt;createDateTimestamp&gt;2016-05-19T10:42:37.623Z&lt;/createDateTimestamp&gt;
        ///  &lt;Acquirer&gt;
        ///    &lt;acquirerID&gt;0030&lt;/acquirerID&gt;
        ///  &lt;/Acquirer&gt;
        ///  &lt;Transaction&gt;
        ///    &lt;transactionID&gt;0030000041871818&lt;/transactionID&gt;
        ///    &lt;status&gt;Open&lt;/status&gt;
        ///    &lt;statusDateTimestamp&gt;2016-05-19T12:42:37.597Z&lt;/statusDateTimestamp&gt;
        ///    &lt;consum [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StatusOpenResponse {
            get {
                return ResourceManager.GetString("StatusOpenResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;AcquirerStatusRes
        ///    xmlns=&quot;http://www.idealdesk.com/ideal/messages/mer-acq/3.3.1&quot;
        ///    xmlns:ns2=&quot;http://www.w3.org/2000/09/xmldsig#&quot; version=&quot;3.3.1&quot;&gt;
        ///  &lt;createDateTimestamp&gt;2016-05-19T10:42:37.623Z&lt;/createDateTimestamp&gt;
        ///  &lt;Acquirer&gt;
        ///    &lt;acquirerID&gt;0030&lt;/acquirerID&gt;
        ///  &lt;/Acquirer&gt;
        ///  &lt;Transaction&gt;
        ///    &lt;transactionID&gt;0030000041871818&lt;/transactionID&gt;
        ///    &lt;status&gt;Success&lt;/status&gt;
        ///    &lt;statusDateTimestamp&gt;2016-05-19T12:42:37.597Z&lt;/statusDateTimestamp&gt;
        ///    &lt;con [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string StatusResponse {
            get {
                return ResourceManager.GetString("StatusResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;AcquirerTrxRes xmlns=&quot;http://www.idealdesk.com/ideal/messages/mer-acq/3.3.1&quot; xmlns:ns2=&quot;http://www.w3.org/2000/09/xmldsig#&quot; version=&quot;3.3.1&quot;&gt;
        ///    &lt;createDateTimestamp&gt;2016-05-16T08:08:16.158Z&lt;/createDateTimestamp&gt;
        ///    &lt;Acquirer&gt;
        ///        &lt;acquirerID&gt;0030&lt;/acquirerID&gt;
        ///    &lt;/Acquirer&gt;
        ///    &lt;Issuer&gt;
        ///        &lt;issuerAuthenticationURL&gt;https://abnamro-test.ideal-payment.de/ideal/issuerSim.do?trxid=0030000041743099&amp;amp;ideal=prob&lt;/issuerAuthenticationURL&gt;
        ///    &lt;/Issuer&gt;
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TransactionResponse {
            get {
                return ResourceManager.GetString("TransactionResponse", resourceCulture);
            }
        }
    }
}
