//------------------------------------------------------------------------------
//
// file name：AtPropertyManager.cs
// author: mayanjun
// create date：2010-11-15 10:11:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtProperty.
    /// </summary>
    public partial class AtPropertyManager:BaseManager
    {
		
		/// <summary>
		/// Delete AtProperty by primary key.
		/// </summary>
		public void Delete(string propertyId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(propertyId);
		}

		/// <summary>
		/// Insert a AtProperty.
		/// </summary>
        public void Insert(Model.AtProperty atProperty)
        {
			//
			// todo:add other logic here
			//
            Validate(atProperty);
            atProperty.PropertyId = Guid.NewGuid().ToString();
            try
            {
                atProperty.InsertTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, atProperty.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, atProperty.InsertTime.Value.Year, atProperty.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, atProperty.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(atProperty);
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
            return "atpRule";
        }
        protected override string GetInvoiceKind()
        {
            return "atp";
        }
		/// <summary>
		/// Update a AtProperty.
		/// </summary>
        public void Update(Model.AtProperty atProperty)
        {
			//
			// todo: add other logic here.
			//
            Validate(atProperty);
            atProperty.UpdateTime = DateTime.Now;
            accessor.Update(atProperty);
        }
        private void Validate(Model.AtProperty atProperty)
        {
            if (string.IsNullOrEmpty(atProperty.Id))
            {
                throw new Helper.RequireValueException(Model.AtProperty.PRO_Id);
            }
            if (string.IsNullOrEmpty(atProperty.PropertyName))
            {
                throw new Helper.RequireValueException(Model.AtProperty.PRO_PropertyName);
            }
        }
        public IList<Model.AtProperty> Select(string atProperty)
        {
            return accessor.Select(atProperty);
        }
        public IList<Book.Model.AtProperty> Select(DateTime startDate, DateTime endDate)
        {
            return accessor.Select(startDate, endDate);
        }
        public IList<Book.Model.AtProperty> SelectByPropertyId(string startPropertyId, string endPropertyId)
        {
            return accessor.SelectByPropertyId(startPropertyId, endPropertyId);
        }
    }
}

