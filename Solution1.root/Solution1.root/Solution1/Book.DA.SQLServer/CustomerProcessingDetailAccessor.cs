//------------------------------------------------------------------------------
//
// file name：CustomerProcessingDetailAccessor.cs
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
    /// Data accessor of CustomerProcessingDetail
    /// </summary>
    public partial class CustomerProcessingDetailAccessor : EntityAccessor, ICustomerProcessingDetailAccessor
    {
        public IList<Model.CustomerProcessingDetail> Select(Model.CustomerProcessing CustomerProcessing)
        {
            //
            // todo: add other logic here.
            //
            return sqlmapper.QueryForList<Model.CustomerProcessingDetail>("CustomerProcessingDetail.selectbyhead", CustomerProcessing.CustomerProcessingId); 
        }

        public IList<Model.CustomerProcessingDetail> SelectbyBomId(string bomid)
        {
            return sqlmapper.QueryForList<Model.CustomerProcessingDetail>("CustomerProcessingDetail.selectbybomid", bomid);
        }
    }
}
