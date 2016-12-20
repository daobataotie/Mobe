using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
namespace Book.UI.Accounting.AccountingCategories
{
    public partial class ListForm : BaseEditForm
    {
        private Book.BL.AtAccountingCategoriesManager AtAccountingCategoriesManager = new Book.BL.AtAccountingCategoriesManager();
        private IList<Book.Model.AtAccountingCategories> _detail = new List<Book.Model.AtAccountingCategories>();
        public ListForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtAccountingCategories.PRO_Id, new AA("資料輸入不完整！", this.gridControl1 as Control));
            this.action = "view";
        }

        private void ListForm_Load(object sender, EventArgs e)
        {

        }
        #region Overrieded
        protected override void Delete()
        {

            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.AtAccountingCategoriesManager.Delete(this.bindingSource1.Current as Model.AtAccountingCategories);


        }
        protected override void Save()
        {
            if (this.action != "view")
                this.AtAccountingCategoriesManager.Update(this._detail);
        }

        #endregion
        protected override void AddNew()
        {
            this.action = "insert";
        }
        public override void Refresh()
        {

            this._detail = AtAccountingCategoriesManager.Select();
            this.bindingSource1.DataSource = this._detail;
            if (this.action == "insert")
            {
                Model.AtAccountingCategories pc = new Model.AtAccountingCategories();
                this._detail.Add(pc);
                this.bindingSource1.DataSource = this._detail;
                this.bindingSource1.Position = this.bindingSource1.IndexOf(pc);
                this.gridControl1.RefreshDataSource();
            }

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
            base.Refresh();
        }
        protected override bool HasRows()
        {
            return this.AtAccountingCategoriesManager.HasRows();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {
                    Model.AtAccountingCategories pc = new Model.AtAccountingCategories();
                    this._detail.Add(pc);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(pc);
                }
                if (e.KeyData == Keys.Delete)
                {
                    this._detail.Remove(this.bindingSource1.Current as Model.AtAccountingCategories);
                }
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}