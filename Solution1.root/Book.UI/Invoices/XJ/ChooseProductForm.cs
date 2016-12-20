using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.XJ
{
    public partial class ChooseProductForm : DevExpress.XtraEditors.XtraForm
    {
        public ChooseProductForm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                ProductEditForm f = new ProductEditForm(); 
                if(f.ShowDialog()!=DialogResult.OK) return;
            }
            if (this.radioGroup1.SelectedIndex == 1)
            {
                EditForm f = new EditForm();
                if(f.ShowDialog()!=DialogResult.OK) return;
            }     
        }

        private void ChooseProductForm_Load(object sender, EventArgs e)
        {

        }
    }
}