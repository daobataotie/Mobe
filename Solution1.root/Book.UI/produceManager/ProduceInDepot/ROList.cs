using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceInDepot
{
    public partial class ROList : DevExpress.XtraReports.UI.XtraReport
    {
        public ROList()
        {
            InitializeComponent();
        }

        public ROList(IList<Model.ProduceInDepotDetail> details, int i)
            : this()
        {
            //Controls
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.ProduceInDepot + "列表";
            this.DataSource = details;
            if (i == 1)
            {
                this.lblReportName.Text = "生产日报表";
                this.lblReportDate.Visible = false;

            }
            else
                this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            //Details

            this.TCRiQi.DataBindings.Add("Text", this.DataSource, "mProduceInDepotDate", "{0:yyyy-MM-dd}");
            this.TCPinMing.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProductName);
            this.TCRuKuDanHao.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceInDepotId);
            this.TCJiaGongDanHao.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_PronoteHeaderId);
            this.TCBuMeng.DataBindings.Add("Text", this.DataSource, "WorkHousename");
            this.TCShenChanShuLiang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProceduresSum);
            this.TCHeGeShuLiang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_CheckOutSum);
            //this.TCZhuanShenChanShuLiang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceTransferQuantity);
            //this.TCRuKuShuLiang.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceQuantity);
            this.TCInvoiceCusId.DataBindings.Add("Text", this.DataSource, "CusXOId");
            this.TCOrderNum.DataBindings.Add("Text", this.DataSource, "PronoteHeaderSum");
            this.TCHejiShengchan.DataBindings.Add("Text", this.DataSource, "HeJiProceduresSum");
            this.TCHejiHege.DataBindings.Add("Text", this.DataSource, "HeJiCheckOutSum");

            this.TC_ZSCSL.DataBindings.Add("Text", this.DataSource, "SCSL");
        }
    }
}
