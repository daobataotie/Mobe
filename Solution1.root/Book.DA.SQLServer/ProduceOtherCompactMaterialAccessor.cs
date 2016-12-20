//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactMaterialAccessor.cs
// author: mayanjun
// create date：2010-12-2 16:11:03
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
    /// Data accessor of ProduceOtherCompactMaterial
    /// </summary>
    public partial class ProduceOtherCompactMaterialAccessor : EntityAccessor, IProduceOtherCompactMaterialAccessor
    {
        public IList<Model.ProduceOtherCompactMaterial> SelectIsInDepotMaterialDetail(Model.ProduceOtherCompact com)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactMaterial>("ProduceOtherCompactMaterial.selectIsInDepotMaterialDetail", com.ProduceOtherCompactId);
        }
        public IList<Model.ProduceOtherCompactMaterial> Select(Model.ProduceOtherCompact ProduceOtherCompact)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactMaterial>("ProduceOtherCompactMaterial.selectByOtherCompact", ProduceOtherCompact.ProduceOtherCompactId);
        }
        public IList<Model.ProduceOtherCompactMaterial> SelectCompactAndFlag(Model.ProduceOtherCompact ProduceOtherCompact)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactMaterial>("ProduceOtherCompactMaterial.selectByOtherCompactAndFlag", ProduceOtherCompact.ProduceOtherCompactId);
        }
    }
}
