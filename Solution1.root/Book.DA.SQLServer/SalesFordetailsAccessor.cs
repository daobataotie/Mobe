//------------------------------------------------------------------------------
//
// file name：SalesFordetailsAccessor.cs
// author: peidun
// create date：2009-12-17 15:29:37
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
    /// Data accessor of SalesFordetails
    /// </summary>
    public partial class SalesFordetailsAccessor : EntityAccessor, ISalesFordetailsAccessor
    {
        public IList<Model.SalesFordetails> Getdetails( Model.SalesForHeader salesForHeader)
        {
            if (salesForHeader == null) return  null;
            else
           return   sqlmapper.QueryForList<Model.SalesFordetails>("SalesFordetails.select_bySalesHeader", salesForHeader.SalesForHeaderId);        
        }
        public IList<Model.SalesFordetails> GetdetailsByProductId(string productId)
        {
            if (productId == null) return null;
            else
                return sqlmapper.QueryForList<Model.SalesFordetails>("SalesFordetails.select_byProductId", productId);
        }
    }
}
