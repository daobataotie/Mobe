//------------------------------------------------------------------------------
//
// file name：StockEditorDetalAccessor.cs
// author: mayanjun
// create date：2010-11-4 11:02:34
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of StockEditorDetal
    /// </summary>
    public partial class StockEditorDetalAccessor : EntityAccessor, IStockEditorDetalAccessor
    {
        public IList<Model.StockEditorDetal> SelectByStockEditorId(string stockEditorId)
        {
            return sqlmapper.QueryForList<Model.StockEditorDetal>("StockEditorDetal.selectByStockEditorId", stockEditorId);
        }

        public Model.StockEditorDetal SelectByProductIdAndPositionIdAndStockHId(string productId, string positionId, string stockEditorId)
        {
            Hashtable ht=new Hashtable();
            ht.Add("productId", productId);
            ht.Add("depotpositionId", positionId);
            ht.Add("stockEditorId", stockEditorId);
            return sqlmapper.QueryForObject<Model.StockEditorDetal>("StockEditorDetal.selectByProductIdAndPositionIdAndStockHId", ht);
        }

        public double SelectByProductIdAndStockHId(string productId, string stockEditorId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", productId);
            ht.Add("stockEditorId", stockEditorId);
            return sqlmapper.QueryForObject<double>("StockEditorDetal.selectByProductIdAndStockHId", ht);
        }

        public IList<Model.StockEditorDetal> GetStockEditorDetalByDate(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate);
            ht.Add("endDate", endDate);
            return sqlmapper.QueryForList<Model.StockEditorDetal>("StockEditorDetal.Select_by_Rangedate", ht);
        }

        public IList<Model.StockEditorDetal> SelectStockEditorDiff(DateTime  startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate);
            ht.Add("endDate", endDate);
            return sqlmapper.QueryForList<Model.StockEditorDetal>("StockEditorDetal.SelectbyRangedate", ht);
        }
    }
}
