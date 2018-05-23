using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.StockLimitations
{
    public partial class AssemblySiteDifferenceRO : DevExpress.XtraReports.UI.XtraReport
    {
        public AssemblySiteDifferenceRO(Model.AssemblySiteDifference model)
        {
            InitializeComponent();

            this.DataSource = model.Details;

            this.lbl_CompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportName.Text = "组装现场盘点差异";
            this.lbl_ReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.lbl_ID.Text = model.AssemblySiteDifferenceId;
            this.lbl_ID2.Text = model.AssemblySiteInventoryId;
            this.lbl_Date.Text = model.InvoiceDate.HasValue ? model.InvoiceDate.Value.ToString("yyyy-MM-dd") : "";
            this.lbl_Employee.Text = model.Employee == null ? "" : model.Employee.EmployeeName;
            this.lbl_Note.Text = model.Note;

            TC_ProductID.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            TCProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            TCCustomerProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            TCProductVersion.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductVersion);
            TCActualQuantity.DataBindings.Add("Text", this.DataSource, Model.AssemblySiteDifferenceDetai.PRO_ActualQuantity, "{0:0.##}");
            TCTheoryQuantit.DataBindings.Add("Text", this.DataSource, Model.AssemblySiteDifferenceDetai.PRO_TheoryQuantity, "{0:0.##}");
            TCChayi.DataBindings.Add("Text", this.DataSource, "DiffQty", "{0:0.##}");
        }


    }
}
