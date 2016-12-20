//------------------------------------------------------------------------------
//
// file name：AcbeginAccountPayableDetailAccessor.cs
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
    /// Data accessor of AcbeginAccountPayableDetail
    /// </summary>
    public partial class AcbeginAccountPayableDetailAccessor : EntityAccessor, IAcbeginAccountPayableDetailAccessor
    {
        public IList<Model.AcbeginAccountPayableDetail> Select(Model.AcbeginAccountPayable acbeginAccountPayable)
        {
            return sqlmapper.QueryForList<Model.AcbeginAccountPayableDetail>("AcbeginAccountPayableDetail.selectByacbeginaccountPaybleId", acbeginAccountPayable.AcbeginAccountPayableId);
        }

        public IList<Book.Model.AcbeginAccountPayableDetail> SelectDefaultDetails()
        {
            return sqlmapper.QueryForList<Model.AcbeginAccountPayableDetail>("AcbeginAccountPayableDetail.SelectDefaultDetails", null);
        }

        public void DeleteByAcbeginAccountPayableId(string id)
        {
            sqlmapper.Delete("AcbeginAccountPayableDetail.DeleteByAcbeginAccountPayableId", id);
        }
    }
}
