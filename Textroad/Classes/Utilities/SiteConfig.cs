using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes.Utilities
{
    public class SiteConfig
    {
        private static string _ApiUrl;
        public static string ApiUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_ApiUrl))
                    _ApiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
                return _ApiUrl;
            }
        }

        public static string SecurityKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SecurityKey"];
            }
        }
        public static string ImportFilesPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ImportFilesPath"];
            }
        }

        public static string UploadFilesPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UploadFilesPath"];
            }
        }
    }
}