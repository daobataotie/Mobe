//------------------------------------------------------------------------------
//
// file name：ProduceOtherReturnDetailAccessor.cs
// author: mayanjun
// create date：2011-08-31 15:05:11
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
    /// Data accessor of ProduceOtherReturnDetail
    /// </summary>
    public partial class ProduceOtherReturnDetailAccessor : EntityAccessor, IProduceOtherReturnDetailAccessor
    {
        public System.Collections.Generic.IList<Model.ProduceOtherReturnDetail> Select(Model.ProduceOtherReturnMaterial Material)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherReturnDetail>("ProduceOtherReturnDetail.selectByHeadId", Material.ProduceOtherReturnMaterialId);
        }
    }
}
