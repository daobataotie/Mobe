using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;

namespace Book.UI.produceManager.ProduceOtherReturnMaterial
{

    //*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010   咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究
    // 功能描述: 为外退料
    // 编 码 人: 刘永亮                   完成时间:2010-08-31
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class EditForm : Settings.BasicData.BaseEditForm
    {

        BL.ProduceOtherReturnMaterialManager _produceOtherReturnMaterialManager = new BL.ProduceOtherReturnMaterialManager();
        Model.ProduceOtherReturnMaterial _produceOtherReturnMaterial;
        BL.ProductManager productManager = new BL.ProductManager();
        private BL.ProduceOtherCompactMaterialManager OtherCompactMaterialManager = new Book.BL.ProduceOtherCompactMaterialManager();
        private BL.DepotPositionManager depotPositionManager = new BL.DepotPositionManager();

        public EditForm()
        {
            InitializeComponent();
            //  this.requireValueExceptions.Add(Model.ProduceOtherReturnDetail.PRO_DepotPositionId, new AA(Properties.Resources.RequireChoosePosition, this.gridControl1));
            this.action = "view";
            this.newChooseEmployee0Id.Choose = new ChooseEmployee();
            this.newChooseEmployee1Id.Choose = new ChooseEmployee();
            this.newChooseEmployee2Id.Choose = new ChooseEmployee();
            this.EmpAudit.Choose = new ChooseEmployee();
            this.newChooseSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseContorlDepot.Choose = new ChooseDepot();
        }

        protected override void Delete()
        {
            if (this._produceOtherReturnMaterial == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._produceOtherReturnMaterialManager.Delete(this._produceOtherReturnMaterial);
            this._produceOtherReturnMaterial = this._produceOtherReturnMaterialManager.GetNext(this._produceOtherReturnMaterial);
            if (this._produceOtherReturnMaterial == null)
            {
                this._produceOtherReturnMaterial = this._produceOtherReturnMaterialManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this._produceOtherReturnMaterial = new Model.ProduceOtherReturnMaterial();
            this._produceOtherReturnMaterial.ProduceOtherReturnMaterialId = this._produceOtherReturnMaterialManager.GetId();
            this._produceOtherReturnMaterial.ProduceOtherReturnMaterialDate = DateTime.Now;
            this._produceOtherReturnMaterial.Employee0 = BL.V.ActiveOperator.Employee;
            this._produceOtherReturnMaterial.Details = new List<Model.ProduceOtherReturnDetail>();
        }

        protected override void Save()
        {
            this._produceOtherReturnMaterial.ProduceOtherReturnMaterialId = this.textProduceOtherReturnMaterialId.Text;


            this._produceOtherReturnMaterial.Employee0 = this.newChooseEmployee0Id.EditValue as Model.Employee;
            if (this.newChooseEmployee0Id.EditValue != null)
            {
                this._produceOtherReturnMaterial.Employee0Id = this._produceOtherReturnMaterial.Employee0.EmployeeId;
            }
            this._produceOtherReturnMaterial.Employee1 = this.newChooseEmployee1Id.EditValue as Model.Employee;
            if (this.newChooseEmployee1Id.EditValue != null)
            {

                this._produceOtherReturnMaterial.Employee1Id = this._produceOtherReturnMaterial.Employee1.EmployeeId;
            }
            this._produceOtherReturnMaterial.Employee2 = this.newChooseEmployee2Id.EditValue as Model.Employee;
            if (this.newChooseEmployee2Id.EditValue != null)
            {

                this._produceOtherReturnMaterial.Employee2Id = this._produceOtherReturnMaterial.Employee2.EmployeeId;
            }

            this._produceOtherReturnMaterial.ProduceOtherReturnMaterialDate = this.dateProduceOtherReturnMaterialDate.DateTime;
            this._produceOtherReturnMaterial.ProduceOtherReturnMaterialDesc = this.textProduceOtherReturnMaterialDesc.Text;
            this._produceOtherReturnMaterial.Supplier = this.newChooseSupplier.EditValue as Model.Supplier;
            if (this._produceOtherReturnMaterial.Supplier != null)
            {
                this._produceOtherReturnMaterial.SupplierId = this._produceOtherReturnMaterial.Supplier.SupplierId;
            }
            this._produceOtherReturnMaterial.AuditState = this.saveAuditState;
            this._produceOtherReturnMaterial.DepotId = this.newChooseContorlDepot.EditValue == null ? null : (this.newChooseContorlDepot.EditValue as Model.Depot).DepotId;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._produceOtherReturnMaterialManager.Insert(this._produceOtherReturnMaterial);
                    break;

                case "update":
                    this._produceOtherReturnMaterialManager.Update(this._produceOtherReturnMaterial);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._produceOtherReturnMaterial == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._produceOtherReturnMaterial = this._produceOtherReturnMaterialManager.GetDetails(this._produceOtherReturnMaterial);
                }
            }

            this.textProduceOtherReturnMaterialId.Text = this._produceOtherReturnMaterial.ProduceOtherReturnMaterialId;
            this.dateProduceOtherReturnMaterialDate.DateTime = this._produceOtherReturnMaterial.ProduceOtherReturnMaterialDate.Value;

            this.newChooseEmployee0Id.EditValue = this._produceOtherReturnMaterial.Employee0;
            this.newChooseEmployee1Id.EditValue = this._produceOtherReturnMaterial.Employee1;
            this.newChooseEmployee2Id.EditValue = this._produceOtherReturnMaterial.Employee2;
            this.EmpAudit.EditValue = this._produceOtherReturnMaterial.AuditEmp;
            this.textEditAuditState.Text = this.GetAuditName(this._produceOtherReturnMaterial.AuditState);
            this.newChooseContorlDepot.EditValue = this._produceOtherReturnMaterial.Depot;
            this.bindingSourceProduceOtherReturnDetail.DataSource = this._produceOtherReturnMaterial.Details;
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

            }

            this.textProduceOtherReturnMaterialId.Properties.ReadOnly = true;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Ro(this._produceOtherReturnMaterial);
        }

        protected override bool HasRows()
        {
            return this._produceOtherReturnMaterialManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._produceOtherReturnMaterialManager.HasRowsAfter(this._produceOtherReturnMaterial);
        }

        protected override bool HasRowsPrev()
        {
            return this._produceOtherReturnMaterialManager.HasRowsBefore(this._produceOtherReturnMaterial);
        }

        protected override void MoveFirst()
        {
            this._produceOtherReturnMaterial = this._produceOtherReturnMaterialManager.GetFirst() == null ? null : this._produceOtherReturnMaterialManager.GetFirst();
        }

        protected override void MoveNext()
        {
            Model.ProduceOtherReturnMaterial material = this._produceOtherReturnMaterialManager.GetNext(this._produceOtherReturnMaterial);
            if (material == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceOtherReturnMaterial = this._produceOtherReturnMaterialManager.Get(material.ProduceOtherReturnMaterialId);
        }

        protected override void MoveLast()
        {
            this._produceOtherReturnMaterial = this._produceOtherReturnMaterialManager.GetLast() == null ? null : this._produceOtherReturnMaterialManager.GetLast();
        }

        protected override void MovePrev()
        {
            Model.ProduceOtherReturnMaterial material = this._produceOtherReturnMaterialManager.GetPrev(this._produceOtherReturnMaterial);
            if (material == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceOtherReturnMaterial = this._produceOtherReturnMaterialManager.Get(material.ProduceOtherReturnMaterialId);
        }

        private void simple_Append_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this._produceOtherReturnMaterial.Details.Count > 0 && this._produceOtherReturnMaterial.Details[0] != null && string.IsNullOrEmpty(this._produceOtherReturnMaterial.Details[0].ProductId))
                    this._produceOtherReturnMaterial.Details.RemoveAt(0);
                Model.ProduceOtherReturnDetail detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {

                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.ProduceOtherReturnDetail();
                        detail.ProduceOtherReturnDetailId = Guid.NewGuid().ToString();
                        detail.ProduceOtherReturnMaterialId = this._produceOtherReturnMaterial.ProduceOtherReturnMaterialId;
                        detail.Product = this.productManager.Get(product.ProductId);
                        detail.ProductId = product.ProductId;
                        detail.ProductUnit = product.DepotUnit.ToString();
                        detail.Quantity = 0;
                        this._produceOtherReturnMaterial.Details.Add(detail);
                    }
                }

                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.ProduceOtherReturnDetail();
                    detail.ProduceOtherReturnDetailId = Guid.NewGuid().ToString();
                    detail.ProduceOtherReturnMaterialId = this._produceOtherReturnMaterial.ProduceOtherReturnMaterialId;
                    detail.Product = this.productManager.Get((f.SelectedItem as Model.Product).ProductId);
                    detail.ProductId = detail.Product.ProductId;
                    detail.ProductUnit = (f.SelectedItem as Model.Product).DepotUnit.ToString();
                    detail.Quantity = 0;
                    this._produceOtherReturnMaterial.Details.Add(detail);

                }

                this.bindingSourceProduceOtherReturnDetail.Position = this.bindingSourceProduceOtherReturnDetail.IndexOf(detail);
                this.gridControl1.RefreshDataSource();

            }
            f.Dispose();
            System.GC.Collect();
        }

        private void simple_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceProduceOtherReturnDetail.Current != null)
            {
                this._produceOtherReturnMaterial.Details.Remove(this.bindingSourceProduceOtherReturnDetail.Current as Book.Model.ProduceOtherReturnDetail);

                if (this._produceOtherReturnMaterial.Details.Count == 0)
                {
                    Model.ProduceOtherReturnDetail detail = new Model.ProduceOtherReturnDetail();
                    detail.ProduceOtherReturnDetailId = Guid.NewGuid().ToString();
                    detail.Quantity = 0;
                    detail.ProduceOtherReturnDetailDesc = "";
                    detail.Product = new Book.Model.Product();
                    this._produceOtherReturnMaterial.Details.Add(detail);
                    this.bindingSourceProduceOtherReturnDetail.Position = this.bindingSourceProduceOtherReturnDetail.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProduceOtherCompact.ChooseOutContract f = new ProduceOtherCompact.ChooseOutContract();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (f.key == null || f.key.Count == 0) return;
            this.newChooseSupplier.EditValue = f.key[0].ProduceOtherCompact.Supplier;
            if (this._produceOtherReturnMaterial.Details.Count > 0 && string.IsNullOrEmpty(this._produceOtherReturnMaterial.Details[0].ProductId))
                this._produceOtherReturnMaterial.Details.RemoveAt(0);

            foreach (Model.ProduceOtherCompactDetail item in f.key)
            {
                Model.ProduceOtherReturnDetail detail = new Model.ProduceOtherReturnDetail();
                detail.ProduceOtherReturnDetailId = Guid.NewGuid().ToString(); detail.ProduceOtherReturnMaterialId = this._produceOtherReturnMaterial.ProduceOtherReturnMaterialId;
                detail.Product = item.Product;
                detail.ProductId = item.ProductId;
                detail.ProductUnit = item.ProductUnit;
                detail.ProduceOtherCompactId = item.ProduceOtherCompactId;
                detail.ProduceOtherCompactDetailId = item.OtherCompactDetailId;
                detail.Quantity = 0; //item.ProduceQuantity;// - (item.AlreadyOutQuantity == null ? 0 : item.AlreadyOutQuantity);
                detail.HandbookId = item.HandbookId;
                detail.HandbookProductId = item.HandbookProductId;

                // detail.ProcessPrice = 0;
                // detail.ProduceOtherReturnDetailDesc = item.Description;
                //detail.Inumber = this._produceOtherMaterial.Details.Count + 1;
                this._produceOtherReturnMaterial.Details.Add(detail);
            }
            this.gridControl1.RefreshDataSource();
            f.Dispose();
            GC.Collect();
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.ProduceOtherReturnDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.ProduceOtherReturnDetail;

            if (e.Column == this.gridColumnId || e.Column == this.gridColumnPName)
            {
                if (detail != null)
                {
                    Model.Product p = detail.Product;
                    //detail.ProduceOtherMaterialDetailId = Guid.NewGuid().ToString();
                    detail.Quantity = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.ProductUnit = p.DepotUnit.ToString();
                    detail.ProduceOtherReturnDetailDesc = string.Empty;
                    this.bindingSourceProduceOtherReturnDetail.Position = this.bindingSourceProduceOtherReturnDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm lForm = new ListForm();
            if (lForm.ShowDialog(this) == DialogResult.OK)
            {
                this._produceOtherReturnMaterial = lForm.SelectItem as Model.ProduceOtherReturnMaterial;
                this.Refresh();
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherReturnDetail> details = this.bindingSourceProduceOtherReturnDetail.DataSource as IList<Model.ProduceOtherReturnDetail>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumnId":
                    if (detail == null) return;
                    e.DisplayText = detail.Id;
                    break;
                case "gridColumnCusProName":
                    if (detail == null) return;
                    e.DisplayText = detail.CustomerProductName;
                    break;
            }
        }

        private void textProduceOtherReturnMaterialDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.textProduceOtherReturnMaterialDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核
        protected override string AuditKeyId()
        {
            return Model.ProduceOtherReturnMaterial.PRO_ProduceOtherReturnMaterialId;
        }

        protected override int AuditState()
        {
            return this._produceOtherReturnMaterial.AuditState.HasValue ? this._produceOtherReturnMaterial.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "ProduceOtherReturnMaterial" + "," + this._produceOtherReturnMaterial.ProduceOtherReturnMaterialId;
        }
        #endregion

        private void newChooseContorlDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseContorlDepot.EditValue != null)
                this.bindingSourceDepotPosition.DataSource = this.depotPositionManager.Select((this.newChooseContorlDepot.EditValue as Model.Depot).DepotId);
            this.gridControl1.RefreshDataSource();
        }
    }
}