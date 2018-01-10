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
    public partial class ANSI2015 : Book.UI.Settings.BasicData.BaseEditForm
    {
        public Model.PCExportReportANSI _PCExportReportANSI = null;
        BL.PCExportReportANSIManager _PCExportReportANSIManager = new Book.BL.PCExportReportANSIManager();
        BL.PCExportReportANSIManager _pcExpANSIManager = new Book.BL.PCExportReportANSIManager();
        string _ServerSavePath = string.Empty;      //附件存放地址
        int tag;                                    //列印标志

        public ANSI2015()
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


            this.NccCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.NccTestPerson.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();
            this.action = "view";
        }

        int sign = 0;
        public ANSI2015(Model.PCExportReportANSI mPCExpANSI)
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
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_last("ANSI2015");
        }

        protected override void MoveFirst()
        {
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_first("ANSI2015");
        }

        protected override void MovePrev()
        {
            Model.PCExportReportANSI csa = this._PCExportReportANSIManager.mget_prev("ANSI2015", this._PCExportReportANSI.InsertTime.Value);
            if (csa == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._PCExportReportANSI = csa;
        }

        protected override void MoveNext()
        {
            Model.PCExportReportANSI csa = this._PCExportReportANSIManager.mget_next("ANSI2015", this._PCExportReportANSI.InsertTime.Value);
            if (csa == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._PCExportReportANSI = csa;
        }

        protected override bool HasRows()
        {
            return this._PCExportReportANSIManager.mhas_rows("ANSI2015");
        }

        protected override bool HasRowsPrev()
        {
            return this._PCExportReportANSIManager.mhas_rows_before("ANSI2015", this._PCExportReportANSI.InsertTime.Value);
        }

        protected override bool HasRowsNext()
        {
            return this._PCExportReportANSIManager.mhas_rows_after("ANSI2015", this._PCExportReportANSI.InsertTime.Value);
        }

        protected override void AddNew()
        {
            this._PCExportReportANSI = new Book.Model.PCExportReportANSI();
            this._PCExportReportANSI.ExportReportId = this._PCExportReportANSIManager.GetId();
            this._PCExportReportANSI.ReportDate = DateTime.Now.Date;
            this._PCExportReportANSI.ExpType = "ANSI2015";

        }

        protected override void Delete()
        {
            if (this._PCExportReportANSI == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._PCExportReportANSIManager.Delete(this._PCExportReportANSI.ExportReportId);
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_next("ANSI2015", this._PCExportReportANSI.InsertTime.Value);
            if (this._PCExportReportANSI == null)
            {
                this._PCExportReportANSI = this._PCExportReportANSIManager.mget_last("ANSI2015");
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
            this._PCExportReportANSI.Clearlens = this.memoTrans.EditValue == null ? "" : this.memoTrans.EditValue.ToString();
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


            this._PCExportReportANSI.QuYangShu1 = this.spe_Quyang1.EditValue == null ? 0 : double.Parse(this.spe_Quyang1.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu2 = this.spe_Quyang2.EditValue == null ? 0 : double.Parse(this.spe_Quyang2.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu3 = this.spe_Quyang3.EditValue == null ? 0 : double.Parse(this.spe_Quyang3.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu4 = this.spe_Quyang4.EditValue == null ? 0 : double.Parse(this.spe_Quyang4.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu5 = this.spe_Quyang5.EditValue == null ? 0 : double.Parse(this.spe_Quyang5.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu6 = this.spe_Quyang6.EditValue == null ? 0 : double.Parse(this.spe_Quyang6.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu7 = this.spe_Quyang7.EditValue == null ? 0 : double.Parse(this.spe_Quyang7.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu8 = this.spe_Quyang8.EditValue == null ? 0 : double.Parse(this.spe_Quyang8.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu9 = this.spe_Quyang9.EditValue == null ? 0 : double.Parse(this.spe_Quyang9.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu10 = this.spe_Quyang10.EditValue == null ? 0 : double.Parse(this.spe_Quyang10.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu11 = this.spe_Quyang11.EditValue == null ? 0 : double.Parse(this.spe_Quyang11.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu12 = this.spe_Quyang12.EditValue == null ? 0 : double.Parse(this.spe_Quyang12.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu13 = this.spe_Quyang13.EditValue == null ? 0 : double.Parse(this.spe_Quyang13.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu14 = this.spe_Quyang14.EditValue == null ? 0 : double.Parse(this.spe_Quyang14.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu15 = this.spe_Quyang15.EditValue == null ? 0 : double.Parse(this.spe_Quyang15.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu16 = this.spe_Quyang16.EditValue == null ? 0 : double.Parse(this.spe_Quyang16.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu17 = this.spe_Quyang17.EditValue == null ? 0 : double.Parse(this.spe_Quyang17.EditValue.ToString());
            this._PCExportReportANSI.QuYangShu18 = this.spe_Quyang18.EditValue == null ? 0 : double.Parse(this.spe_Quyang18.EditValue.ToString());



            this._PCExportReportANSI.PanDing1 = this.spe_Panding1.EditValue == null ? 0 : double.Parse(this.spe_Panding1.EditValue.ToString());
            this._PCExportReportANSI.PanDing2 = this.spe_Panding2.EditValue == null ? 0 : double.Parse(this.spe_Panding2.EditValue.ToString());
            this._PCExportReportANSI.PanDing3 = this.spe_Panding3.EditValue == null ? 0 : double.Parse(this.spe_Panding3.EditValue.ToString());
            this._PCExportReportANSI.PanDing4 = this.spe_Panding4.EditValue == null ? 0 : double.Parse(this.spe_Panding4.EditValue.ToString());
            this._PCExportReportANSI.PanDing5 = this.spe_Panding5.EditValue == null ? 0 : double.Parse(this.spe_Panding5.EditValue.ToString());
            this._PCExportReportANSI.PanDing6 = this.spe_Panding6.EditValue == null ? 0 : double.Parse(this.spe_Panding6.EditValue.ToString());
            this._PCExportReportANSI.PanDing7 = this.spe_Panding7.EditValue == null ? 0 : double.Parse(this.spe_Panding7.EditValue.ToString());
            this._PCExportReportANSI.PanDing8 = this.spe_Panding8.EditValue == null ? 0 : double.Parse(this.spe_Panding8.EditValue.ToString());
            this._PCExportReportANSI.PanDing9 = this.spe_Panding9.EditValue == null ? 0 : double.Parse(this.spe_Panding9.EditValue.ToString());
            this._PCExportReportANSI.PanDing10 = this.spe_Panding10.EditValue == null ? 0 : double.Parse(this.spe_Panding10.EditValue.ToString());
            this._PCExportReportANSI.PanDing11 = this.spe_Panding11.EditValue == null ? 0 : double.Parse(this.spe_Panding11.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu12 = this.spe_Panding12.EditValue == null ? 0 : double.Parse(this.spe_Panding12.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu13 = this.spe_Panding13.EditValue == null ? 0 : double.Parse(this.spe_Panding13.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu14 = this.spe_Panding14.EditValue == null ? 0 : double.Parse(this.spe_Panding14.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu15 = this.spe_Panding15.EditValue == null ? 0 : double.Parse(this.spe_Panding15.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu16 = this.spe_Panding16.EditValue == null ? 0 : double.Parse(this.spe_Panding16.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu17 = this.spe_Panding17.EditValue == null ? 0 : double.Parse(this.spe_Panding17.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu18 = this.spe_Panding18.EditValue == null ? 0 : double.Parse(this.spe_Panding18.EditValue.ToString());


            this._PCExportReportANSI.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();

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
            this.TxtProduct.Enabled = false;
            this.TxtCustomersId.Enabled = false;
        }

        //控件赋值
        private void InitControls()
        {
            this.TxtOrderId.Text = this._PCExportReportANSI.ExportReportId == null ? null : this._PCExportReportANSI.ExportReportId;
            this.TxtCustomersId.Text = this._PCExportReportANSI.InvoiceCusXOId;
            this.NccCustomer.EditValue = this._PCExportReportANSI.Customer;
            this.NccTestPerson.EditValue = this._PCExportReportANSI.Employee;
            this.SpinOrderAmount.EditValue = this._PCExportReportANSI.Amount.HasValue ? this._PCExportReportANSI.Amount.Value : 0;
            this.SpinTestAmount.EditValue = this._PCExportReportANSI.AmountTest.HasValue ? this._PCExportReportANSI.AmountTest.Value : 0;
            this.DateReportDate.EditValue = this._PCExportReportANSI.ReportDate.Value;
            this.TxtProduct.EditValue = this._PCExportReportANSI.Product;
            //this.memoTrans.EditValue = this._PCExportReportANSI.Clearlens;
            //this.textTraceMarking.Text = this._PCExportReportANSI.TraceMarking == null ? null : this._PCExportReportANSI.TraceMarking;
            //this.textProtectionone.Text = this._PCExportReportANSI.Protectionone == null ? null : this._PCExportReportANSI.Protectionone;
            //this.textProtectiontwo.Text = this._PCExportReportANSI.Protectiontwo == null ? null : this._PCExportReportANSI.Protectiontwo;
            this.memoTrans.EditValue = this._PCExportReportANSI.Clearlens;
            this.spe_Quyang1.EditValue = this._PCExportReportANSI.QuYangShu1.HasValue ? this._PCExportReportANSI.QuYangShu1.Value : 0;
            this.spe_Quyang2.EditValue = this._PCExportReportANSI.QuYangShu2.HasValue ? this._PCExportReportANSI.QuYangShu2.Value : 0;
            this.spe_Quyang3.EditValue = this._PCExportReportANSI.QuYangShu3.HasValue ? this._PCExportReportANSI.QuYangShu3.Value : 0;
            this.spe_Quyang4.EditValue = this._PCExportReportANSI.QuYangShu4.HasValue ? this._PCExportReportANSI.QuYangShu4.Value : 0;
            this.spe_Quyang5.EditValue = this._PCExportReportANSI.QuYangShu5.HasValue ? this._PCExportReportANSI.QuYangShu5.Value : 0;
            this.spe_Quyang6.EditValue = this._PCExportReportANSI.QuYangShu6.HasValue ? this._PCExportReportANSI.QuYangShu6.Value : 0;
            this.spe_Quyang7.EditValue = this._PCExportReportANSI.QuYangShu7.HasValue ? this._PCExportReportANSI.QuYangShu7.Value : 0;
            this.spe_Quyang8.EditValue = this._PCExportReportANSI.QuYangShu8.HasValue ? this._PCExportReportANSI.QuYangShu8.Value : 0;
            this.spe_Quyang9.EditValue = this._PCExportReportANSI.QuYangShu9.HasValue ? this._PCExportReportANSI.QuYangShu9.Value : 0;
            this.spe_Quyang10.EditValue = this._PCExportReportANSI.QuYangShu10.HasValue ? this._PCExportReportANSI.QuYangShu10.Value : 0;
            this.spe_Quyang11.EditValue = this._PCExportReportANSI.QuYangShu11.HasValue ? this._PCExportReportANSI.QuYangShu11.Value : 0;
            this.spe_Quyang12.EditValue = this._PCExportReportANSI.QuYangShu12.HasValue ? this._PCExportReportANSI.QuYangShu12.Value : 0;
            this.spe_Quyang13.EditValue = this._PCExportReportANSI.QuYangShu13.HasValue ? this._PCExportReportANSI.QuYangShu13.Value : 0;
            this.spe_Quyang14.EditValue = this._PCExportReportANSI.QuYangShu14.HasValue ? this._PCExportReportANSI.QuYangShu14.Value : 0;
            this.spe_Quyang15.EditValue = this._PCExportReportANSI.QuYangShu15.HasValue ? this._PCExportReportANSI.QuYangShu15.Value : 0;
            this.spe_Quyang16.EditValue = this._PCExportReportANSI.QuYangShu16.HasValue ? this._PCExportReportANSI.QuYangShu16.Value : 0;
            this.spe_Quyang17.EditValue = this._PCExportReportANSI.QuYangShu17.HasValue ? this._PCExportReportANSI.QuYangShu17.Value : 0;
            this.spe_Quyang18.EditValue = this._PCExportReportANSI.QuYangShu18.HasValue ? this._PCExportReportANSI.QuYangShu18.Value : 0;


            this.spe_Panding1.EditValue = this._PCExportReportANSI.PanDing1.HasValue ? this._PCExportReportANSI.PanDing1.Value : 0;
            this.spe_Panding2.EditValue = this._PCExportReportANSI.PanDing2.HasValue ? this._PCExportReportANSI.PanDing2.Value : 0;
            this.spe_Panding3.EditValue = this._PCExportReportANSI.PanDing3.HasValue ? this._PCExportReportANSI.PanDing3.Value : 0;
            this.spe_Panding4.EditValue = this._PCExportReportANSI.PanDing4.HasValue ? this._PCExportReportANSI.PanDing4.Value : 0;
            this.spe_Panding5.EditValue = this._PCExportReportANSI.PanDing5.HasValue ? this._PCExportReportANSI.PanDing5.Value : 0;
            this.spe_Panding6.EditValue = this._PCExportReportANSI.PanDing6.HasValue ? this._PCExportReportANSI.PanDing6.Value : 0;
            this.spe_Panding7.EditValue = this._PCExportReportANSI.PanDing7.HasValue ? this._PCExportReportANSI.PanDing7.Value : 0;
            this.spe_Panding8.EditValue = this._PCExportReportANSI.PanDing8.HasValue ? this._PCExportReportANSI.PanDing8.Value : 0;
            this.spe_Panding9.EditValue = this._PCExportReportANSI.PanDing9.HasValue ? this._PCExportReportANSI.PanDing9.Value : 0;
            this.spe_Panding10.EditValue = this._PCExportReportANSI.PanDing10.HasValue ? this._PCExportReportANSI.PanDing10.Value : 0;
            this.spe_Panding11.EditValue = this._PCExportReportANSI.PanDing11.HasValue ? this._PCExportReportANSI.PanDing11.Value : 0;
            this.spe_Panding12.EditValue = this._PCExportReportANSI.PanDingShu12.HasValue ? this._PCExportReportANSI.PanDingShu12.Value : 0;
            this.spe_Panding13.EditValue = this._PCExportReportANSI.PanDingShu13.HasValue ? this._PCExportReportANSI.PanDingShu13.Value : 0;
            this.spe_Panding14.EditValue = this._PCExportReportANSI.PanDingShu14.HasValue ? this._PCExportReportANSI.PanDingShu14.Value : 0;
            this.spe_Panding15.EditValue = this._PCExportReportANSI.PanDingShu15.HasValue ? this._PCExportReportANSI.PanDingShu15.Value : 0;
            this.spe_Panding16.EditValue = this._PCExportReportANSI.PanDingShu16.HasValue ? this._PCExportReportANSI.PanDingShu16.Value : 0;
            this.spe_Panding17.EditValue = this._PCExportReportANSI.PanDingShu17.HasValue ? this._PCExportReportANSI.PanDingShu17.Value : 0;
            this.spe_Panding18.EditValue = this._PCExportReportANSI.PanDingShu18.HasValue ? this._PCExportReportANSI.PanDingShu18.Value : 0;

            this.newChooseContorlAuditEmp.EditValue = this._PCExportReportANSI.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCExportReportANSI.AuditState);

            this.lookUpEditUnit.EditValue = this._PCExportReportANSI.ProductUnitId;
        }

        //列印
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            tag = 0;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            ANSI2015RO r = new ANSI2015RO(this._PCExportReportANSI, tag);
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
            //Model.PCExportReportANSIDetail _PCExportReportANSIDetail = new BL.PCExportReportANSIDetailManager().SelectForExpCEENDetailsSUM(xd.Invoice.CustomerInvoiceXOId, xd.Product.ProductId);

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

            this._PCExportReportANSI.QuYangShu2 = this._PCExportReportANSI.QuYangShu3 = this._PCExportReportANSI.QuYangShu4 = this._PCExportReportANSI.QuYangShu5 = this._PCExportReportANSI.QuYangShu6 = this._PCExportReportANSI.QuYangShu7 = this._PCExportReportANSI.QuYangShu8 = this._PCExportReportANSI.QuYangShu9 = this._PCExportReportANSI.QuYangShu10 = this._PCExportReportANSI.QuYangShu11 = this._PCExportReportANSI.QuYangShu12 = this._PCExportReportANSI.QuYangShu13 = this._PCExportReportANSI.QuYangShu15 = this._PCExportReportANSI.QuYangShu16 = this._PCExportReportANSI.QuYangShu17 = this._PCExportReportANSI.QuYangShu18 = this._PCExportReportANSI.AmountTest;

            this._PCExportReportANSI.QuYangShu1 = this._PCExportReportANSI.QuYangShu14 = 100;

            #endregion
            //}
            this.InitControls();
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

        private void barPrintAlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tag = 1;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            ANSI2015RO r = new ANSI2015RO(this._PCExportReportANSI, tag);
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

        private void barPrintPPE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tag = 2;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            ANSI2015RO r = new ANSI2015RO(this._PCExportReportANSI, tag);
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