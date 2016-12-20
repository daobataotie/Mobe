//------------------------------------------------------------------------------
//
// file name：AtBankAccountManager.cs
// author: mayanjun
// create date：2010-11-20 10:16:18
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtBankAccount.
    /// </summary>
    public partial class AtBankAccountManager:BaseManager
    {
		
		/// <summary>
		/// Delete AtBankAccount by primary key.
		/// </summary>
		public void Delete(string bankAccountId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(bankAccountId);
		}
        public void Delete(Model.AtBankAccount atBankAccount)
        {

            try
            {
                V.BeginTransaction();
                this.Delete(atBankAccount.BankAccountId);
                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;

            }

        }
		/// <summary>
		/// Insert a AtBankAccount.
		/// </summary>
        public void Insert(Model.AtBankAccount atBankAccount)
        {
			//
			// todo:add other logic here
			//
            Validate(atBankAccount);
            atBankAccount.BankAccountId = Guid.NewGuid().ToString();
            try
            {
                atBankAccount.InsertTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, atBankAccount.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, atBankAccount.InsertTime.Value.Year, atBankAccount.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, atBankAccount.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(atBankAccount);
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
            return "atbaRule";
        }
        protected override string GetInvoiceKind()
        {
            return "atba";
        }
		/// <summary>
		/// Update a AtBankAccount.
		/// </summary>
        public void Update(Model.AtBankAccount atBankAccount)
        {
			//
			// todo: add other logic here.
			//
            Validate(atBankAccount);
            atBankAccount.UpdateTime = DateTime.Now;
            accessor.Update(atBankAccount);
        }
        private void Validate(Model.AtBankAccount atBankAccount)
        {
            if (string.IsNullOrEmpty(atBankAccount.Id))
            {
                throw new Helper.RequireValueException(Model.AtBankAccount.PRO_Id);
            }
            if (string.IsNullOrEmpty(atBankAccount.BankAccountName))
            {
                throw new Helper.RequireValueException(Model.AtBankAccount.PRO_BankAccountName);
            }
            if (string.IsNullOrEmpty(atBankAccount.AccountCategory))
            {
                throw new Helper.RequireValueException(Model.AtBankAccount.PRO_AccountCategory);
            }
            if (string.IsNullOrEmpty(atBankAccount.BankId))
            {
                throw new Helper.RequireValueException(Model.AtBankAccount.PRO_BankId);
            }
        }
    }
}

