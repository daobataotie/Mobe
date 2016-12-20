using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.CustomsClearance.MyRecord
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.BGProduct _bGProduct;
        BL.BGProductManager _manager = new Book.BL.BGProductManager();
        BL.BGProductDetaiManager _detailManager = new Book.BL.BGProductDetaiManager();

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.BGProduct.PRO_Id + "NULL", new AA(Properties.Resources.BGProductIdNotNul, this.txt_BGProductId));
            this.invalidValueExceptions.Add(Model.BGProduct.PRO_Id, new AA(Properties.Resources.BGProductIdHave, this.txt_BGProductId));
            this.newChooseContorlEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "view";
        }

        protected override bool HasRows()
        {
            return this._manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._manager.HasRowsAfter(this._bGProduct);
        }

        protected override bool HasRowsPrev()
        {
            return this._manager.HasRowsBefore(this._bGProduct);
        }

        protected override void MoveFirst()
        {
            this._bGProduct = this._manager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._bGProduct = this._manager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.BGProduct model = this._manager.GetNext(this._bGProduct);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGProduct = model;
        }

        protected override void MovePrev()
        {
            Model.BGProduct model = this._manager.GetPrev(this._bGProduct);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGProduct = model;
        }

        protected override void AddNew()
        {
            this._bGProduct = new Book.Model.BGProduct();
            this._bGProduct.BGProductId = Guid.NewGuid().ToString();
            this.action = "insert";
        }

        public override void Refresh()
        {
            if (this._bGProduct == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._bGProduct = this._manager.Get(this._bGProduct.BGProductId);
            }
            this.txt_QiYeNeibuId.Text = this._bGProduct.QiYeNeiId;
            this.txt_BGProductId.Text = this._bGProduct.Id;
            this.txt_CustomsClearance.Text = this._bGProduct.ZhuGuanHaiGuan;
            this.txt_JingYingDanWeiId.Text = this._bGProduct.JingYingDanWeiId;
            this.txt_JingYingDanWeiName.Text = this._bGProduct.JingYingDanWeiName;
            this.txt_fertility.Text = this._bGProduct.ShengChanNengLi.ToString();
            this.txt_JiaGongDanWeiId.Text = this._bGProduct.JiaGongDanWeiId;
            this.txt_JiaGongDanWeiName.Text = this._bGProduct.JiaGongDanWeiName;
            this.comboBoxEditManagerObject.EditValue = this._bGProduct.GuanLiDuiXiang;
            this.txt_ShenBaoDanWeiId.Text = this._bGProduct.ShenBaoDanWeiId;
            this.txt_ShenBaoDanWeiName.Text = this._bGProduct.ShenBaoDanWeiName;
            this.date_ShenBaoDate.EditValue = this._bGProduct.ShenBaoDate;
            this.newChooseContorlEmployee.EditValue = this._bGProduct.Employee;
            this.txt_Note.Text = this._bGProduct.Desc;
            this.newChooseContorlAuditEmp.EditValue = this._bGProduct.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._bGProduct.AuditState);

            this._bGProduct.DetailProduct = this._detailManager.SelectProductByBGProductId(this._bGProduct.BGProductId);
            this._bGProduct.DetailMaterial = this._detailManager.SelectMaterialByBGProductId(this._bGProduct.BGProductId);
            this.bindingSourceProduct.DataSource = this._bGProduct.DetailProduct;
            this.bindingSourceMaterial.DataSource = this._bGProduct.DetailMaterial;
            base.Refresh();
            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.gridView2.OptionsBehavior.Editable = false;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    break;
            }
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow() || !this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;
            this._bGProduct.QiYeNeiId = string.IsNullOrEmpty(this.txt_QiYeNeibuId.Text) ? null : this.txt_QiYeNeibuId.Text;
            this._bGProduct.Id = string.IsNullOrEmpty(this.txt_BGProductId.Text) ? null : this.txt_BGProductId.Text; ;
            this._bGProduct.ZhuGuanHaiGuan = string.IsNullOrEmpty(this.txt_CustomsClearance.Text) ? null : this.txt_CustomsClearance.Text;
            this._bGProduct.JingYingDanWeiId = string.IsNullOrEmpty(this.txt_JingYingDanWeiId.Text) ? null : this.txt_JingYingDanWeiId.Text;
            this._bGProduct.JingYingDanWeiName = string.IsNullOrEmpty(this.txt_JingYingDanWeiName.Text) ? null : this.txt_JingYingDanWeiName.Text;
            this._bGProduct.ShengChanNengLi = Convert.ToDecimal(string.IsNullOrEmpty(this.txt_fertility.Text) ? null : this.txt_fertility.Text);
            this._bGProduct.JiaGongDanWeiId = string.IsNullOrEmpty(this.txt_JiaGongDanWeiId.Text) ? null : this.txt_JiaGongDanWeiId.Text;
            this._bGProduct.JiaGongDanWeiName = string.IsNullOrEmpty(this.txt_JiaGongDanWeiName.Text) ? null : this.txt_JiaGongDanWeiName.Text;
            this._bGProduct.GuanLiDuiXiang = this.comboBoxEditManagerObject.EditValue == null ? null : this.comboBoxEditManagerObject.EditValue.ToString();
            this._bGProduct.ShenBaoDanWeiId = string.IsNullOrEmpty(this.txt_ShenBaoDanWeiId.Text) ? null : this.txt_ShenBaoDanWeiId.Text;
            this._bGProduct.ShenBaoDanWeiName = string.IsNullOrEmpty(this.txt_ShenBaoDanWeiName.Text) ? null : this.txt_ShenBaoDanWeiName.Text;
            if (this.date_ShenBaoDate.EditValue != null)
                this._bGProduct.ShenBaoDate = this.date_ShenBaoDate.DateTime;
            this._bGProduct.EmployeeId = this.newChooseContorlEmployee.EditValue == null ? null : (this.newChooseContorlEmployee.EditValue as Model.Employee).EmployeeId;
            this._bGProduct.Desc = this.txt_Note.Text;

            switch (this.action)
            {
                case "insert":
                    this._manager.Insert(this._bGProduct);
                    break;
                case "update":
                    this._manager.Update(this._bGProduct);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._bGProduct == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.BGProduct model = this._manager.GetNext(this._bGProduct);
                this._manager.Delete(this._bGProduct.BGProductId);
                if (model != null)
                    this._bGProduct = model;
                else
                    this._bGProduct = this._manager.GetLast();
                MessageBox.Show(Properties.Resources.DeleteSuccess);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.BGProductDetai model = new Book.Model.BGProductDetai();
                model.BGProductDetailId = Guid.NewGuid().ToString();
                if (f.SelectedItem != null)
                {
                    model.Product = f.SelectedItem as Model.Product;
                    model.ProductId = model.Product.ProductId;
                }
                if (this.xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    this._bGProduct.DetailProduct.Add(model);
                    model.Id = this._bGProduct.DetailProduct.Count;
                    this.gridControl2.RefreshDataSource();
                }
                else
                {
                    this._bGProduct.DetailMaterial.Add(model);
                    model.Id = this._bGProduct.DetailMaterial.Count;
                    this.gridControl1.RefreshDataSource();
                }
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPageIndex == 1)
            {
                if (this.bindingSourceProduct.Current != null)
                    this._bGProduct.DetailProduct.Remove(this.bindingSourceProduct.Current as Model.BGProductDetai);
                //this._bGProduct.DetailProduct
                this.gridControl2.RefreshDataSource();
            }
            else
            {
                if (this.bindingSourceMaterial.Current != null)
                    this._bGProduct.DetailMaterial.Remove(this.bindingSourceMaterial.Current as Model.BGProductDetai);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPageIndex == 1)
            {
                foreach (Model.BGProductDetai model in this._bGProduct.DetailProduct)
                {
                    model.Id = this._bGProduct.DetailProduct.IndexOf(model) + 1;
                }
                this.gridControl2.RefreshDataSource();
            }
            else
            {
                foreach (Model.BGProductDetai model in this._bGProduct.DetailMaterial)
                {
                    model.Id = this._bGProduct.DetailMaterial.IndexOf(model) + 1;
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumn6")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.BGProductDetai).Product;

                    if (p == null)
                        return;
                    this.repositoryItemComboBox3.Items.Clear();
                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        IList<Model.ProductUnit> units = (new BL.ProductUnitManager()).Select(p.BasedUnitGroupId);
                        foreach (Model.ProductUnit ut in units)
                        {
                            this.repositoryItemComboBox3.Items.Add(ut.CnName);
                        }
                    }
                }
            }
        }

        private void gridView2_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView2.FocusedColumn.Name == "gridColumn21")
            {
                if (this.gridView2.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as Model.BGProductDetai).Product;

                    if (p == null)
                        return;
                    this.repositoryItemComboBox2.Items.Clear();
                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        IList<Model.ProductUnit> units = (new BL.ProductUnitManager()).Select(p.BasedUnitGroupId);
                        foreach (Model.ProductUnit ut in units)
                        {
                            this.repositoryItemComboBox2.Items.Add(ut.CnName);
                        }
                    }
                }
            }
        }


        #region 审核

        protected override string AuditKeyId()
        {
            return Model.BGProduct.PRO_BGProductId;
        }

        protected override int AuditState()
        {
            return this._bGProduct.AuditState.HasValue ? this._bGProduct.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "BGProduct" + "," + this._bGProduct.BGProductId;
        }

        #endregion
    }
}