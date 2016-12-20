using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Book.UI.produceManager.PCBoxFootCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        int tag = 0;
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.PCBoxFootCheckManager();
        }

        public ListForm(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            this.bindingSource1.DataSource = (this.manager as BL.PCBoxFootCheckManager).SelectByRage(global::Helper.DateTimeParse.NullDate, DateTime.Now, invoiceCusId, null, null);
            this.gridControl1.RefreshDataSource();
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = (this.manager as BL.PCBoxFootCheckManager).SelectByRage(DateTime.Now.AddDays(-15), DateTime.Now, null, null, null);
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

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPCBoxFootCheck f = new Book.UI.Query.ConditionPCBoxFootCheck();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPCBoxFoot condition = f.Condition;
                this.bindingSource1.DataSource = (this.manager as BL.PCBoxFootCheckManager).SelectByRage(condition.StartDate, condition.EndDate, condition.InvoiceXOId, condition.PronoteHeaderId, condition.Product);
                this.gridControl1.RefreshDataSource();
            }
        }

        //public override void gridView1_DoubleClick(object sender, EventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
        //    if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
        //    {
        //        Form viewform = this.GetViewForm();
        //        if (viewform != null)
        //        {
        //            viewform.ShowDialog(this);
        //        }
        //    }
        //    base.gridView1_DoubleClick(sender, e);
        //}

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.Show(this);
        }
    }
}