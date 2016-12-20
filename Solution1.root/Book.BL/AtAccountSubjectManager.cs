//------------------------------------------------------------------------------
//
// file name：AtAccountSubjectManager.cs
// author: mayanjun
// create date：2010-11-10 11:04:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtAccountSubject.
    /// </summary>
    public partial class AtAccountSubjectManager : BaseManager
    {
        public void Delete(string subjectId)
        {
            accessor.Delete(subjectId);
        }

        public void Insert(Model.AtAccountSubject atAccountSubject)
        {
            Validate(atAccountSubject);
            atAccountSubject.SubjectId = Guid.NewGuid().ToString();
            try
            {
                atAccountSubject.InsertTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, atAccountSubject.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, atAccountSubject.InsertTime.Value.Year, atAccountSubject.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, atAccountSubject.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(atAccountSubject);
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
            return "atasRule";
        }

        protected override string GetInvoiceKind()
        {
            return "atas";
        }

        public void Update(Model.AtAccountSubject atAccountSubject)
        {
            Validate(atAccountSubject);
            atAccountSubject.UpdateTime = DateTime.Now;
            accessor.Update(atAccountSubject);
        }

        private void Validate(Model.AtAccountSubject atAccountSubject)
        {
            if (string.IsNullOrEmpty(atAccountSubject.Id))
            {
                throw new Helper.RequireValueException(Model.AtAccountSubject.PRO_Id);
            }
            if (string.IsNullOrEmpty(atAccountSubject.SubjectName))
            {
                throw new Helper.RequireValueException(Model.AtAccountSubject.PRO_SubjectName);
            }
            if (string.IsNullOrEmpty(atAccountSubject.AccountingCategoryId))
            {
                throw new Helper.RequireValueException(Model.AtAccountSubject.PRO_AccountingCategoryId);
            }
        }

        public IList<Model.AtAccountSubject> selectById(string startid, string endid)
        {
            return accessor.selectById(startid, endid);
        }

        //public void UpdateDataTable(Model.AtAccountSubject accountSubject)
        //{
        //    accessor.UpdateDataTable(accountSubject);
        //}
    }
}

