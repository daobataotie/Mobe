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

namespace Book.UI.Accounting.PropertyDebt
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        private BL.AtPropertyDebtManager AtPropertyDebtManager = new Book.BL.AtPropertyDebtManager();
        private Model.AtPropertyDebt AtPropertyDebt = new Book.Model.AtPropertyDebt();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtPropertyDebt.PRO_CategoryName, new AA("請輸入類別名稱..", this.textEditCategoryName));
            this.invalidValueExceptions.Add(Model.AtPropertyDebt.PRO_SubjectId, new AA("請選擇會計科目..", this.newChooseContorl1));
            this.requireValueExceptions.Add(Model.AtPropertyDebt.PRO_CategoriesName, new AA("請大類名稱..", this.lookUpEdit1));
            this.action = "insert";
            this.bindingSource2.DataSource = new BL.AtAccountingCategoriesManager().Select();
            this.newChooseContorl1.Choose = new AtAccountSubject.ChooseAccountSubject();
        }
        public EditForm(Model.AtPropertyDebt AtPropertyDebt)
            : this()
        {
            this.AtPropertyDebt = AtPropertyDebt;
            this.action = "update";
        }
        public EditForm(Model.AtPropertyDebt AtPropertyDebt, string action)
            : this()
        {
            this.AtPropertyDebt = AtPropertyDebt;
            this.action = action;
        }
        protected override void Save()
        {
            this.AtPropertyDebt.CategoryName = this.textEditCategoryName.Text;
            this.AtPropertyDebt.CategoriesName = this.lookUpEdit1.EditValue == null ? null : this.lookUpEdit1.EditValue.ToString();
            this.AtPropertyDebt.Subject = this.newChooseContorl1.EditValue as Model.AtAccountSubject;
            if (AtPropertyDebt.Subject != null)
            {
                this.AtPropertyDebt.SubjectId = this.AtPropertyDebt.Subject.SubjectId;
                this.AtPropertyDebt.SubjectName = this.AtPropertyDebt.Subject.SubjectName;
            }
            this.AtPropertyDebt.IsMoney = this.spinEditIsMoney.EditValue == null ? 0 :decimal.Parse(this.spinEditIsMoney.EditValue.ToString());
            this.AtPropertyDebt.AddMoney = this.spinEditAddMoney.EditValue == null ? 0 : decimal.Parse(this.spinEditAddMoney.EditValue.ToString());
            //this.AtPropertyDebt.Ratio = this.spinEditRatio.EditValue == null ? 0 : double.Parse(this.spinEditRatio.EditValue.ToString());
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditDate.DateTime, new DateTime()))
            {
                this.AtPropertyDebt.PropertyDebtDate = null;
            }
            else
            {
                this.AtPropertyDebt.PropertyDebtDate = this.dateEditDate.DateTime;
            }

            switch (this.action)
            {
                case "insert":
                    this.AtPropertyDebtManager.Insert(this.AtPropertyDebt);
                    break;
                case "update":
                    this.AtPropertyDebtManager.Update(this.AtPropertyDebt);
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
                return this.AtPropertyDebt;
            }
        }

        #endregion

        #region Virtual Method

        protected override void Delete()
        {
            if (this.AtPropertyDebt == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.AtPropertyDebtManager.Delete(this.AtPropertyDebt.PropertyDebtId);
            this.AtPropertyDebt = this.AtPropertyDebtManager.GetNext(this.AtPropertyDebt);
            if (this.AtPropertyDebt == null)
            {
                this.AtPropertyDebt = this.AtPropertyDebtManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.AtPropertyDebt = new Model.AtPropertyDebt();
            //this.AtPropertyDebt.CustomInspectionRuleId = this.AtPropertyDebtManager.GetId();
        }

        protected override void Undo()
        {
        }

        protected override void MovePrev()
        {
            Model.AtPropertyDebt AtPropertyDebt = this.AtPropertyDebtManager.GetPrev(this.AtPropertyDebt);
            if (AtPropertyDebt == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtPropertyDebt = AtPropertyDebt;

        }

        protected override void MoveNext()
        {
            Model.AtPropertyDebt AtPropertyDebt = this.AtPropertyDebtManager.GetNext(this.AtPropertyDebt);
            if (AtPropertyDebt == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.AtPropertyDebt = AtPropertyDebt;
        }

        protected override void MoveFirst()
        {
            this.AtPropertyDebt = this.AtPropertyDebtManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.AtPropertyDebt == null)
                this.AtPropertyDebt = this.AtPropertyDebtManager.GetLast();
        }
        public override void Refresh()
        {

            if (this.AtPropertyDebt == null)
            {
                this.AtPropertyDebt = new Book.Model.AtPropertyDebt();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = AtPropertyDebtManager.Select();
            this.textEditCategoryName.Text = this.AtPropertyDebt.CategoryName;
            this.lookUpEdit1.Text = this.AtPropertyDebt.CategoriesName;
            this.newChooseContorl1.EditValue = this.AtPropertyDebt.Subject;
            this.spinEditAddMoney.EditValue = this.AtPropertyDebt.AddMoney;
            this.spinEditIsMoney.EditValue = this.AtPropertyDebt.IsMoney;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AtPropertyDebt.PropertyDebtDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditDate.EditValue = null;
            }
            else
            {
                this.dateEditDate.EditValue = this.AtPropertyDebt.PropertyDebtDate;
            }
            switch (this.action)
            {
                case "insert":
                    this.textEditCategoryName.Properties.ReadOnly = false;
                    this.lookUpEdit1.Properties.ReadOnly = false;
                    this.newChooseContorl1.ShowButton = true;
                    this.newChooseContorl1.ButtonReadOnly = false;
                    this.spinEditAddMoney.Properties.ReadOnly = false;
                    this.spinEditIsMoney.Properties.ReadOnly = false;
                    this.dateEditDate.Properties.ReadOnly = false;
                    this.dateEditDate.Properties.Buttons[0].Visible = true;
                    break;

                case "update":

                    this.textEditCategoryName.Properties.ReadOnly = false;
                    this.lookUpEdit1.Properties.ReadOnly = false;
                    this.newChooseContorl1.ShowButton = true;
                    this.newChooseContorl1.ButtonReadOnly = false;
                    this.spinEditAddMoney.Properties.ReadOnly = false;
                    this.spinEditIsMoney.Properties.ReadOnly = false;
                    this.dateEditDate.Properties.ReadOnly = false;
                    this.dateEditDate.Properties.Buttons[0].Visible = true;
                    break;

                case "view":

                    this.textEditCategoryName.Properties.ReadOnly = true;
                    this.lookUpEdit1.Properties.ReadOnly = true;
                    this.newChooseContorl1.ShowButton = false;
                    this.newChooseContorl1.ButtonReadOnly = true;
                    this.spinEditAddMoney.Properties.ReadOnly = true;
                    this.spinEditIsMoney.Properties.ReadOnly = true;
                    this.dateEditDate.Properties.ReadOnly = true;
                    this.dateEditDate.Properties.Buttons[0].Visible = false;
                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.AtPropertyDebtManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.AtPropertyDebtManager.HasRowsBefore(this.AtPropertyDebt);
        }

        protected override bool HasRowsNext()
        {
            return this.AtPropertyDebtManager.HasRowsAfter(this.AtPropertyDebt);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditCategoryName });

        }
        #endregion

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AtPropertyDebt productEpiboly = this.bindingSource1.Current as Model.AtPropertyDebt;
                if (productEpiboly != null)
                {
                    this.AtPropertyDebt = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

    }
}