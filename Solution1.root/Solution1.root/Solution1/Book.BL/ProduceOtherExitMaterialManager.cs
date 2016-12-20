//------------------------------------------------------------------------------
//
// file name：ProduceOtherExitMaterialManager.cs
// author: peidun
// create date：2010-1-6 10:20:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherExitMaterial.
    /// </summary>
    public partial class ProduceOtherExitMaterialManager : BaseManager
    {
        private static readonly DA.IProduceOtherExitDetailAccessor ProduceOtherExitDetailAccessor = (DA.IProduceOtherExitDetailAccessor)Accessors.Get("ProduceOtherExitDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IDepotPositionAccessor depotPositionAccessor = (DA.IDepotPositionAccessor)Accessors.Get("DepotPositionAccessor");
        /// <summary>
        /// Delete ProduceOtherExitMaterial by primary key.
        /// </summary>
        public void Delete(string produceOtherExitMaterialId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceOtherExitMaterialId);
        }
        public void Delete(Model.ProduceOtherExitMaterial produceOtherExitMaterial)
        {
            foreach (Model.ProduceOtherExitDetail produceOtherExitDetail in ProduceOtherExitDetailAccessor.Select(produceOtherExitMaterial))
                stockAccessor.Increment(produceOtherExitDetail.DepotPosition, produceOtherExitDetail.Product, produceOtherExitDetail.ProduceQuantity);
            accessor.Delete(produceOtherExitMaterial.ProduceOtherExitMaterialId);
        }
        public Model.ProduceOtherExitMaterial GetDetails(string produceOtherExitMaterialId)
        {
            Model.ProduceOtherExitMaterial produceOtherExitMaterial = accessor.Get(produceOtherExitMaterialId);
            produceOtherExitMaterial.Details = ProduceOtherExitDetailAccessor.Select(produceOtherExitMaterial);
            return produceOtherExitMaterial;
        }
        /// <summary>
        /// Insert a ProduceOtherExitMaterial.
        /// </summary>
        public void Insert(Model.ProduceOtherExitMaterial produceOtherExitMaterial)
        {
            //
            // todo:add other logic here
            //
            Validate(produceOtherExitMaterial);


            try
            {
                produceOtherExitMaterial.InsertTime = DateTime.Now;

                produceOtherExitMaterial.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceOtherExitMaterial.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceOtherExitMaterial.InsertTime.Value.Year, produceOtherExitMaterial.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceOtherExitMaterial.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(produceOtherExitMaterial);

                foreach (Model.ProduceOtherExitDetail produceOtherExitDetail in produceOtherExitMaterial.Details)
                {
                    if (produceOtherExitDetail.Product == null || string.IsNullOrEmpty(produceOtherExitDetail.Product.ProductId))
                        throw new Exception("貨品不為空");
                    if (produceOtherExitDetail.DepotPositionId == null)
                        throw new Helper.MessageValueException("貨位不能為空！");
                    produceOtherExitDetail.ProduceOtherExitMaterialId = produceOtherExitMaterial.ProduceOtherExitMaterialId;                
                    ProduceOtherExitDetailAccessor.Insert(produceOtherExitDetail);
                    Model.Stock temp = stockAccessor.GetStockByProductIdAndDepotPositionId(produceOtherExitDetail.ProductId, produceOtherExitDetail.DepotPositionId);
                    if (temp!=null&&temp.StockQuantity1 < produceOtherExitDetail.ProduceQuantity)
                        throw new Helper.MessageValueException("出庫數量大於庫存！");
                    stockAccessor.Decrement(depotPositionAccessor.Get( produceOtherExitDetail.DepotPositionId), produceOtherExitDetail.Product, produceOtherExitDetail.ProduceQuantity);
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
        /// Update a ProduceOtherExitMaterial.
        /// </summary>
        public void Update(Model.ProduceOtherExitMaterial produceOtherExitMaterial)
        {
            //
            // todo: add other logic here.
            //
            Validate(produceOtherExitMaterial);
            if (produceOtherExitMaterial != null)
            {
                this.Delete(produceOtherExitMaterial);
                produceOtherExitMaterial.UpdateTime = DateTime.Now;
                this.Insert(produceOtherExitMaterial);
            }
        }
        private void Validate(Model.ProduceOtherExitMaterial produceOtherExitMaterial)
        {
            if (string.IsNullOrEmpty(produceOtherExitMaterial.ProduceOtherExitMaterialId))
            {
                throw new Helper.RequireValueException(Model.ProduceOtherExitMaterial.PRO_ProduceOtherExitMaterialId);
            }
            if (produceOtherExitMaterial.Depot == null)
                throw new Helper.MessageValueException("倉庫不能為空！");
            //if (string.IsNullOrEmpty(produceOtherExitMaterial.WorkHouseId))
            //{
            //    throw new Helper.RequireValueException(Model.ProduceOtherExitMaterial.PROPERTY_WORKHOUSEID);
            //}
        }

        public  IList<Model.ProduceOtherExitMaterial> SelectByCondition(DateTime startDate, DateTime endDate, string compactId1,string compactId2,string supperId1,string supperId2)
        {
            return accessor.SelectByCondition(startDate, endDate, compactId1, compactId2, supperId1, supperId2);
        }

        protected override string GetSettingId()
        {
            return "poemRule";
        }
        protected override string GetInvoiceKind()
        {
            return "poem";
        }
    }
}

