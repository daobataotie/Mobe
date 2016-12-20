//------------------------------------------------------------------------------
//
// file name：OutgoingKindManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.OutgoingKind.
    /// </summary>
    public partial class OutgoingKindManager : BaseManager
    {
		
		/// <summary>
		/// Delete OutgoingKind by primary key.
		/// </summary>
		public void Delete(string outgoingKindId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(outgoingKindId);
		}
        public void Delete(Model.OutgoingKind outgoingKind)
        {
            this.Delete(outgoingKind.OutgoingKindId);
        }
		/// <summary>
		/// Insert a OutgoingKind.
		/// </summary>
        public void Insert(Model.OutgoingKind outgoingKind)
        {
			//
			// todo:add other logic here
			//
            //Validate(outgoingKind);

            //if (this.HasRows(outgoingKind.OutgoingKindId)) 
            //{
            //    throw new Helper.InvalidValueException(Model.OutgoingKind.PROPERTY_OUTGOINGKINDID);
            //}

            //outgoingKind.InsertTime = DateTime.Now;
            //outgoingKind.UpdateTime = DateTime.Now;
            //if (this.Exists(outgoingKind.Id))
            //{
            //    throw new Helper.InvalidValueException(Model.OutgoingKind.PROPERTY_ID);
            //}
            outgoingKind.InsertTime = DateTime.Now;
            outgoingKind.UpdateTime = DateTime.Now;
            accessor.Insert(outgoingKind);
        }
        //private void Validate(Model.OutgoingKind outgoingKind) 
        //{
        //    if (string.IsNullOrEmpty(outgoingKind.OutgoingKindId)) 
        //    {
        //        throw new Helper.RequireValueException(Model.OutgoingKind.PROPERTY_OUTGOINGKINDID);
        //    }
        //    if (string.IsNullOrEmpty(outgoingKind.OutgoingKindName)) 
        //    {
        //        throw new Helper.RequireValueException(Model.OutgoingKind.PROPERTY_OUTGOINGKINDNAME);

        //    }
        //}
		/// <summary>
		/// Update a OutgoingKind.
		/// </summary>
        public void Update(IList<Model.OutgoingKind>  detail)
        {
			//
			// todo: add other logic here.
			//

            foreach (Model.OutgoingKind ok in detail)
            {

                if (string.IsNullOrEmpty(ok.Id) || string.IsNullOrEmpty(ok.OutgoingKindName))
                {
                    throw new Helper.RequireValueException(Model.OutgoingKind.PROPERTY_ID);
                }               
                if (accessor.ExistsExcept(ok))
                {
                        throw new Helper.InvalidValueException(Model.OutgoingKind.PROPERTY_ID);
                }
            }
            foreach (Model.OutgoingKind ok in detail)
            {
                if (accessor.ExistsPrimary(ok.OutgoingKindId))
                {
                    ok.UpdateTime = DateTime.Now;
                    accessor.Update(ok);
                }
                else
                {                   
                    this.Insert(ok);
                }
            }
            //Validate(outgoingKind);
            //outgoingKind.UpdateTime = DateTime.Now;
            //accessor.Update(outgoingKind);
        }
		
    }
}

