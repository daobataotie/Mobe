using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Hr.Salary.Salaryset
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-11-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class OverTimeForm : DevExpress.XtraEditors.XtraForm
    {
        //加班管理
        private BL.OverTimeManager OverTimeManger = new Book.BL.OverTimeManager();

        private DataSet overtimeData;

        private MonthSalaryClass _ms;
        private Book.Model.Employee _employee;

        public OverTimeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 三个参数的构造函数
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="dueDate"></param>
        public OverTimeForm(MonthSalaryClass ms, Book.Model.Employee emp)
            : this()
        {
            this._ms = ms;
            this._employee = emp;
        }

        private void OverTimeForm_Load(object sender, EventArgs e)
        {
            InitOverTimeInfo();
        }

        /// <summary>
        /// 初始化加班信息
        /// </summary>
        private void InitOverTimeInfo()
        {
            overtimeData = this.OverTimeManger.SelectOverTimeInfoByEmployeeId(this._ms.mEmployeeId, this._ms.mIdentifyDate);
            this.bindingSourceOverTime.DataSource = overtimeData.Tables[0];

            this.txtDayFee.EditValue = this._ms.mDailyPay;                      //日薪
            this.checkEditCadre.Checked = Convert.ToBoolean(this._employee.IsCadre);              //是否是干部
            this.txtNormalHour.EditValue = this._ms.mGeneralOverTime;           //平日加班时数
            this.txtNormalFee.EditValue = this._ms.mGeneralOverTimeFee;         //平日加班费
            this.txtHolidayCount.EditValue = this._ms.mHolidayOverTime;         //假日加班时数
            this.txtHolidayFee.EditValue = this._ms.mHolidayOverTimeFee;        //假日加班费
            #region @ old Code
            //decimal NormalHour = decimal.Zero;
            //decimal NormalFee = decimal.Zero;
            //decimal HolidayCount = decimal.Zero;
            //decimal HolidayFee = decimal.Zero;
            //for (int i = 0; i < overtimeData.Tables[0].Rows.Count; i++)
            //{
            //    //假日加班          
            //    if ((bool)overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_ISHOLIDAY])
            //    {
            //        HolidayFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
            //        HolidayCount += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_EOVERTIME].ToString());
            //    }
            //    else//平日加班
            //    {
            //        NormalHour += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_EOVERTIME].ToString());
            //        NormalFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
            //    }

            //}
            //this.txtNormalHour.EditValue = NormalHour;
            //this.txtNormalFee.EditValue = NormalFee;
            //this.txtHolidayCount.EditValue = HolidayCount;
            //this.txtHolidayFee.EditValue = HolidayFee;
            #endregion
        }
    }
}