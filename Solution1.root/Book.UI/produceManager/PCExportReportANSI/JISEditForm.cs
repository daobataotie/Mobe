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
    public partial class JISEditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        public Model.PCExportReportANSI _PCExportReportANSI = null;
        BL.PCExportReportANSIManager _PCExportReportANSIManager = new Book.BL.PCExportReportANSIManager();
        BL.PCExportReportANSIManager _pcExpANSIManager = new Book.BL.PCExportReportANSIManager();
        string _ServerSavePath = string.Empty;      //附件存放地址
        int tag;                                    //列印标志

        public JISEditForm()
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
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select(); ;
            this.action = "view";
        }
        int sign = 0;
        public JISEditForm(Model.PCExportReportANSI mPCExpANSI)
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
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_last("JIS");
        }

        protected override void MoveFirst()
        {
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_first("JIS");
        }

        protected override void MovePrev()
        {
            Model.PCExportReportANSI csa = this._PCExportReportANSIManager.mget_prev("JIS", this._PCExportReportANSI.InsertTime.Value);
            if (csa == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._PCExportReportANSI = csa;
        }

        protected override void MoveNext()
        {
            Model.PCExportReportANSI csa = this._PCExportReportANSIManager.mget_next("JIS", this._PCExportReportANSI.InsertTime.Value);
            if (csa == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._PCExportReportANSI = csa;
        }

        protected override bool HasRows()
        {
            return this._PCExportReportANSIManager.mhas_rows("JIS");
        }

        protected override bool HasRowsPrev()
        {
            return this._PCExportReportANSIManager.mhas_rows_before("JIS", this._PCExportReportANSI.InsertTime.Value);
        }

        protected override bool HasRowsNext()
        {
            return this._PCExportReportANSIManager.mhas_rows_after("JIS", this._PCExportReportANSI.InsertTime.Value);
        }

        protected override void AddNew()
        {
            this._PCExportReportANSI = new Book.Model.PCExportReportANSI();
            this._PCExportReportANSI.ExportReportId = this._PCExportReportANSIManager.GetId();
            this._PCExportReportANSI.ReportDate = DateTime.Now.Date;
            this._PCExportReportANSI.ExpType = "JIS";

        }

        protected override void Delete()
        {
            if (this._PCExportReportANSI == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._PCExportReportANSIManager.Delete(this._PCExportReportANSI.ExportReportId);
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_next("JIS", this._PCExportReportANSI.InsertTime.Value);
            if (this._PCExportReportANSI == null)
            {
                this._PCExportReportANSI = this._PCExportReportANSIManager.mget_last("JIS");
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


            this._PCExportReportANSI.ShouCeShu1 = this.spinlensTestApp.EditValue == null ? 0 : double.Parse(this.spinlensTestApp.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu2 = this.spinlensTestPri.EditValue == null ? 0 : double.Parse(this.spinlensTestPri.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu3 = this.spinlensTestRef.EditValue == null ? 0 : double.Parse(this.spinlensTestRef.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu4 = this.spinlensTestAti.EditValue == null ? 0 : double.Parse(this.spinlensTestAti.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu5 = this.spinlensTestTran.EditValue == null ? 0 : double.Parse(this.spinlensTestTran.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu6 = this.spinlensTestShock.EditValue == null ? 0 : double.Parse(this.spinlensTestShock.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu7 = this.spinlensTestSfr.EditValue == null ? 0 : double.Parse(this.spinlensTestSfr.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu8 = this.spinlensTestSAET.EditValue == null ? 0 : double.Parse(this.spinlensTestSAET.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu9 = this.spinlensTestRTC.EditValue == null ? 0 : double.Parse(this.spinlensTestRTC.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu10 = this.spinlensTestIgn.EditValue == null ? 0 : double.Parse(this.spinlensTestIgn.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu11 = this.spinFinTestApp.EditValue == null ? 0 : double.Parse(this.spinFinTestApp.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu12 = this.spinFinTestShock.EditValue == null ? 0 : double.Parse(this.spinFinTestShock.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu13 = this.spinFinTestHCOSTE.EditValue == null ? 0 : double.Parse(this.spinFinTestHCOSTE.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu14 = this.spinFinTestHCOS.EditValue == null ? 0 : double.Parse(this.spinFinTestHCOS.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu15 = this.spinFinTestSOHAS.EditValue == null ? 0 : double.Parse(this.spinFinTestSOHAS.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu16 = this.spinFinTestSFD.EditValue == null ? 0 : double.Parse(this.spinFinTestSFD.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu17 = this.spinFinTestMfpro.EditValue == null ? 0 : double.Parse(this.spinFinTestMfpro.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu18 = this.spinFinTestMfpac.EditValue == null ? 0 : double.Parse(this.spinFinTestMfpac.EditValue.ToString());


            this._PCExportReportANSI.PanDing1 = this.spinlensJudgeApp.EditValue == null ? 0 : double.Parse(this.spinlensJudgeApp.EditValue.ToString());
            this._PCExportReportANSI.PanDing2 = this.spinlensJudgePri.EditValue == null ? 0 : double.Parse(this.spinlensJudgePri.EditValue.ToString());
            this._PCExportReportANSI.PanDing3 = this.spinlensJudgeRef.EditValue == null ? 0 : double.Parse(this.spinlensJudgeRef.EditValue.ToString());
            this._PCExportReportANSI.PanDing4 = this.spinlensJudgeAti.EditValue == null ? 0 : double.Parse(this.spinlensJudgeAti.EditValue.ToString());
            this._PCExportReportANSI.PanDing5 = this.spinlensJudgeTran.EditValue == null ? 0 : double.Parse(this.spinlensJudgeTran.EditValue.ToString());
            this._PCExportReportANSI.PanDing6 = this.spinlensJudgeShock.EditValue == null ? 0 : double.Parse(this.spinlensJudgeShock.EditValue.ToString());
            this._PCExportReportANSI.PanDing7 = this.spinlensJudgeSfr.EditValue == null ? 0 : double.Parse(this.spinlensJudgeSfr.EditValue.ToString());
            this._PCExportReportANSI.PanDing8 = this.spinlensJudgeSAET.EditValue == null ? 0 : double.Parse(this.spinlensJudgeSAET.EditValue.ToString());
            this._PCExportReportANSI.PanDing9 = this.spinlensJudgeRTC.EditValue == null ? 0 : double.Parse(this.spinlensJudgeRTC.EditValue.ToString());
            this._PCExportReportANSI.PanDing10 = this.spinlensJudgeIgn.EditValue == null ? 0 : double.Parse(this.spinlensJudgeIgn.EditValue.ToString());
            this._PCExportReportANSI.PanDing11 = this.spinFinJudgeApp.EditValue == null ? 0 : double.Parse(this.spinFinJudgeApp.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu12 = this.spinFinJudgeShock.EditValue == null ? 0 : double.Parse(this.spinFinJudgeShock.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu13 = this.spinFinJudgeHCOSTE.EditValue == null ? 0 : double.Parse(this.spinFinJudgeHCOSTE.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu14 = this.spinFinJudgeHCOS.EditValue == null ? 0 : double.Parse(this.spinFinJudgeHCOS.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu15 = this.spinFinJudgeSOHAS.EditValue == null ? 0 : double.Parse(this.spinFinJudgeSOHAS.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu16 = this.spinFinJudgeSFD.EditValue == null ? 0 : double.Parse(this.spinFinJudgeSFD.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu17 = this.spinFinJudgeCon.EditValue == null ? 0 : double.Parse(this.spinFinJudgeCon.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu18 = this.spinFinJudgeMat.EditValue == null ? 0 : double.Parse(this.spinFinJudgeMat.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu19 = this.spinFinJudgeMfpro.EditValue == null ? 0 : double.Parse(this.spinFinJudgeMfpro.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu20 = this.spinFinJudgeMfpac.EditValue == null ? 0 : double.Parse(this.spinFinJudgeMfpac.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu21 = this.spinFinJudgeIM.EditValue == null ? 0 : double.Parse(this.spinFinJudgeIM.EditValue.ToString());
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
            //受测数量即取样标准可以由客户自己修改
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

            this.spinlensTestApp.EditValue = this._PCExportReportANSI.ShouCeShu1.HasValue ? this._PCExportReportANSI.ShouCeShu1.Value : 0;
            this.spinlensTestPri.EditValue = this._PCExportReportANSI.ShouCeShu2.HasValue ? this._PCExportReportANSI.ShouCeShu2.Value : 0;
            this.spinlensTestRef.EditValue = this._PCExportReportANSI.ShouCeShu3.HasValue ? this._PCExportReportANSI.ShouCeShu3.Value : 0;
            this.spinlensTestAti.EditValue = this._PCExportReportANSI.ShouCeShu4.HasValue ? this._PCExportReportANSI.ShouCeShu4.Value : 0;
            this.spinlensTestTran.EditValue = this._PCExportReportANSI.ShouCeShu5.HasValue ? this._PCExportReportANSI.ShouCeShu5.Value : 0;
            this.spinlensTestShock.EditValue = this._PCExportReportANSI.ShouCeShu6.HasValue ? this._PCExportReportANSI.ShouCeShu6.Value : 0;
            this.spinlensTestSfr.EditValue = this._PCExportReportANSI.ShouCeShu7.HasValue ? this._PCExportReportANSI.ShouCeShu7.Value : 0;
            this.spinlensTestSAET.EditValue = this._PCExportReportANSI.ShouCeShu8.HasValue ? this._PCExportReportANSI.ShouCeShu8.Value : 0;
            this.spinlensTestRTC.EditValue = this._PCExportReportANSI.ShouCeShu9.HasValue ? this._PCExportReportANSI.ShouCeShu9.Value : 0;
            this.spinlensTestIgn.EditValue = this._PCExportReportANSI.ShouCeShu10.HasValue ? this._PCExportReportANSI.ShouCeShu10.Value : 0;
            this.spinFinTestApp.EditValue = this._PCExportReportANSI.ShouCeShu11.HasValue ? this._PCExportReportANSI.ShouCeShu11.Value : 0;
            this.spinFinTestShock.EditValue = this._PCExportReportANSI.ShouCeShu12.HasValue ? this._PCExportReportANSI.ShouCeShu12.Value : 0;
            this.spinFinTestHCOSTE.EditValue = this._PCExportReportANSI.ShouCeShu13.HasValue ? this._PCExportReportANSI.ShouCeShu13.Value : 0;
            this.spinFinTestHCOS.EditValue = this._PCExportReportANSI.ShouCeShu14.HasValue ? this._PCExportReportANSI.ShouCeShu14.Value : 0;
            this.spinFinTestSOHAS.EditValue = this._PCExportReportANSI.ShouCeShu15.HasValue ? this._PCExportReportANSI.ShouCeShu15.Value : 0;
            this.spinFinTestSFD.EditValue = this._PCExportReportANSI.ShouCeShu16.HasValue ? this._PCExportReportANSI.ShouCeShu16.Value : 0;
            this.spinFinTestMfpro.EditValue = this._PCExportReportANSI.ShouCeShu17.HasValue ? this._PCExportReportANSI.ShouCeShu17.Value : 0;
            this.spinFinTestMfpac.EditValue = this._PCExportReportANSI.ShouCeShu18.HasValue ? this._PCExportReportANSI.ShouCeShu18.Value : 0;

            this.spinlensJudgeApp.EditValue = this._PCExportReportANSI.PanDing1.HasValue ? this._PCExportReportANSI.PanDing1.Value : 0;
            this.spinlensJudgePri.EditValue = this._PCExportReportANSI.PanDing2.HasValue ? this._PCExportReportANSI.PanDing2.Value : 0;
            this.spinlensJudgeRef.EditValue = this._PCExportReportANSI.PanDing3.HasValue ? this._PCExportReportANSI.PanDing3.Value : 0;
            this.spinlensJudgeAti.EditValue = this._PCExportReportANSI.PanDing4.HasValue ? this._PCExportReportANSI.PanDing4.Value : 0;
            this.spinlensJudgeTran.EditValue = this._PCExportReportANSI.PanDing5.HasValue ? this._PCExportReportANSI.PanDing5.Value : 0;
            this.spinlensJudgeShock.EditValue = this._PCExportReportANSI.PanDing6.HasValue ? this._PCExportReportANSI.PanDing6.Value : 0;
            this.spinlensJudgeSfr.EditValue = this._PCExportReportANSI.PanDing7.HasValue ? this._PCExportReportANSI.PanDing7.Value : 0;
            this.spinlensJudgeSAET.EditValue = this._PCExportReportANSI.PanDing8.HasValue ? this._PCExportReportANSI.PanDing8.Value : 0;
            this.spinlensJudgeRTC.EditValue = this._PCExportReportANSI.PanDing9.HasValue ? this._PCExportReportANSI.PanDing9.Value : 0;
            this.spinlensJudgeIgn.EditValue = this._PCExportReportANSI.PanDing10.HasValue ? this._PCExportReportANSI.PanDing10.Value : 0;
            this.spinFinJudgeApp.EditValue = this._PCExportReportANSI.PanDing11.HasValue ? this._PCExportReportANSI.PanDing11.Value : 0;
            this.spinFinJudgeShock.EditValue = this._PCExportReportANSI.PanDingShu12.HasValue ? this._PCExportReportANSI.PanDingShu12.Value : 0;
            this.spinFinJudgeHCOSTE.EditValue = this._PCExportReportANSI.PanDingShu13.HasValue ? this._PCExportReportANSI.PanDingShu13.Value : 0;
            this.spinFinJudgeHCOS.EditValue = this._PCExportReportANSI.PanDingShu14.HasValue ? this._PCExportReportANSI.PanDingShu14.Value : 0;
            this.spinFinJudgeSOHAS.EditValue = this._PCExportReportANSI.PanDingShu15.HasValue ? this._PCExportReportANSI.PanDingShu15.Value : 0;
            this.spinFinJudgeSFD.EditValue = this._PCExportReportANSI.PanDingShu16.HasValue ? this._PCExportReportANSI.PanDingShu16.Value : 0;
            this.spinFinJudgeCon.EditValue = this._PCExportReportANSI.PanDingShu17.HasValue ? this._PCExportReportANSI.PanDingShu17.Value : 0;
            this.spinFinJudgeMat.EditValue = this._PCExportReportANSI.PanDingShu18.HasValue ? this._PCExportReportANSI.PanDingShu18.Value : 0;
            this.spinFinJudgeMfpro.EditValue = this._PCExportReportANSI.PanDingShu19.HasValue ? this._PCExportReportANSI.PanDingShu19.Value : 0;
            this.spinFinJudgeMfpac.EditValue = this._PCExportReportANSI.PanDingShu20.HasValue ? this._PCExportReportANSI.PanDingShu20.Value : 0;
            this.spinFinJudgeIM.EditValue = this._PCExportReportANSI.PanDingShu21.HasValue ? this._PCExportReportANSI.PanDingShu21.Value : 0;

            this.newChooseContorlAuditEmp.EditValue = this._PCExportReportANSI.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCExportReportANSI.AuditState);
            this.lookUpEditUnit.EditValue = this._PCExportReportANSI.ProductUnitId;
        }

        //列印
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            tag = 0;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            JISRO r = new JISRO(this._PCExportReportANSI, tag);
            JISRO2 r2 = new JISRO2(this._PCExportReportANSI, tag);
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
            r.ShowPreview();
            r2.ShowPreview();
            return null;
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
            Model.PCExportReportANSIDetail _PCExportReportANSIDetail = new BL.PCExportReportANSIDetailManager().SelectForExpJISDetailsSUM(xd.Invoice.CustomerInvoiceXOId, xd.Product.ProductId);

            if (_PCExportReportANSIDetail != null)
            {
                #region 测试数量、合格数量

                //受测数量默认为订单数量的1/500,无条件进位，最大为12
                int Orderamount = int.Parse(this._PCExportReportANSI.Amount.HasValue ? this._PCExportReportANSI.Amount.ToString() : "0");
                double MustCheck = 0;

                if (Orderamount < 500)
                    MustCheck = 1;
                else
                    MustCheck = Orderamount % 500 == 0 ? Orderamount / 500 : Orderamount / 500 + 1;

                this._PCExportReportANSI.AmountTest = MustCheck > 12 ? 12 : MustCheck;//受测数量12个，无条件进位

                this._PCExportReportANSI.ShouCeShu1 = this._PCExportReportANSI.ShouCeShu2 = this._PCExportReportANSI.ShouCeShu3 = this._PCExportReportANSI.ShouCeShu4 = this._PCExportReportANSI.ShouCeShu5 = this._PCExportReportANSI.ShouCeShu6 = this._PCExportReportANSI.ShouCeShu7 = this._PCExportReportANSI.ShouCeShu8 = this._PCExportReportANSI.ShouCeShu9 = this._PCExportReportANSI.ShouCeShu10 = this._PCExportReportANSI.ShouCeShu11 = this._PCExportReportANSI.ShouCeShu12 = this._PCExportReportANSI.ShouCeShu13 = this._PCExportReportANSI.ShouCeShu14 = this._PCExportReportANSI.ShouCeShu15 = this._PCExportReportANSI.ShouCeShu16 = this._PCExportReportANSI.ShouCeShu17 = this._PCExportReportANSI.ShouCeShu18 = this._PCExportReportANSI.ShouCeShu19 = this._PCExportReportANSI.ShouCeShu20 = this._PCExportReportANSI.ShouCeShu21 = this._PCExportReportANSI.AmountTest;

                this._PCExportReportANSI.PanDing1 = _PCExportReportANSIDetail.pJISJPWG;
                this._PCExportReportANSI.QuYangShu1 = _PCExportReportANSIDetail.qJISJPWG;
                this._PCExportReportANSI.PanDing2 = _PCExportReportANSIDetail.pJISJPLJD;
                this._PCExportReportANSI.QuYangShu2 = _PCExportReportANSIDetail.qJISJPLJD;
                this._PCExportReportANSI.PanDing3 = _PCExportReportANSIDetail.pJISJPQGD;
                this._PCExportReportANSI.QuYangShu3 = _PCExportReportANSIDetail.qJISJPQGD;
                this._PCExportReportANSI.PanDing4 = _PCExportReportANSIDetail.pJISJPSGD;
                this._PCExportReportANSI.QuYangShu4 = _PCExportReportANSIDetail.qJISJPSGD;
                this._PCExportReportANSI.PanDing5 = _PCExportReportANSIDetail.pJISJPTGL;
                this._PCExportReportANSI.QuYangShu5 = _PCExportReportANSIDetail.qJISJPTGL;
                this._PCExportReportANSI.PanDing6 = _PCExportReportANSIDetail.pJISJPNCCX;
                this._PCExportReportANSI.QuYangShu6 = _PCExportReportANSIDetail.qJISJPNCCX;
                this._PCExportReportANSI.PanDing7 = _PCExportReportANSIDetail.pJISJPBMNMHDK;
                this._PCExportReportANSI.QuYangShu7 = _PCExportReportANSIDetail.qJISJPBMNMHDK;
                this._PCExportReportANSI.PanDing8 = _PCExportReportANSIDetail.pJISJPNREX;
                this._PCExportReportANSI.QuYangShu8 = _PCExportReportANSIDetail.qJISJPNREX;
                this._PCExportReportANSI.PanDing9 = _PCExportReportANSIDetail.pJISJPNSX; ;
                this._PCExportReportANSI.QuYangShu9 = _PCExportReportANSIDetail.qJISJPNSX;
                this._PCExportReportANSI.PanDing10 = _PCExportReportANSIDetail.pJISJPNRAX;
                this._PCExportReportANSI.QuYangShu10 = _PCExportReportANSIDetail.qJISJPNRAX;
                this._PCExportReportANSI.PanDing11 = _PCExportReportANSIDetail.pJISWCPWG;
                this._PCExportReportANSI.QuYangShu11 = _PCExportReportANSIDetail.qJISWCPWG;
                this._PCExportReportANSI.PanDingShu12 = _PCExportReportANSIDetail.pJISWCPNCCX;
                this._PCExportReportANSI.QuYangShu12 = _PCExportReportANSIDetail.qJISWCPNCCX;
                this._PCExportReportANSI.PanDingShu13 = _PCExportReportANSIDetail.pJISWCPJMXDYSY;
                this._PCExportReportANSI.QuYangShu13 = _PCExportReportANSIDetail.qJISWCPJMXDYSY;
                this._PCExportReportANSI.PanDingShu14 = _PCExportReportANSIDetail.pJISWCPJMXDESY;
                this._PCExportReportANSI.QuYangShu14 = _PCExportReportANSIDetail.qJISWCPJMXDESY;
                this._PCExportReportANSI.PanDingShu15 = _PCExportReportANSIDetail.pJISWCPTDQD;
                this._PCExportReportANSI.QuYangShu15 = _PCExportReportANSIDetail.qJISWCPTDQD;
                this._PCExportReportANSI.PanDingShu16 = _PCExportReportANSIDetail.pJISWCPNXDX;
                this._PCExportReportANSI.QuYangShu16 = _PCExportReportANSIDetail.qJISWCPNXDX;
                this._PCExportReportANSI.PanDingShu17 = _PCExportReportANSIDetail.pJISWCPGZ;
                this._PCExportReportANSI.QuYangShu17 = _PCExportReportANSIDetail.qJISWCPGZ;
                this._PCExportReportANSI.PanDingShu18 = _PCExportReportANSIDetail.pJISWCPCL;
                this._PCExportReportANSI.QuYangShu18 = _PCExportReportANSIDetail.qJISWCPCL;
                this._PCExportReportANSI.PanDingShu19 = _PCExportReportANSIDetail.pJISWCPJHBS;
                this._PCExportReportANSI.QuYangShu19 = _PCExportReportANSIDetail.qJISWCPJHBS;
                this._PCExportReportANSI.PanDingShu20 = _PCExportReportANSIDetail.pJISWCPBZSJH;
                this._PCExportReportANSI.QuYangShu20 = _PCExportReportANSIDetail.qJISWCPBZSJH;
                this._PCExportReportANSI.PanDingShu21 = _PCExportReportANSIDetail.pJISWCPSYSC; ;
                this._PCExportReportANSI.QuYangShu21 = _PCExportReportANSIDetail.qJISWCPSYSC;

                #endregion
            }
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

        private void barPrintPPE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tag = 2;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            JISRO r = new JISRO(this._PCExportReportANSI, tag);
            JISRO2 r2 = new JISRO2(this._PCExportReportANSI, tag);
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
            r.ShowPreview();
            r2.ShowPreview();
        }

        private void barPrintAlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tag = 1;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            JISRO r = new JISRO(this._PCExportReportANSI, tag);
            JISRO2 r2 = new JISRO2(this._PCExportReportANSI, tag);
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
            r.ShowPreview();
            r2.ShowPreview();
        }

        private void barPrintJingPianPinZhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tag = 3;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            JISRO r = new JISRO(this._PCExportReportANSI, tag);
            JISRO2 r2 = new JISRO2(this._PCExportReportANSI, tag);
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
            r.ShowPreview();
            r2.ShowPreview();
        }
    }
}