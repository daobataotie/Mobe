using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceMaterialExit
{
    public partial class ChooseProduceMaterialExit : DevExpress.XtraEditors.XtraForm
    {
        private BL.ProduceMaterialExitManager headerManager = new Book.BL.ProduceMaterialExitManager();
        private BL.ProduceMaterialExitDetailManager detailManager = new Book.BL.ProduceMaterialExitDetailManager();
        public IList<Model.ProduceMaterialExitDetail> detailList;
        public IList<Model.ProduceMaterialExit> HeaderList;

        private IList<string> _key = new List<string>();
        public IList<string> key
        {
            get { return _key; }
            set { _key = value; }
        }
        public ChooseProduceMaterialExit()
        {
            InitializeComponent();
            this.dateEditstartdate.DateTime = DateTime.Now.AddMonths(-1).AddDays(1).AddSeconds(-1);
            this.dateEditenddate.DateTime = DateTime.Now;
        }

        private void spb_search_Click(object sender, EventArgs e)
        {


            HeaderList = headerManager.SelectByCondition(this.dateEditstartdate.DateTime, this.dateEditenddate.DateTime);


            this.bindingSourceProduceMaterial.DataSource = HeaderList;
            if (this.bindingSourceProduceMaterial.Current != null)
            {
                detailList = this.detailManager.Select(this.bindingSourceProduceMaterial.Current as Model.ProduceMaterialExit);
                this.bindingSourceProduceMaterialDetails.DataSource = detailList;
            }
        }
        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceMaterialExitDetail> list = this.bindingSourceProduceMaterialDetails.DataSource as IList<Model.ProduceMaterialExitDetail>;
            if (list == null || list.Count == 0) return;
            //Model.Depot depot = list[e.ListSourceRowIndex].Depot;
            //string depotPosition = list[e.ListSourceRowIndex].DepotPosition;
            Model.Product product = list[e.ListSourceRowIndex].Product;
            //string productUnit = list[e.ListSourceRowIndex].ProductUnit;
            switch (e.Column.Name)
            {
                //case "gridColumnDepotId":
                //    if (depot != null)
                //        e.DisplayText = string.IsNullOrEmpty(depot.DepotId) ? "" : depot.ToString();
                //    break;
                case "gridColumnProductId":
                    if (product != null)
                        e.DisplayText = string.IsNullOrEmpty(product.ProductId) ? "" : product.ToString();
                    break;
                case "gridColumnProductName":
                    if (product != null)
                        e.DisplayText = string.IsNullOrEmpty(product.ProductId) ? "" : product.Id;
                    break;
            }
        }

        private void sbtn_sure_Click(object sender, EventArgs e)
        {
            if (key.Count == 0)
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

       
        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumnCheck")
            {
                Model.ProduceMaterialExitDetail detail = this.gridView2.GetRow(e.RowHandle) as Model.ProduceMaterialExitDetail;

                if ((bool)e.Value)
                {
                    key.Add(detail.ProduceMaterialExitDetailId);
                    //  MrsDetails.Add(this.mRSdetailsManager.Get(detail.MRSdetailsId));
                }
                if (!(bool)e.Value)
                {
                    key.Remove(detail.ProduceMaterialExitDetailId);
                    //  MrsDetails.Remove(this.mRSdetailsManager.Get(detail.MRSdetailsId));
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (this.bindingSourceProduceMaterial.Current != null)
            {
                detailList = this.detailManager.Select(this.bindingSourceProduceMaterial.Current as Model.ProduceMaterialExit);
                foreach (Model.ProduceMaterialExitDetail detail in detailList)
                {
                    if (key != null && key.Contains(detail.ProduceMaterialExitDetailId))
                        detail.IsChecked = true;
                }
                this.bindingSourceProduceMaterialDetails.DataSource = detailList;
            }
        }
        private void ChooseProduceMaterialExit_Load(object sender, EventArgs e)
        {
           HeaderList =this.headerManager.SelectByCondition( this.dateEditstartdate.DateTime, this.dateEditenddate.DateTime);
           this.bindingSourceProduceMaterial.DataSource = HeaderList;

        }
       
    }
}