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
    public partial class XRGeneralAccountDetail : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtSummonDetailManager detailManager = new Book.BL.AtSummonDetailManager();
        IList<Model.AtSummonDetail> oList = new List<Model.AtSummonDetail>();

        private readonly DateTime StartDate, EndDate;  //保存日期条件

        public XRGeneralAccountDetail()
        {
            InitializeComponent();
        }
        public XRGeneralAccountDetail(DateTime startdate, DateTime enddate)
            : this()
        {
            this.StartDate = startdate;
            this.EndDate = enddate;
            this.TC_riqi.DataBindings.Add("Text", this.DataSource, "Summon." + Model.AtSummon.PRO_SummonDate, "{0:yyyy-MM-dd}");
            this.TC_chuanpiaobianhao.DataBindings.Add("Text", this.DataSource, "Summon." + Model.AtSummon.PRO_Id);
            this.TC_zhaiyao.DataBindings.Add("Text", this.DataSource, Model.AtSummonDetail.PRO_Summary);
            this.TC_jiefangjine.DataBindings.Add("Text", this.DataSource, Model.AtSummonDetail.PRO_DebitMoney, "{0:0}");
            this.TC_daifangjine.DataBindings.Add("Text", this.DataSource, Model.AtSummonDetail.PRO_CreditMoney, "{0:0}");
            this.TC_yue.DataBindings.Add("Text", this.DataSource, Model.AtSummonDetail.PRO_YuE_flat, "{0:0}");
        }

        public Model.AtAccountSubject ataccountsubject { get; set; }
        public IList<Model.AtSummonDetail> datalist { get; set; }

        private void XRGeneralAccountDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.ataccountsubject != null)
            {
                this.TCH_chuanpiaobianhao.Text = this.ataccountsubject.Id;
                this.TCH_Zhaiyao.Text = this.ataccountsubject.SubjectName;
                this.TCH_YuE.Text = this.ataccountsubject.ZFLZ_Yue.Value.ToString("0.##");

                decimal? FootZongYuE = 0;
                if (this.ataccountsubject.Id == "1101000")
                {
                    datalist = this.detailManager.Select_ZFLZ_XianJinGroupSubject(this.ataccountsubject.SubjectId, this.StartDate, this.EndDate);
                }
                else
                {
                    datalist = this.detailManager.Select_ZFLZ_GroupSubject(this.ataccountsubject.SubjectId, this.StartDate, this.EndDate);
                }
                if (datalist != null && datalist.Count > 0)
                {
                    datalist.ToList<Model.AtSummonDetail>().ForEach(
                     d =>
                     {
                         if (d.Lending.Contains("貸") || d.Lending.Contains("贷"))
                         {
                             d.CreditMoney = d.AMoney;
                             d.DebitMoney = null;
                             this.ataccountsubject.ZFLZ_Dai += d.AMoney;
                             this.ataccountsubject.ZFLZ_Yue -= d.AMoney;
                             d.YuE_flat = this.ataccountsubject.ZFLZ_Yue;
                         }
                         else
                         {
                             d.CreditMoney = null;
                             d.DebitMoney = d.AMoney;
                             this.ataccountsubject.ZFLZ_Jie += d.AMoney;
                             this.ataccountsubject.ZFLZ_Yue += d.AMoney;
                             d.YuE_flat = this.ataccountsubject.ZFLZ_Yue;
                         }
                     });

                    this.DataSource = datalist;

                    FootZongYuE = datalist.Last<Model.AtSummonDetail>().YuE_flat;

                    this.TCF_DaiFangJinE.Text = this.ataccountsubject.ZFLZ_Dai.HasValue ? this.ataccountsubject.ZFLZ_Dai.Value.ToString("0.##") : "0";
                    this.TCF_JieFangjinE.Text = this.ataccountsubject.ZFLZ_Jie.HasValue ? this.ataccountsubject.ZFLZ_Jie.Value.ToString("0.##") : "0";
                    this.TCF_YuE.Text = FootZongYuE.HasValue ? FootZongYuE.Value.ToString("0.##") : "0";
                }
                else
                {
                    this.DataSource = datalist;
                    this.TCF_DaiFangJinE.Text = "";
                    this.TCF_JieFangjinE.Text = "";
                    this.TCF_YuE.Text = "";
                }
            }
            else
            {
                this.DataSource = datalist;
                this.TCF_DaiFangJinE.Text = "";
                this.TCF_JieFangjinE.Text = "";
                this.TCF_YuE.Text = "";
            }
        }
    }
}
