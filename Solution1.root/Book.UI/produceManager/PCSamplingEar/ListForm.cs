using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCSamplingEar
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        int tag = 0;
        public ListForm()
        {
            InitializeComponent();
        }
        BL.PCSamplingEarManager PCSamplingEarManager = new Book.BL.PCSamplingEarManager();
        BL.PCSamplingEarDetailManager detailManager = new Book.BL.PCSamplingEarDetailManager();

        public ListForm(string invoiceCusId)
            : this()
        {
            this.bindingSource1.DataSource = this.detailManager.SelectByCondition(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, invoiceCusId);
            this.gridControl1.RefreshDataSource();
            this.tag = 1;
        }

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        protected override void RefreshData()
        {
            if (tag == 1)
            {
                tag = 0;
                return;
            }
            this.bindingSource1.DataSource = this.detailManager.SelectByCondition(DateTime.Now.AddMonths(-1), DateTime.Now, null, null, null);
            this.gridControl1.RefreshDataSource();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            Model.PCSamplingEar model = this.PCSamplingEarManager.Get((args[0] as Model.PCSamplingEarDetail) == null ? "" : (args[0] as Model.PCSamplingEarDetail).PCSamplingEarId);
            args[0] = model;
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.Show(this);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForm f = new ConditionForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Condition c = f.condition;
                this.bindingSource1.DataSource = this.detailManager.SelectByCondition(c.StartDate, c.EndDate, c.StartPId, c.EndPId, c.InvoiceCusId);
                this.gridControl1.RefreshDataSource();
            }
        }

    }
}