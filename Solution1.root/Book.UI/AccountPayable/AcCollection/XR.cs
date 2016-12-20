using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AcCollection
{
    public partial class XR : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly BL.AcCollectionManager AcCollectionManager = new Book.BL.AcCollectionManager();
        private BL.AcCollectionDetailManager AcCollectionDetailManager = new Book.BL.AcCollectionDetailManager();
        private Model.AcCollection AcCollection;
        public XR(string AcCollectionId)
        {
            InitializeComponent();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "ÊÕ¿î½YËã";
            this.xrLabelPrintDate.Text = DateTime.Now.ToShortDateString();

            this.AcCollection = this.AcCollectionManager.Get(AcCollectionId);

            if (this.AcCollection == null)
                return;

            this.AcCollection.Detail = this.AcCollectionDetailManager.Select(this.AcCollection);

            this.DataSource = this.AcCollection.Detail;
            this.xrLabelAcCollectionId.Text = this.AcCollection.AcCollectionId;
            this.xrLabelAcCollectionDate.Text = this.AcCollection.AcPaymentDate.Value.ToString("yyyy-MM-dd");
            if (this.AcCollection.Subject != null)
            {
                this.xrLabelSubjectId.Text = this.AcCollection.Subject.SubjectName;
            }
            if (this.AcCollection.Customer != null)
            {
                this.xrLabelCustomerId.Text = this.AcCollection.Customer.CustomerShortName;
            }
            if (this.AcCollection.AtCurrencyCategory != null)
            {
                this.xrLabelAtCurrencyCategoryId.Text = this.AcCollection.AtCurrencyCategory.AtCurrencyName;
            }
            if (this.AcCollection.PayMethod != null)
            {
                this.xrLabelPayMethodId.Text = this.AcCollection.PayMethod.PayMethodName;
            }
            if (this.AcCollection.Employee0 != null)
            {
                this.xrLabelEmployee0Id.Text = this.AcCollection.Employee0.EmployeeName;
            }
            if (this.AcCollection.Employee != null)
            {
                this.xrLabelEmployeeId.Text = this.AcCollection.Employee.EmployeeName;
            }
            if (this.AcCollection.Employee1 != null)
            {
                this.xrLabelEmployee1Id.Text = this.AcCollection.Employee1.EmployeeName;
            }
            this.xrLabelDomesticCashAgio.Text = this.AcCollection.DomesticCashAgio.ToString();
            this.xrLabelDomesticEealityPayment.Text = this.AcCollection.DomesticEealityCollection.ToString();
            this.xrLabelDomesticMayChargeMoney.Text = this.AcCollection.DomesticMayChargeMoney.ToString();
            this.xrLabelEealityPayment.Text = this.AcCollection.EealityCollection.ToString();
            this.xrLabelJoinAdvancePayment.Text = this.AcCollection.JoinAdvanceCollection.ToString();
            this.xrLabelSubscriptionAdvancePayment.Text = this.AcCollection.SubscriptionAdvanceCollection.ToString();
            this.xrLabelAcDesc.Text = this.AcCollection.AcDesc;
            this.xrLabelAdvancePaymentBalance.Text = this.AcCollection.AdvanceCollectionBalance.ToString();
            this.xrLabelAlreadyChargeMoney.Text = this.AcCollection.AlreadyChargeMoney.ToString();
            this.xrLabelBankAccount.Text = this.AcCollection.BankAccount;
            this.xrLabelBillNo.Text = this.AcCollection.BillNo;
            this.xrLabelCashAgio.Text = this.AcCollection.CashAgio.ToString();

            //Ã÷Ï¸
            this.xrTableAcInvoiceId.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_AcInvoiceId);
            this.xrTableAcInvoiceType.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_AcInvoiceType);

            this.xrTableDetailCashAgio.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_DetailCashAgio, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableDomesticDetailCashAgio.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_DomesticDetailCashAgio, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

            this.xrTableDomesticNoPaymentMoney.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_DomesticNoPaymentMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableDomesticShouldPaymentMoney.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_DomesticShouldCollectionMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

            this.xrTableDomesticThisChargeMoney.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_DomesticThisChargeMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableMayChargeMoney.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_MayChargeMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

            this.xrTableNoPaymentMoney.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_NoCollectionMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableShouldPaymentMoney.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_ShouldCollectionMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

            this.xrTableThisChargeMoney.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_ThisChargeMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrTableBillId.DataBindings.Add("Text", this.DataSource, Model.AcCollectionDetail.PRO_BillId);
        }

    }
}
