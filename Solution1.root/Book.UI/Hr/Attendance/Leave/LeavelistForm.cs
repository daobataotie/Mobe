using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.BasicData;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Linq;
namespace Book.UI.Hr.Attendance.Leave
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军              完成时间:2009-11-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class LeavelistForm : BaseEditForm
    {
        BL.EmployeeManager empm = new Book.BL.EmployeeManager();
        BL.LeaveTypeManager leaveTypeM = new Book.BL.LeaveTypeManager();
        BL.LeaveManager leaveManamer = new Book.BL.LeaveManager();
        BL.EmployeeManager employeemanager = new Book.BL.EmployeeManager();
        BL.DepartmentManager deprtmentManger = new Book.BL.DepartmentManager();
        Model.Employee _employee;
        private Model.Leave _leave;
        private int flag = 0;
        IList<Model.Leave> _leaveList;

        public LeavelistForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.Leave.PRO_EmployeeId, new AA(Properties.Resources.EmployeeNotNull, this.newChooseContorl1));
            this.requireValueExceptions.Add(Model.Leave.PRO_LeaveTypeId, new AA("請假類別不能為空", this.lookUpEdit1));

            this.invalidValueExceptions.Add(Model.Leave.PRO_LeaveDateCount, new AA("終止時間有誤", this.dateEditLeaveDateEnd));
            this.invalidValueExceptions.Add(Model.Leave.PRO_LeaveId + "_1", new AA("請假信息重複,請重新添加", this.dateEditLeaveDateStart));
            this.invalidValueExceptions.Add(Model.Leave.PRO_LeaveDate + "_IsHoliday", new AA("請假日期不能為假日", this.dateEditLeaveDateStart));

            this.newChooseContorl1.Choose = new ChooseEmployee(EmployeeParameters.ALL);

            this.bindingSource1.DataSource = this.empm.SelectOnActive();
            _employee = (this.bindingSource1.DataSource as IList<Model.Employee>)[0];
            this.bindingSourcetype.DataSource = this.leaveTypeM.Select();
            this.repositoryItemLookUpEdit1.DataSource = this.deprtmentManger.Select();

            //默认加上本年与前一年
            this.dateedit.Properties.Items.Add(System.DateTime.Now.ToString("yyyy年"));
            this.dateedit.Properties.Items.Add(System.DateTime.Now.AddYears(-1).ToString("yyyy年"));
            this.dateedit.SelectedIndex = 0;

            //  this._employee = this.bindingSource1.Current as Model.Employee;
            // this.gridView1.GroupPanelText = dateedit.Text + _employee.EmployeeName + Properties.Resources.YearLeaveManager;
            //  string year = this.dateedit.Text.Substring(0, 4);
            // DataTable dt = this.leaveManamer.GetLeaveInfoByEmployeeId(this._employee.EmployeeId, year).Tables[0];
            //   _leaveList = this.leaveManamer.Getleavebyempidmonth(_employee.EmployeeId, year, null);
            //   this.bindingSource2.DataSource = _leaveList;
            ////    this.newChooseContorl1.EditValue = _employee;
            //if (this.bindingSource2.Current != null)
            //{
            //    object id = (this.bindingSource2.Current as DataRowView)["LeaveId"];
            //    if (id != null)
            //    {
            //        this._leave = this.leaveManamer.Get(id.ToString());
            //    }
            //}
            this.action = "view";
        }

        private void LeavelistForm_Load(object sender, EventArgs e)
        {
            this.Visibles();
        }

        //单击编号连接，显示详细
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            _employee = this.bindingSource1.Current as Model.Employee;
            gridView1.GroupPanelText = dateedit.Text + _employee.EmployeeName + Properties.Resources.YearLeaveManager;
            int year = Int32.Parse(this.dateedit.Text.Substring(0, 4));
            this.newChooseContorl1.EditValue = _employee;
            this._leaveList = this.leaveManamer.Getleavebyempidmonth(_employee.EmployeeId, year, null);
            this.bindingSource2.DataSource = this._leaveList;
            band2();
        }

        private void band2()
        {
            _leave = this.bindingSource2.Current as Model.Leave;
            if (this._leave == null)
            {
                this.Refresh();
            }
            else
            {
                this.action = "view";

                //控件赋值
                // this.newChooseContorl1.EditValue = _employee;
                if ((global::Helper.DateTimeParse.DateTimeEquls(this._leave.LeaveDate, global::Helper.DateTimeParse.NullDate)) || (_leave.LeaveDate == new DateTime()))
                {
                    this.dateEditLeaveDateStart.EditValue = null;
                }
                else
                {
                    dateEditLeaveDateStart.DateTime = _leave.LeaveDate.Value;
                }

                cmbLeaveDayCount.SelectedIndex = _leave.LeaveRange.HasValue ? this._leave.LeaveRange.Value : 0;
                //this.calcLeaveCount.Value = decimal.Parse((this.cmbLeaveDayCount.SelectedIndex == 0 ? 1 : 0.5).ToString());
                lookUpEdit1.EditValue = _leave.LeaveTypeId;
                memoEditLeaveDesc.Text = _leave.LeaveText;

                this.newChooseContorl1.ShowButton = true;
                this.newChooseContorl1.Enabled = false;
                this.dateedit.Enabled = true;
                //this.calcLeaveCount.Enabled = this._leave.LeaveRange > 2 ? true : false;
                base.Refresh();
            }
        }

        protected override void AddNew()
        {
            this._leave = new Book.Model.Leave();
            this._leave.LeaveId = Guid.NewGuid().ToString();
            this._leave.LeaveDate = DateTime.Now.Date;
            this._leave.LeaveRange = 0;
        }

        protected override void MoveLast()
        {
            // if (this.action == "insert")
            {
                this._leave = this.leaveManamer.GetLastForEmployeeYear(_employee.EmployeeId, new DateTime(int.Parse(this.dateedit.Text.Substring(0, 4)), 1, 1));
            }
        }

        public override void Refresh()
        {
            if (this._leave == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._leave = this.leaveManamer.Get(this._leave.LeaveId);
                }
            }

            this.newChooseContorl1.EditValue = _employee;
            if ((global::Helper.DateTimeParse.DateTimeEquls(this._leave.LeaveDate, global::Helper.DateTimeParse.NullDate)) || (_leave.LeaveDate == new DateTime()))
            {
                this.dateEditLeaveDateStart.EditValue = null;
            }
            else
            {
                dateEditLeaveDateStart.DateTime = _leave.LeaveDate.Value;
            }

            //默认终止日期为null
            this.dateEditLeaveDateEnd.EditValue = null;
            cmbLeaveDayCount.SelectedIndex = _leave.LeaveRange.HasValue ? this._leave.LeaveRange.Value : 0;
            //this.calcLeaveCount.Value = decimal.Parse((this.cmbLeaveDayCount.SelectedIndex == 0 ? 1 : 0.5).ToString());
            lookUpEdit1.EditValue = _leave.LeaveTypeId;
            memoEditLeaveDesc.Text = _leave.LeaveText;
            if (this.action != "update" && _employee != null)
            {
                _leaveList = this.leaveManamer.Getleavebyempidmonth(_employee.EmployeeId, int.Parse(this.dateedit.Text.Substring(0, 4)), null);
                this.bindingSource2.DataSource = _leaveList;
                int a = 0;
                for (a = 0; a < _leaveList.Count; a++)
                {
                    if (this._leave.LeaveId == _leaveList[a].LeaveId)
                        break;
                }
                this.bindingSource2.Position = a;
            }
            base.Refresh();

            if (this.bindingSource2.Current == null)
            {
                this.VisiblesLinks0();
            }
            this.newChooseContorl1.ShowButton = true;
            this.newChooseContorl1.Enabled = false;
            this.dateedit.Enabled = true;
            //this.calcLeaveCount.Enabled = this._leave.LeaveRange > 2 ? true : false;
            this.chkIsAll.Enabled = true;

            //this.bar1.ItemLinks[3].Visible = false;       //可以修改
        }

        protected override bool HasRows()
        {
            return this.leaveManamer.HasRows();
        }

        protected override void Save()
        {
            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;

            if (this.lookUpEdit1.EditValue == null)
                throw new Helper.RequireValueException(Model.Leave.PRO_LeaveTypeId);

            this._leave.LeaveType = this.leaveTypeM.Get(this.lookUpEdit1.EditValue.ToString());
            this._leave.LeaveTypeId = this.lookUpEdit1.EditValue.ToString();

            _leave.Employee = this.newChooseContorl1.EditValue as Model.Employee;
            if (_leave.Employee != null)
            {
                _leave.EmployeeId = _leave.Employee.EmployeeId;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditLeaveDateStart.DateTime, new DateTime()))
            {
                _leave.LeaveDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                _leave.LeaveDate = dateEditLeaveDateStart.DateTime;
            }
            IList<Model.Employee> emplist = this.bindingSource1.DataSource as IList<Model.Employee>;
            emplist = (from list in emplist
                       where list.IsChecked == true
                       select list).ToList<Model.Employee>();

            _leave.LeaveRange = this.cmbLeaveDayCount.SelectedIndex == 3 ? 0 : this.cmbLeaveDayCount.SelectedIndex;

            //请假天数
            //_leave.LeaveDateCount = this.calcLeaveCount.EditValue == null ? 0 : this.calcLeaveCount.Value;

            if (this.dateEditLeaveDateEnd.EditValue == null)
            {
                _leave.LeaveDateCount = 1;
            }
            else
            {
                if (this.dateEditLeaveDateStart.DateTime.Date > this.dateEditLeaveDateEnd.DateTime.Date)
                { throw new Helper.InvalidValueException(Model.Leave.PRO_LeaveDateCount); }
                else
                {
                    TimeSpan ts = this.dateEditLeaveDateEnd.DateTime.Date - this.dateEditLeaveDateStart.DateTime.Date;
                    _leave.LeaveDateCount = ts.Days + 1;
                }
            }
            _leave.LeaveText = memoEditLeaveDesc.Text;
            switch (this.action)
            {
                case "insert":
                    if (emplist.Count > 0)
                        this.leaveManamer.Insert(this._leave, emplist);
                    else
                        this.leaveManamer.Insert(this._leave);
                    break;
                case "update":
                    this.leaveManamer.Update(this._leave);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._leave == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.leaveManamer.Delete(this._leave);
            this._leave = this.leaveManamer.GetPrevForEmployeeYear(_employee.EmployeeId, this._leave.LeaveDate.Value);
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

        private void cmbLeaveDayCount_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                if (this.bindingSource2.Current == null) return;
                band2();
            }
        }

        private void chkIsAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsAll.Checked == true)
            {
                IList<Model.Employee> emplist = this.bindingSource1.DataSource as IList<Model.Employee>;
                foreach (Model.Employee emp in emplist)
                {
                    emp.IsChecked = true;
                }
            }
            if (chkIsAll.Checked == false)
            {
                IList<Model.Employee> emplist = this.bindingSource1.DataSource as IList<Model.Employee>;
                foreach (Model.Employee emp in emplist)
                {
                    emp.IsChecked = false;
                }
            }

            this.gridControl2.RefreshDataSource();
        }

        //连打列印
        private void barBtnListPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LeaveDateSelect f = new LeaveDateSelect();
            f.ShowDialog();
        }

        //日期区间删除
        private void barBtnDelbyCondition_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                DateTime startdate = (f.Condition as Query.ConditionA).StartDate;
                DateTime enddate = (f.Condition as Query.ConditionA).EndDate;

                IList<Model.Employee> emplist = (from Model.Employee emp in (this.bindingSource1.DataSource as IList<Model.Employee>)
                                                 where emp.IsChecked == true
                                                 select emp).ToList<Model.Employee>();
                if (emplist == null || emplist.Count == 0)
                {
                    emplist.Add(this._employee);
                }

                this.leaveManamer.DeleteByDateRangeEmp(emplist, startdate, enddate);

                //控制页面
                this._employee = emplist.First<Model.Employee>();
                this.MoveLast();
                this.Refresh();
            }
        }

        //时间区间查询
        private void barBtnSearchBetweenDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<Model.Employee> emplist = this.bindingSource1.DataSource as IList<Model.Employee>;
            emplist = (from list in emplist
                       where list.IsChecked == true
                       select list).ToList<Model.Employee>();

            Model.Employee emp = this.newChooseContorl1.EditValue as Model.Employee;
            if (emp == null && (emplist == null || emplist.Count == 0))
                throw new Helper.RequireValueException(Model.Leave.PRO_EmployeeId);
            else
                if (emplist == null || emplist.Count == 0)
                    emplist.Add(emp);
            LeaveListBetweenDate f = new LeaveListBetweenDate(emplist);

            f.ShowDialog(this);
        }
    }
}
#region 注释部分
//switch (this.action)
//{
//    case "insert":
//        // newChooseContorl1.ButtonReadOnly = true;
//        dateEditLeaveDate.Properties.ReadOnly = false;
//        cmbLeaveDayCount.Properties.ReadOnly = false;
//        memoEditLeaveDesc.Properties.ReadOnly = false;
//        break;
//    case "update":
//        //newChooseContorl1.ButtonReadOnly = true ;
//        dateEditLeaveDate.Properties.ReadOnly = false;
//        cmbLeaveDayCount.Properties.ReadOnly = false;
//        memoEditLeaveDesc.Properties.ReadOnly = false;
//        break;
//    case "view":
//        //newChooseContorl1.ButtonReadOnly = true ;
//        dateEditLeaveDate.Properties.ReadOnly = true;
//        cmbLeaveDayCount.Properties.ReadOnly = true;
//        memoEditLeaveDesc.Properties.ReadOnly = true;
//        break;
//    default:
//        break;
//}
#region 更改请假天数
//private void calcLeaveCount_EditValueChanged(object sender, EventArgs e)
//{
//    if (this.calcLeaveCount.Value == decimal.Parse("0.5"))
//    {
//        this.cmbLeaveDayCount.SelectedIndex = 1;
//    }
//    if (this.calcLeaveCount.Value == 1)
//    {
//        this.cmbLeaveDayCount.SelectedIndex = 0;
//    }
//    this.calcLeaveCount.Enabled = this.calcLeaveCount.Value > 1 ? true : false;
//}
#endregion
#endregion