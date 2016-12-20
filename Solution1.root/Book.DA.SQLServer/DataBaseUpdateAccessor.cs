using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Book.DA.SQLServer
{
    public class DataBaseUpdateAccessor : Accessor, IDataBaseUpdate
    {
        #region IDataBaseUpdate 成员

        public void Update(string fileContent)
        {
            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(fileContent,con))
                {
                    try
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw;
                    }
                    finally 
                    {
                        con.Close();
                    }
                }
            }
        }

        public int GetCurrentDataBaseVersion()
        {
            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from DataBaseVersion", con))
                {
                    try
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.Text;
                        object obj = cmd.ExecuteScalar();
                        if (obj != null)
                            return int.Parse(obj.ToString());
                        return -1;
                    }
                    catch
                    {
                        return -1;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        #endregion
    }
}
