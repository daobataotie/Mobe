//------------------------------------------------------------------------------
//
// file name：ProduceOtherMaterialManager.cs
// author: peidun
// create date：2010-1-5 15:26:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherMaterial.
    /// </summary>
    public partial class ProduceOtherMaterialManager : BaseManager
    {
        private static readonly DA.IProduceOtherMaterialDetailAccessor ProduceOtherMaterialDetailAccessor = (DA.IProduceOtherMaterialDetailAccessor)Accessors.Get("ProduceOtherMaterialDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IProduceOtherCompactMaterialAccessor produceOtherCompactMaterialAccessor = (DA.IProduceOtherCompactMaterialAccessor)Accessors.Get("ProduceOtherCompactMaterialAccessor");
        /// <summary>
        /// Delete ProduceOtherMaterial by primary key.
        /// </summary>
        public void Delete(string produceOtherMaterialId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceOtherMaterialId);
        }
        public void Delete(Model.ProduceOtherMaterial produceOtherMaterial)
        {

            try
            {
                BL.V.BeginTransaction();

                //foreach (var item in ProduceOtherMaterialDetailAccessor.Select(produceOtherMaterial))
                //{
                //    stockAccessor.Increment(new BL.DepotPositionManager().Get(item.DepotPositionId), item.Product, item.OtherMaterialQuantity);

                //    Model.ProduceOtherCompactMaterial CompactDetail = produceOtherCompactMaterialAccessor.Get(item.ProduceOtherCompactMaterialId);
                //    if (CompactDetail != null)
                //    {
                //        CompactDetail.AlreadyOutQuantity = CompactDetail.AlreadyOutQuantity == null ? 0 : CompactDetail.AlreadyOutQuantity - item.OtherMaterialQuantity;
                //        produceOtherCompactMaterialAccessor.Update(CompactDetail);
                //    }
                //}

                accessor.Delete(produceOtherMaterial.ProduceOtherMaterialId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        public Model.ProduceOtherMaterial GetDetails(string produceOtherMaterialID)
        {
            Model.ProduceOtherMaterial produceOtherMaterial = accessor.Get(produceOtherMaterialID);
            if (produceOtherMaterial != null)
                produceOtherMaterial.Details = ProduceOtherMaterialDetailAccessor.Select(produceOtherMaterial);
            return produceOtherMaterial;
        }
        /// <summary>
        /// Insert a ProduceOtherMaterial.
        /// </summary>
        public void Insert(Model.ProduceOtherMaterial produceOtherMaterial)
        {
            //
            // todo:add other logic here
            //
            Validate(produceOtherMaterial);


            try
            {
                produceOtherMaterial.InsertTime = DateTime.Now;

                produceOtherMaterial.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceOtherMaterial.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceOtherMaterial.InsertTime.Value.Year, produceOtherMaterial.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceOtherMaterial.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(produceOtherMaterial);

                foreach (Model.ProduceOtherMaterialDetail produceOtherMaterialDetail in produceOtherMaterial.Details)
                {
                    if (produceOtherMaterialDetail.Product == null || string.IsNullOrEmpty(produceOtherMaterialDetail.Product.ProductId))
                        throw new Exception("貨品不為空");
                    //if (produceOtherMaterialDetail.DepotPositionId == null)
                    //    throw new Helper.MessageValueException("貨位不能為空！");
                    produceOtherMaterialDetail.ProduceOtherMaterialId = produceOtherMaterial.ProduceOtherMaterialId;
                    ProduceOtherMaterialDetailAccessor.Insert(produceOtherMaterialDetail);

                    ////Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(produceOtherMaterialDetail.ProductId, produceOtherMaterialDetail.DepotPositionId);
                   // stockAccessor.Increment(new BL.DepotPositionManager().Get(produceOtherMaterialDetail.DepotPositionId), produceOtherMaterialDetail.Product, -produceOtherMaterialDetail.OtherMaterialQuantity);

                    //Model.ProduceOtherCompactMaterial produceOtherCompactMaterial = produceOtherCompactMaterialAccessor.Get(produceOtherMaterialDetail.ProduceOtherCompactMaterialId);
                    //if (produceOtherCompactMaterial != null)
                    //{
                    //    if (produceOtherCompactMaterial.AlreadyOutQuantity == null)
                    //        produceOtherCompactMaterial.AlreadyOutQuantity = 0;
                    //    produceOtherCompactMaterial.AlreadyOutQuantity += produceOtherMaterialDetail.OtherMaterialQuantity;
                    //    if (produceOtherCompactMaterial.AlreadyOutQuantity >= produceOtherCompactMaterial.ProduceQuantity)
                    //    {
                    //        produceOtherCompactMaterial.DetailsFlag = 2;
                    //    }
                    //    else
                    //    {
                    //        if (produceOtherCompactMaterial.AlreadyOutQuantity > 0)
                    //        {
                    //            produceOtherCompactMaterial.DetailsFlag = 1;
                    //        }
                    //        else
                    //        {
                    //            produceOtherCompactMaterial.DetailsFlag = 0;
                    //        }
                    //    }
                    //    produceOtherCompactMaterialAccessor.Update(produceOtherCompactMaterial);
                    //    UpdateProduceOtherCompactFlag(produceOtherCompactMaterial.ProduceOtherCompact);
                    //}
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
            IList<Model.ProduceOtherCompactMaterial> list = produceOtherCompactMaterialAccessor.Select(produceOtherCompact);
            foreach (Model.ProduceOtherCompactMaterial detail in list)
            {
                flag += detail.DetailsFlag == null ? 0 : detail.DetailsFlag.Value;
            }
            if (flag == 0)
                produceOtherCompact.InvoiceMaterialFlag = 0;
            else if (flag < list.Count * 2)
                produceOtherCompact.InvoiceMaterialFlag = 1;
            else if (flag == list.Count * 2)
                produceOtherCompact.InvoiceMaterialFlag = 2;
            new BL.ProduceOtherCompactManager().UpdateOtherCompact(produceOtherCompact);
        }
        /// <summary>
        /// Update a ProduceOtherMaterial.
        /// </summary>
        public void Update(Model.ProduceOtherMaterial produceOtherMaterial)
        {
            //
            // todo: add other logic here.
            //
            Validate(produceOtherMaterial);
            if (produceOtherMaterial != null)
            {
                this.Delete(produceOtherMaterial);
                produceOtherMaterial.UpdateTime = DateTime.Now;
                this.Insert(produceOtherMaterial);
            }
        }
        public void UpdateDepotOutState(Model.ProduceOtherMaterial produceOtherMaterial)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(produceOtherMaterial);
        }
        private void Validate(Model.ProduceOtherMaterial produceOtherMaterial)
        {
            if (string.IsNullOrEmpty(produceOtherMaterial.ProduceOtherMaterialId))
            {
                throw new Helper.RequireValueException(Model.ProduceOtherMaterial.PRO_ProduceOtherMaterialId);
            }
            //if (string.IsNullOrEmpty(produceOtherMaterial.WorkHouseId))
            //{
            //    throw new Helper.RequireValueException(Model.ProduceOtherMaterial.PROPERTY_WORKHOUSEID);
            //}
        }

        public IList<Model.ProduceOtherMaterial> SelectByCondition(DateTime startdate, DateTime enddate, string supperId1, string supperId2, string cid1, string cid2)
        {
            return accessor.SelectByCondition(startdate, enddate, supperId1, supperId2, cid1, cid2);
        }

        protected override string GetSettingId()
        {
            return "pomRule";
        }
        protected override string GetInvoiceKind()
        {
            return "pom";
        }
        public IList<Model.ProduceOtherMaterial> SelectState()
        {
            return accessor.SelectState();
        }
    }
}

