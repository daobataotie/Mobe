using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.StockLimitations
{
    public partial class ChooseDepotIn : DevExpress.XtraEditors.XtraForm
    {
        public Model.DepotIn _depotIn = new Book.Model.DepotIn();
        BL.DepotInManager _depotInManager = new Book.BL.DepotInManager();

        public ChooseDepotIn()
        {
            InitializeComponent();
            this.date_End.DateTime = DateTime.Now;
            this.date_Start.DateTime = DateTime.Now.Date.AddDays(-15);
            this.newChooseEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseEmployee0.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.bindingSourceDepot.DataSource = (new BL.DepotManager()).Select();

            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void btn_Serach_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期區間不能為空！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.bindingSourceDepotIn.DataSource = _depotInManager.SelectByDateAndOther(this.date_Start.DateTime, this.date_End.DateTime, this.btne_Product.EditValue as Model.Product, this.txt_DepotInId.Text == "" ? null : this.txt_DepotInId.Text, this.newChooseEmployee.EditValue as Model.Employee, this.newChooseEmployee0.EditValue as Model.Employee, this.lookUpEditDepot.EditValue == null ? null : this.lookUpEditDepot.EditValue.ToString(), this.newChooseSupplier.EditValue as Model.Supplier);
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this._depotIn = this.bindingSourceDepotIn.Current as Model.DepotIn;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btne_Product_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.SelectedItem != null)
                {
                    this.btne_Product.EditValue = f.SelectedItem;
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this._depotIn = this.bindingSourceDepotIn.Current as Model.DepotIn;
            this.DialogResult = DialogResult.OK;
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            IList<Model.DepotIn> list = this.bindingSourceDepotIn.DataSource as IList<Model.DepotIn>;
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
    }
}