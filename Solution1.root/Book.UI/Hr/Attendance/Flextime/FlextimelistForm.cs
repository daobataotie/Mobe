using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;
using Book.Model;
using System.Linq;

namespace Book.UI.Hr.Attendance.Flextime
{
    public partial class FlextimelistForm : DevExpress.XtraEditors.XtraForm
    {
        BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        BL.BusinessHoursManager busm = new Book.BL.BusinessHoursManager();
        BL.FlextimeManager flextimeManager = new Book.BL.FlextimeManager();
        Model.Flextime _flextime = new Book.Model.Flextime();
        Model.Employee _employee = new Book.Model.Employee();
        BL.DepartmentManager deptmanager = new Book.BL.DepartmentManager();

        public FlextimelistForm()
        {
            InitializeComponent();
        }

        private void FlextimelistForm_Load(object sender, EventArgs e)
        {
            //foreach (Model.BusinessHours BH in busm.Select())
            //{
            //    this.comboBoxEdit1.Properties.Items.Add(BH);
            //}

            this.Det_SelectDate.DateTime = DateTime.Now.Date;
            this.bindingSource3.DataSource = this.employeeManager.SelectOnActive();
            this.bindingSource1.DataSource = this.busm.Select();
            this.repositoryItemLookUpEdit1.DataSource = this.deptmanager.Select();
        }

        //判读该用户是否已经排班
        private bool check_flextimeexbyeid(string employeeid, DateTime date_flextimeex)
        {
            return this.flextimeManager.selectbyempiddate(employeeid, date_flextimeex);
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            this._employee = this.bindingSource3.Current as Model.Employee;
            if (_employee != null)
            {
                this.lookUpEdit1.EditValue = this._employee.EmployeeId;
                if (this._employee.BusinessHours != null)
                {
                    this.lookUpEdit2.EditValue = this._employee.BusinessHours;
                }

            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.Employee> details = this.bindingSource3.DataSource as IList<Model.Employee>;
            if (details == null || details.Count < 1) return;
            Model.Employee employee = details[e.ListSourceRowIndex];

            if (e.Column.Name == this.gridColumnFlexTime.Name && employee.BusinessHours != null)
                e.DisplayText = Convert.ToDateTime(employee.BusinessHours.Fromtime).ToString("HH:mm") + "~" + Convert.ToDateTime(employee.BusinessHours.ToTime).ToString("HH:mm") + "(" + employee.BusinessHours.BusinessHoursName + ")";

        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            Model.Employee emp = this.bindingSource3.Current as Model.Employee;
            if (emp != null)
            {
                lookUpEdit1.EditValue = emp.EmployeeId;
                if (emp.BusinessHours != null)
                {
                    lookUpEdit2.EditValue = emp.BusinessHours.BusinessHoursId;
                }
            }
        }

        private void sbtn_search_Click(object sender, EventArgs e)
        {
            NewMethod();
        }

        private void NewMethod()
        {
            if (Det_SelectDate.EditValue != null)
            {
                this.bindingSource2.DataSource = this.flextimeManager.getByempid(Det_SelectDate.DateTime);
            }
            else
            {
                MessageBox.Show(Properties.Resources.DateIsNull, this.Text, MessageBoxButtons.OK);
            }
        }

        //lyl--添加排班
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this._flextime = new Book.Model.Flextime();

            this._flextime.FlextimeId = Guid.NewGuid().ToString();

            IList<Model.Employee> mEmplist = (from Model.Employee emp in this.bindingSource3.DataSource as IList<Model.Employee>
                                              where emp.IsChecked == true
                                              select emp).ToList<Model.Employee>();

            if (this.lookUpEdit1.Text == "" && (mEmplist == null || mEmplist.Count == 0))
            {
                MessageBox.Show("請選擇員工！");
                return;
            }
            if (this.lookUpEdit2.Text == "")
            {
                MessageBox.Show("請選擇班別！");
                return;
            }

            if (this.Det_SelectDate.Text == "")
            {
                MessageBox.Show("請選日期！");
                return;
            }

            _flextime.BusinessHoursId = lookUpEdit2.EditValue.ToString();
            _flextime.FlexDate = this.Det_SelectDate.DateTime;

            bool oldListCountFlag = false;          //标示筛选前是否有记录,后面将对mEmplist筛选
            if (mEmplist == null || mEmplist.Count == 0)
            {
                _flextime.EmployeeId = this.lookUpEdit1.EditValue.ToString();
                bool isexist = flextimeManager.selectbyempiddate(this.lookUpEdit1.EditValue.ToString(), this.Det_SelectDate.DateTime);
                if (isexist)
                {
                    MessageBox.Show(Properties.Resources.ExistsObject);
                    return;
                }
                oldListCountFlag = true;
            }
            else
            {
                _flextime.EmployeeId = mEmplist.First<Model.Employee>().EmployeeId;
                flextimeManager.selectbyempListdate(mEmplist, this.Det_SelectDate.DateTime);
                if (mEmplist == null || mEmplist.Count == 0)
                    oldListCountFlag = false;
            }

            if (mEmplist != null && mEmplist.Count != 0)
            {
                this.flextimeManager.Insert(this._flextime, mEmplist);
            }
            else
            {
                if (oldListCountFlag)
                    this.flextimeManager.Insert(this._flextime);
            }

            MessageBox.Show(Properties.Resources.Addsuccess);

            NewMethod();
        }

        //lyl--gridview2展示時間列
        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.Flextime> details = this.bindingSource2.DataSource as IList<Model.Flextime>;
            if (details == null || details.Count < 1) return;
            Model.Flextime flextime = details[e.ListSourceRowIndex];

            if (e.Column.Name == this.gridColumnFlextimes.Name && flextime.BusinessHours != null)
                e.DisplayText = Convert.ToDateTime(flextime.BusinessHours.Fromtime).ToString("HH:mm") + "~" + Convert.ToDateTime(flextime.BusinessHours.ToTime).ToString("HH:mm");
        }

        //lyl--刪除當前行的排班
        private void sbtn_Delete_Click(object sender, EventArgs e)
        {
            this._flextime = bindingSource2.Current as Model.Flextime;
            if (this._flextime == null) return;
            this.flextimeManager.Delete(this._flextime.FlextimeId);
            NewMethod();
        }
    }
}