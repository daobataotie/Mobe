using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;



namespace Book.UI.produceManager.PCEarPressCheck
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {

        Model.PCEarPressCheck _pcEarPress = null;
        BL.PCEarPressCheckManager _pcEarPressManager = new Book.BL.PCEarPressCheckManager();
        int LastFlag = 0;
        bool IsReport;
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PCEarPressCheck.PRO_PCEarPressCheckDate, new AA(Properties.Resources.DateIsNull, this.Date_PCEarPressCheck));
            this.requireValueExceptions.Add(Model.PCEarPressCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.BEProduct));
            this.requireValueExceptions.Add(Model.PCEarPressCheck.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee0));
            this.action = "view";
            this.nccEmployee0.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.bindingSourceProductUnit.DataSource = new BL.ProductUnitManager().Select();
            this.bindingSourceEmployee.DataSource = new BL.EmployeeManager().SelectIdAndName();
            this.bindingSourceBusinessHours.DataSource = new BL.BusinessHoursManager().SelectIdAndName();
            //DataTable dt = new DataTable();
            //DataColumn dc = new DataColumn("Id", typeof(string));
            //DataColumn dc1 = new DataColumn("Name", typeof(string));
            //dt.Columns.Add(dc);
            //dt.Columns.Add(dc1);
            //DataRow dr = dt.NewRow();
            //dr[0] = "0";
            //dr[1] = "√";
            //dt.Rows.Add(dr);
            //DataRow d = dt.NewRow();
            //dr[0] = "1";
            //dr[1] = "X";
            //dt.Rows.Add(dr);
            //DataRow dr = dt.NewRow();
            //dr[0] = "2";
            //dr[1] = "△";
            //dt.Rows.Add(dr);
            //this.repositoryItemLookUpEdit2.DataSource = dt;

        }

        public EditForm(Model.PCEarPressCheck PCEarPressCheck)
            : this()
        {
            if (PCEarPressCheck == null)
            {
                throw new ArithmeticException("invoiceid");
            }
            this._pcEarPress = PCEarPressCheck;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;

        }

        public EditForm(Model.PCEarPressCheck PCEarPressCheck, string action)
            : this()
        {
            this._pcEarPress = PCEarPressCheck;
            this.action = action;
            if (this.action == action)
                LastFlag = 1;
        }

        public EditForm(string tag)
            : this()
        {
            this.IsReport = true;
        }

        protected override void AddNew()
        {
            this._pcEarPress = new Book.Model.PCEarPressCheck();
            this._pcEarPress.PCEarPressCheckId = this._pcEarPressManager.GetId();
            this._pcEarPressManager.TiGuiExists(_pcEarPress);
            this._pcEarPress.PCEarPressCheckDate = DateTime.Now.Date;
            this._pcEarPress.PCEarPressCheckCount = 1;
            this._pcEarPress.Details = new List<Model.PCEarPressCheckDetail>();
            this.AddDataRows();
        }

        //添加行
        private void AddDataRows()
        {
            Model.PCEarPressCheckDetail earpressDetail = new Book.Model.PCEarPressCheckDetail();
            earpressDetail.PCEarPressCheckDetailId = Guid.NewGuid().ToString();
            earpressDetail.PCEarPressCheckId = _pcEarPress.PCEarPressCheckId;
            earpressDetail.CheckDate = DateTime.Now;
            earpressDetail.CheckTime = DateTime.Now.ToString("HH:mm");
            earpressDetail.CheckTimeSec = DateTime.Now.ToString("HH:mm");
            this._pcEarPress.Details.Add(earpressDetail);
            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(earpressDetail);
        }

        protected override void Delete()
        {
            if (this._pcEarPress == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pcEarPressManager.Delete(this._pcEarPress.PCEarPressCheckId);
            this._pcEarPress = this._pcEarPressManager.GetNext(this._pcEarPress);
            if (this._pcEarPress == null)
            {
                this._pcEarPress = this._pcEarPressManager.GetLast();
            }
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1)
            {
                LastFlag = 0;
                return;
            }
            this._pcEarPress = this._pcEarPressManager.mGetLast(this.IsReport);
        }

        protected override void MoveFirst()
        {
            this._pcEarPress = this._pcEarPressManager.mGetFirst(this.IsReport);
        }

        protected override void MoveNext()
        {
            Model.PCEarPressCheck model = this._pcEarPressManager.mGetNext(this._pcEarPress);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcEarPress = model;

        }

        protected override void MovePrev()
        {
            Model.PCEarPressCheck model = this._pcEarPressManager.mGetPrev(this._pcEarPress);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcEarPress = model;
        }

        protected override bool HasRows()
        {
            return this._pcEarPressManager.mHasRows(this.IsReport);
        }

        protected override bool HasRowsNext()
        {
            return this._pcEarPressManager.mHasRowsAfter(this._pcEarPress);
        }
        protected override bool HasRowsPrev()
        {
            return this._pcEarPressManager.mHasRowsBefore(this._pcEarPress);
        }

        public override void Refresh()
        {
            if (this._pcEarPress == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._pcEarPress = this._pcEarPressManager.Get(this._pcEarPress);
                }
            }
            this.txt_PCEarPressCheckId.Text = this._pcEarPress.PCEarPressCheckId;
            this.txtPronoteHeaderId.Text = this._pcEarPress.PronoteHeaderId;
            this.txtInvoiceCusXOId.Text = this._pcEarPress.InvoiceCusXOId;
            this.ceInvoiceXOCount.EditValue = this._pcEarPress.InvoiceXOQuantity.HasValue ? this._pcEarPress.InvoiceXOQuantity.Value : 0;
            this.calcPCCheckCount.EditValue = this._pcEarPress.PCEarPressCheckCount.HasValue ? this._pcEarPress.PCEarPressCheckCount.Value : 0;
            this.Date_PCEarPressCheck.EditValue = this._pcEarPress.PCEarPressCheckDate;
            this.BEProduct.EditValue = this._pcEarPress.Product;
            this.nccEmployee0.EditValue = this._pcEarPress.Employee;
            this.txtCheckedStadard.Text = this._pcEarPress.PCEarPressCheckStandard;
            //
            this.newChooseContorlAuditEmp.EditValue = this._pcEarPress.Employee;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pcEarPress.AuditState);
            this.lookUpEditUnit.EditValue = this._pcEarPress.ProductUnitId;
            this.richTextBoxNote.Rtf = this._pcEarPress.Note;
            //gridControl1.DataSource = bindingSourceDetail;
            this.bindingSourceDetail.DataSource = this._pcEarPress.Details;
            //repositoryItemLookUpEdit2.DataSource = this._pcEarPress.Employee;



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
            this.txt_PCEarPressCheckId.ReadOnly = true;

        }


        protected override void Save()
        {
            this._pcEarPress.PCEarPressCheckId = txt_PCEarPressCheckId.Text;
            this._pcEarPress.PronoteHeaderId = this.txtPronoteHeaderId.EditValue == null ? null : this.txtPronoteHeaderId.Text; ;
            this._pcEarPress.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._pcEarPress.PCEarPressCheckDate = this.Date_PCEarPressCheck.DateTime;
            this._pcEarPress.PCEarPressCheckStandard = this.txtCheckedStadard.Text;
            this._pcEarPress.InvoiceXOQuantity = this.ceInvoiceXOCount.EditValue != null ? double.Parse(this.ceInvoiceXOCount.EditValue.ToString()) : 0;
            this._pcEarPress.PCEarPressCheckCount = this.calcPCCheckCount.EditValue != null ? int.Parse(this.calcPCCheckCount.EditValue.ToString()) : 0;

            this._pcEarPress.Employee = (this.nccEmployee0.EditValue as Model.Employee);
            if (this._pcEarPress.Employee != null)
            {
                this._pcEarPress.EmployeeId = this._pcEarPress.Employee.EmployeeId;
            }

            this._pcEarPress.Product = (this.BEProduct.EditValue as Model.Product);
            if (this._pcEarPress.Product != null)
            {
                this._pcEarPress.ProductId = this._pcEarPress.Product.ProductId;
            }

            this._pcEarPress.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();
            this._pcEarPress.Note = this.richTextBoxNote.Rtf;
            this._pcEarPress.ISReport = this.IsReport;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this._pcEarPressManager.Insert(this._pcEarPress);
                    break;
                case "update":
                    this._pcEarPressManager.Update(this._pcEarPress);
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._pcEarPress);

        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            this.AddDataRows();
            gridControl1.RefreshDataSource();

        }

        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceEmployee.Current != null)
            {
                this._pcEarPress.Details.Remove(this.bindingSourceDetail.Current as Model.PCEarPressCheckDetail);
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
                    this._pcEarPress.PronoteHeaderId = currentModel.PronoteHeaderID;
                    this._pcEarPress.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._pcEarPress.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._pcEarPress.ProductId = currentModel.ProductId;
                    this._pcEarPress.PCEarPressCheckStandard = currentModel.CustomerCheckStandard;
                    this._pcEarPress.InvoiceXOQuantity = currentModel.InvoiceXODetailQuantity;

                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm(this.IsReport);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCEarPressCheck currentModel = form.SelectItem as Model.PCEarPressCheck;
                if (currentModel != null)
                {
                    this._pcEarPress = currentModel;
                    this._pcEarPress = this._pcEarPressManager.Get(this._pcEarPress);
                    this.Refresh();
                }
            }
            form.Dispose();
            GC.Collect();
        }




        //private void BEProduct1_Click(object sender, EventArgs e)
        //{
        //    Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
        //    if (f.ShowDialog(this)==DialogResult.OK)
        //    {
        //        this.BEProduct1.EditValue = f.SelectedItem;
        //    }
        //}


        //审核
        protected override string AuditKeyId()
        {
            return Model.PCEarPressCheck.PRO_PCEarPressCheckId;

        }

        protected override int AuditState()
        {
            return this._pcEarPress.AuditState.HasValue ? this._pcEarPress.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCEarPressCheck" + "," + this._pcEarPress.PCEarPressCheckId;
        }


        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.BEProduct.EditValue = f.SelectedItem;
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            foreach (Model.Setting SET in new BL.SettingManager().SelectByName("PCEarPressCheckH"))
            {
                this.repositoryItemComboBox1.Properties.Items.Add(SET.SettingCurrentValue);
            }
            foreach (Model.Setting SET in new BL.SettingManager().SelectByName("PCEarPressCheckW"))
            {
                this.repositoryItemComboBox2.Properties.Items.Add(SET.SettingCurrentValue);
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumn6)
            {
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn7, (Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn6)) / 9.8 * 2.20462).ToString("F2"));
            }
            if (e.Column == this.gridColumn11)
            {
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn13, (Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn11)) / 9.8 * 2.20462).ToString("F2"));
            }
            this.gridControl1.RefreshDataSource();
        }

    }
}