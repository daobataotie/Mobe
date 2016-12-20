using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;

namespace Book.UI.Invoices.XT
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoiceXTManager invoiceManager = new Book.BL.InvoiceXTManager();
        protected BL.InvoiceXTDetailManager invoiceXTDetailManager = new Book.BL.InvoiceXTDetailManager();
        protected BL.CustomerProductsManager customerProductsManager = new Book.BL.CustomerProductsManager();
        protected BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        protected BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        protected BL.InvoiceXODetailManager invoicexoDetailManager = new Book.BL.InvoiceXODetailManager();

        //被修改的单据
        protected Book.Model.InvoiceXT invoice = null;

        public EditForm()
        {
            InitializeComponent();
            this.colInvoiceXTDetailPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoiceXTDetailPrice.DisplayFormat.FormatString = this.GetFormat(BL.V.SetDataFormat.XSDJXiao.Value);

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Depot", new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepot));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.buttonEditCompany));

            this.invalidValueExceptions.Add(Model.InvoiceXT.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));
            this.requireValueExceptions.Add(Model.InvoiceXSDetail.PRO_DepotPositionId, new AA(Properties.Resources.RequireChoosePosition, this.gridControl1));

            this.action = "view";

            this.buttonEditEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.buttonEditEmployee1.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.buttonEditEmployee2.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.buttonEditCompany.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.buttonEditDepot.Choose = new ChooseDepot();
            this.EmpAudit.Choose = new ChooseEmployee();
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

        public EditForm(string invoiceid)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceid);
            if (this.invoice == null)
                throw new ArgumentNullException();
            this.action = "view";
        }

        public EditForm(Model.InvoiceXT initInvoicexs)
            : this()
        {
            if (initInvoicexs == null)
                throw new ArgumentNullException();
            this.invoice = initInvoicexs;
            this.action = "view";
        }

        private void buttonEditEmployee0_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Settings.BasicData.Employees.ChooseEmployeeForm f = new Settings.BasicData.Employees.ChooseEmployeeForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void buttonEditCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseCustoms f = new ChooseCustoms();
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
            // this.bindingSource2.DataSource = this.productManager.Select();
        }

        protected override string tableCode()
        {
            return "InvoiceXT";
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
                if (this.invoice.Details.Count > 0 && string.IsNullOrEmpty(this.invoice.Details[0].ProductId))
                    this.invoice.Details.RemoveAt(0);
                Model.InvoiceXTDetail detail = null;
                if (Book.UI.Invoices.ChooseProductForm.ProductList != null || Book.UI.Invoices.ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in Book.UI.Invoices.ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.InvoiceXTDetail();
                        detail.InvoiceXTDetailId = Guid.NewGuid().ToString();
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        detail.Invoice = this.invoice;
                        detail.Inumber = this.invoice.Details.Count + 1;
                        //detail.InvoiceId = this.invoice.InvoiceId;
                        detail.InvoiceXTDetailDiscount = decimal.Zero;
                        detail.InvoiceXTDetailDiscountRate = 0;
                        detail.InvoiceXTDetailMoney0 = decimal.Zero;
                        detail.InvoiceXTDetailMoney1 = decimal.Zero;
                        detail.InvoiceXTDetailNote = "";
                        detail.InvoiceXTDetailPrice = decimal.Zero;
                        detail.InvoiceXTDetailQuantity = 1;
                        detail.InvoiceXTDetailTax = decimal.Zero;
                        detail.InvoiceXTDetailTaxRate = 0;
                        detail.InvoiceXTDetailZS = false;
                        detail.InvoiceProductUnit = detail.Product.SellUnit == null ? null : detail.Product.SellUnit.CnName;
                        this.invoice.Details.Add(detail);
                    }
                }

                if (Book.UI.Invoices.ChooseProductForm.ProductList == null || Book.UI.Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.InvoiceXTDetail();
                    detail.InvoiceXTDetailId = Guid.NewGuid().ToString();
                    Model.Product product = f.SelectedItem as Model.Product;
                    detail.Product = product;
                    detail.ProductId = product.ProductId;
                    detail.Invoice = this.invoice;
                    detail.Inumber = this.invoice.Details.Count + 1;
                    //detail.InvoiceId = this.invoice.InvoiceId;
                    detail.InvoiceXTDetailDiscount = decimal.Zero;
                    detail.InvoiceXTDetailDiscountRate = 0;
                    detail.InvoiceXTDetailMoney0 = decimal.Zero;
                    detail.InvoiceXTDetailMoney1 = decimal.Zero;
                    detail.InvoiceXTDetailNote = "";
                    detail.InvoiceXTDetailPrice = decimal.Zero;
                    detail.InvoiceXTDetailQuantity = 1;
                    detail.InvoiceXTDetailTax = decimal.Zero;
                    detail.InvoiceXTDetailTaxRate = 0;
                    detail.InvoiceXTDetailZS = false;
                    detail.InvoiceProductUnit = detail.Product.SellUnit == null ? null : detail.Product.SellUnit.CnName;
                    this.invoice.Details.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                f.Dispose();
                System.GC.Collect();
            }

        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSource1.Current as Book.Model.InvoiceXTDetail);
                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoiceXTDetail detail = new Model.InvoiceXTDetail();
                    detail.InvoiceXTDetailId = Guid.NewGuid().ToString();
                    detail.Inumber = this.invoice.Details.Count + 1;
                    detail.InvoiceXTDetailDiscount = 0;
                    detail.InvoiceXTDetailDiscountRate = 0;
                    detail.InvoiceXTDetailMoney0 = 0;
                    detail.InvoiceXTDetailMoney1 = 0;
                    detail.InvoiceXTDetailNote = "";
                    detail.InvoiceXTDetailPrice = 0;
                    detail.InvoiceXTDetailQuantity = 0;
                    detail.InvoiceXTDetailTax = 0;
                    detail.InvoiceXTDetailTaxRate = 0;
                    detail.InvoiceXTDetailZS = false;
                    detail.InvoiceProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    //detail.PrimaryKey = new Book.Model.CustomerProducts();
                    this.invoice.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
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

            if (this.buttonEditDepot.EditValue != null)
            {
                this.invoice.Depot = this.buttonEditDepot.EditValue as Model.Depot;
                this.invoice.DepotId = this.invoice.Depot.DepotId;
            }
            if (this.buttonEditCompany.EditValue != null)
            {
                this.invoice.Customer = this.buttonEditCompany.EditValue as Model.Customer;
                this.invoice.CustomerId = this.invoice.Customer.CustomerId;
            }
            //this.invoice.Company = this.buttonEditCompany.EditValue as Model.Company;
            //this.invoice.InvoiceAbstract = this.textEditNote.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            this.invoice.InvoiceTax = this.calcEditInvoiceTax1.Value;
            this.invoice.InvoiceTaxRate = double.Parse(this.spinEditInvoiceTaxRate1.Text);
            this.invoice.InvoiceZongJi = this.calcEditInvoiceTotal0.Value;
            this.invoice.InvoiceHeJi = this.calcEditInvoiceTotal1.Value;
            //this.invoice.InvoiceYHE = this.calcEditInvoiceYHE.Value;
            //this.invoice.InvoiceZKE = this.calcEditInvoiceZKE.Value;
            this.invoice.InvoiceZSE = this.calcEditInvoiceZSE.Value;

            this.invoice.InvoiceOwed = this.spinEditInvoiceOwed.Value;
            //this.invoice.Employee1 = BL.V.ActiveEmployee;
            this.invoice.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveEmployee;

            this.invoice.InvoiceCpbh = this.textEditInvoiceCphm.Text;
            this.invoice.InvoiceKslb = this.comboBoxEditInvoiceKslb.Text;
            this.invoice.InvoiceKlfs = this.comboBoxEditInvoiceKlfs.Text;
            this.invoice.InvoiceKpls = this.comboBoxEditInvoiceFpls.Text;
            this.invoice.InvoiceFpje = this.spinEditInvoiceFpje.Value;
            this.invoice.InvoiceFpbh = this.textEditInvoiceFpbh.Text;
            this.invoice.InvoicePayTimeLimit = this.dateEditInvoicePayTimeLimit.DateTime.Date;
            this.invoice.InvoiceZRE = this.spinEditInvoiceZKE.Value;
            //this.invoice.InvoiceHYGS = this.textEditHuoyungongsi.Text;
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

            if (e.Column == this.colInvoiceXTDetailZS || e.Column == this.colInvoiceXTDetailPrice || e.Column == this.colInvoiceXTDetailQuantity)
            {
                decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceXTDetailQuantity).ToString(), out quantity);
                decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceXTDetailPrice).ToString(), out price);
                bool.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceXTDetailZS) == null ? "false" : this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceXTDetailZS).ToString(), out isZS);

                total1 = this.GetDecimal(price * quantity, BL.V.SetDataFormat.XSJEXiao.Value);

                this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceXTDetailMoney1, total1);
                this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceXTDetailMoney0, total1);

                if (isZS)
                {
                    this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceXTDetailDiscount, decimal.Zero);
                    this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceXTDetailDiscountRate, decimal.Zero);
                }
            }
            flag = 0;
            //this.spinEditInvoiceTaxRate1.Properties.Buttons[1].Enabled = true;
            // this.spinEditInvoiceTaxRate1.Properties.Buttons[2].Enabled = true;            
            this.UpdateMoneyFields();
        }

        protected void UpdateMoneyFields()
        {
            decimal yse = 0;//应收额
            decimal zse = 0;//赠送额

            foreach (Model.InvoiceXTDetail detail in this.invoice.Details)
            {
                if (detail.InvoiceXTDetailZS != null && detail.InvoiceXTDetailZS.Value)
                {
                    zse += detail.InvoiceXTDetailMoney0.Value;
                }
                else
                {

                    yse += detail.InvoiceXTDetailMoney0 == null ? 0 : detail.InvoiceXTDetailMoney0.Value;
                }
            }
            yse = this.GetDecimal(yse, BL.V.SetDataFormat.XSZJXiao.Value);
            zse = this.GetDecimal(yse, BL.V.SetDataFormat.XSZJXiao.Value);
            this.calcEditInvoiceZSE.EditValue = zse;
            if (this.action != "view")
            {
                if (flag == 0)
                {
                    this.calcEditInvoiceTotal1.EditValue = yse;
                    this.calcEditInvoiceTax1.EditValue = 0;
                    this.calcEditInvoiceTotal0.EditValue = yse;
                    //this.comboBoxEditInvoiceKslb.SelectedIndex = 2;
                }
                else if (flag == 1)
                {
                    this.calcEditInvoiceTotal1.EditValue = yse;
                    this.calcEditInvoiceTax1.EditValue = this.GetDecimal(yse * this.spinEditInvoiceTaxRate1.Value / 100, BL.V.SetDataFormat.XSZJXiao.Value);
                    this.calcEditInvoiceTotal0.EditValue = this.GetDecimal(yse + this.calcEditInvoiceTax1.Value, BL.V.SetDataFormat.XSZJXiao.Value);
                    //this.comboBoxEditInvoiceKslb.SelectedIndex = 1;
                }
                else
                {
                    this.calcEditInvoiceTotal0.EditValue = yse;
                    this.calcEditInvoiceTotal1.EditValue = this.GetDecimal(yse * 100 / (100 + this.spinEditInvoiceTaxRate1.Value), BL.V.SetDataFormat.XSZJXiao.Value);
                    this.calcEditInvoiceTax1.EditValue = this.GetDecimal(this.calcEditInvoiceTotal0.Value - this.calcEditInvoiceTotal1.Value, BL.V.SetDataFormat.XSZJXiao.Value);
                    //this.comboBoxEditInvoiceKslb.SelectedIndex = 0;
                }
                if (this.comboBoxEditInvoiceKslb.SelectedIndex != 2)
                {
                    decimal _hj = this.calcEditInvoiceTotal1.Value;
                    decimal _shuiE = this.GetDecimal(this.spinEditInvoiceTaxRate1.Value / 100 * _hj, BL.V.SetDataFormat.XSZJXiao.Value);
                    this.calcEditInvoiceTax1.Value = _shuiE;
                    this.calcEditInvoiceTotal0.Value += _shuiE;
                }
            }

            spinEditInvoiceFpje.EditValue = this.calcEditInvoiceTotal0.EditValue;
        }

        protected override void Delete()
        {
            this.invoiceManager.Delete(this.invoice.InvoiceId);
        }

        public override BaseListForm GetListForm()
        {
            return new ListForm();
        }

        public override Book.Model.Invoice Invoice
        {
            get
            {
                return invoice;
            }
            set
            {
                if (value is Model.InvoiceXT)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceXT).InvoiceId);
                }
            }
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoiceXT();
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Employee1 = BL.V.ActiveOperator.Employee;
            this.invoice.InvoiceTax = 0;
            this.invoice.InvoiceZRE = 0;
            this.invoice.InvoiceZSE = 0;
            this.invoice.InvoiceZongJi = 0;
            this.invoice.InvoiceHeJi = 0;
            this.invoice.InvoiceFpje = 0;
            this.invoice.InvoiceOwed = 0;
            this.invoice.InvoiceYHE = 0;
            this.invoice.InvoicePayTimeLimit = DateTime.Now.Date;

            this.invoice.Details = new List<Model.InvoiceXTDetail>();

            Model.InvoiceXTDetail detail = new Model.InvoiceXTDetail();
            detail.Inumber = this.invoice.Details.Count + 1;
            detail.InvoiceXTDetailId = Guid.NewGuid().ToString();
            detail.InvoiceXTDetailDiscount = 0;
            detail.InvoiceXTDetailDiscountRate = 0;
            detail.InvoiceXTDetailMoney0 = 0;
            detail.InvoiceXTDetailMoney1 = 0;
            detail.InvoiceXTDetailNote = "";
            detail.InvoiceXTDetailPrice = 0;
            detail.InvoiceXTDetailQuantity = 0;
            detail.InvoiceXTDetailTax = 0;
            detail.InvoiceXTDetailTaxRate = 0;
            detail.InvoiceXTDetailZS = false;
            detail.InvoiceProductUnit = "";
            this.invoice.Details.Add(detail);
            this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
        }

        protected override void MoveNext()
        {
            Model.InvoiceXT invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceXT invoice = this.invoiceManager.GetPrev(this.invoice);
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
            this.invoice = this.invoiceManager.Get(this.invoiceManager.GetLast() == null ? "" : this.invoiceManager.GetLast().InvoiceId);
        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoiceXT();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.EditValue = this.invoice.InvoiceDate;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.textEditNote.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.buttonEditDepot.EditValue = this.invoice.Depot;

            this.spinEditInvoiceTaxRate1.EditValue = this.invoice.InvoiceTaxRate == null ? 5 : this.invoice.InvoiceTaxRate;
            this.calcEditInvoiceTax1.EditValue = this.invoice.InvoiceTax == null ? 0 : this.invoice.InvoiceTax;
            this.calcEditInvoiceTotal0.EditValue = this.invoice.InvoiceZongJi == null ? 0 : this.invoice.InvoiceZongJi; ;
            this.calcEditInvoiceTotal1.EditValue = this.invoice.InvoiceHeJi == null ? 0 : this.invoice.InvoiceHeJi; ;
            this.calcEditInvoiceZSE.EditValue = this.invoice.InvoiceZSE == null ? 0 : this.invoice.InvoiceZSE; ;
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
            this.bindingSource1.DataSource = this.invoice.Details;

            base.Refresh();
            this.textEditInvoiceId.Properties.ReadOnly = true;
            this.buttonEditEmployee1.ButtonReadOnly = true;
            this.buttonEditEmployee1.ShowButton = false;
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
            double taxrate = double.Parse(this.spinEditInvoiceTaxRate1.Text); //税率
            double ta = (taxrate + 100) / 100;

            foreach (Model.InvoiceXTDetail detail in this.invoice.Details)
            {

                if (flag == 1)
                {
                    detail.InvoiceXTDetailPrice = detail.InvoiceXTDetailMoney0 / decimal.Parse(detail.InvoiceXTDetailQuantity.ToString());
                }
                else
                {
                    detail.InvoiceXTDetailPrice = detail.InvoiceXTDetailMoney0 / decimal.Parse(detail.InvoiceXTDetailQuantity.ToString()) / decimal.Parse(ta.ToString());
                }
                detail.InvoiceXTDetailMoney1 = decimal.Parse(detail.InvoiceXTDetailQuantity.ToString()) * detail.InvoiceXTDetailPrice;
            }
            this.spinEditInvoiceTaxRate1.Properties.Buttons[1].Enabled = flag == 1 ? false : true;
            this.spinEditInvoiceTaxRate1.Properties.Buttons[2].Enabled = flag == -1 ? false : true;
            this.UpdateMoneyFields();
        }

        private void dateEditInvoiceDate_Leave(object sender, EventArgs e)
        {
            //if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
            //this.dateEditInvoicePayTimeLimit.DateTime = this.dateEditInvoiceDate.DateTime;
        }

        private void spinEditInvoiceTaxRate1_EditValueChanged(object sender, EventArgs e)
        {
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
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceXTDetail).Product;

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

                    //if (!string.IsNullOrEmpty(p.ProductBaseUnit))
                    //{
                    //    this.repositoryItemComboBox1.Items.Add(p.ProductBaseUnit);
                    //}
                    //if (!string.IsNullOrEmpty(p.ProductInnerPackagingUnit))
                    //{
                    //    this.repositoryItemComboBox1.Items.Add(p.ProductInnerPackagingUnit);
                    //}
                    //if (!string.IsNullOrEmpty(p.ProductOuterPackagingUnit))
                    //{
                    //    this.repositoryItemComboBox1.Items.Add(p.ProductOuterPackagingUnit);
                    //}
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
            //if (e.Column == this.colProductId || e.Column == this.gridColumn2 || e.Column == this.gridColumn5)
            //{
            //    Model.InvoiceXTDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceXTDetail;
            //    if (detail != null)
            //    {
            //        Model.Product p = this.productManager.Get(e.Value.ToString());
            //        detail.InvoiceXTDetailId = Guid.NewGuid().ToString();
            //        detail.Invoice = this.invoice;
            //        detail.InvoiceXTDetailDiscount = decimal.Zero;
            //        detail.InvoiceXTDetailDiscountRate = 0;
            //        detail.InvoiceXTDetailNote = "";
            //        detail.InvoiceXTDetailQuantity = 1;
            //        detail.InvoiceXTDetailTax = decimal.Zero;
            //        detail.InvoiceXTDetailTaxRate = 0;
            //        detail.InvoiceXTDetailZS = false;
            //       // detail.PrimaryKey = p;
            //        if(p!=null)
            //        detail.Product = p;
            //        detail.ProductId = p.ProductId;
            //        //detail.InvoiceXTDetailPrice = detail.Product.ProductCurrentCGPrice == null ? 0 : detail.Product.ProductCurrentCGPrice.Value;
            //        //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
            //        detail.InvoiceXTDetailMoney0 = Convert.ToDecimal(detail.InvoiceXTDetailQuantity.Value) * detail.InvoiceXTDetailPrice;
            //        detail.InvoiceXTDetailMoney1 = detail.InvoiceXTDetailMoney0;

            //        this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            //    }
            //    this.gridControl1.RefreshDataSource();
            //}
        }

        private bool CanAdd(IList<Model.InvoiceXTDetail> list)
        {
            foreach (Model.InvoiceXTDetail detail in list)
            {
                if (detail.ProductId == null)
                    return false;
            }
            return true;
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                // if (this.CanAdd(this.invoice.Details))
                //{
                if (e.KeyData == Keys.Enter)
                {
                    Model.InvoiceXTDetail detail = new Model.InvoiceXTDetail();
                    detail.InvoiceXTDetailId = Guid.NewGuid().ToString();
                    detail.Inumber = this.invoice.Details.Count + 1;
                    detail.InvoiceXTDetailDiscount = 0;
                    detail.InvoiceXTDetailDiscountRate = 0;
                    detail.InvoiceXTDetailMoney0 = 0;
                    detail.InvoiceXTDetailMoney1 = 0;
                    detail.InvoiceXTDetailNote = "";
                    detail.InvoiceXTDetailPrice = 0;
                    detail.InvoiceXTDetailQuantity = 0;
                    detail.InvoiceXTDetailTax = 0;
                    detail.InvoiceXTDetailTaxRate = 0;
                    detail.InvoiceXTDetailZS = false;
                    detail.InvoiceProductUnit = "";
                    //detail.PrimaryKey = new Book.Model.CustomerProducts();
                    this.invoice.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                    // }
                }
                if (e.KeyData == Keys.Delete)
                {
                    //this.simpleButtonRemove.PerformClick();
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
            else
            {
                this.bindingSourceDepotPosition.Clear();
                foreach (Model.InvoiceXTDetail detail in this.invoice.Details)
                {
                    detail.DepotPosition = null;
                    detail.DepotPositionId = null;
                }
            }
        }

        private void dateEditInvoiceDate_EditValueChanged(object sender, EventArgs e)
        {
            //    if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
            //    this.dateEditInvoicePayTimeLimit.DateTime = this.dateEditInvoiceDate.DateTime;
        }
        //选择订单
        private void barButtonItemXO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Invoices.XS.SearcharInvoiceXSForm form = new Invoices.XS.SearcharInvoiceXSForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {

                if (form.key != null && form.key.Count > 0)
                {
                    if (invoice.Details.Count > 0 && string.IsNullOrEmpty(invoice.Details[0].ProductId))
                        invoice.Details.RemoveAt(0);
                    //string[] str = (from x in xo.Details select "'" + x.ProductId + "'").Distinct().ToArray();
                    //this.bindingSourceProduct.DataSource = this.productManager.SelectByProductIds(str.Aggregate<string>((a, b) => a + "," + b));
                    //this.gridControl1.RefreshDataSource();

                    Model.InvoiceXODetail xo = form.key[0];
                    //invoice.InvoiceXO = xo;
                    //invoice.invoiceid = xo.InvoiceId;

                    invoice.Customer = xo.Invoice.xocustomer;
                    this.buttonEditCompany.EditValue = invoice.Customer;
                    invoice.CustomerId = invoice.Customer.CustomerId;
                    foreach (Model.InvoiceXODetail xos in form.key)
                    {
                        Model.InvoiceXTDetail xtdetail = new Model.InvoiceXTDetail();
                        xtdetail.Inumber = invoice.Details.Count + 1;
                        xtdetail.Invoice = invoice;
                        xtdetail.InvoiceId = invoice.InvoiceId;
                        xtdetail.InvoiceXOId = xos.InvoiceId;
                        xtdetail.InvoiceXTDetailId = Guid.NewGuid().ToString();
                        xtdetail.InvoiceXTDetailPrice = xos.InvoiceXODetailPrice;
                        xtdetail.InvoiceXTDetailQuantity = 0;
                        xtdetail.InvoiceXODetailId = xos.InvoiceXODetailId;
                        xtdetail.InvoiceXODetail = xos;
                        xtdetail.Product = xos.Product;
                        xtdetail.ProductId = xos.Product.ProductId;
                        xtdetail.InvoiceProductUnit = xos.InvoiceProductUnit;
                        xtdetail.HandbookId = xos.HandbookId;
                        xtdetail.HandbookProductId = xos.HandbookProductId;
                        invoice.Details.Add(xtdetail);
                    }
                    this.gridControl1.RefreshDataSource();
                }
                form.Dispose();
                GC.Collect();
            }
        }

        private IList<Model.InvoiceXTDetail> detailsDisplay;

        Model.Product detailDisplay;

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            detailsDisplay = this.bindingSource1.DataSource as IList<Model.InvoiceXTDetail>;
            if (detailsDisplay == null || detailsDisplay.Count < 1) return;
            //Model.CustomerProducts detail = details[e.ListSourceRowIndex].PrimaryKey;
            detailDisplay = detailsDisplay[e.ListSourceRowIndex].Product;
            //Model.InvoiceXODetail xodetail = invoicexoDetailManager.Get(detailsDisplay[e.ListSourceRowIndex].InvoiceXODetailId);
            if (detailDisplay == null) return;
            switch (e.Column.Name)
            {
                case "colProductId":
                    e.DisplayText = detailDisplay.Id;
                    break;
                case "gridColumCusProName":
                    e.DisplayText = detailDisplay.CustomerProductName;
                    break;
                case "gridColumnCuxXOId":
                    if (detailsDisplay[e.ListSourceRowIndex].InvoiceXODetail != null)
                        e.DisplayText = detailsDisplay[e.ListSourceRowIndex].InvoiceXODetail.Invoice.CustomerInvoiceXOId;
                    break;
            }
        }

        private void comboBoxEditInvoiceKslb_EditValueChanged(object sender, EventArgs e)
        {
            this.UpdateMoneyFields();
        }
    }
}