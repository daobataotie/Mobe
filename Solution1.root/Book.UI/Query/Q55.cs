using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q55 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.ProduceOtherExitDetailManager detailManager = new Book.BL.ProduceOtherExitDetailManager();
        BL.ProduceOtherExitMaterialManager produceOtherExitMaterialManager = new Book.BL.ProduceOtherExitMaterialManager();

        public Q55(ConditionOtherExit condition)
        {
            InitializeComponent();
            IList<Model.ProduceOtherExitMaterial> list = produceOtherExitMaterialManager.SelectByCondition(condition.StartDate, condition.EndDate, condition.ProduceOtherCompactId1, condition.ProduceOtherCompactId2, condition.SupplierId1, condition.SupplierId2,condition.ProductId1,condition.ProductId2);
            if (list == null || list.Count <= 0)
                throw new global::Helper.InvalidValueException("無記錄");
            if (!global::Helper.DateTimeParse.DateTimeEquls(condition.StartDate, global::Helper.DateTimeParse.NullDate))
                this.xrLabelDateRange.Text = "自 " + condition.StartDate.ToString("yyyy-MM-dd");
            this.xrLabelDateRange.Text += "至 " + condition.EndDate.ToString("yyyy-MM-dd");
            this.ReportDate.Text += System.DateTime.Now.Date;
            this.xrLabelRepotName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelTitle.Text = Properties.Resources.ProduceOtherExitDetail;
            this.DataSource = list;

            this.ProduceOtherExitMaterialId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherExitMaterial.PRO_ProduceOtherExitMaterialId);
            this.Employee0.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.Employee1.DataBindings.Add("Text", this.DataSource, "Employee1." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.SupplierId.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.DepotId.DataBindings.Add("Text", this.DataSource, "Depot." + Model.Depot.PRO_DepotName);
            this.xrSubreport1.ReportSource = new Q55_1(condition);
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q55_1 subReport = this.xrSubreport1.ReportSource as Q55_1;
            subReport.ProduceOtherExitMaterial = this.GetCurrentRow() as Model.ProduceOtherExitMaterial;
        }

    }
}
