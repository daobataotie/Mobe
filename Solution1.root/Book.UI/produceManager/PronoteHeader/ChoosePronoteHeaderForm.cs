using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PronoteHeader
{
    //*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究

    // 编 码 人:  裴盾             完成时间:2010-03-7
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/
    public partial class ChoosePronoteHeaderForm : DevExpress.XtraEditors.XtraForm
    {
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        Model.PronoteHeader pronoteHeader = new Book.Model.PronoteHeader();
        BL.PronotedetailsMaterialManager pronotedetailsMaterialManager = new Book.BL.PronotedetailsMaterialManager();
        Model.PronotedetailsMaterial pronotedetailsMaterial = new Book.Model.PronotedetailsMaterial();
        public static IList<Model.PronotedetailsMaterial> _PronotedetailsMaterialList;
        //BL.PronotedetailsManager PronotedetailsManager = new Book.BL.PronotedetailsManager();
       // IList<Model.Pronotedetails> _Pronotedetaillist = new List<Model.Pronotedetails>();


       // Model.Pronotedetails _pronotedetail = new Book.Model.Pronotedetails();
        //Model.BomParentPartInfo _bomParentPartInfo = new Book.Model.BomParentPartInfo();
        //Model.BomComponentInfo _bomComponentInfo = new Book.Model.BomComponentInfo();
        //IList<Model.BomComponentInfo> _BomComponentInfoList = new List<Model.BomComponentInfo>();

        //BL.BomComponentInfoManager bomComponentInfoManager = new Book.BL.BomComponentInfoManager();
        //BL.BomParentPartInfoManager bomParentPartInfoManager = new Book.BL.BomParentPartInfoManager();
        private int sourceType = 0;
        public ChoosePronoteHeaderForm()
        {
            InitializeComponent();
            this.newChooseCustomer.Choose=new Settings.BasicData.Customs.ChooseCustoms();
            //this.details = this.pronoteHeaderManager.Select();
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddMonths(-1).Date;
            this.dateEditEndDate.DateTime = DateTime.Now; 
            _PronotedetailsMaterialList = new List<Model.PronotedetailsMaterial>();
        }
        /// <summary>
        /// 1领料单 2退料单
        /// </summary>
        /// <param name="sourceType"></param>
        public ChoosePronoteHeaderForm(int sourceType)
            : base()
        {
            this.sourceType = sourceType;   
        }


        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //foreach (Model.Pronotedetails Pronotedetails in this._Pronotedetaillist)
            //{
            //    if (Pronotedetails.Checkeds == true)
            //    {
            //        produceManager.ProduceMaterial.EditForm._pronotedetails.Add(Pronotedetails);
            //        ProduceMaterialExit.EditForm._pronotedetails.Add(Pronotedetails);
            //        ProduceInDepot.EditForm._pronotedetails.Add(Pronotedetails);
            //    }
            //}

            this.DialogResult = DialogResult.OK;
        }


        /// <summary>
        /// 加载指定数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChoosePronoteHeaderForm_Load(object sender, EventArgs e)
        {
           // this.bindingSource1.DataSource = this.pronoteHeaderManager.GetByDate(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate,null,null,null,null,null,-1,null,true);
        }


        /// <summary>
        /// 自定义列显示 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

            if (e.ListSourceRowIndex < 0) return;
            IList<Model.PronotedetailsMaterial> details = this.bindingSource2.DataSource as IList<Model.PronotedetailsMaterial>;
            if (details == null || details.Count < 1) return;
            Model.PronotedetailsMaterial detail = details[e.ListSourceRowIndex];
            Model.Product product = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumn2Id":
                    if (product == null) return;
                    e.DisplayText = string.IsNullOrEmpty(product.Id) ? "" : product.Id;
                    break;
                //case "gridColumnguige":
                //    if (detail == null) return;
                //    e.DisplayText = detail.Product == null ? "" : detail.Product.ProductSpecification;
                //    break;
                case "gridColumnMaterial":
                    if (product == null) return;
                    e.DisplayText = product.ProduceMaterialDistributioned.HasValue ? product.ProduceMaterialDistributioned.ToString() : "0";
                    break;
                case "OtherMaterialDistributioned":
                    if (product == null) return;
                    e.DisplayText = product.OtherMaterialDistributioned.HasValue ? product.OtherMaterialDistributioned.ToString() : "0";
                    break;
                case "ProductStock":
                    if (product == null) return;
                    e.DisplayText = product.StocksQuantity.HasValue ? product.StocksQuantity.ToString() : "0";
                    break;
            }

        }

        //取消
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            GC.Collect();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.PronoteHeader> details = this.bindingSource1.DataSource as IList<Model.PronoteHeader>;
            if (details == null || details.Count < 1) return;
            Model.Product product = details[e.ListSourceRowIndex].Product;      
            switch (e.Column.Name)
            {
                case "gridColumn1Id":
                    if (product == null) return;
                    e.DisplayText = product.Id;
                    break;
                //    if (detail == null) return;
                //    e.DisplayText = detail.WorkHouse == null ? "" : detail.WorkHouse.Workhousename;
                //    break;
                case "gridColumn1CustomerProName":
                    if (product == null) return;
                    e.DisplayText = string.IsNullOrEmpty(product.CustomerProductName) ? "" : product.CustomerProductName;
                    break;
                //case "gridColumn5":
                //    if (detail == null) return;
                //case "gridColumn3":
                //    e.DisplayText = Pronotedetails.Product == null ? "" : Pronotedetails.Product.Id;

                //    break;
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            this.pronoteHeader = this.bindingSource1.Current as Model.PronoteHeader;
            if (pronoteHeader!=null)
            this.bindingSource2.DataSource = this.pronotedetailsMaterialManager.GetByHeader(this.pronoteHeader);

        }

        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
             if (_PronotedetailsMaterialList == null) return;
            if (e.Column.Name == "gridColumnChecked")
            {
                Model.PronotedetailsMaterial pronotedetailsMaterial = this.gridView2.GetRow(e.RowHandle) as Model.PronotedetailsMaterial;

                if ((bool)e.Value)
                {
                    _PronotedetailsMaterialList.Add(pronotedetailsMaterial);
                }
                if (!(bool)e.Value)
                {
                    _PronotedetailsMaterialList.Remove(pronotedetailsMaterial);
                }
            }
            
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            DateTime startTime = global::Helper.DateTimeParse.NullDate;
            DateTime endTime = global::Helper.DateTimeParse.EndDate;
            if (this.dateEditStartDate.EditValue != null)
            {
                startTime = this.dateEditStartDate.DateTime;
            }
            if (this.dateEditEndDate.EditValue != null)
            {
                endTime = this.dateEditEndDate.DateTime.Date.AddDays(1).AddSeconds(-1);
            }
            this.bindingSource1.DataSource = this.pronoteHeaderManager.GetByDateMa(startTime, endTime, this.newChooseCustomer.EditValue as Model.Customer, this.textEditCusXOId.Text == "" ? null : this.textEditCusXOId.Text, this.buttonEditPro.EditValue as Model.Product,null,null,-1,null,false, this.TXTproNameKey.Text,this.TXTproCusNameKey.Text, this.txtpronoteHeaderIdKey.Text); 
        }

        private void buttonEditPro_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPro.EditValue = form.SelectedItem as Model.Product;

            }
            form.Dispose();
            GC.Collect();
        }

        //private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    _Pronotedetaillist.Clear();
        //    this.gridControl2.RefreshDataSource();
        //    this._pronotedetail = this.bindingSource1.Current as Model.Pronotedetails;
        //    if (this._pronotedetail.Product != null && this.bomParentPartInfoManager.Get(this._pronotedetail.Product) != null)
        //    {
        //        this._bomParentPartInfo.BomId = this.bomParentPartInfoManager.Get(this._pronotedetail.Product).BomId;
        //        _BomComponentInfoList = this.bomComponentInfoManager.Select(this._bomParentPartInfo);

        //        foreach (Model.BomComponentInfo bomCom in this._BomComponentInfoList)
        //        {
        //            if (bomCom.Product == null) continue;
        //            Model.Pronotedetails pronotedetails = new Book.Model.Pronotedetails();
        //            pronotedetails.PronotedetailsID = Guid.NewGuid().ToString();
        //            pronotedetails.Product = bomCom.Product;
        //            pronotedetails.ProductId = bomCom.Product.ProductId;
        //            pronotedetails.ProductUnit = bomCom.Unit;
        //            pronotedetails.DetailsSum = bomCom.UseQuantity * this._pronotedetail.DetailsSum;
        //            pronotedetails.MPSheaderId = this._pronotedetail.MPSheaderId;
        //            pronotedetails.InvoiceXOId = this._pronotedetail.InvoiceXOId;
        //            pronotedetails.InvoiceXODetailId = this._pronotedetail.InvoiceXODetailId;
        //            pronotedetails.Checkeds = true;
        //            _Pronotedetaillist.Add(pronotedetails);

        //        }
        //    }
        //    //this.pronoteHeader.Details = this.PronotedetailsManager.Select(pronoteHeader);

        //    this.bindingSource2.DataSource = _Pronotedetaillist;
        //}
    }
}