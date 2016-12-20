//------------------------------------------------------------------------------
//
// file name：InvoiceXTManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceXT.
    /// </summary>
    public partial class InvoiceXTManager : InvoiceManager
    {
        private static readonly DA.IInvoiceXTDetailAccessor invoiceXTDetailAccessor = (DA.IInvoiceXTDetailAccessor)Accessors.Get("InvoiceXTDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        //private static readonly DA.ICompanyAccessor companyAccessor = (DA.ICompanyAccessor)Accessors.Get("CompanyAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IInvoiceXODetailAccessor invoiceXODetailAccessor = (DA.IInvoiceXODetailAccessor)Accessors.Get("InvoiceXODetailAccessor");
        private BL.InvoiceXOManager invoiceXOManager = new InvoiceXOManager();
        private ProductManager productManager = new ProductManager();
        private BL.BGHandbookDetail1Manager bGHandbookDetail1Manager = new BGHandbookDetail1Manager();
        #region Select

        public IList<Model.InvoiceXT> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceXT> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }
        public Model.InvoiceXT Get(string invoiceId)
        {
            Model.InvoiceXT invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceXTDetailAccessor.Select(invoice);
            return invoice;
        }
        #endregion


        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXT))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceXT)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXT))
                throw new ArgumentException();

            _Update((Model.InvoiceXT)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXT))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceXT)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXT))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceXT)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceXT invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceXT invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "XT";
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

            Validate((Model.InvoiceXT)invoice);

        }

        #endregion

        #endregion


        private void _Insert(Model.InvoiceXT invoice)
        {
            _ValidateForInsert(invoice);
            if (invoice.Customer != null)
                invoice.CustomerId = invoice.Customer.CustomerId;
            //invoice.DepotId = invoice.Depot.DepotId;
            invoice.Employee0Id = invoice.Employee0 == null ? null : invoice.Employee0.EmployeeId;

            invoice.Employee1Id = invoice.Employee1.EmployeeId;
            invoice.Employee2Id = invoice.Employee2 == null ? null : invoice.Employee2.EmployeeId;

            InvoiceCost(invoice);

            accessor.Insert(invoice);

            foreach (Model.InvoiceXTDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                detail.InvoiceId = invoice.InvoiceId;
                invoiceXTDetailAccessor.Insert(detail);
                //修改订单
                Model.InvoiceXODetail xodetail = invoiceXODetailAccessor.Get(detail.InvoiceXODetailId);
                if (xodetail != null)
                {
                    xodetail.DetailsFlag = (int)Helper.DetailsFlag.PartArrived;

                    xodetail.InvoiceXTDetailQuantity = xodetail.InvoiceXTDetailQuantity == null ? 0 : xodetail.InvoiceXTDetailQuantity + Convert.ToDouble(detail.InvoiceXTDetailQuantity);

                    double noArr = xodetail.InvoiceXODetailQuantity0.Value;

                    xodetail.InvoiceXODetailQuantity0 = Convert.ToDouble(xodetail.InvoiceXODetailQuantity) - Convert.ToDouble(xodetail.InvoiceXODetailBeenQuantity) + Convert.ToDouble(xodetail.InvoiceXTDetailQuantity);
                    xodetail.InvoiceXODetailQuantity0 = xodetail.InvoiceXODetailQuantity0 < 0 ? 0 : xodetail.InvoiceXODetailQuantity0;

                    //xodetail.InvoiceXODetailBeenQuantity = xodetail.InvoiceXODetailQuantity - xodetail.InvoiceXODetailQuantity0;
                    invoiceXODetailAccessor.Update(xodetail);
                    invoiceXOManager.UpdateInvoiceFlag(xodetail.Invoice);

                    ////修改手册已定未出
                    if (!string.IsNullOrEmpty(xodetail.HandbookProductId) && !string.IsNullOrEmpty(xodetail.HandbookId))
                    {
                        bGHandbookDetail1Manager.UpdateYDWC(xodetail, xodetail.InvoiceXODetailQuantity0.Value - noArr);
                    }
                }

                if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                {
                    bGHandbookDetail1Manager.UpdateBee(detail.HandbookId, detail.HandbookProductId, 0 - detail.InvoiceXTDetailQuantity.Value);//修改手册已出
                }

                detail.DepotPosition = new BL.DepotPositionManager().Get(detail.DepotPositionId);
                stockAccessor.Increment(detail.DepotPosition, detail.Product, detail.InvoiceXTDetailQuantity.Value);
                //更新产品最近出货日期
                this.productManager.UpdateProduct_Stock(detail.Product);
            }

            //if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            //{
            //    //Model.Depot depot = invoice.Depot;

            //    // 增加库存
            //    foreach (Model.InvoiceXTDetail detail in invoice.Details)
            //    {
            //        if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId)) continue;

            //        //转换为基本单位后的数量
            //        //double? xtQuantity = 0;
            //    }                                
            //}
        }

        private void InvoiceCost(Model.InvoiceXT invoice)
        {
            decimal? totalCost = 0;

            foreach (Model.InvoiceXTDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                //detail.InvoiceXTDetailCostPrice = detail.Product.ProductStandardCost;
                detail.InvoiceXTDetailCostMoney = detail.InvoiceXTDetailCostPrice * Convert.ToDecimal(detail.InvoiceXTDetailQuantity.Value);
                totalCost += detail.InvoiceXTDetailCostMoney;
            }
            invoice.InvoiceCost = totalCost;
        }
        private void _Update(Model.InvoiceXT invoice)
        {
            _ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;
            invoice.CustomerId = invoice.Customer.CustomerId;
            //invoice.DepotId = invoice.Depot.DepotId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            invoice.Employee1Id = invoice.Employee1 == null ? null : invoice.Employee1.EmployeeId;
            invoice.Employee2Id = invoice.Employee2 == null ? null : invoice.Employee2.EmployeeId;

            InvoiceCost(invoice);
            Model.InvoiceXT invocieOriginal = this.Get(invoice.InvoiceId);

            switch ((Helper.InvoiceStatus)invocieOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            invoiceXTDetailAccessor.Delete(invocieOriginal);

                            foreach (Model.InvoiceXTDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceXTDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceXTDetailAccessor.Insert(detail);
                            }
                            break;
                        case Helper.InvoiceStatus.Normal:
                            accessor.Delete(invocieOriginal.InvoiceId);
                            invoice.InsertTime = invocieOriginal.InsertTime;
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
                            invocieOriginal.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
                            _TurnNull(invocieOriginal);
                            accessor.Delete(invocieOriginal.InvoiceId);
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:
                            //invoice.Employee3Id = V.ActiveEmployee.EmployeeId;
                            //invoice.InvoiceZFTime = DateTime.Now;
                            //invoice.InvoiceZFCause = "";
                            //accessor.Update(invoice);
                            //Model.Depot depot1 = invoice.Depot;
                            foreach (Model.InvoiceXTDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;

                                if (detail.DepotPosition != null)
                                {
                                    stockAccessor.Decrement(detail.DepotPosition, detail.Product, detail.InvoiceXTDetailQuantity);
                                    productManager.UpdateProduct_Stock(detail.Product);
                                }

                                Model.InvoiceXODetail xodetail = invoiceXODetailAccessor.Get(detail.InvoiceXODetailId);
                                if (xodetail != null)
                                {
                                    xodetail.InvoiceXTDetailQuantity = Convert.ToDouble(xodetail.InvoiceXTDetailQuantity) - Convert.ToDouble(detail.InvoiceXTDetailQuantity);

                                    double noArr = xodetail.InvoiceXODetailQuantity0.Value;


                                    xodetail.InvoiceXODetailQuantity0 = Convert.ToDouble(xodetail.InvoiceXODetailQuantity) - Convert.ToDouble(xodetail.InvoiceXODetailBeenQuantity) + Convert.ToDouble(xodetail.InvoiceXTDetailQuantity);
                                    if (xodetail.InvoiceXODetailQuantity0 < 0)
                                        xodetail.InvoiceXODetailQuantity0 = 0;

                                    if (xodetail.InvoiceXODetailQuantity0 == 0)
                                        xodetail.DetailsFlag = 2;
                                    else if (xodetail.InvoiceXODetailQuantity0 == xodetail.InvoiceXODetailQuantity)
                                        xodetail.DetailsFlag = 0;
                                    else
                                        xodetail.DetailsFlag = 1;
                                    invoiceXODetailAccessor.Update(xodetail);
                                    invoiceXOManager.UpdateInvoiceFlag(xodetail.Invoice);

                                    ////修改手册已定未出
                                    if (!string.IsNullOrEmpty(xodetail.HandbookProductId) && !string.IsNullOrEmpty(xodetail.HandbookId))
                                    {
                                        bGHandbookDetail1Manager.UpdateYDWC(xodetail, xodetail.InvoiceXODetailQuantity0.Value - noArr);
                                    }


                                }
                                if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                                {
                                    bGHandbookDetail1Manager.UpdateBee(detail.HandbookId, detail.HandbookProductId, detail.InvoiceXTDetailQuantity.Value);//修改手册已出
                                }

                            }




                            break;
                    }
                    break;
                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }
        private void Validate(Model.InvoiceXT invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            if (invoice.Customer == null)
                throw new Helper.RequireValueException("Company");

            if (invoice.Depot == null)
                throw new Helper.RequireValueException("Depot");

            if (invoice.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            foreach (Model.InvoiceXTDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if (detail.InvoiceXTDetailQuantity == 0)
                    throw new Helper.RequireValueException("Details");
                if (string.IsNullOrEmpty(detail.DepotPositionId))
                {
                    throw new Helper.RequireValueException(Model.InvoiceCGDetail.PRO_DepotPositionId);
                }
            }
        }

    }
}