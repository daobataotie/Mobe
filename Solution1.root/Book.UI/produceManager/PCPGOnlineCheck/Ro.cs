using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class Ro : DevExpress.XtraReports.UI.XtraReport
    {
        BL.PCPGOnlineCheckManager _pcpgocm = new Book.BL.PCPGOnlineCheckManager();
        BL.PCPGOnlineCheckDetailManager _pcpgcDetailm = new Book.BL.PCPGOnlineCheckDetailManager();
        BL.ProduceOtherCompactManager _pocm = new Book.BL.ProduceOtherCompactManager();
        IList<Model.PCPGOnlineCheckDetail> _DDetails = new List<Model.PCPGOnlineCheckDetail>();

        public Ro(Model.PCPGOnlineCheck pcpgoc)
        {
            InitializeComponent();
            if (pcpgoc == null)
                return;

            this._DDetails = this._pcpgcDetailm.SelectByFromInvoiceId(pcpgoc.PCPGOnlineCheckId);
            this.DataSource = this._DDetails.OrderBy(d => d.PCPGOnlineCheckDetailDate).ToList();

            //CompanyInfo
            this.lblCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.PCPGOnlineCheck;
            //this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Control
            this.lblFromInvoiceId.Text = pcpgoc.FromPCId;
            this.lblCHCustomer.Text = pcpgoc.Customer == null ? "" : pcpgoc.Customer.ToString();
            this.lblCheckStandard.Text = pcpgoc.Customer == null ? "" : pcpgoc.Customer.CheckedStandard;
            this.lblCustomerInvoiceId.Text = pcpgoc.InvoiceCusXOId;
            this.lblEmployee.Text = pcpgoc.Employee == null ? "" : pcpgoc.Employee.ToString();
            this.lblProductName.Text = pcpgoc.Product == null ? "" : pcpgoc.Product.ToString();
            this.lblInvoiceXOQuantity.Text = pcpgoc.InvoiceXOQuantity.HasValue ? pcpgoc.InvoiceXOQuantity.Value.ToString() : "";
            if (pcpgoc.PCPGOnlineCheckType.Value > 0)
            {
                if (this._pocm.Get(pcpgoc.FromPCId) != null)
                    this.lblSupplier.Text = this._pocm.Get(pcpgoc.FromPCId).Supplier == null ? "" : this._pocm.Get(pcpgoc.FromPCId).Supplier.ToString();
            }
            else
                this.lblSupplier.Text = "";
            if (pcpgoc.Details.Count > 0)
                this.lblOrderCount.Text = pcpgoc.Details[0].CheckQuantity.HasValue ? pcpgoc.Details[0].CheckQuantity.Value.ToString() : "0";
            if (pcpgoc.PCPGOnlineCheckType > 0)
                this.lblFromInvoiceIDText.Text = "委外單編號";
            else
                this.lblFromInvoiceIDText.Text = "加工單編號";
            this.RichTextProductDesc.Rtf = pcpgoc.Product == null ? "" : pcpgoc.Product.ProductDescription;
            this.lbl_CustomerProductName.Text = pcpgoc.Product == null ? "" : pcpgoc.Product.CustomerProductName;

            //Detail
            this.TCPCPGOnlineCheckId.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_PCPGOnlineCheckId);
            this.TCattrDianDuBOLiTest.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrDianDuBOLiTest);
            this.TCattrDianDuPDSLv.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrDianDuPDSLv);
            this.TCattrExterior.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrExterior);
            this.TCattrQiangHuaMo.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrQiangHuaMo);
            //this.TCattrQiangHuaMoYingdu.DataBindings.Add("Text",this.DataSource,Model.PCPGOnlineCheckDetail.PRO_attrQiangHuaMoYingDu);
            this.TCFangWuMo.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrFangWuMo);
            //this.TCattrFangWuMoYingDu.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrFangWuMoYingDu);
            this.TCattrGaoDiJiaoL.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGaoDiJiaoL);
            this.TCattrGaoDiJiaoR.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGaoDiJiaoR);
            //this.TCattrGuanXue.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrGuanXue);
            //this.TCattrHouDu.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrHouDu);
            this.TCattrMaoBian.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrMaoBian);
            this.TCattrTouShiLv.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrTouShiLv);
            this.TCattrUVChengFen.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrUVChengFen);
            //this.TCattrZhePian.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrZhePian);
            this.TCattrZhuangJiaoSJDL.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_attrZhuangJiaoSJDL);
            this.TCCheckQuantity.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_CheckQuantity);
            this.TCImpactCheck.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_ImpactCheck);
            this.TCPCPGOnlineCheckDetailDate.DataBindings.Add("Text", this.DataSource, Model.PCPGOnlineCheckDetail.PRO_PCPGOnlineCheckDetailDate, "{0:yyyy-MM-dd HH:mm:ss}");
            this.xrtBusinessHours.DataBindings.Add("Text", this.DataSource, "BusinessHours." + Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);

            this.subReportGX.ReportSource = new subReportGX();
            this.subReportHD.ReportSource = new subReportHD();
        }

        private void subReportGX_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            subReportGX subGX = this.subReportGX.ReportSource as subReportGX;
            if (this.GetCurrentRow() != null)
                subGX._PCPGOnlineCheckDetailId = (this.GetCurrentRow() as Model.PCPGOnlineCheckDetail).PCPGOnlineCheckDetailId;
        }

        private void subReportHD_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            subReportHD subHD = this.subReportHD.ReportSource as subReportHD;
            if (this.GetCurrentRow() != null)
                subHD._PCPGOnlineCheckDetailId = (this.GetCurrentRow() as Model.PCPGOnlineCheckDetail).PCPGOnlineCheckDetailId;
        }
    }
}
