using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AcPayment
{
    public partial class XR : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly BL.AcPaymentManager AcPaymentManager = new Book.BL.AcPaymentManager();
        private BL.AcPaymentDetailManager AcPaymentDetailManager = new Book.BL.AcPaymentDetailManager();
        private Model.AcPayment AcPayment;
        public XR(string AcPaymentId)
        {
            InitializeComponent();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "¸¶¿î½YËã";
            this.xrLabelPrintDate.Text = DateTime.Now.ToShortDateString();

            this.AcPayment = this.AcPaymentManager.Get(AcPaymentId);

            if (this.AcPayment == null)
                return;

            this.AcPayment.Detail = this.AcPaymentDetailManager.Select(this.AcPayment);

            this.DataSource = this.AcPayment.Detail;
            this.xrLabelAcPaymentId.Text = this.AcPayment.AcPaymentId;
            this.xrLabelAcPaymentDate.Text = this.AcPayment.AcPaymentDate.Value.ToString("yyyy-MM-dd");
            if (this.AcPayment.Subject != null)
            {
                this.xrLabelSubjectId.Text = this.AcPayment.Subject.SubjectName;
            }
            if (this.AcPayment.Supplier != null)
            {
                this.xrLabelSupplierId.Text = this.AcPayment.Supplier.SupplierShortName;
            }
            if (this.AcPayment.AtCurrencyCategory != null)
            {
                this.xrLabelAtCurrencyCategoryId.Text = this.AcPayment.AtCurrencyCategory.AtCurrencyName;
            }
            if (this.AcPayment.PayMethod != null)
            {
                this.xrLabelPayMethodId.Text = this.AcPayment.PayMethod.PayMethodName;
            }
            if (this.AcPayment.Employee0 != null)
            {
                this.xrLabelEmployee0Id.Text = this.AcPayment.Employee0.EmployeeName;
            }
            if (this.AcPayment.Employee != null)
            {
                this.xrLabelEmployeeId.Text = this.AcPayment.Employee.EmployeeName;
            }
            if (this.AcPayment.Employee1 != null)
            {
                this.xrLabelEmployee1Id.Text = this.AcPayment.Employee1.EmployeeName;
            }

            this.xrLabelDomesticCashAgio.Text = this.AcPayment.DomesticCashAgio.ToString();
            this.xrLabelDomesticEealityPayment.Text = this.AcPayment.DomesticEealityPayment.ToString();
            this.xrLabelDomesticMayChargeMoney.Text = this.AcPayment.DomesticMayChargeMoney.ToString();
            this.xrLabelEealityPayment.Text = this.AcPayment.EealityPayment.ToString();

            this.xrLabelSubscriptionAdvancePayment.Text = this.AcPayment.SubscriptionAdvancePayment.ToString();
            this.xrLabelAcDesc.Text = this.AcPayment.AcDesc;
            this.xrLabelAdvancePaymentBalance.Text = this.AcPayment.AdvancePaymentBalance.ToString();

            this.xrLabelBankAccount.Text = this.AcPayment.BankAccount;
            this.xrLabelBillNo.Text = this.AcPayment.BillNo;


            //Ã÷Ï¸
            this.xrTableAcInvoiceId.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_AcInvoiceId);
            this.xrTableAcInvoiceType.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_AcInvoiceType);

            this.xrTableDetailCashAgio.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_DetailCashAgio, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableDomesticDetailCashAgio.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_DomesticDetailCashAgio, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

            this.xrTableDomesticNoPaymentMoney.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_DomesticNoPaymentMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableDomesticShouldPaymentMoney.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_DomesticShouldPaymentMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

            this.xrTableDomesticThisChargeMoney.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_DomesticThisChargeMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableNoPaymentMoney.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_NoPaymentMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableShouldPaymentMoney.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_ShouldPaymentMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

            this.xrTableThisChargeMoney.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_ThisChargeMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableBillId.DataBindings.Add("Text", this.DataSource, Model.AcPaymentDetail.PRO_BillId);
        }

    }
}
