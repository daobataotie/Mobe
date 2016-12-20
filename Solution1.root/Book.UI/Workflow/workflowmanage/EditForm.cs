using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Workflow.workflowmanage;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
//------------------------------------------------------------------------------
//
// 说明：该文件中的内容主要处理工作流部分
//
// author: 徐彦飞
// create date：2009-11-24 上午 11:45:01
//
//------------------------------------------------------------------------------
namespace Book.UI.Workflow
{
    /// <summary>
    /// 流程定义
    /// </summary>
    public partial class EditForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        /// <summary>
        /// 表单集合
        /// </summary>
        Dictionary<string, string> tablelist = new Dictionary<string, string>();
        /// <summary>
        /// 表单管理类
        /// </summary>
        private BL.TablesManager tablemanage = new Book.BL.TablesManager();
        /// <summary>
        /// 表单对象
        /// </summary>
        // private Model.Tables tm = null;
        /// <summary>
        /// workflow object
        /// </summary>
        private Model.Workflow wf = null;
        /// <summary>
        /// workflow manage ojbect
        /// </summary>
        private BL.WorkflowManager workflowmanage = new Book.BL.WorkflowManager();
        /// <summary>
        /// process object
        /// </summary>
        private BL.ProcessManager processmanage = new Book.BL.ProcessManager();


        private BL.accepterattribManager acceptermanage = new Book.BL.accepterattribManager();

        // 表单ID
        string t = null;

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.Workflow.PRO_workflowname, new AA(Properties.Resources.RequireDataForNames, this.textEditName));
            // this.invalidValueExceptions.Add(Model.Workflow.PROPERTY_TABLESID, new AA(Properties.Resources.RequireDataForNames, this.comboBoxEditTable));
            this.ncc_comboBoxEditTable.Choose = new Book.UI.Workflow.Tablem.ChooseTables();

            //inintTable();
        }

        //private void inintTable()
        //{
        //    IList<Model.Tables> tables = this.tablemanage.Select();
        //    this.comboBoxEditTable.Properties.Items.Clear();
        //    foreach (Model.Tables _table in tables)
        //    {
        //        if (!workflowmanage.getTable(_table.TablesID))
        //        {
        //            this.comboBoxEditTable.Properties.Items.Add(_table.Tablename);
        //        }
        //    }
        //}

        public EditForm(Model.Workflow workflow)
            : this()
        {
            this.wf = workflow;
            this.action = "update";
            loadprocess();
        }


        public EditForm(Model.Workflow workflow, string action)
            : this()
        {

            this.wf = workflow;

            this.action = action;
            //loadprocess();
        }

        public override object EditedItem
        {
            get
            {
                return this.wf;
            }
        }

        protected override void Delete()
        {
            if (this.wf == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            // this.workflowm.Delete(this.duty);
            this.workflowmanage.Delete(this.wf);

            //this.duty = this.dutyManager.GetNext(this.duty);


            //if (this.duty == null)
            //{
            //    this.duty = this.dutyManager.GetLast();
            //}
        }

        protected override void MoveFirst()
        {
            base.MoveFirst();
        }

        protected override void MoveLast()
        {
            base.MoveLast();
        }

        protected override void AddNew()
        {
            /// this.duty = new Book.Model.Duty();

            this.action = "insert";
            this.wf = new Book.Model.Workflow();
            this.wf.WorkflowId = Guid.NewGuid().ToString();
            this.wf.OnofOff = true;
        }

        protected override void MoveNext()
        {
            base.MoveNext();
        }

        protected override void MovePrev()
        {
            base.MovePrev();
        }

        protected override bool HasRows()
        {
            return this.workflowmanage.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return base.HasRowsNext();
        }

        protected override bool HasRowsPrev()
        {
            return base.HasRowsPrev();
        }

        public override void Refresh()
        {
            if (this.wf == null)
            {
                this.AddNew();
            }

            this.textEditId.Text = this.wf.Id;
            this.textEditName.Text = this.wf.workflowname;
            this.ncc_comboBoxEditTable.EditValue = this.wf.Tables;
            //this.comboBoxEditTable.Text = this.wf.TablesID;
            this.radio_OnofOff.SelectedIndex = this.wf.OnofOff.HasValue ? (this.wf.OnofOff.Value ? 0 : 1) : 0;
            this.textEditDescription.Text = this.wf.description;

            loadprocess();

            //switch (this.action)
            //{
            //    case "insert":
            //        this.textEditId.Properties.ReadOnly = false;
            //        this.textEditName.Properties.ReadOnly = false;
            //        this.comboBoxEditTable.Properties.ReadOnly = false;
            //        this.textEditDescription.Properties.ReadOnly = false;
            //        this.simpleButtonAppend.Enabled = true;
            //        this.simpleButtonRemove.Enabled = true;
            //        this.gridView1.OptionsBehavior.Editable = true;
            //        break;

            //    case "update":

            //        this.textEditId.Properties.ReadOnly = false;
            //        this.textEditName.Properties.ReadOnly = false;
            //        this.comboBoxEditTable.Properties.ReadOnly = false;
            //        this.textEditDescription.Properties.ReadOnly = false;
            //        this.simpleButtonAppend.Enabled = true;
            //        this.simpleButtonRemove.Enabled = true;
            //        this.gridView1.OptionsBehavior.Editable = true;
            //        break;

            //    case "view":

            //        this.textEditId.Properties.ReadOnly = true;
            //        this.textEditName.Properties.ReadOnly = true;
            //        this.comboBoxEditTable.Properties.ReadOnly = true;
            //        this.textEditDescription.Properties.ReadOnly = true;
            //        this.simpleButtonAppend.Enabled = false;
            //        this.simpleButtonRemove.Enabled = false ;
            //        this.gridView1.OptionsBehavior.Editable = false ;
            //        break;

            //    default:
            //        break;
            //}

            base.Refresh();
        }

        /// <summary>
        /// 保存流程信息
        /// </summary>
        protected override void Save()
        {
            this.wf.Id = this.textEditId.Text;
            this.wf.workflowname = this.textEditName.Text;
            //获取表单ID
            //if (tablemanage.GetIDbyname(this.comboBoxEditTable.Text) != null)
            //{

            //    t = tablemanage.GetIDbyname(this.comboBoxEditTable.Text).TablesID;
            //}
            //wf.TablesID = t;
            string mtablesid = this.ncc_comboBoxEditTable.EditValue == null ? "" : (this.ncc_comboBoxEditTable.EditValue as Model.Tables).TablesID;
            wf.TablesID = mtablesid;
            wf.OnofOff = this.radio_OnofOff.SelectedIndex == 0 ? true : false;
            wf.description = this.textEditDescription.Text;

            switch (this.action)
            {
                case "insert":
                    this.wf.WorkflowId = Guid.NewGuid().ToString();
                    this.workflowmanage.Insert(wf);
                    break;

                case "update":
                    this.workflowmanage.Update(wf);
                    break;
            }
            loadprocess();
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.textEditId, this });
        }
        /// <summary>
        ///  add process of the workflow event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(wf.WorkflowId))
            {
                ProcessEdit from = new ProcessEdit(wf.WorkflowId);

                if (from.ShowDialog() == DialogResult.OK)
                {
                    loadprocess();
                }
            }
        }

        private void loadprocess()
        {
            labelview.Text = "";
            processlist.Clear();
            this.bindingSource1.Clear();
            Model.process beginprocess = processmanage.GetBeginProcess(wf.WorkflowId);
            getprocessnext(beginprocess);
            // this.bindingSource1.DataSource = processlist;
            xtraTabControl1.Refresh();

            string s = "流程示意图： ";
            for (int n = 0; n < processlist.Count; n++)
            {
                Model.process p = processlist[n] as Model.process;

                if (n == (processlist.Count - 1)) { s += p.processname; }
                else
                {
                    s += p.processname + " → ";
                }
            }
            labelview.Text = s;

        }

        List<Model.process> processlist = new List<Book.Model.process>();

        private void getprocessnext(Model.process p)
        {
            Model.process nextp = null;
            if (p != null)
            {
                processlist.Add(p);
                this.bindingSource1.Add(p);
                if (p.processType != "结束")
                {

                    nextp = processmanage.GetProcessbyid(p.Processnex);
                    // processlist.Add(nextp);
                    getprocessnext(nextp);

                }
                else
                {
                    return;
                }
            }


        }

        /// <summary>
        /// delete process of the workflow event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            Model.process currentprocess = bindingSource1.Current as Model.process;
            if (currentprocess == null) return;
            if (currentprocess.processType == "中间")
            {
                Model.process preprocess = processmanage.GetProcessbyid(currentprocess.Processpre);
                Model.process nextprocess = processmanage.GetProcessbyid(currentprocess.Processnex);
                preprocess.Processnex = nextprocess.processId;
                nextprocess.Processpre = preprocess.processId;

                foreach (Model.wfrecord wfrecord in (new BL.wfrecordManager()).Select())
                {

                    if (wfrecord.nowprocessid == currentprocess.processId)
                    {
                        (new BL.wfrecordManager()).Delete(wfrecord.wfrecordId);
                    }
                }


                foreach (Model.accepterattrib acc in acceptermanage.Select())
                {
                    if (acc.processId == currentprocess.processId)
                    {
                        acceptermanage.Delete(acc.accepterattribID);
                    }
                }

                processmanage.Delete(currentprocess.processId);
                processmanage.Update(preprocess);
                processmanage.Update(nextprocess);

                loadprocess();

            }
            else
            {
                MessageBox.Show("不能删除");
            }



        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            //Model.process prcoess = bindingSource1.Current as Model.process;
            //if (prcoess != null)
            //{

            //    ProcessEdit processedit = new ProcessEdit(prcoess.processId, "update");
            //    if (processedit.ShowDialog(this) == DialogResult.OK)
            //    {
            //        loadprocess();
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.action == "view") return;

            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {

                Model.process prcoess = bindingSource1.Current as Model.process;
                if (prcoess != null)
                {
                    ProcessEdit processedit = new ProcessEdit(prcoess.processId, "update");
                    if (processedit.ShowDialog(this) == DialogResult.OK)
                    {
                        loadprocess();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}