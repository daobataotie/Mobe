using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using System.Linq;

namespace Book.UI.Invoices
{
    public partial class ChooseProductForm : Settings.BasicData.BaseChooseForm
    {
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        private Model.Customer _customer;
        private Model.ProductCategory _productCategory;
        private int flag = 0;

        #region Construcotrs
        public static IList<Model.Product> ProductList;
        public ChooseProductForm()
        {

            InitializeComponent();
            this.manager = new Book.BL.ProductCategoryManager();
            ProductList = new List<Model.Product>();
            flag = 1;
            this.comboBoxEditSeach.SelectedIndex = 0;
            this.gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsFind.FindFilterColumns = "ProductName;Id;CustomerProductName;Customer";
        }
        public ChooseProductForm(Model.Customer customer)
            : this()
        {
            this._customer = customer;
        }

        public ChooseProductForm(Model.ProductCategory productCategory)
            : this()
        {
            this._productCategory = productCategory;
        }
        #endregion

        protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.Products.EditForm();
        }

        #region 重写绑定数据方法
        protected override void LoadData()
        {
            this.bindingSource1.DataSource = null;
            // BL.ProductCategoryManager manager = this.manager as BL.ProductCategoryManager;
            //this.productCategoryBindingSource.DataSource = (this.manager as BL.ProductCategoryManager).Select();
            //this.bindingSource1.DataSource = productManager.GetProduct();//productManager.GetProductReader();
            //this.gridControl1.RefreshDataSource();
            if (this._customer != null)
            {
                this.bindingSource1.DataSource = productManager.Select(this._customer);
                this.gridControl1.RefreshDataSource();
            }
            if (this._productCategory != null)
            {
                this.simpleButtonNew.Visible = false;
                //this.productCategoryBindingSource.DataSource = this._productCategory;
                this.bindingSource1.DataSource = productManager.Select(this._productCategory);
                this.gridControl1.RefreshDataSource();
            }
            //else
            //{
            //    this.simpleButtonNew.Visible = true;
            //}

        }
        #endregion

        private void productCategoryBindingSource_CurrentChanged(object sender, EventArgs e)
        {

            //if (flag == 1) return;
            //Cursor.Current = Cursors.WaitCursor;

            //Model.ProductCategory category = this.productCategoryBindingSource.Current as Model.ProductCategory;
            //if (category == null)
            //    return;
            //if (this._customer != null)
            //{
            //    this.bindingSource1.DataSource = this.productManager.Select(this._customer, category);
            //}
            //else
            //{
            //    this.bindingSource1.DataSource = this.productManager.Select(category);
            //}
            //if (this._productCategory != null)
            //{
            //    this.bindingSource1.DataSource = this.productManager.Select(this._productCategory);
            //}
            //this.gridControl1.RefreshDataSource();
            //Cursor.Current = Cursors.Default;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = this.productManager.GetProduct();

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;
            IList<Model.Product> details = this.bindingSource1.DataSource as IList<Model.Product>;
            if (details == null || details.Count <= 0 || details[e.ListSourceRowIndex].IsCustomerProduct == null || details[e.ListSourceRowIndex].IsCustomerProduct == false
                || string.IsNullOrEmpty(details[e.ListSourceRowIndex].CustomerProductName))
                return;
            switch (e.Column.Name)
            {
                case "colProductName":
                    e.DisplayText = details[e.ListSourceRowIndex].ProductName + "{" + details[e.ListSourceRowIndex].CustomerProductName + "}";
                    break;
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (ProductList == null) return;
            if (e.Column.Name == "gridColumnChecked")
            {
                Model.Product product = this.gridView1.GetRow(e.RowHandle) as Model.Product;

                if ((bool)e.Value)
                {
                    ProductList.Add(product);
                }
                if (!(bool)e.Value)
                {
                    ProductList.Remove(product);
                }
            }
        }

        private void simpleButtonSeach_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textEditSeach.Text)) return;
            if (comboBoxEditSeach.SelectedIndex == 0)
                this.bindingSource1.DataSource = this.productManager.SelectALLIdOrNameKey(null, this.textEditSeach.Text, null);
            if (comboBoxEditSeach.SelectedIndex == 1)
                this.bindingSource1.DataSource = this.productManager.SelectALLIdOrNameKey(this.textEditSeach.Text, null, null);
            if (comboBoxEditSeach.SelectedIndex == 2)
                this.bindingSource1.DataSource = this.productManager.SelectALLIdOrNameKey(null, null, this.textEditSeach.Text);
        }

        private void ChooseProductForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //    this.Dispose();
            //    System.GC.Collect();

        }

        private void ChooseProductForm_Load(object sender, EventArgs e)
        {
            //listBoxControl1.DataSource = (this.manager as BL.ProductCategoryManager).Select();
            //listBoxControl1.SelectedIndex = -1;
            IList<Model.ProductCategory> listCategory = (this.manager as BL.ProductCategoryManager).SelectAll();
            List<Model.ProductCategory> firstLevel = listCategory.Where(l => l.CategoryLevel == 1).ToList();
            List<Model.ProductCategory> secondLevel = listCategory.Where(l => l.CategoryLevel == 2).ToList();
            List<Model.ProductCategory> thirdLevel = listCategory.Where(l => l.CategoryLevel == 3).ToList();

            foreach (Model.ProductCategory first in firstLevel)
            {
                TreeListNode firstNode = this.treeList1.AppendNode(new object[] { first.Id + "-" + first.ProductCategoryName }, null);
                firstNode.Tag = first;

                List<Model.ProductCategory> list2 = secondLevel.Where(l => l.ProductCategoryParentId == first.ProductCategoryId).ToList();
                foreach (Model.ProductCategory second in list2)
                {
                    TreeListNode secondNode = this.treeList1.AppendNode(new object[] { second.Id + "-" + second.ProductCategoryName }, firstNode);
                    secondNode.Tag = second;

                    List<Model.ProductCategory> list3 = thirdLevel.Where(l => l.ProductCategoryParentId == second.ProductCategoryId).ToList();
                    foreach (Model.ProductCategory third in list3)
                    {
                        this.treeList1.AppendNode(new object[] { third.Id + "-" + third.ProductCategoryName }, secondNode).Tag = third;
                    }
                }
            }
        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            var list = this.bindingSource1.DataSource as IList<Model.Product>;
            if (list != null)
                foreach (var item in list)
                {
                    item.Checked = chk_All.Checked;

                    if (chk_All.Checked)
                        ProductList.Add(item);
                }

            this.gridControl1.RefreshDataSource();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (flag == 1)
            {
                flag = 0;
                return;
            }
            else
            {
                if (treeList1.FocusedNode != null)
                {
                    Model.ProductCategory category = treeList1.FocusedNode.Tag as Model.ProductCategory;
                    if (category != null)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        this.bindingSource1.DataSource = this.productManager.Select(category);

                        this.gridControl1.RefreshDataSource();
                    }
                }
            }
        }

    }
}