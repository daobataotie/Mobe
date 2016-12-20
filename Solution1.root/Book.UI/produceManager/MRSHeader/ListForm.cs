using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.MRSHeader
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2010-3-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        private BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        private BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        private BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        public IList<Model.MRSHeader> MRShList;

        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.MRSHeaderManager();
            this.gridView1.OptionsBehavior.Editable = true;
        }

        protected override void RefreshData()
        {
            // if (flag == 0)
            //{
            MRShList = (this.manager as BL.MRSHeaderManager).SelectbyCondition(null, null, null, null, DateTime.Now.Date.AddDays(-3), DateTime.Now.Date.AddDays(1).AddSeconds(-1), -1, null, null, null, null);
            // Model.InvoiceXO xo = null;
            //Model.MPSheader mpsH = null;
            //if (MRShList == null || MRShList.Count == 0)
            //    return;
            //foreach (Model.MRSHeader item in MRShList)
            //{
            //    //mpsH = mPSheaderManager.Get(item.MPSheaderId);
            //    //if (mpsH != null)
            //    //{
            //        //xo = this.invoiceXOManager.SelectMrsIsClose(item,0);
            //        //item.CustomerInvoiceXOId = xo == null ? "" : xo.CustomerInvoiceXOId;
            //        //item.IsCloseed = this.mRSHeaderManager.SelectIsCloseed(item.MRSHeaderId);
            //    //}
            //    item.IsChecked = true;
            //}
            this.bindingSource1.DataSource = MRShList;
            // }
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public Model.MRSHeader SelectItem
        {
            get { return this.bindingSource1.Current as Model.MRSHeader; }
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        //private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{
        //    if (e.ListSourceRowIndex < 0) return;
        //    IList<Model.MRSHeader> details = this.bindingSource1.DataSource as IList<Model.MRSHeader>;
        //    if (details == null || details.Count < 1) return;
        //    Model.MRSHeader detail=details[e.ListSourceRowIndex];
        //    switch (e.Column.Name)
        //    {
        //        case "gridColumnIsClose":
        //            detail.IsCloseed = this.mRSHeaderManager.SelectIsCloseed(detail.MRSHeaderId);
        //            this.gridControl1.RefreshDataSource();
        //            break;
        //    }
        //}
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionMRSChooseForm f = new Query.ConditionMRSChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionMRS condition = f.Condition as Query.ConditionMRS;
                MRShList = (this.manager as BL.MRSHeaderManager).SelectbyCondition(condition.MrsStart, condition.MrsEnd, condition.CustomerStart, condition.CustomerEnd, condition.StartDate, condition.EndDate, condition.SourceType, condition.Id1, condition.Id2, condition.Cusxoid, condition.Product);
                //Model.InvoiceXO xo = null;
                //Model.MPSheader mpsH = null;
                //if (MRShList == null || MRShList.Count == 0)
                //    return;
                //foreach (Model.MRSHeader item in MRShList)
                //{
                //    mpsH = mPSheaderManager.Get(item.MPSheaderId);
                //    if (mpsH != null)
                //    {
                //        xo = this.invoiceXOManager.Get(mpsH.InvoiceXOId);
                //        item.CustomerInvoiceXOId = xo == null ? "" : xo.CustomerInvoiceXOId;
                //        item.IsCloseed = this.mRSHeaderManager.SelectIsCloseed(item.MRSHeaderId);
                //        item.YjrqDate = xo == null ? null : xo.InvoiceYjrq;
                //        item.CustomerId = xo == null ? "" : xo.xocustomer.CustomerShortName;
                //        item.PiHao = xo == null ? "" : xo.CustomerLotNumber;
                //    }
                //    item.IsChecked = true;
                //}
                this.bindingSource1.DataSource = MRShList;
                this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
            }
        }

        private void barBtnMoreParamSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow()) return;
            if (this.MRShList == null || MRShList.Count == 0) return;
            IList<Model.MRSHeader> list = (from Model.MRSHeader i in MRShList
                                           where i.IsChecked == true
                                           select i).ToList<Model.MRSHeader>();
            if (list == null || list.Count == 0)
                return;
            RO1Details f = new RO1Details(list);
            f.ShowPreviewDialog();
        }
    }
}