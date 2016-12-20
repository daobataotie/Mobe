//------------------------------------------------------------------------------
//
// file name：BomPackageDetailsAccessor.cs
// author: peidun
// create date：2009-11-12 11:03:19
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
    /// Data accessor of BomPackageDetails
    /// </summary>
    public partial class BomPackageDetailsAccessor : EntityAccessor, IBomPackageDetailsAccessor
    {

        public IList<Book.Model.BomPackageDetails> Select(string pac)
        {
            return sqlmapper.QueryForList<Model.BomPackageDetails>("BomPackageDetails.select_byBomId", pac);
        }
        public void Delete(Model.BomParentPartInfo bom)
        {
            sqlmapper.Delete("BomPackageDetails.deleteByBomId",bom.BomId);
        }

    }
}
