using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceStatistics
{
    public partial class EditForm1 : Settings.BasicData.BaseEditForm
    {
        Model.ProduceStatistics produceStatistics = new Book.Model.ProduceStatistics();
        BL.ProduceStatisticsManager produceStatisticsManager = new Book.BL.ProduceStatisticsManager();
        BL.InvoiceXOManager invoiceXoManager = new BL.InvoiceXOManager();

        BL.ProduceStatisticsDetailManager produceStatisticsDetailManager = new Book.BL.ProduceStatisticsDetailManager();

        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        public EditForm1()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProduceStatistics.PRO_ProduceStatisticsId, new AA(Properties.Resources.RequireDataForId, this.textEditProduceStatisticsId));
            this.newChooseContorlWorkHouseId.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.newChooseContorlEmployeeId.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "insert";
        }
        /// <summary>
        /// 带一个参构造函数
        /// </summary>
        public EditForm1(Model.ProduceStatistics produceStatistics)
            : this()
        {
            this.produceStatistics = produceStatistics;
            this.produceStatistics.Details = this.produceStatisticsDetailManager.Select(produceStatistics);
            this.action = "update";
        }
        /// <summary>
        /// 带两个参构造函数
        /// </summary>
        public EditForm1(Model.ProduceStatistics produceStatistics, string action)
            : this()
        {
            this.produceStatistics = produceStatistics;
            this.produceStatistics.Details = this.produceStatisticsDetailManager.Select(produceStatistics);
            this.action = action;
        }

        #region Save  新增

        protected override void Save()
        {

            this.produceStatistics.ProduceStatisticsId = this.textEditProduceStatisticsId.Text;
            this.produceStatistics.Description = this.memoEditDescription.Text;

            this.produceStatistics.Employee = (this.newChooseContorlEmployeeId.EditValue as Model.Employee);
            if (this.produceStatistics.Employee != null)
            {
                this.produceStatistics.EmployeeId = this.produceStatistics.Employee.EmployeeId;
            }
            this.produceStatistics.WorkHouse = (this.newChooseContorlWorkHouseId.EditValue as Model.WorkHouse);
            if (this.produceStatistics.WorkHouse != null)
            {
                this.produceStatistics.WorkHouseId = this.produceStatistics.WorkHouse.WorkHouseId;
            }
            this.produceStatistics.PronoteHeaderID = this.textEditPronoteHeaderID.Text;

            if (this.produceStatistics.Procedures != null)
            {
                this.produceStatistics.ProceduresId = this.produceStatistics.Procedures.ProceduresId;

            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProduceStatisticsDate.DateTime, new DateTime()))
            {
                this.produceStatistics.ProduceStatisticsDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.produceStatistics.ProduceStatisticsDate = this.dateEditProduceStatisticsDate.DateTime;
            }
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.produceStatisticsManager.Insert(this.produceStatistics);
                    break;
                case "update":
                    this.produceStatisticsManager.Update(this.produceStatistics);
                    break;
                default:
                    break;
            }

        }
        #endregion
        #region  删除
        protected override void Delete()
        {
            if (this.produceStatistics == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {

                this.produceStatisticsManager.Delete(this.produceStatistics.ProduceStatisticsId);
                this.produceStatistics = this.produceStatisticsManager.GetNext(this.produceStatistics);
                if (this.produceStatistics == null)
                {
                    this.produceStatistics = this.produceStatisticsManager.GetLast();
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
            if (this.produceStatistics == null)
            {
                this.produceStatistics = new Book.Model.ProduceStatistics();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {

                    this.produceStatistics = this.produceStatisticsManager.GetDetails(produceStatistics.ProduceStatisticsId);

                }

            }
            this.textEditProduceStatisticsId.Text = this.produceStatistics.ProduceStatisticsId;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.produceStatistics.ProduceStatisticsDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProduceStatisticsDate.EditValue = null;
            }
            else
            {
                this.dateEditProduceStatisticsDate.EditValue = this.produceStatistics.ProduceStatisticsDate;
            }
            this.newChooseContorlEmployeeId.EditValue = this.produceStatistics.Employee;
            this.newChooseContorlWorkHouseId.EditValue = this.produceStatistics.WorkHouse;
            this.textEditPronoteHeaderID.Text = this.produceStatistics.PronoteHeaderID;
            if (this.produceStatistics.PronoteHeader != null)
            {
                this.textEditCustomerXOId.Text = invoiceXoManager.Get(this.produceStatistics.PronoteHeader.InvoiceXOId) == null ? "" : invoiceXoManager.Get(this.produceStatistics.PronoteHeader.InvoiceXOId).CustomerInvoiceXOId;
                if (this.produceStatistics.PronoteHeader.Product != null)
                {
                    this.textEditPruductId.Text = this.produceStatistics.PronoteHeader.Product.Id;
                    this.textEditProductName.Text = this.produceStatistics.PronoteHeader.Product.ProductName;
                    this.richTextBox1.Rtf = this.produceStatistics.PronoteHeader.Product.ProductDescription;
                }
            }
            this.buttonEditProduceresId.EditValue = this.produceStatistics.Procedures == null ? null : this.produceStatistics.Procedures.Id;
            this.richTextBoxProduceresName.Rtf = this.produceStatistics.Procedures == null ? "" : this.produceStatistics.Procedures.Procedurename;
            this.memoEditDescription.Text = this.produceStatistics.Description;
            this.bindingSourceDetails.DataSource = this.produceStatistics.Details;
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
            Model.ProduceStatistics produceStatistics = this.produceStatisticsManager.GetNext(this.produceStatistics);
            if (produceStatistics == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.produceStatistics = this.produceStatisticsManager.Get(produceStatistics.ProduceStatisticsId);
        }
        //上一笔
        protected override void MovePrev()
        {
            Model.ProduceStatistics produceStatistics = this.produceStatisticsManager.GetPrev(this.produceStatistics);
            if (produceStatistics == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.produceStatistics = this.produceStatisticsManager.Get(produceStatistics.ProduceStatisticsId);
        }

        //首笔
        protected override void MoveFirst()
        {
            this.produceStatistics = this.produceStatisticsManager.Get(this.produceStatisticsManager.GetFirst() == null ? "" : this.produceStatisticsManager.GetFirst().ProduceStatisticsId);
        }

        //尾笔
        protected override void MoveLast()
        {
            // if (produceStatistics == null)
            {
                this.produceStatistics = this.produceStatisticsManager.Get(this.produceStatisticsManager.GetLast() == null ? "" : this.produceStatisticsManager.GetLast().ProduceStatisticsId);
            }
        }
        protected override bool HasRows()
        {
            return this.produceStatisticsManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.produceStatisticsManager.HasRowsAfter(this.produceStatistics);
        }

        protected override bool HasRowsPrev()
        {
            return this.produceStatisticsManager.HasRowsBefore(this.produceStatistics);
        }
        protected override void AddNew()
        {

            this.produceStatistics = new Model.ProduceStatistics();
            this.produceStatistics.ProduceStatisticsId = this.produceStatisticsManager.GetId();
            this.produceStatistics.ProduceStatisticsDate = DateTime.Now;
            this.produceStatistics.Details = new List<Model.ProduceStatisticsDetail>();

            if (this.action == "insert")
            {

                Model.ProduceStatisticsDetail detail = new Book.Model.ProduceStatisticsDetail();
                detail.ProduceStatisticsDetailId = Guid.NewGuid().ToString();
                detail.DetailDate = DateTime.Now;
                detail.ProduceQuantity = 0;
                detail.HeGeQuantity = 0;
                detail.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                this.produceStatistics.Details.Add(detail);


            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new produceManager.ProduceStatistics.XR(this.produceStatistics.ProduceStatisticsId);
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
                    this.produceStatistics.PronoteHeader = pronoteHeader;
                    this.textEditPronoteHeaderID.Text = pronoteHeader.PronoteHeaderID;
                    this.textEditCustomerXOId.Text = invoiceXoManager.Get(pronoteHeader.InvoiceXOId) == null ? "" : invoiceXoManager.Get(pronoteHeader.InvoiceXOId).CustomerInvoiceXOId;
                }
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {

                    Model.ProduceStatisticsDetail detail = new Book.Model.ProduceStatisticsDetail();
                    detail.ProduceStatisticsDetailId = Guid.NewGuid().ToString();
                    detail.DetailDate = DateTime.Now;
                    detail.ProduceQuantity = 0;
                    detail.HeGeQuantity = 0;
                    detail.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                    this.produceStatistics.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);

                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this.produceStatistics.Details.Remove(this.bindingSourceDetails.Current as Book.Model.ProduceStatisticsDetail);

                if (this.produceStatistics.Details.Count == 0)
                {

                    Model.ProduceStatisticsDetail detail = new Book.Model.ProduceStatisticsDetail();
                    detail.ProduceStatisticsDetailId = Guid.NewGuid().ToString();
                    detail.DetailDate = DateTime.Now;
                    detail.ProduceQuantity = 0;
                    detail.HeGeQuantity = 0;
                    detail.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                    this.produceStatistics.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                    this.gridControl1.RefreshDataSource();
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void buttonEditProduceresId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Settings.ProduceManager.Techonlogy.ChooseProceduresForm f = new Settings.ProduceManager.Techonlogy.ChooseProceduresForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.produceStatistics.Procedures = f.SelectedItem as Model.Procedures;
                if (this.produceStatistics.Procedures != null)
                {
                    this.buttonEditProduceresId.EditValue = this.produceStatistics.Procedures.Id;
                    this.richTextBoxProduceresName.Rtf = this.produceStatistics.Procedures.Procedurename;
                }
            }
            f.Dispose();
            System.GC.Collect();
        }

        private void EditForm1_Load(object sender, EventArgs e)
        {
            this.bindingSourceEmployee.DataSource = new BL.EmployeeManager().SelectOnActive();
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            float produceQuantity = 0;
            float heGeQuantity = 0;
            float bLV = 0;
            if (e.Column == this.gridColumnProduceQuantity || e.Column == this.gridColumnHeGeQuantity)
            {
                if (e.Column == this.gridColumnProduceQuantity)
                {
                    float.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnProduceQuantity).ToString(), out produceQuantity);
                    float.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnHeGeQuantity).ToString(), out heGeQuantity);
                }
                if (e.Column == this.gridColumnHeGeQuantity)
                {
                    float.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnProduceQuantity).ToString(), out produceQuantity);
                    float.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnHeGeQuantity).ToString(), out heGeQuantity);
                }
                bLV = produceQuantity - heGeQuantity < 0 ? 0 : produceQuantity - heGeQuantity;
                if (produceQuantity > 0)
                {
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnRejectionRate, Math.Round(bLV / produceQuantity, 4));
                }
            }
        }

        private void memoEditDescription_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.memoEditDescription.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }
    }
}