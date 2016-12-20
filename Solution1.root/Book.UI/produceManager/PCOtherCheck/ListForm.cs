using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.PCOtherCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        BL.PCOtherCheckDetailManager pCOtherCheckDetailManager = new Book.BL.PCOtherCheckDetailManager();
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.PCOtherCheckManager();
        }
        int tag = 0;
        public ListForm(string InvoiceCusId)
            : this()
        {
            this.tag = 1;
            //this.bindingSource1.DataSource = (this.manager as BL.PCOtherCheckManager).SelectByDateRage(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, InvoiceCusId);
            this.bindingSource1.DataSource = this.pCOtherCheckDetailManager.SelectByConditon(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, InvoiceCusId);
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            //this.bindingSource1.DataSource = (this.manager as BL.PCOtherCheckManager).SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, null, null, "");
            this.bindingSource1.DataSource = this.pCOtherCheckDetailManager.SelectByConditon(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, null, "");
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPronoteHeader condition = f.Condition as Query.ConditionPronoteHeader;
                this.bindingSource1.DataSource = (this.manager as BL.PCOtherCheckManager).SelectByDateRage(condition.StartDate, condition.EndDate, condition.Product, condition.Customer, condition.CusXOId);

                this.bindingSource1.DataSource = this.pCOtherCheckDetailManager.SelectByConditon(condition.StartDate, condition.EndDate, condition.Product, condition.CusXOId);
                this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
                this.gridControl1.RefreshDataSource();
            }
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
            //Type type = typeof(EditForm);
            //return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            Type type = typeof(EditForm);
            string id = (args[0] as Model.PCOtherCheckDetail).PCOtherCheckId;
            args[0] = id;
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            EditForm f = new EditForm();
            Model.PCOtherCheckDetail model = this.bindingSource1.Current as Model.PCOtherCheckDetail;
            if (model != null)
                f = new EditForm(model.PCOtherCheckId);
            f.Show(this);
        }
    }
}
