using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.CustomsClearance
{
    public partial class BGHandbookDepotOutForm : Settings.BasicData.BaseEditForm
    {
        BL.BGHandbookDepotOutManager manager = new Book.BL.BGHandbookDepotOutManager();
        BL.BGHandbookDepotOutDetailManager detailmanager = new Book.BL.BGHandbookDepotOutDetailManager();
        Model.BGHandbookDepotOut _bGHandbookDepotOut;
        BL.BGHandbookDetail2Manager bGHandbookDetail2Manager = new Book.BL.BGHandbookDetail2Manager();
        IList<Model.BGHandbookDetail2> list;
        public BGHandbookDepotOutForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.BGHandbookDepotOut.PRO_BGHangbookId, new AA(Properties.Resources.ShouceIdIsNotNull, this.lookUpEditBGHandbook));
            this.bindingSourceBGHandbook.DataSource = (new BL.BGHandbookIdSetManager()).Select();
            this.bindingSourceProductUnit.DataSource = (new BL.ProductUnitManager()).Select();
            if (this.lookUpEditBGHandbook.EditValue != null)
                this.bindingSourceBGProduct.DataSource = list = bGHandbookDetail2Manager.SelectByShouce(this.lookUpEditBGHandbook.EditValue.ToString());
            this.action = "view";
        }

        protected override void AddNew()
        {
            this._bGHandbookDepotOut = new Book.Model.BGHandbookDepotOut();
            this._bGHandbookDepotOut.BGHandbookDepotOutId = this.manager.GetId();
            this.action = "insert";
        }
        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._bGHandbookDepotOut);
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._bGHandbookDepotOut);
        }

        protected override void MoveFirst()
        {
            this._bGHandbookDepotOut = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._bGHandbookDepotOut = this.manager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.BGHandbookDepotOut model = this.manager.GetNext(this._bGHandbookDepotOut);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGHandbookDepotOut = model;
        }

        protected override void MovePrev()
        {
            Model.BGHandbookDepotOut model = this.manager.GetPrev(this._bGHandbookDepotOut);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGHandbookDepotOut = model;
        }

        public override void Refresh()
        {
            if (this._bGHandbookDepotOut == null)
                this.AddNew();
            this.txt_BGHandbookDepotOutId.EditValue = this._bGHandbookDepotOut.BGHandbookDepotOutId;
            this.date_BGHandbookDepotOutDate.EditValue = this._bGHandbookDepotOut.BGHandbookDepotOutDate;
            this.lookUpEditBGHandbook.EditValue = this._bGHandbookDepotOut.BGHangbookId;
            this._bGHandbookDepotOut.Detail = detailmanager.SelectByBGHandbookDepotOutId(this._bGHandbookDepotOut.BGHandbookDepotOutId);
            this.bindingSourceDetail.DataSource = this._bGHandbookDepotOut.Detail;
            base.Refresh();

            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.lookUpEditBGHandbook.Properties.ReadOnly = true;
                    break;
                case"insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.lookUpEditBGHandbook.Properties.ReadOnly = false;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.lookUpEditBGHandbook.Properties.ReadOnly = true;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.lookUpEditBGHandbook.Properties.ReadOnly = false;
                    break;
            }
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();
            this._bGHandbookDepotOut.BGHandbookDepotOutId = this.txt_BGHandbookDepotOutId.Text;
            this._bGHandbookDepotOut.BGHangbookId = this.lookUpEditBGHandbook.EditValue == null ? null : this.lookUpEditBGHandbook.EditValue.ToString();
            this._bGHandbookDepotOut.BGHandbookDepotOutDate = this.date_BGHandbookDepotOutDate.EditValue == null ? DateTime.Now : this.date_BGHandbookDepotOutDate.DateTime;
            switch (action)
            {
                case "insert":
                    this.manager.Insert(this._bGHandbookDepotOut);
                    break;
                case "update":
                    this.manager.Update(this._bGHandbookDepotOut);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._bGHandbookDepotOut == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Model.BGHandbookDepotIn model =
                this.manager.Delete(this._bGHandbookDepotOut.BGHandbookDepotOutId);
                this._bGHandbookDepotOut = this.manager.GetNext(this._bGHandbookDepotOut);
                if (this._bGHandbookDepotOut == null)
                    this._bGHandbookDepotOut = this.manager.GetLast();

                MessageBox.Show(Properties.Resources.DeleteSuccess);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Model.BGHandbookDepotOutDetail model = new Book.Model.BGHandbookDepotOutDetail();
            model.BGHandbookDepotOutDetailId = Guid.NewGuid().ToString();
            model.BGHandbookDepotOutDetailDate = DateTime.Now;
            this._bGHandbookDepotOut.Detail.Add(model);
            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(model);
            this.gridControl1.RefreshDataSource();
        }

        private void repositoryItemLookUpEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.lookUpEditBGHandbook.EditValue == null)
            {
                MessageBox.Show("请先选择手册！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (this.bindingSourceBGProduct.Count < 1)
            {
                this.bindingSourceBGProduct.DataSource = this.bGHandbookDetail2Manager.SelectByShouce(this.lookUpEditBGHandbook.EditValue.ToString());
            }
        }

        private void lookUpEditBGHandbook_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEditBGHandbook.EditValue != null)
                this.bindingSourceBGProduct.DataSource = this.bGHandbookDetail2Manager.SelectByShouce(this.lookUpEditBGHandbook.EditValue.ToString());

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (list == null)
                list = this.bGHandbookDetail2Manager.SelectByShouce(this.lookUpEditBGHandbook.EditValue.ToString());
            if (e.Column == this.gridColumn2)
            {
                if (this.bindingSourceBGProduct.Current != null)
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn3, list.Where(d => d.Id == Convert.ToInt32((this.bindingSourceDetail.Current as Model.BGHandbookDepotOutDetail).BGHandbookProductId)).First().ProName);
            }
            this.gridControl1.RefreshDataSource();
        }
    }
}