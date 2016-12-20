using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.Options
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-14
// 修改原因：
// 修 改 人: 刘永亮                   修改时间:2010-08-04
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class CompanyInfo : DevExpress.XtraEditors.XtraForm
    {
        public static string _address = "";

        public CompanyInfo()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BL.Settings.CompanyChineseName = this.textEditChineseName.Text;
            BL.Settings.CompanyEnglishName = this.textEditEnglishName.Text;
            BL.Settings.CompanyPrinciple = this.textEditPrincipal.Text;
            BL.Settings.CompanyPhone = this.textEditPhone.Text;
            BL.Settings.CompanyFax = this.textEditFax.Text;
            BL.Settings.CompanyEMail = this.textEditEMail.Text;
            BL.Settings.CompanyWebUrl = this.textEditWebUrl.Text;
            BL.Settings.CompanyAddress1 = this.textEditAddress1.Text;
            BL.Settings.CompanyAddress2 = this.textEditAddress2.Text;
            BL.Settings.CompanyAddress3 = this.textEditAddress3.Text;
            BL.Settings.CompanyNotes = this.memoEditNotes.Text;
            byte[] piclogo = null;
            if (this.buttonEditPictrue.Text != "")
            {
                piclogo = System.IO.File.ReadAllBytes(this.buttonEditPictrue.Text);
                BL.Settings.PictrueLogo = piclogo;
            }

            BL.Settings.FactoryAddress = this.textEdit1.Text;

            this.MdiParent.Text = this.textEditChineseName.Text;

            MessageBox.Show(Properties.Resources.SaveSuccess, this.Text);
        }


        /// <summary>
        /// 加载，初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompanyInfo_Load(object sender, EventArgs e)
        {
            this.textEditChineseName.Text = BL.Settings.CompanyChineseName;
            this.textEditEnglishName.Text = BL.Settings.CompanyEnglishName;
            this.textEditPrincipal.Text = BL.Settings.CompanyPrinciple;
            this.textEditPhone.Text = BL.Settings.CompanyPhone;
            this.textEditFax.Text = BL.Settings.CompanyFax;
            this.textEditEMail.Text = BL.Settings.CompanyEMail;
            this.textEditWebUrl.Text = BL.Settings.CompanyWebUrl;
            this.textEditAddress1.Text = BL.Settings.CompanyAddress1;
            this.textEditAddress2.Text = BL.Settings.CompanyAddress2;
            this.memoEditNotes.Text = BL.Settings.CompanyNotes;
            this.textEditAddress3.Text = BL.Settings.CompanyAddress3;
            byte[] image = BL.Settings.PictrueLogo;
            if (image != null)
                this.pictureEdit1.Image = Image.FromStream(new System.IO.MemoryStream(image));
            this.textEdit1.Text = BL.Settings.FactoryAddress;
        }



        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEdit1.Checked)
            {
                this.textEditAddress2.Text = this.textEditAddress1.Text;
            }
        }

        private void buttonEditPictrue_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = this.openFileDialog1.FileName;
                if (System.IO.File.Exists(fileName))
                {
                    this.buttonEditPictrue.Text = fileName;
                    this.pictureEdit1.Image = Image.FromFile(fileName);
                }
                else
                {
                    MessageBox.Show(this, "fileNotFound", "文件不存在！", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEdit2.Checked)
            {
                this.textEdit1.Text = this.textEditAddress1.Text;
            }
        }

        private void textEditAddress1_DoubleClick(object sender, EventArgs e)
        {

            BasicData.RegionalAddressForm f = new BasicData.RegionalAddressForm();
            if (f.ShowDialog(this) != DialogResult.OK) return;
           (sender as TextEdit).Text= _address;
        }
    }
}