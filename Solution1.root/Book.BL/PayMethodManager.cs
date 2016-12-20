//------------------------------------------------------------------------------
//
// file name：PayMethodManager.cs
// author: peidun
// create date：2008/6/30 14:20:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PayMethod.
    /// </summary>
    public partial class PayMethodManager : BaseManager
    {
		
		/// <summary>
		/// Delete PayMethod by primary key.
		/// </summary>
		public void Delete(string payMethodId)
		{
			accessor.Delete(payMethodId);
		}
        public void Delete(Model.PayMethod payMethod)
        {
            this.Delete(payMethod.PayMethodId);
        }
		/// <summary>
		/// Insert a PayMethod.
		/// </summary>
        public void Insert(Model.PayMethod payMethod)
        {
            this.Validate(payMethod);

            if (this.Exists(payMethod.Id))
            {
                throw new Helper.InvalidValueException(Model.PayMethod.PROPERTY_ID);
            }

            payMethod.PayMethodId = Guid.NewGuid().ToString();
            payMethod.InsertTime = DateTime.Now;  
            accessor.Insert(payMethod);
        }
		
		/// <summary>
		/// Update a PayMethod.
		/// </summary>
        public void Update(Model.PayMethod payMethod)
        {
            this.Validate(payMethod);
            if (this.ExistsExcept(payMethod))
            {
                throw new Helper.InvalidValueException(Model.PayMethod.PROPERTY_ID);
            }
            payMethod.UpdateTime = DateTime.Now;
            accessor.Update(payMethod);
        }
        public string GetNewId() 
        {
            return Guid.NewGuid().ToString();
        }

        public void InsertUpdate(Model.PayMethod paymethod)
        {
            if (accessor.HasRows(paymethod.PayMethodId))
                this.Update(paymethod);
            else
                this.Insert(paymethod);
        }

        public void Validate(Model.PayMethod paymethod)
        {
            if (string.IsNullOrEmpty(paymethod.Id))
                throw new Helper.RequireValueException(Model.PayMethod.PROPERTY_ID);

            if (string.IsNullOrEmpty(paymethod.PayMethodName))
                throw new Helper.RequireValueException(Model.PayMethod.PROPERTY_PAYMETHODNAME);
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "PayMethod";
        //}

        //protected override string GetSettingId()
        //{
        //    return "PayMethodRule";
        //}
    }
}

