using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;
using System.Linq;

namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    public partial class SelectInvoiceXOListForm : Settings.BasicData.BaseListForm
    {
        private BL.InvoiceXOManager _invoiceXoManager = new BL.InvoiceXOManager();
        private IList<Model.InvoiceXO> lists = new List<Model.InvoiceXO>();
        public SelectInvoiceXOListForm()
        {
            InitializeComponent();
            this.gridView1.OptionsBehavior.Editable = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.bindingSource1.DataSource = this._invoiceXoManager.Select(DateTime.Now.AddMonths(-2), DateTime.Now);
        }

        public Model.InvoiceXO SelectItem
        {
            get
            {
                return this.bindingSource1.Current as Model.InvoiceXO;
            }
        }

        public IList<Model.InvoiceXO> SelectItems
        {
            get
            {
                return this.lists;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionAChooseForm form = new ConditionAChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ConditionA condition = form.Condition as ConditionA;
                this.bindingSource1.DataSource = this._invoiceXoManager.Select(condition.StartDate, condition.EndDate);
            }
        }

        private void simple_OK_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            lists = new List<Model.InvoiceXO>();
            lists = (from i in (this.bindingSource1.DataSource as List<Model.InvoiceXO>) where i.IsChecked == true select i).ToList<Model.InvoiceXO>();
            if (lists == null || lists.Count == 0)
                lists.Add(this.SelectItem);
            this.DialogResult = DialogResult.OK;
        }
    }
}