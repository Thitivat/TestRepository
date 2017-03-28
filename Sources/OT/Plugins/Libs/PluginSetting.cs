using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BND.Services.Security.OTP.Plugins
{
    public class PluginSetting
    {
        public string Name { get; private set; }
        public string Version { get; private set; }
        public PluginSettingParameters Parameters { get; private set; }

        public PluginSetting(string pluginFilePath, string channelName)
        {
            if (String.IsNullOrEmpty(pluginFilePath))
            {
                throw new ArgumentNullException("pluginFilePath");
            }
            if (String.IsNullOrEmpty(channelName))
            {
                throw new ArgumentNullException("channelName");
            }

            if (!File.Exists(pluginFilePath))
            {
                throw new FileNotFoundException(Properties.Resources.ErrorSettingNotFound, pluginFilePath);
            }

            var settingXml = new XmlDocument();

            try
            {
                settingXml.Load(pluginFilePath);
            }
            catch (XmlException ex)
            {
                throw new FormatException(Properties.Resources.ErrorSettingNotXml, ex);
            }

            XmlNode settingNode = settingXml.SelectSingleNode(String.Format(Properties.Resources.XpathRoot, channelName));
            if (settingNode == null)
            {
                throw new FormatException(String.Format(Properties.Resources.ErrorSettingChannelNotFound, channelName));
            }

            Name = settingNode.Attributes[Properties.Resources.AttrName].Value;
            Version = (settingNode.Attributes[Properties.Resources.AttrVersion] == null)
                      ? null
                      : settingNode.Attributes[Properties.Resources.AttrVersion].Value;

            XmlNode paramsNode = settingNode.SelectSingleNode(Properties.Resources.TagParameters);
            if (paramsNode == null)
            {
                throw new FormatException(Properties.Resources.ErrorTagParametersNotFound);
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var xmlNodeList = paramsNode.SelectNodes(Properties.Resources.TagParameter);
            if (xmlNodeList != null)
                foreach (XmlNode param in xmlNodeList)
                {
                    if (param.Attributes[Properties.Resources.AttrKey] == null || param.Attributes[Properties.Resources.AttrValue] == null)
                    {
                        throw new FormatException(Properties.Resources.ErrorAttrKeyValueNotFound);
                    }

                    try
                    {
                        parameters.Add(
                            param.Attributes[Properties.Resources.AttrKey].Value,
                            param.Attributes[Properties.Resources.AttrValue].Value
                            );
                    }
                    catch (ArgumentException)
                    {
                        throw new FormatException(
                            String.Format(Properties.Resources.ErrorAttrKeyDuplicate,param.Attributes[Properties.Resources.AttrKey].Value)
                            );
                    }
                }

            Parameters = new PluginSettingParameters(parameters);
        }
    }
}
