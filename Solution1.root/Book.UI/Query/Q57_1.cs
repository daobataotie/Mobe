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
    public partial class Q57_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceStatisticsDetailManager produceStatisticsDetailManger = new Book.BL.ProduceStatisticsDetailManager();
        private ConditionProduceStatistics _ProduceStatistic;
        /// 构造函数，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q57_1(ConditionProduceStatistics condition)
        {
            InitializeComponent();
            this._ProduceStatistic = condition;
            //明细
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
        private Model.ProduceStatistics produceStatistics;

        public Model.ProduceStatistics ProduceStatistics
        {
            get { return produceStatistics; }
            set { produceStatistics = value; }
        }
        private void Q55_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.produceStatisticsDetailManger.Select(this.produceStatistics);
        }
    }
}
