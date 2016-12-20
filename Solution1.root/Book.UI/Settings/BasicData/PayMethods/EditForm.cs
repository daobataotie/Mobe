using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.PayMethods
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-28
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        protected BL.PayMethodManager paymethodManager = new Book.BL.PayMethodManager();
        private Model.PayMethod paymethod;
        public EditForm()
        {
            InitializeComponent();
            
            this.requireValueExceptions.Add(Model.PayMethod.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.TextEditId));
            this.requireValueExceptions.Add(Model.PayMethod.PROPERTY_PAYMETHODNAME, new AA(Properties.Resources.RequireDataForName, this.PayMethodNameTextEdit));
            this.invalidValueExceptions.Add(Model.PayMethod.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.TextEditId));
            this.action = "insert";
        }

        public EditForm(Model.PayMethod pmethod):this()
        {
            //if (paymethod == null)
            //    throw new ArithmeticException();
            this.paymethod = pmethod;    
            this.action = "update";
        }

        public EditForm(Book.Model.PayMethod pmethod, string action)
            : this()
        {
            //if(cmpy == null)
            //    throw new ArithmeticException();
            this.paymethod = pmethod;
            this.action = action;
        }
      
        private void EditForm_Load(object sender, EventArgs e)
        {
           
        }
        #region Override

        public override object EditedItem
        {
            get
            {
                return this.paymethod;
            }
        }
        protected override void Delete()
        {
            if (this.paymethod == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.paymethodManager.Delete(this.paymethod);
            this.paymethod = this.paymethodManager.GetNext(this.paymethod);
            if (this.paymethod == null)
            {
                this.paymethod = this.paymethodManager.GetLast();
            }
        }

        protected override void Save()
        {
            this.paymethod.Id = this.TextEditId.Text;
            this.paymethod.PayMethodName = this.PayMethodNameTextEdit.Text;
            this.paymethod.PayMethodDescription = this.PayMethodDescriptionMemoEdit.Text;
            switch (this.action)
            {
                case "insert":
                    this.paymethodManager.Insert(this.paymethod);
                    break;
                case "update":    
                    this.paymethodManager.Update(this.paymethod);  
                    break;
                default:
                    break;
            }

        }
        #endregion     
  
        protected override void AddNew()
        {
            this.paymethod = new Model.PayMethod();
            //this.paymethod.PayMethodId = this.paymethodManager.GetId();
        }

        protected override void MoveNext()
        {
            Model.PayMethod paymethod = this.paymethodManager.GetNext(this.paymethod);
            if (paymethod == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.paymethod = paymethod;
        }

        protected override void MovePrev()
        {
            Model.PayMethod paymethod = this.paymethodManager.GetPrev(this.paymethod);
            if (paymethod == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.paymethod = paymethod;
        }

        protected override void MoveFirst()
        {
            this.paymethod = this.paymethodManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this.paymethod = this.paymethodManager.GetLast();
        }

        public override void Refresh()
        {
            if (this.paymethod == null)
            {
                this.paymethod = new Book.Model.PayMethod();
                this.action = "insert";
            }

            this.TextEditId.Text =(string.IsNullOrEmpty(this.paymethod.Id)?this.paymethod.PayMethodId:this.paymethod.Id);
            this.PayMethodNameTextEdit.Text = this.paymethod.PayMethodName;
            this.PayMethodDescriptionMemoEdit.Text = this.paymethod.PayMethodDescription; 
            
            switch (this.action)
            {
                case "insert":
                    this.TextEditId.Properties.ReadOnly = false;
                    this.PayMethodNameTextEdit.Properties.ReadOnly = false;
                    this.PayMethodDescriptionMemoEdit.Properties.ReadOnly = false;
                    break;

                case "update":

                    this.TextEditId.Properties.ReadOnly = false ;
                    this.PayMethodNameTextEdit.Properties.ReadOnly = false;
                    this.PayMethodDescriptionMemoEdit.Properties.ReadOnly = false;
                    break;

                case "view":

                    this.TextEditId.Properties.ReadOnly = true;
                    this.PayMethodNameTextEdit.Properties.ReadOnly = true;
                    this.PayMethodDescriptionMemoEdit.Properties.ReadOnly = true;
                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.paymethodManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.paymethodManager.HasRowsAfter(this.paymethod);
        }

        protected override bool HasRowsPrev()
        {
            return this.paymethodManager.HasRowsBefore(this.paymethod);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.TextEditId, this });
        }
    }
}