using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
namespace Book.UI.Settings.BasicData.Company
{

    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 公司信息設置
   // 文 件 名：EditForm
   // 编 码 人: 马艳军                   完成时间:2009-09-19
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        #region myj---變量對象定義
        Model.Company company;
        Book.BL.CompanyManager companyManager = new Book.BL.CompanyManager();
        #endregion

        #region myj---無慘構造函數
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.Company.PROPERTY_COMPANYNAME, new AA(Properties.Resources.RequireDataForNames, this.textEditName));
            this.action = "insert";
        }
        #endregion

        #region myj---一個參數(model對象)的構造函數
        public EditForm(Model.Company company)
        {
            this.company = company;
            this.action = "update";
        }
        #endregion

        #region myj---兩個參數的構造函數(model對象和當前動作)
        public EditForm(Model.Company company, string action)
        {
            this.company = company;
            this.action = action;
        }
        #endregion

        #region Override
        protected override void AddNew()
        {
            this.company = new Model.Company();
        }
        protected override void Save()
        {
            this.company.CompanyName = textEditName.Text.ToString();
            this.company.Description = textEditDescription.Text.ToString();
            byte[] comsign = new byte[] { };
            if (this.buttonEdit1.Text != "")
            {
                comsign = System.IO.File.ReadAllBytes(this.buttonEdit1.Text);
            }
            this.company.CompanySign = comsign;

            switch (this.action)
            {
                case "insert":
                    this.companyManager.Insert(this.company);
                    break;
                case "update":
                    this.companyManager.Update(this.company);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {
            if (this.company == null)
            {
                this.company = new Book.Model.Company();
                this.action = "insert";
            }
            this.bindingSourceCompany.DataSource = this.companyManager.Select();
            this.textEditName.Text = this.company.CompanyName;
            this.textEditDescription.Text = this.company.Description;
            byte[] image = this.company.CompanySign;
            if (image != null && this.company.CompanySign.Length > 0)
                this.CompanySign.Image = Image.FromStream(new System.IO.MemoryStream(image));
            else
                this.CompanySign.Image = null;
            base.Refresh(); 
        }
        protected override void Delete()
        {
            if (this.company == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.companyManager.Delete(this.company.CompanyId);
                this.company = this.companyManager.GetNext(this.company);
                if (this.company == null)
                {
                    this.company = this.companyManager.GetLast();
                }
            }
            catch
            {
                throw new Exception("");
            }

            return;

        }
        protected override void MoveFirst()
        {
            this.company = this.companyManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.Company company = this.companyManager.GetPrev(this.company);
            if (company == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.company = company;
        }
        protected override void MoveLast()
        {
            this.company = this.companyManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.Company company = this.companyManager.GetNext(this.company);
            if (bindingSourceCompany == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.company = company;
        }
        protected override bool HasRows()
        {
            return this.companyManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.companyManager.HasRowsAfter(this.company);
        }
        protected override bool HasRowsPrev()
        {
            return this.companyManager.HasRowsBefore(this.company);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditDescription, this.textEditDescription });
        }
        #endregion

        #region myj---gridview 的click點解事件
        private void gridView1_Click_1(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.Company company = this.bindingSourceCompany.Current as Model.Company;
                if (bindingSourceCompany != null)
                {
                    this.company = company;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
        #endregion

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.openpicture.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp";
            if (this.openpicture.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = this.openpicture.FileName;
                if (System.IO.File.Exists(fileName))
                {
                    this.buttonEdit1.Text = fileName;
                    this.CompanySign.Image = Image.FromFile(fileName);
                }
                else
                {
                    MessageBox.Show(this, "fileNotFound", "文件不存在！", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.Visibles();
        }
    }
}