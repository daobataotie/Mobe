//------------------------------------------------------------------------------
//
// file name：ProduceInDepotManager.cs
// author: peidun
// create date：2010-1-8 13:43:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceInDepot.
    /// </summary>
    public partial class ProduceInDepotManager:BaseManager
    {
        private static readonly DA.IProduceInDepotDetailAccessor ProduceInDepotDetailAccessor = (DA.IProduceInDepotDetailAccessor)Accessors.Get("ProduceInDepotDetailAccessor");
		/// <summary>
		/// Delete ProduceInDepot by primary key.
		/// </summary>
		public void Delete(string produceInDepotId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceInDepotId);
		}
        public void Delete(Model.ProduceInDepot produceInDepot)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceInDepot.ProduceInDepotId);
        }
        public Model.ProduceInDepot GetDetails(string produceInDepotId)
        {
            Model.ProduceInDepot produceInDepot = accessor.Get(produceInDepotId);
            produceInDepot.Details = ProduceInDepotDetailAccessor.Select(produceInDepot);
            return produceInDepot;
        }
		/// <summary>
		/// Insert a ProduceInDepot.
		/// </summary>
        public void Insert(Model.ProduceInDepot produceInDepot)
        {
			//
			// todo:add other logic here
			//
            Validate(produceInDepot);


            try
            {
                produceInDepot.InsertTime = DateTime.Now;

                produceInDepot.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceInDepot.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceInDepot.InsertTime.Value.Year, produceInDepot.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceInDepot.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(produceInDepot);

                foreach (Model.ProduceInDepotDetail produceInDepotDetail in produceInDepot.Details)
                {
                    if (produceInDepotDetail.Product == null || string.IsNullOrEmpty(produceInDepotDetail.Product.ProductId)) continue;
                    produceInDepotDetail.ProduceInDepotId = produceInDepot.ProduceInDepotId;
                    if (produceInDepotDetail.ProductProce != null)
                        produceInDepotDetail.ProductProceId = produceInDepotDetail.ProductProce.ProductId;
                    ProduceInDepotDetailAccessor.Insert(produceInDepotDetail);
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
		/// Update a ProduceInDepot.
		/// </summary>
        public void Update(Model.ProduceInDepot produceInDepot)
        {
			//
			// todo: add other logic here.
			//
            Validate(produceInDepot);
            if (produceInDepot != null)
            {
                this.Delete(produceInDepot);
                produceInDepot.UpdateTime = DateTime.Now;
                this.Insert(produceInDepot);
            }
        }
        private void Validate(Model.ProduceInDepot produceInDepot)
        {
            if (string.IsNullOrEmpty(produceInDepot.ProduceInDepotId))
            {
                throw new Helper.RequireValueException(Model.ProduceInDepot.PRO_ProduceInDepotId);
            }
            if (string.IsNullOrEmpty(produceInDepot.WorkHouseId))
            {
                throw new Helper.RequireValueException(Model.ProduceInDepot.PRO_WorkHouseId);
            }
        }
        protected override string GetSettingId()
        {
            return "pidRule";
        }
        protected override string GetInvoiceKind()
        {
            return "pid";
        }
    }
}

