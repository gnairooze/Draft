using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DemoAuth.WWW.Helper
{
    public class Config
    {
        public static string AuthenticationType { get; set; }
        public static string Impersonate { get; set; }
        static Config()
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var section = config.GetSection("system.web/authentication");
            AuthenticationType = section.ElementInformation.Properties["mode"].Value.ToString();
            section = config.GetSection("system.web/identity");
            Impersonate = section.ElementInformation.Properties["impersonate"].Value.ToString();
        }
    }
}