//------------------------------------------------------------------------------
//
// file name：ProductOnlineCheckManager.cs
// author: mayanjun
// create date：2013-3-25 17:50:56
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductOnlineCheck.
    /// </summary>
    public partial class ProductOnlineCheckManager : BaseManager
    {

        /// <summary>
        /// Delete ProductOnlineCheck by primary key.
        /// </summary>
        public void Delete(string productOnlineCheckId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                (new BL.ProductOnlineCheckDetailManager()).DelectByProductOnlineCheckId(productOnlineCheckId);
                accessor.Delete(productOnlineCheckId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a ProductOnlineCheck.
        /// </summary>
        public void Insert(Model.ProductOnlineCheck productOnlineCheck)
        {
            //
            // todo:add other logic here
            //
            Validate(productOnlineCheck);
            try
            {
                BL.V.BeginTransaction();
                productOnlineCheck.InsertTime = DateTime.Now;
                productOnlineCheck.UpdateTime = DateTime.Now;
                accessor.Insert(productOnlineCheck);

                foreach (Model.ProductOnlineCheckDetail model in productOnlineCheck.Detail)
                {
                    (new BL.ProductOnlineCheckDetailManager()).Insert(model);
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
        /// Update a ProductOnlineCheck.
        /// </summary>
        public void Update(Model.ProductOnlineCheck productOnlineCheck)
        {
            //
            // todo: add other logic here.
            //
            Validate(productOnlineCheck);
            try
            {
                BL.V.BeginTransaction();
                productOnlineCheck.UpdateTime = DateTime.Now;
                accessor.Update(productOnlineCheck);

                (new BL.ProductOnlineCheckDetailManager()).DelectByProductOnlineCheckId(productOnlineCheck.ProductOnlineCheckId);
                foreach (Model.ProductOnlineCheckDetail model in productOnlineCheck.Detail)
                {
                    (new BL.ProductOnlineCheckDetailManager()).Insert(model);
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
            return "ProductOnlineCheckRule";
        }

        protected override string GetInvoiceKind()
        {
            return "POC";
        }

        public void TiGuiExists(Model.ProductOnlineCheck model)
        {
            if (this.ExistsPrimary(model.ProductOnlineCheckId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, DateTime.Now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, DateTime.Now.Year, DateTime.Now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, DateTime.Now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.ProductOnlineCheckId = this.GetId(DateTime.Now);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }

        private void Validate(Model.ProductOnlineCheck model)
        {
            //if (model.ProductOnlineCheckDate == null)
            //    throw new Helper.RequireValueException(Model.ProductOnlineCheck.PRO_ProductOnlineCheckDate);
            if (model.OnlineDate == null)
                throw new Helper.RequireValueException(Model.ProductOnlineCheck.PRO_OnlineDate);
            if (model.ProductId == null)
                throw new Helper.RequireValueException(Model.ProductOnlineCheck.PRO_ProductId);
        }

        public IList<Model.ProductOnlineCheck> SelectByDate(DateTime startDate, DateTime endDate, string InvoiceCusId)
        {
            return accessor.SelectByDate(startDate, endDate,InvoiceCusId);
        }
    }
}

