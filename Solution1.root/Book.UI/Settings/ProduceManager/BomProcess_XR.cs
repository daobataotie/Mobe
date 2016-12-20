using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.ProduceManager
{

    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-11-22
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class BomProcess_XR : DevExpress.XtraReports.UI.XtraReport
    {
        private Model.Product _product;
        private BL.ProductProcessManager productProcessManager = new Book.BL.ProductProcessManager();
        public BomProcess_XR()
        {
            InitializeComponent();
            {  
                this.xrTableProceCate.DataBindings.Add("Text", this.DataSource,"ProcessCategory."+ Model.ProcessCategory.PROPERTY_PROCESSCATEGORYNAME);
               // this.xrTableContent.DataBindings.Add("Text", this.DataSource, "Process." + Model.Processing.PROPERTY_DESCRIPTION);
              //  this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Process." + Model.Processing.PROPERTY_CONTENT);
               
            }

        }

        public Model.Product Product
        {
            get { return this._product; }
            set { this._product = value; }
        }

        private void BomProcess_XR_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
                this.DataSource =  this.productProcessManager.Select(this.Product.ProductId);
           
        }
       

    }
}
