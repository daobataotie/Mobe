﻿using System;
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


        public ChooseOutStockDepot()
        {
            InitializeComponent();
            this.dateEditStartDate.DateTime = System.DateTime.Now.Date.AddDays(-15);
            this.dateEditEndate.DateTime = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            //this.manager = new BL.DepotOutManager();
        }

        protected override void LoadData()
        {
            this.bindingSource1.DataSource = this.depotOutDetailManager.SelectByDateRange(this.dateEditStartDate.EditValue == null ? DateTime.Now.AddMonths(-1) : this.dateEditStartDate.DateTime, this.dateEditEndate.EditValue == null ? DateTime.Now : this.dateEditEndate.DateTime, (this.buttonEdit1.EditValue as Model.Product) == null ? "" : (this.buttonEdit1.EditValue as Model.Product).ProductId);
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

    }
}