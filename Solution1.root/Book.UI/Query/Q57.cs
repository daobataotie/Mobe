using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 周欣亮            完成时间:2011-7-27
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q57 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.ProduceStatisticsManager produceStatisticsManager = new Book.BL.ProduceStatisticsManager();
        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q57(ConditionProduceStatistics condition)
        {  
            InitializeComponent();
            IList<Model.ProduceStatistics> list = produceStatisticsManager.SelectBycondition(condition.StartDate, condition.EndDate, condition.StartProduceStatisticsId, condition.EndProduceStatisticsId, condition.StartPronoteHeaderID, condition.EndPronoteHeaderID);
            if (list == null || list.Count <= 0)
                throw new global::Helper.InvalidValueException();
            this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();
            this.xrLabelRepotName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "生產車間數量";

            this.DataSource = list;
            this.xrLabelProduceStatisticsId.DataBindings.Add("Text", this.DataSource, Model.ProduceStatistics.PRO_ProduceStatisticsId);
            this.xrLabelProduceStatisticsDate.DataBindings.Add("Text", this.DataSource, Model.ProduceStatistics.PRO_ProduceStatisticsDate, "{0:yyyy-MM-dd}");

            this.xrLabelEmployee.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelCustomerXoId.DataBindings.Add("Text", this.DataSource, Model.ProduceStatistics.PRO_CustomerInvoiceXOId);
            this.xrLabelWorkHouseId.DataBindings.Add("Text", this.DataSource, "WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            this.xrLabelPronoteHeaderID.DataBindings.Add("Text", this.DataSource, "PronoteHeader." + Model.PronoteHeader.PRO_PronoteHeaderID);

            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Procedures." + Model.Procedures.PRO_Procedurename);
            this.xrLabelProceduresId.DataBindings.Add("Text", this.DataSource, "Procedures." + Model.Procedures.PRO_Id);
           
            this.xrLabelPronotedesc.DataBindings.Add("Text", this.DataSource, Model.ProduceStatistics.PRO_Description);
            this.xrSubreport1.ReportSource = new Q57_1(condition);
           
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q57_1 subReport = this.xrSubreport1.ReportSource as Q57_1;
            subReport.ProduceStatistics = this.GetCurrentRow() as Model.ProduceStatistics;
        }
    }
}
