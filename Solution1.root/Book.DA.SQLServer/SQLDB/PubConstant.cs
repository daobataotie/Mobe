using System;
using System.Configuration;
using Book.DA.SQLServer;
namespace Book.DA.SQLServer.SQLDB
{
    
    public class PubConstant
    {        
        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        public static string ConnectionString
        {           
            get 
            {
               //string a= Accessor.sqlmapper.DataSource.ConnectionString;           
               // string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];       
               // string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
               // if (ConStringEncrypt == "true")
               // {
               //     _connectionString = DESEncrypt.Decrypt(_connectionString);
               // }
               // return _connectionString;
                return Accessor.sqlmapper.DataSource.ConnectionString; 
            }
        }

        /// <summary>
        /// �õ�web.config������������ݿ������ַ�����
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }


    }
}
