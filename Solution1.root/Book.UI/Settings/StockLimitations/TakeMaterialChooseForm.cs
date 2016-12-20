using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.StockLimitations
{

    //*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究
    // 编 码 人: 刘永亮            完成时间:2010-10-20
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class TakeMaterialChooseForm : Form
    {
        private BL.ProduceMaterialManager _produceMaterialManager = new Book.BL.ProduceMaterialManager();
        private BL.ProduceMaterialdetailsManager _produceMaterialdetailsManager = new Book.BL.ProduceMaterialdetailsManager();
        private Model.ProduceMaterial _produceMaterial = new Book.Model.ProduceMaterial();
        private Model.ProduceMaterialdetails _produceMaterialdetails = new Book.Model.ProduceMaterialdetails();
        IList<Model.ProduceMaterialdetails> list = new List<Model.ProduceMaterialdetails>();

        public TakeMaterialChooseForm()
        {
            InitializeComponent();
            this.dateEditstartdate.DateTime = System.DateTime.Now.Date.AddDays(-5);
            this.dateEditenddate.DateTime = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            this.buttonEditProduct1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(buttonEditProduct1_ButtonClick);
        }

        void buttonEditProduct1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
                this.buttonEditProduct1.EditValue = form.SelectedItem;
        }

        public Model.ProduceMaterial SelectItem
        {
            get { return this.bindingSourceProduceMaterial.Current as Model.ProduceMaterial; }
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceMaterialdetails> list = this.bindingSourceProduceMaterialDetails.DataSource as IList<Model.ProduceMaterialdetails>;
            foreach (Model.ProduceMaterialdetails model in list)
            {
                model.ProductStock = model.Product.StocksQuantity;
            }
            if (list == null || list.Count == 0) return;
            Model.Product product = list[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumnProductId":
                    if (product != null)
                        e.DisplayText = product.Id;
                    break;
            }
        }

        private void sbtn_sure_Click(object sender, EventArgs e)
        {
            OutStockEditForm._produceMaterial.Details.Clear();
            if (list != null)
                foreach (Model.ProduceMaterialdetails item in list)
                {
                    if (item.IsChecked.HasValue && item.IsChecked.Value)
                    {
                        item.ProduceMaterialID = this._produceMaterial.ProduceMaterialID;
                        OutStockEditForm._produceMaterial.Details.Add(item);
                    }
                }

            this.DialogResult = DialogResult.OK;
        }

        private void sbtn_Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void Button_Sure_Click(object sender, EventArgs e)
        {
            this.bindingSourceProduceMaterial.DataSource = this._produceMaterialManager.SelectByDateRage(this.dateEditstartdate.DateTime, this.dateEditenddate.DateTime, this.buttonEditProduct1.EditValue as Model.Product, this.checkEditIsClose.Checked, this.txt_InvoiceCusXOId.Text);     //SelectState();
            foreach (Model.ProduceMaterial model in this.bindingSourceProduceMaterial.DataSource as IList<Model.ProduceMaterial>)
            {
                Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(model.InvoiceXOId);
                if (xo != null)
                    model.CustomerInvoiceXOId = xo.CustomerInvoiceXOId;
            }
            this.gridControl1.RefreshDataSource();
        }

        private void TakeMaterialChooseForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceProduceMaterial.DataSource = this._produceMaterialManager.SelectByDateRage(this.dateEditstartdate.DateTime, global::Helper.DateTimeParse.EndDate, null, false, null);     //SelectState();

            foreach (Model.ProduceMaterial model in this.bindingSourceProduceMaterial.DataSource as IList<Model.ProduceMaterial>)
            {
                Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(model.InvoiceXOId);
                if (xo != null)
                    model.CustomerInvoiceXOId = xo.CustomerInvoiceXOId;
            }
            this.gridControl1.RefreshDataSource();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this._produceMaterial = this.bindingSourceProduceMaterial.Current as Model.ProduceMaterial;
            if (this._produceMaterial != null)
            {
                list = this._produceMaterialdetailsManager.SelectByState(this._produceMaterial);
                foreach (Model.ProduceMaterialdetails model in list)
                {
                    model.ProductStock = model.Product.StocksQuantity;
                }
                this.bindingSourceProduceMaterialDetails.DataSource = list;
            }
            else
                this.bindingSourceProduceMaterialDetails.DataSource = null;
        }

        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            this._produceMaterial = this.bindingSourceProduceMaterial.Current as Model.ProduceMaterial;
            if (this._produceMaterial != null)
            {
                list = this._produceMaterialdetailsManager.SelectByState(this._produceMaterial);
                foreach (Model.ProduceMaterialdetails model in list)
                {
                    model.ProductStock = model.Product.StocksQuantity;
                }
                this.bindingSourceProduceMaterialDetails.DataSource = list;
            }
            else
                this.bindingSourceProduceMaterialDetails.DataSource = null;

        }

        private void checkEditALL_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkEditALL.Checked == true)
            {
                foreach (Model.ProduceMaterialdetails detail in list)
                {
                    detail.IsChecked = true;
                }
            }
            if (checkEditALL.Checked == false)
            {
                foreach (Model.ProduceMaterialdetails detail in list)
                {
                    detail.IsChecked = false;
                }
            }
            this.gridView2.UpdateCurrentRow();
            this.gridView2.PostEditor();
            this.gridControl2.RefreshDataSource();
        }
    }
}
