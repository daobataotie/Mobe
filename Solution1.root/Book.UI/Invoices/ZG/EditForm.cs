using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.ZG
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.InvoiceZG _invoiceZG;
        BL.InvoiceZGManager _invoiceZGManager = new Book.BL.InvoiceZGManager();
        BL.InvoiceZGDetailManager _invoiceZGDetailManager = new Book.BL.InvoiceZGDetailManager();
        BL.CompanyManager companyManager = new Book.BL.CompanyManager();

        public EditForm()
        {
            InitializeComponent();

            this.bindingSourceCompany.DataSource = this.companyManager.Select();
            this.newChooseXOCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();

            this.requireValueExceptions.Add(Model.InvoiceZG.PRO_InvoiceZGDate, new AA(Properties.Resources.DateIsNull, this.date_InvoiceDate));

            this.action = "view";
        }

        int LastFlag = 0; //页面载入时是否执行 last方法
        public EditForm(string invoiceId)
            : this()
        {
            this._invoiceZG = this._invoiceZGManager.Get(invoiceId);
            if (this._invoiceZG == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.InvoiceZG invoice)
            : this()
        {
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this._invoiceZG = invoice;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public override void Refresh()
        {
            if (this._invoiceZG == null)
                AddNew();
            else
            {
                if (this.action == "view")
                    this._invoiceZG = this._invoiceZGManager.Get(_invoiceZG.InvoiceZGId);
            }

            this.txt_InvoiceId.EditValue = this._invoiceZG.InvoiceZGId;
            this.date_InvoiceDate.EditValue = this._invoiceZG.InvoiceZGDate;
            this.newChooseEmployee.EditValue = this._invoiceZG.Employee;
            this.newChooseXOCustomer.EditValue = this._invoiceZG.XOCustomer;
            if (this.newChooseXOCustomer.EditValue != null)
            {
                this.txt_ToName.EditValue = this._invoiceZG.XOCustomer.CustomerFullName;
                this.txt_ToAddress.EditValue = this._invoiceZG.XOCustomer.CustomerAddress;
            }
            else
            {
                this.txt_ToName.EditValue = null;
                this.txt_ToAddress.EditValue = null;
            }
            this.lookUpEditSHIPPED.EditValue = this._invoiceZG.ShippedBy;
            this.txt_PER.EditValue = this._invoiceZG.PerSS;
            this.txt_SO.EditValue = this._invoiceZG.SorO;
            this.txt_SHIPPED.EditValue = this._invoiceZG.ShippedOnAbout;
            this.txt_ARRIVEL.EditValue = this._invoiceZG.ArrivelOnAbout;
            this.me_From.EditValue = this._invoiceZG.AddressFrom;
            this.me_To.EditValue = this._invoiceZG.AddressTo;
            this.richTextInvoiceZGDes.Rtf = this._invoiceZG.InvoiceZGDes;


            this._invoiceZG.Details = this._invoiceZGDetailManager.SelectByInvoiceZGId(this._invoiceZG.InvoiceZGId);
            this.bindingSourceDetail.DataSource = this._invoiceZG.Details;
            base.Refresh();
        }

        protected override void AddNew()
        {
            this._invoiceZG = new Book.Model.InvoiceZG();
            this._invoiceZG.InvoiceZGId = this._invoiceZGManager.GetId();
            this._invoiceZGManager.TiGuiExists(this._invoiceZG);
            this._invoiceZG.InvoiceZGDate = DateTime.Now;
            this._invoiceZG.Details = new List<Model.InvoiceZGDetail>();
            this.action = "insert";
        }

        protected override void Delete()
        {
            if (this._invoiceZG == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this._invoiceZGManager.Delete(this._invoiceZG.InvoiceZGId);
                this._invoiceZG = this._invoiceZGManager.GetNext(this._invoiceZG);
                if (this._invoiceZG == null)
                    this._invoiceZG = this._invoiceZGManager.GetLast();
            }
        }

        protected override void Save()
        {
            this._invoiceZG.InvoiceZGDate = this.date_InvoiceDate.EditValue == null ? DateTime.Now.Date : this.date_InvoiceDate.DateTime;
            this._invoiceZG.XOCustomerId = this.newChooseXOCustomer.EditValue == null ? null : (this.newChooseXOCustomer.EditValue as Model.Customer).CustomerId;
            this._invoiceZG.EmployeeId = this.newChooseEmployee.EditValue == null ? null : (this.newChooseEmployee.EditValue as Model.Employee).EmployeeId;
            this._invoiceZG.ShippedBy = this.lookUpEditSHIPPED.EditValue == null ? null : this.lookUpEditSHIPPED.EditValue.ToString();
            this._invoiceZG.PerSS = this.txt_PER.Text;
            this._invoiceZG.SorO = this.txt_SO.Text;
            this._invoiceZG.ShippedOnAbout = this.txt_SHIPPED.Text;
            this._invoiceZG.ArrivelOnAbout = this.txt_ARRIVEL.Text;
            this._invoiceZG.AddressFrom = this.me_From.Text;
            this._invoiceZG.AddressTo = this.me_To.Text;
            this._invoiceZG.InvoiceZGDes = this.richTextInvoiceZGDes.Rtf;

            if (this.action == "insert")
                this._invoiceZGManager.Insert(this._invoiceZG);
            if (this.action == "update")
                this._invoiceZGManager.Update(this._invoiceZG);
        }

        protected override void Undo()
        {

        }

        protected override void MoveFirst()
        {
            this._invoiceZG = this._invoiceZGManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1)
            {
                LastFlag = 0;
                return;
            }
            this._invoiceZG = this._invoiceZGManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.InvoiceZG invoiceZG = this._invoiceZGManager.GetNext(this._invoiceZG);
            if (invoiceZG == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows); ;
            this._invoiceZG = invoiceZG;
        }

        protected override void MovePrev()
        {
            Model.InvoiceZG invoiceZG = this._invoiceZGManager.GetPrev(this._invoiceZG);
            if (invoiceZG == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._invoiceZG = invoiceZG;
        }

        protected override bool HasRows()
        {
            return this._invoiceZGManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._invoiceZGManager.HasRowsAfter(this._invoiceZG);
        }

        protected override bool HasRowsPrev()
        {
            return this._invoiceZGManager.HasRowsBefore(this._invoiceZG);
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void newChooseXOCustomer_EditValueChanged(object sender, EventArgs e)
        {
            Model.Customer customer = this.newChooseXOCustomer.EditValue as Model.Customer;
            if (customer != null)
            {
                this.txt_ToName.EditValue = customer.CustomerFullName;
                this.txt_ToAddress.EditValue = customer.CustomerAddress;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(_invoiceZG);
        }

        private void btnCheckInvoiceZX_Click(object sender, EventArgs e)
        {
            ZX.PackingForm f = new Book.UI.Invoices.ZX.PackingForm("Check");
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.checkedInvoicezx != null && f.checkedInvoicezx.Count > 0)
                {
                    foreach (Model.InvoiceZX zx in f.checkedInvoicezx)
                    {
                        Model.InvoiceZGDetail detail = new Book.Model.InvoiceZGDetail();
                        detail.InvoiceZGDetailId = Guid.NewGuid().ToString();
                        detail.InvoiceZGId = this._invoiceZG.InvoiceZGId;
                        detail.InvoiceZXId = zx.InvoiceZXId;
                        detail.InvoiceZX = zx;

                        //detail.InvoiceZX.PackingId = zx.PackingId;
                        //detail.InvoiceZX.InvoiceNote = zx.InvoiceNote;
                        //detail.InvoiceZX.PackingNum = zx.PackingNum;
                        //detail.InvoiceZX.BWeight = zx.BWeight;
                        //detail.InvoiceZX.AllWeight = zx.AllWeight;
                        //detail.InvoiceZX.UNITPRICE = zx.UNITPRICE;
                        this._invoiceZG.Details.Add(detail);
                    }
                }
                if (f.checkedInvoicezx != null && f.checkedInvoicezx.Count == 0)
                {
                    Model.InvoiceZX zx = f.SelectedItem as Model.InvoiceZX;
                    Model.InvoiceZGDetail detail = new Book.Model.InvoiceZGDetail();
                    detail.InvoiceZGDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceZGId = this._invoiceZG.InvoiceZGId;
                    detail.InvoiceZXId = zx.InvoiceZXId;
                    detail.InvoiceZX = zx;

                    //detail.InvoiceZX.PackingId = zx.PackingId;
                    //detail.InvoiceZX.InvoiceNote = zx.InvoiceNote;
                    //detail.InvoiceZX.PackingNum = zx.PackingNum;
                    //detail.InvoiceZX.BWeight = zx.BWeight;
                    //detail.InvoiceZX.AllWeight = zx.AllWeight;
                    //detail.InvoiceZX.UNITPRICE = zx.UNITPRICE;
                    this._invoiceZG.Details.Add(detail);
                }
            }
            this.bindingSourceDetail.DataSource = this._invoiceZG.Details;
            this.bindingSourceDetail.MoveLast();
            this.gridControl1.RefreshDataSource();
            //Refresh();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Model.InvoiceZGDetail detail = new Book.Model.InvoiceZGDetail();
            //detail.InvoiceZX = this.bindingSourceDetail.Current as Model.InvoiceZX;
            //this._invoiceZG.Details.Remove(detail);
            this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);
            this.gridControl1.RefreshDataSource();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List f = new List();
            f.ShowDialog();
        }

        //搜索

    }
}