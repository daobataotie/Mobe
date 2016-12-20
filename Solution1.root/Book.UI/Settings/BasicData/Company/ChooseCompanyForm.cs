using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Company
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 选择公司信息
   // 文 件 名：ChooseCompanyForm
   // 编 码 人: 马艳军                   完成时间:2009-09-19
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/

    public partial class ChooseCompanyForm : BaseChooseForm
    {
        public ChooseCompanyForm()
        {
            InitializeComponent();
        }

        private void simpleButtonNew_Click(object sender, EventArgs e)
        {
            EditForm f = new EditForm();
            if (f.ShowDialog() != DialogResult.OK)
            {
                return;
            }
        }

        protected override void LoadData()
        {
            this.bindingSource1.DataSource = new BL.CompanyManager().Select();
        }
    }
}