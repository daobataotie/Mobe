using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-6-25
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class R14 : DevExpress.XtraReports.UI.XtraReport
    {

        protected BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();

        //无参构造
        public R14()
        {
            InitializeComponent();
            this.DataSource = this.miscDataManager.SelectProductStock();

            this.xrTableCellDepotName.DataBindings.Add("Text", this.DataSource, "DepotName");
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, "Quantity");
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.xrTableCellDepotPositionId.DataBindings.Add("Text", this.DataSource, "DepotPositionId");
            this.xrTableCellCnName.DataBindings.Add("Text", this.DataSource, "CnName");
            this.xrTableCusPro.DataBindings.Add("Text", this.DataSource, "CustomerProductName");
            //this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ProductDescription");
            this.TCId.DataBindings.Add("Text", this.DataSource, "spid");
            this.TCSafeStock.DataBindings.Add("Text", this.DataSource, "SafeStock");
        }

        public R14(DataTable dt)
        {
            InitializeComponent();
            this.DataSource = dt;

            this.xrTableCellDepotName.DataBindings.Add("Text", this.DataSource, "DepotName");
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, "Quantity");
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.xrTableCellDepotPositionId.DataBindings.Add("Text", this.DataSource, "DepotPositionId");
            this.xrTableCellCnName.DataBindings.Add("Text", this.DataSource, "CnName");
            this.xrTableCusPro.DataBindings.Add("Text", this.DataSource, "CustomerProductName");
            //this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ProductDescription");
            this.TCId.DataBindings.Add("Text", this.DataSource, "spid");
            this.TCSafeStock.DataBindings.Add("Text", this.DataSource, "SafeStock");
            this.TCYifenpeiquantity.DataBindings.Add("Text", this.DataSource, "yifenpeiquantity");
            this.TCYDWR.DataBindings.Add("Text", this.DataSource, "OrderOnWayQuantity");
        }
    }
}
