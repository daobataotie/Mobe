using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AcInvoiceCOBill
{
    public partial class Ro : DevExpress.XtraReports.UI.XtraReport
    {
        public Ro(Model.AcInvoiceCOBill acInvoiceCOBill)
        {
            InitializeComponent();
            //报表标题
            this.ReportName.Text = BL.Settings.CompanyChineseName;
            //报表标题
            this.ReportTitle.Text = Properties.Resources.AcInvoiceCOBill;
            //列印日期
            this.ReportDate.Text += System.DateTime.Now.ToShortDateString();
            //头资料
            this.AcInvoiceCOBillId.Text = acInvoiceCOBill.AcInvoiceCOBillId;
            this.AcInvoiceCOBillDate.Text = acInvoiceCOBill.AcInvoiceCOBillDate.Value.ToString("yyyy-MM-dd");
            this.AcInvoiceCOBillDesc.Text = acInvoiceCOBill.AcInvoiceCOBillDesc;
            this.Employee.Text = string.IsNullOrEmpty(acInvoiceCOBill.EmployeeId) ? "" : acInvoiceCOBill.Employee.ToString();
            this.Employee0.Text = string.IsNullOrEmpty(acInvoiceCOBill.Employee1Id) ? "" : acInvoiceCOBill.Employee1.ToString();
            this.Employee1.Text = string.IsNullOrEmpty(acInvoiceCOBill.Employee0Id) ? "" : acInvoiceCOBill.Employee0.ToString();
            this.Supplier.Text = acInvoiceCOBill.Supplier.ToString();
            this.Sup_Supplier.Text = string.IsNullOrEmpty(acInvoiceCOBill.Sup_SupplierId) ? "" : acInvoiceCOBill.Sup_Supplier.ToString();
            this.TaxRate.Text = acInvoiceCOBill.TaxRate.ToString();
            this.TaxRateType.Text = acInvoiceCOBill.TaxRateType.ToString();
            this.TaxRateMoney.Text = acInvoiceCOBill.TaxRateMoney.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.ZongMoney.Text = acInvoiceCOBill.ZongMoney.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.lblmHeXiaoJingE.Text = acInvoiceCOBill.mHeXiaoJingE.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.lblNoHeXiaoTotal.Text = acInvoiceCOBill.NoHeXiaoTotal.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            //绑定详细数据源
            this.DataSource = acInvoiceCOBill.Details;
            //详细资料
            this.CellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceCOBillDetail.PRO_InvoiceId);
            this.CellInvoiceAllowance.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceCOBillDetail.PRO_InvoiceAllowance);
            this.CellProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.CellInvoiceCGDetaiInQuantity.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceCOBillDetail.PRO_InvoiceCGDetaiInQuantity);
            this.CellInvoiceCGDetailMoney.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceCOBillDetail.PRO_InvoiceCGDetailMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.CellInvoiceCGDetailPrice.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceCOBillDetail.PRO_InvoiceCGDetailPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSDJXiao.Value));
            this.CellInvoiceCGDetailTax.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceCOBillDetail.PRO_InvoiceCGDetailTax);
            this.CellInvoiceCGDetailTaxMoney.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceCOBillDetail.PRO_InvoiceCGDetailTaxMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));
            this.CellInvoiceCGDetailTaxPrice.DataBindings.Add("Text", this.DataSource, Model.AcInvoiceCOBillDetail.PRO_InvoiceCGDetailTaxPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSDJXiao.Value));

        }

    }
}
