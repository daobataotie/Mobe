//------------------------------------------------------------------------------
//
// file name：AtBankSaveUpManager.cs
// author: mayanjun
// create date：2010-11-24 09:51:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtBankSaveUp.
    /// </summary>
    public partial class AtBankSaveUpManager:BaseManager
    {
		
		/// <summary>
		/// Delete AtBankSaveUp by primary key.
		/// </summary>
		public void Delete(string saveUpId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(saveUpId);
		}

		/// <summary>
		/// Insert a AtBankSaveUp.
		/// </summary>
        public void Insert(Model.AtBankSaveUp atBankSaveUp)
        {
			//
			// todo:add other logic here
			//
            Validate(atBankSaveUp);
            atBankSaveUp.InsertTime = DateTime.Now;
            atBankSaveUp.SaveUpId = Guid.NewGuid().ToString();
            accessor.Insert(atBankSaveUp);
        }
		
		/// <summary>
		/// Update a AtBankSaveUp.
		/// </summary>
        public void Update(Model.AtBankSaveUp atBankSaveUp)
        {
			//
			// todo: add other logic here.
			//
            Validate(atBankSaveUp);
            atBankSaveUp.UpdateTime = DateTime.Now;
            accessor.Update(atBankSaveUp);
        }
        private void Validate(Model.AtBankSaveUp atBankSaveUp)
        {
            if (string.IsNullOrEmpty(atBankSaveUp.Id))
            {
                throw new Helper.RequireValueException(Model.AtBankSaveUp.PRO_Id);
            }
            if (string.IsNullOrEmpty(atBankSaveUp.BankId))
            {
                throw new Helper.RequireValueException(Model.AtBankSaveUp.PRO_BankId);
            }
            if (string.IsNullOrEmpty(atBankSaveUp.SaveUpCategory))
            {
                throw new Helper.RequireValueException(Model.AtBankSaveUp.PRO_SaveUpCategory);
            }
        }
        public IList<Book.Model.AtBankSaveUp> Select(DateTime startDate, DateTime endDate, string bankAccountId)
        {
            return accessor.Select(startDate,endDate,bankAccountId);
        }
    }
}

