//------------------------------------------------------------------------------
//
// file name：IStockCheckAccessor.cs
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
    /// Interface of data accessor of dbo.StockCheck
    /// </summary>
    public partial interface IStockCheckAccessor : IAccessor
    {
        void Delete(Book.Model.StockCheck invoice);
        Model.StockCheck SelectByStockCheckId(string stockid);
        IList<Model.StockCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate);
    }
}

