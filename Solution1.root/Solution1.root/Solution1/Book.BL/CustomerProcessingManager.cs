//------------------------------------------------------------------------------
//
// file name：CustomerProcessingManager.cs
// author: mayanjun
// create date：2010-7-30 19:31:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerProcessing.
    /// </summary>
    public partial class CustomerProcessingManager:BaseManager
    {

        private static readonly DA.ICustomerProcessingDetailAccessor accessorDetail = (DA.ICustomerProcessingDetailAccessor)Accessors.Get("CustomerProcessingDetailAccessor");
		
		/// <summary>
		/// Delete CustomerProcessing by primary key.
		/// </summary>
		public void Delete(string customerProcessingId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(customerProcessingId);
		}

		/// <summary>
		/// Insert a CustomerProcessing.
		/// </summary>
        public void Insert(Model.CustomerProcessing customerProcessing)
        {
			//
			// todo:add other logic here
			//
            //设置KEY值
            Validate(customerProcessing);
           // string sequencekey = "";
            //foreach (Model.CustomerProcessingDetail customerProcessingDetail in customerProcessing.detail)
            //{ 
            // sequencekey+=customerProcessingDetail.ProcessCategory.Id; 
            //}

            //if (string.IsNullOrEmpty(customerProcessing.Id))
            //    customerProcessing.Id = GetNewId(sequencekey);





            SequenceManager.Increment(this.GetInvoiceKind());

            customerProcessing.InsertTime = DateTime.Now;
            customerProcessing.UpdateTime = DateTime.Now;

            accessor.Insert(customerProcessing);
            foreach (Model.CustomerProcessingDetail customerProcessingDetail in customerProcessing.detail)
            {
                if (customerProcessingDetail.ProcessCategory == null || string.IsNullOrEmpty(customerProcessingDetail.ProcessCategoryId) || customerProcessingDetail.Process==null) continue;

                customerProcessingDetail.CustomerProcessingDetailId = Guid.NewGuid().ToString();
                customerProcessingDetail.ProcessCategoryId = customerProcessingDetail.ProcessCategory.ProcessCategoryId;
                customerProcessingDetail.CustomerProcessing = customerProcessing;
                customerProcessingDetail.CustomerProcessingId = customerProcessing.CustomerProcessingId;
                customerProcessingDetail.ProcessId = customerProcessingDetail.Process.ProcessId;
                accessorDetail.Insert(customerProcessingDetail);


            }
           // 
        }
		
		/// <summary>
		/// Update a CustomerProcessing.
		/// </summary>
        public void Update(Model.CustomerProcessing customerProcessing)
        {
			//
			// todo: add other logic here.
			//
            //string sequencekey = "";
            //foreach (Model.CustomerProcessingDetail customerProcessingDetail in customerProcessing.detail)
            //{
            //    sequencekey += customerProcessingDetail.ProcessCategory.Id;
            //}

            //if (string.IsNullOrEmpty(customerProcessing.Id))
            //    customerProcessing.Id = GetNewId(sequencekey);


            //if (string.IsNullOrEmpty(customerProcessing.Id))
            //{
            //    throw new Helper.RequireValueException(Model.CustomerProcessing.PROPERTY_ID);
            //}
            if (this.ExistsExcept(customerProcessing))
            {
                throw new Helper.InvalidValueException(Model.CustomerProcessing.PROPERTY_ID);
            }
           
            customerProcessing.UpdateTime = DateTime.Now;

            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(customerProcessing.CustomerProcessingId);
                this.Insert(customerProcessing);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;

            }
           // accessor.Update(customerProcessing);
        }
        public IList<Model.CustomerProcessing> Select(Model.Customer Customer)
        {
            //
            // todo: add other logic here.
            //
            return accessor.Select(Customer);
        }
        private void Validate(Model.CustomerProcessing customerProcessing)
        {
            //if (string.IsNullOrEmpty(customerProcessing.Id))
            //{
            //    throw new Helper.RequireValueException(Model.CustomerProcessing.PROPERTY_ID);
            //}
            if (this.Exists(customerProcessing.Id))
            {
                throw new Helper.InvalidValueException(Model.CustomerProcessing.PROPERTY_ID);
            }
           
        }
        protected override string GetInvoiceKind()
        {
            return "Processing";
        }

        protected override string  GetSettingId()
        {
 	          return "ProcessingRule";
        }
        
    }
}

