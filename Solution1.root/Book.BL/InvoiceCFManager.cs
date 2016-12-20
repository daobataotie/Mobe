//------------------------------------------------------------------------------
//
// file name：InvoiceCFManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceCF.
    /// </summary>
    public partial class InvoiceCFManager : InvoiceManager
    {
		/// <summary>
		/// Insert a InvoiceCF.
		/// </summary>
        public void Insert(Model.InvoiceCF invoiceCF)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceCF);
        }
		
		/// <summary>
		/// Update a InvoiceCF.
		/// </summary>
        public void Update(Model.InvoiceCF invoiceCF)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceCF);
        }
        public IList<Model.InvoiceCF> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceCF> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }
        public Model.InvoiceCF Get(string invoiceId)
        {
            //Model.InvoiceCF invoice = accessor.Get(invoiceId);            
            return accessor.Get(invoiceId);
        }

        protected override string GetInvoiceKind()
        {
            return "CF";
        }

        protected override Book.DA.IInvoiceAccessor GetAccessor()
        {
            return accessor;
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {
            base._ValidateForInsert(invoice);
        }

        protected override void _ValidateForUpdate(Book.Model.Invoice invoice)
        {
            base._ValidateForUpdate(invoice);
        }

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCF))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now; ;
            invoice.UpdateTime = DateTime.Now; ;

            _Insert((Model.InvoiceCF)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCF))
                throw new ArgumentException();

            _Update((Model.InvoiceCF)invoice);
        }

        protected override void _TurnNormal(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCF))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceCF)invoice);
        }

        protected override void _TurnNull(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCF))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceCF)invoice);
        }
    }
}

