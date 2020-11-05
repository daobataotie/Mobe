using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using System.IO;
using System.Linq;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class CSAEditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        public Model.PCExportReportANSI _PCExportReportANSI = null;
        BL.PCExportReportANSIManager _PCExportReportANSIManager = new Book.BL.PCExportReportANSIManager();
        BL.PCExportReportANSIManager _pcExpANSIManager = new Book.BL.PCExportReportANSIManager();
        string _ServerSavePath = string.Empty;      //附件存放地址
        int tag;                                    //列印标志

        public CSAEditForm()
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

            IList<Model.Setting> settingList = new BL.SettingManager().SelectByName("CSAChongJiCeShi");
            foreach (var item in settingList)
            {
                if (!string.IsNullOrEmpty(item.SettingCurrentValue))
                    this.comboBox1.Items.Add(item.SettingCurrentValue);
            }

            var jiShuBiaoZhun = new BL.SettingManager().SelectByName("CSAJiShuBiaoZhun");
            foreach (var item in jiShuBiaoZhun)
            {
                comboBoxEdit1.Properties.Items.Add(item.SettingCurrentValue);
            }
            comboBoxEdit1.SelectedIndex = 1;
        }

        int sign = 0;
        public CSAEditForm(Model.PCExportReportANSI mPCExpANSI)
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
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_last("CSA");
        }

        protected override void MoveFirst()
        {
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_first("CSA");
        }

        protected override void MovePrev()
        {
            Model.PCExportReportANSI csa = this._PCExportReportANSIManager.mget_prev("CSA", this._PCExportReportANSI.InsertTime.Value);
            if (csa == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._PCExportReportANSI = csa;
        }

        protected override void MoveNext()
        {
            Model.PCExportReportANSI csa = this._PCExportReportANSIManager.mget_next("CSA", this._PCExportReportANSI.InsertTime.Value);
            if (csa == null)
            {
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            }
            this._PCExportReportANSI = csa;
        }

        protected override bool HasRows()
        {
            return this._PCExportReportANSIManager.mhas_rows("CSA");
        }

        protected override bool HasRowsPrev()
        {
            return this._PCExportReportANSIManager.mhas_rows_before("CSA", this._PCExportReportANSI.InsertTime.Value);
        }

        protected override bool HasRowsNext()
        {
            return this._PCExportReportANSIManager.mhas_rows_after("CSA", this._PCExportReportANSI.InsertTime.Value);
        }

        protected override void AddNew()
        {
            this._PCExportReportANSI = new Book.Model.PCExportReportANSI();
            this._PCExportReportANSI.ExportReportId = this._PCExportReportANSIManager.GetId();
            this._PCExportReportANSI.ReportDate = DateTime.Now.Date;
            this._PCExportReportANSI.Employee = BL.V.ActiveOperator.Employee;
            this._PCExportReportANSI.ExpType = "CSA";

            var jiShuBiaoZhun = new BL.SettingManager().SelectByName("CSAJiShuBiaoZhun").FirstOrDefault(D => D.SettingCurrentValue.Contains("2015"));
            this._PCExportReportANSI.CSAJiShuBiaoZhun = jiShuBiaoZhun == null ? "CSA Z94.3-2015" : jiShuBiaoZhun.SettingCurrentValue;

            this._PCExportReportANSI.CeShiSuLi = this.comboBox1.Items.Count > 0 ? this.comboBox1.Items[0].ToString() : null;
        }

        protected override void Delete()
        {
            if (this._PCExportReportANSI == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._PCExportReportANSIManager.Delete(this._PCExportReportANSI.ExportReportId);
            this._PCExportReportANSI = this._PCExportReportANSIManager.mget_next("CSA", this._PCExportReportANSI.InsertTime.Value);
            if (this._PCExportReportANSI == null)
            {
                this._PCExportReportANSI = this._PCExportReportANSIManager.mget_last("CSA");
            }
        }

        protected override void Save()
        {
            this._PCExportReportANSI.ExportReportId = this.TxtOrderId.Text == null ? null : this.TxtOrderId.Text;
            this._PCExportReportANSI.Amount = this.SpinOrderAmount.EditValue == null ? 0 : double.Parse(this.SpinOrderAmount.EditValue.ToString());
            this._PCExportReportANSI.AmountTest = this.SpinTestAmount.EditValue == null ? 0 : double.Parse(this.SpinTestAmount.EditValue.ToString());
            this._PCExportReportANSI.InvoiceCusXOId = this.TxtCustomersId.Text == null ? null : this.TxtCustomersId.Text.ToString();
            this._PCExportReportANSI.Customer = (this.NccCustomer.EditValue as Model.Customer);
            this._PCExportReportANSI.Mirrorlens = this.memoMirrorlens.EditValue == null ? null : this.memoMirrorlens.EditValue.ToString();
            this._PCExportReportANSI.ReportDate = this.DateReportDate.EditValue == null ? DateTime.Now : this.DateReportDate.DateTime;
            //this._PCExportReportANSI.CSAJiShuBiaoZhun = this.label25.Text;
            this._PCExportReportANSI.CSAJiShuBiaoZhun = this.comboBoxEdit1.SelectedItem == null ? null : this.comboBoxEdit1.SelectedItem.ToString();

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

            this._PCExportReportANSI.ShouCeShu1 = this.SpinOpticsTestAmount.EditValue == null ? 0 : double.Parse(this.SpinOpticsTestAmount.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu2 = this.SpinClearTestAmount.EditValue == null ? 0 : double.Parse(this.SpinClearTestAmount.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu3 = this.SpinPolarizedTestAmount.EditValue == null ? 0 : double.Parse(this.SpinPolarizedTestAmount.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu4 = this.SpinFogTestAmount.EditValue == null ? 0 : double.Parse(this.SpinFogTestAmount.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu5 = this.SpinLightTestAmount.EditValue == null ? 0 : double.Parse(this.SpinLightTestAmount.EditValue.ToString());
            this._PCExportReportANSI.ShouCeShu6 = this.SpinImpactTestAmount.EditValue == null ? 0 : double.Parse(this.SpinImpactTestAmount.EditValue.ToString());

            this._PCExportReportANSI.PanDing0 = this.SpinOpticsJudge.EditValue == null ? 0 : double.Parse(this.SpinOpticsJudge.EditValue.ToString());
            this._PCExportReportANSI.PanDing1 = this.SpinClearJudge.EditValue == null ? 0 : double.Parse(this.SpinClearJudge.EditValue.ToString());
            this._PCExportReportANSI.PanDing2 = this.SpinPolarizedJudge.EditValue == null ? 0 : double.Parse(this.SpinPolarizedJudge.EditValue.ToString());
            this._PCExportReportANSI.PanDing3 = this.SpinFogJudge.EditValue == null ? 0 : double.Parse(this.SpinFogJudge.EditValue.ToString());
            this._PCExportReportANSI.PanDing4 = this.SpinLightJudge.EditValue == null ? 0 : double.Parse(this.SpinLightJudge.EditValue.ToString());
            this._PCExportReportANSI.PanDing5 = this.SpinImpactJudge.EditValue == null ? 0 : double.Parse(this.SpinImpactJudge.EditValue.ToString());
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
            this.memoMirrorlens.EditValue = this._PCExportReportANSI.Mirrorlens == null ? null : this._PCExportReportANSI.Mirrorlens;


            this.SpinOpticsTestAmount.EditValue = this._PCExportReportANSI.ShouCeShu1.HasValue ? this._PCExportReportANSI.ShouCeShu1.Value : 0;
            this.SpinClearTestAmount.EditValue = this._PCExportReportANSI.ShouCeShu2.HasValue ? this._PCExportReportANSI.ShouCeShu2.Value : 0;
            this.SpinPolarizedTestAmount.EditValue = this._PCExportReportANSI.ShouCeShu3.HasValue ? this._PCExportReportANSI.ShouCeShu3.Value : 0;
            this.SpinFogTestAmount.EditValue = this._PCExportReportANSI.ShouCeShu4.HasValue ? this._PCExportReportANSI.ShouCeShu4.Value : 0;
            this.SpinLightTestAmount.EditValue = this._PCExportReportANSI.ShouCeShu5.HasValue ? this._PCExportReportANSI.ShouCeShu5.Value : 0;
            this.SpinImpactTestAmount.EditValue = this._PCExportReportANSI.ShouCeShu6.HasValue ? this._PCExportReportANSI.ShouCeShu6.Value : 0;

            //this.SpinOpticsBringAmout.EditValue = this._PCExportReportANSI.QuYangShu1.HasValue ? this._PCExportReportANSI.QuYangShu1.Value : 0;
            //this.SpinClearBringAmount.EditValue = this._PCExportReportANSI.QuYangShu2.HasValue ? this._PCExportReportANSI.QuYangShu2.Value : 0;
            //this.SpinPolarizedBringAmount.EditValue = this._PCExportReportANSI.QuYangShu3.HasValue ? this._PCExportReportANSI.QuYangShu3.Value : 0;
            //this.SpinFogBringAmount.EditValue = this._PCExportReportANSI.QuYangShu4.HasValue ? this._PCExportReportANSI.QuYangShu4.Value : 0;
            //this.SpinLightBringAmount.EditValue = this._PCExportReportANSI.QuYangShu5.HasValue ? this._PCExportReportANSI.QuYangShu5.Value : 0;
            //this.SpinImpactBringAmount.EditValue = this._PCExportReportANSI.QuYangShu6.HasValue ? this._PCExportReportANSI.QuYangShu6.Value : 0;

            this.SpinOpticsJudge.EditValue = this._PCExportReportANSI.PanDing0.HasValue ? this._PCExportReportANSI.PanDing0.Value : 0;
            this.SpinClearJudge.EditValue = this._PCExportReportANSI.PanDing1.HasValue ? this._PCExportReportANSI.PanDing1.Value : 0;
            this.SpinPolarizedJudge.EditValue = this._PCExportReportANSI.PanDing2.HasValue ? this._PCExportReportANSI.PanDing2.Value : 0;
            this.SpinFogJudge.EditValue = this._PCExportReportANSI.PanDing3.HasValue ? this._PCExportReportANSI.PanDing3.Value : 0;
            this.SpinLightJudge.EditValue = this._PCExportReportANSI.PanDing4.HasValue ? this._PCExportReportANSI.PanDing4.Value : 0;
            this.SpinImpactJudge.EditValue = this._PCExportReportANSI.PanDing5.HasValue ? this._PCExportReportANSI.PanDing5.Value : 0;

            this.newChooseContorlAuditEmp.EditValue = this._PCExportReportANSI.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCExportReportANSI.AuditState);

            this.lookUpEditUnit.EditValue = this._PCExportReportANSI.ProductUnitId;
            //this.label25.Text = string.IsNullOrEmpty(this._PCExportReportANSI.CSAJiShuBiaoZhun) ? "CSA Z94.3-07" : this._PCExportReportANSI.CSAJiShuBiaoZhun;
            this.comboBoxEdit1.Text = string.IsNullOrEmpty(this._PCExportReportANSI.CSAJiShuBiaoZhun) ? "CSA Z94.3-2015" : this._PCExportReportANSI.CSAJiShuBiaoZhun;

            this.comboBox1.SelectedItem = this._PCExportReportANSI.CeShiSuLi;
        }

        //列印
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            tag = 0;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            CSARO r = new CSARO(this._PCExportReportANSI, tag);
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
            //Model.PCExportReportANSIDetail _PCExportReportANSIDetail = new BL.PCExportReportANSIDetailManager().SelectForExpCSADetailsSUM(xd.Invoice.CustomerInvoiceXOId, xd.Product.ProductId);

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

            this._PCExportReportANSI.ShouCeShu1 = this._PCExportReportANSI.ShouCeShu2 = this._PCExportReportANSI.ShouCeShu3 = this._PCExportReportANSI.ShouCeShu4 = this._PCExportReportANSI.ShouCeShu5 = this._PCExportReportANSI.ShouCeShu6 = this._PCExportReportANSI.AmountTest;

            //this._PCExportReportANSI.PanDing0 = _PCExportReportANSIDetail.pCSAGX;
            //this._PCExportReportANSI.QuYangShu1 = _PCExportReportANSIDetail.qCSAGX;
            //this._PCExportReportANSI.PanDing1 = _PCExportReportANSIDetail.pCSAQXD;
            //this._PCExportReportANSI.QuYangShu2 = _PCExportReportANSIDetail.qCSAQXD;
            //this._PCExportReportANSI.PanDing2 = _PCExportReportANSIDetail.pCSAPGPCL;
            //this._PCExportReportANSI.QuYangShu3 = _PCExportReportANSIDetail.qCSAPGPCL;
            //this._PCExportReportANSI.PanDing3 = _PCExportReportANSIDetail.pCSAWDCS;
            //this._PCExportReportANSI.QuYangShu4 = _PCExportReportANSIDetail.qCSAWDCS;
            //this._PCExportReportANSI.PanDing4 = _PCExportReportANSIDetail.pCSAKJGTSL;
            //this._PCExportReportANSI.QuYangShu5 = _PCExportReportANSIDetail.qCSAKJGTSL;
            //this._PCExportReportANSI.PanDing5 = _PCExportReportANSIDetail.pCSAGSCJCS;
            //this._PCExportReportANSI.QuYangShu6 = _PCExportReportANSIDetail.qCSAGSCJCS;

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
            CSARO r = new CSARO(this._PCExportReportANSI, tag);
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
            CSARO r = new CSARO(this._PCExportReportANSI, tag);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this._PCExportReportANSI.CeShiSuLi = this.comboBox1.SelectedItem.ToString();
        }
    }
}