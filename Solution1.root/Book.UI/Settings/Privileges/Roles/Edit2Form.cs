using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.Privileges.Roles
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-10-22
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Edit2Form : DevExpress.XtraEditors.XtraForm
    {
        private Model.Role role;

        private BL.RoleOperationManager roleOperationManager = new Book.BL.RoleOperationManager();

        public Edit2Form(Model.Role role)
        {
            InitializeComponent();

            this.role = role;
        }

        //加载，指定数据源 
        private void Edit2Form_Load(object sender, EventArgs e)
        {           
            this.textEditRoleName.EditValue = this.role;

            this.roleOperationBindingSource.DataSource = this.roleOperationManager.Select(this.role);
        }

        /// <summary>
        /// 保存 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<Model.RoleOperation> list = this.roleOperationBindingSource.DataSource as IList<Model.RoleOperation>;

            foreach (Model.RoleOperation roleoperation in list)
            {
                this.roleOperationManager.Update(roleoperation);
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}