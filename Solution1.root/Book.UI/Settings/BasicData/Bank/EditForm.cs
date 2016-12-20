using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Book.UI.Settings.BasicData.Bank
{

    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述：銀行信息設置
   // 文 件 名：EditForm
   // 编 码 人: 马艳军                   完成时间:2009-09-09
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        //實體對象定義          -----myj
        Model.Bank bank;
        //業務對象定義          -----myj
        BL.BankManager bankManager=new Book.BL.BankManager();
        //無慘構造函數定義      ---- myj
        public EditForm()
        {         
            InitializeComponent();
            this.requireValueExceptions.Add(Model.Bank.PROPERTY_BANKNAME, new AA(Properties.Resources.RequireDataForNames, this.textEditName));
            this.action = "insert";
        }

        /// <summary>
        /// 有参数的构造函数          -----myj
        /// </summary>
        /// <param name="Bank">实体对象</param>
        public EditForm(Model.Bank Bank)
        {           
            this.bank = Bank;
            this.action = "update";
        }

        /// <summary>
        /// 有参数的构造函数          -----myj
        /// </summary>
        /// <param name="Bank">实体对象</param>
        /// <param name="action">動作</param>
        public EditForm(Model.Bank Bank, string action)
        {           
            this.bank = Bank;
            this.action = action;

        }
        #region Override
        protected override void AddNew()
        {         
            this.bank = new Model.Bank();            
        }
        protected override void Save()    
        {

            this.bank.BankId = this.bank.BankId;
            this.bank.BankName = textEditName.Text;
            this.bank.Description = textEditDescription.Text;
            switch (this.action)
            {
                case "insert":                  
                    this.bankManager.Insert(this.bank);
                    break;
                case "update":
                    this.bankManager.Update(this.bank);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {
          
            if (this.bank == null)
            {
                this.bank = new Book.Model.Bank();
                this.action = "insert";
            }

            this.bindingSourceBank.DataSource = this.bankManager.Select();
            this.textEditName.Text = this.bank.BankName;
            this.textEditDescription.Text = this.bank.Description;

            switch (this.action)
            {
                case "insert":
                    this.textEditName.Properties.ReadOnly = false;
                    this.textEditDescription.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.textEditName.Properties.ReadOnly = false;
                    this.textEditDescription.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.textEditName.Properties.ReadOnly = true;
                    this.textEditDescription.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        protected override void Delete()
        {
            if (this.bank == null)
                return;
            if(MessageBox.Show(Properties.Resources.ConfirmToDelete,this.Text,MessageBoxButtons.OKCancel,MessageBoxIcon.Question)!=DialogResult.OK)
                return;
            try
            {
                this.bankManager.Delete(this.bank.BankId);
                this.bank = this.bankManager.GetNext(this.bank);
                if (this.bank == null)
                {
                    this.bank = this.bankManager.GetLast();
                }
            }
            catch
            {
                throw ;
            }

                return;

        }
        protected override void MoveFirst()
        {
            this.bank = this.bankManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.Bank bank= this.bankManager.GetPrev(this.bank);
            if (bank == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.bank = bank;
        }
        protected override void MoveLast()
        {
            this.bank = this.bankManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.Bank bank = this.bankManager.GetNext(this.bank);
            if (bank == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.bank = bank;
        }
        protected override bool HasRows()
        {
            return this.bankManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.bankManager.HasRowsAfter(this.bank);
        }
        protected override bool HasRowsPrev()
        {
            return this.bankManager.HasRowsBefore(this.bank);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditName, this.textEditDescription});
        }
#endregion

        //gridview的click點擊事件     -----myj
        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.Bank  bank = this.bindingSourceBank.Current as Model.Bank;
                if (bank!= null)
                {
                    this.bank =bank;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}