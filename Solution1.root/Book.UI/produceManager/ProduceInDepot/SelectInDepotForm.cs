using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceInDepot
{
    //*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010   咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究
    // 功能描述：窗体分为上下两个部分（上：生产入库单头信息；下：生产入库单详细信息）
    // 编 码 人：刘永亮                    完成时间:2011-03-14
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/
    public partial class SelectInDepotForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.ProduceInDepotManager produceInDepotManager = new Book.BL.ProduceInDepotManager();
        private BL.ProduceInDepotDetailManager produceInDepotDetialManager = new BL.ProduceInDepotDetailManager();
        public static IList<Model.ProduceInDepotDetail> _produceInDepotDetail;
        public IList<Model.ProduceInDepotDetail> SelectItems = new List<Model.ProduceInDepotDetail>();
        public Model.ProduceInDepot SelectProduceInDepot;

        public SelectInDepotForm()
        {
            InitializeComponent();
            this.DE_Start.DateTime = DateTime.Now.Date.AddDays(-7);
            this.DE_End.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            this.bindingSourceProduceInDepot.DataSource = this.produceInDepotManager.SelectByDateRange(this.DE_Start.DateTime, this.DE_End.DateTime);
            if (this.bindingSourceProduceInDepot.Current != null)
            {
                if ((this.bindingSourceProduceInDepot.Current as Model.ProduceInDepot).Details == null || (this.bindingSourceProduceInDepot.Current as Model.ProduceInDepot).Details.Count == 0)
                {
                    this.SelectItems = this.produceInDepotManager.GetDetails((this.bindingSourceProduceInDepot.Current as Model.ProduceInDepot).ProduceInDepotId).Details;
                    if (this.SelectItems == null || this.SelectItems.Count == 0) return;
                    foreach (Model.ProduceInDepotDetail item in this.SelectItems)
                    {
                        if (item.ProduceQuantity > 0)
                            item.mChecked = true;
                    }
                }
                this.bindingSourceProduceInDepotDetail.DataSource = this.SelectItems;
            }
            //this.bindingSourceProduceInDepotDetail.DataSource = this.produceInDepotDetialManager.Select(null, null, this.DE_Start.DateTime, this.DE_End.DateTime, null, null, null, null);
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceProduceInDepot.Current != null)
            {
                this.SelectItems = (from Model.ProduceInDepotDetail i in this.SelectItems
                                    where i.mChecked == true
                                    select i).ToList<Model.ProduceInDepotDetail>();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;
            IList<Model.ProduceInDepotDetail> details = this.bindingSourceProduceInDepotDetail.DataSource as IList<Model.ProduceInDepotDetail>;
            if (details == null || details.Count == 0) return;
            Model.ProduceInDepotDetail detail = details[e.ListSourceRowIndex];
            if (detail.Product != null)
            {
                switch (e.Column.Name)
                {
                    case "gridColumnProductId":
                        e.DisplayText = detail.Product.Id;
                        break;
                    //case "gridColumnProductName":
                    //    e.DisplayText = detail.Product.StocksQuantity.ToString();
                    //    break;
                    case "gridColumnCusProName":
                        e.DisplayText = detail.Product.CustomerProductName;
                        break;
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (this.DE_Start.EditValue != null && this.DE_End.EditValue != null)
            {
                this.bindingSourceProduceInDepot.DataSource = this.produceInDepotManager.SelectByDateRange(this.DE_Start.DateTime, this.DE_End.DateTime);
                if (this.bindingSourceProduceInDepot.Current != null)
                {
                    if ((this.bindingSourceProduceInDepot.Current as Model.ProduceInDepot).Details == null || (this.bindingSourceProduceInDepot.Current as Model.ProduceInDepot).Details.Count == 0)
                    {
                        this.SelectItems = this.produceInDepotManager.GetDetails((this.bindingSourceProduceInDepot.Current as Model.ProduceInDepot).ProduceInDepotId).Details;
                        if (this.SelectItems == null || this.SelectItems.Count == 0) return;
                        foreach (Model.ProduceInDepotDetail item in this.SelectItems)
                        {
                            if (item.ProduceQuantity > 0)
                                item.mChecked = true;
                        }
                    }
                    this.bindingSourceProduceInDepotDetail.DataSource = this.SelectItems;
                }
                this.gridControl1.RefreshDataSource();
            }
            else
            {
                MessageBox.Show(Properties.Resources.DateNotNull, this.Text, MessageBoxButtons.OK);
            }
        }

        private void btn_ChooseCondition_Click(object sender, EventArgs e)
        {
            //Query.ConditionProInDepotChooseForm f = new Book.UI.Query.ConditionProInDepotChooseForm();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    Query.ConditionProInDepotChoose condition = f.Condition as Query.ConditionProInDepotChoose;
            //    this.bindingSourceProduceInDepotDetail.DataSource = this.produceInDepotDetialManager.Select(condition.StartPronoteHeader, condition.EndPronoteHeader, condition.StartDate, condition.EndDate, condition.Product, condition.WorkHouse, condition.MDepot, condition.MDepotPosition);
            //    this.gridControl2.RefreshDataSource();
            //}
        }

        //更改头信息
        private void bindingSourceProduceInDepot_CurrentChanged(object sender, EventArgs e)
        {
            if (this.bindingSourceProduceInDepot.Current != null)
            {
                if ((this.bindingSourceProduceInDepot.Current as Model.ProduceInDepot).Details == null || (this.bindingSourceProduceInDepot.Current as Model.ProduceInDepot).Details.Count == 0)
                {
                    this.SelectItems = this.produceInDepotManager.GetDetails((this.bindingSourceProduceInDepot.Current as Model.ProduceInDepot).ProduceInDepotId).Details;
                    if (this.SelectItems == null || this.SelectItems.Count == 0) return;
                    foreach (Model.ProduceInDepotDetail item in this.SelectItems)
                    {
                        if (item.ProduceQuantity > 0)
                            item.mChecked = true;
                    }
                }
                this.bindingSourceProduceInDepotDetail.DataSource = this.SelectItems;
            }
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }
        public Model.ProduceInDepot SelectItem
        {
            get { return this.bindingSourceProduceInDepot.Current as Model.ProduceInDepot; }
        }

    }
}