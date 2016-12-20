//------------------------------------------------------------------------------
//
// file name：AtBillsIncomeManager.cs
// author: mayanjun
// create date：2010-11-22 14:21:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtBillsIncome.
    /// </summary>
    public partial class AtBillsIncomeManager
    {
		
		/// <summary>
		/// Delete AtBillsIncome by primary key.
		/// </summary>
		public void Delete(string billsId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(billsId);
		}

		/// <summary>
		/// Insert a AtBillsIncome.
		/// </summary>
        public void Insert(Model.AtBillsIncome atBillsIncome)
        {
			//
			// todo:add other logic here
			//
            Validate(atBillsIncome);
            atBillsIncome.InsertTime = DateTime.Now;
            atBillsIncome.BillsId = Guid.NewGuid().ToString();
            accessor.Insert(atBillsIncome);
        }
		
		/// <summary>
		/// Update a AtBillsIncome.
		/// </summary>
        public void Update(Model.AtBillsIncome atBillsIncome)
        {
			//
			// todo: add other logic here.
			//
            Validate(atBillsIncome);
            atBillsIncome.UpdateTime = DateTime.Now;
            accessor.Update(atBillsIncome);
        }
        private void Validate(Model.AtBillsIncome atBillsIncome)
        {
            if (string.IsNullOrEmpty(atBillsIncome.Id))
            {
                throw new Helper.RequireValueException(Model.AtBillsIncome.PRO_Id);
            }
        }
        public IList<Book.Model.AtBillsIncome> SelectAtBillsIncomeByBillsOften(string billsOften, string collectionAccount)
        {
            return accessor.SelectAtBillsIncomeByBillsOften(billsOften, collectionAccount);
        }
        public IList<Book.Model.AtBillsIncome> Select(DateTime startDate, DateTime endDate)
        {
            return accessor.Select(startDate,endDate);
        }
        public IList<Book.Model.AtBillsIncome> Select(DateTime startDate, DateTime endDate, string startCollectionAccount, string endCollectionAccount)
        {
            return accessor.Select(startDate,endDate,startCollectionAccount,endCollectionAccount);
        }
        public IList<Book.Model.AtBillsIncome> SelectOferAndDate(DateTime startDate, DateTime endDate, string billsOften)
        {
            return accessor.SelectOferAndDate(startDate, endDate, billsOften);
        }
    }
}

