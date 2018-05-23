using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        IList<Model.ANSIPCImpactCheckDetail> details = new List<Model.ANSIPCImpactCheckDetail>();
        BL.ANSIPCImpactCheckDetailManager ansipcicManager = new Book.BL.ANSIPCImpactCheckDetailManager();

        public RO(Model.ANSIPCImpactCheck ANSIPCIC)
        {
            InitializeComponent();

            if (ANSIPCIC == null)
                return;

            details = ansipcicManager.SelectByAnispcicID(ANSIPCIC.ANSIPCImpactCheckID);
            this.DataSource = details.OrderBy(d => d.attrDate).ToList();

            //CompanyInfo
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;

            if (ANSIPCIC.ForANSIOrJIS == "JIS")
            {
                this.lblDataName.Text = Properties.Resources.JISCheck;
                this.headTCZhuiQiu.Text = "墜球44g";
                this.xrLabel13.Text = "QW-06附表5";
            }
            else
                this.lblDataName.Text = Properties.Resources.ANSIPCImpactCheck;

            //this.lblPriteDate.Text += DateTime.Now.ToShortDateString();

            //Control
            this.lblANSIPCImpactCheckID.Text = ANSIPCIC.ANSIPCImpactCheckID;
            this.lblANSIPCImpactCheckDate.Text = ANSIPCIC.ANSIPCImpactCheckDate.HasValue ? ANSIPCIC.ANSIPCImpactCheckDate.Value.ToString("yyyy-MM-dd") : "";
            this.lblInvoiceCusXOId.Text = ANSIPCIC.InvoiceCusXOId;
            this.lblPronoteHeaderId.Text = ANSIPCIC.PronoteHeaderId;
            this.lblProduct.Text = ANSIPCIC.Product == null ? "" : ANSIPCIC.Product.ToString();
            this.lblEmployee0.Text = ANSIPCIC.Employee == null ? "" : ANSIPCIC.Employee.ToString();
            this.RTDescript.Text = ANSIPCIC.ANSIPCImpactCheckDesc;
            this.lblPrintDesc.Text = ANSIPCIC.PrintDesc1;
            this.lblChongJILiDu.Text = ANSIPCIC.PowerImpact;
            if (!string.IsNullOrEmpty(ANSIPCIC.ZhuiQiuKG))
                this.headTCZhuiQiu.Text = ANSIPCIC.ZhuiQiuKG;
            this.lblCheckStandard.Text = ANSIPCIC.CheckStandard;
            this.lblPCCheckCount.Text = ANSIPCIC.ANSIPCImpactCheckCount.HasValue ? ANSIPCIC.ANSIPCImpactCheckCount.ToString() : "";
            this.lblInvoiceXOQuantity.Text = ANSIPCIC.InvoiceXOQuantity.HasValue ? ANSIPCIC.InvoiceXOQuantity.ToString() : "";
            this.lblUnit.Text = ANSIPCIC.Unit == null ? null : ANSIPCIC.Unit.ToString();
            //Details
            #region 更改详细显示
            foreach (Model.ANSIPCImpactCheckDetail detail in details)
            {
                foreach (var a in detail.GetType().GetProperties())
                {
                    if (a.Name.IndexOf("attr") > -1)
                    {
                        if (a.GetValue(detail, null) != null)
                        {
                            switch (a.GetValue(detail, null).ToString())
                            {
                                case "0":
                                    a.SetValue(detail, "√", null);
                                    break;
                                case "1":
                                    a.SetValue(detail, "△", null);
                                    break;
                                case "2":
                                    a.SetValue(detail, "X", null);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            #endregion
            this.TCattrDate.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attrDate, "{0:yyyy-MM-dd HH:mm:ss}");
            this.TCattrHM500gL.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attrHM500gL);
            this.TCattrHM500gR.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attrHM500gR);
            this.TCattrZhuiQiu68gL.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attrZhuiQiu68gL);
            this.TCattrZhuiQiu68gR.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attrZhuiQiu68gR);
            this.TCattrChuanTou44_2gL.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attrChuanTou44_2gL);
            this.TCattrChuanTou44_2gR.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attrChuanTou44_2gR);
            this.TCattr_15L.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr_15L);
            this.TCattr_15R.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr_15R);
            this.TCattr0L.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr0L);
            this.TCattr0R.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr0R);
            this.TCattr15L.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr15L);
            this.TCattr15R.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr15R);
            this.TCattr30L.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr30L);
            this.TCattr30R.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr30R);
            this.TCattr45L.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr45L);
            this.TCattr45R.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr45R);
            this.TCattr60L.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr60L);
            this.TCattr60R.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr60R);
            this.TCattr75L.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr75L);
            this.TCattr75R.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr75R);
            this.TCattr90L.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr90L);
            this.TCattr90R.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr90R);
            this.TCattr90cDown10mmL.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr90cDown10mmL);
            this.TCattr90cDown10mmR.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr90cDown10mmR);
            this.TCattr90cUp10mmL.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr90cUp10mmL);
            this.TCattr90cUp10mmR.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attr90cUp10mmR);

            this.TCEmployee.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrBanBie.DataBindings.Add("Text", this.DataSource, Model.ANSIPCImpactCheckDetail.PRO_attrBanBie);
        }
    }
}
