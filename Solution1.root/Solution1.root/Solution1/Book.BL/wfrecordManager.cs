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
    public partial class wfrecordManager:BaseManager
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
        public IList<Model.wfrecord> GetMyexaming(Model.Operators operators)
        {
            return accessor.GetMyexaming(operators);

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
  
   
       
    }
}

