using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCBoxFootCheck
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.PCBoxFootCheck _pcBoxFootCheck;
        BL.PCBoxFootCheckManager _pcBoxFootCheckManager = new Book.BL.PCBoxFootCheckManager();
        BL.PCBoxFootCheckDetailManager _detailManager = new Book.BL.PCBoxFootCheckDetailManager();
        public EditForm()
        {
            InitializeComponent();

            this.newChooseEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();

            this.bindingSourceBusinessHours.DataSource = new BL.BusinessHoursManager().SelectIdAndName();
            this.action = "view";

            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "√";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "×";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "△";
            dt.Rows.Add(dr);

            this.repositoryItemLookUpEdit1.DataSource = dt;

            IList<Model.ProductUnit> unitList = new BL.ProductUnitManager().Select();
            this.cobUnit.Properties.Items.Clear();
            foreach (var item in unitList)
            {
                this.cobUnit.Properties.Items.Add(item.CnName);
            }
        }

        int LastFlag = 0; //页面载 入时是否执行 last方法
        public EditForm(string PCBoxFootCheckId)
            : this()
        {
            this._pcBoxFootCheck = this._pcBoxFootCheckManager.Get(PCBoxFootCheckId);
            if (this._pcBoxFootCheck == null)
                throw new ArithmeticException("PCBoxFootCheckId");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCBoxFootCheck PCBoxFootCheck)
            : this()
        {
            if (PCBoxFootCheck == null)
                throw new ArithmeticException("PCBoxFootCheckId");
            this._pcBoxFootCheck = PCBoxFootCheck;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pcBoxFootCheck = new Book.Model.PCBoxFootCheck();
            this._pcBoxFootCheck.PCBoxFootCheckId = this._pcBoxFootCheckManager.GetId();
            this._pcBoxFootCheckManager.TiGuiExists(this._pcBoxFootCheck);
            this._pcBoxFootCheck.CheckNum = 1;   //检测数量默认为一
            this._pcBoxFootCheck.CheckDate = DateTime.Now;
            this._pcBoxFootCheck.ProductUnit = "PCS";
            this._pcBoxFootCheck.CheckNum = 6;
            //this._pcBoxFootCheck.Employee = BL.V.ActiveOperator.Employee;
            //if (this._pcBoxFootCheck.Employee != null)
            //    this._pcBoxFootCheck.EmployeeId = this._pcBoxFootCheck.Employee.EmployeeId;

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return this._pcBoxFootCheckManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._pcBoxFootCheckManager.HasRowsAfter(this._pcBoxFootCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this._pcBoxFootCheckManager.HasRowsBefore(this._pcBoxFootCheck);
        }

        protected override void MoveFirst()
        {
            this._pcBoxFootCheck = this._pcBoxFootCheckManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._pcBoxFootCheck = this._pcBoxFootCheckManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.PCBoxFootCheck pcBoxFootCheck = this._pcBoxFootCheckManager.GetNext(this._pcBoxFootCheck);
            if (pcBoxFootCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcBoxFootCheck = pcBoxFootCheck;
        }

        protected override void MovePrev()
        {
            Model.PCBoxFootCheck pcBoxFootCheck = this._pcBoxFootCheckManager.GetPrev(this._pcBoxFootCheck);
            if (pcBoxFootCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcBoxFootCheck = pcBoxFootCheck;
        }

        public override void Refresh()
        {
            if (this._pcBoxFootCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._pcBoxFootCheck = this._pcBoxFootCheckManager.Get(this._pcBoxFootCheck.PCBoxFootCheckId);
            }

            this.txt_Id.EditValue = this._pcBoxFootCheck.PCBoxFootCheckId;
            this.date_Check.EditValue = this._pcBoxFootCheck.CheckDate;
            this.newChooseEmployee.EditValue = this._pcBoxFootCheck.Employee;
            this.txt_Product.EditValue = this._pcBoxFootCheck.Product == null ? null : this._pcBoxFootCheck.Product.ToString();
          
            if (this._pcBoxFootCheck.InvoiceXO != null)
            {
                this.txt_InvoiceXO.EditValue = this._pcBoxFootCheck.InvoiceXO.CustomerInvoiceXOId;
            }
            else if (this._pcBoxFootCheck.PronoteHeader != null)
            {
                this.txt_InvoiceXO.EditValue = this._pcBoxFootCheck.PronoteHeader.InvoiceXO == null ? "" : this._pcBoxFootCheck.PronoteHeader.InvoiceXO.CustomerInvoiceXOId;
            }
            this.txt_PronoteHeader.EditValue = this._pcBoxFootCheck.PronoteHeaderId;
            this.richTextNote.Rtf = this._pcBoxFootCheck.Note;
            this.spinEditGetNum.EditValue = this._pcBoxFootCheck.GetNum;
            this.spinEditCheckNum.EditValue = this._pcBoxFootCheck.CheckNum;
            this.spinEditPassNum.EditValue = this._pcBoxFootCheck.PassNum;
            this.spinEditNoPassNum.EditValue = this._pcBoxFootCheck.NoPassNum;
            this.newChooseContorlAuditEmp.EditValue = this._pcBoxFootCheck.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pcBoxFootCheck.AuditState);
            this.cobUnit.EditValue = this._pcBoxFootCheck.ProductUnit;
            this._pcBoxFootCheck.Details = this._detailManager.SelectByPCBoxFootCheckId(this._pcBoxFootCheck.PCBoxFootCheckId);
            this.bindingSourceDetail.DataSource = this._pcBoxFootCheck.Details;
            //GetRadioGroup();
            base.Refresh();

            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
            }

            this.txt_Id.Properties.ReadOnly = true;
        }

        //private void GetRadioGroup()
        //{
        //    this.radioFlap.EditValue = this._pcBoxFootCheck.Flap == null ? 0 : this._pcBoxFootCheck.Flap;
        //    this.radioExterior.EditValue = this._pcBoxFootCheck.Exterior == null ? 0 : this._pcBoxFootCheck.Exterior;
        //    this.radioOfColor.EditValue = this._pcBoxFootCheck.OfColor == null ? 0 : this._pcBoxFootCheck.OfColor; ;
        //    this.radioFootElasticL.EditValue = this._pcBoxFootCheck.FootElasticL == null ? 0 : this._pcBoxFootCheck.FootElasticL;
        //    this.radioFootElasticR.EditValue = this._pcBoxFootCheck.FootElasticR == null ? 0 : this._pcBoxFootCheck.FootElasticR;
        //    this.radioHeightFootL.EditValue = this._pcBoxFootCheck.HeightFootL == null ? 0 : this._pcBoxFootCheck.HeightFootL;
        //    this.radioHeightFootR.EditValue = this._pcBoxFootCheck.HeightFootR == null ? 0 : this._pcBoxFootCheck.HeightFootR;
        //    this.radioImpactTest.EditValue = this._pcBoxFootCheck.ImpactTest == null ? 0 : this._pcBoxFootCheck.ImpactTest;
        //    this.radioAceticacidTest.EditValue = this._pcBoxFootCheck.AceticacidTest == null ? 0 : this._pcBoxFootCheck.AceticacidTest;
        //}

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            this._pcBoxFootCheck.CheckDate = this.date_Check.EditValue == null ? DateTime.Now : this.date_Check.DateTime;
            this._pcBoxFootCheck.EmployeeId = this.newChooseEmployee.EditValue == null ? null : (this.newChooseEmployee.EditValue as Model.Employee).EmployeeId;
            //this._pcBoxFootCheck.Flap = Convert.ToInt32(this.radioFlap.EditValue.ToString());
            //this._pcBoxFootCheck.Exterior = Convert.ToInt32(this.radioExterior.EditValue.ToString());
            //this._pcBoxFootCheck.OfColor = Convert.ToInt32(this.radioOfColor.EditValue.ToString());
            //this._pcBoxFootCheck.FootElasticL = Convert.ToInt32(this.radioFootElasticL.EditValue.ToString());
            //this._pcBoxFootCheck.FootElasticR = Convert.ToInt32(this.radioFootElasticR.EditValue.ToString());
            //this._pcBoxFootCheck.HeightFootL = Convert.ToInt32(this.radioHeightFootL.EditValue.ToString());
            //this._pcBoxFootCheck.HeightFootR = Convert.ToInt32(this.radioHeightFootR.EditValue.ToString());
            //this._pcBoxFootCheck.ImpactTest = Convert.ToInt32(this.radioImpactTest.EditValue.ToString());
            //this._pcBoxFootCheck.AceticacidTest = Convert.ToInt32(this.radioAceticacidTest.EditValue.ToString());
            this._pcBoxFootCheck.Note = this.richTextNote.Rtf;
            this._pcBoxFootCheck.GetNum = Convert.ToDouble(this.spinEditGetNum.EditValue == null ? null : this.spinEditGetNum.EditValue.ToString());
            this._pcBoxFootCheck.CheckNum = Convert.ToDouble(this.spinEditCheckNum.EditValue == null ? null : this.spinEditCheckNum.EditValue.ToString());
            this._pcBoxFootCheck.PassNum = Convert.ToDouble(this.spinEditPassNum.EditValue == null ? null : this.spinEditPassNum.EditValue.ToString());
            this._pcBoxFootCheck.NoPassNum = Convert.ToDouble(this.spinEditNoPassNum.EditValue == null ? null : this.spinEditNoPassNum.EditValue.ToString());
            this._pcBoxFootCheck.ProductUnit = this.cobUnit.EditValue == null ? null : this.cobUnit.Text;

            switch (this.action)
            {
                case "insert":
                    this._pcBoxFootCheckManager.Insert(this._pcBoxFootCheck);
                    break;
                case "update":
                    this._pcBoxFootCheckManager.Update(this._pcBoxFootCheck);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this._pcBoxFootCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pcBoxFootCheckManager.Delete(this._pcBoxFootCheck.PCBoxFootCheckId);

            this._pcBoxFootCheck = this._pcBoxFootCheckManager.GetNext(this._pcBoxFootCheck);
            if (this._pcBoxFootCheck == null)
            {
                this._pcBoxFootCheck = this._pcBoxFootCheckManager.GetLast();
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._pcBoxFootCheck);
        }

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._pcBoxFootCheck = f.SelectItem as Model.PCBoxFootCheck;
                this.action = "view";
                this.Refresh();
            }
        }

        private void spinEditPassNum_EditValueChanged(object sender, EventArgs e)
        {
            this.spinEditNoPassNum.EditValue = Convert.ToDouble(this.spinEditCheckNum.EditValue) - Convert.ToDouble(this.spinEditPassNum.EditValue);
        }

        private void spinEditCheckNum_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(this.spinEditPassNum.EditValue) > 0)
            this.spinEditNoPassNum.EditValue = Convert.ToDouble(this.spinEditCheckNum.EditValue) - Convert.ToDouble(this.spinEditPassNum.EditValue);

        }

        private void btn_InvoiceXO_Click(object sender, EventArgs e)
        {
            createProduce.EditForm f = new Book.UI.produceManager.createProduce.EditForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.SelectList != null && f.SelectList.Count > 0)
                {
                    Model.InvoiceXODetail model = f.SelectList[0];
                    this._pcBoxFootCheck.ProductId = model.ProductId;
                    this._pcBoxFootCheck.InvoiceXO = model.Invoice;
                    this._pcBoxFootCheck.InvoiceXOId = model.InvoiceId;
                    this.txt_InvoiceXO.EditValue = model.InvoiceId == null ? null : model.Invoice.CustomerInvoiceXOId;
                    this.txt_Product.EditValue = model.Product == null ? null : model.Product.ToString();
                    //this._pcBoxFootCheck.InvoiceXOId = model.InvoiceId;
                    this.spinEditGetNum.EditValue = model.InvoiceXODetailQuantity;
                }
            }
        }

        private void btn_PronoteHeader_Click(object sender, EventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.SelectItem != null)
                {
                    this._pcBoxFootCheck.PronoteHeaderId = f.SelectItem.PronoteHeaderID;
                    this._pcBoxFootCheck.InvoiceXO = f.SelectItem.InvoiceXO;
                    this._pcBoxFootCheck.InvoiceXOId = f.SelectItem.InvoiceXOId;
                    this.txt_PronoteHeader.EditValue = f.SelectItem.PronoteHeaderID;
                    this.spinEditGetNum.EditValue = f.SelectItem.DetailsSum;
                    this._pcBoxFootCheck.ProductId = f.SelectItem.ProductId;
                    this.txt_Product.EditValue = f.SelectItem.ProductName;
                }
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCBoxFootCheck.PRO_PCBoxFootCheckId;
        }

        protected override int AuditState()
        {
            return this._pcBoxFootCheck.AuditState.HasValue ? this._pcBoxFootCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCBoxFootCheck" + "," + this._pcBoxFootCheck.PCBoxFootCheckId;
        }

        #endregion

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Model.PCBoxFootCheckDetail model = new Book.Model.PCBoxFootCheckDetail();
            model.PCBoxFootCheckDetailId = Guid.NewGuid().ToString();
            model.CheckDate = DateTime.Now;
            this._pcBoxFootCheck.Details.Add(model);
            this.gridControl1.RefreshDataSource();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);
            }
        }
    }
}