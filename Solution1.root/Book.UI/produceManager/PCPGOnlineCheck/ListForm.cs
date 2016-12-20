using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        int tag = 0;
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.PCPGOnlineCheckManager();
        }
        public ListForm(string InvoiceCusId)
            : this()
        {
            this.tag = 1;
            IList<Model.PCPGOnlineCheck> list = (this.manager as BL.PCPGOnlineCheckManager).SelectByDateRage(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, InvoiceCusId, null, null);

            this.bindingSource1.DataSource = list;
            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            IList<Model.PCPGOnlineCheck> mPCPGlist = (this.manager as BL.PCPGOnlineCheckManager).SelectByDateRage(DateTime.Now.AddDays(-7), global::Helper.DateTimeParse.EndDate, null, null, "", "", "");
            //BL.PCPGOnlineCheckDetailManager mPCPGDetailM = new Book.BL.PCPGOnlineCheckDetailManager();
            //foreach (Model.PCPGOnlineCheck item in mPCPGlist)
            //{
            //    item.DescTime = mPCPGDetailM.GetTimerListString(item.PCPGOnlineCheckId);
            //}
            this.bindingSource1.DataSource = mPCPGlist;
            this.gridView1.GroupPanelText = "默認顯示七天内的記錄";
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPronoteHeader condition = f.Condition as Query.ConditionPronoteHeader;
                this.bindingSource1.DataSource = (this.manager as BL.PCPGOnlineCheckManager).SelectByDateRage(condition.StartDate, condition.EndDate, condition.Product, condition.Customer, condition.CusXOId, condition.PronoteHeaderIdStart, condition.PronoteHeaderIdEnd);
                this.gridControl1.RefreshDataSource();
            }
            f.Dispose();
            GC.Collect();
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
            Form f = GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.Show(this);
        }
    }
}
