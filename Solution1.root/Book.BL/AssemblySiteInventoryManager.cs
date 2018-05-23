//------------------------------------------------------------------------------
//
// file name：AssemblySiteInventoryManager.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AssemblySiteInventory.
    /// </summary>
    public partial class AssemblySiteInventoryManager : BaseManager
    {
        private static readonly DA.IAssemblySiteInventoryDetailAccessor detailaccessor = (DA.IAssemblySiteInventoryDetailAccessor)Accessors.Get("AssemblySiteInventoryDetailAccessor");

        /// <summary>
        /// Delete AssemblySiteInventory by primary key.
        /// </summary>
        public void Delete(string assemblySiteInventoryId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                detailaccessor.DeleteByHeaderId(assemblySiteInventoryId);
                accessor.Delete(assemblySiteInventoryId);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Insert a AssemblySiteInventory.
        /// </summary>
        public void Insert(Model.AssemblySiteInventory assemblySiteInventory)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(assemblySiteInventory);
                this.TiGuiExists(assemblySiteInventory);

                assemblySiteInventory.InsertTime = DateTime.Now;
                assemblySiteInventory.UpdateTime = DateTime.Now;

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, DateTime.Now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, DateTime.Now.Year, DateTime.Now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, DateTime.Now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(assemblySiteInventory);

                foreach (var item in assemblySiteInventory.Details)
                {
                    item.AssemblySiteInventoryId = assemblySiteInventory.AssemblySiteInventoryId; //防止ID变更后不同
                    detailaccessor.Insert(item);
                }

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Update a AssemblySiteInventory.
        /// </summary>
        public void Update(Model.AssemblySiteInventory assemblySiteInventory)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(assemblySiteInventory);
                assemblySiteInventory.UpdateTime = DateTime.Now;

                accessor.Update(assemblySiteInventory);

                detailaccessor.DeleteByHeaderId(assemblySiteInventory.AssemblySiteInventoryId);
                foreach (var item in assemblySiteInventory.Details)
                {
                    item.AssemblySiteInventoryId = assemblySiteInventory.AssemblySiteInventoryId;
                    detailaccessor.Insert(item);
                }

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        protected override string GetInvoiceKind()
        {
            return "ASI";
        }

        protected override string GetSettingId()
        {
            return "AssemblySiteInventoryRule";
        }

        public void TiGuiExists(Model.AssemblySiteInventory model)
        {
            if (this.ExistsPrimary(model.AssemblySiteInventoryId))
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
                model.AssemblySiteInventoryId = this.GetId(DateTime.Now);
                TiGuiExists(model);
            }
        }

        public void Validate(Model.AssemblySiteInventory model)
        {
            if (model.InvoiceDate == null)
                throw new Helper.InvalidValueException(Model.AssemblySiteInventory.PRO_InvoiceDate);

            foreach (var item in model.Details)
            {
                if (item.Quantity == null)
                    throw new Helper.MessageValueException(string.Format("商品：{0} 的盘点数量不能为空！", item.Product.ToString()));
            }
        }

        public Model.AssemblySiteInventory GetDetail(string id)
        {
            Model.AssemblySiteInventory model = this.Get(id);
            if (model != null)
                model.Details = detailaccessor.SelectByHeaderId(id);

            return model;
        }

        public void UpdateState(bool state, string assemblySiteInventoryId)
        {
            accessor.UpdateState(state, assemblySiteInventoryId);
        }
    }
}
