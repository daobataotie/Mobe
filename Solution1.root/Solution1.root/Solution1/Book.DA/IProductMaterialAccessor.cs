//------------------------------------------------------------------------------
//
// file name：IProductMaterialAccessor.cs
// author: mayanjun
// create date：2010-9-23 15:27:44
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductMaterial
    /// </summary>
    public partial interface IProductMaterialAccessor : IAccessor
    {
        bool IsExistProductMaterialName(Model.ProductMaterial productMateridal);
        bool IsExistId(Model.ProductMaterial productMateridal);
    }
}

