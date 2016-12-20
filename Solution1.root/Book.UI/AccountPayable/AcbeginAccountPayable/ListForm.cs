using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using Book.UI.Query;

namespace Book.UI.AccountPayable.AcbeginAccountPayable
{
    public partial class ListForm : BaseListForm
    {
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.AcbeginAccountPayableManager();
            this.bindingSource1.DataSource = (this.manager as BL.AcbeginAccountPayableManager).SelectByDateRange(System.DateTime.Now.AddMonths(-3), System.DateTime.Now);
            this.gridView1.GroupPanelText =  System.DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + " 到 " + System.DateTime.Now.ToString("yyyy-MM-dd");
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

     

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionAChooseForm form = new ConditionAChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ConditionA cond = form.Condition as ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.AcbeginAccountPayableManager).SelectByDateRange(cond.StartDate, cond.EndDate);
                this.gridView1.GroupPanelText = "查詢日期週期是:" + System.DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + " 到 " + System.DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

    }
}