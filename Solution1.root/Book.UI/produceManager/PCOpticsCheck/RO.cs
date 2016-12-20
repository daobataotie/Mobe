using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCOpticsCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO(Model.PCOpticsCheck _pcopc)
        {
            InitializeComponent();
            if (_pcopc == null)
                return;
            //CompanyInfo
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.PCOpticsCheck;
            //this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Details Controls
            this.lblPCOpticsCheckId.Text = _pcopc.PCOpticsCheckId;
            this.lblPCOpticsCheckDate.Text = _pcopc.PCOpticsCheckDate.HasValue ? _pcopc.PCOpticsCheckDate.Value.ToString("yyyy-MM-dd") : "";
            this.lblPronoteHeardId.Text = _pcopc.PronoteHeaderId;
            this.lblProductName.Text = _pcopc.Product == null ? "" : _pcopc.Product.ToString();
            this.lblInvoiceCusXOId.Text = _pcopc.InvoiceCusXOId;
            this.lblInvoiceXOQuantity.Text = _pcopc.InvoiceXOQuantity.HasValue ? _pcopc.InvoiceXOQuantity.Value.ToString() : "0";
            this.lblPCOpticsCheckQuantity.Text = _pcopc.PCOpticsCheckQuantity.HasValue ? _pcopc.PCOpticsCheckQuantity.Value.ToString() : "0";
            this.chkZiWaiXian.Checked = _pcopc.IsZiWaiXian.HasValue ? _pcopc.IsZiWaiXian.Value : false;
            this.chkKeJianGuang.Checked = _pcopc.IsKeJianGuang.HasValue ? _pcopc.IsKeJianGuang.Value : false;
            this.lblPCOpticsCheckDesc.Text = _pcopc.PCOpticsCheckDesc;
            this.lblEmployee.Text = _pcopc.Employee.ToString();
            this.lbl_ProductUnit.Text = _pcopc.ProductUnit == null ? "" : _pcopc.ProductUnit.ToString();
            //this.lblPCOpticsCheckId.Text = _pcfc.PCFinishCheckID;
            //this.lblPCOpticsCheckDate.Text = _pcfc.PCFinishCheckDate.HasValue ? _pcfc.PCFinishCheckDate.Value.ToShortDateString() : "";
            //this.lblProductName.Text = _pcfc.Product == null ? "" : _pcfc.Product.ToString();
            //this.lblBuMen.Text = _pcfc.WorkHouse == null ? "" : _pcfc.WorkHouse.ToString();
            //this.lblInvoiceCusXOId.Text = _pcfc.InvoiceCusXOId;
            //this.lblPCFinishCheckCount.Text = _pcfc.PCFinishCheckCount.ToString();
            //this.lblPCFinishCheckInCoiunt.Text = _pcfc.PCFinishCheckInCoiunt.HasValue ? _pcfc.PCFinishCheckInCoiunt.Value.ToString() : "";
            //this.lblPCFinishCheckDesc.Text = _pcfc.PCFinishCheckDesc;
            //this.lblEmployee0.Text = _pcfc.Employee0 == null ? "" : _pcfc.Employee0.ToString();
            //this.lblEmployee1.Text = _pcfc.Employee1 == null ? "" : _pcfc.Employee1.ToString();
            //this.lblCustomerProductName.Text = _pcfc.Product.CustomerProductName;
            //this.lblPronoteHeardId.Text = _pcfc.PronoteHeaderID;
            //this.lblCustomerProductName.Text = _pcfc.CustomerProductName;

            //details
            //this.lblDZDWQDW.Text = Trans(_pcfc.AttrDZDWQDW);
            //this.lblJWYHWRL.Text = Trans(_pcfc.AttrJWYHWRL);
            //this.lblGZBKYRL.Text = Trans(_pcfc.AttrGZBKYRL);
            //this.lblZZWBXGJ.Text = Trans(_pcfc.AttrZZWBXGJ);
            //this.lblJPBKGCS.Text = Trans(_pcfc.AttrJPBKGS);
            //this.lblJPJJHZQ.Text = Trans(_pcfc.AttrJPJHZQ);
            //this.lblJPJSX.Text = Trans(_pcfc.AttrJPSX);
            //this.lblJJSFTSYH.Text = Trans(_pcfc.AttrJJSFTSYH);
            //this.lblGX.Text = Trans(_pcfc.AttrGX);
            //this.lblTSL.Text = Trans(_pcfc.AttrTSL);
            //this.lblCJBZ.Text = Trans(_pcfc.AttrCJBZ);
            //this.lblWXTB.Text = Trans(_pcfc.AttrWXTB);
            //this.lblZMCM.Text = Trans(_pcfc.AttrZMCM);
            //this.lblSLDKSFMF.Text = Trans(_pcfc.AttrSLDSFMF);
            //this.lblNHDQSFZQ.Text = Trans(_pcfc.AttrNHDQSFZQ);
            //this.lblNHTB.Text = Trans(_pcfc.AttrNHTB);
            //this.lblJSSFZQ.Text = Trans(_pcfc.AttrJSSFZQ);
            //this.lblJDZRFS.Text = Trans(_pcfc.AttrJDZRFS);
            //this.lblPKZRFS.Text = Trans(_pcfc.AttrPKZRFS);
            //this.lblSLDNHWXTMTBSFZQ.Text = Trans(_pcfc.AttrSLDNHWXTMSFZQ);


        }

        private string Trans(int? a)
        {
            string str = string.Empty;
            switch (a.HasValue ? a.Value : -1)
            {
                case -1:
                    str = "";
                    break;
                case 0:
                    str = "¡Ì";
                    break;
                case 1:
                    str = "X";
                    break;
                case 2:
                    str = "¡÷";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }
    }
}
