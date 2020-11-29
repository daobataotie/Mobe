using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.produceManager.PCPGOnlineCheck;
using System.Linq;

namespace Book.UI.produceManager.PCFirstOnlineCheck
{
    public partial class Editform_BackUp : Settings.BasicData.BaseEditForm
    {
        BL.PCFirstOnlineCheckManager _PCFirstOnlineCheckManager = new Book.BL.PCFirstOnlineCheckManager();
        BL.PCFirstOnlineCheckDetailManager _detailManager = new Book.BL.PCFirstOnlineCheckDetailManager();
        Model.PCFirstOnlineCheck _PCFirstOnlineCheck;
        int LastFlag = 0;

        public Editform_BackUp()
        {
            InitializeComponent();

            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.requireValueExceptions.Add(Model.PCFirstOnlineCheck.PRO_OnlineDate, new AA(Properties.Resources.OnlineDateNotNull, this.date_Online));

            this.bindingSourceBusinessHours.DataSource = new BL.BusinessHoursManager().SelectIdAndName();
            this.bindingSourceEmployee.DataSource = new BL.EmployeeManager().SelectOnActive();

            #region LookUp Data
            DataTable dt = new DataTable();
            DataColumn dc;
            dc = new DataColumn("Name", typeof(string));
            dt.Columns.Add(dc);
            DataRow dr;
            dr = dt.NewRow();
            dr[0] = " ";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "√";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "×";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "△";
            dt.Rows.Add(dr);

            this.repositoryItemLookUpEdit4.DataSource = dt;
            this.repositoryItemLookUpEdit5.DataSource = dt;
            this.repositoryItemLookUpEdit6.DataSource = dt;
            this.repositoryItemLookUpEdit7.DataSource = dt;
            this.repositoryItemLookUpEdit8.DataSource = dt;
            this.repositoryItemLookUpEdit9.DataSource = dt;


            #endregion

            this.action = "view";

            IList<Model.ProductUnit> unitList = new BL.ProductUnitManager().Select();
            this.cobProductUnit.Properties.Items.Clear();
            foreach (var item in unitList)
            {
                this.cobProductUnit.Properties.Items.Add(item.CnName);
            }
        }

        public Editform_BackUp(string invoiceId)
            : this()
        {
            this._PCFirstOnlineCheck = this._PCFirstOnlineCheckManager.Get(invoiceId);
            if (this._PCFirstOnlineCheck == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public Editform_BackUp(Model.PCFirstOnlineCheck PCFirstOnlineCheck)
            : this()
        {
            if (PCFirstOnlineCheck == null)
                throw new ArithmeticException("invoiceid");
            this._PCFirstOnlineCheck = PCFirstOnlineCheck;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public Editform_BackUp(Model.PCFirstOnlineCheck PCFirstOnlineCheck, string action)
            : this()
        {
            this._PCFirstOnlineCheck = PCFirstOnlineCheck;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override bool HasRows()
        {
            return this._PCFirstOnlineCheckManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._PCFirstOnlineCheckManager.HasRowsAfter(this._PCFirstOnlineCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this._PCFirstOnlineCheckManager.HasRowsBefore(this._PCFirstOnlineCheck);
        }

        protected override void MoveFirst()
        {
            this._PCFirstOnlineCheck = this._PCFirstOnlineCheckManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0; return;
            }
            this._PCFirstOnlineCheck = this._PCFirstOnlineCheckManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.PCFirstOnlineCheck model = this._PCFirstOnlineCheckManager.GetNext(this._PCFirstOnlineCheck);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCFirstOnlineCheck = model;
        }

        protected override void MovePrev()
        {
            Model.PCFirstOnlineCheck model = this._PCFirstOnlineCheckManager.GetPrev(this._PCFirstOnlineCheck);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCFirstOnlineCheck = model;
        }

        protected override void Delete()
        {
            if (this._PCFirstOnlineCheck == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.PCFirstOnlineCheck model = this._PCFirstOnlineCheckManager.GetNext(this._PCFirstOnlineCheck);
                this._PCFirstOnlineCheckManager.Delete(this._PCFirstOnlineCheck.PCFirstOnlineCheckId);
                if (model == null)
                    this._PCFirstOnlineCheck = this._PCFirstOnlineCheckManager.GetLast();
                else
                    this._PCFirstOnlineCheck = model;
            }
        }

        protected override void AddNew()
        {
            this._PCFirstOnlineCheck = new Book.Model.PCFirstOnlineCheck();
            this._PCFirstOnlineCheck.PCFirstOnlineCheckId = this._PCFirstOnlineCheckManager.GetId();
            this._PCFirstOnlineCheckManager.TiGuiExists(this._PCFirstOnlineCheck);
            this._PCFirstOnlineCheck.OnlineDate = DateTime.Now;

            this.action = "insert";
        }

        public override void Refresh()
        {
            if (this._PCFirstOnlineCheck == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._PCFirstOnlineCheck = this._PCFirstOnlineCheckManager.GetDetail(this._PCFirstOnlineCheck.PCFirstOnlineCheckId);
            }
            this.txt_Id.EditValue = this._PCFirstOnlineCheck.PCFirstOnlineCheckId;
            //this.date_PCFirstOnlineCheck.EditValue = this._PCFirstOnlineCheck.PCFirstOnlineCheckDate;
            this.date_Online.EditValue = this._PCFirstOnlineCheck.OnlineDate;

            this.txt_PronoteHeaderId.EditValue = this._PCFirstOnlineCheck.PronoteHeaderId;

            this.newChooseContorlAuditEmp.EditValue = this._PCFirstOnlineCheck.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCFirstOnlineCheck.AuditState);

            this.spinEditCheckNum.EditValue = this._PCFirstOnlineCheck.CheckNum;
            this.spinEditPassNum.EditValue = this._PCFirstOnlineCheck.PassNum;
            this.cobProductUnit.EditValue = this._PCFirstOnlineCheck.ProductUnit;

            this.txt_CustomerProduct.EditValue = this._PCFirstOnlineCheck.PronoteHeader == null ? "" : this._PCFirstOnlineCheck.PronoteHeader.Product.CustomerProductName;
            this.txt_CheckedStandard.EditValue = this._PCFirstOnlineCheck.CheckedStandard;

            this.bindingSourceDetail.DataSource = this._PCFirstOnlineCheck.Detail;

            this.gridControl1.RefreshDataSource();
            base.Refresh();

            switch (this.action)
            {
                case "view":
                    //this.gridView1.OptionsBehavior.Editable = false;
                    this.gridColumn1.OptionsColumn.AllowEdit = false;
                    this.gridColumn2.OptionsColumn.AllowEdit = false;
                    this.gridColumn3.OptionsColumn.AllowEdit = false;
                    this.gridColumn4.OptionsColumn.AllowEdit = false;
                    this.gridColumn5.OptionsColumn.AllowEdit = false;
                    this.gridColumn6.OptionsColumn.AllowEdit = false;
                    this.gridColumn7.OptionsColumn.AllowEdit = false;
                    this.gridColumn8.OptionsColumn.AllowEdit = false;
                    this.gridColumn9.OptionsColumn.AllowEdit = true;
                    this.gridColumn10.OptionsColumn.AllowEdit = true;
                    this.gridColumn11.OptionsColumn.AllowEdit = true;
                    break;
                default:
                    //this.gridView1.OptionsBehavior.Editable = true;
                    this.gridColumn1.OptionsColumn.AllowEdit = true;
                    this.gridColumn2.OptionsColumn.AllowEdit = false;
                    this.gridColumn3.OptionsColumn.AllowEdit = true;
                    this.gridColumn4.OptionsColumn.AllowEdit = false;
                    this.gridColumn5.OptionsColumn.AllowEdit = true;
                    this.gridColumn6.OptionsColumn.AllowEdit = true;
                    this.gridColumn7.OptionsColumn.AllowEdit = true;
                    this.gridColumn8.OptionsColumn.AllowEdit = true;
                    this.gridColumn9.OptionsColumn.AllowEdit = true;
                    this.gridColumn10.OptionsColumn.AllowEdit = true;
                    this.gridColumn11.OptionsColumn.AllowEdit = true;
                    break;
            }

            this.txt_Id.Enabled = true;
            this.txt_Id.Properties.ReadOnly = true;
            this.txt_CustomerProduct.Properties.ReadOnly = true;
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            if (this.date_Online.EditValue != null)
                this._PCFirstOnlineCheck.OnlineDate = this.date_Online.DateTime;

            this._PCFirstOnlineCheck.PronoteHeaderId = this.txt_PronoteHeaderId.EditValue == null ? null : this.txt_PronoteHeaderId.EditValue.ToString();
            this._PCFirstOnlineCheck.CheckNum = this.spinEditCheckNum.Value;
            this._PCFirstOnlineCheck.PassNum = this.spinEditPassNum.Value;
            this._PCFirstOnlineCheck.ProductUnit = this.cobProductUnit.EditValue == null ? null : this.cobProductUnit.Text;

            this._PCFirstOnlineCheck.CheckedStandard = this.txt_CheckedStandard.Text;

            switch (this.action)
            {
                case "insert":
                    this._PCFirstOnlineCheckManager.Insert(this._PCFirstOnlineCheck);
                    break;
                case "update":
                    this._PCFirstOnlineCheckManager.Update(this._PCFirstOnlineCheck);
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            //return new RO(this._PCFirstOnlineCheck);

            //if (this._PCFirstOnlineCheck.Detail == null || !this._PCFirstOnlineCheck.Detail.Any(d => d.Guangxue == "△" || d.Houdu == "△" || d.Chongji == "△"))
            //{
            //    //首次上线检查表打印，附带光学厚度冲击测试
            //    RO ro = new RO(this._PCFirstOnlineCheck);
            //    ro.ShowPreviewDialog();
            //}
            //else
            //{
            //首次上线检查表打印，不带光学厚度冲击测试
            //RO2 ro = new RO2(this._PCFirstOnlineCheck);
            //ro.ShowPreviewDialog();
            //}
            //return null;

            return new RO(this._PCFirstOnlineCheck);
        }


        private void barPronoteHeader_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                //if (f.SelectItem != null)
                //{
                //    this.txt_PronoteHeaderId.EditValue = f.SelectItem.PronoteHeaderID;


                //    Model.PCFirstOnlineCheckDetail model = new Book.Model.PCFirstOnlineCheckDetail();
                //    model.PCFirstOnlineCheckDetailId = Guid.NewGuid().ToString();
                //    model.PCFirstOnlineCheckId = this._PCFirstOnlineCheck.PCFirstOnlineCheckId;
                //    model.InvoiceXOId = f.SelectItem.InvoiceXOId;
                //    model.InvoiceXOCusId = f.SelectItem.InvoiceCusId;
                //    model.ProductName = f.SelectItem.ProductName;
                //    model.ProductId = f.SelectItem.ProductId;
                //    model.Employee = BL.V.ActiveOperator.Employee;
                //    model.EmployeeId = BL.V.ActiveOperator.EmployeeId;

                //    model.CheckDate = DateTime.Now;

                //    this.bindingSourceDetail.Add(model);
                //    this.gridControl1.RefreshDataSource();
                //}

                if (f.SelectItems != null && f.SelectItems.Count > 0)
                {
                    foreach (var item in f.SelectItems)
                    {
                        this.txt_PronoteHeaderId.EditValue = item.PronoteHeaderID;
                        this.txt_CustomerProduct.EditValue = item.Product == null ? "" : item.Product.CustomerProductName;
                        this.txt_CheckedStandard.EditValue = item.CustomerCheckStandard;

                        Model.PCFirstOnlineCheckDetail model = new Book.Model.PCFirstOnlineCheckDetail();
                        model.PCFirstOnlineCheckDetailId = Guid.NewGuid().ToString();
                        model.PCFirstOnlineCheckId = this._PCFirstOnlineCheck.PCFirstOnlineCheckId;
                        model.InvoiceXOId = item.InvoiceXOId;
                        model.InvoiceXOCusId = item.InvoiceCusId;
                        model.ProductName = item.ProductName;
                        model.ProductId = item.ProductId;
                        model.Employee = BL.V.ActiveOperator.Employee;
                        model.EmployeeId = BL.V.ActiveOperator.EmployeeId;

                        model.CheckDate = DateTime.Now;

                        this.bindingSourceDetail.Add(model);
                        this.gridControl1.RefreshDataSource();
                    }
                }

            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Model.PCFirstOnlineCheckDetail model = new Book.Model.PCFirstOnlineCheckDetail();
            model.PCFirstOnlineCheckDetailId = Guid.NewGuid().ToString();
            model.PCFirstOnlineCheckId = this._PCFirstOnlineCheck.PCFirstOnlineCheckId;
            model.CheckDate = DateTime.Now;

            this.bindingSourceDetail.Add(model);
            this.gridControl1.RefreshDataSource();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
                this.bindingSourceDetail.Remove(this.bindingSourceDetail.Current);
            this.gridControl1.RefreshDataSource();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List f = new List();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCFirstOnlineCheck model = f.SelectItem as Model.PCFirstOnlineCheck;
                if (model != null)
                {
                    this._PCFirstOnlineCheck = model;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCFirstOnlineCheck.PRO_PCFirstOnlineCheckId;
        }

        protected override int AuditState()
        {
            return this._PCFirstOnlineCheck.AuditState.HasValue ? this._PCFirstOnlineCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCFirstOnlineCheck" + "," + this._PCFirstOnlineCheck.PCFirstOnlineCheckId;
        }

        #endregion

        //光学
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            Model.PCFirstOnlineCheckDetail d = (this.bindingSourceDetail.Current as Model.PCFirstOnlineCheckDetail);
            if (d != null)
            {
                OpticsTest f = new OpticsTest(d.PCFirstOnlineCheckDetailId, 2);
                f.ShowDialog();
            }
        }


        //厚度
        private void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            Model.PCFirstOnlineCheckDetail d = (this.bindingSourceDetail.Current as Model.PCFirstOnlineCheckDetail);
            if (d != null)
            {
                ThicknessTest f = new ThicknessTest(d.PCFirstOnlineCheckDetailId, 1);
                f.ShowDialog();
            }
        }


        //冲击
        private void repositoryItemHyperLinkEdit3_Click(object sender, EventArgs e)
        {
            Model.PCFirstOnlineCheckDetail d = (this.bindingSourceDetail.Current as Model.PCFirstOnlineCheckDetail);
            if (d != null)
            {
                PCImpactCheck.EditForm f = new Book.UI.produceManager.PCImpactCheck.EditForm(d.PCFirstOnlineCheckDetailId, 1);
                f.ShowDialog();
            }
        }
    }
}