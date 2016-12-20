//------------------------------------------------------------------------------
//
// file name：CustomerPackageDetailAccessor.cs
// author: peidun
// create date：2010-2-4 11:15:13
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
    /// Data accessor of CustomerPackageDetail
    /// </summary>
    public partial class CustomerPackageDetailAccessor : EntityAccessor, ICustomerPackageDetailAccessor
    {
        public IList<Model.CustomerPackageDetail> GetByPackageId(string customerPackageId)
        {
            return sqlmapper.QueryForList<Model.CustomerPackageDetail>("CustomerPackageDetail.select_byPackageId", customerPackageId);
        }
    }
}
