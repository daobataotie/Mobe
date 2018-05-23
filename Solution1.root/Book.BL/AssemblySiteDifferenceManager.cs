//------------------------------------------------------------------------------
//
// file name：AssemblySiteDifferenceManager.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AssemblySiteDifference.
    /// </summary>
    public partial class AssemblySiteDifferenceManager : BaseManager
    {
        private static readonly DA.IAssemblySiteDifferenceDetaiAccessor detailaccessor = (DA.IAssemblySiteDifferenceDetaiAccessor)Accessors.Get("AssemblySiteDifferenceDetaiAccessor");


        /// <summary>
        /// Delete AssemblySiteDifference by primary key.
        /// </summary>
        public void Delete(string assemblySiteDifferenceId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();

                string assemblySiteInventoryId=this.Get(assemblySiteDifferenceId).AssemblySiteInventoryId;
                detailaccessor.DeleteByHeaderId(assemblySiteDifferenceId);
                accessor.Delete(assemblySiteDifferenceId);
                new BL.AssemblySiteInventoryManager().UpdateState(false, assemblySiteInventoryId);    //设置对应的盘点录入单“未生成盘点差异”状态

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Insert a AssemblySiteDifference.
        /// </summary>
        public void Insert(Model.AssemblySiteDifference assemblySiteDifference)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(assemblySiteDifference);
                this.TiGuiExists(assemblySiteDifference);

                assemblySiteDifference.InsertTime = DateTime.Now;
                assemblySiteDifference.UpdateTime = DateTime.Now;

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, DateTime.Now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, DateTime.Now.Year, DateTime.Now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, DateTime.Now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(assemblySiteDifference);
                new BL.AssemblySiteInventoryManager().UpdateState(true, assemblySiteDifference.AssemblySiteInventoryId);    //设置对应的盘点录入单“已生成盘点差异”状态

                foreach (var item in assemblySiteDifference.Details)
                {
                    item.AssemblySiteDifferenceId = assemblySiteDifference.AssemblySiteDifferenceId;
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
        /// Update a AssemblySiteDifference.
        /// </summary>
        public void Update(Model.AssemblySiteDifference assemblySiteDifference)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(assemblySiteDifference);
                assemblySiteDifference.UpdateTime = DateTime.Now;

                accessor.Update(assemblySiteDifference);
                detailaccessor.DeleteByHeaderId(assemblySiteDifference.AssemblySiteDifferenceId);
                foreach (var item in assemblySiteDifference.Details)
                {
                    item.AssemblySiteDifferenceId = assemblySiteDifference.AssemblySiteDifferenceId;
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
            return "ASD";
        }

        protected override string GetSettingId()
        {
            return "AssemblySiteDifferenceRule";
        }

        public void TiGuiExists(Model.AssemblySiteDifference model)
        {
            if (this.ExistsPrimary(model.AssemblySiteDifferenceId))
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
                model.AssemblySiteDifferenceId = this.GetId(DateTime.Now);
                TiGuiExists(model);
            }
        }

        public void Validate(Model.AssemblySiteDifference model)
        {
            if (string.IsNullOrEmpty(model.AssemblySiteInventoryId))
                throw new Helper.InvalidValueException(Model.AssemblySiteDifference.PRO_AssemblySiteInventoryId);
        }

        public Model.AssemblySiteDifference GetDetail(string id)
        {
            Model.AssemblySiteDifference model = this.Get(id);
            if (model != null)
                model.Details = detailaccessor.SelectByHeaderId(id);

            return model;
        }
    }
}
