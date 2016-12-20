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
    public partial class Q47_1 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.StockManager stockMamager = new Book.BL.StockManager();

        #region 无参构造
        public Q47_1()
        {
            InitializeComponent();
            this.xrTableCellDepotPositionId.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_DEPOTPOSITIONID);
            this.xrTableCellDepotPositionName.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_DEPOTPOSITIONNAME);
            this.xrTableCellStockQuantity1.DataBindings.Add("Text", this.DataSource, Model.Stock.PRO_StockQuantity1);
        }
        #endregion


        private Model.Depot _depot;
        public Model.Depot Depot
        {
            get { return _depot; }
            set { _depot = value; }
        
        }

        //打印前触发
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.stockMamager.Select(this.Depot);
        }

    }
}
