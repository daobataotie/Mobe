//------------------------------------------------------------------------------
//
// file name：IPronoteProceduresAbilityDetailAccessor.cs
// author: mayanjun
// create date：2010-9-23 14:25:23
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PronoteProceduresAbilityDetail
    /// </summary>
    public partial interface IPronoteProceduresAbilityDetailAccessor : IAccessor
    {
         IList<Model.PronoteProceduresAbilityDetail> GetByHeader(Model.PronoteProceduresAbility header);
    }
}

