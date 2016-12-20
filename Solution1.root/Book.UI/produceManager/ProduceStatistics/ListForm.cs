using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceStatistics
{
    public partial class ListForm : UI.Invoices.BaseListForm
    {
        public ListForm()
        {
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceXOManager();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = new BL.ProduceStatisticsManager().Select();
        }
        public Model.ProduceStatistics SelectItem
        {
            get { return this.bindingSource1.Current as Model.ProduceStatistics; }
        }

        private void gridView1_DoubleClick_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
             if (e.ListSourceRowIndex < 0) return;
             IList<Model.ProduceStatistics> details = this.bindingSource1.DataSource as IList<Model.ProduceStatistics>;
            if (details == null || details.Count < 1) return;
            Model.Procedures detail = details[e.ListSourceRowIndex].Procedures;
            switch (e.Column.Name)
            {
                case "gridColumn4":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Procedurename) ? "" : detail.Procedurename;
                    break;
            }
            
        }
    }
}