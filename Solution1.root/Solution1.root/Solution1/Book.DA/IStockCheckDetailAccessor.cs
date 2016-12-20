//------------------------------------------------------------------------------
//
// file name：IStockCheckDetailAccessor.cs
// author: mayanjun
// create date：2010-7-30 11:43:33
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.StockCheckDetail
    /// </summary>
    public partial interface IStockCheckDetailAccessor : IAccessor
    {
        IList<Book.Model.StockCheckDetail> Select(Book.Model.StockCheck stockCheck);
        void Delete(Model.StockCheck invoice);
        Model.StockCheckDetail SelectStockcheck(Model.StockCheckDetail stockcheck);
        IList<Model.StockCheckDetail> GetStockCheckDetailByDate(DateTime startDate, DateTime endDate);
        double GetNumsByProductIdAndDepositionId(string positionId, string productid);
        Model.StockCheckDetail SelectByProductIdAndPositionIdAndStockCheckId(string positionId, string productId, string stockCheckId);
        System.Data.DataSet SelectDataSet();
        System.Data.DataSet SelectDataSet(DateTime start, DateTime end);
        void UpdateDataTable(System.Data.DataTable stockcheck);
        IList<Model.StockCheckDetail> SelectByProductId(string productId);
        IList<Model.StockCheckDetail> SelectRangeDataDiffCheck(DateTime startTime, DateTime endTime);
    }
}

