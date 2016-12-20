using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.BasicData
{
    public partial class ROsupplierlist : DevExpress.XtraReports.UI.XtraReport
    {
        public ROsupplierlist()
        {
            InitializeComponent();

            this.DataSource = new BL.SupplierManager().Select();

            //CompanyInfo
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.ROsupplierlist;
            this.lblReportDate.Text += DateTime.Now.ToShortDateString();

            //BindData
            this.TCId.DataBindings.Add("Text", this.DataSource, Model.Supplier.PROPERTY_ID);
            this.TCFullName.DataBindings.Add("Text", this.DataSource, Model.Supplier.PROPERTY_SUPPLIERFULLNAME);
            this.TCShortName.DataBindings.Add("Text", this.DataSource, Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.TCSupplierCategory.DataBindings.Add("Text", this.DataSource, "SupplierCategory." + Model.SupplierCategory.PROPERTY_SUPPLIERCATEGORYNAME);
            this.TCSupplierPhone1.DataBindings.Add("Text", this.DataSource, Model.Supplier.PROPERTY_SUPPLIERPHONE1);
            this.TCSupplierFax.DataBindings.Add("Text", this.DataSource, Model.Supplier.PROPERTY_SUPPLIERFAX);
            this.TCSupplierMobile.DataBindings.Add("Text", this.DataSource, Model.Supplier.PROPERTY_SUPPLIERMOBILE);
            this.TCEmail.DataBindings.Add("Text", this.DataSource, Model.Supplier.PROPERTY_EMAIL);
            this.TClblCompanyAddress.DataBindings.Add("Text", this.DataSource, Model.Supplier.PROPERTY_COMPANYADDRESS);
        }
    }
}
