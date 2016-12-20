using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices.CG;
namespace Book.UI.Invoices.CT
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoiceCTManager invoiceManager = new Book.BL.InvoiceCTManager();
        protected BL.InvoiceCTDetailManager invoiceCTDetailManager = new Book.BL.InvoiceCTDetailManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        protected BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        protected Book.Model.InvoiceCT invoice = null;

        public EditForm()
        {
            InitializeComponent();
            this.colInvoiceCTDetailPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoiceCTDetailPrice.DisplayFormat.FormatString = GetFormat(BL.V.SetDataFormat.CGDJXiao.Value);
            this.bindingSourceProduct.DataSource = null;
            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Depot", new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepot));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.buttonEditCompany));
            this.invalidValueExceptions.Add(Model.InvoiceCT.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));
            this.requireValueExceptions.Add(Model.InvoiceCGDetail.PRO_DepotPositionId, new AA(Properties.Resources.DepotInStockQuertyIsNull, this.gridControl1));

            this.action = "view";
            this.buttonEditCompany.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.buttonEditDepot.Choose = new Book.UI.Invoices.ChooseDepot();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.buttonEditEmployee1.Choose = new ChooseEmployee();
            this.buttonEditEmployee2.Choose = new ChooseEmployee();
            this.EmpAudit.Choose = new ChooseEmployee();
        }

        int LastFlag = 0; //页面载入时是否执行 last方法
        public EditForm(string invoiceid)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceid);
            if (this.invoice == null)
                throw new ArithmeticException("invoiceid");
            this.action = "update";
        }

        public EditForm(Model.InvoiceCT invoice)
            : this()
        {
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            this.action = "update";
        }

        private void buttonEditEmployee0_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseEmployeeForm f = new ChooseEmployeeForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {

                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void buttonEditCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseSupplier f = new ChooseSupplier();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void buttonEditDepot_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseDepotForm f = new ChooseDepotForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            string sql = "SELECT productid,id,productname FROM product WHERE IsCustomerProduct IS NULL OR IsCustomerProduct =0";
            this.bindingSourceProduct.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);

            this.bindingSourceDepotPosition.DataSource = this.depotPositionManager.Select();
        }

        protected override string tableCode()
        {
            return "InvoiceCT";
        }

        protected override int AuditState()
        {
            return this.invoice.AuditState.HasValue ? this.invoice.AuditState.Value : 0;
        }

        protected override string getName()
        {
            string formName = this.GetType().FullName;
            formName = formName.Substring(formName.IndexOf('.') + 1).Substring(formName.Substring(formName.IndexOf('.') + 1).IndexOf('.') + 1);
            return formName;
        }

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.InvoiceCTDetail detail = new Book.Model.InvoiceCTDetail();
                detail.Invoice = this.invoice;
                //detail.InvoiceId = this.invoice.InvoiceId;
                detail.InvoiceCTDetailId = Guid.NewGuid().ToString();
                detail.InvoiceCTDetailDisount = decimal.Zero;
                detail.Inumber = this.invoice.Details.Count + 1;
                detail.InvoiceCTDetailDiscountRate = 0;
                detail.InvoiceCTDetailMoney0 = decimal.Zero;
                detail.InvoiceCTDetailMoney1 = decimal.Zero;
                detail.InvoiceCTDetailNote = "";
                detail.InvoiceCTDetailPrice = decimal.Zero;
                detail.InvoiceCTDetailQuantity = 1;
                detail.InvoiceCTDetailTax = decimal.Zero;
                detail.InvoiceCTDetailTaxRate = 0;
                detail.InvoiceCTDetailZS = false;
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                detail.InvoiceProductUnit = detail.Product.MainUnit == null ? "" : detail.Product.MainUnit.CnName;
                this.invoice.Details.Add(detail);
                this.gridControl1.RefreshDataSource();

                this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);


            }
        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSourceDetail.Current as Book.Model.InvoiceCTDetail);
                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoiceCTDetail detail = new Model.InvoiceCTDetail();
                    detail.InvoiceCTDetailId = Guid.NewGuid().ToString();
                    detail.Inumber = this.invoice.Details.Count + 1;
                    detail.InvoiceCTDetailDisount = 0;
                    detail.InvoiceCTDetailDiscountRate = 0;
                    detail.InvoiceCTDetailMoney0 = 0;
                    detail.InvoiceCTDetailMoney1 = 0;
                    detail.InvoiceCTDetailNote = "";
                    detail.InvoiceCTDetailPrice = 0;
                    detail.InvoiceCTDetailQuantity = 0;
                    detail.InvoiceCTDetailTax = 0;
                    detail.InvoiceCTDetailTaxRate = 0;
                    detail.InvoiceCTDetailZS = false;
                    detail.InvoiceProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this.invoice.Details.Add(detail);
                    this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.UpdateMoneyFields();
            }
        }

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            this.invoice.Supplier = this.buttonEditCompany.EditValue as Model.Supplier;
            if (this.buttonEditDepot.EditValue != null)
            {
                this.invoice.Depot = this.buttonEditDepot.EditValue as Model.Depot;
                this.invoice.DepotId = (this.buttonEditDepot.EditValue as Model.Depot).DepotId;
            }
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            this.invoice.InvoiceTax = this.calcEditInvoiceTax1.Value;
            if (string.IsNullOrEmpty(this.spinEditInvoiceTaxRate1.Text))
                this.invoice.InvoiceTaxRate = 0;
            else
                this.invoice.InvoiceTaxRate = double.Parse(this.spinEditInvoiceTaxRate1.Text);
            this.invoice.InvoiceZongJi = this.calcEditInvoiceTotal0.Value;
            this.invoice.InvoiceHeJi = this.calcEditInvoiceTotal1.Value;

            this.invoice.InvoiceZSE = this.calcEditInvoiceZSE.Value;
            this.invoice.InvoiceOwed = this.calcEditInvoiceTotal0.Value;
            this.invoice.InvoiceLRTime = DateTime.Now;

            this.invoice.InvoiceCpbh = this.textEditInvoiceCphm.Text;
            this.invoice.InvoiceKslb = this.comboBoxEditInvoiceKslb.Text;
            this.invoice.InvoiceKlfs = this.comboBoxEditInvoiceKlfs.Text;
            this.invoice.InvoiceKpls = this.comboBoxEditInvoiceFpls.Text;
            this.invoice.InvoiceFpje = this.spinEditInvoiceFpje.Value;
            this.invoice.InvoiceFpbh = this.textEditInvoiceFpbh.Text;
            this.invoice.InvoicePayTimeLimit = this.dateEditInvoicePayTimeLimit.DateTime.Date;
            this.invoice.InvoiceZRE = this.spinEditInvoiceZKE.Value;

            this.invoice.Employee1 = this.buttonEditEmployee1.EditValue as Model.Employee;
            this.invoice.Employee2 = this.buttonEditEmployee2.EditValue as Model.Employee;

            this.invoice.AuditState = this.saveAuditState;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.invoiceManager.Insert(this.invoice);
                    break;

                case "update":
                    this.invoiceManager.Update(this.invoice);
                    break;
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bool isZS = false;
            decimal total1 = decimal.Zero;
            decimal quantity = decimal.Zero;
            decimal price = decimal.Zero;

            if (e.Column == this.colInvoiceCTDetailZS || e.Column == this.colInvoiceCTDetailPrice || e.Column == this.colInvoiceCTDetailQuantity)
            {
                decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCTDetailQuantity).ToString(), out quantity);
                decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCTDetailPrice).ToString(), out price);
                bool.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCTDetailZS).ToString(), out isZS);


                total1 = GetDecimal(price * quantity, BL.V.SetDataFormat.CGJEXiao.Value);
                this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCTDetailMoney1, total1);
                this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCTDetailMoney0, total1);

                if (e.Column == this.colInvoiceCTDetailZS)
                {
                    bool.TryParse(e.Value.ToString(), out isZS);

                    if (isZS)
                    {
                        this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCTDetailDiscount, decimal.Zero);
                        this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCTDetailDiscountRate, decimal.Zero);
                    }
                }
            }
            this.flag = 0;
            this.spinEditInvoiceTaxRate1.Properties.Buttons[1].Enabled = true;
            this.spinEditInvoiceTaxRate1.Properties.Buttons[2].Enabled = true;
            this.UpdateMoneyFields();
        }

        private void UpdateMoneyFields()
        {
            decimal yse = 0;
            decimal zse = 0;

            foreach (Model.InvoiceCTDetail detail in this.invoice.Details)
            {
                if (detail.InvoiceCTDetailZS.Value)
                {
                    zse += detail.InvoiceCTDetailMoney1.Value;
                }
                else
                {
                    yse += detail.InvoiceCTDetailMoney1.Value;
                }
            }
            yse = this.GetDecimal(yse, BL.V.SetDataFormat.CGZJXiao.Value);
            this.calcEditInvoiceZSE.EditValue = zse;
            if (this.action != "view")
            {
                if (flag == 0)
                {
                    this.calcEditInvoiceTotal1.EditValue = yse;
                    this.calcEditInvoiceTax1.EditValue = 0;
                    this.calcEditInvoiceTotal0.EditValue = yse + this.calcEditInvoiceTax1.Value;
                    this.comboBoxEditInvoiceKslb.SelectedIndex = 2;
                }
                else if (flag == 1)
                {
                    this.calcEditInvoiceTotal1.EditValue = yse;
                    this.calcEditInvoiceTax1.EditValue = this.GetDecimal(yse * this.spinEditInvoiceTaxRate1.Value / 100, BL.V.SetDataFormat.CGZJXiao.Value);
                    this.calcEditInvoiceTotal0.EditValue = yse + this.calcEditInvoiceTax1.Value;
                    this.comboBoxEditInvoiceKslb.SelectedIndex = 1;
                }
                else
                {
                    this.calcEditInvoiceTotal0.EditValue = yse;
                    this.calcEditInvoiceTotal1.EditValue = this.GetDecimal(yse * 100 / (100 + this.spinEditInvoiceTaxRate1.Value), BL.V.SetDataFormat.CGZJXiao.Value);
                    this.calcEditInvoiceTax1.EditValue = this.calcEditInvoiceTotal0.Value - this.calcEditInvoiceTotal1.Value;
                    this.comboBoxEditInvoiceKslb.SelectedIndex = 0;
                }
            }
            spinEditInvoiceFpje.EditValue = this.calcEditInvoiceTotal0.EditValue;
        }

        public override BaseListForm GetListForm()
        {
            return new ListForm();
        }

        protected override void Delete()
        {
            this.invoiceManager.Delete(this.invoice.InvoiceId);
        }

        public override Book.Model.Invoice Invoice
        {
            get
            {
                return invoice;
            }
            set
            {
                if (value is Model.InvoiceCT)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceCT).InvoiceId);
                }
            }
        }

        protected override void TurnNull()
        {
            if (this.invoice == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this.invoiceManager.TurnNull(this.invoice.InvoiceId);
            this.invoice = this.invoiceManager.GetNext(this.invoice);
            if (this.invoice == null)
            {
                this.invoice = this.invoiceManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoiceCT();

            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.Details = new List<Model.InvoiceCTDetail>();
            this.invoice.InvoiceTax = 0;
            this.invoice.InvoiceZRE = 0;
            this.invoice.InvoiceZSE = 0;
            this.invoice.InvoiceZongJi = 0;
            this.invoice.InvoiceHeJi = 0;
            this.invoice.InvoiceFpje = 0;
            this.invoice.InvoiceOwed = 0;
            this.invoice.InvoiceYHE = 0;
            this.buttonEditEmployee.EditValue = BL.V.ActiveOperator.Employee;
            this.buttonEditEmployee1.EditValue = BL.V.ActiveOperator.Employee;

            this.invoice.InvoicePayTimeLimit = DateTime.Now.Date;

            Model.InvoiceCTDetail detail = new Model.InvoiceCTDetail();
            detail.InvoiceCTDetailId = Guid.NewGuid().ToString();
            detail.InvoiceCTDetailDisount = 0;
            detail.Inumber = this.invoice.Details.Count + 1;
            detail.InvoiceCTDetailDiscountRate = 0;
            detail.InvoiceCTDetailMoney0 = 0;
            detail.InvoiceCTDetailMoney1 = 0;
            detail.InvoiceCTDetailNote = "";
            detail.InvoiceCTDetailPrice = 0;
            detail.InvoiceCTDetailQuantity = 0;
            detail.InvoiceCTDetailTax = 0;
            detail.InvoiceCTDetailTaxRate = 0;
            detail.InvoiceCTDetailZS = false;
            detail.InvoiceProductUnit = "";
            detail.Product = new Book.Model.Product();
            this.invoice.Details.Add(detail);
            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
        }

        protected override void MoveNext()
        {
            Model.InvoiceCT invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceCT invoice = this.invoiceManager.GetPrev(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MoveFirst()
        {
            this.invoice = this.invoiceManager.Get(this.invoiceManager.GetFirst() == null ? "" : this.invoiceManager.GetFirst().InvoiceId);
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this.invoice = this.invoiceManager.Get(this.invoiceManager.GetLast() == null ? "" : this.invoiceManager.GetLast().InvoiceId);
        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoiceCT();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
                else
                {
                    //this.bindingSourceDepotPosition.DataSource = this.depotPositionManager.Select(this.invoice.Depot);
                }
            }

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.EditValue = this.invoice.InvoiceDate;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.textEditNote.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.buttonEditCompany.EditValue = this.invoice.Supplier;
            this.buttonEditDepot.EditValue = this.invoice.Depot;


            this.spinEditInvoiceTaxRate1.EditValue = this.invoice.InvoiceTaxRate == 0 || this.invoice.InvoiceTaxRate == null ? 5 : this.invoice.InvoiceTaxRate;
            this.calcEditInvoiceTax1.EditValue = this.invoice.InvoiceTax == 0 ? 0 : this.invoice.InvoiceTax;
            this.calcEditInvoiceTotal0.EditValue = this.invoice.InvoiceZongJi == 0 ? 0 : this.invoice.InvoiceZongJi; ;
            this.calcEditInvoiceTotal1.EditValue = this.invoice.InvoiceHeJi == 0 ? 0 : this.invoice.InvoiceHeJi;
            this.calcEditInvoiceZSE.EditValue = this.invoice.InvoiceZSE == 0 ? 0 : this.invoice.InvoiceZSE;
            this.dateEditInvoicePayTimeLimit.DateTime = this.invoice.InvoicePayTimeLimit == null ? DateTime.Now.AddDays(3).Date : this.invoice.InvoicePayTimeLimit.Value.Date;

            this.textEditInvoiceCphm.Text = this.invoice.InvoiceCpbh;
            this.comboBoxEditInvoiceKslb.Text = this.invoice.InvoiceKslb;
            this.comboBoxEditInvoiceKlfs.Text = this.invoice.InvoiceKlfs;
            this.comboBoxEditInvoiceFpls.Text = this.invoice.InvoiceKpls;
            this.spinEditInvoiceFpje.EditValue = this.invoice.InvoiceFpje;
            this.textEditInvoiceFpbh.Text = this.invoice.InvoiceFpbh;

            this.spinEditInvoiceOwed.EditValue = this.invoice.InvoiceOwed;
            this.spinEditInvoiceZKE.EditValue = this.invoice.InvoiceZRE;
            this.dateEditInvoicePayTimeLimit.EditValue = this.invoice.InvoicePayTimeLimit;


            this.buttonEditEmployee1.EditValue = this.invoice.Employee1;
            this.buttonEditEmployee2.EditValue = this.invoice.Employee2;

            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSourceDetail.DataSource = this.invoice.Details;

            base.Refresh();

            this.textEditInvoiceId.Properties.ReadOnly = true;
            //this.buttonEditEmployee.Enabled = false;
            //this.buttonEditEmployee1.Enabled = false;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R01(this.invoice.InvoiceId);
        }

        protected override bool HasRows()
        {
            return this.invoiceManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.invoiceManager.HasRowsAfter(this.invoice);
        }

        protected override bool HasRowsPrev()
        {
            return this.invoiceManager.HasRowsBefore(this.invoice);
        }

        private int flag = 0;

        private void spinEditInvoiceTaxRate1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int index = e.Button.Index;

            switch (index)
            {
                case 1:
                    flag = 1;
                    TaxMethod();
                    break;
                case 2:
                    flag = -1;
                    TaxMethod();
                    break;
                default:
                    this.spinEditInvoiceTaxRate1.Properties.Buttons[1].Enabled = true;
                    this.spinEditInvoiceTaxRate1.Properties.Buttons[2].Enabled = true;
                    break;
            }

            this.gridControl1.RefreshDataSource();
        }

        private void dateEditInvoiceDate_Leave(object sender, EventArgs e)
        {
            if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
            this.dateEditInvoicePayTimeLimit.DateTime = this.dateEditInvoiceDate.DateTime;
        }

        private void spinEditInvoiceTaxRate1_EditValueChanged(object sender, EventArgs e)
        {
            this.UpdateMoneyFields();
        }

        private void TaxMethod()
        {
            string message = "";
            if (flag == 0)
                return;
            if (flag == 1)
                message = Properties.Resources.WaiJiaShui;
            else
                message = Properties.Resources.NeiHanShui;

            if (MessageBox.Show(message, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
                return;
            double taxrate = double.Parse(this.spinEditInvoiceTaxRate1.Text); //蝔?
            double ta = (taxrate + 100) / 100;

            //foreach (Model.InvoiceCTDetail detail in this.invoice.Details)
            //{
            //    if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
            //    if (flag == 1)
            //    {
            //        detail.InvoiceCTDetailPrice = detail.InvoiceCTDetailMoney0 / decimal.Parse(detail.InvoiceCTDetailQuantity.ToString());
            //    }
            //    else
            //    {
            //        detail.InvoiceCTDetailPrice = detail.InvoiceCTDetailMoney0 / decimal.Parse(detail.InvoiceCTDetailQuantity.ToString()) / decimal.Parse(ta.ToString());
            //    }
            //    detail.InvoiceCTDetailMoney1 = decimal.Parse(detail.InvoiceCTDetailQuantity.ToString()) * detail.InvoiceCTDetailPrice;
            //}
            this.spinEditInvoiceTaxRate1.Properties.Buttons[1].Enabled = flag == 1 ? false : true;
            this.spinEditInvoiceTaxRate1.Properties.Buttons[2].Enabled = flag == -1 ? false : true;
            this.UpdateMoneyFields();
        }

        private void spinEditInvoiceZKE_EditValueChanged(object sender, EventArgs e)
        {
            this.spinEditInvoiceOwed.EditValue = this.calcEditInvoiceTotal0.Value - this.spinEditInvoiceZKE.Value - this.spinEditYifu.Value;
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumn1")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceCTDetail).Product;

                    this.repositoryItemComboBox1.Items.Clear();

                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        BL.ProductUnitManager unitManager = new Book.BL.ProductUnitManager();
                        IList<Model.ProductUnit> unitList = unitManager.Select(p.BasedUnitGroupId);
                        foreach (Model.ProductUnit item in unitList)
                        {
                            this.repositoryItemComboBox1.Items.Add(item.CnName);
                        }
                    }
                }

            }
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            this.UpdateMoneyFields();
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this.spinEditInvoiceTaxRate1, this.textEditInvoiceCphm, this.textEditInvoiceFpbh, this.textEditInvoiceId, this });
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colProductId || e.Column == this.colProduct)
            {
                Model.InvoiceCTDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceCTDetail;
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.InvoiceCTDetailId = Guid.NewGuid().ToString();
                    detail.Invoice = this.invoice;
                    detail.InvoiceCTDetailDisount = decimal.Zero;
                    detail.InvoiceCTDetailDiscountRate = 0;
                    detail.InvoiceCTDetailNote = "";
                    detail.InvoiceCTDetailQuantity = 1;
                    detail.InvoiceCTDetailTax = decimal.Zero;
                    detail.InvoiceCTDetailTaxRate = 0;
                    detail.InvoiceCTDetailZS = false;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.InvoiceProductUnit = p.DepotUnit == null ? "" : p.DepotUnit.CnName;
                    detail.InvoiceCTDetailMoney0 = Convert.ToDecimal(detail.InvoiceCTDetailQuantity.Value) * detail.InvoiceCTDetailPrice;
                    detail.InvoiceCTDetailMoney1 = detail.InvoiceCTDetailMoney0;

                    this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private bool CanAdd(IList<Model.InvoiceCTDetail> list)
        {
            foreach (Model.InvoiceCTDetail detail in list)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    return false;
            }
            return true;
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.CanAdd(this.invoice.Details))
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        Model.InvoiceCTDetail detail = new Model.InvoiceCTDetail();
                        detail.InvoiceCTDetailId = Guid.NewGuid().ToString();
                        detail.Inumber = this.invoice.Details.Count + 1;
                        detail.InvoiceCTDetailDisount = 0;
                        detail.InvoiceCTDetailDiscountRate = 0;
                        detail.InvoiceCTDetailMoney0 = 0;
                        detail.InvoiceCTDetailMoney1 = 0;
                        detail.InvoiceCTDetailNote = "";
                        detail.InvoiceCTDetailPrice = 0;
                        detail.InvoiceCTDetailQuantity = 0;
                        detail.InvoiceCTDetailTax = 0;
                        detail.InvoiceCTDetailTaxRate = 0;
                        detail.InvoiceCTDetailZS = false;
                        detail.InvoiceProductUnit = "";
                        detail.Product = new Book.Model.Product();
                        this.invoice.Details.Add(detail);
                        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                    }
                }
                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButtonRemove.PerformClick();
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void buttonEditDepot_EditValueChanged(object sender, EventArgs e)
        {
            Model.Depot depot = this.buttonEditDepot.EditValue as Model.Depot;
            if (depot != null)
            {
                this.bindingSourceDepotPosition.DataSource = this.depotPositionManager.Select(depot);
            }

        }

        private void dateEditInvoiceDate_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
            //this.dateEditInvoicePayTimeLimit.DateTime = this.dateEditInvoiceDate.DateTime;
        }

        private void barButtonItemCO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                CGForm form = new CGForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.key != null && form.key.Count > 0)
                    {
                        if (invoice.Details.Count > 0 && string.IsNullOrEmpty(invoice.Details[0].ProductId))
                            invoice.Details.RemoveAt(0);
                        this.buttonEditCompany.EditValue = form.key[0].Invoice.Supplier;
                        this.buttonEditEmployee.EditValue = form.key[0].Invoice.Employee0;

                        Model.InvoiceCTDetail ctdetail;
                        foreach (Model.InvoiceCODetail detail in form.key)
                        {
                            ctdetail = new Book.Model.InvoiceCTDetail();
                            ctdetail.InvoiceCTDetailId = Guid.NewGuid().ToString();
                            ctdetail.Inumber = invoice.Details.Count + 1;
                            ctdetail.InvoiceCODetail = detail;
                            ctdetail.InvoiceCODetailId = detail.InvoiceCODetailId;
                            ctdetail.Invoice = invoice;
                            ctdetail.InvoiceId = detail.InvoiceId;
                            ctdetail.InvoiceCOId = detail.InvoiceId;
                            ctdetail.InvoiceCTDetailNote = string.Empty;
                            ctdetail.Product = detail.Product;
                            ctdetail.ProductId = detail.ProductId;
                            ctdetail.InvoiceProductUnit = detail.InvoiceProductUnit;
                            ctdetail.InvoiceCTDetailPrice = detail.InvoiceCODetailPrice;
                            ctdetail.InvoiceCTDetailQuantity = 0;
                            ctdetail.InvoiceCTDetailMoney1 = 0;
                            ctdetail.InvoiceCTDetailTax = decimal.Zero;
                            ctdetail.InvoiceCTDetailTaxRate = 0;
                            ctdetail.InvoiceCTDetailDisount = decimal.Zero;
                            ctdetail.Inumber = this.invoice.Details.Count + 1;
                            ctdetail.InvoiceCTDetailDiscountRate = 0;
                            ctdetail.InvoiceCTDetailMoney0 = decimal.Zero;

                            ctdetail.HandbookId = detail.HandbookId;
                            ctdetail.HandbookProductId = detail.HandbookProductId;
                            ctdetail.InvoiceCTDetailZS = false;
                            invoice.Details.Add(ctdetail);
                        }
                        //   this.bindingSourceDetails.DataSource = invoicecg.Details;
                        this.gridControl1.RefreshDataSource();
                    }
                }
                form.Dispose();
                GC.Collect();
            }

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoiceCTDetail> details = this.bindingSourceDetail.DataSource as IList<Model.InvoiceCTDetail>;
            if (details == null || details.Count < 1) return;
            Model.InvoiceCODetail codetail = details[e.ListSourceRowIndex].InvoiceCODetail;
            if (codetail == null) return;
            switch (e.Column.Name)
            {
                //case "colProductId":

                //    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                //    break;
                case "gridColumnCOquantity":
                    e.DisplayText = codetail.OrderQuantity.ToString();
                    break;
                default:
                    break;
            }

        }
    }
}
