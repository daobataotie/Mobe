using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList.Nodes;
//------------------------------------------------------------------------------
//
// 说明：   工艺部分
//
// author: 徐彦飞
// create date：2009-11-24 上午 11:45:01
//
//------------------------------------------------------------------------------
namespace Book.UI.Settings.ProduceManager.Techonlogy
{
    public partial class EidtForm : Settings.BasicData.BaseEditForm
    {
        int flag = 0;
        Model.TechonlogyHeader techonlogyHeader;
        BL.TechonlogyHeaderManager techonlogyHeaderManager = new Book.BL.TechonlogyHeaderManager();

        Model.Technologydetails techonlogydetails = new Book.Model.Technologydetails();
        BL.TechnologydetailsManager techonlogydetailsManager = new Book.BL.TechnologydetailsManager();

        Model.Procedures procedures = new Book.Model.Procedures();
        BL.ProceduresManager proceduresManager = new Book.BL.ProceduresManager();

        BL.WorkHouseManager wordhouseManager = new Book.BL.WorkHouseManager();

        public EidtForm()
        {
            //flag1 = 1;
            InitializeComponent();
            this.requireValueExceptions.Add(Model.TechonlogyHeader.PROPERTY_ID, new AA(Properties.Resources.RequireDataForId, this.textTechonlogyHeaderid));
            this.requireValueExceptions.Add(Model.TechonlogyHeader.PROPERTY_TECHONLOGYHEADERNAME, new AA(Properties.Resources.RequireDataForNames, this.textTechonlogyHeadername));
            this.invalidValueExceptions.Add(Model.TechonlogyHeader.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.textTechonlogyHeaderid));

            this.requireValueExceptions.Add(Model.Technologydetails.PROPERTY_TECHNOLOGYDETAILSNO, new AA(Properties.Resources.indexofbomNullDate, this.gridControl1 as Control));
            this.invalidValueExceptions.Add(Model.Technologydetails.PROPERTY_TECHNOLOGYDETAILSNO, new AA(Properties.Resources.IndexOfBom, this.gridControl1 as Control));

            this.action = "insert";
        }
        int LastFlag = 0;
        public EidtForm(string id)
            : this()
        {
            this.techonlogyHeader = this.techonlogyHeaderManager.GetDetail(id);

            this.bindingSourceProcedures.DataSource = this.proceduresManager.Select();
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EidtForm(Model.TechonlogyHeader techonlogyHeader)
            : this()
        {
            this.techonlogyHeader = techonlogyHeader;

            this.bindingSourceProcedures.DataSource = this.proceduresManager.Select();
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }
        public EidtForm(Model.TechonlogyHeader techonlogyHeader, string action)
            : this()
        {
            this.techonlogyHeader = techonlogyHeader;
            this.bindingSourceProcedures.DataSource = this.proceduresManager.Select();
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }
        #region Override
        int flag1 = 0;
        protected override void AddNew()
        {
            //flag1 = 1;
            this.techonlogyHeader = new Model.TechonlogyHeader();
            this.techonlogyHeader.TechonlogyHeaderId = Guid.NewGuid().ToString();
            this.techonlogyHeader.Id = this.techonlogyHeaderManager.GetId(DateTime.Now);
            this.techonlogyHeader.detail = new List<Model.Technologydetails>();

            //if (this.action == "insert")
            //{
            //    Model.Procedures detail = new Model.Procedures();

            //    detail.WorkHouse = new Model.WorkHouse();
            //    detail.ProceduresId = "";
            //    //detail.InvoiceCJDetailNote = "";
            //    //detail.InvoiceCJDetailPrice = 0;
            //    //detail.InvoiceCJDetailQuantity = 0;
            //    //detail.InvoiceProductUnit = "";
            //    //detail.Product = new Book.Model.Product();
            //    this.techonlogyHeader.detail.Add(detail);
            //    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            //}



        }



        protected override void Save()
        {

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            this.techonlogyHeader.Id = this.textTechonlogyHeaderid.Text;
            this.techonlogyHeader.TechonlogyHeadername = this.textTechonlogyHeadername.Text;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateStatrdate.DateTime, new DateTime()))
            {
                this.techonlogyHeader.Statrdate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.techonlogyHeader.Statrdate = this.dateStatrdate.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEnddate.DateTime, new DateTime()))
            {
                this.techonlogyHeader.Enddate = global::Helper.DateTimeParse.EndDate;
            }
            else
            {
                this.techonlogyHeader.Enddate = this.dateEnddate.DateTime;
            }
            this.techonlogyHeader.Techonlogydescription = this.textEditDescription.Text.ToString();
            if (this.comboBoxTechonlogyState.EditValue != null)
            {
                this.techonlogyHeader.TechonlogyState = this.comboBoxTechonlogyState.EditValue.ToString();
            }
            switch (this.action)
            {
                case "insert":
                    this.techonlogyHeaderManager.Insert(this.techonlogyHeader);
                    TreeListNode node = this.treeList1.AppendNode(new object[] { this.techonlogyHeader.TechonlogyHeadername }, null, this.techonlogyHeader.TechonlogyHeaderId);

                    foreach (Model.Procedures details in this.techonlogydetailsManager.DataReaderBind<Model.Procedures>("SELECT Id FROM Procedures WHERE Procedures.ProceduresId IN(SELECT ProceduresId FROM Technologydetails WHERE Technologydetails.TechonlogyHeaderid='" + this.techonlogyHeader.TechonlogyHeaderId + "')", null, CommandType.Text))
                    {
                        treeList1.AppendNode(new object[] { details.Id }, node, null);
                    }


                    #region MyRegion


                    //BL.TablesManager tablesm = new Book.BL.TablesManager();
                    //string talbesid = tablesm.GetIDbyname("工艺").TablesID;
                    //if (!string.IsNullOrEmpty(talbesid))
                    //{
                    //    BL.WorkflowManager wfm = new Book.BL.WorkflowManager();
                    //    string wfid = wfm.getWfbytable(talbesid).WorkflowId;
                    //    if (!string.IsNullOrEmpty(wfid))
                    //    {

                    //        Model.wfrecord wfr = new Book.Model.wfrecord();
                    //        wfr.wfrecordId = Guid.NewGuid().ToString();
                    //        wfr.wfrecordname = "申请添加 " + this.textTechonlogyHeadername.Text + "   工艺";
                    //        wfr.WorkflowId = wfid;
                    //        wfr.applyuserid = BL.V.ActiveOperator.OperatorsId;
                    //        wfr.allstate = "进行中";
                    //        wfr.dealsign = "申请添加 " + this.textTechonlogyHeadername.Text + "   工艺";
                    //        wfr.firsttime= DateTime.Today;

                    //        BL.processManager procm = new Book.BL.processManager();
                    //        string beginid = "";
                    //        foreach( Model.process p in procm.Select())
                    //        {
                    //            if (p.WorkflowId == wfid && p.processType == "开始")
                    //            {
                    //                beginid = p.Processnex;
                    //            }
                    //        }

                    //        if (!string.IsNullOrEmpty(beginid))
                    //        {
                    //            wfr.nowprocessid = beginid;

                    //            Model.wfrecordlog wflog = new Book.Model.wfrecordlog();
                    //            wflog.wfrecordlogid = Guid.NewGuid().ToString();
                    //            wflog.wfrecordId = wfr.wfrecordId;
                    //            wflog.logid = this.techonlogyHeader.TechonlogyHeaderid;
                    //            wflog.logtype = "添加";

                    //                (new BL.wfrecordManager()).Insert(wfr);
                    //                (new BL.wfrecordlogManager()).Insert(wflog);

                    //        }

                    //    }

                    //}
                    //else
                    //{ 
                    //// MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    //}

                    #endregion


                    //Workflowinsert wfinsert = new Workflowinsert();
                    //if (wfinsert.Checkwfbytablescode("TechonlogyHeader"))
                    //{
                    //    wfinsert.insertwfrecord("TechonlogyHeader", this.techonlogyHeader.TechonlogyHeaderId, "工艺 " + this.techonlogyHeader.TechonlogyHeadername);
                    //}
                    //else
                    //{
                    //}

                    break;
                case "update":
                    this.techonlogyHeaderManager.Update(this.techonlogyHeader);
                    this.treeList1.Selection[0].SetValue(this.treeListColumn, this.techonlogyHeader.TechonlogyHeadername);
                    this.treeList1.Selection[0].Nodes.Clear();
                    foreach (Model.Procedures details in this.techonlogydetailsManager.DataReaderBind<Model.Procedures>("SELECT Id FROM Procedures WHERE Procedures.ProceduresId IN(SELECT ProceduresId FROM Technologydetails WHERE Technologydetails.TechonlogyHeaderid='" + this.techonlogyHeader.TechonlogyHeaderId + "')", null, CommandType.Text))
                    {
                        treeList1.AppendNode(new object[] { details.Id }, this.treeList1.Selection[0], null);
                    }
                    break;
                default:
                    break;
            }
            //flag = 1;
            //band();
        }
        public override void Refresh()
        {

            //flag = 0;
            if (this.action == "view" && (treeList1.Selection[0] == null || treeList1.Selection[0].Tag == null || treeList1.Selection[0].Tag.ToString() != this.techonlogyHeader.TechonlogyHeaderId))
            {
                foreach (TreeListNode node in treeList1.Nodes)
                {
                    if (node.Tag.ToString() == this.techonlogyHeader.TechonlogyHeaderId)
                        treeList1.SetFocusedNode(node);
                }
                //treeList1.SetFocusedNode(treeList1.FindNodeByFieldValue("treeListColumn1", this.techonlogyHeader.TechonlogyHeadername));
            }
            if (this.techonlogyHeader == null)
            {
                this.techonlogyHeader = new Book.Model.TechonlogyHeader();
                this.techonlogyHeader.Id = this.techonlogyHeaderManager.GetId(DateTime.Now);
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.techonlogyHeader = this.techonlogyHeaderManager.GetDetail(this.techonlogyHeader.TechonlogyHeaderId);
            }

            this.bindingSource1.DataSource = this.techonlogyHeader.detail;
            this.textTechonlogyHeaderid.Text = this.techonlogyHeader.Id;
            this.textTechonlogyHeadername.Text = this.techonlogyHeader.TechonlogyHeadername;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.techonlogyHeader.Statrdate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateStatrdate.EditValue = null;
            }
            else
            {
                this.dateStatrdate.EditValue = this.techonlogyHeader.Statrdate;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.techonlogyHeader.Enddate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEnddate.EditValue = null;
            }
            else
            {
                this.dateEnddate.EditValue = this.techonlogyHeader.Enddate;
            }
            this.textEditDescription.Text = this.techonlogyHeader.Techonlogydescription;
            if (this.techonlogyHeader.TechonlogyState != null)
            {
                this.comboBoxTechonlogyState.EditValue = this.techonlogyHeader.TechonlogyState;
            }
            switch (this.action)
            {
                case "insert":
                    this.barButtonItem1.Enabled = false;
                    this.textTechonlogyHeaderid.Properties.ReadOnly = false;
                    this.textTechonlogyHeadername.Properties.ReadOnly = false;
                    this.textEditDescription.Properties.ReadOnly = false;
                    this.dateStatrdate.Properties.ReadOnly = false;
                    this.dateEnddate.Properties.ReadOnly = false;
                    this.dateStatrdate.Properties.Buttons[0].Enabled = true;
                    this.dateEnddate.Properties.Buttons[0].Enabled = true;
                    this.comboBoxTechonlogyState.Properties.ReadOnly = false;
                    this.comboBoxTechonlogyState.Properties.Buttons[0].Enabled = true;
                    this.simpleButton1.Enabled = true;
                    this.simpleButton2.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.repositoryItemLookUpEdit1.Buttons[0].Visible = true;
                    this.repositoryItemLookUpEdit2.Buttons[0].Visible = true;
                    break;
                case "update":
                    this.barButtonItem1.Enabled = false;
                    this.textTechonlogyHeaderid.Properties.ReadOnly = false;
                    this.textTechonlogyHeadername.Properties.ReadOnly = false;
                    this.textEditDescription.Properties.ReadOnly = false;
                    this.dateStatrdate.Properties.ReadOnly = false;
                    this.dateEnddate.Properties.ReadOnly = false;
                    this.dateStatrdate.Properties.Buttons[0].Enabled = true;
                    this.dateEnddate.Properties.Buttons[0].Enabled = true;
                    this.comboBoxTechonlogyState.Properties.ReadOnly = false;
                    this.comboBoxTechonlogyState.Properties.Buttons[0].Enabled = true;
                    this.simpleButton1.Enabled = true;
                    this.simpleButton2.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.repositoryItemLookUpEdit1.Buttons[0].Visible = true;
                    this.repositoryItemLookUpEdit2.Buttons[0].Visible = true;
                    break;

                case "view":
                    this.barButtonItem1.Enabled = true;
                    this.textTechonlogyHeaderid.Properties.ReadOnly = true;
                    this.textTechonlogyHeadername.Properties.ReadOnly = true;
                    this.textEditDescription.Properties.ReadOnly = true;
                    this.dateStatrdate.Properties.ReadOnly = true;
                    this.dateEnddate.Properties.ReadOnly = true;
                    this.dateStatrdate.Properties.Buttons[0].Enabled = false;
                    this.dateEnddate.Properties.Buttons[0].Enabled = false;
                    this.comboBoxTechonlogyState.Properties.ReadOnly = true;
                    this.comboBoxTechonlogyState.Properties.Buttons[0].Enabled = false;
                    this.simpleButton1.Enabled = false;
                    this.simpleButton2.Enabled = false;
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.repositoryItemLookUpEdit1.Buttons[0].Visible = false;
                    this.repositoryItemLookUpEdit2.Buttons[0].Visible = false;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        protected override void Delete()
        {
            if (this.techonlogyHeader == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {

                this.techonlogyHeaderManager.Delete(this.techonlogyHeader.TechonlogyHeaderId);
                this.techonlogyHeader = this.techonlogyHeaderManager.GetNext(this.techonlogyHeader);
                if (this.techonlogyHeader == null)
                {
                    this.techonlogyHeader = this.techonlogyHeaderManager.GetLast();
                }
                this.treeList1.DeleteSelectedNodes();
                // this.band();
            }
            catch
            {
                throw new Exception("");
            }

            return;

        }
        protected override void MoveFirst()
        {
            this.techonlogyHeader = this.techonlogyHeaderManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.TechonlogyHeader techonlogyHeader = this.techonlogyHeaderManager.GetPrev(this.techonlogyHeader);
            if (techonlogyHeader == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.techonlogyHeader = techonlogyHeader;
        }
        protected override void MoveLast()
        {
            this.techonlogyHeader = this.techonlogyHeaderManager.GetLast();
        }
        protected override void MoveNext()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            Model.TechonlogyHeader techonlogyHeader = this.techonlogyHeaderManager.GetNext(this.techonlogyHeader);
            if (techonlogyHeader == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.techonlogyHeader = techonlogyHeader;
        }
        protected override bool HasRows()
        {
            return this.techonlogyHeaderManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.techonlogyHeaderManager.HasRowsAfter(this.techonlogyHeader);
        }
        protected override bool HasRowsPrev()
        {
            return this.techonlogyHeaderManager.HasRowsBefore(this.techonlogyHeader);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textTechonlogyHeadername, this.textEditDescription });
        }
        #endregion

        //private void gridView1_Click(object sender, EventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
        //    if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
        //    {
        //        Model.TechonlogyHeader techonlogyHeader = this.bindingSource1.Current as Model.TechonlogyHeader;
        //        if (techonlogyHeader != null)
        //        {
        //            this.techonlogyHeader = techonlogyHeader;
        //            this.action = "view";
        //            this.Refresh();
        //        }
        //    } 
        //}
        int flog = 0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChooseProceduresForm f = new ChooseProceduresForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                flog = 1;

                this.bindingSourceWorkhouse.DataSource = this.wordhouseManager.Select();
                this.bindingSourceProcedures.DataSource = this.proceduresManager.Select();

                Model.Technologydetails detail = new Book.Model.Technologydetails();
                detail.Procedures = f.SelectedItem as Model.Procedures;
                if (detail.Procedures != null)
                {
                    detail.TechnologydetailsID = Guid.NewGuid().ToString();

                    detail.ProceduresId = detail.Procedures.ProceduresId;

                }
                detail.TechonlogyHeader = this.techonlogyHeader;
                detail.TechnologydetailsNo = GetNewTechnologydetailsNo().ToString();
                detail.TechonlogyHeaderId = this.techonlogyHeader.TechonlogyHeaderId;

                this.techonlogyHeader.detail.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.techonlogyHeader.detail.Remove(this.bindingSource1.Current as Book.Model.Technologydetails);

                this.gridControl1.RefreshDataSource();
            }
        }



        //private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    if (e.Column == this.gridWorkhouse)
        //    {
        //        Model.Technologydetails detail = this.gridView1.GetRow(e.RowHandle) as Model.Technologydetails;
        //        if (detail != null)
        //        {
        //            //Model.Procedures p = ;


        //            this.bindingSourceProcedures.DataSource = this.techonlogydetailsManager.Select(e.Value.ToString());
        //            detail.WorkHouse = this.wordhouseManager.Get(e.Value.ToString());
        //            detail.WorkHouseId = e.Value.ToString();



        //            //detail.Invoice = this.invoice;
        //            //detail.InvoiceCJDetailNote = "";
        //            //detail.InvoiceCJDetailQuantity = 1;
        //            //detail.Product = p;
        //            //detail.ProductId = p.ProductId;
        //            //detail.ProcedureType = p.DepotUnit.CnName;
        //            this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
        //        }
        //        this.gridControl1.RefreshDataSource();
        //    }
        //    if (e.Column == this.gridProcedures)
        //    {
        //        Model.Procedures detail = this.gridView1.GetRow(e.RowHandle) as Model.Procedures;
        //        if (detail != null)
        //        {
        //            Model.Procedures p = this.proceduresManager.Get(e.Value.ToString()); ;


        //            detail.ProceduresId = p.ProceduresId;
        //            detail.WorkHouse = p.WorkHouse;
        //            detail.Startdate = p.Startdate;
        //            detail.Enddate = p.Enddate;
        //            detail.Proceduredescription = p.Proceduredescription;
        //            detail.Procedurename = p.Procedurename;
        //            detail.Proceduresate = p.Proceduresate;
        //            //detail.Invoice = this.invoice;
        //            //detail.InvoiceCJDetailNote = "";
        //            //detail.InvoiceCJDetailQuantity = 1;
        //            //detail.Product = p;
        //            //detail.ProductId = p.ProductId;
        //            //detail.ProcedureType = p.DepotUnit.CnName;


        //            this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
        //        }
        //        this.gridControl1.RefreshDataSource();
        //    }
        //}

        private void EidtForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceWorkhouse.DataSource = this.wordhouseManager.Select();
            band();

        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                //if (e.KeyData == Keys.Enter)
                //{

                //    Model.Procedures detail = new Model.Procedures();
                //    detail.ProceduresId =null;

                //    this.techonlogyHeader.detail.Add(detail);
                //    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                //}

                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButton2.PerformClick();
                }
                this.gridControl1.RefreshDataSource();
            }
        }
        private void band()
        {
            this.treeList1.ClearNodes();
            foreach (Model.TechonlogyHeader techonlogy in techonlogyHeaderManager.DataReaderBind<Model.TechonlogyHeader>("SELECT TechonlogyHeaderid,TechonlogyHeadername FROM TechonlogyHeader order by TechonlogyHeadername ", null, CommandType.Text))
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { techonlogy.TechonlogyHeadername }, null, techonlogy.TechonlogyHeaderId);


                foreach (Model.Procedures details in this.techonlogydetailsManager.DataReaderBind<Model.Procedures>("SELECT Id FROM Procedures WHERE Procedures.ProceduresId IN(SELECT ProceduresId FROM Technologydetails WHERE Technologydetails.TechonlogyHeaderid='" + techonlogy.TechonlogyHeaderId + "')", null, CommandType.Text))
                {

                    treeList1.AppendNode(new object[] { details.Id }, treeNode, null);

                }


            }

        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (flag == 0)
            {
                //if (flag1 == 0)
                //{

                if (e.Node != null && e.Node.ParentNode == null)
                {
                    this.techonlogyHeader = techonlogyHeaderManager.GetDetail(e.Node.Tag.ToString());
                    this.action = "view";
                    this.Refresh();
                }
                if (e.Node != null && e.Node.ParentNode != null)
                {
                    flag = 1;
                    treeList1.SetFocusedNode(e.Node.ParentNode);
                }

                // }
                // flag1 = 0;
            }
            flag = 0;

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.Technologydetails> details = this.bindingSource1.DataSource as IList<Model.Technologydetails>;
            if (details == null || details.Count < 1) return;
            Model.Technologydetails detail = details[e.ListSourceRowIndex];
            if (detail.Procedures == null) return;
            switch (e.Column.Name)
            {
                case "gridWorkhouse":
                    e.DisplayText = detail.Procedures == null ? "" : detail.Procedures.WorkHouse.Workhousename;
                    break;
                case "gridProcedures":
                    e.DisplayText = detail.Procedures == null ? "" : detail.Procedures.Id;
                    break;
                case "gridColumnStartDate":
                    e.DisplayText = detail.Procedures.Startdate.HasValue ? detail.Procedures.Startdate.Value.ToShortDateString() : "";
                    break;
                case "gridColumnEndDate":
                    e.DisplayText = detail.Procedures.Enddate.HasValue ? detail.Procedures.Enddate.Value.ToShortDateString() : "";
                    break;


            }
        }
        private int GetNewTechnologydetailsNo()
        {
            int num = 0;
            foreach (Model.Technologydetails tec in this.techonlogyHeader.detail)
            {
                int temp = 0;
                if (tec.TechnologydetailsNo != null)
                    temp = Convert.ToInt32(tec.TechnologydetailsNo);
                if (temp > num)
                    num = temp;
            }
            return num + 1;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag != null)
            {
                if (e.Item.Tag.ToString() == "copy")
                {
                    if (this.techonlogyHeader != null)
                    {
                        this.action = "insert";
                        this.techonlogyHeader.TechonlogyHeaderId = Guid.NewGuid().ToString();

                        foreach (Model.Technologydetails item in this.techonlogyHeader.detail)
                        {
                            item.TechnologydetailsID = Guid.NewGuid().ToString();
                        }
                        this.techonlogyHeader.Id = string.Empty;
                        this.Refresh();
                    }
                }
            }
        }

        private void dateStatrdate_EditValueChanged(object sender, EventArgs e)
        {
            // if (this.action == "insert") { this.textTechonlogyHeaderid.EditValue = this.techonlogyHeaderManager.GetId(this.dateStatrdate.EditValue == null ? DateTime.Now : this.dateStatrdate.DateTime); }
        }

        private void dateStatrdate_Leave(object sender, EventArgs e)
        {
            // if (this.action == "insert") { this.textTechonlogyHeaderid.EditValue = this.techonlogyHeaderManager.GetId(this.dateStatrdate.EditValue == null ? DateTime.Now : this.dateStatrdate.DateTime); }
        }

        private void treeList1_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            //e.Menu.Tag
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {

        }

        private void treeList1_NodesReloaded(object sender, EventArgs e)
        {

        }

        private void treeList1_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            // treeList1.
            //string a=e.Node.Tag.ToString();

        }
        #region 审核

        protected override string AuditKeyId()
        {
            return Model.TechonlogyHeader.PROPERTY_TECHONLOGYHEADERID;
        }

        protected override int AuditState()
        {
            return 0;
        }

        //protected override string tableCode()
        //{
        //    return "TechonlogyHeader" + "," + this.techonlogyHeader.TechonlogyHeaderId;
        //}

        #endregion
    }
}