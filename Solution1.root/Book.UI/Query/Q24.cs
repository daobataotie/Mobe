using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-6-15
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q24 : BaseReport
    {
        protected BL.MiscDataManager miscDateManager = new Book.BL.MiscDataManager();


        /// <summary>
        /// 一参构造，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q24(ConditionD condition)
        {
            InitializeComponent();            

            System.Collections.Generic.IList<Model.Stock> list = this.miscDateManager.Select(condition.StartId, condition.EndId);

            if (list == null || list.Count <= 0) 
            {
                throw new global::Helper.InvalidValueException();
            }

            this.bindingSource1.DataSource = list;
            this.xrLabelReportName.Text = Properties.Resources.StockSafeQuantity;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.ProductIdRange, condition.StartId, condition.EndId);
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.Stock.PRO_ProductId);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PROPERTY_PRODUCTBASEUNIT);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.Stock.PRO_StockQuantity1, "{0:0}");
            this.xrTableCellDepotName.DataBindings.Add("Text", this.DataSource, "Depot." + Model.Depot.PRO_DepotName);
            this.xrTableCellSafeQuantity.DataBindings.Add("Text", this.DataSource,Model.Stock.PRO_StockQuantityD);
            this.xrTableCellDiffQuantity.DataBindings.Add("Text", this.DataSource, Model.Stock.PROPERTY_STOCKDIFFQUANTITY);
            
        }

    }
}
