//------------------------------------------------------------------------------
//
// file name：CustomerContactAccessor.cs
// author: peidun
// create date：2009-08-06 14:53:57
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of CustomerContact
    /// </summary>
    public partial class CustomerContactAccessor : EntityAccessor, ICustomerContactAccessor
    {
        #region ICustomerContactAccessor Members

        public IList<Model.CustomerContact> Select(Model.Customer customer) 
        {
            if (customer == null)
                return (IList<Model.CustomerContact>)new List<Model.CustomerContact>();
            return sqlmapper.QueryForList<Model.CustomerContact>("CustomerContact.selectbycustomer", customer.CustomerId);
        }

        public void Delete(Book.Model.Customer customer)
        {
            sqlmapper.Delete("CustomerContact.delete_by_customer", customer.CustomerId);
        }

        #endregion
    }
}
