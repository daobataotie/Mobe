using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceOtherExitMaterial
{
    public partial class ChooseProduceOtherMaterial : XtraForm
    {

        #region
        private BL.ProduceMaterialExitManager produceMaterialExitManager = new Book.BL.ProduceMaterialExitManager();
        private BL.ProduceMaterialExitDetailManager produceMaterialExitDetailManager = new Book.BL.ProduceMaterialExitDetailManager();

        private Model.ProduceMaterialExit produceMaterialExit;

        public Model.ProduceMaterialExit ProduceMaterialExit
        {
            get { return produceMaterialExit; }
            set { produceMaterialExit = value; }
        }
        #endregion

        public ChooseProduceOtherMaterial()
        {
            InitializeComponent();
        }

        private void ChooseProduceOtherMaterial_Load(object sender, EventArgs e)
        {
            this.dateEditStart.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.dateEditend.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
            this.bindingSourceproduceMaterialExit.DataSource = this.produceMaterialExitManager.SelectByCondition(this.dateEditStart.DateTime, this.dateEditend.DateTime);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.produceMaterialExit = this.bindingSourceproduceMaterialExit.Current as Model.ProduceMaterialExit;
            if (this.produceMaterialExit != null)
            {
                if (this.produceMaterialExit.Detail == null)
                    this.produceMaterialExit.Detail = new List<Model.ProduceMaterialExitDetail>();
                this.produceMaterialExit.Detail = this.produceMaterialExitDetailManager.Select(this.produceMaterialExit);
                this.bindingSourceproduceMaterialExitDetail.DataSource = this.produceMaterialExit.Detail;
            }
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceMaterialExitDetail> details = this.bindingSourceproduceMaterialExitDetail.DataSource as IList<Model.ProduceMaterialExitDetail>;
            if (details == null || details.Count < 1) return;
            Model.Product p = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumnproductId":
                    e.DisplayText = p.Id;
                    break;
                case "gridColumnStock":
                    e.DisplayText = p.StocksQuantity.ToString();
                    break;
            }
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            this.produceMaterialExit.Detail = this.bindingSourceproduceMaterialExitDetail.DataSource as List<Model.ProduceMaterialExitDetail>;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton_search_Click(object sender, EventArgs e)
        {
            this.bindingSourceproduceMaterialExit.DataSource = this.produceMaterialExitManager.SelectByCondition(this.dateEditStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStart.DateTime, this.dateEditend.EditValue == null ? DateTime.Now : this.dateEditend.DateTime);
            this.gridControl1.RefreshDataSource();
        }


    }
}
