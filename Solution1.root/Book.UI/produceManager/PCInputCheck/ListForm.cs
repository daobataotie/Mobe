using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.PCInputCheck
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {

        BL.PCInputCheckManager pCInputCheckManager = new Book.BL.PCInputCheckManager();
        int tag = 0;
        public ListForm()
        {
            InitializeComponent();
            this.gridView1.OptionsBehavior.Editable = true;
        }

        public ListForm(int i)
            : this()
        {
            tag = i;
        }

        public ListForm(string invoiceCusId)
            : this()
        {
            this.bindingSource1.DataSource = this.pCInputCheckManager.SelectByInvoiceCusId(invoiceCusId);
            this.gridControl1.RefreshDataSource();
            this.tag = 1;
        }

        new public Model.PCInputCheck SelectItem { get; set; }

        public IList<Model.PCInputCheck> keys = new List<Model.PCInputCheck>();

        protected override void RefreshData()
        {
            if (tag == 1)
            {
                tag = 0;
                return;
            }
            this.bindingSource1.DataSource = this.pCInputCheckManager.SelectByCondition(DateTime.Now.AddMonths(-1), DateTime.Now, null, null, null, null, false);
            this.gridView1.GroupPanelText = "默認顯示一个月内的記錄";
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPCInputCheckForm f = new Book.UI.Query.ConditionPCInputCheckForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPCInputCheck model = f.condition;
                this.bindingSource1.DataSource = this.pCInputCheckManager.SelectByCondition(model.StartDate, model.EndDate, model.ProductId, model.TestProductId, model.SupplierId, model.LotNumber, model.IsClosed);
                this.gridControl1.RefreshDataSource();
                this.barStaticItem1.Caption = string.Format("{0}項", this.bindingSource1.Count);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (tag == 2)
            {
                if ((this.bindingSource1.DataSource as List<Model.PCInputCheck>) != null)
                {
                    this.keys = (this.bindingSource1.DataSource as List<Model.PCInputCheck>).Where(d => d.IsCheck == true).ToList();
                }
            }
            else
                this.SelectItem = this.bindingSource1.Current as Model.PCInputCheck;
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Model.PCInputCheck model = new Book.Model.PCInputCheck();
            model = this.bindingSource1.Current as Model.PCInputCheck;
            EditForm f = new EditForm(model);
            f.ShowDialog();
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            Model.PCInputCheck model = this.bindingSource1.Current as Model.PCInputCheck;
            if (model == null) return;
            if (model.IsClosed == null || model.IsClosed == false)
            {
                if (MessageBox.Show("是否強制結案?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }
            if (model.IsClosed != null && model.IsClosed.Value)
            {
                if (MessageBox.Show("是否取消結案?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }
            model.IsClosed = (model.IsClosed == null || model.IsClosed == false) ? true : false;
            try
            {
                BL.V.BeginTransaction();
                this.pCInputCheckManager.UpdateIsClosed(model);
                BL.V.CommitTransaction();
                MessageBox.Show("操作成功", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                model.IsClosed = (model.IsClosed == null || model.IsClosed == false) ? true : false;
                throw ex;
            }
            this.bindingSource1.Position = (this.bindingSource1.Position - 1) < 0 ? 0 : (this.bindingSource1.Position - 1);
            this.bindingSource1.Position = (this.bindingSource1.Position + 1) > this.bindingSource1.Count - 1 ? this.bindingSource1.Count - 1 : (this.bindingSource1.Position + 1);

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column.Name == "gridColumn11")
            //{
            //    Model.PCInputCheck model = this.bindingSource1.Current as Model.PCInputCheck;
            //    if (model != null)
            //    {
            //        e.DisplayText = (model.IsClosed == null || model.IsClosed == false) ? "結案" : "取消結案";
            //    }
            //}
        }
    }
}