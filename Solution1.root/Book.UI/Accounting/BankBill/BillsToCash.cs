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
    public partial class BillsToCash : DevExpress.XtraEditors.XtraForm
    {
        BL.AtBankAccountManager bankAccountManager = new Book.BL.AtBankAccountManager();
        BL.AtBillsIncomeManager billIncomeManager = new Book.BL.AtBillsIncomeManager();
        IList<Model.AtBillsIncome> bList = null;
        public BillsToCash()
        {
            InitializeComponent();
            this.dateEdit1.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEdit2.DateTime = DateTime.Now;
            this.comboBoxProduceType.SelectedIndex = 0;
           
        }
        private void Binds()
        {
            IList<Model.AtBillsIncome> billList = billIncomeManager.SelectDuiXianByDate(this.dateEdit1.DateTime, this.dateEdit2.DateTime, this.dateEditDaoQi1.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditDaoQi1.DateTime, this.dateEditDaoQi2.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.dateEditDaoQi2.DateTime, "1",this.comboBoxProduceType.SelectedIndex.ToString());
            bList = new List<Model.AtBillsIncome>();
            if (billList != null)
            {
                foreach (Model.AtBillsIncome i in billList)
                {
                    //if (i.BillsOften == "托收中" || i.BillsOften == "票貼中" || i.BillsOften == "尚未兌現")
                    {
                      //  Model.AtBankAccount at = bankAccountManager.Get(i.CollectionAccount);
                        if (this.comboBoxProduceType.SelectedIndex == 0)
                        {
                            Model.Customer cu = new BL.CustomerManager().Get(i.PassingObject);
                            i.B = cu.ToString();
                        }
                        else
                        {
                            Model.Supplier su= new BL.SupplierManager().Get(i.PassingObject);
                            // i.A = cu.Id;
                            i.B = su.ToString();
                        }
                            //if (at != null)
                            //{
                            //    i.C = at.BankAccountName;
                            //}
                       
                        bList.Add(i);
                    }
                    
                }
            }
            this.bindingSourcestringbillsOften.DataSource = bList;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            int q = 0;
            if (bList != null)
            {
                foreach (Model.AtBillsIncome ab in bList)
                {
                    if (ab.Checked == true)
                    {
                        if (ab.BillsOften != "1") continue;
                        ab.MoveDay = DateTime.Now;
                        ab.BillsOften = "2";
                        q++;
                        billIncomeManager.Update(ab);
                    }
                }
            }
            if (q > 0)
            {
                MessageBox.Show("成功兌現..");
                this.gridControl1.RefreshDataSource();
                q = 0;

            }
            else
            {
                MessageBox.Show("請選擇..");
            }
        }

        private void simpleButtonControl_Click(object sender, EventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (bList != null)
            {
                foreach (Model.AtBillsIncome ab in bList)
                {
                    if (ab.Checked == true)
                    {
                        if (ab.BillsOften != "2") continue;
                        ab.MoveDay = DateTime.Now;
                        ab.BillsOften = "1";
                        billIncomeManager.Update(ab);
                    }
                }
                MessageBox.Show("取消成功..");
            }
            this.gridControl1.RefreshDataSource();
        }

        private void BillsToCash_Load(object sender, EventArgs e)
        {
            Binds();
        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            Binds();
        }
    }
}