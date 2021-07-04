using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigProvider : IConfigProvider
    {
        public Uri ServerEndpoint => new Uri(GetConfigEntryAsString($"ServerEndpoint"));

        public string GetConfigEntryAsString(string key)
        {
            var s = ConfigurationManager.AppSettings.Get(key);

            if (s == null)
            {
                throw new ArgumentException($"Missing configuration app setting: {key}");
            }

            return s;
        }
    }
}
