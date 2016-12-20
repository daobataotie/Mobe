using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceStatisticsCheck
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.ProduceStatisticsCheck _ProduceStatisticsCheck = new Book.Model.ProduceStatisticsCheck();
        BL.ProduceStatisticsCheckManager ProduceStatisticsCheckManager = new Book.BL.ProduceStatisticsCheckManager();
        BL.InvoiceXOManager invoiceXoManager = new BL.InvoiceXOManager();

        BL.ProduceStatisticsCheckDetailManager ProduceStatisticsCheckDetailManager = new Book.BL.ProduceStatisticsCheckDetailManager();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProduceStatisticsCheck.PRO_ProduceStatisticsCheckId, new AA(Properties.Resources.RequireDataForId, this.textEditProduceStatisticsCheckId));
            this.newChooseContorlEmployee0Id.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployee1Id.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "insert";
        }
           /// <summary>
        /// 带一个参构造函数
        /// </summary>
        public EditForm(Model.ProduceStatisticsCheck _ProduceStatisticsCheck)
            : this()
        {
            this._ProduceStatisticsCheck = _ProduceStatisticsCheck;
            this._ProduceStatisticsCheck.Details = this.ProduceStatisticsCheckDetailManager.Select(_ProduceStatisticsCheck);
            this.action = "update";
        }
        /// <summary>
        /// 带两个参构造函数
        /// </summary>
        public EditForm(Model.ProduceStatisticsCheck _ProduceStatisticsCheck, string action)
            : this()
        {
            this._ProduceStatisticsCheck = _ProduceStatisticsCheck;
            this._ProduceStatisticsCheck.Details = this.ProduceStatisticsCheckDetailManager.Select(_ProduceStatisticsCheck);
            this.action = action;
        }
        #region Save  新增

        protected override void Save()
        {

            this._ProduceStatisticsCheck.ProduceStatisticsCheckId = this.textEditProduceStatisticsCheckId.Text;
            this._ProduceStatisticsCheck.Description = this.memoEdit1.Text;

            this._ProduceStatisticsCheck.Employee0 = (this.newChooseContorlEmployee0Id.EditValue as Model.Employee);
            if (this._ProduceStatisticsCheck.Employee0 != null)
            {
                this._ProduceStatisticsCheck.Employee0Id = this._ProduceStatisticsCheck.Employee0.EmployeeId;
            }
            this._ProduceStatisticsCheck.Employee1 = (this.newChooseContorlEmployee1Id.EditValue as Model.Employee);
            if (this._ProduceStatisticsCheck.Employee1 != null)
            {
                this._ProduceStatisticsCheck.Employee1Id = this._ProduceStatisticsCheck.Employee1.EmployeeId;
            }
            this._ProduceStatisticsCheck.PronoteHeaderID = this.textEditPronoteHeaderID.Text;

         
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProduceStatisticsCheckDate.DateTime, new DateTime()))
            {
                this._ProduceStatisticsCheck.ProduceStatisticsCheckDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._ProduceStatisticsCheck.ProduceStatisticsCheckDate = this.dateEditProduceStatisticsCheckDate.DateTime;
            }
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.ProduceStatisticsCheckManager.Insert(this._ProduceStatisticsCheck);
                    break;
                case "update":
                    this.ProduceStatisticsCheckManager.Update(this._ProduceStatisticsCheck);
                    break;
                default:
                    break;
            }

        }
        #endregion
        #region  删除
        protected override void Delete()
        {
            if (this._ProduceStatisticsCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {

                this.ProduceStatisticsCheckManager.Delete(this._ProduceStatisticsCheck.ProduceStatisticsCheckId);
                this._ProduceStatisticsCheck = this.ProduceStatisticsCheckManager.GetNext(this._ProduceStatisticsCheck);
                if (this._ProduceStatisticsCheck == null)
                {
                    this._ProduceStatisticsCheck = this.ProduceStatisticsCheckManager.GetLast();
                }
            }
            catch
            {
                throw new Exception("");
            }

            return;
        }

        #endregion
        #region 刷新
        public override void Refresh()
        {
            if (this._ProduceStatisticsCheck == null)
            {
                this._ProduceStatisticsCheck = new Book.Model.ProduceStatisticsCheck();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {

                    this._ProduceStatisticsCheck = this.ProduceStatisticsCheckManager.GetDetails(_ProduceStatisticsCheck.ProduceStatisticsCheckId);

                }

            }
            this.textEditProduceStatisticsCheckId.Text = this._ProduceStatisticsCheck.ProduceStatisticsCheckId;
            if (global::Helper.DateTimeParse.DateTimeEquls(this._ProduceStatisticsCheck.ProduceStatisticsCheckDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProduceStatisticsCheckDate.EditValue = null;
            }
            else
            {
                this.dateEditProduceStatisticsCheckDate.EditValue = this._ProduceStatisticsCheck.ProduceStatisticsCheckDate;
            }
            this.newChooseContorlEmployee0Id.EditValue = this._ProduceStatisticsCheck.Employee0;
            this.newChooseContorlEmployee1Id.EditValue = this._ProduceStatisticsCheck.Employee1;
            this.textEditPronoteHeaderID.Text = this._ProduceStatisticsCheck.PronoteHeaderID;
            if (this._ProduceStatisticsCheck.PronoteHeader != null)
            {
                this.textEditCustomerXoId.Text = invoiceXoManager.Get(this._ProduceStatisticsCheck.PronoteHeader.InvoiceXOId) == null ? "" : invoiceXoManager.Get(this._ProduceStatisticsCheck.PronoteHeader.InvoiceXOId).CustomerInvoiceXOId;
                if (this._ProduceStatisticsCheck.PronoteHeader.Product != null)
                {
                    this.textEditPruductId.Text = this._ProduceStatisticsCheck.PronoteHeader.Product.Id;
                    this.textEditProductName.Text = this._ProduceStatisticsCheck.PronoteHeader.Product.ProductName;
                    this.richTextBox1.Rtf = this._ProduceStatisticsCheck.PronoteHeader.Product.ProductDescription;
                }
            }
            this.memoEdit1.Text = this._ProduceStatisticsCheck.Description;
            this.bindingSourceProduceStatisticsCheckDetail.DataSource = this._ProduceStatisticsCheck.Details;
            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.barButtonItem1.Enabled = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;

                    this.barButtonItem1.Enabled = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.barButtonItem1.Enabled = false;
                    break;
                default:
                    break;
            }

        }
        #endregion
        protected override void MoveNext()
        {
            Model.ProduceStatisticsCheck ProduceStatisticsCheck = this.ProduceStatisticsCheckManager.GetNext(this._ProduceStatisticsCheck);
            if (ProduceStatisticsCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._ProduceStatisticsCheck = this.ProduceStatisticsCheckManager.Get(ProduceStatisticsCheck.ProduceStatisticsCheckId);
        }
        //上一笔
        protected override void MovePrev()
        {
            Model.ProduceStatisticsCheck ProduceStatisticsCheck = this.ProduceStatisticsCheckManager.GetPrev(this._ProduceStatisticsCheck);
            if (ProduceStatisticsCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._ProduceStatisticsCheck = this.ProduceStatisticsCheckManager.Get(ProduceStatisticsCheck.ProduceStatisticsCheckId);
        }

        //首笔
        protected override void MoveFirst()
        {
            this._ProduceStatisticsCheck = this.ProduceStatisticsCheckManager.Get(this.ProduceStatisticsCheckManager.GetFirst() == null ? "" : this.ProduceStatisticsCheckManager.GetFirst().ProduceStatisticsCheckId);
        }

        //尾笔
        protected override void MoveLast()
        {
            // if (ProduceStatisticsCheck == null)
            {
                this._ProduceStatisticsCheck = this.ProduceStatisticsCheckManager.Get(this.ProduceStatisticsCheckManager.GetLast() == null ? "" : this.ProduceStatisticsCheckManager.GetLast().ProduceStatisticsCheckId);
            }
        }
        protected override bool HasRows()
        {
            return this.ProduceStatisticsCheckManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.ProduceStatisticsCheckManager.HasRowsAfter(this._ProduceStatisticsCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this.ProduceStatisticsCheckManager.HasRowsBefore(this._ProduceStatisticsCheck);
        }
        protected override void AddNew()
        {

            this._ProduceStatisticsCheck = new Model.ProduceStatisticsCheck();
            this._ProduceStatisticsCheck.ProduceStatisticsCheckId = this.ProduceStatisticsCheckManager.GetId();
            this._ProduceStatisticsCheck.ProduceStatisticsCheckDate = DateTime.Now;
            this._ProduceStatisticsCheck.Details = new List<Model.ProduceStatisticsCheckDetail>();

           // if (this.action == "insert")
            {

                Model.ProduceStatisticsCheckDetail detail = new Book.Model.ProduceStatisticsCheckDetail();
                detail.ProduceStatisticsCheckDetailId = Guid.NewGuid().ToString();
                detail.DetailDate = DateTime.Now;
                detail.ProduceQuantity = 0;
                detail.APian = 0;
                detail.BPian = 0;
                detail.CPian = 0;
                detail.FractionDefective = 0;
                detail.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                this._ProduceStatisticsCheck.Details.Add(detail);


            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new produceManager.ProduceStatisticsCheck.XR(this._ProduceStatisticsCheck.ProduceStatisticsCheckId);
        }
        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceEmployee.DataSource = new BL.EmployeeManager().SelectOnActive();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new PronoteHeader.ChoosePronoteHeaderDetailsForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (PronoteHeader.ChoosePronoteHeaderDetailsForm._pronoteHeaderList.Count != 0)
                {
                    Model.PronoteHeader pronoteHeader = PronoteHeader.ChoosePronoteHeaderDetailsForm._pronoteHeaderList[0];
                    if (pronoteHeader.Product != null)
                    {
                        this.textEditPruductId.Text = pronoteHeader.Product.Id;
                        this.textEditProductName.Text = pronoteHeader.Product.ProductName;
                        this.richTextBox1.Rtf = pronoteHeader.Product.ProductDescription;
                    }
                    this._ProduceStatisticsCheck.PronoteHeader = pronoteHeader;
                    this.textEditPronoteHeaderID.Text = pronoteHeader.PronoteHeaderID;
                    this.textEditCustomerXoId.Text = invoiceXoManager.Get(pronoteHeader.InvoiceXOId) == null ? "" : invoiceXoManager.Get(pronoteHeader.InvoiceXOId).CustomerInvoiceXOId;

                }
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {

                    Model.ProduceStatisticsCheckDetail detail = new Book.Model.ProduceStatisticsCheckDetail();
                    detail.ProduceStatisticsCheckDetailId = Guid.NewGuid().ToString();
                    detail.DetailDate = DateTime.Now;
                    detail.ProduceQuantity = 0;
                    detail.APian = 0;
                    detail.BPian = 0;
                    detail.CPian = 0;
                    detail.FractionDefective = 0;
                    detail.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                    this._ProduceStatisticsCheck.Details.Add(detail);
                    this.bindingSourceProduceStatisticsCheckDetail.Position = this.bindingSourceProduceStatisticsCheckDetail.IndexOf(detail);

                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceProduceStatisticsCheckDetail.Current != null)
            {
                this._ProduceStatisticsCheck.Details.Remove(this.bindingSourceProduceStatisticsCheckDetail.Current as Book.Model.ProduceStatisticsCheckDetail);

                if (this._ProduceStatisticsCheck.Details.Count == 0)
                {

                    Model.ProduceStatisticsCheckDetail detail = new Book.Model.ProduceStatisticsCheckDetail();
                    detail.ProduceStatisticsCheckDetailId = Guid.NewGuid().ToString();
                    detail.DetailDate = DateTime.Now;
                    detail.ProduceQuantity = 0;
                    detail.APian = 0;
                    detail.BPian = 0;
                    detail.CPian = 0;
                    detail.FractionDefective = 0;
                    detail.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                    this._ProduceStatisticsCheck.Details.Add(detail);
                    this.bindingSourceProduceStatisticsCheckDetail.Position = this.bindingSourceProduceStatisticsCheckDetail.IndexOf(detail);
                    this.gridControl1.RefreshDataSource();
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            float produceQuantity = 0;
            float cPian = 0;
            if (e.Column == this.gridColumnCPian || e.Column == this.gridColumnProduceQuantity)
            {
                if (e.Column == this.gridColumnProduceQuantity)
                {
                    float.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnProduceQuantity).ToString(), out produceQuantity);
                    float.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnCPian).ToString(), out cPian);
                }
                if (e.Column == this.gridColumnCPian)
                {
                    float.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnProduceQuantity).ToString(), out produceQuantity);
                    float.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnCPian).ToString(), out cPian);
                }
                if (produceQuantity > 0)
                {
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnFractionDefective, Math.Round(cPian / produceQuantity, 4));
                }
            }
        }
    }
}