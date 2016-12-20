//------------------------------------------------------------------------------
//
// file name：ProceduresPriceManager.cs
// author: mayanjun
// create date：2011-3-31 10:22:30
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProceduresPrice.
    /// </summary>
    public partial class ProceduresPriceManager:BaseManager
    {
		
		/// <summary>
		/// Delete ProceduresPrice by primary key.
		/// </summary>
		public void Delete(string bomId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(bomId);
		}

		/// <summary>
		/// Insert a ProceduresPrice.
		/// </summary>
        public void Insert(Model.ProceduresPrice proceduresPrice)
        {
			//
			// todo:add other logic here
            // 
            Validate(proceduresPrice);
            proceduresPrice.InsertTime = DateTime.Now;
            proceduresPrice.ProceduresPriceId = Guid.NewGuid().ToString();
            accessor.Insert(proceduresPrice);
        }
		
		/// <summary>
		/// Update a ProceduresPrice.
		/// </summary>
        public void Update(Model.ProceduresPrice proceduresPrice)
        {
			//
			// todo: add other logic here.
			//
            Validate(proceduresPrice);
            proceduresPrice.UpdateTime = DateTime.Now;
            accessor.Update(proceduresPrice);
        }
        private void Validate(Model.ProceduresPrice proceduresPrice)
        {
            if (string.IsNullOrEmpty(proceduresPrice.SupplierId))
            {
                throw new Helper.RequireValueException(Model.ProceduresPrice.PRO_SupplierId);
            }
            if (string.IsNullOrEmpty(proceduresPrice.BomId))
            {
                throw new Helper.RequireValueException(Model.ProceduresPrice.PRO_BomId);
            }
            if (string.IsNullOrEmpty(proceduresPrice.ProceduresId))
            {
                throw new Helper.RequireValueException(Model.ProceduresPrice.PRO_ProceduresId);
            }
        }
    }
}

