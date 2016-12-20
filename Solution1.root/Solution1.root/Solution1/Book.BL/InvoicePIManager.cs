//------------------------------------------------------------------------------
//
// file name：InvoicePIManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoicePI.
    /// </summary>
    public partial class InvoicePIManager : InvoiceManager
    {
        private readonly static DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IInvoicePIDetailAccessor invoicePIDetailAccessor = (DA.IInvoicePIDetailAccessor)Accessors.Get("InvoicePIDetailAccessor");
        private static readonly DA.IInvoicePODetailAccessor invoicePODetailAccessor = (DA.IInvoicePODetailAccessor)Accessors.Get("InvoicePODetailAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");

        public Model.InvoicePI Get(string invoiceId)
        {
            Model.InvoicePI invoice = accessor.Get(invoiceId);
            invoice.Details = invoicePIDetailAccessor.Select(invoice);
            return invoice;
        }

        protected override string GetInvoiceKind()
        {
            return "PI";
        }

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePI))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            _Insert((Model.InvoicePI)invoice);
        }

        private void _Insert(Book.Model.InvoicePI invoice)
        {
            //往来单位
            if (invoice.Department != null)
            {
                invoice.DepartmentId = invoice.Department.DepartmentId;
            }
            //经手人            
            if (invoice.Employee0 != null)
            {
                invoice.Employee0Id = invoice.Employee0.EmployeeId;
            }
            //录单人
            if (invoice.Employee1 != null)
            {
                invoice.Employee1Id = invoice.Employee1.EmployeeId;
            }
            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                //过账人
                if (invoice.Employee2 != null)
                {
                    invoice.Employee2Id = invoice.Employee2.EmployeeId;
                }
                //过账时间
                invoice.InvoiceGZTime = DateTime.Now;
            }
            //插入表单
            accessor.Insert(invoice);

            //插入明细
            foreach (Model.InvoicePIDetail detail in invoice.Details)
            {
                Model.InvoicePODetail odetail = invoicePODetailAccessor.Get(detail.InvoicePODetailId);

                if (detail.InvoicePIDetailQuantity > 0)
                {
                    if (detail.InvoicePIDetailQuantity <= odetail.InvoicePODetailWHQuantity)
                    {
                        detail.InvoiceId = invoice.InvoiceId;
                        invoicePIDetailAccessor.Insert(detail);

                        Model.Product p = detail.Product;
                        p.StocksQuantity += detail.InvoicePIDetailQuantity;
                        productAccessor.Update(detail.Product);

                        if (odetail != null)
                        {
                            odetail.InvoicePODetailWHQuantity -= detail.InvoicePIDetailQuantity;
                            odetail.InvoicePODetailYHQuantity += detail.InvoicePIDetailQuantity;
                            invoicePODetailAccessor.Update(odetail);
                        }

                        Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(p.ProductId, detail.PoDepotPositionId);
                        if (stock == null)
                        {
                            stock = new Book.Model.Stock();
                            stock.StockId = Guid.NewGuid().ToString();
                            stock.ProductId = p.ProductId;
                            stock.DepotPositionId = detail.PoDepotPositionId;
                            stock.StockQuantity1 = detail.InvoicePIDetailQuantity;
                            stock.StockCurrentJR = stock.StockCurrentJR == null ? detail.InvoicePIDetailQuantity : stock.StockCurrentJR + detail.InvoicePIDetailQuantity;
                            stock.DepotId = invoice.PoDepotId;
                            stock.ProduceUnit = p.ProduceUnit.CnName;
                            stockAccessor.Insert(stock);
                        }
                        else
                            stockAccessor.IncrementJR(new DepotPositionManager().Get(detail.PoDepotPositionId), p, detail.InvoicePIDetailQuantity.Value);

                    }
                    else
                    {
                        throw new Helper.InvalidValueException("HaiRuTaiDuo");
                    }
                }
            }
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePI))
                throw new ArgumentException();
            invoice.UpdateTime = DateTime.Now;
            _Update((Model.InvoicePI)invoice);
        }

        private void _Update(Model.InvoicePI invoice)
        {
            invoice.UpdateTime = DateTime.Now; ;
            invoice.DepartmentId = invoice.Department.DepartmentId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            Model.InvoicePI invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            accessor.Update(invoice);

                            invoicePIDetailAccessor.Delete(invoice);

                            foreach (Model.InvoicePIDetail detail in invoice.Details)
                            {
                                detail.InvoiceId = invoice.InvoiceId;
                                invoicePIDetailAccessor.Insert(detail);
                            }
                            break;
                        case Helper.InvoiceStatus.Normal:
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:
                            throw new InvalidOperationException();
                    }
                    break;
                case Helper.InvoiceStatus.Normal:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            throw new InvalidOperationException();
                        case Helper.InvoiceStatus.Normal:
                            invoiceOriginal.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
                            _TurnNull(invoiceOriginal);
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:
                            foreach (Model.InvoicePIDetail detail in invoice.Details)
                            {

                                Model.Product p = detail.Product;
                                productAccessor.Update(detail.Product);

                                stockAccessor.DecrementJR(new DepotPositionManager().Get(detail.PoDepotPositionId), p, detail.InvoicePIDetailQuantity.Value);

                                Model.InvoicePODetail temp = invoicePODetailAccessor.Get(detail.InvoicePODetailId);
                                if (temp != null)
                                {
                                    detail.InvoicePODetail.InvoicePODetailWHQuantity += detail.InvoicePIDetailQuantity;
                                    detail.InvoicePODetail.InvoicePODetailYHQuantity -= detail.InvoicePIDetailQuantity;
                                    invoicePODetailAccessor.Update(detail.InvoicePODetail);
                                }
                                
                            }
                            break;
                    }
                    break;
                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }

        protected override void _TurnNormal(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePI))
                throw new ArgumentException();

            _TurnNormal((Model.InvoicePI)invoice);
        }
        private void _TurnNormal(Model.InvoicePI invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }
        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePI))
                throw new ArgumentException();

            _TurnNull((Model.InvoicePI)invoice);
        }


        private void _TurnNull(Model.InvoicePI invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
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

        public IList<Model.InvoicePI> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoicePI> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

    }
}

