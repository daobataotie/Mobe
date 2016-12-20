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
    public partial class ChooseProcessForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.ProcessCategoryManager ProcessCategoryManager = new Book.BL.ProcessCategoryManager();
        private  Model.InvoiceXJProcess _invoiceXJProcess = new Book.Model.InvoiceXJProcess();
        private  IList<Model.InvoiceXJProcess> detail = new List<Model.InvoiceXJProcess>();
        private BL.InvoiceXJProcessManager InvoiceXJProcessManager = new Book.BL.InvoiceXJProcessManager();
        private Model.InvoiceXJDetail _invoiceXJDetail = new Book.Model.InvoiceXJDetail();
        private Model.InvoiceXJ _invoiceXJ = new Book.Model.InvoiceXJ();
        public ChooseProcessForm()
        {
            InitializeComponent();
        }
        public ChooseProcessForm(Model.InvoiceXJDetail InvoiceXJDetail,Model.InvoiceXJ invoiceXJ):this()
        {
            this._invoiceXJDetail = InvoiceXJDetail;
            this._invoiceXJ = invoiceXJ;
        }

        private void ChooseProcessForm_Load(object sender, EventArgs e)
        {
            this.detail = this.InvoiceXJProcessManager.Select(this._invoiceXJDetail);
            this.bindingSourceProcessCate.DataSource = this.ProcessCategoryManager.Select();
            this.bindingSource1.DataSource = this.detail;
            if (this.detail.Count == 0)
                this.AddNewRow();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
            //if (e.Column.Name == this.gridColumnProcess.Name)
            //{  
            //    Model.InvoiceXJProcess invoiceXJProcess = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceXJProcess;
                
            
               
            //}

        }

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        private void AddNewRow()
        {
            Model.InvoiceXJProcess InvoiceXJProcess = new Book.Model.InvoiceXJProcess();
            InvoiceXJProcess.InvoiceXJProcessId = Guid.NewGuid().ToString();
            //InvoiceXJProcess.InvoiceXJDetail = this._invoiceXJDetail;
            if (this._invoiceXJDetail!=null)
            InvoiceXJProcess.InvoiceXJDetailId = this._invoiceXJDetail.InvoiceXJDetailId;
            InvoiceXJProcess.Product = this._invoiceXJDetail.Product;
            if (InvoiceXJProcess.Product != null)
            {
                InvoiceXJProcess.ProductId = this._invoiceXJDetail.Product.ProductId;
            }
            InvoiceXJProcess.InvoiceXJ = this._invoiceXJ;
            if (InvoiceXJProcess.InvoiceXJ != null)
                InvoiceXJProcess.InvoiceXJId = InvoiceXJProcess.InvoiceXJ.InvoiceId;
            this.detail.Add(InvoiceXJProcess);
            this.gridControl1.RefreshDataSource();
            this.bindingSource1.Position = this.bindingSource1.IndexOf(InvoiceXJProcess);
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.InvoiceXJProcessManager.Update(this.detail,this._invoiceXJDetail);
            MessageBox.Show(Properties.Resources.SaveSuccess,this.Text , MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;

        }

        private void barButtonItemCacel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult=DialogResult.OK;
        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            Model.InvoiceXJProcess InvoiceXJProcess = this.bindingSource1.Current as Model.InvoiceXJProcess;
            this.detail.Remove(InvoiceXJProcess);
            if (this.detail.Count == 0)
                this.AddNewRow();
            this.gridControl1.RefreshDataSource();
        }
    }
}