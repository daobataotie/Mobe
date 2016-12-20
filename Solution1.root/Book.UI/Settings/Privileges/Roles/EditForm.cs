using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.Privileges.Roles
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
        private Model.Role role = null;

        protected BL.RoleManager roleManager = new Book.BL.RoleManager();

        /// <summary>
        /// 无参构造
        /// </summary>
        public EditForm()
        {
            InitializeComponent();
            
            //this.requireValueExceptions.Add(Model.Role.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.RoleIdTextEdit));
            this.requireValueExceptions.Add(Model.Role.PRO_RoleName, new AA(Properties.Resources.RequireDataForName, this.RoleNameTextEdit));

            //this.invalidValueExceptions.Add(Model.Role.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.RoleIdTextEdit));
            this.action = "insert";
        }
        #region 带参构造
        public EditForm(Model.Role role) : this(role.RoleId) 
        {
            this.action = "update";
        }

        public EditForm(string roleId) :this()
        {
            this.role = this.roleManager.Get(roleId);
            this.action = "update";
        }

        public EditForm(Book.Model.Role role, string action)
            : this()
        {
            this.role = role;
            this.action = action;
        }
        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {
        }
        #region 重写父类方法
        protected override void Delete()
        {
            if (this.role == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.roleManager.Delete(this.role);
            this.role = this.roleManager.GetNext(this.role);
            if (this.role == null)
            {
                this.role = this.roleManager.GetLast();
            }
        }

        protected override void Save()
        {
          //  string roleId = this.RoleIdTextEdit.Text;
            string roleName = this.RoleNameTextEdit.Text;
            string roleDescriptiom = this.RoleDescriptionMemoEdit.Text;
            
          //  this.role.Id = roleId;
            this.role.RoleName = roleName;
            this.role.RoleDescription = roleDescriptiom;
            
            switch (this.action)
            {
                case"insert":                    
                    this.roleManager.Insert(this.role);
                    break;
                case "update":
                    this.roleManager.Update(this.role);
                    break;
                default:
                    break;
            }

        }

        public override object EditedItem
        {
            get
            {
                return this.role;
            }
        }

        protected override void AddNew()
        {
            this.role = new Model.Role();
        }

        protected override void MoveNext()
        {
            Model.Role role = this.roleManager.GetNext(this.role);
            if (role == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.role = role;
        }

        protected override void MovePrev()
        {
            Model.Role role = this.roleManager.GetPrev(this.role);
            if (role == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.role = role;
        }

        protected override void MoveFirst()
        {
            this.role = this.roleManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this.role = this.roleManager.GetLast();
        }

        public override void Refresh()
        {
            if (this.role == null)
            {
                this.role = new Book.Model.Role();
                this.action = "insert";
            }
           // this.RoleIdTextEdit.EditValue = string.IsNullOrEmpty(this.role.Id) ? this.role.RoleId : this.role.Id;
            this.RoleNameTextEdit.Text = this.role.RoleName;
            this.RoleDescriptionMemoEdit.Text = this.role.RoleDescription;

            switch (this.action)
            {
                case "insert":
                  //  this.RoleIdTextEdit.Properties.ReadOnly = false;
                    this.RoleNameTextEdit.Properties.ReadOnly = false;
                    this.RoleDescriptionMemoEdit.Properties.ReadOnly = false;
                    break;

                case "update":
                   // this.RoleIdTextEdit.Properties.ReadOnly = true;
                    this.RoleNameTextEdit.Properties.ReadOnly = false;
                    this.RoleDescriptionMemoEdit.Properties.ReadOnly = false;
                    break;

                case "view":

                    //this.RoleIdTextEdit.Properties.ReadOnly = true;
                    this.RoleNameTextEdit.Properties.ReadOnly = true;
                    this.RoleDescriptionMemoEdit.Properties.ReadOnly = true;

                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.roleManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.roleManager.HasRowsAfter(this.role);
        }

        protected override bool HasRowsPrev()
        {
            return this.roleManager.HasRowsBefore(this.role);
        }
        #endregion 
    }
}