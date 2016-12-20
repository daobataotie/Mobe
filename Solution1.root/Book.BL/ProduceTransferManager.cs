//------------------------------------------------------------------------------
//
// file name：ProduceTransferManager.cs
// author: mayanjun
// create date：2011-4-6 10:53:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceTransfer.
    /// </summary>
    public partial class ProduceTransferManager : BaseManager
    {
        private static readonly DA.IProduceTransferDetailAccessor ProduceTransferDetailAccessor = (DA.IProduceTransferDetailAccessor)Accessors.Get("ProduceTransferDetailAccessor");
		
		/// <summary>
		/// Delete ProduceTransfer by primary key.
		/// </summary>
		public void Delete(string produceTransferId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceTransferId);
		}
        public Model.ProduceTransfer GetDetails(string produceTransferId)
        {
            Model.ProduceTransfer produceTransfer = accessor.Get(produceTransferId);
            produceTransfer.Details = ProduceTransferDetailAccessor.Select(produceTransfer);
            return produceTransfer;
        }
		/// <summary>
		/// Insert a ProduceTransfer.
		/// </summary>
        public void Insert(Model.ProduceTransfer produceTransfer)
        {
            //
            // todo:add other logic here
            //
            Validate(produceTransfer);
            try
            {
                produceTransfer.InsertTime = DateTime.Now;

                produceTransfer.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceTransfer.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceTransfer.InsertTime.Value.Year, produceTransfer.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceTransfer.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(produceTransfer);
                foreach (Model.ProduceTransferDetail produceTransferDetail in produceTransfer.Details)
                {
                    if (produceTransferDetail.Product == null || string.IsNullOrEmpty(produceTransferDetail.Product.ProductId))
                        throw new Exception("貨品不為空");
                    produceTransferDetail.ProduceTransferId = produceTransfer.ProduceTransferId;
                    //produceMaterialdetails.Materialprocesedsum = produceMaterialdetails.Materialprocessum;
                    ProduceTransferDetailAccessor.Insert(produceTransferDetail);
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
		
		/// <summary>
		/// Update a ProduceTransfer.
		/// </summary>
        public void Update(Model.ProduceTransfer produceTransfer)
        {
			//
			// todo: add other logic here.
			//
            Validate(produceTransfer);
            if (produceTransfer != null)
            {
                this.Delete(produceTransfer);
                produceTransfer.UpdateTime = DateTime.Now;
                this.Insert(produceTransfer);
            }
        }
        protected override string GetSettingId()
        {
            return "ptfRule";
        }
        protected override string GetInvoiceKind()
        {
            return "ptf";
        }

        public void Delete(Model.ProduceTransfer produceTransfer)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceTransfer.ProduceTransferId);
        }
        private void Validate(Model.ProduceTransfer produceTransfer)
        {
            if (string.IsNullOrEmpty(produceTransfer.ProduceTransferId))
            {
                throw new Helper.RequireValueException(Model.ProduceTransfer.PRO_ProduceTransferId);
            }
            if (string.IsNullOrEmpty(produceTransfer.WorkHouseInId))
            {
                throw new Helper.RequireValueException(Model.ProduceTransfer.PRO_WorkHouseInId);
            }
            if (string.IsNullOrEmpty(produceTransfer.WorkHouseOutId))
            {
                throw new Helper.RequireValueException(Model.ProduceTransfer.PRO_WorkHouseOutId);
            }
        }
    }
}

