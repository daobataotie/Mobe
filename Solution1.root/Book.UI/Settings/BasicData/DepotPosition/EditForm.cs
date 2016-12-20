using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.DepotPosition
{

    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 貨位設置
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-16
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        //業務對象設置
        protected BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private Model.DepotPosition depotPosition;

        #region 構造函數
        //無慘
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.DepotPosition.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.textEditId));
            this.requireValueExceptions.Add(Model.DepotPosition.PROPERTY_DEPOTID, new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepot));
            this.buttonEditDepot.Choose = new Book.UI.Invoices.ChooseDepot();
            this.action = "insert";
        }
        /// <param name="position">model 對象</param>
        /// <param name="action">當前動作</param>
        public EditForm(Book.Model.DepotPosition position, string action)
            : this()
        {
            this.depotPosition = position;
            this.action = action;
        }
        /// <param name="position">model 對象</param>
        public EditForm(Book.Model.DepotPosition position)
            : this()
        {
            this.depotPosition = position;            
            this.action = "update";
        }
        #endregion

        #region Override

        protected override void AddNew()
        {
            this.depotPosition = new Book.Model.DepotPosition();
        }

        protected override void Delete()
        {
            if (this.depotPosition == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.depotPositionManager.Delete(this.depotPosition.DepotPositionId);
            }
            catch (Helper.InvalidValueException ex)
            {
                if (this.invalidValueExceptions.ContainsKey(ex.Message))
                {
                    AA a = this.invalidValueExceptions[ex.Message];
                    MessageBox.Show(a.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                throw;
            }
            this.depotPosition = this.depotPositionManager.GetNext(this.depotPosition);
            if (this.depotPosition == null)
            {
                this.depotPosition = this.depotPositionManager.GetLast();
            }
        }

        protected override bool HasRows()
        {
            return this.depotPositionManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.depotPositionManager.HasRowsAfter(this.depotPosition);
        }

        protected override bool HasRowsPrev()
        {
            return this.depotPositionManager.HasRowsBefore(this.depotPosition);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditId, this.buttonEditDepot, this.memoEditDescription, this });
        }

        protected override void MoveFirst()
        {
            this.depotPosition = this.depotPositionManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this.depotPosition = this.depotPositionManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.DepotPosition position = this.depotPositionManager.GetNext(this.depotPosition);
            if (position == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.depotPosition = position;
        }

        protected override void MovePrev()
        {
            Model.DepotPosition position = this.depotPositionManager.GetPrev(this.depotPosition);
            if (position == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.depotPosition = position;
        }

        public override void Refresh()
        {
            if (this.depotPosition == null)
            {
                this.depotPosition = new Book.Model.DepotPosition();
                this.action = "insert";

                this.textEditId.Text = string.IsNullOrEmpty(this.depotPosition.Id) ? this.depotPosition.DepotPositionId : this.depotPosition.Id;
                this.memoEditDescription.Text = this.depotPosition.DepotPositionDescription;
                this.buttonEditDepot.EditValue = this.depotPosition.Depot;

                switch (this.action)
                {
                    case "insert":
                        this.textEditId.Properties.ReadOnly = false;
                        this.memoEditDescription.Properties.ReadOnly = false;
                        this.buttonEditDepot.ShowButton = true;
                        this.buttonEditDepot.ButtonReadOnly = false;
                        break;
                    case "update":
                        this.textEditId.Properties.ReadOnly = false;
                        this.memoEditDescription.Properties.ReadOnly = false;
                        this.buttonEditDepot.ShowButton = true;
                        this.buttonEditDepot.ButtonReadOnly = false;
                        break;
                    case "view":                        
                        this.textEditId.Properties.ReadOnly = true;
                        this.memoEditDescription.Properties.ReadOnly = true;
                        this.buttonEditDepot.ShowButton = false;
                        this.buttonEditDepot.ButtonReadOnly = true;
                        break;
                    default:
                        break;
                }
                base.Refresh();
            }
        }

        protected override void Save()
        {
            this.depotPosition.Id = this.textEditId.Text;
            this.depotPosition.DepotPositionDescription = this.memoEditDescription.Text;
            this.depotPosition.Depot = this.buttonEditDepot.EditValue as Model.Depot;
            switch (this.action)
            {
                case "insert":
                    this.depotPositionManager.Insert(this.depotPosition);
                    break;
                case "update":
                    this.depotPositionManager.Update(this.depotPosition);
                    break;
            }
        }
        #endregion
    }
}