using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q59 : DevExpress.XtraReports.UI.XtraReport
    {

        BL.MRSHeaderManager mrsHeaderManager = new BL.MRSHeaderManager();
        BL.MRSdetailsManager mrsDetailManager = new BL.MRSdetailsManager();
        private ConditionMRS condition;
        public Q59(ConditionMRS condition)
        {
            InitializeComponent();
            this.condition = condition;
            IList<Model.MRSHeader> list = this.mrsHeaderManager.SelectbyCondition(condition.MrsStart, condition.MrsEnd, condition.CustomerStart, condition.CustomerEnd, condition.StartDate, condition.EndDate ,condition.SourceType,condition.Id1,condition.Id2,condition.Cusxoid,condition.Product);

            if (list == null || list.Count <= 0)
                throw new global::Helper.InvalidValueException();
            this.DataSource = list;
            this.ReportName.Text = BL.Settings.CompanyChineseName;
            this.ReportTitle.Text = Properties.Resources.MRSDetails;
            this.xrLabelCustomer.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerShortName);
            this.xrLabelEmployee0Id.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelEmployee1Id.DataBindings.Add("Text", this.DataSource, "Employee1." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelMRSheaderId.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_MRSHeaderId);
            this.xrLabelMpsId.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PRO_MPSheaderId);
            this.xrLabelMPSStartDate.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PRO_MRSstartdate);
            this.xrLabelMRSheaderDesc.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PRO_MRSheaderDesc);

            this.xrSubreport1.ReportSource = new Q59_1();

        }

        private void Q59_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q59_1 reportProducts = this.xrSubreport1.ReportSource as Q59_1;
            Model.MRSHeader mrsHeader = this.GetCurrentRow() as Model.MRSHeader;
            if (mrsHeader != null)
            {
                mrsHeader.Details = this.mrsDetailManager.GetByMRSIDAndProId(mrsHeader.MRSHeaderId, this.condition.Product==null?null:this.condition.Product.ProductId);
                reportProducts.MrsHeader = mrsHeader;
            }
        }

    }
}
