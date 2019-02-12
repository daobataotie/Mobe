using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices
{
    public partial class BaseEditForm : DevExpress.XtraEditors.XtraForm
    {

        protected IDictionary<string, AA> requireValueExceptions;
        protected IDictionary<string, AA> invalidValueExceptions;
        private BL.RoleOperationManager RoleOperationManager = new BL.RoleOperationManager();
        //private Workflowinsert wfinsertManager = new Workflowinsert();
        //private BL.ProcessManager processManager = new BL.ProcessManager();
        //private BL.wfrecordManager wfrecordManager = new BL.wfrecordManager();
        private BL.RoleAuditingManager roleAuditingManager = new Book.BL.RoleAuditingManager();
        protected int saveAuditState;//保存时审核 状态 为 等待审核
        private int IsFirstLoad = 0;
        public static bool isDelete;    //指示删除按钮是否可用

        public BaseEditForm()
        {
            InitializeComponent();

            this.requireValueExceptions = new Dictionary<string, AA>();
            this.invalidValueExceptions = new Dictionary<string, AA>();
            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
        }

        protected virtual string tableCode()//停用
        {
            return string.Empty;
        }
        protected virtual int AuditState()
        {
            return 0;
        }

        public virtual BaseListForm GetListForm()
        {
            return null;
        }

        public virtual Model.Invoice Invoice
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            if (!this.HasRows())
                this.action = "insert";

            switch (this.action)
            {
                case "insert":
                    this.AddNew();
                    break;
                case "view":
                    this.MoveLast();
                    break;
            }
            this.IMECtrl();
            this.Refresh();
        }

        protected string action;

        #region Virtual Method

        //protected virtual Model.Invoice Invoice
        //{
        //    get { return null; }
        //}        

        protected virtual void TurnNull() { }

        protected virtual void Save(Helper.InvoiceStatus status)
        {
        }

        protected virtual void AddNew()
        {

        }

        protected virtual void Undo()
        {
            this.MoveLast();
            this.action = "view";
            this.Refresh();
        }

        protected virtual void IMECtrl()
        {

        }

        protected virtual void MovePrev()
        {
        }

        protected virtual void MoveNext()
        {
        }

        protected virtual void MoveFirst()
        {
        }

        protected virtual void MoveLast()
        {

        }

        protected virtual bool HasRows()
        {
            //throw new NotImplementedException();
            return false;
        }

        protected virtual bool HasRowsPrev()
        {
            //throw new NotImplementedException();
            return false;
        }

        protected virtual bool HasRowsNext()
        {
            //throw new NotImplementedException();
            return false;
        }

        protected virtual DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
        }

        protected virtual void Delete()
        {

        }

        #endregion

        protected virtual string getName()
        {
            string formName = this.GetType().FullName;
            formName = formName.Substring(formName.IndexOf('.') + 1).Substring(formName.Substring(formName.IndexOf('.') + 1).IndexOf('.') + 1);
            return formName;
        }

        public override void Refresh()
        {

            #region 页面刷新时获取所有本窗体的控件
            GetAllControls();
            #endregion

            //权限暂时注释
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                IList<Model.Role> roleList = BL.V.RoleList;
                int flag = 0;
                foreach (Model.Role item in roleList)
                {
                    if (item.Id == Settings.BasicData.Employees.EmployeeParameters.SYSTEMMANAGER)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 1)
                {
                    this.barButtonItemNew.Enabled = this.action == "view";
                    this.barButtonItemUpdate.Enabled = this.action == "view";
                    this.barButtonItemDelete.Enabled = this.action == "view";
                    this.barButtonItemPrev.Enabled = this.action == "view" && this.HasRowsPrev();
                    this.barButtonItemNext.Enabled = this.action == "view" && this.HasRowsNext();
                    this.barButtonItemPrint.Enabled = this.action == "view";

                    this.barButtonItemSave.Enabled = this.action != "view";
                    this.barButtonItemUndo.Enabled = this.action != "view" && this.HasRows();

                    this.barButtonItemFirst.Enabled = this.action == "view" && this.HasRowsPrev();
                    this.barButtonItemLast.Enabled = this.action == "view" && this.HasRowsNext();
                    this.barButtonItemQuery.Enabled = this.action == "view";

                    this.barButtonItemUndo.Enabled = this.action != "view" && this.HasRows();
                    this.barButtonItemAttachment.Enabled = this.action == "view";
                }
                else
                {

                    Model.RoleOperation roleOpe = this.SelectOperatorsKeyTag(this.getName());
                    if (roleOpe != null)
                    {
                        this.barButtonItemNew.Enabled = this.action == "view" && roleOpe.PossAdd == true;
                        this.barButtonItemUpdate.Enabled = this.action == "view" && roleOpe.PossUpdate == true;
                        this.barButtonItemDelete.Enabled = this.action == "view" && roleOpe.PossDelete == true;

                        this.barButtonItemPrev.Enabled = this.action == "view" && this.HasRowsPrev();
                        this.barButtonItemNext.Enabled = this.action == "view" && this.HasRowsNext();
                        this.barButtonItemPrint.Enabled = this.action == "view" && roleOpe.PossPrint == true;

                        this.barButtonItemSave.Enabled = (this.action == "insert" && roleOpe.PossAdd == true) || (this.action == "update" && roleOpe.PossUpdate == true);
                        this.barButtonItemUndo.Enabled = this.action != "view" && this.HasRows();

                        this.barButtonItemFirst.Enabled = this.action == "view" && this.HasRowsPrev();
                        this.barButtonItemLast.Enabled = this.action == "view" && this.HasRowsNext();
                        this.barButtonItemQuery.Enabled = this.action == "view";
                        this.barButtonItemUndo.Enabled = this.action != "view" && this.HasRows();
                        this.barButtonItemAttachment.Enabled = this.action == "view";
                    }
                }
                this.barButtonItemAudit.Enabled = false;
                this.barButtonItemAudit.Caption = "審核";
                if (this.action == "view")
                {
                    string tableName = Invoice.GetType().Name;
                    switch (Invoice.AuditState)
                    {
                        case 0:
                            if (this.roleAuditingManager.IsHasAudit(BL.V.ActiveOperator, this.Invoice.InvoiceId, tableName))
                            {
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "審核";
                            }
                            break;
                        case 1:
                            if (this.roleAuditingManager.IsHasAudit(BL.V.ActiveOperator, this.Invoice.InvoiceId, tableName))
                            {
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "審核";
                            }
                            break;
                        case 2:

                            if (this.roleAuditingManager.IsHasGiveUpAudited(BL.V.ActiveOperator, this.Invoice.InvoiceId, tableName))
                            {
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "棄審";
                            }
                            else if (this.roleAuditingManager.IsHasAudit(BL.V.ActiveOperator, this.Invoice.InvoiceId, tableName))
                            {
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "審核";
                            }



                            break;

                        case 4:
                            //操作员是否有审核权限
                            if (this.roleAuditingManager.IsHasAudit(BL.V.ActiveOperator, this.Invoice.InvoiceId, tableName))
                            {
                                //wfr.nowprocessid
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "複核";
                            }

                            break;
                        case 3:
                            //操作员是否有弃审权限
                            if (this.roleAuditingManager.IsHasGiveUpAudited(BL.V.ActiveOperator, this.Invoice.InvoiceId, tableName))
                            {
                                //wfr.nowprocessid
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "棄審";
                            }
                            this.barButtonItemUpdate.Enabled = false;
                            this.barButtonItemDelete.Enabled = false;
                            break;
                    }
                }
            }
            switch (this.action)
            {
                case "insert":
                    SetAllControlState(false);
                    break;
                case "update":
                    SetAllControlState(false);
                    break;
                case "view":
                    SetAllControlState(true);
                    break;
                default:
                    SetAllControlState(true);
                    break;
            }

            isDelete = this.barButtonItemDelete.Enabled;

            base.Refresh();
        }

        #region 设置控件是否是只读

        private static IList<Control> list = new List<Control>();
        void SetAllControlState(bool IsTrue)
        {
            foreach (Control item in list)
            {
                switch (item.GetType().Name.ToString())
                {
                    case "DockedBarControl":
                        break;
                    case "GridControl":
                        ((DevExpress.XtraGrid.Views.Grid.GridView)((DevExpress.XtraGrid.GridControl)item).DefaultView).OptionsBehavior.Editable = !IsTrue;
                        if (IsFirstLoad == 0)
                        {

                            //gridview

                        }
                        break;
                    case "XtraTabControl":
                        break;
                    case "ComboBoxEdit":
                        ((ComboBoxEdit)item).Properties.ReadOnly = IsTrue;
                        ((ComboBoxEdit)item).Properties.Buttons[0].Visible = !IsTrue;
                        break;

                    case "CheckEdit":
                        ((CheckEdit)item).Properties.ReadOnly = IsTrue;
                        break;
                    case "SpinEdit":
                        ((SpinEdit)item).Properties.ReadOnly = IsTrue;

                        if (IsFirstLoad == 0)
                        {
                            if (((SpinEdit)item).Name.EndsWith("xset"))
                            {
                                ((SpinEdit)item).Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                ((SpinEdit)item).Properties.EditFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.XSDJXiao.Value : 0);
                                ((SpinEdit)item).Properties.DisplayFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.XSDJXiao.Value : 0);
                                ((SpinEdit)item).Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            }
                            if (((SpinEdit)item).Name.EndsWith("cset"))
                            {
                                ((SpinEdit)item).Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                ((SpinEdit)item).Properties.EditFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.CGDJXiao.Value : 0);
                                ((SpinEdit)item).Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                ((SpinEdit)item).Properties.DisplayFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.CGDJXiao.Value : 0);
                            }
                        }
                        break;
                    case "CalcEdit":
                        ((CalcEdit)item).Properties.ReadOnly = IsTrue;
                        if (IsFirstLoad == 0)
                        {
                            if (((CalcEdit)item).Name.EndsWith("xset"))
                            {
                                ((CalcEdit)item).Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                ((CalcEdit)item).Properties.EditFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.XSDJXiao.Value : 0);
                                ((CalcEdit)item).Properties.DisplayFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.XSDJXiao.Value : 0);
                                ((CalcEdit)item).Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            }
                            if (((CalcEdit)item).Name.EndsWith("cset"))
                            {
                                ((CalcEdit)item).Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                ((CalcEdit)item).Properties.EditFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.CGDJXiao.Value : 0);
                                ((CalcEdit)item).Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                ((CalcEdit)item).Properties.DisplayFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.CGDJXiao.Value : 0);
                            }

                        }
                        break;
                    case "TextEdit":
                        ((TextEdit)item).Properties.ReadOnly = IsTrue;
                        if (IsFirstLoad == 0)
                        {
                            if (((TextEdit)item).Name.EndsWith("xset"))
                            {
                                ((TextEdit)item).Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                ((TextEdit)item).Properties.EditFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.XSDJXiao.Value : 0);
                                ((TextEdit)item).Properties.DisplayFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.XSDJXiao.Value : 0);
                                ((TextEdit)item).Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            }
                            if (((TextEdit)item).Name.EndsWith("cset"))
                            {
                                ((TextEdit)item).Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                ((TextEdit)item).Properties.EditFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.CGDJXiao.Value : 0);
                                ((TextEdit)item).Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                ((TextEdit)item).Properties.DisplayFormat.FormatString = global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSDJXiao.HasValue ? BL.V.SetDataFormat.CGDJXiao.Value : 0);
                            }
                        }

                        break;
                    case "MemoEdit":
                        ((MemoEdit)item).Properties.ReadOnly = IsTrue;
                        break;
                    case "NewChooseContorl":
                        ((NewChooseContorl)item).Enabled = !IsTrue;
                        break;
                    case "DateEdit":
                        ((DateEdit)item).Properties.ReadOnly = IsTrue;
                        ((DateEdit)item).Properties.Buttons[0].Visible = !IsTrue;
                        break;
                    case "LookUpEdit":
                        ((LookUpEdit)item).Properties.ReadOnly = IsTrue;
                        ((LookUpEdit)item).Properties.Buttons[0].Visible = !IsTrue;
                        break;
                    case "ButtonEdit":
                        ((ButtonEdit)item).Properties.ReadOnly = IsTrue;
                        ((ButtonEdit)item).Properties.Buttons[0].Visible = !IsTrue;
                        break;
                    case "RadioGroup":
                        ((RadioGroup)item).Properties.ReadOnly = IsTrue;
                        break;

                    case "SimpleButton":
                        ((SimpleButton)item).Enabled = !IsTrue;
                        break;

                    case "RichTextBox":
                        ((RichTextBox)item).Enabled = !IsTrue;
                        //case "GridView":
                        //    ((DevExpress.XtraGrid.GridControl)item).Controls.Add(33;
                        break;
                    default:
                        break;
                }

            }
            IsFirstLoad = 1;
        }
        #endregion

        #region 获取窗体上的所有控件
        void GetAllControls()
        {
            list.Clear();
            for (int i = 0; i < this.Controls.Count; i++)
            {
                list.Add(this.Controls[i]);
                foreach (Control item in this.Controls[i].Controls)
                {
                    list.Add(item);
                    GetControlChild(item);
                }
            }
        }

        private static void GetControlChild(Control item)
        {
            foreach (Control var in item.Controls)
            {
                list.Add(var);
                GetControlChild(var);
            }
        }

        #endregion


        #region barButtonItem_ItemClick

        private void barButtonItemUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Undo();
        }

        private void barButtonItemNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.action = "insert";
            this.AddNew();
            this.Refresh();
        }

        private void barButtonItemPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.MovePrev();
                this.Refresh();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void SetButtonEditButtonsVisibility(DevExpress.XtraEditors.ButtonEdit buttonEdit, bool visible)
        {
            foreach (DevExpress.XtraEditors.Controls.EditorButton button in buttonEdit.Properties.Buttons)
            {
                button.Visible = visible;
            }
        }

        private void barButtonItemNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.MoveNext();
                this.Refresh();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraReports.UI.XtraReport r = this.GetReport();
            if (r != null)
            {
                r.ShowPreviewDialog();
            }
        }

        //修改
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Refresh();
            this.action = "update";
            this.Refresh();
        }

        private void barButtonItemQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseListForm form = this.GetListForm();
            if (form != null)
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    this.Invoice = form.SelectedItem;
                    this.Refresh();
                }
            }
        }

        private void barButtonItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.Refresh();
                this.TurnNull();
            }
            catch (Helper.RequireValueException ex)
            {
                if (this.requireValueExceptions.ContainsKey(ex.Message))
                {
                    AA aa = this.requireValueExceptions[ex.Message];
                    MessageBox.Show(aa.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                throw;
            }
            catch (Helper.InvalidValueException ex)
            {
                if (this.invalidValueExceptions.ContainsKey(ex.Message))
                {
                    AA aa = this.invalidValueExceptions[ex.Message];
                    MessageBox.Show(aa.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                throw;
            }
            catch (Helper.MessageValueException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (this.HasRows())
            {
                if (this.HasRowsNext() == true)
                    this.MoveNext();
                else
                    this.MoveLast();
            }
            else
            {
                this.action = "insert";
                this.AddNew();
            }
            this.Refresh();
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                new BL.InvoiceBSManager().InvoiceName = this.Text;
                // bool checkwf = false;
                //if (this.action == "insert")
                //{
                //    Workflowinsert wfinsert = new Workflowinsert();

                //    if (!string.IsNullOrEmpty(this.tableCode()))
                //    {
                //        checkwf = wfinsert.Checkwfbytablescode(this.tableCode());
                //        if (checkwf)
                //            saveAuditState = (int)global::Helper.InvoiceAudit.WaitAudit;
                //    }
                //}

                this.Save((Helper.InvoiceStatus)System.Enum.Parse(typeof(Helper.InvoiceStatus), "Normal"));

                //try
                //{
                //    BL.V.BeginTransaction();
                //    if (!string.IsNullOrEmpty(this.tableCode()) && checkwf)
                //    {
                //        if (this.action == "insert")
                //        {
                //        //    Workflowinsert wfinsert = new Workflowinsert();
                //        //    wfinsert.insertwfrecord(this.tableCode(), this.Invoice.InvoiceId, this.Invoice.InvoiceId);

                //        }
                //        if (this.action == "update")
                //        {

                //            //Model.wfrecord wfr = wfrecordManager.GetByTableCodeAndKeyId(this.tableCode(), this.Invoice.InvoiceId);
                //            //wfr.UpdateTime = DateTime.Now;
                //            //wfrecordManager.Update(wfr);
                //        }
                //    }
                //    BL.V.CommitTransaction();
                //}
                //catch (Exception ex)
                //{
                //    BL.V.RollbackTransaction();
                //    throw ex;
                //}
                this.action = "view";
                this.Refresh();
            }
            catch (Helper.RequireValueException ex)
            {
                if (this.requireValueExceptions.ContainsKey(ex.Message))
                {
                    AA aa = this.requireValueExceptions[ex.Message];
                    MessageBox.Show(aa.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    aa.Control.Focus();
                    return;
                }
                throw;
            }
            catch (Helper.InvalidValueException ex)
            {
                if (this.invalidValueExceptions.ContainsKey(ex.Message))
                {
                    AA aa = this.invalidValueExceptions[ex.Message];
                    MessageBox.Show(aa.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    aa.Control.Focus();
                    return;
                }
                throw;
            }
            catch (Helper.ViolateConstraintException)
            {
                MessageBox.Show(Properties.Resources.InvoiceExist, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.textEditInvoiceId.Focus();
                return;
            }
            catch (Helper.MessageValueException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void barButtonItemFirst_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.MoveFirst();
                this.Refresh();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItemLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.MoveLast();
                this.Refresh();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void BaseEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.action != "view")
            {
                DialogResult result = MessageBox.Show(Properties.Resources.DataChangedDoYouSave, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                switch (result)
                {
                    case DialogResult.Abort:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.Ignore:
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.None:
                        break;
                    case DialogResult.OK:
                        break;
                    case DialogResult.Retry:
                        break;
                    case DialogResult.Yes:
                        this.barButtonItemSave_ItemClick(null, null);
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void barButtonItemAudit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int flag = 0;
            try
            {
                string tableName = Invoice.GetType().Name;
                if (Invoice.AuditState != (int)global::Helper.InvoiceAudit.Audited)
                    flag = 1;
                BL.V.BeginTransaction();

                //Model.RoleAuditing roleAuditing = this.roleAuditingManager.SelectByInvoiceIdAndTable(this.Invoice.InvoiceId, tableName);

                if (this.barButtonItemAudit.Caption == "審核" || this.barButtonItemAudit.Caption == "审核" || this.barButtonItemAudit.Caption == "複核" || this.barButtonItemAudit.Caption == "复核")
                {

                    //roleAuditing.AuditDate = DateTime.Now;
                    //roleAuditing.AuditRank = roleAuditing.AuditRank + 1;
                    ////是否最后审核
                    //if (!this.roleAuditingManager.IsLastAudit(roleAuditing.AuditRank.Value + 1, this.Invoice.InvoiceId, tableName))
                    //{
                    //    roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.Audited;//审核结束    
                    //    roleAuditing.NextAuditRole = null;
                    //    roleAuditing.NextAuditRoleId = null;
                    //    roleAuditing.Role1 = new BL.RoleManager().select_byAuditRandTableName(roleAuditing.AuditRank.Value, tableName);
                    //    if (roleAuditing.Role1 != null)
                    //        roleAuditing.Role1Id = roleAuditing.Role1.RoleId;
                    //}
                    //else
                    //{
                    //    roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.OnAuditing;
                    //    roleAuditing.Role1 = new BL.RoleManager().select_byAuditRandTableName(roleAuditing.AuditRank.Value, tableName);

                    //    if (roleAuditing.Role1 != null)
                    //        roleAuditing.Role1Id = roleAuditing.Role1.RoleId;

                    //    roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(roleAuditing.AuditRank.Value + 1, tableName);
                    //    if (roleAuditing.NextAuditRole != null)
                    //        roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                    //}

                    //roleAuditing.Employee1Id = BL.V.ActiveOperator.EmployeeId;
                    //roleAuditing.UpdateTime = DateTime.Now;
                    //this.roleAuditingManager.Update(roleAuditing);

                    string sql = "update " + tableName + " set  AuditState= " + (int)global::Helper.InvoiceAudit.Audited + ",   AuditEmpId='" + BL.V.ActiveOperator.EmployeeId + "' where " + Model.Invoice.PROPERTY_INVOICEID + "='" + this.Invoice.InvoiceId + "'";
                    new BL.InvoiceXJManager().UpdateSql(sql);
                    // this.barButtonItemAudit.Caption = "棄審";

                }
                else if (this.barButtonItemAudit.Caption == "棄審" || this.barButtonItemAudit.Caption == "弃审")
                {
                    //roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.GiveUpAudited;
                    //roleAuditing.AuditRank = 0;
                    //roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(1, tableName);
                    //if (roleAuditing.NextAuditRole != null)
                    //    roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                    //this.roleAuditingManager.Update(roleAuditing);

                    string sql = "update " + tableName + " set  AuditState= " + (int)global::Helper.InvoiceAudit.GiveUpAudited + ",   AuditEmpId='" + BL.V.ActiveOperator.EmployeeId + "' where " + Model.Invoice.PROPERTY_INVOICEID + "='" + this.Invoice.InvoiceId + "'";
                    new BL.InvoiceXJManager().UpdateSql(sql);
                    //   this.barButtonItemAudit.Caption = "審核";
                }

                this.Refresh();
                BL.V.CommitTransaction();

            }
            catch (Exception ex)
            {
                if (flag == 1)
                    this.barButtonItemAudit.Caption = "審核";
                else
                    this.barButtonItemAudit.Caption = "棄審";
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        private void barButtonItemAttachment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Invoice.InvoiceId))
            {
                Book.UI.Settings.BasicData.BaseAttachmentView bav = new Book.UI.Settings.BasicData.BaseAttachmentView(this.Invoice.InvoiceId, this.GetType().FullName);
                bav.ShowDialog(this);
            }
        }

        ///// <summary>
        ///// 根据操作员 和formname查询 权限
        ///// </summary>
        ///// <param name="formName"></param>
        ///// <returns></returns>
        private Model.RoleOperation SelectOperatorsKeyTag(string formName)
        {
            IList<Model.RoleOperation> roleOpList = this.RoleOperationManager.SelectbyOperatorsKeyTag(BL.V.ActiveOperator, formName);
            if (roleOpList == null || roleOpList.Count == 0) return null;
            Model.RoleOperation roleOperation = new Book.Model.RoleOperation();
            roleOperation.PossAdd = false;
            roleOperation.PossAuditing = false;
            roleOperation.PossDelete = false;
            roleOperation.PossExport = false;
            roleOperation.PossPrint = false;
            roleOperation.PossReportEdit = false;
            roleOperation.PossSearch = false;
            roleOperation.PossUpdate = false;
            foreach (Model.RoleOperation item in roleOpList)
            {
                if (item.PossAdd.HasValue && item.PossAdd.Value)
                {
                    roleOperation.PossAdd = true;
                }
                if (item.PossAuditing.HasValue && item.PossAuditing.Value)
                {
                    roleOperation.PossAuditing = true;
                }
                if (item.PossDelete.HasValue && item.PossDelete.Value)
                {
                    roleOperation.PossDelete = true;
                }
                if (item.PossExport.HasValue && item.PossExport.Value)
                {
                    roleOperation.PossExport = true;
                }
                if (item.PossPrint.HasValue && item.PossPrint.Value)
                {
                    roleOperation.PossPrint = true;
                }
                if (item.PossReportEdit.HasValue && item.PossReportEdit.Value)
                {
                    roleOperation.PossReportEdit = true;
                }
                if (item.PossSearch.HasValue && item.PossSearch.Value)
                {
                    roleOperation.PossSearch = true;
                }
                if (item.PossUpdate.HasValue && item.PossUpdate.Value)
                {
                    roleOperation.PossUpdate = true;
                }

            }
            return roleOperation;
        }

        protected string GetAuditName(int? a)
        {
            string b = string.Empty;
            switch (a)
            {

                case 0: b = "未啟用";
                    break;
                case 1: b = "待審核";
                    break;
                case 2: b = "審核中";
                    break;
                case 3: b = "已審核";
                    break;
                case 4: b = "棄核";
                    break;
                default: b = "未啟用";
                    break;
            }
            return b;
        }
        public decimal GetDecimal(decimal d, int n)
        {
            return global::Helper.DateTimeParse.GetSiSheWuRu(d, n);

        }


        /// <summary>
        /// 设置格式方法. 返回格式 "0.000"
        /// </summary>
        /// <param name="objTarget">小数位长度</param>     
        /// <returns></returns>
        public string GetFormat(int leng)
        {
            string a = "0";
            if (leng > 0)
            {
                a = "0.";
                for (int i = 0; i < leng; i++)
                {
                    a += "0";
                }
            }
            return a;
        }

        /// <summary>
        /// 设置格式方法,返回格式 "0:0.000"
        /// </summary>
        /// <param name="objTarget">小数位长度</param>     
        /// <returns></returns>
        public static string GetFormatA(int leng)
        {
            string a = "{0:0";
            if (leng > 0)
            {
                a = "{0:0.";
                for (int i = 0; i < leng; i++)
                {
                    a += "0";
                }
            }
            return a + "}";
        }

    }
}