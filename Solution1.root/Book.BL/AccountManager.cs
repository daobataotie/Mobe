//------------------------------------------------------------------------------
//
// file name：AccountManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Account.
    /// </summary>
    public partial class AccountManager : BaseManager
    {
		
		/// <summary>
		/// Delete Account by primary key.
		/// </summary>
		public void Delete(string accountId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(accountId);
		}

        public void Delete(Model.Account account)
        {
            this.Delete(account.AccountId);
        }
		/// <summary>
		/// Insert a Account.
		/// </summary>
        public void Insert(Model.Account account)
        {

			//
			// todo:add other logic here
			//
           // Validate(account);
            if (accessor.Exists(account.Id))
            {
                throw new Helper.InvalidValueException(Model.Account.PROPERTY_ID);
            }
          //  account.AccountId = Guid.NewGuid().ToString();
            account.InsertTime = DateTime.Now;
            accessor.Insert(account);
        }
        //private void Validate(Model.Account account) 
        //{
        //    if (string.IsNullOrEmpty(account.Id))
        //    {
        //        throw new Helper.RequireValueException(Model.Account.PROPERTY_ID);
        //    }

        //    if (string.IsNullOrEmpty(account.AccountName)) 
        //    {
        //        throw new Helper.RequireValueException(Model.Account.PROPERTY_ACCOUNTNAME);
        //    }
        //}
		/// <summary>
		/// Update a Account.
		/// </summary>
        public void Update(IList< Model.Account>  detail)
        {
			//
			// todo: add other logic here.
			//

            foreach (Model.Account account in detail)
            { 
                if (string.IsNullOrEmpty(account.Id) || string.IsNullOrEmpty(account.AccountName))
                {
                    throw new Helper.RequireValueException(Model.Account.PROPERTY_ID);
                }
                if (accessor.ExistsExcept(account)) 
                {                        
                       throw new Helper.InvalidValueException(Model.Account.PROPERTY_ID);
                }
            
            }
            foreach (Model.Account account in detail)
            {             
                if (accessor.ExistsPrimary(account.AccountId))
                {                   
                     account.UpdateTime = DateTime.Now;
                     accessor.Update(account);
                }
                else
                {
                    //   accessor.Delete(pc.ProductCategoryId);
                    this.Insert(account);
                }
            }            
         
           
        }

        public System.Data.DataTable SelectDataTable()
        {
            return accessor.SelectDataTable();
        }

        public void UpdateDataTable(System.Data.DataTable accounts)
        {
            accessor.UpdateDataTable(accounts);
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "Account";
        //}

        //protected override string GetSettingId()
        //{
        //    return "AccountRule";
        //}

    }
}

