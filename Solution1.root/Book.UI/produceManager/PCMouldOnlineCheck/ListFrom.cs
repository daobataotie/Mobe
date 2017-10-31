using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCMouldOnlineCheck
{
    public partial class ListFrom : Settings.BasicData.BaseListForm
    {
        int tag = 0;
        public Model.PCMouldOnlineCheckDetail SelectItem { get { return this.bindingSource1.Current as Model.PCMouldOnlineCheckDetail; } }

        public ListFrom()
        {
            InitializeComponent();
        }

        public ListFrom(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            this.bindingSource1.DataSource = new BL.PCMouldOnlineCheckDetailManager().SelectByInvoiceCusId(invoiceCusId);
        }


        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = new BL.PCMouldOnlineCheckDetailManager().SelectByCondition(DateTime.Now.AddMonths(-1), DateTime.Now,DateTime.Now.AddMonths(-1), DateTime.Now, null, null);
            this.gridView1.GroupPanelText = "默認顯示一个月内的記錄";
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPCMouldCheckForm f = new Book.UI.Query.ConditionPCMouldCheckForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPCMouldCheck model = f.condition;
                this.bindingSource1.DataSource = new BL.PCMouldOnlineCheckDetailManager().SelectByCondition(model.OnlineDateStart, model.OnlineDateEnd,model.CheckDateStart,model.CheckDateEnd,  model.ProductId, model.InvoiceCusId);
                this.gridControl1.RefreshDataSource();
            }
            this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
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
            Model.PCMouldOnlineCheck model = new BL.PCMouldOnlineCheckManager().Get((args[0] as Model.PCMouldOnlineCheckDetail) == null ? null : (args[0] as Model.PCMouldOnlineCheckDetail).PCMouldOnlineCheckId);
            args[0] = model;
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Model.PCMouldOnlineCheck model = new Book.Model.PCMouldOnlineCheck();
            if ((this.bindingSource1.Current as Model.PCMouldOnlineCheckDetail) != null)
                model = new BL.PCMouldOnlineCheckManager().Get((this.bindingSource1.Current as Model.PCMouldOnlineCheckDetail).PCMouldOnlineCheckId);
            EditForm f = new EditForm(model);
            f.ShowDialog();
        }
    }
}