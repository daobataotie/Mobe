using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.AccountPayable.AcOtherShouldCollection
{
    public partial class MaintainForm : DevExpress.XtraEditors.XtraForm
    {
        BL.AcOtherShouldCollectionManager _acOtherShouldCollectionManager = new Book.BL.AcOtherShouldCollectionManager();

        public MaintainForm()
        {
            InitializeComponent();
            this.nccCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.nccCompany.Choose = new Settings.BasicData.Company.ChooseCompany();
            this.DateEditStart.DateTime = DateTime.Now.AddDays(-3);
            this.DateEditEnd.DateTime = DateTime.Now;

            this.bindingSource1.DataSource = this._acOtherShouldCollectionManager.SelectByDateRangeAndCustomerCompany(this.DateEditStart.DateTime, this.DateEditEnd.DateTime, null, null);

            this.bindingSource2.DataSource = new BL.CompanyManager().Select();

        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (this.DateEditStart.EditValue == null || this.DateEditEnd.EditValue == null)
                MessageBox.Show("時間區間不完整,請核對");
            else
                this.bindingSource1.DataSource = this._acOtherShouldCollectionManager.SelectByDateRangeAndCustomerCompany(this.DateEditStart.DateTime, this.DateEditEnd.DateTime, this.nccCustomer.EditValue as Model.Customer, this.nccCompany.EditValue as Model.Company);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            IList<Model.AcOtherShouldCollection> list = this.bindingSource1.DataSource as IList<Model.AcOtherShouldCollection>;

            if (list == null || list.Count == 0)
                return;

            string msg = this._acOtherShouldCollectionManager.UpdateAcOtherShouldCollectionList(list);
            MessageBox.Show(msg);
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "colInvoiceId":
                    (this.bindingSource1.DataSource as List<Model.AcOtherShouldCollection>).Where(asc => asc.Checked == true).ToList<Model.AcOtherShouldCollection>().ForEach(asc => asc.InvoiceId = e.Value.ToString());
                    this.gridControl1.RefreshDataSource();
                    break;
                case "colCompany":
                    (this.bindingSource1.DataSource as List<Model.AcOtherShouldCollection>).Where(asc => asc.Checked == true).ToList<Model.AcOtherShouldCollection>().ForEach(asc => asc.CompanyId = e.Value.ToString());
                    this.gridControl1.RefreshDataSource();
                    break;
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name != "colChecked")
            {
                (this.bindingSource1.DataSource as List<Model.AcOtherShouldCollection>).ForEach(asc => asc.Checked = false);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}