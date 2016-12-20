using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Tool.DBManager
{
    public class SQLServer : DBMS
    {
        public override string Name
        {
            get 
            {
                return "SQL Server";
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
            string datasource = ((string)parameters["datasource"]).Replace("0,","");
            SQLServerAuthentication authentication = (SQLServerAuthentication)parameters["authentication"];
            string username = (string)parameters["username"];
            string password = (string)parameters["password"];
            string databasename = (string)parameters["databasename"];            
            string datafilelocation = (string)parameters["datafilelocation"];
            string logfilelocation = (string)parameters["logfilelocation"];
            string datafilelogicalname = System.Configuration.ConfigurationManager.AppSettings["SQLServerDatabaseDataFileLogicalName"];
            string logfilelogicalname = System.Configuration.ConfigurationManager.AppSettings["SQLServerDatabaseLogFileLogicalName"];
            string backupfile = System.Windows.Forms.Application.StartupPath + System.Configuration.ConfigurationManager.AppSettings["SQLServerDatabaseBackupFileName"];
            
            string name = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (name)
            {
                case "zh-CN":
                    backupfile = backupfile + @"_zh_CN";
                    logfilelogicalname = datafilelogicalname + @"_CN_log";
                    datafilelogicalname = datafilelogicalname + @"_CN";
                    
                    break;
                case "zh-TW":
                    backupfile = backupfile + @"_zh_TW";
                    logfilelogicalname = datafilelogicalname + @"_TW_log";
                    datafilelogicalname = datafilelogicalname + @"_TW";
                    break;
                default:
                    break;
            }

            if (datafilelocation.EndsWith(@"\"))
            {
                datafilelocation = datafilelocation.Substring(0, datafilelocation.Length - 1);
            }
            if (logfilelocation.EndsWith(@"\"))
            {
                logfilelocation = logfilelocation.Substring(0, logfilelocation.Length - 1);
            }

            // 数据库名称合法性检查



            SqlConnection conn = new SqlConnection(BuildConnectionString(datasource, authentication, username, password, "master"));
            SqlCommand sc = new SqlCommand();

            sc.Connection = conn;
            sc.CommandType = CommandType.Text;

            // 检测同名数据库是否已存在
            int count = 0;
            sc.CommandText = string.Format(@"select count(*) from sysdatabases where name=@name");
            sc.Parameters.Add("@name", SqlDbType.NVarChar, 128).Value = databasename;
            try
            {
                conn.Open();
                count = (int)sc.ExecuteScalar();
            }
            finally
            {
                conn.Close();
            }
            if (count > 0)
                throw new InvalidOperationException(Properties.Resources.SameDataBaseExists);

            // 恢复数据库            
            sc.CommandText = string.Format(@"restore database {0} from disk='{1}' with move '{2}' to '{3}\{0}.mdf' ,move '{4}' to '{5}\{0}.ldf'", databasename, backupfile, datafilelogicalname, datafilelocation, logfilelogicalname, logfilelocation);
            try
            {
                conn.Open();
                sc.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }

        }


        static string BuildConnectionString(string datasource, SQLServerAuthentication authentication, string username, string password, string databasename)
        {
            string result;

            if (authentication == SQLServerAuthentication.SQLServer)
                result = string.Format("data source={0};user id={1};password={2};initial catalog={3}", datasource, username, password, databasename);
            else
                result = string.Format("data source={0};integrated security=sspi;initial catalog={1}", datasource, databasename);

            return result;
        }
    }
}
