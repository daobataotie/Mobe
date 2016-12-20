//------------------------------------------------------------------------------
//
// file name：ISpecialProfitMarginAccessor.cs
// author: peidun
// create date：2008/6/30 14:20:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.SpecialProfitMargin
    /// </summary>
    public partial interface ISpecialProfitMarginAccessor : IEntityAccessor
    {
        System.Data.DataTable SelectDataTable();

        void UpdateDataTable(System.Data.DataTable table);
    }
}

