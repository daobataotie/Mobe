using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using Book.UI.Settings.BasicData.Employees;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Linq;

namespace Book.UI.Hr.Attendance.OverTime
{
    /*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究

    // 编 码 人: 马艳军              完成时间:2009-10-22
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class OverTimeForm : BaseEditForm
    {
        //加班
        private Model.OverTime _overtime;
        private IList<Model.OverTime> _detailList;

        //加班管理
        private BL.OverTimeManager _overTimeManager = new Book.BL.OverTimeManager();

        //员工管理
        BL.EmployeeManager employeemanager = new Book.BL.EmployeeManager();

        //部门管理
        BL.DepartmentManager deprtmentManger = new Book.BL.DepartmentManager();

        //员工Model
        Model.Employee _employee;

        //员工列表
        IList<Model.Employee> _mEmplist = new List<Model.Employee>();

        private DataSet ds = new DataSet();

        public OverTimeForm()
        {
            InitializeComponent();
            this.cmb_data_Edit.EditValueChanged += new EventHandler(cmb_data_Edit_EditValueChanged);
            this.action = "view";
            this.requireValueExceptions.Add(Model.OverTime.PROPERTY_EMPLOYEEID, new AA(Properties.Resources.EmployeeNotNull, this.newChooseContorl1));
            this.requireValueExceptions.Add(Model.OverTime.PROPERTY_EOVERTIME, new AA("請填寫加班時間", this.TextEditEoverTime));
            this.newChooseContorl1.Choose = new ChooseEmployee(EmployeeParameters.ALL);
        }

        private void OverTimeForm_Load(object sender, EventArgs e)
        {
            //InitOverTimeData();
            this.Visibles();
            //InitTreelistWhenDept();

            //加载日期
            for (int i = 0; i < 10; i++)
            {
                DateTime.Now.AddMonths(-i);
                this.cmb_data_Edit.Properties.Items.Add(DateTime.Now.AddMonths(-i).ToString("yyyy年MM月"));
            }
            this.cmb_data_Edit.SelectedIndex = 0;
            this.newChooseContorl1.Enabled = false;
            // this.chkIsHolidayDate.Enabled = false;
            EmployeeSource.DataSource = employeemanager.SelectOnActive();
            DepartmentSource.DataSource = deprtmentManger.Select();

            //进入时控制是否节假日
            this.mIsHoliday();
        }

        private void cmb_data_Edit_EditValueChanged(object sender, EventArgs e)
        {
            string year = this.cmb_data_Edit.Text.Substring(0, 4);
            string month = this.cmb_data_Edit.Text.Substring(5, 2);
            string mDate = year + "-" + month + "-" + "01";

            this.EmployeeSource.DataSource = this.employeemanager.GetHasThereEmp_ListByDateTime(DateTime.Parse(mDate).Date);
            //this.EmployeeSource.DataSource = employeemanager.
        }

        protected override void AddNew()
        {
            this._overtime = new Book.Model.OverTime();
            this._overtime.OverTimeId = Guid.NewGuid().ToString();
            this._overtime.DueDate = DateTime.Now;
            this._overtime.EoverTime = 0;
            this._overtime.OverTimeBonus = 0;
        }

        protected override void Save()
        {
            if (this.TextEditEoverTime.Text == "" || decimal.Parse(this.TextEditEoverTime.Text) == decimal.Zero)
            {
                throw new Helper.RequireValueException(Model.OverTime.PROPERTY_EOVERTIME);
            }

            //多员工加班处理
            this._mEmplist = (from Model.Employee emp in (this.EmployeeSource.DataSource as IList<Model.Employee>)
                              where emp.IsChecked == true
                              select emp).ToList<Model.Employee>();

            if (this.newChooseContorl1.EditValue == null && this._mEmplist.Count == 0)
            {
                throw new Helper.RequireValueException(Model.OverTime.PROPERTY_EMPLOYEEID);
            }
            else
            {
                if (this.newChooseContorl1.EditValue != null)
                {
                    this._overtime.Employee = this.newChooseContorl1.EditValue as Model.Employee;
                    this._overtime.EmployeeId = this._overtime.Employee.EmployeeId;

                    if (this._mEmplist == null || this._mEmplist.Count == 0)
                    {
                        this._mEmplist.Add(this._overtime.Employee);
                    }
                }
            }

            this._overtime.DueDate = this.dateEditOverTime.DateTime;
            this._overtime.OverTimeBonus = decimal.Parse(this.calcEditBonus.Text);
            this._overtime.EoverTime = Convert.ToDouble(this.TextEditEoverTime.Text);
            this._overtime.IsHoliday = this.chkIsHolidayDate.Checked;
            this._overtime.Note = this.menoDescription.Text;

            //判断是否重复加班信息
            string AllEmpId = string.Empty;

            foreach (Model.Employee de in this._mEmplist)
            {
                AllEmpId += "'" + de.EmployeeId + "',";
            }

            AllEmpId = AllEmpId.Substring(0, AllEmpId.Length - 1);

            if (!this._overTimeManager.JudgeRepeater(AllEmpId, this._mEmplist.Count, this._overtime.DueDate))
            {
                DialogResult diaResult = MessageBox.Show(this, "添加的加班信息中有與數據庫重複的記錄,是否繼續添加", Properties.Resources.AreYouSureLeading, MessageBoxButtons.YesNo);
                if (diaResult == DialogResult.No)
                {
                    throw new Helper.MessageValueException("SaveCancel");
                }
            }

            switch (this.action)
            {
                case "insert":
                    this._overTimeManager.InsertList(this._overtime, this._mEmplist);
                    break;
                case "update":
                    this._overTimeManager.Update(this._overtime);
                    break;
            }

            //定位第一人
            this.newChooseContorl1.EditValue = this._mEmplist[0];
            //取消选择
            foreach (Model.Employee emp in this.EmployeeSource.DataSource as IList<Model.Employee>)
            {
                emp.IsChecked = false;
            }

            this.gridControl2.RefreshDataSource();
        }

        public override void Refresh()
        {
            if (this._overtime == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._overtime = this._overTimeManager.Get(this._overtime.OverTimeId);
                }
            }

            //if (this._overtime.Employee != null)

            this.newChooseContorl1.EditValue = _employee;
            this.TextEditEoverTime.Text = this._overtime.EoverTime.ToString();
            this.dateEditOverTime.DateTime = this._overtime.DueDate;
            this.calcEditBonus.EditValue = this._overtime.OverTimeBonus;
            this.chkIsHolidayDate.Checked = this._overtime.IsHoliday;
            this.menoDescription.EditValue = this._overtime.Note;
            if (this.action != "update" && _employee != null)
            {
                _detailList = this._overTimeManager.SelectByEmployeeAndMonth(_employee, int.Parse(this.cmb_data_Edit.Text.Substring(0, 4)), int.Parse(this.cmb_data_Edit.Text.Substring(5, 2)));
                this.OvertimeSource2.DataSource = _detailList;
                int a = 0;
                for (a = 0; a < _detailList.Count; a++)
                {
                    if (this._overtime.OverTimeId == _detailList[a].OverTimeId)
                        break;
                }
                this.OvertimeSource2.Position = a;
            }

            switch (this.action)
            {
                case "insert":
                    this.newChooseContorl1.Enabled = false;
                    this.dateEditOverTime.Properties.ReadOnly = false;
                    this.TextEditEoverTime.Properties.ReadOnly = false;
                    this.calcEditBonus.Properties.ReadOnly = false;
                    this.chkIsHolidayDate.Properties.ReadOnly = false;
                    this.menoDescription.Properties.ReadOnly = false;
                    break;
                case "update":
                    this.newChooseContorl1.Enabled = false;
                    this.dateEditOverTime.Properties.ReadOnly = false;

                    this.TextEditEoverTime.Properties.ReadOnly = false;
                    this.calcEditBonus.Properties.ReadOnly = false;
                    this.chkIsHolidayDate.Properties.ReadOnly = false;
                    this.menoDescription.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.newChooseContorl1.Enabled = false;
                    this.dateEditOverTime.Properties.ReadOnly = true;
                    this.TextEditEoverTime.Properties.ReadOnly = true;
                    this.calcEditBonus.Properties.ReadOnly = true;
                    this.chkIsHolidayDate.Properties.ReadOnly = true;
                    this.menoDescription.Properties.ReadOnly = true;
                    break;
            }

            base.Refresh();

            this.newChooseContorl1.ShowButton = true;
            this.newChooseContorl1.Enabled = false;
            this.cmb_data_Edit.Properties.ReadOnly = false;
            this.chkEditSelectAllEmp.Properties.ReadOnly = false;

            //this.cmb_data_Edit.Enabled = true;
            //this.chkEditSelectAllEmp.Enabled = true;
        }

        protected override void Delete()
        {
            this._overtime = this.OvertimeSource2.Current as Model.OverTime;

            if (this._overtime != null)
            {
                if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this._overTimeManager.Delete(this._overtime);
                    this._overtime = this._overTimeManager.GetPrevForEmployeeYearMonth(_employee.EmployeeId, this._overtime.InsertTime.Value, int.Parse(this.cmb_data_Edit.Text.Substring(0, 4)), int.Parse(this.cmb_data_Edit.Text.Substring(5, 2)));
                }
            }

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;

            IList<Model.OverTime> overtime = this.OvertimeSource2.DataSource as IList<Model.OverTime>;

            if (overtime == null || overtime.Count < 1) return;

            Model.Employee employee = overtime[e.ListSourceRowIndex].Employee;

            if (employee == null) return;

            if (e.Column.Name == "EmpName")
            {
                e.DisplayText = employee.EmployeeName;
            }
        }

        // 点击“员工编号”连接事件
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            _employee = this.EmployeeSource.Current as Model.Employee;
            int year = int.Parse(this.cmb_data_Edit.Text.Substring(0, 4));
            int month = int.Parse(this.cmb_data_Edit.Text.Substring(5, 2));
            this._detailList = _overTimeManager.SelectByEmployeeAndMonth(_employee, year, month);
            OvertimeSource2.DataSource = this._detailList;
            gridView1.GroupPanelText = cmb_data_Edit.Text + _employee.EmployeeName + Properties.Resources.OverTime;
            this.newChooseContorl1.EditValue = _employee;
            band2();
        }

        private void band2()
        {
            _overtime = this.OvertimeSource2.Current as Model.OverTime;
            if (this._overtime == null)
            {
                this.Refresh();
            }
            else
            {
                this.action = "view";

                this.newChooseContorl1.EditValue = this._overtime.Employee;
                this.dateEditOverTime.DateTime = this._overtime.DueDate;
                this.TextEditEoverTime.EditValue = this._overtime.EoverTime;
                this.calcEditBonus.EditValue = this._overtime.OverTimeBonus;
                this.chkIsHolidayDate.Checked = this._overtime.IsHoliday;
                this.menoDescription.EditValue = this._overtime.Note;
                this.OvertimeSource2.DataSource = _detailList;
                base.Refresh();
            }
            this.cmb_data_Edit.Properties.ReadOnly = false;
            this.chkEditSelectAllEmp.Properties.ReadOnly = false;

        }

        protected override void MoveLast()
        {
            if (this.action == "insert")
            {
                this._overtime = this._overTimeManager.GetLastForEmployeeYearMonth(_employee.EmployeeId, int.Parse(this.cmb_data_Edit.Text.Substring(0, 4)), int.Parse(this.cmb_data_Edit.Text.Substring(5, 2)));
            }
        }

        //更改加班时间,调整加班津贴
        private void spinEditEoverTime_EditValueChanged(object sender, EventArgs e)
        {
            if (!this.chkIsHolidayDate.Checked)
            {
                this.mSetETBounds();
            }
        }

        //设置津贴
        private void mSetETBounds()
        {
            if (this.TextEditEoverTime.Text == "" || this.action == "view" || this.newChooseContorl1.EditValue == null)
                return;
            double dd = 0;
            Double.TryParse(this.TextEditEoverTime.Text, out dd);
            this._overtime.EoverTime = dd;
            //if ((this.newChooseContorl1.EditValue as Model.Employee).IsCadre.HasValue && (this.newChooseContorl1.EditValue as Model.Employee).IsCadre.Value)
            //{
            //    if (this._overtime.EoverTime >= 2)
            //    {
            //        this.calcEditBonus.EditValue = 40;
            //    }
            //    else
            //    {
            //        this.calcEditBonus.EditValue = 0;
            //    }
            //}
            //else
            //{
            //    if (this._overtime.EoverTime >= 3)
            //    {
            //        this.calcEditBonus.EditValue = 40;
            //    }
            //    else
            //    {
            //        this.calcEditBonus.EditValue = 0;
            //    }
            //}
        }

        //判断是否是假日
        private bool mIsHoliday()
        {
            BL.AnnualHolidayManager ahm = new Book.BL.AnnualHolidayManager();
            IList<Model.AnnualHoliday> holidaylist = ahm.Select();
            DateTime mdt = this.dateEditOverTime.DateTime;
            bool isHoliday = false;
            this.chkIsHolidayDate.Checked = false;
            foreach (Model.AnnualHoliday ah in holidaylist)
            {
                if (ah.HolidayDate.Value.Date == mdt.Date)
                {
                    this.chkIsHolidayDate.Checked = true;
                    this.calcEditBonus.EditValue = 0;
                    isHoliday = true;
                }
            }
            return isHoliday;
        }

        //更改日期
        private void dateEditOverTime_EditValueChanged(object sender, EventArgs e)
        {
            if (!this.mIsHoliday())
            {
                this.mSetETBounds();
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                if (this.OvertimeSource2.Current == null) return;
                this.band2();
            }
        }

        //列印本月所有员工加班资料
        private void barBtnPrintList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string year = this.cmb_data_Edit.Text.Substring(0, 4);
            string month = this.cmb_data_Edit.Text.Substring(5, 2);

            DateTime startdate = DateTime.Parse(year + "/" + month + "/01");
            DateTime enddate = startdate.Date.AddMonths(1).AddDays(-1);

            IList<Model.OverTime> Details = this._overTimeManager.SelectOverTimeList(startdate, enddate);

            IList<OverTimeListModel> otListModel = new List<OverTimeListModel>();

            var query = from listModel in Details
                        group listModel by listModel.EmployeeId;

            foreach (IGrouping<string, Model.OverTime> overtime in query)
            {
                OverTimeListModel otlm = new OverTimeListModel();
                otlm.EmpId = overtime.First<Model.OverTime>().Employee.IDNo;
                otlm.EmpName = overtime.First<Model.OverTime>().Employee.EmployeeName;
                double a = 0;
                StringBuilder sb = new StringBuilder();
                foreach (Model.OverTime ot in overtime)
                {
                    a += ot.EoverTime;
                    sb.Append(ot.DueDate.ToString("yyy-MM-dd") + ",");
                }
                otlm.TotalHour = a.ToString();
                otlm.DateList = sb.ToString();

                otListModel.Add(otlm);
            }

            if (otListModel == null || otListModel.Count == 0)
                return;

            OverTimeList otl = new OverTimeList(otListModel, year + "年" + month + "月");
            otl.ShowPreviewDialog();

            //new OverTimeList(year + "/" + month);
        }

        //全选员工
        private void chkEditSelectAllEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditSelectAllEmp.Checked == true)
            {
                IList<Model.Employee> emplist = this.EmployeeSource.DataSource as IList<Model.Employee>;
                foreach (Model.Employee emp in emplist)
                {
                    emp.IsChecked = true;
                }
            }
            if (chkEditSelectAllEmp.Checked == false)
            {
                IList<Model.Employee> emplist = this.EmployeeSource.DataSource as IList<Model.Employee>;
                foreach (Model.Employee emp in emplist)
                {
                    emp.IsChecked = false;
                }
            }
            this.gridControl2.RefreshDataSource();
        }

        //复选员工
        private void repositoryItemCheckEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;
            this._mEmplist = (this.EmployeeSource.DataSource as IList<Model.Employee>).Where(emp => emp.IsChecked == true).ToList<Model.Employee>();
            Model.Employee nccEmp = this.newChooseContorl1.EditValue as Model.Employee;
            Model.Employee currentEmp = this.EmployeeSource.Current as Model.Employee;

            if (currentEmp.IsChecked)
            {
                if (nccEmp == null)
                {
                    this.newChooseContorl1.EditValue = currentEmp;
                }
                else
                {
                    if (!this._mEmplist.Any(emp => emp.EmployeeId == nccEmp.EmployeeId))
                    {
                        if (nccEmp.EmployeeId != currentEmp.EmployeeId)
                        {
                            this.newChooseContorl1.EditValue = currentEmp;
                        }
                    }
                }
            }
            else
            {
                if (nccEmp.EmployeeId == currentEmp.EmployeeId)
                {
                    if (this._mEmplist != null && this._mEmplist.Count != 0)
                    {
                        this.newChooseContorl1.EditValue = this._mEmplist[0];
                    }
                    else
                    {
                        this.newChooseContorl1.EditValue = null;
                    }
                }
            }
        }
    }
}