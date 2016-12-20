using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace Book.UI.Settings.BasicData.SpecificFee
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2009-10-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class SpecificFeeForm : DevExpress.XtraEditors.XtraForm
    {

        //员工管理
        private  Book.BL.EmployeeManager empoyeemanager=new Book.BL.EmployeeManager ();

        //部门管理
        private Book.BL.DepartmentManager departmanager = new Book.BL.DepartmentManager();



        public SpecificFeeForm()
        {
            InitializeComponent();
        }



        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecificFeeForm_Load(object sender, EventArgs e)
        {

            this.txtcmb_edit_date.SelectedIndex = 0;

            for (int i = 0; i < 10; i++)
            {
                DateTime.Now.AddMonths(-i);
                this.txtcmb_edit_date.Properties.Items.Add(DateTime.Now.AddMonths(-i).ToString("yyyy年MM月"));
            }

            this.EmploryeeSource.DataSource = empoyeemanager.SelectOnActive();
            this.DepartSource.DataSource = departmanager.Select();
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

    }
}