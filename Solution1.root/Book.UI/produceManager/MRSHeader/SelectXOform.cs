using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;

namespace Book.UI.produceManager.MRSHeader
{
    public partial class SelectXOform : DevExpress.XtraEditors.XtraForm
    {
        public SelectXOform()
        {
            InitializeComponent();
            
        }

        //private void buttonEditMPSIdstart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    produceManager.MPSheader.ChooseMPSheaderForm f = new Book.UI.produceManager.MPSheader.ChooseMPSheaderForm();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //        Model.MPSheader mPSheader = f.SelectedItem as Model.MPSheader;
        //        if (mPSheader != null)
        //        {
        //            this.buttonEditMPSIdstart.Text = mPSheader.Id;

        //        }
        //    }
        //}

        //private void buttonEditMPSIdend_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    produceManager.MPSheader.ChooseMPSheaderForm f = new Book.UI.produceManager.MPSheader.ChooseMPSheaderForm();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //        Model.MPSheader mPSheader = f.SelectedItem as Model.MPSheader;
        //        if (mPSheader != null)
        //        {
        //            this.buttonEditMPSIdend.Text = mPSheader.Id;

        //        }
        //    }
        //}

        private void buttonEditCustomerStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseCustoms f = new Book.UI.Invoices.ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Customer customer = f.SelectedItem as Model.Customer;
                if (customer != null)
                {
                    this.buttonEditCustomerStart.Text = customer.Id;

                }
            }
        }

        private void buttonEditCustomerEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseCustoms f = new Book.UI.Invoices.ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Customer customer = f.SelectedItem as Model.Customer;
                if (customer != null)
                {
                    this.buttonEditCustomerEnd.Text = customer.Id;

                }
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string customerStart = string.IsNullOrEmpty(this.buttonEditCustomerStart.Text) ? null : this.buttonEditCustomerStart.Text;


            string customerEnd = string.IsNullOrEmpty(this.buttonEditCustomerEnd.Text) ? null : this.buttonEditCustomerEnd.Text;

            string mpsStart = this.comboBoxEditMpsStart.EditValue==null ? null : this.comboBoxEditMpsStart.EditValue.ToString();
            string mpsEnd = this.comboBoxEditMpsEnd.EditValue==null ? null : this.comboBoxEditMpsEnd.EditValue.ToString();

            DateTime dateStart = this.dateEditStart.DateTime.Year < 1500 ? Convert.ToDateTime("1900-01-01") : this.dateEditStart.DateTime;
            DateTime dateEnd = this.dateEditEnd.DateTime.Year < 1500 ? Convert.ToDateTime("3000-01-01") : this.dateEditEnd.DateTime;
            string productId = null;
            //if (this.newChooseProduct.EditValue != null)
            //{
            //    productId = this.newChooseProduct.EditValue.ToString();
            //}

            IList<Model.MPSdetails> mpsDetails = new BL.MPSdetailsManager().Select(customerStart, customerEnd, mpsStart, mpsEnd, dateStart, dateEnd, productId);
            
            if (mpsDetails.Count == 0)
            {
                MessageBox.Show("在此區間無進貨記錄"); return;
            }
            RO2 f = new RO2(mpsDetails, dateStart, dateEnd);
            f.ShowPreview();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectXOform_Load(object sender, EventArgs e)
        {
            this.comboBoxEditMpsStart.Properties.Items.Clear();
            this.comboBoxEditMpsEnd.Properties.Items.Clear();
            BL.MPSheaderManager MPSheaderManager=new Book.BL.MPSheaderManager();
            foreach (Model.MPSheader MPSheader in MPSheaderManager.Select())
            {
                this.comboBoxEditMpsStart.Properties.Items.Add(MPSheader.Id);
                this.comboBoxEditMpsEnd.Properties.Items.Add(MPSheader.Id);
            }
        }

        //private void newChooseProduct_Click(object sender, EventArgs e)
        //{
        //    Invoices.ChooseProductForm f = new ChooseProductForm();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //        Model.MPSdetails mpsDetails = f.SelectedItem as Model.MPSdetails;
        //        if (mpsDetails != null)
        //        {
        //            this.newChooseProduct.EditValue = mpsDetails.ProductId;
        //        }
        //    }
        //}
    }
}