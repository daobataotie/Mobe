//------------------------------------------------------------------------------
//
// file name：ProceduresManager.cs
// author: peidun
// create date：2009-12-8 10:55:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Procedures.
    /// </summary>
    public partial class ProceduresManager:BaseManager
    {
		
		/// <summary>
		/// Delete Procedures by primary key.
		/// </summary>
		public void Delete(string procedureid)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(procedureid);
		}

		/// <summary>
		/// Insert a Procedures.
		/// </summary>
        public void Insert(Model.Procedures procedures)
        {
			//
			// todo:add other logic here
			//
            Validate(procedures);
            if (this.Exists(procedures.Id))
            {
                throw new Helper.InvalidValueException(Model.Procedures.PRO_Id);
            }
            procedures.InsertTime = DateTime.Now;
            procedures.ProceduresId = Guid.NewGuid().ToString();
            accessor.Insert(procedures);
        }
		
		/// <summary>
		/// Update a Procedures.
		/// </summary>
        public void Update(Model.Procedures procedures)
        {
			//
			// todo: add other logic here.
			//
            Validate(procedures);
            if (this.ExistsExcept(procedures))
            {
                throw new Helper.InvalidValueException(Model.Procedures.PRO_Id);
            }
            procedures.UpdateTime = DateTime.Now;
            accessor.Update(procedures);
        }
        private void Validate(Model.Procedures procedures)
        {
            if (string.IsNullOrEmpty(procedures.Id))
            {
                throw new Helper.RequireValueException(Model.Procedures.PRO_Id);
            }
            if (string.IsNullOrEmpty(procedures.Procedurename))
            {
                throw new Helper.RequireValueException(Model.Procedures.PRO_Procedurename);
            }
        }
        public IList<Model.Procedures> Select(Model.TechonlogyHeader techonlogyHeader)
        {
            return accessor.Select(techonlogyHeader);
        }
        public     IList<Book.Model.Procedures> Select(Book.Model.BomParentPartInfo bomPart)
        {
            return accessor.Select(bomPart);
        }
        public IList<Book.Model.Procedures> Select(string workHouseId)
        {
            return accessor.Select(workHouseId);
        }

    }
}

