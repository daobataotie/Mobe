using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceStatisticsCheck
{
    public partial class XR : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceStatisticsCheckManager ProduceStatisticsCheckManager = new Book.BL.ProduceStatisticsCheckManager();
        private BL.ProduceStatisticsCheckDetailManager ProduceStatisticsCheckdetailsManager = new Book.BL.ProduceStatisticsCheckDetailManager();
        BL.InvoiceXOManager invoiceXoManager = new BL.InvoiceXOManager();
        private Model.ProduceStatisticsCheck _ProduceStatisticsCheck;
        public XR(string ProduceStatisticsCheckId)
        {
            InitializeComponent();
            this._ProduceStatisticsCheck = this.ProduceStatisticsCheckManager.Get(ProduceStatisticsCheckId);

            if (this._ProduceStatisticsCheck == null)
                return;

            this._ProduceStatisticsCheck.Details = this.ProduceStatisticsCheckdetailsManager.Select(this._ProduceStatisticsCheck);

            this.DataSource = this._ProduceStatisticsCheck.Details;

            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceCheck;
            this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();
            this.xrLabelProduceStatisticsDate.Text = this._ProduceStatisticsCheck.ProduceStatisticsCheckDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelProduceStatisticsId.Text = this._ProduceStatisticsCheck.ProduceStatisticsCheckId;

            if (this._ProduceStatisticsCheck.Employee0 != null)
            {
                this.xrLabelEmployeeId.Text = this._ProduceStatisticsCheck.Employee0.EmployeeName;
            }

            if (this._ProduceStatisticsCheck.Employee1 != null)
            {
                this.xrLabel4.Text = this._ProduceStatisticsCheck.Employee1.EmployeeName;
            }
            this.xrLabelPronoteHeaderID.Text = this._ProduceStatisticsCheck.PronoteHeaderID;

            if (!string.IsNullOrEmpty(this._ProduceStatisticsCheck.PronoteHeaderID))
            {
                Model.PronoteHeader pronoteHeader = new BL.PronoteHeaderManager().Get(this._ProduceStatisticsCheck.PronoteHeaderID);
                if (pronoteHeader != null)
                {
                    Model.InvoiceXO invoiceXO = new BL.InvoiceXOManager().Get(pronoteHeader.InvoiceXOId);
                    this.xrLabelCustomerXoId.Text = invoiceXO == null ? string.Empty : invoiceXO.CustomerInvoiceXOId;
                    if (pronoteHeader.Product != null)
                    {
                        this.xrLabelProductId.Text = pronoteHeader.Product.Id;
                        this.xrLabelProductName.Text = pronoteHeader.Product.ProductName;
                        this.xrRichText3.Rtf = pronoteHeader.Product.ProductDescription;
                    }
                }
            }
            else
                this.xrLabelCustomerXoId.Text = string.Empty;
            this.xrLabelDescription.Text = this._ProduceStatisticsCheck.Description;
            //Ã÷Ï¸
            this.xrTableCellDetailDate.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_DetailDate, "{0:yyyy-MM-dd}");
            this.xrTableCellBusinessHoursType.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_BusinessHoursType);


            this.xrTableCellEmployee0Id.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellEmployee1Id.DataBindings.Add("Text", this.DataSource, "Employee1." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellProduceQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_ProduceQuantity);
            this.xrTableCellAPian.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_APian);
            this.xrTableCellBPian.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_BPian);
            this.xrTableCellCPian.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_CPian);
            this.xrTableCellFractionDefective.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_FractionDefective);
        }

    }
}
