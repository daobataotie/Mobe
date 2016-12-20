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

// 编 码 人: 周欣亮            完成时间:2011-7-28
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q58 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.ProduceStatisticsCheckManager produceStatisticsCheckManager = new Book.BL.ProduceStatisticsCheckManager();
        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q58(ConditionProduceStatisticsCheck condition)
        {  
            InitializeComponent();
            IList<Model.ProduceStatisticsCheck> list = produceStatisticsCheckManager.SelectBycondition(condition.StartDate, condition.EndDate, condition.StartProduceStatisticsCheckId, condition.EndProduceStatisticsCheckId, condition.StartPronoteHeaderID, condition.EndPronoteHeaderID);
            if (list == null || list.Count <= 0)
                throw new global::Helper.InvalidValueException();
            this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();
            this.xrLabelRepotName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "生產車間質檢";

            this.DataSource = list;
            this.xrLabelProduceStatisticsId.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheck.PRO_ProduceStatisticsCheckId);
            this.xrLabelProduceStatisticsDate.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheck.PRO_ProduceStatisticsCheckDate, "{0:yyyy-MM-dd}");

            this.xrLabelEmployee.DataBindings.Add("Text", this.DataSource, "Employee1." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelCustomerXoId.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheck.PRO_CustomerInvoiceXOId);
          
            this.xrLabelPronoteHeaderID.DataBindings.Add("Text", this.DataSource, "PronoteHeader." + Model.PronoteHeader.PRO_PronoteHeaderID);



            this.xrLabelPronotedesc.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheck.PRO_Description);
            this.xrSubreport1.ReportSource = new Q58_1(condition);
           
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q58_1 subReport = this.xrSubreport1.ReportSource as Q58_1;
            subReport.ProduceStatisticsCheck = this.GetCurrentRow() as Model.ProduceStatisticsCheck;
        }
    }
}
