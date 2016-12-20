using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.XO
{
    public partial class ROAgreement : DevExpress.XtraReports.UI.XtraReport
    {
        public ROAgreement()
        {
            InitializeComponent();
        }
        BL.BGHandbookDetail1Manager manager = new Book.BL.BGHandbookDetail1Manager();
        public ROAgreement(Model.InvoiceXO invoice)
            : this()
        {
            this.lbl_Supplier.Text = BL.Settings.CompanyChineseName;
            this.lbl_Customer.Text = invoice.xocustomer == null ? invoice.Customer.ToString() : invoice.xocustomer.ToString();
            this.lbl_AgreementId.Text = invoice.CustomerInvoiceXOId;
            this.lbl_Date.Text = invoice.InvoiceDate.Value.Year + "Äê" + invoice.InvoiceDate.Value.Month + "ÔÂ" + invoice.InvoiceDate.Value.Day + "ÈÕ";

            this.DataSource = invoice.Details;
            foreach (Model.InvoiceXODetail detail in invoice.Details)
            {
                detail.BGProduct = manager.SelectBGProduct(detail.HandbookId, detail.HandbookProductId);
            }

            this.TCId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_Inumber);
            this.TCName.DataBindings.Add("Text", this.DataSource, "BGProduct." + Model.BGHandbookDetail1.PRO_ProName);
            this.TCFormat.DataBindings.Add("Text", this.DataSource, "BGProduct." + Model.BGHandbookDetail1.PRO_ProGuiGe);
            this.TCQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity, "{0:0.##}");
            this.TCQuantityUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceProductUnit);
            this.TCPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailPrice, "{0:0.####}");
            //this.TCPriceUnit.DataBindings.Add("Text",this.DataSource);
            this.TCAmount.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailMoney, "{0:0.##}");

            this.TCTotalQuantity.Summary.FormatString = "{0:0.##}";
            this.TCTotalQuantity.Summary.Func = SummaryFunc.Sum;
            this.TCTotalQuantity.Summary.IgnoreNullValues = true;
            this.TCTotalQuantity.Summary.Running = SummaryRunning.Report;
            this.TCTotalQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity, "{0:0.##}");
            this.TCTotalMoney.Summary.FormatString = "{0:0.##}";
            this.TCTotalMoney.Summary.Func = SummaryFunc.Sum;
            this.TCTotalMoney.Summary.IgnoreNullValues = true;
            this.TCTotalMoney.Summary.Running = SummaryRunning.Report;
            this.TCTotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceXODetail.PRO_InvoiceXODetailMoney, "{0:0.##}");

            this.lblAddress.Text = invoice.DeliveryAddress;
            this.lblPaymentWay.Text = invoice.PaymentWay;
            this.lblAccountNumber.Text = invoice.AccountNumber;
            this.lblPayee.Text = invoice.Payee;
            this.lblDepositBank.Text = invoice.DepositBank;
            this.lblDelivTime.Text = invoice.DeliveryDate;
            this.lblCheckRule.Text = invoice.CheckRule;
            this.lblOtherAppoint.Text = invoice.OtherAppoint;

            this.lblSupplierName.Text = BL.Settings.CompanyChineseName;
            this.lblSupplierAddress.Text = BL.Settings.CompanyAddress1;
            this.lblSupplierTel.Text = "TEL:" + BL.Settings.CompanyPhone + "  " + "FAX" + BL.Settings.CompanyFax;
            this.lblCustomerName.Text = invoice.xocustomer == null ? invoice.Customer.CustomerShortName : invoice.xocustomer.CustomerShortName;
            this.lblCustomerAddress.Text = invoice.xocustomer == null ? invoice.Customer.CustomerAddress : invoice.xocustomer.CustomerAddress;
        }
    }
}
