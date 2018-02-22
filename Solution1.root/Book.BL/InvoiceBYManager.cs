//------------------------------------------------------------------------------
//
// file name：InvoiceBYManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceBY.
    /// </summary>
    public partial class InvoiceBYManager : InvoiceManager
    {
        private readonly static DA.IInvoiceBYDetailAccessor invoiceBYDetailAccessor = (DA.IInvoiceBYDetailAccessor)Accessors.Get("InvoiceBYDetailAccessor");
        private readonly static DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");

        #region Select

        public IList<Model.InvoiceBY> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceBY> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        public Model.InvoiceBY Get(string invoiceId)
        {
            Model.InvoiceBY invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceBYDetailAccessor.Select(invoice);
            return invoice;
        }
        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceBY))
                throw new ArgumentException();

            _Insert((Model.InvoiceBY)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceBY))
                throw new ArgumentException();

            _Update((Model.InvoiceBY)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceBY))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceBY)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceBY))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceBY)invoice);
        }

        #endregion

        #region Other

        protected override string GetInvoiceKind()
        {
            return "BY";
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

            Model.InvoiceBY invoiceBY = invoice as Model.InvoiceBY;

            if (invoiceBY.Depot == null)
               throw new Helper.RequireValueException("Depot");

            if (invoiceBY.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            foreach (Model.InvoiceBYDetail detail in invoiceBY.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if (detail.InvoiceBYDetailQuantity == 0)
                    throw new Helper.RequireValueException("Details");
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                {
                    throw new Exception("商品不為空");
                }
                if (detail.DepotPosition == null || detail.DepotPositionId == null)
                {
                    throw new Exception("貨位不為空");
                }
            }
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {
            base._ValidateForInsert(invoice);
            
            Model.InvoiceBY invoiceBS = invoice as Model.InvoiceBY;

           if (invoiceBS.Depot == null)
               throw new Helper.RequireValueException("Depot");

            if (invoiceBS.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            foreach (Model.InvoiceBYDetail detail in invoiceBS.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if (detail.InvoiceBYDetailQuantity == 0)
                    throw new Helper.RequireValueException("Details");
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                {
                    throw new Exception("商品不為空");
                }
                if (detail.DepotPosition == null || detail.DepotPositionId == null)
                {
                    throw new Exception("貨位不為空");
                }
            }
        }

        #endregion

        #endregion


        #region Helpers

        private void _TurnNormal(Model.InvoiceBY invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        private void _TurnNull(Model.InvoiceBY invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _Update(Model.InvoiceBY invoice)
        {
            _ValidateForUpdate(invoice);

            Model.InvoiceBY invoiceOriginal = this.Get(invoice.InvoiceId);

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            invoice.UpdateTime = DateTime.Now;
                            //invoice.DepotId = invoice.Depot.DepotId;
                            invoice.Employee0Id = invoice.Employee0.EmployeeId;
                            accessor.Update(invoice);

                            invoiceBYDetailAccessor.Delete(invoiceOriginal);
                            foreach (Model.InvoiceBYDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceBYDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceBYDetailAccessor.Insert(detail);
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
                            invoiceBYDetailAccessor.Delete(invoiceOriginal);
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;                            
                            _Insert(invoice);
                            break;

                        case Helper.InvoiceStatus.Null:
                            // 库存
                            foreach (Model.InvoiceBYDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                               // double? quantity = 0;
                                //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                                //{
                                //    quantity = detail.InvoiceBYDetailQuantity * detail.Product.ProductInnerUnitRelationship * detail.Product.ProductBaseUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    quantity = detail.InvoiceBYDetailQuantity * detail.Product.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    quantity = detail.InvoiceBYDetailQuantity;
                                //}
                                //productAccessor.UpdateCost1(detail.Product, 0, -quantity);
                                stockAccessor.Decrement(detail.DepotPosition, detail.Product, detail.InvoiceBYDetailQuantity);
                                productAccessor.UpdateProduct_Stock(detail.Product);
                            }
                            break;
                    }
                    break;

                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }

        private void _Insert(Model.InvoiceBY invoice)
        {
            _ValidateForInsert(invoice);

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            //invoice.DepotId = invoice.Depot.DepotId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            if(invoice.Employee1!=null)
            invoice.Employee1Id = invoice.Employee1.EmployeeId;
            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                if(invoice.Employee2!=null)
                invoice.Employee2Id = invoice.Employee2.EmployeeId;
                invoice.InvoiceGZTime = DateTime.Now;
            }
            accessor.Insert(invoice);
            foreach (Model.InvoiceBYDetail detail in invoice.Details)
            {             
            
                detail.InvoiceId = invoice.InvoiceId;
                invoiceBYDetailAccessor.Insert(detail);
            }
            // 影响
            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                // 库存
                foreach (Model.InvoiceBYDetail detail in invoice.Details)
                {                  
                    if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                    //double? quantity = 0;
                    //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                    //{
                    //    quantity = detail.InvoiceBYDetailQuantity * detail.Product.ProductInnerUnitRelationship * detail.Product.ProductBaseUnitRelationship;
                    //}
                    //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                    //{
                    //    quantity = detail.InvoiceBYDetailQuantity * detail.Product.ProductBaseUnitRelationship;
                    //}
                    //else
                    //{
                    //    quantity = detail.InvoiceBYDetailQuantity;
                    //}
                   // productAccessor.UpdateCost1(detail.Product, 0, quantity);

                   stockAccessor.Increment(detail.DepotPosition, detail.Product,detail.InvoiceBYDetailQuantity);
                   productAccessor.UpdateProduct_Stock(detail.Product);
                }
            }
        }

        #endregion

    }
}

