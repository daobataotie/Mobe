using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DA.SQLServer
{
    public class Invoice00Accessor : Accessor, IInvoice00Accessor
    {
        #region IInvoice00Accessor Members

        public IList<Book.Model.Invoice00> Select()
        {
            return sqlmapper.QueryForList<Model.Invoice00>("Invoice00.select_all", null);
        }

        #endregion
    }
}
