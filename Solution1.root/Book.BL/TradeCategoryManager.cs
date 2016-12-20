//------------------------------------------------------------------------------
//
// file name：TradeCategoryManager.cs
// author: peidun
// create date：2009-08-03 9:37:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.TradeCategory.
    /// </summary>
    public partial class TradeCategoryManager : BaseManager
    {
		
		/// <summary>
		/// Delete TradeCategory by primary key.
		/// </summary>
		public void Delete(string tradeCategoryId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(tradeCategoryId);
		}

		/// <summary>
		/// Insert a TradeCategory.
		/// </summary>
        public void Insert(Model.TradeCategory tradeCategory)
        {
			//
			// todo:add other logic here
			//
            Validate(tradeCategory);

            if (this.Exists(tradeCategory.Id))
            {
                throw new Helper.InvalidValueException(Model.TradeCategory.PROPERTY_ID);
            }
            tradeCategory.TradeCategoryId = Guid.NewGuid().ToString();
            tradeCategory.InsertTime = DateTime.Now;
            accessor.Insert(tradeCategory);
        }
		
		/// <summary>
		/// Update a TradeCategory.
		/// </summary>
        public void Update(Model.TradeCategory tradeCategory)
        {
			//
			// todo: add other logic here.
			//
            Validate(tradeCategory);

            if (this.ExistsExcept(tradeCategory))
            {
                throw new Helper.InvalidValueException(Model.TradeCategory.PROPERTY_ID);
            }
            if (accessor.ExistsExcept(tradeCategory))
            {
                throw new Helper.InvalidValueException("Id");
            }
            tradeCategory.UpdateTime = DateTime.Now;
            accessor.Update(tradeCategory);
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "TradeCategory";
        //}

        //protected override string GetSettingId()
        //{
        //    return "TradeCategoryRule";
        //}

        public void Validate(Model.TradeCategory tradeCategory)
        {
            if (string.IsNullOrEmpty(tradeCategory.Id))
                throw new Helper.RequireValueException(Model.TradeCategory.PROPERTY_ID);

            if (string.IsNullOrEmpty(tradeCategory.TradeCategoryName))
                throw new Helper.RequireValueException(Model.TradeCategory.PROPERTY_TRADECATEGORYNAME);

        }
    }
}

