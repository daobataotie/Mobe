//------------------------------------------------------------------------------
//
// file name：IConveyanceMethodAccessor.cs
// author: mayanjun
// create date：2010-8-9 15:00:25
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ConveyanceMethod
    /// </summary>
    public partial interface IConveyanceMethodAccessor : IAccessor
    {
        bool IsExists(Model.ConveyanceMethod convery);
    }
}

