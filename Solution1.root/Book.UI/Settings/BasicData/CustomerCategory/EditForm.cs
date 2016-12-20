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

namespace Book.UI.Settings.BasicData.CustomerCategory
{

    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 客戶分類設置
   // 文 件 名：EditForm
   // 编 码 人: 马艳军                   完成时间:2009-10-09
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm :BaseEditForm
    {

        #region myj---變量對象定義
        protected BL.CustomerCategoryManager customerCategoryManager = new Book.BL.CustomerCategoryManager();
        private Model.CustomerCategory customerCategory = null;
        #endregion

        #region myj---無慘構造函數
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.CustomerCategory.PROPERTY_ID, new AA("請輸入編號。",this.textEditId));
            this.requireValueExceptions.Add(Model.CustomerCategory.PROPERTY_CUSTOMERCATEGORYNAME, new AA(Properties.Resources.RequireDataForNames, this.CustomerCategoryNameTextEdit));

            this.invalidValueExceptions.Add(Model.CustomerCategory.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textEditId));
            
            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
            this.action = "insert";
        }
        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {
        }

        #region myj---帶參數的構造函數
        /// <summary>
        /// 一個參數(model 對象)
        /// </summary>
        /// <param name="custom">model對象</param>
        public EditForm(Model.CustomerCategory custom)
            : this()
        {
            this.customerCategory = custom;
            this.action = "update";
        }
        /// <summary>
        /// 兩個參數
        /// </summary>
        /// <param name="custom">model對象</param>
        /// <param name="action">當前呢動作</param>
        public EditForm(Model.CustomerCategory custom, string action)
            : this()
        {
            this.customerCategory = custom;
            this.action = action;
        }
        #endregion 

        #region myj----重載父類的方法
        protected override void Save()
        {
            this.customerCategory.Id= this.textEditId.Text;
            this.customerCategory.CustomerCategoryName = this.CustomerCategoryNameTextEdit.Text;
            this.customerCategory.CustomerCategoryDescription = this.CustomerCategorydetailTextEdit.Text;
            switch (this.action)
            {
                case "insert":
                    this.customerCategoryManager.Insert(this.customerCategory);
                    break;
                case "update":
                    this.customerCategoryManager.Update(this.customerCategory);
                    break;
                default:
                    break;
            }
        }
        #endregion 



        #region Properties

        public override object EditedItem
        {
            get
            {
                return this.customerCategory;
            }
        }

        #endregion

        #region Virtual Method

        protected override void Delete()
        {
            if (this.customerCategory == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.customerCategoryManager.Delete(this.customerCategory);
            this.customerCategory = this.customerCategoryManager.GetNext(this.customerCategory);
            if (this.customerCategory == null)
            {
                this.customerCategory = this.customerCategoryManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.customerCategory = new Model.CustomerCategory();            
        }

        protected override void Undo()
        {
        }

        protected override void MovePrev()
        {
            Model.CustomerCategory customerCategory = this.customerCategoryManager.GetPrev(this.customerCategory);
            if (customerCategory == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.customerCategory = customerCategory;

        }

        protected override void MoveNext()
        {
            Model.CustomerCategory customerCategory = this.customerCategoryManager.GetNext(this.customerCategory);
            if (customerCategory == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.customerCategory = customerCategory;
        }

        protected override void MoveFirst()
        {
            this.customerCategory = this.customerCategoryManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this.customerCategory = this.customerCategoryManager.GetLast();
        }
        public override void Refresh()
        {

            if (this.customerCategory == null)
            {
                this.customerCategory = new Book.Model.CustomerCategory();
                this.action = "insert";
            }
            bindingSource1.DataSource = new BL.CustomerCategoryManager().Select() ;

            this.textEditId.Text = string.IsNullOrEmpty(this.customerCategory.Id) ? this.customerCategory.CustomerCategoryId : this.customerCategory.Id;
            this.CustomerCategoryNameTextEdit.Text = this.customerCategory.CustomerCategoryName;
            this.CustomerCategorydetailTextEdit.Text = this.customerCategory.CustomerCategoryDescription;
            switch (this.action)
            {
                case "insert":
                    this.textEditId.Properties.ReadOnly = false;
                    this.CustomerCategoryNameTextEdit.Properties.ReadOnly = false;
                    this.CustomerCategorydetailTextEdit.Properties.ReadOnly = false;
                    break;

                case "update":

                    this.textEditId.Properties.ReadOnly = false;
                    this.CustomerCategoryNameTextEdit.Properties.ReadOnly = false;
                    this.CustomerCategorydetailTextEdit.Properties.ReadOnly = false;
                    break;

                case "view":

                    this.textEditId.Properties.ReadOnly = true;
                    this.CustomerCategoryNameTextEdit.Properties.ReadOnly = true;
                    this.CustomerCategorydetailTextEdit.Properties.ReadOnly = true;

                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.customerCategoryManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.customerCategoryManager.HasRowsBefore(this.customerCategory);
        }

        protected override bool HasRowsNext()
        {
            return this.customerCategoryManager.HasRowsAfter(this.customerCategory);
        }

        protected override void IMECtrl()
        {
         //   Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.customerCategoryRuleNamedTextEdit, this });

        }
        #endregion

        private void EditForm_Load_1(object sender, EventArgs e)
        {


        }

        #region myj---gridview 點擊事件
        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.CustomerCategory customerCategory = this.bindingSource1.Current as Model.CustomerCategory;
                if (customerCategory != null)
                {
                    this.customerCategory = customerCategory;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
        #endregion
    }
}
