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

namespace Book.UI.AccountPayable.AcInvoiceCOBill
{
    public partial class SelectInvoiceCOListForm : Settings.BasicData.BaseListForm
    {
        private BL.InvoiceCOManager _invoiceCoManager = new BL.InvoiceCOManager();
        private IList<Model.InvoiceCO> lists = new List<Model.InvoiceCO>();
        public SelectInvoiceCOListForm()
        {
            InitializeComponent();
            this.gridView1.OptionsBehavior.Editable = true;
            this.StartDate.DateTime = DateTime.Now.AddMonths(-1);
            this.EndDate.DateTime = System.DateTime.Now;
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    this.bindingSource1.DataSource = this._invoiceCoManager.Select(DateTime.Now.AddMonths(-1), DateTime.Now);
        //    this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        //}

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = this._invoiceCoManager.Select(DateTime.Now.AddMonths(-1), DateTime.Now);
            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        public Model.InvoiceCO SelectItem
        {
            get
            {
                return this.bindingSource1.Current as Model.InvoiceCO;
            }
        }

        public IList<Model.InvoiceCO> SelectItems
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
                this.bindingSource1.DataSource = this._invoiceCoManager.Select(condition.StartDate, condition.EndDate);
            }
            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        private void simple_Search_Click(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = this._invoiceCoManager.Select(this.StartDate.DateTime, this.EndDate.DateTime);
            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            lists = new List<Model.InvoiceCO>();
            lists = (from i in (this.bindingSource1.DataSource as List<Model.InvoiceCO>) where i.IsChecked == true select i).ToList<Model.InvoiceCO>();
            if (lists == null || lists.Count == 0)
                lists.Add(this.SelectItem);
            this.DialogResult = DialogResult.OK;
        }
    }
}