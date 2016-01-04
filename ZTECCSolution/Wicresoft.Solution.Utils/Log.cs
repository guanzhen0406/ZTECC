using NLog;
using System;

namespace Wicresoft.Solution.Utils
{
    public class Log
    {
        private static NLog.Logger dbLogger;
        private static NLog.Logger fileLogger;

        static Log()
        {
            InitLogger();
            LogManager.EnableLogging();
        }

        private static void InitLogger()
        {
            fileLogger = LogManager.GetLogger("FileLogger");
            dbLogger = LogManager.GetLogger("DBLogger");
        }

        public static void Info(string message)
        {
            WriteLog(LogLevel.Info, message, null);
        }

        public static void Info(string message, string url)
        {
            WriteUrlLog(LogLevel.Info, message, null, url);
        }

        public static void Warn(string message)
        {
            WriteLog(LogLevel.Warn, message, null);
        }

        public static void Warn(string message, string url)
        {
            WriteUrlLog(LogLevel.Warn, message, null, url);
        }

        public static void Debug(string message)
        {
            WriteLog(LogLevel.Debug, message, null);
        }

        public static void Debug(string message, string url)
        {
            WriteUrlLog(LogLevel.Debug, message, null, url);
        }

        public static void Error(string message)
        {
            WriteLog(LogLevel.Error, message, null);
        }

        public static void Error(Exception ex)
        {
            var innerEx = ex.InnerException ?? ex;
            WriteLog(LogLevel.Error, innerEx.Message, innerEx);
        }

        public static void Error(Exception ex, string url)
        {
            WriteUrlLog(LogLevel.Error, ex.Message, ex, url);
        }

        private static void WriteUrlLog(LogLevel level, string message, Exception ex, string url)
        {
            var list = new NlogVariable[1]
            {
                new NlogVariable("url",url)
            };

            WriteLog(level, message, ex, list);
        }

        private static void WriteLog(LogLevel level, string message, Exception ex, params NlogVariable[] customvariables)
        {
            var dblog = LogEventInfo.Create(level, dbLogger.Name, message, ex);
            var filelog = LogEventInfo.Create(level, fileLogger.Name, message, ex);
            if (customvariables != null)
            {
                foreach (var item in customvariables)
                {
                    dblog.Properties[item.VariableName] = item.Value;
                }
                foreach (var item in customvariables)
                {
                    filelog.Properties[item.VariableName] = item.Value;
                }
            }
            dbLogger.Log(dblog);
            fileLogger.Log(filelog);
        }
    }

    internal struct NlogVariable
    {
        public NlogVariable(string _VariableName, string _Value)
        {
            this.VariableName = _VariableName;
            this.Value = _Value;
        }
        public string VariableName;
        public string Value;
    }
}
