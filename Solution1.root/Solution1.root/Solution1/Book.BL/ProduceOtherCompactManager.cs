//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactManager.cs
// author: peidun
// create date：2010-1-4 15:32:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherCompact.
    /// </summary>
    public partial class ProduceOtherCompactManager : BaseManager
    {
        private static readonly DA.IProduceOtherCompactDetailAccessor ProduceOtherCompactDetailAccessor = (DA.IProduceOtherCompactDetailAccessor)Accessors.Get("ProduceOtherCompactDetailAccessor");
        private static readonly DA.IProduceOtherCompactMaterialAccessor ProduceOtherCompactMaterialAccessor = (DA.IProduceOtherCompactMaterialAccessor)Accessors.Get("ProduceOtherCompactMaterialAccessor");

        /// <summary>
        /// 
        /// Delete ProduceOtherCompact by primary key.
        /// </summary>
        public void Delete(string produceOtherCompactId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceOtherCompactId);
        }
        public void Delete(Model.ProduceOtherCompact produceOtherCompact)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceOtherCompact.ProduceOtherCompactId);
        }
        public Model.ProduceOtherCompact GetDetails(string produceOtherCompactId)
        {
            Model.ProduceOtherCompact produceOtherCompact = accessor.Get(produceOtherCompactId);
            produceOtherCompact.Details = ProduceOtherCompactDetailAccessor.Select(produceOtherCompact);
            produceOtherCompact.DetailMaterial = ProduceOtherCompactMaterialAccessor.Select(produceOtherCompact);
            return produceOtherCompact;
        }
        /// <summary>
        /// Insert a ProduceOtherCompact.
        /// </summary>
        public void Insert(Model.ProduceOtherCompact produceOtherCompact)
        {
            //
            // todo:add other logic here
            //
            Validate(produceOtherCompact);
            try
            {
                if (this.ExistsPrimary(produceOtherCompact.ProduceOtherCompactId))
                {
                    produceOtherCompact.ProduceOtherCompactId = this.GetId();
                }

                produceOtherCompact.InsertTime = DateTime.Now;
                produceOtherCompact.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceOtherCompact.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceOtherCompact.InsertTime.Value.Year, produceOtherCompact.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceOtherCompact.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(produceOtherCompact);

                foreach (Model.ProduceOtherCompactDetail produceOtherCompactDetail in produceOtherCompact.Details)
                {
                    if (produceOtherCompactDetail.Product == null || string.IsNullOrEmpty(produceOtherCompactDetail.ProductId))
                        throw new Exception("貨品不為空");
                    if (produceOtherCompactDetail.OtherCompactCount < 0) continue;
                    produceOtherCompactDetail.ProduceOtherCompactId = produceOtherCompact.ProduceOtherCompactId;
                    Model.MPSdetails mpsDal = new BL.MPSdetailsManager().Get(produceOtherCompactDetail.MPSDetailId);
                    //double Sum = new PronotedetailsManager().GetByMPSdetail(produceOtherCompactDetail.MPSDetailId);
                    if (mpsDal != null)
                    {
                        mpsDal.MPSHasSingleSum += produceOtherCompactDetail.OtherCompactCount;
                        if (mpsDal.MPSHasSingleSum == mpsDal.MPSdetailssum)
                        {
                            mpsDal.MPSEndState = true;
                        }
                        new BL.MPSdetailsManager().Update(mpsDal);
                    }
                    ProduceOtherCompactDetailAccessor.Insert(produceOtherCompactDetail);
                }
                foreach (Model.ProduceOtherCompactMaterial detailMaterial in produceOtherCompact.DetailMaterial)
                {
                    if (string.IsNullOrEmpty(detailMaterial.ProductId))
                        continue;
                    detailMaterial.ProduceOtherCompactId = produceOtherCompact.ProduceOtherCompactId;
                    //Model.MPSdetails mpsDal = new BL.MPSdetailsManager().Get(produceOtherCompactDetail.MPSDetailId);
                    ////double Sum = new PronotedetailsManager().GetByMPSdetail(produceOtherCompactDetail.MPSDetailId);
                    //if (mpsDal != null)
                    //{
                    //    mpsDal.MPSHasSingleSum += produceOtherCompactDetail.OtherCompactCount;
                    //    if (mpsDal.MPSHasSingleSum == mpsDal.MPSdetailssum)
                    //    {
                    //        mpsDal.MPSEndState = true;
                    //    }
                    //    new BL.MPSdetailsManager().Update(mpsDal);
                    //}
                    ProduceOtherCompactMaterialAccessor.Insert(detailMaterial);
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
        /// Update a ProduceOtherCompact.
        /// </summary>
        public void Update(Model.ProduceOtherCompact produceOtherCompact)
        {
            //
            // todo: add other logic here.
            //
            Validate(produceOtherCompact);
            if (produceOtherCompact != null)
            {
                this.Delete(produceOtherCompact);
                produceOtherCompact.UpdateTime = DateTime.Now;
                this.Insert(produceOtherCompact);
            }
        }
        public void UpdateOtherCompact(Model.ProduceOtherCompact produceOtherCompact)
        {
            //
            // todo: add other logic here.
            //
            Validate(produceOtherCompact);
            produceOtherCompact.UpdateTime = DateTime.Now;
            accessor.Update(produceOtherCompact);
        }
        private void Validate(Model.ProduceOtherCompact produceOtherCompact)
        {
            if (string.IsNullOrEmpty(produceOtherCompact.ProduceOtherCompactId))
            {
                throw new Helper.RequireValueException(Model.ProduceOtherCompact.PRO_ProduceOtherCompactId);
            }
        }

        public IList<Model.ProduceOtherCompact> SelectIsInDepot()
        {
            return accessor.SelectIsInDepot();
        }

        public IList<Model.ProduceOtherCompact> SelectIsInDepotMaterial()
        {
            return accessor.SelectIsInDepotMaterial();
        }

        public IList<Model.ProduceOtherCompact> SelectByMRSHeaderId(string MrsHeaderId)
        {
            return accessor.SelectByMRSHeaderId(MrsHeaderId);
        }

        protected override string GetSettingId()
        {
            return "pocRule";
        }
        protected override string GetInvoiceKind()
        {
            return "poc";
        }
        public IList<Book.Model.ProduceOtherCompact> SelectThreeMonth()
        {
            return accessor.SelectThreeMonth();
        }
        public IList<Book.Model.ProduceOtherCompact> GetByDate(DateTime startDate, DateTime endDate)
        {
            return accessor.GetByDate(startDate,endDate); ;
        }
    }
}

