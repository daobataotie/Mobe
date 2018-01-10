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
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        Model.PCExportReportANSI _pcExpANSI = null;
        BL.PCExportReportANSIManager _pcExpANSIManager = new Book.BL.PCExportReportANSIManager();
        string _ServerSavePath = string.Empty;      //附件存放地址
        int tag;                                    //列印标志

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_ExportReportId, new AA(Properties.Resources.NumsIsNotNull, this.txtPCExportReportId));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.btnEditProduct));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_ReportDate, new AA(Properties.Resources.DateNotNull, this.dateEditReport));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_AmountTest, new AA("測試數量不能為空", this.spinEditCSSL));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_InvoiceCusXOId, new AA("客戶訂單編號不能為空", this.txtCusXOid));

            //this.invalidValueExceptions.Add(Model.PCExportReportANSI.PRO_AmountTest + "_ForInvoiceXoQuantity", new AA("測試數量未達標:測試數量≈訂單數量/500(不齊不足1)", this.spinEditCSSL));
            //this.invalidValueExceptions.Add(Model.PCExportReportANSI.PRO_AmountTest + "_ForDetailsCount", new AA("測試數量未達標:測試數量詳細測試數量總和", this.spinEditCSSL));

            this.nccCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();

            //取得服务器附件存储地址
            if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessoriesPath"] != null)
            {
                this._ServerSavePath = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessoriesPath"].Value;
            }
            this.action = "view";
        }

        int sign = 0;
        public EditForm(string invoiceId)
            : this()
        {
            this._pcExpANSI = this._pcExpANSIManager.Get(invoiceId);
            if (this._pcExpANSI == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            this.sign = 1;
        }

        public EditForm(Model.PCExportReportANSI mPCExpANSI)
            : this()
        {
            if (mPCExpANSI == null)
                throw new ArithmeticException("invoiceid");
            this._pcExpANSI = mPCExpANSI;
            this.action = "view";
            this.sign = 1;
        }

        public EditForm(Model.PCExportReportANSI mPCExpANSI, string action)
            : this()
        {
            if (mPCExpANSI == null)
                throw new ArithmeticException("invoiceid");
            this._pcExpANSI = mPCExpANSI;
            this.action = action;
            this.sign = 1;
        }

        protected override void AddNew()
        {
            this._pcExpANSI = new Book.Model.PCExportReportANSI();
            this._pcExpANSI.ExportReportId = this._pcExpANSIManager.GetId();
            this._pcExpANSI.ReportDate = DateTime.Now.Date;
            this._pcExpANSI.ExpType = "ANSI";
        }

        protected override void Delete()
        {
            if (this._pcExpANSI == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pcExpANSIManager.Delete(this._pcExpANSI.ExportReportId);

            this._pcExpANSI = this._pcExpANSIManager.mget_next("ANSI", this._pcExpANSI.InsertTime.Value);

            if (this._pcExpANSI == null)
            {
                this._pcExpANSI = this._pcExpANSIManager.mget_last("ANSI");
            }
        }

        protected override void MoveLast()
        {
            if (this.sign == 1)
            {
                this.sign = 0;
                return;
            }
            this._pcExpANSI = this._pcExpANSIManager.mget_last("ANSI");
        }

        protected override void MoveFirst()
        {
            this._pcExpANSI = this._pcExpANSIManager.mget_first("ANSI");
        }

        protected override bool HasRows()
        {
            return this._pcExpANSIManager.mhas_rows("ANSI");
        }

        protected override bool HasRowsNext()
        {
            return this._pcExpANSIManager.mhas_rows_after("ANSI", this._pcExpANSI.InsertTime.Value);
        }

        protected override bool HasRowsPrev()
        {
            return this._pcExpANSIManager.mhas_rows_before("ANSI", this._pcExpANSI.InsertTime.Value);
        }

        protected override void MoveNext()
        {
            Model.PCExportReportANSI pcer = this._pcExpANSIManager.mget_next("ANSI", this._pcExpANSI.InsertTime.Value);
            if (pcer == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcExpANSI = pcer;
        }

        protected override void MovePrev()
        {
            Model.PCExportReportANSI pcer = this._pcExpANSIManager.mget_prev("ANSI", this._pcExpANSI.InsertTime.Value);
            if (pcer == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcExpANSI = pcer;
        }

        protected override void Save()
        {
            this._pcExpANSI.ExportReportId = this.txtPCExportReportId.Text;
            this._pcExpANSI.Amount = this.spinEditDDSL.EditValue == null ? 0 : double.Parse(this.spinEditDDSL.EditValue.ToString());
            this._pcExpANSI.AmountTest = this.spinEditCSSL.EditValue == null ? 0 : double.Parse(this.spinEditCSSL.EditValue.ToString());
            this._pcExpANSI.InvoiceCusXOId = this.txtCusXOid.Text;
            this._pcExpANSI.Customer = (this.nccCustomer.EditValue as Model.Customer);
            this._pcExpANSI.ReportDate = this.dateEditReport.EditValue == null ? DateTime.Now : this.dateEditReport.DateTime;

            if (this._pcExpANSI.Customer != null)
            {
                this._pcExpANSI.CustomerId = this._pcExpANSI.Customer.CustomerId;
            }
            this._pcExpANSI.Employee = (this.nccEmployee.EditValue as Model.Employee);
            if (this._pcExpANSI.Employee != null)
            {
                this._pcExpANSI.EmployeeId = this._pcExpANSI.Employee.EmployeeId;
            }
            this._pcExpANSI.Product = (this.btnEditProduct.EditValue as Model.Product);
            if (this._pcExpANSI.Product != null)
            {
                this._pcExpANSI.ProductId = this._pcExpANSI.Product.ProductId;
            }

            this._pcExpANSI.Criteria3 = this.txtCriteria3.Text;

            this._pcExpANSI.PanDing0 = this.spinPanDing0.EditValue == null ? 0 : double.Parse(this.spinPanDing0.EditValue.ToString());
            this._pcExpANSI.PanDing1 = this.spinPanDing1.EditValue == null ? 0 : double.Parse(this.spinPanDing1.EditValue.ToString());
            this._pcExpANSI.PanDing2 = this.spinPanDing2.EditValue == null ? 0 : double.Parse(this.spinPanDing2.EditValue.ToString());
            this._pcExpANSI.PanDing3 = this.spinPanDing3.EditValue == null ? 0 : double.Parse(this.spinPanDing3.EditValue.ToString());
            this._pcExpANSI.PanDing4 = this.spinPanDing4.EditValue == null ? 0 : double.Parse(this.spinPanDing4.EditValue.ToString());
            this._pcExpANSI.PanDing5 = this.spinPanDing5.EditValue == null ? 0 : double.Parse(this.spinPanDing5.EditValue.ToString());
            this._pcExpANSI.PanDing6 = this.spinPanDing6.EditValue == null ? 0 : double.Parse(this.spinPanDing6.EditValue.ToString());
            this._pcExpANSI.PanDing7 = this.spinPanDing7.EditValue == null ? 0 : double.Parse(this.spinPanDing7.EditValue.ToString());
            this._pcExpANSI.PanDing8 = this.spinPanDing8.EditValue == null ? 0 : double.Parse(this.spinPanDing8.EditValue.ToString());
            this._pcExpANSI.PanDing9 = this.spinPanDing9.EditValue == null ? 0 : double.Parse(this.spinPanDing9.EditValue.ToString());
            this._pcExpANSI.PanDing10 = this.spinPanDing10.EditValue == null ? 0 : double.Parse(this.spinPanDing10.EditValue.ToString());
            this._pcExpANSI.PanDing11 = this.spinPanDing11.EditValue == null ? 0 : double.Parse(this.spinPanDing11.EditValue.ToString());
            this._pcExpANSI.PanDingShu12 = this.spinPanDing12.EditValue == null ? 0 : double.Parse(this.spinPanDing12.EditValue.ToString());

            this._pcExpANSI.QuYangShu1 = this.spinQuYangShu1.EditValue == null ? 0 : double.Parse(this.spinQuYangShu1.EditValue.ToString());
            this._pcExpANSI.QuYangShu2 = this.spinQuYangShu2.EditValue == null ? 0 : double.Parse(this.spinQuYangShu2.EditValue.ToString());
            this._pcExpANSI.QuYangShu3 = this.spinQuYangShu3.EditValue == null ? 0 : double.Parse(this.spinQuYangShu3.EditValue.ToString());
            this._pcExpANSI.QuYangShu4 = this.spinQuYangShu4.EditValue == null ? 0 : double.Parse(this.spinQuYangShu4.EditValue.ToString());
            this._pcExpANSI.QuYangShu5 = this.spinQuYangShu5.EditValue == null ? 0 : double.Parse(this.spinQuYangShu5.EditValue.ToString());
            this._pcExpANSI.QuYangShu6 = this.spinQuYangShu6.EditValue == null ? 0 : double.Parse(this.spinQuYangShu6.EditValue.ToString());
            this._pcExpANSI.QuYangShu7 = this.spinQuYangShu7.EditValue == null ? 0 : double.Parse(this.spinQuYangShu7.EditValue.ToString());
            this._pcExpANSI.QuYangShu8 = this.spinQuYangShu8.EditValue == null ? 0 : double.Parse(this.spinQuYangShu8.EditValue.ToString());
            this._pcExpANSI.QuYangShu9 = this.spinQuYangShu9.EditValue == null ? 0 : double.Parse(this.spinQuYangShu9.EditValue.ToString());
            this._pcExpANSI.QuYangShu10 = this.spinQuYangShu10.EditValue == null ? 0 : double.Parse(this.spinQuYangShu10.EditValue.ToString());
            this._pcExpANSI.QuYangShu11 = this.spinQuYangShu11.EditValue == null ? 0 : double.Parse(this.spinQuYangShu11.EditValue.ToString());
            this._pcExpANSI.QuYangShu12 = this.spinQuYangShu12.EditValue == null ? 0 : double.Parse(this.spinQuYangShu12.EditValue.ToString());

            this._pcExpANSI.ShouCeShu1 = this.spinShouCeShu1.EditValue == null ? 0 : double.Parse(this.spinShouCeShu1.EditValue.ToString());
            this._pcExpANSI.ShouCeShu2 = this.spinShouCeShu2.EditValue == null ? 0 : double.Parse(this.spinShouCeShu2.EditValue.ToString());
            this._pcExpANSI.ShouCeShu3 = this.spinShouCeShu3.EditValue == null ? 0 : double.Parse(this.spinShouCeShu3.EditValue.ToString());
            this._pcExpANSI.ShouCeShu4 = this.spinShouCeShu4.EditValue == null ? 0 : double.Parse(this.spinShouCeShu4.EditValue.ToString());
            this._pcExpANSI.ShouCeShu5 = this.spinShouCeShu5.EditValue == null ? 0 : double.Parse(this.spinShouCeShu5.EditValue.ToString());
            this._pcExpANSI.ShouCeShu6 = this.spinShouCeShu6.EditValue == null ? 0 : double.Parse(this.spinShouCeShu6.EditValue.ToString());
            this._pcExpANSI.ShouCeShu7 = this.spinShouCeShu7.EditValue == null ? 0 : double.Parse(this.spinShouCeShu7.EditValue.ToString());
            this._pcExpANSI.ShouCeShu8 = this.spinShouCeShu8.EditValue == null ? 0 : double.Parse(this.spinShouCeShu8.EditValue.ToString());
            this._pcExpANSI.ShouCeShu9 = this.spinShouCeShu9.EditValue == null ? 0 : double.Parse(this.spinShouCeShu9.EditValue.ToString());
            this._pcExpANSI.ShouCeShu10 = this.spinShouCeShu10.EditValue == null ? 0 : double.Parse(this.spinShouCeShu10.EditValue.ToString());
            this._pcExpANSI.ShouCeShu11 = this.spinShouCeShu11.EditValue == null ? 0 : double.Parse(this.spinShouCeShu11.EditValue.ToString());
            this._pcExpANSI.ShouCeShu12 = this.spinShouCeShu12.EditValue == null ? 0 : double.Parse(this.spinShouCeShu12.EditValue.ToString());
            this._pcExpANSI.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();

            switch (this.action)
            {
                case "insert":
                    this._pcExpANSIManager.Insert(this._pcExpANSI);
                    break;
                case "update":
                    this._pcExpANSIManager.Update(this._pcExpANSI);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._pcExpANSI == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            if (this.action == "view")
            {
                this._pcExpANSI = this._pcExpANSIManager.Get(this._pcExpANSI.ExportReportId);
            }

            this.InitControls();

            base.Refresh();

            switch (this.action)
            {
                case "insert":
                    this.barBtnInvoiceXO.Enabled = true;
                    this.nccCustomer.Enabled = true;
                    break;
                case "update":
                    this.barBtnInvoiceXO.Enabled = true;
                    this.nccCustomer.Enabled = true;
                    break;
                case "view":
                    this.barBtnInvoiceXO.Enabled = false;
                    this.nccCustomer.Enabled = false;
                    break;
            }
            this.txtPCExportReportId.Properties.ReadOnly = true;
            this.txtCusXOid.Enabled = false;
            this.btnEditProduct.Enabled = false;
            this.spinEditDDSL.Enabled = false;
        }

        //列印
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            tag = 0;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            RO r = new RO(this._pcExpANSI, tag);
            //r.ShowPreviewDialog();
            if (canSave)
            {
                if (this._pcExpANSI != null && !string.IsNullOrEmpty(this._pcExpANSI.ExportReportId))
                {
                    string sfdir = this._ServerSavePath + "\\" + this._pcExpANSI.ExportReportId;
                    try
                    {
                        System.IO.Directory.CreateDirectory(sfdir);
                        r.ExportToPdf(sfdir + "\\" + this._pcExpANSI.ExportReportId + ".pdf");
                        MessageBox.Show("文件已導出為pdf格式上傳至服務器");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }
            }
            return r;
        }

        //控件赋值
        private void InitControls()
        {
            this.txtCriteria3.Text = this._pcExpANSI.Criteria3;
            this.txtCusXOid.Text = this._pcExpANSI.InvoiceCusXOId;
            this.txtPCExportReportId.Text = this._pcExpANSI.ExportReportId;
            this.dateEditReport.EditValue = this._pcExpANSI.ReportDate.Value;
            this.spinEditDDSL.EditValue = this._pcExpANSI.Amount.HasValue ? this._pcExpANSI.Amount.Value : 0;
            this.spinEditCSSL.EditValue = this._pcExpANSI.AmountTest.HasValue ? this._pcExpANSI.AmountTest.Value : 0;

            this.btnEditProduct.EditValue = this._pcExpANSI.Product;
            this.nccCustomer.EditValue = this._pcExpANSI.Customer;
            this.nccEmployee.EditValue = this._pcExpANSI.Employee;

            this.spinPanDing0.EditValue = this._pcExpANSI.PanDing0.HasValue ? this._pcExpANSI.PanDing0.Value : 0;
            this.spinPanDing1.EditValue = this._pcExpANSI.PanDing1.HasValue ? this._pcExpANSI.PanDing1.Value : 0;
            this.spinPanDing2.EditValue = this._pcExpANSI.PanDing2.HasValue ? this._pcExpANSI.PanDing2.Value : 0;
            this.spinPanDing3.EditValue = this._pcExpANSI.PanDing3.HasValue ? this._pcExpANSI.PanDing3.Value : 0;
            this.spinPanDing4.EditValue = this._pcExpANSI.PanDing4.HasValue ? this._pcExpANSI.PanDing4.Value : 0;
            this.spinPanDing5.EditValue = this._pcExpANSI.PanDing5.HasValue ? this._pcExpANSI.PanDing5.Value : 0;
            this.spinPanDing6.EditValue = this._pcExpANSI.PanDing6.HasValue ? this._pcExpANSI.PanDing6.Value : 0;
            this.spinPanDing7.EditValue = this._pcExpANSI.PanDing7.HasValue ? this._pcExpANSI.PanDing7.Value : 0;
            this.spinPanDing8.EditValue = this._pcExpANSI.PanDing8.HasValue ? this._pcExpANSI.PanDing8.Value : 0;
            this.spinPanDing9.EditValue = this._pcExpANSI.PanDing9.HasValue ? this._pcExpANSI.PanDing9.Value : 0;
            this.spinPanDing10.EditValue = this._pcExpANSI.PanDing10.HasValue ? this._pcExpANSI.PanDing10.Value : 0;
            this.spinPanDing11.EditValue = this._pcExpANSI.PanDing11.HasValue ? this._pcExpANSI.PanDing11.Value : 0;
            this.spinPanDing12.EditValue = this._pcExpANSI.PanDingShu12.HasValue ? this._pcExpANSI.PanDingShu12.Value : 0;

            this.spinQuYangShu1.EditValue = this._pcExpANSI.QuYangShu1.HasValue ? this._pcExpANSI.QuYangShu1.Value : 0;
            this.spinQuYangShu2.EditValue = this._pcExpANSI.QuYangShu2.HasValue ? this._pcExpANSI.QuYangShu2.Value : 0;
            this.spinQuYangShu3.EditValue = this._pcExpANSI.QuYangShu3.HasValue ? this._pcExpANSI.QuYangShu3.Value : 0;
            this.spinQuYangShu4.EditValue = this._pcExpANSI.QuYangShu4.HasValue ? this._pcExpANSI.QuYangShu4.Value : 0;
            this.spinQuYangShu5.EditValue = this._pcExpANSI.QuYangShu5.HasValue ? this._pcExpANSI.QuYangShu5.Value : 0;
            this.spinQuYangShu6.EditValue = this._pcExpANSI.QuYangShu6.HasValue ? this._pcExpANSI.QuYangShu6.Value : 0;
            this.spinQuYangShu7.EditValue = this._pcExpANSI.QuYangShu7.HasValue ? this._pcExpANSI.QuYangShu7.Value : 0;
            this.spinQuYangShu8.EditValue = this._pcExpANSI.QuYangShu8.HasValue ? this._pcExpANSI.QuYangShu8.Value : 0;
            this.spinQuYangShu9.EditValue = this._pcExpANSI.QuYangShu9.HasValue ? this._pcExpANSI.QuYangShu9.Value : 0;
            this.spinQuYangShu10.EditValue = this._pcExpANSI.QuYangShu10.HasValue ? this._pcExpANSI.QuYangShu10.Value : 0;
            this.spinQuYangShu11.EditValue = this._pcExpANSI.QuYangShu11.HasValue ? this._pcExpANSI.QuYangShu11.Value : 0;
            this.spinQuYangShu12.EditValue = this._pcExpANSI.QuYangShu12.HasValue ? this._pcExpANSI.QuYangShu12.Value : 0;

            this.spinShouCeShu1.EditValue = this._pcExpANSI.ShouCeShu1.HasValue ? this._pcExpANSI.ShouCeShu1.Value : 0;
            this.spinShouCeShu2.EditValue = this._pcExpANSI.ShouCeShu2.HasValue ? this._pcExpANSI.ShouCeShu2.Value : 0;
            this.spinShouCeShu3.EditValue = this._pcExpANSI.ShouCeShu3.HasValue ? this._pcExpANSI.ShouCeShu3.Value : 0;
            this.spinShouCeShu4.EditValue = this._pcExpANSI.ShouCeShu4.HasValue ? this._pcExpANSI.ShouCeShu4.Value : 0;
            this.spinShouCeShu5.EditValue = this._pcExpANSI.ShouCeShu5.HasValue ? this._pcExpANSI.ShouCeShu5.Value : 0;
            this.spinShouCeShu6.EditValue = this._pcExpANSI.ShouCeShu6.HasValue ? this._pcExpANSI.ShouCeShu6.Value : 0;
            this.spinShouCeShu7.EditValue = this._pcExpANSI.ShouCeShu7.HasValue ? this._pcExpANSI.ShouCeShu7.Value : 0;
            this.spinShouCeShu8.EditValue = this._pcExpANSI.ShouCeShu8.HasValue ? this._pcExpANSI.ShouCeShu8.Value : 0;
            this.spinShouCeShu9.EditValue = this._pcExpANSI.ShouCeShu9.HasValue ? this._pcExpANSI.ShouCeShu9.Value : 0;
            this.spinShouCeShu10.EditValue = this._pcExpANSI.ShouCeShu10.HasValue ? this._pcExpANSI.ShouCeShu10.Value : 0;
            this.spinShouCeShu11.EditValue = this._pcExpANSI.ShouCeShu11.HasValue ? this._pcExpANSI.ShouCeShu11.Value : 0;
            this.spinShouCeShu12.EditValue = this._pcExpANSI.ShouCeShu12.HasValue ? this._pcExpANSI.ShouCeShu12.Value : 0;

            this.newChooseContorlAuditEmp.EditValue = this._pcExpANSI.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pcExpANSI.AuditState);
            this.lookUpEditUnit.EditValue = this._pcExpANSI.ProductUnitId;
        }

        //选择客户订单
        private void barBtnInvoiceXO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            createProduce.EditForm f = new Book.UI.produceManager.createProduce.EditForm();
            if (f.ShowDialog(this) != DialogResult.OK)
                return;
            if (f.SelectList == null || f.SelectList.Count == 0)
                return;
            Model.InvoiceXODetail xod = f.SelectList[0];

            this._pcExpANSI.Customer = xod.Invoice.xocustomer;
            this._pcExpANSI.CustomerId = xod.Invoice.xocustomer.CustomerId;
            this._pcExpANSI.Specification = xod.Invoice.xocustomer.CheckedStandard;
            this._pcExpANSI.Product = xod.Product;
            this._pcExpANSI.ProductId = xod.Product.ProductId;
            this._pcExpANSI.InvoiceCusXOId = xod.Invoice.CustomerInvoiceXOId;
            this._pcExpANSI.Amount = xod.InvoiceXODetailQuantity.HasValue ? xod.InvoiceXODetailQuantity.Value : 0;

            //获取ANSI,Finish质检统计记录
            //Model.PCExportReportANSI mPCExpANSI = this._pcExpANSIManager.SelectForExpANSI(xod.Invoice.CustomerInvoiceXOId, xod.Product.ProductId);
            //Model.PCExportReportANSIDetail mPCExpANSIDet = new BL.PCExportReportANSIDetailManager().SelectForExpANSIDetailsSUM(xod.Invoice.CustomerInvoiceXOId, xod.Product.ProductId);
            //if (mPCExpANSIDet != null)
            //{
                //测试数量
                //this._pcExpANSI.AmountTest = mPCExpANSI.mCountANSI;

                //this._pcExpANSI.PanDing0 = mPCExpANSI.SUMIsMuShiJianYan;
                //this._pcExpANSI.PanDing3 = mPCExpANSI.SUMIsKeJianGuang;
                //this._pcExpANSI.PanDing4 = mPCExpANSI.SUMIsZiWaiXian;
                //this._pcExpANSI.PanDing7 = mPCExpANSI.SUMIsGaoSuChongJi;
                //this._pcExpANSI.PanDing8 = mPCExpANSI.SUMIsYuanZhuiZhuiLuo;
                //this._pcExpANSI.PanDing10 = mPCExpANSI.SUMIsFogPassing;
                //this._pcExpANSI.PanDing11 = mPCExpANSI.SUMIsNaiRanXing;
                //this._pcExpANSI.PanDing9 = mPCExpANSI.SUMIsPenetrate;

                //this._pcExpANSI.ShouCeShu3 = mPCExpANSI.mCountOptics;
                //this._pcExpANSI.ShouCeShu4 = mPCExpANSI.mCountOptics;

                //this._pcExpANSI.ShouCeShu7 = mPCExpANSI.mCountANSI;
                //this._pcExpANSI.ShouCeShu8 = mPCExpANSI.mCountANSI;
                //this._pcExpANSI.ShouCeShu11 = mPCExpANSI.mCountANSI;

                //this._pcExpANSI.ShouCeShu10 = mPCExpANSI.mCountFog;

                //this._pcExpANSI.ShouCeShu9 = mPCExpANSI.mCountPenetrate;

                //this._pcExpANSI.QuYangShu3 = this._pcExpANSI.QuYangShu4 = mPCExpANSI.mCountOptics;
                //this._pcExpANSI.QuYangShu7 = this._pcExpANSI.QuYangShu8 = this._pcExpANSI.QuYangShu11 = mPCExpANSI.mCountANSI;
                //this._pcExpANSI.QuYangShu10 = mPCExpANSI.mCountFog;
                //this._pcExpANSI.QuYangShu9 = mPCExpANSI.mCountPenetrate;

                #region 测试数量与合格数量

                //受测数量默认为订单数量的1/500,无条件进位.最大12
                int mInvoiceXoDetailQuantity = int.Parse(this._pcExpANSI.Amount.HasValue ? this._pcExpANSI.Amount.ToString() : "0");

                double mMustCheck = 0;

                if (mInvoiceXoDetailQuantity < 500)
                    mMustCheck = 1;
                else
                    mMustCheck = mInvoiceXoDetailQuantity % 500 == 0 ? mInvoiceXoDetailQuantity / 500 : mInvoiceXoDetailQuantity / 500 + 1;

                this._pcExpANSI.AmountTest = mMustCheck > 12 ? 12 : mMustCheck;    //受测数量1/500订单数量,上限12个,无条件进位

                this._pcExpANSI.ShouCeShu1 = this._pcExpANSI.ShouCeShu2 = this._pcExpANSI.ShouCeShu3 = this._pcExpANSI.ShouCeShu4 = this._pcExpANSI.ShouCeShu5 = this._pcExpANSI.ShouCeShu6 = this._pcExpANSI.ShouCeShu7 = this._pcExpANSI.ShouCeShu8 = this._pcExpANSI.ShouCeShu9 = this._pcExpANSI.ShouCeShu10 = this._pcExpANSI.ShouCeShu11 = this._pcExpANSI.ShouCeShu12 = this._pcExpANSI.AmountTest;

                //this._pcExpANSI.PanDing0 = mPCExpANSIDet.pMSJY;
                //this._pcExpANSI.QuYangShu1 = mPCExpANSIDet.qQXD;
                //this._pcExpANSI.PanDing1 = mPCExpANSIDet.pQXD;
                //this._pcExpANSI.QuYangShu2 = mPCExpANSIDet.qLJPHDS;
                //this._pcExpANSI.PanDing2 = mPCExpANSIDet.pLJPHDS;
                //this._pcExpANSI.QuYangShu3 = mPCExpANSIDet.qKJGTSL;
                //this._pcExpANSI.PanDing3 = mPCExpANSIDet.pKJGTSL;
                //this._pcExpANSI.QuYangShu4 = mPCExpANSIDet.qZWXTSL;
                //this._pcExpANSI.PanDing4 = mPCExpANSIDet.pZWXTSL;
                //this._pcExpANSI.QuYangShu5 = mPCExpANSIDet.qQMDS;
                //this._pcExpANSI.PanDing5 = mPCExpANSIDet.pQMDS;
                //this._pcExpANSI.QuYangShu6 = mPCExpANSIDet.qSGDS;
                //this._pcExpANSI.PanDing6 = mPCExpANSIDet.pSGDS;
                //this._pcExpANSI.QuYangShu7 = mPCExpANSIDet.qGSCJCS;
                //this._pcExpANSI.PanDing7 = mPCExpANSIDet.pGSCJCS;
                //this._pcExpANSI.QuYangShu8 = mPCExpANSIDet.qYZZLZJCS;
                //this._pcExpANSI.PanDing8 = mPCExpANSIDet.pYZZLZJCS;
                //this._pcExpANSI.QuYangShu9 = mPCExpANSIDet.qJPCTSC;
                //this._pcExpANSI.PanDing9 = mPCExpANSIDet.pJPCTSC;
                //this._pcExpANSI.QuYangShu10 = mPCExpANSIDet.qWDCS;
                //this._pcExpANSI.PanDing10 = mPCExpANSIDet.pWDCS;
                //this._pcExpANSI.QuYangShu11 = mPCExpANSIDet.qNRXCS;
                //this._pcExpANSI.PanDing11 = mPCExpANSIDet.pNRXCS;
                //this._pcExpANSI.QuYangShu12 = mPCExpANSIDet.qCSAQXD;
                //this._pcExpANSI.PanDingShu12 = mPCExpANSIDet.pCSAQXD;
                #endregion
            //}
            this.InitControls();
        }

        //搜索
        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm(this.Text, isDelete);
            f.etype = this._pcExpANSI.ExpType == null ? null : this._pcExpANSI.ExpType.ToString();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCExportReportANSI currentModel = f.SelectItem as Model.PCExportReportANSI;
                if (currentModel != null)
                {
                    this._pcExpANSI = currentModel;
                    this._pcExpANSI = this._pcExpANSIManager.Get(this._pcExpANSI.ExportReportId);
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
            return this._pcExpANSI.AuditState.HasValue ? this._pcExpANSI.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCExportReportANSI" + "," + this._pcExpANSI.ExportReportId;
        }

        #endregion

        private void barPrintAlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tag = 1;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            RO r = new RO(this._pcExpANSI, tag);
            //r.ShowPreviewDialog();
            if (canSave)
            {
                if (this._pcExpANSI != null && !string.IsNullOrEmpty(this._pcExpANSI.ExportReportId))
                {
                    string sfdir = this._ServerSavePath + "\\" + this._pcExpANSI.ExportReportId;
                    try
                    {
                        System.IO.Directory.CreateDirectory(sfdir);
                        r.ExportToPdf(sfdir + "\\" + this._pcExpANSI.ExportReportId + ".pdf");
                        MessageBox.Show("文件已導出為pdf格式上傳至服務器");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }
            }
            r.ShowPreviewDialog();

        }

        private void barPrintPPE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tag = 2;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            RO r = new RO(this._pcExpANSI, tag);
            //r.ShowPreviewDialog();
            if (canSave)
            {
                if (this._pcExpANSI != null && !string.IsNullOrEmpty(this._pcExpANSI.ExportReportId))
                {
                    string sfdir = this._ServerSavePath + "\\" + this._pcExpANSI.ExportReportId;
                    try
                    {
                        System.IO.Directory.CreateDirectory(sfdir);
                        r.ExportToPdf(sfdir + "\\" + this._pcExpANSI.ExportReportId + ".pdf");
                        MessageBox.Show("文件已導出為pdf格式上傳至服務器");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }
            }
            r.ShowPreviewDialog();
        }
    }
}