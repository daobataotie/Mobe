using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.CO
{
    public partial class ChooseDetailsForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.InvoiceCODetailManager invoiceCODetailManager = new Book.BL.InvoiceCODetailManager();
        private Model.InvoiceCO invoice;

        public ChooseDetailsForm(Model.InvoiceCO invoice)
        {
            InitializeComponent();

            this.invoice = invoice;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this.DialogResult = DialogResult.OK;
        }

        public IList<Model.InvoiceCODetail> SelectedItems
        {
            get
            {
                IList<Model.InvoiceCODetail> items = new List<Model.InvoiceCODetail>();
                for (int i = 0; i < this.gridView1.DataRowCount; i++)
                {
                    object selected = this.gridView1.GetRowCellValue(i, this.colSelection);
                    if ((bool)selected)
                    {
                        items.Add((Model.InvoiceCODetail)this.bindingSource1[i]);
                    }
                }
                return items;
            }
        }

        private void ChooseDetailsForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = this.invoiceCODetailManager.Select(this.invoice);
            for (int i = 0; i < this.gridView1.DataRowCount; i++)
            {
                this.gridView1.SetRowCellValue(0, this.colSelection, true);
            }
        }

    }
}