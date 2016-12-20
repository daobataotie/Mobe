//------------------------------------------------------------------------------
//
// file name：TechnologydetailsManager.cs
// author: peidun
// create date：2009-12-8 16:10:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Technologydetails.
    /// </summary>
    public partial class TechnologydetailsManager:BaseManager
    {
		
		/// <summary>
		/// Delete Technologydetails by primary key.
		/// </summary>
		public void Delete(string technologydetailsID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(technologydetailsID);
		}

		/// <summary>
		/// Insert a Technologydetails.
		/// </summary>
        public void Insert(Model.Technologydetails technologydetails)
        {
			//
			// todo:add other logic here
			//
            //if (this.Exists(technologydetails.ProceduresId))
            //{
            //    throw new Helper.InvalidValueException(Model.Technologydetails.PROPERTY_PROCEDURESID);
            //}
            //if (this.Exists(technologydetails.TechonlogyHeaderId))
            //{
            //    throw new Helper.InvalidValueException(Model.Technologydetails.PROPERTY_PROCEDURESID);
            //}
            Validate(technologydetails);
            technologydetails.InsertTime = DateTime.Now;
            technologydetails.TechnologydetailsID = Guid.NewGuid().ToString();
            accessor.Insert(technologydetails);
        }
		
		/// <summary>
		/// Update a Technologydetails.
		/// </summary>
        public void Update(Model.Technologydetails technologydetails)
        {
			//
			// todo: add other logic here.
			//
            Validate(technologydetails);
            technologydetails.UpdateTime = DateTime.Now;
            accessor.Update(technologydetails);
        }
        private void Validate(Model.Technologydetails technologydetails)
        {
            if (string.IsNullOrEmpty(technologydetails.ProceduresId))
            {
                throw new Helper.RequireValueException(Model.Technologydetails.PROPERTY_PROCEDURESID);
            }
            if (string.IsNullOrEmpty(technologydetails.TechonlogyHeaderId))
            {
                throw new Helper.RequireValueException(Model.Technologydetails.PROPERTY_TECHONLOGYHEADERID);
            }
        }
        public Book.Model.Technologydetails Select(Model.Procedures Procedures)
        {
            return  accessor.Select(Procedures.ProceduresId);
        }
        public IList<Book.Model.Technologydetails> Select(Model.TechonlogyHeader TechonlogyHeader)
        {
            return accessor.Select(TechonlogyHeader);
        }
        public bool IsExists_TechnologydetailsNo(Model.Technologydetails tec)
        {
            return accessor.IsExists_TechnologydetailsNo(tec);
        }
        public IList<Model.Technologydetails> SelectByProceduresId(string ProceduresId, string TechnologydetailsNo)
        {
            return accessor.SelectByProceduresId(ProceduresId, TechnologydetailsNo);
        }
        public void Delete(Model.TechonlogyHeader techonlogyHeader)
        {
            accessor.Delete(techonlogyHeader);

        }
    }
}

