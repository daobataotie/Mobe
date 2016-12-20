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
    public partial class AtSummonManager : BaseManager
    {
        BL.AtSummonDetailManager _atSummonDetailManager = new AtSummonDetailManager();

        public void Delete(string summonId)
        {
            accessor.Delete(summonId);
        }

        public void Delete(Model.AtSummon atSummon)
        {
            try
            {
                BL.V.BeginTransaction();

                _atSummonDetailManager.DeleteByHeadId(atSummon.SummonId);

                this.Delete(atSummon.SummonId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public Model.AtSummon GetDetails(string SummonId)
        {
            Model.AtSummon AtSummon = accessor.Get(SummonId);
            if (AtSummon != null)
                AtSummon.Details = _atSummonDetailManager.Select(AtSummon);
            return AtSummon;
        }

        public void Insert(Model.AtSummon atSummon)
        {
            atSummon.SummonId = Guid.NewGuid().ToString();
            Validate(atSummon);
            try
            {
                atSummon.InsertTime = DateTime.Now;
                BL.V.BeginTransaction();
                TiGuiExists(atSummon);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, atSummon.SummonDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, atSummon.SummonDate.Value.Year, atSummon.SummonDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, atSummon.SummonDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(atSummon);

                foreach (Model.AtSummonDetail atSummonDetail in atSummon.Details)
                {
                    if (atSummonDetail.SummonDetailId == null)
                        continue;
                    if (atSummonDetail.Lending == null || atSummonDetail.SubjectId == null)
                    {
                        throw new global::Helper.MessageValueException("請輸入傳票詳細資料！！");
                    }
                    if (atSummonDetail.Subject == null)
                        continue;
                    atSummonDetail.SummonDetailId = Guid.NewGuid().ToString();
                    atSummonDetail.InsertTime = DateTime.Now;
                    atSummonDetail.SummonCatetory = atSummon.SummonCategory;
                    atSummonDetail.BillCode = atSummon.BIllCode;
                    atSummonDetail.SummonId = atSummon.SummonId;
                    _atSummonDetailManager.Insert(atSummonDetail);
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.AtSummon atSummon)
        {
            Validate(atSummon);
            try
            {
                BL.V.BeginTransaction();
                if (atSummon != null)
                {
                    _atSummonDetailManager.DeleteByHeadId(atSummon.SummonId);
                    foreach (Model.AtSummonDetail d in atSummon.Details)
                    {
                        if (d.Lending == null || d.SubjectId == null)
                            throw new global::Helper.MessageValueException("請輸入傳票詳細資料！！");
                        _atSummonDetailManager.Insert(d);
                    }

                    atSummon.UpdateTime = DateTime.Now;
                    accessor.Update(atSummon);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
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


        public IList<Model.AtSummon> SelectByDateRage(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRage(startdate, enddate);
        }
        private void TiGuiExists(Model.AtSummon model)
        {
            if (this.ExistsPrimary(model.SummonId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.SummonDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.SummonDate.Value.Year, model.SummonDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.SummonDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.SummonId = this.GetId(model.SummonDate.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }
        //public override string GetId(DateTime dateTime)
        //{
        //    string a=base.GetId(dateTime);
        //    return a.Substring(0, 6) + dateTime.Date.Day.ToString("d2") + a.Substring(6);
        //} 


        public IList<Model.AtSummon> SelectByCondition(DateTime startDate, DateTime endDate, string startId, string endId)
        {
            return accessor.SelectByCondition(startDate, endDate, startId, endId);
        }

    }
}

