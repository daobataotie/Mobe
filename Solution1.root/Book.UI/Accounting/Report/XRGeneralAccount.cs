using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Book.UI.Accounting.Report
{
    public partial class XRGeneralAccount : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtSummonDetailManager _atsummondetailmanager = new Book.BL.AtSummonDetailManager();
        IList<Model.AtSummonDetail> oList = new List<Model.AtSummonDetail>();
        BL.AtAccountSubjectManager _ataccountsubjectmanager = new Book.BL.AtAccountSubjectManager();
        IList<Model.AtAccountSubject> _calcSubjects = new List<Model.AtAccountSubject>();


        public XRGeneralAccount(ConditionGeneralAccount condition)
        {
            InitializeComponent();

            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "總分類帳表";

            IList<Model.AtAccountSubject> ataccountsubjects = this._ataccountsubjectmanager.selectById(condition.StartSubjectId, condition.EndSubjectId);
            if (ataccountsubjects == null || ataccountsubjects.Count == 0)
                return;
            foreach (Model.AtAccountSubject item in ataccountsubjects)
            {
                item.ZFLZ_Yue = (item.TheBalance.HasValue ? item.TheBalance.Value : 0) + this._atsummondetailmanager.GET_ZFLZ_Yue(item.SubjectId, condition.StartDate);
            }

            this.DataSource = ataccountsubjects;

            this.xrLabel1.Text = "列表日期：" + DateTime.Now.ToShortDateString();
            this.xrLabel2.Text = "日期區間：" + condition.StartDate.ToShortDateString() + "至" + condition.EndDate.ToShortDateString();

            this.xrSubreport1.ReportSource = new XRGeneralAccountDetail(condition.StartDate, condition.EndDate);
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRGeneralAccountDetail subreport = this.xrSubreport1.ReportSource as XRGeneralAccountDetail;
            Model.AtAccountSubject ataccountsubject = this.GetCurrentRow() as Model.AtAccountSubject;
            if (ataccountsubject != null)
            {
                subreport.ataccountsubject = ataccountsubject;
                this._calcSubjects.Add(subreport.ataccountsubject);
            }
        }

        private void XRGeneralAccount_AfterPrint(object sender, EventArgs e)
        {
            if (this._calcSubjects != null && this._calcSubjects.Count > 0)
            {
                //this.TCF_DaiFangJinE.Text = this._calcSubjects.Sum(d => d.ZFLZ_Dai).ToString();
                //this.TCF_JieFangjinE.Text = this._calcSubjects.Sum(d => d.ZFLZ_Jie).ToString();
                //this.TCF_YuE.Text = this._calcSubjects.Sum(d => d.ZFLZ_Yue).ToString();
            }
        }
    }
}
