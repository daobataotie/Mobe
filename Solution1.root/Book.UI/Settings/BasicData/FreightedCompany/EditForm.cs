using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.FreightedCompany
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-24
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm :BaseEditForm
    {
        private BL.FreightedCompanyManager companyManager = new Book.BL.FreightedCompanyManager();
        private Model.FreightedCompany company = new Book.Model.FreightedCompany();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.FreightedCompany.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.textEditId));
            this.invalidValueExceptions.Add(Model.FreightedCompany.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textEditId));
            this.requireValueExceptions.Add(Model.FreightedCompany.PROPERTY_FREIGHTEDCOMPANYNAME, new AA(Properties.Resources.RequireDataForNames, this.textEditName));
            this.action = "insert";

        }
        public EditForm(Model.FreightedCompany company)
            :this()
        {
            this.company = company;
            this.action = "update";
        }
        public EditForm(Model.FreightedCompany company, string action)
            : this()
           
        {
            this.company = company;
            this.action = action;
        }

        protected override void Save()
        {
            this.company.Id = this.textEditId.Text;
            this.company.FreightedCompanyName = this.textEditName.Text;
            this.company.FreightedCompanyAddress = this.textEditAdd.Text;
            this.company.Telphone = this.textEditTel.Text;
            this.company.Description = this.memoEditDescription.Text;
            switch (this.action)
            {
                case "insert":
                    this.companyManager.Insert(this.company);
                    break;
                case "update":                  
                    this.companyManager.Update(this.company);
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
                return this.company;
            }
        }

        #endregion

        #region Virtual Method

        protected override void Delete()
        {
            if (this.company == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.companyManager.Delete(this.company.FreightedCompanyId);
            this.company = this.companyManager.GetNext(this.company);
            if (this.company == null)
            {
                this.company = this.companyManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.company = new Model.FreightedCompany();
            //this.company.CustomInspectionRuleId = this.companyManager.GetId();
        }

        protected override void Undo()
        {
        }

        protected override void MovePrev()
       {
           Model.FreightedCompany company = this.companyManager.GetPrev(this.company);
           if (company == null)
               throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

           this.company = company;

        }

        protected override void MoveNext()
        {
            Model.FreightedCompany company = this.companyManager.GetNext(this.company);
            if (company == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.company = company;
        }

        protected override void MoveFirst()
        {
           this.company = this.companyManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if(this.company==null)
            this.company = this.companyManager.GetLast();
        }
        public override void Refresh()
        {

            if (this.company == null)
            {
                this.company = new Book.Model.FreightedCompany();
                this.action = "insert";
            }
            this.textEditId.Text = (string.IsNullOrEmpty(this.company.Id) ? this.company.FreightedCompanyId : this.company.Id);
            this.textEditName.Text = this.company.FreightedCompanyName;
            this.textEditAdd.Text = this.company.FreightedCompanyAddress;
            this.textEditTel.Text = this.company.Telphone;
            this.memoEditDescription.Text = this.company.Description;
            switch (this.action)
            {
                case "insert":
                    this.textEditId.Properties.ReadOnly = false;
                    this.textEditName.Properties.ReadOnly = false;
                    this.textEditAdd.Properties.ReadOnly = false;
                    this.textEditTel.Properties.ReadOnly = false;
                    this.memoEditDescription.Properties.ReadOnly = false;
                    break;

                case "update":

                    this.textEditId.Properties.ReadOnly = false;
                    this.textEditName.Properties.ReadOnly = false;
                    this.textEditAdd.Properties.ReadOnly = false;
                    this.textEditTel.Properties.ReadOnly = false;
                    this.memoEditDescription.Properties.ReadOnly = false;
                    break;

                case "view":

                    this.textEditId.Properties.ReadOnly = true;
                    this.textEditName.Properties.ReadOnly = true;
                    this.textEditAdd.Properties.ReadOnly = true;
                    this.textEditTel.Properties.ReadOnly = true;
                    this.memoEditDescription.Properties.ReadOnly = true;
                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.companyManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
             return this.companyManager.HasRowsBefore(this.company);
        }

        protected override bool HasRowsNext()
        {
            return this.companyManager.HasRowsAfter(this.company);
        }

        protected override void IMECtrl()
        {       
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditId });
           
        }
        #endregion
    }
}
