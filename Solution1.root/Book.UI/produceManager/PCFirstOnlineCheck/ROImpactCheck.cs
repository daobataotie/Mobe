using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.PCFirstOnlineCheck
{
    public partial class ROImpactCheck : DevExpress.XtraReports.UI.XtraReport
    {
        BL.PCImpactCheckManager manager = new Book.BL.PCImpactCheckManager();
        Model.PCImpactCheck _pcic = new Book.Model.PCImpactCheck();

        public ROImpactCheck()
        {
            InitializeComponent();

            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(ROImpactCheck_BeforePrint);

            //CompanyInfo
            //this.lblCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            //this.lblDataName.Text = Properties.Resources.PCImpactCheck;
            //this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Control
            this.lblPCImpactCheckId.Text = _pcic.PCImpactCheckId;
            this.lblPCImpactCheckDate.Text = _pcic.PCImpactCheckDate.HasValue ? _pcic.PCImpactCheckDate.Value.ToShortDateString() : "";
            this.lblInvoiceCusXOId.Text = _pcic.InvoiceCusXOId;
            this.lblPronoteHeaderId.Text = _pcic.PronoteHeaderId;
            //this.lblPCImpactCheckDesc.Text = _pcic.PCImpactCheckDesc;
            this.lblPCImpactCheckQuantity.Text = _pcic.PCImpactCheckQuantity.HasValue ? _pcic.PCImpactCheckQuantity.Value.ToString() : "";
            //this.lblEmployeeId.Text = _pcic.Employee == null ? "" : _pcic.Employee.ToString();
            this.lblWorkHouseId.Text = _pcic.WorkHouse == null ? "" : _pcic.WorkHouse.ToString();
            this.lblProductName.Text = _pcic.Product == null ? "" : _pcic.Product.ToString();
            this.lblDanJuTest.Text = _pcic.PCFromType > 0 ? "委外單據編號:" : "加工單據編號:";
            this.lblCheckStandard.Text = _pcic.mCheckStandard;      //质检标准
            this.lblInvoiceXOQuantity.Text = _pcic.InvoiceXOQuantity.HasValue ? _pcic.InvoiceXOQuantity.Value.ToString() : "";
            this.lbl_ProuductUnit.Text = _pcic.ProductUnit == null ? "" : _pcic.ProductUnit.ToString();
            //this.lbl_CustomerProductName.Text = _pcic.Product == null ? "" : _pcic.Product.CustomerProductName;
            if (_pcic.Product != null)
            {
                if (string.IsNullOrEmpty(_pcic.Product.CustomerProductName))
                    this.lbl_CustomerProductName.Text = new Help().GetCustomerProductNameByPronoteHeaderId(_pcic.PronoteHeaderId, _pcic.ProductId);
                else
                    this.lbl_CustomerProductName.Text = _pcic.Product.CustomerProductName;
            }

            this.lbl_MaterialUnit.Text = _pcic.MaterialUnit;

            //Details
            this.TCattrDate.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_attrDate, "{0:yyyy-MM-dd  HH:mm:dd}");
            this.TCattrGlassUpL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassUpLDis);
            this.TCattrGlassUpR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassUpRDis);
            this.TCattrGlassDownL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassDownLDis);
            this.TCattrGlassDownR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassDownRDis);
            this.TCattrGlassLeftL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassLeftLDis);
            this.TCattrGlassLeftR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassLeftRDis);
            this.TCattrGlassRightL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassRightLDis);
            this.TCattrGlassRightR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGlassRightRDis);
            this.TCattrCentralL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrCentralLDis);
            this.TCattrCentralR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrCentralRDis);
            this.TCattrNoseCentral.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrNoseCentralDis);
            this.TCattrGuanZui.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrGuanZuiDis);
            this.TCattrJieHenL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrJieHenLDis);
            this.TCattrJieHenR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrJieHenRDis);
            this.TCattrWingL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrWingLDis);
            this.TCattrWingR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrWingRDis);
            this.TCattr_15L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr_15LDis);
            this.TCattr_15R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr_15RDis);
            this.TCattr0L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr0LDis);
            this.TCattr0R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr0RDis);
            this.TCattr15L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr15LDis);
            this.TCattr15R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr15RDis);
            this.TCattr30L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr30LDis);
            this.TCattr30R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr30RDis);
            this.TCattr45L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr45LDis);
            this.TCattr45R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr45RDis);
            //this.TCattr60L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr60LDis);
            //this.TCattr60R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr60RDis);
            //this.TCattr75L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr75LDis);
            //this.TCattr75R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr75RDis);
            this.TCattr90L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr90LDis);
            this.TCattr90R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr90RDis);
            this.TCattrFootL.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrFootLDis);
            this.TCattrFootR.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrFootRDis);
            //this.lblBanbie.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_attrBanBie);
            this.RT_retest.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_AttrRetestDis);
            this.xrTBanBie.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_attrBanBie);
            //this.lblNote.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Note);
            this.lblNote.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_NoteShowPrint);


            this.TCHM500L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_HM500gLDis);
            this.TCHM500R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_HM500gRDis);
            this.TCZhuiqiu68L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Zhuiqiu68gLDis);
            this.TCZhuiqiu68R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Zhuiqiu68gRDis);
            this.TCZhuiqiu44L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Zhuiqiu44gLDis);
            this.TCZhuiqiu44R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Zhuiqiu44gRDis);
            this.TCZhuiqiu43L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Zhuiqiu43gLDis);
            this.TCZhuiqiu43R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Zhuiqiu43gRDis);
            this.TCZhuiqiu42L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Zhuiqiu42gLDis);
            this.TCZhuiqiu42R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Zhuiqiu42gRDis);
            this.TCChuantou442L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Chuantou442gLDis);
            this.TCChuantou442R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Chuantou442gRDis);

        }

        public string PCFirstOnlineCheckDetailId { get; set; }

        void ROImpactCheck_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //this.DataSource = manager.PFCSelect(PCFirstOnlineCheckDetailId);   //一般对应一张冲击测试单据

            _pcic = manager.PFCGetFirst(PCFirstOnlineCheckDetailId);
            if (_pcic != null)
            {
                _pcic = manager.GetDetail(_pcic.PCImpactCheckId);
                this.DataSource = _pcic.Details.OrderBy(d => d.attrDate).ToList();
            }
            else
            {
                _pcic = new Book.Model.PCImpactCheck();
            }
        }

    }
}
