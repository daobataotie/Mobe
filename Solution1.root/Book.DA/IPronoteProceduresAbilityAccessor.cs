//------------------------------------------------------------------------------
//
// file name：IPronoteProceduresAbilityAccessor.cs
// author: mayanjun
// create date：2010-9-23 14:25:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PronoteProceduresAbility
    /// </summary>
    public partial interface IPronoteProceduresAbilityAccessor : IAccessor
    {
        Model.PronoteProceduresAbility GetByProcedures(string proceduresId);
    }
}

