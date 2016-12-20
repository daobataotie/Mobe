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

// 编 码 人: 裴盾            完成时间:2009-11-18
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class BomManagerForm2 : Settings.BasicData.BaseListForm
    {
        //活动的树的 Tag
        private string eTag = "";

        BL.BomParentPartInfoManager BomparentManager = new Book.BL.BomParentPartInfoManager();
        BL.BomComponentInfoManager BomcomManager = new Book.BL.BomComponentInfoManager();
        Model.BomComponentInfo _bomcom = new Book.Model.BomComponentInfo();
        Model.BomParentPartInfo _bomparent = new Book.Model.BomParentPartInfo();
        Model.BomParentPartInfo _bomparents = new Book.Model.BomParentPartInfo();
        IList<Model.BomComponentInfo> _comDetail = new List<Model.BomComponentInfo>();
        IList<Model.BomComponentInfo> _comDetails = new List<Model.BomComponentInfo>();
        IList<Model.BomComponentInfo> _comDetailss = new List<Model.BomComponentInfo>();
        BL.ProductCategoryManager productCategoryManager = new Book.BL.ProductCategoryManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        Model.Product _product = new Model.Product();
        Model.ProductCategory _productCategory = new Model.ProductCategory();
        BL.CustomerManager customerManager = new Book.BL.CustomerManager();

        //构造函数
        public BomManagerForm2()
        {
            InitializeComponent();
            this.manager = new BL.BomComponentInfoManager();
        }


        #region 重写父类方法

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            //return new BomEdit(this._product);
            return new BomEdit();
        }
        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
           // Type type = typeof(BOMManagerForm);
           return new BomEdit(this._bomparent);
            //if (this._bomparent != null)
            //{
            //    BomEdit f = new BomEdit(this._bomparent);
            //    f.Show();
            //}
           // return null;

          //  return (BOMManagerForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }
        #endregion


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BomManagerForm2_Load(object sender, EventArgs e)
        {

           // this.WindowState = FormWindowState.Maximized;
            band();


            if (treeList1.Nodes.Count!=0)
            {
                this._product = productManager.Get(treeList1.Nodes[0].Tag.ToString());
                this._bomparent = this.BomparentManager.Get(this._product);
                this._comDetails.Clear();

                this._comDetails = bomComponenmtInfoByBom(this._bomparent);
                this.bindingSource1.DataSource = this._comDetails;
                this.gridControl1.RefreshDataSource();
            }



        }
        public IList<Model.BomComponentInfo> band()
        { 
           TreeListNode node2 = null;
           TreeListNode node3 = null;
           TreeListNode node4 = null;
           IList<Model.BomComponentInfo> bomcomList=null ;
           IList<Model.BomComponentInfo> bomcomList1 = null;
           IList<Model.BomComponentInfo> bomcomList2 = null;
            treeList1.Nodes.Clear();
            if (this._bomparents != null)
                this._comDetailss.Clear();
          //  int flag = 0;

            foreach (Model.BomParentPartInfo _bomparents in this.BomparentManager.SelectNotContent())
            {


                TreeListNode node0 = treeList1.AppendNode(new object[] { _bomparents.Product.IsCustomerProduct == true ? _bomparents.Product.CustomerProductName : _bomparents.Product.ProductName }, null, _bomparents.Product.IsCustomerProduct == true ?
                  _bomparents.ProductId+"customer"+_bomparents.CustomerId : _bomparents.ProductId);
                TreeListNode node1 = null;
               //客户BOM

               // if (_bomparents.IsCustomerProcut == true)
              //  {
                    this._comDetailss = this.BomcomManager.Select(_bomparents);

                    foreach (Model.BomComponentInfo comm in this._comDetailss)
                    {
                        node1 = this.treeList1.AppendNode(new object[] { comm.Product.ProductName }, node0, comm.ProductId);

                        //如果母件是客户BOM
                        if (_bomparents.IsCustomerProcut == true && _bomparents.Customer != null)
                        {
                            this._bomparent = this.BomparentManager.Get(comm.Product, comm.Bom.Customer); 
                        }
                        else
                        {
                            this._bomparent = this.BomparentManager.Get(comm.Product); 
                        }
                        if (this._bomparent == null) continue;

                        bomcomList = this.BomcomManager.Select(this._bomparent);

                        foreach (Model.BomComponentInfo com in bomcomList)
                        {

                            node2 = this.treeList1.AppendNode(new object[] { com.Product.ProductName }, node1, com.ProductId);
                            //4ceng
                            

                            //如果母件是客户BOM
                            if (com.Bom.IsCustomerProcut == true && com.Bom.Customer != null)
                            {
                                this._bomparent = this.BomparentManager.Get(com.Product, com.Bom.Customer)==null?                                this.BomparentManager.Get(com.Product):this.BomparentManager.Get(com.Product, com.Bom.Customer);
                            }
                            else
                            {
                                this._bomparent = this.BomparentManager.Get(com.Product);
                            }
                            if (this._bomparent == null) continue;


                            bomcomList1 = this.BomcomManager.Select(this._bomparent);
                            foreach (Model.BomComponentInfo co in bomcomList1)
                            {

                                node3 = this.treeList1.AppendNode(new object[] { co.Product.ProductName }, node2, co.ProductId);

                                //5ceng
                            

                                //如果母件是客户BOM
                                if (co.Bom.IsCustomerProcut == true && co.Bom.Customer != null)
                                {
                                    this._bomparent = this.BomparentManager.Get(co.Product, co.Bom.Customer) == null ? this.BomparentManager.Get(co.Product) : this.BomparentManager.Get(co.Product, co.Bom.Customer);
                                }
                                else
                                {
                                    this._bomparent = this.BomparentManager.Get(co.Product);
                                }
                                if (this._bomparent == null) continue;

                                bomcomList2 = this.BomcomManager.Select(this._bomparent);
                                foreach (Model.BomComponentInfo bomcom in bomcomList2)
                                {

                                    node4 = this.treeList1.AppendNode(new object[] { bomcom.Product.ProductName }, node3, bomcom.ProductId);

                                }
                                //

                            }




                        }

                //    }
                }


            }
            return null;// _comDetailss;
        }


        #region tree节点光标改变触发事件
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
           
            string productid=e.Node.Tag.ToString();
            if (e.Node != null)
            {
                if (e.Node.Tag.ToString().IndexOf("customer") >= 0)
                {
                    this._product=this.productManager.Get(productid.Substring(0, productid.IndexOf("customer")));
                    this._bomparent = this.BomparentManager.Get(this.productManager.Get(productid.Substring(0, productid.IndexOf("customer"))), this.customerManager.Get(productid.Substring(productid.IndexOf("customer") + 8)));
                }
                else
                {
                    // this.action = "view";
                    this._product = productManager.Get(e.Node.Tag.ToString());
                    this._bomparent = this.BomparentManager.Get(this._product);
                }

               this._comDetails.Clear();
              // if ( this._bomparent!= null)
                //{
                  
                this._comDetails= bomComponenmtInfoByBom( this._bomparent);
                this.bindingSource1.DataSource = this._comDetails;
                this.gridControl1.RefreshDataSource();
                //}
            }
        }
        #endregion


        private IList<Model.BomComponentInfo> bomComponenmtInfoByBom(Model.BomParentPartInfo bomparent)
        {
            return BomcomManager.Select(bomparent);     
        }

        /// <summary>
        /// 自定义列显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.BomComponentInfo> details = this.bindingSource1.DataSource as IList<Model.BomComponentInfo>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                    case "gridColumnProduct":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
            }
        }

        #region 重写gridview双击事件
        public  override void gridView1_DoubleClick(object sender, EventArgs e)
        {
             Model.BomComponentInfo detail=  this.bindingSource1.Current as Model.BomComponentInfo;
             BomEdit f1 = new BomEdit(this._bomparent,"view", detail);
            
             f1.MdiParent = this.MdiParent;
             f1.Show();
             //if (f1.ShowDialog() != DialogResult.OK) return;

        }
        #endregion 



        protected override void RefreshData()
        {
            band();
        }
        public override void updates()
        {           
                if(!string.IsNullOrEmpty(eTag))
                {   this._product = productManager.Get(eTag);
                    this._bomparent = this.BomparentManager.Get(this._product);
                    if (this._bomparent != null)
                    {
                        BomEdit bomEdit = new BomEdit(this._bomparent);
                        if (bomEdit != null)
                        {
                            if (bomEdit.ShowDialog() == DialogResult.OK)
                            {
                                RefreshData();
                                this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
                            }
                            this.bindingSource1.Position = this.bindingSource1.IndexOf(bomEdit.EditedItem);
                        }
                    }
                }
               
               
           
             
        }
        


    }
}