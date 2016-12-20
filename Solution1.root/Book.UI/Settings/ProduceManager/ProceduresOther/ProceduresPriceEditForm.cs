using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.ProduceManager.ProceduresOther
{
    public partial class ProceduresPriceEditForm : Settings.BasicData.BaseEditForm
    {
        private BL.ProceduresPriceManager ProceduresPriceManager = new Book.BL.ProceduresPriceManager();
        private Model.ProceduresPrice ProceduresPrice = new Book.Model.ProceduresPrice();
        public ProceduresPriceEditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProceduresPrice .PRO_SupplierId, new AA("請選擇委外商..", this.newChooseContorlSupplierId));
            this.requireValueExceptions.Add(Model.ProceduresPrice.PRO_BomId, new AA("請選擇物料編碼..", this.buttonEditBom));
            this.requireValueExceptions.Add(Model.ProceduresPrice.PRO_ProceduresId, new AA("請選擇工序代碼..", this.buttonEditProcedure));
            this.action = "insert";
            this.newChooseContorlSupplierId.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
        }
           public ProceduresPriceEditForm(Model.ProceduresPrice ProceduresPrice)
            : this()
        {
            this.ProceduresPrice = ProceduresPrice;
            this.action = "update";
        }
           public ProceduresPriceEditForm(Model.ProceduresPrice ProceduresPrice, string action)
            : this()
        {
            this.ProceduresPrice = ProceduresPrice;
            this.action = action;
        }
        protected override void Save()
        {
            this.ProceduresPrice.Supplier=this.newChooseContorlSupplierId.EditValue as Model.Supplier;
            if(this.ProceduresPrice.Supplier!=null)
            {
                this.ProceduresPrice.SupplierId=this.ProceduresPrice.Supplier.SupplierId;
            }
            this.ProceduresPrice.Bom = this.buttonEditBom.EditValue as Model.BomParentPartInfo;
            if (this.ProceduresPrice.Bom != null)
            {
                this.ProceduresPrice.BomId = this.ProceduresPrice.Bom.BomId;
            }
            this.ProceduresPrice.Procedures= new BL.ProceduresManager().GetById(this.buttonEditProcedure.Text);
            if (this.ProceduresPrice.Procedures!= null)
            {
                this.ProceduresPrice.ProceduresId = this.ProceduresPrice.Procedures.ProceduresId;
            }
            this.ProceduresPrice.ProductUnit = this.comboBoxEditUnit.Text;
            this.ProceduresPrice.Description = this.memoEditDescription.Text;
            this.ProceduresPrice.PriceUnit = this.comboBoxEditPriceUnit.Text;
            this.ProceduresPrice.PriceTag = this.comboBoxEditPriceTag.SelectedIndex;
            this.ProceduresPrice.ConversionRate = this.spinEditConversionRate.EditValue == null ? 0 : float.Parse(this.spinEditConversionRate.EditValue.ToString());
            this.ProceduresPrice.ChargePrice = this.spinEditChargePrice.EditValue == null ? 0 :decimal.Parse(this.spinEditChargePrice.EditValue.ToString());
            this.ProceduresPrice.TaxRate=this.spinEditTaxRate.EditValue==null?0:float.Parse(this.spinEditTaxRate.EditValue.ToString());
            this.ProceduresPrice.ObsolescenceDoMode = this.comboBoxEditObsolescenceDoMode.SelectedIndex;
            this.ProceduresPrice.WasteDoMode = this.comboBoxEditWasteDoMode.SelectedIndex;
            this.ProceduresPrice.NotReasonLossDoMode = this.comboBoxEditNotReasonLossDoMode.SelectedIndex;
            this.ProceduresPrice.ObsolescencePrice = this.spinEditObsolescencePrice.EditValue == null ? 0 : decimal.Parse(this.spinEditObsolescencePrice.EditValue.ToString());
            this.ProceduresPrice.WastePrice = this.spinEditWastePrice.EditValue == null ? 0 : decimal.Parse(this.spinEditWastePrice.EditValue.ToString());
            this.ProceduresPrice.NotReasonLossPrice = this.spinEditNotReasonLossPrice.EditValue == null ? 0 : decimal.Parse(this.spinEditNotReasonLossPrice.EditValue.ToString());
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
            {
                this.ProceduresPrice.StartDate = null;
            }
            else
            {
                this.ProceduresPrice.StartDate = this.dateEditStartDate.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
            {
                this.ProceduresPrice.EndDate = null;
            }
            else
            {
                this.ProceduresPrice.EndDate = this.dateEditEndDate.DateTime;
            }
            switch (this.action)
            {
                case "insert":
                    this.ProceduresPriceManager.Insert(this.ProceduresPrice);
                    break;
                case "update":
                    this.ProceduresPriceManager.Update(this.ProceduresPrice);
                    break;
                default:
                    break;
            }
        }
        #region Properties

        public override object EditedItem
        {
            get
            {
                return this.ProceduresPrice;
            }
        }

        #endregion

        #region Virtual Method

        protected override void Delete()
        {
            if (this.ProceduresPrice == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.ProceduresPriceManager.Delete(this.ProceduresPrice.ProceduresPriceId);
            this.ProceduresPrice = this.ProceduresPriceManager.GetNext(this.ProceduresPrice);
            if (this.ProceduresPrice == null)
            {
                this.ProceduresPrice = this.ProceduresPriceManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.ProceduresPrice = new Model.ProceduresPrice();
        }

        protected override void Undo()
        {
        }

        protected override void MovePrev()
        {
            Model.ProceduresPrice ProceduresPrice = this.ProceduresPriceManager.GetPrev(this.ProceduresPrice);
            if (ProceduresPrice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.ProceduresPrice = ProceduresPrice;

        }

        protected override void MoveNext()
        {
            Model.ProceduresPrice ProceduresPrice = this.ProceduresPriceManager.GetNext(this.ProceduresPrice);
            if (ProceduresPrice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.ProceduresPrice = ProceduresPrice;
        }

        protected override void MoveFirst()
        {
            this.ProceduresPrice = this.ProceduresPriceManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.ProceduresPrice == null)
                this.ProceduresPrice = this.ProceduresPriceManager.GetLast();
        }
        public override void Refresh()
        {

            if (this.ProceduresPrice == null)
            {
                this.ProceduresPrice = new Book.Model.ProceduresPrice();
                this.action = "insert";
            }
            this.newChooseContorlSupplierId.EditValue = this.ProceduresPrice.Supplier;
            //this.newChooseContorlPriceUnit.EditValue=
            if (this.ProceduresPrice.Bom != null)
            {
                this.buttonEditBom.EditValue = this.ProceduresPrice.Bom;
                if (this.ProceduresPrice.Bom.Product != null)
                {
                    this.textEditBom.Text = this.ProceduresPrice.Bom.Product.ProductName;
                    this.textEditBomCode.Text = this.ProceduresPrice.Bom.Product.Id;
                    this.textEdit4.Text = this.ProceduresPrice.Bom.Product.ProductSpecification;
                }
            }
            if (this.ProceduresPrice.Procedures != null)
            {
                this.buttonEditProcedure.Text = this.ProceduresPrice.Procedures.Id;
                this.richTextBox1.Rtf = this.ProceduresPrice.Procedures.Procedurename;
            } 
            this.comboBoxEditUnit.EditValue = this.ProceduresPrice.ProductUnit;
            this.comboBoxEditPriceUnit.EditValue=this.ProceduresPrice.PriceUnit;
          this.spinEditConversionRate.EditValue =   this.ProceduresPrice.ConversionRate ;
            this.spinEditChargePrice.EditValue =  this.ProceduresPrice.ChargePrice;
            this.spinEditTaxRate.EditValue = this.ProceduresPrice.TaxRate ;
            this.comboBoxEditPriceTag.SelectedIndex = this.ProceduresPrice.PriceTag == null ? -1 : this.ProceduresPrice.PriceTag.Value;
            this.comboBoxEditObsolescenceDoMode.SelectedIndex = this.ProceduresPrice.ObsolescenceDoMode==null?-1:this.ProceduresPrice.ObsolescenceDoMode.Value;
            this.comboBoxEditWasteDoMode.SelectedIndex = this.ProceduresPrice.WasteDoMode==null?-1:this.ProceduresPrice.WasteDoMode.Value;
            this.comboBoxEditNotReasonLossDoMode.SelectedIndex = this.ProceduresPrice.NotReasonLossDoMode==null?-1:this.ProceduresPrice.NotReasonLossDoMode.Value;
           this.spinEditObsolescencePrice.EditValue =  this.ProceduresPrice.ObsolescencePrice ;
             this.spinEditWastePrice.EditValue =this.ProceduresPrice.WastePrice ;
            this.spinEditNotReasonLossPrice.EditValue = this.ProceduresPrice.NotReasonLossPrice;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.ProceduresPrice.StartDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditStartDate.EditValue = null;
            }
            else
            {
                this.dateEditStartDate.EditValue = this.ProceduresPrice.StartDate;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.ProceduresPrice.EndDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditEndDate.EditValue = null;
            }
            else
            {
                this.dateEditEndDate.EditValue = this.ProceduresPrice.EndDate;
            }
            this.memoEditDescription.Text=this.ProceduresPrice.Description;
            switch (this.action)
            {
                case "insert":
                    
                    break;

                case "update":

                    break;

                case "view":

                   
                    break;

                default:
                    break;
            }
            base.Refresh();
        }

        protected override bool HasRows()
        {
            return this.ProceduresPriceManager.HasRows();
        }

        protected override bool HasRowsPrev()
        {
            return this.ProceduresPriceManager.HasRowsBefore(this.ProceduresPrice);
        }

        protected override bool HasRowsNext()
        {
            return this.ProceduresPriceManager.HasRowsAfter(this.ProceduresPrice);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.newChooseContorlSupplierId,this.textEditBom });

        }
        #endregion

        private void buttonEditBom_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Settings.ProduceManager.ChooseBomParentPartInfoForm f = new ChooseBomParentPartInfoForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.BomParentPartInfo bom = f.SelectedItem as Model.BomParentPartInfo;
                if (bom != null)
                {
                    this.buttonEditBom.EditValue = bom;
                    if (bom.Product != null)
                    {
                        this.textEditBomCode.Text = bom.Product.Id;
                        this.textEditBom.Text = bom.Product.ProductName;
                        this.textEdit4.Text = bom.Product.ProductSpecification;
                      
                        this.comboBoxEditPriceUnit.Properties.Items.Clear();

                        if (!string.IsNullOrEmpty(bom.Product.BasedUnitGroupId))
                        {
                            BL.ProductUnitManager unitManager = new Book.BL.ProductUnitManager();
                            IList<Model.ProductUnit> unitList = unitManager.Select(bom.Product.BasedUnitGroup);
                            foreach (Model.ProductUnit item in unitList)
                            {
                                this.comboBoxEditPriceUnit.Properties.Items.Add(item.CnName);
                                this.comboBoxEditUnit.Properties.Items.Add(item.CnName);
                            }
                        }
                        this.comboBoxEditUnit.EditValue =bom.Product.ProduceUnit==null?null: bom.Product.ProduceUnit.CnName;
                        this.comboBoxEditPriceUnit.EditValue = bom.Product.ProduceUnit == null ? null : bom.Product.ProduceUnit.CnName;
                        this.spinEditConversionRate.Value = 1;


                    }
                }
            }
            f.Dispose();
            System.GC.Collect();
        }

        private void buttonEditProcedure_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Settings.ProduceManager.Techonlogy.ChooseProceduresForm f = new Techonlogy.ChooseProceduresForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Procedures procedure = f.SelectedItem as Model.Procedures;
                if (procedure != null)
                {
                    this.buttonEditProcedure.EditValue = procedure.Id;
                    this.richTextBox1.Rtf = procedure.Procedurename;
                }
            }
            f.Dispose();
            System.GC.Collect();
        }
    }
}