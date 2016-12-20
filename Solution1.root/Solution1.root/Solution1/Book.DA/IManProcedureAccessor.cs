//------------------------------------------------------------------------------
//
// file name：IManProcedureAccessor.cs
// author: peidun
// create date：2009-12-9 9:32:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ManProcedure
    /// </summary>
    public partial interface IManProcedureAccessor : IAccessor
    {
         void Delete(Model.BomParentPartInfo bom);
         Model.ManProcedure Select(Model.BomParentPartInfo bom, Model.Customer customer);
         Model.ManProcedure Select(Model.BomParentPartInfo bom, Model.Customer customer, Model.Product MadeProduct);
    }
}

