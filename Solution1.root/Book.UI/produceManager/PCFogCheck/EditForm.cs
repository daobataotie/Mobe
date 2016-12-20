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

namespace Book.UI.produceManager.PCFogCheck
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        Model.PCFogCheck _pcfog = null;

        BL.PCFogCheckManager _pcfogManager = new Book.BL.PCFogCheckManager();


        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PCFogCheck.PRO_PCFogCheckId, new AA(Properties.Resources.NumsIsNotNull, this.txtPCFogCheckId));
            this.requireValueExceptions.Add(Model.PCFogCheck.PRO_PCFogCheckDate, new AA(Properties.Resources.DateIsNull, this.DE_PCFogCheckDate));
            this.requireValueExceptions.Add(Model.PCFogCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.BEProduct));
            this.requireValueExceptions.Add(Model.PCFogCheck.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee0));

            this.action = "view";
            this.nccEmployee0.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();
            this.bindingSourceBusinessHours.DataSource = new BL.BusinessHoursManager().SelectIdAndName();
            this.bindingSourceMethod.DataSource = new BL.SettingManager().SelectByName("Method");


        }

        int LastFlag = 0;
        public EditForm(string invoiceId)
            : this()
        {
            this._pcfog = this._pcfogManager.Get(invoiceId);
            if (this._pcfog == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCFogCheck mPCFogCheck)
            : this()
        {
            if (mPCFogCheck == null)
                throw new ArithmeticException("invoiceid");
            this._pcfog = mPCFogCheck;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCFogCheck mPCFogCheck, string action)
            : this()
        {
            this._pcfog = mPCFogCheck;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pcfog = new Book.Model.PCFogCheck();
            this._pcfog.PCFogCheckId = this._pcfogManager.GetId();
            this._pcfog.PCFogCheckDate = DateTime.Now.Date;
            this._pcfog.PCFogCheckQuantity = 6;     //默认检测数量为1
            this._pcfog.ProductUnitId = "f7f95879-3444-494b-92eb-2aa784c52e8c";  //默认单位PCS

            this._pcfog.Details = new List<Model.PCFogCheckDetail>();
            Model.PCFogCheckDetail pcfcd = new Book.Model.PCFogCheckDetail();
            pcfcd.PCImpactCheckDetailId = Guid.NewGuid().ToString();
            pcfcd.PCFogCheckId = this._pcfog.PCFogCheckId;
            pcfcd.CommentLDate = DateTime.Now;
            pcfcd.CommentLTime = DateTime.Now;
            pcfcd.CommentRDate = DateTime.Now;
            pcfcd.CommentRTime = DateTime.Now;
            pcfcd.CheckStandard = this.repositoryItemComboBox1.Items[0].ToString();
            this._pcfog.Details.Add(pcfcd);
        }

        public override void Refresh()
        {
            if (this._pcfog == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._pcfog = this._pcfogManager.GetDetail(this._pcfog.PCFogCheckId);
                }
            }

            //初始化控件
            this.txtPCFogCheckId.Text = this._pcfog.PCFogCheckId;
            this.txtPronoteHeaderId.Text = this._pcfog.PronoteHeaderId;
            this.txtInvoiceCusXOId.Text = this._pcfog.InvoiceCusXOId;
            this.txtPCFogCheckDesc.Text = this._pcfog.PCFogCheckDesc;
            this.ceInvoiceXOCount.Text = (this._pcfog.InvoiceXOQuantity.HasValue ? this._pcfog.InvoiceXOQuantity.Value : 0).ToString();
            this.calcPCFogCheckQuantity.EditValue = this._pcfog.PCFogCheckQuantity.HasValue ? this._pcfog.PCFogCheckQuantity.Value : 0;
            this.DE_PCFogCheckDate.EditValue = this._pcfog.PCFogCheckDate.Value;
            this.BEProduct.EditValue = this._pcfog.Product;
            this.nccEmployee0.EditValue = this._pcfog.Employee;
            this.txtCheckedStandard.Text = this._pcfog.mCheckStandard;

            this.chkEditWDCS.Checked = this._pcfog.IsFogPassing.HasValue ? this._pcfog.IsFogPassing.Value : false;
            this.newChooseContorlAuditEmp.EditValue = this._pcfog.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pcfog.AuditState);
            this.lookUpEditUnit.EditValue = this._pcfog.ProductUnitId;

            this.bindingSource1.DataSource = this._pcfog.Details;

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

            this.txtPCFogCheckId.Properties.ReadOnly = true;
            this.ceInvoiceXOCount.Enabled = false;
            this.txtPCFogCheckId.Enabled = false;
        }

        protected override void Delete()
        {
            if (this._pcfog == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pcfogManager.Delete(this._pcfog);

            this._pcfog = this._pcfogManager.GetNext(this._pcfog);

            if (this._pcfog == null)
            {
                this._pcfog = this._pcfogManager.GetLast();
            }
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._pcfog = this._pcfogManager.GetLast();
        }

        protected override void MoveFirst()
        {
            this._pcfog = this._pcfogManager.GetFirst();
        }

        protected override void MovePrev()
        {
            Model.PCFogCheck mPCFog = this._pcfogManager.GetPrev(this._pcfog);
            if (mPCFog == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._pcfog = this._pcfogManager.Get(mPCFog.PCFogCheckId);
        }

        protected override void MoveNext()
        {
            Model.PCFogCheck mPCFog = this._pcfogManager.GetNext(this._pcfog);
            if (mPCFog == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcfog = this._pcfogManager.Get(mPCFog.PCFogCheckId);
        }

        protected override bool HasRows()
        {
            return this._pcfogManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._pcfogManager.HasRowsAfter(this._pcfog);
        }

        protected override bool HasRowsPrev()
        {
            return this._pcfogManager.HasRowsBefore(this._pcfog);
        }

        protected override void Save()
        {
            this._pcfog.PCFogCheckId = this.txtPCFogCheckId.Text;
            this._pcfog.PCFogCheckDesc = this.txtPCFogCheckDesc.Text;
            this._pcfog.PronoteHeaderId = this.txtPronoteHeaderId.Text;
            this._pcfog.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._pcfog.PCFogCheckDate = this.DE_PCFogCheckDate.DateTime;
            this._pcfog.mCheckStandard = this.txtCheckedStandard.Text;
            this._pcfog.InvoiceXOQuantity = double.Parse(this.ceInvoiceXOCount.EditValue != null ? this.ceInvoiceXOCount.EditValue.ToString() : "0");
            this._pcfog.PCFogCheckQuantity = double.Parse(this.calcPCFogCheckQuantity.EditValue != null ? this.calcPCFogCheckQuantity.EditValue.ToString() : "0");

            this._pcfog.IsFogPassing = this.chkEditWDCS.Checked;

            this._pcfog.Employee = (this.nccEmployee0.EditValue as Model.Employee);
            if (this._pcfog.Employee != null)
            {
                this._pcfog.EmployeeId = this._pcfog.Employee.EmployeeId;
            }

            this._pcfog.Product = (this.BEProduct.EditValue as Model.Product);
            if (this._pcfog.Product != null)
            {
                this._pcfog.ProductId = this._pcfog.Product.ProductId;
            }

            this._pcfog.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this._pcfogManager.Insert(this._pcfog);
                    break;
                case "update":
                    this._pcfogManager.Update(this._pcfog);
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._pcfog);
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
                    this._pcfog.PronoteHeaderId = currentModel.PronoteHeaderID;
                    this._pcfog.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._pcfog.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._pcfog.ProductId = currentModel.ProductId;
                    this._pcfog.mCheckStandard = currentModel.CustomerCheckStandard;
                    this._pcfog.InvoiceXOQuantity = currentModel.InvoiceXODetailQuantity;

                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        //选择测试单据
        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCFogCheck currentModel = f.SelectItem as Model.PCFogCheck;
                if (currentModel != null)
                {
                    this._pcfog = currentModel;
                    this._pcfog = this._pcfogManager.GetDetail(this._pcfog.PCFogCheckId);
                    this.Refresh();
                }
            }
            f.Dispose();
            GC.Collect();
        }

        //选择备注
        private void txtANSIPCImpactCheckDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.txtPCFogCheckDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCFogCheck.PRO_PCFogCheckId;
        }

        protected override int AuditState()
        {
            return this._pcfog.AuditState.HasValue ? this._pcfog.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCFogCheck" + "," + this._pcfog.PCFogCheckId;
        }

        #endregion

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Model.PCFogCheckDetail pcfcd = new Book.Model.PCFogCheckDetail();
            pcfcd.PCImpactCheckDetailId = Guid.NewGuid().ToString();
            pcfcd.PCFogCheckId = this._pcfog.PCFogCheckId;
            pcfcd.CommentLDate = DateTime.Now;
            pcfcd.CommentLTime = DateTime.Now;
            pcfcd.CommentRDate = DateTime.Now;
            pcfcd.CommentRTime = DateTime.Now;
            this._pcfog.Details.Add(pcfcd);

            this.gridControl1.RefreshDataSource();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Count > 1)
                this._pcfog.Details.Remove(this.bindingSource1.Current as Model.PCFogCheckDetail);
            this.gridControl1.RefreshDataSource();
        }

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCFogCheck currentModel = form.SelectItem as Model.PCFogCheck;
                if (currentModel != null)
                {
                    this._pcfog = currentModel;
                    this._pcfog = this._pcfogManager.GetDetail(this._pcfog.PCFogCheckId);
                    this.Refresh();
                }
            }
            form.Dispose();
            GC.Collect();
        }

        private void BEProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.BEProduct.EditValue = f.SelectedItem;
            }
        }
    }
}
