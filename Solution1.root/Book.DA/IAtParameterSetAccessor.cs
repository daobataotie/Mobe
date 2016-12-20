//------------------------------------------------------------------------------
//
// file name：IAtParameterSetAccessor.cs
// author: mayanjun
// create date：2012-3-26 14:33:25
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtParameterSet
    /// </summary>
    public partial interface IAtParameterSetAccessor : IAccessor
    {
        Model.AtParameterSet SelectByAtCurrentlyYear(int myear);
        void UpdateIsThisYear(string notId);
    }
}

