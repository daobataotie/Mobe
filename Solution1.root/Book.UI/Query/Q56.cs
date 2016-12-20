using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q56 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceOtherInDepotDetailManager detailManager = new Book.BL.ProduceOtherInDepotDetailManager();
        private BL.ProduceOtherInDepotManager inDepotManager = new Book.BL.ProduceOtherInDepotManager();
        private ConditionOtherInDepot _condition;
        public Q56(ConditionOtherInDepot condition)
        {
            InitializeComponent();
            this._condition = condition;
            IList<Model.ProduceOtherInDepot> list = inDepotManager.SelectByCondition(condition.StartDate, condition.EndDate, condition.Supplier1, condition.Supplier2, condition.ProduceOtherCompactId1, condition.ProduceOtherCompactId2, condition.Product1, condition.Product2, condition.InvouceCusIdStart, condition.InvoiceCusIdEnd);
            if (list == null || list.Count <= 0)
                throw new global::Helper.InvalidValueException("查詢無記錄.");

            if (!global::Helper.DateTimeParse.DateTimeEquls(condition.StartDate, global::Helper.DateTimeParse.NullDate))
                this.xrLabelDateRange.Text += "自 " + condition.StartDate.ToString("yyyy-MM-dd");
            this.xrLabelDateRange.Text += "至 " + condition.EndDate.ToString("yyyy-MM-dd");
            this.xrLabelDates.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.RepotName.Text = BL.Settings.CompanyChineseName;
            this.ReportTitle.Text = Properties.Resources.ProduceOtherInDepot;
            this.DataSource = list;

            this.ProduceOtherInDepotId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepot.PRO_ProduceOtherInDepotId);
            this.SupplierId.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.ProduceOtherInDepotDate.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepot.PRO_ProduceOtherInDepotDate, "{0:yyyy-MM-dd}");
            this.ProduceOtherInDepotId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepot.PRO_ProduceOtherInDepotId);
            this.DepotId.DataBindings.Add("Text", this.DataSource, "Depot." + Model.Depot.PRO_DepotName);
            //this.Employee1Id.DataBindings.Add("Text", this.DataSource, "Employee1." + Model.Employee.PROPERTY_EMPLOYEENAME);
            //this.Employee0Id.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            //this.ProduceOtherInDepotDesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepot.PRO_ProduceOtherInDepotDesc);

            this.xrSubreport1.ReportSource = new Q56_1();
        }
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q56_1 subReport = this.xrSubreport1.ReportSource as Q56_1;
            Model.ProduceOtherInDepot currentModel = this.GetCurrentRow() as Model.ProduceOtherInDepot;
            if (currentModel != null)
            {
                currentModel.Details = this.detailManager.SelectByCondition(currentModel.ProduceOtherInDepotId, this._condition.Product1 == null ? null : this._condition.Product1.ProductName, this._condition.Product2 == null ? null : this._condition.Product2.ProductName);
                subReport.ProduceOtherInDepot = currentModel;
            }
        }

    }
}
