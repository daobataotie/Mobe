using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Settings.BasicData.Products
{
    public partial class ProductConvertMaterial : DevExpress.XtraEditors.XtraForm
    {
        BL.MaterialManager manage = new Book.BL.MaterialManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        DataTable dt;
        IList<Model.Product> productList;
        public ProductConvertMaterial()
        {
            InitializeComponent();
            this.bindingSourceDepot.DataSource = new BL.DepotManager().Select();
            this.bindingSourceProductCategory.DataSource = new BL.ProductCategoryManager().Select();

            //增加列来存放原料类型
            dt = new DataTable();
            dt.Columns.Add("商品编号", typeof(string));
            dt.Columns.Add("商品名称", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            IList<string> str = this.manage.SelectMaterialCategory();
            foreach (var item in str)
            {
                dt.Columns.Add(item, typeof(string));
                dt.Columns.Add(item + "单重", typeof(string));
            }
        }

        public ProductConvertMaterial(DataTable dt)
            : this()
        {
            DataTable dt2 = dt.Copy();
            dt2.Clear();
            DataRow dr;
            string ProductIds = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0 || dt.Rows[i]["productid"].ToString() != dt.Rows[i - 1]["productid"].ToString())
                {
                    ProductIds += "'" + dt.Rows[i]["productid"].ToString() + "',";
                    dr = dt2.NewRow();
                    dr = dt.Rows[i];
                    dt2.Rows.Add(dr.ItemArray);
                }
                else
                {
                    dt2.Rows[dt2.Rows.Count - 1]["Quantity"] = Convert.ToDouble(dt2.Rows[dt2.Rows.Count - 1]["Quantity"].ToString()) + Convert.ToDouble(dt.Rows[i]["Quantity"].ToString());
                }

            }
            ProductIds = ProductIds.Substring(0, ProductIds.Length - 1);
            this.productList = this.productManager.SelectProductsByProductIds(ProductIds);
            dt2.PrimaryKey = new DataColumn[] { dt2.Columns["productid"] };
            foreach (var item in productList)
            {
                if (dt2.Rows.Find(item.ProductId) != null)
                    item.StocksQuantity = Convert.ToDouble(dt2.Rows.Find(item.ProductId)["Quantity"]);
            }
            this.CountMaterial();
        }

        private void btn_ProductStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_ProductStart.EditValue = (f.SelectedItem as Model.Product).Id;
                this.btn_ProductEnd.EditValue = this.btn_ProductStart.EditValue;
            }
        }

        private void btn_ProductEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_ProductEnd.EditValue = (f.SelectedItem as Model.Product).Id;
            }
        }

        private void lookUpEditDepotIdStart_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEditDepotIdEnd.EditValue == null)
                this.lookUpEditDepotIdEnd.EditValue = this.lookUpEditDepotIdStart.EditValue;
        }

        private void lookUpEditCategoryIdStart_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEditCategoryIdEnd.EditValue == null)
                this.lookUpEditCategoryIdEnd.EditValue = this.lookUpEditCategoryIdStart.EditValue;
        }

        //搜索
        private void btn_Search_Click(object sender, EventArgs e)
        {
            this.productList = this.productManager.SelectProductByCondition(this.btn_ProductStart.EditValue == null ? null : this.btn_ProductStart.EditValue.ToString(), this.btn_ProductEnd.EditValue == null ? null : this.btn_ProductEnd.EditValue.ToString(), this.lookUpEditDepotIdStart.EditValue == null ? null : this.lookUpEditDepotIdStart.EditValue.ToString(), this.lookUpEditDepotIdEnd.EditValue == null ? null : this.lookUpEditDepotIdEnd.EditValue.ToString(), this.lookUpEditCategoryIdStart.EditValue == null ? null : this.lookUpEditCategoryIdStart.EditValue.ToString(), this.lookUpEditCategoryIdEnd.EditValue == null ? null : this.lookUpEditCategoryIdEnd.EditValue.ToString());

            CountMaterial();
        }

        private void CountMaterial()
        {
            dt.Rows.Clear();
            double? a;
            DataRow dr;
            Model.Material model;
            foreach (var item in productList)
            {
                dr = dt.NewRow();
                dr[0] = item.Id;
                dr[1] = item.ProductName;
                dr[2] = item.StocksQuantity == null ? 0 : item.StocksQuantity;
                if (!string.IsNullOrEmpty(item.MaterialIds))
                {
                    string[] materialIds = item.MaterialIds.Split(',');
                    string[] materialnums = item.MaterialNum.Split(',');

                    for (int i = 0; i < materialIds.Length; i++)
                    {
                        model = manage.Get(materialIds[i]);
                        if (model != null)
                        {
                            //dr[model.MaterialCategoryName] = ((Convert.ToDouble(materialnums[i]) * model.JWeight / item.NetWeight * item.StocksQuantity) == null ? "0" : (Convert.ToDouble(materialnums[i]) * model.JWeight / item.NetWeight * item.StocksQuantity).Value + Convert.ToDouble(dr[model.MaterialCategoryName].ToString())).ToString("0.####");
                            dr[model.MaterialCategoryName] = ((Convert.ToDouble(materialnums[i]) * model.JWeight * item.StocksQuantity) == null ? 0 : (Convert.ToDouble(materialnums[i]) * model.JWeight * item.StocksQuantity).Value + Convert.ToDouble(dr[model.MaterialCategoryName].ToString() == "" ? "0" : dr[model.MaterialCategoryName].ToString())).ToString("0.####");
                            dr[model.MaterialCategoryName + "单重"] = ((Convert.ToDouble(materialnums[i]) * model.JWeight) == null ? 0 : (Convert.ToDouble(materialnums[i]) * model.JWeight).Value + Convert.ToDouble(dr[model.MaterialCategoryName + "单重"].ToString() == "" ? "0" : dr[model.MaterialCategoryName + "单重"].ToString())).ToString("0.####");
                        }
                    }
                }
                dt.Rows.Add(dr);
            }

            dr = dt.NewRow();
            dr[0] = "合计：";
            for (int i = 2; i < dt.Columns.Count; i++)
            {
                a = 0;
                foreach (DataRow item in dt.Rows)
                {
                    a += Convert.ToDouble(item[i] is DBNull ? "0" : item[i]);
                }
                dr[i] = a.ToString();
            }
            dt.Rows.Add(dr);
            this.bindingSource1.DataSource = dt;
            foreach (DevExpress.XtraGrid.Columns.GridColumn item in this.gridView1.Columns)
            {
                if (item.VisibleIndex == 0)
                    item.Width = 200;
                else
                    item.Width = 85;
            }
            this.gridControl1.RefreshDataSource();
        }

        //打印
        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Count < 2)
            {
                MessageBox.Show("无数据", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RO ro = new RO(this.dt);
            ro.ShowPreviewDialog();
        }

        private void lookUpEditDepotIdStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                this.lookUpEditDepotIdStart.EditValue = null;
        }

        private void lookUpEditDepotIdEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                this.lookUpEditDepotIdEnd.EditValue = null;
        }

        private void lookUpEditCategoryIdStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                this.lookUpEditCategoryIdStart.EditValue = null;
        }

        private void lookUpEditCategoryIdEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                this.lookUpEditCategoryIdEnd.EditValue = null;
        }
    }
}