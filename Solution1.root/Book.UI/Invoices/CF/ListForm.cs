using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.CF
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 拆分單據一覽表(包括入庫出庫詳細信息的展示)
     * 繼承了基類窗體,風格統一,介面比較美觀
   // 文 件 名：ListForm
   // 编 码 人: 茍波濤                   完成时间:2009-05-09
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ListForm : BaseListForm
    {

        #region 構造函數
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceZZManager();
        }
        #endregion

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        #region 重載父類的方法
        protected override Form GetViewForm()
        {
            Model.InvoiceZZ invoice = this.SelectedItem as Model.InvoiceZZ;
            if (invoice != null)
                return new ViewForm(invoice.InvoiceId);

            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
            //return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceZZ>);
        }

        protected override DevExpress.XtraGrid.Views.Grid.GridView MainView
        {
            get
            {
                return this.gridView1;
            }
        }
        #endregion

    }
}