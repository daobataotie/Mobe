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

// 编 码 人: 马艳军            完成时间:2009-6-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q47 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.StockManager stockManager = new Book.BL.StockManager();

        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        /// <param name="stock"></param>
        public Q47(ConditionStockByProduct stock)
        {
            InitializeComponent();
            if (stock.Product == null)
                return;
            System.Collections.Generic.IList<Model.Stock> list = this.stockManager.Select(stock.Product.ProductId);
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }
            this.xrLabelRepotName.Text = String.Format("{0} 庫房貨存統計",stock.Product.ProductName);
            bindingSource1.DataSource = list;
            this.xrTableCellDepotName.DataBindings.Add("Text", this.DataSource, "DepotPosition." + "Depot." + Model.Depot.PRO_DepotName);
            this.xrTableCellDepotStockCount.DataBindings.Add("Text",this.DataSource,Model.Stock.PRO_StockQuantity1);            
            this.xrSubreport1.ReportSource = new Q47_1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q47_1 report = this.xrSubreport1.ReportSource as Q47_1;
            report.Depot = (this.GetCurrentRow() as Model.Stock).Depot;
        }

    }
}
