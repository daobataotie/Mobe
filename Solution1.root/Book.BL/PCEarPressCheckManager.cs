//------------------------------------------------------------------------------
//
// file name：PCEarPressCheckManager.cs
// author: mayanjun
// create date：2013-08-23 16:50:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{

    /// <summary>
    /// Business logic for dbo.PCEarPressCheck.
    /// </summary>

    public partial class PCEarPressCheckManager : BaseManager
    {
        BL.PCEarPressCheckDetailManager detailManager = new PCEarPressCheckDetailManager();

        /// <summary>
        /// Delete PCEarPressCheck by primary key.
        /// </summary>
        public void Delete(string pCEarPressCheckId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                detailManager.DeleteByPCEarPressCheckId(pCEarPressCheckId);
                accessor.Delete(pCEarPressCheckId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        public void Delete(Model.PCEarPressCheck PCEarPressCheck)
        {
            if (PCEarPressCheck != null)
            {
                Delete(PCEarPressCheck.PCEarPressCheckId);
            }
        }
        /// <summary>
        /// Insert a PCEarPressCheck.
        /// </summary>
        public void Insert(Model.PCEarPressCheck pCEarPressCheck)
        {
            //
            // todo:add other logic here
            //
            Validate(pCEarPressCheck);
            try
            {
                BL.V.BeginTransaction();
                pCEarPressCheck.InsertTime = DateTime.Now;
                pCEarPressCheck.UpdateTime = DateTime.Now;
                accessor.Insert(pCEarPressCheck);

                foreach (Model.PCEarPressCheckDetail model in pCEarPressCheck.Details)
                {
                    detailManager.Insert(model);
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
        /// Update a PCEarPressCheck.
        /// </summary>
        public void Update(Model.PCEarPressCheck pCEarPressCheck)
        {
            //
            // todo: add other logic here.
            //
            Validate(pCEarPressCheck);
            try
            {
                BL.V.BeginTransaction();
                pCEarPressCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCEarPressCheck);
                detailManager.DeleteByPCEarPressCheckId(pCEarPressCheck.PCEarPressCheckId);
                foreach (Model.PCEarPressCheckDetail model in pCEarPressCheck.Details)
                {
                    detailManager.Insert(model);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }







        }

        protected override string GetInvoiceKind()
        {
            return "PCE";
        }

        protected override string GetSettingId()
        {
            return "PCEarPressCheckRule";
        }
        public void TiGuiExists(Model.PCEarPressCheck model)
        {
            if (this.ExistsPrimary(model.PCEarPressCheckId))
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
                model.PCEarPressCheckId = this.GetId(DateTime.Now);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }
        }


        private void Validate(Model.PCEarPressCheck pCEarPressCheck)
        {
            if (pCEarPressCheck.ProductId == null)
                throw new Helper.RequireValueException(Model.PCEarPressCheck.PRO_ProductId);
            if (pCEarPressCheck.EmployeeId == null)
                throw new Helper.RequireValueException(Model.PCEarPressCheck.PRO_EmployeeId);
        }

        public Model.PCEarPressCheck Get(Model.PCEarPressCheck pCEarPressCheck)
        {
            Model.PCEarPressCheck model = this.Get(pCEarPressCheck.PCEarPressCheckId);
            if (model != null)
            {
                model.Details = new PCEarPressCheckDetailManager().SelectByPCEarPressCheckId(pCEarPressCheck.PCEarPressCheckId);
            }

            return model;
        }

        public bool mHasRows(bool IsReport)
        {
            return accessor.mHasRows(IsReport);
        }

        public bool mHasRowsBefore(Model.PCEarPressCheck e)
        {
            return accessor.mHasRowsBefore(e);
        }

        public bool mHasRowsAfter(Model.PCEarPressCheck e)
        {
            return accessor.mHasRowsAfter(e);
        }

        public Model.PCEarPressCheck mGetFirst(bool IsReport)
        {
            return accessor.mGetFirst(IsReport);
        }

        public Model.PCEarPressCheck mGetLast(bool IsReport)
        {
            return accessor.mGetLast(IsReport);
        }

        public Model.PCEarPressCheck mGetNext(Model.PCEarPressCheck e)
        {
            return accessor.mGetNext(e);
        }

        public Model.PCEarPressCheck mGetPrev(Model.PCEarPressCheck e)
        {
            return accessor.mGetPrev(e);
        }
    }
}

