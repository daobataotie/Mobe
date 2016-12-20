using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using Book.UI.Settings.BasicData.Supplier;
using Book.UI.Settings.BasicData.Employees;
using System.Linq;

namespace Book.UI.AccountPayable.AcInvoiceCOBill
{
    public partial class EditForm : BaseEditForm
    {
        private BL.AcInvoiceCOBillManager _acInvoiceCoBillManager = new BL.AcInvoiceCOBillManager();
        private Model.AcInvoiceCOBill _acInvoiceCoBill;

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AcInvoiceCOBill.PRO_Id, new AA(Properties.Resources.AcInvoiceXOBillfpbh, this.textId));
            this.requireValueExceptions.Add(Model.AcInvoiceCOBill.PRO_SupplierId, new AA(Properties.Resources.ChooseSupplier, this.newChooseSupplierId));
            this.requireValueExceptions.Add("AcInvoiceCOBill.Details", new AA(Properties.Resources.AcInvoiceHasDetails, this.btn_add));

            this.newChooseEmployee0Id.Choose = new ChooseEmployee();
            //  this.newChooseEmployee1Id.Choose = new ChooseEmployee();
            //  this.newChooseEmployeeId.Choose = new ChooseEmployee();
            this.newChooseSupplierId.Choose = new ChooseSupplier();
            this.newChooseSup_SupplierId.Choose = new ChooseSupplier();

            this.action = "view";
        }

        #region override
        protected override void MoveFirst()
        {
            this._acInvoiceCoBill = this._acInvoiceCoBillManager.Get(this._acInvoiceCoBillManager.GetFirst() == null ? "" : this._acInvoiceCoBillManager.GetFirst().AcInvoiceCOBillId);
        }

        protected override void MovePrev()
        {
            Model.AcInvoiceCOBill temp = this._acInvoiceCoBillManager.GetPrev(this._acInvoiceCoBill);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acInvoiceCoBill = this._acInvoiceCoBillManager.Get(temp.AcInvoiceCOBillId);
        }

        /// <summary>
        /// 尾笔
        /// </summary>
        protected override void MoveLast()
        {
            // if (_acbeginAccountPayble == null)
            {
                this._acInvoiceCoBill = this._acInvoiceCoBillManager.Get(this._acInvoiceCoBillManager.GetLast() == null ? "" : this._acInvoiceCoBillManager.GetLast().AcInvoiceCOBillId);
            }
        }

        protected override void MoveNext()
        {
            Model.AcInvoiceCOBill temp = this._acInvoiceCoBillManager.GetNext(this._acInvoiceCoBill);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acInvoiceCoBill = this._acInvoiceCoBillManager.Get(temp.AcInvoiceCOBillId);
        }

        /// <summary>
        /// 是否有返回行
        /// </summary>
        /// <returns></returns>
        protected override bool HasRows()
        {
            return this._acInvoiceCoBillManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._acInvoiceCoBillManager.HasRowsAfter(this._acInvoiceCoBill);
        }

        protected override bool HasRowsPrev()
        {
            return this._acInvoiceCoBillManager.HasRowsBefore(this._acInvoiceCoBill);
        }

        protected override void AddNew()
        {
            this._acInvoiceCoBill = new Model.AcInvoiceCOBill();
            this._acInvoiceCoBill.AcInvoiceCOBillId = this._acInvoiceCoBillManager.GetId();
            //this._acInvoiceCoBill.Id = this._acInvoiceCoBillManager.GetId();
            this._acInvoiceCoBill.AcInvoiceCOBillDate = DateTime.Now;
            this._acInvoiceCoBill.Employee = BL.V.ActiveOperator.Employee;
            this._acInvoiceCoBill.TaxRateType = 1;
            this._acInvoiceCoBill.TaxRate = 5;
            this._acInvoiceCoBill.Details = new List<Model.AcInvoiceCOBillDetail>();
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this._acInvoiceCoBillManager.Delete(this._acInvoiceCoBill);
                this._acInvoiceCoBill = this._acInvoiceCoBillManager.GetNext(this._acInvoiceCoBill);
                if (this._acInvoiceCoBill == null)
                {
                    this._acInvoiceCoBill = this._acInvoiceCoBillManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override void Save()
        {
            this._acInvoiceCoBill.AcInvoiceCOBillId = this.AcInvoiceCOBillId.Text;
            this._acInvoiceCoBill.Id = this.textId.Text;
            this._acInvoiceCoBill.Sup_Supplier = this.newChooseSup_SupplierId.EditValue as Model.Supplier;
            if (this._acInvoiceCoBill.Sup_Supplier != null)
                this._acInvoiceCoBill.Sup_SupplierId = this._acInvoiceCoBill.Sup_Supplier.SupplierId;
            this._acInvoiceCoBill.Supplier = this.newChooseSupplierId.EditValue as Model.Supplier;
            if (this._acInvoiceCoBill.Supplier != null)
                this._acInvoiceCoBill.SupplierId = this._acInvoiceCoBill.Supplier.SupplierId;
            this._acInvoiceCoBill.TaxRate = Convert.ToDouble(this.calcTaxRate.Value);
            this._acInvoiceCoBill.TaxRateMoney = this.TaxRateMoney.Value;
            this._acInvoiceCoBill.TaxRateType = this.TaxType.SelectedIndex;
            this._acInvoiceCoBill.ZongMoney = this.ZongMoney.Value;
            this._acInvoiceCoBill.mHeXiaoJingE = this.calcNoHeXiaoTotal.Value;
            this._acInvoiceCoBill.NoHeXiaoTotal = this.calcNoHeXiaoTotal.Value;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateAcInvoiceCOBillDate.DateTime, new DateTime()))
                this._acInvoiceCoBill.AcInvoiceCOBillDate = global::Helper.DateTimeParse.NullDate;
            else
                this._acInvoiceCoBill.AcInvoiceCOBillDate = this.dateAcInvoiceCOBillDate.DateTime;
            this._acInvoiceCoBill.AcInvoiceCOBillDesc = this.memoAcInvoiceCOBillDesc.Text;
            this._acInvoiceCoBill.Employee = this.newChooseEmployeeId.EditValue as Model.Employee;
            if (this._acInvoiceCoBill.Employee != null)
                this._acInvoiceCoBill.EmployeeId = this._acInvoiceCoBill.Employee.EmployeeId;
            this._acInvoiceCoBill.Employee0 = this.newChooseEmployee0Id.EditValue as Model.Employee;
            if (this._acInvoiceCoBill.Employee0 != null)
                this._acInvoiceCoBill.Employee0Id = this._acInvoiceCoBill.Employee0.EmployeeId;
            //  this._acInvoiceCoBill.Employee1 = this.newChooseEmployee1Id.EditValue as Model.Employee;
            //if (this._acInvoiceCoBill.Employee1 != null)
            //    this._acInvoiceCoBill.Employee1Id = this._acInvoiceCoBill.Employee1.EmployeeId;

            this._acInvoiceCoBill.InvoiceStatus = 1;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._acInvoiceCoBillManager.Insert(this._acInvoiceCoBill);
                    break;

                case "update":
                    this._acInvoiceCoBillManager.Update(this._acInvoiceCoBill);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._acInvoiceCoBill == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._acInvoiceCoBill = this._acInvoiceCoBillManager.GetDetails(this._acInvoiceCoBill);
                }
            }

            this.AcInvoiceCOBillId.Text = this._acInvoiceCoBill.AcInvoiceCOBillId;
            this.textId.Text = this._acInvoiceCoBill.Id;
            this.HeJiMoney.Value = this._acInvoiceCoBill.HeJiMoney == null ? 0 : this._acInvoiceCoBill.HeJiMoney.Value;
            this.calcTaxRate.Value = this._acInvoiceCoBill.TaxRate == null ? 0 : Convert.ToDecimal(this._acInvoiceCoBill.TaxRate.Value);
            this.calcmHeXiaoJingE.Value = this._acInvoiceCoBill.mHeXiaoJingE == null ? 0 : this.calcmHeXiaoJingE.Value;
            this.calcNoHeXiaoTotal.Value = this._acInvoiceCoBill.NoHeXiaoTotal == null ? 0 : this.calcNoHeXiaoTotal.Value;
            this.TaxType.SelectedIndex = Convert.ToInt32(this._acInvoiceCoBill.TaxRateType);
            this.TaxRateMoney.Value = this._acInvoiceCoBill.TaxRateMoney == null ? 0 : this._acInvoiceCoBill.TaxRateMoney.Value;
            this.ZongMoney.Value = this._acInvoiceCoBill.ZongMoney == null ? 0 : this._acInvoiceCoBill.ZongMoney.Value;
            this.newChooseEmployee0Id.EditValue = this._acInvoiceCoBill.AuditEmp;
            // this.newChooseEmployee1Id.EditValue = this._acInvoiceCoBill.Employee1;
            this.newChooseEmployeeId.EditValue = this._acInvoiceCoBill.Employee;
            this.newChooseSup_SupplierId.EditValue = this._acInvoiceCoBill.Sup_Supplier;
            this.newChooseSupplierId.EditValue = this._acInvoiceCoBill.Supplier;
            this.memoAcInvoiceCOBillDesc.Text = this._acInvoiceCoBill.AcInvoiceCOBillDesc;
            this.dateAcInvoiceCOBillDate.DateTime = this._acInvoiceCoBill.AcInvoiceCOBillDate.Value;
            this.txt_AuditState.EditValue = this.GetAuditName(this._acInvoiceCoBill.AuditState);
            this.bindingSourceDetails.DataSource = this._acInvoiceCoBill.Details;

            base.Refresh();
            //if (this.action == "view")
            //{
            //    this.barBtnJinHuoDan.Enabled = false;
            //}
            //else
            //{
            //    this.barBtnJinHuoDan.Enabled = true;
            //}
            this.newChooseEmployeeId.Enabled = false;
            //this.HeJiMoney.Enabled = false;
            this.ZongMoney.Enabled = false;
            this.TaxRateMoney.Enabled = false;
            this.calcmHeXiaoJingE.Enabled = false;
            this.calcNoHeXiaoTotal.Enabled = false;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Ro(this._acInvoiceCoBill);
        }

        #endregion

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm lf = new ListForm();
            if (lf.ShowDialog(this) == DialogResult.OK)
            {
                this._acInvoiceCoBill = (Model.AcInvoiceCOBill)lf.SelectItem;
                this.action = "view";
                this.Refresh();
            }
            lf.Dispose();
            GC.Collect();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            //采购单
            //SelectInvoiceCOListForm form = new SelectInvoiceCOListForm();
            //if (form.ShowDialog(this) == DialogResult.OK)
            //{
            //    if (form.SelectItems == null || form.SelectItems.Count == 0)
            //        form.SelectItems.Add(form.SelectItem);
            //    this._acInvoiceCoBill.Details = (this._acInvoiceCoBill.Details.Union((from i in form.SelectItems
            //                                                                          select new Model.AcInvoiceCOBillDetail()
            //                                                                          {
            //                                                                              AcInvoiceCOBillDetailId = Guid.NewGuid().ToString(),
            //                                                                              AcInvoiceCOBillId = this._acInvoiceCoBill.AcInvoiceCOBillId,
            //                                                                              InvoiceId = i.InvoiceId,
            //                                                                              InvoiceCGDetailMoney = i.InvoiceHeji.HasValue ? i.InvoiceHeji.Value : 0,
            //                                                                              InvoiceCGDetailTaxMoney = i.InvoiceTotal.HasValue ? i.InvoiceTotal.Value : 0,
            //                                                                              InvoiceCGDetailTax = i.InvoiceTax.HasValue ? i.InvoiceTax.Value : 0,
            //                                                                          }).ToList<Model.AcInvoiceCOBillDetail>())).ToList<Model.AcInvoiceCOBillDetail>();
            //}
            //this.TaxRateMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailTax.Value).Sum();
            //this.HeJiMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailMoney.Value).Sum();
            //this.ZongMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailTaxMoney.Value).Sum();
            //this.bindingSourceDetails.DataSource = this._acInvoiceCoBill.Details;
            //this.gridControl1.RefreshDataSource();
            //form.Dispose();
            //GC.Collect();
            //进库单
            Invoices.CG.SearchCGDetail f = new Book.UI.Invoices.CG.SearchCGDetail();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.selectItems.Count != 0)
                {
                    foreach (Model.InvoiceCGDetail item in f.selectItems)
                    {
                        Model.AcInvoiceCOBillDetail detail = new Book.Model.AcInvoiceCOBillDetail();
                        detail.AcInvoiceCOBillDetailId = Guid.NewGuid().ToString();
                        detail.InvoiceCGDetailId = item.InvoiceCGDetailId;
                        detail.ProductId = item.ProductId;
                        detail.Product = item.Product;
                        detail.Invoice = item.Invoice;
                        detail.InvoiceId = item.InvoiceId;
                        detail.AcInvoiceCOBillId = this._acInvoiceCoBill.AcInvoiceCOBillId;
                        detail.InvoiceAllowance = decimal.Parse((item.InvoiceAllowance == null ? 0 : item.InvoiceAllowance.Value).ToString());
                        detail.InvoiceCGDetaiInQuantity = ((item.InvoiceCGDetailQuantity == null ? 0 : item.InvoiceCGDetailQuantity.Value) - (item.InvoiceCGDetailFPQuantity == null ? 0 : item.InvoiceCGDetailFPQuantity.Value)) < 0 ? 0 : ((item.InvoiceCGDetailQuantity == null ? 0 : item.InvoiceCGDetailQuantity.Value) - (item.InvoiceCGDetailFPQuantity == null ? 0 : item.InvoiceCGDetailFPQuantity.Value));

                        detail.InvoiceCGDetailPrice = item.InvoiceCGDetailPrice == null ? 0 : item.InvoiceCGDetailPrice.Value;
                        //  detail.InvoiceCGDetailMoney = item.InvoiceCGDetailMoney == null ? 0 : item.InvoiceCGDetailMoney.Value;
                        detail.InvoiceCGDetailMoney = global::Helper.DateTimeParse.GetSiSheWuRu(decimal.Parse(detail.InvoiceCGDetaiInQuantity.ToString()) * detail.InvoiceCGDetailPrice.Value - detail.InvoiceAllowance.Value, BL.V.SetDataFormat.XSJEXiao.Value);


                        //detail.InvoiceCGDetailTax = item.InvoiceCGDetailTax == null ? 0 : item.InvoiceCGDetailTax.Value;
                        //detail.InvoiceCGDetailTaxMoney = item.InvoiceCGDetailTaxMoney == null ? 0 : item.InvoiceCGDetailTaxMoney.Value;
                        //detail.InvoiceCGDetailTaxPrice = item.InvoiceCGDetailTaxPrice == null ? 0 : item.InvoiceCGDetailTaxPrice.Value;

                        this._acInvoiceCoBill.Details.Add(detail);
                    }

                    //this.TaxRateMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailTax.Value).Sum();
                    //this.HeJiMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailMoney.Value).Sum();
                    //this.ZongMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailTaxMoney.Value).Sum();
                    //this.calcTaxRate.Value = f.selectItems[0].Invoice == null ? 0 : (f.selectItems[0].Invoice.InvoiceTaxrate == null ? 0 : decimal.Parse(f.selectItems[0].Invoice.InvoiceTaxrate.ToString()));
                    //this.TaxType.SelectedIndex = f.selectItems[0].Invoice == null ? 0 : (f.selectItems[0].Invoice.TaxCaluType == null ? 0 : f.selectItems[0].Invoice.TaxCaluType.Value);
                    this.bindingSourceDetails.DataSource = this._acInvoiceCoBill.Details;
                    this.gridControl1.RefreshDataSource();
                    this.UpdateMoneyFields();
                }
                f.Dispose();
                GC.Collect();
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this._acInvoiceCoBill.Details.Remove(this.bindingSourceDetails.Current as Model.AcInvoiceCOBillDetail);
                this.gridView1.RefreshData();

                //this.TaxRateMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailTax.Value).Sum();
                //this.HeJiMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailMoney.Value).Sum();
                //this.ZongMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailTaxMoney.Value).Sum();
            }
        }

        private void dateAcInvoiceCOBillDate_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.AcInvoiceCOBillId.Text = this._acInvoiceCoBillManager.GetId(this.dateAcInvoiceCOBillDate.DateTime);
        }

        private void barBtnJinHuoDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Invoices.CG.SearchCGDetail f = new Book.UI.Invoices.CG.SearchCGDetail();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    if (f.selectItems.Count != 0)
            //    {
            //        foreach (Model.InvoiceCGDetail item in f.selectItems)
            //        {
            //            Model.AcInvoiceCOBillDetail detail = new Book.Model.AcInvoiceCOBillDetail();
            //            detail.AcInvoiceCOBillDetailId = Guid.NewGuid().ToString();
            //            detail.InvoiceCGDetailId = item.InvoiceCGDetailId;
            //            detail.ProductId = item.ProductId;
            //            detail.Product = item.Product;
            //            detail.Invoice = item.Invoice;
            //            detail.InvoiceId = item.InvoiceId;
            //            detail.AcInvoiceCOBillId = this._acInvoiceCoBill.AcInvoiceCOBillId;
            //            detail.InvoiceAllowance = decimal.Parse((item.InvoiceAllowance == null ? 0 : item.InvoiceAllowance.Value).ToString());
            //            detail.InvoiceCGDetaiInQuantity = ((item.InvoiceCGDetailQuantity == null ? 0 : item.InvoiceCGDetailQuantity.Value) - (item.InvoiceCGDetailFPQuantity == null ? 0 : item.InvoiceCGDetailFPQuantity.Value)) < 0 ? 0 : ((item.InvoiceCGDetailQuantity == null ? 0 : item.InvoiceCGDetailQuantity.Value) - (item.InvoiceCGDetailFPQuantity == null ? 0 : item.InvoiceCGDetailFPQuantity.Value));
            //            detail.InvoiceCGDetailMoney = item.InvoiceCGDetailMoney == null ? 0 : item.InvoiceCGDetailMoney.Value;
            //            detail.InvoiceCGDetailPrice = item.InvoiceCGDetailPrice == null ? 0 : item.InvoiceCGDetailPrice.Value;
            //            detail.InvoiceCGDetailTax = item.InvoiceCGDetailTax == null ? 0 : item.InvoiceCGDetailTax.Value;
            //            detail.InvoiceCGDetailTaxMoney = item.InvoiceCGDetailTaxMoney == null ? 0 : item.InvoiceCGDetailTaxMoney.Value;
            //            detail.InvoiceCGDetailTaxPrice = item.InvoiceCGDetailTaxPrice == null ? 0 : item.InvoiceCGDetailTaxPrice.Value;

            //            this._acInvoiceCoBill.Details.Add(detail);
            //        }

            //        this.TaxRateMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailTax.Value).Sum();
            //        this.HeJiMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailMoney.Value).Sum();
            //        this.ZongMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailTaxMoney.Value).Sum();
            //        this.calcTaxRate.Value = f.selectItems[0].Invoice == null ? 0 : (f.selectItems[0].Invoice.InvoiceTaxrate == null ? 0 : decimal.Parse(f.selectItems[0].Invoice.InvoiceTaxrate.ToString()));
            //        this.TaxType.SelectedIndex = f.selectItems[0].Invoice == null ? 0 : (f.selectItems[0].Invoice.TaxCaluType == null ? 0 : f.selectItems[0].Invoice.TaxCaluType.Value);
            //        this.bindingSourceDetails.DataSource = this._acInvoiceCoBill.Details;
            //        this.gridControl1.RefreshDataSource();
            //    }
            //    f.Dispose();
            //    GC.Collect();
            //}
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            foreach (Model.AcInvoiceCOBillDetail item in this._acInvoiceCoBill.Details)
            {
                item.InvoiceCGDetailMoney = (item.InvoiceCGDetaiInQuantity == null ? 0 : decimal.Parse(item.InvoiceCGDetaiInQuantity.Value.ToString())) * (item.InvoiceCGDetailPrice == null ? 0 : decimal.Parse(item.InvoiceCGDetailPrice.Value.ToString())) - (item.InvoiceAllowance == null ? 0 : decimal.Parse(item.InvoiceAllowance.Value.ToString()));
                item.InvoiceCGDetailTaxMoney = item.InvoiceCGDetailMoney + (item.InvoiceCGDetailTax == null ? 0 : item.InvoiceCGDetailTax.Value);

                //按照设置位数 四舍五入
                item.InvoiceCGDetailMoney = this.GetDecimal(item.InvoiceCGDetailMoney.Value, BL.V.SetDataFormat.KJJEXiao.Value);
                item.InvoiceCGDetailTaxMoney = this.GetDecimal(item.InvoiceCGDetailTaxMoney.Value, BL.V.SetDataFormat.KJZJXiao.Value);
            }
            //this.TaxRateMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailTax.Value).Sum();
            //this.HeJiMoney.Value = (from i in this._acInvoiceCoBill.Details select i.InvoiceCGDetailMoney.Value).Sum();
            //this.ZongMoney.Value = (this.HeJiMoney.EditValue == null ? 0 : this.HeJiMoney.Value) + (this.TaxRateMoney.EditValue == null ? 0 : this.TaxRateMoney.Value);

            this.gridControl1.RefreshDataSource();
            UpdateMoneyFields();

        }

        private int flag = 0;

        private void UpdateMoneyFields()
        {
            decimal yse = 0;//茼彶塗               

            foreach (Model.AcInvoiceCOBillDetail detail in this._acInvoiceCoBill.Details)
            {
                if (detail.InvoiceCGDetailMoney == null)
                    detail.InvoiceCGDetailMoney = 0;
                yse += detail.InvoiceCGDetailMoney.Value;
            }
            //yse = global::Helper.DateTimeParse.GetSiSheWuRu(yse, 0);
            yse = this.GetDecimal(yse, BL.V.SetDataFormat.KJZJXiao.Value);

            if (this.action != "view")
            {
                if (flag == 0)
                {
                    this.HeJiMoney.EditValue = yse;
                    this.TaxRateMoney.EditValue = 0;
                    this.ZongMoney.EditValue = yse;
                    this.calcNoHeXiaoTotal.Value = this.ZongMoney.Value - this.calcmHeXiaoJingE.Value;
                }
                else if (flag == 1)
                {
                    this.HeJiMoney.EditValue = yse;
                    //this.TaxRateMoney.EditValue = global::Helper.DateTimeParse.GetSiSheWuRu(yse * this.calcTaxRate.Value / 100, 0);
                    this.TaxRateMoney.EditValue = this.GetDecimal(yse * this.calcTaxRate.Value / 100, BL.V.SetDataFormat.KJZJXiao.Value);
                    //this.ZongMoney.EditValue = yse + this.TaxRateMoney.Value;
                    this.ZongMoney.EditValue = this.GetDecimal(yse + this.TaxRateMoney.Value, BL.V.SetDataFormat.KJZJXiao.Value);
                    //this.calcNoHeXiaoTotal.Value = this.ZongMoney.Value - this.calcmHeXiaoJingE.Value;
                    this.calcNoHeXiaoTotal.Value = this.GetDecimal(this.ZongMoney.Value - this.calcmHeXiaoJingE.Value, BL.V.SetDataFormat.KJZJXiao.Value);
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

        private void calcTaxRate_EditValueChanged(object sender, EventArgs e)
        {

            //this.UpdateMoneyFields();
            UpdateZongMoney();

        }

        private void UpdateZongMoney()
        {
            if (flag == 0)
            {
                this.TaxRateMoney.EditValue = 0;
                this.ZongMoney.EditValue = this.HeJiMoney.EditValue;
            }
            else if (flag == 1)
            {
                //this.TaxRateMoney.EditValue = global::Helper.DateTimeParse.GetSiSheWuRu(this.HeJiMoney.Value * this.calcTaxRate.Value / 100, 0);
                this.TaxRateMoney.EditValue = this.GetDecimal(this.HeJiMoney.Value * this.calcTaxRate.Value / 100, BL.V.SetDataFormat.KJZJXiao.Value);
                //this.ZongMoney.EditValue = this.HeJiMoney.Value + this.TaxRateMoney.Value;
                this.ZongMoney.EditValue = this.GetDecimal(this.HeJiMoney.Value + this.TaxRateMoney.Value, BL.V.SetDataFormat.KJZJXiao.Value);
            }
            else
            {
                //this.calcEditInvoiceTotal.EditValue = tol;
                //this.calcEditInvoiceHeji.EditValue = yse;
                //// this.calcEditInvoiceHeji.EditValue = yse * 100 / (100 + this.spinEditInvoiceTaxRate.Value);
                //this.calcEditInvoiceTax.EditValue = tol - yse;
                //this.comboBoxEditInvoiceKslb.SelectedIndex = 0;
            }
            this.calcNoHeXiaoTotal.Value = this.GetDecimal(this.ZongMoney.Value - this.calcmHeXiaoJingE.Value, BL.V.SetDataFormat.KJZJXiao.Value);
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
            this.UpdateMoneyFields();
        }

        private void HeJiMoney_EditValueChanged(object sender, EventArgs e)
        {
            UpdateZongMoney();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.AcInvoiceCOBill.PRO_AcInvoiceCOBillId;
        }

        protected override int AuditState()
        {
            return this._acInvoiceCoBill.AuditState.HasValue ? this._acInvoiceCoBill.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "AcInvoiceCOBill" + "," + this._acInvoiceCoBill.AcInvoiceCOBillId;
        }

        #endregion
    }
}