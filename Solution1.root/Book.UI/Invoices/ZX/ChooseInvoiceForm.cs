using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.ZX
{
    public partial class ChooseInvoiceForm : DevExpress.XtraEditors.XtraForm
    {
        BL.InvoicePackingManager invoicePackingManager = new Book.BL.InvoicePackingManager();
        BL.InvoicePackingDetailManager detailManager = new Book.BL.InvoicePackingDetailManager();
        IList<Model.InvoicePackingDetail> details = new List<Model.InvoicePackingDetail>();
        IList<Model.InvoicePackingDetail> _key = new List<Model.InvoicePackingDetail>();

        public IList<Model.InvoicePackingDetail> Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public ChooseInvoiceForm()
        {
            InitializeComponent();

            this.bindingSourceCompany.DataSource = (new BL.CompanyManager()).Select();
            this.newChooseContorlConsignee.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.date_Start.EditValue = DateTime.Now.AddDays(-7);
            this.Date_End.EditValue = DateTime.Now;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.bindingSourceInvoiceXO.DataSource = invoicePackingManager.SelectByCondition(this.date_Start.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.date_Start.DateTime, this.Date_End.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.Date_End.DateTime, this.txt_NO.Text, this.txt_InvoiceOf.Text, this.lookUpEditShippedBy.EditValue == null ? null : this.lookUpEditShippedBy.EditValue.ToString(), this.newChooseContorlConsignee.EditValue == null ? null : (this.newChooseContorlConsignee.EditValue as Model.Customer).CustomerId);

            this.gridControl1.RefreshDataSource();
        }

        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            BandDetail();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            BandDetail();
        }

        private void BandDetail()
        {
            if (this.bindingSourceInvoiceXO.Current != null)
            {
                this.details = this.detailManager.SelectByInvoicePackingId((this.bindingSourceInvoiceXO.Current as Model.InvoicePacking).InvoicePackingId);

                int count = 0;
                foreach (Model.InvoicePackingDetail model in details)
                {
                    if (this.Key.Contains(model))
                    {
                        model.Checked = true;
                        count += 1;
                    }
                }
                if (count == details.Count)
                    this.checkAll.Checked = true;
                else
                    this.checkAll.Checked = false;
            }
            else
                this.details = null;
            this.bindingSourceInvoiceXODetail.DataSource = details;
        }

        private void checkAll_Click(object sender, EventArgs e)
        {
            if (this.checkAll.Checked == false)
            {
                foreach (Model.InvoicePackingDetail model in details)
                {
                    model.Checked = true;
                    if (Key.Contains(model))
                        continue;
                    Key.Add(model);
                }
            }
            else
            {
                foreach (Model.InvoicePackingDetail model in details)
                {
                    model.Checked = false;
                    if (Key.Contains(model))
                        Key.Remove(model);
                }
            }
            this.gridControl2.RefreshDataSource();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.Key.Count == 0)
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView3_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumnCheck")
            {
                Model.InvoicePackingDetail detail = this.gridView3.GetRow(e.RowHandle) as Model.InvoicePackingDetail;
                if ((bool)e.Value)
                {
                    this.Key.Add(detail);
                }
                else
                {
                    for (int i = 0; i < Key.Count; i++)
                    {
                        if (Key[i].InvoicePackingDetailId == detail.InvoicePackingDetailId)
                        {
                            this.Key.RemoveAt(i);
                            break;
                        }
                    }
                    this.checkAll.Checked = false;
                    this.gridControl2.RefreshDataSource();
                }
            }
            this.gridView3.PostEditor();
            this.gridView3.UpdateCurrentRow();
        }
    }
}