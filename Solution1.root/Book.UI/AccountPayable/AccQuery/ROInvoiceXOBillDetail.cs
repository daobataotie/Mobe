using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AccQuery
{
    public partial class ROInvoiceXOBillDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public ROInvoiceXOBillDetail()
        {
            InitializeComponent();
        }

        public ROInvoiceXOBillDetail(ConditionAcInvoiceXOBill condition)
            : this()
        {
            IList<Model.AcInvoiceXOBillDetail> mdetail = new BL.AcInvoiceXOBillDetailManager().selectByConditionInvoiceXODetail(condition.StartDate, condition.EndDate, condition.StartXOid, condition.EndXOid, condition.mStartCustomer, condition.mEndCustomer);

            if (mdetail == null || mdetail.Count == 0)
                throw new global::Helper.InvalidValueException();

            this.DataSource = mdetail;

            //Info
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.ROAcInvoiceXOBillDetail;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            //Bind
            this.TCdanjia.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSDJXiao.Value));
            this.TCdanwei.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceProductUnit);
            this.TCfapiaobianhao.DataBindings.Add("Text", this.DataSource, "AcInvoiceXOBill." + Model.AcInvoiceXOBill.PRO_AcInvoiceXOBillId);
            this.TCkehu.DataBindings.Add("Text", this.DataSource, "AcInvoiceXOBill.Customer." + Model.Customer.PRO_CustomerFullName);
            this.TCriqi.DataBindings.Add("Text", this.DataSource, "AcInvoiceXOBill." + Model.AcInvoiceXOBill.PRO_AcInvoiceXOBillDate, "{0:yyyy-MM-dd}");
            this.TCshuliang.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetaiInQuantity);
            this.TCxiaoji.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

            this.xrlblTotalShuiE.Summary.FormatString =  global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value);
            this.xrlblTotalShuiE.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalShuiE.Summary.IgnoreNullValues = true;
            this.xrlblTotalShuiE.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalShuiE.DataBindings.Add("Text", this.DataSource, "AcInvoiceXOBill." + Model.AcInvoiceXOBill.PRO_TaxRateMoney);

            this.xrlblTotalJinE.Summary.FormatString =  global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value);
            this.xrlblTotalJinE.Summary.Func = SummaryFunc.Sum;
            this.xrlblTotalJinE.Summary.IgnoreNullValues = true;
            this.xrlblTotalJinE.Summary.Running = SummaryRunning.Report;
            this.xrlblTotalJinE.DataBindings.Add("Text", this.DataSource, "AcInvoiceXOBill." + Model.AcInvoiceXOBill.PRO_ZongMoney);
        }

    }
}
