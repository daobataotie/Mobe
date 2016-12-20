using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.AccountPayable.AcOtherShouldPayment
{

    public partial class MaintainForm : DevExpress.XtraEditors.XtraForm
    {
        public BL.AcOtherShouldPaymentManager _acOtherShouldPaymentManager = new Book.BL.AcOtherShouldPaymentManager();

        public MaintainForm()
        {
            InitializeComponent();
            this.nccSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.nccCompany.Choose = new Settings.BasicData.Company.ChooseCompany();
            this.dateEditStart.DateTime = DateTime.Now.AddDays(-3);
            this.dateEditEnd.DateTime = DateTime.Now;

            this.bindingSource1.DataSource = new BL.AcOtherShouldPaymentManager().SelectByDateRangeAndSupCompany(this.dateEditStart.DateTime, this.dateEditEnd.DateTime, null, null);
            this.bindingSource2.DataSource = new BL.CompanyManager().Select();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (this.dateEditStart.EditValue == null || this.dateEditEnd.EditValue == null)
                MessageBox.Show("時間區間不完整,請核對");
            else
                this.bindingSource1.DataSource = new BL.AcOtherShouldPaymentManager().SelectByDateRangeAndSupCompany(this.dateEditStart.DateTime, this.dateEditEnd.DateTime, this.nccSupplier.EditValue as Model.Supplier, this.nccCompany.EditValue as Model.Company);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            IList<Model.AcOtherShouldPayment> list = this.bindingSource1.DataSource as IList<Model.AcOtherShouldPayment>;

            if (list == null || list.Count == 0)
                return;

            string msg = this._acOtherShouldPaymentManager.UpdateAcOtherShouldPaymentList(list);
            MessageBox.Show(msg);
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "colInvoiceId":
                    (this.bindingSource1.DataSource as List<Model.AcOtherShouldPayment>).Where(asp => asp.Checked == true).ToList<Model.AcOtherShouldPayment>().ForEach(asp => asp.InvoiceId = e.Value.ToString());
                    this.gridControl1.RefreshDataSource();
                    break;
                case "colCompany":
                    (this.bindingSource1.DataSource as List<Model.AcOtherShouldPayment>).Where(asp => asp.Checked == true).ToList<Model.AcOtherShouldPayment>().ForEach(asp => asp.CompanyId = e.Value.ToString());
                    this.gridControl1.RefreshDataSource();
                    break;
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name != "colChecked")
            {
                (this.bindingSource1.DataSource as List<Model.AcOtherShouldPayment>).ForEach(asp => asp.Checked = false);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}