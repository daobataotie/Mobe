using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace Book.UI.produceManager.PronoteHeader
{
    public partial class ROZZJiaGongDetail : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        BL.MRSdetailsManager mrsdetailManager = new Book.BL.MRSdetailsManager();
        BL.InvoiceXOManager xomanamager = new Book.BL.InvoiceXOManager();
        BL.MRSdetailsManager mRSDetailsManager = new BL.MRSdetailsManager();
        public ROZZJiaGongDetail(Query.ConditionPronoteHeader condition)
        {
            InitializeComponent();
            IList<Model.PronoteHeader> list = pronoteHeaderManager.GetByDateMa(condition.StartDate, condition.EndDate, condition.Customer, condition.CusXOId, condition.Product, condition.PronoteHeaderIdStart, condition.PronoteHeaderIdEnd, condition.SourceTpye, null, false, condition.ProNameKey, condition.ProCusNameKey, condition.PronoteHeaderIdKey);
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();

            }
            foreach (Model.PronoteHeader detail in list)
            {
                if (!string.IsNullOrEmpty(detail.InvoiceXOId))
                    detail.InvoiceXO = this.xomanamager.Get(detail.InvoiceXOId);
                detail.MRSDetails = this.mrsdetailManager.Get(detail.MRSdetailsId);
                if (detail.MRSDetails != null)
                    detail.Plannum = detail.MRSDetails.MRSdetailsQuantity;
            }
            this.DataSource = list;
            this.xrLabelDataName.Text = Properties.Resources.Pronotedetails;

            if (condition.SourceTpye == 5)
            {
                this.xrLabelDataName.Text = Properties.Resources.GZZhiShi;

            }
            else if (condition.SourceTpye == 4)
            {
                this.xrLabelDataName.Text = Properties.Resources.ZZJiaGong;
            }
            this.bind();

        }
        public ROZZJiaGongDetail(IList<Model.PronoteHeader> headerlist, int sourcetype)
        {
            InitializeComponent();
            IList<Model.PronoteHeader> list = headerlist;
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();

            }
            foreach (Model.PronoteHeader detail in list)
            {
                if (!string.IsNullOrEmpty(detail.InvoiceXOId))
                    detail.InvoiceXO = this.xomanamager.Get(detail.InvoiceXOId);
                detail.MRSDetails = this.mrsdetailManager.Get(detail.MRSdetailsId);
                if (detail.MRSDetails != null)
                    detail.Plannum = detail.MRSDetails.MRSdetailsQuantity;
            }
            this.DataSource = list;
            this.xrLabelDataName.Text = Properties.Resources.Pronotedetails;

            if (sourcetype == 5)
            {
                this.xrLabelDataName.Text = Properties.Resources.GZZhiShi;

            }
            else if (sourcetype == 4)
            {
                this.xrLabelDataName.Text = Properties.Resources.ZZJiaGong;
            }
            this.bind();
        }
        private void bind()
        {
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;

            this.lblHandBookId.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_HandbookId);
            this.lblHandBookProductId.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_HandbookProductId);
            this.lblPanNum.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_Plannum);
            //Model.MRSdetails mrsdetail = this.mRSDetailsManager.Get((this.GetCurrentRow() as Model.PronoteHeader).MRSdetailsId);
            //if (mrsdetail != null)
            //    this.lblPanNum.Text = mrsdetail.MRSdetailsQuantity.ToString();

            this.xrLabelPrintDate.Text = this.xrLabelPrintDate.Text + DateTime.Now.ToShortDateString();
            this.xrLabelPronoteHeaderID.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_PronoteHeaderID);
            this.xrLabelPronoteDte.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_PronoteDate, "{0:yyyy-MM-dd}");

            this.xrLabelCustomerXOId.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_InvoiceCusId);
            this.xrLabelCustomer.DataBindings.Add("Text", this.DataSource, "CustomerShortName");
            this.xrLabelCheckedStandard.DataBindings.Add("Text", this.DataSource, "CustomerCheckStandard");
            this.xrLabelCustomerProductName.DataBindings.Add("Text", this.DataSource, "CustomerProductName");
            this.xrLabelCount.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_DetailsSum);

            this.xrLabelUnit.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_ProductUnit);
            this.xrLabelXOJHDate.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_InvoiceYjrq, "{0:yyyy-MM-dd}");
            this.xrLabelPiHao.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerLotNumber);
            this.xrLabelProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.xrRichTextProDesc.DataBindings.Add("Rtf", this.DataSource, "ProductDesc");
            this.xrLabelMRP.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_MRSHeaderId);
            this.xrLabelWorkHouse.DataBindings.Add("Text", this.DataSource, "Workhousename");
            this.xrLabelBeforepPackage.DataBindings.Add("Text", this.DataSource, "MRSDetails.BeforePackageProduct." + Model.Product.PRO_ProductName);
            this.GroupHeader1.GroupFields.Add(new GroupField(Model.PronoteHeader.PRO_PronoteHeaderID));
            this.xrLabelPronotedesc.DataBindings.Add("Text", this.DataSource, Model.PronoteHeader.PRO_Pronotedesc);
            this.xrLabelEmployee.DataBindings.Add("Text", this.DataSource, "Employee0Name");
            this.xrSubreport1.ReportSource = new RO1();
            this.xrSubreport2.ReportSource = new RO2();
        }

        private void RO_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RO1 material = this.xrSubreport1.ReportSource as RO1;
            material.PronoteHeader = this.GetCurrentRow() as Model.PronoteHeader;
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RO2 material = this.xrSubreport2.ReportSource as RO2;
            material.PronoteHeader = this.GetCurrentRow() as Model.PronoteHeader;
        }

    }
}
