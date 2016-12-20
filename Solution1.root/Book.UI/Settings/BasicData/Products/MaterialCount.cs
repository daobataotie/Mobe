using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
namespace Book.UI.Settings.BasicData.Products
{
    public partial class MaterialCount : DevExpress.XtraEditors.XtraForm
    {
        public MaterialCount()
        {
            InitializeComponent();
        }
          
        private void btn_Product1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_Product1.EditValue = (f.SelectedItem as Model.Product).ProductName;
        }

        private void btn_Product2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_Product2.EditValue = (f.SelectedItem as Model.Product).ProductName;
        }

        
    }
}