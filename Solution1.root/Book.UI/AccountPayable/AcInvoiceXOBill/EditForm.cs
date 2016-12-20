using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.BasicData.Customs;
using System.Linq;

namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    public partial class EditForm : BaseEditForm
    {
        private BL.AcInvoiceXOBillManager _acInvoiceXoBillManager = new BL.AcInvoiceXOBillManager();
        private Model.AcInvoiceXOBill _acInvoiceXoBill;

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AcInvoiceXOBill.PRO_Id, new AA(Properties.Resources.AcInvoiceXOBillfpbh, this.XoId));
            this.requireValueExceptions.Add(Model.AcInvoiceXOBill.PRO_CustomerId, new AA(Properties.Resources.RequireDataForComtomer, this.newChooseCustomerId));
            this.requireValueExceptions.Add("AcInvoiceXOBill.Details", new AA(Properties.Resources.AcInvoiceHasDetails, this.simbtnAppend));
            this.newChooseEmployee0Id.Choose = new ChooseEmployee();
            //   this.newChooseEmployee1Id.Choose = new ChooseEmployee();
            this.newChooseEmployeeId.Choose = new ChooseEmployee();
            this.newChooseCustomerId.Choose = new ChooseCustoms();
            this.newChooseCustomer2.Choose = new ChooseCustoms();
            this.action = "view";
        }

        #region override
        protected override void MoveFirst()
        {
            this._acInvoiceXoBill = this._acInvoiceXoBillManager.Get(this._acInvoiceXoBillManager.GetFirst() == null ? "" : this._acInvoiceXoBillManager.GetFirst().AcInvoiceXOBillId);
        }

        /// <summary>
        /// 尾笔
        /// </summary>
        protected override void MoveLast()
        {
            // if (_acbeginAccountPayble == null)
            {
                this._acInvoiceXoBill = this._acInvoiceXoBillManager.Get(this._acInvoiceXoBillManager.GetLast() == null ? "" : this._acInvoiceXoBillManager.GetLast().AcInvoiceXOBillId);
            }
        }

        protected override void MovePrev()
        {
            Model.AcInvoiceXOBill temp = this._acInvoiceXoBillManager.GetPrev(this._acInvoiceXoBill);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acInvoiceXoBill = this._acInvoiceXoBillManager.Get(temp.AcInvoiceXOBillId);
        }

        protected override void MoveNext()
        {
            Model.AcInvoiceXOBill temp = this._acInvoiceXoBillManager.GetNext(this._acInvoiceXoBill);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acInvoiceXoBill = this._acInvoiceXoBillManager.Get(temp.AcInvoiceXOBillId);
        }

        /// <summary>
        /// 是否有返回行
        /// </summary>
        /// <returns></returns>
        protected override bool HasRows()
        {
            return this._acInvoiceXoBillManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._acInvoiceXoBillManager.HasRowsAfter(this._acInvoiceXoBill);
        }

        protected override bool HasRowsPrev()
        {
            return this._acInvoiceXoBillManager.HasRowsBefore(this._acInvoiceXoBill);
        }

        protected override void AddNew()
        {
            this._acInvoiceXoBill = new Model.AcInvoiceXOBill();
            this._acInvoiceXoBill.AcInvoiceXOBillId = this._acInvoiceXoBillManager.GetId(DateTime.Now);
            this._acInvoiceXoBill.AcInvoiceXOBillDate = DateTime.Now;
            this._acInvoiceXoBill.Employee = BL.V.ActiveOperator.Employee;
            this._acInvoiceXoBill.TaxRateType = 1;
            this._acInvoiceXoBill.TaxRate = 5;
            this._acInvoiceXoBill.Details = new List<Model.AcInvoiceXOBillDetail>();
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this._acInvoiceXoBillManager.Delete(this._acInvoiceXoBill);
                this._acInvoiceXoBill = this._acInvoiceXoBillManager.GetNext(this._acInvoiceXoBill);
                if (this._acInvoiceXoBill == null)
                {
                    this._acInvoiceXoBill = this._acInvoiceXoBillManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override void Save()
        {
            this._acInvoiceXoBill.AcInvoiceXOBillId = this.AcInvoiceXOBillId.Text;
            this._acInvoiceXoBill.Id = this.XoId.Text;
            this._acInvoiceXoBill.Customer = this.newChooseCustomerId.EditValue as Model.Customer;
            if (this._acInvoiceXoBill.Customer != null)
                this._acInvoiceXoBill.CustomerId = this._acInvoiceXoBill.Customer.CustomerId;
            this._acInvoiceXoBill.TaxRate = Convert.ToDouble(this.calcTaxRate.Value);
            this._acInvoiceXoBill.TaxRateMoney = this.calcTaxRateMoney.Value;
            this._acInvoiceXoBill.TaxRateType = this.TaxType.SelectedIndex;
            this._acInvoiceXoBill.HeJiMoney = this.calcHeJiMoney.Value;
            this._acInvoiceXoBill.ZongMoney = this.calcZongMoney.Value;
            this._acInvoiceXoBill.mHeXiaoJingE = this.calcmHeXiaoJingE.Value;
            this._acInvoiceXoBill.CustomerShouPiao = this.newChooseCustomer2.EditValue as Model.Customer;
            if (this.newChooseCustomer2.EditValue != null)
                this._acInvoiceXoBill.CustomerShouPiaoId = this._acInvoiceXoBill.CustomerShouPiao.CustomerId;
            this._acInvoiceXoBill.NoHeXiaoTotal = this.calcNoHeXiaoTotal.Value;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateAcInvoiceXOBillDate.DateTime, new DateTime()))
                this._acInvoiceXoBill.AcInvoiceXOBillDate = global::Helper.DateTimeParse.NullDate;
            else
                this._acInvoiceXoBill.AcInvoiceXOBillDate = this.dateAcInvoiceXOBillDate.DateTime;
            //if (global::Helper.DateTimeParse.DateTimeEquls(this.dateForYSDate.DateTime, new DateTime()))
            //    this._acInvoiceXoBill.YSDate = global::Helper.DateTimeParse.NullDate;
            //else
            //    this._acInvoiceXoBill.YSDate = this.dateForYSDate.DateTime;
            this._acInvoiceXoBill.AcInvoiceXOBillDesc = this.memoAcInvoiceXOBillDesc.Text;
            this._acInvoiceXoBill.Employee = this.newChooseEmployeeId.EditValue as Model.Employee;
            if (this._acInvoiceXoBill.Employee != null)
                this._acInvoiceXoBill.EmployeeId = this._acInvoiceXoBill.Employee.EmployeeId;
            this._acInvoiceXoBill.Employee0 = this.newChooseEmployee0Id.EditValue as Model.Employee;
            if (this._acInvoiceXoBill.Employee0 != null)
                this._acInvoiceXoBill.Employee0Id = this._acInvoiceXoBill.Employee0.EmployeeId;
            //    this._acInvoiceXoBill.Employee1 = this.newChooseEmployee1Id.EditValue as Model.Employee;
            //  if (this._acInvoiceXoBill.Employee1 != null)
            //    this._acInvoiceXoBill.Employee1Id = this._acInvoiceXoBill.Employee1.EmployeeId;

            this._acInvoiceXoBill.InvoiceStatus = 1;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._acInvoiceXoBillManager.Insert(this._acInvoiceXoBill);
                    break;

                case "update":
                    this._acInvoiceXoBillManager.Update(this._acInvoiceXoBill);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._acInvoiceXoBill == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._acInvoiceXoBill = this._acInvoiceXoBillManager.GetDetails(this._acInvoiceXoBill);
                    if (this._acInvoiceXoBill == null)
                    {
                        this._acInvoiceXoBill = new Book.Model.AcInvoiceXOBill();
                    }
                }


            }
            this.newChooseCustomer2.EditValue = this._acInvoiceXoBill.CustomerShouPiao;
            this.AcInvoiceXOBillId.Text = this._acInvoiceXoBill.AcInvoiceXOBillId;
            this.XoId.Text = this._acInvoiceXoBill.Id;
            this.memoAcInvoiceXOBillDesc.Text = this._acInvoiceXoBill.AcInvoiceXOBillDesc;
            this.dateAcInvoiceXOBillDate.DateTime = this._acInvoiceXoBill.AcInvoiceXOBillDate.Value;
            this.calcHeJiMoney.Value = this._acInvoiceXoBill.HeJiMoney == null ? 0 : this._acInvoiceXoBill.HeJiMoney.Value;
            this.TaxType.SelectedIndex = Convert.ToInt32(this._acInvoiceXoBill.TaxRateType);
            this.calcTaxRate.Value = Convert.ToDecimal(this._acInvoiceXoBill.TaxRate == null ? 0 : this._acInvoiceXoBill.TaxRate.Value);
            this.calcTaxRateMoney.Value = this._acInvoiceXoBill.TaxRateMoney == null ? 0 : this._acInvoiceXoBill.TaxRateMoney.Value;
            this.calcZongMoney.Value = this._acInvoiceXoBill.ZongMoney == null ? 0 : this._acInvoiceXoBill.ZongMoney.Value;
            this.calcmHeXiaoJingE.Value = this._acInvoiceXoBill.mHeXiaoJingE == null ? 0 : this._acInvoiceXoBill.mHeXiaoJingE.Value;
            this.calcNoHeXiaoTotal.Value = this._acInvoiceXoBill.NoHeXiaoTotal == null ? 0 : this._acInvoiceXoBill.NoHeXiaoTotal.Value;
            //  this.dateForYSDate.DateTime = this._acInvoiceXoBill.YSDate == null ? DateTime.Now : this._acInvoiceXoBill.YSDate.Value;
            this.newChooseEmployee0Id.EditValue = this._acInvoiceXoBill.AuditEmp;
            //  this.newChooseEmployee1Id.EditValue = this._acInvoiceXoBill.Employee1;
            this.newChooseEmployeeId.EditValue = this._acInvoiceXoBill.Employee;
            this.newChooseCustomerId.EditValue = this._acInvoiceXoBill.Customer;

            this.txt_AuditState.EditValue = this.GetAuditName(this._acInvoiceXoBill.AuditState);
            this.bindingSourceDetails.DataSource = this._acInvoiceXoBill.Details;

            base.Refresh();
            //if (this.action == "view")
            //{
            //    this.barBtnChuHuoDan.Enabled = false;
            //}
            //else
            //{
            //    this.barBtnChuHuoDan.Enabled = true;
            //}

            this.newChooseEmployeeId.Enabled = false;
            // this.calcHeJiMoney.Enabled = false;
            this.calcZongMoney.Enabled = false;
            this.calcTaxRateMoney.Enabled = false;
            this.calcmHeXiaoJingE.Enabled = false;
            this.calcNoHeXiaoTotal.Enabled = false;

        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Ro(this._acInvoiceXoBill);
        }

        #endregion

        private void simbtnAppend_Click(object sender, EventArgs e)
        {
            //厦门方面要求选择Invoice单
            Invoices.ZX.ChooseInvoiceForm form = new Book.UI.Invoices.ZX.ChooseInvoiceForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (form.Key != null && form.Key.Count > 0)
                {
                    List<string> idlist = this._acInvoiceXoBill.Details.Select(d => d.InvoiceXODetailId).ToList();
                    foreach (Model.InvoicePackingDetail item in form.Key)
                    {
                        if (!idlist.Contains(item.InvoiceXODetailId))
                        {
                            Model.AcInvoiceXOBillDetail detail = new Book.Model.AcInvoiceXOBillDetail();
                            detail.AcInvoiceXOBillDetailId = Guid.NewGuid().ToString();
                            detail.InvoiceXODetailId = item.InvoiceXODetailId;
                            detail.ProductId = item.ProductId;
                            detail.Product = item.Product;
                            detail.InvoicePacking = item.InvoicePacking;
                            detail.InvoicePackingId = item.InvoicePackingId;
                            detail.AcInvoiceXOBillId = this._acInvoiceXoBill.AcInvoiceXOBillId;
                            if (item.InvoiceXODetail != null)
                                detail.InvoiceAllowance = decimal.Parse((item.InvoiceXODetail.InvoiceAllowance == null ? 0 : item.InvoiceXODetail.InvoiceAllowance.Value).ToString());
                            detail.InvoiceXODetaiInQuantity = (item.PackingNum - (item.HasFPQuantity == null ? 0 : item.HasFPQuantity.Value)) < 0 ? 0 : (item.PackingNum - (item.HasFPQuantity == null ? 0 : item.HasFPQuantity.Value));
                            detail.InvoiceXODetailPrice = item.UnitPrice == null ? 0 : item.UnitPrice.Value;
                            detail.InvoiceXODetailMoney = global::Helper.DateTimeParse.GetSiSheWuRu(decimal.Parse(detail.InvoiceXODetaiInQuantity.ToString()) * detail.InvoiceXODetailPrice.Value - detail.InvoiceAllowance.Value, BL.V.SetDataFormat.XSJEXiao.Value);
                            this._acInvoiceXoBill.Details.Add(detail);
                        }
                    }
                    this.gridControl1.RefreshDataSource();
                }
            }
            form.Dispose();
            GC.Collect();
        }

        private void simBtnRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this._acInvoiceXoBill.Details.Remove(this.bindingSourceDetails.Current as Model.AcInvoiceXOBillDetail);
                this.gridView1.RefreshData();

                //this.calcTaxRateMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTax.Value).Sum();
                //this.calcHeJiMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailMoney.Value).Sum();
                //this.calcZongMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTaxMoney.Value).Sum();
            }
        }

        private void barButtonSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm lf = new ListForm();
            if (lf.ShowDialog(this) == DialogResult.OK)
            {
                this._acInvoiceXoBill = (Model.AcInvoiceXOBill)lf.SelectItem;
                this.action = "view";
                this.Refresh();
            }
            lf.Dispose();
            GC.Collect();
        }

        //选择出货单
        private void barBtnChuHuoDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Invoices.XS.SearchXSDetail f = new Book.UI.Invoices.XS.SearchXSDetail();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    if (f.selectItems.Count > 0)
            //    {
            //        foreach (Model.InvoiceXSDetail item in f.selectItems)
            //        {
            //            Model.AcInvoiceXOBillDetail detail = new Book.Model.AcInvoiceXOBillDetail();
            //            detail.AcInvoiceXOBillDetailId = Guid.NewGuid().ToString();
            //            detail.InvoiceXODetailId = item.InvoiceXSDetailId;
            //            detail.ProductId = item.ProductId;
            //            detail.Product = item.Product;
            //            detail.Invoice = item.Invoice;
            //            detail.InvoiceId = item.InvoiceId;
            //            detail.AcInvoiceXOBillId = this._acInvoiceXoBill.AcInvoiceXOBillId;
            //            detail.InvoiceAllowance = decimal.Parse((item.InvoiceAllowance == null ? 0 : item.InvoiceAllowance.Value).ToString());
            //            detail.InvoiceXODetaiInQuantity = ((item.InvoiceXODetailQuantity == null ? 0 : item.InvoiceXODetailQuantity.Value) - (item.InvoiceXSDetailFPQuantity == null ? 0 : item.InvoiceXSDetailFPQuantity.Value)) < 0 ? 0 : ((item.InvoiceXODetailQuantity == null ? 0 : item.InvoiceXODetailQuantity.Value) - (item.InvoiceXSDetailFPQuantity == null ? 0 : item.InvoiceXSDetailFPQuantity.Value));
            //            detail.InvoiceXODetailMoney = item.InvoiceXSDetailMoney == null ? 0 : item.InvoiceXSDetailMoney.Value;
            //            detail.InvoiceXODetailPrice = item.InvoiceXSDetailPrice == null ? 0 : item.InvoiceXSDetailPrice.Value;
            //            detail.InvoiceXODetailTax = item.InvoiceXSDetailTax == null ? 0 : item.InvoiceXSDetailTax.Value;
            //            detail.InvoiceXODetailTaxMoney = item.InvoiceXSDetailTaxMoney == null ? 0 : item.InvoiceXSDetailTaxMoney.Value;
            //            detail.InvoiceXODetailTaxPrice = item.InvoiceXSDetailTaxPrice == null ? 0 : item.InvoiceXSDetailTaxPrice.Value;

            //            this._acInvoiceXoBill.Details.Add(detail);
            //        }

            //        this.calcTaxRateMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTax.Value).Sum();
            //        this.calcHeJiMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailMoney.Value).Sum();
            //        this.calcZongMoney.Value = (from i in this._acInvoiceXoBill.Details select i.InvoiceXODetailTaxMoney.Value).Sum();
            //        this.calcTaxRate.Value = f.selectItems[0].Invoice.InvoiceTaxrate == null ? 0 : decimal.Parse(f.selectItems[0].Invoice.InvoiceTaxrate.ToString());
            //        this.TaxType.SelectedIndex = f.selectItems[0].Invoice.TaxCaluType == null ? 0 : f.selectItems[0].Invoice.TaxCaluType.Value;
            //        this.bindingSourceDetails.DataSource = this._acInvoiceXoBill.Details;
            //        this.gridControl1.RefreshDataSource();
            //    }
            //    f.Dispose();
            //    GC.Collect();
            //}
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            foreach (Model.AcInvoiceXOBillDetail item in this._acInvoiceXoBill.Details)
            {
                item.InvoiceXODetailMoney = (item.InvoiceXODetaiInQuantity == null ? 0 : decimal.Parse(item.InvoiceXODetaiInQuantity.Value.ToString())) * (item.InvoiceXODetailPrice == null ? 0 : decimal.Parse(item.InvoiceXODetailPrice.Value.ToString())) - (item.InvoiceAllowance == null ? 0 : decimal.Parse(item.InvoiceAllowance.Value.ToString()));
                item.InvoiceXODetailTaxMoney = item.InvoiceXODetailMoney + (item.InvoiceXODetailTax == null ? 0 : item.InvoiceXODetailTax.Value);
                //按照设置位数 四舍五入
                item.InvoiceXODetailMoney = this.GetDecimal(item.InvoiceXODetailMoney.Value, BL.V.SetDataFormat.KJJEXiao.Value);
                item.InvoiceXODetailTaxMoney = this.GetDecimal(item.InvoiceXODetailTaxMoney.Value, BL.V.SetDataFormat.KJZJXiao.Value);
            }
            this.gridControl1.RefreshDataSource();
            this.UpdateMoneyFields();
        }

        private int flag = 0;

        private void UpdateMoneyFields()
        {
            decimal yse = 0;//茼彶塗

            foreach (Model.AcInvoiceXOBillDetail detail in this._acInvoiceXoBill.Details)
            {
                if (detail.InvoiceXODetailMoney == null)
                    detail.InvoiceXODetailMoney = 0;
                yse += detail.InvoiceXODetailMoney.Value;
            }
            //yse = global::Helper.DateTimeParse.GetSiSheWuRu(yse, 0);
            yse = this.GetDecimal(yse, BL.V.SetDataFormat.KJZJXiao.Value);
            if (this.action != "view")
            {
                if (flag == 0)
                {
                    this.calcHeJiMoney.EditValue = yse;
                    this.calcTaxRate.EditValue = 0;
                    this.calcTaxRateMoney.EditValue = 0;
                    this.calcZongMoney.EditValue = yse;
                    this.calcNoHeXiaoTotal.Value = this.calcZongMoney.Value - this.calcmHeXiaoJingE.Value;
                }
                else if (flag == 1)
                {
                    this.calcHeJiMoney.EditValue = yse;
                    //this.calcTaxRateMoney.EditValue = global::Helper.DateTimeParse.GetSiSheWuRu(yse * this.calcTaxRate.Value / 100, 0);
                    this.calcTaxRateMoney.EditValue = this.GetDecimal((yse * this.calcTaxRate.Value / 100), BL.V.SetDataFormat.KJZJXiao.Value);
                    //this.calcZongMoney.EditValue = yse + decimal.Parse(this.calcTaxRateMoney.Text);
                    this.calcZongMoney.EditValue = this.GetDecimal(yse + decimal.Parse(this.calcTaxRateMoney.Text), BL.V.SetDataFormat.KJZJXiao.Value);
                    //this.calcNoHeXiaoTotal.Value = this.calcZongMoney.Value - this.calcmHeXiaoJingE.Value;
                    this.calcNoHeXiaoTotal.Value = this.GetDecimal(this.calcZongMoney.Value - this.calcmHeXiaoJingE.Value, BL.V.SetDataFormat.KJZJXiao.Value);
                }
                else
                {
                    //this.calcEditInvoiceTotal.EditValue = tol;
                    //this.calcEditInvoiceHeji.EditValue = yse;
                    //// this.calcEditInvoiceHeji.EditValue = yse * 100 / (100 + this.spinEditInvoiceTaxRate.Value);
                    //this.calcEditInvoiceTax.EditValue = tol - yse;
                    //this.comboBoxEditInvoiceKslb.SelectedIndex = 0;
                }
            }
        }

        private void UpdateZongMoney()
        {
            if (flag == 0)
            {
                this.calcTaxRateMoney.EditValue = 0;
                this.calcZongMoney.EditValue = this.calcHeJiMoney.EditValue;
            }
            else if (flag == 1)
            {
                //this.calcTaxRateMoney.EditValue = global::Helper.DateTimeParse.GetSiSheWuRu(this.calcHeJiMoney.Value * this.calcTaxRate.Value / 100, 0);
                this.calcTaxRateMoney.EditValue = this.GetDecimal(this.calcHeJiMoney.Value * this.calcTaxRate.Value / 100, BL.V.SetDataFormat.KJZJXiao.Value);
                //this.calcZongMoney.EditValue = this.calcHeJiMoney.Value + this.calcTaxRateMoney.Value;
                this.calcZongMoney.EditValue = this.GetDecimal(this.calcHeJiMoney.Value + this.calcTaxRateMoney.Value, BL.V.SetDataFormat.KJZJXiao.Value);
            }
            else
            {
                //this.calcEditInvoiceTotal.EditValue = tol;
                //this.calcEditInvoiceHeji.EditValue = yse;
                //// this.calcEditInvoiceHeji.EditValue = yse * 100 / (100 + this.spinEditInvoiceTaxRate.Value);
                //this.calcEditInvoiceTax.EditValue = tol - yse;
                //this.comboBoxEditInvoiceKslb.SelectedIndex = 0;
            }
            this.calcNoHeXiaoTotal.Value = this.GetDecimal(this.calcZongMoney.Value - this.calcmHeXiaoJingE.Value, BL.V.SetDataFormat.KJZJXiao.Value);
        }

        private void calcTaxRate_EditValueChanged(object sender, EventArgs e)
        {
            UpdateZongMoney();
        }

        private void TaxType_EditValueChanged(object sender, EventArgs e)
        {

            int index = TaxType.SelectedIndex;
            switch (index)
            {
                case 1:
                    flag = 1;
                    break;
                case 2:
                    flag = 2;
                    break;
                default:
                    flag = 0;
                    break;
            }
            UpdateZongMoney();
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            UpdateMoneyFields();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.gridView1.FocusedColumn.Name == "colInvoiceProductUnit")
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.AcInvoiceXOBillDetail).Product;
                    if (p == null)
                        return;
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

        private void calcHeJiMoney_EditValueChanged(object sender, EventArgs e)
        {
            UpdateZongMoney();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.AcInvoiceXOBill.PRO_AcInvoiceXOBillId;
        }

        protected override int AuditState()
        {
            return this._acInvoiceXoBill.AuditState.HasValue ? this._acInvoiceXoBill.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "AcInvoiceXOBill" + "," + this._acInvoiceXoBill.AcInvoiceXOBillId;
        }

        #endregion
    }
}