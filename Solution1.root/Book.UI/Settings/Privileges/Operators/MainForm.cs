using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.Privileges.Operators
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-10-20
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        protected BL.OperatorsManager operatorsManager = new Book.BL.OperatorsManager();
       
        public MainForm()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditForm f = null;
            DialogResult result = DialogResult.Cancel;
            Model.Operators _operator = this.employeeBindingSource.Current as Model.Operators;

            switch (e.Item.Tag.ToString())
            {
                case"insert":
                    f = new EditForm();
                    result = f.ShowDialog(this);
                    break;
                case "update":
                    if (_operator != null) 
                    {
                        f = new EditForm(_operator);
                        result = f.ShowDialog(this);
                    }
                    break;
                case "delete":
                    if (_operator != null)
                    {
                        if (MessageBox.Show(Properties.Resources.ConfirmToDelete, "text", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK) return;
                        operatorsManager.Delete(_operator);                    
                        result = DialogResult.OK;
                    }
                    break;
                default:
                    break;
            }
            if (result == DialogResult.OK) 
            {
                this.employeeBindingSource.DataSource = this.operatorsManager.SelectOperators();
            }
        }

        //加载
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.employeeBindingSource.DataSource = this.operatorsManager.Select();
        }

        //control双击事件
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Model.Operators _operator = this.employeeBindingSource.Current as Model.Operators;
            RoleToOperators f = new RoleToOperators(_operator);
            if (f.ShowDialog(this) != DialogResult.OK)     return;

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.Operators> details = this.employeeBindingSource.DataSource as IList<Model.Operators>;
            if (details == null || details.Count < 1) return;
            Model.Employee emp = details[e.ListSourceRowIndex].Employee;
            if (emp == null) return;
            switch (e.Column.Name)
            {
                case "gridColumndepot":
          
                    e.DisplayText =emp.Department==null?"": emp.Department.ToString();
                    break;

            }
        }
    }
}