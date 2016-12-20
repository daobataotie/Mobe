using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{

    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-6-19
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q34 : BaseReport
    {
        
        private ConditionF condition;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="condition"></param>
        public Q34(ConditionF condition)
        {
            this.condition = condition;

            InitializeComponent();
            
            this.xrLabelReportName.Text = Properties.Resources.FKCXRBB;

            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, condition.StartDate.ToString("yyyy-MM-dd"), condition.EndDate.ToString("yyyy-MM-dd"));
            

            //System.Collections.Generic.IList<Model.Company> list = this.companyManager.Select(condition.StartId, condition.EndId, Helper.CompanyKind.Supplier);

            //if (list == null || list.Count <= 0)
            //    throw new Helper.InvalidValueException();

            //this.bindingSource1.DataSource = list;

            //this.xrTableCellCompanyId.DataBindings.Add("Text", this.DataSource, Model.Company.PROPERTY_COMPANYID);
            //this.xrTableCellCompanyName.DataBindings.Add("Text", this.DataSource, Model.Company.PROPERTY_COMPANYNAME1);

            //this.GroupHeader1.GroupFields.Add(new GroupField(Model.Company.PROPERTY_COMPANYID));
            //this.xrSubreport1.ReportSource = new Q34_1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q34_1 reportProducts = this.xrSubreport1.ReportSource as Q34_1;
            //reportProducts.Company = this.GetCurrentRow() as Model.Company;
            reportProducts.Condition = this.condition;
        }
    }
}
