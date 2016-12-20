//------------------------------------------------------------------------------
//
// file name：InvoiceHCManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceHC.
    /// </summary>
    public partial class InvoiceHCManager : InvoiceManager
    {
        private readonly static DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IInvoiceHCDetailAccessor invoiceHCDetailAccessor = (DA.IInvoiceHCDetailAccessor)Accessors.Get("InvoiceHCDetailAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IInvoiceJRDetailAccessor invoiceJRDetailAccessor = (DA.IInvoiceJRDetailAccessor)Accessors.Get("InvoiceJRDetailAccessor");

        public Model.InvoiceHC Get(string invoiceId)
        {
            Model.InvoiceHC invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceHCDetailAccessor.Select(invoice);
            return invoice;
        }

        protected override string GetInvoiceKind()
        {
            return "HC";
        }

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceHC))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            _Insert((Model.InvoiceHC)invoice);
        }

        private void _Insert(Book.Model.InvoiceHC invoice)
        {
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
            foreach (Model.InvoiceHCDetail detail in invoice.Details)
            {
                Model.InvoiceJRDetail odetail = invoiceJRDetailAccessor.Get(detail.InvoiceJRDetailId);

                Model.InvoiceHCDetail hcdetail = new Book.Model.InvoiceHCDetail();
                if (detail.InvoiceHCDetailQuantity > 0)
                {
                    if (detail.InvoiceHCDetailQuantity <= odetail.InvoiceWeiHuaiChuQuantity)
                    {
                        hcdetail.InvoiceId = invoice.InvoiceId;
                        hcdetail.InvoiceHCDetailId = Guid.NewGuid().ToString();
                        hcdetail.InvoiceHCDetailNote = detail.InvoiceHCDetailNote;
                        hcdetail.InvoiceHCDetailQuantity = detail.InvoiceHCDetailQuantity;
                        hcdetail.InvoiceJRDetailId = detail.InvoiceJRDetailId;
                        hcdetail.InvoiceProductUnit = detail.InvoiceProductUnit;
                        hcdetail.DepotPositionId = detail.DepotPositionId;
                        hcdetail.ProductId = detail.ProductId;

                        invoiceHCDetailAccessor.Insert(hcdetail);
                        Model.Product p = productAccessor.Get(detail.ProductId);
                        //if (p.ProductImage == null || p.ProductImage.Length == 0)
                        //    p.ProductImage = new byte[] { };
                        //if (p.ProductImage1 == null || p.ProductImage1.Length == 0)
                        //    p.ProductImage1 = new byte[] { };
                        //if (p.ProductImage2 == null || p.ProductImage2.Length == 0)
                        //    p.ProductImage2 = new byte[] { };
                        //if (p.ProductImage3 == null || p.ProductImage3.Length == 0)
                        //    p.ProductImage3 = new byte[] { };
                        p.StocksQuantity -= detail.InvoiceHCDetailQuantity;
                        productAccessor.Update(p);
                        odetail.InvoiceWeiHuaiChuQuantity -= detail.InvoiceHCDetailQuantity;
                        odetail.InvoiceYiHuaiChuQuantity += detail.InvoiceHCDetailQuantity;

                        invoiceJRDetailAccessor.Update(odetail);

                        Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(p.ProductId, detail.DepotPositionId);
                        if (stock == null)
                        {
                            stock = new Book.Model.Stock();
                            stock.StockId = Guid.NewGuid().ToString();
                            stock.ProductId = p.ProductId;
                            stock.DepotPositionId = detail.DepotPositionId;
                            stock.StockQuantity1 = detail.InvoiceHCDetailQuantity;
                            stock.StockCurrentJC = stock.StockCurrentJC == null ? detail.InvoiceHCDetailQuantity : stock.StockCurrentJC + detail.InvoiceHCDetailQuantity;
                            stock.DepotId = invoice.DepotId;
                            stock.ProduceUnit = p.ProduceUnit.CnName;
                            stockAccessor.Insert(stock);
                        }
                        else
                            stockAccessor.IncrementJC(new BL.DepotPositionManager().Get(detail.DepotPositionId), p, detail.InvoiceHCDetailQuantity.Value);
                    }
                    else
                    {
                        throw new Helper.InvalidValueException("HaiRuTaiDuo");
                    }
                }
                else
                {
                    throw new Helper.InvalidValueException("Details");
                }
            }
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceHC))
                throw new ArgumentException();
            invoice.UpdateTime = DateTime.Now;
            _Update((Model.InvoiceHC)invoice);
        }

        private void _Update(Model.InvoiceHC invoice)
        {
            invoice.UpdateTime = DateTime.Now; ;
            //invoice.CustomerId = invoice.Customer.CustomerId;
            if (invoice.Employee0 != null)
                invoice.Employee0Id = invoice.Employee0.EmployeeId;

            Model.InvoiceHC invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
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
                        case Helper.InvoiceStatus.Normal:
                            invoiceOriginal.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
                            _TurnNull(invoiceOriginal);
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:
                            foreach (Model.InvoiceHCDetail detail in invoice.Details)
                            {
                                if (detail.InvoiceHCDetailQuantity > 0)
                                {
                                    Model.InvoiceJRDetail temp = invoiceJRDetailAccessor.Get(detail.InvoiceJRDetailId);
                                    if (temp != null)
                                    {
                                        temp.InvoiceWeiHuaiChuQuantity += detail.InvoiceHCDetailQuantity;
                                        temp.InvoiceYiHuaiChuQuantity -= detail.InvoiceHCDetailQuantity;
                                        invoiceJRDetailAccessor.Update(temp);
                                    }
                                    Model.Product p = detail.Product;
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
                                    stockAccessor.DecrementJC(new BL.DepotPositionManager().Get(detail.DepotPositionId), p, detail.InvoiceHCDetailQuantity.Value);
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
            if (!(invoice is Model.InvoiceHC))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceHC)invoice);
        }
        private void _TurnNormal(Model.InvoiceHC invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }
        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceHC))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceHC)invoice);
        }


        private void _TurnNull(Model.InvoiceHC invoice)
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

        public IList<Model.InvoiceHC> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceHC> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

    }
}

