//------------------------------------------------------------------------------
//
// file name：ProduceOtherInDepotManager.cs
// author: peidun
// create date：2010-1-8 13:43:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherInDepot.
    /// </summary>
    public partial class ProduceOtherInDepotManager : BaseManager
    {
        private static readonly DA.IProduceOtherInDepotDetailAccessor ProduceOtherInDepotDetailAccessor = (DA.IProduceOtherInDepotDetailAccessor)Accessors.Get("ProduceOtherInDepotDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IProduceOtherCompactDetailAccessor produceOtherCompactDetailAccessor = (DA.IProduceOtherCompactDetailAccessor)Accessors.Get("ProduceOtherCompactDetailAccessor");

        /// <summary>
        /// Delete ProduceOtherInDepot by primary key.
        /// </summary>
        public void Delete(string produceOtherInDepotId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceOtherInDepotId);
        }
        public void Delete(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            try
            {
                BL.V.BeginTransaction();

                foreach (var item in ProduceOtherInDepotDetailAccessor.Select(produceOtherInDepot))
                {
                    stockAccessor.Increment(new BL.DepotPositionManager().Get(item.DepotPositionId), item.Product, -item.ProduceQuantity);

                    Model.ProduceOtherCompactDetail CompactDetail = produceOtherCompactDetailAccessor.Get(item.ProduceOtherCompactDetailId);
                    if (CompactDetail != null)
                    {
                        CompactDetail.InDepotCount = CompactDetail.InDepotCount == null ? 0 : CompactDetail.InDepotCount - item.ProduceQuantity;
                        produceOtherCompactDetailAccessor.Update(CompactDetail);
                    }
                }

                accessor.Delete(produceOtherInDepot.ProduceOtherInDepotId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        public Model.ProduceOtherInDepot GetDetails(string produceOtherInDepotId)
        {
            Model.ProduceOtherInDepot produceOtherInDepot = accessor.Get(produceOtherInDepotId);
            if (produceOtherInDepot != null)
                produceOtherInDepot.Details = ProduceOtherInDepotDetailAccessor.Select(produceOtherInDepot);
            return produceOtherInDepot;
        }
        /// <summary>
        /// Insert a ProduceOtherInDepot.
        /// </summary>
        public void Insert(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            //
            // todo:add other logic here
            //
            Validate(produceOtherInDepot);

            try
            {
                produceOtherInDepot.InsertTime = DateTime.Now;

                produceOtherInDepot.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceOtherInDepot.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceOtherInDepot.InsertTime.Value.Year, produceOtherInDepot.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceOtherInDepot.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                                    
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(produceOtherInDepot);

                foreach (Model.ProduceOtherInDepotDetail produceOtherInDepotDetail in produceOtherInDepot.Details)
                {
                    if (produceOtherInDepotDetail.Product == null || string.IsNullOrEmpty(produceOtherInDepotDetail.Product.ProductId))
                        throw new Exception("貨品不為空");
                    produceOtherInDepotDetail.ProduceOtherInDepotId = produceOtherInDepot.ProduceOtherInDepotId;
                    ProduceOtherInDepotDetailAccessor.Insert(produceOtherInDepotDetail);

                    Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(produceOtherInDepotDetail.ProductId, produceOtherInDepotDetail.DepotPositionId);
                    stockAccessor.Increment(new BL.DepotPositionManager().Get(produceOtherInDepotDetail.DepotPositionId), produceOtherInDepotDetail.Product, produceOtherInDepotDetail.ProduceQuantity);

                    Model.ProduceOtherCompactDetail CompactDetail = produceOtherCompactDetailAccessor.Get(produceOtherInDepotDetail.ProduceOtherCompactDetailId);
                    if (CompactDetail != null)
                    {
                        if (CompactDetail.InDepotCount == null)
                            CompactDetail.InDepotCount = 0;
                        CompactDetail.InDepotCount += produceOtherInDepotDetail.ProduceQuantity;
                        if (CompactDetail.InDepotCount >= CompactDetail.OtherCompactCount)
                        {
                            CompactDetail.DetailsFlag = 2;
                        }
                        else
                        {
                            if (CompactDetail.InDepotCount > 0)
                            {
                                CompactDetail.DetailsFlag = 1;
                            }
                            else
                            {
                                CompactDetail.DetailsFlag = 0;
                            }
                        }
                        produceOtherCompactDetailAccessor.Update(CompactDetail);
                        UpdateProduceOtherCompactFlag(CompactDetail.ProduceOtherCompact);
                    }
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }   
        }
        public void UpdateProduceOtherCompactFlag(Model.ProduceOtherCompact produceOtherCompact)
        {
            int flag = 0;
            IList<Model.ProduceOtherCompactDetail> list = produceOtherCompactDetailAccessor.Select(produceOtherCompact);
            foreach (Model.ProduceOtherCompactDetail detail in list)
            {
                flag += detail.DetailsFlag == null ? 0 : detail.DetailsFlag.Value;
            }
            if (flag == 0)
                produceOtherCompact.InvoiceDetailFlag = 0;
            else if (flag < list.Count * 2)
                produceOtherCompact.InvoiceDetailFlag = 1;
            else if (flag == list.Count * 2)
                produceOtherCompact.InvoiceDetailFlag = 2;
            new BL.ProduceOtherCompactManager().UpdateOtherCompact(produceOtherCompact);
        }
        /// <summary>
        /// Update a ProduceOtherInDepot.
        /// </summary>
        public void Update(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            //
            // todo: add other logic here.
            //
            Validate(produceOtherInDepot);
            if (produceOtherInDepot != null)
            {
                this.Delete(produceOtherInDepot);
                produceOtherInDepot.UpdateTime = DateTime.Now;
                this.Insert(produceOtherInDepot);
            }
        }
        private void Validate(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            if (string.IsNullOrEmpty(produceOtherInDepot.ProduceOtherInDepotId))
            {
                throw new Helper.RequireValueException(Model.ProduceOtherInDepot.PRO_ProduceOtherInDepotId);
            }
            //if (string.IsNullOrEmpty(produceOtherInDepot.WorkHouseId))
            //{
            //    throw new Helper.RequireValueException(Model.ProduceOtherInDepot.PROPERTY_WORKHOUSEID);
            //}
        }

        public IList<Model.ProduceOtherInDepot> SelectByCondition(DateTime startdate, DateTime enddate, string supperId1, string supperId2, string ProduceOtherCompactId1, string ProduceOtherCompactId2)
        {
            return accessor.SelectByCondition(startdate, enddate, supperId1, supperId2, ProduceOtherCompactId1, ProduceOtherCompactId2);
        }

        protected override string GetSettingId()
        {
            return "podRule";
        }
        protected override string GetInvoiceKind()
        {
            return "pod";
        }
    }
}

