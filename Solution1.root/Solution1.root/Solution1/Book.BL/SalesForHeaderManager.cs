//------------------------------------------------------------------------------
//
// file name：SalesForHeaderManager.cs
// author: peidun
// create date：2009-12-17 15:29:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.SalesForHeader.
    /// </summary>
    public partial class SalesForHeaderManager : BaseManager
    {
        private static readonly DA.ISalesFordetailsAccessor salesFordetailsAccessor = (DA.ISalesFordetailsAccessor)Accessors.Get("SalesFordetailsAccessor");

		
		/// <summary>
		/// Delete SalesForHeader by primary key.
		/// </summary>
		public void Delete(string salesForHeaderId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(salesForHeaderId);
		}
        public void Delete(Model.SalesForHeader salesForHeader)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(salesForHeader.SalesForHeaderId);
        }

        /// <summary>
        /// Delete SalesForHeader by primary key.
        /// </summary>


		/// <summary>
		/// Insert a SalesForHeader.
		/// </summary>
        public void Insert(Model.SalesForHeader salesForHeader)
        {
			//
			// todo:add other logic here
			//
           
            salesForHeader.InsertTime=DateTime.Now;
accessor.Insert(salesForHeader);
            if (salesForHeader.details!=null)  
            foreach (Model.SalesFordetails SalesFordetails in salesForHeader.details)
            {
                if (SalesFordetails.Product == null || string.IsNullOrEmpty(SalesFordetails.Product.ProductId))
                        throw new Exception("貨品不為空");
        
                SalesFordetails.SalesForHeaderId = salesForHeader.SalesForHeaderId;
                salesFordetailsAccessor.Insert(SalesFordetails);
            }         
          
        }
		
		/// <summary>
		/// Update a SalesForHeader.
		/// </summary>
        public void Update(Model.SalesForHeader salesForHeader)
        {
			//
			// todo: add other logic here.
			//
            //accessor.Update(salesForHeader);

            if (salesForHeader != null)
            {
                this.Delete(salesForHeader);

                salesForHeader.UpdateTime = DateTime.Now;
                this.Insert(salesForHeader);
            }            
        }
        public Model.SalesForHeader Getdetails(string salesForHeaderId)
        {
            Model.SalesForHeader SalesForHeader = accessor.Get(salesForHeaderId);
            if (SalesForHeader!=null)
            SalesForHeader.details = salesFordetailsAccessor.Getdetails(SalesForHeader);
            return SalesForHeader;
        }
      
    }
}

