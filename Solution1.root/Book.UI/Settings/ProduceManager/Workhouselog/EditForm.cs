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

namespace Book.UI.Settings.ProduceManager.Workhouselog
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-11-16
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
       Model.Workhouselog workHouselog;
        BL.WorkhouselogManager workHouselogManager = new Book.BL.WorkhouselogManager();

        #region 构造函数
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.Workhouselog.PROPERTY_WORKHOUSEID, new AA(Properties.Resources.RequireChooseWorkHouse, this.newChooseWorkHouseId));
            this.action = "insert";
            this.newChooseWorkHouseId.Choose = new ChooseWorkHouse();
        }
        public EditForm(Model.Workhouselog workHouselog)
        {
            this.workHouselog = workHouselog;
            this.action = "update";
        }
        public EditForm(Model.Workhouselog workHouselog, string action)
        {
            this.workHouselog = workHouselog;
            this.action = action;
        }
        #endregion


        #region 重写父类方法
        protected override void AddNew()
        {
            this.workHouselog = new Model.Workhouselog();
        }

        protected override void Save()
        {
            if (this.newChooseWorkHouseId.EditValue != null)
            {
                this.workHouselog.WorkHouse = this.newChooseWorkHouseId.EditValue as Model.WorkHouse;
                this.workHouselog.WorkHouseId = (this.newChooseWorkHouseId.EditValue as Model.WorkHouse).WorkHouseId;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateWorkhouselogdate.DateTime, new DateTime()))
            {
                this.workHouselog.Workhouselogdate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.workHouselog.Workhouselogdate = this.dateWorkhouselogdate.DateTime;
            }
            this.workHouselog.Workhouselogcontent = this.textWorkhouselogcontent.Text;
            switch (this.action)
            {
                case "insert":
                    this.workHouselogManager.Insert(this.workHouselog);
                    break;
                case "update":
                    this.workHouselogManager.Update(this.workHouselog);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {

            if (this.workHouselog == null)
            {
                this.workHouselog = new Book.Model.Workhouselog();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = this.workHouselogManager.Select();
            if (this.workHouselog.WorkHouse != null)
            {
                this.newChooseWorkHouseId.EditValue = this.workHouselog.WorkHouse as Model.WorkHouse;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.workHouselog.Workhouselogdate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateWorkhouselogdate.EditValue = null;
            }
            else
            {
                this.dateWorkhouselogdate.EditValue = this.workHouselog.Workhouselogdate;
            }
            this.textWorkhouselogcontent.Text = this.workHouselog.Workhouselogcontent;
            switch (this.action)
            {
                case "insert":

                    this.newChooseWorkHouseId.ShowButton = true;
                    this.newChooseWorkHouseId.ButtonReadOnly = false;
                    this.textWorkhouselogcontent.Properties.ReadOnly = false;
                    this.dateWorkhouselogdate.Properties.ReadOnly=false;
                    break;

                case "update":
                    this.newChooseWorkHouseId.ShowButton = true;
                    this.newChooseWorkHouseId.ButtonReadOnly = false;
                    this.textWorkhouselogcontent.Properties.ReadOnly = false;
                    this.dateWorkhouselogdate.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.newChooseWorkHouseId.ShowButton = true;
                    this.newChooseWorkHouseId.ButtonReadOnly = false;
                    this.textWorkhouselogcontent.Properties.ReadOnly = true;
                    this.dateWorkhouselogdate.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        protected override void Delete()
        {
            if (this.workHouselog == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.workHouselogManager.Delete(this.workHouselog.WorkhouselogID);
                this.workHouselog = this.workHouselogManager.GetNext(this.workHouselog);
                if (this.workHouselog == null)
                {
                    this.workHouselog = this.workHouselogManager.GetLast();
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
            this.workHouselog = this.workHouselogManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.Workhouselog workHouselog = this.workHouselogManager.GetPrev(this.workHouselog);
            if (workHouselog == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.workHouselog = workHouselog;
        }
        protected override void MoveLast()
        {
            this.workHouselog = this.workHouselogManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.Workhouselog workHouselog = this.workHouselogManager.GetNext(this.workHouselog);
            if (workHouselog == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.workHouselog = workHouselog;
        }
        protected override bool HasRows()
        {
            return this.workHouselogManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.workHouselogManager.HasRowsAfter(this.workHouselog);
        }
        protected override bool HasRowsPrev()
        {
            return this.workHouselogManager.HasRowsBefore(this.workHouselog);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textWorkhouselogcontent });
        }
        #endregion


        //gridview单击事件
        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.Workhouselog workHouselog = this.bindingSource1.Current as Model.Workhouselog;
                if (workHouselog != null)
                {
                    this.workHouselog = workHouselog;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}