using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class ASEditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        public Model.PCExportReportANSI _PCExportReportANSI = null;
        BL.PCExportReportANSIManager _PCExportReportANSIManager = new Book.BL.PCExportReportANSIManager();
        BL.PCExportReportANSIManager _pcExpANSIManager = new Book.BL.PCExportReportANSIManager();
        string _ServerSavePath = string.Empty;      //附件存放地址
        int tag;                                    //列印标志

        public ASEditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_ExportReportId, new AA(Properties.Resources.NumsIsNotNull, this.TxtOrderId));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.TxtProduct));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_InvoiceCusXOId, new AA("客戶訂單編號不能為空", this.TxtCustomersId));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_CustomerId, new AA("客戶不能為空", this.NccCustomer));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_Amount, new AA("訂單數量不能為空", this.SpinOrderAmount));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_AmountTest, new AA("測試數量不能為空", this.SpinTestAmount));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_EmployeeId, new AA(Properties.Resources.EmployeeNotNull, this.NccTestPerson));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_ReportDate, new AA(Properties.Resources.DateNotNull, this.DateReportDate));

            //this.invalidValueExceptions.Add(Model.PCExportReportANSI.PRO_AmountTest + "_ForInvoiceXoQuantity", new AA("測試數量未達標:測試數量≈訂單數量/500(不齊不足1)", this.SpinTestAmount));
            //this.invalidValueExceptions.Add(Model.PCExportReportANSI.PRO_AmountTest + "_ForDetailsCount", new AA("測試數量未達標:測試數量詳細測試數量總和", this.SpinTestAmount));

            this.NccCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.NccTestPerson.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();
            this.action = "view";
        }

        int sign = 0;
        public ASEditForm(Model.PCExportReportANSI mPCExpANSI)
            : this()
        {
            if (mPCExpANSI == null)
                throw new ArithmeticException("invoiceid");
            this._PCExportReportANSI = mPCExpANSI;
            this.sign = 1;
            this.action = "view";
        }

        protected override void MoveLast()
        {
            if (this.sign == 1)
            {
                this.sign = 0;
                return;
            }
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_last("AS");
        }

        protected override void MoveFirst()
        {
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_first("AS");
        }

        protected override void MovePrev()
        {
            Model.PCExportReportANSI csa = this._PCExportReportANSIManager.mget_prev("AS", this._PCExportReportANSI.InsertTime.Value);
            if (csa == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._PCExportReportANSI = csa;
        }

        protected override void MoveNext()
        {
            Model.PCExportReportANSI csa = this._PCExportReportANSIManager.mget_next("AS", this._PCExportReportANSI.InsertTime.Value);
            if (csa == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._PCExportReportANSI = csa;
        }

        protected override bool HasRows()
        {
            return this._PCExportReportANSIManager.mhas_rows("AS");
        }

        protected override bool HasRowsPrev()
        {
            return this._PCExportReportANSIManager.mhas_rows_before("AS", this._PCExportReportANSI.InsertTime.Value);
        }

        protected override bool HasRowsNext()
        {
            return this._PCExportReportANSIManager.mhas_rows_after("AS", this._PCExportReportANSI.InsertTime.Value);
        }

        protected override void AddNew()
        {
            this._PCExportReportANSI = new Book.Model.PCExportReportANSI();
            this._PCExportReportANSI.ExportReportId = this._PCExportReportANSIManager.GetId();
            this._PCExportReportANSI.ReportDate = DateTime.Now.Date;
            this._PCExportReportANSI.ExpType = "AS";

        }

        protected override void Delete()
        {
            if (this._PCExportReportANSI == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._PCExportReportANSIManager.Delete(this._PCExportReportANSI.ExportReportId);
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_next("AS", this._PCExportReportANSI.InsertTime.Value);
            if (this._PCExportReportANSI == null)
            {
                this._PCExportReportANSI = this._PCExportReportANSIManager.mget_last("AS");
            }
        }

        protected override void Save()
        {
            this._PCExportReportANSI.ExportReportId = this.TxtOrderId.Text == null ? null : this.TxtOrderId.Text;
            this._PCExportReportANSI.Amount = this.SpinOrderAmount.EditValue == null ? 0 : double.Parse(this.SpinOrderAmount.EditValue.ToString());
            this._PCExportReportANSI.AmountTest = this.SpinTestAmount.EditValue == null ? 0 : double.Parse(this.SpinTestAmount.EditValue.ToString());
            this._PCExportReportANSI.InvoiceCusXOId = this.TxtCustomersId.Text == null ? null : this.TxtCustomersId.Text.ToString();
            this._PCExportReportANSI.Customer = (this.NccCustomer.EditValue as Model.Customer);
            this._PCExportReportANSI.ReportDate = this.DateReportDate.EditValue == null ? DateTime.Now : this.DateReportDate.DateTime;

            if (this._PCExportReportANSI.Customer != null)
            {
                this._PCExportReportANSI.CustomerId = this._PCExportReportANSI.Customer.CustomerId;
            }
            this._PCExportReportANSI.Employee = (this.NccTestPerson.EditValue as Model.Employee);
            if (this._PCExportReportANSI.Employee != null)
            {
                this._PCExportReportANSI.EmployeeId = this._PCExportReportANSI.Employee.EmployeeId;
            }

            this._PCExportReportANSI.Product = (this.TxtProduct.EditValue as Model.Product);
            if (this._PCExportReportANSI.Product != null)
            {
                this._PCExportReportANSI.ProductId = this._PCExportReportANSI.Product.ProductId;
            }
            this._PCExportReportANSI.AuditEmp = this.newChooseContorlAuditEmp.EditValue as Model.Employee;
            if (this._PCExportReportANSI.AuditEmp != null)
            {
                this._PCExportReportANSI.AuditEmpId = this._PCExportReportANSI.AuditEmp.EmployeeId;
                //this._PCExportReportANSI.AuditState = (int)global::Helper.InvoiceAudit.Audited;
            }

            this._PCExportReportANSI.ProductBatchNo = this.txtBatchNo.Text;
            this._PCExportReportANSI.QuYangShu2 = this.spinQtyTest.EditValue == null ? 0 : double.Parse(this.spinQtyTest.EditValue.ToString());
            this._PCExportReportANSI.VisualTest = this.checkVisualTest.Checked;
            this._PCExportReportANSI.ThermalStability = this.checkThemal.Checked;
            this._PCExportReportANSI.PrismaticPowerHIn = this.checkPrismaticHIn.Checked;
            this._PCExportReportANSI.PrismaticPowerHOut = this.checkPrismaticHOut.Checked;
            this._PCExportReportANSI.PrismaticPowerVDwn = this.checkPrismaticVDwn.Checked;
            this._PCExportReportANSI.PrismaticPowerVUp = this.checkPrismaticVUp.Checked;
            this._PCExportReportANSI.ScatterLight = this.checkScatter.Checked;
            this._PCExportReportANSI.MediumImpact = this.checkMedium.Checked;
            this._PCExportReportANSI.HighImpact = this.checkHigh.Checked;
            this._PCExportReportANSI.ExtraHighImpact = this.checkExtraHigh.Checked;
            this._PCExportReportANSI.PermertrationTest = this.checkPemertration.Checked;
            this._PCExportReportANSI.IgnitionTest = this.checkIgnition.Checked;
            this._PCExportReportANSI.Corrsion = this.checkCorrsion.Checked;
            this._PCExportReportANSI.Markings = this.checkMarkings.Checked;
            this._PCExportReportANSI.RefractivePower = this.spinRefractivePower.EditValue == null ? 0 : double.Parse(this.spinRefractivePower.EditValue.ToString());
            this._PCExportReportANSI.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();
            if (this.date_ApprovalDate.EditValue == null)
                this._PCExportReportANSI.ApprovalDate = this.txt_PZEmp.Text + "";
            else
                this._PCExportReportANSI.ApprovalDate = this.txt_PZEmp.Text + " " + this.date_ApprovalDate.DateTime.ToString("yyyy-MM-dd");


            switch (this.action)
            {
                case "insert":
                    this._PCExportReportANSIManager.Insert(this._PCExportReportANSI);
                    break;
                case "update":
                    this._PCExportReportANSIManager.Update(this._PCExportReportANSI);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._PCExportReportANSI == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            if (this.action == "view")
            {
                this._PCExportReportANSI = this._PCExportReportANSIManager.Get(this._PCExportReportANSI.ExportReportId);
            }

            InitControls();

            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    this.BarBtnCutomerOrder.Enabled = true;
                    this.NccCustomer.Enabled = true;
                    break;
                case "update":
                    this.BarBtnCutomerOrder.Enabled = true;
                    this.NccCustomer.Enabled = true;
                    break;
                case "view":
                    this.BarBtnCutomerOrder.Enabled = false;
                    this.NccCustomer.Enabled = false;
                    break;

            }

            this.TxtOrderId.Properties.ReadOnly = true;
            this.SpinOrderAmount.Enabled = false;
            //this.SpinTestAmount.Enabled = false;
            this.TxtCustomersId.Enabled = false;
            this.TxtProduct.Enabled = false;


        }

        //列印
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            tag = 0;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            ASRO r = new ASRO(this._PCExportReportANSI, tag);
            //r.ShowPreviewDialog();
            if (canSave)
            {
                if (this._PCExportReportANSI != null && !string.IsNullOrEmpty(this._PCExportReportANSI.ExportReportId))
                {
                    string sfdir = this._ServerSavePath + "\\" + this._PCExportReportANSI.ExportReportId;
                    try
                    {
                        System.IO.Directory.CreateDirectory(sfdir);
                        r.ExportToPdf(sfdir + "\\" + this._PCExportReportANSI.ExportReportId + ".pdf");
                        MessageBox.Show("文件已導出為pdf格式上傳至服務器");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }
            }
            return r;
        }

        //客户订单
        private void BarBtnCutomerOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            createProduce.EditForm f = new Book.UI.produceManager.createProduce.EditForm();
            if (f.ShowDialog(this) != DialogResult.OK)
                return;
            if (f.SelectList == null || f.SelectList.Count == 0)
                return;
            Model.InvoiceXODetail xd = f.SelectList[0];


            this._PCExportReportANSI.Customer = xd.Invoice.xocustomer;
            this._PCExportReportANSI.CustomerId = xd.Invoice.xocustomer.CustomerId;
            this._PCExportReportANSI.Specification = xd.Invoice.xocustomer.CheckedStandard;
            this._PCExportReportANSI.Product = xd.Product;
            this._PCExportReportANSI.InvoiceCusXOId = xd.Invoice.CustomerInvoiceXOId;
            this._PCExportReportANSI.Amount = xd.InvoiceXODetailQuantity.HasValue ? xd.InvoiceXODetailQuantity.Value : 0;

            //获取质检统计记录
            //Model.PCExportReportANSIDetail _PCExportReportANSIDetail = new BL.PCExportReportANSIDetailManager().SelectForExpASDetailsSUM(xd.Invoice.CustomerInvoiceXOId, xd.Product.ProductId);

            //if (_PCExportReportANSIDetail != null)
            //{
            #region 测试数量、合格数量

            //受测数量默认为订单数量的1/500,无条件进位，最大为12
            int Orderamount = int.Parse(this._PCExportReportANSI.Amount.HasValue ? this._PCExportReportANSI.Amount.ToString() : "0");
            double MustCheck = 0;

            if (Orderamount < 500)
                MustCheck = 1;
            else
                MustCheck = Orderamount % 500 == 0 ? Orderamount / 500 : Orderamount / 500 + 1;

            this._PCExportReportANSI.AmountTest = MustCheck > 12 ? 12 : MustCheck;//受测数量12个，无条件进位

            this._PCExportReportANSI.ShouCeShu2 = this._PCExportReportANSI.AmountTest;

            //this._PCExportReportANSI.PanDing2 = _PCExportReportANSIDetail.pASCCSL;
            //this._PCExportReportANSI.QuYangShu2 = _PCExportReportANSIDetail.qASCCSL;

            #endregion
            //}
            this.InitControls();
        }

        //控件赋值
        private void InitControls()
        {
            this.spinRefractivePower.EditValue = this._PCExportReportANSI.RefractivePower.HasValue ? this._PCExportReportANSI.RefractivePower.Value : 0;
            this.TxtOrderId.Text = this._PCExportReportANSI.ExportReportId == null ? null : this._PCExportReportANSI.ExportReportId;
            this.TxtCustomersId.Text = this._PCExportReportANSI.InvoiceCusXOId;
            this.NccCustomer.EditValue = this._PCExportReportANSI.Customer;
            this.NccTestPerson.EditValue = this._PCExportReportANSI.Employee;
            this.SpinOrderAmount.EditValue = this._PCExportReportANSI.Amount.HasValue ? this._PCExportReportANSI.Amount.Value : 0;
            this.SpinTestAmount.EditValue = this._PCExportReportANSI.AmountTest.HasValue ? this._PCExportReportANSI.AmountTest.Value : 0;
            this.DateReportDate.EditValue = this._PCExportReportANSI.ReportDate.Value;
            this.TxtProduct.EditValue = this._PCExportReportANSI.Product;
            this.txtBatchNo.Text = this._PCExportReportANSI.ProductBatchNo;
            this.spinQtyTest.EditValue = this._PCExportReportANSI.QuYangShu2.HasValue ? this._PCExportReportANSI.QuYangShu2.Value : 0;
            this.checkVisualTest.Checked = this._PCExportReportANSI.VisualTest.HasValue ? this._PCExportReportANSI.VisualTest.Value : false;
            this.checkThemal.Checked = this._PCExportReportANSI.ThermalStability.HasValue ? this._PCExportReportANSI.ThermalStability.Value : false;
            this.checkPrismaticHIn.Checked = this._PCExportReportANSI.PrismaticPowerHIn.HasValue ? this._PCExportReportANSI.PrismaticPowerHIn.Value : false;
            this.checkPrismaticHOut.Checked = this._PCExportReportANSI.PrismaticPowerHOut.HasValue ? this._PCExportReportANSI.PrismaticPowerHOut.Value : false;
            this.checkPrismaticVDwn.Checked = this._PCExportReportANSI.PrismaticPowerVDwn.HasValue ? this._PCExportReportANSI.PrismaticPowerVDwn.Value : false;
            this.checkPrismaticVUp.Checked = this._PCExportReportANSI.PrismaticPowerVUp.HasValue ? this._PCExportReportANSI.PrismaticPowerVUp.Value : false;
            this.checkScatter.Checked = this._PCExportReportANSI.ScatterLight.HasValue ? this._PCExportReportANSI.ScatterLight.Value : false;
            this.checkMedium.Checked = this._PCExportReportANSI.MediumImpact.HasValue ? this._PCExportReportANSI.MediumImpact.Value : false;
            this.checkHigh.Checked = this._PCExportReportANSI.HighImpact.HasValue ? this._PCExportReportANSI.HighImpact.Value : false;
            this.checkExtraHigh.Checked = this._PCExportReportANSI.ExtraHighImpact.HasValue ? this._PCExportReportANSI.ExtraHighImpact.Value : false;
            this.checkPemertration.Checked = this._PCExportReportANSI.PermertrationTest.HasValue ? this._PCExportReportANSI.PermertrationTest.Value : false;
            this.checkIgnition.Checked = this._PCExportReportANSI.IgnitionTest.HasValue ? this._PCExportReportANSI.IgnitionTest.Value : false;
            this.checkCorrsion.Checked = this._PCExportReportANSI.Corrsion.HasValue ? this._PCExportReportANSI.Corrsion.Value : false;
            this.checkMarkings.Checked = this._PCExportReportANSI.Markings.HasValue ? this._PCExportReportANSI.Markings.Value : false;

            this.newChooseContorlAuditEmp.EditValue = this._PCExportReportANSI.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCExportReportANSI.AuditState);
            this.lookUpEditUnit.EditValue = this._PCExportReportANSI.ProductUnitId;
            string[] str = new string[] { };
            if (!string.IsNullOrEmpty(this._PCExportReportANSI.ApprovalDate))
                str = this._PCExportReportANSI.ApprovalDate.Split(' ');
            if (str.Length > 0)
                this.txt_PZEmp.Text = str[0];
            else
                this.txt_PZEmp.Text = null;
            if (str.Length > 1)
            {
                this.date_ApprovalDate.EditValue = str[1];
            }
            else
                this.date_ApprovalDate.EditValue = null;
        }

        //搜索
        private void BarBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm(this.Text, isDelete);
            f.etype = this._PCExportReportANSI.ExpType == null ? null : this._PCExportReportANSI.ExpType.ToString();
            if (f.ShowDialog(this) == DialogResult.OK)
            {

                Model.PCExportReportANSI currentModel = f.SelectItem as Model.PCExportReportANSI;
                if (currentModel != null)
                {
                    this._PCExportReportANSI = currentModel;
                    this._PCExportReportANSI = this._PCExportReportANSIManager.Get(this._PCExportReportANSI.ExportReportId);
                    this.Refresh();
                }
            }

            f.Dispose();
            GC.Collect();
        }

        //点击标签,弹出对应子窗口
        private void ChildFrmLable_Click(object sender, EventArgs e)
        {
            //Label lbl = sender as Label;
            //if (lbl.Tag != null && !string.IsNullOrEmpty(lbl.Tag.ToString()))
            //{
            //    MessageBox.Show(this.getName() + "," + this.GetType().ToString() + "," + lbl.Tag.ToString());
            //}
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCExportReportANSI.PRO_ExportReportId;
        }

        protected override int AuditState()
        {
            return this._PCExportReportANSI.AuditState.HasValue ? this._PCExportReportANSI.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCExportReportANSI" + "," + this._PCExportReportANSI.ExportReportId;
        }

        #endregion

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tag = 1;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            ASRO r = new ASRO(this._PCExportReportANSI, tag);
            //r.ShowPreviewDialog();
            if (canSave)
            {
                if (this._PCExportReportANSI != null && !string.IsNullOrEmpty(this._PCExportReportANSI.ExportReportId))
                {
                    string sfdir = this._ServerSavePath + "\\" + this._PCExportReportANSI.ExportReportId;
                    try
                    {
                        System.IO.Directory.CreateDirectory(sfdir);
                        r.ExportToPdf(sfdir + "\\" + this._PCExportReportANSI.ExportReportId + ".pdf");
                        MessageBox.Show("文件已導出為pdf格式上傳至服務器");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }
            }
            r.ShowPreviewDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tag = 2;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            ASRO r = new ASRO(this._PCExportReportANSI, tag);
            //r.ShowPreviewDialog();
            if (canSave)
            {
                if (this._PCExportReportANSI != null && !string.IsNullOrEmpty(this._PCExportReportANSI.ExportReportId))
                {
                    string sfdir = this._ServerSavePath + "\\" + this._PCExportReportANSI.ExportReportId;
                    try
                    {
                        System.IO.Directory.CreateDirectory(sfdir);
                        r.ExportToPdf(sfdir + "\\" + this._PCExportReportANSI.ExportReportId + ".pdf");
                        MessageBox.Show("文件已導出為pdf格式上傳至服務器");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }
            }
            r.ShowPreviewDialog();
        }
    }


}