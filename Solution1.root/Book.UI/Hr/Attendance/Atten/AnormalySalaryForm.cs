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
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 徐炎飞               完成时间:2010-2-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class AnormalySalaryForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.HrDailyEmployeeAttendInfoManager _hrManager = new Book.BL.HrDailyEmployeeAttendInfoManager();
        Model.HrDailyEmployeeAttendInfo trans_HDEA = new Model.HrDailyEmployeeAttendInfo();
        DataSet mPrintDs = new DataSet();
        public AnormalySalaryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载所有的异常报表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnormalySalaryForm_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = DateTime.Now;
            InitAnormalySalaryInfo(dateEdit1.DateTime);
        }
        private void InitAnormalySalaryInfo(DateTime dutyDate)
        {
            mPrintDs = this._hrManager.SelectHrInfoByStateAndDate(this.dateEdit1.DateTime);
            //根据考勤日期来查询考勤信息
            this.bindingSource1.DataSource = this._hrManager.SelectHrInfoByStateAndDate(this.dateEdit1.DateTime).Tables[0];
        }
        /// <summary>
        /// 转向异常的数据进行编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            string id = ((this.bindingSource1.Current as DataRowView))[Model.HrDailyEmployeeAttendInfo.PRO_HrDailyEmployeeAttendInfoId].ToString();
            DataRowView drv = (this.bindingSource1.Current as DataRowView);
            this.trans_HDEA.HrDailyEmployeeAttendInfoId = drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_HrDailyEmployeeAttendInfoId].ToString();
            this.trans_HDEA.EmployeeId = drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_EmployeeId].ToString();
            this.trans_HDEA.DutyDate = Convert.ToDateTime(drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_DutyDate]);
            this.trans_HDEA.EmployeeName = drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_EmployeeName].ToString();
            this.trans_HDEA.ShouldCheckIn = drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckIn] == DBNull.Value ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckIn]);
            this.trans_HDEA.ShouldCheckOut = drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckOut] == DBNull.Value ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckOut]);
            this.trans_HDEA.ActualCheckIn = drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckIn] == DBNull.Value ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckIn]);
            this.trans_HDEA.ActualCheckOut = drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckOut] == DBNull.Value ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckOut]);
            this.trans_HDEA.Note = drv.Row[Model.HrDailyEmployeeAttendInfo.PRO_Note].ToString();
            AnormalySalaryEditForm salaryFrm = new AnormalySalaryEditForm(trans_HDEA);
            if (salaryFrm.ShowDialog() == DialogResult.OK)
            {
                this.bindingSource1.DataSource = this._hrManager.SelectHrInfoByStateAndDate(this.dateEdit1.DateTime).Tables[0];
            }
        }
        /// <summary>
        /// 自定义列数据显示格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            DataTable table = this.bindingSource1.DataSource as DataTable;
            if (table == null || table.Rows.Count < 1) return;
            switch (e.Column.Name)
            {
                case "Employee":
                    e.DisplayText = table.Rows[e.ListSourceRowIndex][Model.Employee.PROPERTY_IDNO].ToString();
                    break;
            }
        }

        private void sbtn_print_Click(object sender, EventArgs e)
        {
            AttenreporCrystalForm acf = new AttenreporCrystalForm(this.mPrintDs, this.dateEdit1.DateTime);
            acf.Show();
        }
        //选择时间,绑定数据
        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.dateEdit1.EditValue == null)
            {
                MessageBox.Show("日期不能为空", this.Text, MessageBoxButtons.OK);
                return;
            }
            InitAnormalySalaryInfo(this.dateEdit1.DateTime);
        }
    }
}