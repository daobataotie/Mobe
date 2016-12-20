using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

//物料清单
namespace Book.UI.Settings.ProduceManager
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-11-12
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    public partial class BomList : Settings.BasicData.BaseListForm
    {
        private int flag = 0;
        public BomList()
        {
            InitializeComponent();
            this.manager = new BL.BomParentPartInfoManager();
        }
        /// <summary>
        /// 1:成品一览
        /// </summary>
        /// <param name="i"></param>
        public BomList(int i):this()
        {
            flag = i;         
           
        }
        protected override void RefreshData()
        {
            if (flag == 1)
            {              
                    this.bindingSource1.DataSource = (this.manager as BL.BomParentPartInfoManager).SelectNotContentDataSet().Tables[0];             
            }
            else
            {
                this.bindingSource1.DataSource = (this.manager as BL.BomParentPartInfoManager).SelectDataSet().Tables[0];
            }
        }
        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new BomEdit();
        }
        /// <summary>
        /// 重写父类方法 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(BomEdit);
            return (BomEdit)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }


        //自定义列显示
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.BomParentPartInfo> details = this.bindingSource1.DataSource as IList<Model.BomParentPartInfo>;
            //if (details == null || details.Count < 1) return;
            //Model.Product product = details[e.ListSourceRowIndex].Product;
            //if (product == null) return;
            //switch (e.Column.Name)
            //{
            //    case "ProductId":
            //        e.DisplayText = product.Id;
            //        break;
            //    case "gridColumnName":
            //        e.DisplayText =string.IsNullOrEmpty(product.CustomerProductName)? product.ProductName: product.ProductName +"{"+ product.CustomerProductName+"}";
            //        break;
            //    case "gridColumnCustomer":
            //        e.DisplayText = product.Customer==null?"":product.Customer.CustomerShortName;
            //        break;


            //}
        }
        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                //BaseEditForm f1 = this.GetEditForm(new object[] { this.bindingSource1.Current, "view" });

                ////  f1.Show();
                //// 

                //if (f1.ShowDialog() == DialogResult.OK)
                //{
                //    f1.MdiParent = this.MdiParent;
                //    RefreshData();
                //}
                this.DialogResult = DialogResult.OK;
            }          
            
        }
      
        //public Model.BomParentPartInfo bomSelect
        //{
        //    get { return this.bindingSource1.Current as Model.BomParentPartInfo; }
         
        //}
    }
}