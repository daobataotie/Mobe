using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Book.UI.Hr.Salary.Salaryset
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  裴盾             完成时间:2009-10-18
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class EditForm : DevExpress.XtraEditors.XtraForm
    {
        //dataset
        private DataSet dataSet = new DataSet();

        //定义变量
        private string premoney;//修改前薪资信息

        //员工管理
        private BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        private BL.DepartmentManager deptmanager = new Book.BL.DepartmentManager();
        private BL.DutyManager dutymananage = new Book.BL.DutyManager();

        public EditForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = deptmanager.Select();
            dataSet = this.employeeManager.SelectOnActiveDataSet();
            this.lookUpEditId.Properties.DataSource = this.employeeManager.SelectOnActive();
            this.lookUpEditName.Properties.DataSource = this.employeeManager.SelectOnActive();
            this.bindingSourceMonthlySalary.DataSource = dataSet.Tables[0];
            this.repositoryItemLookUpEdit2.DataSource = dutymananage.Select();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
            {
                return;
            }
            if (this.dataSet.HasChanges())
                this.employeeManager.UpdateDataDataSet(this.dataSet.GetChanges());
            try
            {
                this.dataSet.AcceptChanges();
            }
            catch
            {
                MessageBox.Show(Properties.Resources.SavaFailure, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        //全部员工信息
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.lookUpEditId.EditValue = null;
            this.lookUpEditName.EditValue = null;
            dataSet = this.employeeManager.SelectOnActiveDataSet();
            this.bindingSourceMonthlySalary.DataSource = dataSet.Tables[0];
        }

        private void lookUpEditId_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {

            LookUpEdit lookUpEdit = sender as LookUpEdit;
            if (lookUpEdit.EditValue == null) return;

            this.lookUpEditId.EditValue = lookUpEdit.EditValue;
            this.lookUpEditName.EditValue = lookUpEdit.EditValue;
            dataSet = this.employeeManager.SelectOnActiveDataSetByEmployeeId(lookUpEdit.EditValue.ToString());
            this.bindingSourceMonthlySalary.DataSource = dataSet.Tables[0];

        }

        private void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;

            DataTable dt = this.bindingSourceMonthlySalary.DataSource as DataTable;
            DataRow dr = dt.Rows[e.ListSourceRowIndex];
            if (string.IsNullOrEmpty(dr[Model.BusinessHours.PROPERTY_FROMTIME].ToString()) || string.IsNullOrEmpty(dr[Model.BusinessHours.PROPERTY_TOTIME].ToString()))
                return;
            if (e.Column.Name == this.gridColumnBusinessHours.Name && dr[Model.BusinessHours.PROPERTY_FROMTIME] != null && dr[Model.BusinessHours.PROPERTY_TOTIME] != null)
                e.DisplayText = Convert.ToDateTime(dr[Model.BusinessHours.PROPERTY_FROMTIME]).ToString("HH:mm") + "~" + Convert.ToDateTime(dr[Model.BusinessHours.PROPERTY_TOTIME]).ToString("HH:mm");


        }

        //设定员工基本默认工资
        private void btnSetDefmoney_Click(object sender, EventArgs e)
        {
            SetDefMoney sdf = new SetDefMoney();
            sdf.Show();
        }

        //当修改数据时，提示信息：修改某员工薪资信息
        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            DataTable dt = this.bindingSourceMonthlySalary.DataSource as DataTable;
            DataRow dr = dt.Rows[e.RowHandle];
            string employee = dt.Rows[e.RowHandle]["EmployeeName"].ToString();//员工
            string money = this.gridView1.FocusedColumn.Caption.ToString() + "由" + premoney + "更改為：" + this.gridView1.FocusedValue.ToString() + "，";
            MessageBox.Show("員工：" + employee + "的" + money + "是否確定？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //定位时，获取员工薪资信息
        private void gridView1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                premoney = this.gridView1.FocusedValue.ToString(); //修改前薪资信息   
            }
        }
    }
}