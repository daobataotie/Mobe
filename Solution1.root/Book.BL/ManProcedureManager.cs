//------------------------------------------------------------------------------
//
// file name：ManProcedureManager.cs
// author: peidun
// create date：2009-12-9 9:32:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ManProcedure.
    /// </summary>
    public partial class ManProcedureManager:BaseManager
    {
		
		/// <summary>
		/// Delete ManProcedure by primary key.
		/// </summary>
		public void Delete(string manProcedureId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(manProcedureId);
		}

		/// <summary>
		/// Insert a ManProcedure.
		/// </summary>
        public void Insert(Model.ManProcedure manProcedure)
        {
			//
			// todo:add other logic here
			//
            if (string.IsNullOrEmpty(manProcedure.BomId))
            {
                throw new Helper.RequireValueException(Model.ManProcedure.PRO_BomId);
            }
            manProcedure.ManProcedureId = Guid.NewGuid().ToString();
            manProcedure.InsertTime = DateTime.Now;
            accessor.Insert(manProcedure);           
            
          
        }
		
		/// <summary>
		/// Update a ManProcedure.
		/// </summary>
        public void Update(Model.ManProcedure manProcedure)
        {
			//
			// todo: add other logic here.
			//
            manProcedure.UpdateTime = DateTime.Now;
            this.Delete(manProcedure.Bom);
            this.Insert(manProcedure);
          
        }
        public void Delete(Model.BomParentPartInfo bom)
        {
            accessor.Delete(bom);
        }
        public Model.ManProcedure Select(Model.BomParentPartInfo bom, Model.Customer customer)
        {
            return accessor.Select(bom, customer);
        }
        /// <summary>
        /// 根据BOM,客户 ,产成品查询
        /// </summary>
        /// <param name="bom"></param>
        /// <param name="customer"></param>
        /// <param name="MadeProduct"></param>
        /// <returns></returns>
        public Model.ManProcedure Select(Model.BomParentPartInfo bom, Model.Customer customer, Model.Product MadeProduct)
        {
            return accessor.Select(bom, customer,MadeProduct);
        }
   
    }
}

