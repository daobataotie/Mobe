//------------------------------------------------------------------------------
//
// file name：WorkflowManager.cs
// author: peidun
// create date：2009-11-18 15:33:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Workflow.
    /// </summary>
    public partial class WorkflowManager : BaseManager
    {
        private BL.ProcessManager processmanage = new Book.BL.ProcessManager();
        BL.TablesManager tableM = new Book.BL.TablesManager();
        List<Model.Tables> tablelist = new List<Book.Model.Tables>();
        /// <summary>
        /// Delete Workflow by primary key.
        /// </summary>
        public void Delete(Model.Workflow workflow)
        {
            //
            // todo:add other logic here
            //

            accessor.Delete(workflow.WorkflowId);
        }

        /// <summary>
        /// Insert a Workflow.
        /// </summary>
        public void Insert(Model.Workflow workflow)
        {
            //
            // todo:add other logic here
            //

            Validate(workflow);
            workflow.WorkflowId = Guid.NewGuid().ToString();
            workflow.InsertTime = DateTime.Now;
            accessor.Insert(workflow);
            addprocess(workflow.WorkflowId);
        }


        /// <summary>
        /// 初始化添加过程(在添加流程的同时添加开始结束过程)
        /// </summary>
        private void addprocess(string wfid)
        {
            Model.process beginprocess = new Book.Model.process();//开始过程
            beginprocess.processId = Guid.NewGuid().ToString();
            Model.process finishprocess = new Book.Model.process();//结束过程
            finishprocess.processId = Guid.NewGuid().ToString();
            #region add begin process


            beginprocess.WorkflowId = wfid;
            beginprocess.descript = "开始";
            beginprocess.number = 0;
            beginprocess.andrule = "全拒绝";
            beginprocess.processname = "开始";
            beginprocess.Processnex = finishprocess.processId;
            beginprocess.processType = "开始";

            processmanage.Insert(beginprocess);
            #endregion

            #region add finish process



            finishprocess.WorkflowId = wfid;
            finishprocess.Processpre = beginprocess.processId;
            finishprocess.processname = "结束";
            finishprocess.number = 0;
            finishprocess.andrule = "全拒绝";
            finishprocess.processType = "结束";
            finishprocess.descript = "结束";

            processmanage.Insert(finishprocess);
            #endregion
        }
        /// <summary>
        /// Update a Workflow.
        /// </summary>
        public void Update(Model.Workflow workflow)
        {
            //
            // todo: add other logic here.
            //
            Validate(workflow);
            workflow.UpdateTime = DateTime.Now;
            accessor.Update(workflow);
        }
        /// <summary>
        ///根据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool getTable(string id)
        {
            return accessor.getTable(id);
        }
        /// <summary>
        /// 根据表单编号获取工作流程
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public Model.Workflow getWfbytable(string tableid)
        {
            return accessor.getWfbytable(tableid);
        }

        private void Validate(Model.Workflow Workflow)
        {
            if (string.IsNullOrEmpty(Workflow.workflowname))
            {
                throw new Helper.RequireValueException(Model.Workflow.PRO_workflowname);
            }
            //if (string.IsNullOrEmpty(Workflow.TablesID))
            //{
            //    throw new Helper.InvalidValueException(Model.Workflow.PROPERTY_TABLESID);
            //}


        }



        ///// <summary>
        ///// 判断该表单是否可以被使用
        ///// </summary>
        ///// <returns></returns>
        //private bool Checktableuser()
        //{
        //    bool checkresult = false;
        //    Model.Workflow wflow = null;
        //    try
        //    {
        //        wflow = workflowm.getWfbytable(workflow.TablesID);
        //    }
        //    catch { wflow = null; }

        //    if (wflow != null) { checkresult = true; }
        //    return checkresult;

        //}
        /// <summary>
        /// 判讀該表單是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool checektable(string name)
        {
            Model.Tables tables = null;
            bool checkresult = true;
            try
            {
                tables = this.getTablebyname(name);
                if (tables == null) { checkresult = false; }
            }
            catch { checkresult = false; }

            return checkresult;

        }
        /// <summary>
        /// 根据名称获取表单
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Model.Tables getTablebyname(string name)
        {
            Model.Tables tables = null;

            foreach (Model.Tables tab in tablelist)
            {
                if (tab.Tablename == name)
                {
                    tables = tab;
                }
            }



            return tables;
        }




    }
}

