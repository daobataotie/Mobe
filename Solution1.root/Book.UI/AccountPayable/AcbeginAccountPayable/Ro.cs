using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AcbeginAccountPayable
{
    public partial class Ro : DevExpress.XtraReports.UI.XtraReport
    {

        public Ro(Model.AcbeginAccountPayable payable)
        {
            InitializeComponent();

            this.xrLabelName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelTitle.Text = Properties.Resources.AcbeginAccountPayable;
            this.xrLabelPrint.Text += System.DateTime.Now.ToShortDateString();
            #region header
            this.AcbeginAccountPayableId.Text = payable.AcbeginAccountPayableId;
            this.AcbeginAccountPayableDate.Text = payable.AcbeginAccountPayableDate.Value.ToString("yyyy-MM-dd");
            this.Employee1.Text = payable.Employee == null ? "" : payable.Employee.EmployeeName;
            this.Employee0.Text = payable.Employee0 == null ? "" : payable.Employee0.EmployeeName;
          //  this.AtCurrencyCategoryId.Text = payable.AtCurrencyCategory == null ? "" : payable.AtCurrencyCategory.ToString();
            #endregion

            #region Details
            this.DataSource = payable.Details;

            this.xrTableCell1SupplierId.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
          //  this.xrTableCell1AdvancePayment.DataBindings.Add("Text", this.DataSource, Model.AcbeginAccountPayableDetail.PRO_AdvancePayment,"{0:0.####}");
            this.xrTableCell1DomesticAdvancePayment.DataBindings.Add("Text", this.DataSource, Model.AcbeginAccountPayableDetail.PRO_DomesticAdvancePayment, "{0:0.####}");
          //  this.xrTableCell1ShouldPayment.DataBindings.Add("Text", this.DataSource, Model.AcbeginAccountPayableDetail.PRO_ShouldPayment, "{0:0.####}");
           // this.xrTableCell1DomesticAccountPayable.DataBindings.Add("Text", this.DataSource, Model.AcbeginAccountPayableDetail.PRO_DomesticAccountPayable, "{0:0.####}");
           // this.xrTableCell1AlreadyPayment.DataBindings.Add("Text", this.DataSource, Model.AcbeginAccountPayableDetail.PRO_AlreadyPayment, "{0:0.####}");
          //  this.xrTableCell1DomesticAlreadyPayment.DataBindings.Add("Text", this.DataSource, Model.AcbeginAccountPayableDetail.PRO_DomesticAlreadyPayment, "{0:0.####}");
          //  this.xrTableCellBeginningBalance.DataBindings.Add("Text", this.DataSource, Model.AcbeginAccountPayableDetail.PRO_BeginningBalance, "{0:0.####}");
            this.xrTableCellDomesticBeginningBalance.DataBindings.Add("Text", this.DataSource, Model.AcbeginAccountPayableDetail.PRO_DomesticBeginningBalance, "{0:0.####}");

            #endregion
        }

    }
}
