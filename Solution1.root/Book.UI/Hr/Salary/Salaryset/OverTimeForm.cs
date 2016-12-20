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
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���޾�            ���ʱ��:2009-11-10
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class OverTimeForm : DevExpress.XtraEditors.XtraForm
    {
        //�Ӱ����
        private BL.OverTimeManager OverTimeManger = new Book.BL.OverTimeManager();

        private DataSet overtimeData;

        private MonthSalaryClass _ms;
        private Book.Model.Employee _employee;

        public OverTimeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���������Ĺ��캯��
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
        /// ��ʼ���Ӱ���Ϣ
        /// </summary>
        private void InitOverTimeInfo()
        {
            overtimeData = this.OverTimeManger.SelectOverTimeInfoByEmployeeId(this._ms.mEmployeeId, this._ms.mIdentifyDate);
            this.bindingSourceOverTime.DataSource = overtimeData.Tables[0];

            this.txtDayFee.EditValue = this._ms.mDailyPay;                      //��н
            this.checkEditCadre.Checked = Convert.ToBoolean(this._employee.IsCadre);              //�Ƿ��Ǹɲ�
            this.txtNormalHour.EditValue = this._ms.mGeneralOverTime;           //ƽ�ռӰ�ʱ��
            this.txtNormalFee.EditValue = this._ms.mGeneralOverTimeFee;         //ƽ�ռӰ��
            this.txtHolidayCount.EditValue = this._ms.mHolidayOverTime;         //���ռӰ�ʱ��
            this.txtHolidayFee.EditValue = this._ms.mHolidayOverTimeFee;        //���ռӰ��
            #region @ old Code
            //decimal NormalHour = decimal.Zero;
            //decimal NormalFee = decimal.Zero;
            //decimal HolidayCount = decimal.Zero;
            //decimal HolidayFee = decimal.Zero;
            //for (int i = 0; i < overtimeData.Tables[0].Rows.Count; i++)
            //{
            //    //���ռӰ�          
            //    if ((bool)overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_ISHOLIDAY])
            //    {
            //        HolidayFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
            //        HolidayCount += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_EOVERTIME].ToString());
            //    }
            //    else//ƽ�ռӰ�
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