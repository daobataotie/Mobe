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

namespace Book.UI.Accounting.ProfitLoss
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        private BL.AtProfitLossManager AtProfitLossManager = new Book.BL.AtProfitLossManager();
        private Model.AtProfitLoss AtProfitLoss = new Book.Model.AtProfitLoss();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtProfitLoss.PRO_ProfitLossCategory, new AA("請輸入損益類型..", this.textEditCategory));
            this.invalidValueExceptions.Add(Model.AtProfitLoss.PRO_SubjectId, new AA("請選擇會計科目..", this.newChooseContorl1));
            this.requireValueExceptions.Add(Model.AtProfitLoss.PRO_CategoriesName, new AA("請大類名稱..", this.lookUpEdit1));
            this.action = "insert";
            this.bindingSource2.DataSource = new BL.AtAccountingCategoriesManager().Select();
            this.newChooseContorl1.Choose = new AtAccountSubject.ChooseAccountSubject();
        }
        public EditForm(Model.AtProfitLoss AtProfitLoss)
            : this()
        {
            this.AtProfitLoss = AtProfitLoss;
            this.action = "update";
        }
        public EditForm(Model.AtProfitLoss AtProfitLoss, string action)
            : this()
        {
            this.AtProfitLoss = AtProfitLoss;
            this.action = action;
        }
        protected override void Save()
        {
            this.AtProfitLoss.ProfitLossCategory = this.textEditCategory.Text;
            this.AtProfitLoss.CategoriesName= this.lookUpEdit1.EditValue==null ? null :this.lookUpEdit1.EditValue.ToString();
            this.AtProfitLoss.Subject = this.newChooseContorl1.EditValue as Model.AtAccountSubject;
            if (AtProfitLoss.Subject != null)
            {
                this.AtProfitLoss.SubjectId = this.AtProfitLoss.Subject.SubjectId;
                this.AtProfitLoss.SubjectName = this.AtProfitLoss.Subject.SubjectName;
            }
            this.AtProfitLoss.ThisMoney = this.spinEditThisMoney.EditValue == null ? 0 : double.Parse(this.spinEditThisMoney.EditValue.ToString());
            this.AtProfitLoss.IsMoney = this.spinEditIsMoney.EditValue == null ? 0 : double.Parse(this.spinEditIsMoney.EditValue.ToString());
            this.AtProfitLoss.Ratio = this.spinEditRatio.EditValue == null ? 0 : double.Parse(this.spinEditRatio.EditValue.ToString());
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditDate.DateTime, new DateTime()))
            {
                this.AtProfitLoss.ProfitLossDate = null;
            }
            else
            {
                this.AtProfitLoss.ProfitLossDate = this.dateEditDate.DateTime;
            }

            switch (this.action)
            {
                case "insert":
                    this.AtProfitLossManager.Insert(this.AtProfitLoss);
                    break;
                case "update":
                    this.AtProfitLossManager.Update(this.AtProfitLoss);
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
                return this.AtProfitLoss;
            }
        }

        #endregion

        #region Virtual Method

        protected override void Delete()
        {
            if (this.AtProfitLoss == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.AtProfitLossManager.Delete(this.AtProfitLoss.ProfitLossId);
            this.AtProfitLoss = this.AtProfitLossManager.GetNext(this.AtProfitLoss);
            if (this.AtProfitLoss == null)
            {
                this.AtProfitLoss = this.AtProfitLossManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.AtProfitLoss = new Model.AtProfitLoss();
            //this.AtProfitLoss.CustomInspectionRuleId = this.AtProfitLossManager.GetId();
        }

        protected override void Undo()
        {
        }

        protected override void MovePrev()
        {
            Model.AtProfitLoss AtProfitLoss = this.AtProfitLossManager.GetPrev(this.AtProfitLoss);
            if (AtProfitLoss == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtProfitLoss = AtProfitLoss;

        }

        protected override void MoveNext()
        {
            Model.AtProfitLoss AtProfitLoss = this.AtProfitLossManager.GetNext(this.AtProfitLoss);
            if (AtProfitLoss == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.AtProfitLoss = AtProfitLoss;
        }

        protected override void MoveFirst()
        {
            this.AtProfitLoss = this.AtProfitLossManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.AtProfitLoss == null)
                this.AtProfitLoss = this.AtProfitLossManager.GetLast();
        }
        public override void Refresh()
        {

            if (this.AtProfitLoss == null)
            {
                this.AtProfitLoss = new Book.Model.AtProfitLoss();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = AtProfitLossManager.Select();
            this.textEditCategory.Text = this.AtProfitLoss.ProfitLossCategory;
            this.lookUpEdit1.EditValue = this.AtProfitLoss.CategoriesName;
            this.newChooseContorl1.EditValue = this.AtProfitLoss.Subject;
            this.spinEditThisMoney.EditValue = this.AtProfitLoss.ThisMoney;
            this.spinEditIsMoney.EditValue = this.AtProfitLoss.IsMoney;
            this.spinEditRatio.EditValue = this.AtProfitLoss.Ratio;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AtProfitLoss.ProfitLossDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditDate.EditValue = null;
            }
            else
            {
                this.dateEditDate.EditValue = this.AtProfitLoss.ProfitLossDate;
            }
            switch (this.action)
            {
                case "insert":
                    this.textEditCategory.Properties.ReadOnly = false;
                    this.lookUpEdit1.Properties.ReadOnly = false;
                    this.newChooseContorl1.ShowButton = true;
                    this.newChooseContorl1.ButtonReadOnly = false;
                    this.spinEditThisMoney.Properties.ReadOnly = false;
                     this.spinEditIsMoney.Properties.ReadOnly = false;
                     this.spinEditRatio.Properties.ReadOnly = false;
                      this.dateEditDate.Properties.ReadOnly = false;
                      this.dateEditDate.Properties.Buttons[0].Visible = true;
                    break;

                case "update":

                     this.textEditCategory.Properties.ReadOnly = false;
                    this.lookUpEdit1.Properties.ReadOnly = false;
                    this.newChooseContorl1.ShowButton = true;
                    this.newChooseContorl1.ButtonReadOnly = false;
                    this.spinEditThisMoney.Properties.ReadOnly = false;
                     this.spinEditIsMoney.Properties.ReadOnly = false;
                     this.spinEditRatio.Properties.ReadOnly = false;
                      this.dateEditDate.Properties.ReadOnly = false;
                      this.dateEditDate.Properties.Buttons[0].Visible = true;
                    break;

                case "view":

                    this.textEditCategory.Properties.ReadOnly = true;
                    this.lookUpEdit1.Properties.ReadOnly = true;
                    this.newChooseContorl1.ShowButton = false;
                    this.newChooseContorl1.ButtonReadOnly = true;
                    this.spinEditThisMoney.Properties.ReadOnly = true;
                    this.spinEditIsMoney.Properties.ReadOnly = true;
                    this.spinEditRatio.Properties.ReadOnly = true;
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
            return this.AtProfitLossManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.AtProfitLossManager.HasRowsBefore(this.AtProfitLoss);
        }

        protected override bool HasRowsNext()
        {
            return this.AtProfitLossManager.HasRowsAfter(this.AtProfitLoss);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditCategory});

        }
        #endregion

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AtProfitLoss productEpiboly = this.bindingSource1.Current as Model.AtProfitLoss;
                if (productEpiboly != null)
                {
                    this.AtProfitLoss = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}