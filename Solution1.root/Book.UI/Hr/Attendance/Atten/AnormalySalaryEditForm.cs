using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace Book.UI.Hr.Attendance.Atten
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 徐炎飞            完成时间:2010-2-5
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class AnormalySalaryEditForm : DevExpress.XtraEditors.XtraForm
    {
        public AnormalySalaryEditForm()
        {
            InitializeComponent();
        }
        //薪资编号
        private string _HrDailyEmployeeAttendInfoId;
        //BL层HrDailyEmployeeAttendInfoManager类
        private BL.HrDailyEmployeeAttendInfoManager _hrManger = new Book.BL.HrDailyEmployeeAttendInfoManager();
        private Model.HrDailyEmployeeAttendInfo _hrDaily_begUpdate;     //传递过来的Model
        private Model.HrDailyEmployeeAttendInfo _hrDaily;
        //保存异常考勤信息集合
        private DataSet data;
        //员工
        private Model.Employee _employee;
        private BL.EmployeeManager _employeeManager = new Book.BL.EmployeeManager();
        //获取传来异常数据Model
        public AnormalySalaryEditForm(Model.HrDailyEmployeeAttendInfo trans_HDEA)
        {
            InitializeComponent();
            this._hrDaily_begUpdate = trans_HDEA;
        }
        // 加载异常考勤信息
        private void AnormalySalaryEditForm_Load(object sender, EventArgs e)
        {
            if (this._hrDaily_begUpdate != null)
            {
                this.lblEmployeeName.Text = this._hrDaily_begUpdate.EmployeeName;
                this.lblNote.Text = this._hrDaily_begUpdate.Note;
                this.lblShouldCheckIn.Text = this._hrDaily_begUpdate.ShouldCheckIn.Value.ToString("HH:mm");
                this.lblShouldCheckOut.Text = this._hrDaily_begUpdate.ShouldCheckOut.Value.ToString("HH:mm");
                this.lblActualCheckIn.Text = this._hrDaily_begUpdate.ActualCheckIn == global::Helper.DateTimeParse.NullDate ? "" : this._hrDaily_begUpdate.ActualCheckIn.Value.ToString("HH:mm");
                this.lblActualCheckOut.Text = this._hrDaily_begUpdate.ActualCheckOut == global::Helper.DateTimeParse.NullDate ? "" : this._hrDaily_begUpdate.ActualCheckOut.Value.ToString("HH:mm");
                this.lblDutyDate.Text = this._hrDaily_begUpdate.DutyDate.Value.ToString("HH:mm");

                //获取重新考勤之后的结果
                Model.Employee emp = new BL.EmployeeManager().Get(this._hrDaily_begUpdate.EmployeeId);
                this._hrDaily = new BL.HrDailyEmployeeAttendInfoManager().Reatten_Controller(this._hrDaily_begUpdate.DutyDate.Value, emp);
                if (this._hrDaily != null)
                {
                    this.ShouldCheckIn.Text = this._hrDaily.ShouldCheckIn.Value.ToString("HH:mm");
                    this.ShouldCheckOut.Text = this._hrDaily.ShouldCheckOut.Value.ToString("HH:mm");
                    this.txtActualCheckInTime.EditValue = this._hrDaily.ActualCheckIn == null ? "" : this._hrDaily_begUpdate.ActualCheckIn.Value.ToString("HH:mm");
                    this.txtActualCheckOutTime.EditValue = this._hrDaily.ActualCheckOut == null ? "" : this._hrDaily_begUpdate.ActualCheckOut.Value.ToString("HH:mm");
                    this.Note.Text = this._hrDaily.Note;
                }
            }
        }
        // 根据考勤编号查询信息并为各个控件进行赋值
        //private void LoadSingalHrInfo(string HrDailyEmployeeAttendInfoId)
        //{
        //    data = this._hrManger.SelectHrInfoById(HrDailyEmployeeAttendInfoId);
        //    if (data.Tables[0].Rows.Count > 0)
        //    {
        //        if (data.Tables[0].Rows[0][Model.Employee.PROPERTY_EMPLOYEENAME] != null && data.Tables[0].Rows[0][Model.Employee.PROPERTY_EMPLOYEENAME] != DBNull.Value)
        //            this.lblEmployeeName.Text = data.Tables[0].Rows[0][Model.Employee.PROPERTY_EMPLOYEENAME].ToString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_Note] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_Note] != DBNull.Value)
        //            this.lblNote.Text = data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_Note].ToString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckIn] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckIn] != DBNull.Value)
        //            this.lblShouldCheckIn.Text = Convert.ToDateTime(data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckIn]).ToShortTimeString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckOut] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckOut] != DBNull.Value)
        //            this.lblShouldCheckOut.Text = Convert.ToDateTime(data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckOut]).ToShortTimeString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckIn] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckIn] != DBNull.Value)
        //            this.lblActualCheckIn.Text = Convert.ToDateTime(data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckIn]).ToShortTimeString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckOut] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckOut] != DBNull.Value)
        //            this.lblActualCheckOut.Text = Convert.ToDateTime(data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckOut]).ToShortTimeString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_DutyDate] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_DutyDate] != DBNull.Value)
        //            this.lblDutyDate.Text = Convert.ToDateTime(data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_DutyDate]).ToShortDateString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckIn] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckIn] != DBNull.Value)
        //            this.ShouldCheckIn.Text = Convert.ToDateTime(data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckIn]).ToShortTimeString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckOut] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckOut] != DBNull.Value)
        //            this.ShouldCheckOut.Text = Convert.ToDateTime(data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ShouldCheckOut]).ToShortTimeString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckIn] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckIn] != DBNull.Value)
        //            this.txtActualCheckIn.EditValue = Convert.ToDateTime(data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckIn]).ToShortTimeString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckOut] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckOut] != DBNull.Value)
        //            this.txtActualCheckOut.EditValue = Convert.ToDateTime(data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_ActualCheckOut]).ToShortTimeString();
        //        if (data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_Note] != null && data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_Note] != DBNull.Value)
        //            this.Note.Text = data.Tables[0].Rows[0][Model.HrDailyEmployeeAttendInfo.PRO_Note].ToString();
        //    }
        //}
        // 对考勤进行修改的操作
        private void btnValidate_Click(object sender, EventArgs e)
        {
            //if (txtActualCheckIn.EditValue == null)
            //{
            //    dxErrorProvider1.SetError(this.txtActualCheckIn, "上班时间不能为空!");
            //    return;
            //}
            //if (txtActualCheckOut.EditValue == null)
            //{
            //    dxErrorProvider2.SetError(this.txtActualCheckOut, "下班时间不能为空!");
            //    return;
            //}
            //if (!ValidateInput(txtActualCheckIn.EditValue.ToString()))
            //{
            //    dxErrorProvider1.SetError(this.txtActualCheckIn, "格式不正确，类似: 00:00");
            //    return;
            //}
            //if (!ValidateInput(txtActualCheckOut.EditValue.ToString()))
            //{
            //    dxErrorProvider2.SetError(this.txtActualCheckOut, "格式不正确,类似: 00:00");
            //    return;
            //}
            //dxErrorProvider1.SetError(this.txtActualCheckIn, string.Empty);
            //dxErrorProvider2.SetError(this.txtActualCheckOut, string.Empty);
            //this._hrDaily = new Book.Model.HrDailyEmployeeAttendInfo();
            //this._hrDaily.HrDailyEmployeeAttendInfoId = this._HrDailyEmployeeAttendInfoId;
            //this._employee = this._employeeManager.Get(this.data.Tables[0].Rows[0][Model.Employee.PROPERTY_EMPLOYEEID].ToString());
            //if (data.Tables[0].Rows.Count > 0)
            //{
            //    this._hrDaily.EmployeeId = data.Tables[0].Rows[0][Model.Employee.PROPERTY_EMPLOYEEID].ToString();
            //}
            //this._hrDaily.DutyDate = Convert.ToDateTime(this.lblDutyDate.Text);
            //if (!string.IsNullOrEmpty(this.lblShouldCheckIn.Text))
            //    this._hrDaily.ShouldCheckIn = Convert.ToDateTime(this.lblShouldCheckIn.Text);
            //if (!string.IsNullOrEmpty(this.lblShouldCheckOut.Text))
            //    this._hrDaily.ShouldCheckOut = Convert.ToDateTime(this.lblShouldCheckOut.Text);
            //if (txtActualCheckOut.EditValue.ToString().IndexOf(':') == -1)
            //    this._hrDaily.ActualCheckOut = Convert.ToDateTime(Convert.ToDateTime(lblDutyDate.Text).ToShortDateString() + " " + Convert.ToDateTime(txtActualCheckOut.EditValue.ToString() + ":00").ToShortTimeString());
            //else
            //    this._hrDaily.ActualCheckOut = Convert.ToDateTime(Convert.ToDateTime(lblDutyDate.Text).ToShortDateString() + " " + Convert.ToDateTime(txtActualCheckOut.EditValue).ToShortTimeString());
            //if (txtActualCheckIn.EditValue.ToString().IndexOf(':') == -1)
            //    this._hrDaily.ActualCheckIn = Convert.ToDateTime(Convert.ToDateTime(lblDutyDate.Text).ToShortDateString() + " " + Convert.ToDateTime(this.txtActualCheckIn.EditValue.ToString() + ":00").ToShortTimeString());
            //else
            //    this._hrDaily.ActualCheckIn = Convert.ToDateTime(Convert.ToDateTime(lblDutyDate.Text).ToShortDateString() + " " + Convert.ToDateTime(this.txtActualCheckIn.EditValue).ToShortTimeString());
            //if (!string.IsNullOrEmpty(lblShouldCheckIn.Text))
            //{
            //    double shouldCheckIn = 0;
            //    if (Convert.ToDateTime(lblShouldCheckIn.Text).Minute >= 10)
            //        shouldCheckIn = Convert.ToDouble(Convert.ToDateTime(lblShouldCheckIn.Text).Hour + "." + Convert.ToDateTime(lblShouldCheckIn.Text).Minute);
            //    else
            //        shouldCheckIn = Convert.ToDouble(Convert.ToDateTime(lblShouldCheckIn.Text).Hour + ".0" + Convert.ToDateTime(lblShouldCheckIn.Text).Minute);
            //    double actualCheckIn = 0;
            //    if (txtActualCheckIn.EditValue.ToString().IndexOf(':') == -1)
            //    {
            //        actualCheckIn = Convert.ToDouble(txtActualCheckIn.EditValue);
            //    }
            //    else
            //    {
            //        if (Convert.ToDateTime(txtActualCheckIn.Text).Minute >= 10)
            //            actualCheckIn = Convert.ToDouble(Convert.ToDateTime(txtActualCheckIn.EditValue).Hour + "." + Convert.ToDateTime(txtActualCheckIn.EditValue).Minute);
            //        else
            //            actualCheckIn = Convert.ToDouble(Convert.ToDateTime(txtActualCheckIn.EditValue).Hour + ".0" + Convert.ToDateTime(txtActualCheckIn.EditValue).Minute);
            //    }
            //    double result = actualCheckIn - shouldCheckIn;
            //    if (result > 0)
            //    {
            //        if (result.ToString().IndexOf('.') != -1)
            //        {
            //            if (result.ToString().Substring(result.ToString().IndexOf('.') + 1).Length > 1)
            //            {
            //                if (Convert.ToInt32(result.ToString().Substring(0, result.ToString().IndexOf('.'))) * 60 + (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1, 2))) < 10)
            //                {
            //                    this._hrManger.DeleteLateInfoByEmployeeIdAndDate(this._employee.EmployeeId, Convert.ToDateTime(lblDutyDate.Text));
            //                    DataSet ds = _hrManger.SelectLateInfo(this.data.Tables[0].Rows[0]["EmployeeId"].ToString(), Convert.ToDateTime(this._hrDaily.DutyDate));
            //                    if (ds.Tables[0].Rows.Count < 3)
            //                    {
            //                        double total = 0;
            //                        for (int mm = 0; mm < ds.Tables[0].Rows.Count; mm++)
            //                        {
            //                            total += Convert.ToInt32(ds.Tables[0].Rows[mm]["LateInMinute"]);
            //                        }
            //                        if (result.ToString().Substring(result.ToString().IndexOf('.') + 1).Length >= 3)
            //                        {
            //                            if (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 3, 1)) > 5)
            //                            {
            //                                result = Convert.ToDouble(result.ToString().Substring(0, result.ToString().IndexOf('.')) + "." + (Convert.ToDouble(result.ToString().Substring(result.ToString().IndexOf('.') + 1, 2)) + 1));
            //                            }
            //                            else
            //                            {
            //                                result = Convert.ToDouble(result.ToString().Substring(0, result.ToString().IndexOf('.')) + "." + (Convert.ToDouble(result.ToString().Substring(result.ToString().IndexOf('.') + 1, 2))));
            //                            }
            //                        }
            //                        this._hrDaily.LateInMinute = Convert.ToInt32(result.ToString().Substring(0, result.ToString().IndexOf('.'))) * 60 + (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1)));
            //                        this._hrDaily.IsNormal = false;
            //                        this._hrDaily.Note = "迟到";
            //                        total += Convert.ToDouble(this._hrDaily.LateInMinute);
            //                        if (total <= 10)
            //                        {
            //                            this._hrManger.InsertLateInfo(Guid.NewGuid().ToString(), data.Tables[0].Rows[0]["EmployeeId"].ToString(), Convert.ToDateTime(this._hrDaily.DutyDate), Convert.ToInt32(this._hrDaily.LateInMinute));
            //                            this._hrDaily.LateInMinute = null;
            //                            this._hrDaily.IsNormal = true;
            //                            this._hrDaily.Note = null;
            //                            this._hrDaily.ActualCheckIn = this._hrDaily.ShouldCheckIn;
            //                            if (_employee.EmployeeJoinDate != null)
            //                            {
            //                                if (DateTime.Now.Subtract(Convert.ToDateTime(_employee.EmployeeJoinDate)).Days > 20)
            //                                {
            //                                    this._hrDaily.DayFactor = 1;
            //                                    this._hrDaily.MonthFactor = 1;
            //                                }
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        this._hrDaily.LateInMinute = Convert.ToInt32(result.ToString().Substring(0, result.ToString().IndexOf('.'))) * 60 + (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1, 2)));
            //                        this._hrDaily.Note = "迟到";
            //                        this._hrDaily.IsNormal = false;
            //                    }

            //                }
            //                else
            //                {
            //                    this._hrDaily.LateInMinute = Convert.ToInt32(result.ToString().Substring(0, result.ToString().IndexOf('.'))) * 60 + (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1, 2)));
            //                    this._hrDaily.Note = "迟到";
            //                    this._hrDaily.IsNormal = false;
            //                }
            //            }
            //            else
            //            {
            //                //Convert.ToInt32(result.ToString().Substring(0, result.ToString().IndexOf('.'))) * 60 + (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1))) == 1
            //                DataSet ds = _hrManger.SelectLateInfo(_employee.EmployeeId, Convert.ToDateTime(this._hrDaily.DutyDate));
            //                if (ds.Tables[0].Rows.Count < 3)
            //                {
            //                    int total = 0;
            //                    for (int mm = 0; mm < ds.Tables[0].Rows.Count; mm++)
            //                    {
            //                        total += Convert.ToInt32(ds.Tables[0].Rows[mm]["LateInMinute"]);
            //                    }
            //                    this._hrDaily.LateInMinute = Convert.ToInt32(result.ToString().Substring(0, result.ToString().IndexOf('.'))) * 60 + (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1)));
            //                    this._hrDaily.Note = "迟到";
            //                    total += Convert.ToInt32(this._hrDaily.LateInMinute);
            //                    this._hrDaily.IsNormal = false;
            //                    if (total <= 10)
            //                    {
            //                        this._hrManger.InsertLateInfo(Guid.NewGuid().ToString(), _employee.EmployeeId, Convert.ToDateTime(this._hrDaily.DutyDate), Convert.ToInt32(this._hrDaily.LateInMinute));
            //                        this._hrDaily.LateInMinute = null;
            //                        this._hrDaily.IsNormal = true;
            //                        this._hrDaily.Note = null;
            //                        this._hrDaily.ActualCheckIn = this._hrDaily.ShouldCheckIn;
            //                        if (_employee.EmployeeJoinDate != null)
            //                        {
            //                            if (DateTime.Now.Subtract(Convert.ToDateTime(_employee.EmployeeJoinDate)).Days > 20)
            //                            {
            //                                this._hrDaily.DayFactor = 1;
            //                                this._hrDaily.MonthFactor = 1;
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    this._hrDaily.LateInMinute = Convert.ToInt32(result.ToString().Substring(0, result.ToString().IndexOf('.'))) * 60 + (Convert.ToInt32(result.ToString().Substring(result.ToString().IndexOf('.') + 1)));
            //                    this._hrDaily.IsNormal = false;
            //                    this._hrDaily.Note = "迟到";
            //                }
            //            }
            //        }
            //        else
            //        {
            //            this._hrDaily.IsNormal = false;
            //            this._hrDaily.LateInMinute = Convert.ToInt32(result) * 60;
            //            this._hrDaily.Note = "迟到";
            //        }
            //    }
            //    else
            //    {
            //        this._hrDaily.Note = null;
            //        if (_employee.EmployeeJoinDate != null)
            //        {
            //            if (DateTime.Now.Subtract(Convert.ToDateTime(_employee.EmployeeJoinDate)).Days > 20)
            //            {
            //                this._hrDaily.DayFactor = 1;
            //                this._hrDaily.MonthFactor = 1;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    this._hrManger.DeleteLateInfoByEmployeeIdAndDate(this._employee.EmployeeId, Convert.ToDateTime(lblDutyDate.Text));
            //    if (txtActualCheckOut.EditValue.ToString().IndexOf(':') == -1)
            //        this._hrDaily.ActualCheckOut = Convert.ToDateTime(Convert.ToDateTime(lblDutyDate.Text).ToShortDateString() + " " + Convert.ToDateTime(txtActualCheckOut.EditValue.ToString() + ":00").ToShortTimeString());
            //    else
            //        this._hrDaily.ActualCheckOut = Convert.ToDateTime(Convert.ToDateTime(lblDutyDate.Text).ToShortDateString() + " " + Convert.ToDateTime(txtActualCheckOut.EditValue).ToShortTimeString());
            //    if (txtActualCheckIn.EditValue.ToString().IndexOf(':') == -1)
            //        this._hrDaily.ActualCheckIn = Convert.ToDateTime(Convert.ToDateTime(lblDutyDate.Text).ToShortDateString() + " " + Convert.ToDateTime(this.txtActualCheckIn.EditValue.ToString() + ":00").ToShortTimeString());
            //    else
            //        this._hrDaily.ActualCheckIn = Convert.ToDateTime(Convert.ToDateTime(lblDutyDate.Text).ToShortDateString() + " " + Convert.ToDateTime(this.txtActualCheckIn.EditValue).ToShortTimeString());
            //    if (_employee.EmployeeJoinDate != null)
            //    {
            //        if (DateTime.Now.Subtract(Convert.ToDateTime(_employee.EmployeeJoinDate)).Days > 20)
            //        {
            //            this._hrDaily.DayFactor = 1;
            //            this._hrDaily.MonthFactor = 1;
            //        }
            //    }
            //}
            //if (this._hrDaily.Note != null)
            //    this._hrDaily.IsNormal = false;
            //else
            //    this._hrDaily.IsNormal = true;
            //this._hrManger.Update(this._hrDaily);
        }
        // 当实际上班时间发生改变
        private void txtActualCheckIn_EditValueChanged(object sender, EventArgs e)
        {
            //if (txtActualCheckIn.EditValue == null)
            //{
            //    dxErrorProvider1.SetError(this.txtActualCheckIn, "上班时间不能为空!");
            //    chkNote.Checked = false;
            //    return;
            //}
            //if (txtActualCheckOut.EditValue == null)
            //{
            //    dxErrorProvider2.SetError(this.txtActualCheckOut, "下班时间不能为空!");
            //    chkNote.Checked = false;
            //    return;
            //}
            //if (!ValidateInput(txtActualCheckIn.EditValue.ToString()))
            //{
            //    dxErrorProvider1.SetError(this.txtActualCheckIn, "格式不正确，类似: 00:00");
            //    chkNote.Checked = false;
            //    return;
            //}
            //if (!ValidateInput(txtActualCheckOut.EditValue.ToString()))
            //{
            //    dxErrorProvider2.SetError(this.txtActualCheckOut, "格式不正确,类似: 00:00");
            //    chkNote.Checked = false;
            //    return;
            //}
            //dxErrorProvider1.SetError(this.txtActualCheckIn, string.Empty);
            //dxErrorProvider2.SetError(this.txtActualCheckOut, string.Empty);
            //this.Note.Text = string.Empty;
            //this.chkNote.Checked = true;
        }
        // 当实际下班时间发生改变
        private void txtActualCheckOut_EditValueChanged(object sender, EventArgs e)
        {
            //if (txtActualCheckIn.EditValue == null)
            //{
            //    dxErrorProvider1.SetError(this.txtActualCheckIn, "上班时间不能为空!");
            //    chkNote.Checked = false;
            //    return;
            //}
            //if (txtActualCheckOut.EditValue == null)
            //{
            //    dxErrorProvider2.SetError(this.txtActualCheckOut, "下班时间不能为空!");
            //    chkNote.Checked = false;
            //    return;
            //}
            //if (!ValidateInput(txtActualCheckIn.EditValue.ToString()))
            //{
            //    dxErrorProvider1.SetError(this.txtActualCheckIn, "格式不正确，类似: 00:00");
            //    chkNote.Checked = false;
            //    return;
            //}
            //if (!ValidateInput(txtActualCheckOut.EditValue.ToString()))
            //{
            //    dxErrorProvider2.SetError(this.txtActualCheckOut, "格式不正确,类似: 00:00");
            //    chkNote.Checked = false;
            //    return;
            //}
            //dxErrorProvider1.SetError(this.txtActualCheckIn, string.Empty);
            //dxErrorProvider2.SetError(this.txtActualCheckOut, string.Empty);
            //this.Note.Text = string.Empty;
            //this.chkNote.Checked = true;
        }
        //保存修改
        private void barBtn_save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region 判断输入时间
            if (this.txtActualCheckInTime.Text != string.Empty)
            {
                try
                {
                    this._hrDaily.ActualCheckIn = DateTime.Parse(this._hrDaily_begUpdate.DutyDate.Value.ToString("yyyy-MM-dd") + " " + this.txtActualCheckInTime.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("輸入時間格式有誤,請查證!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("實際上班時間不能為空!");
                return;
            }
            if (this.txtActualCheckOutTime.Text != string.Empty)
            {
                try
                {
                    this._hrDaily.ActualCheckOut = DateTime.Parse(this._hrDaily_begUpdate.DutyDate.Value.ToString("yyyy-MM-dd") + " " + this.txtActualCheckOutTime.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("輸入時間格式有誤,請查證!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("實際下班時間不能為空!");
                return;
            }

            //判断换日
            TimeSpan? ts_1 = this._hrDaily.ShouldCheckIn - this._hrDaily.ActualCheckIn;
            if (Math.Abs(ts_1.Value.Days * 24 + ts_1.Value.Hours) > 13)
                this._hrDaily.ActualCheckIn = this._hrDaily.ActualCheckIn.Value.AddDays(1);
            TimeSpan? ts_2 = this._hrDaily.ShouldCheckOut - this._hrDaily.ActualCheckOut;
            if (Math.Abs(ts_2.Value.Days * 24 + ts_2.Value.Hours) > 13)
                this._hrDaily.ActualCheckOut = this._hrDaily.ActualCheckOut.Value.AddDays(1);

            if (this._hrDaily.ActualCheckIn > this._hrDaily.ActualCheckOut)
            {
                MessageBox.Show("實際上班時間,不能大於實際下班時間!");
                return;
            }
            #endregion
            if (this._hrDaily.MLeave != null)
            {
                if (this._hrDaily.MLeave.LeaveRange != 0)
                {
                    this._hrDaily.DayFactor = 0.5 + 0.5 * this._hrDaily.MLeave.LeaveType.PayRate;
                    this._hrDaily.SpecialBonus = Convert.ToInt32(Convert.ToDouble(this._hrDaily.MBusinessHours.SpecialPay) * 0.5);
                    if (this._hrDaily.MLeave.LeaveRange == 1)
                    {
                        this._hrDaily.Note = this._hrDaily.MLeave.LeaveType.ToString() + "(上半日)";
                    }
                    else
                    {
                        this._hrDaily.Note = this._hrDaily.MLeave.LeaveType.ToString() + "(下半日)";
                    }
                }
            }
            else
            {
                this._hrDaily.SpecialBonus = Convert.ToInt32(this._hrDaily.MBusinessHours.SpecialPay);
                this._hrDaily.DayFactor = 1;
            }

            TimeSpan? ts = this._hrDaily.ActualCheckIn - this._hrDaily.ShouldCheckIn;
            if ((ts.Value.Hours * 60 + ts.Value.Minutes) > 1)
            {
                this._hrDaily.LateInMinute = ts.Value.Hours * 60 + ts.Value.Minutes;
            }
            else
            {
                this._hrDaily.LateInMinute = 0;
            }

            this._hrDaily.Note = this._hrDaily.Note.Replace(";卻刷卡資料","");
            this._hrDaily.Note = this._hrDaily.Note.Replace("卻刷卡資料","");

            this._hrDaily.MonthFactor = 1;  //月基数设为1
            this._hrDaily.IsNormal = true;  //设置为不存在异常
            if (this._hrManger.UpdateSave_AnormalySalaryEditForm(this._hrDaily) > 0)//保存修改
            {
                MessageBox.Show(Properties.Resources.SaveSuccess);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(Properties.Resources.SavaFailure);
            }
        }
        //取消
        private void barBtn_cancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}