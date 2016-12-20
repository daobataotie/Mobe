using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
using System.Xml;

namespace Book.UI.produceManager.PCClarityCheck
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        Model.PCClarityCheck _pcClarity = null;
        BL.PCClarityCheckManager _pcClarityManager = new Book.BL.PCClarityCheckManager();

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PCClarityCheck.PRO_CheckDate, new AA(Properties.Resources.DateIsNull, this.Date_PCClarityCheck));
            this.requireValueExceptions.Add(Model.PCClarityCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.BEProduct));
            this.requireValueExceptions.Add(Model.PCClarityCheck.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee0));
            this.action = "view";
            this.nccEmployee0.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.bindingSourceProductUnit.DataSource = (new BL.ProductUnitManager()).Select();
            this.bindingSourceEmployee.DataSource = new BL.EmployeeManager().SelectIdAndName();
            this.bindingSourceBusinessHours.DataSource = new BL.BusinessHoursManager().SelectIdAndName();
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Id", typeof(string));
            DataColumn dc1 = new DataColumn("Name", typeof(string));
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1);
            DataRow dr;
            dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "√";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "X";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = "△";
            dt.Rows.Add(dr);
            this.repositoryItemLookUpEdit2.DataSource = dt;
        }

        int LastFlag = 0;

        public EditForm(Model.PCClarityCheck PCClarityCheck)
            : this()
        {
            if (PCClarityCheck == null)
                throw new ArithmeticException("invoiceid");
            this._pcClarity = PCClarityCheck;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCClarityCheck PCClarityCheck, string action)
            : this()
        {
            this._pcClarity = PCClarityCheck;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pcClarity = new Book.Model.PCClarityCheck();
            this._pcClarity.PCClarityCheckId = this._pcClarityManager.GetId();
            this._pcClarityManager.TiGuiExists(this._pcClarity);
            this._pcClarity.CheckDate = DateTime.Now.Date;
            this._pcClarity.CheckCount = 6;  //检测数量默认为6
            this._pcClarity.ProductUnitId = "f7f95879-3444-494b-92eb-2aa784c52e8c";
            //this._ansipcic.Employee = BL.V.ActiveOperator.Employee;
            //this._ansipcic.EmployeeId = BL.V.ActiveOperator.EmployeeId;
            //初始化添加一条详细
            this._pcClarity.Details = new List<Model.PCClarityCheckDetail>();
            this.AddDataRows();
        }

        protected override void Delete()
        {
            if (this._pcClarity == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pcClarityManager.Delete(this._pcClarity.PCClarityCheckId);

            this._pcClarity = this._pcClarityManager.GetNext(this._pcClarity);
            if (this._pcClarity == null)
            {
                this._pcClarity = this._pcClarityManager.GetLast();
            }
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._pcClarity = this._pcClarityManager.GetLast();
        }

        protected override void MoveFirst()
        {
            this._pcClarity = this._pcClarityManager.GetFirst();
        }

        protected override void MovePrev()
        {
            Model.PCClarityCheck model = this._pcClarityManager.GetPrev(this._pcClarity);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcClarity = model;
        }

        protected override void MoveNext()
        {
            Model.PCClarityCheck model = this._pcClarityManager.GetNext(this._pcClarity);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcClarity = model;
        }

        protected override bool HasRows()
        {
            return this._pcClarityManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._pcClarityManager.HasRowsAfter(this._pcClarity);
        }

        protected override bool HasRowsPrev()
        {
            return this._pcClarityManager.HasRowsBefore(this._pcClarity);
        }

        public override void Refresh()
        {
            if (this._pcClarity == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._pcClarity = this._pcClarityManager.Get(this._pcClarity);
                }
            }

            //初始化控件
            this.txt_PCClarityCheckId.Text = this._pcClarity.PCClarityCheckId;
            this.txtPronoteHeaderId.Text = this._pcClarity.PronoteHeaderId;
            this.txtInvoiceCusXOId.Text = this._pcClarity.InvoiceCusXOId;
            this.ceInvoiceXOCount.EditValue = this._pcClarity.InvoiceXOQuantity.HasValue ? this._pcClarity.InvoiceXOQuantity.Value : 0;
            this.calcPCCheckCount.EditValue = this._pcClarity.CheckCount.HasValue ? this._pcClarity.CheckCount.Value : 0;
            this.Date_PCClarityCheck.EditValue = this._pcClarity.CheckDate.Value;
            this.BEProduct.EditValue = this._pcClarity.Product;
            this.nccEmployee0.EditValue = this._pcClarity.Employee;
            this.txtCheckedStadard.Text = this._pcClarity.CheckStadard;

            this.newChooseContorlAuditEmp.EditValue = this._pcClarity.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pcClarity.AuditState);
            this.lookUpEditUnit.EditValue = this._pcClarity.ProductUnitId;
            this.richTextBoxNote.Rtf = this._pcClarity.Note;

            this.bindingSourceDetail.DataSource = this._pcClarity.Details;

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

            this.ceInvoiceXOCount.Enabled = false;
            this.txt_PCClarityCheckId.Properties.ReadOnly = true;
        }

        protected override void Save()
        {
            this._pcClarity.PCClarityCheckId = this.txt_PCClarityCheckId.Text;
            this._pcClarity.PronoteHeaderId = this.txtPronoteHeaderId.EditValue == null ? null : this.txtPronoteHeaderId.Text;
            this._pcClarity.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._pcClarity.CheckDate = this.Date_PCClarityCheck.DateTime;
            this._pcClarity.CheckStadard = this.txtCheckedStadard.Text;
            this._pcClarity.InvoiceXOQuantity = this.ceInvoiceXOCount.EditValue != null ? double.Parse(this.ceInvoiceXOCount.EditValue.ToString()) : 0;
            this._pcClarity.CheckCount = this.calcPCCheckCount.EditValue != null ? int.Parse(this.calcPCCheckCount.EditValue.ToString()) : 0;

            this._pcClarity.Employee = (this.nccEmployee0.EditValue as Model.Employee);
            if (this._pcClarity.Employee != null)
            {
                this._pcClarity.EmployeeId = this._pcClarity.Employee.EmployeeId;
            }

            this._pcClarity.Product = (this.BEProduct.EditValue as Model.Product);
            if (this._pcClarity.Product != null)
            {
                this._pcClarity.ProductId = this._pcClarity.Product.ProductId;
            }

            this._pcClarity.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();
            this._pcClarity.Note = this.richTextBoxNote.Rtf;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this._pcClarityManager.Insert(this._pcClarity);
                    break;
                case "update":
                    this._pcClarityManager.Update(this._pcClarity);
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._pcClarity);
        }

        //添加行
        private void AddDataRows()
        {
            Model.PCClarityCheckDetail clarityDetail = new Book.Model.PCClarityCheckDetail();
            clarityDetail.PCClarityCheckDetailId = Guid.NewGuid().ToString();
            clarityDetail.PCClarityCheckId = this._pcClarity.PCClarityCheckId;
            clarityDetail.CheckDate = DateTime.Now;
            this._pcClarity.Details.Add(clarityDetail);

            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(clarityDetail);
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            this.AddDataRows();
            this.gridControl1.RefreshDataSource();
        }

        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceEmployee.Current != null)
            {
                this._pcClarity.Details.Remove(this.bindingSourceDetail.Current as Model.PCClarityCheckDetail);
                this.gridControl1.RefreshDataSource();
            }
        }

        //选择加工单据
        private void btnGetPronoteHeader_Click(object sender, EventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm pronoForm = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(null, 0);
            if (pronoForm.ShowDialog(this) == DialogResult.OK)
            {
                Model.PronoteHeader currentModel = pronoForm.SelectItem;
                if (currentModel != null)
                {
                    this._pcClarity.PronoteHeaderId = currentModel.PronoteHeaderID;
                    this._pcClarity.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._pcClarity.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._pcClarity.ProductId = currentModel.ProductId;
                    this._pcClarity.CheckStadard = currentModel.CustomerCheckStandard;
                    this._pcClarity.InvoiceXOQuantity = currentModel.InvoiceXODetailQuantity;

                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        //搜索
        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCClarityCheck currentModel = form.SelectItem as Model.PCClarityCheck;
                if (currentModel != null)
                {
                    this._pcClarity = currentModel;
                    this._pcClarity = this._pcClarityManager.Get(this._pcClarity);
                    this.Refresh();
                }
            }
            form.Dispose();
            GC.Collect();
        }

        private void BEProduct_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.BEProduct.EditValue = f.SelectedItem;
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCClarityCheck.PRO_PCClarityCheckId;
        }

        protected override int AuditState()
        {
            return this._pcClarity.AuditState.HasValue ? this._pcClarity.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCClarityCheck" + "," + this._pcClarity.PCClarityCheckId;
        }

        #endregion
    }
}
