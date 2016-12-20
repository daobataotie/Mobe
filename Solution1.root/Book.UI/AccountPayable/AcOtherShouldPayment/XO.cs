using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AcOtherShouldPayment
{
    public partial class XO : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly BL.AcOtherShouldPaymentManager AcOtherShouldPaymentManager = new Book.BL.AcOtherShouldPaymentManager();
        private BL.AcOtherShouldPaymentDetailManager AcOtherShouldPaymentDetailManager = new Book.BL.AcOtherShouldPaymentDetailManager();
        private Model.AcOtherShouldPayment AcOtherShouldPayment;

        public XO(string AcOtherShouldPaymentId)
        {
            InitializeComponent();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "其他應付款";
            this.xrLabelPrintDate.Text = "列表日期： " + DateTime.Now.ToShortDateString();

            this.AcOtherShouldPayment = this.AcOtherShouldPaymentManager.Get(AcOtherShouldPaymentId);

            if (this.AcOtherShouldPayment == null)
                return;

            this.AcOtherShouldPayment.Details = this.AcOtherShouldPaymentDetailManager.Select(this.AcOtherShouldPayment);

            this.DataSource = this.AcOtherShouldPayment.Details;

            if (this.AcOtherShouldPayment.Supplier != null)
            {
                this.xrLabelCustomerId.Text = this.AcOtherShouldPayment.Supplier.SupplierShortName;
            }
            this.xrLabelId.Text = this.AcOtherShouldPayment.AcOtherShouldPaymentId;
            this.xrLabelAcOtherShouldPaymentDate.Text = this.AcOtherShouldPayment.AcOtherShouldPaymentDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelAdvancePayableDate.Text = this.AcOtherShouldPayment.AdvancePayableDate.Value.ToString("yyyy-MM-dd");
            if (this.AcOtherShouldPayment.Employee0 != null)
            {
                this.xrLabelEmployee0Id.Text = this.AcOtherShouldPayment.Employee0.EmployeeName;
            }
            if (this.AcOtherShouldPayment.Employee != null)
            {
                this.xrLabelEmployeeId.Text = this.AcOtherShouldPayment.Employee.EmployeeName;
            }
            if (this.AcOtherShouldPayment.Employee1 != null)
            {
                this.xrLabelEmployee1Id.Text = this.AcOtherShouldPayment.Employee1.EmployeeName;
            }
            //if (this.AcOtherShouldPayment.AtCurrencyCategory != null)
            //{
            //    this.xrLabelAtCurrencyCategoryId.Text = this.AcOtherShouldPayment.AtCurrencyCategory.AtCurrencyName;
            //}
            this.xrLabelAcDesc.Text = this.AcOtherShouldPayment.AcDesc;
            //   this.xrLabelExchangeRate.Text = this.AcOtherShouldPayment.ExchangeRate.ToString();
            this.lblHeJi.Text = this.AcOtherShouldPayment.HeJi.ToString();
            // this.xrLabelInvoiceId.Text = this.AcOtherShouldPayment.InvoiceId;
            this.xrLabelObjectName.Text = this.AcOtherShouldPayment.ObjectName;
            this.lblInvoiceHeJi.Text = this.AcOtherShouldPayment.InvoiceHeji.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.lblInvoiceTax.Text = this.AcOtherShouldPayment.InvoiceTax.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.lblInvoiceTaxrate.Text = this.AcOtherShouldPayment.InvoiceTaxrate.ToString();
            this.lblCompany.Text = this.AcOtherShouldPayment.Company == null ? "" : this.AcOtherShouldPayment.Company.ToString();
            switch (this.AcOtherShouldPayment.TaxCaluType)
            {
                case 0:
                    this.lblTaxCaluType.Text = "免稅";
                    break;
                case 1:
                    this.lblTaxCaluType.Text = "外加稅";
                    break;
                case 2:
                    this.lblTaxCaluType.Text = "內含稅";
                    break;
            }
            //明细
            // this.xrTableCellAcOtherShouldPaymentId.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldPaymentDetail.PRO_AcOtherShouldPaymentId);
            this.xrTableCellLoanName.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldPaymentDetail.PRO_LoanName);

            this.xrTableCellSubjectId.DataBindings.Add("Text", this.DataSource, "Subject." + Model.AtAccountSubject.PRO_SubjectName);
            this.xrTableCellDetailDesc.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldPaymentDetail.PRO_DetailDesc);
            this.xrTableCellAcMoney.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldPaymentDetail.PRO_AcMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableQuantity.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldPaymentDetail.PRO_AcQuantity);
            this.xrTablePrice.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldPaymentDetail.PRO_AcItemPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSDJXiao.Value));

            this.xrTableCellAcMoney.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldPaymentDetail.PRO_AcMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

        }

    }
}
