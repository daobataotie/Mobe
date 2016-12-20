using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using Book.BL;
namespace Book.UI.Workflow.Tablem
{
    /// <summary>
    ///表单编辑
    /// </summary>
    public partial class EditForm : BaseEditForm
    {
        /// <summary>
        /// 表单管理
        /// </summary>
        private Book.BL.TablesManager tablemanage = new Book.BL.TablesManager();
        /// <summary>
        /// 表单
        /// </summary>
        private Model.Tables tablem = null;
        public EditForm()
        {
            InitializeComponent();

            //this.invalidValueExceptions.Add(Model.Tables.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textEditTableid));
            //this.requireValueExceptions.Add(Model.Tables.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.textEditTableid));
            this.requireValueExceptions.Add(Model.Tables.PRO_Tablename, new AA(Properties.Resources.RequireDataForNames, this.textEditTablename));
            dbtable();
            this.action = "insert";

        }
        /// <summary>
        /// 填充数据库表
        /// </summary>
        private void dbtable()
        {

            comboBoxEdittable.Properties.Items.Add("TechonlogyHeader");

            // comboBoxEdittable.Properties.Items.Add("WorkHouse");

        }


        public EditForm(Book.Model.Tables tm)
            : this()
        {
            this.tablem = tm;
            this.action = "update";
        }

        public EditForm(Book.Model.Tables tm, string action)
            : this()
        {
            this.tablem = tm;
            this.action = action;
        }

        #region 重写部分

        public override object EditedItem
        {


            get
            {
                return this.tablem;
            }

        }
        /// <summary>
        /// 重写表单删除
        /// </summary>
        protected override void Delete()
        {
            if (this.tablem == null) { return; }
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            { this.tablemanage.Delete(this.tablem.TablesID); }
        }



        protected override void MoveFirst()
        {
            base.MoveFirst();
        }

        protected override void MoveLast()
        {
            base.MoveLast();
        }

        protected override void AddNew()
        {
            this.tablem = new Book.Model.Tables();


        }



        protected override void MoveNext()
        {
            base.MoveNext();
        }



        protected override void MovePrev()
        {
            base.MovePrev();
        }


        protected override bool HasRows()
        {
            // return this.pricingwanymanager.HasRows();
            return this.tablemanage.HasRows();
        }



        protected override bool HasRowsNext()
        {
            return base.HasRowsNext();
        }



        protected override bool HasRowsPrev()
        {
            return base.HasRowsPrev();


        }

        public override void Refresh()
        {
            #region MyRegion
            if (this.tablem == null)
            {
                this.tablem = new Book.Model.Tables();
                this.action = "insert";
            }

            this.textEditTableid.Text = this.tablem.TablesID;
            this.textEditTablename.Text = this.tablem.Tablename;
            this.comboBoxEdittable.SelectedText = this.tablem.TableCode;
            // this.memoEditTablebewrite.Text = this.tablem.Tabledescription;

            switch (this.action)
            {
                case "insert":


                    this.textEditTableid.Properties.ReadOnly = false;
                    this.textEditTablename.Properties.ReadOnly = false;
                    this.comboBoxEdittable.Properties.ReadOnly = false;
                    //  this.memoEditTablebewrite.Properties.ReadOnly = false;

                    break;


                case "update":


                    this.textEditTableid.Properties.ReadOnly = false;
                    this.textEditTablename.Properties.ReadOnly = false;
                    this.comboBoxEdittable.Properties.ReadOnly = false;
                    //  this.memoEditTablebewrite.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.textEditTableid.Properties.ReadOnly = true;
                    this.textEditTablename.Properties.ReadOnly = true;
                    this.comboBoxEdittable.Properties.ReadOnly = true;
                    //  this.memoEditTablebewrite.Properties.ReadOnly = true;
                    break;

                default: break;
                //    default:
                //        break;
            }
            #endregion
            base.Refresh();
        }

        /// <summary>
        /// 保存
        /// </summary>
        protected override void Save()
        {
            this.tablem.TablesID = this.textEditTableid.Text;
            this.tablem.Tablename = this.textEditTablename.Text;
            this.tablem.TableCode = this.comboBoxEdittable.Text;
            // this.tablem.Tabledescription = this.memoEditTablebewrite.Text;

            switch (this.action)
            {
                case "insert":
                    this.tablemanage.Insert(this.tablem);
                    break;

                case "update": this.tablemanage.Update(this.tablem); break;


            }

            this.tablem.TablesID = "";

        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.textEditTableid, this });
        }

        #endregion
    }
}