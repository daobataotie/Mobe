using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Customs;
using Book.UI.Settings.BasicData.Supplier;

namespace Book.UI.produceManager.ProduceOtherCompact
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010 咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2010-3-20
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ChooseOutContract : DevExpress.XtraEditors.XtraForm
    {
        BL.ProduceOtherCompactManager produceOtherCompactManager = new Book.BL.ProduceOtherCompactManager();
        Model.ProduceOtherCompact produceOtherCompact = new Book.Model.ProduceOtherCompact();
        BL.ProduceOtherCompactDetailManager ProduceOtherCompactDetailManager = new Book.BL.ProduceOtherCompactDetailManager();
        IList<Model.ProduceOtherCompactDetail> details = new List<Model.ProduceOtherCompactDetail>();
        Model.ProduceOtherCompactDetail produceOtherCompactDetail = new Book.Model.ProduceOtherCompactDetail();
        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.ProduceOtherCompactMaterialManager produceOtherCompactMaterialManager = new BL.ProduceOtherCompactMaterialManager();
        // public static IList<Model.ProduceOtherCompactDetail> _OtherCompactDetailList;
        private IList<Model.ProduceOtherCompactDetail> detailList;
        private IList<Model.ProduceOtherCompactMaterial> detailMaterialList;
        private IList<Model.ProduceOtherCompactDetail> _key = new List<Model.ProduceOtherCompactDetail>();
        public IList<Model.ProduceOtherCompactDetail> key
        {
            get { return _key; }
            set { _key = value; }
        }
        private int flag = 0;
        private IList<Model.ProduceOtherCompactMaterial> _keyMaterial = new List<Model.ProduceOtherCompactMaterial>();
        public IList<Model.ProduceOtherCompactMaterial> keyMaterial
        {
            get { return _keyMaterial; }
            set { _keyMaterial = value; }
        }

        public ChooseOutContract()
        {
            InitializeComponent();
            this.dateEditStart.DateTime = DateTime.Now.Date.AddDays(-15);
            this.dateEditEnd.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            this.nccCustomer.Choose = new ChooseCustoms();
            this.nccSupplier.Choose = new ChooseSupplier();

            //this.details = this.produceOtherCompactManager.Select();
            //_OtherCompactDetailList = new List<Model.ProduceOtherCompactDetail>();
        }

        /// <summary>
        /// 1选择料
        /// </summary>
        /// <param name="flag"></param>
        public ChooseOutContract(int flag)
            : this()
        {
            this.flag = flag;
            //this.details = this.produceOtherCompactManager.Select();
            //_OtherCompactDetailList = new List<Model.ProduceOtherCompactDetail>();
        }

        private object selectItem;

        public object SelectItem
        {
            get
            {
                return this.bindingSourceHeader.Current;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //foreach (Model.ProduceOtherCompactDetail produceOtherCompactDetail in this.details)
            //{
            //    if (produceOtherCompactDetail.Checkeds == true)
            //    {
            //        produceManager.ProduceOtherMaterial.EditForm._produceOtherCompactDetail.Add(produceOtherCompactDetail);
            //        produceManager.ProduceOtherExitMaterial.EditForm._produceOtherCompactDetail.Add(produceOtherCompactDetail);
            //    }
            //}

            this.DialogResult = DialogResult.OK;
        }

        //取消
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChooseOutContract_Load(object sender, EventArgs e)
        {
            if (this.flag == 1)
            {
                this.gridView2.OptionsBehavior.Editable = true;
                this.gridColumnCheck.Visible = true;
                this.gridColumnChecked.Visible = false;
                this.gridColumnChecked.OptionsColumn.AllowEdit = true;
                this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
            }
            else
            {
                this.gridColumnCheck.Visible = false;
                this.gridColumnChecked.Visible = true;
            }
            this.bindingSourceHeader.DataSource = this.produceOtherCompactManager.GetByDate(this.dateEditStart.DateTime, this.dateEditEnd.DateTime, null, null, null, null, null);
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherCompactMaterial> details = this.bindingSource2.DataSource as IList<Model.ProduceOtherCompactMaterial>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumn8":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
                case "gridColumn9":
                    if (detail == null) return;
                    e.DisplayText = detail.ProductSpecification;
                    break;
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {


            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherCompactDetail> details = this.bindingSourceDetail.DataSource as IList<Model.ProduceOtherCompactDetail>;

            Model.ProduceOtherCompactDetail detailss = details[e.ListSourceRowIndex];
            Model.ProduceOtherCompact detail = details[e.ListSourceRowIndex].ProduceOtherCompact;
            switch (e.Column.Name)
            {
                //case "gridColumn2":
                //    if (detail == null) return;
                //    e.DisplayText = detail.ProduceOtherCompactDate.Value.ToString("yyyy-MM-dd");
                //    break;
                case "gridColumn3":
                    if (detail == null) return;
                    e.DisplayText = detail.Supplier == null ? "" : detail.Supplier.SupplierShortName;
                    break;
                case "gridColumnguige":
                    if (detailss == null) return;
                    e.DisplayText = detailss == null ? "" : detailss.ProductSpecification;
                    break;
                case "gridColumnProductId":
                    if (detailss == null) return;
                    e.DisplayText = detailss.Product == null ? "" : detailss.Product.Id;
                    break;
                case "gridColumnstock":
                    if (detailss == null || detailss.Product.StocksQuantity == null) return;
                    e.DisplayText = detailss.Product.StocksQuantity.ToString();
                    break;

            }
        }

        //查询
        private void simpleButtonselect_Click(object sender, EventArgs e)
        {
            string customerid = this.nccCustomer.EditValue == null ? null : (this.nccCustomer.EditValue as Model.Customer).CustomerId;
            string supplierid = this.nccSupplier.EditValue == null ? null : (this.nccSupplier.EditValue as Model.Supplier).SupplierId;
            string cusxoid = this.txtInvoiceXOId.EditValue == null ? null : this.txtInvoiceXOId.Text;
            this.bindingSourceHeader.DataSource = this.produceOtherCompactManager.GetByDate(this.dateEditStart.DateTime, this.dateEditEnd.DateTime, null, cusxoid, customerid, supplierid, null);
            bandDetail();
        }


        private void gridViewHeader_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bandDetail();

        }

        private void bandDetail()
        {
            if (this.bindingSourceHeader.Current == null)
            {
                this.bindingSourceDetail.Clear();
                this.bindingSource2.Clear();
                return;
            }
            detailList = this.ProduceOtherCompactDetailManager.Select(this.bindingSourceHeader.Current as Model.ProduceOtherCompact);
            if (detailList != null)
            {
                int flag = 0;
                for (int i = 0; i < key.Count; i++)
                {
                    foreach (Model.ProduceOtherCompactDetail detail in detailList)
                    {
                        if (key[i].OtherCompactDetailId == detail.OtherCompactDetailId)
                        {
                            detail.Checked = true;
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 1)
                        break;
                }
            }
            this.bindingSourceDetail.DataSource = detailList;
            //料

            detailMaterialList = this.produceOtherCompactMaterialManager.Select(this.bindingSourceHeader.Current as Model.ProduceOtherCompact);
            if (detailMaterialList != null)
            {
                int flag = 0;
                for (int i = 0; i < keyMaterial.Count; i++)
                {
                    foreach (Model.ProduceOtherCompactMaterial detail in detailMaterialList)
                    {
                        if (keyMaterial[i].ProduceOtherCompactMaterialId == detail.ProduceOtherCompactMaterialId)
                        {
                            detail.Checked = true;
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 1)
                        break;
                }
            }
            this.bindingSource2.DataSource = detailMaterialList;

            //  this.checkEditALL.Checked = false;

        }

        private void gridViewHeader_ColumnFilterChanged(object sender, EventArgs e)
        {
            bandDetail();
        }

        private void gridViewDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumnChecked")
            {
                Model.ProduceOtherCompactDetail detail = this.gridViewDetail.GetRow(e.RowHandle) as Model.ProduceOtherCompactDetail;

                if ((bool)e.Value)
                {
                    key.Add(detail);
                    //  MrsDetails.Add(this.mRSdetailsManager.Get(detail.MRSdetailsId));
                }
                if (!(bool)e.Value)
                {
                    for (int i = 0; i < key.Count; i++)
                    {
                        if (key[i].OtherCompactDetailId == detail.OtherCompactDetailId)
                        {
                            key.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumnCheck")
            {
                Model.ProduceOtherCompactMaterial detail = this.gridView2.GetRow(e.RowHandle) as Model.ProduceOtherCompactMaterial;
                if ((bool)e.Value)
                {
                    keyMaterial.Add(detail);
                }
                if (!(bool)e.Value)
                {
                    for (int i = 0; i < keyMaterial.Count; i++)
                    {
                        if (keyMaterial[i].ProduceOtherCompactMaterialId == detail.ProduceOtherCompactMaterialId)
                        {
                            keyMaterial.RemoveAt(i);
                            break;
                        }
                    }
                    //  MrsDetails.Remove(this.mRSdetailsManager.Get(detail.MRSdetailsId));
                }
            }
        }
    }
}