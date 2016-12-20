//------------------------------------------------------------------------------
//
// file name：processAccessor.cs
// author: peidun
// create date：2009-11-18 15:33:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of process
    /// </summary>
    public partial class processAccessor : EntityAccessor, IprocessAccessor
    {
        public IList<Model.process> SelectProcessbywf(string wfid)
        {
            return sqlmapper.QueryForList<Model.process>("process.select_by_wfid", wfid);
        }

        public Model.process SelectProcessbyname(string name)
        {

            return sqlmapper.QueryForObject<Model.process>("process.select_by_name", name);
        }

        /// <summary>
        /// 获取该过程的上一过程
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        public Model.process getpreprocess(string processid)
        {

            return sqlmapper.QueryForObject<Model.process>("process.selectpre_by_id", processid);
        }
        /// <summary>
        /// 获取该过程的下一过程
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        public Model.process GetNextProcess(string processid)
        {
            return sqlmapper.QueryForObject<Model.process>("process.selectnext_by_id", processid);
        }

        /// <summary>
        /// 获取开始过程
        /// </summary>
        /// <param name="wfid"></param>
        /// <returns></returns>
        public Model.process GetBeginProcess(string wfid)
        {
            return sqlmapper.QueryForObject<Model.process>("process.selectbegin_by_wfid", wfid);
      
        }

        Book.Model.process IprocessAccessor.GetProcess(string processid)
        {
            return sqlmapper.QueryForObject<Model.process>("process.select_by_id", processid);
       
        }
      
    

}
}
