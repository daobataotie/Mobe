using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.CustomsClearance
{
    public partial class BGhandbookIdSet : Settings.BasicData.BaseEditForm
    {
        BL.BGHandbookIdSetManager manager = new Book.BL.BGHandbookIdSetManager();
        IList<Model.BGHandbookIdSet> _bgHandboolIdSetList = new List<Model.BGHandbookIdSet>();
        public BGhandbookIdSet()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.BGHandbookIdSet.PRO_BGHangBookId, new AA(Properties.Resources.NumsIsNotNull, this.gridControl1));
            this.invalidValueExceptions.Add(Model.BGHandbookIdSet.PRO_BGHangBookId, new AA(Properties.Resources.NunsIsExists, this.gridControl1));
            this.action = "view";

            foreach (var item in manager.SelectBGHandbookId())
            {
                if (!string.IsNullOrEmpty(item))
                    this.repositoryItemComboBox2.Items.Add(item);
            }
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override void AddNew()
        {
            this.action = "insert";
        }

        public override void Refresh()
        {
            this._bgHandboolIdSetList = this.manager.Select();
            this.bindingSource1.DataSource = this._bgHandboolIdSetList;
            if (this.action == "insert")
            {
                Model.BGHandbookIdSet model = new Book.Model.BGHandbookIdSet();
                model.IsUsing = true;
                this._bgHandboolIdSetList.Add(model);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(model);
                this.gridControl1.RefreshDataSource();
            }
            base.Refresh();

            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    break;
            }
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (this.action != "view")
                this.manager.Update(this._bgHandboolIdSetList);
        }

        protected override void Delete()
        {
            if (this.bindingSource1.Current != null)
            {
                if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
                this.manager.Delete((this.bindingSource1.Current as Model.BGHandbookIdSet).BGHangBookId);
                this._bgHandboolIdSetList.Remove(this.bindingSource1.Current as Model.BGHandbookIdSet);
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action != "view")
            {
                if (e.KeyData == Keys.Enter)
                {
                    Model.BGHandbookIdSet model = new Book.Model.BGHandbookIdSet();
                    model.IsUsing = true;
                    this._bgHandboolIdSetList.Add(model);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(model);
                }
                if (e.KeyData == Keys.Delete)
                {
                    this.Delete();
                }
            }
            this.gridControl1.RefreshDataSource();
        }

        private void Material_Load(object sender, EventArgs e)
        {
            this.Visibles();
        }
    }
}