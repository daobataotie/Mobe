//------------------------------------------------------------------------------
//
// file name：StockEditorManager.cs
// author: mayanjun
// create date：2010-11-4 11:02:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.StockEditor.
    /// </summary>
    public partial class StockEditorManager : BaseManager
    {

        private static readonly DA.IStockEditorDetalAccessor stockEditorDetailsAccessor = (DA.IStockEditorDetalAccessor)Accessors.Get("StockEditorDetalAccessor");
        /// <summary>
        /// Delete StockEditor by primary key.
        /// </summary>
        public void Delete(string stockEditorId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(stockEditorId);
        }

        /// <summary>
        /// Insert a StockEditor.
        /// </summary>
        public void Insert(Model.StockEditor stockEditor)
        {
            try
            {
                BL.V.BeginTransaction();
                stockEditor.InsertTime = DateTime.Now;
                TiGuiExists(stockEditor);
                stockEditor.UpdateTime = DateTime.Now;
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, stockEditor.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, stockEditor.InsertTime.Value.Year, stockEditor.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, stockEditor.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = invoiceKind;

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(stockEditor);

                foreach (Model.StockEditorDetal stockEditorDetail in stockEditor.ProductPositionNums)
                {
                    stockEditorDetail.StockEditorId = stockEditor.StockEditorId;
                    stockEditorDetailsAccessor.Insert(stockEditorDetail);
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
        /// Update a StockEditor.
        /// </summary>
        public void Update(Model.StockEditor stockEditor)
        {
            try
            {
                BL.V.BeginTransaction();

                accessor.Update(stockEditor);

                foreach (Model.StockEditorDetal stockEditorDetail in stockEditor.ProductPositionNums)
                {
                    Model.StockEditorDetal temp = stockEditorDetailsAccessor.Get(stockEditorDetail.StockEditorDetalId);
                    if (temp != null)
                        stockEditorDetailsAccessor.Update(stockEditorDetail);
                    else
                        stockEditorDetailsAccessor.Insert(stockEditorDetail);
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        protected override string GetSettingId()
        {
            return "StockEditorRule";
        }

        protected override string GetInvoiceKind()
        {
            return "StockEditor";
        }

        public Model.StockEditor GetDetails(Model.StockEditor stockEditor)
        {
            stockEditor = this.Get(stockEditor.StockEditorId);
            if (stockEditor != null)
                stockEditor.Details = stockEditorDetailsAccessor.SelectByStockEditorId(stockEditor.StockEditorId);
            return stockEditor;
        }

        //public string GetNewId()
        //{
        //    string sequencekey = this.GetSettingId().ToLower();
        //    int sequenceval = SequenceManager.GetCurrentVal(sequencekey) + 1;
        //    return string.Format("{0}{1:d4}", System.DateTime.Now.ToString("yyyyMMdd"), sequenceval);
        //}

        public IList<Model.StockEditor> SelectNoStockCheck()
        {
            return accessor.SelectNoStockCheck();
        }
        private void TiGuiExists(Model.StockEditor model)
        {
            if (this.ExistsPrimary(model.StockEditorId))
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
                model.StockEditorId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }
    }
}

