//------------------------------------------------------------------------------
//
// file name：BOMProductProcessAccessor.cs
// author: peidun
// create date：2009-11-14 9:44:35
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
    /// Data accessor of BOMProductProcess
    /// </summary>
    public partial class BOMProductProcessAccessor : EntityAccessor, IBOMProductProcessAccessor
    {
        public void Delete(Model.BomParentPartInfo bomParentPartInfo)
        {
            sqlmapper.Delete("BOMProductProcess.delete_byBomId",bomParentPartInfo.BomId);
        }
        public IList<Model.BOMProductProcess> Select(string bomId)
        {
         return  sqlmapper.QueryForList<Model.BOMProductProcess>("BOMProductProcess.select_byBomId", bomId);


            //System.Collections.Generic.IList<Model.BOMProductProcess> list = null;
            //if (string.IsNullOrEmpty(bomId))
            //{
            //    list = sqlmapper.QueryForList<Model.BOMProductProcess>("BOMProductProcess.select_processcategory", null);
            //}
            //else
            //{
            //    list = sqlmapper.QueryForList<Model.BOMProductProcess>("BOMProductProcess.select_processcategory_by_customerproductid", bomId);
            //    if (list.Count == 0)
            //    {
            //        list = sqlmapper.QueryForList<Model.BOMProductProcess>("BOMProductProcess.select_processcategory", null);
            //    }
            //}

            //return list;
        }
    }
}
