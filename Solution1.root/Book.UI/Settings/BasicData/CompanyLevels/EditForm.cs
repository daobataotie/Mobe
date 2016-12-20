using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.CompanyLevels
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 客戶級別設置
   // 文 件 名：EditForm
   // 编 码 人: 马艳军                   完成时间:2009-09-23
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {

        #region myj---定義變量對象
        private BL.CompanyLevelManager companyLevelManager = new Book.BL.CompanyLevelManager();
        private Model.CompanyLevel companyLevel;
        #endregion

        #region myj---無慘構造函數
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions = new Dictionary<string, AA>();

            this.requireValueExceptions.Add(Model.CompanyLevel.PROPERTY_COMPANYLEVELID, new AA(Properties.Resources.RequireDataForId, this.CompanyLevelIdTextEdit));
            this.requireValueExceptions.Add(Model.CompanyLevel.PROPERTY_COMPANYLEVELNAME, new AA(Properties.Resources.RequireDataForName, this.CompanyLevelNameTextEdit));

            this.invalidValueExceptions.Add(Model.CompanyLevel.PROPERTY_COMPANYLEVELID, new AA(Properties.Resources.EntityExists, this.CompanyLevelIdTextEdit));
            this.action = "insert";
        }
        #endregion

        #region myj---一個參數(model實體對象)的構造函數
        public EditForm(Model.CompanyLevel level) : this() 
        {
            //if (level == null)
            //    throw new ArithmeticException();
           // oldId =level.CompanyLevelId;
            this.companyLevel = level;
            this.action = "update";
        }
        #endregion

        #region myj---兩個參數(model實體對象和當前的動作)的構造函數
        public EditForm(Model.CompanyLevel level,string action)
            : this()
        {
            //if (level == null)
            //    throw new ArithmeticException();
            this.companyLevel = level;
            this.action = action;
        }
        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {
           
        }       

        #region Override

        public override object EditedItem
        {
            get
            {
                return this.companyLevel;
            }
        }

        protected override void Delete()
        {
            if (this.companyLevel == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.companyLevelManager.Delete(this.companyLevel);
            this.companyLevel = this.companyLevelManager.GetNext(this.companyLevel);
            if (this.companyLevel == null)
            {
                this.companyLevel = this.companyLevelManager.GetLast();
            }
        }

        protected override void Save()
        {
            this.companyLevel.CompanyLevelId = this.CompanyLevelIdTextEdit.Text;
            this.companyLevel.CompanyLevelName = this.CompanyLevelNameTextEdit.Text;
          
            switch (this.action)
            {
                case "insert":
                    this.companyLevelManager.Insert(this.companyLevel);
                    break;
                case "update":                    
                    this.companyLevelManager.Update(this.companyLevel);
                    break;
            }
        }

        #endregion

        #region myj---重載基類的新增方法
        protected override void AddNew()
        {
            this.companyLevel = new Model.CompanyLevel();
        }
        #endregion

        #region myj---重載基類的方法
        protected override void MoveNext()
        {
            Model.CompanyLevel companyLevel = this.companyLevelManager.GetNext(this.companyLevel);
            if (companyLevel == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.companyLevel = companyLevel;
        }

        protected override void MovePrev()
        {
            Model.CompanyLevel companyLevel = this.companyLevelManager.GetPrev(this.companyLevel);
            if (companyLevel == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.companyLevel = companyLevel;
        }

        protected override void MoveFirst()
        {
            this.companyLevel = this.companyLevelManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this.companyLevel = this.companyLevelManager.GetLast();
        }

        public override void Refresh()
        {
            if (this.companyLevel == null)
            {
                this.companyLevel = new Book.Model.CompanyLevel();
                this.action = "insert";
            }

            this.CompanyLevelIdTextEdit.EditValue = this.companyLevel.CompanyLevelId;
            this.CompanyLevelNameTextEdit.EditValue = this.companyLevel.CompanyLevelName;
            switch (this.action)
            {
                case "insert":
                    this.CompanyLevelIdTextEdit.Properties.ReadOnly = false;
                    this.CompanyLevelNameTextEdit.Properties.ReadOnly = false;
                    break;

                case "update":

                    this.CompanyLevelIdTextEdit.Properties.ReadOnly = false;
                    this.CompanyLevelNameTextEdit.Properties.ReadOnly = false;

                    break;

                case "view":

                    this.CompanyLevelIdTextEdit.Properties.ReadOnly = true;
                    this.CompanyLevelNameTextEdit.Properties.ReadOnly = true;
                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.companyLevelManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.companyLevelManager.HasRowsAfter(this.companyLevel);
        }

        protected override bool HasRowsPrev()
        {
            return this.companyLevelManager.HasRowsBefore(this.companyLevel);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.CompanyLevelIdTextEdit, this });
        }
        #endregion
    }
}