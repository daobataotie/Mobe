//------------------------------------------------------------------------------
//
// file name：PronoteProceduresAbilityDetailManager.cs
// author: mayanjun
// create date：2010-9-23 14:25:23
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PronoteProceduresAbilityDetail.
    /// </summary>
    public partial class PronoteProceduresAbilityDetailManager
    {
		
		/// <summary>
		/// Delete PronoteProceduresAbilityDetail by primary key.
		/// </summary>
		public void Delete(string pronoteProceduresAbilityDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pronoteProceduresAbilityDetailId);
		}

		/// <summary>
		/// Insert a PronoteProceduresAbilityDetail.
		/// </summary>
        public void Insert(Model.PronoteProceduresAbilityDetail pronoteProceduresAbilityDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pronoteProceduresAbilityDetail);
        }
		
		/// <summary>
		/// Update a PronoteProceduresAbilityDetail.
		/// </summary>
        public void Update(Model.PronoteProceduresAbilityDetail pronoteProceduresAbilityDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(pronoteProceduresAbilityDetail);
        }
        public IList<Model.PronoteProceduresAbilityDetail> GetByHeader(Model.PronoteProceduresAbility header)
        {
            return  accessor.GetByHeader(header);
        }
    }
}

