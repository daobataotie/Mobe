//------------------------------------------------------------------------------
//
// file name：ProcessingManager.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Processing.
    /// </summary>
    public partial class ProcessingManager:BaseManager
    {
		
		/// <summary>
		/// Delete Processing by primary key.
		/// </summary>
		public void Delete(string processId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(processId);
		}

		/// <summary>
		/// Insert a Processing.
		/// </summary>
        public void Insert(Model.Processing processing)
        {
			//
			// todo:add other logic here
			//
            Validate(processing);
            processing.InsertTime = DateTime.Now;
           // processing.CustomerId = processing.Customer.CustomerId;
            //processing.ProcessCategoryId = processing.ProcessCategory.ProcessCategoryId;
            processing.ProcessId = Guid.NewGuid().ToString();
            accessor.Insert(processing);
        }
		
		/// <summary>
		/// Update a Processing.
		/// </summary>
        public void Update(Model.Processing processing)
        {
			//
			// todo: add other logic here.
			//
           // if(this.ExistsConcent(processing.Content,processing.ProcessId))
              //  throw new  Helper.InvalidValueException(Model.Processing.PROPERTY_CONTENT);
            Validate(processing);
            processing.UpdateTime = DateTime.Now;
           // processing.CustomerId = processing.Customer.CustomerId;
            //processing.ProcessCategoryId = processing.ProcessCategory.ProcessCategoryId;
            accessor.Update(processing);
        }
        private void Validate(Model.Processing processing) 
        {
            //if (processing.Customer == null) 
            //{
            //    throw new Helper.RequireValueException(Model.Processing.PROPERTY_CUSTOMERID);
            //}
            if (string.IsNullOrEmpty(processing.ProcessCategoryId)) 
            {
                throw new Helper.RequireValueException(Model.Processing.PROPERTY_PROCESSCATEGORYID);
            }
            if (string.IsNullOrEmpty(processing.Content)) 
            {
                throw new Helper.RequireValueException(Model.Processing.PROPERTY_CONTENT);
            }
        }

        //public IList<Model.Processing> Select(Book.Model.Customer customer)
        //{
        //    return accessor.Select(customer);
        //}

        public IList<Book.Model.Processing> Select(Book.Model.ProcessCategory processCategory)
        {
            return accessor.Select(processCategory);
        }
        //public IList<Book.Model.Processing> Selectbycategorycustomer(string processCategory, Book.Model.Customer customer)
        //{
        //    return accessor.Selectbycategorycustomer(processCategory, customer);
        //}
        public void Delete(Book.Model.Processing process)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(process.ProcessId);
        }
        //public DataTable SelectProceCateByCustom(Model.Customer customer)
        //{
        //    return accessor.SelectProceCateByCustom(customer);
        //}
        public bool ExistsConcent(string Content, string id)
        { 
             return accessor.ExistsConcent(Content,id);
        }
    }
}

