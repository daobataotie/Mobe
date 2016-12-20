using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;


namespace Book.UI.Hr.Salary.Salaryset
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军           完成时间:2009-10-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class CalCrystalReportForm : DevExpress.XtraEditors.XtraForm
    {

        //table
        private DataTable table = new DataTable();
        //变量
        private int years = 0;
        private int months = 0;
        public CalCrystalReportForm()
        {
            InitializeComponent();
        }

        public CalCrystalReportForm(DataTable tables, int year, int month)
            : this()
        {
            this.table = tables;
            this.years = year;
            this.months = month;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

            CrystalReport4 cr = new CrystalReport4();
            cr.SetDataSource(table);
            cr.SetParameterValue("DateYearAndMonth", this.years + "年" + this.months + "月—薪資月報表");

            this.crystalReportViewer1.ReportSource = cr;

        }
    }
}