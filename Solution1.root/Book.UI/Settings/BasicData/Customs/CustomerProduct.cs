using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Settings.BasicData.Customs
{
    public partial class CustomerProduct : DevExpress.XtraEditors.XtraForm
    {
        string CustomerIds = null;
        IList<Model.Product> customerProductList;
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.CustomerProductsManager customerProductsManager = new Book.BL.CustomerProductsManager();
        public List<Model.Product> SelectProduct { get; set; }
        private List<Model.Product> ShowProduct = new List<Book.Model.Product>();

        public CustomerProduct()
        {
            InitializeComponent();
        }

        public CustomerProduct(string customerIds)
            : this()
        {
            this.CustomerIds = customerIds;
            this.bindingSource1.DataSource = customerProductList = productManager.SelectAllProductByCustomers(this.CustomerIds, true);
        }

        public CustomerProduct(string customerIds, IList<Model.Product> productList)
            : this()
        {
            CustomerIds = customerIds;
            customerProductList = productManager.SelectAllProductByCustomers(this.CustomerIds, true);

            foreach (var item in customerProductList)
            {
                Model.Product p = productList.FirstOrDefault(d => d.ProductId == item.ProductId);
                if (p != null)
                {
                    item.Checked = true;
                    item.ProductCategoryName = p.ProductCategoryName;
                }
            }
            this.bindingSource1.DataSource = customerProductList;
        }

        private void CustomerProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.gridView2.PostEditor();
            this.gridView2.UpdateCurrentRow();
            SelectProduct = customerProductList.Where(d => d.Checked == true).ToList();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            //foreach (var item in customerProductList)
            //{
            //    item.Checked = this.checkEdit1.Checked;
            //}

            //筛选过后全选，只选中/取消 筛选后的数据
            string proNameCat = null;      //用于筛选商品的关键字，按照这个分类

            if (this.gridView2.RowFilter.Contains("ProductName"))
                proNameCat = this.gridView2.RowFilter.Substring(this.gridView2.RowFilter.IndexOf("'") + 1, this.gridView2.RowFilter.LastIndexOf("'") - this.gridView2.RowFilter.IndexOf("'") - 2);

            foreach (var item in customerProductList)
            {
                if (this.ShowProduct.Count > 0 && this.ShowProduct.Exists(P => P.ProductId == item.ProductId))
                {
                    item.Checked = this.checkEdit1.Checked;

                    //暂用商品类型名称存放该字段
                    item.ProductCategoryName = proNameCat;
                }
            }

            this.gridControl2.RefreshDataSource();
        }

        private void che_IsShowUnuseProduct_CheckedChanged(object sender, EventArgs e)
        {
            SelectProduct = customerProductList.Where(d => d.Checked == true).ToList();
            customerProductList = productManager.SelectAllProductByCustomers(this.CustomerIds, !this.che_IsShowUnuseProduct.Checked);

            if (this.SelectProduct != null && this.SelectProduct.Count > 0)
            {
                foreach (var item in customerProductList)
                {
                    if (this.SelectProduct.Any(d => d.ProductId == item.ProductId))
                        item.Checked = true;
                }
            }

            this.bindingSource1.DataSource = customerProductList;
            this.gridControl2.RefreshDataSource();
        }

        private void gridView2_RowCountChanged(object sender, EventArgs e)
        {
            this.ShowProduct.Clear();

            var showList = gridView2.DataController.GetAllFilteredAndSortedRows();
            if (showList != null && showList.Count > 0)
            {
                foreach (object item in showList)
                {
                    Model.Product p = item as Model.Product;
                    this.ShowProduct.Add(p);
                }
            }
        }
    }
}