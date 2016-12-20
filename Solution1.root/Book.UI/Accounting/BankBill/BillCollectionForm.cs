using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.BankBill
{
    public partial class BillCollectionForm : DevExpress.XtraEditors.XtraForm
    {
        BL.AtBankAccountManager bankAccountManager = new Book.BL.AtBankAccountManager();
        BL.AtBillsIncomeManager billIncomeManager = new Book.BL.AtBillsIncomeManager();
        IList<Model.AtBillsIncome> bList = null;
        public BillCollectionForm()
        {
            InitializeComponent();
            IList<Model.AtBillsIncome> billList = billIncomeManager.SelectAtBillsIncomeByBillsOften("0", null);
            bList = new List<Model.AtBillsIncome>();
            if (billList != null)
            {
                foreach (Model.AtBillsIncome i in billList)
                {
                    Model.AtBankAccount at = bankAccountManager.Get(i.CollectionAccount);
                    Model.Customer cu = new BL.CustomerManager().Get(i.PassingObject);
                    if (at != null)
                    {
                        i.C = at.BankAccountName;
                    }
                    if (cu != null)
                    {
                        i.A = cu.ToString();

                    }
                    bList.Add(i);
                }
            }
            this.bindingSourcestringbillsOften.DataSource = billList;
           //this.bindingSourceAtBankAccount.DataSource = bankAccountManager.Select();
        }

        private void lookUpEditCollectionAccount_EditValueChanged(object sender, EventArgs e)
        {
            string collectionAccount = "";
            if (this.lookUpEditCollectionAccount.EditValue != null)
            {
                collectionAccount = this.lookUpEditCollectionAccount.EditValue.ToString();
            }
            IList<Model.AtBillsIncome> billList= billIncomeManager.SelectAtBillsIncomeByBillsOften("0", collectionAccount);
            bList = new List<Model.AtBillsIncome>();
            if (billList != null)
            {
                foreach (Model.AtBillsIncome i in billList)
                {
                    Model.AtBankAccount at = bankAccountManager.Get(i.CollectionAccount);
                    Model.Customer cu = new BL.CustomerManager().Get(i.PassingObject);
                    if (at != null)
                    {
                        i.C = at.BankAccountName;
                    }
                    if (cu != null)
                    {
                        i.A = cu.ToString();
                       
                    }
                    bList.Add(i);
                }
            }
            this.bindingSourcestringbillsOften.DataSource = billList;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
             if(this.dateEditMoveDay.EditValue==null) return;
            if (bList != null)
            {                
                foreach (Model.AtBillsIncome ab in bList)
                {
                   if (ab.Up == true)
                    {
                       
                        ab.MoveDay = this.dateEditMoveDay.DateTime;
                        ab.BillsOften = "1";
                        billIncomeManager.Update(ab);
                    }
                }
                MessageBox.Show("托收成功..");
            }
            this.gridControl1.RefreshDataSource();
        }

        private void simpleButtonControl_Click(object sender, EventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (bList != null)
            {
                foreach (Model.AtBillsIncome ab in bList)
                {
                    if (ab.Up == true)
                    {
                        ab.MoveDay = this.dateEditMoveDay.DateTime;
                        ab.BillsOften = "0";
                        billIncomeManager.Update(ab);
                    }
                }
                MessageBox.Show("取消成功..");
            }
            this.gridControl1.RefreshDataSource();
        }
    }
}