using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;

namespace Book.UI.Hr.Attendance.FlecTime
{
    public partial class FlexTimeListForm : DevExpress.XtraEditors.XtraForm
    {
        //员工管理
        private Book.BL.EmployeeManager employeemanager = new Book.BL.EmployeeManager();
        //部门管理
        private Book.BL.DepartmentManager departmanager = new Book.BL.DepartmentManager();
        //排班管理
        private Book.BL.FlextimeManager flextimemanager = new Book.BL.FlextimeManager();
        //班别管理
        private Book.BL.BusinessHoursManager businesshourmanager = new Book.BL.BusinessHoursManager();
        //班别model
        private Book.Model.BusinessHours _business = new Book.Model.BusinessHours();
        //员工model
        private Book.Model.Employee _employee = new Book.Model.Employee();
        //排班model
        private Book.Model.Flextime fl;
        //已經存在,覆蓋
        private bool Isconvert;
        //已經存在,不覆蓋
        private bool Notconvert;

        //无参构造
        public FlexTimeListForm()
        {
            InitializeComponent();
            this.date_Start.Enabled = false;
            this.date_End.Enabled = false;
        }

        private void FlexTimeListForm_Load(object sender, EventArgs e)
        {
            //加载班别
            //foreach (Model.BusinessHours BH in businesshourmanager.Select())
            //{
            //    this.cmb_business.Properties.Items.Add(BH);
            //}

            this.EmployeeSource.DataSource = employeemanager.SelectOnActive();
            this.DepartSource.DataSource = departmanager.Select();
            this.bindingSource1.DataSource = this.businesshourmanager.Select();

            //this.comBox_Month.SelectedText = "01";

            //初始化日期选择
            int mYear = DateTime.Now.Year;
            this.comBox_Year.Properties.Items.Clear();
            for (int i = mYear + 1; i > mYear - 5; i--)
            {
                this.comBox_Year.Properties.Items.Add(i.ToString());
            }

            this.comBox_Year.SelectedIndex = 1;
            this.comBox_Month.SelectedIndex = DateTime.Now.Month - 1;
            string s = this.comBox_Year.SelectedItem.ToString();
            string ss = this.comBox_Month.SelectedItem.ToString();


            //int mMonthMaxDay = DateTime.Parse(this.comBox_Year.SelectedItem.ToString() + "/" + this.comBox_Month.SelectedItem.ToString() + "/01").Date.AddMonths(1).AddDays(-1).Day;
            //for (int i = 1; i <= mMonthMaxDay; i++)
            //{
            //    this.comBox_Day.Properties.Items.Add(i.ToString(), i.ToString());
            //}
        }

        //员工编号连接事件
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            Isconvert = false;     //默認覆蓋為假
            Notconvert = false;    //默認不覆蓋為假
            _employee = this.EmployeeSource.Current as Book.Model.Employee;
            if (_employee != null)
            {
                this.FlexTimeSource.DataSource = flextimemanager.getByempid(_employee.EmployeeId);
                this.lookUpEdit1.EditValue = _employee.EmployeeId;
                this.cmb_business.EditValue = _employee.BusinessHours.BusinessHoursId;
                //IList<Model.Flextime> list= flextimemanager.getByempid(_employee.EmployeeId); 
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;

            IList<Model.Employee> employeelist = this.EmployeeSource.DataSource as IList<Model.Employee>;

            if (employeelist == null || employeelist.Count < 1) return;

            _business = employeelist[e.ListSourceRowIndex].BusinessHours;

            switch (e.Column.Name)
            {
                case "BusinessTime":

                    if (_business == null) return;
                    e.DisplayText = Convert.ToDateTime(_business.Fromtime).ToString("HH:mm") + "~" + Convert.ToDateTime(_business.ToTime).ToString("HH:mm") + "(" + _business.BusinessHoursName + ")";
                    break;
            }
        }

        private void gridView2_CustomColumnDisplayText_1(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;

            IList<Model.Flextime> flextimelist = this.FlexTimeSource.DataSource as IList<Model.Flextime>;

            if (flextimelist == null || flextimelist.Count < 1) return;

            _business = flextimelist[e.ListSourceRowIndex].BusinessHours;
            if (_business == null) return;
            switch (e.Column.Name)
            {
                case "BusinessDate":
                    e.DisplayText = Convert.ToDateTime(_business.Fromtime).ToString("HH:mm") + "~" + Convert.ToDateTime(_business.ToTime).ToString("HH:mm");
                    break;
            }
        }

        //按区段排班
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //实例化model
            if (fl == null)
            {
                fl = new Book.Model.Flextime();
            }

            //获取选择员工
            IList<Model.Employee> mEmps = (from Model.Employee emp in (this.EmployeeSource.DataSource as IList<Model.Employee>)
                                           where emp.IsChecked == true
                                           select emp).ToList<Model.Employee>();

            //非空验证
            if (this.lookUpEdit1.Text == "" && (mEmps == null || mEmps.Count == 0))
            {
                MessageBox.Show("請選擇員工！");
                return;
            }

            string empids = string.Empty;
            if (mEmps != null && mEmps.Count != 0)
            {
                StringBuilder sb1 = new StringBuilder();

                foreach (Model.Employee emp in mEmps)
                {
                    sb1.Append("'" + emp.EmployeeId + "',");
                }

                empids = sb1.ToString().Substring(0, sb1.ToString().Length - 1);

                //empids = (from Model.Employee emp in mEmps
                //          select emp.EmployeeId).Aggregate((s1, s2) => string.Format("'{0}','{1}'", s1, s2));
            }
            else
            {
                Model.Employee aemp = (from Model.Employee em in (this.EmployeeSource.DataSource as IList<Model.Employee>)
                                       where em.EmployeeId == this.lookUpEdit1.EditValue.ToString()
                                       select em).First<Model.Employee>();
                mEmps.Add(aemp);
                empids = "'" + this.lookUpEdit1.EditValue.ToString() + "'";
            }

            if (this.cmb_business.Text == "")
            {
                MessageBox.Show("請選擇班別！");
                return;
            }

            if (this.radioGroup1.SelectedIndex == 0 && (this.comBox_Day.Properties.Items.GetCheckedValues() == null || this.comBox_Day.Properties.Items.GetCheckedValues().Count == 0))
            {
                MessageBox.Show("請選擇排班日期!");
                return;
            }
            else if (this.radioGroup1.SelectedIndex == 1 && (this.date_Start.EditValue == null || this.date_End.EditValue == null))
            {
                MessageBox.Show("請完善排班區間");
                return;
            }

            //获取日期列表
            string flexdates = string.Empty;
            if (this.radioGroup1.SelectedIndex == 0)
            {
                string mYearAndMonth = this.comBox_Year.SelectedItem.ToString() + "/" + this.comBox_Month.SelectedItem.ToString() + "/";
                IList<string> daylist = (from string s in this.comBox_Day.Properties.Items.GetCheckedValues()
                                         select s).ToList<string>();
                StringBuilder sb = new StringBuilder();
                foreach (string str in daylist)
                {
                    sb.Append("'" + DateTime.Parse((mYearAndMonth + str)).ToString("yyyy-MM-dd") + "',");
                }

                flexdates = sb.ToString().Substring(0, sb.ToString().Length - 1);
            }
            else
            {
                for (DateTime date = this.date_Start.DateTime; date <= this.date_End.DateTime; date = date.AddDays(1))
                {
                    flexdates += "'" + date.ToString("yyyy-MM-dd") + "',";
                }
                flexdates = flexdates.Substring(0, flexdates.Length - 1);
            }

            //已存在记录列表
            IList<Model.Flextime> existFlex = this.flextimemanager.selectByEmpidsAndDates(empids, flexdates);

            fl.BusinessHoursId = cmb_business.EditValue.ToString();

            #region 连续排班
            //DateTime a = this.dateEdit_start.DateTime.Date;
            //DateTime b = this.dateEdit_end.DateTime.Date;

            //while (a <= b)
            //{
            //    fl.FlextimeId = Guid.NewGuid().ToString();
            //    //查詢此員工是否已經排班
            //    if (this.flextimemanager.getbyempiddate(fl.EmployeeId, a) != null)
            //    {
            //        if (Isconvert == false && Notconvert == false)
            //        {
            //            if (MessageBox.Show(Properties.Resources.ExistObjectIsConvert, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            //            {
            //                Isconvert = true;   //設置為覆蓋重複項
            //            }
            //            else
            //            {
            //                Notconvert = true;  //設置為不覆蓋
            //            }
            //        }
            //        if (Isconvert)
            //        {
            //            this.flextimemanager.DeleteByEmpidDate(fl.EmployeeId, a);
            //            fl.FlexDate = a;
            //            flextimemanager.Insert(fl);
            //        }
            //        a = a.AddDays(1);
            //    }
            //    else
            //    {
            //        fl.FlexDate = a;
            //        flextimemanager.Insert(fl);
            //        a = a.AddDays(1);
            //    }
            //}
            #endregion

            //挑选排班
            DateTime mFlexDate;
            try
            {
                BL.V.BeginTransaction();
                foreach (Model.Employee emp in mEmps)
                {
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        foreach (string s in this.comBox_Day.Properties.Items.GetCheckedValues())
                        {
                            fl.Employee = emp;
                            fl.EmployeeId = emp.EmployeeId;
                            fl.FlextimeId = Guid.NewGuid().ToString();
                            mFlexDate = DateTime.Parse(this.comBox_Year.SelectedItem.ToString() + "/" + this.comBox_Month.SelectedItem.ToString() + "/" + s).Date;
                            //查詢此員工是否已經排班
                            bool hasFlex = existFlex.Any(le => le.EmployeeId == emp.EmployeeId && le.FlexDate.Value.ToString("yyyy-MM-dd") == mFlexDate.ToString("yyyy-MM-dd"));
                            if (hasFlex)
                            {
                                Isconvert = false;

                                DialogResult d = MessageBox.Show("員工:" + emp.EmployeeName + "," + mFlexDate.ToString("yyyy-MM-dd") + "排班記錄" + Properties.Resources.ExistObjectIsConvert, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                switch (d)
                                {
                                    case DialogResult.Yes:
                                        Isconvert = true; //設置為覆蓋重複項
                                        break;
                                    case DialogResult.No://設置為覆蓋重複項
                                        continue;
                                    default:
                                        throw new global::Helper.MessageValueException("");

                                }
                                if (Isconvert)
                                {
                                    this.flextimemanager.DeleteByEmpidDate(fl.EmployeeId, mFlexDate);
                                    fl.FlexDate = mFlexDate;
                                    flextimemanager.Insert(fl);
                                }
                            }
                            else
                            {
                                fl.FlexDate = mFlexDate;
                                flextimemanager.Insert(fl);
                            }
                        }
                    }
                    else
                    {
                        foreach (string s in flexdates.Split(','))
                        {
                            fl.Employee = emp;
                            fl.EmployeeId = emp.EmployeeId;
                            fl.FlextimeId = Guid.NewGuid().ToString();
                            mFlexDate = Convert.ToDateTime(s.Replace('\'', ' '));
                            bool hasFlex = existFlex.Any(le => le.EmployeeId == emp.EmployeeId && le.FlexDate.Value.ToString("yyyy-MM-dd") == mFlexDate.ToString("yyyy-MM-dd"));
                            if (hasFlex)
                            {
                                Isconvert = false;

                                DialogResult d = MessageBox.Show("員工:" + emp.EmployeeName + "," + mFlexDate.ToString("yyyy-MM-dd") + "排班記錄" + Properties.Resources.ExistObjectIsConvert, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                                switch (d)
                                {
                                    case DialogResult.Yes:
                                        Isconvert = true; //設置為覆蓋重複項
                                        break;
                                    case DialogResult.No://設置為覆蓋重複項
                                        continue;
                                    default:
                                        throw new global::Helper.MessageValueException("");

                                }
                                if (Isconvert)
                                {
                                    this.flextimemanager.DeleteByEmpidDate(fl.EmployeeId, mFlexDate);
                                    fl.FlexDate = mFlexDate;
                                    flextimemanager.Insert(fl);
                                }
                            }
                            else
                            {
                                fl.FlexDate = mFlexDate;
                                flextimemanager.Insert(fl);
                            }
                        }
                    }
                }
                BL.V.CommitTransaction();
                MessageBox.Show(Properties.Resources.SaveSuccess, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (global::Helper.MessageValueException)
            {
                BL.V.RollbackTransaction();
                return;

            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

            //弹出操作结果提示
            //MessageBox.Show(Properties.Resources.Addsuccess);
            for (int i = 0; i < this.comBox_Day.Properties.Items.Count; i++)
            {
                this.comBox_Day.Properties.Items[i].CheckState = CheckState.Unchecked;
            }
            this.FlexTimeSource.DataSource = flextimemanager.getByempid(fl.EmployeeId);
        }

        //删除多余排班
        private void sbtn_Delete_Click(object sender, EventArgs e)
        {
            if (FlexTimeSource.Current == null) return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            Model.Flextime _flex = FlexTimeSource.Current as Model.Flextime;
            flextimemanager.Delete(_flex.FlextimeId);
            this.FlexTimeSource.DataSource = flextimemanager.getByempid(_flex.EmployeeId);
        }

        //private void dateEdit_end_EditValueChanged(object sender, EventArgs e)
        //{

        //    if (dateEdit_end.DateTime < dateEdit_start.DateTime)
        //        dateEdit_end.DateTime = dateEdit_start.DateTime;
        //}

        //private void dateEdit_start_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (dateEdit_end.Text == "" || dateEdit_start.Text == "") return;
        //    if (dateEdit_end.DateTime < dateEdit_start.DateTime)
        //        dateEdit_end.DateTime = dateEdit_start.DateTime;
        //}

        //日期更改
        private void comBox_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.comBox_Year.SelectedItem.ToString()) || this.comBox_Month.SelectedItem == null || string.IsNullOrEmpty(this.comBox_Month.SelectedItem.ToString()))
                return;
            int mMonthMaxDay = DateTime.Parse(this.comBox_Year.SelectedItem.ToString() + "/" + this.comBox_Month.SelectedItem.ToString() + "/01").Date.AddMonths(1).AddDays(-1).Day;
            this.comBox_Day.Properties.Items.Clear();
            for (int i = 1; i <= mMonthMaxDay; i++)
            {
                this.comBox_Day.Properties.Items.Add(i.ToString(), i.ToString());
            }
        }

        private void comBox_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.comBox_Year.SelectedItem.ToString()))
                return;
            int mMonthMaxDay = DateTime.Parse(this.comBox_Year.SelectedItem.ToString() + "/" + this.comBox_Month.SelectedItem.ToString() + "/01").Date.AddMonths(1).AddDays(-1).Day;
            this.comBox_Day.Properties.Items.Clear();
            for (int i = 1; i <= mMonthMaxDay; i++)
            {
                this.comBox_Day.Properties.Items.Add(i.ToString(), i.ToString());
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.comBox_Year.Enabled = true;
                this.comBox_Month.Enabled = true;
                this.comBox_Day.Enabled = true;
                //this.date_Start.EditValue = null;
                //this.date_End.EditValue = null;
                this.date_Start.Enabled = false;
                this.date_End.Enabled = false;
            }
            else
            {
                this.date_Start.Enabled = true;
                this.date_End.Enabled = true;
                //this.comBox_Year.EditValue = null;
                //this.comBox_Month.EditValue = null;
                //this.comBox_Day.EditValue = null;
                this.comBox_Year.Enabled = false;
                this.comBox_Month.Enabled = false;
                this.comBox_Day.Enabled = false;
            }
        }
    }
}