using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;

namespace Book.UI.Settings.StockLimitations
{
    public partial class ChooseOutStockDepot : BaseChooseForm
    {

        #region
        private BL.DepotOutDetailManager depotOutDetailManager = new BL.DepotOutDetailManager();

        #endregion

        int tag = 0;
        public ChooseOutStockDepot()
        {
            InitializeComponent();
            this.dateEditStartDate.DateTime = System.DateTime.Now.Date.AddDays(-15);
            this.dateEditEndate.DateTime = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            //this.manager = new BL.DepotOutManager();
            this.bindingSourceDepot.DataSource = (new BL.DepotManager()).Select();
        }

        public ChooseOutStockDepot(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            this.bindingSource1.DataSource = this.depotOutDetailManager.SelectByDateRange(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, invoiceCusId, null);
            this.gridControl1.RefreshDataSource();
        }

        protected override void LoadData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = this.depotOutDetailManager.SelectByDateRange(this.dateEditStartDate.EditValue == null ? DateTime.Now.AddMonths(-1) : this.dateEditStartDate.DateTime, this.dateEditEndate.EditValue == null ? DateTime.Now : this.dateEditEndate.DateTime, (this.buttonEdit1.EditValue as Model.Product) == null ? "" : (this.buttonEdit1.EditValue as Model.Product).ProductId, this.txt_InvoiceCusID.Text, (this.lookUpEditDepot.EditValue == null ? null : this.lookUpEditDepot.EditValue.ToString()));
            this.gridControl1.RefreshDataSource();
        }

        protected override BaseEditForm GetEditForm()
        {
            return new OutStockEditForm();
        }

        private void Button_SearCh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.buttonEdit1.EditValue = f.SelectedItem as Model.Product;
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            IList<Model.DepotOutDetail> list = this.bindingSource1.DataSource as IList<Model.DepotOutDetail>;
            if (list == null || list.Count < 1)
            {
                MessageBox.Show("无数据！", this.Text, MessageBoxButtons.OK);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "选择保存路径";
            sfd.AddExtension = true;
            sfd.DefaultExt = ".xlsx";
            sfd.Filter = "Excel文件(*.xlsx)|*.xlsx";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.gridView1.OptionsPrint.AutoWidth = true;

                this.gridView1.ExportToXlsx(sfd.FileName, new DevExpress.XtraPrinting.XlsxExportOptions { ShowGridLines = true });

                MessageBox.Show("导出成功！", this.Text);
            }
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Model.DepotOutDetail detail = this.bindingSource1.Current as Model.DepotOutDetail;
            if (detail != null)
            {
                OutStockEditForm f = new OutStockEditForm(detail.DepotOutId);
                f.Show(this);
            }
        }
    }
}