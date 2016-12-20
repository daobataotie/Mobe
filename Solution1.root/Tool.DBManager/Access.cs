using System;
using System.Collections.Generic;
using System.Text;

namespace Tool.DBManager
{
    public class Access : DBMS
    {
        public override string Name
        {
            get
            {
                return "Access";
            }
        }

        public override string Description
        {
            get
            {
                return "";
            }
        }

        public override void CreateDatabase(System.Collections.Hashtable parameters)
        {
            string backupfile = System.Windows.Forms.Application.StartupPath + System.Configuration.ConfigurationManager.AppSettings["AccessDatabaseBackupFileName"];
            string databasefile = (string)parameters["databasefile"];
            string name = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (name)
            {
                case "zh-CN":
                    backupfile = backupfile + @"_zh_CN";
                    break;
                case "zh-TW":
                    backupfile = backupfile + @"_zh_TW";
                    break;
                default:
                    break;
            }
            System.IO.File.Copy(backupfile, databasefile);
        }
    }
}
