using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.PCDoubleImpactCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        private int _pcFlag = 0;     //表示检测单据类型
        int tag = 0;
        public ListForm(int pcFlag)
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this._pcFlag = pcFlag;
            this.manager = new BL.PCDoubleImpactCheckManager();
        }

        public ListForm(string InvoiceCusId, int flag)
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.PCDoubleImpactCheckManager();
            if (flag == 0)
            {
                this.Text = "CSA冲击/H.M.测试报告";
                this.bindingSource1.DataSource = (this.manager as BL.PCDoubleImpactCheckManager).SelectByDateRage(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, 0, null, null, InvoiceCusId);
            }
            else if (flag == 1)
            {
                this.Text = "BS/EN冲击H.M.测试报告";
                this.bindingSource1.DataSource = (this.manager as BL.PCDoubleImpactCheckManager).SelectByDateRage(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, 1, null, null, InvoiceCusId);
            }
            else if (flag == 2)
            {
                this.Text = "AS/NZS冲击测试报告";
                this.bindingSource1.DataSource = (this.manager as BL.PCDoubleImpactCheckManager).SelectByDateRage(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, 2, null, null, InvoiceCusId);
            }
            this.tag = 1;
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = (this.manager as BL.PCDoubleImpactCheckManager).SelectByDateRage(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, this._pcFlag, null, null, "");
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPronoteHeader condition = f.Condition as Query.ConditionPronoteHeader;
                this.bindingSource1.DataSource = (this.manager as BL.PCDoubleImpactCheckManager).SelectByDateRage(condition.StartDate, condition.EndDate, this._pcFlag, condition.Product, condition.Customer, condition.CusXOId);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            //this.bindingSource1.DataSource = (this.manager as BL.PCDoubleImpactCheckManager).SelectByDateRage(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, this._pcFlag, null, null, "");
            //this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
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
    }
}
