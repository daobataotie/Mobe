//------------------------------------------------------------------------------
//
// file name：IStockEditorAccessor.cs
// author: mayanjun
// create date：2010-11-4 11:02:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.StockEditor
    /// </summary>
    public partial interface IStockEditorAccessor : IAccessor
    {
        IList<Model.StockEditor> SelectNoStockCheck();
    }
}

