using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.MPSheader
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2010-4-2
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        private BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        /// <summary>
        /// 无参构造
        /// </summary>
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.MPSheaderManager();
        }

        protected override void RefreshData()
        {

            this.bindingSource1.DataSource = (this.manager as BL.MPSheaderManager).Select(DateTime.Now.AddDays(-30).Date, DateTime.Now);
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }
        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        public Model.MPSheader SelectItem
        {
            get { return this.bindingSource1.Current as Model.MPSheader; }
        }

        private void barButtonItemQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condition = f.Condition as Query.ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.MPSheaderManager).Select(condition.StartDate, condition.EndDate);
            }
            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        //private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{

        //        if (e.ListSourceRowIndex < 0) return;
        //    IList<Model.MPSheader> details = this.bindingSource1.DataSource as IList<Model.MPSheader>;
        //    if (details == null || details.Count < 1) return;


        //    switch (e.Column.Name)
        //    {
        //        case "gridColumncustomerXO":
        //            if (invoiceXOManager.Get(details[e.ListSourceRowIndex].InvoiceXOId) != null)
        //                e.DisplayText = invoiceXOManager.Get(details[e.ListSourceRowIndex].InvoiceXOId).CustomerInvoiceXOId;
        //            break;
        //        case "gridColumncustomer":
        //            if (invoiceXOManager.Get(details[e.ListSourceRowIndex].InvoiceXOId) != null)
        //                e.DisplayText = invoiceXOManager.Get(details[e.ListSourceRowIndex].InvoiceXOId).xocustomer.CustomerShortName;
        //            break;

        //    }

        //}
    }
}