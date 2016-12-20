//------------------------------------------------------------------------------
//
// file name：InvoiceHZManager.cs
// author: peidun
// create date：2008/6/20 15:33:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceHZ.
    /// </summary>
    public partial class InvoiceHZManager : InvoiceManager
    {
        private readonly static DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IInvoiceHZDetailAccessor invoiceHZDetailAccessor = (DA.IInvoiceHZDetailAccessor)Accessors.Get("InvoiceHZDetailAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");

        #region Select

        public IList<Model.InvoiceHZ> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceHZ> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }
        public Model.InvoiceHZ Get(string invoiceId)
        {
            Model.InvoiceHZ invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceHZDetailAccessor.Select(invoice);
            return invoice;
        }
        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceHZ))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;            
            _Insert((Model.InvoiceHZ)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceHZ))
                throw new ArgumentException();

            _Update((Model.InvoiceHZ)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceHZ))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceHZ)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceHZ))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceHZ)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceHZ invoice) 
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceHZ invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "HZ";
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

            Validate((Model.InvoiceHZ)invoice);
        }

        #endregion

        #endregion

        #region Helpers

        private void _Insert(Model.InvoiceHZ invoice)
        {
            _ValidateForInsert(invoice);
            if (invoice.Supplier!=null)
            invoice.SupplierId = invoice.Supplier.SupplierId;
            // 库房
            //invoice.DepotId = invoice.Depot.DepotId;
            //经手人
            if(invoice.Employee0!=null)
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            //录单人
            if(invoice.Employee1!=null)
            invoice.Employee1Id = invoice.Employee1.EmployeeId;

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                //过账人
                if(invoice.Employee2!=null)
                invoice.Employee2Id = invoice.Employee2.EmployeeId;
                //过账时间
                invoice.InvoiceGZTime = DateTime.Now;
            }
            //插入表单
            accessor.Insert(invoice);

            //插入明细
            foreach (Model.InvoiceHZDetail detail in invoice.Details)
            {
                if (detail.DepotPosition == null || detail.DepotPositionId == null)
                {
                    throw new Exception("貨位不為空");
                }
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                {
                    throw new Exception("商品不為空");
                }

                detail.InvoiceId = invoice.InvoiceId;               
                invoiceHZDetailAccessor.Insert(detail);
            }
            //影响库存
            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                foreach (Model.InvoiceHZDetail detail in invoice.Details)
                {
                    if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                  
                    //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                    //{
                    //    p.ProductCurrentCGPrice = detail.InvoiceHZDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value) / Convert.ToDecimal(p.ProductInnerUnitRelationship.Value);
                    //    hzQuantity = detail.InvoiceHZDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                    //}
                    //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                    //{
                    //    p.ProductCurrentCGPrice = detail.InvoiceHZDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value);
                    //    hzQuantity = detail.InvoiceHZDetailQuantity * p.ProductBaseUnitRelationship;
                    //}
                    //else
                    //{
                    //    p.ProductCurrentCGPrice = detail.InvoiceHZDetailPrice;
                    //    hzQuantity = detail.InvoiceHZDetailQuantity;
                    //}

                    //productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice, hzQuantity);

                    //stockAccessor.Increment(invoice.Depot, p, hzQuantity.Value);
                    //更新产品信息
                    Model.Product pro = detail.Product;                   
                    pro.StocksQuantity += detail.InvoiceHZDetailQuantity;
                    productAccessor.Update(pro);
                    stockAccessor.Increment(detail.DepotPosition, detail.Product, detail.InvoiceHZDetailQuantity);
                  
                }
            }
        }

        private void _Update(Model.InvoiceHZ invoice)
        {
            invoice.UpdateTime = DateTime.Now;           
            //invoice.DepotId = invoice.Depot.DepotId;
            if(invoice.Employee0!=null)
            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            Model.InvoiceHZ invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            accessor.Update(invoice);

                            invoiceHZDetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceHZDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceHZDetailAccessor.Insert(detail);
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
                            invoiceHZDetailAccessor.Delete(invoiceOriginal);
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:
                            foreach (Model.InvoiceHZDetail detail in invoice.Details)
                            {
                                //if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                //Model.Product p = detail.Product;

                                //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                                //{
                                //    p.ProductCurrentCGPrice = detail.InvoiceHZDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value) / Convert.ToDecimal(p.ProductInnerUnitRelationship.Value);
                                //    hzQuantity = detail.InvoiceHZDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    p.ProductCurrentCGPrice = detail.InvoiceHZDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value);
                                //    hzQuantity = detail.InvoiceHZDetailQuantity * p.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    p.ProductCurrentCGPrice = detail.InvoiceHZDetailPrice;
                                //    hzQuantity = detail.InvoiceHZDetailQuantity;
                                //}
                                //productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice, -hzQuantity);                                

                                //stockAccessor.Decrement(invoice.Depot,p,hzQuantity.Value);



                                //更新产品信息
                                Model.Product pro = detail.Product;                           
                                pro.StocksQuantity-= detail.InvoiceHZDetailQuantity;
                                productAccessor.Update(pro);
                                //修改货位库存。
                                stockAccessor.Decrement(detail.DepotPosition, pro, detail.InvoiceHZDetailQuantity);


                            }
                            break;
                    }
                    break;
                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }

        private void Validate(Model.InvoiceHZ invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            if (invoice.Depot == null)
                throw new Helper.RequireValueException("Depot");

            if (invoice.Supplier == null)
                throw new Helper.RequireValueException("Supplier");

            if (invoice.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            //foreach (Model.InvoiceHZDetail detail in invoice.Details)
            //{
            //    if (detail.InvoiceHZDetailQuantity == 0)
            //        throw new Helper.RequireValueException("Details");
            //}
        }

        #endregion

    }
}

