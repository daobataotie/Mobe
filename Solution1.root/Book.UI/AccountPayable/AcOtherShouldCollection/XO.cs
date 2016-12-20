using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AcOtherShouldCollection
{
    public partial class XO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.AcOtherShouldCollectionManager acOtherShouldCollectionManager = new Book.BL.AcOtherShouldCollectionManager();
        private BL.AcOtherShouldCollectionDetailManager acOtherShouldCollectionDetailManager = new Book.BL.AcOtherShouldCollectionDetailManager();
        private Model.AcOtherShouldCollection acOtherShouldCollection;
        public XO(string acOtherShouldCollectionId)
        {
            InitializeComponent();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "其他應收款";
            this.xrLabelPrintDate.Text = "列表日期： " + DateTime.Now.ToShortDateString();
            this.acOtherShouldCollection = this.acOtherShouldCollectionManager.Get(acOtherShouldCollectionId);
            if (this.acOtherShouldCollection == null)
                return;
            this.acOtherShouldCollection.Details = this.acOtherShouldCollectionDetailManager.Select(this.acOtherShouldCollection);
            this.DataSource = this.acOtherShouldCollection.Details;
            if (this.acOtherShouldCollection.Customer != null)
            {
                this.xrLabelCustomerId.Text = this.acOtherShouldCollection.Customer.CustomerShortName;
            }
            this.xrLabelId.Text = this.acOtherShouldCollection.AcOtherShouldCollectionId;
            this.xrLabelAcOtherShouldCollectionDate.Text = this.acOtherShouldCollection.AcOtherShouldCollectionDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelAdvancePayableDate.Text = this.acOtherShouldCollection.AdvancePayableDate.Value.ToString("yyyy-MM-dd");
            if (this.acOtherShouldCollection.Employee0 != null)
            {
                this.xrLabelEmployee0Id.Text = this.acOtherShouldCollection.Employee0.EmployeeName;
            }
            if (this.acOtherShouldCollection.Employee != null)
            {
                this.xrLabelEmployeeId.Text = this.acOtherShouldCollection.Employee.EmployeeName;
            }
            if (this.acOtherShouldCollection.Employee1 != null)
            {
                this.xrLabelEmployee1Id.Text = this.acOtherShouldCollection.Employee1.EmployeeName;
            }
            //if (this.acOtherShouldCollection.AtCurrencyCategory != null)
            //{
            //    this.xrLabelAtCurrencyCategoryId.Text = this.acOtherShouldCollection.AtCurrencyCategory.AtCurrencyName;
            //}
            this.xrLabelAcDesc.Text = this.acOtherShouldCollection.AcDesc;
            //  this.xrLabelExchangeRate.Text = this.acOtherShouldCollection.ExchangeRate.ToString();
            this.lblHeJi.Text = this.acOtherShouldCollection.HeJi.ToString();
            //  this.xrLabelInvoiceId.Text = this.acOtherShouldCollection.InvoiceId;
            this.lblObjectName.Text = this.acOtherShouldCollection.ObjectName;
            this.lblInvoiceHeJi.Text = this.acOtherShouldCollection.InvoiceHeji.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.lblInvoiceTax.Text = this.acOtherShouldCollection.InvoiceTax.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.lblInvoiceTaxrate.Text = this.acOtherShouldCollection.InvoiceTaxrate.Value.ToString();
            switch (this.acOtherShouldCollection.TaxCaluType)
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
            //  this.xrTableCellAcOtherShouldCollectionId.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldCollectionDetail.PRO_AcOtherShouldCollectionId);
            this.xrTableCellLoanName.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldCollectionDetail.PRO_LoanName);

            this.xrTableCellSubjectId.DataBindings.Add("Text", this.DataSource, "Subject." + Model.AtAccountSubject.PRO_SubjectName);
            this.xrTableCellDetailDesc.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldCollectionDetail.PRO_DetailDesc);
            this.xrTableCellAcMoney.DataBindings.Add("Text", this.DataSource, Model.AcOtherShouldCollectionDetail.PRO_AcMoney,global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
        }

    }
}
