using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class ListDetail : DevExpress.XtraEditors.XtraForm
    {
        Model.PCExportReportANSIDetail detail = new Book.Model.PCExportReportANSIDetail();
        BL.PCExportReportANSIDetailManager manager = new Book.BL.PCExportReportANSIDetailManager();
        Hashtable htFormName = new Hashtable();
        string _FromPc;
        public ListDetail()
        {
            InitializeComponent();

            htFormName.Add("ANSI", "ANSI外銷詳細");
            htFormName.Add("CSA", "CSA外銷詳細");
            htFormName.Add("CEEN", "CEEN外銷詳細");
            htFormName.Add("AS", "AS外銷詳細");
            htFormName.Add("JIS", "JIS外銷詳細");

            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.date_Start.EditValue = DateTime.Now.Date.AddDays(-7);
            this.date_End.EditValue = DateTime.Now;

            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public ListDetail(string FormName)
            : this()
        {
            this._FromPc = FormName.Substring(FormName.IndexOf('-') + 1);
            this.Text = htFormName[this._FromPc].ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //if (this.txt_InvoiceCusXoId.EditValue == null || this.btne_Product.EditValue == null)
            //{
            //    MessageBox.Show("客戶訂單編號和商品名稱不能為空!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            this.bindingSource1.DataSource = this.manager.SelectAllDetail(this.date_Start.EditValue == null ? DateTime.Now.Date.AddDays(-7) : this.date_Start.DateTime, this.date_End.EditValue == null ? DateTime.Now : this.date_End.DateTime, this.txt_InvoiceCusXoId.Text, this.btne_Product.EditValue == null ? null : (this.btne_Product.EditValue as Model.Product).ProductId, this.newChooseCustomer.EditValue == null ? null : (this.newChooseCustomer.EditValue as Model.Customer).CustomerId, this._FromPc);

            if (this.bindingSource1.DataSource == null || this.bindingSource1.Count < 1)
            {
                MessageBox.Show("無記錄!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.detail.InvoiceCusXOId = txt_InvoiceCusXoId.Text;
            this.detail.Product = (Model.Product)this.btne_Product.EditValue;
            this.detail.Customer = (Model.Customer)this.newChooseCustomer.EditValue;
        }

        private void btne_Product_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btne_Product.EditValue = f.SelectedItem;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.DataSource == null || this.bindingSource1.Count < 1)
            {
                MessageBox.Show("無記錄!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RODetail RO = new RODetail(this.Text, this.detail, (IList<Model.PCExportReportANSIDetail>)this.bindingSource1.DataSource);
            RO.ShowPreviewDialog();
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            Model.PCExportReportANSIDetail model = this.bindingSource1.Current as Model.PCExportReportANSIDetail;
            DetailsForm f = new DetailsForm(model);
            f.ShowDialog();
        }
    }
}