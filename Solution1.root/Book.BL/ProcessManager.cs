//------------------------------------------------------------------------------
//
// file name：processManager.cs
// author: peidun
// create date：2009-11-18 15:33:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.process.
    /// </summary>
    public partial class ProcessManager
    {

        /// <summary>
        /// Delete process by primary key.
        /// </summary>
        public void Delete(string processid)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(processid);
        }

        /// <summary>
        /// Insert a process.
        /// </summary>
        public void Insert(Model.process process)
        {
            //
            // todo:add other logic here
            //
            Validate(process);
            accessor.Insert(process);
        }

        /// <summary>
        /// Update a process.
        /// </summary>
        public void Update(Model.process process)
        {
            //
            // todo: add other logic here.
            //
            Validate(process);
            accessor.Update(process);
        }


        public IList<Model.process> SelectProcessbywf(string wfid)
        {
            return accessor.SelectProcessbywf(wfid);
        }
        /// <summary>
        /// 根据名称获取过程
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Model.process SelectProcessbyname(string name)
        {
            return accessor.SelectProcessbyname(name);
        }

        /// <summary>
        /// 获取该过程的上一过程
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        public Model.process getpreprocess(string processid)
        {
            return accessor.getpreprocess(processid);

        }
        /// <summary>
        /// 根据编号获取过程
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        public Model.process GetProcessbyid(string processid)
        {
            return accessor.GetProcess(processid);
        }
        /// <summary>
        /// 获取开始过程
        /// </summary>
        /// <param name="wfid"></param>
        /// <returns></returns>
        public Model.process GetBeginProcess(string wfid)
        {
            return accessor.GetBeginProcess(wfid);
        }
        private void Validate(Model.process process)
        {
            if (string.IsNullOrEmpty(process.processname))
            {
                throw new Helper.RequireValueException(Model.process.PRO_processname);
            }

            if (string.IsNullOrEmpty(process.andrule))
            {
                throw new Helper.RequireValueException(Model.process.PRO_andrule);
            }


        }
    }
}

