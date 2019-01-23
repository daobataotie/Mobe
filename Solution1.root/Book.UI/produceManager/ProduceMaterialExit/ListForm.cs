using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceMaterialExit
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        //BL.PronoteHeaderManager PronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        BL.ProduceMaterialExitDetailManager DetailManager = new Book.BL.ProduceMaterialExitDetailManager();

        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.ProduceMaterialExitManager();
        }

        protected override void RefreshData()
        {
            //this.bindingSource1.DataSource = (this.manager as BL.ProduceMaterialExitManager).SelectForListForm(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(1).AddSeconds(-1), null, null, null, null, null, null, null, null, null);

            //foreach (Model.ProduceMaterialExit pme in (this.bindingSource1.DataSource as IList<Model.ProduceMaterialExit>))
            //{
            //    pme.MPronoteHeader = this.PronoteHeaderManager.Get(pme.PronoteHeaderID);
            //}

            this.bindingSource1.DataSource = DetailManager.SelectForListForm(DateTime.Now.AddDays(-15), DateTime.Now.AddDays(1).AddSeconds(-1), null, null, null, null, null, null, null, null, null);
        }

        private void barBtnChangeSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForList f = new ConditionForList();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                ConditionForListCls con = f.Condition as ConditionForListCls;

                //this.bindingSource1.DataSource = (this.manager as BL.ProduceMaterialExitManager).SelectForListForm(con.StartDate, con.EndDate, con.StartPMEid, con.EndPMEid, con.StartPronoteHeaderId, con.EndPronoteHeaderId, con.StartProduct, con.EndProduct, con.WorkhouseId, con.InvocieXOCusId, con.HandBookId);

                //foreach (Model.ProduceMaterialExit pme in (this.bindingSource1.DataSource as IList<Model.ProduceMaterialExit>))
                //{
                //    pme.MPronoteHeader = this.PronoteHeaderManager.Get(pme.PronoteHeaderID);
                //}
                this.bindingSource1.DataSource = DetailManager.SelectForListForm(con.StartDate, con.EndDate, con.StartPMEid, con.EndPMEid, con.StartPronoteHeaderId, con.EndPronoteHeaderId, con.StartProduct, con.EndProduct, con.WorkhouseId, con.InvocieXOCusId, con.HandBookId);
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.ProduceMaterialExit> list = this.bindingSource1.DataSource as IList<Model.ProduceMaterialExit>;
            //if (list == null || list.Count < 0) return;
            //Model.PronoteHeader ph = list[e.RowHandle].MPronoteHeader;
            //switch (e.Column.Name)
            //{
            //    case "colProduct":
            //        e.DisplayText = ph == null ? "" : ph.Product.ToString();
            //        break;
            //    case "colCustomerProduct":
            //        e.DisplayText = ph == null ? "" : ph.Product.CustomerProductName;
            //        break;
            //}
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Model.ProduceMaterialExitDetail detail = args[0] as Model.ProduceMaterialExitDetail;
            if (detail != null)
            {
                args[0] = detail.ProduceMaterialExit;
            }

            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.Show(this);
        }
    }
}