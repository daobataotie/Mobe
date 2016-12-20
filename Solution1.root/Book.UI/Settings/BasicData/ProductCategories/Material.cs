using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.ProductCategories
{
    public partial class Material : BaseEditForm
    {
        BL.MaterialManager materialManager = new Book.BL.MaterialManager();
        IList<Model.Material> _materialList = new List<Model.Material>();

        public Material()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.Material.PRO_Id, new AA(Properties.Resources.RequireIdName, this.gridControl1 as Control));
            this.invalidValueExceptions.Add(Model.Material.PRO_Id, new AA(Properties.Resources.EntityExists, this.gridControl1 as Control));
            //this.invalidValueExceptions.Add(Model.Material.PRO_MaterialCategoryName, new AA(Properties.Resources.EntityName, this.gridControl1 as Control));
            this.action = "view";
        }

        protected override bool HasRows()
        {
            return this.materialManager.HasRows();
        }

        protected override void AddNew()
        {
            this.action = "insert";
        }

        public override void Refresh()
        {
            this._materialList = this.materialManager.Select();
            this.bindingSource1.DataSource = this._materialList;
            if (this.action == "insert")
            {
                Model.Material mt = new Book.Model.Material();
                mt.MaterialId = Guid.NewGuid().ToString();
                this._materialList.Add(mt);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(mt);
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
                this.materialManager.Update(this._materialList);
        }

        protected override void Delete()
        {
            if (this.bindingSource1.Current != null)
            {
                if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
                this.materialManager.Delete((this.bindingSource1.Current as Model.Material).MaterialId);
                this._materialList.Remove(this.bindingSource1.Current as Model.Material);
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action != "view")
            {
                if (e.KeyData == Keys.Enter)
                {
                    Model.Material material = new Book.Model.Material();
                    material.MaterialId = Guid.NewGuid().ToString();
                    this._materialList.Add(material);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(material);
                }
                if (e.KeyData == Keys.Delete)
                {
                    this.Delete();
                }
            }
            this.gridControl1.RefreshDataSource();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void Material_Load(object sender, EventArgs e)
        {
            this.Visibles();
        }
    }
}