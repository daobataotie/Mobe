using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.AccountPayable.APParameterSet
{
    public partial class EditForm : Settings.BasicData.BaseEditForm1
    {
        IList<Model.AcItem> _detail = new List<Model.AcItem>();
        BL.AcItemManager _ACitemManager = new Book.BL.AcItemManager();

        public EditForm()
        {
            InitializeComponent();
            this.invalidValueExceptions.Add(Model.AcItem.PRO_ItemName, new AA("項目名稱不能重複", this.gridControl1));
            this.action = "view";
        }

        protected override void AddNew()
        {
            Model.AcItem mac = new Book.Model.AcItem();
            mac.AcItemId = Guid.NewGuid().ToString();
            mac.Id = "01";
            mac.AcItemDate = DateTime.Now;
            mac.mState = true;
            this._detail.Add(mac);
        }

        public override void Refresh()
        {
            this._detail = this._ACitemManager.Select();
            if (this._detail == null || this._detail.Count == 0)
            {
                this.AddNew();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = this._detail;
            //if (this.action == "insert")
            //{
            //    this.gridControl1.RefreshDataSource();
            //}
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

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._ACitemManager.Delete((this.bindingSource1.Current as Model.AcItem).AcItemId);
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this._ACitemManager.Insert(this._detail);
        }

        protected override void grid_keyDpwn()
        {
            //Model.Setting set = new Book.Model.Setting();
            //set.SettingId = Guid.NewGuid().ToString();
            //set.SettingTags = "PCISO";

            //this._detail.Add(set);
            //this.bindingSource1.Position = this.bindingSource1.IndexOf(set);
            //this.gridControl1.RefreshDataSource();
            Model.AcItem mac = new Book.Model.AcItem();
            mac.AcItemId = Guid.NewGuid().ToString();
            mac.Id = this._detail.Max(item => Convert.ToInt32(item.Id)).ToString();
            mac.AcItemDate = DateTime.Now;
            mac.mState = true;
            this._detail.Add(mac);
            this.gridControl1.RefreshDataSource();

        }

        protected override void grid_KeyDelete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._ACitemManager.Delete((this.bindingSource1.Current as Model.AcItem).AcItemId);
            this._detail.Remove(this.bindingSource1.Current as Model.AcItem);
            if (this._detail.Count == 0)
            {
                this.AddNew();
            }
            this.gridControl1.RefreshDataSource();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            (this.bindingSource1.Current as Model.AcItem).mState = true;
        }
    }
}