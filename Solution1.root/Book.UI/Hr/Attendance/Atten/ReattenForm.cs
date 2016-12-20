using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using Book.UI.Settings.BasicData.Employees;

namespace Book.UI.Hr.Attendance.Atten
{
    internal partial class ReattenForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 打卡記錄管理實例
        /// </summary>
        BL.ClockDataManager clockdata = new Book.BL.ClockDataManager();
        /// <summary>
        /// 员工管理实例
        /// </summary>
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        /// <summary>
        /// 部门管理实例
        /// </summary>
        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        /// <summary>
        /// 当前待处理员工
        /// </summary>
        private Book.Model.Employee employee = null;
        /// <summary>
        /// 出勤记录
        /// </summary>
        private BL.HrDailyEmployeeAttendInfoManager _hrManager = new Book.BL.HrDailyEmployeeAttendInfoManager();

        public ReattenForm()
        {
            InitializeComponent();
            this.nccEmployee.Choose = new ChooseEmployee(EmployeeParameters.ALL);
        }

        //按部门排列
        protected void TreeLoad()
        {
            this.bindingSourceDepartment.DataSource = new BL.DepartmentManager().Select();
            //this.bindingSourceEmployee.DataSource = this.employeeManager.SelectOnActive();
        }

        private void SelectEmployee()
        {

            this.bindingSourceEmployee.DataSource = this.employeeManager.SelectOnActive();
        }

        private void LoadDepartMent()
        {
            IList<Model.Department> DepartList = this.bindingSourceDepartment.DataSource as IList<Model.Department>;
            foreach (Model.Department item in DepartList)
            {
                this.chkComBox_Depot.Properties.Items.Add(item.DepartmentId, item.DepartmentName);
            }
        }

        private void ReattenForm_Load(object sender, EventArgs e)
        {
            TreeLoad();
            SelectEmployee();
            LoadDepartMent();
            //LoadMonth();
        }

        //顶部按钮,日期选择功能控件事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.nccEmployee.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.EmployeeNotNull, this.Text, MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrEmpty(StartcmbDutyDate.SelectedText) || string.IsNullOrEmpty(EndcmbDutyDate.SelectedText))
            {
                MessageBox.Show(Properties.Resources.DateNotNull, this.Text, MessageBoxButtons.OK);
                return;
            }
            //this.bindingSource_atten.DataSource = _hrManager.SelectDailyInfoByEmployee(this.lookUpEmployeeId.EditValue.ToString(), new DateTime(Convert.ToInt32(this.cmbDutyDate.SelectedText.Substring(0, 4)), Convert.ToInt32(this.cmbDutyDate.SelectedText.Substring(5)), 1), "1").Tables[0];
            this.bindingSource_atten.DataSource = _hrManager.SelectDailyInfoByEmployeeForDoubleDate((this.nccEmployee.EditValue as Model.Employee).EmployeeId, this.StartcmbDutyDate.DateTime, this.EndcmbDutyDate.DateTime, "1").Tables[0];
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;

            IList<Model.Employee> detail = this.bindingSourceEmployee.DataSource as IList<Model.Employee>;
            Model.Employee Employee = detail[e.ListSourceRowIndex];
        }

        private void gridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            DataTable hrTable = this.bindingSource_atten.DataSource as DataTable;
            if (hrTable == null || hrTable.Rows.Count < 1) return;
            switch (e.Column.Name)
            {
                case "LateInMinute":
                    if (hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_LateInMinute] == null)
                        e.DisplayText = "0";
                    else
                        e.DisplayText = hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_LateInMinute].ToString();
                    break;
                case "ShouldCheckIn":
                    if (hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckIn].ToString() == "")
                        e.DisplayText = "--:--";
                    break;
                case "ShouldCheckOut":
                    if (hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckOut].ToString() == "")
                        e.DisplayText = "--:--";
                    break;
                case "ActualCheckIn":
                    if (hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckIn].ToString() == "")
                        e.DisplayText = "--:--";
                    break;
                case "ActualCheckOut":
                    if (hrTable.Rows[e.ListSourceRowIndex][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckOut].ToString() == "")
                        e.DisplayText = "--:--";
                    break;
            }
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            this.employee = this.bindingSourceEmployee.Current as Model.Employee;
            if (this.employee == null) return;
            this.nccEmployee.EditValue = this.employee;
        }

        //重新考勤按钮点击
        private void btnReatten_Click(object sender, EventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            //选择员工列表
            IList<Model.Employee> SelectItems = new List<Model.Employee>();
            SelectItems = (from i in (this.bindingSourceEmployee.DataSource as List<Model.Employee>) where i.IsChecked == true select i).ToList<Model.Employee>();
            if (SelectItems.Count == 0)
            {
                if (this.nccEmployee.EditValue == null)
                {
                    MessageBox.Show("請選擇要重新考勤的員工", "錯誤提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    SelectItems.Add(this.nccEmployee.EditValue as Model.Employee);
                }
            }
            if (string.IsNullOrEmpty(this.StartcmbDutyDate.SelectedText) && string.IsNullOrEmpty(this.EndcmbDutyDate.SelectedText))
            {
                MessageBox.Show(Properties.Resources.RequireDateToDate, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime starttime = this.StartcmbDutyDate.DateTime.Date;
            DateTime endtime = this.EndcmbDutyDate.DateTime.Date;
            if (starttime > endtime)
            {
                MessageBox.Show(Properties.Resources.StartDateGTEndDate, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("重新考勤後，該人員該區段所有【手動】輸入的出勤記錄將會被取代！", "請再次確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                do
                {
                    foreach (Model.Employee emp in SelectItems)
                    {
                        if (emp.EmployeeJoinDate <= starttime)  //考勤日已到職才計算
                        {
                            _hrManager.ReCheck(starttime.Date, emp);
                        }
                    }
                    starttime = starttime.AddDays(1);
                }
                while (starttime <= endtime);
            }
            else
            {
                return;
            }

            ////處理關於更改員工離職日期調整
            //this._hrManager.DeleteForChangeLeaveDateEmpHrDay();

            this.bindingSource_atten.DataSource = _hrManager.SelectDailyInfoByEmployeeForDoubleDate(SelectItems.First<Model.Employee>().EmployeeId, this.StartcmbDutyDate.DateTime.Date, this.EndcmbDutyDate.DateTime.Date, "1").Tables[0];
            this.nccEmployee.EditValue = SelectItems.First<Model.Employee>();
            MessageBox.Show("重新考勤成功", "錯誤提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //全选用户
        public bool selectAllFlag = true;

        private void btnSelectAllEmp_Click(object sender, EventArgs e)
        {
            if (selectAllFlag)
            {
                foreach (Model.Employee item in this.bindingSourceEmployee.DataSource as IList<Model.Employee>)
                {
                    item.IsChecked = true;
                }
                selectAllFlag = false;
                this.btnSelectAllEmp.Text = "取消全選";
            }
            else
            {
                foreach (Model.Employee item in this.bindingSourceEmployee.DataSource as IList<Model.Employee>)
                {
                    item.IsChecked = false;
                }
                selectAllFlag = true;
                this.btnSelectAllEmp.Text = "全部選擇";
            }
            this.gridControl1.RefreshDataSource();
        }

        //选定部门同事获取选定员工
        private void chkComBox_Depot_EditValueChanged(object sender, EventArgs e)
        {
            IList<Model.Employee> emplist = this.bindingSourceEmployee.DataSource as IList<Model.Employee>;
            if (emplist != null && emplist.Count != 0)
            {
                foreach (Model.Employee emp in emplist)
                {
                    emp.IsChecked = false;
                }

                foreach (string s in this.chkComBox_Depot.Properties.Items.GetCheckedValues())
                {
                    foreach (Model.Employee emp in emplist)
                    {
                        if (emp.DepartmentId == s)
                            emp.IsChecked = true;
                    }
                }
                //this.bindingSourceEmployee.DataSource = emplist;
                //this.gridControl1.DataSource = this.bindingSourceEmployee;
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}
//private void LoadMonth()
//{
//    DateTime currentDate = DateTime.Now;

//    if (currentDate.Month >= 10)
//    {
//        for (int i = 10; i >= 1; i--)
//        {

//            StartcmbDutyDate.Properties.Items.Add(currentDate.Year + "-" + i);
//            EndcmbDutyDate.Properties.Items.Add(currentDate.Year + "-" + i);
//        }
//    }
//    else
//    {
//        for (int i = currentDate.Month; i >= 1; i--)
//        {
//            StartcmbDutyDate.Properties.Items.Add(currentDate.Year + "-" + i);
//            EndcmbDutyDate.Properties.Items.Add(currentDate.Year + "-" + i);
//        }
//        int leaveCount = 10 - currentDate.Month;
//        for (int j = 12; j >= 1; j--)
//        {
//            if (leaveCount > 0)
//            {
//                StartcmbDutyDate.Properties.Items.Add((currentDate.Year - 1) + "-" + j);
//                EndcmbDutyDate.Properties.Items.Add((currentDate.Year - 1) + "-" + j);
//                leaveCount--;
//            }
//        }
//    }

//}