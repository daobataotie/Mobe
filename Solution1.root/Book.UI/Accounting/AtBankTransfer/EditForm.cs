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
namespace Book.UI.Accounting.AtBankTransfer
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {

        Model.AtBankTransfer AtBankTransfer;
        BL.AtBankTransferManager AtBankTransferManager = new Book.BL.AtBankTransferManager();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtBankTransfer.PRO_Id, new AA("請輸入轉帳單號..", this.textEditTransferId));
            this.requireValueExceptions.Add(Model.AtBankTransfer.PRO_WithBankId, new AA("請選擇轉出帳戶..", this.newChooseContorlWithBankId));
            this.requireValueExceptions.Add(Model.AtBankTransfer.PRO_IntoBankId, new AA("請選擇轉入帳戶..", this.newChooseContorlIntoBankId));
            this.newChooseContorlWithBankId.Choose = new Accounting.AtBankAccount.ChooseAtBankAccount();
            this.newChooseContorlIntoBankId.Choose = new Accounting.AtBankAccount.ChooseAtBankAccount();
        }
        #region Override
        protected override void AddNew()
        {
            this.AtBankTransfer = new Model.AtBankTransfer();
        }
        protected override void Save()
        {
            this.AtBankTransfer.WithBank = this.newChooseContorlWithBankId.EditValue as Model.AtBankAccount;
            if (this.AtBankTransfer.WithBank != null)
            {
                this.AtBankTransfer.WithBankId = this.AtBankTransfer.WithBank.BankAccountId;
            }
            this.AtBankTransfer.IntoBank = this.newChooseContorlIntoBankId.EditValue as Model.AtBankAccount;
            if (this.AtBankTransfer.IntoBank != null)
            {
                this.AtBankTransfer.IntoBankId = this.AtBankTransfer.IntoBank.BankAccountId;
            }
            this.AtBankTransfer.Id = this.textEditTransferId.Text;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditTransferDate.DateTime, new DateTime()))
            {
                this.AtBankTransfer.TransferDate = null;
            }
            else
            {
                this.AtBankTransfer.TransferDate = this.dateEditTransferDate.DateTime;
            }
            this.AtBankTransfer.WithMoney = this.spinEditWithMoney.EditValue == null ? 0 : decimal.Parse(this.spinEditWithMoney.EditValue.ToString());
            switch (this.action)
            {
                case "insert":
                    this.AtBankTransferManager.Insert(this.AtBankTransfer);
                    break;
                case "update":
                    this.AtBankTransferManager.Update(this.AtBankTransfer);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {

            if (this.AtBankTransfer == null)
            {
                this.AtBankTransfer = new Book.Model.AtBankTransfer();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = this.AtBankTransferManager.Select();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AtBankTransfer.TransferDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditTransferDate.EditValue = null;
            }
            else
            {
                this.dateEditTransferDate.EditValue = this.AtBankTransfer.TransferDate;
            }
            this.newChooseContorlWithBankId.EditValue = this.AtBankTransfer.WithBank;
            this.newChooseContorlIntoBankId.EditValue = this.AtBankTransfer.IntoBank;
            this.textEditTransferId.Text = this.AtBankTransfer.Id;

            this.spinEditWithMoney.EditValue = this.AtBankTransfer.WithMoney;
            switch (this.action)
            {
                case "insert":
                    this.dateEditTransferDate.Properties.ReadOnly = false;
                    this.dateEditTransferDate.Properties.Buttons[0].Visible = true;
                    this.newChooseContorlWithBankId.ShowButton = true;
                    this.newChooseContorlWithBankId.ButtonReadOnly = false;
                    this.newChooseContorlIntoBankId.ShowButton = true;
                    this.newChooseContorlIntoBankId.ButtonReadOnly = false;

                    this.textEditTransferId.Properties.ReadOnly = false;
                    this.spinEditWithMoney.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.dateEditTransferDate.Properties.ReadOnly = false;
                    this.dateEditTransferDate.Properties.Buttons[0].Visible = true;
                    this.newChooseContorlWithBankId.ShowButton = true;
                    this.newChooseContorlWithBankId.ButtonReadOnly = false;
                    this.newChooseContorlIntoBankId.ShowButton = true;
                    this.newChooseContorlIntoBankId.ButtonReadOnly = false;

                    this.textEditTransferId.Properties.ReadOnly = false;
                    this.spinEditWithMoney.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.dateEditTransferDate.Properties.ReadOnly = true;
                    this.dateEditTransferDate.Properties.Buttons[0].Visible = false;
                    this.newChooseContorlWithBankId.ShowButton = false;
                    this.newChooseContorlWithBankId.ButtonReadOnly = true;
                    this.newChooseContorlIntoBankId.ShowButton = false;
                    this.newChooseContorlIntoBankId.ButtonReadOnly = true;

                    this.textEditTransferId.Properties.ReadOnly = true;
                    this.spinEditWithMoney.Properties.ReadOnly = true;
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
            if (this.AtBankTransfer == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.AtBankTransferManager.Delete(this.AtBankTransfer.TransferId);
                this.AtBankTransfer = this.AtBankTransferManager.GetNext(this.AtBankTransfer);
                if (this.AtBankTransfer == null)
                {
                    this.AtBankTransfer = this.AtBankTransferManager.GetLast();
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
            this.AtBankTransfer = this.AtBankTransferManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.AtBankTransfer AtBankTransfer = this.AtBankTransferManager.GetPrev(this.AtBankTransfer);
            if (AtBankTransfer == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtBankTransfer = AtBankTransfer;
        }
        protected override void MoveLast()
        {
            this.AtBankTransfer = this.AtBankTransferManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.AtBankTransfer AtBankTransfer = this.AtBankTransferManager.GetNext(this.AtBankTransfer);
            if (AtBankTransfer == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtBankTransfer = AtBankTransfer;
        }
        protected override bool HasRows()
        {
            return this.AtBankTransferManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.AtBankTransferManager.HasRowsAfter(this.AtBankTransfer);
        }
        protected override bool HasRowsPrev()
        {
            return this.AtBankTransferManager.HasRowsBefore(this.AtBankTransfer);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.dateEditTransferDate, this.textEditTransferId, this });
        }
        #endregion

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AtBankTransfer productEpiboly = this.bindingSource1.Current as Model.AtBankTransfer;
                if (productEpiboly != null)
                {
                    this.AtBankTransfer = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {

        }
    }
}