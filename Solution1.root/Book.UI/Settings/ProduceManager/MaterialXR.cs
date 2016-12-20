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

// 编 码 人: 裴盾            完成时间:2009-11-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class MaterialXR : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProductProcessManager productProcessManager = new Book.BL.ProductProcessManager();

        #region 构造函数
        public MaterialXR()
        {
            InitializeComponent();
        }     
        public MaterialXR(System.Collections.Generic.IList<Model.BomComponentInfo> detail,Model.BomParentPartInfo par):this()
        {
            if (par != null && par.Product != null)
            {
                this.xrLabelProductID.Text = par.Product.Id;
                this.xrLabelProductName.Text = par.Product.ProductName;
            }
        
            this.xrLabel1.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.BomInFo;

            this.xrLabelLossRate.Text = par.LossRate.ToString();
            this.xrLabelSpec.Text = par.Product.ProductSpecification;
            if (par.Customer != null)
            {
                this.xrLabelCustomer.Text = par.Customer.CustomerShortName;
                this.xrLabelCustomerProductName.Text = par.CustomerProductName;
            }

            this.xrLabelDate.Text = DateTime.Now.ToShortDateString(); ;

            this.DataSource=detail;
            this.xrTableDengji.DataBindings.Add("Text", this.DataSource, "Jibie"); 
            this.xrTableId.DataBindings.Add("Text", this.DataSource, "Product.Id");
            this.xrTableUserCount.DataBindings.Add("Text", this.DataSource, "UseQuantity");
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PROPERTY_UNIT);
            this.xrTableName.DataBindings.Add("Text", this.DataSource, "Product.ProductName");
            this.xrTableGuiGe.DataBindings.Add("Text", this.DataSource, "Product.ProductSpecification");
            this.xrTableLossRate.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PROPERTY_SUBLOSERATE);
            this.xrTableEffectsDate.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PROPERTY_EFFECTSDATE, "{0:yyyy-MM-dd}");
            this.xrTableExpiringDate.DataBindings.Add("Text", this.DataSource, Model.BomComponentInfo.PROPERTY_EXPIRINGDATE, "{0:yyyy-MM-dd}");
            this.xrSubreport1.ReportSource = new BomProcess_XR();
            this.xrLabel11.DataBindings.Add("Text", this.DataSource, "Product.Id");

        }
        #endregion



        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BomProcess_XR bomxr = this.xrSubreport1.ReportSource as BomProcess_XR;
            bomxr.Product = (this.GetCurrentRow() as Model.BomComponentInfo).Product;
            
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BomProcess_XR bomxr = this.xrSubreport1.ReportSource as BomProcess_XR;
            bomxr.Product = (this.GetCurrentRow() as Model.BomComponentInfo).Product ;
            if (this.productProcessManager.Select(bomxr.Product.ProductId).Count ==0)
            {
                this.Detail.Height = 55;
                this.xrSubreport1.Visible = false;            
            }
            else
            {
                this.xrSubreport1.Visible = true;
            }

        }

    }
}
