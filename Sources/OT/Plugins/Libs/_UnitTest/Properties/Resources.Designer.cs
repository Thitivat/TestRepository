﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BND.Services.Security.OTP.Plugins.LibsTest.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BND.Services.Security.OTP.Plugins.LibsTest.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to &lt;plugin-settings&gt;
        ///	&lt;plugin-setting name=&quot;EMAIL&quot; version=&quot;1.0.0.0&quot;&gt;
        ///		&lt;parameters&gt;
        ///			&lt;parameter key=&quot;ComponentCode&quot; value=&quot;BRANDNMAILQ_CZKwC83c5P3M&quot; /&gt;
        ///			&lt;parameter key=&quot;HtmlToText&quot; value=&quot;BRANDNHtmlToXml_eruWqpbOZD7k&quot; /&gt;
        ///			&lt;parameter key=&quot;EmailServer&quot; value=&quot;smtp.gmail.com&quot; /&gt;
        ///			&lt;parameter key=&quot;EmailSmtpPort&quot; value=&quot;465&quot; /&gt;
        ///			&lt;parameter key=&quot;EmailAccount&quot; value=&quot;kobkiat.peace@gmail.com&quot; /&gt;
        ///			&lt;parameter key=&quot;EmailPassword&quot; value=&quot;qwerty@12345&quot; /&gt;
        ///			&lt;parameter key=&quot;EmailSubject&quot; value=&quot;This is [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string PluginSettings {
            get {
                return ResourceManager.GetString("PluginSettings", resourceCulture);
            }
        }
    }
}
