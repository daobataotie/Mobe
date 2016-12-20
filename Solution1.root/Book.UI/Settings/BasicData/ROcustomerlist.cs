using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.BasicData
{
    public partial class ROcustomerlist : DevExpress.XtraReports.UI.XtraReport
    {
        public ROcustomerlist()
        {
            InitializeComponent();

            this.DataSource = new BL.CustomerManager().Select();

            //CompanyInfo
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.ROsupplierlist;
            this.lblReportDate.Text += DateTime.Now.ToShortDateString();

            //BindData
            this.TCId.DataBindings.Add("Text", this.DataSource, Model.Customer.PRO_Id);
            this.TCFullName.DataBindings.Add("Text", this.DataSource, Model.Customer.PRO_CustomerFullName);
            this.TCShortName.DataBindings.Add("Text", this.DataSource, Model.Customer.PRO_CustomerShortName);
            this.TCCustomerPhone.DataBindings.Add("Text", this.DataSource, Model.Customer.PRO_CustomerPhone);
            this.TCCustomerFax.DataBindings.Add("Text", this.DataSource, Model.Customer.PRO_CustomerFax);
            this.TCCustomerMobile.DataBindings.Add("Text", this.DataSource, Model.Customer.PRO_CustomerMobile);
            this.TCEmail.DataBindings.Add("Text", this.DataSource, Model.Customer.PRO_CustomerEMail);
            this.TClblCustomerAddress.DataBindings.Add("Text", this.DataSource, Model.Customer.PRO_CustomerAddress);
        }
    }
}
