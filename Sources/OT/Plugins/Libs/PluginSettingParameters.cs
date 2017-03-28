using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Security.OTP.Plugins
{
    public class PluginSettingParameters : IEnumerable
    {
        private KeyValuePair<string, string>[] _pluginSettingparams;


        public int Count
        {
            get { return _pluginSettingparams.Length; }
        }

        public KeyValuePair<string, string> this[int i]
        {
            get { return _pluginSettingparams[i]; }
        }

        public KeyValuePair<string, string> this[string key]
        {
            get { return _pluginSettingparams.FirstOrDefault(p => p.Key == key); }
        }


        internal PluginSettingParameters(IEnumerable<KeyValuePair<string, string>> pluginSettingparams)
        {
            _pluginSettingparams = pluginSettingparams.ToArray();
        }


        public IEnumerator GetEnumerator()
        {
            foreach (KeyValuePair<string, string> param in _pluginSettingparams)
            {
                yield return param;
            }
        }
    }
}
