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
    public partial class Q58_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceStatisticsCheckDetailManager produceStatisticsCheckDetailManger = new Book.BL.ProduceStatisticsCheckDetailManager();
        private ConditionProduceStatisticsCheck _ProduceStatisticCheck;
        /// 构造函数，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q58_1(ConditionProduceStatisticsCheck condition)
        {
            InitializeComponent();
            this._ProduceStatisticCheck = condition;
            //明细
            this.xrTableCellDate.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_DetailDate, "{0:yyyy-MM-dd}");
            this.xrTableCellType.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_BusinessHoursType);


            this.xrTableCellEllo.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellElpple.DataBindings.Add("Text", this.DataSource, "Employee1." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellPcount.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_ProduceQuantity);
            this.xrTableCellNoPcount.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_FractionDefective);
            this.xrTableCell10.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_APian);
            this.xrTableCell11.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_BPian);
            this.xrTableCell12.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsCheckDetail.PRO_CPian);
            
        }
        private Model.ProduceStatisticsCheck produceStatisticsCheck;

        public Model.ProduceStatisticsCheck ProduceStatisticsCheck
        {
            get { return produceStatisticsCheck; }
            set { produceStatisticsCheck = value; }
        }
        private void Q55_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.produceStatisticsCheckDetailManger.Select(this.produceStatisticsCheck);
        }
    }
}
