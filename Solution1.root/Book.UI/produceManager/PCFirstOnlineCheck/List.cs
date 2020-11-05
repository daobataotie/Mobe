using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCFirstOnlineCheck
{
    public partial class List : Settings.BasicData.BaseListForm
    {
        int tag = 0;

        BL.PCFirstOnlineCheckDetailManager _detailManager = new Book.BL.PCFirstOnlineCheckDetailManager();
        public List()
        {
            if (!Editform.isDelete)
                this.CancleDelete();
            InitializeComponent();
        }

        public List(string InvoiceCusId)
            : this()
        {
            this.tag = 1;
            this.bindingSource1.DataSource = _detailManager.SelectByCondition(global::Helper.DateTimeParse.NullDate, DateTime.Now, InvoiceCusId);
            this.gridControl1.RefreshDataSource();
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = _detailManager.SelectByCondition(DateTime.Now.AddDays(-15), DateTime.Now, null);
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new Editform();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(Editform);
            Model.PCFirstOnlineCheckDetail detail = args[0] as Model.PCFirstOnlineCheckDetail;
            if (detail != null)
            {
                args[0] = detail.PCFirstOnlineCheck;

                return (Editform)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            }
            return new Editform();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForm f = new ConditionForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Condition condition = f.condition as Condition;
                this.bindingSource1.DataSource = _detailManager.SelectByCondition(condition.StartDate, condition.EndDate, condition.InvoiceCusId);
            }
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.Show(this);
        }
    }
}