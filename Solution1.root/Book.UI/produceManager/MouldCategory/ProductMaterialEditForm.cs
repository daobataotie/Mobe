using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData;

namespace Book.UI.produceManager.MouldCategory
{
    public partial class ProductMaterialEditForm : BaseEditForm
    {
        private BL.ProductMaterialManager productMateManager = new Book.BL.ProductMaterialManager();
        private Model.ProductMaterial productMaterial;

        public ProductMaterialEditForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.ProductMaterial.PRO_Id, new AA(Properties.Resources.NunsIsExists, this.textEditId as Control));
            this.requireValueExceptions.Add(Model.ProductMaterial.PRO_Id, new AA(Properties.Resources.NumsIsNotNull, this.textEditId as Control));
            this.invalidValueExceptions.Add(Model.ProductMaterial.PRO_ProductMaterialName, new AA(Properties.Resources.NameIsExists, this.textEditProductMaterialName as Control));
            this.requireValueExceptions.Add(Model.ProductMaterial.PRO_ProductMaterialName, new AA(Properties.Resources.NameIsNotNull, this.textEditProductMaterialName as Control));

            this.action = "view";
        }

        protected override void AddNew()
        {
            this.productMaterial = new Book.Model.ProductMaterial();
            this.productMaterial.ProductMaterialId = Guid.NewGuid().ToString();
        }

        protected override void MoveNext() 
        {
            Model.ProductMaterial pro = this.productMateManager.GetNext(this.productMaterial);
            if (pro == null) 
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.productMaterial = pro;
        }

        protected override void MovePrev()
        {
            Model.ProductMaterial pro = this.productMateManager.GetPrev(this.productMaterial);
            if (pro == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.productMaterial = pro;
        }

        protected override bool HasRows()
        {
            return this.productMateManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.productMateManager.HasRowsAfter(this.productMaterial);
        }

        protected override bool HasRowsPrev()
        {
            return this.productMateManager.HasRowsBefore(this.productMaterial);
        }

        protected override void Save()
        {
            this.productMaterial.Id = this.textEditId.Text;
            this.productMaterial.ProductMaterialName = this.textEditProductMaterialName.Text;
            this.productMaterial.ProductMaterialDescription = this.memoEditProductMaterialDescription.Text;

            switch (this.action)
            {
                case "insert":
                    this.productMateManager.Insert(this.productMaterial);
                    break;

                case "update":
                    this.productMateManager.Update(this.productMaterial);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this.productMaterial == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                this.productMaterial = this.productMateManager.Get(this.productMaterial.ProductMaterialId);
                if (this.productMaterial == null)
                {
                    this.productMaterial = new Book.Model.ProductMaterial();
                }
            }
            this.textEditId.Text = this.productMaterial.Id;
            this.textEditProductMaterialName.Text = this.productMaterial.ProductMaterialName;
            this.memoEditProductMaterialDescription.Text = this.productMaterial.ProductMaterialDescription;
            this.bindingSource1.DataSource = this.productMateManager.Select();
            base.Refresh();
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            Model.ProductMaterial productMaterial = this.bindingSource1.Current as Model.ProductMaterial;
            this.productMateManager.Delete(productMaterial.ProductMaterialId);
        }

        protected override void MoveFirst()
        {
            this.productMaterial = this.productMateManager.GetFirst();
        }

        protected override void MoveLast()
        {
            this.productMaterial = this.productMateManager.GetLast();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            this.productMaterial = this.bindingSource1.Current as Model.ProductMaterial;
            if (productMaterial != null)
            {
                this.action = "view";
                this.textEditId.Text = this.productMaterial.Id;
                this.textEditProductMaterialName.Text = this.productMaterial.ProductMaterialName;
                this.memoEditProductMaterialDescription.Text = this.productMaterial.ProductMaterialDescription;
                this.Refresh();
            }
        }
    }
}
