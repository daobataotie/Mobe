using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
namespace Book.UI.Accounting.Report
{
    public partial class XRTayTo : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtSummonDetailManager detailManager = new Book.BL.AtSummonDetailManager();
        IList<Model.AtSummonDetail> oList = new List<Model.AtSummonDetail>();
        public XRTayTo(ConditionTryTo condition)
        {
            InitializeComponent();
            decimal? bb = 0;
            decimal? cc = 0;
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "試算表";
            IList<Model.AtSummonDetail> list = detailManager.Select(condition.StartDate, condition.EndDate,null,null);
            this.xrLabel1.Text = "列表日期：" + DateTime.Now.ToShortDateString();
            this.xrLabel2.Text = "日期區間：" + condition.StartDate.ToShortDateString() + "至" + condition.EndDate.ToShortDateString();

            if (list != null)
            {
                foreach (Model.AtSummonDetail at in list)
                {
                   
                    Model.AtAccountSubject atSub = new BL.AtAccountSubjectManager().Get(at.SubjectId);
                    if (atSub != null)
                    {
                        at.B = atSub.SubjectName;
                        at.A = atSub.Id;
                    }
                    at.E = at.DebitMoney;
                    at.F = at.CreditMoney;
                    bb += at.E;
                    cc += at.F;
                    oList.Add(at);
                }
            }
            this.DataSource = oList;
            this.xrLabel3.Text =  bb.ToString();
            this.xrLabel4.Text =  cc.ToString();
            
            this.xrTableCell5.DataBindings.Add("Text", this.DataSource, "A");

            this.xrTableCell6.DataBindings.Add("Text", this.DataSource, "B");
            this.xrTableCell7.DataBindings.Add("Text", this.DataSource, "E", "{0:0}");
            this.xrTableCell8.DataBindings.Add("Text", this.DataSource, "F", "{0:0}");
        }

    }
}
