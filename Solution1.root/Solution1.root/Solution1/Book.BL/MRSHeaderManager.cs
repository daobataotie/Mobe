//------------------------------------------------------------------------------
//
// file name：MRSHeaderManager.cs
// author: peidun
// create date：2009-12-18 11:12:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.MRSHeader.
    /// </summary>
    public partial class MRSHeaderManager : BaseManager
    {
        private static readonly DA.IMRSdetailsAccessor MRSdetailsAccessor = (DA.IMRSdetailsAccessor)Accessors.Get("MRSdetailsAccessor");
        /// <summary>
        /// Delete MRSHeader by primary key.
        /// </summary>
        public void Delete(string mRSHeaderID)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(mRSHeaderID);
        }
        public void Delete(Model.MRSHeader mRSHeader)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(mRSHeader.MRSHeaderId);
        }

        public Model.MRSHeader GetDetails(string mRSheaderId)
        {
            Model.MRSHeader mRSheader = accessor.Get(mRSheaderId);
            if (mRSheader != null)
                mRSheader.Details = MRSdetailsAccessor.Select(mRSheader);
            return mRSheader;
        }
        /// <summary>
        /// Insert a MRSHeader.
        /// </summary>
        public void Insert(Model.MRSHeader mRSHeader)
        {
            //
            // todo:add other logic here
            //
            Validate(mRSHeader);
            if (this.Exists(mRSHeader.Id))
            {
                throw new Helper.InvalidValueException(Model.MRSHeader.PRO_Id);
            }

            try
            {
                mRSHeader.InsertTime = DateTime.Now;
                mRSHeader.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, mRSHeader.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, mRSHeader.InsertTime.Value.Year, mRSHeader.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, mRSHeader.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);


                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                if (mRSHeader.Employee0 != null)
                    mRSHeader.Employee0Id = mRSHeader.Employee0.EmployeeId;
                if (mRSHeader.Employee1 != null)
                    mRSHeader.Employee1Id = mRSHeader.Employee1.EmployeeId;
                accessor.Insert(mRSHeader);

                if (mRSHeader.Details != null)
                {
                    foreach (Model.MRSdetails mRSdetails in mRSHeader.Details)
                    {
                        if (mRSdetails.Product == null || string.IsNullOrEmpty(mRSdetails.Product.ProductId))
                            continue;
                        // mRSdetails.MRSHeaderId = mRSHeader.MRSHeaderId;
                        MRSdetailsAccessor.Insert(mRSdetails);
                    }
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
        /// Update a MRSHeader.
        /// </summary>
        public void Update(Model.MRSHeader mRSHeader)
        {
            //
            // todo: add other logic here.
            Validate(mRSHeader);
            if (this.ExistsExcept(mRSHeader))
            {
                throw new Helper.InvalidValueException(Model.MRSHeader.PRO_Id);
            }
            // mPSheader.UpdateTime = DateTime.Now;
            // accessor.Update(mPSheader);
            if (mRSHeader != null)
            {
                this.Delete(mRSHeader);
                mRSHeader.UpdateTime = DateTime.Now;
                this.Insert(mRSHeader);
            }
        }

        public void UpdateHeader(Model.MRSHeader mrsHeader)
        {
            accessor.Update(mrsHeader);
        }

        private void Validate(Model.MRSHeader mRSHeader)
        {
            if (mRSHeader.MRSstartdate == new DateTime() || mRSHeader.MRSstartdate == global::Helper.DateTimeParse.NullDate || mRSHeader.MRSstartdate == null)
                throw new Helper.RequireValueException(Model.MRSHeader.PRO_MRSstartdate);

            bool IsNullOrZero = false;
            if (mRSHeader.Details != null)
            {
                foreach (Model.MRSdetails item in mRSHeader.Details)
                {
                    if (item.MRSdetailssum != null)
                        IsNullOrZero = true;
                }
                if (IsNullOrZero == false)
                    throw new Helper.MessageValueException("數量不能為空！");
            }
          
        }
        protected override string GetSettingId()
        {
            return "mrpRule";
        }
        protected override string GetInvoiceKind()
        {
            return "mrp";
        }

        public IList<Model.MRSHeader> SelectbySourceType(string type)
        {
            return accessor.SelectbySourceType(type);
        }

    }
}

