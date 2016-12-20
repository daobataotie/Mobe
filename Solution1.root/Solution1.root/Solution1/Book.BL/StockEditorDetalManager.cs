//------------------------------------------------------------------------------
//
// file name：StockEditorDetalManager.cs
// author: mayanjun
// create date：2010-11-4 11:02:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.StockEditorDetal.
    /// </summary>
    public partial class StockEditorDetalManager
    {
		
		/// <summary>
		/// Delete StockEditorDetal by primary key.
		/// </summary>
		public void Delete(string stockEditorDetalId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(stockEditorDetalId);
		}

		/// <summary>
		/// Insert a StockEditorDetal.
		/// </summary>
        public void Insert(Model.StockEditorDetal stockEditorDetal)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(stockEditorDetal);
        }
		
		/// <summary>
		/// Update a StockEditorDetal.
		/// </summary>
        public void Update(Model.StockEditorDetal stockEditorDetal)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(stockEditorDetal);
        }

        public IList<Model.StockEditorDetal> SelectByStockEditorId(string stockEditorId)
        {
            return accessor.SelectByStockEditorId(stockEditorId);
        }

        public Model.StockEditorDetal SelectByProductIdAndPositionIdAndStockHId(string productId, string positionId, string stockEditorId)
        {
            return accessor.SelectByProductIdAndPositionIdAndStockHId(productId, positionId, stockEditorId);
        }

        public double SelectByProductIdAndStockHId(string productId, string stockEditorId)
        {
            return accessor.SelectByProductIdAndStockHId(productId, stockEditorId);
        }

        public IList<Model.StockEditorDetal> GetStockEditorDetalByDate(DateTime startDate, DateTime endDate)
        {
            return accessor.GetStockEditorDetalByDate(startDate, endDate);
        }

        public IList<Model.StockEditorDetal> SelectStockEditorDiff(DateTime startDate, DateTime endDate)
        {
            return accessor.SelectStockEditorDiff(startDate, endDate);
        }
    }
}

