using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Linq;

namespace Book.UI.Settings.BasicData.Employees
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-22
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        //标致是否是保存后 执行 TREELIST事件的
        private int flag = 0;
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        protected BL.BankManager bankManager = new Book.BL.BankManager();
        protected BL.AcademicBackGroundManager academicBackGroundManager = new Book.BL.AcademicBackGroundManager();
        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        protected BL.DutyManager dutyManager = new Book.BL.DutyManager();
        protected BL.BusinessHoursManager businessHoursManager = new Book.BL.BusinessHoursManager();
        protected BL.CompanyManager companyManager = new Book.BL.CompanyManager();
        private Book.Model.Employee employee = null;
        private BL.SettingManager settingManager = new Book.BL.SettingManager();
        private int tag = 0;

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.Employee.PROPERTY_IDNO, new AA(Properties.Resources.RequireDataForId, this.textEditIDNo));
            this.requireValueExceptions.Add(Model.Employee.PROPERTY_EMPLOYEENAME, new AA(Properties.Resources.RequireDataForName, this.textEditEmployeeName));
            this.requireValueExceptions.Add(Model.Employee.PROPERTY_EMPLOYEEGENDER, new AA(Properties.Resources.RequireDataForSex, this.comboBoxEditEmployeeGender));
            this.requireValueExceptions.Add(Model.Employee.PROPERTY_EMPLOYEEJOINDATE, new AA("員工到職日期不能為空", this.dateEditEmployeeJoinDate));

            this.invalidValueExceptions.Add(Model.Employee.PROPERTY_IDNO, new AA(Properties.Resources.EntityExists, this.textEditIDNo));
            this.invalidValueExceptions.Add(Model.Employee.PROPERTY_IDNO + "1", new AA(Properties.Resources.InputError, this.textEditIDNo));
            this.action = "view";
        }

        public EditForm(Model.Employee emp)
            : this()
        {
            //if (emp == null)
            //    throw new ArithmeticException();
            this.employee = emp;

            this.action = "update";
        }

        public EditForm(Book.Model.Employee emp, string action)
            : this()
        {
            //if(cmpy == null)
            //    throw new ArithmeticException();
            this.employee = emp;

            this.action = action;
        }


        public EditForm(Model.Department Department)
            : this()
        {

            this.employee = new Book.Model.Employee();
            this.employee.EmployeeId = Guid.NewGuid().ToString();
            this.employee.Department = Department;
            tag = 1;

        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            //this.bindingSourceEmployee.DataSource = this.employeeManager.SelectOnActive();
            //this.radioGroup1.SelectedIndex = 0;
            //TreeLoad();
            this.repositoryItemLookUpEdit1.DataSource = departmentManager.Select();
            this.barButtonItemPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        public override object EditedItem
        {
            get
            {
                return this.employee;
            }
        }

        protected override void Delete()
        {
            if (this.employee == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.employeeManager.Delete(this.employee);
            this.employee = this.employeeManager.GetNext(this.employee);
            if (this.employee == null)
            {
                this.employee = this.employeeManager.GetLast();
            }
        }

        protected override void Save()
        {
            this.employee.AcademicBackGround = this.comboBoxEditAcademicBackGroundId.EditValue as Model.AcademicBackGround;
            this.employee.AcademicBackGroundId = this.employee.AcademicBackGround == null ? null : this.employee.AcademicBackGround.AcademicBackGroundId;
            this.employee.Bank = this.comboBoxEditBankId.EditValue as Model.Bank;
            this.employee.BankId = this.employee.Bank == null ? null : this.employee.Bank.BankId;
            this.employee.BankAccount = this.textEditBankAccount.Text;
            this.employee.BusinessHours = this.comboBoxEditBusinessHoursId.EditValue as Model.BusinessHours;
            if (this.employee.BusinessHours != null)
                this.employee.BusinessHoursId = this.employee.BusinessHours.BusinessHoursId;
            else
                this.employee.BusinessHoursId = null;
            this.employee.CardNo = this.textEditCardNo.Text;
            this.employee.Cellphone = this.textEditCellphone.Text;
            this.employee.Company = this.comboBoxEditCompanyId.EditValue as Model.Company;
            this.employee.CompanyId = this.employee.Company == null ? null : this.employee.Company.CompanyId;
            this.employee.ContactAddress = this.textEditContactAddress.Text;
            this.employee.ContactPhone = this.textEditContactPhone.Text;
            this.employee.Department = this.comboBoxEditDepartmentId.EditValue as Model.Department;
            if (this.employee.Department != null)
                this.employee.DepartmentId = this.employee.Department.DepartmentId;
            else
                this.employee.DepartmentId = null;
            //this.employee.Duty = this.comboBoxEditDutyId.EditValue as Model.Duty;
            if (this.employee.Duty != null)
                this.employee.DutyId = this.employee.Duty.DutyId;
            else
                this.employee.DutyId = null;
            //this.employee.EmployeeBirthday = this.dateEditEmployeeBirthday.DateTime;
            this.employee.EmployeeBloodType = this.comboBoxEditEmployeeBloodType.SelectedIndex;
            this.employee.EmployeeExperience = this.textEditEmployeeExperience.Text;
            this.employee.EmployeeGender = this.comboBoxEditEmployeeGender.SelectedIndex;

            this.employee.EmployeeIdentityNO = this.textEditEmployeeIdentityNO.Text;
            //this.employee.EmployeeJoinDate = this.dateEditEmployeeJoinDate.DateTime;
            //this.employee.EmployeeLeaveDate = this.dateEditEmployeeLeaveDate.DateTime;
            this.employee.EmployeeMarried = this.comboBoxEditEmployeeMarried.SelectedIndex;
            this.employee.EmployeeName = this.textEditEmployeeName.Text;
            this.employee.EmployeeNativePlace = this.textEditEmployeeNativePlace.Text;
            this.employee.EmployeePassword = "1";
            //this.employee.EmployeePhoto = this.pictureEdit1.EditValue 
            this.employee.IDNo = this.textEditIDNo.Text;
            //this.employee.IsCadre = this.checkEditIsCadre.Checked;
            this.employee.MilitaryState = this.comboBoxEditMilitaryState.SelectedIndex;
            this.employee.UrgentContact = this.textEditUrgentContact.Text;
            this.employee.UrgentPhone = this.textEditUrgentPhone.Text;

            if (this.buttonEditPictrue.EditValue != null)
            {
                if (System.IO.File.Exists(this.buttonEditPictrue.EditValue.ToString()))
                {
                    byte[] b = System.IO.File.ReadAllBytes(this.buttonEditPictrue.Text);
                    if (b != null)
                    {
                        this.employee.EmployeePhoto = b;
                    }
                }
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEmployeeBirthday.DateTime, new DateTime()))
            {
                this.employee.EmployeeBirthday = null;
            }
            else
            {
                this.employee.EmployeeBirthday = this.dateEditEmployeeBirthday.DateTime.Date; ;

            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEmployeeJoinDate.DateTime, new DateTime()))
            {
                this.employee.EmployeeJoinDate = null;
            }
            else
            {
                this.employee.EmployeeJoinDate = this.dateEditEmployeeJoinDate.DateTime.Date;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEmployeeLeaveDate.DateTime, new DateTime()))
            {
                this.employee.EmployeeLeaveDate = null;
            }
            else
            {
                this.employee.EmployeeLeaveDate = this.dateEditEmployeeLeaveDate.DateTime.Date;
            }

            switch (this.action)
            {
                case "insert":
                    if (this.buttonEditPictrue.EditValue == null)
                    {
                        string defaultImage = Application.StartupPath + @"\NoImage.jpg";
                        if (File.Exists(defaultImage))
                        {
                            this.employee.EmployeePhoto = File.ReadAllBytes(defaultImage);
                        }
                        else
                        {
                            this.employee.EmployeePhoto = new byte[] { };
                        }
                    }

                    #region 使用员工性别判断日薪,月薪
                    //string ConfigFile = Application.ExecutablePath + ".config";
                    //XmlDocument document = new XmlDocument();
                    //document.Load(ConfigFile);
                    //XmlNode xn = document.SelectSingleNode("/configuration/userSettings/Book.UI.Properties.Settings/setting[@name='Pay']");
                    if (this.employee.EmployeeGender != null)
                    {
                        if (this.employee.EmployeeGender == 0)
                        {
                            //    this.employee.DailyPay = decimal.Parse(xn.SelectSingleNode("add[@key='womanDayilyPay']").Attributes["value"].Value);
                            //    this.employee.MonthlyPay = decimal.Parse(xn.SelectSingleNode("add[@key='womanMonthPay']").Attributes["value"].Value);
                            this.employee.DailyPay = decimal.Parse(this.settingManager.Get("womanDayilyPay").SettingCurrentValue);
                            this.employee.MonthlyPay = decimal.Parse(this.settingManager.Get("womanMonthPay").SettingCurrentValue);
                        }
                        else
                        {
                            this.employee.DailyPay = decimal.Parse(this.settingManager.Get("PaymanDayily").SettingCurrentValue);
                            this.employee.MonthlyPay = decimal.Parse(this.settingManager.Get("manMonthPay").SettingCurrentValue);
                            //this.employee.DailyPay = decimal.Parse(xn.SelectSingleNode("add[@key='manDayilyPay']").Attributes["value"].Value);
                            //this.employee.MonthlyPay = decimal.Parse(xn.SelectSingleNode("add[@key='manMonthPay']").Attributes["value"].Value);
                        }
                    }
                    #endregion

                    this.employeeManager.Insert(employee);
                    break;
                case "update":
                    if (this.buttonEditPictrue.EditValue == null && this.employee.EmployeePhoto == null)
                    {
                        string defaultImage = Application.StartupPath + @"\NoImage.jpg";
                        if (File.Exists(defaultImage))
                        {
                            this.employee.EmployeePhoto = File.ReadAllBytes(defaultImage);
                        }
                        else
                        {
                            this.employee.EmployeePhoto = new byte[] { };
                        }
                    }

                    #region 使用员工性别判断日薪,月薪
                    //string ConfigFile = Application.ExecutablePath + ".config";
                    //XmlDocument document = new XmlDocument();
                    //document.Load(ConfigFile);
                    //XmlNode xn = document.SelectSingleNode("/configuration/userSettings/Book.UI.Properties.Settings/setting[@name='Pay']");
                    if (this.employee.EmployeeGender != null)
                    {
                        if (this.employee.EmployeeGender == 0)
                        {
                            //    this.employee.DailyPay = decimal.Parse(xn.SelectSingleNode("add[@key='womanDayilyPay']").Attributes["value"].Value);
                            //    this.employee.MonthlyPay = decimal.Parse(xn.SelectSingleNode("add[@key='womanMonthPay']").Attributes["value"].Value);
                            this.employee.DailyPay = decimal.Parse(this.settingManager.Get("womanDayilyPay").SettingCurrentValue);
                            this.employee.MonthlyPay = decimal.Parse(this.settingManager.Get("womanMonthPay").SettingCurrentValue);
                        }
                        else
                        {
                            this.employee.DailyPay = decimal.Parse(this.settingManager.Get("PaymanDayily").SettingCurrentValue);
                            this.employee.MonthlyPay = decimal.Parse(this.settingManager.Get("manMonthPay").SettingCurrentValue);
                            //this.employee.DailyPay = decimal.Parse(xn.SelectSingleNode("add[@key='manDayilyPay']").Attributes["value"].Value);
                            //this.employee.MonthlyPay = decimal.Parse(xn.SelectSingleNode("add[@key='manMonthPay']").Attributes["value"].Value);
                        }
                    }
                    #endregion

                    this.employeeManager.Update(employee);
                    break;
                default:
                    break;
            }
            flag = 1;
            //  TreeLoad();
        }

        protected override void AddNew()
        {

            if (tag != 1)
            {
                this.employee = new Model.Employee();
                this.employee.EmployeeId = Guid.NewGuid().ToString();
                this.employee.EmployeeJoinDate = DateTime.Now;
                //    return;
            }
            tag = 0;
            //this.employee.IDNo = BL.InvoiceManager.GetEmployeeNewId();       
        }

        protected override void MovePrev()
        {
            Model.Employee employee = this.employeeManager.mGetPrev(this.employee);
            if (employee == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.employee = employee;
        }

        protected override void MoveNext()
        {
            Model.Employee employee = this.employeeManager.mGetNext(this.employee);
            if (employee == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.employee = employee;
        }

        protected override void MoveFirst()
        {
            this.employee = this.employeeManager.mGetFirst();
        }

        protected override void MoveLast()
        {
            if (this.employee == null)
                this.employee = this.employeeManager.mGetLast();
        }

        protected override bool HasRows()
        {
            return this.employeeManager.mHasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.employeeManager.mHasRowsAfter(this.employee);
        }

        protected override bool HasRowsPrev()
        {
            return this.employeeManager.mHasRowsBefore(this.employee);
        }

        public override void Refresh()
        {
            if (this.employee == null)
            {
                this.employee = new Book.Model.Employee();
                this.employee.EmployeeId = Guid.NewGuid().ToString();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.employee = this.employeeManager.Get(employee.EmployeeId);
            }
            if (this.employee == null)
            {
                this.employee = new Book.Model.Employee();
                this.employee.EmployeeId = Guid.NewGuid().ToString();
                this.action = "insert";
            }
            if (this.employee.EmployeePhoto != null && this.employee.EmployeePhoto.Length > 0)
            {
                this.pictureEdit1.Image = Image.FromStream(new System.IO.MemoryStream(this.employee.EmployeePhoto));

            }
            else
            {
                this.pictureEdit1.Image = null;
                this.buttonEditPictrue.EditValue = null;
            }

            this.bindingSourceEmployee.DataSource = this.employeeManager.SelectOnActive();
            this.bindingSourceEmployee.Position = this.bindingSourceEmployee.IndexOf(this.employee);
            //设置控件值
            this.SetControlValue();
            switch (this.action)
            {
                case "insert":
                    #region Controls State
                    this.buttonEditPictrue.Enabled = true;
                    this.buttonEditPictrue.Properties.ReadOnly = false;
                    this.textEditIDNo.Properties.ReadOnly = false;
                    this.textEditEmployeeName.Properties.ReadOnly = false;
                    this.dateEditEmployeeJoinDate.Properties.ReadOnly = false;
                    this.dateEditEmployeeLeaveDate.Properties.ReadOnly = false;
                    this.textEditCardNo.Properties.ReadOnly = false;
                    this.comboBoxEditDepartmentId.Properties.ReadOnly = false;
                    this.textEditEmployeeIdentityNO.Properties.ReadOnly = false;
                    //this.comboBoxEditDutyId.Properties.ReadOnly = false;
                    this.dateEditEmployeeBirthday.Properties.ReadOnly = false;
                    this.comboBoxEditBusinessHoursId.Properties.ReadOnly = false;
                    this.textEditContactPhone.Properties.ReadOnly = false;
                    this.comboBoxEditCompanyId.Properties.ReadOnly = false;
                    this.comboBoxEditBankId.Properties.ReadOnly = false;
                    this.textEditBankAccount.Properties.ReadOnly = false;
                    this.textEditCellphone.Properties.ReadOnly = false;
                    this.textEditContactAddress.Properties.ReadOnly = false;
                    this.comboBoxEditEmployeeBloodType.Properties.ReadOnly = false;
                    this.comboBoxEditEmployeeGender.Properties.ReadOnly = false;
                    this.comboBoxEditAcademicBackGroundId.Properties.ReadOnly = false;
                    this.textEditUrgentContact.Properties.ReadOnly = false;
                    this.textEditUrgentPhone.Properties.ReadOnly = false;
                    this.comboBoxEditEmployeeMarried.Properties.ReadOnly = false;
                    this.comboBoxEditMilitaryState.Properties.ReadOnly = false;
                    this.textEditEmployeeNativePlace.Properties.ReadOnly = false;
                    //this.checkEditIsCadre.Properties.ReadOnly = false;
                    this.textEditEmployeeExperience.Properties.ReadOnly = false;
                    this.pictureEdit1.Properties.ReadOnly = false;
                    #endregion
                    break;
                case "update":
                    #region Controls State
                    this.buttonEditPictrue.Enabled = true;
                    this.buttonEditPictrue.Properties.ReadOnly = false;
                    this.textEditIDNo.Properties.ReadOnly = false;
                    this.textEditEmployeeName.Properties.ReadOnly = false;
                    this.dateEditEmployeeJoinDate.Properties.ReadOnly = false;
                    this.dateEditEmployeeLeaveDate.Properties.ReadOnly = false;
                    this.textEditCardNo.Properties.ReadOnly = false;
                    this.comboBoxEditDepartmentId.Properties.ReadOnly = false;
                    this.textEditEmployeeIdentityNO.Properties.ReadOnly = false;
                    //this.comboBoxEditDutyId.Properties.ReadOnly = false;
                    this.dateEditEmployeeBirthday.Properties.ReadOnly = false;
                    this.comboBoxEditBusinessHoursId.Properties.ReadOnly = false;
                    this.textEditContactPhone.Properties.ReadOnly = false;
                    this.comboBoxEditCompanyId.Properties.ReadOnly = false;
                    this.comboBoxEditBankId.Properties.ReadOnly = false;
                    this.textEditBankAccount.Properties.ReadOnly = false;
                    this.textEditCellphone.Properties.ReadOnly = false;
                    this.textEditContactAddress.Properties.ReadOnly = false;
                    this.comboBoxEditEmployeeBloodType.Properties.ReadOnly = false;
                    this.comboBoxEditEmployeeGender.Properties.ReadOnly = false;
                    this.comboBoxEditAcademicBackGroundId.Properties.ReadOnly = false;
                    this.textEditUrgentContact.Properties.ReadOnly = false;
                    this.textEditUrgentPhone.Properties.ReadOnly = false;
                    this.comboBoxEditEmployeeMarried.Properties.ReadOnly = false;
                    this.comboBoxEditMilitaryState.Properties.ReadOnly = false;
                    this.textEditEmployeeNativePlace.Properties.ReadOnly = false;
                    //this.checkEditIsCadre.Properties.ReadOnly = false;
                    this.textEditEmployeeExperience.Properties.ReadOnly = false;
                    this.pictureEdit1.Properties.ReadOnly = false;
                    #endregion
                    break;
                case "view":
                    #region Controls State
                    this.buttonEditPictrue.Enabled = false;
                    this.buttonEditPictrue.Properties.ReadOnly = true;
                    this.textEditIDNo.Properties.ReadOnly = true;
                    this.textEditEmployeeName.Properties.ReadOnly = true;
                    this.dateEditEmployeeJoinDate.Properties.ReadOnly = true;
                    this.dateEditEmployeeLeaveDate.Properties.ReadOnly = true;
                    this.textEditCardNo.Properties.ReadOnly = true;
                    this.comboBoxEditDepartmentId.Properties.ReadOnly = true;
                    this.textEditEmployeeIdentityNO.Properties.ReadOnly = true;
                    //this.comboBoxEditDutyId.Properties.ReadOnly = true;
                    this.dateEditEmployeeBirthday.Properties.ReadOnly = true;
                    this.comboBoxEditBusinessHoursId.Properties.ReadOnly = true;
                    this.textEditContactPhone.Properties.ReadOnly = true;
                    this.comboBoxEditCompanyId.Properties.ReadOnly = true;
                    this.comboBoxEditBankId.Properties.ReadOnly = true;
                    this.textEditBankAccount.Properties.ReadOnly = true;
                    this.textEditCellphone.Properties.ReadOnly = true;
                    this.textEditContactAddress.Properties.ReadOnly = true;
                    this.comboBoxEditEmployeeBloodType.Properties.ReadOnly = true;
                    this.comboBoxEditEmployeeGender.Properties.ReadOnly = true;
                    this.comboBoxEditAcademicBackGroundId.Properties.ReadOnly = true;
                    this.textEditUrgentContact.Properties.ReadOnly = true;
                    this.textEditUrgentPhone.Properties.ReadOnly = true;
                    this.comboBoxEditEmployeeMarried.Properties.ReadOnly = true;
                    this.comboBoxEditMilitaryState.Properties.ReadOnly = true;
                    this.textEditEmployeeNativePlace.Properties.ReadOnly = true;
                    //this.checkEditIsCadre.Properties.ReadOnly = true;
                    this.textEditEmployeeExperience.Properties.ReadOnly = true;
                    this.pictureEdit1.Properties.ReadOnly = true;
                    #endregion
                    break;
                default:
                    break;
            }
            base.Refresh();
            if (this.action == "view")
            {
                this.simpleButtonOn.Enabled = true;
                this.simpleButtonLeave.Enabled = true;
            }
            else
            {
                this.simpleButtonOn.Enabled = false;
                this.simpleButtonLeave.Enabled = false;
            }
        }

        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Book.UI.Invoices.ChooseDutyForm f = new Book.UI.Invoices.ChooseDutyForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as DevExpress.XtraEditors.ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Book.UI.Invoices.ChooseDepartmentForm f = new Book.UI.Invoices.ChooseDepartmentForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as DevExpress.XtraEditors.ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditCardNo, this.textEditContactPhone, this.textEditEmployeeIdentityNO });
        }

        private void comboBoxEditAcademicBackGroundId_Enter(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.ComboBoxEdit combox = sender as DevExpress.XtraEditors.ComboBoxEdit;
            if (combox == null || combox.Tag == null)
                return;
            string tag = combox.Tag.ToString();

            switch (tag)
            {
                case "bank":
                    this.comboBoxEditBankId.Properties.Items.Clear();

                    foreach (Model.Bank bank in bankManager.Select())
                    {
                        this.comboBoxEditBankId.Properties.Items.Add(bank);
                    }
                    break;
                //case "duty":
                //    this.comboBoxEditDutyId.Properties.Items.Clear();

                //    foreach (Model.Duty duty in dutyManager.Select())
                //    {
                //        this.comboBoxEditDutyId.Properties.Items.Add(duty);
                //    }
                //    break;
                case "company":
                    this.comboBoxEditCompanyId.Properties.Items.Clear();

                    foreach (Model.Company company in companyManager.Select())
                    {
                        this.comboBoxEditCompanyId.Properties.Items.Add(company);
                    }
                    break;
                case "department":
                    this.comboBoxEditDepartmentId.Properties.Items.Clear();

                    foreach (Model.Department department in departmentManager.Select())
                    {
                        this.comboBoxEditDepartmentId.Properties.Items.Add(department);
                    }
                    break;
                case "businesshour":
                    this.comboBoxEditBusinessHoursId.Properties.Items.Clear();

                    foreach (Model.BusinessHours businessHours in businessHoursManager.Select())
                    {
                        this.comboBoxEditBusinessHoursId.Properties.Items.Add(businessHours);
                    }
                    break;
                case "academicbackground":
                    this.comboBoxEditAcademicBackGroundId.Properties.Items.Clear();

                    foreach (Model.AcademicBackGround academicBackGround in academicBackGroundManager.Select())
                    {
                        this.comboBoxEditAcademicBackGroundId.Properties.Items.Add(academicBackGround);
                    }
                    break;
                default:
                    break;
            }
        }

        private void simpleButtonAddMember_Click(object sender, EventArgs e)
        {
            FamilyMemberEditForm f = new FamilyMemberEditForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.employee.FamilyMembers.Add(f.FamilyMember);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButtonDelMember_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要刪除該眷屬?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.employee.FamilyMembers.Remove(this.bindingSourceFamilyMember.Current as Model.FamilyMembers);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButtonModifiedMember_Click(object sender, EventArgs e)
        {
            FamilyMemberEditForm f = new FamilyMemberEditForm(this.bindingSourceFamilyMember.Current as Model.FamilyMembers);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.gridControl1.RefreshDataSource();
            }
        }

        public static Model.Employee _employee;

        private void simpleButtonOn_Click(object sender, EventArgs e)
        {
            OnListForm f = new OnListForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.employee = _employee;
                this.action = "view";
                this.Refresh();
            }
        }

        private void simpleButtonLeave_Click(object sender, EventArgs e)
        {
            LeaveListForm f = new LeaveListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.employee = _employee;
                this.action = "view";
                this.Refresh();
            }
        }
        //protected void TreeLoad()
        //{
        //    this.treeList1.ClearNodes();
        //    foreach (Model.Department dt in departmentManager.Select())
        //    {

        //        DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { dt.DepartmentName }, null, dt.DepartmentId);


        //        foreach (Model.Employee Employee in employeeManager.Select(dt))
        //        {

        //            treeList1.AppendNode(new object[] { Employee.EmployeeName }, treeNode,Employee.EmployeeId);

        //        }


        //    }
        //}

        //private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        //{
        //    if (flag == 0)
        //    {

        //        if (e.Node != null && e.Node.ParentNode != null)
        //        {                
        //            this.action = "view";
        //            this.employee = employeeManager.Get(e.Node.Tag.ToString());                  
        //            this.Refresh();
        //        }
        //    }
        //}

        private void buttonEditPictrue_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = this.openFileDialog1.FileName;
                if (System.IO.File.Exists(fileName))
                {
                    this.buttonEditPictrue.Text = fileName;
                    this.pictureEdit1.Image = Image.FromFile(fileName);
                }
                else
                {
                    MessageBox.Show(this, "fileNotFound", "文件不存在！", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {

            this.action = "view";
            this.employee = this.bindingSourceEmployee.Current as Model.Employee;

            this.SetControlValue();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EmployeeSearchForm f = new EmployeeSearchForm();
            f.Show();

        }

        private void SetControlValue()
        {
            this.textEditIDNo.Text = this.employee.IDNo;
            this.textEditEmployeeName.Text = this.employee.EmployeeName;
            //this.dateEditEmployeeJoinDate.EditValue = this.employee.EmployeeJoinDate;
            //this.dateEditEmployeeLeaveDate.EditValue = this.employee.EmployeeLeaveDate;
            this.textEditCardNo.Text = this.employee.CardNo;
            this.comboBoxEditDepartmentId.EditValue = this.employee.Department;
            this.textEditEmployeeIdentityNO.Text = this.employee.EmployeeIdentityNO;
            //this.comboBoxEditDutyId.EditValue = this.employee.Duty;
            //this.dateEditEmployeeBirthday.EditValue = this.employee.EmployeeBirthday;
            this.comboBoxEditBusinessHoursId.EditValue = this.employee.BusinessHours;
            this.textEditContactPhone.Text = this.employee.ContactPhone;
            this.comboBoxEditCompanyId.EditValue = this.employee.Company;
            this.comboBoxEditBankId.EditValue = this.employee.Bank;
            this.textEditBankAccount.Text = this.employee.BankAccount;
            this.textEditCellphone.Text = this.employee.Cellphone;
            this.textEditContactAddress.Text = this.employee.ContactAddress;
            this.comboBoxEditEmployeeBloodType.SelectedIndex = this.employee.EmployeeBloodType == null ? -1 : this.employee.EmployeeBloodType.Value;
            this.comboBoxEditEmployeeGender.SelectedIndex = this.employee.EmployeeGender == null ? -1 : this.employee.EmployeeGender.Value;
            this.comboBoxEditAcademicBackGroundId.EditValue = this.employee.AcademicBackGround;
            this.textEditUrgentContact.Text = this.employee.UrgentContact;
            this.textEditUrgentPhone.Text = this.employee.UrgentPhone;
            this.comboBoxEditEmployeeMarried.SelectedIndex = this.employee.EmployeeMarried == null ? -1 : this.employee.EmployeeMarried.Value;
            this.comboBoxEditMilitaryState.SelectedIndex = this.employee.MilitaryState == null ? -1 : this.employee.MilitaryState.Value;
            this.textEditEmployeeNativePlace.Text = this.employee.EmployeeNativePlace;
            //this.checkEditIsCadre.Checked = this.employee.IsCadre == null ? false : this.employee.IsCadre.Value;
            this.textEditEmployeeExperience.Text = this.employee.EmployeeExperience;

            this.bindingSourceFamilyMember.DataSource = this.employee.FamilyMembers;

            if (global::Helper.DateTimeParse.DateTimeEquls(this.employee.EmployeeBirthday, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditEmployeeBirthday.EditValue = null;
            }
            else
            {
                this.dateEditEmployeeBirthday.EditValue = this.employee.EmployeeBirthday;

            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.employee.EmployeeLeaveDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditEmployeeLeaveDate.EditValue = null;
            }
            else
            {
                this.dateEditEmployeeLeaveDate.EditValue = this.employee.EmployeeLeaveDate;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.employee.EmployeeJoinDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditEmployeeJoinDate.EditValue = null;
            }
            else
            {
                this.dateEditEmployeeJoinDate.EditValue = this.employee.EmployeeJoinDate;
            }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        //private void textEditIDNo_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!(char.IsDigit(e.KeyChar) || char.IsUpper(e.KeyChar) || char.IsLower(e.KeyChar) || e.KeyChar == '\b' ||char.))
        //    {
        //        MessageBox.Show(Properties.Resources.InputError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        e.Handled = true;
        //    }
        //}

        //private void TreeLoadPy()
        //{      
        //    treeList1.ClearNodes();
        //    foreach (DataRow dr in (this.employeeManager.SelectPinyin() as DataTable).Rows)
        //    {
        //        DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { dr[Model.Employee.PROPERTY_PINYIN] }, null, dr[Model.Employee.PROPERTY_PINYIN]);

        //        foreach (Model.Employee Employee in this.employeeManager.SelectbyPinYin(dr[Model.Employee.PROPERTY_PINYIN].ToString()))
        //        {
        //            treeList1.AppendNode(new object[] { Employee.EmployeeName }, treeNode, Employee.EmployeeId);
        //        }
        //    }
        //}
        //private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    switch (this.radioGroup1.SelectedIndex)
        //    { 
        //        case 0:
        //            TreeLoad();
        //            break;
        //        case 1:
        //            TreeLoadPy();
        //            break;
        //    }

        //}
    }
}