//------------------------------------------------------------------------------
//
// file name：CurrencyCategoryManager.cs
// author: peidun
// create date：2009-09-09 下午 04:08:30
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CurrencyCategory.
    /// </summary>
    public partial class CurrencyCategoryManager
    {

        /// <summary>
        /// Delete CurrencyCategory by primary key.
        /// </summary>
        public void Delete(string pkId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pkId);
        }

        /// <summary>
        /// Insert a CurrencyCategory.
        /// </summary>
        public void Insert(Model.CurrencyCategory currencyCategory)
        {
            //
            // todo:add other logic here
            //
            Validate(currencyCategory);
            currencyCategory.CurrencyCategoryId = Guid.NewGuid().ToString();
            accessor.Insert(currencyCategory);
        }

        /// <summary>
        /// Update a CurrencyCategory.
        /// </summary>
        public void Update(Model.CurrencyCategory currencyCategory)
        {
            //
            // todo: add other logic here.
            //
            Validate(currencyCategory);
            accessor.Update(currencyCategory);
        }
        private void Validate(Model.CurrencyCategory currencyCategory)
        {
            if (string.IsNullOrEmpty(currencyCategory.AtCurrencyCategoryId))
            {
                throw new Helper.RequireValueException(Model.CurrencyCategory.PRO_AtCurrencyCategoryId);
            }
        }
        public IList<Model.CurrencyCategory> SelectByEffectDate()
        {
            return accessor.SelectByEffectDate();
        }
    }
}

