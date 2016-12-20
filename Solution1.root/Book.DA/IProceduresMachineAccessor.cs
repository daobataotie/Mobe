//------------------------------------------------------------------------------
//
// file name：IProceduresMachineAccessor.cs
// author: mayanjun
// create date：2010-9-17 16:47:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProceduresMachine
    /// </summary>
    public partial interface IProceduresMachineAccessor : IAccessor
    {
        void DelelteByProduresMachines(string ProceduresId);
    }
}

