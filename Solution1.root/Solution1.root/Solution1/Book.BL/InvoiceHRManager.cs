//------------------------------------------------------------------------------
//
// file name：InvoiceHRManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceHR.
    /// </summary>
    public partial class InvoiceHRManager : InvoiceManager
    {
        private readonly static DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IInvoiceHRDetailAccessor invoiceHRDetailAccessor = (DA.IInvoiceHRDetailAccessor)Accessors.Get("InvoiceHRDetailAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IInvoiceJCDetailAccessor invoiceJCDetailAccessor = (DA.IInvoiceJCDetailAccessor)Accessors.Get("InvoiceJCDetailAccessor");

        public Model.InvoiceHR Get(string invoiceId)
        {
            Model.InvoiceHR invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceHRDetailAccessor.Select(invoice);
            return invoice;
        }

        protected override string GetInvoiceKind()
        {
            return "HR";
        }

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceHR))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            _Insert((Model.InvoiceHR)invoice);
        }

        private void _Insert(Book.Model.InvoiceHR invoice)
        {
            //往来单位
            if (invoice.Customer != null)
            {
                invoice.CustomerId = invoice.Customer.CustomerId;
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
            foreach (Model.InvoiceHRDetail detail in invoice.Details)
            {
                Model.InvoiceJCDetail odetail = invoiceJCDetailAccessor.Get(detail.InvoiceJCDetailId);

                if (detail.InvoiceHRDetailQuantity > 0)
                {
                    if (detail.InvoiceHRDetailQuantity <= odetail.InvoiceWeiHuaiRuQuantity)
                    {
                        detail.InvoiceId = invoice.InvoiceId;
                        invoiceHRDetailAccessor.Insert(detail);

                        Model.Product p = detail.Product;
                        p.StocksQuantity += detail.InvoiceHRDetailQuantity;
                        productAccessor.Update(detail.Product);

                        stockAccessor.IncrementJR(new DepotPositionManager().Get(detail.DepotPositionId), p, detail.InvoiceHRDetailQuantity.Value);

                        if (odetail != null)
                        {
                            odetail.InvoiceYiHuaiRuQuantity += detail.InvoiceHRDetailQuantity;
                            odetail.InvoiceWeiHuaiRuQuantity -= detail.InvoiceHRDetailQuantity;
                            invoiceJCDetailAccessor.Update(odetail);
                        }

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
            if (!(invoice is Model.InvoiceHR))
                throw new ArgumentException();
            invoice.UpdateTime = DateTime.Now;
            _Update((Model.InvoiceHR)invoice);
        }

        private void _Update(Model.InvoiceHR invoice)
        {
            invoice.UpdateTime = DateTime.Now; ;
            invoice.CustomerId = invoice.Customer.CustomerId;
            if (invoice.Employee0 != null)
                invoice.Employee0Id = invoice.Employee0.EmployeeId;

            Model.InvoiceHR invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            accessor.Update(invoice);

                            invoiceHRDetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceHRDetail detail in invoice.Details)
                            {
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceHRDetailAccessor.Insert(detail);
                            }
                            break;
                        case Helper.InvoiceStatus.Normal:
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);

                            //accessor.Update(invoice);

                            //invoiceHZDetailAccessor.Delete(invoice);

                            //foreach (Model.InvoiceHZDetail detail in invoice.Details)
                            //{
                            //    stockAccessor.Increment(invoice.Depot, detail.Product, detail.InvoiceHZDetailQuantity.Value);

                            //    detail.InvoiceHZDetailId = Guid.NewGuid().ToString();
                            //    detail.InvoiceId = invoice.InvoiceId;

                            //    invoiceHZDetailAccessor.Insert(detail);
                            //}
                            //invoice.Employee2Id = invoice.Employee2.EmployeeId;
                            //invoice.InvoiceGZTime = DateTime.Now;
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
                            foreach (Model.InvoiceHRDetail detail in invoice.Details)
                            {

                                Model.Product p = detail.Product;
                                //if (detail.InvoiceProductUnit == p.ProductOuterPackagingUnit)
                                //{
                                //    quantity = detail.InvoiceHRDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    quantity = detail.InvoiceHRDetailQuantity * p.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    quantity = detail.InvoiceHRDetailQuantity;
                                //}

                                //detail.Product.ProductCurrentJCQuantity += quantity;
                                p.StocksQuantity -= detail.InvoiceHRDetailQuantity;
                                productAccessor.Update(p);

                                stockAccessor.DecrementJR(new DepotPositionManager().Get(detail.DepotPositionId), p, detail.InvoiceHRDetailQuantity.Value);
                                Model.InvoiceJCDetail temp = invoiceJCDetailAccessor.Get(detail.InvoiceJCDetailId);
                                if (temp != null)
                                {
                                    temp.InvoiceWeiHuaiRuQuantity += detail.InvoiceHRDetailQuantity;
                                    temp.InvoiceYiHuaiRuQuantity -= detail.InvoiceHRDetailQuantity;
                                    invoiceJCDetailAccessor.Update(temp);
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
            if (!(invoice is Model.InvoiceHR))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceHR)invoice);
        }
        private void _TurnNormal(Model.InvoiceHR invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }
        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceHR))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceHR)invoice);
        }


        private void _TurnNull(Model.InvoiceHR invoice)
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

        public IList<Model.InvoiceHR> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceHR> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }
    }
}

