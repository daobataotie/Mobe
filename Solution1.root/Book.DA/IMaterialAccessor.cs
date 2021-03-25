//------------------------------------------------------------------------------
//
// file name：IMaterialAccessor.cs
// author: mayanjun
// create date：2013-5-4 16:09:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Material
    /// </summary>
    public partial interface IMaterialAccessor : IAccessor
    {
        double CountJWeightByMaterial(string MaterialId);
        IList<Model.Material> SelectOther();
        IList<string> SelectIdByMaterialId(string MaterialId);
        Model.Material SelectMaterialByPrimary(string id);

        IList<string> SelectMaterialCategory();
        IList<Model.Material> SelectAll();
        string SelectIdByPrimary(string Id);

        IList<Model.Material> SelectAllByPrimaryIds(string Ids);
    }
}

