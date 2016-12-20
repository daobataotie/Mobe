using System;
using System.Collections.Generic;
using System.Text;
//------------------------------------------------------------------------------
//
// 说明：表单插入工作流 接口类
//
// author: 徐彦飞
// create date：2009-12-18 上午 11:45:01
//
//------------------------------------------------------------------------------
namespace Book.UI
{
   public  class Workflowinsert
    {

       /// <summary>
       /// 表单管理实例
       /// </summary>
       BL.TablesManager tablesManager = new Book.BL.TablesManager();
       BL.WorkflowManager workflowm = new Book.BL.WorkflowManager();
       BL.wfrecordManager wfrecordMana = new BL.wfrecordManager();
       BL.wfrecordlogManager wfrecordlogMana = new BL.wfrecordlogManager();
       //private bool checkTables(string tablecode)
       //{
       //    Model.Tables t = null;
       //    t= tablesm.GetIDbyname(tablecode);
       //    return (tablesm != null) ? true : false;
       //}

       private Model.Tables GetTablesbycode(string tablecode)
       {
           return tablesManager.GetIDbycode(tablecode);                      
       }

       /// <summary>
       /// 根据tablecode查寻 wflow
       /// </summary>
       /// <param name="tablecode"></param>
       /// <returns></returns>
       public  Model.Workflow GetWorkflowbytablecode(string tablecode)
       {
           Model.Workflow wf = null;
           Model.Tables tables = GetTablesbycode(tablecode);
           if (tables!=null)
           {
               wf = workflowm.getWfbytable(tables.TablesID);
           }

           return wf;
       }

      public  bool Checkwfbytablescode(string tablecode)
       {
          
           Model.Tables tables = GetTablesbycode(tablecode);
           if (tables==null)   return false;
           return workflowm.getWfbytable(tables.TablesID) != null ? true : false;

       }
       /// <summary>
      /// 添加执行记录wfrecord
       /// </summary>
       /// <param name="tablecode">表名</param>
       /// <param name="wfreordname">单据主键</param>
       /// <param name="addname">单据名称</param>
       /// <returns></returns>
      public bool insertwfrecord(string tablecode, string wfreordname,string addname)
      {
          bool result = false;
         // if (Checkwfbytablescode(tablecode))
          {
              Model.wfrecord wfr = new Book.Model.wfrecord();
              wfr.wfrecordId = Guid.NewGuid().ToString();
              wfr.wfrecordname = wfreordname;
              wfr.WorkflowId = (GetWorkflowbytablecode(tablecode)).WorkflowId;
              wfr.applyuserid = BL.V.ActiveOperator.OperatorsId;
              wfr.allstate = (int)global::Helper.InvoiceAudit.WaitAudit;
              wfr.dealsign = "申请添加 " + addname;
              wfr.firsttime = DateTime.Now;
           
            //  wfr.

              BL.ProcessManager procm = new Book.BL.ProcessManager();
              string beginid = "";
              foreach (Model.process p in procm.SelectProcessbywf( wfr.WorkflowId ))
              {
                  if ( p.processType == "开始")
                  {
                      wfr.processId = p.processId;
                      beginid = p.Processnex;
                      break;
                  }
              }
               
              if (!string.IsNullOrEmpty(beginid))
              {
                  wfr.nowprocessid = beginid;
                  Model.wfrecordlog wflog = new Book.Model.wfrecordlog();
                  wflog.wfrecordlogid = Guid.NewGuid().ToString();
                  wflog.wfrecordId = wfr.wfrecordId;
                  wflog.logid = BL.V.ActiveOperator.OperatorsId;
                  wflog.logtype = "添加";
                  try
                  {
                      wfrecordMana.Insert(wfr);
                      wfrecordlogMana.Insert(wflog);
                      result = true;
                  }
                  catch {
                      //(new BL.wfrecordlogManager()).Delete(wflog.wfrecordlogid);
                      //(new BL.wfrecordManager()).Delete(wfr.wfrecordId);
                      result = false;
                  }
              }
          }

          return result;
      }



    }
}
