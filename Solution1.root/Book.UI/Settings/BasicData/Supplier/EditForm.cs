using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Book.UI.Settings.BasicData.Supplier
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-11-06
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        //标致是否是保存后 执行 TREELIST事件的
        //  private int flag = 0;
        protected BL.SupplierManager supplierManager = new Book.BL.SupplierManager();
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        private Book.Model.Supplier _supplier = null;
        protected BL.PayMethodManager payManager = new Book.BL.PayMethodManager();

        private string supplierCategory = null;
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.Supplier.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.TextEditId));
            this.requireValueExceptions.Add(Model.Supplier.PROPERTY_SUPPLIERFULLNAME, new AA(Properties.Resources.RequireDataForNames, this.TextEditSupplierFullName));
            //this.requireValueExceptions.Add(Model.Supplier.PROPERTY_SUPPLIERSHORTNAME, new AA(Properties.Resources.RequireDataForNames, this.TextEditSupplierShortName));
            //this.requireValueExceptions.Add(Model.Supplier.PROPERTY_SUPPLIERNUMBER, new AA(Properties.Resources.RequireDataForCompanyNumber, this.TextEditSupplierNumber));            
            //this.requireValueExceptions.Add(Model.Supplier.PROPERTY_SUPPLIERPHONE1, new AA(Properties.Resources.RequireDataForCompanyPhone, this.TextEditSupplierPhone1));
            //this.requireValueExceptions.Add(Model.Supplier.PROPERTY_SUPPLIERPHONE2, new AA(Properties.Resources.RequireDataForCompanyPhone, this.TextEditSupplierPhone2));
            //this.requireValueExceptions.Add(Model.Supplier.PROPERTY_SUPPLIERCATEGORYID, new AA(Properties.Resources.ChooseSupplier, this.newChooseContorlSupplierCategory));

            this.invalidValueExceptions.Add(Model.Supplier.PROPERTY_ID
, new AA(Properties.Resources.EntityExists, this.TextEditId));


            this.action = "insert";

            this.newChooseContorlArea.Choose = new BasicData.AreaCategory.ChooseAreaCategory();
            // this.newChooseContorlSupplierCategory.Choose = new BasicData.SupplierCategory.ChooseSupplierCategory();
            //   this.newChooseContorlTrade.Choose = new BasicData.TradeCategory.ChooseTradeCategory();
        }

        public EditForm(Book.Model.Supplier cmpy)
            : this()
        {
            //if(cmpy == null)
            //    throw new ArithmeticException();
            this._supplier = cmpy;
            // oldId = cmpy.SupplierId;
            this.action = "update";
            if (this._supplier != null)
            {
                if (this._supplier.SupplierCategory != null)
                    supplierCategory = this._supplier.SupplierCategory.Id;
            }

        }
        public EditForm(Model.SupplierCategory SupplierCategory)
            : this()
        {

            this._supplier = new Book.Model.Supplier();
            this._supplier.SupplierCategory = SupplierCategory;

        }
        public EditForm(Book.Model.Supplier cmpy, string action)
            : this()
        {
            this._supplier = cmpy;
            this.action = action;
            if (this.action == "update" || this.action == "view")
            {

                if (this._supplier != null)
                {
                    if (this._supplier.SupplierCategory != null)
                        supplierCategory = this._supplier.SupplierCategory.Id;
                }
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            IList<Model.PayMethod> list = this.payManager.Select();
            foreach (Model.PayMethod payMethod in list)
            {
                comboBoxPayMethod.Properties.Items.Add(payMethod);
            }
        }

        #region Override

        public override object EditedItem
        {
            get
            {
                return this._supplier;
            }
        }

        protected override void Delete()
        {
            if (this._supplier == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.supplierManager.Delete(this._supplier.SupplierId);
            this._supplier = this.supplierManager.GetNext(this._supplier);
            if (this._supplier == null)
            {
                this._supplier = this.supplierManager.GetLast();
            }
        }
        protected override void AddNew()
        {
            this._supplier = new Model.Supplier();
        }

        protected override void MoveNext()
        {
            Model.Supplier _supplier = this.supplierManager.GetNext(this._supplier);
            if (_supplier == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._supplier = _supplier;
        }

        protected override void MovePrev()
        {
            Model.Supplier _supplier = this.supplierManager.GetPrev(this._supplier);
            if (_supplier == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._supplier = _supplier;
        }

        protected override void MoveFirst()
        {
            this._supplier = this.supplierManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this._supplier == null)
                this._supplier = this.supplierManager.GetLast();
        }

        public override void Refresh()
        {
            if (this._supplier == null)
            {
                this._supplier = new Book.Model.Supplier();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this._supplier = this.supplierManager.Get(this._supplier.SupplierId);
            }
            this.bindingSourceEmployee.DataSource = this.employeeManager.SelectOnActive();
            this.bindingSourceSupplier.DataSource = this.supplierManager.Select();

            if (this._supplier.SupplierCategory != null)
                this.buttonEditSupplierCategory.EditValue = this._supplier.SupplierCategory;

            this.TextEditId.Text = (string.IsNullOrEmpty(this._supplier.Id) ? this._supplier.SupplierId : this._supplier.Id);
            this.TextEditSupplierFullName.Text = this._supplier.SupplierFullName;
            this.TextEditSupplierShortName.Text = this._supplier.SupplierShortName;
            this.memoEditRemarks.Text = this._supplier.Remark;
            this.MemoEditSupplierRemarks.Text = this._supplier.SupplierRemark;
            this.TextEditSupplierPhone1.Text = this._supplier.SupplierPhone1;
            this.TextEditSupplierPhone2.Text = this._supplier.SupplierPhone2;
            this.TextEditSupplierAddress.Text = this._supplier.CompanyAddress;
            this.TextEditSupplierEmail.Text = this._supplier.Email;
            this.TextEditSupplierManager.Text = this._supplier.SupplierManager;
            this.TextEditSupplierMobile.Text = this._supplier.SupplierMobile;
            this.TextEditSupplierNumber.Text = this._supplier.SupplierNumber;
            this.TextEditSupplierFax.Text = this._supplier.SupplierFax;
            this.TextEditPostCode.Text = this._supplier.PostCode;
            this.textEditPayAddress.Text = this._supplier.PayAddress;
            this.textEditFactoryAddress.Text = this._supplier.FactoryAddress;
            if (this._supplier.PayMethod != null)
                this.comboBoxPayMethod.Text = this._supplier.PayMethod.PayMethodName;
            this.spinEditAdvancePatment.Value = (this._supplier.AdvancePatment == null ? 0 : decimal.Parse(this._supplier.AdvancePatment.ToString()));
            this.spinEditPayDay.Value = (this._supplier.PayDay == null ? 0 : decimal.Parse(this._supplier.PayDay.ToString()));

            this.newChooseContorlArea.EditValue = this._supplier.AreaCategory;
            this.lookUpEditBusinessId.EditValue = this._supplier.EmployeeBusinessId;
            this.newChooseContorlEmployeeChangeId.Text = this._supplier.EmployeeChangeId;
            this.newChooseContorlEmployeeCreatorId.Text = this._supplier.EmployeeCreatorId;

            // this.newChooseContorlTrade.EditValue = this._supplier.TradeCategory;


            this.dateEditInsertTime.EditValue = this._supplier.InsertTime;
            this.dateEditLastPayDate.EditValue = this._supplier.LastPayDate;
            this.dateEditLastTransactionDate.EditValue = this._supplier.LastTransactionDate;
            this.dateEditUpdateTime.EditValue = this._supplier.UpdateTime;
            this.spinEditLastPayMoney.EditValue = this._supplier.LastPayMoney;
            this.spinEditLastTransactionMoney.EditValue = this._supplier.LastTransactionMoney;
            //this.spinEditLineOfCredit.EditValue = this._supplier.LineOfCredit;
            this.spinEditPayableOwe.EditValue = this._supplier.PayableOwe;
            this.comboBoxEditAbcCategory.Text = this._supplier.AbcCategory;

            this.TextEditSupplierNetAddress.Text = this._supplier.NetAddress;

            this.bindingSourceSupplierContact.DataSource = this._supplier.Contacts;

           this.textEditContact.Text =  this._supplier.SupplierContact;

            SetTextEditReadOnly(true);

            switch (this.action)
            {
                case "insert":

                    this.buttonEditSupplierCategory.Properties.Buttons[0].Enabled = true;
                    this.buttonEditSupplierCategory.Properties.ReadOnly = false;
                    this.dateEditInsertTime.DateTime = DateTime.Now;
                    this.TextEditId.Properties.ReadOnly = false;
                    this.TextEditSupplierAddress.Properties.ReadOnly = false;
                    this.TextEditSupplierFax.Properties.ReadOnly = false;
                    this.TextEditSupplierFullName.Properties.ReadOnly = false;
                    this.TextEditSupplierShortName.Properties.ReadOnly = false;
                    this.TextEditSupplierPhone1.Properties.ReadOnly = false;
                    this.MemoEditSupplierRemarks.Properties.ReadOnly = false;
                    this.TextEditSupplierEmail.Properties.ReadOnly = false;
                    this.TextEditSupplierManager.Properties.ReadOnly = false;
                    this.TextEditSupplierMobile.Properties.ReadOnly = false;
                    this.TextEditSupplierNumber.Properties.ReadOnly = false;
                    this.TextEditSupplierPhone2.Properties.ReadOnly = false;
                    this.TextEditSupplierNetAddress.Properties.ReadOnly = false;
                    this.TextEditPostCode.Properties.ReadOnly = false;
                    this.comboBoxPayMethod.Properties.ReadOnly = false;
                    this.spinEditAdvancePatment.Properties.ReadOnly = false;
                    this.spinEditPayDay.Properties.ReadOnly = false;
                    this.textEditPayAddress.Properties.ReadOnly = false;
                    this.textEditFactoryAddress.Properties.ReadOnly = false;

                    this.newChooseContorlArea.ButtonReadOnly = false;
                    this.newChooseContorlArea.ShowButton = true;
                    this.lookUpEditBusinessId.Properties.ReadOnly = false;
                    this.lookUpEditBusinessId.Properties.Buttons[0].Visible = true;


                    //  this.newChooseContorlTrade.ButtonReadOnly = false;
                    // this.newChooseContorlTrade.ShowButton = true;                    
                    this.dateEditLastPayDate.Properties.ReadOnly = false;
                    this.dateEditLastPayDate.Properties.Buttons[0].Visible = true;
                    this.dateEditLastTransactionDate.Properties.ReadOnly = false;
                    this.dateEditLastTransactionDate.Properties.Buttons[0].Visible = true;
                    this.spinEditLastPayMoney.Properties.ReadOnly = false;
                    this.spinEditLastPayMoney.Properties.Buttons[0].Visible = true;
                    this.spinEditLastTransactionMoney.Properties.ReadOnly = false;
                    this.spinEditLastTransactionMoney.Properties.Buttons[0].Visible = true;
                    this.spinEditPayableOwe.Properties.ReadOnly = false;
                    this.spinEditPayableOwe.Properties.Buttons[0].Visible = true;
                    this.comboBoxEditAbcCategory.Properties.ReadOnly = false;
                    this.comboBoxEditAbcCategory.Properties.Buttons[0].Visible = true;

                    this.simpleButtonAdd.Enabled = true;
                    this.simpleButtonDel.Enabled = false;
                    this.simpleButtonModify.Enabled = false;
                    this.simpleButtonSave.Enabled = false;
                    this.simpleButtonUndo.Enabled = false;
                    break;

                case "update":
                    this.buttonEditSupplierCategory.Properties.Buttons[0].Enabled = true;
                    this.buttonEditSupplierCategory.Properties.ReadOnly = false;
                    this.dateEditInsertTime.DateTime = DateTime.Now;
                    this.dateEditUpdateTime.DateTime = DateTime.Now;
                    this.TextEditId.Properties.ReadOnly = false;
                    this.TextEditSupplierAddress.Properties.ReadOnly = false;
                    this.TextEditSupplierFax.Properties.ReadOnly = false;
                    this.TextEditSupplierFullName.Properties.ReadOnly = false;
                    this.TextEditSupplierShortName.Properties.ReadOnly = false;
                    this.TextEditSupplierPhone1.Properties.ReadOnly = false;
                    this.MemoEditSupplierRemarks.Properties.ReadOnly = false;
                    this.TextEditSupplierEmail.Properties.ReadOnly = false;
                    this.TextEditSupplierMobile.Properties.ReadOnly = false;
                    this.TextEditSupplierManager.Properties.ReadOnly = false;
                    this.TextEditSupplierNumber.Properties.ReadOnly = false;
                    this.TextEditSupplierPhone2.Properties.ReadOnly = false;
                    this.TextEditSupplierNetAddress.Properties.ReadOnly = false;
                    this.TextEditPostCode.Properties.ReadOnly = false;
                    this.comboBoxPayMethod.Properties.ReadOnly = false;
                    this.spinEditAdvancePatment.Properties.ReadOnly = false;
                    this.spinEditPayDay.Properties.ReadOnly = false;
                    this.textEditPayAddress.Properties.ReadOnly = false;
                    this.textEditFactoryAddress.Properties.ReadOnly = false;

                    this.newChooseContorlArea.ButtonReadOnly = false;
                    this.newChooseContorlArea.ShowButton = true;
                    this.lookUpEditBusinessId.Properties.ReadOnly = false;
                    this.lookUpEditBusinessId.Properties.Buttons[0].Visible = true;


                    //  this.newChooseContorlTrade.ShowButton = true;
                    this.dateEditLastPayDate.Properties.ReadOnly = false;
                    this.dateEditLastPayDate.Properties.Buttons[0].Visible = true;
                    this.dateEditLastTransactionDate.Properties.ReadOnly = false;
                    this.dateEditLastTransactionDate.Properties.Buttons[0].Visible = true;
                    this.spinEditLastPayMoney.Properties.ReadOnly = false;
                    this.spinEditLastPayMoney.Properties.Buttons[0].Visible = true;
                    this.spinEditLastTransactionMoney.Properties.ReadOnly = false;
                    this.spinEditLastTransactionMoney.Properties.Buttons[0].Visible = true;
                    this.spinEditPayableOwe.Properties.ReadOnly = false;
                    this.spinEditPayableOwe.Properties.Buttons[0].Visible = true;
                    this.comboBoxEditAbcCategory.Properties.ReadOnly = false;
                    this.comboBoxEditAbcCategory.Properties.Buttons[0].Visible = true;

                    this.simpleButtonAdd.Enabled = true;
                    this.simpleButtonDel.Enabled = true;
                    this.simpleButtonModify.Enabled = true;
                    this.simpleButtonSave.Enabled = false;
                    this.simpleButtonUndo.Enabled = false;
                    break;

                case "view":
                    this.buttonEditSupplierCategory.Properties.Buttons[0].Enabled = false;
                    this.buttonEditSupplierCategory.Properties.ReadOnly = true;
                    this.TextEditId.Properties.ReadOnly = true;
                    this.TextEditSupplierAddress.Properties.ReadOnly = true;
                    this.TextEditSupplierFax.Properties.ReadOnly = true;
                    this.TextEditSupplierFullName.Properties.ReadOnly = true;
                    this.TextEditSupplierShortName.Properties.ReadOnly = true;
                    this.TextEditSupplierPhone1.Properties.ReadOnly = true;
                    this.MemoEditSupplierRemarks.Properties.ReadOnly = true;
                    this.TextEditSupplierEmail.Properties.ReadOnly = true;
                    this.TextEditSupplierMobile.Properties.ReadOnly = true;
                    this.TextEditSupplierManager.Properties.ReadOnly = true;
                    this.TextEditSupplierNumber.Properties.ReadOnly = true;
                    this.TextEditSupplierPhone2.Properties.ReadOnly = true;
                    this.TextEditSupplierNetAddress.Properties.ReadOnly = true;
                    this.TextEditPostCode.Properties.ReadOnly = true;
                    this.comboBoxPayMethod.Properties.ReadOnly = true;
                    this.spinEditAdvancePatment.Properties.ReadOnly = true;
                    this.spinEditPayDay.Properties.ReadOnly = true;
                    this.textEditPayAddress.Properties.ReadOnly = true;
                    this.textEditFactoryAddress.Properties.ReadOnly = true;

                    this.newChooseContorlArea.ButtonReadOnly = true;
                    this.newChooseContorlArea.ShowButton = false;
                    this.lookUpEditBusinessId.Properties.ReadOnly = true;
                    this.lookUpEditBusinessId.Properties.Buttons[0].Visible = false;

                    //  this.newChooseContorlTrade.ButtonReadOnly = true;

                    this.dateEditLastPayDate.Properties.ReadOnly = true;
                    this.dateEditLastPayDate.Properties.Buttons[0].Visible = false;
                    this.dateEditLastTransactionDate.Properties.ReadOnly = true;
                    this.dateEditLastTransactionDate.Properties.Buttons[0].Visible = false;
                    this.spinEditLastPayMoney.Properties.ReadOnly = true;
                    this.spinEditLastPayMoney.Properties.Buttons[0].Visible = false;
                    this.spinEditLastTransactionMoney.Properties.ReadOnly = true;
                    this.spinEditLastTransactionMoney.Properties.Buttons[0].Visible = false;
                    this.spinEditPayableOwe.Properties.ReadOnly = true;
                    this.spinEditPayableOwe.Properties.Buttons[0].Visible = false;
                    this.comboBoxEditAbcCategory.Properties.ReadOnly = true;
                    this.comboBoxEditAbcCategory.Properties.Buttons[0].Visible = false;

                    this.simpleButtonAdd.Enabled = false;
                    this.simpleButtonDel.Enabled = false;
                    this.simpleButtonModify.Enabled = false;
                    this.simpleButtonSave.Enabled = false;
                    this.simpleButtonUndo.Enabled = false;

                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.supplierManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.supplierManager.HasRowsAfter(this._supplier);
        }

        protected override bool HasRowsPrev()
        {
            return this.supplierManager.HasRowsBefore(this._supplier);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditEmail, this.textEditMobile, this.textEditName, this.textEditPhone, this.TextEditPostCode, this.TextEditSupplierAddress, this.TextEditSupplierEmail, this.TextEditSupplierFax, this.TextEditSupplierFullName, this.TextEditId, this.TextEditSupplierManager, this.TextEditSupplierMobile, this.TextEditSupplierNetAddress, this.TextEditSupplierNetAddress, this.TextEditSupplierNumber, this.TextEditSupplierPhone1, this.TextEditSupplierPhone2, this.TextEditSupplierShortName, this.memoEditRemarks, this.MemoEditSupplierRemarks });
        }
        protected override void Save()
        {
            this._supplier.Id = this.TextEditId.Text;
            this._supplier.AbcCategory = this.comboBoxEditAbcCategory.Text;
            this._supplier.AreaCategory = this.newChooseContorlArea.EditValue as Model.AreaCategory;
            this._supplier.CompanyAddress = this.TextEditSupplierAddress.Text;
            this._supplier.Email = this.TextEditSupplierEmail.Text;
            this._supplier.EmployeeBusinessId = this.lookUpEditBusinessId.Text;
            this._supplier.LastPayMoney = this.spinEditLastPayMoney.Value;
            this._supplier.LastTransactionMoney = this.spinEditLastTransactionMoney.Value;
            this._supplier.PayableOwe = this.spinEditPayableOwe.Value;
            this._supplier.PostCode = this.TextEditPostCode.Text;
            this._supplier.Remark = this.memoEditRemarks.Text;
            this._supplier.SupplierRemark = this.MemoEditSupplierRemarks.Text;
            this._supplier.SupplierCategory = this.buttonEditSupplierCategory.EditValue as Model.SupplierCategory;
            //this.newChooseContorlSupplierCategory.EditValue as Model.SupplierCategory;
            this._supplier.SupplierFax = this.TextEditSupplierFax.Text;
            this._supplier.SupplierFullName = this.TextEditSupplierFullName.Text;
            this._supplier.SupplierManager = this.TextEditSupplierManager.Text;
            this._supplier.SupplierMobile = this.TextEditSupplierMobile.Text;
            this._supplier.SupplierNumber = this.TextEditSupplierNumber.Text;
            this._supplier.SupplierPhone1 = this.TextEditSupplierPhone1.Text;
            this._supplier.SupplierPhone2 = this.TextEditSupplierPhone2.Text;
            this._supplier.SupplierShortName = this.TextEditSupplierShortName.Text;
            // this._supplier.TradeCategory = this.newChooseContorlTrade.EditValue as Model.TradeCategory;
            this._supplier.NetAddress = this.TextEditSupplierNetAddress.Text;
            this._supplier.PayMethod = this.comboBoxPayMethod.SelectedItem as Model.PayMethod;
            this._supplier.PayDay = Convert.ToInt32(this.spinEditPayDay.Value);
            this._supplier.AdvancePatment = this.spinEditAdvancePatment.Value;
            this._supplier.FactoryAddress = this.textEditFactoryAddress.Text;
            this._supplier.PayAddress = this.textEditPayAddress.Text;
            this._supplier.SupplierContact = this.textEditContact.Text;

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditLastPayDate.DateTime, new DateTime()))
            {
                this._supplier.LastPayDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._supplier.LastPayDate = this.dateEditLastPayDate.DateTime;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditLastTransactionDate.DateTime, new DateTime()))
            {
                this._supplier.LastTransactionDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._supplier.LastTransactionDate = this.dateEditLastTransactionDate.DateTime;
            }

            switch (this.action)
            {
                case "insert":

                    this.supplierManager.Insert(this._supplier);
                    break;

                case "update":
                    //this._supplier.SupplierId = oldId;
                    this.supplierManager.Update(this._supplier);
                    break;
            }
        }

        #endregion



        private void CompanyName1TextEdit_EditValueChanged(object sender, EventArgs e)
        {
            this.TextEditSupplierShortName.Text = this.TextEditSupplierFullName.Text.Length > 6 ? this.TextEditSupplierFullName.Text.Substring(0, 6) : this.TextEditSupplierFullName.Text;
        }


        #region  AddContact

        private string _ac;
        private Model.SupplierContact sc;

        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            if (action != "view")
            {
                ClearText();
                SetTextEditReadOnly(false);
                _ac = "insert";
                sc = new Book.Model.SupplierContact();

                this.simpleButtonAdd.Enabled = false;
                this.simpleButtonSave.Enabled = true;
                this.simpleButtonModify.Enabled = false;
                this.simpleButtonDel.Enabled = false;
                this.simpleButtonUndo.Enabled = true;
            }
        }

        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
            if (action != "view")
            {
                if (MessageBox.Show("确定删除", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this._supplier.Contacts.Remove(this.bindingSourceSupplierContact.Current as Model.SupplierContact);
                }
            }
        }

        private void simpleButtonModifiy_Click(object sender, EventArgs e)
        {
            if (action != "view")
            {
                sc = this.bindingSourceSupplierContact.Current as Model.SupplierContact;
                if (sc != null)
                {
                    SetTextEditReadOnly(false);
                    _ac = "modify";

                    this.simpleButtonAdd.Enabled = false;
                    this.simpleButtonDel.Enabled = false;
                    this.simpleButtonModify.Enabled = false;
                    this.simpleButtonSave.Enabled = true;
                    this.simpleButtonUndo.Enabled = true;

                    this.textEditEmail.Text = sc.SupplierContactEmail;
                    this.textEditMobile.Text = sc.SupplierContactMobile;
                    this.textEditName.Text = sc.SupplierContactName;
                    this.textEditPhone.Text = sc.SupplierContactPhone;
                    this.memoEditRemarks.Text = sc.SupplierContactRemark;
                }
            }
        }

        void ClearText()
        {
            this.textEditEmail.Text = "";
            this.textEditMobile.Text = "";
            this.textEditName.Text = "";
            this.textEditPhone.Text = "";
            this.memoEditRemarks.Text = "";
        }

        void SetTextEditReadOnly(bool flag)
        {
            this.textEditEmail.Properties.ReadOnly = flag;
            this.textEditMobile.Properties.ReadOnly = flag;
            this.textEditName.Properties.ReadOnly = flag;
            this.textEditPhone.Properties.ReadOnly = flag;
            this.memoEditRemarks.Properties.ReadOnly = flag;
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            if (action != "view")
            {
                this.simpleButtonAdd.Enabled = true;
                this.simpleButtonDel.Enabled = true;
                this.simpleButtonModify.Enabled = true;
                this.simpleButtonSave.Enabled = false;
                this.simpleButtonUndo.Enabled = false;

                sc.Supplier = this._supplier;
                sc.SupplierContactEmail = this.textEditEmail.Text;
                sc.SupplierContactMobile = this.textEditMobile.Text;
                sc.SupplierContactName = this.textEditName.Text;
                sc.SupplierContactPhone = this.textEditPhone.Text;
                sc.SupplierContactRemark = this.memoEditRemarks.Text;

                if (_ac == "insert")
                {
                    sc.SupplierContactId = Guid.NewGuid().ToString();
                    sc.Supplier = _supplier;
                    this._supplier.Contacts.Add(sc);
                }

                this.bindingSourceSupplierContact.DataSource = this._supplier.Contacts;

                this.gridControl1.RefreshDataSource();
                SetTextEditReadOnly(true);
            }
        }

        #endregion

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                sc = this.bindingSourceSupplierContact.Current as Model.SupplierContact;

                if (sc != null)
                {
                    this.textEditEmail.Text = sc.SupplierContactEmail;
                    this.textEditMobile.Text = sc.SupplierContactMobile;
                    this.textEditName.Text = sc.SupplierContactName;
                    this.textEditPhone.Text = sc.SupplierContactPhone;
                    this.memoEditRemarks.Text = sc.SupplierContactRemark;
                }
            }
        }

        private void simpleButtonUndo_Click(object sender, EventArgs e)
        {
            ClearText();
            SetTextEditReadOnly(true);
            this.simpleButtonUndo.Enabled = false;
            this.simpleButtonModify.Enabled = true;
            this.simpleButtonSave.Enabled = false;
            this.simpleButtonAdd.Enabled = true;
            this.simpleButtonDel.Enabled = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.textEditFactoryAddress.Text = this.TextEditSupplierAddress.Text;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            this.textEditPayAddress.Text = this.TextEditSupplierAddress.Text;
        }



        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.action != "view")
            {

                Settings.BasicData.SupplierCategory.ChooseSupplierCategoryForm f = new Settings.BasicData.SupplierCategory.ChooseSupplierCategoryForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    this.buttonEditSupplierCategory.EditValue = f.SelectedItem as Model.SupplierCategory;

                    this._supplier.SupplierCategory = (this.buttonEditSupplierCategory.EditValue as Model.SupplierCategory);

                    //生成编号
                    if (this.action == "update")
                    {
                        if (supplierCategory == this._supplier.SupplierCategory.Id)
                            this.TextEditId.Text = this._supplier.Id;
                        else
                            this.TextEditId.Text = this.supplierManager.GetNewId(this._supplier.SupplierCategory);

                    }
                    else
                    {
                        this.TextEditId.Text = this.supplierManager.GetNewId(this._supplier.SupplierCategory);
                    }

                }


            }

        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            this._supplier = this.bindingSourceSupplier.Current as Model.Supplier;
            this.action = "view";
            Refresh();

        }


    }
}