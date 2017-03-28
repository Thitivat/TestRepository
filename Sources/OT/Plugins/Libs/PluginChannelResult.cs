using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BND.Services.Security.OTP.Plugins
{
    /// <summary>
    /// Class PluginChannelResult.
    /// </summary>
    public class PluginChannelResult
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets the name of the plugin.
        /// </summary>
        /// <value>The name of the plugin.</value>
        public string PluginName { get; private set; }

        /// <summary>
        /// Gets or sets the success.
        /// </summary>
        /// <value>The success.</value>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public List<PluginChannelMessage> Messages { get; private set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginChannelResult"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="messages">The message.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        public PluginChannelResult(bool success, List<PluginChannelMessage> messages)
        {
            // Get plugin name from 
            StackFrame frame = new StackFrame(1);
            var declaringType = frame.GetMethod().DeclaringType;
            if (declaringType != null)
            {
                ChannelExportAttribute attribute = (ChannelExportAttribute)
                    (declaringType.GetCustomAttributes(typeof(ChannelExportAttribute), false).FirstOrDefault());

                if (attribute == null)
                {
                    throw new InvalidOperationException(Properties.Resources.Class_No_Attribute);
                }

                // Assign property value.
                PluginName = attribute.Name;
            }
            Success = success;
            Messages = messages;
        }
        #endregion
    }
}
