using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCFlameRetardant
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.PCFlameRetardant _pCFlameRetardant;
        BL.PCFlameRetardantManager _pCFlameRetardantManager = new Book.BL.PCFlameRetardantManager();
        BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();

        public EditForm()
        {
            InitializeComponent();

            this.ncc_Employee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "view";

            #region LookUpEdit
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            //dt.Columns.Add("name", typeof(string));
            DataRow dr;
            dr = dt.NewRow();
            //dr[0] = "";
            dr[0] = " ";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "0";
            dr[0] = "√";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "1";
            dr[0] = "×";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = "2";
            dr[0] = "△";
            dt.Rows.Add(dr);

            for (int i = 0; i < this.gridView1.Columns.Count; i++)
            {
                if (this.gridView1.Columns[i].Name == "gridColumn4" || this.gridView1.Columns[i].Name == "gridColumn5" || this.gridView1.Columns[i].Name == "gridColumn6")
                {
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DataSource = dt;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.Clear();
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).NullText = "";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id",25, "标识"),
                     });
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DisplayMember = "id";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).ValueMember = "id";
                }
            }
            #endregion

            this.bindingSourceProduct.DataSource = this._pCFlameRetardantManager.Query("select ProductId,Id,ProductName from Product", 30, "Product").Tables[0];
            this.bindingSourceEmployee.DataSource = this._pCFlameRetardantManager.Query("select EmployeeId,IDNo,EmployeeName from Employee", 30, "Employee").Tables[0];
        }

        int LastFlag = 0; //页面载 入时是否执行 last方法
        public EditForm(string PCFlameRetardantId)
            : this()
        {
            this._pCFlameRetardant = this._pCFlameRetardantManager.Get(PCFlameRetardantId);
            if (this._pCFlameRetardant == null)
                throw new ArithmeticException("PCFlameRetardantId");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCFlameRetardant PCFlameRetardant)
            : this()
        {
            if (PCFlameRetardant == null)
                throw new ArithmeticException("PCFlameRetardantId");
            this._pCFlameRetardant = PCFlameRetardant;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pCFlameRetardant = new Book.Model.PCFlameRetardant();
            this._pCFlameRetardant.PCFlameRetardantId = this._pCFlameRetardantManager.GetId();
            this._pCFlameRetardant.InvoiceDate = DateTime.Now;

            this._pCFlameRetardant.Employee = BL.V.ActiveOperator.Employee;
            this._pCFlameRetardant.EmployeeId = BL.V.ActiveOperator.EmployeeId;

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this._pCFlameRetardantManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._pCFlameRetardantManager.HasRowsAfter(this._pCFlameRetardant);
        }

        protected override bool HasRowsPrev()
        {
            return this._pCFlameRetardantManager.HasRowsBefore(this._pCFlameRetardant);
        }

        protected override void MoveFirst()
        {
            this._pCFlameRetardant = this._pCFlameRetardantManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._pCFlameRetardant = this._pCFlameRetardantManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.PCFlameRetardant PCFlameRetardant = this._pCFlameRetardantManager.GetNext(this._pCFlameRetardant);
            if (PCFlameRetardant == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCFlameRetardant = PCFlameRetardant;
        }

        protected override void MovePrev()
        {
            Model.PCFlameRetardant PCFlameRetardant = this._pCFlameRetardantManager.GetPrev(this._pCFlameRetardant);
            if (PCFlameRetardant == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pCFlameRetardant = PCFlameRetardant;
        }

        public override void Refresh()
        {
            if (this._pCFlameRetardant == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._pCFlameRetardant = this._pCFlameRetardantManager.GetDetail(this._pCFlameRetardant.PCFlameRetardantId);
            }

            this.txt_Id.EditValue = this._pCFlameRetardant.PCFlameRetardantId;
            this.date_Check.EditValue = this._pCFlameRetardant.InvoiceDate;
            this.ncc_Employee.EditValue = this._pCFlameRetardant.Employee;

            this.txt_Note.EditValue = this._pCFlameRetardant.Note;
            this.newChooseContorlAuditEmp.EditValue = this._pCFlameRetardant.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pCFlameRetardant.AuditState);

            this.bindingSourceDetail.DataSource = this._pCFlameRetardant.Details;

            base.Refresh();

            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
            }

            this.txt_Id.Properties.ReadOnly = true;
        }

        //private void GetRadioGroup()
        //{
        //    this.radioFlap.EditValue = this._pCFlameRetardant.Flap == null ? 0 : this._pCFlameRetardant.Flap;
        //    this.radioExterior.EditValue = this._pCFlameRetardant.Exterior == null ? 0 : this._pCFlameRetardant.Exterior;
        //    this.radioOfColor.EditValue = this._pCFlameRetardant.OfColor == null ? 0 : this._pCFlameRetardant.OfColor; ;
        //    this.radioFootElasticL.EditValue = this._pCFlameRetardant.FootElasticL == null ? 0 : this._pCFlameRetardant.FootElasticL;
        //    this.radioFootElasticR.EditValue = this._pCFlameRetardant.FootElasticR == null ? 0 : this._pCFlameRetardant.FootElasticR;
        //    this.radioHeightFootL.EditValue = this._pCFlameRetardant.HeightFootL == null ? 0 : this._pCFlameRetardant.HeightFootL;
        //    this.radioHeightFootR.EditValue = this._pCFlameRetardant.HeightFootR == null ? 0 : this._pCFlameRetardant.HeightFootR;
        //    this.radioImpactTest.EditValue = this._pCFlameRetardant.ImpactTest == null ? 0 : this._pCFlameRetardant.ImpactTest;
        //    this.radioAceticacidTest.EditValue = this._pCFlameRetardant.AceticacidTest == null ? 0 : this._pCFlameRetardant.AceticacidTest;
        //}

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this._pCFlameRetardant.PCFlameRetardantId = this.txt_Id.Text;
            if (this.date_Check.EditValue != null)
                this._pCFlameRetardant.InvoiceDate = this.date_Check.DateTime;
            this._pCFlameRetardant.EmployeeId = this.ncc_Employee.EditValue == null ? null : (this.ncc_Employee.EditValue as Model.Employee).EmployeeId;
            this._pCFlameRetardant.Note = this.txt_Note.Text;

            switch (this.action)
            {
                case "insert":
                    this._pCFlameRetardantManager.Insert(this._pCFlameRetardant);
                    break;
                case "update":
                    this._pCFlameRetardantManager.Update(this._pCFlameRetardant);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._pCFlameRetardant == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pCFlameRetardantManager.Delete(this._pCFlameRetardant.PCFlameRetardantId);

            this._pCFlameRetardant = this._pCFlameRetardantManager.GetNext(this._pCFlameRetardant);
            if (this._pCFlameRetardant == null)
            {
                this._pCFlameRetardant = this._pCFlameRetardantManager.GetLast();
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._pCFlameRetardant);
        }

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            f.ShowDialog();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    this._pCFlameRetardant = f.SelectItem as Model.PCFlameRetardant;
            //    this.action = "view";
            //    this.Refresh();
            //}
        }

        //选择加工单
        private void btn_PronoteHeader_Click(object sender, EventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.SelectItems != null)
                {
                    foreach (var item in f.SelectItems)
                    {
                        Model.PCFlameRetardantDetail detail = new Book.Model.PCFlameRetardantDetail();
                        detail.PCFlameRetardantDetailId = Guid.NewGuid().ToString();
                        detail.Number = this._pCFlameRetardant.Details.Count + 1;
                        detail.Product = item.Product;
                        detail.ProductId = item.ProductId;
                        detail.EmployeeId = BL.V.ActiveOperator.EmployeeId;
                        detail.InvoiceXOId = item.InvoiceXOId;

                        Model.InvoiceXO xo = invoiceXOManager.Get(detail.InvoiceXOId);
                        if (xo != null)
                            detail.Pihao = xo.CustomerLotNumber;

                        this._pCFlameRetardant.Details.Add(detail);
                    }

                    this.gridControl1.RefreshDataSource();
                }
            }
        }

        //选择采购单
        private void btn_SelectCO_Click(object sender, EventArgs e)
        {
            Invoices.CG.CGForm form = new Book.UI.Invoices.CG.CGForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.key.Count == 0)
                    return;

                foreach (var item in form.key)
                {
                    Model.PCFlameRetardantDetail detail = new Book.Model.PCFlameRetardantDetail();
                    detail.PCFlameRetardantDetailId = Guid.NewGuid().ToString();
                    detail.Number = this._pCFlameRetardant.Details.Count + 1;
                    detail.Product = item.Product;
                    detail.ProductId = item.ProductId;
                    detail.EmployeeId = BL.V.ActiveOperator.EmployeeId;
                    detail.InvoiceXOId = item.Invoice.InvoiceXOId;

                    Model.InvoiceXO xo = invoiceXOManager.Get(detail.InvoiceXOId);
                    if (xo != null)
                        detail.Pihao = xo.CustomerLotNumber;

                    this._pCFlameRetardant.Details.Add(detail);
                }

                this.gridControl1.RefreshDataSource();
            }

            form.Dispose();
            GC.Collect();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCFlameRetardant.PRO_PCFlameRetardantId;
        }

        protected override int AuditState()
        {
            return this._pCFlameRetardant.AuditState.HasValue ? this._pCFlameRetardant.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCFlameRetardant" + "," + this._pCFlameRetardant.PCFlameRetardantId;
        }

        #endregion

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (Invoices.ChooseProductForm.ProductList != null && Invoices.ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in Invoices.ChooseProductForm.ProductList)
                    {
                        Model.PCFlameRetardantDetail detail = new Book.Model.PCFlameRetardantDetail();
                        detail.PCFlameRetardantDetailId = Guid.NewGuid().ToString();
                        detail.Number = this._pCFlameRetardant.Details.Count + 1;
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        detail.EmployeeId = BL.V.ActiveOperator.EmployeeId;
                        this._pCFlameRetardant.Details.Add(detail);
                        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                    }
                }
                else if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    Model.PCFlameRetardantDetail detail = new Book.Model.PCFlameRetardantDetail();
                    detail.PCFlameRetardantDetailId = Guid.NewGuid().ToString();
                    detail.Number = this._pCFlameRetardant.Details.Count + 1;
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    detail.EmployeeId = BL.V.ActiveOperator.EmployeeId;
                    this._pCFlameRetardant.Details.Add(detail);
                    this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);
            }
        }
    }
}