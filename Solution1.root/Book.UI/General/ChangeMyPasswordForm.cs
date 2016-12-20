using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.General
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军          完成时间:2009-10-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ChangeMyPasswordForm : DevExpress.XtraEditors.XtraForm
    {
        protected BL.OperatorsManager operatorsManager = new Book.BL.OperatorsManager();

        public ChangeMyPasswordForm()
        {
            InitializeComponent();
        }

        private void simpleButtonModify_Click(object sender, EventArgs e)
        {
            string old = this.textEditOldPassWord.Text.Trim();
            string new1 = this.textEditNewPassWord1.Text.Trim();
            string new2 = this.textEditNewPassWord2.Text.Trim();

            if (string.IsNullOrEmpty(old)) 
            {
                MessageBox.Show(Properties.Resources.KeyOldPass, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.textEditOldPassWord.Focus();
                this.textEditOldPassWord.SelectAll();
                return;
            }

            if (string.IsNullOrEmpty(new1))
            {
                MessageBox.Show(Properties.Resources.KeyNewPass, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.textEditNewPassWord1.Focus();
                this.textEditNewPassWord1.SelectAll();
                return;
            }

            if (string.IsNullOrEmpty(new2))
            {
                MessageBox.Show(Properties.Resources.AreYouSureThePass, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.textEditNewPassWord2.Focus();
                this.textEditNewPassWord2.SelectAll();
                return;
            }

            if (new1 != new2) 
            {
                MessageBox.Show(Properties.Resources.TwicePassDifference, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.textEditNewPassWord1.Focus();
                this.textEditNewPassWord1.SelectAll();
                return;
            }

            if (BL.V.ActiveOperator.Password != old) 
            {
                MessageBox.Show(Properties.Resources.OldPassError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.textEditOldPassWord.Focus();
                this.textEditOldPassWord.SelectAll();
                return;
            }

            BL.V.ActiveOperator.Password = new1;

            this.operatorsManager.Update(BL.V.ActiveOperator);

            this.DialogResult = DialogResult.OK;
            
        }
    }
}