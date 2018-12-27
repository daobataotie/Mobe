//------------------------------------------------------------------------------
//
// file name：PCFlameRetardantManager.cs
// author: mayanjun
// create date：2018/12/27 11:27:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCFlameRetardant.
    /// </summary>
    public partial class PCFlameRetardantManager : BaseManager
    {
        private static readonly DA.IPCFlameRetardantDetailAccessor accessorDetail = (DA.IPCFlameRetardantDetailAccessor)Accessors.Get("PCFlameRetardantDetailAccessor");

        /// <summary>
        /// Delete PCFlameRetardant by primary key.
        /// </summary>
        public void Delete(string pCFlameRetardantId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();

                accessorDetail.DeleteByPrimaryId(pCFlameRetardantId);
                accessor.Delete(pCFlameRetardantId);

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();

                throw ex;
            }
        }

        /// <summary>
        /// Insert a PCFlameRetardant.
        /// </summary>
        public void Insert(Model.PCFlameRetardant pCFlameRetardant)
        {
            //
            // todo:add other logic here
            //
            try
            {
                this.Validate(pCFlameRetardant);
                this.TiGuiExists(pCFlameRetardant);

                BL.V.BeginTransaction();

                pCFlameRetardant.InsertTime = DateTime.Now;
                pCFlameRetardant.UpdateTime = DateTime.Now;

                accessor.Insert(pCFlameRetardant);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCFlameRetardant.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCFlameRetardant.InsertTime.Value.Year, pCFlameRetardant.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCFlameRetardant.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                //插入详细
                foreach (var item in pCFlameRetardant.Details)
                {
                    item.PCFlameRetardantId = pCFlameRetardant.PCFlameRetardantId;

                    accessorDetail.Insert(item);
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
        /// Update a PCFlameRetardant.
        /// </summary>
        public void Update(Model.PCFlameRetardant pCFlameRetardant)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                this.Validate(pCFlameRetardant);
                BL.V.BeginTransaction();

                pCFlameRetardant.UpdateTime = DateTime.Now;
                accessor.Update(pCFlameRetardant);

                //先删除所有详细再插入新的
                accessorDetail.DeleteByPrimaryId(pCFlameRetardant.PCFlameRetardantId);
                foreach (var item in pCFlameRetardant.Details)
                {
                    item.PCFlameRetardantId = pCFlameRetardant.PCFlameRetardantId;

                    accessorDetail.Insert(item);
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
            return "PFR";
        }

        protected override string GetSettingId()
        {
            return "PCFlameRetardant";
        }

        public void Validate(Model.PCFlameRetardant model)
        {
            if (model.InvoiceDate == null)
                throw new Helper.InvalidValueException(Model.PCFlameRetardant.PRO_InvoiceDate);
        }

        private void TiGuiExists(Model.PCFlameRetardant model)
        {
            if (this.ExistsPrimary(model.PCFlameRetardantId))
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
                model.PCFlameRetardantId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Model.PCFlameRetardant GetDetail(string PCFlameRetardantId)
        {
            Model.PCFlameRetardant model = this.Get(PCFlameRetardantId);
            if (model != null)
                model.Details = accessorDetail.SelectByPrimaryId(PCFlameRetardantId);
            return model;
        }
    }
}
