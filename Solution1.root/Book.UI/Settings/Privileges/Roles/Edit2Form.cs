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
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���            ���ʱ��:2009-10-22
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
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

        //���أ�ָ������Դ 
        private void Edit2Form_Load(object sender, EventArgs e)
        {           
            this.textEditRoleName.EditValue = this.role;

            this.roleOperationBindingSource.DataSource = this.roleOperationManager.Select(this.role);
        }

        /// <summary>
        /// ���� 
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