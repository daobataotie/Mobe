using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCEarPressCheck.cs
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.PCEarPressCheckManager();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionAChooseForm f = new Book.UI.Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA ConditionA = f.Condition as Query.ConditionA;
                bindingSource1.DataSource = (this.manager as BL.PCEarPressCheckManager).SelectByDateRage(ConditionA.StartDate, ConditionA.EndDate);
                gridControl1.RefreshDataSource();
            }

        }

        protected override void RefreshData()
        {
            bindingSource1.DataSource = (this.manager as BL.PCEarPressCheckManager).SelectByDateRage(DateTime.Now.AddDays(-15), DateTime.Now.AddDays(1).AddSeconds(-1));
            this.gridView1.GroupPanelText = "默認顯示半個月內的記錄";
        }


        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);

            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }


    }
}