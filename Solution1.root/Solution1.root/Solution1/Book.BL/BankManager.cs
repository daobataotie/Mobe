//------------------------------------------------------------------------------
//
// file name：BankManager.cs
// author: peidun
// create date：2009-09-02 上午 10:38:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Bank.
    /// </summary>
    public partial class BankManager : BaseManager
    {
		
		/// <summary>
		/// Delete Bank by primary key.
		/// </summary>
		public void Delete(string bankId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(bankId);
		}

		/// <summary>
		/// Insert a Bank.
		/// </summary>
        public void Insert(Model.Bank bank)
        {
			//
			// todo:add other logic here
			//
            Validate(bank);
            bank.InsertTime = DateTime.Now;
            bank.BankId = Guid.NewGuid().ToString();
            accessor.Insert(bank);
        }
		
		/// <summary>
		/// Update a Bank.
		/// </summary>
        public void Update(Model.Bank bank)
        {
			//
			// todo: add other logic here.
			//
            Validate(bank); 
            bank.UpdateTime = DateTime.Now;
            accessor.Update(bank);
        }
        private void Validate(Model.Bank bank)
        {
            if (string.IsNullOrEmpty(bank.BankName))
            {
                throw new Helper.RequireValueException(Model.Bank.PROPERTY_BANKNAME);
            }
            if (accessor.IsEixstsBankName(bank.BankId, bank.BankName))
            {
                throw new Helper.InvalidValueException(Model.Bank.PROPERTY_BANKNAME);
            }
        }
        public DataSet SelectNoModel()
        {
            return accessor.SelectNoModel();
        }
        public void UpdateDataTable(DataTable accounts)
        {
            accessor.UpdateDataTable(accounts);
        }
    }
}

