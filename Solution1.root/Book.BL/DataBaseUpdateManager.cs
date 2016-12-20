using System;
using System.Collections.Generic;
using System.Text;

namespace Book.BL
{
    public class DataBaseUpdateManager
    {
        private static readonly DA.IDataBaseUpdate accessor = (DA.IDataBaseUpdate)Accessors.Get("DataBaseUpdateAccessor");

        public void Update(String[] sqls) 
        {   
            try
            {
                V.BeginTransaction();
                for (int i = 1; i < sqls.Length; i++)
                {
                    string sql = sqls[i];
                    sql = sql.Replace("\r\n", "");
                    accessor.Update(sql);
                }
                V.CommitTransaction();
            }
            catch 
            {
                V.RollbackTransaction();
            }
        }

        public static int GetCurrentDataBaseVersion() 
        {
            return accessor.GetCurrentDataBaseVersion();
        }
    }
}
