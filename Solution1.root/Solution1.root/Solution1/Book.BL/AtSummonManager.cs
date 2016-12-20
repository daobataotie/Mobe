//------------------------------------------------------------------------------
//
// file name：AtSummonManager.cs
// author: mayanjun
// create date：2010-11-24 09:40:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtSummon.
    /// </summary>
    public partial class AtSummonManager:BaseManager
    {
        private static readonly DA.IAtSummonDetailAccessor AtSummonDetailAccessor = (DA.IAtSummonDetailAccessor)Accessors.Get("AtSummonDetailAccessor");
		/// <summary>
		/// Delete AtSummon by primary key.
		/// </summary>
		public void Delete(string summonId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(summonId);
		}
        public void Delete(Model.AtSummon atSummon)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(atSummon.SummonId);
        }
        public Model.AtSummon GetDetails(string SummonId)
        {
            Model.AtSummon AtSummon = accessor.Get(SummonId);
            if (AtSummon != null)
                AtSummon.Details = AtSummonDetailAccessor.Select(AtSummon);
            return AtSummon;
        }
		/// <summary>
		/// Insert a AtSummon.
		/// </summary>
        public void Insert(Model.AtSummon atSummon)
        {
			//
			// todo:add other logic here
			//
            atSummon.SummonId = Guid.NewGuid().ToString();
            Validate(atSummon);
            try
            {
                atSummon.InsertTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, atSummon.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, atSummon.InsertTime.Value.Year, atSummon.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, atSummon.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(atSummon);

                foreach (Model.AtSummonDetail atSummonDetail in atSummon.Details)
                {
                    if (atSummonDetail.Lending == null || atSummonDetail.SubjectId==null)
                    {
                        throw new global::Helper.MessageValueException("請輸入傳票詳細資料！！");
                    }
                    if (atSummonDetail.Subject == null)
                       return;
                    atSummonDetail.SummonDetailId = Guid.NewGuid().ToString();
                    atSummonDetail.InsertTime = DateTime.Now;
                    atSummonDetail.SummonCatetory = atSummon.SummonCategory;
                    atSummonDetail.BillCode = atSummon.BIllCode;
                    atSummonDetail.SummonId = atSummon.SummonId;
                    AtSummonDetailAccessor.Insert(atSummonDetail);
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
		/// Update a AtSummon.
		/// </summary>
        public void Update(Model.AtSummon atSummon)
        {
			//
			// todo: add other logic here.
			//
            Validate(atSummon);
            if (atSummon != null)
            {
                this.Delete(atSummon);
                atSummon.UpdateTime = DateTime.Now;
                this.Insert(atSummon);
            }
        }
        protected override string GetSettingId()
        {
            return "atsRule";
        }
        protected override string GetInvoiceKind()
        {
            return "ats";
        }
        private void Validate(Model.AtSummon atSummon)
        {
            if (string.IsNullOrEmpty(atSummon.Id))
            {
                throw new Helper.RequireValueException(Model.AtSummon.PRO_Id);
            }
            if (string.IsNullOrEmpty(atSummon.SummonCategory))
            {
                throw new Helper.RequireValueException(Model.AtSummon.PRO_SummonCategory);
            }
        }
    }
}

