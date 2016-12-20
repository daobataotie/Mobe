//------------------------------------------------------------------------------
//
// file name：IProduceOtherCompactMaterialAccessor.cs
// author: mayanjun
// create date：2010-12-2 16:11:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceOtherCompactMaterial
    /// </summary>
    public partial interface IProduceOtherCompactMaterialAccessor : IAccessor
    {
        IList<Model.ProduceOtherCompactMaterial> SelectIsInDepotMaterialDetail(Model.ProduceOtherCompact com);
        IList<Model.ProduceOtherCompactMaterial> Select(Model.ProduceOtherCompact ProduceOtherCompact);
        IList<Model.ProduceOtherCompactMaterial> SelectCompactAndFlag(Model.ProduceOtherCompact ProduceOtherCompact);
    }
}

