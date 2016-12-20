//------------------------------------------------------------------------------
//
// file name：AcbeginbillReceivableManager.cs
// author: mayanjun
// create date：2011-6-9 14:42:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcbeginbillReceivable.
    /// </summary>
    public partial class AcbeginbillReceivableManager : BaseManager
    {

        private static readonly DA.IAcbeginbillReceivableDetailAccessor detailsaccessor = (DA.IAcbeginbillReceivableDetailAccessor)Accessors.Get("AcbeginbillReceivableDetailAccessor");
        /// <summary>
        /// Delete AcbeginbillReceivable by primary key.
        /// </summary>
        public void Delete(string acbeginbillReceivableId)
        {
            accessor.Delete(acbeginbillReceivableId);
        }

        public void Delete(Model.AcbeginbillReceivable abr)
        {
            try
            {
                BL.V.BeginTransaction();

                detailsaccessor.DeleteByParentId(abr.AcbeginbillReceivableId);

                this.Delete(abr.AcbeginbillReceivableId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
            }
        }

        public Model.AcbeginbillReceivable GetDetail(Model.AcbeginbillReceivable acbeginbillReceivable)
        {
            Model.AcbeginbillReceivable temp = accessor.Get(acbeginbillReceivable.AcbeginbillReceivableId);
            if (temp != null)
                temp.Details = detailsaccessor.Select(temp);
            return temp;
        }

        /// <summary>
        /// Insert a AcbeginbillReceivable.
        /// </summary>
        public void Insert(Model.AcbeginbillReceivable acbeginbillReceivable)
        {

            try
            {
                BL.V.BeginTransaction();
                acbeginbillReceivable.InsertTime = DateTime.Now;
                acbeginbillReceivable.UpdateTime = DateTime.Now;
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, acbeginbillReceivable.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, acbeginbillReceivable.InsertTime.Value.Year, acbeginbillReceivable.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, acbeginbillReceivable.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(acbeginbillReceivable);

                foreach (Model.AcbeginbillReceivableDetail detail in acbeginbillReceivable.Details)
                {
                    detail.AcbeginbillReceivableDetailId = Guid.NewGuid().ToString();
                    detail.AcbeginbillReceivableId = acbeginbillReceivable.AcbeginbillReceivableId;
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
        /// Update a AcbeginbillReceivable.
        /// </summary>
        public void Update(Model.AcbeginbillReceivable acbeginbillReceivable)
        {
            if (acbeginbillReceivable != null)
            {
                try
                {
                    BL.V.BeginTransaction();
                    //更新详细
                    detailsaccessor.DeleteByParentId(acbeginbillReceivable.AcbeginbillReceivableId);
                    foreach (Model.AcbeginbillReceivableDetail detial in acbeginbillReceivable.Details)
                    {
                        detailsaccessor.Insert(detial);
                    }
                    acbeginbillReceivable.UpdateTime = DateTime.Now;
                    accessor.Update(acbeginbillReceivable);
                    BL.V.CommitTransaction();
                }
                catch
                {
                    BL.V.RollbackTransaction();
                }
            }
        }

        public IList<Model.AcbeginbillReceivable> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }

        protected override string GetSettingId()
        {
            return "AcbeginbillReceivableRule";
        }

        protected override string GetInvoiceKind()
        {
            return "AcbeginbillReceivable";
        }
    }
}

