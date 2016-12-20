//------------------------------------------------------------------------------
//
// file name：ProduceMaterialExitManager.cs
// author: peidun
// create date：2010-1-6 10:20:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceMaterialExit.
    /// </summary>
    public partial class ProduceMaterialExitManager : BaseManager
    {
        private static readonly DA.IProduceMaterialExitDetailAccessor detailsAccessor = (DA.IProduceMaterialExitDetailAccessor)Accessors.Get("ProduceMaterialExitDetailAccessor");
        private static readonly DA.IDepotPositionAccessor depotPositionAccessor = (DA.IDepotPositionAccessor)Accessors.Get("DepotPositionAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
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
                TiGuiExists(produceMaterialExit);
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
                addDetail(produceMaterialExit);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        private void addDetail(Model.ProduceMaterialExit produceMaterialExit)
        {
            foreach (Model.ProduceMaterialExitDetail produceMaterialExitDetail in produceMaterialExit.Detail)
            {
                if (produceMaterialExitDetail.Product == null || string.IsNullOrEmpty(produceMaterialExitDetail.Product.ProductId))
                    continue;
                produceMaterialExitDetail.ProduceMaterialExitId = produceMaterialExit.ProduceMaterialExitId;
                detailsAccessor.Insert(produceMaterialExitDetail);
                stockAccessor.Increment(depotPositionAccessor.Get(produceMaterialExitDetail.DepotPositionId), produceMaterialExitDetail.Product, produceMaterialExitDetail.ProduceQuantity);
                productAccessor.UpdateProduct_Stock(produceMaterialExitDetail.Product);
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
            try
            {
                BL.V.BeginTransaction();
                if (produceMaterialExit != null)
                {
                    produceMaterialExit.UpdateTime = DateTime.Now;
                    accessor.Update(produceMaterialExit);
                    Model.ProduceMaterialExit oldModel = this.GetDetails(produceMaterialExit.ProduceMaterialExitId);
                    cancelAffect(oldModel);
                    detailsAccessor.Delete(produceMaterialExit);
                    addDetail(produceMaterialExit);

                }
                BL.V.CommitTransaction();
            }

            catch
            {
                BL.V.RollbackTransaction();
                throw;
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
            cancelAffect(produceMaterialExit);
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
            foreach (Model.ProduceMaterialExitDetail detail in produceMaterialExit.Detail)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    continue;
                if (string.IsNullOrEmpty(detail.DepotPositionId))
                    throw new Helper.RequireValueException(Model.ProduceOtherExitDetail.PRO_DepotPositionId);
            }
        }

        private void TiGuiExists(Model.ProduceMaterialExit model)
        {
            if (this.ExistsPrimary(model.ProduceMaterialExitId))
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
                model.ProduceMaterialExitId = this.GetId();
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
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
        private void cancelAffect(Model.ProduceMaterialExit ProduceMaterialExit)
        {
            foreach (Model.ProduceMaterialExitDetail oldDetail in ProduceMaterialExit.Detail)
            {
                if (oldDetail.Product == null || oldDetail.Product.ProductId == null) continue;
                oldDetail.DepotPosition = depotPositionAccessor.Get(oldDetail.DepotPositionId);
                stockAccessor.Decrement(oldDetail.DepotPosition, oldDetail.Product, oldDetail.ProduceQuantity);
            }
        }

        /// <summary>
        /// 根据加工单头编号,查询退料单据
        /// </summary>
        /// <param name="produceHeaderid">加工单头编号</param>
        /// <returns></returns>
        public IList<Model.ProduceMaterialExit> SelectByProduceHeaderId(string pronoteHeaderid)
        {
            return accessor.SelectByProduceHeaderId(pronoteHeaderid);
        }

        public IList<Book.Model.ProduceMaterialExit> SelectForListForm(DateTime startDate, DateTime endDate, string startPMEId, string endPMEId, string startPronoteHeaderId, string endPronoteHeaderId, Book.Model.Product startProduct, Book.Model.Product endProduct)
        {
            return accessor.SelectForListForm(startDate, endDate, startPMEId, endPMEId, startPronoteHeaderId, endPronoteHeaderId, startProduct, endProduct);
        }
    }
}

