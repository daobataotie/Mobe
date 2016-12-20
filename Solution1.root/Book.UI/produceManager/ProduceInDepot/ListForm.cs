using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using System.Linq;

namespace Book.UI.produceManager.ProduceInDepot
{
    public partial class ListForm : BaseListForm
    {
        int tag = 0;
        int flag = 0;
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.ProduceInDepotDetailManager();
        }

        public ListForm(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            this.flag = 1;
            this.bindingSource1.DataSource = (this.manager as BL.ProduceInDepotDetailManager).SelectList(null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, null, null, null, null, invoiceCusId, null, null, -1);
            this.gridControl1.RefreshDataSource();
            this.gridView1.OptionsBehavior.Editable = true;
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = (this.manager as BL.ProduceInDepotDetailManager).SelectList(null, null, DateTime.Now.Date.AddDays(-3), DateTime.Now, null, null, null, null, null, null, null, null, null, -1);

            double? procedureSum, checkoutsum;
            procedureSum = (this.manager as BL.ProduceInDepotDetailManager).select_SumPronoteHeaderWorkhouseDateRang(DateTime.Now.Date.AddDays(-3), global::Helper.DateTimeParse.EndDate, null, null);
            checkoutsum = (this.manager as BL.ProduceInDepotDetailManager).select_CheckOutSumPronoteHeaderWorkhouseDateRang(DateTime.Now.Date.AddDays(-3), global::Helper.DateTimeParse.EndDate, null, null);
            this.barButtonProduceSum.Caption += procedureSum + "   ";
            this.barButtonCheckSum.Caption += checkoutsum + "   ";
            if (procedureSum != 0)
                this.barButtonCheckPercent.Caption += ((procedureSum - (checkoutsum == null ? 0 : checkoutsum)) / procedureSum * 100).Value.ToString("F1") + "%";

            this.gridView1.OptionsBehavior.Editable = true;
        }

        //更改时间周期
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionProInDepotChooseForm condition = new Query.ConditionProInDepotChooseForm();
            if (condition.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionProInDepotChoose con = condition.Condition as Query.ConditionProInDepotChoose;
                this.bindingSource1.DataSource = (this.manager as BL.ProduceInDepotDetailManager).SelectList(con.StartPronoteHeader, con.EndPronoteHeader, con.StartDate, con.EndDate, con.Product, con.WorkHouse, con.MDepot, con.MDepotPosition, con.Id1, con.Id2, con.Cusxoid, con.Customer1, con.Customer2, con.ProductState);
                this.gridView1.GroupPanelText = "時間從 " + con.StartDate.ToString("yyyy年MM月dd日") + " 到 " + con.EndDate.ToString("yyyy年MM月dd日");
                double? procedureSum, checkoutsum;
                if (this.bindingSource1.DataSource == null)
                {
                    procedureSum = 0;
                    checkoutsum = 0;
                    this.barStaticItem1.Caption = "0";
                }
                else
                {
                    this.barStaticItem1.Caption = (this.bindingSource1.DataSource as IList<Model.ProduceInDepotDetail>).Count.ToString();
                    procedureSum = (this.manager as BL.ProduceInDepotDetailManager).select_SumPronoteHeaderWorkhouseDateRang(con.StartDate, con.EndDate, con.StartPronoteHeader == null ? null : con.StartPronoteHeader, con.WorkHouse == null ? null : con.WorkHouse.WorkHouseId);
                    checkoutsum = (this.manager as BL.ProduceInDepotDetailManager).select_CheckOutSumPronoteHeaderWorkhouseDateRang(con.StartDate, con.EndDate, con.StartPronoteHeader == null ? null : con.StartPronoteHeader, con.WorkHouse == null ? null : con.WorkHouse.WorkHouseId);
                }
                this.barButtonProduceSum.Caption = "總生產數量" + procedureSum + "   ";
                this.barButtonCheckSum.Caption = "總合格數量" + checkoutsum + "   ";
                if (procedureSum != 0)
                    this.barButtonCheckPercent.Caption = "總不良率" + ((procedureSum - (checkoutsum == null ? 0 : checkoutsum)) / procedureSum * 100).Value.ToString("F1") + "%";
                else
                    this.barButtonCheckPercent.Caption = "總不良率:0";

            }
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.Show(this);
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            Model.ProduceInDepot model = new BL.ProduceInDepotManager().Get((args[0] as Model.ProduceInDepotDetail) == null ? null : (args[0] as Model.ProduceInDepotDetail).ProduceInDepotId);
            args[0] = model;
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        //列印数据
        private void barBtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<Model.ProduceInDepotDetail> details = this.bindingSource1.DataSource as IList<Model.ProduceInDepotDetail>;
            if (details != null || details.Count > 0)
            {
                string RowFilter = this.gridView1.RowFilter;
                if (this.flag == 1 && RowFilter.Contains("WorkHousename") && RowFilter.Contains("射出"))
                {
                    details = details.Where(d => d.WorkHousename == "射出").ToList();
                    new ROList(details, 1).ShowPreviewDialog();
                }
                else
                    new ROList(details, 0).ShowPreviewDialog();
            }
        }

        //加工单查看
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            string pronoteHeaderid = (this.bindingSource1.Current as Model.ProduceInDepotDetail).PronoteHeaderId;
            Model.PronoteHeader d = new BL.PronoteHeaderManager().Get(pronoteHeaderid);
            if (d != null)
            {
                PronoteHeader.EditForm f = new Book.UI.produceManager.PronoteHeader.EditForm(d);
                f.ShowDialog();
            }
        }

        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            if (!(this.bindingSource1.Current as Model.ProduceInDepotDetail).PIsClose.HasValue || (this.bindingSource1.Current as Model.ProduceInDepotDetail).PIsClose.Value)
                return;

            if (MessageBox.Show("結 案后,此加工單將不能做領料,退料與入庫動作,是否繼續?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;


            this.gridView1.SetRowCellValue((this.bindingSource1.DataSource as IList<Model.ProduceInDepotDetail>).IndexOf(this.bindingSource1.Current as Model.ProduceInDepotDetail), this.gridColumn17, true);

            new BL.PronoteHeaderManager().UpdateHeaderIsClse((this.bindingSource1.Current as Model.ProduceInDepotDetail).PronoteHeaderId, true);
            MessageBox.Show("結案成功");
        }

    }
}