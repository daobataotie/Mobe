using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Reflection;

namespace Book.UI.Settings.BasicData.Customs
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 客戶設置
   // 文 件 名：EditForm
   // 编 码 人: 马艳军                   完成时间:2009-10-10
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {

        #region 變量對象設置
        protected BL.CustomerManager customerManager = new Book.BL.CustomerManager();
        protected BL.CompanyLevelManager companyLevelManager = new Book.BL.CompanyLevelManager();
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        private Book.Model.Customer _customer = null;
        private string _ac;
        private Model.CustomerContact cc;
        //private BL.CustomerMarksManager customerMarksManager = new Book.BL.CustomerMarksManager();
        #endregion

        #region 構造函數
        //無慘
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.Customer.PRO_Id, new AA(Properties.Resources.RequireDataForId, this.CompanyIdTextEdit));
            this.requireValueExceptions.Add(Model.Customer.PRO_CustomerFullName, new AA(Properties.Resources.RequireDataForName, this.CustomerFullNameTextEdit));

            this.invalidValueExceptions.Add(Model.Customer.PRO_Id, new AA(Properties.Resources.EntityExists, this.CompanyIdTextEdit));
            this.invalidValueExceptions.Add(Model.Customer.PRO_CustomerFullName, new AA(Properties.Resources.CustomerFullName, this.CustomerFullNameTextEdit));
            this.invalidValueExceptions.Add(Model.Customer.PRO_CustomerShortName, new AA(Properties.Resources.CustomerShortName, this.CustomerShortNameTextEdit));
            //this.invalidValueExceptions.Add(Model.CustomerMarks.PRO_CustomerMarksId, new AA(Properties.Resources.EntityExists, this.gridControl3));
            //客户分类,地区分类，行业分类
            //this.newChooseContorlArea.Choose = new BasicData.AreaCategory.ChooseAreaCategory();
            //this.newChooseContorlCategoryCustom.Choose = new BasicData.CustomerCategory.ChooseCustomerCategory();
            //this.newChooseContorlTrade.Choose = new BasicData.TradeCategory.ChooseTradeCategory();            
            this.action = "view";
        }
        /// <param name="cmpy">model 對象</param>
        public EditForm(Book.Model.Customer cmpy)
            : this()
        {
            this._customer = cmpy;
            this.action = "update";
        }
        /// <param name="cmpy">model 對象</param>
        /// <param name="action">當前動作</param>
        public EditForm(Book.Model.Customer cmpy, string action)
            : this()
        {
            this._customer = cmpy;
            this.action = action;
        }
        #endregion

        #region 窗體加載事件
        private void EditForm_Load(object sender, EventArgs e)
        {

            IList<Model.CompanyLevel> levelList = this.companyLevelManager.Select();
            foreach (Model.CompanyLevel level in levelList)
            {
                this.comboBoxEditCompanyLevel.Properties.Items.Add(level.CompanyLevelId + @"-" + level.CompanyLevelName);
            }
            this.bindingSourceCustom.DataSource = customerManager.Select();

        }
        #endregion

        #region 重載事件

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.CompanyIdTextEdit, this.textEditCompanyEMail, this.textEditCompanyMobile, this.textEditCompanyNumber, this.textEditCompanyPhone1, this.textEditCompanyWebSiteAddress, this.textEditCompanyPhone, this.CompanyFaxTextEdit, this.spinEditCompanyPayDate, this.textEditName, this.textEditPhone, this.textEditMobile, this.textEditEmail, this.memoEditRemarks, this });
        }
        public override object EditedItem
        {
            get
            {
                return _customer;
            }
        }

        protected override void Delete()
        {
            if (this._customer == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.customerManager.Delete(this._customer.CustomerId);

                this._customer = this.customerManager.GetNext(this._customer);
                if (this._customer == null)
                {
                    this._customer = this.customerManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override void Save()
        {
            this._customer.CustomerDescription = this.CompanyDescriptionMemoEdit.Rtf;
            this._customer.Id = this.CompanyIdTextEdit.Text;
            this._customer.CustomerFullName = this.CustomerFullNameTextEdit.Text;
            this._customer.CustomerShortName = this.CustomerShortNameTextEdit.Text;
            this._customer.CustomerPhone = this.textEditCompanyPhone.Text;
            this._customer.CustomerFax = this.CompanyFaxTextEdit.Text;
            this._customer.CustomerAddress = this.CompanyAddressTextEdit.Text;
            this._customer.CustomerEMail = this.textEditCompanyEMail.Text;
            this._customer.CustomerJinChuAddress = this.textEditCompanyJinChuAddress.Text;
            this._customer.CustomerManager = this.textEditCompanyManager.Text;
            this._customer.CustomerMobile = this.textEditCompanyMobile.Text;
            this._customer.CustomerNumber = this.textEditCompanyNumber.Text;
            this._customer.CustomerPhone1 = this.textEditCompanyPhone1.Text;
            this._customer.CustomerWebSiteAddress = this.textEditCompanyWebSiteAddress.Text;
            this._customer.CustomerPayDate = int.Parse(this.spinEditCompanyPayDate.Text == "" ? "1" : this.spinEditCompanyPayDate.Text);
            this._customer.CustomerContact = this.textEditContact.Text;
            this._customer.EmployeesBusinessId = this.lookUpEditBusiness.Text;
            string levelId = this.comboBoxEditCompanyLevel.Text;

            if (!string.IsNullOrEmpty(levelId))
            {
                this._customer.CompanyLevelId = levelId.Split(new char[] { '-' })[0];
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditCompanyExchangeDay.DateTime, new DateTime()))
            {
                this._customer.LastTransactionDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._customer.LastTransactionDate = this.dateEditCompanyExchangeDay.DateTime;
            }
            this._customer.CheckedStandard = this.textBoxCheckedStandard.Text;
            this._customer.CustomerName = this.txt_CustomerName.Text;

            if (!string.IsNullOrEmpty(this.richTextBoxMarks1.Text))
                this._customer.Marks1 = this.richTextBoxMarks1.Rtf;
            if (!string.IsNullOrEmpty(this.richTextBoxMarks2.Text))
                this._customer.Marks2 = this.richTextBoxMarks2.Rtf;
            if (!string.IsNullOrEmpty(this.richTextBoxMarks3.Text))
                this._customer.Marks3 = this.richTextBoxMarks3.Rtf;

            switch (this.action)
            {
                case "insert":
                    this._customer.CustomerReceivable = decimal.Zero;
                    this._customer.EmployeeCreatorId = BL.V.ActiveOperator.OperatorName;
                    this.customerManager.Insert(this._customer);
                    break;

                case "update":
                    this._customer.CustomerReceivable = this._customer.CustomerReceivable;
                    this._customer.EmployeeChangeId = BL.V.ActiveOperator.OperatorName;
                    this.customerManager.Update(this._customer);
                    break;
            }
        }

        protected override void AddNew()
        {
            this._customer = new Model.Customer();
            this._customer.LastTransactionDate = DateTime.Now;
            this._customer.Id = this.customerManager.GetNewId();
        }

        protected override void MoveNext()
        {
            Model.Customer company = this.customerManager.GetNext(this._customer);
            if (company == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._customer = company;
        }

        protected override void MovePrev()
        {
            Model.Customer company = this.customerManager.GetPrev(this._customer);
            if (company == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._customer = company;
        }

        protected override void MoveFirst()
        {
            this._customer = this.customerManager.GetFirst();
        }

        protected override void MoveLast()
        {
            //if (this._customer == null)
            this._customer = this.customerManager.GetLast();
        }

        public override void Refresh()
        {
            if (this._customer == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    if (this._customer.CustomerId != null)
                        this._customer = this.customerManager.Get(this._customer.CustomerId);
                }
            }
            this.bindingSourceBusiness.DataSource = this.employeeManager.SelectOnActive();
            this.bindingSourceCustom.DataSource = this.customerManager.Select();
            this.bindingSourceCustomsContact.DataSource = this._customer.Contacts;

            this.CompanyIdTextEdit.Text = string.IsNullOrEmpty(this._customer.Id) ? this._customer.CustomerId : this._customer.Id;
            this.CustomerFullNameTextEdit.Text = this._customer.CustomerFullName;
            this.CustomerShortNameTextEdit.Text = this._customer.CustomerShortName;
            this.CompanyDescriptionMemoEdit.Rtf = this._customer.CustomerDescription;
            this.textEditCompanyPhone.Text = this._customer.CustomerPhone;
            this.CompanyFaxTextEdit.Text = this._customer.CustomerFax;
            this.CompanyAddressTextEdit.Text = this._customer.CustomerAddress;
            this.textEditCompanyEMail.Text = this._customer.CustomerEMail;

            this.textEditCompanyJinChuAddress.Text = this._customer.CustomerJinChuAddress;
            this.textEditCompanyManager.Text = this._customer.CustomerManager;
            this.textEditCompanyMobile.Text = this._customer.CustomerMobile;
            this.textEditCompanyNumber.Text = this._customer.CustomerNumber;
            this.textEditCompanyPhone1.Text = this._customer.CustomerPhone1;
            this.textEditCompanyWebSiteAddress.Text = this._customer.CustomerWebSiteAddress;
            this.spinEditCompanyPayDate.EditValue = this._customer.CustomerPayDate;
            this.lookUpEditBusiness.EditValue = this._customer.EmployeesBusinessId;
            this.textBoxCheckedStandard.Text = this._customer.CheckedStandard;
            this.textEditContact.Text = this._customer.CustomerContact;
            if (global::Helper.DateTimeParse.DateTimeEquls(this._customer.LastTransactionDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditCompanyExchangeDay.EditValue = null;
            }
            else
            {
                this.dateEditCompanyExchangeDay.EditValue = this._customer.LastTransactionDate;
            }
            Model.CompanyLevel level = this.companyLevelManager.Get(this._customer.CompanyLevelId);

            if (level != null)
            {
                this.comboBoxEditCompanyLevel.Text = level.CompanyLevelId + @"-" + level.CompanyLevelName;
            }

            SetTextEditReadOnly(true);

            this.richTextBoxMarks1.Rtf = this._customer.Marks1;
            this.richTextBoxMarks2.Rtf = this._customer.Marks2;
            this.richTextBoxMarks3.Rtf = this._customer.Marks3;

            this.txt_CustomerName.EditValue = this._customer.CustomerName;
            //this._customer.Marks = this.customerMarksManager.SelectByCustomerId(this._customer.CustomerId);
            //this.bindingSourceCustomerMarks.DataSource = this._customer.Marks;

            //switch (this.action)
            //{
            //    case "insert":
            //        this.CompanyIdTextEdit.Properties.ReadOnly = false;
            //        this.CompanyAddressTextEdit.Properties.ReadOnly = false;
            //        this.CompanyFaxTextEdit.Properties.ReadOnly = false;
            //        this.CustomerShortNameTextEdit.Properties.ReadOnly = false;
            //        this.CustomerFullNameTextEdit.Properties.ReadOnly = false;
            //        //this.CompanyPayDayTextEdit.Properties.ReadOnly = false;
            //        this.textEditCompanyPhone.Properties.ReadOnly = false;
            //        this.CompanyDescriptionMemoEdit.Rtf.ReadOnly = false;

            //        this.textEditCompanyEMail.Properties.ReadOnly = false;
            //        this.dateEditCompanyExchangeDay.Properties.ReadOnly = false;
            //        this.textEditCompanyJinChuAddress.Properties.ReadOnly = false;
            //        this.textEditCompanyManager.Properties.ReadOnly = false;
            //        this.textEditCompanyMobile.Properties.ReadOnly = false;
            //        this.textEditCompanyNumber.Properties.ReadOnly = false;
            //        this.textEditCompanyPhone1.Properties.ReadOnly = false;
            //        this.textEditCompanyWebSiteAddress.Properties.ReadOnly = false;
            //        this.spinEditCompanyPayDate.Properties.ReadOnly = false;
            //        this.dateEditCompanyExchangeDay.Properties.Buttons[0].Visible = true;

            //        this.lookUpEditBusiness.Properties.ReadOnly = false;
            //        this.lookUpEditBusiness.Properties.Buttons[0].Visible = true;

            //        this.simpleButtonAdd.Enabled = true;
            //        this.simpleButtonDel.Enabled = false;
            //        this.simpleButtonModify.Enabled = false;
            //        this.simpleButtonSave.Enabled = false;
            //        this.simpleButtonUndo.Enabled = false;
            //        break;

            //    case "update":

            //        this.CompanyIdTextEdit.Properties.ReadOnly = false;
            //        this.CompanyAddressTextEdit.Properties.ReadOnly = false;
            //        this.CompanyFaxTextEdit.Properties.ReadOnly = false;
            //        this.CustomerShortNameTextEdit.Properties.ReadOnly = false;
            //        this.CustomerFullNameTextEdit.Properties.ReadOnly = false;
            //        //this.CompanyPayDayTextEdit.Properties.ReadOnly = false;
            //        this.textEditCompanyPhone.Properties.ReadOnly = false;
            //        this.CompanyDescriptionMemoEdit.Rtf.ReadOnly = false;

            //        this.textEditCompanyEMail.Properties.ReadOnly = false;
            //        this.dateEditCompanyExchangeDay.Properties.ReadOnly = false;
            //        this.textEditCompanyJinChuAddress.Properties.ReadOnly = false;
            //        this.textEditCompanyManager.Properties.ReadOnly = false;
            //        this.textEditCompanyMobile.Properties.ReadOnly = false;
            //        this.textEditCompanyNumber.Properties.ReadOnly = false;
            //        this.textEditCompanyPhone1.Properties.ReadOnly = false;
            //        this.textEditCompanyWebSiteAddress.Properties.ReadOnly = false;
            //        this.spinEditCompanyPayDate.Properties.ReadOnly = false;
            //        this.dateEditCompanyExchangeDay.Properties.Buttons[0].Visible = true;

            //        this.lookUpEditBusiness.Properties.ReadOnly = false;
            //        this.lookUpEditBusiness.Properties.Buttons[0].Visible = true;

            //        this.simpleButtonAdd.Enabled = true;
            //        this.simpleButtonDel.Enabled = true;
            //        this.simpleButtonModify.Enabled = true;
            //        this.simpleButtonSave.Enabled = false;
            //        this.simpleButtonUndo.Enabled = false;
            //        break;

            //    case "view":
            //        this.CompanyIdTextEdit.Properties.ReadOnly = true;
            //        this.CompanyAddressTextEdit.Properties.ReadOnly = true;
            //        this.CompanyFaxTextEdit.Properties.ReadOnly = true;
            //        this.CustomerShortNameTextEdit.Properties.ReadOnly = true;
            //        this.CustomerFullNameTextEdit.Properties.ReadOnly = true;
            //        //this.CompanyPayDayTextEdit.Properties.ReadOnly = true;
            //        this.textEditCompanyPhone.Properties.ReadOnly = true;
            //        this.CompanyDescriptionMemoEdit.Rtf.ReadOnly = true;
            //        this.comboBoxEditCompanyLevel.Properties.ReadOnly = true;
            //        this.textEditCompanyEMail.Properties.ReadOnly = true;
            //        this.dateEditCompanyExchangeDay.Properties.ReadOnly = true;
            //        this.textEditCompanyJinChuAddress.Properties.ReadOnly = true;
            //        this.textEditCompanyManager.Properties.ReadOnly = true;
            //        this.textEditCompanyMobile.Properties.ReadOnly = true;
            //        this.textEditCompanyNumber.Properties.ReadOnly = true;
            //        this.textEditCompanyPhone1.Properties.ReadOnly = true;
            //        this.textEditCompanyWebSiteAddress.Properties.ReadOnly = true;
            //        this.spinEditCompanyPayDate.Properties.ReadOnly = true;
            //        this.dateEditCompanyExchangeDay.Properties.Buttons[0].Visible = false;

            //        this.lookUpEditBusiness.Properties.ReadOnly = true;
            //        this.lookUpEditBusiness.Properties.Buttons[0].Visible = false;

            //        this.simpleButtonAdd.Enabled = false;
            //        this.simpleButtonDel.Enabled = false;
            //        this.simpleButtonModify.Enabled = false;
            //        this.simpleButtonSave.Enabled = false;
            //        this.simpleButtonUndo.Enabled = false;
            //        break;

            //    default:
            //        break;
            //}
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.customerManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.customerManager.HasRowsAfter(this._customer);
        }
        protected override bool HasRowsPrev()
        {
            return this.customerManager.HasRowsBefore(this._customer);
        }
        #endregion

        #region 編輯值改變事件
        private void CompanyName1TextEdit_EditValueChanged(object sender, EventArgs e)
        {
            this.CustomerShortNameTextEdit.Text = this.CustomerFullNameTextEdit.Text.Length > 6 ? this.CustomerFullNameTextEdit.Text.Substring(0, 6) : this.CustomerFullNameTextEdit.Text;
        }
        #endregion

        #region 勾選事件
        private void checkEditSongHuoTongDengji_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit checkEdit = sender as DevExpress.XtraEditors.CheckEdit;

            if (checkEdit != null)
            {
                if (checkEdit.Checked == true)
                {
                    this.textEditCompanyJinChuAddress.EditValue = this.CompanyAddressTextEdit.EditValue;
                }
            }
        }
        #endregion

        #region gridview 的點擊事件
        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                cc = this.bindingSourceCustomsContact.Current as Model.CustomerContact;

                if (cc != null)
                {
                    this.textEditEmail.Text = cc.CustomerContactEmail;
                    this.textEditMobile.Text = cc.CustomerContactMobile;
                    this.textEditName.Text = cc.CustomerContactName;
                    this.textEditPhone.Text = cc.CustomerContactPhone;
                    this.memoEditRemarks.Text = cc.CustomerContactRemark;
                }
            }
        }
        #endregion

        #region 添加按鈕
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            if (action != "view")
            {
                ClearText();
                SetTextEditReadOnly(false);
                _ac = "insert";
                cc = new Book.Model.CustomerContact();

                this.simpleButtonAdd.Enabled = false;
                this.simpleButtonSave.Enabled = true;
                this.simpleButtonModify.Enabled = false;
                this.simpleButtonDel.Enabled = false;
                this.simpleButtonUndo.Enabled = true;
            }
        }
        #endregion

        #region 刪除按鈕
        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
            if (action != "view")
            {
                if (MessageBox.Show("确定删除", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this._customer.Contacts.Remove(this.bindingSourceCustomsContact.Current as Model.CustomerContact);
                }
            }
        }
        #endregion

        #region 修改按鈕
        private void simpleButtonModify_Click(object sender, EventArgs e)
        {
            if (action != "view")
            {
                cc = this.bindingSourceCustomsContact.Current as Model.CustomerContact;
                if (cc != null)
                {
                    SetTextEditReadOnly(false);
                    _ac = "modify";

                    this.simpleButtonAdd.Enabled = false;
                    this.simpleButtonDel.Enabled = false;
                    this.simpleButtonModify.Enabled = false;
                    this.simpleButtonSave.Enabled = true;
                    this.simpleButtonUndo.Enabled = true;

                    this.textEditEmail.Text = cc.CustomerContactEmail;
                    this.textEditMobile.Text = cc.CustomerContactMobile;
                    this.textEditName.Text = cc.CustomerContactName;
                    this.textEditPhone.Text = cc.CustomerContactPhone;
                    this.memoEditRemarks.Text = cc.CustomerContactRemark;
                }
            }
        }
        #endregion

        #region 保存按鈕
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            if (action != "view")
            {
                this.simpleButtonAdd.Enabled = true;
                this.simpleButtonDel.Enabled = true;
                this.simpleButtonModify.Enabled = true;
                this.simpleButtonSave.Enabled = false;
                this.simpleButtonUndo.Enabled = false;

                cc.Customer = this._customer;
                cc.CustomerContactEmail = this.textEditEmail.Text;
                cc.CustomerContactMobile = this.textEditMobile.Text;
                cc.CustomerContactName = this.textEditName.Text;
                cc.CustomerContactPhone = this.textEditPhone.Text;
                cc.CustomerContactRemark = this.memoEditRemarks.Text;

                if (_ac == "insert")
                {
                    cc.CustomerContactId = Guid.NewGuid().ToString();
                    cc.Customer = this._customer;
                    this._customer.Contacts.Add(cc);
                }

                this.bindingSourceCustomsContact.DataSource = this._customer.Contacts;

                this.gridControl1.RefreshDataSource();
                SetTextEditReadOnly(true);
            }
        }
        #endregion

        #region 撤銷按鈕
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
        #endregion

        #region 清屏方法
        void ClearText()
        {
            this.textEditEmail.Text = "";
            this.textEditMobile.Text = "";
            this.textEditName.Text = "";
            this.textEditPhone.Text = "";
            this.memoEditRemarks.Text = "";
        }
        #endregion

        #region 設置文本框是否為只讀
        void SetTextEditReadOnly(bool flag)
        {
            this.textEditEmail.Properties.ReadOnly = flag;
            this.textEditMobile.Properties.ReadOnly = flag;
            this.textEditName.Properties.ReadOnly = flag;
            this.textEditPhone.Properties.ReadOnly = flag;
            this.memoEditRemarks.Properties.ReadOnly = flag;
        }
        #endregion

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            _customer = this.bindingSourceCustom.Current as Model.Customer;
            Refresh();
        }

        public static Model.Customer cstomer;
        private void btn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (customerManager.HasRows())
            {
                ListForm f = new ListForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    this._customer = cstomer;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

    }
}