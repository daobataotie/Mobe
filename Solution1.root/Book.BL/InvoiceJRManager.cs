//------------------------------------------------------------------------------
//
// file name：InvoiceJRManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceJR.
    /// </summary>
    public partial class InvoiceJRManager : InvoiceManager
    {
        private readonly static DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IInvoiceJRDetailAccessor invoiceJRDetailAccessor = (DA.IInvoiceJRDetailAccessor)Accessors.Get("InvoiceJRDetailAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IDepotPositionAccessor depotPositionAcccessor = (DA.IDepotPositionAccessor)Accessors.Get("DepotPositionAccessor");

        public Model.InvoiceJR Get(string invoiceId)
        {
            Model.InvoiceJR invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceJRDetailAccessor.Select(invoice);
            return invoice;
        }

        protected override string GetInvoiceKind()
        {
            return "JR";
        }

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceJR))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            _Insert((Model.InvoiceJR)invoice);
        }

        private void _Insert(Book.Model.InvoiceJR invoice)
        {
            //往来单位
            //if (invoice.Customer != null)
            //    invoice.CustomerId = invoice.Customer.CustomerId;
            // 库房
            //invoice.DepotId = invoice.Depot.DepotId;
            //经手人
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
            foreach (Model.InvoiceJRDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                detail.InvoiceId = invoice.InvoiceId;
                detail.InvoiceWeiHuaiChuQuantity = detail.InvoiceJRDetailQuantity;
                detail.InvoiceYiHuaiChuQuantity = 0;
                invoiceJRDetailAccessor.Insert(detail);

                Model.Product p = detail.Product;
                //if (detail.InvoiceProductUnit == p.ProductOuterPackagingUnit)
                //{
                //    quantity = detail.InvoiceJRDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                //}
                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                //{
                //    quantity = detail.InvoiceJRDetailQuantity * p.ProductBaseUnitRelationship;
                //}
                //else
                //{
                //    quantity = detail.InvoiceJRDetailQuantity;
                //}
                //byte[] pic = new byte[] { };
                //if (p.ProductImage == null)
                //    p.ProductImage = pic;
                //if (p.ProductImage1 == null)
                //    p.ProductImage1 = pic;
                //if (p.ProductImage2 == null)
                //    p.ProductImage2 = pic;
                //if (p.ProductImage3 == null)
                //    p.ProductImage3 = pic;
                p.StocksQuantity += detail.InvoiceJRDetailQuantity;
                productAccessor.Update(p);

                Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(p.ProductId, detail.DepotPositionId);
                if (stock == null)
                {
                    stock = new Book.Model.Stock();
                    stock.StockId = Guid.NewGuid().ToString();
                    stock.ProductId = p.ProductId;
                    stock.DepotPositionId = detail.DepotPositionId;
                    stock.StockQuantity1 = detail.InvoiceJRDetailQuantity;
                    stock.StockCurrentJR = stock.StockCurrentJR == null ? detail.InvoiceJRDetailQuantity : stock.StockCurrentJR + detail.InvoiceJRDetailQuantity;
                    stock.DepotId = invoice.DepotId;
                    stock.ProduceUnit = p.ProduceUnit.CnName;
                    stockAccessor.Insert(stock);
                }
                else
                    stockAccessor.IncrementJR(depotPositionAcccessor.Get(detail.DepotPositionId), p, detail.InvoiceJRDetailQuantity.Value);
            }
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceJR))
                throw new ArgumentException();
            invoice.UpdateTime = DateTime.Now;
            _Update((Model.InvoiceJR)invoice);
        }

        private void _Update(Model.InvoiceJR invoice)
        {
            invoice.UpdateTime = DateTime.Now; ;
            //if (invoice.Customer != null)
            //    invoice.CustomerId = invoice.Customer.CustomerId;
            //invoice.DepotId = invoice.Depot.DepotId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            Model.InvoiceJR invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            accessor.Update(invoice);

                            invoiceJRDetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceJRDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceId = invoice.InvoiceId;
                                detail.InvoiceWeiHuaiChuQuantity = detail.InvoiceJRDetailQuantity;
                                detail.InvoiceYiHuaiChuQuantity = 0;
                                invoiceJRDetailAccessor.Insert(detail);
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

                            invoiceJRDetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceJRDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                Model.Product p = detail.Product;
                                //if (detail.InvoiceProductUnit == p.ProductOuterPackagingUnit)
                                //{
                                //    quantity = detail.InvoiceJRDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    quantity = detail.InvoiceJRDetailQuantity * p.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    quantity = detail.InvoiceJRDetailQuantity;
                                //}
                                //p.ProductCurrentJRQuantity += quantity;
                                if (detail.InvoiceJRDetailQuantity.Value == 0)
                                    throw new Helper.InvalidValueException(Model.InvoiceJRDetail.PRO_InvoiceJRDetailQuantity);
                                byte[] pic = new byte[] { };
                                //if (p.ProductImage == null)
                                //    p.ProductImage = pic;
                                //if (p.ProductImage1 == null)
                                //    p.ProductImage1 = pic;
                                //if (p.ProductImage2 == null)
                                //    p.ProductImage2 = pic;
                                //if (p.ProductImage3 == null)
                                //    p.ProductImage3 = pic;
                                p.StocksQuantity += detail.InvoiceJRDetailQuantity.Value;
                                productAccessor.Update(p);

                                detail.InvoiceWeiHuaiChuQuantity = detail.InvoiceJRDetailQuantity - detail.InvoiceYiHuaiChuQuantity;

                                Model.InvoiceJRDetail temp = invoiceJRDetailAccessor.Get(detail.InvoiceJRDetailId);
                                if (temp != null)
                                {
                                    temp.InvoiceId = invoice.InvoiceId;
                                    temp.InvoiceHCDetailQuantity = detail.InvoiceHCDetailQuantity;
                                    invoiceJRDetailAccessor.Update(temp);
                                }
                                else
                                {
                                    detail.InvoiceId = invoice.InvoiceId;
                                    invoiceJRDetailAccessor.Insert(detail);
                                }

                                Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(p.ProductId, detail.DepotPositionId);
                                if (stock == null)
                                {
                                    stock = new Book.Model.Stock();
                                    stock.StockId = Guid.NewGuid().ToString();
                                    stock.ProductId = p.ProductId;
                                    stock.DepotPositionId = detail.DepotPositionId;
                                    stock.StockQuantity1 = detail.InvoiceJRDetailQuantity;
                                    stock.StockCurrentJR = stock.StockCurrentJR == null ? detail.InvoiceJRDetailQuantity : stock.StockCurrentJR + detail.InvoiceJRDetailQuantity;
                                    stock.DepotId = invoice.DepotId;
                                    stock.ProduceUnit = p.ProduceUnit.CnName;
                                    stockAccessor.Insert(stock);
                                }
                                else
                                    stockAccessor.IncrementJR(depotPositionAcccessor.Get(detail.DepotPositionId), p, detail.InvoiceJRDetailQuantity.Value);
                            }
                            accessor.Update(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:

                            foreach (Model.InvoiceJRDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                Model.Product p = detail.Product;
                                //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                                //{                                    
                                //    jrQuantity = detail.InvoiceJRDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    jrQuantity = detail.InvoiceJRDetailQuantity * p.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    jrQuantity = detail.InvoiceJRDetailQuantity;
                                //}

                                //p.ProductCurrentJRQuantity -= jrQuantity;
                                p.StocksQuantity -= detail.InvoiceJRDetailQuantity.Value;
                                //byte[] pic = new byte[] { };
                                //if (p.ProductImage == null)
                                //    p.ProductImage = pic;
                                //if (p.ProductImage1 == null)
                                //    p.ProductImage1 = pic;
                                //if (p.ProductImage2 == null)
                                //    p.ProductImage2 = pic;
                                //if (p.ProductImage3 == null)
                                //    p.ProductImage3 = pic;
                                productAccessor.Update(p);

                                stockAccessor.DecrementJR(depotPositionAcccessor.Get(detail.DepotPositionId), p, detail.InvoiceJRDetailQuantity.Value);

                                detail.InvoiceWeiHuaiChuQuantity -= detail.InvoiceJRDetailQuantity;
                                detail.InvoiceJRDetailQuantity -= detail.InvoiceJRDetailQuantity;

                                invoiceJRDetailAccessor.Update(detail);
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
            if (!(invoice is Model.InvoiceJR))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceJR)invoice);
        }
        private void _TurnNormal(Model.InvoiceJR invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }
        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceJR))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceJR)invoice);
        }


        private void _TurnNull(Model.InvoiceJR invoice)
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

        public IList<Model.InvoiceJR> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceJR> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        public IList<Model.InvoiceJR> Select(Model.InvoiceJR invoicejr)
        {
            return accessor.Select(invoicejr);
        }

        public IList<Model.InvoiceJR> Select(DateTime startdate, DateTime enddate, Model.Supplier supplier)
        {
            return accessor.Select(startdate, enddate, supplier);
        }
    }
}

