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

        public ROImpactCheck()
        {
            InitializeComponent();

            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(ROImpactCheck_BeforePrint);

            this.ImpackCheckBub.ReportSource = new ROImpactCheckSub();
        }

        public string PCFirstOnlineCheckDetailId { get; set; }

        void ROImpactCheck_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //this.DataSource = manager.PFCSelect(PCFirstOnlineCheckDetailId);   //一般对应一张冲击测试单据

            ROImpactCheckSub sub = this.ImpackCheckBub.ReportSource as ROImpactCheckSub;
            sub.PCFirstOnlineCheckDetailId = PCFirstOnlineCheckDetailId;

            //Model.PCImpactCheck _pcic = manager.PFCGetFirst(PCFirstOnlineCheckDetailId);
            //if (_pcic != null)
            //{
            //    _pcic = manager.GetDetail(_pcic.PCImpactCheckId);
            //    this.DataSource = _pcic.Details.OrderBy(d => d.attrDate).ToList();

            //}
            //else
            //{
            //    _pcic = new Book.Model.PCImpactCheck();
            //}
        }

    }
}
