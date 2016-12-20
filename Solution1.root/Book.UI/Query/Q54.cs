using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q54 : XtraReport
    {
        BL.ProduceOtherMaterialManager otherManager = new Book.BL.ProduceOtherMaterialManager();
        private BL.ProduceOtherMaterialDetailManager detailManger = new Book.BL.ProduceOtherMaterialDetailManager();
        private ConditionOtherMaterial conditionMaterial;
        public Q54(ConditionOtherMaterial condition)
        {
            InitializeComponent();
            this.conditionMaterial = condition;
            IList<Model.ProduceOtherMaterial> list = otherManager.SelectByCondition(condition.StartDate, condition.EndDate, condition.SupplierId1, condition.SupplierId2, condition.ProduceOtherCompactId1, condition.ProduceOtherCompactId2, condition.ProductId1, condition.ProductId2, null);
            if (list == null || list.Count <= 0)
                throw new global::Helper.MessageValueException("無記錄！");
            if (!global::Helper.DateTimeParse.DateTimeEquls(condition.StartDate, global::Helper.DateTimeParse.NullDate))
                this.xrLabelDateRange.Text += "自 " + condition.StartDate.ToString("yyyy-MM-dd");
            this.xrLabelDateRange.Text += "至 " + condition.EndDate.ToString("yyyy-MM-dd");
            this.xrLabelDates.Text = DateTime.Now.ToShortDateString();
            this.RepotName.Text = BL.Settings.CompanyChineseName;
            this.ReportTitle.Text = Properties.Resources.ProduceOtherMaterialDetail;
            this.DataSource = list;
            this.SupplierId.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.ProduceOtherMaterialId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterial.PRO_ProduceOtherMaterialId);
            this.ProduceMaterialdesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterial.PRO_ProduceOtherMaterialDesc);
            this.ProduceOtherCompactId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterial.PRO_ProduceOtherCompactId);
            this.ProduceOtherMaterialDate.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterial.PRO_ProduceOtherMaterialDate, "{0:yyyy-MM-dd}");
            this.WorkHouseId.DataBindings.Add("Text", this.DataSource, "WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            this.DepotId.DataBindings.Add("Text", this.DataSource, "Depot." + Model.Depot.PRO_DepotName);
            //this.Employee0Id.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            //this.Employee1Id.DataBindings.Add("Text", this.DataSource, "Employee1." + Model.Employee.PROPERTY_EMPLOYEENAME);
            //this.Employee2Id.DataBindings.Add("Text", this.DataSource, "Employee2." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrSubreport1.ReportSource = new Q54_1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q54_1 subReport = this.xrSubreport1.ReportSource as Q54_1;
            Model.ProduceOtherMaterial temp = this.GetCurrentRow() as Model.ProduceOtherMaterial;
            if (temp != null)
            {
                temp.Details = detailManger.SelectByCondition(temp.ProduceOtherMaterialId, conditionMaterial.ProductId1, conditionMaterial.ProductId2);
                subReport.ProduceOtherMaterial = temp;
            }
        }

    }
}
