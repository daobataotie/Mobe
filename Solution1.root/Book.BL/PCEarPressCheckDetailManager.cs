//------------------------------------------------------------------------------
//
// file name：PCEarPressCheckDetailManager.cs
// author: mayanjun
// create date：2013-08-23 16:50:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarPressCheckDetail.
    /// </summary>
    public partial class PCEarPressCheckDetailManager
    {
		
		/// <summary>
		/// Delete PCEarPressCheckDetail by primary key.
		/// </summary>
		public void Delete(string pCEarPressCheckDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pCEarPressCheckDetailId);
		}

		/// <summary>
		/// Insert a PCEarPressCheckDetail.
		/// </summary>
        public void Insert(Model.PCEarPressCheckDetail pCEarPressCheckDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pCEarPressCheckDetail);
        }
		
		/// <summary>
		/// Update a PCEarPressCheckDetail.
		/// </summary>
        public void Update(Model.PCEarPressCheckDetail pCEarPressCheckDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(pCEarPressCheckDetail);
        }

        internal IList<Book.Model.PCEarPressCheckDetail> SelectByPCEarPressCheckId(string pCEarPressCheckId)
        {
            return accessor.SelectByPCEarPressCheckId(pCEarPressCheckId);
        }

        internal void DeleteByPCEarPressCheckId(string pCEarPressCheckId)
        {
            accessor.DeleteByPCEarPressCheckId(pCEarPressCheckId);
        }
    }
}

