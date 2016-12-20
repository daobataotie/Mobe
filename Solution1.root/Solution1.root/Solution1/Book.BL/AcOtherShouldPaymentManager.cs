//------------------------------------------------------------------------------
//
// file name：AcOtherShouldPaymentManager.cs
// author: mayanjun
// create date：2011-6-10 10:11:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcOtherShouldPayment.
    /// </summary>
    public partial class AcOtherShouldPaymentManager:BaseManager
    {
        private static readonly DA.IAcOtherShouldPaymentDetailAccessor AcOtherShouldPaymentDetailAccessor = (DA.IAcOtherShouldPaymentDetailAccessor)Accessors.Get("AcOtherShouldPaymentDetailAccessor");
		/// <summary>
		/// Delete AcOtherShouldPayment by primary key.
		/// </summary>
		public void Delete(string acOtherShouldPaymentId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(acOtherShouldPaymentId);
		}
        public void Delete(Model.AcOtherShouldPayment AcOtherShouldPayment)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(AcOtherShouldPayment.AcOtherShouldPaymentId);
        }
        public Model.AcOtherShouldPayment GetDetails(string AcOtherShouldPaymentId)
        {
            Model.AcOtherShouldPayment AcOtherShouldPayment = accessor.Get(AcOtherShouldPaymentId);
            if (AcOtherShouldPayment != null)
                AcOtherShouldPayment.Details = AcOtherShouldPaymentDetailAccessor.Select(AcOtherShouldPayment);
            return AcOtherShouldPayment;
        }
		/// <summary>
		/// Insert a AcOtherShouldPayment.
		/// </summary>
        public void Insert(Model.AcOtherShouldPayment acOtherShouldPayment)
        {
			//
			// todo:add other logic here
			//
            Validate(acOtherShouldPayment);
            try
            {
                acOtherShouldPayment.InsertTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, acOtherShouldPayment.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, acOtherShouldPayment.InsertTime.Value.Year, acOtherShouldPayment.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, acOtherShouldPayment.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(acOtherShouldPayment);

                foreach (Model.AcOtherShouldPaymentDetail acOtherShouldPaymentDetail in acOtherShouldPayment.Details)
                {
                    acOtherShouldPaymentDetail.AcOtherShouldPaymentDetailId = Guid.NewGuid().ToString();
                    acOtherShouldPaymentDetail.AcOtherShouldPaymentId = acOtherShouldPayment.AcOtherShouldPaymentId;
                    AcOtherShouldPaymentDetailAccessor.Insert(acOtherShouldPaymentDetail);
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
		/// Update a AcOtherShouldPayment.
		/// </summary>
        public void Update(Model.AcOtherShouldPayment acOtherShouldPayment)
        {
			//
			// todo: add other logic here.
			//
            Validate(acOtherShouldPayment);
            if (acOtherShouldPayment != null)
            {
                this.Delete(acOtherShouldPayment);
                acOtherShouldPayment.UpdateTime = DateTime.Now;
                this.Insert(acOtherShouldPayment);
            }
        }
        protected override string GetSettingId()
        {
            return "aospRule";
        }
        protected override string GetInvoiceKind()
        {
            return "aosp";
        }
        private void Validate(Model.AcOtherShouldPayment acOtherShouldPayment)
        {
            if (string.IsNullOrEmpty(acOtherShouldPayment.AcOtherShouldPaymentId))
            {
                throw new Helper.RequireValueException(Model.AcOtherShouldPayment.PRO_AcOtherShouldPaymentId);
            }
        }
    }
}

