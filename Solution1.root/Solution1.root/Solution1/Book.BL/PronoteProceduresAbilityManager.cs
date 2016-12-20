//------------------------------------------------------------------------------
//
// file name：PronoteProceduresAbilityManager.cs
// author: mayanjun
// create date：2010-9-23 14:25:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PronoteProceduresAbility.
    /// </summary>
    public partial class PronoteProceduresAbilityManager
    {
        private static readonly DA.IPronoteProceduresAbilityDetailAccessor DetailAccessor = (DA.IPronoteProceduresAbilityDetailAccessor)Accessors.Get("PronoteProceduresAbilityDetailAccessor");
		
		/// <summary>
		/// Delete PronoteProceduresAbility by primary key.
		/// </summary>
		public void Delete(string pronoteProceduresAbilityId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pronoteProceduresAbilityId);
		}

		/// <summary>
		/// Insert a PronoteProceduresAbility.
		/// </summary>
        public void Insert(Model.PronoteProceduresAbility pronoteProceduresAbility)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pronoteProceduresAbility);
            foreach (Model.PronoteProceduresAbilityDetail detail in pronoteProceduresAbility.Details)
            {
                if (string.IsNullOrEmpty(detail.ProductId)) continue;
                detail.PronoteProceduresAbilityId = pronoteProceduresAbility.PronoteProceduresAbilityId;
                DetailAccessor.Insert(detail);
            }
        }
		
		/// <summary>
		/// Update a PronoteProceduresAbility.
		/// </summary>
        public void Update(Model.PronoteProceduresAbility pronoteProceduresAbility)
        {
			//
			// todo: add other logic here.
			//
            //accessor.Update(pronoteProceduresAbility);
            accessor.Delete(pronoteProceduresAbility.PronoteProceduresAbilityId);
            accessor.Insert(pronoteProceduresAbility);
        }
        public Model.PronoteProceduresAbility GetDetail(string id )
        {
            Model.PronoteProceduresAbility PronoteProceduresAbility = accessor.Get(id);
            if (PronoteProceduresAbility!=null)
            PronoteProceduresAbility.Details= DetailAccessor.GetByHeader(PronoteProceduresAbility);
            return PronoteProceduresAbility;
           
        }
        public Model.PronoteProceduresAbility GetByProcedures(string proceduresId)
        {
            return accessor.GetByProcedures(proceduresId);
        }
    }
}

