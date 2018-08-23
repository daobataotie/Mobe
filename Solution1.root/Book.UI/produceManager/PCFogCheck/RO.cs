using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCFogCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        //IList<Model.ANSIPCImpactCheckDetail> details = new List<Model.ANSIPCImpactCheckDetail>();
        //BL.ANSIPCImpactCheckDetailManager ansipcicManager = new Book.BL.ANSIPCImpactCheckDetailManager();

        public RO(Model.PCFogCheck mPCFogC)
        {
            InitializeComponent();

            if (mPCFogC == null)
                return;

            this.DataSource = mPCFogC.Details;
            //details = ansipcicManager.SelectByAnispcicID(ANSIPCIC.ANSIPCImpactCheckID);
            //this.DataSource = details;

            //CompanyInfo
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.PCFogCheck;
            //this.lblPriteDate.Text += DateTime.Now.ToShortDateString();

            //Control
            this.lblPCFogCheckId.Text = mPCFogC.PCFogCheckId;
            this.lblPCFogCheckDate.Text = mPCFogC.PCFogCheckDate.HasValue ? mPCFogC.PCFogCheckDate.Value.ToShortDateString() : "";
            this.lblInvoiceCusXOId.Text = mPCFogC.InvoiceCusXOId;
            this.lblPronoteHeaderId.Text = mPCFogC.PronoteHeaderId;
            this.lblProduct.Text = mPCFogC.Product == null ? "" : mPCFogC.Product.ToString();
            this.lblEmployee.Text = mPCFogC.Employee == null ? "" : mPCFogC.Employee.ToString();
            this.RTDescript.Text = mPCFogC.PCFogCheckDesc;
            this.lblCheckStandard.Text = mPCFogC.mCheckStandard;
            this.lblPCFogCheckQuantity.Text = mPCFogC.PCFogCheckQuantity.HasValue ? mPCFogC.PCFogCheckQuantity.ToString() : "";
            this.lblInvoiceXOQuantity.Text = mPCFogC.InvoiceXOQuantity.HasValue ? mPCFogC.InvoiceXOQuantity.ToString() : "";
            this.ChkIsPassWuDu.Checked = mPCFogC.IsFogPassing.HasValue ? mPCFogC.IsFogPassing.Value : false;
            this.lbl_productunit.Text = mPCFogC.ProductUnit == null ? "" : mPCFogC.ProductUnit.ToString();
            //this.lbl_CustomerProductName.Text = mPCFogC.Product == null ? "" : mPCFogC.Product.CustomerProductName;
            if (mPCFogC.Product != null)
            {
                if (string.IsNullOrEmpty(mPCFogC.Product.CustomerProductName))
                    this.lbl_CustomerProductName.Text = new Help().GetCustomerProductNameByPronoteHeaderId(mPCFogC.PronoteHeaderId, mPCFogC.ProductId);
                else
                    this.lbl_CustomerProductName.Text = mPCFogC.Product.CustomerProductName;
            }

            //Details
            this.TCChkPassL.DataBindings.Add("Checked", this.DataSource, Model.PCFogCheckDetail.PRO_PassingL);
            this.TCChkPassR.DataBindings.Add("Checked", this.DataSource, Model.PCFogCheckDetail.PRO_PassingR);
            this.TClblCL.DataBindings.Add("Text", this.DataSource, Model.PCFogCheckDetail.PRO_attrCL);
            this.TClblCR.DataBindings.Add("Text", this.DataSource, Model.PCFogCheckDetail.PRO_attrCR);
            this.TClblCommentL.DataBindings.Add("Text", this.DataSource, Model.PCFogCheckDetail.PRO_CommentLTime, "{0:yyyy-MM-dd HH:mm}");
            this.TClblCommentR.DataBindings.Add("Text", this.DataSource, Model.PCFogCheckDetail.PRO_CommentRTime, "{0:yyyy-MM-dd HH:mm}");
            this.TClblHL.DataBindings.Add("Text", this.DataSource, Model.PCFogCheckDetail.PRO_attrHL);
            this.TClblHR.DataBindings.Add("Text", this.DataSource, Model.PCFogCheckDetail.PRO_attrHR);
            this.TClblTL.DataBindings.Add("Text", this.DataSource, Model.PCFogCheckDetail.PRO_attrTL);
            this.TClblTR.DataBindings.Add("Text", this.DataSource, Model.PCFogCheckDetail.PRO_attrTR);
            this.TClblMethod.DataBindings.Add("Text", this.DataSource, Model.PCFogCheckDetail.PRO_Method);
            xrLBusinessHours.DataBindings.Add("Text", this.DataSource, "BusinessHours." + Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);
            this.TCCheckStandard.DataBindings.Add("Text", this.DataSource, Model.PCFogCheckDetail.PRO_CheckStandard);
        }
    }
}
