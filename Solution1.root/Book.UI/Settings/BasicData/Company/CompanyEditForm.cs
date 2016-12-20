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
    public partial class CompanyEditForm : BaseEditForm
    {

        private Model.Company _company = new Book.Model.Company();
        private BL.CompanyManager _companyManager = new Book.BL.CompanyManager();

        public CompanyEditForm()
        {
            InitializeComponent();
            this.action = "view";
            this._company = this._companyManager.SelectIsDefaultCompany();
        }

        private void CompanyEditForm_Load(object sender, EventArgs e)
        {
            this.Visibles();

            //this.checkEditIsDefault.Checked = true;
            //this.textEditCompanyName.Text = BL.Settings.CompanyChineseName;
            //this.textEditCompanyEnglishName.Text = BL.Settings.CompanyEnglishName;
            //this.textEditCompanyPrinciple.Text = BL.Settings.CompanyPrinciple;
            //this.textEditCompanyPhone.Text = BL.Settings.CompanyPhone;
            //this.textEditCompanyFax.Text = BL.Settings.CompanyFax;
            //this.textEditCompanyEMail.Text = BL.Settings.CompanyEMail;
            //this.textEditCompanyWebUrl.Text = BL.Settings.CompanyWebUrl;
            //this.textEditCompanyAddress1.Text = BL.Settings.CompanyAddress1;
            //this.textEditCompanyAddress2.Text = BL.Settings.CompanyAddress2;
            //this.memoEditDescription.Text = BL.Settings.CompanyNotes;
            //this.textEditCompanyAddress3.Text = BL.Settings.CompanyAddress3;
            //byte[] image = BL.Settings.PictrueLogo;
            //if (image != null)
            //    this.pictureEditCompanySign.Image = Image.FromStream(new System.IO.MemoryStream(image));
        }

        protected override void Save()
        {
            this._company.CompanyName = this.textEditCompanyName.Text;
            this._company.CompanyEnglishName = this.textEditCompanyEnglishName.Text;
            this._company.CompanyPrinciple = this.textEditCompanyPrinciple.Text;
            this._company.CompanyPhone = this.textEditCompanyPhone.Text;
            this._company.CompanyFax = this.textEditCompanyFax.Text;
            this._company.CompanyEMail = this.textEditCompanyEMail.Text;
            this._company.NoNnite = this.textEditNoNnite.Text;
            this._company.CompanyWebUrl = this.textEditCompanyWebUrl.Text;
            this._company.Description = this.memoEditDescription.Text;
            this._company.CompanyAddress1 = this.textEditCompanyAddress1.Text;
            this._company.CompanyAddress2 = this.textEditCompanyAddress2.Text;
            this._company.CompanyAddress3 = this.textEditCompanyAddress3.Text;
            byte[] piclogo = null;
            if (this.buttonEditCompanySign.Text != "")
            {
                piclogo = System.IO.File.ReadAllBytes(this.buttonEditCompanySign.Text);
                this._company.CompanySign = piclogo;
            }
            else
            {
                if (this._company.CompanySign==null || this._company.CompanySign.Length <= 0)
                    this._company.CompanySign = new byte[] { };
            }
            if (this.checkEditIsDefault.Checked)
            {

                IList<Model.Company> list = this._companyManager.Select();
                foreach (Model.Company company in list)
                {
                    if (company.IsDefault != null)
                    {
                        if (company.IsDefault.Value)
                        {
                            company.IsDefault = false;
                            if (company.CompanySign == null)
                                company.CompanySign = new byte[] { };
                            this._companyManager.Update(company);
                        }
                    }
                }

                #region 设置settings

                BL.Settings.CompanyChineseName = this.textEditCompanyName.Text;
                BL.Settings.CompanyEnglishName = this.textEditCompanyEnglishName.Text;
                BL.Settings.CompanyPrinciple = this.textEditCompanyPrinciple.Text;
                BL.Settings.CompanyPhone = this.textEditCompanyPhone.Text;
                BL.Settings.CompanyFax = this.textEditCompanyFax.Text;
                BL.Settings.CompanyEMail = this.textEditCompanyEMail.Text;
                BL.Settings.CompanyWebUrl = this.textEditCompanyWebUrl.Text;
                BL.Settings.CompanyAddress1 = this.textEditCompanyAddress1.Text;
                BL.Settings.CompanyAddress2 = this.textEditCompanyAddress2.Text;
                BL.Settings.CompanyAddress3 = this.textEditCompanyAddress3.Text;
                BL.Settings.CompanyNotes = this.memoEditDescription.Text;

                if (this.buttonEditCompanySign.Text != "")
                {
                    piclogo = null;
                    piclogo = System.IO.File.ReadAllBytes(this.buttonEditCompanySign.Text);
                    BL.Settings.PictrueLogo = piclogo;
                }
                else
                {
                    if (this._company.CompanySign.Length <= 0)
                        BL.Settings.PictrueLogo = new byte[] { };
                }

                #endregion

                this._company.IsDefault = true;
            }

            switch (this.action)
            {
                case "insert":
                    this._company.CompanyId = Guid.NewGuid().ToString();
                    this._companyManager.Insert(this._company);
                    break;
                case "update":
                    this._companyManager.Update(this._company);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._company == null)
            {
                this._company = new Book.Model.Company();
                this.action = "insert";
            }
            else
            {
                this._company = this._companyManager.Get(this._company.CompanyId);
                if (this._company == null)
                    this._company = new Book.Model.Company();
            }

            this.textEditCompanyName.Text = this._company.CompanyName;
            this.textEditCompanyEnglishName.Text = this._company.CompanyEnglishName;
            this.textEditCompanyPrinciple.Text = this._company.CompanyPrinciple;
            this.textEditCompanyPhone.Text = this._company.CompanyPhone;
            this.textEditCompanyFax.Text = this._company.CompanyFax;
            this.textEditCompanyEMail.Text = this._company.CompanyEMail;
            this.textEditNoNnite.Text = this._company.NoNnite;
            this.textEditCompanyWebUrl.Text = this._company.CompanyWebUrl;
            this.memoEditDescription.Text = this._company.Description;
            this.textEditCompanyAddress1.Text = this._company.CompanyAddress1;
            this.textEditCompanyAddress2.Text = this._company.CompanyAddress2;
            this.textEditCompanyAddress3.Text = this._company.CompanyAddress3;
            if (this._company.CompanySign != null && this._company.CompanySign.Length > 0)
            {
                this.pictureEditCompanySign.Image = Image.FromStream(new System.IO.MemoryStream(this._company.CompanySign));
            }
            else
                this.pictureEditCompanySign.Image = null;
            if (this._company.IsDefault != null && this._company.IsDefault != false)
                this.checkEditIsDefault.Checked = true;
            else
                this.checkEditIsDefault.Checked = false;

            this.bindingSourceCompany.DataSource = this._companyManager.Select();
            base.Refresh();
        }

        protected override void Delete()
        {
            if (this._company == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this._company = this.bindingSourceCompany.Current as Model.Company;
            if (this._company == null) return;
            this._companyManager.Delete(this._company.CompanyId);
        }

        private void buttonEditCompanySign_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            openFileDialogpic.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg|*.gif|*.bmp";
            if (this.openFileDialogpic.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = this.openFileDialogpic.FileName;
                if (System.IO.File.Exists(fileName))
                {
                    this.buttonEditCompanySign.Text = fileName;
                    this.pictureEditCompanySign.Image = Image.FromFile(fileName);
                }
            }
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            this._company = this.bindingSourceCompany.Current as Model.Company;
            if (this._company != null)
            {
                this.action = "view";
                this.Refresh();
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEdit1.Checked)
                this.textEditCompanyAddress2.Text = this.textEditCompanyAddress1.Text;
            else
                this.textEditCompanyAddress2.Text = "";
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEdit2.Checked)
                this.textEditCompanyAddress3.Text = this.textEditCompanyAddress2.Text;
            else
                this.textEditCompanyAddress3.Text = "";
        }
    }
}