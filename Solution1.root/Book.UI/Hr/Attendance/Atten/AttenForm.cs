using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Hr.Attendance.Atten
{
    public partial class AttenForm : DevExpress.XtraEditors.XtraForm
    {
        //打卡記錄管理實例
        BL.ClockDataManager clockdata = new Book.BL.ClockDataManager();

        //员工管理实例
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();

        //部门管理实例
        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();

        //当前待处理员工
        private Book.Model.Employee employee = null;

        //出勤记录
        private BL.HrDailyEmployeeAttendInfoManager _hrManager = new Book.BL.HrDailyEmployeeAttendInfoManager();

        public AttenForm()
        {
            InitializeComponent();
        }

        private void LoadMonth()
        {
            cmbDutyDate.Properties.Items.Clear();
            //DateTime currentDate = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                cmbDutyDate.Properties.Items.Add(DateTime.Now.Date.AddMonths((0 - i)).ToString("yyyy-MM"));
            }
            this.cmbDutyDate.SelectedIndex = 0;
            //if (currentDate.Month >= 10)
            //{
            //    for (int i = currentDate.Month; i >= currentDate.Month - 9; i--)
            //    {
            //        cmbDutyDate.Properties.Items.Add(currentDate.Year + "-" + i);
            //    }
            //}
            //else
            //{
            //    for (int i = currentDate.Month; i >= 1; i--)
            //    {
            //        cmbDutyDate.Properties.Items.Add(currentDate.Year + "-" + i);
            //    }
            //    int leaveCount = 10 - currentDate.Month;
            //    for (int j = 12; j >= 1; j--)
            //    {
            //        if (leaveCount > 0)
            //        {
            //            cmbDutyDate.Properties.Items.Add((currentDate.Year - 1) + "-" + j);
            //            leaveCount--;
            //        }
            //    }
            //}
        }

        //按部门排列
        protected void TreeLoad()
        {
            this.bindingSourceDepartment.DataSource = new BL.DepartmentManager().Select();
            //this.bindingSourceEmployee.DataSource = this.employeeManager.SelectOnActive();
        }

        private void SelectEmployee()
        {
            DateTime mdate = DateTime.Parse(this.cmbDutyDate.SelectedItem.ToString() + "-01").Date;
            this.bindingSourceEmployee.DataSource = this.employeeManager.GetHasThereEmp_ListByDateTime(mdate);
            //this.bindingSourceEmployee.DataSource = this.employeeManager.SelectOnActive();

        }

        private void AttenForm_Load(object sender, EventArgs e)
        {
            LoadMonth();
            TreeLoad();
            //SelectEmployee();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null && e.Node.ParentNode != null)
            {

                this.employee = employeeManager.Get(e.Node.Tag.ToString());
                try
                {
                }
                catch
                {
                    MessageBox.Show("开始日期填写错误!", "系統提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                try
                {
                }
                catch
                {
                    MessageBox.Show("结束日期填写错误!", "系統提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (this.employee != null)
                    this.lookUpEmployeeId.EditValue = this.employee.EmployeeId;
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
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

        //根据员工编号和日期进行查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.lookUpEmployeeId.Text))
            {
                MessageBox.Show(Properties.Resources.EmployeeNotNull, this.Text, MessageBoxButtons.OK);
                return;
            }
            if (cmbDutyDate.SelectedIndex < 0)
            {
                MessageBox.Show(Properties.Resources.DateNotNull, this.Text, MessageBoxButtons.OK);
                return;
            }
            this.bindingSource_atten.DataSource = _hrManager.SelectDailyInfoByEmployee(this.lookUpEmployeeId.EditValue.ToString(), new DateTime(Convert.ToInt32(this.cmbDutyDate.Text.Substring(0, 4)), Convert.ToInt32(this.cmbDutyDate.Text.Substring(5)), 1), "2").Tables[0];
        }

        private void lookupEmployeeName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookupEmployeeName.EditValue != null)
            {
                this.lookUpEmployeeId.EditValue = this.lookupEmployeeName.EditValue;
            }
        }

        private void lookUpEmployeeId_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEmployeeId.EditValue != null)
            {
                this.lookupEmployeeName.EditValue = this.lookUpEmployeeId.EditValue;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (lookUpEmployeeId.Text == "" || lookupEmployeeName.Text == "" || cmbDutyDate.Text == "") return;
            Model.Employee _empployee = employeeManager.Get(lookUpEmployeeId.EditValue.ToString());
            DateTime date = new DateTime();
            if (cmbDutyDate.Text != "")
            {
                date = DateTime.Parse(cmbDutyDate.Text.ToString());
            }
            DataSet ds = _hrManager.SelectByEmpMonth(_empployee, date);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            AttenreporCrystalForm form = new AttenreporCrystalForm(_empployee, date, ds);
            form.Show();
        }

        //个人月报表
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (lookUpEmployeeId.Text == "" || lookupEmployeeName.Text == "" || cmbDutyDate.Text == "") return;
            Model.Employee _empployee = employeeManager.Get(lookUpEmployeeId.EditValue.ToString());
            DateTime date = new DateTime();
            if (cmbDutyDate.Text != "")
            {
                date = DateTime.Parse(cmbDutyDate.Text.ToString());
            }
            DataSet ds = _hrManager.SelectByEmpMonth(_empployee, date);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return;
            }
            AttenreporCrystalForm1 form = new AttenreporCrystalForm1(_empployee, date);
            form.Show();
        }

        //总月报表
        private void sbtn_SearchTree_Click(object sender, EventArgs e)
        {
            if (cmbDutyDate.Text == "") return;
            //Model.Employee _empployee = employeeManager.Get(lookUpEmployeeId.EditValue.ToString());
            DateTime date = new DateTime();
            if (cmbDutyDate.Text != "")
            {
                date = DateTime.Parse(cmbDutyDate.Text.ToString());
            }
            //DataSet ds = _hrManager.SelectByEmpMonth(date);
            //DataTable dt = ds.Tables[0];
            //if (dt.Rows.Count == 0)
            //{
            //    return;
            //}
            AttenreportForm2 ff = new AttenreportForm2(date);
            ff.Show();
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            this.employee = this.bindingSourceEmployee.Current as Model.Employee;
            if (this.employee == null) return;
            lookUpEmployeeId.EditValue = employee.EmployeeId;
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;

            IList<Model.Employee> detail = this.bindingSourceEmployee.DataSource as IList<Model.Employee>;
            Model.Employee Employee = detail[e.ListSourceRowIndex];

            //if (e.Column.Name == this.gridColumn4.Name && Employee.BusinessHours != null)
            //    e.DisplayText = Convert.ToDateTime(Employee.BusinessHours.Fromtime).ToString("HH:mm") + "~" + Convert.ToDateTime(Employee.BusinessHours.ToTime).ToString("HH:mm");
        }

        //更改日期时间
        private void cmbDutyDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectEmployee();
        }
    }
}

#region Note
//private void loadatten(string empid, DateTime starttime, DateTime endtime)
//{
//    this.bindingSource_atten.DataSource = this._hrManager.Select();

//}
//private void loadatten(DateTime starttime, DateTime endtime)
//{
//    this.bindingSource_atten.DataSource = this._hrManager.Select();
//}
//private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
//{

//}
#endregion