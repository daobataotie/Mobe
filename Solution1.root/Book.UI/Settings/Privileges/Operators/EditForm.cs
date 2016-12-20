using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Settings.Privileges.Operators
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-10-20
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        protected BL.OperatorsManager operatorsManager = new Book.BL.OperatorsManager();
        private Model.Operators _operators;


        #region 构造函数，初始化
        public EditForm()
        {
            InitializeComponent();
            //this.requireValueExceptions.Add(Model.Operators.PROPERTY_ID, new AA("請填寫編號", this.textEditId));

            this.requireValueExceptions.Add(Model.Operators.PROPERTY_OPERATORNAME, new AA(Properties.Resources.RequireDataForName, this.textEditLoginName));
            this.requireValueExceptions.Add(Model.Operators.PROPERTY_PASSWORD, new AA(Properties.Resources.password, this.textEditPassWord));
            //this.invalidValueExceptions.Add(Model.Operators.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textEditId));
            this.invalidValueExceptions.Add(Model.Operators.PROPERTY_PASSWORD, new AA(Properties.Resources.TwicePassDifference, this.textEditRePassWord));

            this.NewChooseEmplyee.Choose = new ChooseEmployee(EmployeeParameters.ALL);
            this.action = "insert";
        }

        public EditForm(Model.Operators operators)
            : this() 
        {
            this._operators = operators;
            this.action = "update";

        }

        public EditForm(string employeeid) 
        {
            this._operators = this.operatorsManager.Get(employeeid);

            this.action = "update";
        }
        #endregion 

        private void EditForm_Load(object sender, EventArgs e)
        {
        }
        #region 重写父类方法

        public override object EditedItem
        {
            get
            {
                return this._operators;
            }
        }

        protected override void Delete()
        {            
            this._operators.Password = null;
            this.operatorsManager.Update(this._operators);
            this._operators = this.operatorsManager.GetNext(this._operators);
            if (this._operators == null)
            {
                this._operators = this.operatorsManager.GetLast();
            }
        }

        protected override void Save()
        {
            Model.Employee emp =  this.NewChooseEmplyee.EditValue as Model.Employee;
            
            if (emp != null)
            {
                this._operators.EmployeeId = emp.EmployeeId;
            }

            this._operators.OperatorName = this.textEditLoginName.Text;
            //this._operators.Id = this.textEditId.Text;

            string pass1 = this.textEditPassWord.Text;
            string pass2 = this.textEditRePassWord.Text;   
           
            if (pass1 != pass2)
            {
                throw new Helper.InvalidValueException(Model.Operators.PROPERTY_PASSWORD);
            }            
            this._operators.Password = pass1;


            switch (this.action)
            {
                case "insert":
                    this.operatorsManager.Insert(this._operators);
                    break;
                case "update":
                    this.operatorsManager.Update(this._operators);
                    break;
                default:
                    break;
            }
        }

        

        protected override void AddNew()
        {
            this._operators = new Model.Operators();
            this.action = "insert";
        }

        protected override void MoveNext()
        {
            Model.Operators employee = this.operatorsManager.GetNext(this._operators);
            if (employee == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._operators = employee;
        }

        protected override void MovePrev()
        {
            Model.Operators employee = this.operatorsManager.GetPrev(this._operators);
            if (employee == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._operators = employee;
        }

        protected override void MoveFirst()
        {
            this._operators = this.operatorsManager.GetFirst();
        }

        protected override void MoveLast()
        {
            //if(this._operators==null||)
            this._operators = this.operatorsManager.GetLast();
        }

        public override void Refresh()
        {
            if (this._operators == null) 
            {
                this.AddNew();                
            }
            //this.textEditId.Text = string.IsNullOrEmpty(this._operators.Id) ? this._operators.OperatorsId : this._operators.Id;
            this.textEditLoginName.Text = this._operators.OperatorName;
            this.NewChooseEmplyee.EditValue = this._operators.Employee as Model.Employee;

            switch (this.action)
            {
                case "insert":                   
                    this.textEditLoginName.Properties.ReadOnly = false;
                    this.textEditPassWord.Properties.ReadOnly = false;
                    this.textEditRePassWord.Properties.ReadOnly = false;
                    break;
                case "update":                    
                    this.textEditLoginName.Properties.ReadOnly = false;
                    this.textEditPassWord.Properties.ReadOnly = false;
                    this.textEditRePassWord.Properties.ReadOnly = false;
                    break;
                case "view":                    
                    this.textEditLoginName.Properties.ReadOnly = true ;
                    this.textEditPassWord.Properties.ReadOnly = true ;
                    this.textEditRePassWord.Properties.ReadOnly = true ;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.operatorsManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.operatorsManager.HasRowsAfter(this._operators);
        }

        protected override bool HasRowsPrev()
        {
            return this.operatorsManager.HasRowsBefore(this._operators);
        }
        #endregion
    }
}