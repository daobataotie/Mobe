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

// �� �� ��: ���            ���ʱ��:2009-10-24
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        protected BL.RoleManager roleManager = new Book.BL.RoleManager();

        public MainForm()
        {
            InitializeComponent();
        }
        //������أ�ָ������Դ
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.roleBindingSource.DataSource = this.roleManager.Select();
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void barButtonItemInsert_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Model.Role role = this.roleBindingSource.Current as Model.Role;
            EditForm f = null;
            DialogResult result = DialogResult.Cancel;
            switch (e.Item.Tag.ToString())
            {
                case "insert":
                    f = new EditForm();
                    result = f.ShowDialog(this);
                    break;
                case "update":
                    if (role != null)
                    {
                        f = new EditForm(role);
                        result = f.ShowDialog(this);
                    }
                    break;
                case "delete":
                    if (role != null)
                    {
                        if ((result = MessageBox.Show("ȷ��ɾ����", this.Text, MessageBoxButtons.OKCancel)) == DialogResult.OK)
                        {
                            this.roleManager.Delete(role.RoleId);
                        }
                    }
                    break;
                case "setprivilege":
                    if (role != null)
                    {
                        Edit2Form f2 = new Edit2Form(role);
                        f2.ShowDialog(this);
                    }
                    break;
                default:
                    break;
            }
            if (result == DialogResult.OK)
            {
                this.roleBindingSource.DataSource = this.roleManager.Select();
            }
        }
    }
}