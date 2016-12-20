using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace Book.UI.Hr.Attendance.Leave
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-11-10
// 修改原因：
// 修 改 人: 刘永亮                   修改时间:2010-07-19
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class LeaveTypeForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.LeaveTypeManager _leaveTypeManagaer = new Book.BL.LeaveTypeManager();
        //private IDictionary<string, AA> invalidValueExceptions;
        private DataSet leaveTypeData = new DataSet();
        public LeaveTypeForm()
        {
            InitializeComponent();
            //this.invalidValueExceptions.Add(Model.LeaveType.PROPERTY_LEAVETYPENAME, new AA(Properties.Resources.IsExistsLeaveTypeName, this.dataGridView1));
        }
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeaveTypeForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            InitLeaveTypeInfo();
        }

        private void InitLeaveTypeInfo()
        {
            leaveTypeData = this._leaveTypeManagaer.SelectLeaveTypeInfo();
            this.bindingSource1.DataSource = leaveTypeData.Tables[0];
        }
        /// <summary>
        ///保存操作
        /// </summary>
        /// <param name="sender"></param
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textEdit1.Visible = true;
            textEdit1.Focus();

            try
            {
                try
                {
                    this._leaveTypeManagaer.SaveLeaveTypeInfo(leaveTypeData.Tables[0]);
                }
                catch (global::Helper.InvalidValueException ex)
                {
                    MessageBox.Show(Properties.Resources.EntityName + "\r\t" + ex.Message);
                    InitLeaveTypeInfo();
                    textEdit1.Visible = false;
                    return;
                }
                leaveTypeData.AcceptChanges();
                MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK);
                InitLeaveTypeInfo();
                textEdit1.Visible = false;
            }
            catch (Exception ex)
            {
                textEdit1.Visible = false;
                MessageBox.Show("此信息有其他引用,不能删除", this.Text, MessageBoxButtons.OK);
                InitLeaveTypeInfo();
                return;
            }



        }
    }
}