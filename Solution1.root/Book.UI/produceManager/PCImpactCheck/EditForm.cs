using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.ProduceManager.Workhouselog;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;

namespace Book.UI.produceManager.PCImpactCheck
{

    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        Model.PCImpactCheck _PCIC = null;
        BL.PCImpactCheckManager _PCICManager = new Book.BL.PCImpactCheckManager();
        private BL.ProduceOtherCompactDetailManager OtherCompactDetailManager = new Book.BL.ProduceOtherCompactDetailManager();
        private string _PCFirstOnlineCheckDetailId;

        /// <summary>
        /// 0，本身；1，首件上线检查表
        /// </summary>
        int sourceInvoice = 0;

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.PCImpactCheck.PRO_PCImpactCheckId, new AA(Properties.Resources.NumsIsNotNull, this.txtPCImpactCheckId));
            this.requireValueExceptions.Add(Model.PCImpactCheck.PRO_PCImpactCheckDate, new AA(Properties.Resources.DateIsNull, this.DE_PCImpactCheckDate));
            this.requireValueExceptions.Add(Model.PCImpactCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.BEProduct));
            this.requireValueExceptions.Add(Model.PCImpactCheck.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee0));
            this.action = "view";
            this.nccEmployee0.Choose = new ChooseEmployee();
            this.nccWorkHouse.Choose = new ChooseWorkHouse();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();

            IList<Model.ProductUnit> UnitList = (new BL.ProductUnitManager()).Select();
            this.bindingSourceUnit.DataSource = UnitList;

            foreach (var item in UnitList)
            {
                this.cob_MaterialUnit.Properties.Items.Add(item.CnName);
            }

            #region LookUpEditor
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("id", typeof(string));
            DataColumn dc1 = new DataColumn("name", typeof(string));
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1);
            DataRow dr;
            dr = dt.NewRow();
            dr[0] = string.Empty;
            dr[1] = "  ";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "○";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "△";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = "X";
            dt.Rows.Add(dr);
            for (int i = 0; i < this.gridView1.Columns.Count; i++)
            {
                if (this.gridView1.Columns[i].ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
                {
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DataSource = dt;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.Clear();
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name",25, "标识"),
                     });
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).NullText = "";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DisplayMember = "name";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).ValueMember = "id";
                }
            }
            #endregion

            //List<string> listZaiceshi = new List<string>() { "再测试1：AS加热 60℃±2 30 分钟", "再测试2：EN 加热 55℃±2 60 分钟 & 冰冻 -5±2℃ 1 小时" };
            //this.repositoryItemCheckedComboBoxEdit1.DataSource = listZaiceshi;

            this.repositoryItemCheckedComboBoxEdit1.Items.Add("再测试1：AS加热 60℃±2 30 分钟");
            this.repositoryItemCheckedComboBoxEdit1.Items.Add("再测试2：EN 加热 55℃±2 60 分钟 & 冰冻 -5±2℃ 1 小时");

        }

        int LastFlag = 0;
        public EditForm(string invoiceId)
            : this()
        {
            this._PCIC = this._PCICManager.Get(invoiceId);
            if (this._PCIC == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCImpactCheck mPCIC)
            : this()
        {
            if (mPCIC == null)
                throw new ArithmeticException("invoiceid");
            this._PCIC = mPCIC;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCImpactCheck mPCIC, string action)
            : this()
        {
            this._PCIC = mPCIC;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(string PCFirstOnlineCheckDetailId, int i)
            : this()
        {
            this._PCFirstOnlineCheckDetailId = PCFirstOnlineCheckDetailId;
            sourceInvoice = i;
        }

        protected override void AddNew()
        {
            this._PCIC = new Book.Model.PCImpactCheck();
            this._PCIC.PCImpactCheckId = this._PCICManager.GetId();
            this._PCIC.PCImpactCheckDate = DateTime.Now.Date;
            this._PCIC.PCFromType = -1;
            this._PCIC.PCImpactCheckQuantity = 6;       //默认抽检数量为6
            this._PCIC.ProductUnitId = "f7f95879-3444-494b-92eb-2aa784c52e8c";

            //this._PCIC.Employee = BL.V.ActiveOperator.Employee;
            //this._PCIC.EmployeeId = BL.V.ActiveOperator.EmployeeId;

            //初始化一条详细
            this._PCIC.Details = new List<Model.PCImpactCheckDetail>();
            this.AddDataRows();
        }

        protected override void Delete()
        {
            if (this._PCIC == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._PCICManager.Delete(this._PCIC);
            this._PCIC = this._PCICManager.GetNext(this._PCIC);
            if (this._PCIC == null)
            {
                this._PCIC = this._PCICManager.GetLast();
            }
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._PCIC = this._PCICManager.Get(this._PCICManager.GetLast() == null ? "" : this._PCICManager.GetLast().PCImpactCheckId);
        }

        protected override void MoveFirst()
        {
            this._PCIC = this._PCICManager.Get(this._PCICManager.GetFirst() == null ? "" : this._PCICManager.GetFirst().PCImpactCheckId);
        }

        protected override void MovePrev()
        {
            Model.PCImpactCheck pcic = this._PCICManager.GetPrev(this._PCIC);
            if (pcic == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCIC = this._PCICManager.Get(pcic.PCImpactCheckId);
        }

        protected override void MoveNext()
        {
            Model.PCImpactCheck pcic = this._PCICManager.GetNext(this._PCIC);
            if (pcic == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._PCIC = this._PCICManager.Get(pcic.PCImpactCheckId);
        }

        protected override bool HasRows()
        {
            return this._PCICManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._PCICManager.HasRowsAfter(this._PCIC);
        }

        protected override bool HasRowsPrev()
        {
            return this._PCICManager.HasRowsBefore(this._PCIC);
        }

        protected override void Save()
        {
            this._PCIC.PCImpactCheckId = this.txtPCImpactCheckId.Text;
            this._PCIC.PCImpactCheckDesc = this.txtPCImpactCheckDesc.Text;
            this._PCIC.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._PCIC.PronoteHeaderId = this.txtPronoteHeaderId.Text;
            this._PCIC.PCImpactCheckQuantity = Convert.ToInt32(this.CE_PCImpactCheckQuantity.Value);
            this._PCIC.PCImpactCheckDate = this.DE_PCImpactCheckDate.DateTime;
            this._PCIC.Employee = (this.nccEmployee0.EditValue as Model.Employee);
            this._PCIC.mCheckStandard = this.txtCheckStandard.Text;
            if (this._PCIC.Employee != null)
            {
                this._PCIC.EmployeeId = this._PCIC.Employee.EmployeeId;
            }

            this._PCIC.WorkHouse = (this.nccWorkHouse.EditValue as Model.WorkHouse);
            if (this._PCIC.WorkHouse != null)
            {
                this._PCIC.WorkHouseId = this._PCIC.WorkHouse.WorkHouseId;
            }

            this._PCIC.Product = (this.BEProduct.EditValue as Model.Product);
            if (this._PCIC.Product != null)
            {
                this._PCIC.ProductId = this._PCIC.Product.ProductId;
            }

            this._PCIC.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();

            this._PCIC.MaterialUnit = this.cob_MaterialUnit.Text;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._PCICManager.Insert(this._PCIC);
                    break;
                case "update":
                    this._PCICManager.Update(this._PCIC);
                    break;
            }

        }

        public override void Refresh()
        {
            if (this._PCIC == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._PCIC = this._PCICManager.GetDetail(this._PCIC.PCImpactCheckId);
                }
            }
            this.txtPCImpactCheckId.Text = this._PCIC.PCImpactCheckId;
            this.txtInvoiceCusXOId.Text = this._PCIC.InvoiceCusXOId;
            this.txtPronoteHeaderId.Text = this._PCIC.PronoteHeaderId;
            this.txtPCImpactCheckDesc.Text = this._PCIC.PCImpactCheckDesc;
            this.DE_PCImpactCheckDate.EditValue = this._PCIC.PCImpactCheckDate.Value;
            this.CE_PCImpactCheckQuantity.EditValue = this._PCIC.PCImpactCheckQuantity.HasValue ? this._PCIC.PCImpactCheckQuantity.Value : 0;
            this.BEProduct.EditValue = this._PCIC.Product;
            this.nccEmployee0.EditValue = this._PCIC.Employee;
            this.nccWorkHouse.EditValue = this._PCIC.WorkHouse;
            this.lblDanJuBianHao.Text = this._PCIC.PCFromType > 0 ? "委外單編號:" : "加工單編號:";
            this.txtCheckStandard.Text = this._PCIC.mCheckStandard;
            this.calcInvoiceXOQuantity.EditValue = this._PCIC.InvoiceXOQuantity.HasValue ? this._PCIC.InvoiceXOQuantity.Value : 0;

            this.newChooseContorlAuditEmp.EditValue = this._PCIC.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._PCIC.AuditState);
            this.lookUpEditUnit.EditValue = this._PCIC.ProductUnitId;

            this.cob_MaterialUnit.Text = this._PCIC.MaterialUnit;

            this.bsPCImpactCheckDetail.DataSource = this._PCIC.Details;

            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
            }

            this.calcInvoiceXOQuantity.Enabled = false;
            this.txtPCImpactCheckId.Properties.ReadOnly = true;
            //this.nccEmployee0.Enabled = false;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this._PCIC);
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            this.AddDataRows();
            this.gridControl1.RefreshDataSource();
        }

        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            if (this.bsPCImpactCheckDetail.Current != null)
            {
                this._PCIC.Details.Remove(this.bsPCImpactCheckDetail.Current as Model.PCImpactCheckDetail);
                if (this._PCIC.Details.Count == 0)
                {
                    this.AddDataRows();
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action != "view")
            {
                switch (e.KeyValue)
                {
                    case 13:
                        this.AddDataRows();
                        break;
                    case 46:
                        if (this.bsPCImpactCheckDetail.Current != null)
                        {
                            this._PCIC.Details.Remove(this.bsPCImpactCheckDetail.Current as Model.PCImpactCheckDetail);
                            if (this._PCIC.Details.Count == 0)
                            {
                                this.AddDataRows();
                            }
                        }
                        break;
                    default:
                        break;
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        //添加行
        private void AddDataRows()
        {
            Model.PCImpactCheckDetail pcicDetail = new Book.Model.PCImpactCheckDetail();
            pcicDetail.PCImpactCheckDetailId = Guid.NewGuid().ToString();
            pcicDetail.PCImpactCheckId = this._PCIC.PCImpactCheckId;
            pcicDetail.attrDate = DateTime.Now;
            this._PCIC.Details.Add(pcicDetail);

            this.bsPCImpactCheckDetail.Position = this.bsPCImpactCheckDetail.IndexOf(pcicDetail);
        }

        //选择单据
        private void BarBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCImpactCheck currentModel = form.SelectItem as Model.PCImpactCheck;
                if (currentModel != null)
                {
                    this._PCIC = currentModel;
                    this._PCIC = this._PCICManager.GetDetail(this._PCIC.PCImpactCheckId);
                    this.Refresh();
                }
            }
            form.Dispose();
            GC.Collect();
        }

        //选择加工单据
        private void btnGetPronoteHeader_Click(object sender, EventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm pronoForm = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(null, 0);
            if (pronoForm.ShowDialog(this) == DialogResult.OK)
            {
                Model.PronoteHeader currentModel = pronoForm.SelectItem;
                if (currentModel != null)
                {
                    this._PCIC.WorkHouse = (this.nccWorkHouse.EditValue as Model.WorkHouse);
                    if (this._PCIC.WorkHouse != null)
                    {
                        this._PCIC.WorkHouseId = this._PCIC.WorkHouse.WorkHouseId;
                    }
                    this._PCIC.PCImpactCheckDate = this.DE_PCImpactCheckDate.DateTime;
                    this._PCIC.PronoteHeaderId = currentModel.PronoteHeaderID;
                    this._PCIC.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._PCIC.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._PCIC.ProductId = this._PCIC.Product.ProductId;
                    this._PCIC.mCheckStandard = currentModel.CustomerCheckStandard;
                    this._PCIC.PCFromType = -1;     //单据类型
                    this._PCIC.InvoiceXOQuantity = currentModel.InvoiceXODetailQuantity.HasValue ? currentModel.InvoiceXODetailQuantity.Value : 0;
                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        //选择委外单据
        private void btnGetOtherPacmt_Click(object sender, EventArgs e)
        {
            ProduceOtherCompact.ChooseOutContract f = new Book.UI.produceManager.ProduceOtherCompact.ChooseOutContract();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.ProduceOtherCompact currentModel = f.SelectItem as Model.ProduceOtherCompact;
                if (currentModel != null)
                {
                    this._PCIC.WorkHouse = (this.nccWorkHouse.EditValue as Model.WorkHouse);
                    if (this._PCIC.WorkHouse != null)
                    {
                        this._PCIC.WorkHouseId = this._PCIC.WorkHouse.WorkHouseId;
                    }
                    this._PCIC.PCImpactCheckDate = this.DE_PCImpactCheckDate.DateTime;
                    this._PCIC.PronoteHeaderId = currentModel.PronoteHeaderId;              //加工单据编号
                    this._PCIC.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;           //客户订单编号
                    this._PCIC.mCheckStandard = currentModel.Customer.CheckedStandard;      //质检标准

                    //if (!string.IsNullOrEmpty(currentModel.MRSHeaderId))                  //客户订单编号
                    //{
                    //    Model.MRSHeader mrsHeader = new BL.MRSHeaderManager().Get(currentModel.MRSHeaderId);
                    //    if (mrsHeader != null)
                    //    {
                    //        Model.MPSheader mPSheader = new BL.MPSheaderManager().Get(mrsHeader.MPSheaderId);
                    //        if (mPSheader != null)
                    //        {
                    //            Model.InvoiceXO invoiceXo = new BL.InvoiceXOManager().Get(mPSheader.InvoiceXOId);
                    //            this._PCIC.InvoiceCusXOId = invoiceXo == null ? string.Empty : invoiceXo.CustomerInvoiceXOId;
                    //        }
                    //    }
                    //}
                    if (this.OtherCompactDetailManager.Select(currentModel)[0] != null)
                    {
                        this._PCIC.Product = this.OtherCompactDetailManager.Select(currentModel)[0].Product;
                        this._PCIC.ProductId = this.OtherCompactDetailManager.Select(currentModel)[0].ProductId;
                        this._PCIC.PronoteHeaderId = currentModel.ProduceOtherCompactId;
                        this._PCIC.PCFromType = 1;      //单据类型
                    }
                    this.Refresh();
                }
            }
            f.Dispose();
            GC.Collect();
        }

        private void txtPCImpactCheckDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.txtPCImpactCheckDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCImpactCheck.PRO_PCImpactCheckId;
        }

        protected override int AuditState()
        {
            return this._PCIC.AuditState.HasValue ? this._PCIC.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCImpactCheck" + "," + this._PCIC.PCImpactCheckId;
        }

        #endregion

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        //採購單
        private void btn_InvoiceCO_Click(object sender, EventArgs e)
        {
            Invoices.CG.CGForm form = new Book.UI.Invoices.CG.CGForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.key.Count == 0)
                    return;

                this._PCIC.PCFromType = 2;     //单据类型
                this._PCIC.PCImpactCheckDate = this.DE_PCImpactCheckDate.DateTime;
                this._PCIC.PronoteHeaderId = form.key[0].InvoiceId;
                this._PCIC.InvoiceCusXOId = form.key[0].Invoice.InvoiceCustomXOId;
                this._PCIC.Product = new BL.ProductManager().Get(form.key[0].ProductId);
                this._PCIC.ProductId = this._PCIC.Product.ProductId;
                this._PCIC.mCheckStandard = (form.key[0].Invoice.Customer == null ? "" : form.key[0].Invoice.Customer.CheckedStandard);
                this._PCIC.InvoiceXOQuantity = form.key[0].OrderQuantity;
                this.Refresh();
            }

            form.Dispose();
            GC.Collect();
        }

        private void repositoryItemCheckedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Model.PCImpactCheckDetail model = this.bsPCImpactCheckDetail.Current as Model.PCImpactCheckDetail;
            if (model == null) return;

            string value = "";
            foreach (CheckedListBoxItem item in repositoryItemCheckedComboBoxEdit1.Items)
            {
                if (item.CheckState == CheckState.Checked)
                    value += item.ToString() + ",";
            }

            value = value.TrimEnd(',');

            model.Note = value;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "gridColumn3")
            {
                Model.PCImpactCheckDetail model = this.bsPCImpactCheckDetail.Current as Model.PCImpactCheckDetail;
                if (model == null) return;

                e.DisplayText = model.Note;
            }
        }
    }
}


#region 注释备用
//DataTable dt = new DataTable();
//DataColumn dc = new DataColumn("id", typeof(string));
//DataColumn dc1 = new DataColumn("name", typeof(string));
//dt.Columns.Add(dc);
//dt.Columns.Add(dc1);
//DataRow dr;
//dr = dt.NewRow();
//dr[0] = "0";
//dr[1] = "正确";
//dt.Rows.Add(dr);
//dr = dt.NewRow();
//dr[0] = "1";
//dr[1] = "不正确";
//dt.Rows.Add(dr);
//dr = dt.NewRow();
//dr[0] = "2";
//dr[1] = "不存在";
//dt.Rows.Add(dr);
#endregion