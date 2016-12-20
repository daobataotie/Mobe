//------------------------------------------------------------------------------
//
// file name：AtParameterSetManager.cs
// author: mayanjun
// create date：2012-3-26 14:33:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtParameterSet.
    /// </summary>
    public partial class AtParameterSetManager : BaseManager
    {
        public void Delete(string atParameterSet)
        {
            try
            {
                BL.V.BeginTransaction();

                accessor.Delete(atParameterSet);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        public void Insert(Model.AtParameterSet atParameterSet)
        {
            Validate(atParameterSet);
            try
            {
                BL.V.BeginTransaction();

                TiGuiExists(atParameterSet);
                atParameterSet.InsertTime = DateTime.Now;
                atParameterSet.UpdateTime = DateTime.Now;

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, atParameterSet.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, atParameterSet.InsertTime.Value.Year, atParameterSet.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, atParameterSet.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                if (atParameterSet.IsThisYear.Value)
                {
                    this.UpdateIsThisYear(atParameterSet.AtParameterSetId);
                }
                accessor.Insert(atParameterSet);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        public void Update(Model.AtParameterSet atParameterSet)
        {
            Validate(atParameterSet);
            if (atParameterSet != null)
            {
                if (atParameterSet.IsThisYear.Value)
                {
                    this.UpdateIsThisYear(atParameterSet.AtParameterSetId);
                }
                atParameterSet.UpdateTime = DateTime.Now;
                accessor.Update(atParameterSet);
            }
        }

        public Model.AtParameterSet SelectByAtCurrentlyYear(int myear)
        {
            return accessor.SelectByAtCurrentlyYear(myear);
        }

        protected override string GetSettingId()
        {
            return "atParaRule";
        }

        protected override string GetInvoiceKind()
        {
            return "atPara";
        }

        private void Validate(Model.AtParameterSet atParameterSet)
        {
            if (string.IsNullOrEmpty(atParameterSet.AtParameterSetId))
                throw new Helper.RequireValueException(Model.AtParameterSet.PRO_AtParameterSetId);
            if (string.IsNullOrEmpty(atParameterSet.ACMoneySubjectId))
                throw new Helper.RequireValueException(Model.AtParameterSet.PRO_ACMoneySubjectId);
            if (!atParameterSet.AtBeginDate.HasValue)
                throw new Helper.RequireValueException(Model.AtParameterSet.PRO_AtBeginDate);
            if (!atParameterSet.AtCurrentlyYear.HasValue)
                throw new Helper.RequireValueException(Model.AtParameterSet.PRO_AtCurrentlyYear);
            if (!atParameterSet.AtEndDate.HasValue)
                throw new Helper.RequireValueException(Model.AtParameterSet.PRO_AtEndDate);
            if (string.IsNullOrEmpty(atParameterSet.AtOldSunYiSubjectId))
                throw new Helper.RequireValueException(Model.AtParameterSet.PRO_AtOldSunYiSubjectId);
            if (string.IsNullOrEmpty(atParameterSet.AtSunYiSubjectId))
                throw new Helper.RequireValueException(Model.AtParameterSet.PRO_AtSunYiSubjectId);
            IList<Model.AtParameterSet> atplist = this.Select();
            foreach (Model.AtParameterSet d in atplist)
            {
                if (atParameterSet.AtCurrentlyYear.Value == d.AtCurrentlyYear.Value && atParameterSet.AtParameterSetId != d.AtParameterSetId)
                {
                    throw new Helper.InvalidValueException(Model.AtParameterSet.PRO_AtCurrentlyYear + "_Exists");
                }
            }
        }

        private void TiGuiExists(Model.AtParameterSet model)
        {
            if (this.ExistsPrimary(model.AtParameterSetId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.InsertTime.Value.Year, model.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.AtParameterSetId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        //修改是否为当前年度,删除其他
        private void UpdateIsThisYear(string notId)
        {
            accessor.UpdateIsThisYear(notId);
        }
    }
}

