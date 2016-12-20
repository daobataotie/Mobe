//------------------------------------------------------------------------------
//
// file name：AcbeginAccountPayableManager.cs
// author: mayanjun
// create date：2011-6-9 14:42:08
//
//------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcbeginAccountPayable.
    /// </summary>
    public partial class AcbeginAccountPayableManager : BaseManager
    {
        private static readonly DA.IAcbeginAccountPayableDetailAccessor detailsaccessor = (DA.IAcbeginAccountPayableDetailAccessor)Accessors.Get("AcbeginAccountPayableDetailAccessor");
        /// <summary>
        /// Delete AcbeginAccountPayable by primary key.
        /// </summary>
        public void Delete(string acbeginAccountPayableId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acbeginAccountPayableId);
        }

        public void Delete(Model.AcbeginAccountPayable acbeginAccountPayable)
        {
            try
            {
                BL.V.BeginTransaction();

                detailsaccessor.DeleteByAcbeginAccountPayableId(acbeginAccountPayable.AcbeginAccountPayableId);

                this.Delete(acbeginAccountPayable.AcbeginAccountPayableId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        /// <summary>
        /// Insert a AcbeginAccountPayable.
        /// </summary>
        public void Insert(Model.AcbeginAccountPayable acbeginAccountPayable)
        {

            try
            {
                BL.V.BeginTransaction();
                acbeginAccountPayable.InsertTime = DateTime.Now;
                acbeginAccountPayable.UpdateTime = DateTime.Now;
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, acbeginAccountPayable.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, acbeginAccountPayable.InsertTime.Value.Year, acbeginAccountPayable.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, acbeginAccountPayable.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(acbeginAccountPayable);

                foreach (Model.AcbeginAccountPayableDetail detail in acbeginAccountPayable.Details)
                {
                    detail.AcbeginAccountPayableDetailId = Guid.NewGuid().ToString();
                    detail.AcbeginAccountPayableId = acbeginAccountPayable.AcbeginAccountPayableId;
                    detailsaccessor.Insert(detail);
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
        /// Update a AcbeginAccountPayable.
        /// </summary>
        public void Update(Model.AcbeginAccountPayable acbeginAccountPayable)
        {
            if (acbeginAccountPayable != null)
            {
                try
                {
                    BL.V.BeginTransaction();
                    //更新详细
                    detailsaccessor.DeleteByAcbeginAccountPayableId(acbeginAccountPayable.AcbeginAccountPayableId);
                    foreach (Model.AcbeginAccountPayableDetail detail in acbeginAccountPayable.Details)
                    {
                        detailsaccessor.Insert(detail);
                    }
                    acbeginAccountPayable.UpdateTime = DateTime.Now;
                    accessor.Update(acbeginAccountPayable);

                    BL.V.CommitTransaction();
                }
                catch
                {
                    BL.V.RollbackTransaction();
                    throw;
                }
            }
        }

        public Model.AcbeginAccountPayable GetDetails(Model.AcbeginAccountPayable acbeginAccountPayable)
        {
            Model.AcbeginAccountPayable acbeginAccount = accessor.Get(acbeginAccountPayable.AcbeginAccountPayableId);
            if (acbeginAccount != null)
            {
                acbeginAccount.Details = detailsaccessor.Select(acbeginAccount);
            }
            return acbeginAccount;
        }

        public IList<Model.AcbeginAccountPayable> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }

        protected override string GetSettingId()
        {
            return "AcbeginAccountPayableRule";
        }

        protected override string GetInvoiceKind()
        {
            return "AcbeginAccountPayable";
        }
    }
}

