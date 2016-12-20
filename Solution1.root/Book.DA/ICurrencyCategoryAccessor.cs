//------------------------------------------------------------------------------
//
// file name：ICurrencyCategoryAccessor.cs
// author: peidun
// create date：2009-09-09 下午 04:08:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.CurrencyCategory
    /// </summary>
    public partial interface ICurrencyCategoryAccessor : IAccessor
    {
        IList<Model.CurrencyCategory> SelectByEffectDate();
    }
}

