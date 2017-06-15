using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using System.Linq;

namespace Book.UI.Invoices.CG
{

    public partial class EditForm : BaseEditForm
    {
        BL.InvoiceCGManager invoiceManager = new Book.BL.InvoiceCGManager();
        BL.InvoiceCGDetailManager invoiceDetailManager = new Book.BL.InvoiceCGDetailManager();
        BL.InvoiceCJDetailManager invoiceCJDetailManager = new Book.BL.InvoiceCJDetailManager();
        BL.InvoiceCODetailManager invoiceCODetailManager = new Book.BL.InvoiceCODetailManager();
        BL.InvoiceCJManager invoiceCJManager = new Book.BL.InvoiceCJManager();
        BL.InvoiceCOManager invoiceCOManager = new Book.BL.InvoiceCOManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.SupplierProductManager supplierproductmanager = new Book.BL.SupplierProductManager();
        BL.DepotManager depotManager = new Book.BL.DepotManager();
        BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private Model.InvoiceCO invoiceco;
        public static double SetNums = 0;
        public static Model.InvoiceCG invoicecg;
        public static Dictionary<string, Model.InvoiceCGDetail> dic = new Dictionary<string, Book.Model.InvoiceCGDetail>();

        Model.InvoiceCGDetail cgdetail;
        IList<Model.DepotPosition> DepotPositionList = null;

        //0 免税 1 外加 -1 内含
        private int flag = 0;

        public EditForm()
        {
            InitializeComponent();
            this.colInvoiceCGDetailPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoiceCGDetailPrice.DisplayFormat.FormatString = GetFormat(BL.V.SetDataFormat.CGDJXiao.Value);

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add(Model.InvoiceCG.PRO_DepotId, new AA(Properties.Resources.deptNotNull, this.newChooseContorDepotId));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.invalidValueExceptions.Add(Model.InvoiceCG.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));
            this.requireValueExceptions.Add(Model.InvoiceCGDetail.PRO_DepotPositionId, new AA(Properties.Resources.DepotInStockQuertyIsNull, this.gridControl1));
            this.invalidValueExceptions.Add("CGQgtCOQ", new AA("進貨數量不能大於未進貨數量", this.gridControl1 as Control));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.Supplier, this.newChooseContorlSuplier));
            this.newChooseContorlAtCurrencyCate.Choose = new Accounting.CurrencyCategory.ChooseAtCurrencyCategory();

            //this.requireValueExceptions.Add(Model.InvoiceXSDetail.PROPERTY_DEPOTPOSITIONID, new AA(Properties.Resources.RequireChoosePosition, this.gridControl1));
            this.action = "view";
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.buttonEditEmployee1.Choose = new ChooseEmployee();
            this.buttonEditEmployee2.Choose = new ChooseEmployee();
            this.newChooseContorDepotId.Choose = new Book.UI.Invoices.ChooseDepot();
            this.newChooseContorlSuplier.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();

            string sql = "SELECT productid,id,productname FROM product WHERE IsCustomerProduct IS NULL OR IsCustomerProduct =0";
            this.bindingSourceWorkHouse.DataSource = new BL.WorkHouseManager().Select();
            this.bindingSourceProductId.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            this.EmpAudit.Choose = new ChooseEmployee();

        }

        int LastFlag = 0; //页面载入时是否执行 last方法

        public EditForm(string invoiceid)
            : this()
        {
            invoicecg = this.invoiceManager.Get(invoiceid);
            if (invoicecg == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.InvoiceCG invoice)
            : this()
        {
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            invoicecg = this.invoiceManager.Get(invoice.InvoiceId);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.InvoiceCO invoice)
            : this()
        {
            this.invoiceco = this.invoiceCOManager.Get(invoice.InvoiceId);

            this.action = "insert";
        }

        #region Choose Object
        protected override string getName()
        {
            string formName = this.GetType().FullName;
            formName = formName.Substring(formName.IndexOf('.') + 1).Substring(formName.Substring(formName.IndexOf('.') + 1).IndexOf('.') + 1);
            return formName;
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

        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {
            //this.bindingSourceProductId.DataSource = productManager.SelectNotCustomer();

            //判断操作人员角色权限
            Model.Role mRole = this.SelectOperatorKeyTag(BL.V.ActiveOperator);
            if (mRole != null)
            {
                if (mRole.IsCOJinHuoJinE == false)
                {
                    this.colInvoiceCGDetailPrice.Visible = false;
                    this.colInvoiceCGDetailMoney.Visible = false;
                    //this.colInvoiceCGDetailTaxPrice.Visible = false;
                    //this.colInvoiceCGDetailTax.Visible = false;
                    //this.colInvoiceCGDetailTaxMoney.Visible = false;
                    this.gridColumnZR.Visible = false;
                }

                if (mRole.IsCOJiaoYiMingXi == false && mRole.IsCOFaPiaoZiLiao == false && mRole.IsCOXiangGuanZiLiao == false && mRole.IsCOZhangKuanZiLiao == false)
                {
                    //this.xtraTabControl1.Hide();
                    //this.xtraTabControl1.Visible = false;
                    //this.xtraTabControl1.Height = 0;
                    this.xtraTabControl1.Dispose();
                }
                else
                {
                    this.xTabPageJYMX.PageVisible = mRole.IsCOJiaoYiMingXi.HasValue ? mRole.IsCOJiaoYiMingXi.Value : false;
                    this.xTabPageFPZL.PageVisible = mRole.IsCOFaPiaoZiLiao.HasValue ? mRole.IsCOFaPiaoZiLiao.Value : false;
                    this.xTabPageZKZL.PageVisible = mRole.IsCOZhangKuanZiLiao.HasValue ? mRole.IsCOZhangKuanZiLiao.Value : false;
                    this.xTabPageXGZL.PageVisible = mRole.IsCOXiangGuanZiLiao.HasValue ? mRole.IsCOXiangGuanZiLiao.Value : false;
                }
            }
        }

        protected override string tableCode()
        {
            return "InvoiceCG";
        }

        protected override int AuditState()
        {
            return this.Invoice.AuditState.HasValue ? this.Invoice.AuditState.Value : 0;
        }

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        Model.InvoiceCGDetail detail = new Book.Model.InvoiceCGDetail();
                        detail.InvoiceCGDetailId = Guid.NewGuid().ToString();
                        detail.Inumber = invoicecg.Details.Count + 1;
                        detail.Invoice = invoicecg;
                        detail.InvoiceCGDetailNote = "";
                        detail.InvoiceCGDetailQuantity = 1;
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        detail.InvoiceProductUnit = detail.Product.DepotUnit.CnName;
                        detail.DepotPositionId = null;
                        detail.OrderQuantity = 0;
                        detail.InvoiceCGDetailPrice = 0;
                        if (string.IsNullOrEmpty(detail.Product.SupplierId))
                        {
                            detail.DetailsPriceRange = string.Empty;
                        }
                        else
                        {
                            detail.DetailsPriceRange = supplierproductmanager.GetPriceRangeForSupAndProduct(detail.Product.SupplierId, detail.ProductId);
                        }
                        detail.NoArrivalQuantity = 0;
                        detail.InvoiceCGDetailQuantity = 0;
                        detail.InvoiceCGDetaiInQuantity = 0;
                        detail.ProduceTransferQuantity = 0;
                        detail.InvoiceCODetailId = null;
                        detail.InvoiceAllowance = 0;
                        detail.Donatetowards = false;
                        invoicecg.Details.Add(detail);
                        this.gridControl1.RefreshDataSource();
                        this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    Model.InvoiceCGDetail detail = new Book.Model.InvoiceCGDetail();
                    detail.Inumber = invoicecg.Details.Count + 1;
                    detail.InvoiceCGDetailId = Guid.NewGuid().ToString();
                    detail.Invoice = invoicecg;
                    detail.InvoiceCGDetailNote = "";
                    detail.InvoiceCGDetailQuantity = 1;
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    detail.InvoiceProductUnit = detail.Product.DepotUnit.CnName;
                    detail.DepotPositionId = null;
                    detail.OrderQuantity = 0;
                    detail.InvoiceCGDetailPrice = 0;
                    if (string.IsNullOrEmpty(detail.Product.SupplierId))
                    {
                        detail.DetailsPriceRange = string.Empty;
                    }
                    else
                    {
                        detail.DetailsPriceRange = supplierproductmanager.GetPriceRangeForSupAndProduct(detail.Product.SupplierId, detail.ProductId);
                    }
                    detail.NoArrivalQuantity = 0;
                    detail.InvoiceCGDetailQuantity = 0;
                    detail.InvoiceCGDetaiInQuantity = 0;
                    detail.ProduceTransferQuantity = 0;
                    detail.InvoiceCODetailId = null;
                    detail.InvoiceAllowance = 0;
                    detail.Donatetowards = false;
                    invoicecg.Details.Add(detail);
                    this.gridControl1.RefreshDataSource();
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
            }
        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                invoicecg.Details.Remove(this.bindingSourceDetails.Current as Book.Model.InvoiceCGDetail);

                if (invoicecg.Details.Count == 0)
                {
                    Model.InvoiceCGDetail detail = new Model.InvoiceCGDetail();
                    detail.InvoiceCGDetailId = Guid.NewGuid().ToString();
                    detail.Inumber = invoicecg.Details.Count + 1;
                    //detail.InvoiceCGDetailDiscount = 0;
                    //detail.InvoiceCGDetailDiscountRate = 0;
                    //detail.InvoiceCGDetailMoney0 = 0;
                    //detail.InvoiceCGDetailMoney1 = 0;
                    detail.InvoiceCGDetailNote = "";
                    //detail.InvoiceCGDetailPrice = 0;
                    detail.InvoiceCGDetailQuantity = 0;
                    //detail.InvoiceCGDetailTax = 0;
                    //detail.InvoiceCGDetailTaxRate = 0;
                    //detail.InvoiceCGDetailZS = false;
                    detail.InvoiceProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    invoicecg.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.UpdateMoneyFields();
            }
        }

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            if (this.newChooseContorDepotId.EditValue != null)
            {
                Model.Depot depot = this.newChooseContorDepotId.EditValue as Model.Depot;

                this.bindingSourceDepotPosition.DataSource = DepotPositionList = new BL.DepotPositionManager().Select(depot);
            }
            //if (this.action != "view" && invoicecg.Details.Count > 0 && (this.bindingSourceDepotPosition.DataSource as IList<Model.DepotPosition>) != null && !(this.bindingSourceDepotPosition.DataSource as IList<Model.DepotPosition>).Any(d => d.DepotPositionId == invoicecg.Details[0].DepotPositionId))
            //    foreach (var item in invoicecg.Details)
            //    {
            //        item.DepotPositionId = null;
            //    }
            if (DepotPositionList != null)
            {
                foreach (var item in invoicecg.Details)
                {
                    if (!DepotPositionList.Any(d => d.DepotPositionId == item.DepotPositionId))
                    {
                        item.DepotPositionId = null;
                        item.DepotPosition = null;
                    }
                }
            }

            invoicecg.InvoiceStatus = (int)status;
            invoicecg.InvoiceId = this.textEditInvoiceId.Text;
            invoicecg.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            invoicecg.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            invoicecg.Depot = this.newChooseContorDepotId.EditValue as Model.Depot;
            if (invoicecg.Depot != null)
                invoicecg.DepotId = invoicecg.Depot.DepotId;
            invoicecg.Supplier = this.newChooseContorlSuplier.EditValue as Model.Supplier;
            if (invoicecg.Supplier != null)
                invoicecg.SupplierId = invoicecg.Supplier.SupplierId;
            //  invoicecg.InvoiceCOId = this.textEditInvoiceCO.Text;
            invoicecg.InvoiceNote = this.textEditNote.Text;
            if (this.buttonEditEmployee.EditValue != null)
                invoicecg.Employee0Id = (this.buttonEditEmployee.EditValue as Model.Employee).EmployeeId;
            if (this.buttonEditEmployee1.EditValue != null)
                invoicecg.Employee1 = this.buttonEditEmployee1.EditValue as Model.Employee;
            if (this.buttonEditEmployee2.EditValue != null)
                invoicecg.Employee2 = this.buttonEditEmployee2.EditValue as Model.Employee;
            invoicecg.InvoiceTaxrate = double.Parse(this.spinEditInvoiceTaxRate.Text);

            invoicecg.InvoiceTotal = decimal.Parse(this.calcEditInvoiceTotal.Text);
            invoicecg.InvoiceTax = decimal.Parse(this.calcEditInvoiceTax.Text);
            invoicecg.InvoiceHeji = decimal.Parse(this.calcEditInvoiceHeji.Text);
            invoicecg.TaxCaluType = this.comboBoxEditInvoiceKslb.SelectedIndex;
            invoicecg.InvoiceHisDate = this.dateHisDate.DateTime;
            invoicecg.InvoicePaymentDate = this.datePaymentDate.DateTime;
            invoicecg.InvoiceAllowance = double.Parse(this.calcInvoiceAllowance.Text);
            this.Invoice.AuditState = this.saveAuditState;
            //if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
            //    return;
            if (this.newChooseContorlAtCurrencyCate.EditValue != null)
            {
                invoicecg.AtCurrencyCategory = this.newChooseContorlAtCurrencyCate.EditValue as Model.AtCurrencyCategory;

                invoicecg.AtCurrencyCategoryId = invoicecg.AtCurrencyCategory.AtCurrencyCategoryId;
            }
            //选项卡资料
            invoicecg.InvoiceFPDate = this.dateEditFPDate.DateTime;
            invoicecg.InvoicePayable = this.spinEditInvoiceOwed.Value;
            invoicecg.InvoiceLRTime = DateTime.Now;
            invoicecg.InvoiceFPBH = this.textEditInvoiceFpbh.Text;
            invoicecg.InvoiceKSLB = this.comboBoxEditInvoiceKslb.Text;
            invoicecg.InvoiceKLFS = this.comboBoxEditInvoiceKlfs.Text;
            invoicecg.InvoiceKPLS = this.comboBoxEditInvoiceFpls.Text;
            invoicecg.InvoiceFPJE = decimal.Parse(string.IsNullOrEmpty(this.spinEditInvoiceFpje.Text) ? "0" : this.spinEditInvoiceFpje.Text);
            invoicecg.InvoiceCPBH = this.textEditInvoiceCphm.Text;
            invoicecg.InvoiceDiscount = this.spinEditInvoiceZKE.Value;
            invoicecg.InvoicePayable = decimal.Parse(string.IsNullOrEmpty(this.spinEditInvoiceOwed.Text) ? "0" : this.spinEditInvoiceOwed.Text);

            invoicecg.InsertTime = invoicecg.InvoiceDate;
            switch (this.action)
            {
                case "insert":
                    this.invoiceManager.Insert(invoicecg);
                    break;

                case "update":
                    this.invoiceManager.Update(invoicecg);
                    break;
            }
        }

        protected override void Delete()
        {
            this.invoiceManager.Delete(invoicecg.InvoiceId);
        }

        public override BaseListForm GetListForm()
        {
            return new ListForm();
        }

        public override Book.Model.Invoice Invoice
        {
            get
            {
                return invoicecg;
            }
            set
            {
                if (value is Model.InvoiceCG)
                {
                    invoicecg = invoiceManager.Get((value as Model.InvoiceCG).InvoiceId);
                }
            }
        }

        protected override void TurnNull()
        {
            if (invoicecg == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this.invoiceManager.TurnNull(invoicecg.InvoiceId);
            invoicecg = this.invoiceManager.GetNext(invoicecg);
            if (invoicecg == null)
            {
                invoicecg = this.invoiceManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            invoicecg = new Model.InvoiceCG();
            invoicecg.InvoiceId = this.invoiceManager.GetNewId();
            invoicecg.InvoiceDate = DateTime.Now;
            invoicecg.Details = new List<Model.InvoiceCGDetail>();
            invoicecg.InvoiceHisDate = DateTime.Now;
            invoicecg.InvoicePaymentDate = DateTime.Now;
            invoicecg.InvoiceTaxrate = 0;
            invoicecg.InvoiceTax = 0;
            invoicecg.InvoiceTotal = 0;
            invoicecg.InvoiceHeji = 0;
            invoicecg.InvoiceAllowance = 0;
            invoicecg.Employee1 = BL.V.ActiveOperator.Employee;
            flag = 0;

            if (this.invoiceco != null)
            {
                SetNums = 0;
                invoicecg = new Book.Model.InvoiceCG();
                dic.Clear();

                invoicecg.Supplier = this.invoiceco.Supplier;
                invoicecg.SupplierId = this.invoiceco.SupplierId;

                invoicecg.Employee0 = this.invoiceco.Employee0;
                invoicecg.Employee0Id = this.invoiceco.Employee0Id;

                invoicecg.InvoiceAbstract = this.invoiceco.InvoiceAbstract;
                invoicecg.InvoiceNote = this.invoiceco.InvoiceNote;
                invoicecg.InvoiceId = this.invoiceManager.GetNewId(DateTime.Now.Date);
                invoicecg.InvoiceTaxrate = this.invoiceco.InvoiceTaxrate;
                invoicecg.InvoiceTotal = 0;
                invoicecg.InvoiceHeji = 0;
                this.invoiceco.Details = this.invoiceCODetailManager.Select(this.invoiceco);
                invoicecg.PositionAndNumsSet.Clear();

                foreach (Model.InvoiceCODetail detail in this.invoiceco.Details)
                {
                    this.cgdetail = new Book.Model.InvoiceCGDetail();
                    this.cgdetail.InvoiceCODetail = detail;
                    this.cgdetail.Inumber = invoicecg.Details.Count + 1;
                    this.cgdetail.CODetailID = detail.InvoiceCODetailId;
                    this.cgdetail.Invoice = invoicecg;
                    this.cgdetail.InvoiceId = detail.InvoiceId;
                    this.cgdetail.COinvoinceID = detail.InvoiceId;
                    this.cgdetail.InvoiceCGDetailId = Guid.NewGuid().ToString();
                    this.cgdetail.InvoiceCGDetailNote = detail.InvoiceCODetailNote;
                    this.cgdetail.Product = detail.Product;
                    this.cgdetail.ProductId = detail.ProductId;
                    this.cgdetail.InvoiceProductUnit = detail.InvoiceProductUnit;
                    this.cgdetail.OrderQuantity = detail.OrderQuantity;
                    this.cgdetail.CoPrice = detail.InvoiceCODetailPrice;
                    this.cgdetail.NoArrivalQuantity = detail.NoArrivalQuantity;
                    this.cgdetail.InvoiceCGDetailQuantity = 0;
                    this.cgdetail.InvoiceCODetailId = detail.InvoiceCODetailId;

                    invoicecg.PositionAndNumsSet.Add(this.cgdetail);
                }
            }
        }

        protected override void MoveNext()
        {
            Model.InvoiceCG invoice = this.invoiceManager.GetNext(invoicecg);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            invoicecg = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceCG invoice = this.invoiceManager.GetPrev(invoicecg);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            invoicecg = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MoveFirst()
        {
            invoicecg = this.invoiceManager.Get(this.invoiceManager.GetFirst() == null ? "" : this.invoiceManager.GetFirst().InvoiceId);
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            invoicecg = this.invoiceManager.Get(this.invoiceManager.GetLast() == null ? "" : this.invoiceManager.GetLast().InvoiceId);
        }

        public override void Refresh()
        {
            if (invoicecg == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    invoicecg = this.invoiceManager.GetDetails(invoicecg);
                }
            }

            #region 注释备用=============主要勿删
            //invoicecg.PositionAndNumsSet.Clear();
            //Dictionary<string, Model.InvoiceCGDetail> dicInvoiceCGDetail = new Dictionary<string, Book.Model.InvoiceCGDetail>();
            //foreach (Model.InvoiceCGDetail item in invoiceDetailManager.Select(invoicecg))
            //{
            //    if (!dicInvoiceCGDetail.ContainsKey(item.InvoiceCODetailId))
            //    {
            //        dicInvoiceCGDetail.Add(item.InvoiceCODetailId, item);
            //        //invoicecg.PositionAndNumsSet.Add(item);
            //    }
            //    else
            //    {
            //        if (dicInvoiceCGDetail[item.InvoiceCODetailId].ProductId != item.ProductId)
            //        {
            //            dicInvoiceCGDetail.Add(item.InvoiceCODetailId, item);
            //            //invoicecg.PositionAndNumsSet.Add(item);
            //        }
            //        else
            //        {
            //            if (dicInvoiceCGDetail[item.InvoiceCODetailId].InvoiceProductUnit != item.InvoiceProductUnit)
            //            {
            //                dicInvoiceCGDetail.Add(item.InvoiceCODetailId, item);
            //                //invoicecg.PositionAndNumsSet.Add(item);
            //            }
            //            else
            //            {
            //                item.InvoiceCGDetailQuantity += dicInvoiceCGDetail[item.InvoiceCODetailId].InvoiceCGDetailQuantity;
            //                dicInvoiceCGDetail.Remove(item.InvoiceCODetailId);
            //                dicInvoiceCGDetail.Add(item.InvoiceCODetailId, item);
            //            }
            //        }
            //    }
            //}

            //foreach (Model.InvoiceCGDetail item in dicInvoiceCGDetail.Values)
            //{
            //    invoicecg.PositionAndNumsSet.Add(item);
            //}

            //}
            //else
            //{
            //    foreach (Model.InvoiceCGDetail item in invoicecg.PositionAndNumsSet)
            //    {
            //        item.InvoiceCGDetailQuantity = 0;
            //    }
            //    IList<Model.InvoiceCGDetail> tempcg = invoiceDetailManager.Select(invoicecg);
            //    foreach (Model.InvoiceCGDetail item in invoicecg.PositionAndNumsSet)
            //    {
            //        foreach (Model.InvoiceCGDetail temp in tempcg)
            //        {
            //            if (item.ProductId == temp.ProductId && item.InvoiceProductUnit == temp.InvoiceProductUnit)
            //                item.InvoiceCGDetailQuantity += temp.InvoiceCGDetailQuantity;
            //        }
            //    }
            //}
            //}

            #endregion

            this.textEditInvoiceId.Text = invoicecg.InvoiceId;
            this.newChooseContorlSuplier.EditValue = invoicecg.Supplier;
            this.dateHisDate.EditValue = invoicecg.InvoiceHisDate;
            this.datePaymentDate.EditValue = invoicecg.InvoicePaymentDate.HasValue ? invoicecg.InvoicePaymentDate.Value : DateTime.Now;
            this.dateEditInvoiceDate.EditValue = invoicecg.InvoiceDate;
            //this.textEditInvoiceCO.Text = invoicecg.InvoiceCOId;
            this.buttonEditEmployee.EditValue = invoicecg.Employee0;
            this.textEditNote.EditValue = invoicecg.InvoiceNote;
            this.buttonEditEmployee2.EditValue = invoicecg.Employee2;
            this.buttonEditEmployee1.EditValue = invoicecg.Employee1;
            this.calcEditInvoiceHeji.EditValue = invoicecg.InvoiceHeji;
            this.calcEditInvoiceTax.EditValue = invoicecg.InvoiceTax;
            this.calcEditInvoiceTotal.EditValue = invoicecg.InvoiceTotal;
            this.calcInvoiceAllowance.EditValue = invoicecg.InvoiceAllowance;
            this.spinEditInvoiceTaxRate.EditValue = invoicecg.InvoiceTaxrate;
            this.comboBoxEditInvoiceKslb.SelectedIndex = invoicecg.TaxCaluType.HasValue ? invoicecg.TaxCaluType.Value : 0;
            this.newChooseContorDepotId.EditValue = invoicecg.Depot;
            this.bindingSourceDetails.DataSource = invoicecg.Details;
            this.flag = invoicecg.TaxCaluType.HasValue ? invoicecg.TaxCaluType.Value : 0;
            this.spinEditInvoiceTaxRate.Properties.Buttons[1].Enabled = flag == 0 ? false : true;
            this.spinEditInvoiceTaxRate.Properties.Buttons[2].Enabled = flag == 1 ? false : true;
            this.spinEditInvoiceTaxRate.Properties.Buttons[3].Enabled = flag == 2 ? false : true;
            this.EmpAudit.EditValue = this.Invoice.AuditEmp;
            this.textEditAuditState.Text = this.Invoice.AuditStateName;
            this.newChooseContorlAtCurrencyCate.EditValue = invoicecg.AtCurrencyCategory;
            this.textEditInvoiceCphm.Text = invoicecg.InvoiceCPBH;
            this.textEditInvoiceFpbh.Text = invoicecg.InvoiceFPBH;
            this.comboBoxEditInvoiceKslb.EditValue = invoicecg.InvoiceKSLB;
            this.comboBoxEditInvoiceKlfs.EditValue = invoicecg.InvoiceKLFS;
            this.comboBoxEditInvoiceFpls.EditValue = invoicecg.InvoiceKPLS;
            this.dateEditFPDate.EditValue = invoicecg.InvoiceFPDate.HasValue ? invoicecg.InvoiceFPDate.Value : DateTime.Now.AddDays(3);
            this.spinEditInvoiceFpje.EditValue = invoicecg.InvoiceFPJE;

            //this.spinEditInvoiceOwed.EditValue = this.invoice.InvoicePayable;
            this.spinEditInvoiceZKE.EditValue = invoicecg.InvoiceDiscount;
            this.spinEditInvoiceOwed.EditValue = decimal.Parse(this.calcEditInvoiceTotal.EditValue.ToString()) - this.spinEditInvoiceZKE.Value - this.spinEditYifu.Value;


            base.Refresh();

            //税别标记
            switch (this.action)
            {
                case "insert":
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.barButtonItem3.Enabled = true;
                    // this.simpleButtonOver.Enabled = false;
                    this.barButtonItem4.Enabled = true;
                    this.datePaymentDate.Enabled = true;

                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.barButtonItem3.Enabled = false;
                    // this.simpleButtonOver.Enabled = true;
                    this.barButtonItem4.Enabled = false;
                    this.datePaymentDate.Enabled = false;
                    break;
            }
            this.textEditInvoiceId.Properties.ReadOnly = true;
            this.buttonEditEmployee1.Enabled = false;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R01(invoicecg.InvoiceId);
        }

        protected override bool HasRows()
        {
            return this.invoiceManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.invoiceManager.HasRowsAfter(invoicecg);
        }

        protected override bool HasRowsPrev()
        {
            return this.invoiceManager.HasRowsBefore(invoicecg);
        }

        //private void dateEditInvoiceDate_Leave(object sender, EventArgs e)
        //{
        //    if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
        //    this.dateEditInvoicePayTimeLimit.DateTime = this.dateEditInvoiceDate.DateTime;
        //}

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this.textEditInvoiceId });
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.gridView1.FocusedColumn.Name == "gridColumnUnit")
                {

                    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {
                        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceCGDetail).Product;

                        this.repositoryItemComboBoxUnit.Items.Clear();

                        if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                        {
                            BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                            IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroupId);
                            foreach (Model.ProductUnit item in unitList)
                            {
                                this.repositoryItemComboBoxUnit.Items.Add(item.CnName);
                            }

                        }

                    }
                }
            }
        }

        private void simpleButtonChooseFromInvoice_Click(object sender, EventArgs e)
        {

            CO.ChooseInvoiceForm f1 = new Book.UI.Invoices.CO.ChooseInvoiceForm();
            f1.ShowDialog(this);
            if (f1.DialogResult != DialogResult.OK)
                return;

            CO.ChooseDetailsForm f2 = new Book.UI.Invoices.CO.ChooseDetailsForm(f1.SelectedItem);
            f2.ShowDialog(this);
            if (f2.DialogResult != DialogResult.OK)
                return;

            invoicecg.Details.Clear();
            foreach (Model.InvoiceCODetail detail in f2.SelectedItems)
            {
                Model.InvoiceCGDetail cgdetail = new Book.Model.InvoiceCGDetail();
                cgdetail.Inumber = invoicecg.Details.Count + 1;
                //cgdetail.InvoiceCGDetailDiscount = decimal.Zero;
                //cgdetail.InvoiceCGDetailDiscountRate = 0;
                //cgdetail.InvoiceCGDetailMoney0 = detail.InvoiceCODetailMoney;
                //cgdetail.InvoiceCGDetailMoney1 = detail.InvoiceCODetailMoney;
                cgdetail.InvoiceCGDetailNote = "";
                //cgdetail.InvoiceCGDetailPrice = detail.InvoiceCODetailPrice;
                //cgdetail.InvoiceCGDetailQuantity = detail.InvoiceCODetailQuantity;
                //cgdetail.InvoiceCGDetailTax = decimal.Zero;
                //cgdetail.InvoiceCGDetailTaxRate = 0;
                //cgdetail.InvoiceCGDetailZS = false;
                cgdetail.Product = detail.Product;
                cgdetail.ProductId = detail.ProductId;
                invoicecg.Details.Add(cgdetail);
            }
            this.gridControl1.RefreshDataSource();
            this.invoiceco = f2.SelectedItems[0].Invoice;

        }

        private bool CanAdd(IList<Model.InvoiceCGDetail> list)
        {
            foreach (Model.InvoiceCGDetail detail in list)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    return false;
            }
            return true;
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //    if (this.action == "insert" || this.action == "update")
            //    {    
            //        if (this.CanAdd(invoicecg.Details))
            //        {
            //            if (e.KeyData == Keys.Enter)
            //            {
            //                Model.InvoiceCGDetail detail = new Model.InvoiceCGDetail();
            //                detail.InvoiceCGDetailId = Guid.NewGuid().ToString();
            //                //detail.InvoiceCGDetailDiscount = 0;
            //                //detail.InvoiceCGDetailDiscountRate = 0;
            //                //detail.InvoiceCGDetailMoney0 = 1;
            //                //detail.InvoiceCGDetailMoney1 = 1;
            //                detail.InvoiceCGDetailNote = "";
            //                //detail.InvoiceCGDetailPrice = 1;
            //                detail.InvoiceCGDetailQuantity = 1;
            //                //detail.InvoiceCGDetailTax = 0;
            //                //detail.InvoiceCGDetailTaxRate = 0;
            //                //detail.InvoiceCGDetailZS = false;
            //                detail.InvoiceProductUnit = "";
            //                detail.Product = new Book.Model.Product();
            //                invoicecg.Details.Add(detail);
            //                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            //            }
            //        }
            //if (e.KeyData == Keys.Delete)
            //{
            //    this.simpleButtonRemove.PerformClick();
            //}
            // this.gridControl1.RefreshDataSource();
            //}
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoiceCGDetail> details = this.bindingSourceDetails.DataSource as IList<Model.InvoiceCGDetail>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            Model.InvoiceCODetail codetail = details[e.ListSourceRowIndex].InvoiceCODetail;
            switch (e.Column.Name)
            {
                case "colProductId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
                case "gridColumn7":
                    if (codetail != null)
                        e.DisplayText = codetail.NoArrivalQuantity.HasValue ? codetail.NoArrivalQuantity.Value.ToString("0.####") : "0";
                    break;
                case "gridColumn5":
                    if (codetail != null)
                        e.DisplayText = codetail.OrderQuantity.HasValue ? codetail.OrderQuantity.Value.ToString() : "0";
                    break;
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                Model.InvoiceCGDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceCGDetail;

                if (e.Column == this.colProductId)
                {
                    if (detail != null)
                    {
                        Model.Product p = productManager.Get(e.Value.ToString());
                        detail.InvoiceCGDetailId = Guid.NewGuid().ToString();
                        detail.InvoiceCGDetailNote = "";
                        detail.Product = p;
                        detail.ProductId = p.ProductId;
                        detail.InvoiceProductUnit = p.DepotUnit.CnName;

                        this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                    }
                    this.gridControl1.RefreshDataSource();
                }

                if (e.Column == this.gridColumnPositionId)
                {
                    if (detail != null)
                    {
                        Model.DepotPosition position = this.depotPositionManager.Get(e.Value.ToString());
                        detail.DepotPosition = position;

                        detail.DepotPositionId = position.DepotPositionId;

                    }
                    this.gridControl1.RefreshDataSource();
                }

                #region 备注留用
                //if (e.Column == this.gridColumn6)
                //{
                ////   // Model.InvoiceCGDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceCGDetail;


                ////    if (detail != null)
                ////    {
                //      double a = 0;
                //      double.TryParse(e.Value.ToString(), out a);
                //      detail.InvoiceCGDetailQuantity = a;
                ////       // if (detail.NoArrivalQuantity < 0)
                ////         //   detail.NoArrivalQuantity = 0;
                ////        //
                ////        //if (e.Value != null && detail.InvoiceCGDetailQuantity > detail.NoArrivalQuantity)
                ////        //{
                ////        //    MessageBox.Show("餇?顪掕瑳绁ュ婀缓甯わ牭顜涱湌璎?);
                ////        //    detail.InvoiceCGDetailQuantity = 0;

                ////        //}
                //      this.gridControl1.RefreshDataSource();
                ////    }
                //}

                #endregion
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colInvoiceCGDetailQuantity || e.Column == this.colInvoiceCGDetailPrice || e.Column == this.gridColumnZS || e.Column == this.gridColumnZR)
            {
                decimal price = decimal.Zero;
                decimal quantity = decimal.Zero;
                decimal zherang = decimal.Zero;
                //decimal tax = decimal.Zero;
                //decimal money = decimal.Zero;
                //decimal taxmoney = decimal.Zero;
                if (e.Column == this.colInvoiceCGDetailPrice)
                {
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out price);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailQuantity) == null ? "0" : this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailQuantity).ToString(), out quantity);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnZR) == null ? "0" : this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnZR).ToString(), out zherang);
                }
                if (e.Column == this.colInvoiceCGDetailQuantity)
                {
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out quantity);     //获得数量

                    Model.InvoiceCGDetail invoicecgdetail = this.bindingSourceDetails.Current as Model.InvoiceCGDetail;
                    if (string.IsNullOrEmpty(invoicecgdetail.Product.SupplierId))
                    {
                        invoicecgdetail.DetailsPriceRange = string.Empty;
                    }
                    else
                    {
                        invoicecgdetail.DetailsPriceRange = supplierproductmanager.GetPriceRangeForSupAndProduct(invoicecgdetail.Product.SupplierId, invoicecgdetail.ProductId);
                    }

                    if (string.IsNullOrEmpty(invoicecgdetail.DetailsPriceRange))
                    {
                        decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailPrice) == null ? "0" : this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailPrice).ToString(), out price); //获得单价
                    }
                    else
                    {
                        price = BL.SupplierProductManager.CountPrice(invoicecgdetail.DetailsPriceRange, Convert.ToDouble(quantity));
                        this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailPrice, price);
                    }

                    //this.gridView1.SetRowCellValue(e.RowHandle, gridColumn7, (Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, gridColumn5)) - Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, colInvoiceCGDetailQuantity))));
                    this.gridView1.SetRowCellValue(e.RowHandle, gridColumn9, Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, colInvoiceCGDetailQuantity)));
                    //if(price == decimal.Zero)            
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnZR) == null ? "0" : this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnZR).ToString(), out zherang);
                }
                if (e.Column == this.gridColumnZR)//折让后改变 金额 税额 税价和为0
                {
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out zherang);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailPrice) == null ? "0" : this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailPrice).ToString(), out price);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailQuantity) == null ? "0" : this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailQuantity).ToString(), out quantity);
                }

                //if (e.Column == this.gridColumnZS && (bool)e.Value)
                //{
                //    this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceXODetailMoney, 0);
                //    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn2, 0);
                //}

                //if (e.Column != this.gridColumnZS)
                //{
                //    this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceXODetailMoney, price * quantity - zherang);
                //    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn2, price * quantity - zherang);
                //}

                if (e.Column == this.gridColumnZS)
                {
                    //if ((bool)e.Value)
                    //{
                    this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailMoney, 0);
                    this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailPrice, 0);
                    //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTax, 0);
                    //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTaxMoney, 0);
                    //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTaxPrice, 0);
                    //}
                }

                if (e.Column != this.gridColumnZS)
                {
                    if (bool.Parse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnZS) == null ? "false" : this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnZS).ToString()) == true)
                    {
                        this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailMoney, 0);
                        //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTax, 0);
                        //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTaxMoney, 0);
                    }
                    else
                    {
                        price = this.GetDecimal(price, BL.V.SetDataFormat.CGDJXiao.Value);
                        zherang = this.GetDecimal(zherang, BL.V.SetDataFormat.CGJEXiao.Value);
                        if (flag == 0) //免税
                        {
                            this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailMoney, this.GetDecimal(price * quantity, BL.V.SetDataFormat.CGJEXiao.Value) - zherang);
                            //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTax, 0);
                            //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTaxMoney, this.GetDecimal(price * quantity, BL.V.SetDataFormat.CGJEXiao.Value) - zherang);
                            //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTaxPrice, 0);
                        }
                        if (flag == 1) //外加
                        {
                            this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailMoney, this.GetDecimal(price * quantity - zherang, BL.V.SetDataFormat.CGJEXiao.Value));
                            //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTax, this.GetDecimal(price * quantity * this.spinEditInvoiceTaxRate.Value / 100, BL.V.SetDataFormat.CGJEXiao.Value));
                            //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTaxMoney, this.GetDecimal(price * quantity - zherang + price * quantity * this.spinEditInvoiceTaxRate.Value / 100, BL.V.SetDataFormat.CGJEXiao.Value));
                            //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTaxPrice, 0);
                        }
                        if (flag == 2) //内含
                        {
                            //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceXODetailMoney, price * quantity - zherang);                   
                            //this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailTax, price * quantity * this.spinEditInvoiceTaxRate.Value / 100);
                            //this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn2, price * quantity - zherang);
                        }
                    }
                }


                price = this.GetDecimal(price, BL.V.SetDataFormat.CGDJXiao.Value);

                this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailMoney, GetDecimal(price * quantity, BL.V.SetDataFormat.CGJEXiao.Value));

                this.gridControl1.RefreshDataSource();
                this.UpdateMoneyFields();



                //Model.InvoiceCGDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceCGDetail;
                //if (detail != null)
                //{
                //    double a = 0;
                //    double.TryParse(e.Value.ToString(), out a);
                //    detail.InvoiceCGDetailQuantity = a;
                //    if (detail.NoArrivalQuantity < 0)
                //        detail.NoArrivalQuantity = 0;

                //    #region 判断进货数量是否大于未进货数量
                //    //if (e.Value != null && detail.InvoiceCGDetailQuantity > detail.NoArrivalQuantity)
                //    //{
                //    //    MessageBox.Show(Properties.Resources.InNumBigNoNum, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    //    detail.InvoiceCGDetailQuantity = 0;
                //    //}
                //    #endregion


                //}

            }
        }

        private void newChooseContorDepotId_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseContorDepotId.EditValue != null)
            {
                Model.Depot depot = this.newChooseContorDepotId.EditValue as Model.Depot;
                this.bindingSourceDepotPosition.DataSource = DepotPositionList = new BL.DepotPositionManager().Select(depot);
            }
            //if (this.action != "view" && invoicecg.Details.Count > 0 && this.bindingSourceDepotPosition.DataSource != null && !(this.bindingSourceDepotPosition.DataSource as IList<Model.DepotPosition>).Any(d => d.DepotPositionId == invoicecg.Details[0].DepotPositionId))
            //    foreach (var item in invoicecg.Details)
            //    {
            //        item.DepotPositionId = null;
            //    }
            if (DepotPositionList != null)
            {
                foreach (var item in invoicecg.Details)
                {
                    if (!DepotPositionList.Any(d => d.DepotPositionId == item.DepotPositionId))
                    {
                        item.DepotPositionId = null;
                        item.DepotPosition = null;
                    }
                }
            }
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            //if (this.newChooseContorDepotId.EditValue == null)
            //{
            //    MessageBox.Show(Properties.Resources.deptNotNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.newChooseContorDepotId.Focus();
            //    return;
            //}

            //Model.InvoiceCGDetail invoiceCGDetail = this.bindingSourceDetails.Current as Model.InvoiceCGDetail;
            //invoiceCGDetail.Depot = this.newChooseContorDepotId.EditValue as Model.Depot;

            ////IList<Model.DepotPosition> list = depotPositionManager.GetDepotPositionsByDepotAndProduct(invoiceCGDetail.ProductId, invoiceCGDetail.Depot.DepotId);

            ////if (list.Count == 0)
            ////{
            ////    MessageBox.Show("此库房没有当前的产品库存！",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
            ////    return;
            ////}

            //SettingPositionAndNumForm spf = new SettingPositionAndNumForm(invoiceCGDetail);
            //if (spf.ShowDialog() == DialogResult.OK)
            //{
            //    if (SetNums == 0)
            //        invoiceCGDetail.InvoiceCGDetailQuantity = 0;
            //    else
            //        invoiceCGDetail.InvoiceCGDetailQuantity = SetNums;
            //}

            //this.gridControl1.RefreshDataSource();
        }

        //选择采购订单
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                CGForm form = new CGForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.key != null && form.key.Count > 0)
                    {
                        if (invoicecg.Details.Count > 0 && string.IsNullOrEmpty(invoicecg.Details[0].ProductId))
                            invoicecg.Details.RemoveAt(0);
                        this.newChooseContorlSuplier.EditValue = form.key[0].Invoice.Supplier;
                        this.buttonEditEmployee.EditValue = form.key[0].Invoice.Employee0;
                        int orderId = 0;
                        foreach (Model.InvoiceCODetail detail in form.key)
                        {
                            this.cgdetail = new Book.Model.InvoiceCGDetail();
                            this.cgdetail.InvoiceCGDetailId = Guid.NewGuid().ToString();
                            this.cgdetail.Inumber = invoicecg.Details.Count + 1;
                            this.cgdetail.InvoiceCODetail = detail;
                            this.cgdetail.InvoiceCODetailId = detail.InvoiceCODetailId;
                            this.cgdetail.Invoice = invoicecg;
                            this.cgdetail.InvoiceId = detail.InvoiceId;
                            this.cgdetail.InvoiceCOId = detail.InvoiceId;
                            this.cgdetail.InvoiceCGDetailNote = string.Empty;
                            this.cgdetail.Product = detail.Product;
                            this.cgdetail.ProductId = detail.ProductId;
                            this.cgdetail.InvoiceProductUnit = detail.InvoiceProductUnit;
                            this.cgdetail.InvoiceCGDetailPrice = detail.InvoiceCODetailPrice;
                            //if (detail.YiPrice != detail.InvoiceCODetailMoney)
                            //{
                            //    this.cgdetail.InvoiceCGDetailQuantity = detail.OrderQuantity;
                            //    this.cgdetail.InvoiceCGDetailMoney = detail.InvoiceCODetailMoney;
                            //}
                            //else
                            //{
                            //    this.cgdetail.InvoiceCGDetailQuantity = 0;
                            //    this.cgdetail.InvoiceCGDetailMoney = 0;
                            //}
                            this.cgdetail.InvoiceCGDetailQuantity = 0;
                            this.cgdetail.InvoiceCGDetailMoney = 0;
                            this.cgdetail.InvoiceCGDetaiInQuantity = 0;
                            this.cgdetail.ProduceTransferQuantity = 0;
                            this.cgdetail.InvoiceAllowance = 0;
                            //this.cgdetail.InvoiceCGDetailTax = 0;
                            //this.cgdetail.InvoiceCGDetailTaxMoney = 0;
                            this.cgdetail.Donatetowards = false;
                            this.cgdetail.ORDERId = orderId;
                            this.cgdetail.Remark = detail.Remark;

                            this.cgdetail.HandbookId = detail.HandbookId;
                            this.cgdetail.HandbookProductId = detail.HandbookProductId;
                            invoicecg.Details.Add(this.cgdetail);
                            orderId++;
                        }
                        //   this.bindingSourceDetails.DataSource = invoicecg.Details;
                        this.gridControl1.RefreshDataSource();
                    }
                }
                form.Dispose();
                GC.Collect();
            }
        }

        private void simpleButtonOver_Click(object sender, EventArgs e)
        {
            //if (this.invoiceco != null)
            //{
            //    this.invoiceco.InvoiceFlag = 2;
            //    this.invoiceCOManager.Updates(this.invoiceco);

            //}
            //invoicecg.CaseClosed = true;
            //invoiceManager.Updates(invoicecg);

            //MessageBox.Show(Properties.Resources.AlReadyOk, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void spinEditInvoiceTaxRate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int index = e.Button.Index - 1;
            switch (index)
            {
                case 1:
                    flag = 1;
                    TaxMethod();
                    break;
                case 2:
                    flag = 2;
                    TaxMethod();
                    break;
                default:
                    flag = 0;
                    TaxMethod();
                    //this.spinEditInvoiceTaxRate.Properties.Buttons[1].Enabled = false;
                    //this.spinEditInvoiceTaxRate.Properties.Buttons[2].Enabled = true;
                    //this.spinEditInvoiceTaxRate.Properties.Buttons[3].Enabled = true;
                    break;
            }

            this.gridControl1.RefreshDataSource();
        }

        private void TaxMethod()
        {
            string message = "";
            if (flag == 0)
                message = "免稅";// Properties.Resources.WaiJiaShui;
            else if (flag == 1)
                message = Properties.Resources.WaiJiaShui;
            else
                message = Properties.Resources.NeiHanShui;
            if (MessageBox.Show(message, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
                return;
            double taxrate = double.Parse(this.spinEditInvoiceTaxRate.Text); //阭薹
            double ta = (taxrate + 100) / 100;

            foreach (Model.InvoiceCGDetail detail in invoicecg.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if (detail.Donatetowards != null && detail.Donatetowards == true) continue;
                if (detail.InvoiceCGDetailPrice == 0 || detail.InvoiceCGDetailQuantity == 0) continue;
                if (flag == 0)
                {
                    // detail.InvoiceCGDetailTaxPrice = 0;
                    // detail.InvoiceCGDetailTax = 0;
                    // detail.InvoiceCGDetailTaxMoney = detail.InvoiceCGDetailMoney;
                    this.spinEditInvoiceTaxRate.EditValue = 0;

                }
                else if (flag == 1)
                {
                    //    detail.InvoiceCGDetailTaxPrice = 0;
                    //    detail.InvoiceCGDetailTax = detail.InvoiceCGDetailPrice.Value * decimal.Parse(detail.InvoiceCGDetailQuantity.ToString()) * decimal.Parse(this.spinEditInvoiceTaxRate.Text) / 100;
                    //    detail.InvoiceCGDetailTax = this.GetDecimal(detail.InvoiceCGDetailTax.Value, BL.V.SetDataFormat.CGJEXiao.Value);
                    //    detail.InvoiceCGDetailTaxMoney = detail.InvoiceCGDetailTax + detail.InvoiceCGDetailMoney;
                }
                else
                {
                    //暂未考虑内含税
                    //detail.InvoiceCODetailPrice = detail.TotalMoney / decimal.Parse(detail.OrderQuantity.ToString()) / decimal.Parse(ta.ToString());
                }

                // detail.InvoiceCODetailMoney = decimal.Parse(detail.OrderQuantity.ToString()) * detail.InvoiceCODetailPrice;
            }
            this.spinEditInvoiceTaxRate.Properties.Buttons[1].Enabled = flag == 0 ? false : true;
            this.spinEditInvoiceTaxRate.Properties.Buttons[2].Enabled = flag == 1 ? false : true;
            this.spinEditInvoiceTaxRate.Properties.Buttons[3].Enabled = flag == 2 ? false : true;
            this.UpdateMoneyFields();
        }

        private void UpdateMoneyFields()
        {
            decimal yse = 0;//合计       
            decimal tol = 0;
            decimal tax = 0;
            foreach (Model.InvoiceCGDetail detail in invoicecg.Details)
            {
                if (detail.InvoiceCGDetailMoney == null)
                    detail.InvoiceCGDetailMoney = 0;
                yse += detail.InvoiceCGDetailMoney.Value;
                //tax += detail.InvoiceCGDetailTax.HasValue ? detail.InvoiceCGDetailTax.Value : 0;
                //tol = yse + tax - (this.calcInvoiceAllowance.EditValue == null ? 0 : this.calcInvoiceAllowance.Value);

            }
            yse = this.GetDecimal(yse, BL.V.SetDataFormat.CGZJXiao.Value);
            if (this.action != "view")
            {
                if (flag == 0)
                {
                    this.calcEditInvoiceHeji.EditValue = yse;
                    this.calcEditInvoiceTax.EditValue = 0;
                    this.calcEditInvoiceTotal.EditValue = yse;
                    this.spinEditInvoiceTaxRate.EditValue = 0;
                    this.comboBoxEditInvoiceKslb.SelectedIndex = 0;
                }
                else if (flag == 1)
                {
                    this.calcEditInvoiceHeji.EditValue = yse;
                    this.calcEditInvoiceTax.EditValue = this.GetDecimal(yse * this.spinEditInvoiceTaxRate.Value / 100, BL.V.SetDataFormat.CGZJXiao.Value);
                    this.calcEditInvoiceTotal.EditValue = this.GetDecimal(yse + decimal.Parse(this.calcEditInvoiceTax.EditValue.ToString()) - this.calcInvoiceAllowance.Value, BL.V.SetDataFormat.CGZJXiao.Value);
                    this.comboBoxEditInvoiceKslb.SelectedIndex = 1;
                }
                else
                {
                    this.comboBoxEditInvoiceKslb.SelectedIndex = 2;
                }
                this.spinEditInvoiceFpje.EditValue = this.calcEditInvoiceTotal.EditValue;
            }
        }

        //判断当前登录的所属角色集合
        private Model.Role SelectOperatorKeyTag(Model.Operators mOperators)
        {
            IList<Model.Role> roleList = new BL.RoleManager().Select(mOperators.OperatorsId);
            if (roleList == null || roleList.Count == 0) return null;
            Model.Role mRole = new Book.Model.Role();
            mRole.IsCOCount = false;
            mRole.IsCOPrice = false;
            mRole.IsEmployeeBasicInfo = false;
            mRole.IsProductCost = false;
            mRole.IsSalaryViewCalc = false;
            mRole.IsStockCount = false;
            mRole.IsStockPrice = false;
            mRole.IsXOPrice = false;
            mRole.IsXOQuantity = false;

            mRole.IsCOJiaoYiMingXi = false;
            mRole.IsCOFaPiaoZiLiao = false;
            mRole.IsCOZhangKuanZiLiao = false;
            mRole.IsCOXiangGuanZiLiao = false;
            mRole.IsCOJinHuoJinE = false;
            mRole.IsXOJiaoYiMingXi = false;
            mRole.IsXOFaPiaoZiLiao = false;
            mRole.IsXOZhangKuanZiLiao = false;
            mRole.IsXOXiangGuanZiLiao = false;
            mRole.IsXOJinHuoJinE = false;

            foreach (Model.Role item in roleList)
            {
                if (item.IsCOCount.HasValue && item.IsCOCount.Value)
                    mRole.IsCOCount = true;
                if (item.IsCOPrice.HasValue && item.IsCOPrice.Value)
                    mRole.IsCOPrice = true;
                if (item.IsEmployeeBasicInfo.HasValue && item.IsEmployeeBasicInfo.Value)
                    mRole.IsEmployeeBasicInfo = true;
                if (item.IsProductCost.HasValue && item.IsProductCost.Value)
                    mRole.IsProductCost = true;
                if (item.IsSalaryViewCalc.HasValue && item.IsSalaryViewCalc.Value)
                    mRole.IsSalaryViewCalc = true;
                if (item.IsStockCount.HasValue && item.IsStockCount.Value)
                    mRole.IsStockCount = true;
                if (item.IsStockPrice.HasValue && item.IsStockPrice.Value)
                    mRole.IsStockPrice = true;
                if (item.IsXOPrice.HasValue && item.IsXOPrice.Value)
                    mRole.IsXOPrice = true;
                if (item.IsXOQuantity.HasValue && item.IsXOQuantity.Value)
                    mRole.IsXOQuantity = true;
                if (item.IsCOJiaoYiMingXi.HasValue && item.IsCOJiaoYiMingXi.Value)
                    mRole.IsCOJiaoYiMingXi = true;
                if (item.IsCOFaPiaoZiLiao.HasValue && item.IsCOFaPiaoZiLiao.Value)
                    mRole.IsCOFaPiaoZiLiao = true;
                if (item.IsCOZhangKuanZiLiao.HasValue && item.IsCOZhangKuanZiLiao.Value)
                    mRole.IsCOZhangKuanZiLiao = true;
                if (item.IsCOXiangGuanZiLiao.HasValue && item.IsCOXiangGuanZiLiao.Value)
                    mRole.IsCOXiangGuanZiLiao = true;
                if (item.IsXOJiaoYiMingXi.HasValue && item.IsXOJiaoYiMingXi.Value)
                    mRole.IsXOJiaoYiMingXi = true;
                if (item.IsXOFaPiaoZiLiao.HasValue && item.IsXOFaPiaoZiLiao.Value)
                    mRole.IsXOFaPiaoZiLiao = true;
                if (item.IsXOZhangKuanZiLiao.HasValue && item.IsXOZhangKuanZiLiao.Value)
                    mRole.IsXOZhangKuanZiLiao = true;
                if (item.IsXOXiangGuanZiLiao.HasValue && item.IsXOXiangGuanZiLiao.Value)
                    mRole.IsXOXiangGuanZiLiao = true;
                if (item.IsCOJinHuoJinE.HasValue && item.IsCOJinHuoJinE.Value)
                    mRole.IsCOJinHuoJinE = true;
                if (item.IsXOJinHuoJinE.HasValue && item.IsXOJinHuoJinE.Value)
                    mRole.IsXOJinHuoJinE = true;
            }
            return mRole;
        }

        private void spinEditInvoiceZKE_EditValueChanged(object sender, EventArgs e)
        {
            this.spinEditInvoiceOwed.EditValue = decimal.Parse(this.calcEditInvoiceTotal.EditValue.ToString()) - this.spinEditInvoiceZKE.Value - this.spinEditYifu.Value;
        }

        private void spinEditYifu_EditValueChanged(object sender, EventArgs e)
        {
            this.spinEditInvoiceOwed.EditValue = decimal.Parse(this.calcEditInvoiceTotal.EditValue.ToString()) - this.spinEditInvoiceZKE.Value - this.spinEditYifu.Value;
        }

        private void dateEditInvoiceDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.dateEditInvoiceDate.DateTime != null && this.action == "insert")
                invoicecg.InvoiceId = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime);
            this.textEditInvoiceId.Text = invoicecg.InvoiceId;
        }

        private void spinEditInvoiceTaxRate_EditValueChanged(object sender, EventArgs e)
        {
            foreach (var detail in invoicecg.Details)
            {
                detail.InvoiceCGDetailTaxPrice = 0;
                detail.InvoiceCGDetailTax = (detail.InvoiceCGDetailPrice.HasValue ? detail.InvoiceCGDetailPrice.Value : 0) * decimal.Parse(detail.InvoiceCGDetailQuantity.ToString()) * decimal.Parse(this.spinEditInvoiceTaxRate.Text) / 100;
                detail.InvoiceCGDetailTax = this.GetDecimal(detail.InvoiceCGDetailTax.Value, BL.V.SetDataFormat.CGJEXiao.Value);
                detail.InvoiceCGDetailTaxMoney = detail.InvoiceCGDetailTax + detail.InvoiceCGDetailMoney;
            }
            this.UpdateMoneyFields();
            this.gridControl1.RefreshDataSource();
        }

    }
}

#region 备注留用
/*private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
{
    bool isZS = false;
    decimal total1 = decimal.Zero;
    decimal quantity = decimal.Zero;
    decimal price = decimal.Zero;

    if (e.Column == this.colInvoiceCGDetailZS || e.Column == this.colInvoiceCGDetailPrice || e.Column == this.colInvoiceCGDetailQuantity)
    {
        decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailQuantity).ToString(), out quantity);
        decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailPrice).ToString(), out price);
        bool.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCGDetailZS).ToString(), out isZS);

        total1 = price * quantity;

        this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailMoney1, Math.Round(total1));
        this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailMoney0, Math.Round(total1));

        if (isZS)
        {
            this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailDiscount, decimal.Zero);
            this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCGDetailDiscountRate, decimal.Zero);
        }
    }
    flag = 0;
    this.spinEditInvoiceTaxRate1.Properties.Buttons[1].Enabled = true;
    this.spinEditInvoiceTaxRate1.Properties.Buttons[2].Enabled = true;
    this.UpdateMoneyFields();
}*/

/*public static ChooseSuppliers GetChooseSuppliersForm()
{
    return new ChooseSuppliers();
}
 */

/*private void gridView1_RowCountChanged(object sender, EventArgs e)
//{
//    this.UpdateMoneyFields();
}*/
#endregion