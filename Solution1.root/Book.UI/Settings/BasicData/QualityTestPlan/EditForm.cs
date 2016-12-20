using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.QualityTestPlan
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-11-05
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        protected BL.QualityTestPlanManager qualityTextPlanManager = new Book.BL.QualityTestPlanManager();
        private Model.QualityTestPlan qualityTextPlan = null;       

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.QualityTestPlan.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.textEditId));
            this.requireValueExceptions.Add(Model.QualityTestPlan.PROPERTY_QUALITYTESTPLANNAME, new AA(Properties.Resources.RequireDataForNames, this.QualityTestPlanNametextEdit));

            this.requireValueExceptions.Add(Model.QualityTestPlan.PROPERTY_QUALITYTESTCODE, new AA(Properties.Resources.RequireDataForCode, this.QualityTestPlanCodetextEdit));

            this.requireValueExceptions.Add(Model.QualityTestPlan.PROPERTY_QUALITYTESTSTANDARDCODE, new AA(Properties.Resources.RequireDataForStandardCode, this.QualityTestStandardCodetextEdit));
            //this.requireValueExceptions.Add(Model.QualityTestPlan.PROPERTY_CREATEMAN, new AA(Properties.Resources.RequireDataForCreateMan, this.CreateMantextEdit));
            //this.requireValueExceptions.Add(Model.QualityTestPlan.PROPERTY_ENTERMAN, new AA(Properties.Resources.RequireDataForEnterMan, this.EnterMantextEdit));
            //this.requireValueExceptions.Add(Model.QualityTestPlan.PROPERTY_CHANGEMAN, new AA(Properties.Resources.RequireDataForChangeMan, this.ChangeMantextEdit));
            this.invalidValueExceptions.Add(Model.QualityTestPlan.PROPERTY_ID, new AA(Properties.Resources.EntityExists,this.textEditId));
            this.invalidValueExceptions.Add(Model.QualityTestPlan.PROPERTY_QUALITYTESTPLANNAME, new AA(Properties.Resources.EntityName, this.QualityTestPlanNametextEdit));

            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
            this.action = "insert";

        }

        private void EditForm_Load(object sender, EventArgs e)
        {


        }
        public EditForm(Model.QualityTestPlan qualitytextplan)
            : this()
        {
            //if (custom == null)
            //    throw new ArithmeticException();
            //this.invalidValueExceptions.Add(Model.CustomInspectionRule.PROPERTY_CUSTOMINSPECTIONRULEID, new AA("已有相同编号的项存在", this.CustomInspectionRuleIdTextEdit));

            this.qualityTextPlan = qualitytextplan ;            
            this.action = "update";
        }
        public EditForm(Model.QualityTestPlan qualitytextplan, string action)
            : this()
        {
            //if (custom == null)
            //    throw new ArithmeticException();
            this.qualityTextPlan =  qualitytextplan;
            this.action = action;
        }
        protected override void Save()
       {  //           this.invalidValueExceptions.Add(Model.QualityTestPlan.PROPERTY_QUALITYTESTPLANNAME, new AA("已有相同名称的项存在", this.QualityTestPlanNametextEdit));
            this.qualityTextPlan.Id= this.textEditId.Text;
            this.qualityTextPlan.QualityTestPlanName = this.QualityTestPlanNametextEdit.Text;
            this.qualityTextPlan.QualityTestCode = this.QualityTestPlanCodetextEdit.Text;
            this.qualityTextPlan.QualityTestStandardCode = this.QualityTestStandardCodetextEdit.Text;
            this.qualityTextPlan.EnterMan = this.EnterMantextEdit.Text;
            this.qualityTextPlan.CreateMan = this.CreateMantextEdit.Text;
            this.qualityTextPlan.ChangeMan = this.ChangeMantextEdit.Text;
            switch (this.action)
            {
                case "insert":
                    this.qualityTextPlanManager.Insert(this.qualityTextPlan);
                    break;
                case "update":
                    //this.qualityTextPlan.QualityTestPlanId = oldId;
                    this.qualityTextPlanManager.Update(this.qualityTextPlan);
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
                return this.qualityTextPlan;
            }
        }

        #endregion

        #region Virtual Method

        protected override void Delete()
        {
            if (this.qualityTextPlan == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.qualityTextPlanManager.Delete(this.qualityTextPlan);
          //  this.qualityTextPlan = this.qualityTextPlanManager.GetNext(this.customInspection);
            if (this.qualityTextPlan == null)
            {
             //   this.qualityTextPlan = this.qualityTextPlanManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.qualityTextPlan = new Model.QualityTestPlan();
            //this.qualityTextPlan.QualityTestPlanId = this.qualityTextPlanManager.GetId();
        }

        protected override void Undo()
        {
        }

        protected override void MovePrev()
        {
            Model.QualityTestPlan qualitytestplan = this.qualityTextPlanManager.GetPrev(this.qualityTextPlan);
            if (qualitytestplan == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.qualityTextPlan = qualitytestplan;


        }

        protected override void MoveNext()
        {
            Model.QualityTestPlan qualitytestplan = this.qualityTextPlanManager.GetNext(this.qualityTextPlan);
            if (qualitytestplan == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.qualityTextPlan = qualitytestplan;
               
        }

        protected override void MoveFirst()
        {
            this.qualityTextPlan = this.qualityTextPlanManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this.qualityTextPlan = this.qualityTextPlanManager.GetLast();
        }
        public override void Refresh()
        {

            if (this.qualityTextPlan == null)
            {
                this.qualityTextPlan = new Book.Model.QualityTestPlan();
                this.action = "insert";
            }

            this.textEditId.Text =(string.IsNullOrEmpty(this.qualityTextPlan.Id)? this.qualityTextPlan.QualityTestPlanId:this.qualityTextPlan.Id);
            this.QualityTestPlanNametextEdit .Text = this.qualityTextPlan.QualityTestPlanName;
            this.QualityTestPlanCodetextEdit.Text = this.qualityTextPlan.QualityTestCode;
            this.QualityTestStandardCodetextEdit.Text = this.qualityTextPlan.QualityTestStandardCode;
            this.CreateMantextEdit.Text = this.qualityTextPlan.CreateMan;
            this.ChangeMantextEdit.Text = this.qualityTextPlan.ChangeMan;
            this.EnterMantextEdit.Text=this.qualityTextPlan.EnterMan;
            switch (this.action)
            {
                case "insert":
                    this.textEditId.Properties.ReadOnly = false;
                    this.QualityTestPlanNametextEdit.Properties.ReadOnly = false;
                    this.QualityTestPlanCodetextEdit.Properties.ReadOnly = false;
                    this.QualityTestStandardCodetextEdit.Properties.ReadOnly = false;
                    this.CreateMantextEdit.Properties.ReadOnly = false;
                    this.ChangeMantextEdit.Properties.ReadOnly = false;
                    this.EnterMantextEdit.Properties.ReadOnly = false;
                    break;

               case "update":
                    this.textEditId.Properties.ReadOnly = false;
                    this.QualityTestPlanNametextEdit.Properties.ReadOnly = false;
                    this.QualityTestPlanCodetextEdit.Properties.ReadOnly = false;
                    this.QualityTestStandardCodetextEdit.Properties.ReadOnly = false;
                    this.CreateMantextEdit.Properties.ReadOnly = false;
                    this.ChangeMantextEdit.Properties.ReadOnly = false;
                    this.EnterMantextEdit.Properties.ReadOnly = false;
                    break;

                case "view":

                    this.textEditId.Properties.ReadOnly =true ;
                    this.QualityTestPlanNametextEdit.Properties.ReadOnly = true;
                    this.QualityTestPlanCodetextEdit.Properties.ReadOnly = true;
                    this.QualityTestStandardCodetextEdit.Properties.ReadOnly = true;
                    this.CreateMantextEdit.Properties.ReadOnly = true;
                    this.ChangeMantextEdit.Properties.ReadOnly = true;
                    this.EnterMantextEdit.Properties.ReadOnly = true;

                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.qualityTextPlanManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.qualityTextPlanManager.HasRowsBefore(this.qualityTextPlan);
        }

        protected override bool HasRowsNext()
        {
            return this.qualityTextPlanManager.HasRowsAfter(this.qualityTextPlan);
        }

        protected override void IMECtrl()
        {
           // Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.qualityTextPlan, this });

        }
        #endregion

        private void EditForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
