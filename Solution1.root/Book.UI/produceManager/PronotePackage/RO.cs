using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Book.UI.Query;

namespace Book.UI.produceManager.PronotePackage
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.PronotePackageManager pronotePackageManager = new BL.PronotePackageManager();
        public RO(ConditionA condition)
        {
            InitializeComponent();
            this.DataSource = this.pronotePackageManager.SelectByDateRange(Convert.ToDateTime(condition.StartDate.ToString("yyyy-MM") + "-01"), Convert.ToDateTime(condition.EndDate.ToString("yyyy-MM") + "-01"));
            this.ReaportName.Text = BL.Settings.CompanyChineseName;
            this.ReportTitle.Text = Properties.Resources.PronotePackageReport;
            this.ReportDate.Text += System.DateTime.Now;
            this.xrTableCellBadPercent.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_BadPercent);
            this.xrTableCellBadProductNum.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_BadProductNum);
            this.xrTableCellBlackPoint.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_BlackPoint);
            this.xrTableCellBox.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_Box);
            this.xrTableCellCaShang.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_CaShang);
            this.xrTableCellFeet.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_Feet);
            this.xrTableCellFullProductNum.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_FullProductNum);
            this.xrTableCellGuaShang.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_GuaShang);
            this.xrTableCellGuoHuo.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_GuoHuo);
            this.xrTableCellLiuHen.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_LiuHen);
            this.xrTableCellLouGuang.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_LouGuang);
            this.xrTableCellMianXu.DataBindings.Add("Text", this.dataBrowser, Model.PronotePackage.PRO_MianXu);
            this.xrTableCellOthers.DataBindings.Add("Text", this.dataBrowser, Model.PronotePackage.PRO_Others);
            this.xrTableCellPenYao.DataBindings.Add("Text", this.dataBrowser, Model.PronotePackage.PRO_PenYao);
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellPronoteHeaderId.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_PronoteHeaderId);
            this.xrTableCellPronotePackageDate.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_PronotePackageDate, "{0:yyyy-MM-dd}");
            this.xrTableCellQiPao.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_QiPao);
            this.xrTableCellSuoShui.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_SuoShui);
            this.xrTableCellTotal.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_Total);
            this.xrTableCellWanMo.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_WanMo);
            this.xrTableCellZaZhi.DataBindings.Add("Text", this.DataSource, Model.PronotePackage.PRO_ZaZhi);
        }

    }
}
