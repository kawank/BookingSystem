using log4net;
using log4net.Config;
using System;

namespace Logger
{
    public class Log
    {
        /// <summary>
        /// Enumeration used for Types of error
        /// </summary>
        public enum LogType
        {
            Info = 1,
            Debug = 2,
            Error = 3,
            Warn = 4,
            Fatal = 5,
        }

        private static readonly ILog logger = LogManager.GetLogger(typeof(Log));

        static Log()
        {
            XmlConfigurator.Configure();
           
        }

        /// <summary>
        ///  Function to show various types of error / debug messages
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="logType"></param>
        public static void LogMessage(string strMessage, LogType logType)
        {
            //LogMessage(strMessage);
            switch (logType)
            {
                case LogType.Debug:
                    logger.Debug(strMessage);
                    break;
                case LogType.Info:
                    logger.Info(strMessage);
                    break;
                case LogType.Error:
                    logger.Error(strMessage);
                    break;
                case LogType.Fatal:
                    logger.Fatal(strMessage);
                    break;
                case LogType.Warn:
                    logger.Warn(strMessage);
                    break;
                default:
                    break;
            }
        }
    }
}
