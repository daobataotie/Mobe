//------------------------------------------------------------------------------
//
// file name：AcOtherShouldCollectionManager.cs
// author: mayanjun
// create date：2011-6-10 11:19:27
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcOtherShouldCollection.
    /// </summary>
    public partial class AcOtherShouldCollectionManager : BaseManager
    {
        private static readonly DA.IAcOtherShouldCollectionDetailAccessor AcOtherShouldCollectionDetailAccessor = (DA.IAcOtherShouldCollectionDetailAccessor)Accessors.Get("AcOtherShouldCollectionDetailAccessor");
        /// <summary>
        /// Delete AcOtherShouldCollection by primary key.
        /// </summary>
        public void Delete(string acOtherShouldCollectionId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acOtherShouldCollectionId);
        }
        public void Delete(Model.AcOtherShouldCollection acOtherShouldCollection)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acOtherShouldCollection.AcOtherShouldCollectionId);
        }
        public Model.AcOtherShouldCollection GetDetails(string AcOtherShouldCollectionId)
        {
            Model.AcOtherShouldCollection AcOtherShouldCollection = accessor.Get(AcOtherShouldCollectionId);
            if (AcOtherShouldCollection != null)
                AcOtherShouldCollection.Details = AcOtherShouldCollectionDetailAccessor.Select(AcOtherShouldCollection);
            return AcOtherShouldCollection;
        }
        /// <summary>
        /// Insert a AcOtherShouldCollection.
        /// </summary>
        public void Insert(Model.AcOtherShouldCollection acOtherShouldCollection)
        {
            //
            // todo:add other logic here
            //
            Validate(acOtherShouldCollection);
            try
            {
                acOtherShouldCollection.InsertTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, acOtherShouldCollection.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, acOtherShouldCollection.InsertTime.Value.Year, acOtherShouldCollection.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, acOtherShouldCollection.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(acOtherShouldCollection);

                foreach (Model.AcOtherShouldCollectionDetail acOtherShouldCollectionDetail in acOtherShouldCollection.Details)
                {
                    acOtherShouldCollectionDetail.AcOtherShouldCollectionDetailId = Guid.NewGuid().ToString();
                    acOtherShouldCollectionDetail.AcOtherShouldCollectionId = acOtherShouldCollection.AcOtherShouldCollectionId;
                    AcOtherShouldCollectionDetailAccessor.Insert(acOtherShouldCollectionDetail);
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
        /// Update a AcOtherShouldCollection.
        /// </summary>
        public void Update(Model.AcOtherShouldCollection acOtherShouldCollection)
        {
            //
            // todo: add other logic here.
            //
            Validate(acOtherShouldCollection);
            if (acOtherShouldCollection != null)
            {
                this.Delete(acOtherShouldCollection);
                acOtherShouldCollection.UpdateTime = DateTime.Now;
                this.Insert(acOtherShouldCollection);
            }
        }
        protected override string GetSettingId()
        {
            return "aoscRule";
        }
        protected override string GetInvoiceKind()
        {
            return "aosc";
        }
        private void Validate(Model.AcOtherShouldCollection acOtherShouldCollection)
        {
            if (string.IsNullOrEmpty(acOtherShouldCollection.AcOtherShouldCollectionId))
            {
                throw new Helper.RequireValueException(Model.AcOtherShouldCollection.PRO_AcOtherShouldCollectionId);
            }
            if (acOtherShouldCollection.InvoiceTax + acOtherShouldCollection.InvoiceHeji != acOtherShouldCollection.HeJi)
            {
                throw new Helper.InvalidValueException(Model.AcOtherShouldCollection.PRO_InvoiceHeji);
            }
        }

        public IList<Model.AcOtherShouldCollection> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }

        public IList<Model.AcOtherShouldCollection> SelectByDateRangeAndCustomerCompany(DateTime startdate, DateTime enddate, Model.Customer customer, Model.Company company)
        {
            return accessor.SelectByDateRangeAndCustomerCompany(startdate, enddate, customer, company);
        }

        public string UpdateAcOtherShouldCollectionList(IList<Model.AcOtherShouldCollection> list)
        {
            try
            {
                BL.V.BeginTransaction();

                foreach (Model.AcOtherShouldCollection item in list)
                {
                    accessor.Update(item);
                }

                BL.V.CommitTransaction();

                return "保存成功";
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                return ex.Message;
            }
        }
    }
}

