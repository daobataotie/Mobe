using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtBankAccount
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        private BL.AtBankAccountManager AtBankAccountManager = new Book.BL.AtBankAccountManager();
        private Model.AtBankAccount AtBankAccount = new Book.Model.AtBankAccount();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtBankAccount.PRO_Id, new AA("請輸入帳戶編號..", this.textEditBankAccountId));
            this.invalidValueExceptions.Add(Model.AtBankAccount.PRO_BankAccountName, new AA("請選擇帳戶類別..", this.comboBoxEditAccountCategory));
            this.requireValueExceptions.Add(Model.AtBankAccount.PRO_AccountCategory, new AA("請輸入帳戶名稱..", this.textEditBankAccountName));
            this.invalidValueExceptions.Add(Model.AtBankAccount.PRO_BankId, new AA("請選擇銀行..", this.newChooseContorlBankId));
            this.action = "insert";
            this.bindingSource1.DataSource = new BL.AtAccountSubjectManager().Select();
            this.newChooseContorlBankId.Choose = new Settings.BasicData.Bank.ChooseBank();
        }
        public EditForm(Model.AtBankAccount AtBankAccount)
            : this()
        {
            this.AtBankAccount = AtBankAccount;
            this.action = "update";
        }
        public EditForm(Model.AtBankAccount AtBankAccount, string action)
            : this()
        {
            this.AtBankAccount = AtBankAccount;
            this.action = action;
        }
        protected override void Save()
        {
            this.AtBankAccount.Id = this.textEditBankAccountId.Text;
            this.AtBankAccount.BankAccountName = this.textEditBankAccountName.Text;
            this.AtBankAccount.AccountCategory = this.comboBoxEditAccountCategory.EditValue==null?null:this.comboBoxEditAccountCategory.EditValue.ToString();
            this.AtBankAccount.Bank = this.newChooseContorlBankId.EditValue as Model.Bank;
            if (AtBankAccount.Bank != null)
            {
                this.AtBankAccount.BankId = this.AtBankAccount.Bank.BankId;
            }
            this.AtBankAccount.Contact = this.textEditContact.Text;
            this.AtBankAccount.ContactPhone = this.textEditContactPhone.Text;
            this.AtBankAccount.Fax = this.textEditFax.Text;
            this.AtBankAccount.VotesAgainst = this.spinEditVotesAgainst.EditValue == null ? 0 :decimal.Parse(this.spinEditVotesAgainst.EditValue.ToString());
            this.AtBankAccount.VotesAgainstAfew = this.spinEditVotesAgainstAfew.EditValue == null ? 0 :double.Parse(this.spinEditVotesAgainstAfew.EditValue.ToString());
            this.AtBankAccount.SecurityBalance = this.spinEditSecurityBalance.EditValue == null ? 0 : decimal.Parse(this.spinEditSecurityBalance.EditValue.ToString());
            this.AtBankAccount.TheirBalance = this.spinEditTheirBalance.EditValue == null ? 0 : decimal.Parse(this.spinEditTheirBalance.EditValue.ToString());
            this.AtBankAccount.ExistingBalance = this.spinEditExistingBalance.EditValue == null ? 0 : decimal.Parse(this.spinEditExistingBalance.EditValue.ToString());
            this.AtBankAccount.DepositSubject = this.lookUpEditDepositSubject.EditValue == null ? null : this.lookUpEditDepositSubject.EditValue.ToString();
            this.AtBankAccount.VoteSubject = this.lookUpEditVoteSubject.EditValue == null ? null : this.lookUpEditVoteSubject.EditValue.ToString();
            this.AtBankAccount.BorrowingSubject = this.lookUpEditBorrowingSubject.EditValue == null ? null : this.lookUpEditBorrowingSubject.EditValue.ToString();
            this.AtBankAccount.Mark = this.memoEditMark.Text;

            switch (this.action)
            {
                case "insert":
                    this.AtBankAccountManager.Insert(this.AtBankAccount);
                    break;
                case "update":
                    this.AtBankAccountManager.Update(this.AtBankAccount);
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
                return this.AtBankAccount;
            }
        }

        #endregion

        #region Virtual Method

        protected override void Delete()
        {
            if (this.AtBankAccount == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.AtBankAccountManager.Delete(this.AtBankAccount.BankAccountId);
            this.AtBankAccount = this.AtBankAccountManager.GetNext(this.AtBankAccount);
            if (this.AtBankAccount == null)
            {
                this.AtBankAccount = this.AtBankAccountManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.AtBankAccount = new Model.AtBankAccount();
            this.AtBankAccount.Id = this.AtBankAccountManager.GetId();
        }

        protected override void Undo()
        {
        }

        protected override void MovePrev()
        {
            Model.AtBankAccount AtBankAccount = this.AtBankAccountManager.GetPrev(this.AtBankAccount);
            if (AtBankAccount == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtBankAccount = AtBankAccount;

        }

        protected override void MoveNext()
        {
            Model.AtBankAccount AtBankAccount = this.AtBankAccountManager.GetNext(this.AtBankAccount);
            if (AtBankAccount == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.AtBankAccount = AtBankAccount;
        }

        protected override void MoveFirst()
        {
            this.AtBankAccount = this.AtBankAccountManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.AtBankAccount == null)
                this.AtBankAccount = this.AtBankAccountManager.GetLast();
        }
        public override void Refresh()
        {

            if (this.AtBankAccount == null)
            {
                this.AtBankAccount = new Book.Model.AtBankAccount();
                this.action = "insert";
            }
             this.textEditBankAccountId.Text=this.AtBankAccount.Id;
            this.textEditBankAccountName.Text= this.AtBankAccount.BankAccountName;
             this.comboBoxEditAccountCategory.EditValue=this.AtBankAccount.AccountCategory;
             this.newChooseContorlBankId.EditValue=this.AtBankAccount.Bank;
              this.textEditContact.Text=this.AtBankAccount.Contact;
              this.textEditContactPhone.Text=this.AtBankAccount.ContactPhone;
            this.textEditFax.Text=this.AtBankAccount.Fax ;
              this.spinEditVotesAgainst.EditValue=this.AtBankAccount.VotesAgainst;
              this.spinEditVotesAgainstAfew.EditValue=this.AtBankAccount.VotesAgainstAfew;
            this.spinEditSecurityBalance.EditValue=this.AtBankAccount.SecurityBalance;
              this.spinEditTheirBalance.EditValue =this.AtBankAccount.TheirBalance;
             this.spinEditExistingBalance.EditValue=this.AtBankAccount.ExistingBalance ;
              this.lookUpEditDepositSubject.EditValue=this.AtBankAccount.DepositSubject;
              this.lookUpEditVoteSubject.EditValue=this.AtBankAccount.VoteSubject;
              this.lookUpEditBorrowingSubject.EditValue=this.AtBankAccount.BorrowingSubject;
              this.memoEditMark.Text=this.AtBankAccount.Mark;
            switch (this.action)
            {
                case "insert":
                    this.textEditBankAccountId.Properties.ReadOnly = false;
                    this.textEditBankAccountName.Properties.ReadOnly = false;
                    this.comboBoxEditAccountCategory.Properties.ReadOnly = false;

                    this.newChooseContorlBankId.ShowButton = true;
                    this.newChooseContorlBankId.ButtonReadOnly = false;
                    this.textEditContact.Properties.ReadOnly = false;
                    this.textEditContactPhone.Properties.ReadOnly = false;
                    this.textEditFax.Properties.ReadOnly = false;
                    this.spinEditVotesAgainst.Properties.ReadOnly = false;
                    this.spinEditVotesAgainstAfew.Properties.ReadOnly = false;
                    this.spinEditSecurityBalance.Properties.ReadOnly = false;
                    this.spinEditTheirBalance.Properties.ReadOnly = false;
                    this.spinEditExistingBalance.Properties.ReadOnly = false;
                    this.lookUpEditDepositSubject.Properties.ReadOnly = false;
                    this.lookUpEditVoteSubject.Properties.ReadOnly = false;
                    this.lookUpEditBorrowingSubject.Properties.ReadOnly = false;
                    this.memoEditMark.Properties.ReadOnly = false;
                    break;

                case "update":

                    this.textEditBankAccountId.Properties.ReadOnly = false;
                    this.textEditBankAccountName.Properties.ReadOnly = false;
                    this.comboBoxEditAccountCategory.Properties.ReadOnly = false;

                    this.newChooseContorlBankId.ShowButton = true;
                    this.newChooseContorlBankId.ButtonReadOnly = false;
                    this.textEditContact.Properties.ReadOnly = false;
                    this.textEditContactPhone.Properties.ReadOnly = false;
                    this.textEditFax.Properties.ReadOnly = false;
                    this.spinEditVotesAgainst.Properties.ReadOnly = false;
                    this.spinEditVotesAgainstAfew.Properties.ReadOnly = false;
                    this.spinEditSecurityBalance.Properties.ReadOnly = false;
                    this.spinEditTheirBalance.Properties.ReadOnly = false;
                    this.spinEditExistingBalance.Properties.ReadOnly = false;
                    this.lookUpEditDepositSubject.Properties.ReadOnly = false;
                    this.lookUpEditVoteSubject.Properties.ReadOnly = false;
                    this.lookUpEditBorrowingSubject.Properties.ReadOnly = false;
                    this.memoEditMark.Properties.ReadOnly = false;
                    break;

                case "view":

                    this.textEditBankAccountId.Properties.ReadOnly = true;
                    this.textEditBankAccountName.Properties.ReadOnly = true;
                    this.comboBoxEditAccountCategory.Properties.ReadOnly = true;

                    this.newChooseContorlBankId.ShowButton = false;
                    this.newChooseContorlBankId.ButtonReadOnly = true;
                    this.textEditContact.Properties.ReadOnly = true;
                    this.textEditContactPhone.Properties.ReadOnly = true;
                    this.textEditFax.Properties.ReadOnly = true;
                    this.spinEditVotesAgainst.Properties.ReadOnly = true;
                    this.spinEditVotesAgainstAfew.Properties.ReadOnly = true;
                    this.spinEditSecurityBalance.Properties.ReadOnly = true;
                    this.spinEditTheirBalance.Properties.ReadOnly = true;
                    this.spinEditExistingBalance.Properties.ReadOnly = true;
                    this.lookUpEditDepositSubject.Properties.ReadOnly = true;
                    this.lookUpEditVoteSubject.Properties.ReadOnly = true;
                    this.lookUpEditBorrowingSubject.Properties.ReadOnly = true;
                    this.memoEditMark.Properties.ReadOnly = true;
                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.AtBankAccountManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.AtBankAccountManager.HasRowsBefore(this.AtBankAccount);
        }

        protected override bool HasRowsNext()
        {
            return this.AtBankAccountManager.HasRowsAfter(this.AtBankAccount);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditBankAccountId,this.textEditBankAccountName });

        }
        #endregion
    }
}