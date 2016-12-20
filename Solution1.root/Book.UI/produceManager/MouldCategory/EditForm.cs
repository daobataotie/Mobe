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
    public partial class EditForm : BaseEditForm
    {

        BL.MouldCategoryManager mouldCateManager = new Book.BL.MouldCategoryManager();
        Model.MouldCategory mouldCategory = null;

        public EditForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.MouldCategory.PROPERTY_ID, new AA(Properties.Resources.NunsIsExists, this.textEditId as Control));
            this.requireValueExceptions.Add(Model.MouldCategory.PROPERTY_ID, new AA(Properties.Resources.NumsIsNotNull, this.textEditId as Control));
            this.invalidValueExceptions.Add(Model.MouldCategory.PROPERTY_MOULDCATEGORYNAME, new AA(Properties.Resources.NameIsExists, this.textEditMouldCategoryName as Control));
            this.requireValueExceptions.Add(Model.MouldCategory.PROPERTY_MOULDCATEGORYNAME, new AA(Properties.Resources.NameIsNotNull, this.textEditMouldCategoryName as Control));
            //this.requireValueExceptions.Add(Model.MouldCategory.PROPERTY_MOULDCATEGORYID, new AA(Properties.Resources.MouldIdIsNull,this.textEditId as Control));

            this.action = "view";

        }


        protected override bool HasRows()
        {
            return this.mouldCateManager.HasRows();
        }

        protected override void AddNew()
        {
            this.mouldCategory = new Book.Model.MouldCategory();
            this.mouldCategory.MouldCategoryId = Guid.NewGuid().ToString();
        }

        protected override void Delete()
        {
            //if (this.mouldCategory == null)
            //    return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            Model.MouldCategory mouldCategory = this.bindingSource1.Current as Model.MouldCategory;
            this.mouldCateManager.Delete(mouldCategory.MouldCategoryId);
            //this.mouldCategory = this.mouldCateManager.GetNext(this.mouldCategory);
            //if (this.mouldCategory == null)
            //    this.mouldCategory = this.mouldCateManager.GetLast();
        }

        protected override void Save()
        {
            this.mouldCategory.Id = textEditId.Text;
            this.mouldCategory.MouldCategoryName = textEditMouldCategoryName.Text;
            this.mouldCategory.MouldCategoryDescription = memoEditMouldCategoryDescription.Text;

            switch (this.action)
            {
                case "insert":
                    this.mouldCateManager.Insert(this.mouldCategory);
                    break;

                case "update":
                    this.mouldCateManager.Update(this.mouldCategory);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this.mouldCategory == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            if (this.action == "view")
            {
                this.mouldCategory = this.mouldCateManager.Get(this.mouldCategory.MouldCategoryId);
            }

            this.textEditId.Text = this.mouldCategory.Id;
            this.textEditMouldCategoryName.Text = this.mouldCategory.MouldCategoryName;
            this.memoEditMouldCategoryDescription.Text = this.mouldCategory.MouldCategoryDescription;

            this.bindingSource1.DataSource = this.mouldCateManager.Select();


            base.Refresh();
        }

        protected override void MoveLast()
        {
            this.mouldCategory = this.mouldCateManager.GetLast();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.Visibles();
        }

        private void memoEditMouldCategoryDescription_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.memoEditMouldCategoryDescription.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            this.mouldCategory = this.bindingSource1.Current as Model.MouldCategory;
            if (mouldCategory != null)
            {
                this.action = "view";
                this.textEditId.Text = this.mouldCategory.Id;
                this.textEditMouldCategoryName.Text = this.mouldCategory.MouldCategoryName;
                this.memoEditMouldCategoryDescription.Text = this.mouldCategory.MouldCategoryDescription;
                this.Refresh();
            }

        }
    }
}
