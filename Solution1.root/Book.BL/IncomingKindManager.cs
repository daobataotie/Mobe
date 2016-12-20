//------------------------------------------------------------------------------
//
// file name：IncomingKindManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.IncomingKind.
    /// </summary>
    public partial class IncomingKindManager : BaseManager
    {
		
		/// <summary>
		/// Delete IncomingKind by primary key.
		/// </summary>
		public void Delete(string incomingKindId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(incomingKindId);
		}
        public void Delete(Model.IncomingKind incomingKind)
        {
            this.Delete(incomingKind.IncomingKindId);
        }
		/// <summary>
		/// Insert a IncomingKind.
		/// </summary>
        public void Insert(Model.IncomingKind incomingKind)
        {
			//
			// todo:add other logic here
			//
            //if (this.Exists(incomingKind.Id))
            //{
            //    throw new Helper.InvalidValueException(Model.IncomingKind.PROPERTY_ID);
            //}
            incomingKind.InsertTime = DateTime.Now;
            incomingKind.UpdateTime = DateTime.Now;
            accessor.Insert(incomingKind);                           
        }
    
		/// <summary>
		/// Update a IncomingKind.
		/// </summary>
        public void Update(IList<Model.IncomingKind> detail)
        {
			//
			// todo: add other logic here.
			//
            foreach (Model.IncomingKind ik in detail)
            {

                if (string.IsNullOrEmpty(ik.Id) || string.IsNullOrEmpty(ik.IncomingKindName))
                {
                    throw new Helper.RequireValueException(Model.IncomingKind.PROPERTY_ID);
                }
                if (this.ExistsExcept(ik))
                {
                    throw new Helper.InvalidValueException(Model.IncomingKind.PROPERTY_ID);
                }

            }
            
            foreach (Model.IncomingKind ik in detail)
            {

                if (accessor.ExistsPrimary(ik.IncomingKindId))
                {
                    ik.UpdateTime = DateTime.Now;
                    accessor.Update(ik);
                }
                else
                {
                 
                    this.Insert(ik);
                }
            }
          
        }
		
    }
}

