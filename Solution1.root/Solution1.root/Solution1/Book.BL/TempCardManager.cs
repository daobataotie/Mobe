//------------------------------------------------------------------------------
//
// file name：TempCardManager.cs
// author: peidun
// create date：2010-2-6 10:33:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.TempCard.
    /// </summary>
    public partial class TempCardManager
    {
		
		/// <summary>
		/// Delete TempCard by primary key.
		/// </summary>
		public void Delete(string tempCardId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(tempCardId);
		}

		/// <summary>
		/// Insert a TempCard.
		/// </summary>
        public void Insert(Model.TempCard tempCard)
        {
			//
			// todo:add other logic here
			//
            Validate(tempCard);
            tempCard.InsertTime = DateTime.Now;
            accessor.Insert(tempCard);
        }
		
		/// <summary>
		/// Update a TempCard.
		/// </summary>
        public void Update(Model.TempCard tempCard)
        {
			//
			// todo: add other logic here.
			//
            Validate(tempCard);
            tempCard.UpdateTime = DateTime.Now;
            accessor.Update(tempCard);
        }

        public IList<Model.TempCard> SelectbyCardnoDate(DateTime clockdate, string cadno)
        {
           return  accessor.SelectbyCardnoDate(clockdate, cadno);
        }


        public IList<Model.TempCard> Selectbyemployee(string empid)
        {
            return accessor.Selectbyemployee(empid);
        }

        public IList<Model.TempCard> Selectbydate(DateTime startdate, DateTime enddate)
        {
            return accessor.Selectbydate(startdate, enddate);
        }

        public IList<Model.TempCard> Selectbyemployeedate(string empid, DateTime startdate, DateTime enddate)
        {
            return accessor.Selectbyemployeedate(empid,startdate,enddate);
        }
        public void Validate(Model.TempCard tempcard)
        {
            if (string.IsNullOrEmpty(tempcard.CardNo))
            {
                throw new Helper.RequireValueException(Model.TempCard.PROPERTY_CARDNO);
            }
            if (string.IsNullOrEmpty(tempcard.EmployeeId))
            {
                throw new Helper.RequireValueException(Model.TempCard.PROPERTY_EMPLOYEEID);
            }
        }


        public IList<Model.TempCard> SelectbyDateTop()
        {

            return accessor.SelectbyDateTop();
        }
        public IList<Book.Model.TempCard> SelectByCardType(string cardNo, string employeeId, DateTime startDate, DateTime endDate)
        {
            return accessor.SelectByCardType(cardNo,employeeId,startDate,endDate);
        }
    }
}

