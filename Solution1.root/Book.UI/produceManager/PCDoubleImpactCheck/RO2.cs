using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.PCDoubleImpactCheck
{
    public partial class RO2 : DevExpress.XtraReports.UI.XtraReport
    {
        IList<Model.PCDoubleImpactCheckDetail> details = new List<Model.PCDoubleImpactCheckDetail>();
        BL.PCDoubleImpactCheckDetailManager pcdicdManager = new Book.BL.PCDoubleImpactCheckDetailManager();

        public RO2(Model.PCDoubleImpactCheck _pcdic)
        {
            InitializeComponent();
            if (_pcdic == null)
                return;

            details = pcdicdManager.SelectByPCDoubleICId(_pcdic.PCDoubleImpactCheckID);
            this.DataSource = details.OrderBy(d => d.PCDoubleImpactCheckDetailDate).ToList();
            //this.DataSource = _pcdic.Detail;

            //CompanyInfo
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            switch (_pcdic.PCDoubleImpactCheckType)
            {
                case 0:
                    this.lblDataName.Text = Properties.Resources.CSAcjcsd;
                    //this.xtcHengWen.Visible = false;
                    //this.xtcJiaRe.Visible = false;
                    //this.TCattrHotL.Visible = false;
                    //this.TCattrHotR.Visible = false;
                    //this.TCattrCoolL.Visible = false;
                    //this.TCattrCoolR.Visible = false;
                    break;
                case 1:
                    this.lblDataName.Text = Properties.Resources.BSENcjcsd;
                    //this.xtcJIaoLian.Visible = false;
                    //this.TCattrJiaoLianL.Visible = false;
                    //this.TCattrJiaoLianR.Visible = false;
                    break;
                case 2:
                    this.lblDataName.Text = Properties.Resources.ASNZScjcsd;
                    //this.xtcJIaoLian.Visible = false;
                    //this.TCattrJiaoLianL.Visible = false;
                    //this.TCattrJiaoLianR.Visible = false;
                    //this.xtcHengWen.Visible = false;
                    //this.xtcJiaRe.Visible = false;
                    //this.TCattrHotL.Visible = false;
                    //this.TCattrHotR.Visible = false;
                    //this.TCattrCoolL.Visible = false;
                    //this.TCattrCoolR.Visible = false;
                    break;
                default:
                    break;
            }
            //this.lblPriteDate.Text += DateTime.Now.ToShortDateString();

            //Control
            this.lblANSIPCImpactCheckID.Text = _pcdic.PCDoubleImpactCheckID;
            this.lblANSIPCImpactCheckDate.Text = _pcdic.PCDoubleImpactCheckDate.HasValue ? _pcdic.PCDoubleImpactCheckDate.Value.ToShortDateString() : "";
            this.lblInvoiceCusXOId.Text = _pcdic.InvoiceCusXOId;
            this.lblPronoteHeaderId.Text = _pcdic.PronoteHeaderId;
            this.lblProduct.Text = _pcdic.Product == null ? "" : _pcdic.Product.ToString();
            this.lblEmployee0.Text = _pcdic.Employee == null ? "" : _pcdic.Employee.ToString();
            this.RTDescript.Text = _pcdic.PCDoubleImpactCheckDesc;
            this.lblCheckStandard.Text = _pcdic.CheckStandard;
            this.lblChongJiLiDu.Text = _pcdic.PowerImpact;
            this.lblInvoiceXOQuantity.Text = _pcdic.InvoiceXOQuantity.HasValue ? _pcdic.InvoiceXOQuantity.ToString() : "";
            this.lblPCDoubleImpactCheckCount.Text = _pcdic.PCDoubleImpactCheckCount.HasValue ? _pcdic.PCDoubleImpactCheckCount.ToString() : "";
            this.lbl_productunit.Text = _pcdic.ProductUnit == null ? "" : _pcdic.ProductUnit.ToString();
            //Details
            #region ¸ü¸ÄÏêÏ¸ÏÔÊ¾
            foreach (Model.PCDoubleImpactCheckDetail detail in details)
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
                                    a.SetValue(detail, "¡Ì", null);
                                    break;
                                case "1":
                                    a.SetValue(detail, "¡÷", null);
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
            this.TCPCDoubleImpactCheckDetailDate.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_PCDoubleImpactCheckDetailDate, "{0:yyyy-MM-dd HH:mm:ss}");
            //this.TCattrJiaoLianL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJiaoLianL);
            //this.TCattrJiaoLianR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJiaoLianR);
            this.TCattrJPUpL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJPUpL);
            this.TCattrJPUpR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJPUpR);
            this.TCattrJPDownL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJPDownL);
            this.TCattrJPDownR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJPDownR);
            this.TCattrJPLeftL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJPLeftL);
            this.TCattrJPLeftR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJPLeftR);
            this.TCattrJPRightL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJPRightL);
            this.TCattrJPRightR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJPRightR);
            this.TCattrBiZhong.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrBiZhong);
            //this.DlblBiZhong.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrBiZhong);
            this.TCattrShangLiangL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrShangLiangL);
            this.TCattrShangLiangR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrShangLiangR);
            this.TCattrJPZYL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJPZYL);
            this.TCattrJPZYR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrJPZYR);
            this.TCattrS_SZhongL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrS_SZhongL);
            this.TCattrS_SZhongR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrS_SZhongR);
            this.TCattrS_SShangL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrS_SShangL);
            this.TCattrS_SShangR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrS_SShangR);
            this.TCattrS_SXiaL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrS_SXiaL);
            this.TCattrS_SXiaR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrS_SXiaR);
            //this.TCattrHotL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrHotL);
            //this.TCattrHotR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrHotR);
            //this.TCattrCoolR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrCoolL);
            //this.TCattrCoolR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrCoolR);
            this.TCEmployee.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.lblAttrHeat60.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrHeat60);
            this.lblAttrHeat30m.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrHeat30m);
            this.xrLBanBie.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_PCDoubleImpactCheckBanBie);
            this.lblHML.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrHM500gL);
            this.lblHMR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrHM500gR);
            this.lblZhuiqiuL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrZhuiQiu68gL);
            this.lblZhuiqiuR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrZhuiQiu68gR);
            this.lblChuantouL.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrChuanTou44_2gL);
            this.lblChuantouR.DataBindings.Add("Text", this.DataSource, Model.PCDoubleImpactCheckDetail.PRO_attrChuanTou44_2gR);
        }
    }
}
