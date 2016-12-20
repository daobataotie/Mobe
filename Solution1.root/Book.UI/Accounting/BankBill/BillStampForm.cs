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
    public partial class BillStampForm : DevExpress.XtraEditors.XtraForm
    {
        BL.AtBankAccountManager bankAccountManager = new Book.BL.AtBankAccountManager();
        BL.AtBillsIncomeManager billIncomeManager = new Book.BL.AtBillsIncomeManager();
        IList<Model.AtBillsIncome> bList = null;
        public BillStampForm()
        {
            InitializeComponent();
            this.bindingSourceAtBankAccount.DataSource = bankAccountManager.Select();
        }
        private void Binds(string acount)
        {
            IList<Model.AtBillsIncome> billList = billIncomeManager.SelectAtBillsIncomeByBillsOften("尚未兌現", acount);
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
                        i.A = cu.Id;
                        i.B = cu.CustomerFullName;
                    }
                    bList.Add(i);
                }
            }
            this.bindingSourcestringbillsOften.DataSource = bList;
        }
        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            int q = 0;
            if (bList != null)
            {
                foreach (Model.AtBillsIncome ab in bList)
                {
                    if (ab.Up == true)
                    {
                        ab.MoveDay = this.dateEditMoveDay.EditValue == null ? global::Helper.DateTimeParse.NullDate : DateTime.Parse(this.dateEditMoveDay.EditValue.ToString());
                        ab.BillsOften = "票貼中";
                        q++;
                        billIncomeManager.Update(ab);
                    }
                } 
            }
            if (q > 0)
            {
                MessageBox.Show("票貼成功..");
                if (this.lookUpEditCollectionAccount.EditValue != null)
                {
                    Binds(this.lookUpEditCollectionAccount.EditValue.ToString());
                }
                q = 0;

            }
            else
            {
                MessageBox.Show("請選擇..");
            }
        }

        private void simpleButtonControl_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lookUpEditCollectionAccount_EditValueChanged(object sender, EventArgs e)
        {
            string collectionAccount = "";
            if (this.lookUpEditCollectionAccount.EditValue != null)
            {
                collectionAccount = this.lookUpEditCollectionAccount.EditValue.ToString();
            }
            Binds(collectionAccount);
        }
    }
}