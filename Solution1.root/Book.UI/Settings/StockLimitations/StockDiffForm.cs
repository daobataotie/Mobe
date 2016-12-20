using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Helper;

namespace Book.UI.Settings.StockLimitations
{
    public partial class StockDiffForm : DevExpress.XtraEditors.XtraForm
    {
        private DataSet _ds;
        private BL.StockCheckDetailManager stockCheckDetailManager = new Book.BL.StockCheckDetailManager();
        public StockDiffForm()
        {
            InitializeComponent();

        }

        private void StockDiffForm_Load(object sender, EventArgs e)
        {
            this._ds = this.stockCheckDetailManager.SelectDataSet();
            this.bindingSource1.DataSource = this._ds.Tables[0];
            this.dateEditEnd.EditValue = this.dateEditStart.EditValue = DateTime.Now;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (!this._ds.HasChanges()) return;
            this.stockCheckDetailManager.UpdateDataTable(this._ds.GetChanges().Tables[0]);
            this._ds.AcceptChanges();
            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            DataTable table = this.bindingSource1.DataSource as DataTable;
            if (table == null || table.Rows.Count < 1) return;

            decimal old = decimal.Parse(table.Rows[e.ListSourceRowIndex]["StockCheckQuantity"].ToString());
            decimal quantity = decimal.Parse(table.Rows[e.ListSourceRowIndex]["StockCheckBookQuantity"].ToString());
            switch (e.Column.Name)
            {
                case "gridColumn8":
                    e.DisplayText = (quantity - old).ToString();
                    break;
                //case "gridColumnProductDescription":
                //    e.DisplayText = table.Rows[e.ListSourceRowIndex]["ProductDescription"];
                //    break;

            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        { 

            DateTime startDate=Convert .ToDateTime(this.dateEditStart.EditValue);
            DateTime endDate = Convert.ToDateTime(this.dateEditEnd.EditValue);
            if (DateTimeParse.DateTimeEquls(startDate, endDate))
            {
                MessageBox.Show("起始时间不能和结束时间相等！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (DateTime.Compare(startDate, endDate) > 0)
            {
                MessageBox.Show("起始时间不能大于结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataSet ds=this.stockCheckDetailManager.SelectDataSet(startDate,endDate);
            this.bindingSource1.DataSource = ds.Tables[0];
            
        }
    }
}