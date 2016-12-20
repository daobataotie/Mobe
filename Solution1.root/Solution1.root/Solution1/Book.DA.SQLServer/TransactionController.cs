using System;
using System.Collections.Generic;
using System.Text;
using IBatisNet.DataMapper;

namespace Book.DA.SQLServer
{
    public class TransactionController : Accessor, DA.ITransactionController
    {
        #region ITransactionController Members

        public void BeginTransaction()
        {
            sqlmapper.BeginTransaction();
        }

        public void CommitTransaction()
        {
            sqlmapper.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            try
            {
                sqlmapper.RollBackTransaction();
            }
            catch
            {
            }
        }

        #endregion
    }
}
