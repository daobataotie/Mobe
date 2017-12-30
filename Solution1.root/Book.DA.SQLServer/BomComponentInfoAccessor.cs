//------------------------------------------------------------------------------
//
// file name：BomComponentInfoAccessor.cs
// author: peidun
// create date：2009-08-25 17:08:56
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
    /// Data accessor of BomComponentInfo
    /// </summary>
    public partial class BomComponentInfoAccessor : EntityAccessor, IBomComponentInfoAccessor
    {

        public IList<Book.Model.BomComponentInfo> Select(Book.Model.BomParentPartInfo par)
        {
            return sqlmapper.QueryForList<Model.BomComponentInfo>("BomComponentInfo.select_byParents", par == null ? "" : par.BomId);
        }

        public void Delete(Book.Model.BomParentPartInfo bomParentPartInfo)
        {
            sqlmapper.Delete("BomComponentInfo.delete_byParents", bomParentPartInfo == null ? "" : bomParentPartInfo.BomId);
        }

        public IList<Model.BomComponentInfo> SelectNotContent(Model.BomParentPartInfo bom)
        {
            return sqlmapper.QueryForList<Model.BomComponentInfo>("BomComponentInfo.selectnotcontentByBom", bom.BomId);
        }

        public Model.BomComponentInfo IsExistsIndexOfBom(Model.BomComponentInfo bomcom)
        {
            return sqlmapper.QueryForObject<Model.BomComponentInfo>("BomComponentInfo.IsExistsIndexOfBom", bomcom.IndexOfBom);
        }

        public IList<Book.Model.BomComponentInfo> SelectLessInfoByHeaderId(string BomId)
        {
            return sqlmapper.QueryForList<Model.BomComponentInfo>("BomComponentInfo.SelectLessInfoByHeaderId", BomId);
        }

        public IList<Model.BomComponentInfo> SelectBomIdAndUseQty(string productIds)
        {
            string sql = "select BomId,UseQuantity,ProductId from BomComponentInfo where ProductId in (" + productIds + ")";
            return this.DataReaderBind<Model.BomComponentInfo>(sql, null, CommandType.Text);
        }
    }
}
