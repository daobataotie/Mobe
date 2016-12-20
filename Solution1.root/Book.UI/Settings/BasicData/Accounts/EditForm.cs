using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.Accounts
{

    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 文 件 名：EditForm
   // 编 码 人: 马艳军                   完成时间:2009-09-15
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间: 
   //----------------------------------------------------------------*/

    public partial class EditForm : BaseEditForm1
    {
        IList<Model.Account> _detail = new List<Model.Account>();
        protected BL.AccountManager accountManager = new Book.BL.AccountManager();
       // private Model.Account account = null;
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.Account.PROPERTY_ID, new AA("請輸入編號。", this.gridControl1 as Control));
            //this.requireValueExceptions.Add(Model.Account.PROPERTY_ACCOUNTNAME, new AA(Properties.Resources.RequireDataForName, this.AccountNameTextEdit));
            this.invalidValueExceptions.Add(Model.Account.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.gridControl1 as Control));
            this.action = "view";
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.accountManager.Delete(this.bindingSource1.Current as Model.Account);
        }
        protected override void Save()
        {
            switch (this.action)
            {
                case "insert":
                    this.accountManager.Update(this._detail);
                    break;
                case "update":
                    this.accountManager.Update(this._detail);
                    break;
                default:
                    break;
            }
        }


        protected override void AddNew()
        {

        }
        public override void Refresh()
        {
            this._detail = this.accountManager.Select();
            this.bindingSource1.DataSource = this._detail;
            if (this.action == "insert")
            {
                Model.Account account = new Book.Model.Account();
                account.AccountId = Guid.NewGuid().ToString();
                account.AccountBalance0 = decimal.Zero;
                account.AccountBalance1 = decimal.Zero;
                this._detail.Add(account);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(account);
                this.gridControl1.RefreshDataSource();
            }
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        protected override void grid_keyDpwn()
        {
            Model.Account account = new Book.Model.Account();
            account.AccountId = Guid.NewGuid().ToString();
            account.AccountBalance0 = decimal.Zero;
            account.AccountBalance1 = decimal.Zero;
            this._detail.Add(account);
            this.bindingSource1.Position = this.bindingSource1.IndexOf(account);
        }
        protected override void grid_KeyDelete()
        {
            this._detail.Remove(this.bindingSource1.Current as Model.Account);
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.Account account = this.gridView1.GetRow(e.RowHandle) as Model.Account;
            if (e.Value == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnId":
                    account.Id = e.Value.ToString();
                    break;
                case "gridColumnName":
                    account.AccountName = e.Value.ToString();
                    break;
                case "gridColumnDesc":
                    account.AccountDescription = e.Value.ToString();
                    break;
            }
        }

    }
}

