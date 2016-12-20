//------------------------------------------------------------------------------
//
// file name：ProduceStatisticsManager.cs
// author: mayanjun
// create date：2011-4-8 09:17:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceStatistics.
    /// </summary>
    public partial class ProduceStatisticsManager:BaseManager
    {
        private static readonly DA.IProduceStatisticsDetailAccessor ProduceStatisticsDetailAccessor = (DA.IProduceStatisticsDetailAccessor)Accessors.Get("ProduceStatisticsDetailAccessor");
		/// <summary>
		/// Delete ProduceStatistics by primary key.
		/// </summary>
		public void Delete(string produceStatisticsId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceStatisticsId);
		}
        public Model.ProduceStatistics GetDetails(string produceStatisticsId)
        {
            Model.ProduceStatistics produceStatistics = accessor.Get(produceStatisticsId);
            produceStatistics.Details = ProduceStatisticsDetailAccessor.Select(produceStatistics);
            return produceStatistics;
        }
		/// <summary>
		/// Insert a ProduceStatistics.
		/// </summary>
        public void Insert(Model.ProduceStatistics produceStatistics)
        {
			//
			// todo:add other logic here
			//
            Validate(produceStatistics);
            try
            {
                produceStatistics.InsertTime = DateTime.Now;

                produceStatistics.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceStatistics.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceStatistics.InsertTime.Value.Year, produceStatistics.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceStatistics.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(produceStatistics);
                foreach (Model.ProduceStatisticsDetail produceStatisticsDetail in produceStatistics.Details)
                {

                    produceStatisticsDetail.ProduceStatisticsId = produceStatistics.ProduceStatisticsId;
                    ProduceStatisticsDetailAccessor.Insert(produceStatisticsDetail);
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
		/// Update a ProduceStatistics.
		/// </summary>-
        public void Update(Model.ProduceStatistics produceStatistics)
        {
			//
			// todo: add other logic here.
			//
            Validate(produceStatistics);
            if (produceStatistics != null)
            {
                try
                {
                    this.Delete(produceStatistics);
                    produceStatistics.UpdateTime = DateTime.Now;
                    this.Insert(produceStatistics);
                }
                catch
                {
                    accessor.Update(produceStatistics);  
                }
            }
        }
        protected override string GetSettingId()
        {
            return "psmRule";
        }
        protected override string GetInvoiceKind()
        {
            return "psm";
        }

        public void Delete(Model.ProduceStatistics produceStatistics)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceStatistics.ProduceStatisticsId);
        }
        private void Validate(Model.ProduceStatistics produceStatistics)
        {
            if (string.IsNullOrEmpty(produceStatistics.ProduceStatisticsId))
            {
                throw new Helper.RequireValueException(Model.ProduceStatistics.PRO_ProduceStatisticsId);
            }
        }
        public IList<Model.ProduceStatistics> SelectBycondition(DateTime starDate, DateTime endDate, string produceStatisticsId1, string produceStatisticsId2, string PronoteHeaderId0, string PronoteHeaderId1)
        {
            return accessor.SelectBycondition(starDate, endDate, produceStatisticsId1, produceStatisticsId2, PronoteHeaderId0, PronoteHeaderId1);
        }
    }
}

