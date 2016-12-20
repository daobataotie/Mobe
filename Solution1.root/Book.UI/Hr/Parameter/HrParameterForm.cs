using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;

namespace Book.UI.Hr.Parameter
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  裴盾             完成时间:2009-10-10
// 修改原因：
// 修 改 人:  劉永亮                  修改时间:2010-07-15
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class HrParameterForm : BaseEditForm
    {

        #region 定義變量以及對象
        private int pageindex = 0;
        /// <summary>
        /// 当前待处理对象
        /// </summary>
        Model.AcademicBackGround academ = null;
        /// <summary>
        ///学历管理
        /// </summary>
        BL.AcademicBackGroundManager academmanage = new Book.BL.AcademicBackGroundManager();
        /// <summary>
        /// 职务管理
        /// </summary>
        BL.DutyManager dutymanage = new Book.BL.DutyManager();
        /// <summary>
        /// 待处理职务
        /// </summary>
        Model.Duty currentduty = null;
        /// <summary>
        /// 银行管理
        /// </summary>
        BL.BankManager bankmanage = new Book.BL.BankManager();
        /// <summary>
        /// 待处理银行
        /// </summary>
        Model.Bank currentBank = null;
        /// <summary>
        /// 公司管理
        /// </summary>
        BL.CompanyManager companymanage = new Book.BL.CompanyManager();
        /// <summary>
        /// 待处理公司
        /// </summary>
        Model.Company currentcompany = null;
        /// <summary>
        /// 班别管理
        /// </summary>
        BL.BusinessHoursManager businesshoursmanage = new Book.BL.BusinessHoursManager();
        /// <summary>
        /// 待处理班别
        /// </summary>
        Model.BusinessHours currentbusinessHours = null;
        /// <summary>
        /// 休假类别管理
        /// </summary>
        BL.LeaveTypeManager leaveTypemanage = new Book.BL.LeaveTypeManager();
        /// <summary>
        /// 待处理休假类别
        /// </summary>
        Model.LeaveType currentleaveType = null;
        /// <summary>
        /// 年假管理
        /// </summary>
        BL.AnnualHolidayManager annualholidaymanage = new Book.BL.AnnualHolidayManager();
        /// <summary>
        /// 待处理年假
        /// </summary>
        Model.AnnualHoliday currentAnnualholiday = null;
        #endregion


        public HrParameterForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AcademicBackGround.PROPERTY_ACADEMICBACKGROUNDNAME, new AA(Properties.Resources.AcademicNameNotNull, this.textEdit_AcademicBackGroundName));
            this.requireValueExceptions.Add(Model.Bank.PROPERTY_BANKNAME, new AA(Properties.Resources.RequireDataForNames, this.textEdit_BankName));
            this.requireValueExceptions.Add(Model.Duty.PROPERTY_DUTYNAME, new AA(Properties.Resources.RequireDataForNames, this.textEdit_DutyName));
            this.requireValueExceptions.Add(Model.Company.PROPERTY_COMPANYNAME, new AA(Properties.Resources.RequireDataForNames, this.textEdit_CompanyName));
            this.requireValueExceptions.Add(Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME, new AA(Properties.Resources.RequireDataForNames, this.textEdit_BusinessHoursName));

            this.invalidValueExceptions.Add(Model.AcademicBackGround.PROPERTY_ACADEMICBACKGROUNDNAME, new AA(Properties.Resources.AcademicNameIsExist, this.textEdit_AcademicName));
            this.invalidValueExceptions.Add(Model.Duty.PROPERTY_DUTYNAME, new AA(Properties.Resources.IsExistsDutyName, this.textEdit_DutyName));
            this.invalidValueExceptions.Add(Model.Bank.PROPERTY_BANKNAME, new AA(Properties.Resources.IsExistsBankName, this.textEdit_BankName));
            this.invalidValueExceptions.Add(Model.Company.PROPERTY_COMPANYNAME, new AA(Properties.Resources.IsExistsCompanyName, this.textEdit_CompanyName));
            this.invalidValueExceptions.Add(Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME, new AA(Properties.Resources.IsExistsBusinessName, this.textEdit_BusinessHoursName));


            //this.requireValueExceptions.Add(Model.LeaveType.PROPERTY_LEAVETYPENAME, new AA(Properties.Resources.RequireDataForNames,this.textEdit_LeaveTypeName));
            //this.requireValueExceptions.Add(Model.AnnualHoliday.PROPERTY_HOLIDAYNAME, new AA(Properties.Resources.RequireDataForNames,this.textEdit_HolidayName));

            this.action = "insert";
        }



        /// <summary>
        /// 选项卡改变触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            pageindex = xtraTabControl1.SelectedTabPageIndex;
            loadParmeter();
            AddNew();
            this.action = "insert";
            this.Refresh();
        }



        /// <summary>
        /// 加载信息
        /// </summary>
        private void loadParmeter()
        {

            switch (pageindex)
            {
                case 0:
                    this.bindingSource_Academic.DataSource = academmanage.Select();
                    break;
                case 1:
                    this.bindingSource_duty.DataSource = dutymanage.Select();
                    break;
                case 2:
                    this.bindingSource_bank.DataSource = bankmanage.Select();
                    break;
                case 3:
                    this.bindingSource_company.DataSource = companymanage.Select();
                    break;
                case 4:
                    this.bindingSource_BusinessHours.DataSource = businesshoursmanage.Select();
                    break;
                case 5:
                    this.bindingSource_leaveType.DataSource = leaveTypemanage.Select();
                    break;

                case 6:
                    this.bindingSource_AnnualHoliday.DataSource = annualholidaymanage.Select();
                    break;
                case 7:
                    loadriqi(DateTime.Now.Year);
                    break;
            }
        }



        private void HrParameterForm_Load(object sender, EventArgs e)
        {
            loadParmeter();
            this.Visibles();
        }

        //新增
        protected override void AddNew()
        {
            switch (pageindex)
            {
                case 0:
                    this.academ = new Book.Model.AcademicBackGround();
                    this.academ.AcademicBackGroundId = Guid.NewGuid().ToString();
                    break;
                case 1:
                    this.currentduty = new Book.Model.Duty();
                    this.currentduty.DutyId = Guid.NewGuid().ToString();
                    break;
                case 2:
                    this.currentBank = new Book.Model.Bank();
                    this.currentBank.BankId = Guid.NewGuid().ToString();
                    break;
                case 3:
                    this.currentcompany = new Book.Model.Company();
                    this.currentcompany.CompanyId = Guid.NewGuid().ToString();
                    break;
                case 4:
                    this.currentbusinessHours = new Book.Model.BusinessHours();
                    this.currentbusinessHours.BusinessHoursId = Guid.NewGuid().ToString();
                    break;
                case 5:
                    this.currentleaveType = new Book.Model.LeaveType();
                    this.currentleaveType.LeaveTypeId = Guid.NewGuid().ToString();
                    break;
                case 6:
                    this.currentAnnualholiday = new Book.Model.AnnualHoliday();
                    this.currentAnnualholiday.AnnualHolidayId = Guid.NewGuid().ToString();
                    break;
                case 7:
                    break;
            }
        }


        /// <summary>
        /// 保存
        /// </summary>
        protected override void Save()
        {
            switch (pageindex)
            {
                case 0:
                    if (this.textEdit_AcademicName.EditValue != null)
                        this.academ.AcademicBackGroundName = this.textEdit_AcademicName.EditValue.ToString();
                    if (this.memoEdit_AcademicDescription.EditValue != null)
                        this.academ.Description = this.memoEdit_AcademicDescription.EditValue.ToString();
                    switch (this.action)
                    {
                        case "insert":
                            this.academmanage.Insert(this.academ);
                            break;
                        case "update":
                            this.academmanage.Update(this.academ);
                            break;
                    }
                    loadParmeter();
                    break;
                case 1:
                    if (this.textEdit_DutyName.EditValue != null)
                        this.currentduty.DutyName = this.textEdit_DutyName.EditValue.ToString();
                    if (this.memoEdit_DutyNote.EditValue != null)
                        this.currentduty.DutyNote = this.memoEdit_DutyNote.EditValue.ToString();

                    switch (this.action)
                    {
                        case "insert":
                            this.dutymanage.Insert(this.currentduty);
                            break;
                        case "update":
                            this.dutymanage.Update(this.currentduty);
                            break;
                    }
                    loadParmeter();
                    break;
                case 2:
                    if (this.textEdit_BankName.EditValue != null)
                        this.currentBank.BankName = this.textEdit_BankName.EditValue.ToString();
                    if (this.textEdit_BankPhone.EditValue != null)
                        this.currentBank.BankPhone = this.textEdit_BankPhone.EditValue.ToString();
                    if (this.textEdit_BankAddress.EditValue != null)
                        this.currentBank.BankAddress = this.textEdit_BankAddress.EditValue.ToString();
                    if (this.memoEdit_BankDescription.EditValue != null)
                        this.currentBank.Description = this.memoEdit_BankDescription.EditValue.ToString();
                    switch (this.action)
                    {
                        case "insert":
                            this.bankmanage.Insert(this.currentBank);
                            break;
                        case "update":
                            this.bankmanage.Update(this.currentBank);
                            break;
                    }
                    loadParmeter();
                    this.currentBank = new Book.Model.Bank();
                    break;
                case 3:
                    if (this.textEdit_CompanyName.EditValue != null)
                        this.currentcompany.CompanyName = this.textEdit_CompanyName.EditValue.ToString();
                    if (this.memoEdit_CompanyDescription.EditValue != null)
                        this.currentcompany.Description = this.memoEdit_CompanyDescription.EditValue.ToString();
                    if (this.currentcompany.CompanySign == null)
                    {
                       this.currentcompany.CompanySign = new byte[] { };
                        
                    }
                    switch (this.action)
                    {
                        case "insert":
                            this.companymanage.Insert(this.currentcompany);
                            break;
                        case "update":
                            this.companymanage.Update(this.currentcompany);
                            break;
                    }
                    loadParmeter();
                    this.currentcompany = new Book.Model.Company();
                    break;
                case 4:
                    if (this.textEdit_BusinessHoursName.EditValue != null)
                        this.currentbusinessHours.BusinessHoursName = this.textEdit_BusinessHoursName.EditValue.ToString();
                    if (this.memoEdit_BusinessDescription.EditValue != null)
                        this.currentbusinessHours.Description = this.memoEdit_BusinessDescription.EditValue.ToString();
                    if (this.timeEdit_Formtime.EditValue != null)
                        this.currentbusinessHours.Fromtime = Convert.ToDateTime(this.timeEdit_Formtime.EditValue);
                    else
                        this.currentbusinessHours.Fromtime = DateTime.Now;
                    if (this.timeEdit_Totime.EditValue != null)
                        this.currentbusinessHours.ToTime = Convert.ToDateTime(this.timeEdit_Totime.EditValue);
                    else
                        this.currentbusinessHours.ToTime = DateTime.Now;
                    if (this.textEdit_SpecialPay.EditValue != null)
                        this.currentbusinessHours.SpecialPay = Convert.ToDecimal(this.textEdit_SpecialPay.EditValue);
                    switch (this.action)
                    {
                        case "insert":
                            this.businesshoursmanage.Insert(this.currentbusinessHours);
                            break;
                        case "update":
                            this.businesshoursmanage.Update(this.currentbusinessHours);
                            break;
                    }
                    loadParmeter();
                    break;
                case 5:
                    //if (this.textEdit_LeaveTypeName.EditValue != null)
                    //    this.currentleaveType.LeaveTypeName = this.textEdit_LeaveTypeName.EditValue.ToString();
                    //if (this.textEdit_PayRate.EditValue != null)
                    //    this.currentleaveType.PayRate = Convert.ToDouble(this.textEdit_PayRate.EditValue);
                    //if (this.checkEdit_IsCountToPunish.Checked == true)
                    //    this.currentleaveType.IsCountToPunish = true;
                    //else
                    //    this.currentleaveType.IsCountToPunish = false;
                    switch (this.action)
                    {
                        case "insert":
                            this.leaveTypemanage.Insert(this.currentleaveType);
                            break;
                        case "update":
                            this.leaveTypemanage.Update(this.currentleaveType);
                            break;
                    }
                    loadParmeter();
                    break;
                case 6:
                    //if (this.textEdit_HolidayName.EditValue != null)
                    //    this.currentAnnualholiday.HolidayName = this.textEdit_HolidayName.EditValue.ToString();
                    //if (this.dateEdit_HolidayDate.DateTime != null)
                    //    this.currentAnnualholiday.HolidayDate = this.dateEdit_HolidayDate.DateTime;
                    //else
                    //    this.currentAnnualholiday.HolidayDate = DateTime.Now;
                    switch (this.action)
                    {
                        case "insert":
                            this.annualholidaymanage.Insert(this.currentAnnualholiday);
                            break;
                        case "update":
                            this.annualholidaymanage.Update(this.currentAnnualholiday);
                            break;
                    }
                    loadParmeter();
                    break;
                case 7:
                    break;
            }
        }

        protected override bool HasRows()
        {

            return this.academmanage.HasRows();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Refresh()
        {
            switch (pageindex)
            {
                case 0:
                    if (this.academ == null)
                    {
                        this.academ = new Book.Model.AcademicBackGround();
                        this.action = "insert";
                    }
                    this.textEdit_AcademicName.EditValue = this.academ.AcademicBackGroundName;
                    this.memoEdit_AcademicDescription.EditValue = this.academ.Description;

                    switch (this.action)
                    {
                        case "insert":
                            this.textEdit_AcademicName.Properties.ReadOnly = false;
                            this.memoEdit_AcademicDescription.Properties.ReadOnly = false;
                            break;

                        case "update":
                            this.textEdit_AcademicName.Properties.ReadOnly = false;
                            this.memoEdit_AcademicDescription.Properties.ReadOnly = false;
                            break;

                        case "view":
                            this.textEdit_AcademicName.Properties.ReadOnly = true;
                            this.memoEdit_AcademicDescription.Properties.ReadOnly = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    if (this.currentduty == null)
                    {
                        this.currentduty = new Book.Model.Duty();
                        this.action = "insert";
                    }
                    this.textEdit_DutyName.EditValue = this.currentduty.DutyName;
                    this.memoEdit_DutyNote.EditValue = this.currentduty.DutyNote;
                    switch (this.action)
                    {
                        case "insert":
                            this.textEdit_DutyName.Properties.ReadOnly = false;
                            this.memoEdit_DutyNote.Properties.ReadOnly = false;
                            break;

                        case "update":
                            this.textEdit_DutyName.Properties.ReadOnly = false;
                            this.memoEdit_DutyNote.Properties.ReadOnly = false;
                            break;

                        case "view":
                            this.textEdit_DutyName.Properties.ReadOnly = true;
                            this.memoEdit_DutyNote.Properties.ReadOnly = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    if (this.currentBank == null)
                    {
                        this.currentBank = new Book.Model.Bank();
                        this.action = "insert";
                    }
                    this.textEdit_BankName.EditValue = this.currentBank.BankName;
                    this.textEdit_BankPhone.EditValue = this.currentBank.BankPhone;
                    this.textEdit_BankAddress.EditValue = this.currentBank.BankAddress;
                    this.memoEdit_BankDescription.EditValue = this.currentBank.Description;
                    switch (this.action)
                    {
                        case "insert":
                            this.textEdit_BankName.Properties.ReadOnly = false;
                            this.textEdit_BankPhone.Properties.ReadOnly = false;
                            this.textEdit_BankAddress.Properties.ReadOnly = false;
                            this.memoEdit_BankDescription.Properties.ReadOnly = false;
                            break;

                        case "update":
                            this.textEdit_BankName.Properties.ReadOnly = false;
                            this.textEdit_BankPhone.Properties.ReadOnly = false;
                            this.textEdit_BankAddress.Properties.ReadOnly = false;
                            this.memoEdit_BankDescription.Properties.ReadOnly = false; ;
                            break;

                        case "view":
                            this.textEdit_BankName.Properties.ReadOnly = true;
                            this.textEdit_BankPhone.Properties.ReadOnly = true;
                            this.textEdit_BankAddress.Properties.ReadOnly = true;
                            this.memoEdit_BankDescription.Properties.ReadOnly = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    if (this.currentcompany == null)
                    {
                        this.currentcompany = new Book.Model.Company();
                        this.action = "null";
                    }
                    this.textEdit_CompanyName.EditValue = this.currentcompany.CompanyName;
                    this.memoEdit_CompanyDescription.EditValue = this.currentcompany.Description;
                    switch (this.action)
                    {
                        case "insert":
                            this.textEdit_CompanyName.Properties.ReadOnly = false;
                            this.memoEdit_CompanyDescription.Properties.ReadOnly = false;
                            break;

                        case "update":
                            this.textEdit_CompanyName.Properties.ReadOnly = false;
                            this.memoEdit_CompanyDescription.Properties.ReadOnly = false;
                            break;

                        case "view":
                            this.textEdit_CompanyName.Properties.ReadOnly = true;
                            this.memoEdit_CompanyDescription.Properties.ReadOnly = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case 4:
                    if (this.currentbusinessHours == null)
                    {
                        this.currentbusinessHours = new Book.Model.BusinessHours();
                        this.action = "insert";
                    }
                    this.textEdit_BusinessHoursName.EditValue = this.currentbusinessHours.BusinessHoursName;
                    this.memoEdit_BusinessDescription.EditValue = this.currentbusinessHours.Description;
                    this.timeEdit_Formtime.EditValue = this.currentbusinessHours.Fromtime;
                    this.timeEdit_Totime.EditValue = this.currentbusinessHours.ToTime;
                    this.textEdit_SpecialPay.EditValue = this.currentbusinessHours.SpecialPay;
                    switch (this.action)
                    {
                        case "insert":
                            this.textEdit_BusinessHoursName.Properties.ReadOnly = false;
                            this.memoEdit_BusinessDescription.Properties.ReadOnly = false;
                            break;

                        case "update":
                            this.textEdit_BusinessHoursName.Properties.ReadOnly = false;
                            this.memoEdit_BusinessDescription.Properties.ReadOnly = false;
                            break;

                        case "view":
                            this.textEdit_BusinessHoursName.Properties.ReadOnly = true;
                            this.memoEdit_BusinessDescription.Properties.ReadOnly = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case 5:
                    if (this.currentleaveType == null)
                    {
                        this.currentleaveType = new Book.Model.LeaveType();
                        this.action = "insert";
                    }
                    //this.textEdit_LeaveTypeName.EditValue = this.currentleaveType.LeaveTypeName;
                    //this.textEdit_PayRate.EditValue = this.currentleaveType.PayRate;
                    //if (this.currentleaveType.IsCountToPunish == true)
                    //    this.checkEdit_IsCountToPunish.Checked = true;
                    //else
                    //    this.checkEdit_IsCountToPunish.Checked = false;
                    switch (this.action)
                    {
                        case "insert":
                            //this.textEdit_LeaveTypeName.Properties.ReadOnly = false;
                            //this.textEdit_PayRate.Properties.ReadOnly = false;
                            //this.checkEdit_IsCountToPunish.Properties.ReadOnly = false;
                            break;

                        case "update":
                            // this.textEdit_LeaveTypeName.Properties.ReadOnly = false;
                            // this.textEdit_PayRate.Properties.ReadOnly = false;
                            // this.checkEdit_IsCountToPunish.Properties.ReadOnly = false;
                            break;

                        case "view":
                            //this.textEdit_LeaveTypeName.Properties.ReadOnly = true;
                            //this.textEdit_PayRate.Properties.ReadOnly = true;
                            //this.checkEdit_IsCountToPunish.Properties.ReadOnly = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case 6:
                    if (this.currentAnnualholiday == null)
                    {
                        this.currentAnnualholiday = new Book.Model.AnnualHoliday();
                        this.action = "insert";
                    }

                    break;
                case 7:
                    break;
            }

            base.Refresh();
        }


        /// <summary>
        /// 删除
        /// </summary>
        protected override void Delete()
        {
            switch (pageindex)
            {
                case 0:
                    this.academ = this.bindingSource_Academic.Current as Model.AcademicBackGround;
                    if (this.academ != null)
                        this.academmanage.Delete(this.academ.AcademicBackGroundId);
                    loadParmeter();
                    break;
                case 1:
                    this.currentduty = this.bindingSource_duty.Current as Model.Duty;
                    if (this.currentduty != null)
                        this.dutymanage.Delete(this.currentduty);
                    loadParmeter();
                    break;
                case 2:
                    this.currentBank = this.bindingSource_bank.Current as Model.Bank;
                    if (this.currentBank != null)
                        this.bankmanage.Delete(this.currentBank.BankId);
                    loadParmeter();
                    break;
                case 3:
                    this.currentcompany = this.bindingSource_company.Current as Model.Company;
                    if (this.currentcompany != null)
                        this.companymanage.Delete(this.currentcompany.CompanyId);
                    loadParmeter();
                    break;
                case 4:
                    this.currentbusinessHours = this.bindingSource_BusinessHours.Current as Model.BusinessHours;
                    if (this.currentbusinessHours != null)
                        this.businesshoursmanage.Delete(this.currentbusinessHours.BusinessHoursId);
                    loadParmeter();
                    break;
                case 5:
                    this.currentleaveType = this.bindingSource_leaveType.Current as Model.LeaveType;
                    if (this.currentleaveType != null)
                        this.leaveTypemanage.Delete(this.currentleaveType.LeaveTypeId);
                    loadParmeter();
                    break;
                case 6:
                    this.currentAnnualholiday = this.bindingSource_AnnualHoliday.Current as Model.AnnualHoliday;
                    if (this.currentAnnualholiday != null)
                        this.annualholidaymanage.Delete(this.currentAnnualholiday.AnnualHolidayId);
                    loadParmeter();
                    break;
                case 7:
                    break;
            }
        }


        /// <summary>
        /// 班别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView5_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.BusinessHours> details = this.bindingSource_BusinessHours.DataSource as IList<Model.BusinessHours>;
            //if (details == null || details.Count < 1) { return; }

            //switch (e.Column.Name)
            //{
            //    case "Fromtime":

            //        e.DisplayText = details[e.ListSourceRowIndex].Fromtime == null ? string.Empty : details[e.ListSourceRowIndex].Fromtime.Value.ToShortTimeString();
            //        break;
            //    case "ToTime":
            //        e.DisplayText = details[e.ListSourceRowIndex].ToTime == null ? string.Empty : details[e.ListSourceRowIndex].ToTime.Value.ToShortTimeString();
            //        break;
            //}


        }



        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            // loadriqi(dateEdit2.DateTime.Year);
        }



        /// <summary>
        /// 加载日期
        /// </summary>
        /// <param name="year"></param>
        private void loadriqi(int year)
        {
            List<Model.AnnualHoliday> riqilist = new List<Model.AnnualHoliday>();

            Model.AnnualHoliday a = null;
            #region 1月
            //1月
            for (int n1 = 1; n1 < 32; n1++)
            {
                DateTime d1 = new DateTime(year, 1, n1);

                if (d1.DayOfWeek == DayOfWeek.Sunday)
                {
                    a = new Book.Model.AnnualHoliday();
                    a.AnnualHolidayId = Guid.NewGuid().ToString();
                    a.HolidayDate = d1;
                    riqilist.Add(a);
                    a = null;
                }


            }
            #endregion
            #region 2



            if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0))
            {

                //2月
                for (int n2 = 1; n2 < 29; n2++)
                {
                    DateTime d2 = new DateTime(year, 2, n2);

                    if (d2.DayOfWeek == DayOfWeek.Sunday)
                    {

                        a = new Book.Model.AnnualHoliday();
                        a.AnnualHolidayId = Guid.NewGuid().ToString();
                        a.HolidayDate = d2;
                        riqilist.Add(a);
                        a = null;
                    }


                }


            }
            else
            {

                //2月
                for (int n21 = 1; n21 < 29; n21++)
                {
                    DateTime d21 = new DateTime(year, 2, n21);

                    if (d21.DayOfWeek == DayOfWeek.Sunday)
                    {
                        a = new Book.Model.AnnualHoliday();
                        a.AnnualHolidayId = Guid.NewGuid().ToString();
                        a.HolidayDate = d21;
                        riqilist.Add(a);
                        a = null;
                    }


                }

            }
            #endregion
            #region 3月
            //1月
            for (int n3 = 1; n3 < 32; n3++)
            {
                DateTime d3 = new DateTime(year, 3, n3);

                if (d3.DayOfWeek == DayOfWeek.Sunday)
                {
                    a = new Book.Model.AnnualHoliday();
                    a.AnnualHolidayId = Guid.NewGuid().ToString();
                    a.HolidayDate = d3;
                    riqilist.Add(a);
                    a = null;
                }


            }
            #endregion

            #region 5月
            //5月
            for (int n5 = 1; n5 < 32; n5++)
            {
                DateTime d5 = new DateTime(year, 5, n5);

                if (d5.DayOfWeek == DayOfWeek.Sunday)
                {
                    a = new Book.Model.AnnualHoliday();
                    a.AnnualHolidayId = Guid.NewGuid().ToString();
                    a.HolidayDate = d5;
                    riqilist.Add(a);
                    a = null;
                }


            }
            #endregion

            #region 6月

            for (int n6 = 1; n6 < 31; n6++)
            {
                DateTime d6 = new DateTime(year, 6, n6);

                if (d6.DayOfWeek == DayOfWeek.Sunday)
                {
                    a = new Book.Model.AnnualHoliday();
                    a.AnnualHolidayId = Guid.NewGuid().ToString();
                    a.HolidayDate = d6;
                    riqilist.Add(a);
                    a = null;
                }


            }
            #endregion

            #region 7月

            for (int n7 = 1; n7 < 32; n7++)
            {
                DateTime d7 = new DateTime(year, 7, n7);

                if (d7.DayOfWeek == DayOfWeek.Sunday)
                {
                    a = new Book.Model.AnnualHoliday();
                    a.AnnualHolidayId = Guid.NewGuid().ToString();
                    a.HolidayDate = d7;
                    riqilist.Add(a);
                    a = null;
                }


            }
            #endregion

            #region 8月

            for (int n8 = 1; n8 < 32; n8++)
            {
                DateTime d8 = new DateTime(year, 8, n8);

                if (d8.DayOfWeek == DayOfWeek.Sunday)
                {
                    a = new Book.Model.AnnualHoliday();
                    a.AnnualHolidayId = Guid.NewGuid().ToString();
                    a.HolidayDate = d8;
                    riqilist.Add(a);
                    a = null;
                }


            }
            #endregion

            #region 9月

            for (int n9 = 1; n9 < 31; n9++)
            {
                DateTime d9 = new DateTime(year, 9, n9);

                if (d9.DayOfWeek == DayOfWeek.Sunday)
                {
                    a = new Book.Model.AnnualHoliday();
                    a.AnnualHolidayId = Guid.NewGuid().ToString();
                    a.HolidayDate = d9;
                    riqilist.Add(a);
                    a = null;
                }


            }
            #endregion

            #region 10月

            for (int n10 = 1; n10 < 32; n10++)
            {
                DateTime d10 = new DateTime(year, 10, n10);

                if (d10.DayOfWeek == DayOfWeek.Sunday)
                {
                    a = new Book.Model.AnnualHoliday();
                    a.AnnualHolidayId = Guid.NewGuid().ToString();
                    a.HolidayDate = d10;
                    riqilist.Add(a);
                    a = null;
                }


            }
            #endregion


            #region 11月

            for (int n11 = 1; n11 < 31; n11++)
            {
                DateTime d11 = new DateTime(year, 11, n11);

                if (d11.DayOfWeek == DayOfWeek.Sunday)
                {
                    a = new Book.Model.AnnualHoliday();
                    a.AnnualHolidayId = Guid.NewGuid().ToString();
                    a.HolidayDate = d11;
                    riqilist.Add(a);
                    a = null;
                }


            }
            #endregion


            #region 12月

            for (int n12 = 1; n12 < 32; n12++)
            {
                DateTime d12 = new DateTime(year, 12, n12);

                if (d12.DayOfWeek == DayOfWeek.Sunday)
                {
                    a = new Book.Model.AnnualHoliday();
                    a.AnnualHolidayId = Guid.NewGuid().ToString();
                    a.HolidayDate = d12;
                    riqilist.Add(a);
                    a = null;
                }


            }
            #endregion
            this.bindingSource_zhouri.DataSource = riqilist;

        }


        /// <summary>
        /// gridview1单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_Click(object sender, EventArgs e)
        {

            Model.AcademicBackGround academic = this.bindingSource_Academic.Current as Model.AcademicBackGround;
            if (academic != null)
            {
                this.academ = academic;
                this.action = "view";
                this.Refresh();
            }
        }



        /// <summary>
        /// gridview2单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_Click(object sender, EventArgs e)
        {
            Model.Duty dy = this.bindingSource_duty.Current as Model.Duty;
            if (dy != null)
            {
                this.currentduty = dy;
                this.action = "view";
                this.Refresh();
            }
        }



        /// <summary>
        /// gridview3单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView3_Click(object sender, EventArgs e)
        {
            Model.Bank bank = this.bindingSource_bank.Current as Model.Bank;
            if (bank != null)
            {
                this.currentBank = bank;
                this.action = "view";
                this.Refresh();
            }

        }



        /// <summary>
        /// gridview4单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView4_Click(object sender, EventArgs e)
        {

            Model.Company company = this.bindingSource_company.Current as Model.Company;
            if (company != null)
            {
                this.currentcompany = company;
                this.action = "view";
                this.Refresh();
            }

        }


        /// <summary>
        /// gridview5单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView5_Click(object sender, EventArgs e)
        {
            Model.BusinessHours business = this.bindingSource_BusinessHours.Current as Model.BusinessHours;
            if (business != null)
            {
                this.currentbusinessHours = business;
                this.action = "view";
                this.Refresh();
            }

        }



        /// <summary>
        /// gridview6单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView6_Click(object sender, EventArgs e)
        {
            Model.LeaveType leavetype = this.bindingSource_leaveType.Current as Model.LeaveType;
            if (leavetype != null)
            {
                this.currentleaveType = leavetype;
                this.action = "view";
                this.Refresh();
            }
        }



        /// <summary>
        /// gridview7单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView7_Click(object sender, EventArgs e)
        {
            this.bindingSource_AnnualHoliday.DataSource = annualholidaymanage.Select();
            Model.AnnualHoliday holiday = this.bindingSource_AnnualHoliday.Current as Model.AnnualHoliday;
            if (holiday != null)
            {
                this.currentAnnualholiday = holiday;
                this.action = "view";
                this.Refresh();
            }
        }


        /// <summary>
        /// gridControl单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl9_Click(object sender, EventArgs e)
        {
            Model.AcademicBackGround academic = this.bindingSource_Academic.Current as Model.AcademicBackGround;
            if (academic != null)
            {
                this.academ = academic;
                this.action = "view";
                this.Refresh();
            }
        }

    }
}