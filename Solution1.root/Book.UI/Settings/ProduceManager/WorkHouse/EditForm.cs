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

namespace Book.UI.Settings.ProduceManager.WorkHouse
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-11-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.WorkHouse workHouse;
        BL.WorkHouseManager workHouseManager = new Book.BL.WorkHouseManager();

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.WorkHouse.PROPERTY_WORKHOUSECODE, new AA(Properties.Resources.NumsIsNotNull, this.textWorkhouseCode));
            this.requireValueExceptions.Add(Model.WorkHouse.PROPERTY_WORKHOUSENAME, new AA(Properties.Resources.RequireDataForNames, this.textWorkhousename));

            this.bindingSource1.DataSource = this.workHouseManager.Select();
            this.action = "view";
        }

        public EditForm(Model.WorkHouse workHouse)
            :this()
        {
            this.workHouse = workHouse;
            this.action = "update";
        }

        public EditForm(Model.WorkHouse workHouse, string action)
            :this()
        {
            this.workHouse = workHouse;
            this.action = action;
        }

        protected override void AddNew()
        {
            this.workHouse = new Model.WorkHouse();
            this.workHouse.WorkhouseCode = workHouseManager.GetId();
        }

        protected override void Save()
        {
            this.workHouse.Workhousename = this.textWorkhousename.Text;
            this.workHouse.Workhouseaddress = this.textWorkhouseaddress.Text;
            this.workHouse.WorkhouseCode = this.textWorkhouseCode.Text;

            switch (this.action)
            {
                case "insert":
                    this.workHouseManager.Insert(this.workHouse);
                    break;
                case "update":
                    this.workHouseManager.Update(this.workHouse);
                    break;
                default:
                    break;
            }

            this.bindingSource1.DataSource = this.workHouseManager.Select();
        }

        public override void Refresh()
        {
            if (this.workHouse == null)
            {
                this.AddNew();
                this.action = "insert";
            }

            this.textWorkhouseCode.Text = this.workHouse.WorkhouseCode;
            this.textWorkhousename.Text = this.workHouse.Workhousename;
            this.textWorkhouseaddress.Text = this.workHouse.Workhouseaddress;

            #region switch
            //switch (this.action)
            //{
            //    case "insert":
            //        this.textWorkhousename.Properties.ReadOnly = false;
            //        this.textWorkhouseaddress.Properties.ReadOnly = false;
            //        //this.textWorkhouseCode.Properties.ReadOnly = false;
            //        break;
            //    case "update":
            //        this.textWorkhousename.Properties.ReadOnly = false;
            //        this.textWorkhouseaddress.Properties.ReadOnly = false;
            //        //this.textWorkhouseCode.Properties.ReadOnly = false;
            //        break;

            //    case "view":
            //        this.textWorkhousename.Properties.ReadOnly = true;
            //        this.textWorkhouseaddress.Properties.ReadOnly = true;
            //        //this.textWorkhouseCode.Properties.ReadOnly = true;
            //        break;
            //    default:
            //        break;
            //}
            #endregion

            base.Refresh();

            this.textWorkhouseCode.Properties.ReadOnly = true;
        }

        protected override void Delete()
        {
            if (this.workHouse == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.workHouseManager.Delete(this.workHouse.WorkHouseId);
                this.workHouse = this.workHouseManager.GetNext(this.workHouse);
                if (this.workHouse == null)
                {
                    this.workHouse = this.workHouseManager.GetLast();
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
            this.workHouse = this.workHouseManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this.workHouse = this.workHouseManager.GetLast();
        }

        protected override void MovePrev()
        {
            Model.WorkHouse workHouse = this.workHouseManager.GetPrev(this.workHouse);
            if (workHouse == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.workHouse = workHouse;
        }

        protected override void MoveNext()
        {
            Model.WorkHouse workHouse = this.workHouseManager.GetNext(this.workHouse);
            if (workHouse == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.workHouse = workHouse;
        }

        protected override bool HasRows()
        {
            return this.workHouseManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.workHouseManager.HasRowsAfter(this.workHouse);
        }

        protected override bool HasRowsPrev()
        {
            return this.workHouseManager.HasRowsBefore(this.workHouse);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textWorkhousename });
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.WorkHouse workHouse = this.bindingSource1.Current as Model.WorkHouse;
                if (workHouse != null)
                {
                    this.workHouse = workHouse;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}