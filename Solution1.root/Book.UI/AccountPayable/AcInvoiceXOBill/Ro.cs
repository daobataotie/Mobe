using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    public partial class Ro : DevExpress.XtraReports.UI.XtraReport
    {
        public Ro(Model.AcInvoiceXOBill acInvoiceXOBill)
        {
            InitializeComponent();

            //报表标题
            this.ReportName.Text = BL.Settings.CompanyChineseName;
            this.ReportTitle.Text = Properties.Resources.AcInvoiceXOBill;
            //列印日期
            this.ReportDate.Text += System.DateTime.Now.ToShortDateString();
            //头资料
            this.AcInvoiceXOBillId.Text = acInvoiceXOBill.AcInvoiceXOBillId;
            this.lblfpbh.Text = acInvoiceXOBill.Id;
            this.RepDate.Text = acInvoiceXOBill.AcInvoiceXOBillDate.Value.ToString("yyyy-MM-dd");
            this.lblBeiZhu.Text = acInvoiceXOBill.AcInvoiceXOBillDesc;
            //this.newChooseCustomerId.Text = acInvoiceXOBill.Employee.ToString();
            this.newChooseCustomerId.Text = acInvoiceXOBill.Customer.CustomerShortName;
            //this.newChooseEmployee0Id.Text = acInvoiceXOBill.Employee0.ToString();
            this.newChooseEmployeeId.Text = string.IsNullOrEmpty(acInvoiceXOBill.EmployeeId) ? "" : acInvoiceXOBill.Employee.ToString();
            this.newChooseEmployee1Id.Text = string.IsNullOrEmpty(acInvoiceXOBill.Employee1Id) ? "" : acInvoiceXOBill.Employee1.ToString();
            this.newChooseEmployee0Id.Text = string.IsNullOrEmpty(acInvoiceXOBill.Employee0Id) ? "" : acInvoiceXOBill.Employee0.ToString();

            this.calcTaxRate.Text = acInvoiceXOBill.TaxRate.ToString();
            this.calcTaxRateMoney.Text = acInvoiceXOBill.TaxRateMoney.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.calcShuiBie.Text = acInvoiceXOBill.TaxRateType.ToString();
            this.calcHeJi.Text = acInvoiceXOBill.HeJiMoney.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value)); ;
            this.calcZongE.Text = acInvoiceXOBill.ZongMoney.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.lblYSDate.Text = acInvoiceXOBill.YSDate == null ? "" : acInvoiceXOBill.YSDate.Value.ToString();
            this.lblmHeXiaoJingE.Text = (acInvoiceXOBill.mHeXiaoJingE == null ? 0 : acInvoiceXOBill.mHeXiaoJingE.Value).ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.lblNoHeXiaoTotal.Text = (acInvoiceXOBill.NoHeXiaoTotal == null ? 0 : acInvoiceXOBill.NoHeXiaoTotal.Value).ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            //绑定详细数据源
            this.DataSource = acInvoiceXOBill.Details;
            //详细资料
            this.CellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceId);
            this.CellProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.CellInvoiceAllowance.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceAllowance);
            this.CellInvoiceXODetailPrice.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSDJXiao.Value));
            this.CellInvoiceXODetaiInQuantity.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetaiInQuantity);
            this.CellInvoiceXODetailMoney.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.CellInvoiceXODetailTax.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailTax, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.CellInvoiceXODetailTaxMoney.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailTaxMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.CellInvoiceXODetailTaxPrice.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceXOBillDetail.PRO_InvoiceXODetailTaxPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSDJXiao.Value));
        }

    }
}
