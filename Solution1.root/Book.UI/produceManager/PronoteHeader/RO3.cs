using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PronoteHeader  
{
    public partial class RO3 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        private BL.PronoteProceduresDetailManager pronoteProceduresDetailManager = new Book.BL.PronoteProceduresDetailManager();
        private IList<Model.PronoteMachine> machineList = new List<Model.PronoteMachine>();
        private BL.PronoteMachineManager pronoteMachineManager = new BL.PronoteMachineManager();
        BL.MRSdetailsManager mRSDetailsManager = new BL.MRSdetailsManager();
        private Model.PronoteHeader pronoteHeader;
        public RO3(string pronoteHeaderId,int flag)
        {
            InitializeComponent();
            this.pronoteHeader = this.pronoteHeaderManager.Get(pronoteHeaderId);

            if (this.pronoteHeader == null)
                return;

            this.pronoteHeader.DetailProcedures = this.pronoteProceduresDetailManager.GetPronotedetailsMaterialByHeaderId(this.pronoteHeader);

            if (this.pronoteHeader.DetailProcedures != null)
            {
                foreach (Model.PronoteProceduresDetail detail in this.pronoteHeader.DetailProcedures)
                {
                    this.machineList = this.pronoteMachineManager.GetPronoteMachineByPronoteProceduresDetailId(detail.PronoteProceduresDetailId);
                    {
                        foreach (Model.PronoteMachine machine in machineList)
                        {
                            if (machineList.IndexOf(machine) == machineList.Count - 1)
                                detail.Machine += machine.PronoteMachineName;
                            else
                                detail.Machine += machine.PronoteMachineName + ",";
                            //this.proceManager.Insert(promachine);
                        }
                    }
                }
            }
            this.DataSource = this.pronoteHeader.DetailProcedures;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProducePronoteName;

            if (flag == 1)
                this.xrLabelDataName.Text = Properties.Resources.GZZhiShi;
            else if (flag == 2)
                this.xrLabelDataName.Text = Properties.Resources.ZZJiaGong;
            this.xrLabelPrintDate.Text = this.xrLabelPrintDate.Text + DateTime.Now.ToString("yyyy-MM-dd");
            //生產通知
            this.xrLabelPronoteHeaderID.Text = this.pronoteHeader.PronoteHeaderID;
            this.xrLabelPronoteDte.Text = this.pronoteHeader.PronoteDate.Value.ToString("yyyy-MM-dd");
            this.xrRichTextProDesc.Rtf = this.pronoteHeader.Product.ProductDescription;
            Model.MRSdetails mrsdetail = this.mRSDetailsManager.Get(this.pronoteHeader.MRSdetailsId);
            if (mrsdetail != null)
                this.xrLabelBeforepPackage.Text = mrsdetail.BeforePackageProduct == null ? string.Empty : (mrsdetail.BeforePackageProduct.IsCustomerProduct.HasValue && mrsdetail.BeforePackageProduct.IsCustomerProduct.Value ? mrsdetail.BeforePackageProduct.ProductName + "{" + mrsdetail.BeforePackageProduct.CustomerProductName + "}" : mrsdetail.BeforePackageProduct.ProductName);
            else
                this.xrLabelBeforepPackage.Text = string.Empty;
            if (pronoteHeader.WorkHouse != null)
                this.xrLabelWorkHouse.Text = this.pronoteHeader.WorkHouse.Workhousename;

            if (this.pronoteHeader.Employee0 != null)
            {
                this.xrLabelEmployee0.Text = this.pronoteHeader.Employee0.EmployeeName;
            }
            if (pronoteHeader.Product != null)
            {
               // this.xrLabelProductId.Text = pronoteHeader.Product.Id;
                this.xrLabelProductName.Text = pronoteHeader.Product.ProductName;
                this.xrLabelCustomerProductName.Text = pronoteHeader.Product.CustomerProductName;
            }
            Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(this.pronoteHeader.InvoiceXOId);
            if(xo!=null)
            {
                this.xrLabelCustomer.Text = xo.xocustomer.CustomerShortName;
                this.xrLabelCheckedStandard.Text = xo.xocustomer.CheckedStandard;
                this.xrLabelCustomerXOId.Text = xo.CustomerInvoiceXOId;
                this.xrLabelXOJHDate.Text = xo.InvoiceYjrq.Value.ToString("yyyy-MM-dd");
                
            }
            this.xrLabelCount.Text = pronoteHeader.DetailsSum.ToString();
            this.xrLabelUnit.Text = pronoteHeader.ProductUnit;
            this.xrLabelPronotedesc.Text = this.pronoteHeader.Pronotedesc;
            //this.xrLabelMachine.Text=

            //明细
            this.xrTableCellPronoteProceduresDate.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_PronoteProceduresDate, "{0:yyyy-MM-dd}");
            //this.xrTableCellProceduresNo.DataBindings.Add("Text", this.DataSource,Model.PronoteProceduresDetail.PRO_ProceduresNo);

            this.xrTableCellWorkHouseId.DataBindings.Add("Text", this.DataSource, "WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            this.xrTableCellSupplierId.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.xrTableCellPronoteYingQuantity.DataBindings.Add("Text", this.DataSource,Model.PronoteProceduresDetail.PRO_PronoteYingQuantity);
            //this.xrTableCellFulfillQuantity.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_FulfillQuantity);
            //this.xrTableCellDeposeQuantity.DataBindings.Add("Text", this.DataSource,  Model.PronoteProceduresDetail.PRO_DeposeQuantity);
            //this.xrTableCellcheckQuantity.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_checkQuantity);
            //this.xrTableCellLossQuantity.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_LossQuantity);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Procedures." + Model.Procedures.PRO_Procedurename);
            this.xrTableCell12.DataBindings.Add("Text", this.DataSource, "Procedures." + Model.Procedures.PRO_Proceduredescription);
            this.xrTableCellNO.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_ProceduresNo);
            this.xrTableCellMachine.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_Machine);
            
        }

    }
}
