using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace Book.UI.produceManager.createProduce
{

    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2010-4-1
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class EditForm : DevExpress.XtraEditors.XtraForm
    {
        // int flag = 0;
        IList<Model.InvoiceXODetail> XOdetail = new List<Model.InvoiceXODetail>();
        //销售订单管理
        BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        //销售订单详细
        BL.InvoiceXODetailManager invoiceXODetailManager = new Book.BL.InvoiceXODetailManager();
        //销售订单详细管理  
        string tagType = string.Empty;

        //窗体初始化
        public EditForm()
        {
            InitializeComponent();
            this.dateEditStart.DateTime = DateTime.Now.Date.AddDays(-7);
            this.dateEditEnd.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            //this.XOdetail = this.invoiceXODetailManager.select_XOnotInMps();
        }

        //采购单 选择销售订单
        public EditForm(string tagType)
            : this()
        {
            this.tagType = tagType;
        }

        //加载指定数据源
        private void EditForm_Load(object sender, EventArgs e)
        {
            this.chkHasInvoiceClose.Checked = true;
            this.chkHasNotMRSOver.Checked = true;
            //switch(this.tagType)
            //   {
            //    case "mps" :
            this.bindingSourceHeader.DataSource = this.invoiceXOManager.SelectByYJRQCustomEmpCusXOId(null, null, this.dateEditStart.DateTime, this.dateEditEnd.DateTime, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, null, null, null, null, null, true, true, false);
            //    break;
            //case "co" :
            //    this.bindingSourceHeader.DataSource = this.invoiceXOManager.SelectDateRangCusXOCustomer(DateTime.Now.Date.AddDays(-15),global::Helper.DateTimeParse.EndDate,null,null);
            //    break;
            // }

        }

        //“确定”
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //foreach (Model.InvoiceXODetail invoiceXODetail in this.XOdetail)
            //{

            //    if (invoiceXODetail.Checkeds == true)
            //    {
            //        produceManager.MPSheader.EditForm._InvoiceXO.Add(invoiceXODetail);               

            //    }
            //    else
            //    { }

            //}
            this.DialogResult = DialogResult.OK;
        }

        //取消
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;
            IList<Model.InvoiceXODetail> xodetail = this.bindingSource1.DataSource as IList<Model.InvoiceXODetail>;
            if (xodetail == null || xodetail.Count == 0) return;
            Model.InvoiceXODetail detail = xodetail[e.ListSourceRowIndex];
            if (detail.Product != null)
            {
                switch (e.Column.Name)
                {
                    case "gridColumnProductId":
                        e.DisplayText = detail.Product.Id;
                        break;
                    case "gridColumnStock":
                        e.DisplayText = detail.Product.StocksQuantity.ToString();
                        break;
                    case "gridColumnGuiGe":
                        e.DisplayText = detail.Product.ProductSpecification;
                        break;
                    //case "gridColumnCustomer":
                    //    e.DisplayText = detail.Invoice.Customer.CustomerShortName;
                    //    break;
                    //case "gridColumnDate":
                    //    e.DisplayText = Convert.ToDateTime(detail.Invoice.InvoieDate).ToString("yyyy/MM/dd"); ;
                    //    break;
                    case "gridColumnUnit":
                        e.DisplayText = detail.InvoiceProductUnit; ;
                        break;
                    //case "gridColumn3":
                    //    e.DisplayText = detail.Invoice.InvoiceYjrq == null ? "" : Convert.ToDateTime(detail.Invoice.InvoiceYjrq).ToString("yyyy/MM/dd");
                    //    break;
                    case "gridColumnCustomProduct":
                        e.DisplayText = detail.Product.CustomerProductName;
                        break;
                    case "gridColumnSupplier":
                        e.DisplayText = detail.Product.Supplier == null ? string.Empty : detail.Product.Supplier.ToString();
                        break;
                    //case "gridColumn2":
                    //    e.DisplayText = detail.Invoice.CustomerInvoiceXOId;
                    //    break;
                }
            }


        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //    Model.InvoiceXODetail invoiceXoDetail = this.bindingSource1.Current as Model.InvoiceXODetail;
            //    if (invoiceXoDetail != null)
            //        produceManager.MPSheader.EditForm._InvoiceXO.Add(invoiceXoDetail);
            this.DialogResult = DialogResult.OK;
        }

        public IList<Model.InvoiceXODetail> SelectList
        {
            get { return (from x in XOdetail where x.Checkeds == true select x).ToList<Model.InvoiceXODetail>(); }
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.bindingSourceHeader.Current == null) return;
            this.XOdetail = this.invoiceXODetailManager.Select(this.bindingSourceHeader.Current as Model.InvoiceXO, true);
            this.bindingSource1.DataSource = this.XOdetail;
            this.gridControl1.RefreshDataSource();
            this.checkEditAll.Checked = false;
        }

        private void gridView2_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (this.bindingSourceHeader.Current == null) return;
            this.XOdetail = this.invoiceXODetailManager.Select(this.bindingSourceHeader.Current as Model.InvoiceXO, false);
            this.bindingSource1.DataSource = this.XOdetail;
            this.gridControl1.RefreshDataSource();
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            //switch (this.tagType)
            //{
            // case "mps":
            this.bindingSourceHeader.DataSource = this.invoiceXOManager.SelectByYJRQCustomEmpCusXOId(this.newChooseCustomer.EditValue as Model.Customer, this.newChooseCustomer.EditValue as Model.Customer, this.dateEditStart.DateTime, this.dateEditEnd.DateTime, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, null, null, this.txtCusXOId.Text, null, null, this.chkHasInvoiceClose.Checked, this.chkHasNotMRSOver.Checked, false);
            //    break;
            //case "co":
            //    this.bindingSourceHeader.DataSource = this.invoiceXOManager.SelectDateRangCusXOCustomer(this.dateEditStart.DateTime, this.dateEditEnd.DateTime, null, null);
            //    break;
            // }
            if (this.bindingSourceHeader.Current == null) return;
            this.XOdetail = this.invoiceXODetailManager.Select(this.bindingSourceHeader.Current as Model.InvoiceXO, false);
            this.bindingSource1.DataSource = this.XOdetail;
            this.gridControl1.RefreshDataSource();
        }

        private void checkEditAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEditAll.Checked == true)
            {
                foreach (var item in this.XOdetail)
                {
                    item.Checkeds = true;
                }
            }
            else
            {
                foreach (var item in this.XOdetail)
                {
                    item.Checkeds = false;
                }
            }
            this.gridControl1.RefreshDataSource();
        }
    }
}