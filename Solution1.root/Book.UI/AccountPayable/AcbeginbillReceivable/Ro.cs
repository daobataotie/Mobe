using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.AccountPayable.AcbeginbillReceivable
{
    public partial class Ro : DevExpress.XtraReports.UI.XtraReport
    {

        public Ro(Model.AcbeginbillReceivable reable)
        {
            InitializeComponent();

            this.ReportName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelTitle.Text = Properties.Resources.AcbeginbillReceivable;
            this.xrLabelPrint.Text += System.DateTime.Now.ToShortDateString();
         
            #region header
            this.AcbeginbillReceivableId.Text = reable.AcbeginbillReceivableId;
            this.AcbeginbillReceivableDate.Text = reable.AcbeginbillReceivableDate.Value.ToString("yyyy-MM-dd");
            this.Employee1Id.Text = reable.Employee1 == null ? "" : reable.Employee1.EmployeeName;
            this.EmployeeId.Text = reable.Employee == null ? "" : reable.Employee.EmployeeName;
            this.AcbeginbillReceivableDesc.Text = reable.AcbeginbillReceivableDesc;
        //    this.AtCurrencyCategoryId.Text = reable.AtCurrencyCategory == null ? "" : reable.AtCurrencyCategory.ToString();

            #endregion

            #region Details
            this.DataSource = reable.Details;

            this.xrTableCell1CustomerId.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerShortName);
           // this.xrTableCell1AdvanceCollection.DataBindings.Add("Text", this.DataSource, Model.AcbeginbillReceivableDetail.PRO_AdvanceCollection, "{0:0.####}");
          //  this.xrTableCell1ShouldCollection.DataBindings.Add("Text", this.DataSource, Model.AcbeginbillReceivableDetail.PRO_ShouldCollection, "{0:0.####}");
            this.xrTableCell1DomesticAdvanceCollection.DataBindings.Add("Text", this.DataSource, Model.AcbeginbillReceivableDetail.PRO_DomesticAdvanceCollection, "{0:0.####}");
          //  this.xrTableCell1ShouldCollection.DataBindings.Add("Text", this.DataSource, Model.AcbeginbillReceivableDetail.PRO_ShouldCollection, "{0:0.####}");
        //    this.xrTableCellDomesticAlreadyCollection.DataBindings.Add("Text", this.DataSource, Model.AcbeginbillReceivableDetail.PRO_DomesticAlreadyCollection, "{0:0.####}");
           // this.xrTableCellDomesticShouldCollection.DataBindings.Add("Text", this.DataSource, Model.AcbeginbillReceivableDetail.PRO_DomesticShouldCollection, "{0:0.####}");
           // this.xrTableCellAlreadyCollection.DataBindings.Add("Text", this.DataSource, Model.AcbeginbillReceivableDetail.PRO_AlreadyCollection, "{0:0.####}");
          //  this.xrTableCellBeginningBalance.DataBindings.Add("Text", this.DataSource, Model.AcbeginbillReceivableDetail.PRO_BeginningBalance, "{0:0.####}");
            this.xrTableCellDomesticBeginningBalance.DataBindings.Add("Text", this.DataSource, Model.AcbeginbillReceivableDetail.PRO_DomesticBeginningBalance, "{0:0.####}");
            #endregion

        }

    }
}
