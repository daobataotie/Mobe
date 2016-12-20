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

// 编 码 人: 马艳军            完成时间:2009-6-8
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q46_1 : DevExpress.XtraReports.UI.XtraReport
    {
        Book.BL.ProductManager productManager = new Book.BL.ProductManager();
        Book.BL.StockManager stockManager = new Book.BL.StockManager();
        #region 无参构造
        public Q46_1()
        {
            InitializeComponent();
            this.xrTableCellDepotPositionId.DataBindings.Add("Text", this.DataSource, Model.Stock.PRO_DepotId);
            this.xrTableCellDepotPositionName.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_DEPOTPOSITIONNAME);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.Stock.PRO_StockQuantity1);
        }
        #endregion

        //货品
        private Model.Product _product=null;
        public Model.Product product 
        {
            get { return _product; }
            set { _product = value;}          
            
        }

        private void Q46_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.stockManager.Select(product.ProductId);
          
        }

    }
}
