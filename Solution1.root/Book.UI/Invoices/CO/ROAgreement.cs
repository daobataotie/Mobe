using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.CO
{
    public partial class ROAgreement : DevExpress.XtraReports.UI.XtraReport
    {
        public ROAgreement()
        {
            InitializeComponent();
        }
        BL.BGHandbookDetail2Manager manager = new Book.BL.BGHandbookDetail2Manager();
        public ROAgreement(Model.InvoiceCO invoice)
            : this()
        {
            this.lbl_Supplier.Text = invoice.Supplier == null ? null : invoice.Supplier.ToString();
            this.lbl_Customer.Text = BL.Settings.CompanyChineseName;
            this.lbl_AgreementId.Text = invoice.SupplierLotNumber;
            this.lbl_Date.Text = invoice.InvoiceDate.Value.Year + "Äê" + invoice.InvoiceDate.Value.Month + "ÔÂ" + invoice.InvoiceDate.Value.Day + "ÈÕ";

            this.DataSource = invoice.Details;
            foreach (Model.InvoiceCODetail detail in invoice.Details)
            {
                detail.BGProduct = manager.SelectBGProduct(detail.HandbookId, detail.HandbookProductId);
            }

            this.TCId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_Inumber);
            this.TCName.DataBindings.Add("Text", this.DataSource, "BGProduct." + Model.BGHandbookDetail1.PRO_ProName);
            this.TCFormat.DataBindings.Add("Text", this.DataSource, "BGProduct." + Model.BGHandbookDetail1.PRO_ProGuiGe);
            this.TCQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_OrderQuantity, "{0:0.##}");
            this.TCQuantityUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceProductUnit);
            this.TCPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailPrice, "{0:0.####}");
            this.TCAmount.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_YiPrice, "{0:0.##}");

            this.TCTotalMoney.Summary.FormatString = "{0:0.##}";
            this.TCTotalMoney.Summary.Func = SummaryFunc.Sum;
            this.TCTotalMoney.Summary.IgnoreNullValues = true;
            this.TCTotalMoney.Summary.Running = SummaryRunning.Report;
            this.TCTotalMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_YiPrice, "{0:0.##}");

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
            this.lblCustomerName.Text = invoice.Supplier == null ? null : invoice.Supplier.SupplierShortName;
            this.lblCustomerAddress.Text = invoice.Supplier == null ? null : invoice.Supplier.CompanyAddress;
        }
    }
}
