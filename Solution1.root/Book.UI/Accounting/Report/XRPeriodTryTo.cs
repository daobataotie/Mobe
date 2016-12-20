using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
namespace Book.UI.Accounting.Report
{
    public partial class XRPeriodTryTo : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtSummonDetailManager detailManager = new Book.BL.AtSummonDetailManager();
        IList<Model.AtSummonDetail> oList = new List<Model.AtSummonDetail>();
        public XRPeriodTryTo(ConditionPeriodTryTo condition)
        {
            InitializeComponent();
            decimal? bb = 0;
            decimal? cc = 0;
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "期試算表";
            IList<Model.AtSummonDetail> list = detailManager.Select(condition.StartDate, condition.EndDate, null, null);
            this.xrLabel1.Text = "列表日期：" + DateTime.Now.ToShortDateString();
            this.xrLabel2.Text = "日期區間：" + condition.StartDate.ToShortDateString() + "至" + condition.EndDate.ToShortDateString();

            if (list != null)
            {
                foreach (Model.AtSummonDetail at in list)
                {
                    at.J = new BL.AtSummonDetailManager().CountSummonTo("借", at.SubjectId);
                    at.K = new BL.AtSummonDetailManager().CountSummonTo("貸", at.SubjectId);
                    
                    Model.AtAccountSubject atSub = new BL.AtAccountSubjectManager().Get(at.SubjectId);
                    if (atSub != null)
                    {
                        at.B = atSub.SubjectName;
                        at.A = atSub.Id;
                    }
                    at.E = at.DebitMoney;
                    at.F = at.CreditMoney;
                    at.G = at.E - at.F;
                    bb += at.E;
                    cc += at.F;
                    oList.Add(at);
                }
            }
            this.DataSource = oList;
            this.xrLabel3.Text = bb.ToString();
            this.xrLabel4.Text = cc.ToString();

            this.xrTableCell8.DataBindings.Add("Text", this.DataSource, "A");

            this.xrTableCell9.DataBindings.Add("Text", this.DataSource, "B");
            this.xrTableCell10.DataBindings.Add("Text", this.DataSource, "E", "{0:0}");
            this.xrTableCell11.DataBindings.Add("Text", this.DataSource, "F", "{0:0}");
            this.xrTableCell12.DataBindings.Add("Text", this.DataSource, "G", "{0:0}");
            this.xrTableCell13.DataBindings.Add("Text", this.DataSource, "J");
            this.xrTableCell14.DataBindings.Add("Text", this.DataSource, "K");
        }

    }
}
