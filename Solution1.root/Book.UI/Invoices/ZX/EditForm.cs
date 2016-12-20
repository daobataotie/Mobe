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

namespace Book.UI.Invoices.ZX
{
    public partial class EditForm : BaseEditForm
    {
        Model.InvoicePacking _invoicePacking = new Book.Model.InvoicePacking();
        BL.InvoicePackingManager _invoicePackingManager = new Book.BL.InvoicePackingManager();
        BL.InvoicePackingDetailManager _invoicePackingDetailManager = new Book.BL.InvoicePackingDetailManager();
        BL.CustomerMarksManager customerMarksManager = new Book.BL.CustomerMarksManager();

        /// <summary>
        /// 成箱数量
        /// </summary>
        //double? PackingSpecification = 0.0;
        //double? _JWeight = 0;
        //double? _MWeight = 0;
        //double? _Caiji = 0;
        //int Id = 65;

        public EditForm()
        {
            InitializeComponent();
            this.bindingSourceCompany.DataSource = (new BL.CompanyManager()).Select();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlConsignee.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.requireValueExceptions.Add(Model.InvoicePacking.PRO_InvoiceNO, new AA(Properties.Resources.NoIsNotNull, this.txt_NO));
            this.requireValueExceptions.Add(Model.InvoicePacking.PRO_InvoicePackingDate, new AA(Properties.Resources.DateIsNull, this.Date_PackingDate));
            this.requireValueExceptions.Add(Model.InvoicePackingDetail.PRO_HandPackingId, new AA(Properties.Resources.HandIdNotNull, this.gridControl1));

            this.action = "view";
        }

        int LastFlag = 0; //页面载 入时是否执行 last方法
        public EditForm(string invoiceId)
            : this()
        {
            this._invoicePacking = this._invoicePackingManager.Get(invoiceId);
            if (this._invoicePacking == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.InvoicePacking invoice)
            : this()
        {
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this._invoicePacking = invoice;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public override Book.Model.Invoice Invoice
        {
            get
            {
                return _invoicePacking;
            }
            set
            {
                if (value is Model.InvoicePacking)
                {
                    _invoicePacking = _invoicePackingManager.Get((value as Model.InvoicePacking).InvoicePackingId);
                }
            }
        }

        protected override void AddNew()
        {
            this._invoicePacking = new Book.Model.InvoicePacking();
            this._invoicePacking.InvoicePackingId = this._invoicePackingManager.GetId();
            this._invoicePackingManager.TiGuiExists(this._invoicePacking);
            this._invoicePacking.Details = new List<Model.InvoicePackingDetail>();

            this.action = "insert";
        }

        public override void Refresh()
        {
            if (this._invoicePacking == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._invoicePacking = this._invoicePackingManager.Get(this._invoicePacking.InvoicePackingId);
            }

            this.txt_NO.EditValue = this._invoicePacking.InvoiceNO;
            this.Date_PackingDate.EditValue = this._invoicePacking.InvoicePackingDate;
            this.txt_InvoiceOf.EditValue = this._invoicePacking.InvoiceOf;
            this.txt_Messrs.EditValue = this._invoicePacking.Messrs;
            this.lookUpEditSHIPPED.EditValue = this._invoicePacking.ShippedById;
            this.newChooseContorlConsignee.EditValue = this._invoicePacking.CONSIGNEE;
            this.txt_ADDRESS1.EditValue = this._invoicePacking.ADDRESS1;
            this.txt_ADDRESS2.EditValue = this._invoicePacking.ADDRESS2;
            this.txt_Sailing.EditValue = this._invoicePacking.Sailing;
            this.txt_TotalAmount.EditValue = this._invoicePacking.TotalAmount.ToString();

            this.newChooseContorlAuditEmp.EditValue = this._invoicePacking.AuditEmp;
            this.txt_AuditState.EditValue = this._invoicePacking.AuditStateName;

            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
            }

            this._invoicePacking.Details = this._invoicePackingDetailManager.SelectByInvoicePackingId(this._invoicePacking.InvoicePackingId);
            this.bindingSourceDetail.DataSource = this._invoicePacking.Details;

            this._invoicePacking.Marks = this.customerMarksManager.SelectByInvoicePackingId(this._invoicePacking.InvoicePackingId);
            this.bindingSourceMarks.DataSource = this._invoicePacking.Marks;
            base.Refresh();
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._invoicePacking = this._invoicePackingManager.GetLast();
        }

        protected override void MoveFirst()
        {
            this._invoicePacking = this._invoicePackingManager.GetFirst();
        }

        protected override void MovePrev()
        {
            Model.InvoicePacking invoicePacking = this._invoicePackingManager.GetPrev(this._invoicePacking);
            if (invoicePacking == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._invoicePacking = invoicePacking;
        }

        protected override void MoveNext()
        {
            Model.InvoicePacking invoicePacking = this._invoicePackingManager.GetNext(this._invoicePacking);
            if (invoicePacking == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._invoicePacking = invoicePacking;
        }

        protected override bool HasRows()
        {
            return this._invoicePackingManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this._invoicePackingManager.HasRowsBefore(this._invoicePacking);
        }

        protected override bool HasRowsNext()
        {
            return this._invoicePackingManager.HasRowsAfter(this._invoicePacking);
        }

        protected override void Delete()
        {
            if (this._invoicePacking == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this._invoicePackingManager.Delete(this._invoicePacking.InvoicePackingId);
                this._invoicePacking = this._invoicePackingManager.GetNext(this._invoicePacking);
                if (this._invoicePacking == null)
                    this._invoicePacking = this._invoicePackingManager.GetLast();
            }
        }

        protected override void TurnNull()
        {
            if (this._invoicePacking == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this._invoicePackingManager.Delete(this._invoicePacking.InvoicePackingId);
                this._invoicePacking = this._invoicePackingManager.GetNext(this._invoicePacking);
                if (this._invoicePacking == null)
                    this._invoicePacking = this._invoicePackingManager.GetLast();
            }
        }

        protected override void Save(Helper.InvoiceStatus status)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;
            this._invoicePacking.InvoiceNO = this.txt_NO.Text;
            this._invoicePacking.InvoicePackingDate = this.Date_PackingDate.EditValue == null ? DateTime.Now.Date : this.Date_PackingDate.DateTime;
            this._invoicePacking.InvoiceOf = this.txt_InvoiceOf.Text;
            this._invoicePacking.Messrs = this.txt_Messrs.Text;
            this._invoicePacking.ShippedById = this.lookUpEditSHIPPED.EditValue == null ? null : this.lookUpEditSHIPPED.EditValue.ToString();
            this._invoicePacking.CONSIGNEEId = this.newChooseContorlConsignee.EditValue == null ? null : (this.newChooseContorlConsignee.EditValue as Model.Customer).CustomerId;
            this._invoicePacking.ADDRESS1 = this.txt_ADDRESS1.Text;
            this._invoicePacking.ADDRESS2 = this.txt_ADDRESS2.Text;
            this._invoicePacking.Sailing = this.txt_Sailing.Text;
            this._invoicePacking.TotalAmount = Convert.ToDouble(this.txt_TotalAmount.EditValue);

            if (this.action == "insert")
                this._invoicePackingManager.Insert(this._invoicePacking);
            if (this.action == "update")
                this._invoicePackingManager.Update(this._invoicePacking);
        }

        public override BaseListForm GetListForm()
        {
            return new ZX.List();
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            DevExpress.XtraReports.UI.XtraReport ro1 = new ROInvoice(this._invoicePacking);
            DevExpress.XtraReports.UI.XtraReport ro2 = new ROPackingList(this._invoicePacking);
            ro1.ShowPreview();
            ro2.ShowPreview();
            return null;
        }

        /// <summary>
        /// 添加客户订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            XS.SearcharInvoiceXSForm f = new Book.UI.Invoices.XS.SearcharInvoiceXSForm();
            Model.CustomerProducts cp = null;
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.key != null && f.key.Count > 0)
                {
                    Model.CustomerMarks mark = null;
                    Model.InvoicePackingDetail packingDetail = null;
                    //if (this._invoicePacking.Details != null && this._invoicePacking.Details.Count > 1)
                    //Id = 65 + this._invoicePacking.Details.Count;
                    foreach (Model.InvoiceXODetail detail in f.key)
                    {
                        if (this._invoicePacking.Details.Where(d => d.InvoiceXODetailId == detail.InvoiceXODetailId).ToList().Count > 0)
                        {
                            MessageBox.Show("已存在相同的訂單！", this.Text, MessageBoxButtons.OK);
                            continue;
                        }
                        packingDetail = new Book.Model.InvoicePackingDetail();
                        //packingDetail.InvoicePackingDetailId = this._invoicePackingDetailManager.GetId();
                        //this._invoicePackingDetailManager.TiGuiExists(packingDetail);
                        packingDetail.InvoicePackingDetailId = Guid.NewGuid().ToString();
                        packingDetail.InvoicePackingId = this._invoicePacking.InvoicePackingId;
                        packingDetail.HandbookId = detail.HandbookId;
                        packingDetail.HandbookProductId = detail.HandbookProductId;
                        packingDetail.Product = detail.Product;
                        packingDetail.ProductId = detail.ProductId;
                        packingDetail.Customer = detail.Invoice.Customer;
                        packingDetail.CustomerId = detail.Invoice.CustomerId;
                        packingDetail.XOCustomer = detail.Invoice.xocustomer;
                        packingDetail.XOCustomerId = detail.Invoice.xocustomerId;
                        packingDetail.UnitPrice = detail.InvoiceXODetailPrice;
                        packingDetail.InvoiceXOQuantity = detail.InvoiceXODetailQuantity0 == null ? 0 : detail.InvoiceXODetailQuantity0;
                        packingDetail.PackingNum = packingDetail.InvoiceXOQuantity;
                        packingDetail.HasFPQuantity = 0;
                        packingDetail.Amount = Convert.ToDecimal(packingDetail.PackingNum) * packingDetail.UnitPrice;
                        packingDetail.InvoiceXOId = detail.InvoiceId;
                        packingDetail.InvoiceXODetailId = detail.InvoiceXODetailId;
                        packingDetail.ProductUnit = detail.InvoiceProductUnit;
                        packingDetail.WeightUnit = "KGS";
                        packingDetail.PriceUnit = "USD";
                        packingDetail.BGHandBookProduct = new BL.BGHandbookDetail1Manager().SelectProName(detail.HandbookId, detail.HandbookProductId);

                        if (detail.Invoice.CustomerMarks != null && this._invoicePacking.Marks.Where(d => d.CustomerMarksName == detail.Invoice.CustomerMarks).ToList().Count < 1)
                        {
                            mark = new Book.Model.CustomerMarks();
                            mark.CustomerMarksId = Guid.NewGuid().ToString();
                            mark.InvoicePackingId = this._invoicePacking.InvoicePackingId;
                            mark.CustomerMarksName = detail.Invoice.CustomerMarks;

                            this._invoicePacking.Marks.Add(mark);
                        }
                        if (packingDetail.Product != null && !(string.IsNullOrEmpty(packingDetail.Product.CustomerProductName)) && !(string.IsNullOrEmpty(packingDetail.Product.CustomerId)))
                        {
                            cp = new BL.CustomerProductsManager().SelectByCustomerProductProceId(packingDetail.ProductId);
                        }
                        if (cp != null)
                        {
                            packingDetail.BLong = cp.BLong;
                            packingDetail.BWidth = cp.BWide;
                            packingDetail.BHeight = cp.BHigh;
                            packingDetail.UnitJWeight = cp.JWeight;
                            packingDetail.UnitMWeight = cp.MWeight;
                            packingDetail.UnitCaiji = cp.Caiji;
                            packingDetail.UnitNum = cp.PackingSpecification;
                            if (packingDetail.UnitNum > packingDetail.InvoiceXOQuantity)
                            {
                                packingDetail.BoxNum = 1;
                                packingDetail.AllJweight = Math.Round((double)(packingDetail.UnitJWeight * packingDetail.InvoiceXOQuantity / packingDetail.UnitNum), 4);
                                packingDetail.AllMWeight = Math.Round((double)(packingDetail.UnitMWeight * packingDetail.InvoiceXOQuantity / packingDetail.UnitNum), 4);
                                packingDetail.AllCaiji = Math.Round((double)(packingDetail.UnitCaiji * packingDetail.InvoiceXOQuantity / packingDetail.UnitNum), 4);

                                packingDetail.PackingNum = packingDetail.InvoiceXOQuantity == null ? 0 : packingDetail.InvoiceXOQuantity;
                                packingDetail.Amount = Convert.ToDecimal(packingDetail.PackingNum) * packingDetail.UnitPrice;
                            }
                            else
                            {
                                packingDetail.BoxNum = Convert.ToInt32(Math.Truncate(Convert.ToDouble(detail.InvoiceXODetailQuantity0 / packingDetail.UnitNum)));
                                packingDetail.AllJweight = packingDetail.UnitJWeight * packingDetail.BoxNum;
                                packingDetail.AllMWeight = packingDetail.UnitMWeight * packingDetail.BoxNum;
                                packingDetail.AllCaiji = packingDetail.UnitCaiji * packingDetail.BoxNum;

                                packingDetail.PackingNum = (packingDetail.UnitNum * packingDetail.BoxNum) == null ? packingDetail.InvoiceXOQuantity : packingDetail.UnitNum * packingDetail.BoxNum;
                                packingDetail.Amount = Convert.ToDecimal(packingDetail.PackingNum) * packingDetail.UnitPrice;
                            }
                        }
                        //if (packingDetail.UnitNum == 0 || packingDetail.UnitNum == null)
                        //{
                        //    MessageBox.Show("請設置該產品的成箱數量！\r" + (packingDetail.Product == null ? null : packingDetail.Product), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    //return;
                        //}
                        //else
                        //{
                        //    packingDetail.BoxNum = (int)(detail.InvoiceXODetailQuantity0 / this.PackingSpecification);
                        //    //packingDetail.HandPackingId = Convert.ToChar(Id).ToString() + " 1" + "-" + packingDetail.BoxNum.ToString();
                        //    packingDetail.PackingNum = this.PackingSpecification * packingDetail.BoxNum;
                        //    packingDetail.AllJweight = packingDetail.UnitJWeight * packingDetail.BoxNum;
                        //    packingDetail.AllMWeight = packingDetail.UnitMWeight * packingDetail.BoxNum;
                        //    packingDetail.AllCaiji = packingDetail.UnitCaiji * packingDetail.BoxNum;
                        //}

                        this._invoicePacking.Details.Add(packingDetail);

                        if (packingDetail.UnitNum > 0 && packingDetail.InvoiceXOQuantity % packingDetail.UnitNum != 0 && packingDetail.UnitNum < packingDetail.InvoiceXOQuantity)
                        {
                            Model.InvoicePackingDetail invoicePackingDetail = new Book.Model.InvoicePackingDetail();
                            //invoicePackingDetail.InvoicePackingDetailId = this._invoicePackingDetailManager.GetId();
                            //this._invoicePackingDetailManager.TiGuiExists(invoicePackingDetail);
                            invoicePackingDetail.InvoicePackingDetailId = Guid.NewGuid().ToString();
                            invoicePackingDetail.InvoicePackingId = this._invoicePacking.InvoicePackingId;
                            invoicePackingDetail.HandbookId = detail.HandbookId;
                            invoicePackingDetail.HandbookProductId = detail.HandbookProductId;
                            invoicePackingDetail.Product = packingDetail.Product;
                            invoicePackingDetail.ProductId = packingDetail.ProductId;
                            invoicePackingDetail.Customer = packingDetail.Customer;
                            invoicePackingDetail.CustomerId = packingDetail.CustomerId;
                            invoicePackingDetail.XOCustomer = packingDetail.XOCustomer;
                            invoicePackingDetail.XOCustomerId = packingDetail.XOCustomerId;
                            invoicePackingDetail.UnitPrice = packingDetail.UnitPrice;
                            invoicePackingDetail.InvoiceXOQuantity = detail.InvoiceXODetailQuantity0 == null ? 0 : detail.InvoiceXODetailQuantity0;
                            invoicePackingDetail.PackingNum = invoicePackingDetail.InvoiceXOQuantity % packingDetail.UnitNum;
                            invoicePackingDetail.HasFPQuantity = 0;
                            invoicePackingDetail.Amount = Convert.ToDecimal(invoicePackingDetail.PackingNum) * invoicePackingDetail.UnitPrice;
                            invoicePackingDetail.ProductUnit = packingDetail.ProductUnit;
                            invoicePackingDetail.BoxNum = 1;
                            invoicePackingDetail.BLong = packingDetail.BLong;
                            invoicePackingDetail.BWidth = packingDetail.BWidth;
                            invoicePackingDetail.BHeight = packingDetail.BHeight;
                            invoicePackingDetail.UnitJWeight = Math.Round((double)(packingDetail.UnitJWeight * invoicePackingDetail.PackingNum / packingDetail.UnitNum), 4);
                            invoicePackingDetail.UnitMWeight = Math.Round((double)(packingDetail.UnitMWeight * invoicePackingDetail.PackingNum / packingDetail.UnitNum), 4);
                            invoicePackingDetail.UnitCaiji = Math.Round((double)(packingDetail.UnitCaiji * invoicePackingDetail.PackingNum / packingDetail.UnitNum), 4);
                            invoicePackingDetail.AllJweight = invoicePackingDetail.UnitJWeight;
                            invoicePackingDetail.AllMWeight = invoicePackingDetail.UnitMWeight;
                            invoicePackingDetail.AllCaiji = invoicePackingDetail.UnitCaiji;
                            invoicePackingDetail.WeightUnit = "KGS";
                            invoicePackingDetail.PriceUnit = "USD";
                            invoicePackingDetail.BGHandBookProduct = packingDetail.BGHandBookProduct;

                            this._invoicePacking.Details.Add(invoicePackingDetail);
                        }
                    }
                }
                double amount = 0;
                foreach (var item in this._invoicePacking.Details)
                {
                    amount += Convert.ToDouble(item.Amount);
                }
                this.txt_TotalAmount.Text = amount.ToString();
                this.bindingSourceDetail.DataSource = _invoicePacking.Details;
                this.gridControl1.RefreshDataSource();
                this.bindingSourceDetail.MoveLast();
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Count < 1)
                return;
            Model.InvoicePackingDetail detail = this.bindingSourceDetail.Current as Model.InvoicePackingDetail;
            this._invoicePacking.Details.Remove(detail);
            this.bindingSourceDetail.Position = this.bindingSourceDetail.Count - 1;
            this.gridControl1.RefreshDataSource();
            double amount = 0;
            foreach (Model.InvoicePackingDetail model in this._invoicePacking.Details)
            {
                amount += Convert.ToDouble(model.Amount);
            }
            this.txt_TotalAmount.Text = amount.ToString();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumn6)
            {
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn19, Convert.ToDouble(e.Value) * Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn7)));
                //this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn22, Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn20)) - Convert.ToDouble(e.Value));
                if (Convert.ToDouble((this.gridView1.GetRow(e.RowHandle) as Model.InvoicePackingDetail).UnitNum) > 0)
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn18, Math.Ceiling(Convert.ToDouble(e.Value) / Convert.ToDouble((this.gridView1.GetRow(e.RowHandle) as Model.InvoicePackingDetail).UnitNum)));

                double amount = 0;
                foreach (var item in this._invoicePacking.Details)
                {
                    amount += Convert.ToDouble(item.Amount);
                }
                this.txt_TotalAmount.Text = amount.ToString();
            }
            if (e.Column == this.gridColumn11)
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn14, Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn11)));
            if (e.Column == this.gridColumn12)
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn15, Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn12)));
            if (e.Column == this.gridColumn13)
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn16, Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn13)));
        }

        private void btn_AddMark_Click(object sender, EventArgs e)
        {
            Model.CustomerMarks mark = new Book.Model.CustomerMarks();
            mark.CustomerMarksId = Guid.NewGuid().ToString();
            mark.InvoicePackingId = this._invoicePacking.InvoicePackingId;
            this._invoicePacking.Marks.Add(mark);
            this.bindingSourceMarks.Position = this.bindingSourceMarks.IndexOf(mark);
            this.gridControl2.RefreshDataSource();
        }

        private void btn_RemoveMark_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceMarks.Current != null)
            {
                Model.CustomerMarks mark = this.bindingSourceMarks.Current as Model.CustomerMarks;
                this._invoicePacking.Marks.Remove(mark);
                this.bindingSourceMarks.Position = this.bindingSourceMarks.Count - 1;
                this.gridControl2.RefreshDataSource();
            }
        }
    }

}
