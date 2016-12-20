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
    public partial class BGHandbookDepotInForm : Settings.BasicData.BaseEditForm
    {
        BL.BGHandbookDepotInManager manager = new Book.BL.BGHandbookDepotInManager();
        BL.BGHandbookDepotInDetailManager detailmanager = new Book.BL.BGHandbookDepotInDetailManager();
        Model.BGHandbookDepotIn _bGHandbookDepotIn;
        BL.BGHandbookDetail2Manager bGHandbookDetail2Manager = new Book.BL.BGHandbookDetail2Manager();
        IList<Model.BGHandbookDetail2> list;
        public BGHandbookDepotInForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.BGHandbookDepotIn.PRO_BGHangbookId, new AA(Properties.Resources.ShouceIdIsNotNull, this.lookUpEditBGHandbook));
            this.bindingSourceBGHandbook.DataSource = (new BL.BGHandbookIdSetManager()).Select();
            this.bindingSourceProductUnit.DataSource = (new BL.ProductUnitManager()).Select();
            if (this.lookUpEditBGHandbook.EditValue != null)
                this.bindingSourceBGProduct.DataSource = list = bGHandbookDetail2Manager.SelectByShouce(this.lookUpEditBGHandbook.EditValue.ToString());
            this.action = "view";
        }

        protected override void AddNew()
        {
            this._bGHandbookDepotIn = new Book.Model.BGHandbookDepotIn();
            this._bGHandbookDepotIn.BGHandbookDepotInId = this.manager.GetId();
            this.action = "insert";
        }
        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._bGHandbookDepotIn);
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._bGHandbookDepotIn);
        }

        protected override void MoveFirst()
        {
            this._bGHandbookDepotIn = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._bGHandbookDepotIn = this.manager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.BGHandbookDepotIn model = this.manager.GetNext(this._bGHandbookDepotIn);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGHandbookDepotIn = model;
        }

        protected override void MovePrev()
        {
            Model.BGHandbookDepotIn model = this.manager.GetPrev(this._bGHandbookDepotIn);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGHandbookDepotIn = model;
        }

        public override void Refresh()
        {
            if (this._bGHandbookDepotIn == null)
                this.AddNew();
            this.txt_BGHandbookDepotInId.EditValue = this._bGHandbookDepotIn.BGHandbookDepotInId;
            this.date_BGHandbookDepotInDate.EditValue = this._bGHandbookDepotIn.BGHandbookDepotInDate;
            this.lookUpEditBGHandbook.EditValue = this._bGHandbookDepotIn.BGHangbookId;
            this._bGHandbookDepotIn.Detail = detailmanager.SelectByBGHandbookDepotInId(this._bGHandbookDepotIn.BGHandbookDepotInId);
            this.bindingSourceDetail.DataSource = this._bGHandbookDepotIn.Detail;
            base.Refresh();

            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.lookUpEditBGHandbook.Properties.ReadOnly = true;
                    break;
                case "insert":
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
            this._bGHandbookDepotIn.BGHandbookDepotInId = this.txt_BGHandbookDepotInId.Text;
            this._bGHandbookDepotIn.BGHangbookId = this.lookUpEditBGHandbook.EditValue == null ? null : this.lookUpEditBGHandbook.EditValue.ToString();
            this._bGHandbookDepotIn.BGHandbookDepotInDate = this.date_BGHandbookDepotInDate.EditValue == null ? DateTime.Now : this.date_BGHandbookDepotInDate.DateTime;
            switch (action)
            {
                case "insert":
                    this.manager.Insert(this._bGHandbookDepotIn);
                    break;
                case "update":
                    this.manager.Update(this._bGHandbookDepotIn);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._bGHandbookDepotIn == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Model.BGHandbookDepotIn model =
                this.manager.Delete(this._bGHandbookDepotIn.BGHandbookDepotInId);
                this._bGHandbookDepotIn = this.manager.GetNext(this._bGHandbookDepotIn);
                if (this._bGHandbookDepotIn == null)
                    this._bGHandbookDepotIn = this.manager.GetLast();

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
            Model.BGHandbookDepotInDetail model = new Book.Model.BGHandbookDepotInDetail();
            model.BGHandbookDepotInDetailId = Guid.NewGuid().ToString();
            model.BGHandbookDepotInDetailDate = DateTime.Now;
            this._bGHandbookDepotIn.Detail.Add(model);
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
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn3, list.Where(d => d.Id == Convert.ToInt32((this.bindingSourceDetail.Current as Model.BGHandbookDepotInDetail).BGHandbookProductId)).First().ProName);
            }
            this.gridControl1.RefreshDataSource();
        }
    }
}