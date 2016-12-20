using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceStatistics
{
    public partial class XR : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceStatisticsManager produceStatisticsManager = new Book.BL.ProduceStatisticsManager();
        private BL.ProduceStatisticsDetailManager produceStatisticsdetailsManager = new Book.BL.ProduceStatisticsDetailManager();
        BL.InvoiceXOManager invoiceXoManager = new BL.InvoiceXOManager();
        private Model.ProduceStatistics produceStatistics;
        public XR(string ProduceStatisticsId)
        {
            InitializeComponent();
            this.produceStatistics = this.produceStatisticsManager.Get(ProduceStatisticsId);

            if (this.produceStatistics == null)
                return;

            this.produceStatistics.Details = this.produceStatisticsdetailsManager.Select(this.produceStatistics);

            this.DataSource = this.produceStatistics.Details;

            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceNums;
            this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();



            this.xrLabelProduceStatisticsDate.Text = this.produceStatistics.ProduceStatisticsDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelProduceStatisticsId.Text = this.produceStatistics.ProduceStatisticsId;

            if (this.produceStatistics.Employee != null)
            {
                this.xrLabelEmployeeId.Text = this.produceStatistics.Employee.EmployeeName;
            }

            if (this.produceStatistics.WorkHouse != null)
            {
                this.xrLabelWorkHouseId.Text = this.produceStatistics.WorkHouse.Workhousename;
            }
            this.xrLabelPronoteHeaderID.Text = this.produceStatistics.PronoteHeaderID;

            if (!string.IsNullOrEmpty(this.produceStatistics.PronoteHeaderID))
            {
                Model.PronoteHeader pronoteHeader = new BL.PronoteHeaderManager().Get(this.produceStatistics.PronoteHeaderID);
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
            if (this.produceStatistics.Procedures != null)
            {
                this.xrLabelProceduresId.Text = this.produceStatistics.Procedures.Id;
                this.xrRichText1.Rtf = this.produceStatistics.Procedures.Procedurename;
            }
            this.xrLabelDescription.Text = this.produceStatistics.Description;
            //Ã÷Ï¸
            this.xrTableCellDate.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_DetailDate, "{0:yyyy-MM-dd}");
            this.xrTableCellType.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_BusinessHoursType);

           
            this.xrTableCellEllo.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellElpple.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellPcount.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_ProduceQuantity);
            this.xrTableCellHege.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_HeGeQuantity);
            this.xrTableCellNoPcount.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_RejectionRate);
            this.xrTableCelldescription.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_Description);
            this.xrTableCellUpdatDate.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_UpdateTime, "{0:yyyy-MM-dd}");


            this.xrTableCell25.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_HeiDian);
            this.xrTableCell26.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_Zazhi);
            this.xrTableCell27.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_JingDian);
            this.xrTableCell28.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_ChaShang);
            this.xrTableCell29.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_FuMo);
            this.xrTableCell30.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_SuoShui);
            this.xrTableCell31.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_GuoHuo);
            this.xrTableCell32.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_BaiYan);
            this.xrLabel2.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_HeiYan);
            this.xrLabel4.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_JieHeXian);
            this.xrLabel6.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_HuiWen);
            this.xrLabel8.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_QiPao);
            this.xrLabel10.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_LengLiao);
            this.xrLabel12.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_GuaiShouZhuangShang);
            this.xrLabel14.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_ChaMoCiShu);
            this.xrLabel16.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_LiaoDian);

        }

    }
}
