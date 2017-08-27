//------------------------------------------------------------------------------
//
// file name：ProduceInDepotManager.cs
// author: peidun
// create date：2010-1-8 13:43:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceInDepot.
    /// </summary>
    public partial class ProduceInDepotManager : BaseManager
    {
        private static readonly DA.IProduceInDepotDetailAccessor DetailAccessor = (DA.IProduceInDepotDetailAccessor)Accessors.Get("ProduceInDepotDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IPronoteHeaderAccessor pronoteHeaderAccessor = (DA.IPronoteHeaderAccessor)Accessors.Get("PronoteHeaderAccessor");
        private ProductManager productManager = new ProductManager();

        /// <summary>
        /// Delete ProduceInDepot by primary key.
        /// </summary>
        public void Delete(string produceInDepotId)
        {
            accessor.Delete(produceInDepotId);
        }

        public void Delete(Model.ProduceInDepot produceInDepot)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                calEffectDelete(DetailAccessor.Select(produceInDepot));
                accessor.Delete(produceInDepot.ProduceInDepotId);
                //修改详细合计生产数量,合计合格数量 2012年6月7日10:38:42
                this.UpdateDetailHjSum(produceInDepot);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        public Model.ProduceInDepot GetDetails(string produceInDepotId)
        {
            Model.ProduceInDepot produceInDepot = accessor.Get(produceInDepotId);
            produceInDepot.Details = DetailAccessor.Select(produceInDepot);
            return produceInDepot;
        }

        /// <summary>
        /// Insert a ProduceInDepot.
        /// </summary>
        public void Insert(Model.ProduceInDepot produceInDepot)
        {
            Validate(produceInDepot);
            try
            {
                BL.V.BeginTransaction();
                produceInDepot.InsertTime = produceInDepot.ProduceInDepotDate;
                produceInDepot.UpdateTime = DateTime.Now;
                TiGuiExists(produceInDepot);

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
                addDetail(produceInDepot);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        private void calEffectDelete(IList<Model.ProduceInDepotDetail> detail)
        {
            foreach (Model.ProduceInDepotDetail produceInDepotDetail in detail)
            {
                if (!string.IsNullOrEmpty(produceInDepotDetail.DepotPositionId))
                {
                    //Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(produceInDepotDetail.ProductId, produceInDepotDetail.DepotPositionId);
                    stockAccessor.Decrement(produceInDepotDetail.DepotPosition, produceInDepotDetail.Product, produceInDepotDetail.ProduceQuantity);
                    productManager.UpdateProduct_Stock(produceInDepotDetail.Product);
                    //if (!string.IsNullOrEmpty(produceInDepotDetail.PronoteHeaderId))
                    //    pronoteHeaderAccessor.UpdatePronoteIsClose(produceInDepotDetail.PronoteHeaderId, 0 - produceInDepotDetail.CheckOutSum);

                }
            }
        }

        private void calEffectUpdate(IList<Model.ProduceInDepotDetail> detail)
        {
            foreach (Model.ProduceInDepotDetail produceInDepotDetail in detail)
            {
                if (!string.IsNullOrEmpty(produceInDepotDetail.DepotPositionId))
                {
                    //Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(produceInDepotDetail.ProductId, produceInDepotDetail.DepotPositionId);
                    stockAccessor.Decrement(produceInDepotDetail.DepotPosition, produceInDepotDetail.Product, produceInDepotDetail.ProduceQuantity);
                    productManager.UpdateProduct_Stock(produceInDepotDetail.Product);
                    //if (!string.IsNullOrEmpty(produceInDepotDetail.PronoteHeaderId))
                    //    pronoteHeaderAccessor.UpdatePronoteIsClose(produceInDepotDetail.PronoteHeaderId, 0-produceInDepotDetail.CheckOutSum);

                }
            }
        }

        private void addDetail(Model.ProduceInDepot produceInDepot)
        {
            foreach (Model.ProduceInDepotDetail produceInDepotDetail in produceInDepot.Details)
            {
                if (produceInDepotDetail.Product == null || string.IsNullOrEmpty(produceInDepotDetail.Product.ProductId)) continue;
                produceInDepotDetail.ProduceInDepotId = produceInDepot.ProduceInDepotId;

                //如果没有加工单号
                //if (string.IsNullOrEmpty(produceInDepotDetail.PronoteHeaderId))
                //{
                //    produceInDepotDetail.HeJiProceduresSum = 0;
                //    produceInDepotDetail.HeJiCheckOutSum = 0;
                //    produceInDepotDetail.HeJiProduceQuantity = 0;
                //    produceInDepotDetail.HeJiProduceTransferQuantity = 0;
                //    DetailAccessor.Insert(produceInDepotDetail);
                //}

                double? _InProceduresSum = produceInDepot.Details.Where(d => d.Inumber <= produceInDepotDetail.Inumber && d.ProductId == produceInDepotDetail.ProductId && d.PronoteHeaderId == produceInDepotDetail.PronoteHeaderId).Sum(d => d.ProceduresSum);
                double? _InCheckOutSum = produceInDepot.Details.Where(d => d.Inumber <= produceInDepotDetail.Inumber && d.ProductId == produceInDepotDetail.ProductId && d.PronoteHeaderId == produceInDepotDetail.PronoteHeaderId).Sum(d => d.CheckOutSum);
                double? _InProduceQuantity = produceInDepot.Details.Where(d => d.Inumber <= produceInDepotDetail.Inumber && d.ProductId == produceInDepotDetail.ProductId && d.PronoteHeaderId == produceInDepotDetail.PronoteHeaderId).Sum(d => d.ProduceQuantity);
                double? _InProduceTransferQuantity = produceInDepot.Details.Where(d => d.Inumber <= produceInDepotDetail.Inumber && d.ProductId == produceInDepotDetail.ProductId && d.PronoteHeaderId == produceInDepotDetail.PronoteHeaderId).Sum(d => d.ProduceTransferQuantity);

                produceInDepotDetail.HeJiProceduresSum = DetailAccessor.Get_HJForColumnName(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId, produceInDepotDetail.ProductId, produceInDepotDetail.ProduceInDepotId, produceInDepotDetail.Inumber.Value, produceInDepot.InsertTime.Value, "ProceduresSum") + _InProceduresSum;
                produceInDepotDetail.HeJiCheckOutSum = DetailAccessor.Get_HJForColumnName(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId, produceInDepotDetail.ProductId, produceInDepotDetail.ProduceInDepotId, produceInDepotDetail.Inumber.Value, produceInDepot.InsertTime.Value, "CheckOutSum") + _InCheckOutSum;
                produceInDepotDetail.HeJiProduceQuantity = DetailAccessor.Get_HJForColumnName(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId, produceInDepotDetail.ProductId, produceInDepotDetail.ProduceInDepotId, produceInDepotDetail.Inumber.Value, produceInDepot.InsertTime.Value, "ProduceQuantity") + _InProduceQuantity;
                produceInDepotDetail.HeJiProduceTransferQuantity = DetailAccessor.Get_HJForColumnName(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId, produceInDepotDetail.ProductId, produceInDepotDetail.ProduceInDepotId, produceInDepotDetail.Inumber.Value, produceInDepot.InsertTime.Value, "ProduceTransferQuantity") + _InProduceTransferQuantity;

                DetailAccessor.Insert(produceInDepotDetail);

                if (!string.IsNullOrEmpty(produceInDepotDetail.DepotPositionId))
                {
                    //Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(produceInDepotDetail.ProductId, produceInDepotDetail.DepotPositionId);
                    stockAccessor.Increment(new BL.DepotPositionManager().Get(produceInDepotDetail.DepotPositionId), produceInDepotDetail.Product, produceInDepotDetail.ProduceQuantity);
                    productManager.UpdateProduct_Stock(produceInDepotDetail.Product);
                }
                if (isUpdate == 1)
                {
                    IList<Model.ProduceInDepotDetail> _detailsList = DetailAccessor.select_NextbyPronHeaderId(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId, produceInDepotDetail.ProductId, produceInDepot.InsertTime.Value);

                    var gbyHeaderId = from Model.ProduceInDepotDetail d in _detailsList
                                      group d by d.ProduceInDepotId;

                    foreach (IList<Model.ProduceInDepotDetail> outdetail in gbyHeaderId)
                    {
                        foreach (Model.ProduceInDepotDetail detail in outdetail)
                        {
                            double? _nProceduresSum = outdetail.Where(d => d.Inumber <= detail.Inumber.Value).Sum(d => d.ProceduresSum);
                            double? _nCheckOutSum = outdetail.Where(d => d.Inumber <= detail.Inumber).Sum(d => d.CheckOutSum);
                            double? _nProduceQuantity = outdetail.Where(d => d.Inumber <= detail.Inumber).Sum(d => d.ProduceQuantity);
                            double? _nProduceTransferQuantity = outdetail.Where(d => d.Inumber <= detail.Inumber).Sum(d => d.ProduceTransferQuantity);

                            detail.HeJiProceduresSum = DetailAccessor.Get_HJForColumnName(detail.PronoteHeaderId, produceInDepot.WorkHouseId, detail.ProductId, detail.ProduceInDepotId, detail.Inumber.Value, detail.ProduceInDepot.InsertTime.Value, "ProceduresSum") + _nProceduresSum;
                            detail.HeJiCheckOutSum = DetailAccessor.Get_HJForColumnName(detail.PronoteHeaderId, produceInDepot.WorkHouseId, detail.ProductId, detail.ProduceInDepotId, detail.Inumber.Value, detail.ProduceInDepot.InsertTime.Value, "CheckOutSum") + _nCheckOutSum;
                            detail.HeJiProduceQuantity = DetailAccessor.Get_HJForColumnName(detail.PronoteHeaderId, produceInDepot.WorkHouseId, detail.ProductId, detail.ProduceInDepotId, detail.Inumber.Value, detail.ProduceInDepot.InsertTime.Value, "ProduceQuantity") + _nProduceQuantity;
                            detail.HeJiProduceTransferQuantity = DetailAccessor.Get_HJForColumnName(detail.PronoteHeaderId, produceInDepot.WorkHouseId, detail.ProductId, detail.ProduceInDepotId, detail.Inumber.Value, detail.ProduceInDepot.InsertTime.Value, "ProduceTransferQuantity") + _nProduceTransferQuantity;

                            DetailAccessor.Update(detail);

                            //detail.HeJiProceduresSum = DetailAccessor.select_SumbyPronHeaderId(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId, produceInDepotDetail.ProductId) + DetailAccessor.select_FrontSumByProduceIndepotIdAndProId(produceInDepot.ProduceInDepotId, produceInDepotDetail.ProductId, produceInDepotDetail.Inumber.Value) + detail.ProceduresSum;

                            //detail.HeJiCheckOutSum = DetailAccessor.select_CheckOutSumByPronHeaderId(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId, produceInDepotDetail.ProductId) + DetailAccessor.select_FrontCheckoutSumByProduceIndepotIdAndProId(produceInDepot.ProduceInDepotId, produceInDepotDetail.ProductId, produceInDepotDetail.Inumber.Value) + detail.CheckOutSum;


                            //this.UpdateSql(@"update ProduceInDepotDetail set HeJiProceduresSum=" + (DetailAccessor.select_SumbyPronHeaderId(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId) + detail.ProceduresSum) + " , HeJiCheckOutSum=" + (DetailAccessor.select_CheckOutSumByPronHeaderId(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId) + detail.CheckOutSum) + " where produceInDepotDetailId='"+detail.ProduceInDepotDetailId+"'");
                        }
                    }
                }
                //更新加工单 已合格数量 和是否
                //if (!string.IsNullOrEmpty(produceInDepotDetail.PronoteHeaderId))
                //    pronoteHeaderAccessor.UpdatePronoteIsClose(produceInDepotDetail.PronoteHeaderId, produceInDepotDetail.CheckOutSum);

            }
        }

        //修改详细合计生产数量,合计合格数量 2012年6月7日10:38:42
        private void UpdateDetailHjSum(Model.ProduceInDepot produceindepot)
        {
            foreach (Model.ProduceInDepotDetail detail in produceindepot.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;

                IList<Model.ProduceInDepotDetail> updatelist = DetailAccessor.Select_ByWorkHosueAndPronoteId(produceindepot.WorkHouseId, detail.PronoteHeaderId);
                foreach (Model.ProduceInDepotDetail indetail in updatelist)
                {
                    indetail.HeJiProceduresSum = (from i in updatelist where i.ProduceInDepot.InsertTime < indetail.ProduceInDepot.InsertTime select i.ProceduresSum).Sum() + indetail.ProceduresSum;
                    indetail.HeJiCheckOutSum = (from i in updatelist where i.ProduceInDepot.InsertTime < indetail.ProduceInDepot.InsertTime select i.CheckOutSum).Sum() + indetail.CheckOutSum;
                    DetailAccessor.Update(indetail);
                }
            }
        }

        private int isUpdate = 0; //修改状态

        public void _Update(Model.ProduceInDepot produceInDepot)
        {
            isUpdate = 1;
            produceInDepot.UpdateTime = DateTime.Now;
            accessor.Update(produceInDepot);

            IList<Model.ProduceInDepotDetail> olddetail = DetailAccessor.Select(produceInDepot);
            calEffectUpdate(olddetail);
            DetailAccessor.DeleteByHeader(produceInDepot);
            addDetail(produceInDepot);
            //修改详细合计生产数量,合计合格数量 2012年6月7日10:38:42
            //this.UpdateDetailHjSum(produceInDepot);
            isUpdate = 0;
        }

        public void Update(Model.ProduceInDepot produceInDepot)
        {
            Validate(produceInDepot);
            //若已有修改,则抛出异常
            //if (produceInDepot.UpdateTime != this.JudgeHasNewVersion<Model.ProduceInDepot>(produceInDepot, produceInDepot.ProduceInDepotId))
            //{
            //    throw new Helper.VersionOverTimeException();
            //}
            try
            {
                BL.V.BeginTransaction();
                this._Update(produceInDepot);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
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
            //foreach (var item in produceInDepot.Details)
            //{
            //    if (item.ProduceQuantity > item.CheckOutSum)
            //        throw new Helper.MessageValueException("入庫數量不能大於合格數量");
            //    if (item.ProduceTransferQuantity > item.CheckOutSum)
            //        throw new Helper.MessageValueException("轉生產數量不能大於合格數量");
            //}

            //foreach (Model.ProduceInDepotDetail d in produceInDepot.Details)
            //{
            //    if ((d.HeJiProceduresSum.HasValue ? d.HeJiProceduresSum.Value : 0) > (d.beforeTransferQuantity.HasValue ? d.beforeTransferQuantity.Value : 0))
            //        throw new Helper.InvalidValueException(Model.ProduceInDepotDetail.PRO_HeJiProceduresSum);
            //}
        }

        protected override string GetSettingId()
        {
            return "pidRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pid";
        }

        private void TiGuiExists(Model.ProduceInDepot model)
        {
            if (this.ExistsPrimary(model.ProduceInDepotId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.ProduceInDepotDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.ProduceInDepotDate.Value.Year, model.ProduceInDepotDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.ProduceInDepotDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.ProduceInDepotId = this.GetId(model.ProduceInDepotDate.Value);
                TiGuiExists(model);
            }

        }

        public IList<Model.ProduceInDepot> SelectByDateRange(DateTime stardate, DateTime enddate)
        {
            return accessor.SelectByDateRange(stardate, enddate);
        }

        public IList<Model.ProduceInDepot> SelectExcel(DateTime startDate, DateTime endDate, string workHouseId, string keyWords)
        {
            return accessor.SelectExcel(startDate, endDate, workHouseId, keyWords);
        }
    }
}

