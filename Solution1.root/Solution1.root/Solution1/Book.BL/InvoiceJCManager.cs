//------------------------------------------------------------------------------
//
// file name：InvoiceJCManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceJC.
    /// </summary>

    public partial class InvoiceJCManager : InvoiceManager
    {
        private readonly static DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IInvoiceJCDetailAccessor invoiceJCDetailAccessor = (DA.IInvoiceJCDetailAccessor)Accessors.Get("InvoiceJCDetailAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IDepotPositionAccessor depotpositionAccessor = (DA.IDepotPositionAccessor)Accessors.Get("DepotPositionAccessor");

        public Model.InvoiceJC Get(string invoiceId)
        {
            Model.InvoiceJC invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceJCDetailAccessor.Select(invoice);
            return invoice;
        }

        protected override string GetInvoiceKind()
        {
            return "JC";
        }

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceJC))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            _Insert((Model.InvoiceJC)invoice);
        }

        private void _Insert(Book.Model.InvoiceJC invoice)
        {
            //往来单位
            //if (invoice.Customer != null)
            //{
            //    invoice.CustomerId = invoice.Customer.CustomerId;
            //}
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
            foreach (Model.InvoiceJCDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                detail.InvoiceId = invoice.InvoiceId;
                detail.InvoiceWeiHuaiRuQuantity = detail.InvoiceJCDetailQuantity;
                detail.InvoiceYiHuaiRuQuantity = 0;
                invoiceJCDetailAccessor.Insert(detail);

                Model.Product p = detail.Product;
                p.StocksQuantity += detail.InvoiceJCDetailQuantity.Value;
                productAccessor.Update(p);
                //if (detail.InvoiceProductUnit == p.ProductOuterPackagingUnit)
                //{
                //    quantity = detail.InvoiceJCDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                //}
                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                //{
                //    quantity = detail.InvoiceJCDetailQuantity * p.ProductBaseUnitRelationship;
                //}
                //else
                //{
                //    quantity = detail.InvoiceJCDetailQuantity;
                //}
                Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(p.ProductId, detail.DepotPositionId);
                if (stock == null)
                {
                    stock = new Book.Model.Stock();
                    stock.StockId = Guid.NewGuid().ToString();
                    stock.ProductId = p.ProductId;
                    stock.DepotPositionId = detail.DepotPositionId;
                    stock.StockQuantity1 = detail.InvoiceJCDetailQuantity;
                    stock.StockCurrentJC = stock.StockCurrentJR == null ? detail.InvoiceJCDetailQuantity : stock.StockCurrentJC - detail.InvoiceJCDetailQuantity;
                    stock.DepotId = invoice.DepotId;
                    stock.ProduceUnit = p.ProduceUnit.CnName;
                    stockAccessor.Insert(stock);
                }
                else
                    stockAccessor.IncrementJC(depotpositionAccessor.Get(detail.DepotPositionId), p, detail.InvoiceJCDetailQuantity.Value);

            }
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceJC))
                throw new ArgumentException();
            invoice.UpdateTime = DateTime.Now;
            _Update((Model.InvoiceJC)invoice);
        }

        private void _Update(Model.InvoiceJC invoice)
        {
            invoice.UpdateTime = DateTime.Now; ;
            //invoice.CustomerId = invoice.Customer.CustomerId;
            //invoice.DepotId = invoice.Depot.DepotId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            Model.InvoiceJC invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            accessor.Update(invoice);

                            invoiceJCDetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceJCDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceId = invoice.InvoiceId;
                                detail.InvoiceWeiHuaiRuQuantity = detail.InvoiceJCDetailQuantity;
                                detail.InvoiceYiHuaiRuQuantity = 0;
                                invoiceJCDetailAccessor.Insert(detail);
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

                            invoiceJCDetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceJCDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                Model.Product p = detail.Product;

                                //if (detail.InvoiceProductUnit == p.ProductOuterPackagingUnit)
                                //{
                                //    quantity = detail.InvoiceJCDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    quantity = detail.InvoiceJCDetailQuantity * p.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    quantity = detail.InvoiceJCDetailQuantity;
                                //}
                                p.StocksQuantity -= detail.InvoiceJCDetailQuantity.Value;
                                productAccessor.Update(p);

                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceJCDetailAccessor.Insert(detail);

                                Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(p.ProductId, detail.DepotPositionId);
                                if (stock == null)
                                {
                                    stock = new Book.Model.Stock();
                                    stock.StockId = Guid.NewGuid().ToString();
                                    stock.ProductId = p.ProductId;
                                    stock.DepotPositionId = detail.DepotPositionId;
                                    stock.StockQuantity1 = detail.InvoiceJCDetailQuantity;
                                    stock.StockCurrentJC = stock.StockCurrentJC == null ? detail.InvoiceJCDetailQuantity : stock.StockCurrentJC + detail.InvoiceJCDetailQuantity;
                                    stock.DepotId = invoice.DepotId;
                                    stock.ProduceUnit = p.ProduceUnit.CnName;
                                    stockAccessor.Insert(stock);
                                }
                                else
                                    stockAccessor.IncrementJC(depotpositionAccessor.Get(detail.DepotPositionId), p, detail.InvoiceJCDetailQuantity.Value);

                                
                            }

                            break;
                        case Helper.InvoiceStatus.Null:
                            foreach (Model.InvoiceJCDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                Model.Product p = detail.Product;
                                //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                                //{
                                //    jrQuantity = detail.InvoiceJCDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    jrQuantity = detail.InvoiceJCDetailQuantity * p.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    jrQuantity = detail.InvoiceJCDetailQuantity;
                                //}

                                //p.ProductCurrentJCQuantity -= jrQuantity;
                                productAccessor.Update(p);

                                stockAccessor.DecrementJC(depotpositionAccessor.Get(detail.DepotPositionId), p, detail.InvoiceJCDetailQuantity.Value);
                                //stockAccessor.DecrementJC(invoice.Depot, p, jrQuantity.Value);
                                detail.InvoiceWeiHuaiRuQuantity += detail.InvoiceJCDetailQuantity;
                                detail.InvoiceJCDetailQuantity -= detail.InvoiceJCDetailQuantity;
                                invoiceJCDetailAccessor.Update(detail);

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
            if (!(invoice is Model.InvoiceJC))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceJC)invoice);
        }
        private void _TurnNormal(Model.InvoiceJC invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }
        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceJC))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceJC)invoice);
        }


        private void _TurnNull(Model.InvoiceJC invoice)
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

        public IList<Model.InvoiceJC> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceJC> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }
    }
}

