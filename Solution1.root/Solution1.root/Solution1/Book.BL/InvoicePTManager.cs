//------------------------------------------------------------------------------
//
// file name：InvoicePTManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoicePT.
    /// </summary>
    public partial class InvoicePTManager : InvoiceManager
    {
        private static readonly DA.IInvoicePTDetailAccessor invoicePTDetailAccessor = (DA.IInvoicePTDetailAccessor)Accessors.Get("InvoicePTDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        //private static readonly DA.ICompanyAccessor companyAccessor = (DA.ICompanyAccessor)Accessors.Get("CompanyAccessor");


        #region Select

        public IList<Model.InvoicePT> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoicePT> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }
        public Model.InvoicePT Get(string invoiceId)
        {
            Model.InvoicePT invoice = accessor.Get(invoiceId);
            invoice.Details = invoicePTDetailAccessor.Select(invoice);
            return invoice;
        }
        #endregion

        #region Override

        #region Operations

        private void Delete(string invoiceId)
        {
            accessor.Delete(invoiceId);
        }

        public void Delete(Book.Model.InvoicePT invoice)
        {
            this._TurnNull(invoice);
            this.Delete(invoice.InvoiceId);
        }

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePT))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoicePT)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePT))
                throw new ArgumentException();

            _Update((Model.InvoicePT)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePT))
                throw new ArgumentException();

            _TurnNormal((Model.InvoicePT)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoicePT))
                throw new ArgumentException();

            _TurnNull((Model.InvoicePT)invoice);
        }

        private void _TurnNull(Model.InvoicePT invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoicePT invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #endregion

        #region Other

        protected override string GetInvoiceKind()
        {
            return "PT";
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

            Validate((Model.InvoicePT)invoice);
        }

        #endregion

        #endregion

        #region Helpers

        private void _Insert(Model.InvoicePT invoice)
        {
            _ValidateForInsert(invoice);

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
            //invoice.Depot0Id = invoice.Depot0.DepotId;
            //invoice.Depot1Id = invoice.Depot1.DepotId;

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
            foreach (Model.InvoicePTDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                detail.InvoiceId = invoice.InvoiceId;
                invoicePTDetailAccessor.Insert(detail);

                //影响库存
                Model.Stock tem = stockAccessor.GetStockByProductIdAndDepotPositionId(detail.ProductId, detail.DepotPositionId);
                if(tem==null)
                    throw new Helper.InvalidValueException(Model.InvoicePTDetail.PROPERTY_DEPOTPOSITIONID);
                if (tem.StockQuantity1 < detail.InvoicePTDetailQuantity.Value)
                    throw new Helper.InvalidValueException(Model.InvoicePTDetail.PROPERTY_DEPOTPOSITIONID);

                stockAccessor.Increment(new DepotPositionManager().Get(detail.DepotPositionInId), detail.Product, detail.InvoicePTDetailQuantity.Value);
                stockAccessor.Decrement(new DepotPositionManager().Get(detail.DepotPositionId), detail.Product, detail.InvoicePTDetailQuantity.Value);
            }

            //if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            //{
            //    //Model.Depot depotOut = invoice.Depot0;
            //    //Model.Depot depotIn = invoice.Depot1;
            //    foreach (Model.InvoicePTDetail detail in invoice.Details)
            //    {
            //        if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
            //        Model.Product p = detail.Product;

            //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
            //{
            //    ptQuantity = detail.InvoicePTDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
            //}
            //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
            //{
            //    ptQuantity = detail.InvoicePTDetailQuantity * p.ProductBaseUnitRelationship;
            //}
            //else
            //{
            //    ptQuantity = detail.InvoicePTDetailQuantity;
            //}

            //stockAccessor.Increment(depotIn, p, ptQuantity.Value);
            //stockAccessor.Decrement(depotOut, p, ptQuantity.Value);
            //    }
            //}
        }

        #region Validate
        /// <summary>
        /// 验证单据编号，经手人，出货和收货库房等
        /// </summary>        
        private void Validate(Model.InvoicePT invoicePT)
        {
            if (string.IsNullOrEmpty(invoicePT.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoicePT.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            //if (invoicePT.Depot0 == null)
            //    throw new Helper.RequireValueException("Depot0");

            //if (invoicePT.Depot1 == null)
            //    throw new Helper.RequireValueException("Depot1");

            if (invoicePT.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            foreach (Model.InvoicePTDetail detail in invoicePT.Details)
            {
                if (detail.InvoicePTDetailQuantity == 0)
                    throw new Helper.RequireValueException("Details");
            }
        }
        #endregion

        private void _Update(Model.InvoicePT invoice)
        {
            _ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;

            Model.InvoicePT invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            invoicePTDetailAccessor.Delete(invoice);

                            foreach (Model.InvoicePTDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoicePTDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoicePTDetailAccessor.Insert(detail);
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
                            this.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:
                            //Model.Depot Out = invoice.Depot0;
                            //Model.Depot In = invoice.Depot1;

                            invoice.Details = invoicePTDetailAccessor.Select(invoice);


                            foreach (Model.InvoicePTDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                Model.Product p = detail.Product;

                                //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                                //{
                                //    ptQuantity = detail.InvoicePTDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    ptQuantity = detail.InvoicePTDetailQuantity * p.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    ptQuantity = detail.InvoicePTDetailQuantity;
                                //}
                                stockAccessor.Decrement(detail.DepotPositionIn, p, detail.InvoicePTDetailQuantity);
                                stockAccessor.Increment(detail.DepotPosition, p, detail.InvoicePTDetailQuantity);
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