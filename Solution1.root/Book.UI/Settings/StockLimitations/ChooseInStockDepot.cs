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
    public partial class ChooseInStockDepot : BaseChooseForm
    {

        #region
        private BL.DepotInManager depotInManager = new BL.DepotInManager();
        #endregion


        public ChooseInStockDepot()
        {
            InitializeComponent();
            this.dateEditStartDate.DateTime = System.DateTime.Now.AddMonths(-3);
            this.dateEditEndate.DateTime = global::Helper.DateTimeParse.EndDate;
            //this.manager = new BL.DepotOutManager();
        }

        protected override void LoadData()
        {
            this.bindingSource1.DataSource = this.depotInManager.SelectByDateRange(this.dateEditStartDate.DateTime, this.dateEditEndate.DateTime.AddDays(1).AddSeconds(-1));
            this.gridControl1.RefreshDataSource();
        }

        protected override BaseEditForm GetEditForm()
        {
            return new DepotInForm();
        }

        private void Button_SearCh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

    }
}