using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.ProduceOtherMaterial
{

    //*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究
    // 编 码 人: 刘永亮            完成时间:2010-10-20
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class ChooseProduceOtherMaterialForm : Form
    {
        private BL.ProduceOtherMaterialManager _ProduceOtherMaterialManager = new Book.BL.ProduceOtherMaterialManager();
        private BL.ProduceOtherMaterialDetailManager _ProduceOtherMaterialDetailManager = new Book.BL.ProduceOtherMaterialDetailManager();
        private Model.ProduceOtherMaterial _ProduceOtherMaterial = new Book.Model.ProduceOtherMaterial();
        private Model.ProduceOtherMaterialDetail _ProduceOtherMaterialDetail = new Book.Model.ProduceOtherMaterialDetail();
        public ChooseProduceOtherMaterialForm()
        {
            InitializeComponent();
            this.dateEditStartdate.DateTime = System.DateTime.Now.AddMonths(-1);
            this.dateEditenddate.DateTime = System.DateTime.Now.AddDays(1).AddSeconds(-1);
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherMaterialDetail> list = this.bindingSourceProduceOtherMaterialDetail.DataSource as IList<Model.ProduceOtherMaterialDetail>;
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

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherMaterial> list = this.bindingSourceProduceOtherMaterial.DataSource as IList<Model.ProduceOtherMaterial>;
            if (list == null || list.Count == 0) return;
            Model.Employee employee0 = list[e.ListSourceRowIndex].Employee0;
            Model.Employee employee1 = list[e.ListSourceRowIndex].Employee1;
            Model.WorkHouse workHouse = list[e.ListSourceRowIndex].WorkHouse;
            Model.Depot depot = list[e.ListSourceRowIndex].Depot;
            switch (e.Column.Name)
            {
                case "gridColumnDepot":
                    if (depot != null)
                        e.DisplayText = string.IsNullOrEmpty(depot.DepotId) ? "" : depot.ToString();
                    break;
                case "gridColumnEmployee0Id":
                    if (employee0 != null)
                        e.DisplayText = string.IsNullOrEmpty(employee0.EmployeeId) ? "" : employee0.ToString();
                    break;
                case "gridColumnWorkHouseId":
                    if (workHouse != null)
                        e.DisplayText = string.IsNullOrEmpty(workHouse.WorkHouseId) ? "" : workHouse.ToString();
                    break;
                case "gridColumnEmployee1Id":
                    if (employee1 != null)
                        e.DisplayText = string.IsNullOrEmpty(employee1.EmployeeId) ? "" : employee1.ToString();
                    break;
            }
        }

        private void sbtn_sure_Click(object sender, EventArgs e)
        {
            IList<Model.ProduceOtherMaterialDetail> list = this.bindingSourceProduceOtherMaterialDetail.DataSource as IList<Model.ProduceOtherMaterialDetail>;
            Settings.StockLimitations.OutStockEditForm._ProduceOtherMaterial.Details.Clear();
            if (list != null)
                foreach (Model.ProduceOtherMaterialDetail item in list)
                {
                    if (item.IsChecked.HasValue && item.IsChecked.Value == true)
                    {
                        item.ProduceOtherMaterialId = this._ProduceOtherMaterial.ProduceOtherMaterialId;
                        Settings.StockLimitations.OutStockEditForm._ProduceOtherMaterial.Details.Add(item);
                    }
                }

            this.DialogResult = DialogResult.OK;
        }

        private void sbtn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this._ProduceOtherMaterial = this.bindingSourceProduceOtherMaterial.Current as Model.ProduceOtherMaterial;
            if (this._ProduceOtherMaterial == null)
                this.bindingSourceProduceOtherMaterialDetail.DataSource = null;
            else
                this.bindingSourceProduceOtherMaterialDetail.DataSource = this._ProduceOtherMaterialDetailManager.Select(this._ProduceOtherMaterial);
        }

        private void Button_Sure_Click(object sender, EventArgs e)
        {
            this.bindingSourceProduceOtherMaterial.DataSource = this._ProduceOtherMaterialManager.SelectByDateRange(this.dateEditStartdate.DateTime, this.dateEditenddate.DateTime,this.checkEdit1.Checked);
        }

        private void ChooseProduceOtherMaterialForm_Load(object sender, EventArgs e)
        {
            this.Button_Sure.PerformClick();
        }
    }
}
