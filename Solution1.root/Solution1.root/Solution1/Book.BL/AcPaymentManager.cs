//------------------------------------------------------------------------------
//
// file name：AcPaymentManager.cs
// author: mayanjun
// create date：2011-6-23 09:29:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcPayment.
    /// </summary>
    public partial class AcPaymentManager : BaseManager
    {
        private static readonly DA.IAcPaymentDetailAccessor acPaymentDetailaccessor = (DA.IAcPaymentDetailAccessor)Accessors.Get("AcPaymentDetail");
        /// <summary>
        /// Delete AcPayment by primary key.
        /// </summary>
        public void Delete(string acPaymentId)
        {
            accessor.Delete(acPaymentId);
        }

        /// <summary>
        /// Insert a AcPayment.
        /// </summary>
        public void Insert(Model.AcPayment acPayment)
        {
            ValidateForInsert(acPayment);
            acPayment.InsertTime = System.DateTime.Now;
            acPayment.UpdateTime = System.DateTime.Now;
            accessor.Insert(acPayment);
            foreach (Model.AcPaymentDetail item in acPayment.Detail)
            {
                acPaymentDetailaccessor.Insert(item);
            }
        }

        private void ValidateForInsert(Model.AcPayment acPayment)
        {

        }



        /// <summary>
        /// Update a AcPayment.
        /// </summary>
        public void Update(Model.AcPayment acPayment)
        {
            this.Delete(acPayment.AcPaymentId);

            this.Insert(acPayment);
        }

        public Model.AcPayment GetDetails(Model.AcPayment acPayment) 
        {
            Model.AcPayment temp = accessor.Get(acPayment.AcPaymentId);
            if (temp != null)
                temp.Detail = acPaymentDetailaccessor.Select(temp);
            return temp;
        }

        protected override string GetInvoiceKind()
        {
            return "AcPayment";
        }

        protected override string GetSettingId()
        {
            return "AcPaymentRule";
        }
    }
}

