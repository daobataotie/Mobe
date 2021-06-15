using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
using System.Xml;

namespace Book.UI.produceManager.PCDoubleImpactCheck
{
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        Model.PCDoubleImpactCheck _pcdic = null;
        BL.PCDoubleImpactCheckManager _pcdicManage = new Book.BL.PCDoubleImpactCheckManager();
        int pcType = -1;

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.PCDoubleImpactCheck.PRO_PCDoubleImpactCheckID, new AA(Properties.Resources.NumsIsNotNull, this.txtPCDoubleImpactCheckID));
            this.requireValueExceptions.Add(Model.PCDoubleImpactCheck.PRO_PCDoubleImpactCheckDate, new AA(Properties.Resources.DateNotNull, this.DE_PCDoubleImpactCheckDate));
            this.requireValueExceptions.Add(Model.PCDoubleImpactCheck.PRO_ProductId, new AA(Properties.Resources.Noproduct, this.BEProduct));
            this.requireValueExceptions.Add(Model.PCDoubleImpactCheck.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee0));
            this.action = "view";
            this.nccEmployee0.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();

            this.bsLUEemployees.DataSource = new BL.EmployeeManager().SelectOnActive();
        }

        public EditForm(int pcFlag)
            : this()
        {
            this.pcType = pcFlag;
        }

        int LastFlag = 0;
        public EditForm(string invoiceId)
            : this()
        {
            this._pcdic = this._pcdicManage.Get(invoiceId);
            if (this._pcdic == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCDoubleImpactCheck mpcdic)
            : this()
        {
            if (mpcdic == null)
                throw new ArithmeticException("invoiceid");
            this._pcdic = mpcdic;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.PCDoubleImpactCheck mpcdic, string action)
            : this()
        {
            this._pcdic = mpcdic;
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override void AddNew()
        {
            this._pcdic = new Book.Model.PCDoubleImpactCheck();
            this._pcdic.PCDoubleImpactCheckID = this._pcdicManage.GetId();
            this._pcdic.PCDoubleImpactCheckDate = DateTime.Now.Date;
            this._pcdic.PCDoubleImpactCheckType = 0;
            this._pcdic.PCDoubleImpactCheckCount = 6;   //默认抽检数量为1
            this._pcdic.ProductUnitId = "f7f95879-3444-494b-92eb-2aa784c52e8c";
            //this._pcdic.Employee = BL.V.ActiveOperator.Employee;
            //初始化一条详细
            this._pcdic.Detail = new List<Model.PCDoubleImpactCheckDetail>();
            this.AddDataRows();
        }

        protected override void Delete()
        {

            if (this._pcdic == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._pcdicManage.Delete(this._pcdic);

            this._pcdic = this._pcdicManage.GetNext(this._pcdic);
            if (this._pcdic == null)
            {
                //this._pcdic = this._pcdicManage.GetLast(this._pcdic.PCDoubleImpactCheckType.Value);
                this._pcdic = this._pcdicManage.GetLast(this.pcType);
            }
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            this._pcdic = this._pcdicManage.Get(this._pcdicManage.GetLast(this.pcType) == null ? "" : this._pcdicManage.GetLast(this.pcType).PCDoubleImpactCheckID);
        }

        protected override void MoveFirst()
        {
            this._pcdic = this._pcdicManage.Get(this._pcdicManage.GetFirst(this._pcdic.PCDoubleImpactCheckType.HasValue ? this._pcdic.PCDoubleImpactCheckType.Value : -1) == null ? "" : this._pcdicManage.GetFirst(this._pcdic.PCDoubleImpactCheckType.HasValue ? this._pcdic.PCDoubleImpactCheckType.Value : -1).PCDoubleImpactCheckID);
        }

        protected override void MovePrev()
        {
            Model.PCDoubleImpactCheck p = this._pcdicManage.GetPrev(this._pcdic);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcdic = this._pcdicManage.Get(p.PCDoubleImpactCheckID);
        }

        protected override void MoveNext()
        {
            Model.PCDoubleImpactCheck p = this._pcdicManage.GetNext(this._pcdic);
            if (p == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._pcdic = this._pcdicManage.Get(p.PCDoubleImpactCheckID);
        }

        protected override bool HasRows()
        {
            return this._pcdicManage.HasRows(this._pcdic.PCDoubleImpactCheckType.Value);
        }

        protected override bool HasRowsNext()
        {
            return this._pcdicManage.HasRowsAfter(this._pcdic);
        }

        protected override bool HasRowsPrev()
        {
            return this._pcdicManage.HasRowsBefore(this._pcdic);
        }

        public override void Refresh()
        {
            if (this._pcdic == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._pcdic = this._pcdicManage.GetDetail(this._pcdic);
                }
            }
            //初始化控件
            this.txtPCDoubleImpactCheckID.Text = this._pcdic.PCDoubleImpactCheckID;
            this.txtInvoiceCusXOId.Text = this._pcdic.InvoiceCusXOId;
            this.txtPronoteHeaderId.Text = this._pcdic.PronoteHeaderId;
            this.txtPCDoubleImpactCheckDesc.Text = this._pcdic.PCDoubleImpactCheckDesc;
            this.DE_PCDoubleImpactCheckDate.EditValue = this._pcdic.PCDoubleImpactCheckDate.Value;
            this.BEProduct.EditValue = this._pcdic.Product;
            this.nccEmployee0.EditValue = this._pcdic.Employee;
            this.txtCheckedStandard.Text = this._pcdic.CheckStandard;
            this.calcPCDoubleImpactCheckCount.EditValue = this._pcdic.PCDoubleImpactCheckCount.HasValue ? this._pcdic.PCDoubleImpactCheckCount.Value : 0;
            this.ceInvoiceXOCount.EditValue = this._pcdic.InvoiceXOQuantity.HasValue ? this._pcdic.InvoiceXOQuantity.Value : 0;
            this.newChooseContorlAuditEmp.EditValue = this._pcdic.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._pcdic.AuditState);

            this.lookUpEditUnit.EditValue = this._pcdic.ProductUnitId;
            #region LookUpEditor

            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("id", typeof(string));
            DataColumn dc1 = new DataColumn("name", typeof(string));
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1);
            DataRow dr;
            dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "√";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "△";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = "X";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = string.Empty;
            dr[1] = " ";
            dt.Rows.Add(dr);

            for (int i = 0; i < this.gridView1.Columns.Count; i++)
            {
                if (this.gridView1.Columns[i].ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit && this.gridView1.Columns[i].Name != "Employee")
                {
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DataSource = dt;
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.Clear();
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name",25, "标识"),
                     });
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).DisplayMember = "name";
                    ((DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)this.gridView1.Columns[i].ColumnEdit).ValueMember = "id";
                }
            }

            #endregion

            this.coBoxCJLD.EditValue = this._pcdic.PowerImpact;

            this.GirdViewLookUpEditEmployee.DataSource = this.bsLUEemployees.DataSource;
            this.bindingSource1.DataSource = this._pcdic.Detail;

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

            this.ceInvoiceXOCount.Enabled = false;
            this.txtPCDoubleImpactCheckID.Properties.ReadOnly = true;
            //this.nccEmployee0.Enabled = false;
        }

        protected override void Save()
        {
            this._pcdic.PCDoubleImpactCheckID = this.txtPCDoubleImpactCheckID.Text;
            this._pcdic.PCDoubleImpactCheckDate = this.DE_PCDoubleImpactCheckDate.DateTime;
            this._pcdic.PCDoubleImpactCheckDesc = this.txtPCDoubleImpactCheckDesc.Text;
            this._pcdic.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._pcdic.PronoteHeaderId = this.txtPronoteHeaderId.Text;
            this._pcdic.PCDoubleImpactCheckType = this.pcType;
            this._pcdic.PowerImpact = this.coBoxCJLD.SelectedItem == null ? null : this.coBoxCJLD.SelectedItem.ToString();
            this._pcdic.CheckStandard = this.txtCheckedStandard.Text;
            this._pcdic.PCDoubleImpactCheckCount = int.Parse(this.calcPCDoubleImpactCheckCount.EditValue != null ? calcPCDoubleImpactCheckCount.EditValue.ToString() : "0");

            this._pcdic.Employee = (this.nccEmployee0.EditValue as Model.Employee);
            if (this._pcdic.Employee != null)
            {
                this._pcdic.EmployeeId = this._pcdic.Employee.EmployeeId;
            }

            this._pcdic.Product = (this.BEProduct.EditValue as Model.Product);
            if (this._pcdic.Product != null)
            {
                this._pcdic.ProductId = this._pcdic.Product.ProductId;
            }
            this._pcdic.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this._pcdicManage.Insert(this._pcdic);
                    break;
                case "update":
                    this._pcdicManage.Update(this._pcdic);
                    break;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            switch (this._pcdic.PCDoubleImpactCheckType)
            {
                case 0:     //CSA
                    return new RO1(this._pcdic);
                    break;
                case 1:    //BS-EN
                    return new RO(this._pcdic);
                    break;
                case 2:   //AS-NZS
                    return new RO2(this._pcdic);
                    break;
                default:
                    return null;
                    break;
            }
            //return new RO(this._pcdic);
        }

        //添加行
        private void AddDataRows()
        {
            Model.PCDoubleImpactCheckDetail pcdicDetail = new Book.Model.PCDoubleImpactCheckDetail();
            pcdicDetail.PCDoubleImpactCheckDetailID = Guid.NewGuid().ToString();
            pcdicDetail.PCDoubleImpactCheckID = this._pcdic.PCDoubleImpactCheckID;
            pcdicDetail.PCDoubleImpactCheckDetailDate = DateTime.Now;
            this._pcdic.Detail.Add(pcdicDetail);

            this.bindingSource1.Position = this.bindingSource1.IndexOf(pcdicDetail);
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            switch (this.pcType)
            {
                case 0:     //CSA
                    //this.attrHotL.Visible = false;
                    //this.attrHotR.Visible = false;
                    this.attrCoolL.Visible = false;
                    this.attrCoolR.Visible = false;

                    this.attrZhuiQiu68gL.Caption = "眼球中心15mm范围(左)";
                    this.attrZhuiQiu68gR.Caption = "眼球中心15mm范围(右)";
                    //this.attrChuanTou44_2gL.Caption = "镜片下(左)(15mm)";
                    //this.attrChuanTou44_2gR.Caption = "镜片下(右)(15mm)";
                    this.attrChuanTou44_2gL.Visible = false;
                    this.attrChuanTou44_2gR.Visible = false;

                    this.gridColumn1.Visible = false;
                    this.gridColumn2.Visible = false;
                    this.gridColumn30left.Visible = true;
                    this.gridColumn30right.Visible = true;
                    this.Employee.VisibleIndex = 35;
                    break;
                case 1:     //BS_EN
                    this.attrJiaoLianL.Visible = false;
                    this.attrJiaoLianR.Visible = false;
                    //this.attrHotL.Visible = true;
                    //this.attrHotR.Visible = true;
                    this.attrCoolL.Visible = true;
                    this.attrCoolR.Visible = true;
                    this.gridColumn1.Visible = false;
                    this.gridColumn2.Visible = false;
                    this.gridColumn5.Caption = "坠球43g(左)";
                    this.gridColumn6.Caption = "坠球43g(右)";

                    this.Employee.VisibleIndex = 35;
                    break;
                case 2:     //AS_NZS
                    //this.gridColumn1.Caption = "加热(60℃±2)";
                    //this.gridColumn2.Caption = "加热(30m)";
                    //2021年6月8日23:58:27，将这两列合并为 加热60℃±2℃/30m
                    this.gridColumn1.Visible = false;
                    this.gridColumn2.Caption = "加热60℃±2℃/30m";

                    this.attrJiaoLianL.Visible = false;
                    this.attrJiaoLianR.Visible = false;
                    //this.attrHotL.Visible = false;
                    //this.attrHotR.Visible = false;
                    this.attrCoolL.Visible = false;
                    this.attrCoolR.Visible = false;
                    this.gridColumn5.Width = 120;
                    this.gridColumn6.Width = 120;
                    this.gridColumn5.Caption = "坠球42g土0.5g(左)";
                    this.gridColumn6.Caption = "坠球42g土0.5g(右)";
                    this.attrZhuiQiu68gL.Caption = "镜片上(左)(20mm)";
                    this.attrZhuiQiu68gR.Caption = "镜片上(右)(20mm)";

                    attr45L.Caption = "眼球位置(左)";
                    attr45R.Caption = "眼球位置(右)";
                    attr60L.Caption = "s.s(90度中眼球位置)(左)";
                    attr60R.Caption = "s.s(90度中眼球位置)(右)";
                    attr60L.Width = 150;
                    attr60R.Width = 150;


                    break;
                default: break;
            }

            //XmlDocument xDoc = new XmlDocument();
            //string filePath = Application.StartupPath + "\\QualityCheck.config";
            //xDoc.Load(filePath);

            //Hashtable ansiHT = new Hashtable();
            //XmlNodeList ANSINodeList = xDoc.SelectSingleNode("/configuration/CheckList/ImpactCheck").ChildNodes;
            ////冲击力度
            //ansiHT.Add(this.coBoxCJLD, ANSINodeList.Item(0).ChildNodes);
            //BindAllListBox(ansiHT);

            foreach (Model.Setting SET in new BL.SettingManager().SelectByName("ChongJiLiDao"))
            {
                this.coBoxCJLD.Properties.Items.Add(SET.SettingCurrentValue);
            }
            //this.coBoxCJLD.SelectedIndex = 0;
        }

        private void BindAllListBox(Hashtable mHt)
        {
            foreach (DictionaryEntry de in mHt)
            {
                DevExpress.XtraEditors.ComboBoxEdit mcoBox = de.Key as DevExpress.XtraEditors.ComboBoxEdit;
                XmlNodeList mNodes = de.Value as XmlNodeList;
                mcoBox.Properties.Items.Clear();
                foreach (XmlNode nd in mNodes)
                {
                    mcoBox.Properties.Items.Add(nd.Attributes["value"].Value);
                }
                mcoBox.SelectedIndex = 0;
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
                        if (this.bindingSource1.Current != null)
                        {
                            this._pcdic.Detail.Remove(this.bindingSource1.Current as Model.PCDoubleImpactCheckDetail);
                            if (this._pcdic.Detail.Count == 0)
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

        //选择加工单据
        private void btnGetPronoteHeader_Click(object sender, EventArgs e)
        {
            Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm pronoForm = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(null, 0);
            if (pronoForm.ShowDialog(this) == DialogResult.OK)
            {
                Model.PronoteHeader currentModel = pronoForm.SelectItem;
                if (currentModel != null)
                {
                    this._pcdic.PronoteHeaderId = currentModel.PronoteHeaderID;
                    this._pcdic.InvoiceCusXOId = currentModel.CustomerInvoiceXOId;
                    this._pcdic.Product = new BL.ProductManager().Get(currentModel.ProductId);
                    this._pcdic.ProductId = this._pcdic.Product.ProductId;
                    this._pcdic.CheckStandard = currentModel.CustomerCheckStandard;
                    this._pcdic.InvoiceXOQuantity = currentModel.InvoiceXODetailQuantity;
                    this.Refresh();
                }
            }
            pronoForm.Dispose();
            GC.Collect();
        }

        //搜索测试单据
        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm(this.pcType);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCDoubleImpactCheck currentModel = form.SelectItem as Model.PCDoubleImpactCheck;
                if (currentModel != null)
                {
                    this._pcdic = currentModel;
                    this._pcdic = this._pcdicManage.GetDetail(this._pcdic);
                    this.Refresh();
                }
            }
            form.Dispose();
            GC.Collect();
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            this.AddDataRows();
            this.gridControl1.RefreshDataSource();
        }

        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this._pcdic.Detail.Remove(this.bindingSource1.Current as Model.PCDoubleImpactCheckDetail);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void txtPCDoubleImpactCheckDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.txtPCDoubleImpactCheckDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCDoubleImpactCheck.PRO_PCDoubleImpactCheckID;
        }

        protected override int AuditState()
        {
            return this._pcdic.AuditState.HasValue ? this._pcdic.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PCDoubleImpactCheck" + "," + this._pcdic.PCDoubleImpactCheckID;
        }

        #endregion
    }
}
