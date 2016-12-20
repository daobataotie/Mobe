//------------------------------------------------------------------------------
//
// file name：ProduceOtherReturnMaterialManager.cs
// author: mayanjun
// create date：2011-08-31 15:05:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherReturnMaterial.
    /// </summary>
    public partial class ProduceOtherReturnMaterialManager : BaseManager
    {
        private static readonly DA.IProduceOtherReturnDetailAccessor accessorDetail = (DA.IProduceOtherReturnDetailAccessor)Accessors.Get("ProduceOtherReturnDetailAccessor");
        private static readonly DA.IStockAccessor stockAccess = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IProductAccessor productAccess = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IDepotPositionAccessor depotpositionAccess = (DA.IDepotPositionAccessor)Accessors.Get("DepotPositionAccessor");
        private static readonly DA.IProduceOtherCompactDetailAccessor produceOtherCompactDetailAccessor = (DA.IProduceOtherCompactDetailAccessor)Accessors.Get("ProduceOtherCompactDetailAccessor");
        /// <summary>
        /// Delete ProduceOtherReturnMaterial by primary key.
        /// </summary>
        public void Delete(string produceOtherReturnMaterialId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                CancelAffect(accessor.Get(produceOtherReturnMaterialId));
                accessor.Delete(produceOtherReturnMaterialId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        private void CancelAffect(Model.ProduceOtherReturnMaterial model)
        {
            foreach (Model.ProduceOtherReturnDetail item in accessorDetail.Select(model))
            {

                Model.ProduceOtherCompactDetail CompactDetail = produceOtherCompactDetailAccessor.Get(item.ProduceOtherCompactDetailId);
                if (CompactDetail != null)
                {
                    CompactDetail.CancelQuantity = CompactDetail.CancelQuantity == null ? 0 : CompactDetail.CancelQuantity - item.Quantity;
                    produceOtherCompactDetailAccessor.Update(CompactDetail);
                }
            }
        }
        public void Delete(Model.ProduceOtherReturnMaterial produceOtherReturnMaterial)
        {
            this.Delete(produceOtherReturnMaterial.ProduceOtherReturnMaterialId);           
        }
        /// <summary>
        /// Insert a ProduceOtherReturnMaterial.
        /// </summary>
        public void Insert(Model.ProduceOtherReturnMaterial produceOtherReturnMaterial)
        {
            //
            // todo:add other logic here
            //

           // Valid(produceOtherReturnMaterial);

            try
            {
                BL.V.BeginTransaction();
                produceOtherReturnMaterial.InsertTime = DateTime.Now;
                produceOtherReturnMaterial.UpdateTime = DateTime.Now;
                TiGuiExists(produceOtherReturnMaterial);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceOtherReturnMaterial.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceOtherReturnMaterial.InsertTime.Value.Year, produceOtherReturnMaterial.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceOtherReturnMaterial.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(produceOtherReturnMaterial);
                foreach (Model.ProduceOtherReturnDetail item in produceOtherReturnMaterial.Details)
                {
                    item.ProduceOtherReturnMaterialId = produceOtherReturnMaterial.ProduceOtherReturnMaterialId;
                    accessorDetail.Insert(item);

                    Model.ProduceOtherCompactDetail CompactDetail = produceOtherCompactDetailAccessor.Get(item.ProduceOtherCompactDetailId);
                    if (CompactDetail != null)
                    {
                        if (CompactDetail.CancelQuantity == null)
                            CompactDetail.CancelQuantity = 0;
                        CompactDetail.CancelQuantity += item.Quantity;                      
                        produceOtherCompactDetailAccessor.Update(CompactDetail);                 
                    }
                    //stockAccess.Increment(depotpositionAccess.Get(item.DepotPositionId), item.Product, item.Quantity);
                    //item.Product.StocksQuantity += stockAccess.GetTheCountByProduct(item.Product);
                    //item.Product.ProductImage = new byte[0];
                    //item.Product.ProductImage1 = new byte[0];
                    //item.Product.ProductImage2 = new byte[0];
                    //item.Product.ProductImage3 = new byte[0];
                    //productAccess.Update(item.Product);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }


        void Valid(Model.ProduceOtherReturnMaterial Material)
        {
            foreach (Model.ProduceOtherReturnDetail detail in Material.Details)
                if (string.IsNullOrEmpty(detail.DepotPositionId))
                    throw new global::Helper.RequireValueException(Model.ProduceOtherReturnDetail.PRO_DepotPositionId);
        }

        /// <summary>
        /// Update a ProduceOtherReturnMaterial.
        /// </summary>
        ///
        public void Update(Model.ProduceOtherReturnMaterial produceOtherReturnMaterial)
        {
            //
            // todo: add other logic here.
            //
            //accessor.Update(produceOtherReturnMaterial);
            this.Delete(produceOtherReturnMaterial);
            this.Insert(produceOtherReturnMaterial);
        }

        public Model.ProduceOtherReturnMaterial GetDetails(Model.ProduceOtherReturnMaterial Material)
        {
            Model.ProduceOtherReturnMaterial temp = accessor.Get(Material.ProduceOtherReturnMaterialId);
            if (temp != null)
                temp.Details = accessorDetail.Select(temp);
            return temp;
        }

        protected override string GetSettingId()
        {
            return "ProduceOtherReturnMaterialRule";
        }

        protected override string GetInvoiceKind()
        {
            return "ProduceOtherReturnMaterial";
        }

        public IList<Model.ProduceOtherReturnMaterial> Select(DateTime startdate, DateTime enddate)
        {
            return accessor.Select(startdate, enddate);
        }
        private void TiGuiExists(Model.ProduceOtherReturnMaterial model)
        {
            if (this.ExistsPrimary(model.ProduceOtherReturnMaterialId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.InsertTime.Value.Year, model.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.ProduceOtherReturnMaterialId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }
    }
}

