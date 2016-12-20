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

namespace Book.UI.Settings.ProduceManager.Techonlogy
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾           完成时间:2009-11-8
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ProceduresEditForm : Settings.BasicData.BaseEditForm
    {
        BL.PronoteMachineManager machineManager = new Book.BL.PronoteMachineManager();
        Model.Procedures procedures = new Book.Model.Procedures();
        Model.WorkHouse WorkHouse = new Book.Model.WorkHouse();
        BL.ProceduresManager proceduresManager = new Book.BL.ProceduresManager();
        BL.WorkHouseManager workHouseManager = new Book.BL.WorkHouseManager();
        IList<Model.PronoteMachine> _selectItems = new List<Model.PronoteMachine>();
        private BL.ProceduresMachineManager proceManager = new BL.ProceduresMachineManager();
        private IList<Model.PronoteMachine> pronoteMachines = new List<Model.PronoteMachine>();
        BL.PronoteMachineManager pronoteMachineManager = new BL.PronoteMachineManager();
        BL.ProcessCategoryManager processCategoryManager = new Book.BL.ProcessCategoryManager();

        #region 构造函数
        public ProceduresEditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.Procedures.PRO_Id, new AA(Properties.Resources.RequireDataForId, this.textProcedureid));
            this.requireValueExceptions.Add(Model.Procedures.PRO_Procedurename, new AA(Properties.Resources.RequireDataForNames, this.richTextBoxName));
            this.invalidValueExceptions.Add(Model.Procedures.PRO_Id, new AA(Properties.Resources.EntityExists, this.textProcedureid));

            this.textWorkhouseid.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.newChooseContorl1.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.action = "insert";
        }
        public ProceduresEditForm(Model.Procedures procedures)
        {
            this.procedures = procedures;
            this.action = "update";
        }
        public ProceduresEditForm(Model.Procedures procedures, string action)
        {
            this.procedures = procedures;
            this.action = action;
        }
        #endregion

        #region 重写方法
        protected override void AddNew()
        {
            this.procedures = new Model.Procedures();
            if (WorkHouse != null)
            {
                this.textWorkhouseid.EditValue = WorkHouse;
            }
          
        }
        protected override void Save()
        {
            this.procedures.Id = this.textProcedureid.Text;
            this.procedures.WorkHouse = this.textWorkhouseid.EditValue as Model.WorkHouse;
            if (this.procedures.WorkHouse != null)
                this.procedures.WorkHouseId = this.procedures.WorkHouse.WorkHouseId;
            this.procedures.Procedurename = this.richTextBoxName.Rtf;

            if (this.lookUpEditProcessCate.EditValue != null)
                this.procedures.ProcessCategory = this.processCategoryManager.Get(this.lookUpEditProcessCate.EditValue.ToString());
            if (this.procedures.ProcessCategory != null)
                this.procedures.ProcessCategoryId = this.procedures.ProcessCategory.ProcessCategoryId;
            if (this.comboBoxProceduresate.EditValue != null)
            {
                this.procedures.Proceduresate = this.comboBoxProceduresate.EditValue.ToString();
            }
            this.procedures.Supplier = this.newChooseContorl1.EditValue as Model.Supplier;
            if (this.procedures.Supplier != null)
            {
                this.procedures.SupplierId = this.procedures.Supplier.SupplierId;
            }
            if (this.checkEdit1.Checked == true)
            {
                this.procedures.IsOtherProduceOther = true;
            }
            else
            {
                this.procedures.IsOtherProduceOther = false;
            }
            //this.procedures.ProcedureType = this.textProcedureType.Text;

            //if (global::Helper.DateTimeParse.DateTimeEquls(this.dateStartdate.DateTime, new DateTime()))
            //{
            //    this.procedures.Startdate = global::Helper.DateTimeParse.NullDate;
            //}
            //else
            //{
            //    this.procedures.Startdate = this.dateStartdate.DateTime;
            //}
            //if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEnddate.DateTime, new DateTime()))
            //{
            //    this.procedures.Enddate = global::Helper.DateTimeParse.EndDate;
            //}
            //else
            //{
            //    this.procedures.Enddate = this.dateEnddate.DateTime;
            //}
            //this.procedures.Leadtime = Convert.ToInt32(this.calcLeadtime.EditValue);
            this.procedures.Proceduredescription = this.textEditDescription.Text.ToString();
            //this.procedures.PronoteMachineId = this.buttonEditMachine.Text;
            switch (this.action)
            {
                case "insert":
                    this.proceduresManager.Insert(this.procedures);
                    break;
                case "update":
                    this.proceduresManager.Update(this.procedures);
                    break;
                default:
                    break;
            }
            flag1 = 1;
            band();
        }
        int flag = 0;
        public override void Refresh()
        {
            flag1 = 0;
            if (this.procedures == null)
            {
                this.procedures = new Book.Model.Procedures();
                this.action = "insert";
            }


            if (WorkHouse != null)
            {
                this.bindingSource1.DataSource = this.proceduresManager.Select(WorkHouse.WorkHouseId);
            }
            if (flag == 0)
            {
                this.textProcedureid.Text = this.procedures.Id;
                this.textWorkhouseid.EditValue = this.procedures.WorkHouse;



                this.richTextBoxName.Rtf = this.procedures.Procedurename;
                this.lookUpEditProcessCate.EditValue = this.procedures.ProcessCategoryId;
                if (this.procedures.Proceduresate != null)
                {
                    this.comboBoxProceduresate.EditValue = this.procedures.Proceduresate;
                }
                this.newChooseContorl1.EditValue = this.procedures.Supplier;
                if (this.procedures.IsOtherProduceOther == true)
                {
                    this.checkEdit1.Checked = true;
                }
                else
                {
                    this.checkEdit1.Checked = false;
                }
                // this.textProcedureType.Text = this.procedures.ProcedureType;
                //if (global::Helper.DateTimeParse.DateTimeEquls(this.procedures.Startdate, global::Helper.DateTimeParse.NullDate))
                //{
                //    this.dateStartdate.EditValue = null;
                //}
                //else
                //{
                //    this.dateStartdate.EditValue = this.procedures.Startdate;
                //}
                //if (global::Helper.DateTimeParse.DateTimeEquls(this.procedures.Enddate, global::Helper.DateTimeParse.NullDate))
                //{
                //    this.dateEnddate.EditValue = null;
                //}
                //else
                //{
                //    this.dateEnddate.EditValue = this.procedures.Enddate;
                //}
                //this.calcLeadtime.EditValue = this.procedures.Leadtime;
                this.textEditDescription.Text = this.procedures.Proceduredescription;

                //this.pronoteMachines.Clear();
                //string text = string.Empty;
                //this.pronoteMachines = this.pronoteMachineManager.SelectMachineByProduresId(this.procedures.ProceduresId);
                //if (this.pronoteMachines.Count != 0)
                //{
                //    foreach (Model.PronoteMachine pro in this.pronoteMachines)
                //    {
                //        text += pro.PronoteMachineName + ",";
                //    }
                //    if (text.Length > 0)
                //        text = text.Substring(0, text.Length - 1);
                //}
                //this.buttonEditMachine.Text = text;
            }
            switch (this.action)
            {
                case "insert":
                    this.textProcedureid.Properties.ReadOnly = false;
                    // this.textProcedurename.Properties.ReadOnly = false;
                    this.comboBoxProceduresate.Properties.ReadOnly = false;
                    this.comboBoxProceduresate.Properties.Buttons[0].Enabled = true;
                    //this.textProcedureType.Properties.ReadOnly = false;
                    //this.dateStartdate.Properties.ReadOnly = false;
                    //this.dateEnddate.Properties.ReadOnly = false;
                    //this.calcLeadtime.Properties.ReadOnly = false;
                    this.textEditDescription.Properties.ReadOnly = false;
                    this.textWorkhouseid.ButtonReadOnly = true;
                    this.textWorkhouseid.ShowButton = false;
                    this.checkEdit1.Properties.ReadOnly = false;
                    //this.lookUpEditPronoteMachineId.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.textProcedureid.Properties.ReadOnly = false;
                    // this.textProcedurename.Properties.ReadOnly = false;
                    this.comboBoxProceduresate.Properties.ReadOnly = false;
                    this.comboBoxProceduresate.Properties.Buttons[0].Enabled = true;
                    //this.textProcedureType.Properties.ReadOnly = false;
                    // this.dateStartdate.Properties.ReadOnly = false;
                    // this.dateEnddate.Properties.ReadOnly = false;
                    // this.calcLeadtime.Properties.ReadOnly = false;
                    this.textEditDescription.Properties.ReadOnly = false;
                    this.textWorkhouseid.ButtonReadOnly = true;
                    this.textWorkhouseid.ShowButton = false;
                    this.checkEdit1.Properties.ReadOnly = false;
                    //this.lookUpEditPronoteMachineId.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.textProcedureid.Properties.ReadOnly = true;
                    // this.textProcedurename.Properties.ReadOnly = true;
                    this.comboBoxProceduresate.Properties.ReadOnly = true;
                    this.comboBoxProceduresate.Properties.Buttons[0].Enabled = false;
                    //this.textProcedureType.Properties.ReadOnly = true;
                    //this.dateStartdate.Properties.ReadOnly = true;
                    // this.dateEnddate.Properties.ReadOnly = true;
                    //this.calcLeadtime.Properties.ReadOnly = true;
                    this.textEditDescription.Properties.ReadOnly = true;
                    this.textWorkhouseid.ButtonReadOnly = true;
                    this.textWorkhouseid.ShowButton = false;
                    this.checkEdit1.Properties.ReadOnly = true;
                    //this.lookUpEditPronoteMachineId.Properties.ReadOnly = true;

                    break;
                default:
                    break;
            }
            base.Refresh();
            if (this.action == "insert")
            {
                this.newChooseContorl1.ButtonReadOnly = true;
                this.newChooseContorl1.ShowButton = false;
            }
        }
        protected override void Delete()
        {
            if (this.procedures == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.proceduresManager.Delete(this.procedures.ProceduresId);
                this.textProcedureid.Text = "";
                //this.textProcedurename.Text = "";
                this.comboBoxProceduresate.EditValue = "";
                //this.textProcedureType.Text = "";
                //this.dateStartdate.EditValue = "";
                //this.dateEnddate.EditValue = "";
                //this.calcLeadtime.EditValue = "";
                this.textEditDescription.Text = "";
                this.richTextBoxName.Text = "";
                this.lookUpEditProcessCate.EditValue = "";
                flag1 = 1;
                band();
                flag = 1;
            }
            catch
            {
                throw new Exception("");
            }

            return;

        }

        protected override void MoveFirst()
        {
            this.procedures = this.proceduresManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.Procedures procedures = this.proceduresManager.GetPrev(this.procedures);
            if (procedures == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.procedures = procedures;
        }
        protected override void MoveLast()
        {
            this.procedures = this.proceduresManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.Procedures procedures = this.proceduresManager.GetNext(this.procedures);
            if (procedures == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.procedures = procedures;
        }
        protected override bool HasRows()
        {
            return this.proceduresManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.proceduresManager.HasRowsAfter(this.procedures);
        }
        protected override bool HasRowsPrev()
        {
            return this.proceduresManager.HasRowsBefore(this.procedures);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.richTextBoxName, this.textEditDescription });
        }

        #endregion

        /// <summary>
        /// gridview单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.Procedures procedures = this.bindingSource1.Current as Model.Procedures;
                if (procedures != null)
                {
                    this.procedures = procedures;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
        int flag1 = 0;

        /// <summary>
        /// tree节点光标改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (flag1 == 0)
            {
                if (e.Node != null && e.Node.ParentNode == null)
                {
                    this.textProcedureid.Text = "";
                    // this.textProcedurename.Text = "";
                    this.comboBoxProceduresate.EditValue = "";
                    // this.textProcedureType.Text = "";
                    //this.dateStartdate.EditValue = "";
                    // this.dateEnddate.EditValue = "";
                    //this.calcLeadtime.EditValue = "";
                    this.textEditDescription.Text = "";
                    this.richTextBoxName.Text = "";
                    this.lookUpEditProcessCate.EditValue = null;

                    flag = 1;

                    this.WorkHouse = workHouseManager.Get(e.Node.Tag.ToString());
                    if (WorkHouse != null)
                    {
                        this.textWorkhouseid.EditValue = WorkHouse;
                    }
                    this.action = "insert";
                    this.Refresh();
                }

                if (e.Node != null && e.Node.ParentNode != null)
                {
                    Model.Procedures procedures = this.proceduresManager.Get(e.Node.Tag.ToString());
                    if (procedures != null)
                    {
                        this.procedures = procedures;
                        this.action = "view";
                        this.Refresh();
                    }
                }

                flag = 0;
            }
        }

        //窗体加载时
        private void ProceduresEditForm_Load(object sender, EventArgs e)
        {
            band();
            this.bindingSourceMachine.DataSource = machineManager.Select();
            this.bindingSourceProcessCate.DataSource = this.processCategoryManager.Select();
        }
        private void band()
        {
            this.treeList1.ClearNodes();
            foreach (Model.WorkHouse wh in workHouseManager.Select())
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { wh.Workhousename }, null, wh.WorkHouseId);

                foreach (Model.Procedures Procedures in this.proceduresManager.Select(wh.WorkHouseId))
                {

                    treeList1.AppendNode(new object[] { Procedures.Id }, treeNode, Procedures.ProceduresId);

                }


            }

        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseMachineForm cho = new ChooseMachineForm();
            if (cho.ShowDialog(this) == DialogResult.OK)
                this._selectItems = cho.SelectItem;
        }

        //private void buttonEditMachine_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    this.pronoteMachines = this.pronoteMachineManager.SelectMachineByProduresId(this.procedures.ProceduresId);
        //    ChooseMachineForm chooseMachine = new ChooseMachineForm(pronoteMachines, this.procedures.ProceduresId);
        //    if (chooseMachine.ShowDialog(this) == DialogResult.OK)
        //    {
        //        pronoteMachines.Clear();
        //        pronoteMachines = chooseMachine.SelectItem;
        //        string text = string.Empty;
        //        foreach (Model.PronoteMachine machine in pronoteMachines)
        //        {
        //            text += machine.PronoteMachineName + ",";
        //            Model.ProceduresMachine promachine = new Book.Model.ProceduresMachine();
        //            promachine.ProceduresMachineId = Guid.NewGuid().ToString();
        //            promachine.ProceduresId = this.procedures.ProceduresId;
        //            promachine.PronoteMachineId = machine.PronoteMachineId;
        //            this.proceManager.Insert(promachine);
        //        }
        //        if (text != "")
        //            this.buttonEditMachine.Text = text.Substring(0, text.Length - 1);
        //    }
        //}

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag != null)
            {
                if (e.Item.Tag.ToString() == "copy")
                {
                    if (this.procedures != null)
                    {
                        this.action = "insert";
                        this.procedures.ProceduresId = Guid.NewGuid().ToString();
                        this.procedures.Id = string.Empty;
                        this.Refresh();
                    }
                }
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEdit1.Checked == true)
            {
                this.newChooseContorl1.ButtonReadOnly = false;
                this.newChooseContorl1.ShowButton = true;
            }
            else
            {
                this.newChooseContorl1.ButtonReadOnly = true;
                this.newChooseContorl1.ShowButton = false;
                this.newChooseContorl1.EditValue = null;
            }
        }
    }
}