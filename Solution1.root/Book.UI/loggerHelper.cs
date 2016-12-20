using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;

namespace Book.UI
{
    public static class loggerHelper
    {
        public const string LoggerName = "loggerHelper";
        public const int EmptyFunctionKey = -1;
        public const int AnonymouseUserKey = -1;
        public static readonly ILog Logger = log4net.LogManager.GetLogger(LoggerName);

        public static void AddError(string errorMessage)
        {
            Logger.Error(errorMessage);
        }

        public static void AddError(string errorMessage, Exception ex)
        {
            Logger.Error(errorMessage + System.Environment.NewLine + ex.Message + System.Environment.NewLine + ex.ToString());
        }

        public static void AddWarn(string warnMessage)
        {
            Logger.Warn(warnMessage);
        }

        public static void AddWarn(string warnMessage, Exception ex)
        {
            Logger.Warn(warnMessage + System.Environment.NewLine + ex.Message + System.Environment.NewLine + ex.ToString());
        }

        public static void AddInfo(string infoMessage)
        {
            Logger.Info(infoMessage);
        }

        public static void AddInfo(string infoMessage, Exception ex) { Logger.Info(infoMessage + System.Environment.NewLine + ex.Message + System.Environment.NewLine + ex.ToString()); }

        public static void AddDebug(string debugMessage) { Logger.Debug(debugMessage); }

        public static void AddDebug(string debugMessage, Exception ex) { Logger.Debug(debugMessage + System.Environment.NewLine + ex.Message + System.Environment.NewLine + ex.ToString()); }

        public static void AddDatabaseError(string objectName, DatabaseAction dbAction, Exception ex)
        {
            string logMessage = string.Empty;
            switch (dbAction)
            {
                case DatabaseAction.Select:
                    logMessage = "Error occurred when retrieve data from table " + objectName;
                    break;
                case DatabaseAction.Insert:
                    logMessage = "Error occurred when insert data to table " + objectName;
                    break;
                case DatabaseAction.Update:
                    logMessage = "Error occurred when update data to table " + objectName;
                    break;
                case DatabaseAction.Delete:
                    logMessage = "Error occurred when delete data from table " + objectName;
                    break;
            }
            AddError(logMessage, ex);
        }

    }

    public enum DatabaseAction 
    { 
        Select, 
        Insert, 
        Update, 
        Delete
    }
}
