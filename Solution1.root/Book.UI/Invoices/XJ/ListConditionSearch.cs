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
    public partial class ListConditionSearch : DevExpress.XtraEditors.XtraForm
    {

        #region 查询变量
        public DateTime _startDate { get; set; }

        public DateTime _endDate { get; set; }

        public string _productid { get; set; }

        public string _customerid { get; set; }

        public string _invoiceXJId { get; set; }

        public string _companyid { get; set; }
        #endregion

        public ListConditionSearch()
        {
            InitializeComponent();
            this.DateEditStart.DateTime = DateTime.Now.AddDays(-30);
            this.DateEditEnd.DateTime = DateTime.Now;
            this.bindingSourceCompany.DataSource = new BL.CompanyManager().Select();
            this.nccCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this._startDate = this.DateEditStart.EditValue == null ? DateTime.Now.AddDays(-30) : this.DateEditStart.DateTime;
            this._endDate = this.DateEditEnd.EditValue == null ? DateTime.Now.AddDays(1).Date : this.DateEditEnd.DateTime;
            this._productid = this.btnEdit_Product.EditValue is Model.Product ? (this.btnEdit_Product.EditValue as Model.Product).ProductId : null;
            this._customerid = this.nccCustomer.EditValue is Model.Customer ? (this.nccCustomer.EditValue as Model.Customer).CustomerId : null;
            this._invoiceXJId = string.IsNullOrEmpty(this.txtInvoiceXJId.Text) ? null : this.txtInvoiceXJId.Text;
            this._companyid = (this.lookUpEdit_Company.EditValue == null || string.IsNullOrEmpty(this.lookUpEdit_Company.EditValue.ToString())) ? null : this.lookUpEdit_Company.EditValue.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Product_Click(object sender, EventArgs e)
        {
            Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btnEdit_Product.EditValue = f.SelectedItem as Model.Product;
            }
        }
    }
}