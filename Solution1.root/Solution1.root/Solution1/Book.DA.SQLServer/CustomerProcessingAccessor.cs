//------------------------------------------------------------------------------
//
// file name：CustomerProcessingAccessor.cs
// author: mayanjun
// create date：2010-7-30 19:31:57
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
    /// Data accessor of CustomerProcessing
    /// </summary>
    public partial class CustomerProcessingAccessor : EntityAccessor, ICustomerProcessingAccessor
    {
        public IList<Model.CustomerProcessing> Select(Model.Customer Customer)
        {
            //
            // todo: add other logic here.
            //
           return   sqlmapper.QueryForList<Model.CustomerProcessing>("CustomerProcessing.selectbycostomer",Customer.CustomerId);
        }
    }
}
