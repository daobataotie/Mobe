//------------------------------------------------------------------------------
//
// file name：AcbeginbillReceivableDetailAccessor.cs
// author: mayanjun
// create date：2011-6-9 14:42:10
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
    /// Data accessor of AcbeginbillReceivableDetail
    /// </summary>
    public partial class AcbeginbillReceivableDetailAccessor : EntityAccessor, IAcbeginbillReceivableDetailAccessor
    {

        public IList<Model.AcbeginbillReceivableDetail> Select(Model.AcbeginbillReceivable acbeginbillReceivable)
        {
            return sqlmapper.QueryForList<Model.AcbeginbillReceivableDetail>("AcbeginbillReceivableDetail.selectBybillId", acbeginbillReceivable.AcbeginbillReceivableId);
        }

        public IList<Book.Model.AcbeginbillReceivableDetail> SelectDefaultDetails()
        {
            return sqlmapper.QueryForList<Model.AcbeginbillReceivableDetail>("AcbeginbillReceivableDetail.SelectDefaultDetails", null);
        }

        public void DeleteByParentId(string id)
        {
            sqlmapper.Delete("AcbeginbillReceivableDetail.DeleteByParentId", id);
        }
    }
}
