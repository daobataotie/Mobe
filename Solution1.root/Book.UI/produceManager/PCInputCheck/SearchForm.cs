using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCInputCheck
{
    public partial class SearchForm : DevExpress.XtraEditors.XtraForm
    {
        public SearchForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public string InvoiceCusId { get; set; }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.InvoiceCusId = this.textEdit1.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}