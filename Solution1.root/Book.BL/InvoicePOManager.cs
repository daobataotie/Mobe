//------------------------------------------------------------------------------
//
// file name：InvoicePOManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoicePO.
    /// </summary>
    public partial class InvoicePOManager : InvoiceManager
    {
        private readonly static DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IInvoicePODetailAccessor invoicePODetailAccessor = (DA.IInvoicePODetailAccessor)Accessors.Get("InvoicePODetailAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");

        public Model.InvoicePO Get(string invoiceId)
        {
            Model.InvoicePO invoice = accessor.Get(invoiceId);
            invoice.Details = invoicePODetailAccessor.Select(invoice);
            return invoice;
        }

        protected override string GetInvoiceKind()
        {
            return "PO";
        }

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePO))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            _Insert((Model.InvoicePO)invoice);
        }

        private void _Insert(Book.Model.InvoicePO invoice)
        {
            //往来单位
            if (invoice.Department != null)
            {
                invoice.DepartmentId = invoice.Department.DepartmentId;
            }
            // 库房
            //invoice.DepotId = invoice.Depot.DepotId;
            //经手人
            if (invoice.Employee0 != null)
                invoice.Employee0Id = invoice.Employee0.EmployeeId;
            //录单人
            if (invoice.Employee1 != null)
                invoice.Employee1Id = invoice.Employee1.EmployeeId;

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                //过账人
                if (invoice.Employee2 != null)
                    invoice.Employee2Id = invoice.Employee2.EmployeeId;
                //过账时间
                invoice.InvoiceGZTime = DateTime.Now;
            }
            //插入表单
            accessor.Insert(invoice);

            //插入明细
            foreach (Model.InvoicePODetail detail in invoice.Details)
            {
                if (detail.DepotPosition == null || detail.DepotPositionId == null)
                {
                    throw new Exception("貨位不為空");
                }
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                {
                    continue;
                }
                detail.InvoiceId = invoice.InvoiceId;
                detail.InvoicePODetailWHQuantity = detail.InvoicePODetailJCQuantity;
                detail.InvoicePODetailYHQuantity = 0;
                invoicePODetailAccessor.Insert(detail);

                Model.Product p = detail.Product;
                p.StocksQuantity -= detail.InvoicePODetailJCQuantity;
                productAccessor.Update(p);

                Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(p.ProductId, detail.DepotPositionId);
                if (stock == null)
                {
                    stock = new Book.Model.Stock();
                    stock.StockId = Guid.NewGuid().ToString();
                    stock.ProductId = p.ProductId;
                    stock.DepotPositionId = detail.DepotPositionId;
                    stock.StockQuantity1 = detail.InvoicePODetailJCQuantity;
                    stock.StockCurrentJC = stock.StockCurrentJC == null ? detail.InvoicePODetailJCQuantity : stock.StockCurrentJC + detail.InvoicePODetailJCQuantity;
                    stock.DepotId = invoice.PoDepotId;
                    stock.ProduceUnit = p.ProduceUnit.CnName;
                    stockAccessor.Insert(stock);
                }
                else
                    stockAccessor.IncrementJC(new DepotPositionManager().Get(detail.DepotPositionId), p, detail.InvoicePODetailJCQuantity.Value);
            }
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePO))
                throw new ArgumentException();
            invoice.UpdateTime = DateTime.Now;
            _Update((Model.InvoicePO)invoice);
        }

        private void _Update(Model.InvoicePO invoice)
        {
            invoice.UpdateTime = DateTime.Now; ;
            if (invoice.Department != null)
                invoice.DepartmentId = invoice.Department.DepartmentId;
            //invoice.DepotId = invoice.Depot.DepotId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            Model.InvoicePO invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            accessor.Update(invoice);

                            invoicePODetailAccessor.Delete(invoice);

                            foreach (Model.InvoicePODetail detail in invoice.Details)
                            {
                                detail.InvoiceId = invoice.InvoiceId;
                                detail.InvoicePODetailWHQuantity = detail.InvoicePODetailJCQuantity;
                                detail.InvoicePODetailYHQuantity = 0;
                                invoicePODetailAccessor.Insert(detail);
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
                            #region

                            invoiceOriginal.InvoiceStatus = (int)Helper.InvoiceStatus.Null;

                            _TurnNull(invoiceOriginal);

                            invoicePODetailAccessor.Delete(invoice);

                            foreach (Model.InvoicePODetail detail in invoice.Details)
                            {
                                Model.Product p = detail.Product;
                                p.StocksQuantity -= detail.InvoicePODetailJCQuantity;
                                productAccessor.Update(p);

                                detail.InvoicePODetailWHQuantity = detail.InvoicePODetailJCQuantity - detail.InvoicePODetailYHQuantity;
                                invoicePODetailAccessor.Insert(detail);

                                Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(p.ProductId, detail.DepotPositionId);
                                if (stock == null)
                                {
                                    stock = new Book.Model.Stock();
                                    stock.StockId = Guid.NewGuid().ToString();
                                    stock.ProductId = p.ProductId;
                                    stock.DepotPositionId = detail.DepotPositionId;
                                    stock.StockQuantity1 = detail.InvoicePODetailYHQuantity;
                                    stock.StockCurrentJC = stock.StockCurrentJC == null ? detail.InvoicePODetailJCQuantity : stock.StockCurrentJC - detail.InvoicePODetailJCQuantity;
                                    stock.DepotId = invoice.PoDepotId;
                                    stock.ProduceUnit = p.ProduceUnit.CnName;
                                    stockAccessor.Insert(stock);
                                }
                                else
                                    stockAccessor.IncrementJC(new DepotPositionManager().Get(detail.DepotPositionId), p, detail.InvoicePODetailJCQuantity.Value);
                            }

                            #endregion
                            break;
                        case Helper.InvoiceStatus.Null:
                            foreach (Model.InvoicePODetail detail in invoice.Details)
                            { 
                                Model.Product p = detail.Product;
                                p.StocksQuantity += detail.InvoicePODetailJCQuantity;
                                productAccessor.Update(p);

                                stockAccessor.DecrementJC(new DepotPositionManager().Get(detail.DepotPositionId), p, detail.InvoicePODetailJCQuantity.Value);

                                detail.InvoicePODetailWHQuantity += detail.InvoicePODetailJCQuantity;
                                detail.InvoicePODetailJCQuantity -= detail.InvoicePODetailJCQuantity;                                
                                invoicePODetailAccessor.Update(detail);                                
                                
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
            if (!(invoice is Model.InvoicePO))
                throw new ArgumentException();

            _TurnNormal((Model.InvoicePO)invoice);
        }
        private void _TurnNormal(Model.InvoicePO invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }
        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePO))
                throw new ArgumentException();

            _TurnNull((Model.InvoicePO)invoice);
        }


        private void _TurnNull(Model.InvoicePO invoice)
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

        public IList<Model.InvoicePO> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoicePO> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

    }
}

