using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;


namespace Book.UI.Settings.ProduceManager
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-11-16
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class BOMManagerForm : Settings.BasicData.BaseEditForm
    {
        BL.BomParentPartInfoManager BomparentManager = new Book.BL.BomParentPartInfoManager();
        BL.BomComponentInfoManager BomcomManager = new Book.BL.BomComponentInfoManager();
        Model.BomComponentInfo _bomcom = new Book.Model.BomComponentInfo();
        Model.BomParentPartInfo _bomparent = new Book.Model.BomParentPartInfo();

        Model.BomParentPartInfo _bomparents = new Book.Model.BomParentPartInfo();

        IList<Model.BomComponentInfo> _bomcomDetail = new List<Model.BomComponentInfo>();
        IList<Model.BomComponentInfo> _comDetail = new List<Model.BomComponentInfo>();
        IList<Model.BomComponentInfo> _comDetails = new List<Model.BomComponentInfo>();

        IList<Model.BomComponentInfo> _comDetailss = new List<Model.BomComponentInfo>();



        BL.ProductCategoryManager productCategoryManager = new Book.BL.ProductCategoryManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        Model.Product _product = new Model.Product();
        Model.ProductCategory _productCategory = new Model.ProductCategory();


        //客户产品Bom表定义
        BL.CustomerProductsManager CusProductsManager = new Book.BL.CustomerProductsManager();
     //   BL.CustomerProductsBomManager CusProductsBomManager = new Book.BL.CustomerProductsBomManager();

        Model.CustomerProducts _cusProduct = new Book.Model.CustomerProducts();
        Model.CustomerProducts _cusProducts = new Book.Model.CustomerProducts();
     //   Model.CustomerProductsBom _cusProductsBom = new Book.Model.CustomerProductsBom();

     //   IList<Model.CustomerProductsBom> _cusProductsBomDetail = new List<Model.CustomerProductsBom>();

        // 构造函数
        public BOMManagerForm()
        {
            InitializeComponent();
        }


        #region 重写方法
        protected override void AddNew()
        {
            this.action = "insert";
        }
        public override void Refresh()
        {
            if (this._bomcom == null)
            {
                this.AddNew();
            }
            this.bindingSource1.DataSource = this._bomcomDetail;

            base.Refresh();
        }

        #endregion 


        //绑定
        public IList<Model.BomComponentInfo> band()
        {
            if (this._bomparents != null)
                this._comDetailss.Clear();
            this._comDetailss = this.BomcomManager.Select(_bomparents);

            if (this._comDetailss.Count != 0)
            {
                IList<Model.BomComponentInfo> a = null;
             //   string strlenth = "";
                Model.BomComponentInfo _bomcom = new Book.Model.BomComponentInfo(); ;

                for (int i = 0; i < this._comDetailss.Count; i++)
                // foreach(Model.BomComponentInfo com in this._bomcomDetail)
                {
                   
                        //在物料中查询 是否 存在此子件
                    this._bomparent = this.BomparentManager.Get(_comDetailss[i].Product);


                    if (this._bomparent == null)
                    {
                        if (this._comDetailss[i].Product.IsProcee == true)
                        {

                            this._bomparent = this.BomparentManager.Get(this.productManager.Get(_comDetailss[i].Product.ProceebeforeProductId));
                            
                        }
                    }
                    if (this._bomparent != null)
                    {
                        a = this.BomcomManager.Select(this._bomparent);

                        int m = this._comDetailss.Count;

                        for (int j = i + 1; j < m; j++)
                        {
                            _comDetails.Add(this._comDetailss[i + 1]);
                            this._comDetailss.RemoveAt(i + 1);
                        }

                        foreach (Model.BomComponentInfo bom in a)
                        {
                            bom.Jibie = _comDetailss[i].Jibie + 1;
                            bom.UseQuantity = _comDetailss[i].UseQuantity * bom.UseQuantity;
                            //for (int g = 0; g < bom.Jibie; g++)
                            //{
                            //    strlenth += "    ";
                            //}
                            //if (bom.Product != null)
                            //    bom.Product.Id = strlenth + bom.Product.Id;

                            this._comDetailss.Add(bom);

                            //this._bomcomDetail.RemoveAt
                            //strlenth = "";
                        }

                        foreach (Model.BomComponentInfo boms in _comDetails)
                        {
                            this._comDetailss.Add(boms);
                        }

                        _comDetails.Clear();
                        a.Clear();
                    }
                }
            }

            return _comDetailss;
        }


        /// <summary>
        /// 自定义列显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;
            Model.Product p = null;
            IList<Model.BomComponentInfo> Details = this.bindingSource1.DataSource as IList<Model.BomComponentInfo>;

            if (Details == null || Details.Count <= 0)
                return;
            p = Details[e.ListSourceRowIndex].Product;

            if (p == null) return;

            if (e.Column.Name == this.gridColumnProductID.Name)
            {

                e.DisplayText = p.Id;
            }
        }


        //加载
        private void BOMManagerForm_Load(object sender, EventArgs e)
        {

            this.bindingSourceProduct.DataSource = new BL.ProductManager().Select();
            foreach (Model.ProductCategory cate in productCategoryManager.Select())
            {

                DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { cate.ProductCategoryName }, null, cate.ProductCategoryId);
                _productCategory.ProductCategoryId = cate.ProductCategoryId;

                foreach (Model.Product prodcut in productManager.Select(_productCategory))
                {

                    treeList1.AppendNode(new object[] { prodcut.ProductName }, treeNode, prodcut.ProductId);

                }


            }


        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this._bomparents == null) return;
            MaterialXR f = new MaterialXR(band(), this._bomparents);
            f.ShowPreview();

        }

       
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null && e.Node.ParentNode != null)
            {
                this._bomcomDetail.Clear();
                Model.Product product = productManager.Get(e.Node.Tag.ToString());
                if (product != null)
                {
                    this._bomparent = this.BomparentManager.Get(product);
                    this._bomparents = this._bomparent;                   
                    if (_bomparent != null)
                        //根据母件查询子件
                        this._bomcomDetail = this.BomcomManager.Select(_bomparent);
                    IList<Model.BomComponentInfo> a = null;
                    if (this._bomcomDetail.Count != 0)
                    {
                        for (int i = 0; i < this._bomcomDetail.Count; i++)
                        // foreach(Model.BomComponentInfo com in this._bomcomDetail)
                        {    //在BOM中查询 是否 存在此子件
                            this._bomparent = this.BomparentManager.Get(_bomcomDetail[i].Product);

                            //判定是否加工后产品
                            if (this._bomparent == null)
                            {
                                if (this._bomcomDetail[i].Product.IsProcee == true)
                                {
                                    this._bomparent = this.BomparentManager.Get(this.productManager.Get(_bomcomDetail[i].Product.ProceebeforeProductId));

                                }
                            }
                            //如果存在此子件
                            if (this._bomparent != null)
                            {
                                //将此子件作为BOM 继续 查询他 的子件
                                a = this.BomcomManager.Select(this._bomparent);

                                //将a的子件填进 集合
                                foreach (Model.BomComponentInfo bom in a)
                                {
                                    //子件级别为他的母件级别加1
                                    bom.Jibie = _bomcomDetail[i].Jibie + 1;
                                    bom.Product.Id = this._bomparent.Product.Id + "/" + bom.Product.Id;
                                    this._bomcomDetail.Add(bom);
                                }
                                //清空临时a
                                a.Clear();
                            }
                        }
                    }
                    this.bindingSource1.DataSource = this._bomcomDetail;
                    this.gridControl1.RefreshDataSource();
                }
            }
            //if (e.Node != null && e.Node.ParentNode != null)
            //{
            //    this._cusProductsBomDetail.Clear();
            //    Model.Product product = productManager.Get(e.Node.Tag.ToString());
            //    if (product != null)
            //    {
            //        this._cusProduct = this.CusProductsManager.Get(product);
            //        this._cusProducts = this._cusProduct;
            //        if (_cusProduct != null)
            //            this._cusProductsBomDetail = this.CusProductsBomManager.Select(_cusProduct);
            //        IList<Model.CustomerProductsBom> c= null;
            //        if (this._cusProductsBomDetail.Count != 0)
            //        {
            //            for (int i = 0; i < this._cusProductsBomDetail.Count; i++)
            //            {   
            //                this._cusProduct = this.CusProductsManager.Get(_cusProductsBomDetail[i].Product);
            //               
            //                if (this._cusProduct != null)
            //                {
            //                    c = this.CusProductsBomManager.Select(this._cusProduct);
            //                    foreach (Model.CustomerProductsBom cusBom in c)
            //                    {
            //                        cusBom.Jibie = _cusProductsBomDetail[i].Jibie + 1;
            //                        cusBom.Product.Id = this._cusProduct.Product.Id + "/" + cusBom.Product.Id;
            //                        this._cusProductsBomDetail.Add(cusBom);
            //                    }
            //                    c.Clear();
            //                }
            //            }
            //        }
            //        this.bindingSource1.DataSource = this._cusProductsBomDetail;
            //        this.gridControl1.RefreshDataSource();
            //    }
            //}
        }
    }
}
