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

namespace Book.UI.Settings.BasicData.ProductUnit
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-11-05
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        private Model.ProductUnit _productUnit;

        private BL.ProductUnitManager productUnitManager = new Book.BL.ProductUnitManager();
        private Model.UnitGroup unitGroup;
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProductUnit.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.textEditId));
            this.requireValueExceptions.Add(Model.ProductUnit.PROPERTY_CNNAME, new AA(Properties.Resources.RequireDataForNames, this.textEditCnName));

            this.requireValueExceptions.Add(Model.ProductUnit.PROPERTY_UNITGROUPID, new AA("請選擇單位組", this.newChooseContorlUnitGroupId));


            this.invalidValueExceptions.Add(Model.ProductUnit.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textEditId));

            this.invalidValueExceptions.Add(Model.ProductUnit.PROPERTY_CNNAME, new AA(Properties.Resources.EntityName, this.textEditCnName));

            this.newChooseContorlUnitGroupId.Choose = new Book.UI.Settings.BasicData.UnitGroup.ChooseUnitGroup();
            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
            this.action = "insert";
        }
        public EditForm(Model.UnitGroup unitGroup)
            : this()
        {
            this.unitGroup = unitGroup;
        }
        public EditForm(Model.ProductUnit productunit)
            : this()
        {
            this._productUnit = productunit;
            this.action = "update";
        }
        public EditForm(Model.ProductUnit productunit, string action)
            : this()
        {
            this._productUnit = productunit;
            this.action = action;
        }

        #region Override

        protected override void AddNew()
        {
            this._productUnit = new Model.ProductUnit();
            this._productUnit.UnitGroup = this.unitGroup;
        }

        protected override void Delete()
        {
            if (this._productUnit == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.productUnitManager.Delete(this._productUnit.ProductUnitId);
                this._productUnit = this.productUnitManager.GetNext(this._productUnit);
                if (this._productUnit == null)
                {
                    this._productUnit = this.productUnitManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override bool HasRows()
        {
            return this.productUnitManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.productUnitManager.HasRowsAfter(this._productUnit);
        }

        protected override bool HasRowsPrev()
        {
            return this.productUnitManager.HasRowsBefore(this._productUnit);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditCnName, this.textEditId });
        }

        protected override void MoveFirst()
        {
            this._productUnit = this.productUnitManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._productUnit = this.productUnitManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.ProductUnit ProductUnit = this.productUnitManager.GetNext(this._productUnit);
            if (ProductUnit == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._productUnit = ProductUnit;
        }

        protected override void MovePrev()
        {
            Model.ProductUnit ProductUnit = this.productUnitManager.GetPrev(this._productUnit);
            if (ProductUnit == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._productUnit = ProductUnit;
        }


        public override void Refresh()
        {
            if (this._productUnit == null)
            {
                this.AddNew();
                this.action = "insert";
            }
           
            this.textEditId.Text = string.IsNullOrEmpty(this._productUnit.Id) ?"": this._productUnit.Id;
            this.textEditCnName.Text = this._productUnit.CnName;
            this.spinEditConvertRate.EditValue = this._productUnit.ConvertRate;
            this.newChooseContorlUnitGroupId.EditValue = this._productUnit.UnitGroup;
            this.checkEditIsMainUnit.EditValue = this._productUnit.IsMainUnit;

            switch (this.action)
            {
                case "insert":
                    this.textEditCnName.Properties.ReadOnly = false;
                    this.textEditId.Properties.ReadOnly = false;
                    this.spinEditConvertRate.Properties.ReadOnly = false;
                    this.newChooseContorlUnitGroupId.ButtonReadOnly = false;
                    this.checkEditIsMainUnit.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.textEditCnName.Properties.ReadOnly = false;
                    this.textEditId.Properties.ReadOnly = false;
                    this.spinEditConvertRate.Properties.ReadOnly = false;
                    this.newChooseContorlUnitGroupId.ButtonReadOnly = false;
                    this.checkEditIsMainUnit.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.textEditCnName.Properties.ReadOnly = true;
                    this.textEditId.Properties.ReadOnly = true;
                    this.spinEditConvertRate.Properties.ReadOnly = true;
                    this.newChooseContorlUnitGroupId.ButtonReadOnly = true;
                    this.checkEditIsMainUnit.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }

        protected override void Save()
        {
            this._productUnit.Id = this.textEditId.Text;
            this._productUnit.CnName = this.textEditCnName.Text;
            this._productUnit.ConvertRate = double.Parse(this.spinEditConvertRate.Value.ToString());
            this._productUnit.UkName = "uName";
            this._productUnit.UkNames = "uNames";
            this._productUnit.UnitBarCode = "barcode";
            this._productUnit.UnitGroup = this.newChooseContorlUnitGroupId.EditValue as Model.UnitGroup;
            this._productUnit.IsMainUnit = this.checkEditIsMainUnit.Checked;

            switch (this.action)
            {
                case "insert":
                    this.productUnitManager.Insert(this._productUnit);
                    break;
                case "update":
                    this.productUnitManager.Update(this._productUnit);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}