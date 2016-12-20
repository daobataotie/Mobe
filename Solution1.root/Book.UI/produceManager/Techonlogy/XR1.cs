using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.Techonlogy
{
    public partial class XR1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceTransferManager produceTransferManager = new Book.BL.ProduceTransferManager();
        private BL.ProduceTransferDetailManager produceTransferDetailManager = new Book.BL.ProduceTransferDetailManager();
        Model.ProduceTransfer produceTransfer = null;
        public XR1(Model.ProduceTransfer produceTransfer)
        {
            InitializeComponent();
            this.produceTransfer = produceTransfer;
            if (this.produceTransfer == null)
                return;
            this.produceTransfer.Details = this.produceTransferDetailManager.Select(this.produceTransfer);

            this.DataSource = this.produceTransfer.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceTransferReport;
            //aa.WorkHouseIn
            this.xrLabelProduceTransferId.Text = this.produceTransfer.ProduceTransferId;
            this.xrLabelProduceTransferDate.Text = this.produceTransfer.ProduceTransferDate.Value.ToString("yyyy-MM-dd");
            if (this.produceTransfer.WorkHouseOut != null)
            {
                this.xrLabelOutWorkHorse.Text = this.produceTransfer.WorkHouseOut.Workhousename;
            }
            if (this.produceTransfer.Employee0 != null)
            {
                this.xrLabelErry0.Text = this.produceTransfer.Employee0.EmployeeName;
            }
            if (this.produceTransfer.Employee1 != null)
            {
                this.xrLabelErry1.Text = this.produceTransfer.Employee1.EmployeeName;
            }
            if (this.produceTransfer.Employee2 != null)
            {
                this.xrLabelErry2.Text = this.produceTransfer.Employee2.EmployeeName;
            }
            this.xrLabeldescription.Text = this.produceTransfer.description;

            //Ã÷Ï¸
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellCustomerInvoiceXOId.DataBindings.Add("Text", this.DataSource, Model.ProduceTransferDetail.PRO_CustomerInvoiceXOId);
            this.xrTableCellProceduresMoQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceTransferDetail.PRO_ProceduresMoQuantity);
            this.xrTableCellProceduresQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceTransferDetail.PRO_ProceduresQuantity);
            this.xrTableCellScrapQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceTransferDetail.PRO_ScrapQuantity);
            this.xrTableCellTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceTransferDetail.PRO_TransferQuantity);
            this.xrTableCel1ProductUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceTransferDetail.PRO_ProductUnit);
            this.xrTableCellWorkHouseInId.DataBindings.Add("Text", this.DataSource, "WorkHouseIn." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
        }

    }
}
