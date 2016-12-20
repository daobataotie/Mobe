//------------------------------------------------------------------------------
//
// file name：InvoiceZZManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceZZ.
    /// </summary>
    public partial class InvoiceZZManager : InvoiceManager
    {
        private static readonly DA.IInvoiceZZDetailAccessor invoiceZZDetailAccessor = (DA.IInvoiceZZDetailAccessor)Accessors.Get("InvoiceZZDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");

        #region Select

        public IList<Model.InvoiceZZ> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceZZ> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }
        public Model.InvoiceZZ Get(string invoiceId)
        {
            Model.InvoiceZZ invoice = accessor.Get(invoiceId);
            invoice.DetailsIn = invoiceZZDetailAccessor.Select("I",invoice);
            invoice.DetailsOut = invoiceZZDetailAccessor.Select("O",invoice);
            return invoice;
        }
        #endregion


        #region Validate
        /// <summary>
        /// 验证单据编号，经手人，出货和收货库房等
        /// </summary>        
        private void Validate(Model.InvoiceZZ invoiceZZ)
        {
            if (string.IsNullOrEmpty(invoiceZZ.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoiceZZ.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            if (invoiceZZ.DetailsIn.Count == 0)
                throw new Helper.RequireValueException("Details1");

            foreach (Model.InvoiceZZDetail detail in invoiceZZ.DetailsIn)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if(detail.InvoiceZZDetailZongji ==0)
                    throw new Helper.RequireValueException("Details1");
            }

            if (invoiceZZ.DetailsOut.Count == 0)
                throw new Helper.RequireValueException("Details0");
            foreach (Model.InvoiceZZDetail detail in invoiceZZ.DetailsOut)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if (detail.InvoiceZZDetailZongji == 0)
                    throw new Helper.RequireValueException("Details0");
            }
        }

        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceZZ))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceZZ)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceZZ))
                throw new ArgumentException();
            invoice.UpdateTime = DateTime.Now;
            _Update((Model.InvoiceZZ)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceZZ))
                throw new ArgumentException();          
            _TurnNormal((Model.InvoiceZZ)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceZZ))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceZZ)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceZZ invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceZZ invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "ZZ";
        }

        protected override Book.DA.IInvoiceAccessor GetAccessor()
        {
            return accessor;
        }

        #endregion

        #region Validation

        protected override void _ValidateForUpdate(Book.Model.Invoice invoice)
        {
            base._ValidateForUpdate(invoice);
            //
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {
            base._ValidateForInsert(invoice);

            Validate((Model.InvoiceZZ)invoice);
        }

        #endregion

        #endregion


        #region Helpers

        private void _Insert(Model.InvoiceZZ invoice)
        {
            _ValidateForInsert(invoice);
            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            invoice.Employee1Id = invoice.Employee1.EmployeeId;


            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                invoice.Employee2Id = invoice.Employee2.EmployeeId;
                invoice.InvoiceGZTime = DateTime.Now;
            }
            accessor.Insert(invoice);

            //插入收货商品明细
           
            Model.InvoiceZZDetail itemIn = invoice.DetailsIn[0];

            if (itemIn.Product == null || string.IsNullOrEmpty(itemIn.Product.ProductId)) { }
            else
            {
                //itemIn.DepotId = itemIn.Depot.DepotId;
                Model.Product product = itemIn.Product;

                //if (itemIn.InvoiceProductUnit == product.ProductOuterPackagingUnit)
                //{
                //    product.ProductCurrentCGPrice = itemIn.InvoiceZZDetailPrice / Convert.ToDecimal(product.ProductBaseUnitRelationship.Value) / Convert.ToDecimal(product.ProductInnerUnitRelationship.Value);
                //    quantity = itemIn.InvoiceZZDetailQuantity * product.ProductBaseUnitRelationship * product.ProductInnerUnitRelationship;
                //}
                //else if (itemIn.InvoiceProductUnit == product.ProductInnerPackagingUnit)
                //{
                //    product.ProductCurrentCGPrice = itemIn.InvoiceZZDetailPrice / Convert.ToDecimal(product.ProductBaseUnitRelationship.Value);
                //    quantity = itemIn.InvoiceZZDetailQuantity * product.ProductBaseUnitRelationship;
                //}
                //else
                //{
                //    product.ProductCurrentCGPrice = itemIn.InvoiceZZDetailPrice;
                //    quantity = itemIn.InvoiceZZDetailQuantity;
                //}

                itemIn.InvoiceId = invoice.InvoiceId;
                //productAccessor.UpdateCost1(product, product.ProductCurrentCGPrice,quantity);
                //stockAccessor.Increment(itemIn.Depot, itemIn.Product, quantity.Value);
                invoiceZZDetailAccessor.Insert(itemIn);
            }
            //插入出库商品明细
            foreach (Model.InvoiceZZDetail itemOut in invoice.DetailsOut)
            {
                if (itemOut.Product == null || string.IsNullOrEmpty(itemOut.Product.ProductId)) continue;
                //itemOut.DepotId = itemOut.Depot.DepotId;
                Model.Product p = itemOut.Product;

                //if (itemOut.InvoiceProductUnit == itemOut.Product.ProductOuterPackagingUnit)
                //{
                //    p.ProductCurrentCGPrice = itemOut.InvoiceZZDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value) / Convert.ToDecimal(p.ProductInnerUnitRelationship.Value);
                //    zsQuantity = itemOut.InvoiceZZDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                //}
                //else if (itemOut.InvoiceProductUnit == itemOut.Product.ProductInnerPackagingUnit)
                //{
                //    p.ProductCurrentCGPrice = itemOut.InvoiceZZDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value);
                //    zsQuantity = itemOut.InvoiceZZDetailQuantity * p.ProductBaseUnitRelationship;
                //}
                //else
                //{
                //    p.ProductCurrentCGPrice = itemOut.InvoiceZZDetailPrice;
                //    zsQuantity = itemOut.InvoiceZZDetailQuantity;
                //}
                //stockAccessor.Decrement(itemOut.Depot, p, zsQuantity.Value * itemIn.InvoiceZZDetailQuantity.Value);
                itemOut.InvoiceId = invoice.InvoiceId;
                invoiceZZDetailAccessor.Insert(itemOut);
            }           
        }

        private void _Update(Model.InvoiceZZ invoice)
        {
            _ValidateForUpdate(invoice);

           
            Model.InvoiceZZ invoiceOriginal = this.Get(invoice.InvoiceId);

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            invoice.UpdateTime = DateTime.Now;
                            accessor.Update(invoice);

                            invoiceZZDetailAccessor.Delete(invoiceOriginal);

                            foreach (Model.InvoiceZZDetail detail in invoice.DetailsIn)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceZZDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceZZDetailAccessor.Insert(detail);
                            }
                            foreach (Model.InvoiceZZDetail detail in invoice.DetailsOut)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceZZDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceZZDetailAccessor.Insert(detail);
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
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
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

                            Model.InvoiceZZDetail itemIn = invoice.DetailsIn[0];
                            Model.Product product = itemIn.Product;
                            //if (itemIn.InvoiceProductUnit == product.ProductOuterPackagingUnit)
                            //{
                            //    product.ProductCurrentCGPrice = itemIn.InvoiceZZDetailPrice / Convert.ToDecimal(product.ProductBaseUnitRelationship.Value) / Convert.ToDecimal(product.ProductInnerUnitRelationship.Value);
                            //    zzInQuantity = itemIn.InvoiceZZDetailQuantity * product.ProductBaseUnitRelationship * product.ProductInnerUnitRelationship;
                            //}
                            //else if (itemIn.InvoiceProductUnit == product.ProductInnerPackagingUnit)
                            //{
                            //    product.ProductCurrentCGPrice = itemIn.InvoiceZZDetailPrice / Convert.ToDecimal(product.ProductBaseUnitRelationship.Value);
                            //    zzInQuantity = itemIn.InvoiceZZDetailQuantity * product.ProductBaseUnitRelationship;
                            //}
                            //else
                            //{
                            //    product.ProductCurrentCGPrice = itemIn.InvoiceZZDetailPrice;
                            //    zzInQuantity = itemIn.InvoiceZZDetailQuantity;
                            //}
                            
                            //productAccessor.UpdateCost1(product, product.ProductCurrentCGPrice, -zzInQuantity);
                            //stockAccessor.Decrement(itemIn.Depot, product, zzInQuantity.Value);

                            foreach (Model.InvoiceZZDetail itemOut in invoice.DetailsOut)
                            {
                                if (itemOut.Product == null || string.IsNullOrEmpty(itemOut.Product.ProductId)) continue;
                                Model.Product p = itemOut.Product;
                                //if (itemOut.InvoiceProductUnit == itemOut.Product.ProductOuterPackagingUnit)
                                //{
                                //    p.ProductCurrentCGPrice = itemOut.InvoiceZZDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value) / Convert.ToDecimal(p.ProductInnerUnitRelationship.Value);
                                //    zzOutQuantity = itemOut.InvoiceZZDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                                //}
                                //else if (itemOut.InvoiceProductUnit == itemOut.Product.ProductInnerPackagingUnit)
                                //{
                                //    p.ProductCurrentCGPrice = itemOut.InvoiceZZDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value);
                                //    zzOutQuantity = itemOut.InvoiceZZDetailQuantity * p.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    p.ProductCurrentCGPrice = itemOut.InvoiceZZDetailPrice;
                                //    zzOutQuantity = itemOut.InvoiceZZDetailQuantity;
                                //}
                                //stockAccessor.Increment(itemOut.Depot, p, zzOutQuantity.Value * itemIn.InvoiceZZDetailQuantity.Value);
                            }
                            break;
                    }
                    break;

                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }

        #endregion

    }
}