//------------------------------------------------------------------------------
//
// file name：InvoiceBSManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceBS.
    /// </summary>
    public partial class InvoiceBSManager : InvoiceManager
    {
        private readonly static DA.IInvoiceBSDetailAccessor invoiceBSDetailAccessor = (DA.IInvoiceBSDetailAccessor)Accessors.Get("InvoiceBSDetailAccessor");
        private readonly static DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
       
        #region Select

        public IList<Model.InvoiceBS> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceBS> Select(Helper.InvoiceStatus status) 
        {
            return accessor.Select(status);
        }

        public Model.InvoiceBS Get(string invoiceId)
        {
            Model.InvoiceBS invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceBSDetailAccessor.Select(invoice);
            return invoice;
        }
        #endregion

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceBS))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceBS)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceBS))
                throw new ArgumentException();

            _Update((Model.InvoiceBS)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceBS))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceBS)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceBS))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceBS)invoice);
        }

        private void _TurnNull(Model.InvoiceBS invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "BS";
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

            Model.InvoiceBS invoiceBS = invoice as Model.InvoiceBS;
            if (invoiceBS.Depot == null)
                throw new Helper.RequireValueException("Depot");

            if (invoiceBS.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            foreach (Model.InvoiceBSDetail detail in invoiceBS.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if (detail.InvoiceBSDetailQuantity == 0)
                    throw new Helper.RequireValueException("Details");
            }
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {   
            base._ValidateForInsert(invoice);
            Model.InvoiceBS invoiceBS = invoice as Model.InvoiceBS;
           if (invoiceBS.Depot == null)
               throw new Helper.RequireValueException(Model.InvoiceBS.PROPERTY_DEPOTID);

            if (invoiceBS.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            foreach (Model.InvoiceBSDetail detail in invoiceBS.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if (detail.InvoiceBSDetailQuantity == 0)
                    throw new Helper.RequireValueException("Details");
            }            
        }

        #endregion

        #region Helpers

        private void _TurnNormal(Model.InvoiceBS invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        private void _Insert(Model.InvoiceBS invoice)
        {
            _ValidateForInsert(invoice);            
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            if(invoice.Employee1!=null)
            invoice.Employee1Id = invoice.Employee1.EmployeeId;

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                if(invoice.Employee2!=null)
                invoice.Employee2Id = invoice.Employee2.EmployeeId;
                invoice.InvoiceGZTime = DateTime.Now;
            }
            //invoice.DepotId = invoice.Depot.DepotId;
            accessor.Insert(invoice);

            foreach (Model.InvoiceBSDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                {
                    throw new Exception("商品不為空"); 
                }
                if (detail.DepotPosition == null || detail.DepotPositionId == null)
                    {
                        throw new Exception("貨位不為空"); 
                    }
                detail.InvoiceId = invoice.InvoiceId;                
                invoiceBSDetailAccessor.Insert(detail);
            }

            // 影响
            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                // 库存
                foreach (Model.InvoiceBSDetail detail in invoice.Details)
                {
         
                    //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                    //{
                    //    quantity = detail.InvoiceBSDetailQuantity * detail.Product.ProductInnerUnitRelationship * detail.Product.ProductBaseUnitRelationship;
                    //}
                    //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                    //{
                    //    quantity = detail.InvoiceBSDetailQuantity * detail.Product.ProductBaseUnitRelationship;
                    //}
                    //else
                    //{
                    //    quantity = detail.InvoiceBSDetailQuantity;
                    //}              
                   stockAccessor.Decrement(detail.DepotPosition, detail.Product, detail.InvoiceBSDetailQuantity);
                   productAccessor.UpdateProduct_Stock(detail.Product);
                }
            }
        }

        private void _Update(Model.InvoiceBS invoice)
        {
          
            _ValidateForUpdate(invoice);

            Model.InvoiceBS invoiceOriginal = this.Get(invoice.InvoiceId);

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

                            invoiceBSDetailAccessor.Delete(invoiceOriginal);
                            foreach (Model.InvoiceBSDetail detail in invoice.Details)
                            {
                                detail.InvoiceBSDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceBSDetailAccessor.Insert(detail);
                            }
                            break;

                        case Helper.InvoiceStatus.Normal:

                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            //invoice.UpdateTime = DateTime.Now;
                            //invoice.DepotId = invoice.Depot.DepotId;
                            //invoice.Employee0Id = invoice.Employee0.EmployeeId;
                            //invoice.Employee2Id = invoice.Employee2.EmployeeId;
                            //invoice.InvoiceGZTime = DateTime.Now;
                            //accessor.Update(invoice);

                            //invoiceBSDetailAccessor.Delete(invoiceOriginal);
                            //foreach (Model.InvoiceBSDetail detail in invoice.Details)
                            //{
                            //    detail.InvoiceBSDetailId = Guid.NewGuid().ToString();
                            //    detail.InvoiceId = invoice.InvoiceId;
                            //    invoiceBSDetailAccessor.Insert(detail);
                            //}

                            //// 成本
                            //// 无

                            //// 库存
                            //foreach (Model.InvoiceBSDetail detail in invoice.Details)
                            //{
                            //    double? quantity = 0;
                            //    if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                            //    {
                            //        quantity = detail.InvoiceBSDetailQuantity * detail.Product.ProductInnerUnitRelationship * detail.Product.ProductBaseUnitRelationship;
                            //    }
                            //    else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                            //    {
                            //        quantity = detail.InvoiceBSDetailQuantity * detail.Product.ProductBaseUnitRelationship;
                            //    }
                            //    else
                            //    {
                            //        quantity = detail.InvoiceBSDetailQuantity;
                            //    }

                            //    stockAccessor.Increment(invoice.Depot, detail.Product, quantity);
                            //}

                            // 应收应付
                            // 无

                            break;


                        case Helper.InvoiceStatus.Null:
                            throw new InvalidOperationException();
                    }
                    break;

                case Helper.InvoiceStatus.Normal:

                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            throw new ArgumentException();

                        case Helper.InvoiceStatus.Normal:

                            invoiceOriginal.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
                            _TurnNull(invoiceOriginal);
                            invoiceBSDetailAccessor.Delete(invoiceOriginal);
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);                            
                            break;

                        case Helper.InvoiceStatus.Null:

                            //invoice.UpdateTime = DateTime.Now;
                            //invoice.Employee3Id = invoice.Employee3.EmployeeId;
                            //invoice.InvoiceZFTime = DateTime.Now;
                            //accessor.Update(invoice);

                            //foreach (Model.InvoiceBSDetail detail in invoice.Details)
                            //{
                            //    invoiceBSDetailAccessor.Update(detail);
                            //}

                            // 库存
                            foreach (Model.InvoiceBSDetail detail in invoice.Details)
                            {
                               // double? quantity = 0;

                                //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                                //{
                                //    quantity = detail.InvoiceBSDetailQuantity * detail.Product.ProductInnerUnitRelationship * detail.Product.ProductBaseUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    quantity = detail.InvoiceBSDetailQuantity * detail.Product.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    quantity = detail.InvoiceBSDetailQuantity;
                                //}
                               // productAccessor.UpdateCost1(detail.Product,0,detail.InvoiceBSDetailQuantity);
                                stockAccessor.Increment(detail.DepotPosition, detail.Product,detail.InvoiceBSDetailQuantity);
                                productAccessor.UpdateProduct_Stock(detail.Product);
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