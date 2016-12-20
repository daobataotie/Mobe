using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Book.UI.Invoices.CO
{
    public partial class CoSelectForm : DevExpress.XtraEditors.XtraForm
    {
        public CoSelectForm()
        {
            InitializeComponent();
        }
        private BL.InvoiceCOManager _invoiceCoManager = new BL.InvoiceCOManager();
        private Model.MRSHeader _mrsHeader;
        public CoSelectForm(Model.MRSHeader mrsHeader)
            : this()
        {
            this._mrsHeader = mrsHeader;
        }

        public Model.InvoiceCO SelectItem
        {
            get
            {
                return this.bindingSourceInvoiceCO.Current as Model.InvoiceCO;
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_lookInfo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void CoSelectForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceInvoiceCO.DataSource = this._invoiceCoManager.SelectByMrsHeaderId(this._mrsHeader.MRSHeaderId);
        }
    }
}