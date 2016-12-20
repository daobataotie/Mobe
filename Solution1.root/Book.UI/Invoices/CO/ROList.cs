using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.CO
{
    public partial class ROList : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCOManager invoiceCOManager = new Book.BL.InvoiceCOManager();
        private BL.InvoiceCODetailManager invoiceCODetailManager = new Book.BL.InvoiceCODetailManager();
        private BL.InvoiceXOManager invoiceXoManager = new BL.InvoiceXOManager();
        private Model.InvoiceCO invoice;
        int pp = 0;
        public ROList(string invoiceid)
        {
            //InitializeComponent();

            //this.invoice = this.invoiceCGManager.Get(invoiceid);


            //if (this.invoice == null)
            //    return;

            //// this.invoice.Details = this.invoiceCGDetailManager.Select(this.invoice);
            //this.DataSource = this.invoice.Details;

            //this.xrBarCode1.Text = this.invoice.InvoiceId;
            ////CompanyInfo
            //this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            //this.xrLabelData.Text = Properties.Resources.InvoiceCO;
            //this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();
            ////客户信息
            //this.xrLabelCustomName.Text = this.invoice.Supplier.SupplierShortName;
            //this.xrLabelLianluoName.Text = this.invoice.Supplier.SupplierManager;
            //this.xrLabelCustomFax.Text = this.invoice.Supplier.SupplierFax;
            //this.xrLabelCustomTel.Text = string.IsNullOrEmpty(this.invoice.Supplier.SupplierPhone1) ? this.invoice.Supplier.SupplierPhone2 : this.invoice.Supplier.SupplierPhone1;
            //this.xrLabelTongYiNo.Text = this.invoice.Supplier.SupplierNumber;
            //if (this.invoice.Customer != null)
            //{
            //    this.xrLabelCustomer.Text = this.invoice.Customer.CustomerShortName;
            //    this.xrLabelJianCe.Text = this.invoice.Customer.CheckedStandard;
            //}
            ////单据信息
            //this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToShortDateString();
            //this.xrLabelYJDate.Text = this.invoice.InvoiceYjrq == null ? "" : this.invoice.InvoiceYjrq.Value.ToShortDateString();
            //this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;

            //this.xrLabelYeWuName.Text = this.invoice.Employee0 == null ? "" : this.invoice.Employee0.EmployeeName;
            //this.xrLabelTotal1.Text = this.invoice.InvoiceTotal.Value.ToString("0");
            //this.xrLabelNote.Text = this.invoice.InvoiceNote;
            //Model.InvoiceXO temp = this.invoiceXoManager.Get(this.invoice.InvoiceXOId);
            //if (temp != null)
            //{
            //    this.xrLabelInvoiceXOId.Text = temp.CustomerInvoiceXOId;
            //}

            ////明细信息
            //this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_Inumber);
            //this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            ////this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            //this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceProductUnit);
            //this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_OrderQuantity, "{0:0.##}");
            //this.xrTableCellUintPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailPrice, "{0:0.###}");
            //this.xrTableCellMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailMoney, "{0:0.###}");
            ////this.xrTableCellNetWeight.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_GrossWeight, "{0:0}");
            ////this.xrTableCellGrossWeight.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_NetWeight, "{0:0}");
            ////this.xrTableCellBulk.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_Bulk, "{0:0}");
            //this.xrRichTextProductDescribe.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
            //this.xrLabelDetailDesc.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailNote);
        }

        public ROList(IList<Model.InvoiceCO> mInvoiceCOList)
        {
            InitializeComponent();

            if (mInvoiceCOList == null || mInvoiceCOList.Count == 0)
                return;
            this.DataSource = mInvoiceCOList;
            this.mBand();
        }

        public void mBand()
        {
            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelData.Text = Properties.Resources.InvoiceCO;
            this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();

            //客户信息
            this.xrBarCode1.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PROPERTY_INVOICEID);
            this.xrLabelCustomName.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.xrLabelLianluoName.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERMANAGER);
            this.xrLabelCustomFax.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERFAX);
            this.xrLabelCustomTel.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERPHONE1);
            this.xrLabelTongYiNo.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERNUMBER);
            this.xrLabelCustomer.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerShortName);
            this.xrLabelJianCe.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CheckedStandard);
            this.xrLabelInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PROPERTY_INVOICEDATE);
            this.xrLabelYJDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PRO_InvoiceYjrq);
            this.xrLabelInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PROPERTY_INVOICEID);
            this.xrLabelYeWuName.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelShenHeName.DataBindings.Add("Text", this.DataSource, "AuditEmp." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelTotal1.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PRO_InvoiceTotal, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGZJXiao.Value));
            this.xrLabelNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PROPERTY_INVOICENOTE);
            this.xrLabelInvoiceXOId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCO.PRO_InvoiceCustomXOId);
            this.GroupHeader1.GroupFields.Add(new GroupField(Model.InvoiceCO.PROPERTY_INVOICEID));
            this.xrSubreport1.ReportSource = new ROListDetails();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ROListDetails detail = this.xrSubreport1.ReportSource as ROListDetails;
            Model.InvoiceCO mico = this.GetCurrentRow() as Model.InvoiceCO;
            if (mico != null)
            {
                mico.Details = this.invoiceCODetailManager.Select(mico);
                detail.MInvoiceCO = mico;
            }
        }
    }
}
#region Note
//this.xrLabelCustomName.Text = this.invoice.Supplier.SupplierShortName;
//this.xrLabelLianluoName.Text = this.invoice.Supplier.SupplierManager;
//this.xrLabelCustomFax.Text = this.invoice.Supplier.SupplierFax;
//this.xrLabelCustomTel.Text = string.IsNullOrEmpty(this.invoice.Supplier.SupplierPhone1) ? this.invoice.Supplier.SupplierPhone2 : this.invoice.Supplier.SupplierPhone1;
//this.xrLabelTongYiNo.Text = this.invoice.Supplier.SupplierNumber;
//if (this.invoice.Customer != null)
//{
//    this.xrLabelCustomer.Text = this.invoice.Customer.CustomerShortName;
//    this.xrLabelJianCe.Text = this.invoice.Customer.CheckedStandard;

//}
//单据信息
//this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToShortDateString();
//this.xrLabelYJDate.Text = this.invoice.InvoiceYjrq == null ? "" : this.invoice.InvoiceYjrq.Value.ToShortDateString();
//this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;

//this.xrLabelYeWuName.Text = this.invoice.Employee0 == null ? "" : this.invoice.Employee0.EmployeeName;
//this.xrLabelTotal1.Text = this.invoice.InvoiceTotal.Value.ToString("0");
//this.xrLabelNote.Text = this.invoice.InvoiceNote;
//Model.InvoiceXO temp = this.invoiceXoManager.Get(this.invoice.InvoiceXOId);
//if (temp != null)
//{
//    this.xrLabelInvoiceXOId.Text = temp.CustomerInvoiceXOId;
//}
#endregion