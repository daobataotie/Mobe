//------------------------------------------------------------------------------
//
// file name：ProduceStatisticsCheckManager.cs
// author: mayanjun
// create date：2011-07-22 10:44:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceStatisticsCheck.
    /// </summary>
    public partial class ProduceStatisticsCheckManager : BaseManager
    {
        private static readonly DA.IProduceStatisticsCheckDetailAccessor ProduceStatisticsCheckDetailAccessor = (DA.IProduceStatisticsCheckDetailAccessor)Accessors.Get("ProduceStatisticsCheckDetailAccessor");
		/// <summary>
		/// Delete ProduceStatisticsCheck by primary key.
		/// </summary>
		public void Delete(string produceStatisticsCheckId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceStatisticsCheckId);
		}
        public Model.ProduceStatisticsCheck GetDetails(string produceStatisticsCheckId)
        {
            Model.ProduceStatisticsCheck produceStatisticsCheck = accessor.Get(produceStatisticsCheckId);
            produceStatisticsCheck.Details = ProduceStatisticsCheckDetailAccessor.Select(produceStatisticsCheck);
            return produceStatisticsCheck;
        }
		/// <summary>
		/// Insert a ProduceStatisticsCheck.
		/// </summary>
        public void Insert(Model.ProduceStatisticsCheck produceStatisticsCheck)
        {
			//
			// todo:add other logic here
			//
            Validate(produceStatisticsCheck);
            try
            {
                produceStatisticsCheck.InsertTime = DateTime.Now;

                produceStatisticsCheck.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceStatisticsCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceStatisticsCheck.InsertTime.Value.Year, produceStatisticsCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceStatisticsCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(produceStatisticsCheck);
                foreach (Model.ProduceStatisticsCheckDetail produceStatisticsCheckDetail in produceStatisticsCheck.Details)
                {

                    produceStatisticsCheckDetail.ProduceStatisticsCheckId = produceStatisticsCheck.ProduceStatisticsCheckId;
                    ProduceStatisticsCheckDetailAccessor.Insert(produceStatisticsCheckDetail);
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
		/// Update a ProduceStatisticsCheck.
		/// </summary>
        public void Update(Model.ProduceStatisticsCheck produceStatisticsCheck)
        {
			//
			// todo: add other logic here.
			//
            Validate(produceStatisticsCheck);
            if (produceStatisticsCheck != null)
            {
                try
                {
                    this.Delete(produceStatisticsCheck);
                    produceStatisticsCheck.UpdateTime = DateTime.Now;
                    this.Insert(produceStatisticsCheck);
                }
                catch
                {
                   // accessor.Update(produceStatisticsCheck);
                }
            }
        }
        protected override string GetSettingId()
        {
            return "pscRule";
        }
        protected override string GetInvoiceKind()
        {
            return "psc";
        }
        public void Delete(Model.ProduceStatisticsCheck produceStatisticsCheck)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceStatisticsCheck.ProduceStatisticsCheckId);
        }
        private void Validate(Model.ProduceStatisticsCheck produceStatisticsCheck)
        {
            if (string.IsNullOrEmpty(produceStatisticsCheck.ProduceStatisticsCheckId))
            {
                throw new Helper.RequireValueException(Model.ProduceStatisticsCheck.PRO_ProduceStatisticsCheckId);
            }
        }
        public IList<Model.ProduceStatisticsCheck> SelectBycondition(DateTime starDate, DateTime endDate, string produceStatisticsCheckId1, string produceStatisticsCheckId2, string PronoteHeaderId0, string PronoteHeaderId1)
        {
            return accessor.SelectBycondition(starDate,endDate,produceStatisticsCheckId1,produceStatisticsCheckId2,PronoteHeaderId0,PronoteHeaderId1);
        }
    }
}

