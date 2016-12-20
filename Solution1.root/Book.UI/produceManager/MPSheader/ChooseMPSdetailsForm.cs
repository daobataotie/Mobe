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
// 编 码 人: 马艳军             完成时间:2010-3-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    public partial class ChooseMPSdetailsForm : DevExpress.XtraEditors.XtraForm
    {
        //需求单

        Model.MRSdetails mRSdetails = new Book.Model.MRSdetails();
        BL.MRSdetailsManager mRSDetailManager = new Book.BL.MRSdetailsManager();
        IList<Model.MRSdetails> list = new List<Model.MRSdetails>();
        private BL.MPSheaderManager mPSheaderManager = new Book.BL.MPSheaderManager();
        private BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        private int FlagIsProcess = 0;
        public ChooseMPSdetailsForm()
        {
            InitializeComponent();
            this.dateMPSStartDate.DateTime = System.DateTime.Now.AddDays(-15).Date;
            this.dateEnddate.DateTime = System.DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }
        public ChooseMPSdetailsForm(int flagIsProcess)
            : this()
        {
            this.FlagIsProcess = flagIsProcess;

        }

        /// <summary>
        /// "确定"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (Model.MRSdetails MRSdetails in list)
            {
                if (MRSdetails.IsChecked == true)
                {


                    if (MRSdetails.MRSdetailssum <= MRSdetails.MRSHasSingleSum)
                    {
                        if (MessageBox.Show("已排單數量大於或等於需求數量,是否繼續排單", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                            return;
                    }
                    Book.UI.produceManager.PronoteHeader.EditForm._mrsdetail = MRSdetails;

                    //produceManager.ProduceOtherCompact.EditForm._MPSdetails.Add(MPSdetails);
                }               
            }
            if (Book.UI.produceManager.PronoteHeader.EditForm._mrsdetail == null || Book.UI.produceManager.PronoteHeader.EditForm._mrsdetail.MRSHeaderId == null)
            {
                MessageBox.Show(Properties.Resources.NoData);
                return;
            }
  


            this.DialogResult = DialogResult.OK;
        }


        /// <summary>
        /// "取消"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();

        }



        private void ChooseMPSdetailsForm_Load(object sender, EventArgs e)
        {
            switch (this.FlagIsProcess)
            {
                //最后参数 null 查全部 非null 查未排完单
                case 0: list = mRSDetailManager.Select(this.dateMPSStartDate.DateTime, this.dateEnddate.DateTime, Convert.ToInt32(global::Helper.ProductType.HomeMade).ToString(), null, null, null, 0);

                    break;
                case 1: list = mRSDetailManager.Select(this.dateMPSStartDate.DateTime, this.dateEnddate.DateTime, null, Convert.ToInt32(global::Helper.ProductType.HomeMadeProcee).ToString(), null, null, 0);

                    break;
                case 2: list = mRSDetailManager.Select(this.dateMPSStartDate.DateTime, this.dateEnddate.DateTime, null, null, Convert.ToInt32(global::Helper.ProductType.Package).ToString(), null, 0);

                    break;
            }
            this.bindingSource1.DataSource = list;

        }


        /// <summary>
        /// 自定义列显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{
        //    if (e.ListSourceRowIndex < 0) return;
        //    IList<Model.MRSdetails> details = this.bindingSource1.DataSource as IList<Model.MRSdetails>;
        //    if (details == null || details.Count < 1) return;
        //    Model.MRSHeader detail = details[e.ListSourceRowIndex].MRSHeader;
        //    Model.Product products = details[e.ListSourceRowIndex].Product;
        //    switch (e.Column.Name)
        //    {
        //        case "gridColumn7":
        //            if (detail == null) return;
        //            e.DisplayText = detail == null ? "" : detail.MPSheaderId;
        //            break;
        //        case "gridColumn1":
        //            if (products == null) return;
        //            e.DisplayText = string.IsNullOrEmpty(products.Id) ? "" : products.Id;
        //            break;
        //        case "gridColumn4":
        //            if (products == null) return;
        //            e.DisplayText = string.IsNullOrEmpty(products.ProductSpecification) ? "" : products.ProductSpecification;
        //            break;
        //        case "gridColumnCusPro":
        //            if (products == null) return;
        //            e.DisplayText = products.CustomerProductName;
        //            break;
        //        case "gridColumnCusXO":
        //            if (!string.IsNullOrEmpty(detail.MPSheaderId))
        //            {
        //                Model.MPSheader mpsHeader = this.mPSheaderManager.Get(detail.MPSheaderId);
        //                if (mpsHeader != null)
        //                {
                           
        //                    if (!string.IsNullOrEmpty(mpsHeader.InvoiceXOId))
        //                    {
        //                        Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mpsHeader.InvoiceXOId);
        //                        e.DisplayText =invoiceXO==null?string.Empty: invoiceXO.CustomerInvoiceXOId;
        //                    }
        //                }
        //            }

        //            break;

        //    }
        //}

        private void repositoryItemCheckEdit1_Click(object sender, EventArgs e)
        {
            Model.MRSdetails mps = bindingSource1.Current as Model.MRSdetails;
            foreach (Model.MRSdetails item in list)
            {
                item.IsChecked = false;
            }
            mps.IsChecked = true;
            this.gridControl1.RefreshDataSource();
        }


        private void ChooseMPSdetailsForm_DoubleClick(object sender, EventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.EditForm._mrsdetail = bindingSource1.Current as Model.MRSdetails;
            if ((bindingSource1.Current as Model.MRSdetails).MRSdetailssum <= (bindingSource1.Current as Model.MRSdetails).MRSHasSingleSum)
            {
                if (MessageBox.Show("已排單數量大於或等於需求數量,是否繼續排單", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }
            this.DialogResult = DialogResult.OK;

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DateTime startTime = global::Helper.DateTimeParse.NullDate;
            DateTime endTime = global::Helper.DateTimeParse.EndDate;
            string CusXOId = this.textEditCusXOId.Text == "" ? null : this.textEditCusXOId.Text;
            if (this.dateMPSStartDate.EditValue != null)
            {
                startTime = this.dateMPSStartDate.DateTime;
            }
            if (this.dateEnddate.EditValue != null)
            {
                endTime = this.dateEnddate.DateTime;
            }
            switch (this.FlagIsProcess)
            {

                case 0: list = mRSDetailManager.Select(this.dateMPSStartDate.DateTime, this.dateEnddate.DateTime, Convert.ToInt32(global::Helper.ProductType.HomeMade).ToString(), null, null, null, 0);

                    break;
                case 1: list = mRSDetailManager.Select(this.dateMPSStartDate.DateTime, this.dateEnddate.DateTime, null, Convert.ToInt32(global::Helper.ProductType.HomeMadeProcee).ToString(), null, null, 0);

                    break;
                case 2: list = mRSDetailManager.Select(this.dateMPSStartDate.DateTime, this.dateEnddate.DateTime, null, null, Convert.ToInt32(global::Helper.ProductType.Package).ToString(), null, 0);

                    break;
            }
            this.bindingSource1.DataSource = list;
        }


    }
}