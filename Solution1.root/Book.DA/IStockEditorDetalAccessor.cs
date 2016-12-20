//------------------------------------------------------------------------------
//
// file name：IStockEditorDetalAccessor.cs
// author: mayanjun
// create date：2010-11-4 11:02:33
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.StockEditorDetal
    /// </summary>
    public partial interface IStockEditorDetalAccessor : IAccessor
    {
        IList<Model.StockEditorDetal> SelectByStockEditorId(string stockEditorId);
        Model.StockEditorDetal SelectByProductIdAndPositionIdAndStockHId(string productId, string positionId, string stockEditorId);
        double SelectByProductIdAndStockHId(string productId, string stockEditorId);
        IList<Model.StockEditorDetal> GetStockEditorDetalByDate(DateTime startDate, DateTime endDate);
        IList<Model.StockEditorDetal> SelectStockEditorDiff(DateTime startDate, DateTime endDate);
    }
}

