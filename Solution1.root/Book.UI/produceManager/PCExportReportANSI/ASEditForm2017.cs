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
    public partial class ASEditForm2017 : Book.UI.Settings.BasicData.BaseEditForm
    {
        public Model.PCExportReportANSI _PCExportReportANSI = null;
        BL.PCExportReportANSIManager _PCExportReportANSIManager = new Book.BL.PCExportReportANSIManager();
        BL.PCExportReportANSIManager _pcExpANSIManager = new Book.BL.PCExportReportANSIManager();
        string _ServerSavePath = string.Empty;      //附件存放地址
        int tag;

        public ASEditForm2017()
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
            this.NccTestPerson2.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.NccTestPerson3.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.NccTestPerson4.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();
            this.action = "view";

            var jiShuBiaoZhun = new BL.SettingManager().SelectByName("ASJiShuBiaoZhun");
            foreach (var item in jiShuBiaoZhun)
            {
                comboBoxEdit1.Properties.Items.Add(item.SettingCurrentValue);
            }
            comboBoxEdit1.SelectedIndex = 0;
        }

        int sign = 0;
        public ASEditForm2017(Model.PCExportReportANSI mPCExpANSI)
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

            this._PCExportReportANSI.Employee = BL.V.ActiveOperator.Employee;
            this._PCExportReportANSI.EmployeeId = BL.V.ActiveOperator.EmployeeId;

            //默認勾選
            this._PCExportReportANSI.VisualTest = true;
            this._PCExportReportANSI.ThermalStability = true;
            this._PCExportReportANSI.IsShowGX2 = true;
            this._PCExportReportANSI.ScatterLight = true;
            this._PCExportReportANSI.LowImpact = true;
            this._PCExportReportANSI.MediumImpact = true;
            this._PCExportReportANSI.PermertrationTest = true;
            this._PCExportReportANSI.IgnitionTest = true;
            this._PCExportReportANSI.Corrsion = true;
            this._PCExportReportANSI.Markings = true;

            //默認數量
            this._PCExportReportANSI.ShouCeShu5 = this._PCExportReportANSI.ShouCeShu7 = this._PCExportReportANSI.ShouCeShu9 = this._PCExportReportANSI.ShouCeShu10 = this._PCExportReportANSI.ShouCeShu1 = this._PCExportReportANSI.ShouCeShu11 = this._PCExportReportANSI.ShouCeShu14 = 2;

            this._PCExportReportANSI.ShouCeShu15 = this._PCExportReportANSI.ShouCeShu16 = 1;

            this._PCExportReportANSI.ShouCeShu12 = this._PCExportReportANSI.ShouCeShu13 = 0;
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
            this._PCExportReportANSI.Employee2 = (this.NccTestPerson2.EditValue as Model.Employee);
            if (this._PCExportReportANSI.Employee2 != null)
            {
                this._PCExportReportANSI.EmployeeId2 = this._PCExportReportANSI.Employee2.EmployeeId;
            }
            this._PCExportReportANSI.Employee3 = (this.NccTestPerson3.EditValue as Model.Employee);
            if (this._PCExportReportANSI.Employee3 != null)
            {
                this._PCExportReportANSI.EmployeeId3 = this._PCExportReportANSI.Employee3.EmployeeId;
            }
            this._PCExportReportANSI.Employee4 = (this.NccTestPerson4.EditValue as Model.Employee);
            if (this._PCExportReportANSI.Employee4 != null)
            {
                this._PCExportReportANSI.EmployeeId4 = this._PCExportReportANSI.Employee4.EmployeeId;
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
            this._PCExportReportANSI.IsShowGX2 = this.che_IsShowGX2.Checked;

            //对外观，加热，坐标等判定新增的 测试数量
            if (this.sp_VisualNum.EditValue == null || this.sp_VisualNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu4 = null;
            else
                this._PCExportReportANSI.ShouCeShu4 = Convert.ToDouble(this.sp_VisualNum.Value);
            if (this.sp_ThemalNum.EditValue == null || this.sp_ThemalNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu5 = null;
            else
                this._PCExportReportANSI.ShouCeShu5 = Convert.ToDouble(this.sp_ThemalNum.Value);
            if (this.sp_PrismaticHInNum.EditValue == null || this.sp_PrismaticHInNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu6 = null;
            else
                this._PCExportReportANSI.ShouCeShu6 = Convert.ToDouble(this.sp_PrismaticHInNum.Value);
            if (this.sp_PrismaticHOutNum.EditValue == null || this.sp_PrismaticHOutNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu7 = null;
            else
                this._PCExportReportANSI.ShouCeShu7 = Convert.ToDouble(this.sp_PrismaticHOutNum.Value);
            if (this.sp_PrismaticVUpNum.EditValue == null || this.sp_PrismaticVUpNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu8 = null;
            else
                this._PCExportReportANSI.ShouCeShu8 = Convert.ToDouble(this.sp_PrismaticVUpNum.Value);
            if (this.sp_PrismaticVDwnNum.EditValue == null || this.sp_PrismaticVDwnNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu9 = null;
            else
                this._PCExportReportANSI.ShouCeShu9 = Convert.ToDouble(this.sp_PrismaticVDwnNum.Value);
            if (this.sp_ScatterNum.EditValue == null || this.sp_ScatterNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu10 = null;
            else
                this._PCExportReportANSI.ShouCeShu10 = Convert.ToDouble(this.sp_ScatterNum.Value);
            if (this.sp_MediumNum.EditValue == null || this.sp_MediumNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu11 = null;
            else
                this._PCExportReportANSI.ShouCeShu11 = Convert.ToDouble(this.sp_MediumNum.Value);
            if (this.sp_HighNum.EditValue == null || this.sp_HighNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu12 = null;
            else
                this._PCExportReportANSI.ShouCeShu12 = Convert.ToDouble(this.sp_HighNum.Value);
            if (this.sp_ExtraHighNum.EditValue == null || this.sp_ExtraHighNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu13 = null;
            else
                this._PCExportReportANSI.ShouCeShu13 = Convert.ToDouble(this.sp_ExtraHighNum.Value);
            if (this.sp_PemertrationNum.EditValue == null || this.sp_PemertrationNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu14 = null;
            else
                this._PCExportReportANSI.ShouCeShu14 = Convert.ToDouble(this.sp_PemertrationNum.Value);
            if (this.sp_IgnitionNum.EditValue == null || this.sp_IgnitionNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu15 = null;
            else
                this._PCExportReportANSI.ShouCeShu15 = Convert.ToDouble(this.sp_IgnitionNum.Value);
            if (this.sp_CorrsionNum.EditValue == null || this.sp_CorrsionNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu16 = null;
            else
                this._PCExportReportANSI.ShouCeShu16 = Convert.ToDouble(this.sp_CorrsionNum.Value);
            if (this.sp_MarkingsNum.EditValue == null || this.sp_MarkingsNum.Value == 0)
                this._PCExportReportANSI.ShouCeShu17 = null;
            else
                this._PCExportReportANSI.ShouCeShu17 = Convert.ToDouble(this.sp_MarkingsNum.Value);


            //New Add
            this._PCExportReportANSI.LowImpact = this.checkLow.Checked;
            if (this.sp_Low.EditValue == null || this.sp_Low.Value == 0)
                this._PCExportReportANSI.ShouCeShu1 = null;
            else
                this._PCExportReportANSI.ShouCeShu1 = Convert.ToDouble(this.sp_Low.Value);
            this._PCExportReportANSI.CSAJiShuBiaoZhun = this.comboBoxEdit1.SelectedText;

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
            //this.SpinOrderAmount.Enabled = false;
            //this.SpinTestAmount.Enabled = false;
            this.TxtCustomersId.Enabled = false;
            this.TxtProduct.Enabled = false;


        }

        //列印
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            tag = 0;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            ASRO2017 r = new ASRO2017(this._PCExportReportANSI, tag);
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
            this._PCExportReportANSI.CustomerId = xd.Invoice.xocustomerId;
            this._PCExportReportANSI.Specification = xd.Invoice.Customer.CheckedStandard;
            this._PCExportReportANSI.Product = xd.Product;
            this._PCExportReportANSI.InvoiceCusXOId = xd.Invoice.CustomerInvoiceXOId;
            this._PCExportReportANSI.Amount = xd.InvoiceXODetailQuantity.HasValue ? xd.InvoiceXODetailQuantity.Value : 0;
            this._PCExportReportANSI.ProductUnitId = xd.Product.SellUnitId;

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
            //this._PCExportReportANSI.AmountTest = Common.AutoCalculation.Calculation("as", Convert.ToInt32(this._PCExportReportANSI.Amount));

            //this._PCExportReportANSI.ShouCeShu2 = this._PCExportReportANSI.AmountTest;

            //this._PCExportReportANSI.PanDing2 = _PCExportReportANSIDetail.pASCCSL;
            this._PCExportReportANSI.QuYangShu2 = this._PCExportReportANSI.ShouCeShu4 = this._PCExportReportANSI.ShouCeShu17 = this._PCExportReportANSI.AmountTest;

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
            this.NccTestPerson2.EditValue = this._PCExportReportANSI.Employee2;
            this.NccTestPerson3.EditValue = this._PCExportReportANSI.Employee3;
            this.NccTestPerson4.EditValue = this._PCExportReportANSI.Employee4;
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
            this.che_IsShowGX2.Checked = this._PCExportReportANSI.IsShowGX2.HasValue ? this._PCExportReportANSI.IsShowGX2.Value : false;
            this.checkPrismaticVUp.Checked = this._PCExportReportANSI.PrismaticPowerVUp.HasValue ? this._PCExportReportANSI.PrismaticPowerVUp.Value : false;
            this.checkScatter.Checked = this._PCExportReportANSI.ScatterLight.HasValue ? this._PCExportReportANSI.ScatterLight.Value : false;
            this.checkMedium.Checked = this._PCExportReportANSI.MediumImpact.HasValue ? this._PCExportReportANSI.MediumImpact.Value : false;
            this.checkHigh.Checked = this._PCExportReportANSI.HighImpact.HasValue ? this._PCExportReportANSI.HighImpact.Value : false;
            this.checkExtraHigh.Checked = this._PCExportReportANSI.ExtraHighImpact.HasValue ? this._PCExportReportANSI.ExtraHighImpact.Value : false;
            this.checkPemertration.Checked = this._PCExportReportANSI.PermertrationTest.HasValue ? this._PCExportReportANSI.PermertrationTest.Value : false;
            this.checkIgnition.Checked = this._PCExportReportANSI.IgnitionTest.HasValue ? this._PCExportReportANSI.IgnitionTest.Value : false;
            this.checkCorrsion.Checked = this._PCExportReportANSI.Corrsion.HasValue ? this._PCExportReportANSI.Corrsion.Value : false;
            this.checkMarkings.Checked = this._PCExportReportANSI.Markings.HasValue ? this._PCExportReportANSI.Markings.Value : false;

            //对外观，加热，坐标等判定新增的 测试数量
            this.sp_VisualNum.EditValue = this._PCExportReportANSI.ShouCeShu4.HasValue ? this._PCExportReportANSI.ShouCeShu4.Value : 0;
            this.sp_ThemalNum.EditValue = this._PCExportReportANSI.ShouCeShu5.HasValue ? this._PCExportReportANSI.ShouCeShu5.Value : 0;
            this.sp_PrismaticHInNum.EditValue = this._PCExportReportANSI.ShouCeShu6.HasValue ? this._PCExportReportANSI.ShouCeShu6.Value : 0;
            this.sp_PrismaticHOutNum.EditValue = this._PCExportReportANSI.ShouCeShu7.HasValue ? this._PCExportReportANSI.ShouCeShu7.Value : 0;
            this.sp_PrismaticVUpNum.EditValue = this._PCExportReportANSI.ShouCeShu8.HasValue ? this._PCExportReportANSI.ShouCeShu8.Value : 0;
            this.sp_PrismaticVDwnNum.EditValue = this._PCExportReportANSI.ShouCeShu9.HasValue ? this._PCExportReportANSI.ShouCeShu9.Value : 0;
            this.sp_ScatterNum.EditValue = this._PCExportReportANSI.ShouCeShu10.HasValue ? this._PCExportReportANSI.ShouCeShu10.Value : 0;
            this.sp_MediumNum.EditValue = this._PCExportReportANSI.ShouCeShu11.HasValue ? this._PCExportReportANSI.ShouCeShu11.Value : 0;
            this.sp_HighNum.EditValue = this._PCExportReportANSI.ShouCeShu12.HasValue ? this._PCExportReportANSI.ShouCeShu12.Value : 0;
            this.sp_ExtraHighNum.EditValue = this._PCExportReportANSI.ShouCeShu13.HasValue ? this._PCExportReportANSI.ShouCeShu13.Value : 0;
            this.sp_PemertrationNum.EditValue = this._PCExportReportANSI.ShouCeShu14.HasValue ? this._PCExportReportANSI.ShouCeShu14.Value : 0;
            this.sp_IgnitionNum.EditValue = this._PCExportReportANSI.ShouCeShu15.HasValue ? this._PCExportReportANSI.ShouCeShu15.Value : 0;
            this.sp_CorrsionNum.EditValue = this._PCExportReportANSI.ShouCeShu16.HasValue ? this._PCExportReportANSI.ShouCeShu16.Value : 0;
            this.sp_MarkingsNum.EditValue = this._PCExportReportANSI.ShouCeShu17.HasValue ? this._PCExportReportANSI.ShouCeShu17.Value : 0;

            this.newChooseContorlAuditEmp.EditValue = this._PCExportReportANSI.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCExportReportANSI.AuditState);
            this.lookUpEditUnit.EditValue = this._PCExportReportANSI.ProductUnitId;

            //New Add
            this.checkLow.Checked = this._PCExportReportANSI.LowImpact.HasValue ? this._PCExportReportANSI.LowImpact.Value : false;
            this.sp_Low.EditValue = this._PCExportReportANSI.ShouCeShu1.HasValue ? this._PCExportReportANSI.ShouCeShu1.Value : 0;
            this.comboBoxEdit1.Text = string.IsNullOrEmpty(this._PCExportReportANSI.CSAJiShuBiaoZhun) ? "Tested against AS/NZS 1337.1:2010 " : this._PCExportReportANSI.CSAJiShuBiaoZhun;
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
            ASRO2017 r = new ASRO2017(this._PCExportReportANSI, tag);
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
            ASRO2017 r = new ASRO2017(this._PCExportReportANSI, tag);
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