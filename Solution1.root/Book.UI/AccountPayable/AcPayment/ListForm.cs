using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;

namespace Book.UI.AccountPayable.AcPayment
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.AcPaymentManager();
        }
        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new AccountPayable.AcPayment.EditForm();
        }
        public Model.AcPayment SelectItem
        {
            get { return this.bindingSource1.Current as Model.AcPayment; }
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

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.AcPaymentManager).SelectByDateRange(System.DateTime.Now.AddMonths(-3), System.DateTime.Now);
        }

        private void ibtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionAChooseForm form = new ConditionAChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ConditionA cond = form.Condition as ConditionA;
                IList<Model.AcPayment> acPayment = (this.manager as BL.AcPaymentManager).SelectByDateRange(cond.StartDate, cond.EndDate);
                if (acPayment != null)
                {
                    this.bindingSource1.DataSource = acPayment;
                }
                this.gridView1.GroupPanelText = "查詢日期週期是:" + System.DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + " 到 " + System.DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}