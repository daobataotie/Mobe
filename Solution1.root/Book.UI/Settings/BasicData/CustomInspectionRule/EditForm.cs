using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.CustomInspectionRule
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 设定-基础设定-自定义抽检规则
   // 文 件 名：EditForm
   // 编 码 人: 马艳军                   完成时间:2009-10-10
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {

        #region 變臉對象定義
        protected BL.CustomInspectionRuleManager customInspectionRuleManager = new Book.BL.CustomInspectionRuleManager();
        private Model.CustomInspectionRule customInspection = null;
        #endregion

        #region 無慘構造函數
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.CustomInspectionRule.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.TextEditId));
            this.requireValueExceptions.Add(Model.CustomInspectionRule.PROPERTY_CUSTOMINSPECTIONRULENAME, new AA(Properties.Resources.RequireDataForNames, this.CustomInspectionRuleNamedTextEdit));           
            this.invalidValueExceptions.Add(Model.CustomInspectionRule.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.TextEditId));
            this.invalidValueExceptions.Add(Model.CustomInspectionRule.PROPERTY_CUSTOMINSPECTIONRULENAME, new AA(Properties.Resources.EntityName, this.CustomInspectionRuleNamedTextEdit));

            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));          
            this.action = "insert";

        }
        #endregion

        #region 帶參數的構造函數
        /// <param name="custom">model 對象</param>
        public EditForm(Model.CustomInspectionRule custom)
            : this()
        {
            //if (custom == null)
            //    throw new ArithmeticException();
            //this.invalidValueExceptions.Add(Model.CustomInspectionRule.PROPERTY_CUSTOMINSPECTIONRULEID, new AA("已有相同编号的项存在", this.CustomInspectionRuleIdTextEdit));
            //this.invalidValueExceptions.Add(Model.CustomInspectionRule.PROPERTY_CUSTOMINSPECTIONRULENAME, new AA("已有相同名称的项存在", this.CustomInspectionRuleNamedTextEdit));
           this.customInspection = custom;           
           this.action = "update";
        }
        /// <param name="custom">model對象</param>
        /// <param name="action">當前動作</param>
        public EditForm(Model.CustomInspectionRule custom, string action)
            : this()
        {
            //if (custom == null)
            //    throw new ArithmeticException();
            this.customInspection = custom;
            this.action = action;
        }
        #endregion 

        #region 重載父類的方法
        protected override void Save()
        {
            this.customInspection.Id = this.TextEditId.Text;
            this.customInspection.CustomInspectionRuleName = this.CustomInspectionRuleNamedTextEdit.Text;
            switch (this.action)
            {
                case "insert":
                    this.customInspectionRuleManager.Insert(this.customInspection);
                    break;
                case "update":
                    //this.customInspection.CustomInspectionRuleId = oldId;
                    this.customInspectionRuleManager.Update(this.customInspection);
                    break;
                default:
                    break;
            }
        }
        #endregion


        #region Properties

        public override object EditedItem
        {
            get
            {
                return this.customInspection;
            }
        }

        #endregion

        #region Virtual Method

        protected override void Delete()
        {
            if (this.customInspection == null)  
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)               
                return;
            this.customInspectionRuleManager.Delete(this.customInspection);
            this.customInspection = this.customInspectionRuleManager.GetNext(this.customInspection);
            if (this.customInspection == null)
            {
                this.customInspection = this.customInspectionRuleManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.customInspection = new Model.CustomInspectionRule();
            //this.customInspection.CustomInspectionRuleId = this.customInspectionRuleManager.GetId();
        }

        protected override void Undo()
        {
        }

        protected override void MovePrev()
        {
            Model.CustomInspectionRule customInspection = this.customInspectionRuleManager.GetPrev(this.customInspection);
            if (customInspection == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.customInspection = customInspection;

        }

        protected override void MoveNext()
        {
            Model.CustomInspectionRule customInspection = this.customInspectionRuleManager.GetNext(this.customInspection);
            if (customInspection == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.customInspection = customInspection;
        }

        protected override void MoveFirst()
        {
            this.customInspection = this.customInspectionRuleManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this.customInspection = this.customInspectionRuleManager.GetLast();
        }
        public override void Refresh()
        {

            if (this.customInspection == null)
            {
                this.customInspection = new Book.Model.CustomInspectionRule();                
                this.action = "insert";
            }

            this.TextEditId.Text =(string.IsNullOrEmpty(this.customInspection.Id )? this.customInspection.CustomInspectionRuleId:this.customInspection.Id);
            this.CustomInspectionRuleNamedTextEdit.Text = this.customInspection.CustomInspectionRuleName;
            switch (this.action)
            {
                case "insert":
                    this.TextEditId.Properties.ReadOnly = false;
                    this.CustomInspectionRuleNamedTextEdit.Properties.ReadOnly = false;
                    break;

                case "update":

                    this.TextEditId.Properties.ReadOnly = false ;
                    this.CustomInspectionRuleNamedTextEdit.Properties.ReadOnly = false;
                    break;

                case "view":

                    this.TextEditId.Properties.ReadOnly = true;
                    this.CustomInspectionRuleNamedTextEdit.Properties.ReadOnly = true;

                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.customInspectionRuleManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.customInspectionRuleManager.HasRowsBefore(this.customInspection);
        }

        protected override bool HasRowsNext()
        {
            return this.customInspectionRuleManager.HasRowsAfter(this.customInspection);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.CustomInspectionRuleNamedTextEdit, this });

        }
        #endregion
    }
}
