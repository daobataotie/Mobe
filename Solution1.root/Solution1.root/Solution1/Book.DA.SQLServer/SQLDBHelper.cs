using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Data.Common;

//using installDB;

using Microsoft.Win32;
using System.Windows.Forms;
namespace Book.DA.SQLServer
{
    public static class SQLDBHelper 
    {
        public static  SqlConnection conn = null;
        private static String GetConnStr()
        {
            //return installDB.installdb.ConString;
            return Accessor._sqlmapper.DataSource.ToString();
            

        }
        static SQLDBHelper()
        {
            // 打开数据库连接
            //if (conn == null) 
            //{

            //    RegistryKey rk=Registry.LocalMachine;
            //    RegistryKey subrk=rk.OpenSubKey("software\\Sunsoft\\SunWebExam");
            //    string connString=(string)subrk.GetValue("server");
            //    conn = new SqlConnection(connString);
            //}
            conn =new SqlConnection( Accessor._sqlmapper.DataSource.ConnectionString);
         }
       
        public static bool ExcuteSql(string sql, SqlParameter[] paras)
        {
            bool result = false;
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (paras != null)
                {
                    cmd.Parameters.AddRange(paras);
                }
                cmd.Transaction = trans;
                int count = cmd.ExecuteNonQuery();
                trans.Commit();
                if (count > 0)
                {
                    result = true;
                }
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static DataTable GetDataTable(string sql, SqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            try
            {
                //SqlConnection.ClearAllPools();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (paras != null)
                {
                    cmd.Parameters.AddRange(paras);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException e)
            {
                throw e;
            }
            return dt;
        }
        public static object GetSingle(string SQLString)
        {
            using (SqlCommand cmd = new SqlCommand(SQLString, conn))
            {
                try
                {
                    conn.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static object GetSingle(string SQLString, SqlParameter[] paras)
        {
            using (SqlCommand cmd = new SqlCommand(SQLString, conn))
            {
                try
                {
                    conn.Open();
                    if (paras != null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;

            object obj = SQLDBHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        public static string AutoCreatID(string TableName, string FieldName)
        {
            string dbDateTime = DateTime.Now.ToString("yyyyMMdd");
            string Str = "select max(" + FieldName + ") as id from " + TableName;//查询表中的字段
            object obj = SQLDBHelper.GetSingle(Str);//把查询字符串Str放到GetSingle()中执行查询(Command)并返回查询结果（对象）
            string maxID = "";
            if (obj != null)//返回（查询字符串）对象
                maxID = SQLDBHelper.GetSingle(Str).ToString();
            string Result = "";
            if (maxID == "")//没有最大编号
            {
                Result = dbDateTime + "0001";//CG200902250001
            }
            else
            {
                //截取字符
                //string strFirstEight = maxID.Substring(4, 8);
                string strLastFour = maxID.Substring(8, 4);
                //if (dbDateTime == strFirstEight)//截取的最大编号（20090225）是否和数据库服务器系统时间相等
                //{
                    string strNewFour = (Convert.ToInt32(strLastFour) + 1).ToString("0000");//0000+1
                    Result =  dbDateTime + strNewFour;//CG200902250001
                //}
                //else
                //{
                //    Result = dbDateTime + "0001";
                //}
            }
            return Result;
        }
        
        public static string AutoNumber(string TableName, string FieldName)
        {
            string Str = "select max(" + FieldName + ") as id from " + TableName;//查询表中的字段
            object obj = SQLDBHelper.GetSingle(Str);//把查询字符串Str放到GetSingle()中执行查询(Command)并返回查询结果（对象）
            string maxID = "";
            if (obj != null)//返回（查询字符串）对象
                maxID = SQLDBHelper.GetSingle(Str).ToString();
            string Result = "";
            if (maxID == "")//没有最大编号
            {
                Result = "0001";
            }
            else
            {
                string strNewFour = (Convert.ToInt32(maxID) + 1).ToString("0000");//0000+1
                Result = strNewFour;//CG200902250001
            }
            return Result;
        }
    }
}
