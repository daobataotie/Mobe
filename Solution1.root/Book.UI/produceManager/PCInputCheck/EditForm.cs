using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCInputCheck
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.PCInputCheck _PCInputCheck = null;
        BL.PCInputCheckManager manager = new Book.BL.PCInputCheckManager();

        int LastFlag = 0;
        int tag = 0;
        public EditForm()
        {
            InitializeComponent();

            this.invalidValueExceptions.Add(Model.PCInputCheck.PRO_PCInputCheckDate, new AA(Properties.Resources.DateNotNull, this.date_PCInputCheckDate));
            //this.invalidValueExceptions.Add(Model.PCMouldOnlineCheckDetail.PRO_CheckDate, new AA("檢查日期不能為空！", this.gridControl1));
            //this.invalidValueExceptions.Add(Model.PCMouldOnlineCheckDetail.PRO_OnlineDate, new AA(Properties.Resources.OnlineDateNotNull, this.gridControl1));

            //this.action = "view";
            this.nccSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.nccMadeEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccConfirmor.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccTestEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccDuise.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccChongji.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccNairan.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccUV.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccToushilv.Choose = new Settings.BasicData.Employees.ChooseEmployee();

            this.action = "view";
        }

        public EditForm(string PCInputCheckId)
            : this()
        {
            this._PCInputCheck = this.manager.Get(PCInputCheckId);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCInputCheck model)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._PCInputCheck = model;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCInputCheck model, string action)
            : this()
        {
            if (model == null)
                throw new ArithmeticException("invoiceid");
            this._PCInputCheck = model;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(IList<Model.InvoiceCGDetail> list)
            : this()
        {
            this.AddNew();

            this.btn_InvoiceCGId1.EditValue = list[0].InvoiceId;
            this.btn_InvoiceCOId1.EditValue = list[0].InvoiceCOId;
            this.btn_Product.EditValue = list[0].Product;
            this.nccSupplier.EditValue = list[0].Invoice == null ? null : list[0].Invoice.Supplier;
            this.cobUnit.EditValue = list[0].InvoiceProductUnit;

            this._PCInputCheck.InvoiceCGId = list[0].InvoiceId;
            this._PCInputCheck.InvoiceCOId = list[0].InvoiceCOId;
            this._PCInputCheck.Product = list[0].Product;
            this._PCInputCheck.Supplier = list[0].Invoice == null ? null : list[0].Invoice.Supplier;
            this._PCInputCheck.ProductUnit = list[0].InvoiceProductUnit;

            this.LastFlag = 1;
            this.tag = 1;
        }

        protected override void AddNew()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this._PCInputCheck = new Book.Model.PCInputCheck();
            this._PCInputCheck.PCInputCheckId = this.manager.GetId();
            this._PCInputCheck.PCInputCheckDate = DateTime.Now;
            this._PCInputCheck.CheckEmployee = BL.V.ActiveOperator.Employee;

            this._PCInputCheck.Heidian = "0";
            this._PCInputCheck.Guohuo = "0";
            this._PCInputCheck.Liaodian = "0";
            this._PCInputCheck.Wasiqi = "0";
            this._PCInputCheck.Zazhi = "0";
            this._PCInputCheck.Qipao = "0";

            this._PCInputCheck.Duise = "0";
            this._PCInputCheck.Chongji = "0";
            this._PCInputCheck.Nairanceshi = "0";
            this._PCInputCheck.UVvalue = "0";
            this._PCInputCheck.Wudu = "0";

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this.manager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.manager.HasRowsBefore(this._PCInputCheck);
        }

        protected override bool HasRowsNext()
        {
            return this.manager.HasRowsAfter(this._PCInputCheck);
        }

        protected override void MoveFirst()
        {
            this._PCInputCheck = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._PCInputCheck = this.manager.GetLast();
        }

        protected override void MovePrev()
        {
            Model.PCInputCheck p = this.manager.GetPrev(this._PCInputCheck);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCInputCheck = p;
        }

        protected override void MoveNext()
        {
            Model.PCInputCheck p = this.manager.GetNext(this._PCInputCheck);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCInputCheck = p;
        }

        protected override void Save()
        {
            this._PCInputCheck.PCInputCheckId = this.txt_PCInputCheckId.Text;
            if (this.date_PCInputCheckDate.EditValue != null)
                this._PCInputCheck.PCInputCheckDate = this.date_PCInputCheckDate.DateTime;
            this._PCInputCheck.SupplierId = this.nccSupplier.EditValue == null ? null : (this.nccSupplier.EditValue as Model.Supplier).SupplierId;
            this._PCInputCheck.CheckEmployeeId = this.nccMadeEmp.EditValue == null ? null : (this.nccMadeEmp.EditValue as Model.Employee).EmployeeId;
            this._PCInputCheck.ProductId = this.btn_Product.EditValue == null ? null : (this.btn_Product.EditValue as Model.Product)
                .ProductId;
            this._PCInputCheck.TestProductId = this.txt_TestProduct.Text;
            if (this.date_Chouliao.EditValue != null)
                this._PCInputCheck.ChouliaoDate = this.date_Chouliao.DateTime;
            if (this.date_Test.EditValue != null)
                this._PCInputCheck.TestDate = this.date_Test.DateTime;
            this._PCInputCheck.Quantity = this.spe_Quantity.Value;
            this._PCInputCheck.ProductUnit = this.cobUnit.Text;
            this._PCInputCheck.TestEmployeeId = this.nccTestEmployee.EditValue == null ? null : (this.nccTestEmployee.EditValue as Model.Employee).EmployeeId;
            this._PCInputCheck.LotNumber = this.txt_LotNumber.Text;

            this._PCInputCheck.Heidian = string.IsNullOrEmpty(this.rdo_Heidian.SelectedIndex.ToString()) ? "0" : this.rdo_Heidian.SelectedIndex.ToString();
            this._PCInputCheck.Guohuo = string.IsNullOrEmpty(this.rdo_Guohuo.SelectedIndex.ToString()) ? "0" : this.rdo_Guohuo.SelectedIndex.ToString();
            this._PCInputCheck.Liaodian = string.IsNullOrEmpty(this.rdo_Liaodian.SelectedIndex.ToString()) ? "0" : this.rdo_Liaodian.SelectedIndex.ToString();
            this._PCInputCheck.Wasiqi = string.IsNullOrEmpty(this.rdo_Wasiqi.SelectedIndex.ToString()) ? "0" : this.rdo_Wasiqi.SelectedIndex.ToString();
            this._PCInputCheck.Zazhi = string.IsNullOrEmpty(this.rdo_Zazhi.SelectedIndex.ToString()) ? "0" : this.rdo_Zazhi.SelectedIndex.ToString();
            this._PCInputCheck.Qipao = string.IsNullOrEmpty(this.rdo_Qipao.SelectedIndex.ToString()) ? "0" : this.rdo_Qipao.SelectedIndex.ToString();
            this._PCInputCheck.Duise = string.IsNullOrEmpty(this.rdo_Duise.SelectedIndex.ToString()) ? "0" : this.rdo_Duise.SelectedIndex.ToString();
            this._PCInputCheck.Chongji = string.IsNullOrEmpty(this.rdo_Chongji.SelectedIndex.ToString()) ? "0" : this.rdo_Chongji.SelectedIndex.ToString();
            this._PCInputCheck.Nairanceshi = string.IsNullOrEmpty(this.rdo_Nairan.SelectedIndex.ToString()) ? "0" : this.rdo_Nairan.SelectedIndex.ToString();
            this._PCInputCheck.UVvalue = string.IsNullOrEmpty(this.rdo_UV.SelectedIndex.ToString()) ? "0" : this.rdo_UV.SelectedIndex.ToString();
            //this._PCInputCheck.Wudu = this.rdo_Wudu.SelectedIndex.ToString();
            this._PCInputCheck.Wudu = this.txt_Wudu.Text;

            this._PCInputCheck.ANSICSAToushilv = this.txt_ANSICSA.Text;
            this._PCInputCheck.ENToushilv = this.txt_EN.Text;
            this._PCInputCheck.ASToushilv = this.txt_AS.Text;
            this._PCInputCheck.JISToushilv = this.txt_JIS.Text;

            this._PCInputCheck.DuiseEmployeeId = this.nccDuise.EditValue == null ? null : (this.nccDuise.EditValue as Model.Employee).EmployeeId;
            this._PCInputCheck.ChongjiEmployeeId = this.nccChongji.EditValue == null ? null : (this.nccChongji.EditValue as Model.Employee).EmployeeId;
            this._PCInputCheck.NairanEmployeeId = this.nccNairan.EditValue == null ? null : (this.nccNairan.EditValue as Model.Employee).EmployeeId;
            this._PCInputCheck.UVEmployeeId = this.nccUV.EditValue == null ? null : (this.nccUV.EditValue as Model.Employee).EmployeeId;
            this._PCInputCheck.ToushiEmployeeId = this.nccToushilv.EditValue == null ? null : (this.nccToushilv.EditValue as Model.Employee).EmployeeId;

            this._PCInputCheck.ConfirmorId = this.nccConfirmor.EditValue == null ? null : (this.nccConfirmor.EditValue as Model.Employee).EmployeeId;

            //采购单，进库单
            this._PCInputCheck.InvoiceCOId = this.btn_InvoiceCOId1.Text + "," + this.btn_InvoiceCOId2.Text + "," + this.btn_InvoiceCOId3.Text;
            this._PCInputCheck.InvoiceCGId = this.btn_InvoiceCGId1.Text + "," + this.btn_InvoiceCGId2.Text + "," + this.btn_InvoiceCGId3.Text;
            this._PCInputCheck.InvoiceXOCusId = this.txt_InvoiceXOCusId.Text;

            this._PCInputCheck.TestQuantity = this.spe_TestQuantity.Value;

            this._PCInputCheck.PCImpactCheckId = this.btn_Impact.Text;
            this._PCInputCheck.PCFogCheckId = this.btn_Fog.Text;
            this._PCInputCheck.PCFlameRetardantId = this.btn_FlameRetardant.Text;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._PCInputCheck);
                    break;
                case "update":
                    this.manager.Update(this._PCInputCheck);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._PCInputCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._PCInputCheck = this.manager.Get(this._PCInputCheck.PCInputCheckId);
            }

            this.txt_PCInputCheckId.EditValue = this._PCInputCheck.PCInputCheckId;
            this.date_PCInputCheckDate.EditValue = this._PCInputCheck.PCInputCheckDate;
            this.nccSupplier.EditValue = this._PCInputCheck.Supplier;
            this.nccMadeEmp.EditValue = this._PCInputCheck.CheckEmployee;
            this.btn_Product.EditValue = this._PCInputCheck.Product;
            this.txt_TestProduct.EditValue = this._PCInputCheck.TestProductId;
            this.date_Chouliao.EditValue = this._PCInputCheck.ChouliaoDate;
            this.date_Test.EditValue = this._PCInputCheck.TestDate;
            this.spe_Quantity.EditValue = this._PCInputCheck.Quantity;
            this.cobUnit.EditValue = this._PCInputCheck.ProductUnit;
            this.nccTestEmployee.EditValue = this._PCInputCheck.TestEmployee;
            this.txt_LotNumber.EditValue = this._PCInputCheck.LotNumber;

            this.rdo_Heidian.EditValue = string.IsNullOrEmpty(this._PCInputCheck.Heidian) ? "0" : this._PCInputCheck.Heidian;
            this.rdo_Guohuo.EditValue = string.IsNullOrEmpty(this._PCInputCheck.Guohuo) ? "0" : this._PCInputCheck.Guohuo;
            this.rdo_Liaodian.EditValue = string.IsNullOrEmpty(this._PCInputCheck.Liaodian) ? "0" : this._PCInputCheck.Liaodian;
            this.rdo_Wasiqi.EditValue = string.IsNullOrEmpty(this._PCInputCheck.Wasiqi) ? "0" : this._PCInputCheck.Wasiqi;
            this.rdo_Zazhi.EditValue = string.IsNullOrEmpty(this._PCInputCheck.Zazhi) ? "0" : this._PCInputCheck.Zazhi;
            this.rdo_Qipao.EditValue = string.IsNullOrEmpty(this._PCInputCheck.Qipao) ? "0" : this._PCInputCheck.Qipao;
            this.rdo_Duise.EditValue = string.IsNullOrEmpty(this._PCInputCheck.Duise) ? "0" : this._PCInputCheck.Duise;
            this.rdo_Chongji.EditValue = string.IsNullOrEmpty(this._PCInputCheck.Chongji) ? "0" : this._PCInputCheck.Chongji;
            this.rdo_Nairan.EditValue = string.IsNullOrEmpty(this._PCInputCheck.Nairanceshi) ? "0" : this._PCInputCheck.Nairanceshi;
            this.rdo_UV.EditValue = string.IsNullOrEmpty(this._PCInputCheck.UVvalue) ? "0" : this._PCInputCheck.UVvalue;
            //this.rdo_Wudu.EditValue = this._PCInputCheck.Wudu;
            this.txt_Wudu.EditValue = this._PCInputCheck.Wudu;

            this.txt_ANSICSA.EditValue = this._PCInputCheck.ANSICSAToushilv;
            this.txt_EN.EditValue = this._PCInputCheck.ENToushilv;
            this.txt_AS.EditValue = this._PCInputCheck.ASToushilv;
            this.txt_JIS.EditValue = this._PCInputCheck.JISToushilv;

            this.nccDuise.EditValue = this._PCInputCheck.DuiseEmployee;
            this.nccChongji.EditValue = this._PCInputCheck.ChongjiEmployee;
            this.nccNairan.EditValue = this._PCInputCheck.NairanEmployee;
            this.nccUV.EditValue = this._PCInputCheck.UVEmployee;
            this.nccToushilv.EditValue = this._PCInputCheck.ToushiEmployee;

            this.nccConfirmor.EditValue = this._PCInputCheck.Confirmor;
            this.txt_InvoiceXOCusId.EditValue = this._PCInputCheck.InvoiceXOCusId;

            this.spe_TestQuantity.EditValue = this._PCInputCheck.TestQuantity;

            //采购单，进库单
            string[] coid = string.IsNullOrEmpty(this._PCInputCheck.InvoiceCOId) ? null : this._PCInputCheck.InvoiceCOId.Split(',');
            string[] cgid = string.IsNullOrEmpty(this._PCInputCheck.InvoiceCGId) ? null : this._PCInputCheck.InvoiceCGId.Split(',');
            if (cgid != null)
            {
                this.btn_InvoiceCGId1.EditValue = cgid[0];
                this.btn_InvoiceCGId2.EditValue = cgid.Length > 1 ? cgid[1] : "";
                this.btn_InvoiceCGId3.EditValue = cgid.Length > 2 ? cgid[2] : "";
            }
            else
            {
                this.btn_InvoiceCGId1.EditValue = null;
                this.btn_InvoiceCGId2.EditValue = null;
                this.btn_InvoiceCGId3.EditValue = null;

            }
            if (coid != null)
            {
                this.btn_InvoiceCOId1.EditValue = coid[0];
                this.btn_InvoiceCOId2.EditValue = coid.Length > 1 ? coid[1] : "";
                this.btn_InvoiceCOId3.EditValue = coid.Length > 2 ? coid[2] : "";
            }
            else
            {
                this.btn_InvoiceCOId1.EditValue = null;
                this.btn_InvoiceCOId2.EditValue = null;
                this.btn_InvoiceCOId3.EditValue = null;
            }

            this.btn_Impact.EditValue = this._PCInputCheck.PCImpactCheckId;
            this.btn_Fog.EditValue = this._PCInputCheck.PCFogCheckId;
            this.btn_FlameRetardant.EditValue = this._PCInputCheck.PCFlameRetardantId;

            base.Refresh();

            switch (this.action)
            {
                case "view":
                    this.btn_InvoiceCGId1.Properties.Buttons[1].Visible = false;
                    this.btn_InvoiceCGId2.Properties.Buttons[1].Visible = false;
                    this.btn_InvoiceCGId3.Properties.Buttons[1].Visible = false;
                    this.btn_InvoiceCGId1.Properties.ReadOnly = true;
                    this.btn_InvoiceCGId2.Properties.ReadOnly = true;
                    this.btn_InvoiceCGId3.Properties.ReadOnly = true;
                    this.btn_InvoiceCOId1.Properties.ReadOnly = true;
                    this.btn_InvoiceCOId2.Properties.ReadOnly = true;
                    this.btn_InvoiceCOId3.Properties.ReadOnly = true;
                    this.btn_Product.Properties.ReadOnly = true;
                    //this.btn_Impact.Properties.ReadOnly = true;
                    //this.btn_Fog.Properties.ReadOnly = true;
                    //this.btn_FlameRetardant.Properties.ReadOnly = true;
                    break;
                default:
                    this.btn_InvoiceCGId1.Properties.Buttons[1].Visible = true;
                    this.btn_InvoiceCGId2.Properties.Buttons[1].Visible = true;
                    this.btn_InvoiceCGId3.Properties.Buttons[1].Visible = true;
                    this.btn_InvoiceCGId1.Properties.ReadOnly = true;
                    this.btn_InvoiceCGId2.Properties.ReadOnly = true;
                    this.btn_InvoiceCGId3.Properties.ReadOnly = true;
                    this.btn_InvoiceCOId1.Properties.ReadOnly = true;
                    this.btn_InvoiceCOId2.Properties.ReadOnly = true;
                    this.btn_InvoiceCOId3.Properties.ReadOnly = true;
                    this.btn_Product.Properties.ReadOnly = true;
                    //this.btn_Impact.Properties.ReadOnly = true;
                    //this.btn_Fog.Properties.ReadOnly = true;
                    //this.btn_FlameRetardant.Properties.ReadOnly = true;
                    break;
            }
            updateCaption();
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._PCInputCheck);
        }

        protected override void Delete()
        {
            if (this._PCInputCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.PCInputCheck model = this.manager.GetNext(this._PCInputCheck);
                this.manager.Delete(this._PCInputCheck.PCInputCheckId);
                if (model == null)
                    this._PCInputCheck = this.manager.GetLast();
                else
                    this._PCInputCheck = model;
            }
        }

        private void btn_Product_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_Product.EditValue = f.SelectedItem as Model.Product;

            }
        }

        private void btn_Product_EditValueChanged(object sender, EventArgs e)
        {
            Model.Product p = this.btn_Product.EditValue as Model.Product;
            if (p != null)
            {
                if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                {
                    BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                    this.cobUnit.Properties.Items.Clear();
                    IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroupId);
                    foreach (Model.ProductUnit unit in unitList)
                    {
                        this.cobUnit.Properties.Items.Add(unit.CnName);
                    }
                }
            }
            else
                this.cobUnit.Properties.Items.Clear();
        }

        private void cobUnit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //Model.Product p = this.btn_Product.EditValue as Model.Product;
            //if (p != null)
            //{
            //    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
            //    {
            //        BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
            //        this.cobUnit.Properties.Items.Clear();
            //        IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroupId);
            //        foreach (Model.ProductUnit unit in unitList)
            //        {
            //            this.cobUnit.Properties.Items.Add(unit.CnName);
            //        }
            //    }
            //}
            //else
            //    this.cobUnit.Properties.Items.Clear();
            if (this.cobUnit.Properties.Items.Count < 1)
            {
                MessageBox.Show("請先選擇商品！", this.Text, MessageBoxButtons.OK);
            }

        }

        //#region  选取采购单，改到选取进库单时拉取

        //private void btn_InvoiceCOId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    Invoices.CG.CGForm f = new Book.UI.Invoices.CG.CGForm();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //        if (f.key != null && f.key.Count > 0)
        //            this.btn_InvoiceCOId1.EditValue = f.key[0].InvoiceId;
        //    }
        //}

        //private void btn_InvoiceCOId2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    Invoices.CG.CGForm f = new Book.UI.Invoices.CG.CGForm();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //        if (f.key != null && f.key.Count > 0)
        //            this.btn_InvoiceCOId2.EditValue = f.key[0].InvoiceId;
        //    }
        //}

        //private void btn_InvoiceCOId3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    Invoices.CG.CGForm f = new Book.UI.Invoices.CG.CGForm();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //        if (f.key != null && f.key.Count > 0)
        //            this.btn_InvoiceCOId3.EditValue = f.key[0].InvoiceId;
        //    }
        //}
        //#endregion

        private void btn_InvoiceCGId1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                Invoices.CG.SearchCGDetail f = new Book.UI.Invoices.CG.SearchCGDetail();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    if (f.selectItems != null && f.selectItems.Count > 0)
                    {
                        this.btn_InvoiceCGId1.EditValue = f.selectItems[0].InvoiceId;
                        this.btn_InvoiceCOId1.EditValue = f.selectItems[0].InvoiceCOId;
                        this.btn_Product.EditValue = f.selectItems[0].Product;
                        this.nccSupplier.EditValue = f.selectItems[0].Invoice == null ? null : f.selectItems[0].Invoice.Supplier;
                        this.cobUnit.EditValue = f.selectItems[0].InvoiceProductUnit;
                    }
                }
            }
            else
            {
                this.btn_InvoiceCGId1.EditValue = null;
                this.btn_InvoiceCOId1.EditValue = null;

            }
        }

        private void btn_InvoiceCGId2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                Invoices.CG.SearchCGDetail f = new Book.UI.Invoices.CG.SearchCGDetail();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    if (f.selectItems != null && f.selectItems.Count > 0)
                    {
                        this.btn_InvoiceCGId2.EditValue = f.selectItems[0].InvoiceId;
                        this.btn_InvoiceCOId2.EditValue = f.selectItems[0].InvoiceCOId;
                    }
                }
            }
            else
            {
                this.btn_InvoiceCGId2.EditValue = null;
                this.btn_InvoiceCOId2.EditValue = null;

            }
        }

        private void btn_InvoiceCGId3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                Invoices.CG.SearchCGDetail f = new Book.UI.Invoices.CG.SearchCGDetail();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    if (f.selectItems != null && f.selectItems.Count > 0)
                    {
                        this.btn_InvoiceCGId3.EditValue = f.selectItems[0].InvoiceId;
                        this.btn_InvoiceCOId3.EditValue = f.selectItems[0].InvoiceCOId;
                    }
                }
            }
            else
            {
                this.btn_InvoiceCGId3.EditValue = null;
                this.btn_InvoiceCOId3.EditValue = null;

            }
        }

        private void btn_InvoiceCGId1_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.btn_InvoiceCGId1.Text))
            {
                Invoices.CG.EditForm f = new Book.UI.Invoices.CG.EditForm(this.btn_InvoiceCGId1.Text);
                f.ShowDialog(this);
            }
        }

        private void btn_InvoiceCGId2_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.btn_InvoiceCGId2.Text))
            {
                Invoices.CG.EditForm f = new Book.UI.Invoices.CG.EditForm(this.btn_InvoiceCGId2.Text);
                f.ShowDialog(this);
            }
        }

        private void btn_InvoiceCGId3_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.btn_InvoiceCGId3.Text))
            {
                Invoices.CG.EditForm f = new Book.UI.Invoices.CG.EditForm(this.btn_InvoiceCGId3.Text);
                f.ShowDialog(this);
            }
        }

        private void btn_InvoiceCOId_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.btn_InvoiceCOId1.Text))
            {
                Invoices.CO.EditForm f = new Book.UI.Invoices.CO.EditForm(this.btn_InvoiceCOId1.Text);
                f.ShowDialog(this);
            }
        }

        private void btn_InvoiceCOId2_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.btn_InvoiceCOId2.Text))
            {
                Invoices.CO.EditForm f = new Book.UI.Invoices.CO.EditForm(this.btn_InvoiceCOId2.Text);
                f.ShowDialog(this);
            }
        }

        private void btn_InvoiceCOId3_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.btn_InvoiceCOId3.Text))
            {
                Invoices.CO.EditForm f = new Book.UI.Invoices.CO.EditForm(this.btn_InvoiceCOId3.Text);
                f.ShowDialog(this);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._PCInputCheck = f.SelectItem;
                this.action = "view";
                this.Refresh();
            }
        }

        //关联客户订单
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RelationXOForm f = new RelationXOForm();
            f.Show(this);
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCInputCheck.PRO_PCInputCheckId;
        }

        protected override int AuditState()
        {
            return this._PCInputCheck.AuditState.HasValue ? this._PCInputCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCInputCheck" + "," + this._PCInputCheck.PCInputCheckId;
        }

        #endregion

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this._PCInputCheck == null) return;
            if (this._PCInputCheck.IsClosed == null || this._PCInputCheck.IsClosed == false)
            {
                if (MessageBox.Show("是否強制結案?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }
            if (this._PCInputCheck.IsClosed != null && this._PCInputCheck.IsClosed.Value)
            {
                if (MessageBox.Show("是否取消結案?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }
            this._PCInputCheck.IsClosed = (this._PCInputCheck.IsClosed == null || this._PCInputCheck.IsClosed == false) ? true : false;
            try
            {
                BL.V.BeginTransaction();
                this.manager.UpdateIsClosed(this._PCInputCheck);
                BL.V.CommitTransaction();
                MessageBox.Show("操作成功", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.updateCaption();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                this._PCInputCheck.IsClosed = (this._PCInputCheck.IsClosed == null || this._PCInputCheck.IsClosed == false) ? true : false;
                throw ex;
            }
        }

        private void updateCaption()
        {
            if (this._PCInputCheck.IsClosed == null)
            {
                this._PCInputCheck.IsClosed = false;
            }
            if (this._PCInputCheck.IsClosed.Value)
            {
                this.barButtonItem4.Caption = "取消結案";
            }

            else
                this.barButtonItem4.Caption = "結案";
            this.barButtonItem4.Enabled = this.action == "view" ? true : false;
        }

        private void btn_Impact_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            PCImpactCheck.ListForm f = new Book.UI.produceManager.PCImpactCheck.ListForm(true);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_Impact.EditValue = f.PCImpactCheckId;
            }

        }

        private void btn_Impact_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.btn_Impact.Text))
            {
                PCImpactCheck.EditForm f = new Book.UI.produceManager.PCImpactCheck.EditForm(this.btn_Impact.Text);
                f.ShowDialog(this);
            }
        }

        private void btn_Fog_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            PCFogCheck.ListForm f = new Book.UI.produceManager.PCFogCheck.ListForm(true);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_Fog.EditValue = f.PCFogCheckId;
            }
        }

        private void btn_Fog_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.btn_Fog.Text))
            {
                PCFogCheck.EditForm f = new Book.UI.produceManager.PCFogCheck.EditForm(this.btn_Fog.Text);
                f.ShowDialog(this);
            }
        }

        private void btn_FlameRetardant_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            PCFlameRetardant.ListForm f = new Book.UI.produceManager.PCFlameRetardant.ListForm(true);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btn_FlameRetardant.EditValue = f.PCFlameRetardantId;
            }
        }

        private void btn_FlameRetardant_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.btn_FlameRetardant.Text))
            {
                PCFlameRetardant.EditForm f = new Book.UI.produceManager.PCFlameRetardant.EditForm(this.btn_FlameRetardant.Text);
                f.ShowDialog(this);
            }
        }
    }
}