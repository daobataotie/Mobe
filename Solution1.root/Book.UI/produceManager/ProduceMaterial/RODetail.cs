using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Book.UI.Query;
using System.Collections.Generic;
namespace Book.UI.produceManager.ProduceMaterial
{
    public partial class RODetail : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceMaterialManager produceMaterialManager = new Book.BL.ProduceMaterialManager();
        private BL.ProduceMaterialdetailsManager produceMaterialdetailsManager = new Book.BL.ProduceMaterialdetailsManager();
        BL.InvoiceXOManager invoiceXoManager = new BL.InvoiceXOManager();
        BL.PronoteHeaderManager pronoteHeaderManager = new BL.PronoteHeaderManager();
        private Model.ProduceMaterial produceMaterial;
        public RODetail(ConditionMaterial condition)
        {
            InitializeComponent();
            IList<Model.ProduceMaterial> list = produceMaterialManager.SelectBycondition(condition.StartDate, condition.EndDate, condition.ProduceMaterialId0, condition.ProduceMaterialId1, condition.Product0, condition.Product1, condition.DepartmentId0, condition.DepartmentId1, condition.PronoteHeaderId0, condition.PronoteHeaderId1,condition.CusInvoiceXOId,condition.HandBookId);
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.MessageValueException("无数据");
            }
            foreach (Model.ProduceMaterial pm in list)
            {
                if (pm.SourceType != 1)
                {
                    pm.PronoteHeader = this.pronoteHeaderManager.Get(pm.InvoiceId);
                    if (pm.PronoteHeader != null)
                    {
                        pm.ParenProductName = string.IsNullOrEmpty(pm.PronoteHeader.Product.CustomerProductName) ? pm.PronoteHeader.Product.ProductName : pm.PronoteHeader.Product.ProductName + "{" + pm.PronoteHeader.Product.CustomerProductName + "}";
                    }
                }
                if (!string.IsNullOrEmpty(pm.InvoiceXOId))
                {
                    pm.InvoiceXO = invoiceXoManager.Get(pm.InvoiceXOId);
                }
            }
            this.DataSource = list;
            band();
            //CompanyInfo

        }
        private void RO_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RODetail1 material = this.xrSubreport1.ReportSource as RODetail1;
            material.ProduceMaterial = this.GetCurrentRow() as Model.ProduceMaterial;
        }
        private void band()
        {
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceMaterialdetails;
            //加工领料
            this.xrLabelInsertTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.xrLabelProduceMaterialId.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterial.PRO_ProduceMaterialID);
            this.xrLabelDepartment.DataBindings.Add("Text", this.DataSource, "Workhousename");
            //this.xrLabelPronoteHeaderID.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterial.PRO_InvoiceId);
            this.xrLabelProduceMaterialDate.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterial.PRO_ProduceMaterialDate, "{0:yyyy-MM-dd}");
            this.xrLabelProduct.DataBindings.Add("Text", this.DataSource, "ParenProductName");
            //this.xrLabelXOId.DataBindings.Add("Text", this.DataSource, "CusXOId");
            this.xrLabelPiHao.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerLotNumber);
            this.xrLabelInvoiceSum.DataBindings.Add("Text", this.DataSource, "PronoteHeader." + Model.PronoteHeader.PRO_InvoiceXODetailQuantity);
            //this.xrLabelSourceType.DataBindings.Add("Text", this.DataSource, "SourceTypeName");
            this.xrLabel1ProduceMaterialdesc.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterial.PRO_ProduceMaterialdesc);
            this.xrLabelEmployee0.DataBindings.Add("Text", this.DataSource, "Employee0Name");
            this.GroupHeader1.GroupFields.Add(new GroupField(Model.ProduceMaterial.PRO_ProduceMaterialID));

            this.xrSubreport1.ReportSource = new RODetail1();
        }

    }
}
