//------------------------------------------------------------------------------
//
// file name：wfrecordManager.cs
// author: peidun
// create date：2009-11-18 15:33:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.wfrecord.
    /// </summary>
    public partial class wfrecordManager : BaseManager
    {

        /// <summary>
        /// Delete wfrecord by primary key.
        /// </summary>
        public void Delete(string wfrecordid)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(wfrecordid);
        }

        /// <summary>
        /// Insert a wfrecord.
        /// </summary>
        public void Insert(Model.wfrecord wfrecord)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(wfrecord);
        }

        /// <summary>
        /// Update a wfrecord.
        /// </summary>
        public void Update(Model.wfrecord wfrecord)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(wfrecord);
        }
        /// <summary>
        /// 待我审批
        /// </summary>
        /// <param name="operators"></param>
        /// <returns></returns>
        public IList<Model.wfrecord> GetMyexaming(Model.Operators operators, DateTime startdate, DateTime enddate)
        {
            return accessor.GetMyexaming(operators,startdate,enddate);
        }
        ///// <summary>
        ///// 我的申请
        ///// </summary>
        ///// <param name="operators"></param>
        ///// <returns></returns>
        //public IList<Model.wfrecord> GetWfrcordbyoperator(Model.Operators operators)
        //{
        //    return accessor.GetWfrcordbyoperator(operators);    
        //}
        /// <summary>
        /// 根据表名 主键查询
        /// </summary>
        /// <param name="code">表名</param>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public Model.wfrecord GetByTableCodeAndKeyId(string code, string id)
        {
            return accessor.GetByTableCodeAndKeyId(code, id);
        }
        /// <summary>
        ///  //根据表名 主键 操作员查询 当前过程有审核权限
        /// </summary>
        /// <param name="code">表名</param>
        /// <param name="keyid">主键编号</param>
        /// <param name="opeid">操作员编号</param>
        /// <returns></returns>
        public bool HoldAudit(string code, string keyid, string opeid)
        {
            return accessor.HoldAudit(code, keyid, opeid);
        }
        public bool HoldGiveUpAudit(string code, string keyid, string opeid)
        {
            return accessor.HoldGiveUpAudit(code, keyid, opeid);
        }

    }
}

