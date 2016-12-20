//------------------------------------------------------------------------------
//
// file name：ProduceMaterialExitManager.cs
// author: peidun
// create date：2010-1-6 10:20:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceMaterialExit.
    /// </summary>
    public partial class ProduceMaterialExitManager : BaseManager
    {
        private static readonly DA.IProduceMaterialExitDetailAccessor detailsAccessor = (DA.IProduceMaterialExitDetailAccessor)Accessors.Get("ProduceMaterialExitDetailAccessor");
        /// <summary>
        /// Delete ProduceMaterialExit by primary key.
        /// </summary>
        public void Delete(string produceExitMaterialId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceExitMaterialId);
        }

        /// <summary>
        /// Insert a ProduceMaterialExit.
        /// </summary>
        public void Insert(Model.ProduceMaterialExit produceMaterialExit)
        {
            //
            // todo:add other logic here
            //
            Validate(produceMaterialExit);
            try
            {
                produceMaterialExit.InsertTime = DateTime.Now;

                produceMaterialExit.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceMaterialExit.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceMaterialExit.InsertTime.Value.Year, produceMaterialExit.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceMaterialExit.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(produceMaterialExit);
                foreach (Model.ProduceMaterialExitDetail produceMaterialExitDetail in produceMaterialExit.Detail)
                {
                    if (produceMaterialExitDetail.Product == null || string.IsNullOrEmpty(produceMaterialExitDetail.Product.ProductId))
                        throw new Helper.RequireValueException(Model.ProduceMaterialExitDetail.PRO_ProductId);
                    produceMaterialExitDetail.ProduceMaterialExitId = produceMaterialExit.ProduceMaterialExitId;

                    detailsAccessor.Insert(produceMaterialExitDetail);
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
        /// Update a ProduceMaterialExit.
        /// </summary>
        public void Update(Model.ProduceMaterialExit produceMaterialExit)
        {
            //
            // todo: add other logic here.
            //
            Validate(produceMaterialExit);
            if (produceMaterialExit != null)
            {
                this.Delete(produceMaterialExit);
                produceMaterialExit.UpdateTime = DateTime.Now;
                this.Insert(produceMaterialExit);
            }
        }
        public Model.ProduceMaterialExit GetDetails(string produceExitMaterialId)
        {
            Model.ProduceMaterialExit produceMaterialExit = accessor.Get(produceExitMaterialId);
            if (produceMaterialExit != null)
                produceMaterialExit.Detail = detailsAccessor.Select(produceMaterialExit);
            return produceMaterialExit;
        }
        public void Delete(Model.ProduceMaterialExit produceMaterialExit)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceMaterialExit.ProduceMaterialExitId);
        }
        private void Validate(Model.ProduceMaterialExit produceMaterialExit)
        {
            if (string.IsNullOrEmpty(produceMaterialExit.ProduceMaterialExitId))
            {
                throw new Helper.RequireValueException(Model.ProduceMaterialExit.PRO_ProduceMaterialExitId);
            }
            if (string.IsNullOrEmpty(produceMaterialExit.WorkHouseId))
            {
                throw new Helper.RequireValueException(Model.ProduceMaterialExit.PRO_WorkHouseId);
            }
        }
        protected override string GetSettingId()
        {
            return "pmeRule";
        }
        protected override string GetInvoiceKind()
        {
            return "pme";
        }

        public IList<Model.ProduceMaterialExit> SelectByCondition(DateTime start, DateTime end)
        {
            return accessor.SelectByCondition(start, end);
        }

    }
}

