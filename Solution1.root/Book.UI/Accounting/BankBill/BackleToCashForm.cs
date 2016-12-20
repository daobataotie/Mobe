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
    public partial class BackleToCashForm : DevExpress.XtraEditors.XtraForm
    {
        BL.AtBankAccountManager bankAccountManager = new Book.BL.AtBankAccountManager();
        BL.AtBillsIncomeManager billIncomeManager = new Book.BL.AtBillsIncomeManager();
        IList<Model.AtBillsIncome> bList = null;
        public BackleToCashForm()
        {
            InitializeComponent();
        }
        private void Binds()
        {
            IList<Model.AtBillsIncome> billList = billIncomeManager.Select();
            bList = new List<Model.AtBillsIncome>();
            if (billList != null)
            {
                foreach (Model.AtBillsIncome i in billList)
                {
                    if (i.BillsOften == "尚未兌現")
                    {
                        Model.AtBankAccount at = bankAccountManager.Get(i.CollectionAccount);
                        Model.Supplier cu = new BL.SupplierManager().Get(i.PassingObject);
                        if (at != null)
                        {
                            i.C = at.BankAccountName;
                        }
                        if (cu != null)
                        {
                            i.A = cu.Id;
                            i.B = cu.SupplierFullName;
                        }
                        bList.Add(i);
                    }

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
                        ab.BillsOften = "兌現";
                        q++;
                        billIncomeManager.Update(ab);
                    }
                }
            }
            if (q > 0)
            {
                MessageBox.Show("成功兌現..");
                Binds();
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

        private void BackleToCashForm_Load(object sender, EventArgs e)
        {
            Binds();
        }
    }
}