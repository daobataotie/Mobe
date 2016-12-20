using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.StockLimitations
{
    public partial class ViewOutInRecord : DevExpress.XtraEditors.XtraForm
    {
        private BL.StockManager stockManager = new Book.BL.StockManager();
        string _productid;

        public ViewOutInRecord()
        {
            InitializeComponent();
        }

        public ViewOutInRecord(string productid)
            : this()
        {
            this._productid = productid;

            this.dateEditStart.EditValue = DateTime.Now.Date.AddMonths(-1);
            this.dateEditStop.EditValue = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            this.gridControlStock.DataSource = this.stockManager.SelectReaderByPro(this._productid, this.dateEditStart.DateTime, this.dateEditStop.DateTime);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            this.gridControlStock.DataSource = this.stockManager.SelectReaderByPro(this._productid, this.dateEditStart.DateTime, this.dateEditStop.DateTime);
        }
    }
}