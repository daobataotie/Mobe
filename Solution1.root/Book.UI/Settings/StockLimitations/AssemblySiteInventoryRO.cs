using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.StockLimitations
{
    public partial class AssemblySiteInventoryRO : DevExpress.XtraReports.UI.XtraReport
    {
        public AssemblySiteInventoryRO(Model.AssemblySiteInventory model)
        {
            InitializeComponent();

            this.DataSource = model.Details;

            this.lbl_CompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportName.Text = "��װ�ֳ��̵�¼��";
            this.lbl_ReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.lbl_ID.Text = model.AssemblySiteInventoryId;
            this.lbl_Date.Text = model.InvoiceDate.HasValue ? model.InvoiceDate.Value.ToString("yyyy-MM-dd") : "";
            this.lbl_Employee.Text = model.Employee == null ? "" : model.Employee.EmployeeName;
            this.lbl_Note.Text = model.Note;

            TC_ProductID.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            TCProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            TCCustomerProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            TCProductVersion.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductVersion);
            TCActualQuantity.DataBindings.Add("Text", this.DataSource, Model.AssemblySiteInventoryDetail.PRO_Quantity, "{0:0.##}");
        }

    }
}
