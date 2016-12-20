using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Hr.Attendance.Leave
{
    public partial class LeaveListBetweenDate : DevExpress.XtraEditors.XtraForm
    {
        string emps = string.Empty;
        DateTime? dt_s;
        DateTime? dt_e;

        public LeaveListBetweenDate()
        {
            InitializeComponent();
            this.dt_s = DateTime.Now.Date.AddMonths(-1).Date;
            this.dt_e = DateTime.Now.Date;
            this.dateEditStart.DateTime = this.dt_s.Value;
            this.dateEditEnd.DateTime = this.dt_e.Value;
        }

        public LeaveListBetweenDate(IList<Model.Employee> emplist)
            : this()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Model.Employee emp in emplist)
            {
                sb.Append("'" + emp.EmployeeId + "',");
            }
            this.emps = sb.ToString().Substring(0, sb.ToString().Length - 1);
            this.bindingSource1.DataSource = new BL.LeaveManager().SelectByDateRangeEmp(this.emps, dt_s.Value, dt_e.Value);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.dt_s = this.dateEditStart.DateTime.Date;
            this.dt_e = this.dateEditEnd.DateTime.Date;

            if (!dt_s.HasValue || !dt_e.HasValue)
            {
                MessageBox.Show("日期條件不完整");
            }
            else
            {
                this.bindingSource1.DataSource = new BL.LeaveManager().SelectByDateRangeEmp(this.emps, dt_s.Value, dt_e.Value);
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            if (e.Column.Name == "Column_LeaveRange")
            {
                switch (e.Value.ToString())
                {
                    case "0":
                        e.DisplayText = "整日";
                        break;
                    case "1":
                        e.DisplayText = "上半日";
                        break;
                    case "2":
                        e.DisplayText = "下半日";
                        break;
                }
            }
        }
    }
}