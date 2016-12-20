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

namespace Book.UI.Accounting.AtProperty
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        private BL.AtPropertyManager atPropertyManager = new Book.BL.AtPropertyManager();
        private Model.AtProperty atProperty = new Book.Model.AtProperty();
        public EditForm()
        {
            InitializeComponent(); 
            this.requireValueExceptions.Add(Model.AtProperty.PRO_Id, new AA("請輸入財產編號..", this.textEditPropertyId));
            this.invalidValueExceptions.Add(Model.AtProperty.PRO_PropertyName, new AA("請輸入財產名稱..", this.textEditPropertyName));
            this.action = "insert";
            this.bindingSource1.DataSource = new BL.AtAccountSubjectManager().Select();
        }
        public EditForm(Model.AtProperty atProperty)
            : this()
        {
            this.atProperty = atProperty;
            this.action = "update";
        }
        public EditForm(Model.AtProperty atProperty, string action)
            : this()
        { 
            this.atProperty = atProperty;
            this.action = action;
        }
        protected override void Save()
        {
            this.atProperty.Id = this.textEditPropertyId.Text;
            this.atProperty.PropertyName = this.textEditPropertyName.Text;
            this.atProperty.Specifications = this.textEditSpecifications.Text;
            this.atProperty.Position = this.textEditPosition.Text;
            this.atProperty.Quantity = this.spinEditQuantity.EditValue == null ? 0 : float.Parse(this.spinEditQuantity.EditValue.ToString());
            this.atProperty.Unit = this.textEditUnit.Text;
            this.atProperty.DurableMonths = this.spinEditDurableMonths.EditValue == null ? 0 : int.Parse(this.spinEditDurableMonths.EditValue.ToString());
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditToDate.DateTime, new DateTime()))
            {
                this.atProperty.ToDate = null;
            }
            else
            {
                this.atProperty.ToDate = this.dateEditToDate.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditDepreciationDate.DateTime, new DateTime()))
            {
                this.atProperty.DepreciationDate = null;
            }
            else
            {
                this.atProperty.DepreciationDate = this.dateEditDepreciationDate.DateTime;
            }
            this.atProperty.ReserveValue = this.spinEditReserveValue.EditValue == null ? 0 : float.Parse(this.spinEditReserveValue.EditValue.ToString());
            this.atProperty.ObtainRegular = this.spinEditObtainRegular.EditValue == null ? 0 : decimal.Parse(this.spinEditObtainRegular.EditValue.ToString());
            this.atProperty.Often = this.comboBoxEditOften.EditValue == null ? null : this.comboBoxEditOften.EditValue.ToString();
            this.atProperty.DepreciationMoney = this.spinEditDepreciationMoney.EditValue == null ? 0 : decimal.Parse(this.spinEditDepreciationMoney.EditValue.ToString());
            this.atProperty.DepreciationSubject = this.lookUpEditDepreciationSubject.EditValue == null ? null : this.lookUpEditDepreciationSubject.EditValue.ToString();
            this.atProperty.RespectiveSubject = this.lookUpEditRespectiveSubject.EditValue == null ? null : this.lookUpEditRespectiveSubject.EditValue.ToString();
            this.atProperty.CostSubject = this.lookUpEditCostSubject.EditValue == null ? null : this.lookUpEditCostSubject.EditValue.ToString();
            this.atProperty.Mark = this.memoEditMark.Text;
            switch (this.action)
            {
                case "insert":
                    this.atPropertyManager.Insert(this.atProperty);
                    break;
                case "update":
                    this.atPropertyManager.Update(this.atProperty);
                    break;
                default:
                    break;
            }
        }
        #region Properties

        public override object EditedItem
        {
            get
            {
                return this.atProperty;
            }
        }

        #endregion

        #region Virtual Method

        protected override void Delete()
        {
            if (this.atProperty == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.atPropertyManager.Delete(this.atProperty.PropertyId);
            this.atProperty = this.atPropertyManager.GetNext(this.atProperty);
            if (this.atProperty == null)
            {
                this.atProperty = this.atPropertyManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.atProperty = new Model.AtProperty();
            this.atProperty.Id = this.atPropertyManager.GetId();
        }

        protected override void Undo()
        {
        }

        protected override void MovePrev()
        {
            Model.AtProperty atProperty = this.atPropertyManager.GetPrev(this.atProperty);
            if (atProperty == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.atProperty = atProperty;

        }

        protected override void MoveNext()
        {
            Model.AtProperty atProperty = this.atPropertyManager.GetNext(this.atProperty);
            if (atProperty == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.atProperty = atProperty;
        }

        protected override void MoveFirst()
        {
            this.atProperty = this.atPropertyManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.atProperty == null)
                this.atProperty = this.atPropertyManager.GetLast();
        }
        public override void Refresh()
        {

            if (this.atProperty == null)
            {
                this.atProperty = new Book.Model.AtProperty();
                this.action = "insert";
            }
            this.textEditPropertyId.Text=this.atProperty.Id;
              this.textEditPropertyName.Text=this.atProperty.PropertyName;
             this.textEditSpecifications.Text=this.atProperty.Specifications ;
              this.textEditPosition.Text=this.atProperty.Position;
              this.spinEditQuantity.EditValue = this.atProperty.Quantity;
            this.textEditUnit.Text= this.atProperty.Unit ;
            this.spinEditDurableMonths.EditValue = this.atProperty.DurableMonths;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.atProperty.ToDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditToDate.EditValue = null;
            }
            else
            {
                this.dateEditToDate.EditValue = this.atProperty.ToDate;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.atProperty.DepreciationDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditDepreciationDate.EditValue = null;
            }
            else
            {
                this.dateEditDepreciationDate.EditValue = this.atProperty.DepreciationDate;
            }
             this.spinEditReserveValue.EditValue =this.atProperty.ReserveValue;
             this.spinEditObtainRegular.EditValue = this.atProperty.ObtainRegular;
             this.comboBoxEditOften.EditValue = this.atProperty.Often;
             this.spinEditDepreciationMoney.EditValue = this.atProperty.DepreciationMoney;
             this.lookUpEditDepreciationSubject.EditValue = this.atProperty.DepreciationSubject;
             this.lookUpEditRespectiveSubject.EditValue = this.atProperty.RespectiveSubject;
             this.lookUpEditCostSubject.EditValue = this.atProperty.CostSubject;
             this.memoEditMark.Text = this.atProperty.Mark;
            switch (this.action)
            {
                case "insert":
                    this.textEditPropertyId.Properties.ReadOnly = false;
                    this.textEditPropertyName.Properties.ReadOnly = false;
                    this.textEditSpecifications.Properties.ReadOnly = false;
                    this.textEditPosition.Properties.ReadOnly = false;
                    this.spinEditQuantity.Properties.ReadOnly = false;
                    this.textEditUnit.Properties.ReadOnly = false;
                    this.spinEditDurableMonths.Properties.ReadOnly = false;
                    this.dateEditDepreciationDate.Properties.ReadOnly = false;
                    this.dateEditDepreciationDate.Properties.Buttons[0].Visible = true;
                    this.dateEditToDate.Properties.ReadOnly = false;
                    this.dateEditToDate.Properties.Buttons[0].Visible = true;

                    this.spinEditReserveValue.Properties.ReadOnly = false;
                    this.spinEditObtainRegular.Properties.ReadOnly = false;
                    this.comboBoxEditOften.Properties.ReadOnly = false;
                    this.spinEditDepreciationMoney.Properties.ReadOnly = false;
                    this.lookUpEditDepreciationSubject.Properties.ReadOnly = false;
                    this.lookUpEditRespectiveSubject.Properties.ReadOnly = false;
                    this.lookUpEditCostSubject.Properties.ReadOnly = false;
                    this.memoEditMark.Properties.ReadOnly = false;
                    break;

                case "update":

                    this.textEditPropertyId.Properties.ReadOnly = false;
                    this.textEditPropertyName.Properties.ReadOnly = false;
                    this.textEditSpecifications.Properties.ReadOnly = false;
                    this.textEditPosition.Properties.ReadOnly = false;
                    this.spinEditQuantity.Properties.ReadOnly = false;
                    this.textEditUnit.Properties.ReadOnly = false;
                    this.spinEditDurableMonths.Properties.ReadOnly = false;
                    this.dateEditDepreciationDate.Properties.ReadOnly = false;
                    this.dateEditDepreciationDate.Properties.Buttons[0].Visible = true;
                    this.dateEditToDate.Properties.ReadOnly = false;
                    this.dateEditToDate.Properties.Buttons[0].Visible = true;

                    this.spinEditReserveValue.Properties.ReadOnly = false;
                    this.spinEditObtainRegular.Properties.ReadOnly = false;
                    this.comboBoxEditOften.Properties.ReadOnly = false;
                    this.spinEditDepreciationMoney.Properties.ReadOnly = false;
                    this.lookUpEditDepreciationSubject.Properties.ReadOnly = false;
                    this.lookUpEditRespectiveSubject.Properties.ReadOnly = false;
                    this.lookUpEditCostSubject.Properties.ReadOnly = false;
                    this.memoEditMark.Properties.ReadOnly = false;
                    break;

                case "view":

                    this.textEditPropertyId.Properties.ReadOnly = true;
                    this.textEditPropertyName.Properties.ReadOnly = true;
                    this.textEditSpecifications.Properties.ReadOnly = true;
                    this.textEditPosition.Properties.ReadOnly = true;
                    this.spinEditQuantity.Properties.ReadOnly = true;
                    this.textEditUnit.Properties.ReadOnly = true;
                    this.spinEditDurableMonths.Properties.ReadOnly = true;
                    this.dateEditDepreciationDate.Properties.ReadOnly = true;
                    this.dateEditDepreciationDate.Properties.Buttons[0].Visible = false;
                    this.dateEditToDate.Properties.ReadOnly = true;
                    this.dateEditToDate.Properties.Buttons[0].Visible = false;

                    this.spinEditReserveValue.Properties.ReadOnly = true;
                    this.spinEditObtainRegular.Properties.ReadOnly = true;
                    this.comboBoxEditOften.Properties.ReadOnly = true;
                    this.spinEditDepreciationMoney.Properties.ReadOnly = true;
                    this.lookUpEditDepreciationSubject.Properties.ReadOnly = true;
                    this.lookUpEditRespectiveSubject.Properties.ReadOnly = true;
                    this.lookUpEditCostSubject.Properties.ReadOnly = true;
                    this.memoEditMark.Properties.ReadOnly = true;
                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.atPropertyManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.atPropertyManager.HasRowsBefore(this.atProperty);
        }

        protected override bool HasRowsNext()
        {
            return this.atPropertyManager.HasRowsAfter(this.atProperty);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditPropertyId });

        }
        #endregion
    }
}