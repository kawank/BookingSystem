using System;
using System.Configuration;
using System.Net;
using System.Xml;

namespace Utility
{
    public class AppSettings
    {
        # region Connection String
        /// <summary>
        /// Property to get the connection string
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string result = string.Empty;
                if (ConfigurationManager.ConnectionStrings[ConfigKeys.CONNECTION_STRING].ToString() != null)
                { result = ConfigurationManager.ConnectionStrings[ConfigKeys.CONNECTION_STRING].ToString(); }

                return result;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string FromEmailAddress()
        {
            string result = string.Empty;
            if (ConfigurationManager.AppSettings[ConfigKeys.FROM_EMAIL_ADDRESS].ToString() != null)
            { result = ConfigurationManager.AppSettings[ConfigKeys.FROM_EMAIL_ADDRESS].ToString(); }

            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CCTO()
        {
            string result = string.Empty;
            if (ConfigurationManager.AppSettings[ConfigKeys.CC_TO].ToString() != null)
            { result = ConfigurationManager.AppSettings[ConfigKeys.CC_TO].ToString(); }

            return result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Boolean EnableSSL()
        {
            Boolean result = false;
            if (ConfigurationManager.AppSettings[ConfigKeys.ENABLE_SSL].ToString() != null)
            { result = ConfigurationManager.AppSettings[ConfigKeys.ENABLE_SSL].ToBooleanSafe(); }

            return result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string FromDisplay()
        {
            string result = string.Empty;
            if (ConfigurationManager.AppSettings[ConfigKeys.FROM_DISPLAY].ToString() != null)
            { result = ConfigurationManager.AppSettings[ConfigKeys.FROM_DISPLAY].ToString(); }

            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string MailPassword()
        {
            string result = string.Empty;
            if (ConfigurationManager.AppSettings[ConfigKeys.MAIL_PASSWORD].ToString() != null)
            { result = ConfigurationManager.AppSettings[ConfigKeys.MAIL_PASSWORD].ToString(); }

            return result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string MailUserName()
        {
            string result = string.Empty;
            if (ConfigurationManager.AppSettings[ConfigKeys.MAIL_USER_NAME].ToString() != null)
            { result = ConfigurationManager.AppSettings[ConfigKeys.MAIL_USER_NAME].ToString(); }

            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static short Port()
        {
            short result = 25;
            if (ConfigurationManager.AppSettings[ConfigKeys.PORT].ToString() != null)
            { result = ConfigurationManager.AppSettings[ConfigKeys.PORT].ToShortSafe(); }

            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SmtpServerName()
        {
            string result = string.Empty;
            if (ConfigurationManager.AppSettings[ConfigKeys.SMTP_SERVER_NAME].ToString() != null)
            { result = ConfigurationManager.AppSettings[ConfigKeys.SMTP_SERVER_NAME].ToString(); }

            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int NoOfDaysDisplayed()
        {
            string result = string.Empty;
            if (ConfigurationManager.AppSettings[ConfigKeys.NO_OF_DAYS_DISPLAYED].ToString() != null)
            { result = ConfigurationManager.AppSettings[ConfigKeys.NO_OF_DAYS_DISPLAYED].ToString(); }

            return result.ToIntSafe();

        }

    }
}
