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

namespace Book.UI.Settings.BasicData.AcademicBackGround
{
    /*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸陽飞驰软件科技有限公司
    //                     版權所有 圍著必究
    // 文 件 名：EditForm
    // 功能描述：學歷設置(繼承了BaseEditForm類,統一了風格)
    // 编 码 人: 马艳军                   完成时间:2009-09-09
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/
    public partial class EditForm :BaseEditForm
    {

        #region 變量對象定義
        Model.AcademicBackGround academic;
        BL.AcademicBackGroundManager academickManager = new Book.BL.AcademicBackGroundManager();
        #endregion

        #region 無參數構造函數
        public EditForm()
        {            
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AcademicBackGround.PROPERTY_ACADEMICBACKGROUNDNAME, new AA(Properties.Resources.RequireDataForNames, this.textEditName));
            this.action = "insert";
        }
        #endregion

        #region 有一個model的參數的構造函數
        /// <summary>
        ///  函數名稱EditForm
        /// </summary>
        /// <param name="academic">學歷</param>
        public EditForm(Model.AcademicBackGround academic)
        {
            this.academic = academic;
            this.action = "update";
        }
        #endregion

        #region 帶有兩個參數的構造函數
        /// <summary>
        /// 函數名稱EditForm
        /// </summary>
        /// <param name="academic">學歷對象</param>
        /// <param name="action">動作</param>
        public EditForm(Model.AcademicBackGround academic, string action)
        {
            this.academic = academic;
            this.action = action;

        }
        #endregion

        #region 重載添加的方法
        protected override void AddNew()
        {
            this.academic = new Model.AcademicBackGround();            
        }
        protected override void Save()
        {           
            this.academic.AcademicBackGroundId = this.academic.AcademicBackGroundId;
            this.academic.AcademicBackGroundName = textEditName.Text.ToString();
            this.academic.Description = textEditDescription.Text.ToString();
            switch (this.action)
            {
                case "insert":
                    this.academickManager.Insert(this.academic);
                    break;
                case "update":
                    this.academickManager.Update(this.academic);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {           
            if (this.academic == null)
            {
                this.academic = new Book.Model.AcademicBackGround();
                this.action = "insert";
            }
            this.bindingSourceAcademic.DataSource = this.academickManager.Select();
            this.textEditName.Text = this.academic.AcademicBackGroundName;
            this.textEditDescription.Text = this.academic.Description;

            switch (this.action)
            {
                case "insert":
                    this.textEditName.Properties.ReadOnly = false;
                    this.textEditDescription.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.textEditName.Properties.ReadOnly = false;
                    this.textEditDescription.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.textEditName.Properties.ReadOnly = true;
                    this.textEditDescription.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        protected override void Delete()
        {
            if (this.academic == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.academickManager.Delete(this.academic.AcademicBackGroundId);
                this.academic = this.academickManager.GetNext(this.academic);
                if (this.academic == null)
                {
                    this.academic = this.academickManager.GetLast();
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
            this.academic = this.academickManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.AcademicBackGround academic = this.academickManager.GetPrev(this.academic);
            if (academic == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.academic = academic;
        }
        protected override void MoveLast()
        {
            this.academic = this.academickManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.AcademicBackGround academic = this.academickManager.GetNext(this.academic);
            if (academic == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.academic = academic;
        }
        protected override bool HasRows()
        {
            return this.academickManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.academickManager.HasRowsAfter(this.academic);
        }
        protected override bool HasRowsPrev()
        {
            return this.academickManager.HasRowsBefore(this.academic);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditName, this.textEditDescription });
        }
        #endregion

        #region gridview1的click點擊事件
        private void gridView1_Click_1(object sender, EventArgs e)
        {

            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AcademicBackGround academic = this.bindingSourceAcademic.Current as Model.AcademicBackGround;
                if (academic != null)
                {
                    this.academic = academic;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
        #endregion
    }
}