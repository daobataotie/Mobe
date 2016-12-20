using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using Book.UI.Invoices;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using System.Linq;

namespace Book.UI.Settings.BasicData
{
    public partial class BaseEditForm : XtraForm
    {
        public static DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
        private BL.RoleAuditingManager roleAuditingManager = new Book.BL.RoleAuditingManager();
        protected IDictionary<string, AA> requireValueExceptions;
        protected IDictionary<string, AA> invalidValueExceptions;
        private BL.RoleOperationManager RoleOperationManager = new BL.RoleOperationManager();
        //  private Workflowinsert wfinsertManager = new Workflowinsert();
        // private BL.ProcessManager processManager = new BL.ProcessManager();
        //  private BL.wfrecordManager wfrecordManager = new BL.wfrecordManager();
        protected string action;
        protected int saveAuditState;   //保存时审核   状态 为 等待审核
        protected bool flagSave = false;//保存时 确认
        public static bool isDelete;       //指示删除按钮是否可用

        public BaseEditForm()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(BaseEditForm_FormClosing);

            this.requireValueExceptions = new Dictionary<string, AA>();
            this.invalidValueExceptions = new Dictionary<string, AA>();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        void BaseEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.action != "view")
            {
                DialogResult result = MessageBox.Show(Properties.Resources.DataChangedDoYouSave, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                switch (result)
                {
                    case DialogResult.Yes:
                        int flagAut = 0;

                        string KeyIdName = null;
                        string tableName = null;
                        string tableKeyId = null;
                        string tableDesc = null;
                        if (!string.IsNullOrEmpty(tableCode()))
                        {
                            flagAut = 1;
                            KeyIdName = AuditKeyId();
                            tableName = this.tableCode().Substring(0, this.tableCode().IndexOf(','));
                            tableKeyId = this.tableCode().Substring(this.tableCode().IndexOf(',') + 1);
                            tableDesc = new BL.OperationManager().GetOperationNamebyTabel(tableName);
                        }
                        ClickSave(flagAut, KeyIdName, tableName, tableKeyId);
                        e.Cancel = true;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void BaseEditForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.StartPosition = FormStartPosition.CenterParent;
            //this.action = "view";
            switch (this.action)
            {
                case "insert":
                    this.AddNew();
                    break;
                case "view":
                    //if (IsDoubleClickedGrid == true)
                    this.MoveLast();
                    break;
            }
            this.IMECtrl();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            this.Refresh();
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int flagAut = 0;

            string KeyIdName = null;
            string tableName = null;
            string tableKeyId = null;
            string tableDesc = null;
            if (!string.IsNullOrEmpty(tableCode()))
            {
                flagAut = 1;
                KeyIdName = AuditKeyId();
                tableName = this.tableCode().Substring(0, this.tableCode().IndexOf(','));
                tableKeyId = this.tableCode().Substring(this.tableCode().IndexOf(',') + 1);
                tableDesc = new BL.OperationManager().GetOperationNamebyTabel(tableName);
            }

            switch ((string)e.Item.Tag)
            {
                case "save":
                    //try
                    //{
                    ClickSave(flagAut, KeyIdName, tableName, tableKeyId);
                    //}
                    //catch (Helper.MessageValueException ex)
                    //{
                    //    if (!string.IsNullOrEmpty(ex.Message) && !ex.Message.Equals("SaveCancel", StringComparison.InvariantCultureIgnoreCase))
                    //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    //catch (Helper.RequireValueException ex)
                    //{
                    //    if (this.requireValueExceptions.ContainsKey(ex.Message))
                    //    {
                    //        AA aa = this.requireValueExceptions[ex.Message];
                    //        MessageBox.Show(aa.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        aa.Control.Focus();
                    //        return;
                    //    }
                    //    throw;
                    //}
                    //catch (Helper.InvalidValueException ex)
                    //{
                    //    if (this.invalidValueExceptions.ContainsKey(ex.Message))
                    //    {
                    //        AA aa = this.invalidValueExceptions[ex.Message];
                    //        MessageBox.Show(aa.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        aa.Control.Focus();
                    //        return;
                    //    }
                    //    throw;
                    //}
                    //catch (Helper.ViolateConstraintException ex)
                    //{
                    //    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //    return;
                    //}
                    //catch (Helper.VersionOverTimeException)
                    //{
                    //    MessageBox.Show(Properties.Resources.ObjectHasChange, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    this.action = "view";
                    //    this.Refresh();
                    //    return;
                    //}

                    break;
                case "undo":
                    this.MoveLast();
                    this.action = "view";
                    this.Refresh();
                    break;
                case "new":
                    this.AddNew();
                    this.action = "insert";
                    this.Refresh();
                    break;
                case "update":
                    this.action = "update";
                    this.Refresh();
                    break;
                case "prev":
                    try
                    {
                        this.MovePrev();
                        this.Refresh();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "next":
                    try
                    {
                        this.MoveNext();
                        this.Refresh();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "first":
                    try
                    {
                        this.MoveFirst();
                        this.Refresh();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "last":
                    try
                    {
                        this.MoveLast();
                        this.Refresh();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "delete":
                    try
                    {
                        int a = AuditState();
                        this.Delete();
                        if (flagAut == 1 && a != 0)
                            roleAuditingManager.DeleteByInvoiceIdAndTable(tableKeyId, tableName);

                        this.Refresh();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Helper.ViolateConstraintException ex)
                    {
                        MessageBox.Show(Properties.Resources.DeleteError + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch
                    {
                        MessageBox.Show(Properties.Resources.DeleteError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "Audit":
                    if (flagAut == 1)
                    {
                        int flag = 0;
                        try
                        {

                            if (AuditState() != (int)global::Helper.InvoiceAudit.Audited)
                                flag = 1;
                            BL.V.BeginTransaction();

                            Model.RoleAuditing roleAuditing = this.roleAuditingManager.SelectByInvoiceIdAndTable(tableKeyId, tableName);

                            if (this.barButtonItemAudit.Caption == "審核" || this.barButtonItemAudit.Caption == "审核" || this.barButtonItemAudit.Caption == "複核" || this.barButtonItemAudit.Caption == "复核")
                            {

                                roleAuditing.AuditDate = DateTime.Now;
                                roleAuditing.AuditRank = roleAuditing.AuditRank + 1;
                                //是否最后审核
                                if (!this.roleAuditingManager.IsLastAudit(roleAuditing.AuditRank.Value + 1, tableKeyId, tableName))
                                {
                                    roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.Audited;//审核结束    
                                    roleAuditing.NextAuditRole = null;
                                    roleAuditing.NextAuditRoleId = null;
                                    roleAuditing.Role1 = new BL.RoleManager().select_byAuditRandTableName(roleAuditing.AuditRank.Value, tableName);
                                    if (roleAuditing.Role1 != null)
                                        roleAuditing.Role1Id = roleAuditing.Role1.RoleId;
                                }
                                else
                                {
                                    roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.OnAuditing;
                                    roleAuditing.Role1 = new BL.RoleManager().select_byAuditRandTableName(roleAuditing.AuditRank.Value, tableName);

                                    if (roleAuditing.Role1 != null)
                                        roleAuditing.Role1Id = roleAuditing.Role1.RoleId;

                                    roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(roleAuditing.AuditRank.Value + 1, tableName);
                                    if (roleAuditing.NextAuditRole != null)
                                        roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                                }

                                roleAuditing.Employee1Id = BL.V.ActiveOperator.EmployeeId;
                                roleAuditing.UpdateTime = DateTime.Now;
                                this.roleAuditingManager.Update(roleAuditing);
                                string sql = "update " + tableName + " set  AuditState= " + roleAuditing.AuditState + ",   AuditEmpId='" + BL.V.ActiveOperator.EmployeeId + "' where " + KeyIdName + "='" + tableKeyId + "'";
                                new BL.AcademicBackGroundManager().UpdateSqlMap(sql);
                                // this.barButtonItemAudit.Caption = "棄審";

                            }
                            else if (this.barButtonItemAudit.Caption == "棄審" || this.barButtonItemAudit.Caption == "弃审")
                            {//弃核
                                roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.GiveUpAudited;
                                roleAuditing.AuditRank = 0;
                                roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(1, tableName);
                                if (roleAuditing.NextAuditRole != null)
                                    roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                                this.roleAuditingManager.Update(roleAuditing);

                                string sql = "update " + tableName + " set  AuditState= " + (int)global::Helper.InvoiceAudit.GiveUpAudited + ",   AuditEmpId='" + BL.V.ActiveOperator.EmployeeId + "' where " + KeyIdName + "='" + tableKeyId + "'";
                                new BL.AcademicBackGroundManager().UpdateSqlMap(sql);
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
                    break;
                //if (!string.IsNullOrEmpty(this.tableCode()))
                //{

                //    Model.wfrecord wfr = wfrecordManager.GetByTableCodeAndKeyId(this.tableCode().Substring(0, this.tableCode().IndexOf(',')), this.tableCode().Substring(this.tableCode().IndexOf(',') + 1, this.tableCode().LastIndexOf(',') - this.tableCode().IndexOf(',') - 1));
                //    if (wfr != null)
                //    {
                //        if (this.AuditState() != (int)global::Helper.InvoiceAudit.Audited)
                //        {//审核

                //            Model.process noprocess = processManager.GetProcessbyid(wfr.nowprocessid);
                //            if (noprocess != null)
                //            {
                //                wfr.processId = wfr.nowprocessid;
                //                wfr.nowprocessid = noprocess.Processnex;
                //                Model.process proc = processManager.GetProcessbyid(wfr.nowprocessid);
                //                wfr.allovertime = DateTime.Today;
                //                if (proc.processType == "结束")
                //                {
                //                    wfr.allstate = (int)global::Helper.InvoiceAudit.Audited;
                //                }
                //                else
                //                {
                //                    wfr.allstate = (int)global::Helper.InvoiceAudit.OnAuditing;
                //                }
                //                wfrecordManager.Update(wfr);
                //                string sql = "update " + this.tableCode().Substring(0, this.tableCode().IndexOf(',')) + " set  AuditState= " + wfr.allstate + ",   AuditEmpId='" + BL.V.ActiveOperator.EmployeeId + "' where " + this.AuditKeyId() + "='" + this.tableCode().Substring(this.tableCode().IndexOf(',') + 1, this.tableCode().LastIndexOf(',') - this.tableCode().IndexOf(',') - 1) + "'";
                //                new BL.InvoiceXJManager().UpdateSql(sql);
                //            }
                //            this.barButtonItemAudit.Caption = "棄審";
                //        }
                //        else
                //        {
                //            //弃核                            
                //            foreach (Model.process p in processManager.SelectProcessbywf(wfr.WorkflowId))
                //            {
                //                if (p.processType == "开始")
                //                {
                //                    wfr.processId = p.processId;
                //                    wfr.nowprocessid = p.Processnex;
                //                    break;
                //                }
                //            }
                //            wfr.allovertime = DateTime.Now;
                //            wfr.allstate = (int)global::Helper.InvoiceAudit.GiveUpAudited;
                //            wfrecordManager.Update(wfr);

                //            string sql = "update " + this.tableCode().Substring(0, this.tableCode().IndexOf(',')) + " set  AuditState= " + wfr.allstate + ",   AuditEmpId='" + BL.V.ActiveOperator.EmployeeId + "' where " + this.AuditKeyId() + "='" + this.tableCode().Substring(this.tableCode().IndexOf(',') + 1, this.tableCode().LastIndexOf(',') - this.tableCode().IndexOf(',') - 1) + "'";
                //            new BL.InvoiceXJManager().UpdateSql(sql);

                //            this.barButtonItemAudit.Caption = "審核";

                //        }


                //    }
                //}
                //更新表单
                //this.Refresh();
                //BL.V.CommitTransaction();
                //if (wfinsertManager.Checkwfbytablescode(tableCode()))
                //{
                //    wfinsertManager.insertwfrecord("TechonlogyHeader", "工艺 " + this.techonlogyHeader.TechonlogyHeadername, this.techonlogyHeader.TechonlogyHeaderId);
                //}
                //else
                //{
                //}
                //    }
                //    catch (Exception ex)
                //    {
                //        this.barButtonItemAudit.Caption = "審核";
                //        BL.V.RollbackTransaction();
                //        throw ex;
                //    }
                //    break;
                case "Attachment":
                    if (!string.IsNullOrEmpty(tableKeyId))
                    {
                        BaseAttachmentView bav = new BaseAttachmentView(tableKeyId, this.GetType().FullName);
                        bav.ShowDialog(this);
                    }
                    break;
            }
        }

        private void ClickSave(int flagAut, string KeyIdName, string tableName, string tableKeyId)
        {
            try
            {
                bool checkwf = false;

                //if (this.action == "insert")
                //{
                //    Workflowinsert wfinsert = new Workflowinsert();

                //    if (!string.IsNullOrEmpty(this.tableCode()))
                //    {
                //        checkwf = wfinsert.Checkwfbytablescode(this.tableCode().Substring(0, this.tableCode().IndexOf(',')));
                //        if (checkwf)
                //            saveAuditState = (int)global::Helper.InvoiceAudit.WaitAudit;
                //    }
                //}
                saveAuditState = (int)global::Helper.InvoiceAudit.NoUsing;
                this.Save();


                //MessageBox.Show("fdafda");
                if (flagAut == 1 && roleAuditingManager.IsNeedAuditByTableName(tableName))
                {

                    //  Model.RoleAuditing roleAuditing = this.roleAuditingManager.SelectByInvoiceIdAndTable(invoice.InvoiceId, tableName);
                    //if (   isUpdate == 0) //代表 添加
                    if (this.action == "insert")
                    {

                        Model.RoleAuditing roleAuditing = new Book.Model.RoleAuditing();
                        roleAuditing.RoleAuditingId = Guid.NewGuid().ToString();
                        roleAuditing.AuditRank = 0;
                        roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(1, tableName);
                        if (roleAuditing.NextAuditRole != null)
                            roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                        roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;
                        roleAuditing.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                        roleAuditing.InsertTime = DateTime.Now;
                        roleAuditing.InvoiceId = tableKeyId;
                        roleAuditing.InvoiceName = new BL.OperationManager().GetOperationNamebyTabel(tableName); ;
                        roleAuditing.TableName = tableName;
                        this.roleAuditingManager.Insert(roleAuditing);


                    }
                    if (this.action == "update")
                    {
                        if (AuditState() == 4)
                        {


                            Model.RoleAuditing roleAuditing = this.roleAuditingManager.SelectByInvoiceIdAndTable(tableKeyId, tableName);
                            roleAuditing.AuditRank = 0;
                            roleAuditing.NextAuditRole = new BL.RoleManager().select_byAuditRandTableName(1, tableName);
                            if (roleAuditing.NextAuditRole != null)
                                roleAuditing.NextAuditRoleId = roleAuditing.NextAuditRole.RoleId;
                            roleAuditing.AuditState = (int)global::Helper.InvoiceAudit.WaitAudit;
                            roleAuditing.Employee0Id = BL.V.ActiveOperator.EmployeeId;
                            this.roleAuditingManager.Update(roleAuditing);

                        }
                    }
                    string sql = "update " + tableName + " set  AuditState= " + (int)global::Helper.InvoiceAudit.WaitAudit + "  where " + KeyIdName + "='" + tableKeyId + "'";
                    new BL.AcademicBackGroundManager().UpdateSqlMap(sql);
                }
                //catch (Exception ex)
                //{
                //    BL.V.RollbackTransaction();
                //    throw ex;
                //}

                this.action = "view";
                this.Refresh();
            }
            catch (Helper.MessageValueException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message) && !ex.Message.Equals("SaveCancel", StringComparison.InvariantCultureIgnoreCase))
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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
            catch (Helper.ViolateConstraintException ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            catch (Helper.VersionOverTimeException)
            {
                MessageBox.Show(Properties.Resources.ObjectHasChange, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.action = "view";
                this.Refresh();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        protected virtual string tableCode()
        {
            return string.Empty;
        }

        protected virtual string tableKeyId()
        {
            return string.Empty;
        }

        protected virtual string AuditKeyId()
        {
            return string.Empty;
        }

        protected virtual int AuditState()
        {
            return 0;
        }

        #region Properties

        public virtual object EditedItem
        {
            get
            {
                return null;
            }
        }

        #endregion

        #region Virtual Method
        protected virtual void Delete()
        {

        }

        protected virtual void AddNew()
        {
        }

        protected virtual void Save()
        {
        }

        protected virtual void Undo()
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

        protected virtual void IMECtrl()
        {

        }

        /// <summary>
        /// 设置是否显示gridview排序行
        /// </summary>
        /// <returns></returns>
        protected virtual bool SetColumnNumber()
        {
            return false;
        }
        #endregion

        #region Override Method

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

            #region 权限
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

                this.barButtonItemFirst.Enabled = this.action == "view" && this.HasRowsPrev();
                this.barButtonItemPrev.Enabled = this.action == "view" && this.HasRowsPrev();
                this.barButtonItemNext.Enabled = this.action == "view" && this.HasRowsNext();
                this.barButtonItemLast.Enabled = this.action == "view" && this.HasRowsNext();
                this.barButtonItemPrint.Enabled = this.action == "view";
                this.barButtonItemSave.Enabled = this.action != "view";
                this.barButtonItemDelete.Enabled = this.action == "view";
                this.barButtonItemUndo.Enabled = this.action != "view" && this.HasRows();
                this.barButtonitemAllAttachment.Enabled = this.action == "view";
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
                    // this.barButtonItemQuery.Enabled = this.action == "view";
                    this.barButtonItemUndo.Enabled = this.action != "view" && this.HasRows();
                    this.barButtonitemAllAttachment.Enabled = this.action == "view";
                }
            }
            #endregion

            this.barButtonItemAudit.Enabled = false;
            this.barButtonItemAudit.Caption = "審核";
            if (!string.IsNullOrEmpty(this.tableCode()))
            {
                if (this.action == "view")
                {
                    string tableName = this.tableCode().Substring(0, this.tableCode().IndexOf(','));
                    string tableKeyId = this.tableCode().Substring(this.tableCode().IndexOf(',') + 1);
                    switch (AuditState())
                    {
                        case 0:
                            if (this.roleAuditingManager.IsHasAudit(BL.V.ActiveOperator, tableKeyId, tableName))
                            {
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "審核";
                            }
                            break;
                        case 1:
                            if (this.roleAuditingManager.IsHasAudit(BL.V.ActiveOperator, tableKeyId, tableName))
                            {
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "審核";
                            }
                            break;
                        case 2:

                            if (this.roleAuditingManager.IsHasGiveUpAudited(BL.V.ActiveOperator, tableKeyId, tableName))
                            {
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "棄審";
                            }
                            else if (this.roleAuditingManager.IsHasAudit(BL.V.ActiveOperator, tableKeyId, tableName))
                            {
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "審核";
                            }



                            break;

                        case 4:
                            //操作员是否有审核权限
                            if (this.roleAuditingManager.IsHasAudit(BL.V.ActiveOperator, tableKeyId, tableName))
                            {
                                //wfr.nowprocessid
                                this.barButtonItemAudit.Enabled = true;
                                this.barButtonItemAudit.Caption = "複核";
                            }

                            break;
                        case 3:
                            //操作员是否有弃审权限
                            if (this.roleAuditingManager.IsHasGiveUpAudited(BL.V.ActiveOperator, tableKeyId, tableName))
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

        private static IList<Control> list = new List<Control>();

        #region 设置控件是否是只读
        void SetAllControlState(bool IsTrue)
        {
            foreach (Control item in list)
            {
                try
                {
                    switch (item.GetType().Name.ToString())
                    {
                        case "DockedBarControl":
                            break;
                        case "GridControl":
                            //((GridView)((GridControl)item).MainView).OptionsBehavior.ReadOnly = IsTrue;
                            DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
                            col.Caption = "序號";
                            col.FieldName = "Inumber";
                            col.Name = "gridcolumnOrderId";
                            if (this.SetColumnNumber())
                            {
                                if (!((GridView)((GridControl)item).MainView).Columns.Contains(((GridView)((GridControl)item).MainView).Columns.ColumnByName(col.Name)))
                                {
                                    ((GridView)((GridControl)item).MainView).Columns.Add(col);
                                    col.Visible = true;
                                    col.VisibleIndex = 0;
                                }
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
                            break;
                        case "TextEdit":
                            ((TextEdit)item).Properties.ReadOnly = IsTrue;
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
                            break;
                        case "CheckedComboBoxEdit":
                            ((CheckedComboBoxEdit)item).Properties.ReadOnly = IsTrue;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ec)
                {

                }
            }
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

        #endregion

        protected void SetButtonEditButtonsVisibility(DevExpress.XtraEditors.ButtonEdit buttonEdit, bool visible)
        {
            foreach (DevExpress.XtraEditors.Controls.EditorButton button in buttonEdit.Properties.Buttons)
            {
                button.Visible = visible;
            }
        }

        private void barButtonItemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraReports.UI.XtraReport r = this.GetReport();
            if (r != null)
            {
                r.ShowPreviewDialog();
                {
                    // MessageBox.Show("fdas");s
                }
            }
        }

        protected virtual DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
        }

        protected void Visibles()
        {
            if (this.bar1.ItemLinks.Count < 7) return;
            this.bar1.ItemLinks[5].Visible = false;

            this.bar1.ItemLinks[6].Visible = false;

            this.bar1.ItemLinks[7].Visible = false;

            this.bar1.ItemLinks[8].Visible = false;

            this.bar1.ItemLinks[9].Visible = false;
        }

        protected void VisiblesLinks0()
        {
            this.barButtonItemUndo.Enabled = false;
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

        ///// <summary>
        ///// 根据操作员 和formname查询 权限
        ///// </summary>
        ///// <param name="formName"></param>
        ///// <returns></returns>
        private Model.RoleOperation SelectOperatorsKeyTag(string formName)
        {
            Model.RoleOperation roleOperation = null;
            if (!this.DesignMode)
            {
                IList<Model.RoleOperation> roleOpList = this.RoleOperationManager.SelectbyOperatorsKeyTag(BL.V.ActiveOperator, formName);
                if (roleOpList == null || roleOpList.Count == 0) return null;
                roleOperation = new Book.Model.RoleOperation();
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
            }
            return roleOperation;
        }

        public decimal GetDecimal(decimal? d, int n)
        {
            if (d == null)
                d = 0M;
            return global::Helper.DateTimeParse.GetSiSheWuRu(d.Value, n);
        }

        public decimal GetDecimal(decimal d, int n)
        {
            return global::Helper.DateTimeParse.GetSiSheWuRu(d, n);
        }
    }
}