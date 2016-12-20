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
    public partial class CEENEditsForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        public Model.PCExportReportANSI _PCExportReportANSI = null;
        BL.PCExportReportANSIManager _PCExportReportANSIManager = new Book.BL.PCExportReportANSIManager();
        BL.PCExportReportANSIManager _pcExpANSIManager = new Book.BL.PCExportReportANSIManager();
        string _ServerSavePath = string.Empty;      //附件存放地址
        int tag;                                    //列印标志

        public CEENEditsForm()
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
            foreach (var item in new BL.SettingManager().SelectByName("CEEN7.2.2"))
            {
                if (!string.IsNullOrEmpty(item.SettingCurrentValue))
                    this.comboBox1.Items.Add(item.SettingCurrentValue);
            }
        }

        int sign = 0;
        public CEENEditsForm(Model.PCExportReportANSI mPCExpANSI)
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
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_last("CEEN");
        }

        protected override void MoveFirst()
        {
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_first("CEEN");
        }

        protected override void MovePrev()
        {
            Model.PCExportReportANSI csa = this._PCExportReportANSIManager.mget_prev("CEEN", this._PCExportReportANSI.InsertTime.Value);
            if (csa == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._PCExportReportANSI = csa;
        }

        protected override void MoveNext()
        {
            Model.PCExportReportANSI csa = this._PCExportReportANSIManager.mget_next("CEEN", this._PCExportReportANSI.InsertTime.Value);
            if (csa == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._PCExportReportANSI = csa;
        }

        protected override bool HasRows()
        {
            return this._PCExportReportANSIManager.mhas_rows("CEEN");
        }

        protected override bool HasRowsPrev()
        {
            return this._PCExportReportANSIManager.mhas_rows_before("CEEN", this._PCExportReportANSI.InsertTime.Value);
        }

        protected override bool HasRowsNext()
        {
            return this._PCExportReportANSIManager.mhas_rows_after("CEEN", this._PCExportReportANSI.InsertTime.Value);
        }

        protected override void AddNew()
        {
            this._PCExportReportANSI = new Book.Model.PCExportReportANSI();
            this._PCExportReportANSI.ExportReportId = this._PCExportReportANSIManager.GetId();
            this._PCExportReportANSI.ReportDate = DateTime.Now.Date;
            this._PCExportReportANSI.ExpType = "CEEN";

        }

        protected override void Delete()
        {
            if (this._PCExportReportANSI == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._PCExportReportANSIManager.Delete(this._PCExportReportANSI.ExportReportId);
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_next("CEEN", this._PCExportReportANSI.InsertTime.Value);
            if (this._PCExportReportANSI == null)
            {
                this._PCExportReportANSI = this._PCExportReportANSIManager.mget_last("CEEN");
            }
        }

        protected override void Save()
        {
            this._PCExportReportANSI.ExportReportId = this.TxtOrderId.Text == null ? null : this.TxtOrderId.Text;
            this._PCExportReportANSI.Amount = this.SpinOrderAmount.EditValue == null ? 0 : double.Parse(this.SpinOrderAmount.EditValue.ToString());
            this._PCExportReportANSI.AmountTest = this.SpinTestAmount.EditValue == null ? 0 : double.Parse(this.SpinTestAmount.EditValue.ToString());
            this._PCExportReportANSI.InvoiceCusXOId = this.TxtCustomersId.Text == null ? null : this.TxtCustomersId.Text.ToString();
            this._PCExportReportANSI.Customer = (this.NccCustomer.EditValue as Model.Customer);
            this._PCExportReportANSI.Clearlens = this.memoTrans.EditValue == null ? "" : this.memoTrans.EditValue.ToString();
            this._PCExportReportANSI.TraceMarking = this.textTraceMarking.Text == null ? null : this.textTraceMarking.Text.ToString();
            //this._PCExportReportANSI.Protectionone = this.textProtectionone.Text == null ? null : this.textProtectionone.Text.ToString();
            //this._PCExportReportANSI.Protectiontwo = this.textProtectiontwo.Text == null ? null : this.textProtectiontwo.Text.ToString();
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


            this._PCExportReportANSI.ShouCeShu1 = this.SpinTestConstruction.EditValue == null ? 0 : double.Parse(this.SpinTestConstruction.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu2 = this.SpinTestSp.EditValue == null ? 0 : double.Parse(this.SpinTestSp.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu3 = this.SpinTestAst.EditValue == null ? 0 : double.Parse(this.SpinTestAst.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu4 = this.SpinTestPrism.EditValue == null ? 0 : double.Parse(this.SpinTestPrism.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu5 = this.SpinTestDiffrence.EditValue == null ? 0 : double.Parse(this.SpinTestDiffrence.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu6 = this.SpinTestTrans.EditValue == null ? 0 : double.Parse(this.SpinTestTrans.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu7 = this.SpinTestSurface.EditValue == null ? 0 : double.Parse(this.SpinTestSurface.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu8 = this.SpinTestRobustness.EditValue == null ? 0 : double.Parse(this.SpinTestRobustness.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu9 = this.SpinTestHighSpeed.EditValue == null ? 0 : double.Parse(this.SpinTestHighSpeed.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu10 = this.SpinTestMarking.EditValue == null ? 0 : double.Parse(this.SpinTestMarking.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu11 = this.SpinTestInformation.EditValue == null ? 0 : double.Parse(this.SpinTestInformation.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu12 = this.SpinTestInternal.EditValue == null ? 0 : double.Parse(this.SpinTestInternal.EditValue.ToString());

            this._PCExportReportANSI.PanDing1 = this.SpinJudgeConstruction.EditValue == null ? 0 : double.Parse(this.SpinJudgeConstruction.EditValue.ToString());
            this._PCExportReportANSI.PanDing2 = this.SpinJudgeSp.EditValue == null ? 0 : double.Parse(this.SpinJudgeSp.EditValue.ToString());
            this._PCExportReportANSI.PanDing3 = this.SpinJudgeAst.EditValue == null ? 0 : double.Parse(this.SpinJudgeAst.EditValue.ToString());
            this._PCExportReportANSI.PanDing4 = this.SpinJudgePrism.EditValue == null ? 0 : double.Parse(this.SpinJudgePrism.EditValue.ToString());
            this._PCExportReportANSI.PanDing5 = this.SpinJudgeDiffrence.EditValue == null ? 0 : double.Parse(this.SpinJudgeDiffrence.EditValue.ToString());
            this._PCExportReportANSI.PanDing6 = this.SpinJudgeTrans.EditValue == null ? 0 : double.Parse(this.SpinJudgeTrans.EditValue.ToString());
            this._PCExportReportANSI.PanDing7 = this.SpinJudgeSurface.EditValue == null ? 0 : double.Parse(this.SpinJudgeSurface.EditValue.ToString());
            this._PCExportReportANSI.PanDing8 = this.SpinJudgeRobustness.EditValue == null ? 0 : double.Parse(this.SpinJudgeRobustness.EditValue.ToString());
            this._PCExportReportANSI.PanDing9 = this.SpinJudgeHighSpeed.EditValue == null ? 0 : double.Parse(this.SpinJudgeHighSpeed.EditValue.ToString());
            this._PCExportReportANSI.PanDing10 = this.SpinJudgeMarking.EditValue == null ? 0 : double.Parse(this.SpinJudgeMarking.EditValue.ToString());
            this._PCExportReportANSI.PanDing11 = this.SpinJudgeInformation.EditValue == null ? 0 : double.Parse(this.SpinJudgeInformation.EditValue.ToString());
            this._PCExportReportANSI.PanDingShu12 = this.SpinJudgeInternal.EditValue == null ? 0 : double.Parse(this.SpinJudgeInternal.EditValue.ToString());

            this._PCExportReportANSI.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();

            if (this.comboBox1.SelectedItem != null)
                this._PCExportReportANSI.CeShiSuLi = this.comboBox1.SelectedItem.ToString();
            else
                this._PCExportReportANSI.CeShiSuLi = null;
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
            this.comboBox1.SelectedItem = this._PCExportReportANSI.CeShiSuLi;
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
            this.memoTrans.EditValue = this._PCExportReportANSI.Clearlens;
            this.textTraceMarking.Text = this._PCExportReportANSI.TraceMarking == null ? null : this._PCExportReportANSI.TraceMarking;
            //this.textProtectionone.Text = this._PCExportReportANSI.Protectionone == null ? null : this._PCExportReportANSI.Protectionone;
            //this.textProtectiontwo.Text = this._PCExportReportANSI.Protectiontwo == null ? null : this._PCExportReportANSI.Protectiontwo;

            this.SpinTestConstruction.EditValue = this._PCExportReportANSI.ShouCeShu1.HasValue ? this._PCExportReportANSI.ShouCeShu1.Value : 0;
            this.SpinTestSp.EditValue = this._PCExportReportANSI.ShouCeShu2.HasValue ? this._PCExportReportANSI.ShouCeShu2.Value : 0;
            this.SpinTestAst.EditValue = this._PCExportReportANSI.ShouCeShu3.HasValue ? this._PCExportReportANSI.ShouCeShu3.Value : 0;
            this.SpinTestPrism.EditValue = this._PCExportReportANSI.ShouCeShu4.HasValue ? this._PCExportReportANSI.ShouCeShu4.Value : 0;
            this.SpinTestDiffrence.EditValue = this._PCExportReportANSI.ShouCeShu5.HasValue ? this._PCExportReportANSI.ShouCeShu5.Value : 0;
            this.SpinTestTrans.EditValue = this._PCExportReportANSI.ShouCeShu6.HasValue ? this._PCExportReportANSI.ShouCeShu6.Value : 0;
            this.SpinTestSurface.EditValue = this._PCExportReportANSI.ShouCeShu7.HasValue ? this._PCExportReportANSI.ShouCeShu7.Value : 0;
            this.SpinTestRobustness.EditValue = this._PCExportReportANSI.ShouCeShu8.HasValue ? this._PCExportReportANSI.ShouCeShu8.Value : 0;
            this.SpinTestHighSpeed.EditValue = this._PCExportReportANSI.ShouCeShu9.HasValue ? this._PCExportReportANSI.ShouCeShu9.Value : 0;
            this.SpinTestMarking.EditValue = this._PCExportReportANSI.ShouCeShu10.HasValue ? this._PCExportReportANSI.ShouCeShu10.Value : 0;
            this.SpinTestInformation.EditValue = this._PCExportReportANSI.ShouCeShu11.HasValue ? this._PCExportReportANSI.ShouCeShu11.Value : 0;
            this.SpinTestInternal.EditValue = this._PCExportReportANSI.ShouCeShu12.HasValue ? this._PCExportReportANSI.ShouCeShu12.Value : 0;

            //this.SpinBringConstruction.EditValue = this._PCExportReportANSI.QuYangShu1.HasValue ? this._PCExportReportANSI.QuYangShu1.Value : 0;
            //this.SpinBringSp.EditValue = this._PCExportReportANSI.QuYangShu2.HasValue ? this._PCExportReportANSI.QuYangShu2.Value : 0;
            //this.SpinBringAst.EditValue = this._PCExportReportANSI.QuYangShu3.HasValue ? this._PCExportReportANSI.QuYangShu3.Value : 0;
            //this.SpinBringPrism.EditValue = this._PCExportReportANSI.QuYangShu4.HasValue ? this._PCExportReportANSI.QuYangShu4.Value : 0;
            //this.SpinBringDiffrence.EditValue = this._PCExportReportANSI.QuYangShu5.HasValue ? this._PCExportReportANSI.QuYangShu5.Value : 0;
            //this.SpinBringTrans.EditValue = this._PCExportReportANSI.QuYangShu6.HasValue ? this._PCExportReportANSI.QuYangShu6.Value : 0;
            //this.SpinBringSurface.EditValue = this._PCExportReportANSI.QuYangShu7.HasValue ? this._PCExportReportANSI.QuYangShu7.Value : 0;
            //this.SpinBringRobustness.EditValue = this._PCExportReportANSI.QuYangShu8.HasValue ? this._PCExportReportANSI.QuYangShu8.Value : 0;
            //this.SpinBringHighSpeed.EditValue = this._PCExportReportANSI.QuYangShu9.HasValue ? this._PCExportReportANSI.QuYangShu9.Value : 0;
            //this.SpinBringMarking.EditValue = this._PCExportReportANSI.QuYangShu10.HasValue ? this._PCExportReportANSI.QuYangShu10.Value : 0;
            //this.SpinBringInformation.EditValue = this._PCExportReportANSI.QuYangShu11.HasValue ? this._PCExportReportANSI.QuYangShu11.Value : 0;
            //this.SpinBringInternal.EditValue = this._PCExportReportANSI.QuYangShu12.HasValue ? this._PCExportReportANSI.QuYangShu12.Value : 0;

            this.SpinJudgeConstruction.EditValue = this._PCExportReportANSI.PanDing1.HasValue ? this._PCExportReportANSI.PanDing1.Value : 0;
            this.SpinJudgeSp.EditValue = this._PCExportReportANSI.PanDing2.HasValue ? this._PCExportReportANSI.PanDing2.Value : 0;
            this.SpinJudgeAst.EditValue = this._PCExportReportANSI.PanDing3.HasValue ? this._PCExportReportANSI.PanDing3.Value : 0;
            this.SpinJudgePrism.EditValue = this._PCExportReportANSI.PanDing4.HasValue ? this._PCExportReportANSI.PanDing4.Value : 0;
            this.SpinJudgeDiffrence.EditValue = this._PCExportReportANSI.PanDing5.HasValue ? this._PCExportReportANSI.PanDing5.Value : 0;
            this.SpinJudgeTrans.EditValue = this._PCExportReportANSI.PanDing6.HasValue ? this._PCExportReportANSI.PanDing6.Value : 0;
            this.SpinJudgeSurface.EditValue = this._PCExportReportANSI.PanDing7.HasValue ? this._PCExportReportANSI.PanDing7.Value : 0;
            this.SpinJudgeRobustness.EditValue = this._PCExportReportANSI.PanDing8.HasValue ? this._PCExportReportANSI.PanDing8.Value : 0;
            this.SpinJudgeHighSpeed.EditValue = this._PCExportReportANSI.PanDing9.HasValue ? this._PCExportReportANSI.PanDing9.Value : 0;
            this.SpinJudgeMarking.EditValue = this._PCExportReportANSI.PanDing10.HasValue ? this._PCExportReportANSI.PanDing10.Value : 0;
            this.SpinJudgeInformation.EditValue = this._PCExportReportANSI.PanDing11.HasValue ? this._PCExportReportANSI.PanDing11.Value : 0;
            this.SpinJudgeInternal.EditValue = this._PCExportReportANSI.PanDingShu12.HasValue ? this._PCExportReportANSI.PanDingShu12.Value : 0;

            this.newChooseContorlAuditEmp.EditValue = this._PCExportReportANSI.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCExportReportANSI.AuditState);

            this.lookUpEditUnit.EditValue = this._PCExportReportANSI.ProductUnitId;
        }

        //列印
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            tag = 0;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            CEENRO r = new CEENRO(this._PCExportReportANSI, tag);
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
            Model.PCExportReportANSIDetail _PCExportReportANSIDetail = new BL.PCExportReportANSIDetailManager().SelectForExpCEENDetailsSUM(xd.Invoice.CustomerInvoiceXOId, xd.Product.ProductId);

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

                this._PCExportReportANSI.ShouCeShu1 = this._PCExportReportANSI.ShouCeShu2 = this._PCExportReportANSI.ShouCeShu3 = this._PCExportReportANSI.ShouCeShu4 = this._PCExportReportANSI.ShouCeShu5 = this._PCExportReportANSI.ShouCeShu6 = this._PCExportReportANSI.ShouCeShu7 = this._PCExportReportANSI.ShouCeShu8 = this._PCExportReportANSI.ShouCeShu9 = this._PCExportReportANSI.ShouCeShu10 = this._PCExportReportANSI.ShouCeShu11 = this._PCExportReportANSI.ShouCeShu12 = this._PCExportReportANSI.AmountTest;

                this._PCExportReportANSI.PanDing1 = _PCExportReportANSIDetail.pCEENCONSTRUCTION;
                this._PCExportReportANSI.QuYangShu1 = _PCExportReportANSIDetail.qCEENCONSTRUCTION;
                this._PCExportReportANSI.PanDing2 = _PCExportReportANSIDetail.pCEENQMDS;
                this._PCExportReportANSI.QuYangShu2 = _PCExportReportANSIDetail.qCEENQMDS;
                this._PCExportReportANSI.PanDing3 = _PCExportReportANSIDetail.pCEENSGDS;
                this._PCExportReportANSI.QuYangShu3 = _PCExportReportANSIDetail.qCEENSGDS;
                this._PCExportReportANSI.PanDing4 = _PCExportReportANSIDetail.pCEENLJDS;
                this._PCExportReportANSI.QuYangShu4 = _PCExportReportANSIDetail.qCEENLJDS;
                this._PCExportReportANSI.PanDing5 = _PCExportReportANSIDetail.pCEENZB;
                this._PCExportReportANSI.QuYangShu5 = _PCExportReportANSIDetail.qCEENZB;
                this._PCExportReportANSI.PanDing6 = _PCExportReportANSIDetail.pCEENTSL;
                this._PCExportReportANSI.QuYangShu6 = _PCExportReportANSIDetail.qCEENTSL;
                this._PCExportReportANSI.PanDing7 = _PCExportReportANSIDetail.pCEENBMPZ;
                this._PCExportReportANSI.QuYangShu7 = _PCExportReportANSIDetail.qCEENBMPZ;
                this._PCExportReportANSI.PanDing8 = _PCExportReportANSIDetail.pCEENZSCJ;
                this._PCExportReportANSI.QuYangShu8 = _PCExportReportANSIDetail.qCEENZSCJ;
                this._PCExportReportANSI.PanDing9 = _PCExportReportANSIDetail.pCEENGSCJ; ;
                this._PCExportReportANSI.QuYangShu9 = _PCExportReportANSIDetail.qCEENGSCJ;
                this._PCExportReportANSI.PanDing10 = _PCExportReportANSIDetail.pCEENJH;
                this._PCExportReportANSI.QuYangShu10 = _PCExportReportANSIDetail.qCEENJH;
                this._PCExportReportANSI.PanDing11 = _PCExportReportANSIDetail.pCEENZX;
                this._PCExportReportANSI.QuYangShu11 = _PCExportReportANSIDetail.qCEENZX;
                this._PCExportReportANSI.PanDingShu12 = _PCExportReportANSIDetail.pCEENUVCF;
                this._PCExportReportANSI.QuYangShu12 = _PCExportReportANSIDetail.qCEENUVCF;

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

        private void barPrintAlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tag = 1;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            CEENRO r = new CEENRO(this._PCExportReportANSI, tag);
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
            CEENRO r = new CEENRO(this._PCExportReportANSI, tag);
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

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this._PCExportReportANSI.CSAChongJiCeShi = this.comboBox1.SelectedItem.ToString();
        }
    }

}