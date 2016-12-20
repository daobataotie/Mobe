using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Book.DA.SQLServer.SQLDB
{

    /// <summary>
    /// SqlHelper����ר���ṩ������û����ڸ����ܡ��������������ϰ��sql���ݲ���
    /// </summary>
    public abstract class SqlHelper
    {

        //���ݿ������ַ���
        public static readonly string ConnectionStringLocalTransaction = PubConstant.ConnectionString;// ConfigurationManager.ConnectionStrings["SQLConnString1"].ConnectionString;
        //public static readonly string ConnectionStringInventoryDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString2"].ConnectionString;
        //public static readonly string ConnectionStringOrderDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString3"].ConnectionString;
        //public static readonly string ConnectionStringProfile = ConfigurationManager.ConnectionStrings["SQLProfileConnString"].ConnectionString;

        // ���ڻ��������HASH��
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        ///  �������ӵ����ݿ��ü������ִ��һ��sql������������ݼ���
        /// </summary>
        /// <param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>ִ��������Ӱ�������</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// �����е����ݿ�����ִ��һ��sql������������ݼ���
        /// </summary>
        /// <param name="conn">һ�����е����ݿ�����</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>ִ��������Ӱ�������</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        ///ʹ�����е�SQL����ִ��һ��sql������������ݼ���
        /// </summary>
        /// <remarks>
        ///����:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">һ�����е�����</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>ִ��������Ӱ�������</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ��ִ�е����ݿ�����ִ��һ���������ݼ���sql����
        /// </summary>
        /// <remarks>
        /// ����:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>��������Ķ�ȡ��</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //����һ��SqlCommand����
            SqlCommand cmd = new SqlCommand();
            //����һ��SqlConnection����
            SqlConnection conn = new SqlConnection(connectionString);

            //������������һ��try/catch�ṹִ��sql�ı�����/�洢���̣���Ϊ��������������һ���쳣����Ҫ�ر����ӣ���Ϊû�ж�ȡ�����ڣ�
            //���commandBehaviour.CloseConnection �Ͳ���ִ��
            try
            {
                //���� PrepareCommand �������� SqlCommand �������ò���
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //���� SqlCommand  �� ExecuteReader ����
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //�������
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //�ر����ӣ��׳��쳣
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// ��ָ�������ݿ������ַ���ִ��һ���������һ�����ݼ��ĵ�һ��
        /// </summary>
        /// <remarks>
        ///����:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        ///<param name="connectionString">һ����Ч�������ַ���</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>�� Convert.To{Type}������ת��Ϊ��Ҫ�� </returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// ��ָ�������ݿ�����ִ��һ���������һ�����ݼ��ĵ�һ��
        /// </summary>
        /// <remarks>
        /// ����:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">һ�����ڵ����ݿ�����</param>
        /// <param name="commandType">��������(�洢����, �ı�, �ȵ�)</param>
        /// <param name="commandText">�洢�������ƻ���sql�������</param>
        /// <param name="commandParameters">ִ���������ò����ļ���</param>
        /// <returns>�� Convert.To{Type}������ת��Ϊ��Ҫ�� </returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ������������ӵ�����
        /// </summary>
        /// <param name="cacheKey">��ӵ�����ı���</param>
        /// <param name="cmdParms">һ����Ҫ��ӵ������sql��������</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// �һỺ���������
        /// </summary>
        /// <param name="cacheKey">�����һز����Ĺؼ���</param>
        /// <returns>����Ĳ�������</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// ׼��ִ��һ������
        /// </summary>
        /// <param name="cmd">sql����</param>
        /// <param name="conn">Sql����</param>
        /// <param name="trans">Sql����</param>
        /// <param name="cmdType">������������ �洢���̻����ı�</param>
        /// <param name="cmdText">�����ı�,���磺Select * from Products</param>
        /// <param name="cmdParms">ִ������Ĳ���</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        /// <summary>
        /// �������뷽��.
        /// </summary>
        /// <param name="objTarget">Ҫ������double��������</param>
        /// <param name="mIndex">������С��λ��</param>
        /// <returns></returns>
        public static double GetSiSheWuRu(double objTarget, int mIndex)
        {
            double a1 = Math.Pow(10, mIndex);
            double a2 = Math.Pow(10, mIndex + 1);
            double b1 = Math.Truncate(objTarget * a1);
            double b2 = Math.Truncate(objTarget * a2);
            if (b2 % 10 >= 5)
            {
                return (b1 + 1) / a1;
            }
            else
            {
                return b1 / a1;
            }
        }

        /// <summary>
        /// �������뷽��.
        /// </summary>
        /// <param name="objTarget">Ҫ������Decimal��������</param>
        /// <param name="mIndex">������С��λ��</param>
        /// <returns></returns>
        public static decimal GetSiSheWuRu(decimal objTarget, int mIndex)
        {
            decimal a1 = decimal.Parse(Math.Pow(10, mIndex).ToString());
            decimal a2 = decimal.Parse(Math.Pow(10, mIndex + 1).ToString());
            decimal b1 = Math.Truncate(objTarget * a1);
            decimal b2 = Math.Truncate(objTarget * a2);
            if (b2 % 10 >= 5)
            {
                return (b1 + 1) / a1;
            }
            else
            {
                return b1 / a1;
            }
        }

    }
}