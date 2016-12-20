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

// 编 码 人: 马艳军            完成时间:2009-6-6
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q46 : DevExpress.XtraReports.UI.XtraReport
    {

        BL.ProductManager productManager = new Book.BL.ProductManager();
    
        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q46(DepCondition condition)
        { 
            InitializeComponent();
            if (condition.Depot == null)
                return;
            System.Collections.Generic.IList<Model.Product> list = this.productManager.Select(condition.Depot);
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }

            this.bindingSource1.DataSource = list;
            this.xrLabelRepotName.Text = string.Format("{0} 庫房產品統計表", condition.Depot.DepotName);

            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_ProductName);
            this.xrTableCellBeenAssigned.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_BeenAssigned);
            this.xrTableCellOrderOnWayQuantity.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_OrderOnWayQuantity);
            this.xrTableCellStocksQuantity.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_StocksQuantity);

            this.xrSubreport1.ReportSource = new Q46_1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q46_1 reportProducts = this.xrSubreport1.ReportSource as Q46_1;
            reportProducts.product = this.GetCurrentRow() as Model.Product;

        }

    }
}
