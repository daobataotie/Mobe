using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Query;

namespace Book.UI.AccountPayable.AcInvoiceCOBill
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.AcInvoiceCOBillManager();

            this.gridView1.GroupPanelText = "默認顯示:" + System.DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + " 到 " + System.DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.AcInvoiceCOBillManager).SelectByDateRange(System.DateTime.Now.AddMonths(-3), System.DateTime.Now);
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionAChooseForm form = new ConditionAChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ConditionA cond = form.Condition as ConditionA;
                IList<Model.AcInvoiceCOBill> acInvoiceXOBill = (this.manager as BL.AcInvoiceCOBillManager).SelectByDateRange(cond.StartDate, cond.EndDate);
                if (acInvoiceXOBill != null)
                {
                    this.bindingSource1.DataSource = acInvoiceXOBill;
                }
                this.gridView1.GroupPanelText = "查詢日期週期是:" + System.DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + " 到 " + System.DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private Model.AcInvoiceCOBill _AcInvoiceCOBil;

        public Model.AcInvoiceCOBill AcInvoiceCOBil
        {
            get { return (this.bindingSource1.Current as Model.AcInvoiceCOBill); }
        }

    }
}
