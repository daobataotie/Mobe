using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.PCFirstOnlineCheck
{
    public partial class ROImpactCheckSub : DevExpress.XtraReports.UI.XtraReport
    {
        BL.PCImpactCheckManager manager = new Book.BL.PCImpactCheckManager();

        public ROImpactCheckSub()
        {
            InitializeComponent();

            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(ROImpactCheck_BeforePrint);

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
            this.TCattr0L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr0LDis);
            this.TCattr0R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr0RDis);
            this.TCattr30L.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr30LDis);
            this.TCattr30R.DataBindings.Add("Text", this.DataSource, Model.PCImpactCheckDetail.PRO_Attr30RDis);
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

            Model.PCImpactCheck _pcic = manager.PFCGetFirst(PCFirstOnlineCheckDetailId);
            if (_pcic != null)
            {
                _pcic = manager.GetDetail(_pcic.PCImpactCheckId);
                this.DataSource = _pcic.Details.OrderBy(d => d.attrDate).ToList();

            }
            else
            {
                _pcic = new Book.Model.PCImpactCheck();
                this.DataSource = new List<Model.PCImpactCheckDetail>();
            }
        }

    }
}
