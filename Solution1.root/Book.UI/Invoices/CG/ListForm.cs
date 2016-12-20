using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;

namespace Book.UI.Invoices.CG
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 單據一覽表(包括單據一些詳細信息的展示)
     * 繼承了基類窗體,風格統一,介面比較美觀
   // 文 件 名：ListForm
   // 编 码 人: 茍波濤                   完成时间:2009-05-10
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
            this.invoiceManager = new BL.InvoiceCGManager();
        }

        int tag = 0;
        public ListForm(string InvoiceCusid)
            : this()
        {
            this.bindingSource1.DataSource = ((BL.InvoiceCGManager)this.invoiceManager).Select(null, null, null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, InvoiceCusid, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate);
            this.tag = 1;
        }
        #endregion

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        #region 重載父類的一些方法

        protected override void LoadInvoices(DateTime datetime1, DateTime datetime2)
        {
            // this.bindingSource1.DataSource = (this.invoiceManager as BL.InvoiceCGManager).Select().OrderByDescending(invoice => invoice.InvoiceId).ThenByDescending(i=>i.InvoiceCOId);
            //this.bindingSource1.DataSource = from ins in (this.invoiceManager as BL.InvoiceCGManager).Select() orderby ins.InvoiceCOId descending, ins.InvoiceId descending select ins;
            
        }

        protected override Form GetViewForm()
        {
            Model.InvoiceCG invoice = this.SelectedItem as Model.InvoiceCG;
            if (invoice != null)
                return new EditForm(invoice.InvoiceId);
            // return new ViewForm(invoice.InvoiceId);

            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceCG>);
        }

        protected override DevExpress.XtraGrid.Views.Grid.GridView MainView
        {
            get
            {
                return this.gridView1;
            }
        }


        #endregion
        protected override void ShowSearchForm()
        {
            Query.ConditionCOChooseForm f = new Query.ConditionCOChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionCO con = f.Condition as Query.ConditionCO;
                this.bindingSource1.DataSource = ((BL.InvoiceCGManager)this.invoiceManager).Select(con.COStartId, con.COEndId, con.SupplierStart, con.SupplierEnd, con.StartDate, con.EndDate, con.ProductStart, con.ProductEnd, con.CusXOId, con.StartJHDate, con.EndJHDate);
                foreach (Model.InvoiceCG model in (this.bindingSource1.DataSource) as IList<Model.InvoiceCG>)
                {
                    if (model.InvoiceCOId == null)
                    {
                        model.DetailCOId = (new BL.InvoiceCGDetailManager()).SelectByInvoiceId(model.InvoiceId);
                    }
                }
            }
            f.Dispose();
            GC.Collect();
        }

    }
}