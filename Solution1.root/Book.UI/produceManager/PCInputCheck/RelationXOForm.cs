using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.PCInputCheck
{
    public partial class RelationXOForm : Settings.BasicData.BaseEditForm
    {
        public Model.RelationXO _relationXO;
        BL.RelationXOManager manager = new Book.BL.RelationXOManager();

        int LastFlag = 0;

        public RelationXOForm()
        {
            InitializeComponent();
            this.invalidValueExceptions.Add(Model.RelationXO.PRO_RelationXOId, new AA(Properties.Resources.RequireCusXoId, this.txt_InvoiceCusId));
            this.action = "view";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.nccEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
        }

        public RelationXOForm(string invoiceXOCusId)
            : this()
        {
            this._relationXO = this.manager.SelectByInvoiceXOCusId(invoiceXOCusId);
            //if (this._relationXO != null)  //如果为Null，则提示
            this.LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._relationXO = new Book.Model.RelationXO();
            this._relationXO.RelationXOId = Guid.NewGuid().ToString();
            this._relationXO.Employee = BL.V.ActiveOperator.Employee;

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._relationXO);
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._relationXO);
        }

        protected override void MoveFirst()
        {
            this._relationXO = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._relationXO = this.manager.GetLast();
        }

        protected override void MovePrev()
        {
            Model.RelationXO p = this.manager.GetPrev(this._relationXO);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._relationXO = p;
        }

        protected override void MoveNext()
        {
            Model.RelationXO p = this.manager.GetNext(this._relationXO);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._relationXO = p;
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            this._relationXO.InvoiceCusId = this.txt_InvoiceCusId.Text;
            this._relationXO.InvoiceXOId = this.txt_InvoiceXOId.Text;
            this._relationXO.EmployeeId = this.nccEmp.EditValue == null ? null : (this.nccEmp.EditValue as Model.Employee).EmployeeId;

            if (this.manager.ExistsXO(this._relationXO.InvoiceCusId, this._relationXO.RelationXOId))
                throw new Helper.MessageValueException("已存在相同的客戶訂單號碼！");
            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._relationXO);
                    break;
                case "update":
                    this.manager.Update(this._relationXO);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._relationXO == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._relationXO = this.manager.GetDetail(this._relationXO.RelationXOId);
            }
            this.txt_InvoiceXOId.EditValue = this._relationXO.InvoiceXOId;
            //this.txt_InvoiceCusId.EditValue = this._relationXO.InvoiceCusId;
            Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(this._relationXO.InvoiceXOId);
            if (xo != null)
                this.txt_InvoiceCusId.EditValue = xo.CustomerInvoiceXOId;
            else
                this.txt_InvoiceCusId.EditValue = null;
            this.nccEmp.EditValue = this._relationXO.Employee;

            this.bindingSourceDetail.DataSource = this._relationXO.Detail;

            base.Refresh();
            this.txt_InvoiceCusId.Properties.ReadOnly = true;
            this.txt_InvoiceXOId.Properties.ReadOnly = true;
            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
            }
            this.btn_Search.Enabled = true;
        }

        protected override void Delete()
        {
            if (this._relationXO == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.RelationXO model = this.manager.GetNext(this._relationXO);
                this.manager.Delete(this._relationXO.RelationXOId);
                if (model == null)
                    this._relationXO = this.manager.GetLast();
                else
                    this._relationXO = model;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            ListForm f = new ListForm(2);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.keys != null && f.keys.Count > 0)
                {
                    Model.RelationXODetail detail;
                    foreach (var item in f.keys)
                    {
                        detail = new Book.Model.RelationXODetail();
                        detail.RelationXODetailId = Guid.NewGuid().ToString();
                        detail.RelationXOId = this._relationXO.RelationXOId;
                        detail.PCInputCheckId = item.PCInputCheckId;
                        detail.LotNumber = item.LotNumber;
                        detail.ProductName = item.Product == null ? null : item.Product.ProductName;
                        detail.InvoiceXOCusId = item.InvoiceXOCusId;

                        if (this._relationXO.Detail != null && this._relationXO.Detail.Count > 0)
                        {
                            if (this._relationXO.Detail.Any(d => d.PCInputCheckId == detail.PCInputCheckId))
                            {
                                MessageBox.Show(detail.PCInputCheckId + "已存在，請勿重複添加！", this.Text, MessageBoxButtons.OK);
                                continue;
                            }
                        }

                        this._relationXO.Detail.Add(detail);
                    }
                }
            }

            this.gridControl1.RefreshDataSource();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Invoices.XS.SearcharInvoiceXSForm f = new Book.UI.Invoices.XS.SearcharInvoiceXSForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.key != null && f.key.Count > 0)
                {
                    this.txt_InvoiceCusId.EditValue = f.key[0].Invoice.CustomerInvoiceXOId;
                    this.txt_InvoiceXOId.EditValue = f.key[0].InvoiceId;
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                string id = (this.bindingSourceDetail.Current as Model.RelationXODetail).PCInputCheckId;
                EditForm f = new EditForm(id);
                f.ShowDialog();
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            SearchForm f = new SearchForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.RelationXO model = this.manager.SelectByInvoiceXOCusId(f.InvoiceCusId);
                if (model != null)
                {
                    this._relationXO = model;
                    this.Refresh();
                }
                else
                {
                    MessageBox.Show("沒有符合條件的項!", this.Text, MessageBoxButtons.OK);
                }
            }
        }
    }
}