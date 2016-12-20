using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtBillsIncome
{
    public partial class EditForm2 : Settings.BasicData.BaseEditForm
    {
        private BL.AtBillsIncomeManager AtBillsIncomeManager = new Book.BL.AtBillsIncomeManager();
        private Model.AtBillsIncome AtBillsIncome = new Book.Model.AtBillsIncome();
        public EditForm2()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtBillsIncome.PRO_Id, new AA("請輸入票據編號..", this.textEditBillsId));
            this.action = "view";
            //this.bindingSource2.DataSource = new BL.AtBankAccountManager().Select();
            //this.bindingSource1.DataSource = new BL.SupplierManager().Select();          
            this.newChooseBank.Choose = new Settings.BasicData.Bank.ChooseBank();
            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseSullier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseSubject.Choose = new AtAccountSubject.ChooseAccountSubject();


        }
        public EditForm2(Model.AtBillsIncome AtBillsIncome)
            : this()
        {
            this.AtBillsIncome = AtBillsIncome;
            this.action = "update";
        }
        public EditForm2(Model.AtBillsIncome AtBillsIncome, string action)
            : this()
        {
            this.AtBillsIncome = AtBillsIncome;
            this.action = action;
        }
        protected override void Save()
        {
            this.AtBillsIncome.Id = this.textEditBillsId.Text;
            this.AtBillsIncome.IncomeCategory = this.comboBoxProduceType.SelectedIndex.ToString();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditTheOpenDate.DateTime, new DateTime()))
            {
                this.AtBillsIncome.TheOpenDate = null;
            }
            else
            {
                this.AtBillsIncome.TheOpenDate = this.dateEditTheOpenDate.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditMaturity.DateTime, new DateTime()))
            {
                this.AtBillsIncome.Maturity = null;
            }
            else
            {
                this.AtBillsIncome.Maturity = this.dateEditMaturity.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditMoveDay.DateTime, new DateTime()))
            {
                this.AtBillsIncome.MoveDay = null;
            }
            else
            {
                this.AtBillsIncome.MoveDay = this.dateEditMoveDay.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditTheJpy.DateTime, new DateTime()))
            {
                this.AtBillsIncome.TheJpy = null;
            }
            else
            {
                this.AtBillsIncome.TheJpy = this.dateEditTheJpy.DateTime;
            }
            this.AtBillsIncome.BillsOften = this.comboBoxEditBillsOften.SelectedIndex.ToString();
            this.AtBillsIncome.NotesMoney = this.spinEditNotesMoney.EditValue == null ? 0 :decimal.Parse(this.spinEditNotesMoney.EditValue.ToString());

            if (this.newChooseSubject.EditValue as Model.AtAccountSubject != null)
                this.AtBillsIncome.SubjectId = (this.newChooseSubject.EditValue as Model.AtAccountSubject).SubjectId;
            if (this.AtBillsIncome.IncomeCategory == "0")
            {
                if (this.newChooseCustomer.EditValue != null)
                    this.AtBillsIncome.PassingObject = (this.newChooseCustomer.EditValue as Model.Customer).CustomerId;
            }
            else if (this.AtBillsIncome.IncomeCategory =="1")
            {
                if (this.newChooseSullier.EditValue != null)
                    this.AtBillsIncome.PassingObject = (this.newChooseSullier.EditValue as Model.Supplier).SupplierId;          
            }
            if (this.newChooseBank.EditValue != null)
                this.AtBillsIncome.BankId = (this.newChooseBank.EditValue as Model.Bank).BankId;
         //   this.AtBillsIncome.CollectionAccount = this.lookUpEdit1.EditValue == null ? null : this.lookUpEdit1.EditValue.ToString();
            this.AtBillsIncome.Mark = this.memoEditMark.Text;
            switch (this.action)
            {
                case "insert":
                    if (this.AtBillsIncome.IncomeCategory == "1")
                        this.AtBillsIncome.BillsOften = "1";
                    this.AtBillsIncomeManager.Insert(this.AtBillsIncome);
                    break;
                case "update":
                    this.AtBillsIncomeManager.Update(this.AtBillsIncome);
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
                return this.AtBillsIncome;
            }
        }

        #endregion

        #region Virtual Method

        protected override void Delete()
        {
            if (this.AtBillsIncome == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.AtBillsIncomeManager.Delete(this.AtBillsIncome.BillsId);
            this.AtBillsIncome = this.AtBillsIncomeManager.GetNext(this.AtBillsIncome);
            if (this.AtBillsIncome == null)
            {
                this.AtBillsIncome = this.AtBillsIncomeManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.AtBillsIncome = new Model.AtBillsIncome();
             this.AtBillsIncome.BillsId=Guid.NewGuid().ToString();
            this.AtBillsIncome.IncomeCategory="0";
            this.AtBillsIncome.BillsOften = "0";
            this.AtBillsIncome.TheOpenDate = DateTime.Now;
            //this.AtBillsIncome.CustomInspectionRuleId = this.AtBillsIncomeManager.GetId();
        }

        protected override void Undo()
        {
        }

        protected override void MovePrev()
        {
            Model.AtBillsIncome AtBillsIncome = this.AtBillsIncomeManager.GetPrev(this.AtBillsIncome);
            if (AtBillsIncome == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtBillsIncome = AtBillsIncome;

        }

        protected override void MoveNext()
        {
            Model.AtBillsIncome AtBillsIncome = this.AtBillsIncomeManager.GetNext(this.AtBillsIncome);
            if (AtBillsIncome == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.AtBillsIncome = AtBillsIncome;
        }

        protected override void MoveFirst()
        {
            this.AtBillsIncome = this.AtBillsIncomeManager.GetFirst();
        }

        protected override void MoveLast()
        {
         
                this.AtBillsIncome = this.AtBillsIncomeManager.GetLast();
        }
        public override void Refresh()
        {

            if (this.AtBillsIncome == null)
            {
                this.AtBillsIncome = new Book.Model.AtBillsIncome();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this.AtBillsIncome = this.AtBillsIncomeManager.Get(AtBillsIncome.BillsId);                    
                }

            }

          
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AtBillsIncome.TheOpenDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditTheOpenDate.EditValue = null;
            }
            else
            {
                this.dateEditTheOpenDate.EditValue = this.AtBillsIncome.TheOpenDate;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AtBillsIncome.Maturity, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditMaturity.EditValue = null;
            }
            else
            {
                this.dateEditMaturity.EditValue = this.AtBillsIncome.Maturity;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AtBillsIncome.MoveDay, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditMoveDay.EditValue = null;
            }
            else
            {
                this.dateEditMoveDay.EditValue = this.AtBillsIncome.MoveDay;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AtBillsIncome.TheJpy, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditTheJpy.EditValue = null;
            }
            else
            {
                this.dateEditTheJpy.EditValue = this.AtBillsIncome.TheJpy;
            }


            this.comboBoxProduceType.SelectedIndex = Convert.ToInt32(this.AtBillsIncome.IncomeCategory);
            if (this.AtBillsIncome.IncomeCategory == "0")
            {
                this.newChooseSullier.EditValue = null;
                this.newChooseCustomer.EditValue = new BL.CustomerManager().Get(this.AtBillsIncome.PassingObject);
            }
            else if (this.AtBillsIncome.IncomeCategory == "1")
            {
                this.newChooseCustomer.EditValue = null;
                this.newChooseSullier.EditValue = new BL.SupplierManager().Get(this.AtBillsIncome.PassingObject);
            }

            this.newChooseSubject.EditValue = this.AtBillsIncome.Subject;
            this.newChooseBank.EditValue = this.AtBillsIncome.Bank;

            this.textEditBillsId.Text = this.AtBillsIncome.Id;
            this.comboBoxEditBillsOften.SelectedIndex = Convert.ToInt32(this.AtBillsIncome.BillsOften);
             this.spinEditNotesMoney.EditValue=this.AtBillsIncome.NotesMoney;
           // this.lookUpEditCustomerId.EditValue= this.AtBillsIncome.PassingObject ;
           // this.lookUpEdit1.EditValue = this.AtBillsIncome.CollectionAccount;
            this.memoEditMark.Text = this.AtBillsIncome.Mark;
            switch (this.action)
            {
                case "insert":
                   
                    this.dateEditTheOpenDate.Properties.ReadOnly = false;
                    this.dateEditTheOpenDate.Properties.Buttons[0].Visible = true;
                    this.dateEditMaturity.Properties.ReadOnly = false;
                    this.dateEditMaturity.Properties.Buttons[0].Visible = true;
                    this.dateEditMoveDay.Properties.ReadOnly = false;
                    this.dateEditMoveDay.Properties.Buttons[0].Visible = true;
                    this.dateEditTheJpy.Properties.ReadOnly = false;
                    this.dateEditTheJpy.Properties.Buttons[0].Visible = true;
                    this.textEditBillsId.Properties.ReadOnly = false;
                    this.comboBoxEditBillsOften.Properties.ReadOnly = false;
                    this.spinEditNotesMoney.Properties.ReadOnly = false;
                    //this.lookUpEditCustomerId.Properties.ReadOnly = false;
                   
                    this.memoEditMark.Properties.ReadOnly = false;
                    break;

                case "update":

                    this.dateEditTheOpenDate.Properties.ReadOnly = false;
                    this.dateEditTheOpenDate.Properties.Buttons[0].Visible = true;
                    this.dateEditMaturity.Properties.ReadOnly = false;
                    this.dateEditMaturity.Properties.Buttons[0].Visible = true;
                    this.dateEditMoveDay.Properties.ReadOnly = false;
                    this.dateEditMoveDay.Properties.Buttons[0].Visible = true;
                    this.dateEditTheJpy.Properties.ReadOnly = false;
                    this.dateEditTheJpy.Properties.Buttons[0].Visible = true;
                    this.textEditBillsId.Properties.ReadOnly = false;
                    this.comboBoxEditBillsOften.Properties.ReadOnly = false;
                    this.spinEditNotesMoney.Properties.ReadOnly = false;
                    //this.lookUpEditCustomerId.Properties.ReadOnly = false;
                
                    this.memoEditMark.Properties.ReadOnly = false;
                    break;

                case "view":

                    this.dateEditTheOpenDate.Properties.ReadOnly = true;
                    this.dateEditTheOpenDate.Properties.Buttons[0].Visible = false;
                    this.dateEditMaturity.Properties.ReadOnly = true;
                    this.dateEditMaturity.Properties.Buttons[0].Visible = false;
                    this.dateEditMoveDay.Properties.ReadOnly = true;
                    this.dateEditMoveDay.Properties.Buttons[0].Visible = false;
                    this.dateEditTheJpy.Properties.ReadOnly = true;
                    this.dateEditTheJpy.Properties.Buttons[0].Visible = false;
                    this.textEditBillsId.Properties.ReadOnly = true;
                    this.comboBoxEditBillsOften.Properties.ReadOnly = true;
                    this.spinEditNotesMoney.Properties.ReadOnly = true;
                  //  this.lookUpEditCustomerId.Properties.ReadOnly = true;
                  
                    this.memoEditMark.Properties.ReadOnly = true;
                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.AtBillsIncomeManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.AtBillsIncomeManager.HasRowsBefore(this.AtBillsIncome);
        }

        protected override bool HasRowsNext()
        {
            return this.AtBillsIncomeManager.HasRowsAfter(this.AtBillsIncome);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditBillsId });

        }
        #endregion
    }
}