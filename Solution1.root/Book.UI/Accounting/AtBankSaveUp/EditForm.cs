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
namespace Book.UI.Accounting.AtBankSaveUp
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.AtBankSaveUp AtBankSaveUp;
        BL.AtBankSaveUpManager AtBankSaveUpManager = new Book.BL.AtBankSaveUpManager();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtBankSaveUp.PRO_Id, new AA("請輸入存提編號..", this.textEditSaveUpId));
            this.requireValueExceptions.Add(Model.AtBankSaveUp.PRO_BankId, new AA("請選擇銀行帳戶..", this.newChooseContorlBankId));
            this.requireValueExceptions.Add(Model.AtBankSaveUp.PRO_SaveUpCategory, new AA("存提類別不能為空！..", this.comboBoxEditSaveUpCategory));
            this.newChooseContorlBankId.Choose = new Accounting.AtBankAccount.ChooseAtBankAccount();
        }
        #region Override
        protected override void AddNew()
        {
            this.AtBankSaveUp = new Model.AtBankSaveUp();
        }
        protected override void Save()
        {
            this.AtBankSaveUp.Bank = this.newChooseContorlBankId.EditValue as Model.AtBankAccount;
            if (this.AtBankSaveUp.Bank != null)
            {
                this.AtBankSaveUp.BankId = this.AtBankSaveUp.Bank.BankAccountId;
            }
            this.AtBankSaveUp.Id = this.textEditSaveUpId.Text;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditSaveUpdate.DateTime, new DateTime()))
            {
                this.AtBankSaveUp.SaveUpdate = null;
            }
            else
            {
                this.AtBankSaveUp.SaveUpdate = this.dateEditSaveUpdate.DateTime;
            }
            this.AtBankSaveUp.SaveUpCategory = this.comboBoxEditSaveUpCategory.EditValue == null ? null : this.comboBoxEditSaveUpCategory.EditValue.ToString();
            this.AtBankSaveUp.SaveUpMoney = this.spinEditSaveUpMoney.EditValue == null ? 0 : decimal.Parse(this.spinEditSaveUpMoney.EditValue.ToString());
            this.AtBankSaveUp.Mark = this.memoEditMark.Text;

            switch (this.action)
            {
                case "insert":
                    this.AtBankSaveUpManager.Insert(this.AtBankSaveUp);
                    break;
                case "update":
                    this.AtBankSaveUpManager.Update(this.AtBankSaveUp);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {

            if (this.AtBankSaveUp == null)
            {
                this.AtBankSaveUp = new Book.Model.AtBankSaveUp();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = this.AtBankSaveUpManager.Select();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AtBankSaveUp.SaveUpdate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditSaveUpdate.EditValue = null;
            }
            else
            {
                this.dateEditSaveUpdate.EditValue = this.AtBankSaveUp.SaveUpdate;
            }
            this.newChooseContorlBankId.EditValue = this.AtBankSaveUp.Bank;
            this.textEditSaveUpId.Text = this.AtBankSaveUp.Id;
            this.comboBoxEditSaveUpCategory.Text = this.AtBankSaveUp.SaveUpCategory;
            this.spinEditSaveUpMoney.EditValue = this.AtBankSaveUp.SaveUpMoney;
            this.memoEditMark.Text = this.AtBankSaveUp.Mark;
            switch (this.action)
            {
                case "insert":
                    this.dateEditSaveUpdate.Properties.ReadOnly = false;
                    this.dateEditSaveUpdate.Properties.Buttons[0].Visible = true;
                    this.newChooseContorlBankId.ShowButton = true;
                    this.newChooseContorlBankId.ButtonReadOnly = false;
                    this.textEditSaveUpId.Properties.ReadOnly=false;
                    this.comboBoxEditSaveUpCategory.Properties.ReadOnly = false;
                    this.spinEditSaveUpMoney.Properties.ReadOnly = false;
                    this.memoEditMark.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.dateEditSaveUpdate.Properties.ReadOnly = false;
                    this.dateEditSaveUpdate.Properties.Buttons[0].Visible = true;
                    this.newChooseContorlBankId.ShowButton = true;
                    this.newChooseContorlBankId.ButtonReadOnly = false;
                    this.textEditSaveUpId.Properties.ReadOnly = false;
                    this.comboBoxEditSaveUpCategory.Properties.ReadOnly = false;
                    this.spinEditSaveUpMoney.Properties.ReadOnly = false;
                    this.memoEditMark.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.dateEditSaveUpdate.Properties.ReadOnly = true;
                    this.dateEditSaveUpdate.Properties.Buttons[0].Visible = false;
                    this.newChooseContorlBankId.ShowButton = false;
                    this.newChooseContorlBankId.ButtonReadOnly = true;
                    this.textEditSaveUpId.Properties.ReadOnly = true;
                    this.comboBoxEditSaveUpCategory.Properties.ReadOnly = true;
                    this.spinEditSaveUpMoney.Properties.ReadOnly = true;
                    this.memoEditMark.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        /// <summary>
        /// 删除
        /// </summary>
        protected override void Delete()
        {
            if (this.AtBankSaveUp == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.AtBankSaveUpManager.Delete(this.AtBankSaveUp.SaveUpId);
                this.AtBankSaveUp = this.AtBankSaveUpManager.GetNext(this.AtBankSaveUp);
                if (this.AtBankSaveUp == null)
                {
                    this.AtBankSaveUp = this.AtBankSaveUpManager.GetLast();
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
            this.AtBankSaveUp = this.AtBankSaveUpManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.AtBankSaveUp AtBankSaveUp = this.AtBankSaveUpManager.GetPrev(this.AtBankSaveUp);
            if (AtBankSaveUp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtBankSaveUp = AtBankSaveUp;
        }
        protected override void MoveLast()
        {
            this.AtBankSaveUp = this.AtBankSaveUpManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.AtBankSaveUp AtBankSaveUp = this.AtBankSaveUpManager.GetNext(this.AtBankSaveUp);
            if (AtBankSaveUp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtBankSaveUp = AtBankSaveUp;
        }
        protected override bool HasRows()
        {
            return this.AtBankSaveUpManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.AtBankSaveUpManager.HasRowsAfter(this.AtBankSaveUp);
        }
        protected override bool HasRowsPrev()
        {
            return this.AtBankSaveUpManager.HasRowsBefore(this.AtBankSaveUp);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.dateEditSaveUpdate, this.textEditSaveUpId,this });
        }
        #endregion

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AtBankSaveUp productEpiboly = this.bindingSource1.Current as Model.AtBankSaveUp;
                if (productEpiboly != null)
                {
                    this.AtBankSaveUp = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}