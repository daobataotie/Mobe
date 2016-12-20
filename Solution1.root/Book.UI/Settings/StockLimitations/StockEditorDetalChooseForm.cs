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
    public partial class StockEditorDetalChooseForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.StockEditorDetalManager stockEditorDetalManager = new Book.BL.StockEditorDetalManager();

        public StockEditorDetalChooseForm()
        {
            InitializeComponent();
            this.dateEditEnd.EditValue = this.dateEditStart.EditValue = DateTime.Now;
        }

        private void SerchBtn_Click(object sender, EventArgs e)
        {  
            
            DateTime startDate = Convert.ToDateTime(this.dateEditStart.EditValue);
            DateTime endDate = Convert.ToDateTime(this.dateEditEnd.EditValue);

            if (DateTimeParse.DateTimeEquls(startDate, endDate))
            {
                MessageBox.Show(Properties.Resources .StartDateNotEndDate, this .Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (DateTime.Compare(startDate, endDate) > 0)
            {
                MessageBox.Show(Properties .Resources .StartDateGTEndDate, this .Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.bindingSource1.DataSource = stockEditorDetalManager.GetStockEditorDetalByDate (startDate ,endDate );
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.StockEditorDetal> stockEditorDetal = this.bindingSource1.DataSource as IList<Model.StockEditorDetal>;
            //if (stockEditorDetal == null || stockEditorDetal.Count == 0) return;
            //Model.Product product = stockEditorDetal[e.ListSourceRowIndex].Product;
            //switch (switch_on)
            //{

            //    default:
            //}

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            IList<Model.StockEditorDetal> list = this.bindingSource1.DataSource as IList<Model.StockEditorDetal>;
            
        }

        private void StockEditorDetalChooseForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = stockEditorDetalManager.Select();
        }
    }
}