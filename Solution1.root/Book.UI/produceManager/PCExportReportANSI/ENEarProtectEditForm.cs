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
    public partial class ENEarProtectEditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        Model.PCExportReportANSI _pcExpANSI = null;
        BL.PCExportReportANSIManager _pcExpANSIManager = new Book.BL.PCExportReportANSIManager();
        string _ServerSavePath = string.Empty;      //附件存放地址
        int tag;                                    //列印标志

        public ENEarProtectEditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_ExportReportId, new AA(Properties.Resources.NumsIsNotNull, this.txtPCExportReportId));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.btnEditProduct));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_ReportDate, new AA(Properties.Resources.DateNotNull, this.dateEditReport));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_AmountTest, new AA("測試數量不能為空", this.spinEditCSSL));
            this.requireValueExceptions.Add(Model.PCExportReportANSI.PRO_InvoiceCusXOId, new AA("客戶訂單編號不能為空", this.txtCusXOid));

            this.nccCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();

            this.action = "view";
        }

        int sign = 0;
        public ENEarProtectEditForm(string invoiceId)
            : this()
        {
            this._pcExpANSI = this._pcExpANSIManager.Get(invoiceId);
            if (this._pcExpANSI == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            this.sign = 1;
        }

        public ENEarProtectEditForm(Model.PCExportReportANSI mPCExpANSI)
            : this()
        {
            if (mPCExpANSI == null)
                throw new ArithmeticException("invoiceid");
            this._pcExpANSI = mPCExpANSI;
            this.action = "view";
            this.sign = 1;
        }

        public ENEarProtectEditForm(Model.PCExportReportANSI mPCExpANSI, string action)
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
            this._pcExpANSI.ExpType = "EarPro";
        }

        protected override void Delete()
        {
            if (this._pcExpANSI == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pcExpANSIManager.Delete(this._pcExpANSI.ExportReportId);

            this._pcExpANSI = this._pcExpANSIManager.mget_next("EarPro", this._pcExpANSI.InsertTime.Value);

            if (this._pcExpANSI == null)
            {
                this._pcExpANSI = this._pcExpANSIManager.mget_last("EarPro");
            }
        }

        protected override void MoveLast()
        {
            if (this.sign == 1)
            {
                this.sign = 0;
                return;
            }
            this._pcExpANSI = this._pcExpANSIManager.mget_last("EarPro");
        }

        protected override void MoveFirst()
        {
            this._pcExpANSI = this._pcExpANSIManager.mget_first("EarPro");
        }

        protected override bool HasRows()
        {
            return this._pcExpANSIManager.mhas_rows("EarPro");
        }

        protected override bool HasRowsNext()
        {
            return this._pcExpANSIManager.mhas_rows_after("EarPro", this._pcExpANSI.InsertTime.Value);
        }

        protected override bool HasRowsPrev()
        {
            return this._pcExpANSIManager.mhas_rows_before("EarPro", this._pcExpANSI.InsertTime.Value);
        }

        protected override void MoveNext()
        {
            Model.PCExportReportANSI pcer = this._pcExpANSIManager.mget_next("EarPro", this._pcExpANSI.InsertTime.Value);
            if (pcer == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcExpANSI = pcer;
        }

        protected override void MovePrev()
        {
            Model.PCExportReportANSI pcer = this._pcExpANSIManager.mget_prev("EarPro", this._pcExpANSI.InsertTime.Value);
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

            this._pcExpANSI.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();

            this._pcExpANSI.ShouCeShu1 = this.spinEditShouCe1.EditValue == null ? 0 : double.Parse(this.spinEditShouCe1.EditValue.ToString());
            this._pcExpANSI.ShouCeShu2 = this.spinEditShouCe2.EditValue == null ? 0 : double.Parse(this.spinEditShouCe2.EditValue.ToString());
            this._pcExpANSI.ShouCeShu3 = this.spinEditShouCe3.EditValue == null ? 0 : double.Parse(this.spinEditShouCe3.EditValue.ToString());
            this._pcExpANSI.ShouCeShu4 = this.spinEditShouCe4.EditValue == null ? 0 : double.Parse(this.spinEditShouCe4.EditValue.ToString());
            this._pcExpANSI.ShouCeShu5 = this.spinEditShouce5.EditValue == null ? 0 : double.Parse(this.spinEditShouce5.EditValue.ToString());
            this._pcExpANSI.ShouCeShu6 = this.spinEditShouce6.EditValue == null ? 0 : double.Parse(this.spinEditShouce6.EditValue.ToString());
            this._pcExpANSI.PanDing1 = this.spinEditPanDing1.EditValue == null ? 0 : double.Parse(this.spinEditPanDing1.EditValue.ToString());
            this._pcExpANSI.PanDing2 = this.spinEditPanDing2.EditValue == null ? 0 : double.Parse(this.spinEditPanDing2.EditValue.ToString());
            this._pcExpANSI.PanDing3 = this.spinEditPanDing3.EditValue == null ? 0 : double.Parse(this.spinEditPanDing3.EditValue.ToString());
            this._pcExpANSI.PanDing4 = this.spinEditPanDing4.EditValue == null ? 0 : double.Parse(this.spinEditPanDing4.EditValue.ToString());
            this._pcExpANSI.PanDing5 = this.spinEditPanDing5.EditValue == null ? 0 : double.Parse(this.spinEditPanDing5.EditValue.ToString());
            this._pcExpANSI.PanDing6 = this.spinEditPanDing6.EditValue == null ? 0 : double.Parse(this.spinEditPanDing6.EditValue.ToString());

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
            this.cob_Size324.Enabled = true;
            this.cob_Size324.Properties.ReadOnly = false;
            this.cob_EarmuffsAbove.Properties.ReadOnly = false;
        }

        //列印
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            tag = 0;
            bool canSave = (DialogResult.OK == MessageBox.Show("是否將打印文件上傳至服務器(pdf格式)", "操作提示", MessageBoxButtons.OKCancel));
            ENEarProtectRO r = new ENEarProtectRO(this._pcExpANSI, tag, this.cob_Size324.Text, this.lbl_ValueA1.Text, this.lbl_ValueB1.Text, this.cob_EarmuffsAbove.Text);
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
            this.txtCusXOid.Text = this._pcExpANSI.InvoiceCusXOId;
            this.txtPCExportReportId.Text = this._pcExpANSI.ExportReportId;
            this.dateEditReport.EditValue = this._pcExpANSI.ReportDate.Value;
            this.spinEditDDSL.EditValue = this._pcExpANSI.Amount.HasValue ? this._pcExpANSI.Amount.Value : 0;
            this.spinEditCSSL.EditValue = this._pcExpANSI.AmountTest.HasValue ? this._pcExpANSI.AmountTest.Value : 0;

            this.btnEditProduct.EditValue = this._pcExpANSI.Product;
            this.nccCustomer.EditValue = this._pcExpANSI.Customer;
            this.nccEmployee.EditValue = this._pcExpANSI.Employee;

            this.newChooseContorlAuditEmp.EditValue = this._pcExpANSI.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pcExpANSI.AuditState);
            this.lookUpEditUnit.EditValue = this._pcExpANSI.ProductUnitId;

            this.spinEditShouCe1.EditValue = this._pcExpANSI.ShouCeShu1.HasValue ? this._pcExpANSI.ShouCeShu1.Value : 0;
            this.spinEditShouCe2.EditValue = this._pcExpANSI.ShouCeShu2.HasValue ? this._pcExpANSI.ShouCeShu2.Value : 0;
            this.spinEditShouCe3.EditValue = this._pcExpANSI.ShouCeShu3.HasValue ? this._pcExpANSI.ShouCeShu3.Value : 0;
            this.spinEditShouCe4.EditValue = this._pcExpANSI.ShouCeShu4.HasValue ? this._pcExpANSI.ShouCeShu4.Value : 0;
            this.spinEditShouce5.EditValue = this._pcExpANSI.ShouCeShu5.HasValue ? this._pcExpANSI.ShouCeShu5.Value : 0;
            this.spinEditShouce6.EditValue = this._pcExpANSI.ShouCeShu6.HasValue ? this._pcExpANSI.ShouCeShu6.Value : 0;
            this.spinEditPanDing1.EditValue = this._pcExpANSI.PanDing1.HasValue ? this._pcExpANSI.PanDing1.Value : 0;
            this.spinEditPanDing2.EditValue = this._pcExpANSI.PanDing2.HasValue ? this._pcExpANSI.PanDing2.Value : 0;
            this.spinEditPanDing3.EditValue = this._pcExpANSI.PanDing3.HasValue ? this._pcExpANSI.PanDing3.Value : 0;
            this.spinEditPanDing4.EditValue = this._pcExpANSI.PanDing4.HasValue ? this._pcExpANSI.PanDing4.Value : 0;
            this.spinEditPanDing5.EditValue = this._pcExpANSI.PanDing5.HasValue ? this._pcExpANSI.PanDing5.Value : 0;
            this.spinEditPanDing6.EditValue = this._pcExpANSI.PanDing6.HasValue ? this._pcExpANSI.PanDing6.Value : 0;
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

            ////获取ANSI,Finish质检统计记录
            ////Model.PCExportReportANSI mPCExpANSI = this._pcExpANSIManager.SelectForExpANSI(xod.Invoice.CustomerInvoiceXOId, xod.Product.ProductId);
            //Model.PCExportReportANSIDetail mPCExpANSIDet = new BL.PCExportReportANSIDetailManager().SelectForExpANSIDetailsSUM(xod.Invoice.CustomerInvoiceXOId, xod.Product.ProductId);
            //if (mPCExpANSIDet != null)
            //{
            //    #region 测试数量与合格数量

            //    //受测数量默认为订单数量的1/500,无条件进位.最大12
            //    int mInvoiceXoDetailQuantity = int.Parse(this._pcExpANSI.Amount.HasValue ? this._pcExpANSI.Amount.ToString() : "0");

            //    double mMustCheck = 0;

            //    if (mInvoiceXoDetailQuantity < 500)
            //        mMustCheck = 1;
            //    else
            //        mMustCheck = mInvoiceXoDetailQuantity % 500 == 0 ? mInvoiceXoDetailQuantity / 500 : mInvoiceXoDetailQuantity / 500 + 1;

            //    this._pcExpANSI.AmountTest = mMustCheck > 12 ? 12 : mMustCheck;    //受测数量1/500订单数量,上限12个,无条件进位

            //    this._pcExpANSI.ShouCeShu1 = this._pcExpANSI.ShouCeShu2 = this._pcExpANSI.ShouCeShu3 = this._pcExpANSI.ShouCeShu4 = this._pcExpANSI.ShouCeShu5 = this._pcExpANSI.ShouCeShu6 = this._pcExpANSI.ShouCeShu7 = this._pcExpANSI.ShouCeShu8 = this._pcExpANSI.ShouCeShu9 = this._pcExpANSI.ShouCeShu10 = this._pcExpANSI.ShouCeShu11 = this._pcExpANSI.AmountTest;

            //    this._pcExpANSI.PanDing0 = mPCExpANSIDet.pMSJY;
            //    this._pcExpANSI.QuYangShu1 = mPCExpANSIDet.qQXD;
            //    this._pcExpANSI.PanDing1 = mPCExpANSIDet.pQXD;
            //    this._pcExpANSI.QuYangShu2 = mPCExpANSIDet.qLJPHDS;
            //    this._pcExpANSI.PanDing2 = mPCExpANSIDet.pLJPHDS;
            //    this._pcExpANSI.QuYangShu3 = mPCExpANSIDet.qKJGTSL;
            //    this._pcExpANSI.PanDing3 = mPCExpANSIDet.pKJGTSL;
            //    this._pcExpANSI.QuYangShu4 = mPCExpANSIDet.qZWXTSL;
            //    this._pcExpANSI.PanDing4 = mPCExpANSIDet.pZWXTSL;
            //    this._pcExpANSI.QuYangShu5 = mPCExpANSIDet.qQMDS;
            //    this._pcExpANSI.PanDing5 = mPCExpANSIDet.pQMDS;
            //    this._pcExpANSI.QuYangShu6 = mPCExpANSIDet.qSGDS;
            //    this._pcExpANSI.PanDing6 = mPCExpANSIDet.pSGDS;
            //    this._pcExpANSI.QuYangShu7 = mPCExpANSIDet.qGSCJCS;
            //    this._pcExpANSI.PanDing7 = mPCExpANSIDet.pGSCJCS;
            //    this._pcExpANSI.QuYangShu8 = mPCExpANSIDet.qYZZLZJCS;
            //    this._pcExpANSI.PanDing8 = mPCExpANSIDet.pYZZLZJCS;
            //    this._pcExpANSI.QuYangShu9 = mPCExpANSIDet.qJPCTSC;
            //    this._pcExpANSI.PanDing9 = mPCExpANSIDet.pJPCTSC;
            //    this._pcExpANSI.QuYangShu10 = mPCExpANSIDet.qWDCS;
            //    this._pcExpANSI.PanDing10 = mPCExpANSIDet.pWDCS;
            //    this._pcExpANSI.QuYangShu11 = mPCExpANSIDet.qNRXCS;
            //    this._pcExpANSI.PanDing11 = mPCExpANSIDet.pNRXCS;
            //    #endregion
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
            ENEarProtectRO r = new ENEarProtectRO(this._pcExpANSI, tag, this.cob_Size324.Text, this.lbl_ValueA1.Text, this.lbl_ValueB1.Text, this.cob_EarmuffsAbove.Text);
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
            ENEarProtectRO r = new ENEarProtectRO(this._pcExpANSI, tag, this.cob_Size324.Text, this.lbl_ValueA1.Text, this.lbl_ValueB1.Text, this.cob_EarmuffsAbove.Text);
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

        private void cob_Size324_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cob_Size324.SelectedIndex == 0)       //S
            {
                this.lbl_ValueA1.Text = "122";
                this.lbl_ValueB1.Text = "135";
                this.lbl_ValueB2.Text = "135";
            }
            else if (cob_Size324.SelectedIndex == 1)  //M
            {
                this.lbl_ValueA1.Text = "130";
                this.lbl_ValueB1.Text = "145";
                this.lbl_ValueB2.Text = "145";
            }
            else                                       //L
            {
                this.lbl_ValueA1.Text = "135";
                this.lbl_ValueB1.Text = "150";
                this.lbl_ValueB2.Text = "150";
            }
        }
    }
}