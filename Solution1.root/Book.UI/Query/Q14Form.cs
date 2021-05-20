using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace Book.UI.Query
{
    //*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究

    // 编 码 人: 裴盾            完成时间:2009-6-4
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    //库存查询
    public partial class Q14Form : BaseForm
    {
        protected BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();
        protected BL.StockManager stockManager = new Book.BL.StockManager();
        private BL.DepotManager depotManager = new BL.DepotManager();
        private BL.DepotPositionManager depotPositionMananger = new BL.DepotPositionManager();
        private BL.ProductCategoryManager productCategoryManager = new BL.ProductCategoryManager();
        private DataTable miscDatas;
        private ServiceReference1.Service1SoapClient s;

        //构造
        public Q14Form()
        {
            InitializeComponent();
            this.barEditItem3.EditValue = false;
            this.bindingSourceDepot.DataSource = this.depotManager.Query(" select DepotId,DepotName from Depot order by Id", 30, "depot").Tables[0];
            this.bindingSourceCategory.DataSource = this.depotManager.Query("select ProductCategoryId,ProductCategoryName,Id from ProductCategory where CategoryLevel=1 order by Id", 30, "ProductCategory").Tables[0];

        }

        #region 重写父类
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R14(this.miscDatas);
        }

        protected override void DoQuery()
        {
            // miscDatas = this.miscDataManager.SelectDataTable("Q14");
            this.bindingSource1.DataSource = miscDatas;

            this.gridView1.GroupPanelText = "当前共有: " + (miscDatas == null ? "0" : miscDatas.Rows.Count.ToString()) + " 条数据";
        }

        #endregion

        private void Q14Form_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton_Search_Click(object sender, EventArgs e)
        {
            if (this.lookUpEditDepot.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.deptNotNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                lookUpEditDepot.Focus();
                return;
            }
            if (this.barEditItem3.EditValue.ToString() == "True")
            {


                miscDatas = s.SelectStock(this.lookUpEditDepot.EditValue == null ? null : this.lookUpEditDepot.EditValue.ToString(), this.lookUpEditDepotPosition.EditValue == null ? null : this.lookUpEditDepotPosition.EditValue.ToString(), this.lookUpEditProductType.EditValue == null ? null : this.lookUpEditProductType.EditValue.ToString(), this.textProductNameOrId.Text);

            }

            else
            {
                miscDatas = this.miscDataManager.SelectByCondition("Q14", this.lookUpEditDepot.EditValue == null ? null : this.lookUpEditDepot.EditValue.ToString(), this.lookUpEditDepotPosition.EditValue == null ? null : this.lookUpEditDepotPosition.EditValue.ToString(), this.lookUpEditProductType.EditValue == null ? null : this.lookUpEditProductType.EditValue.ToString(), this.textProductNameOrId.Text, null, null, null, null, false);
            }

            this.bindingSource1.DataSource = miscDatas;
            this.gridView1.GroupPanelText = "總記錄: " + miscDatas.Rows.Count;
            this.gridControl1.RefreshDataSource();
        }

        private void lookUpEditDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEditDepot.EditValue != null)
            {
                //if (this.barEditItem3.EditValue.ToString() == "True")
                this.bindingSourceDepotPosition.DataSource = this.depotPositionMananger.Select(this.depotManager.Get(this.lookUpEditDepot.EditValue.ToString()));
                this.lookUpEditDepotPosition.EditValue = null;
            }
        }

        private void lookUpEditProductType_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    break;
                case 1:
                    this.lookUpEditProductType.EditValue = null;
                    break;
            }

        }

        private void lookUpEditDepotPosition_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    break;
                case 1:
                    this.lookUpEditDepotPosition.EditValue = null;
                    break;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.miscDatas == null || this.miscDatas.Rows.Count == 0)
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            R14 r = new R14(this.miscDatas);
            r.ShowPreview();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setwcfadd f = new setwcfadd();
            if (f.ShowDialog(this) == DialogResult.OK)
            {

            }

        }




        private void barEditItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.barEditItem3.EditValue.ToString() == "False")
            {
                if (MessageBox.Show("確認連接遠程?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;



                s = new ServiceReference1.Service1SoapClient();


                this.bindingSourceDepot.DataSource = s.Query("select DepotId,DepotName from Depot order by Id", 30, "depot").Tables[0];
                this.bindingSourceCategory.DataSource = s.Query("select ProductCategoryId,ProductCategoryName,Id from ProductCategory order by Id", 30, "ProductCategory").Tables[0];
                this.barEditItem3.EditValue = true;
                return;

            }
            else
            {
                if (MessageBox.Show("確認取消遠程連接?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
                this.bindingSourceDepot.DataSource = this.depotManager.Query("select DepotId,DepotName from Depot order by Id", 30, "depot").Tables[0];
                this.bindingSourceCategory.DataSource = this.depotManager.Query("select ProductCategoryId,ProductCategoryName,Id from ProductCategory order by Id", 30, "ProductCategory").Tables[0];
                this.barEditItem3.EditValue = false;
            }

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Book.UI.StockPrompt f = new StockPrompt();
            f.ShowDialog();
        }

        //private void sbtSeach_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty( this.textEditProName.Text))
        //        this.bindingSource1.DataSource = this.miscDataManager.SelectDataTable("Q14");
        //    else
        //    this.bindingSource1.DataSource = this.stockManager.SelectDataTableProName(this.textEditProName.Text);
        //}

        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    this.bindingSource1.DataSource = this.miscDataManager.SelectDataTable("Q14");
        //}
    }
}