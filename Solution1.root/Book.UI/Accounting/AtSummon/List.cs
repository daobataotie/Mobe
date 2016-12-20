using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;

namespace Book.UI.Accounting.AtSummon
{
    public partial class List : Settings.BasicData.BaseListForm
    {
        public List()
        {
            InitializeComponent();

            this.manager = new Book.BL.AtSummonManager();
        }

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        protected override void RefreshData()
        {
            //默认显示一个月记录
            this.bindingSource1.DataSource = (this.manager as BL.AtSummonManager).SelectByDateRage(DateTime.Now.AddDays(-30), DateTime.Now);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condtion = f.Condition as Query.ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.AtSummonManager).SelectByDateRage(condtion.StartDate, condtion.EndDate);
                this.gridControl1.RefreshDataSource();
            }
            f.Dispose();
            GC.Collect();
        }
    }
}