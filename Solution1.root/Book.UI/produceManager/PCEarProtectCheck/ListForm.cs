using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.PCEarProtectCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        bool tag;
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.PCEarProtectCheckManager();
        }

        public ListForm(bool IsReport)
            : this()
        {
            this.tag = IsReport;
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.PCEarProtectCheckManager).SelectByDateRage(DateTime.Now.Date.AddDays(-15), DateTime.Now.Date.AddDays(1).AddSeconds(-1), this.tag);
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA conditionA = f.Condition as Query.ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.PCEarProtectCheckManager).SelectByDateRage(conditionA.StartDate, conditionA.EndDate, this.tag);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}
