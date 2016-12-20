//------------------------------------------------------------------------------
//
// file name：MPSheaderManager.cs
// author: peidun
// create date：2009-12-18 11:12:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Book.Model;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.MPSheader.
    /// </summary>
    public partial class MPSheaderManager : BaseManager
    {
        private DA.IInvoiceXODetailAccessor InvoiceXODetailAccessor = (DA.IInvoiceXODetailAccessor)Accessors.Get("InvoiceXODetailAccessor");
        private DA.IInvoiceXOAccessor InvoiceXOAccessor = (DA.IInvoiceXOAccessor)Accessors.Get("InvoiceXOAccessor");
        private readonly DA.IMPSdetailsAccessor MPSdetailsAccessor = (DA.IMPSdetailsAccessor)Accessors.Get("MPSdetailsAccessor");
        private InvoiceXOManager invoiceXOManager = new InvoiceXOManager();
        private InvoiceXODetailManager invoiceManager = new InvoiceXODetailManager();
        private InvoiceXODetail invoiceXODetail = new InvoiceXODetail();
        private ProductManager productManager = new ProductManager();

        /// <summary>
        /// Delete MPSheader by primary key.
        /// </summary>
        public void Delete(string mPSheaderId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(mPSheaderId);
        }

        public void Delete(Model.MPSheader mPSheader)
        {
            try
            {
                BL.V.BeginTransaction();

                foreach (Model.MPSdetails mPSdetails in mPSheader.Details)
                {
                    Model.InvoiceXODetail xodetail = this.InvoiceXODetailAccessor.Get(mPSdetails.InvoiceXODetailId);
                    if (xodetail != null)
                    {
                        //xodetail.InvoiceMPSQuantity = xodetail.InvoiceMPSQuantity - Convert.ToDouble(mPSdetails.MPSdetailssum);
                        //invoiceManager.Update(xodetail);
                        if (xodetail != null)
                        {
                            if (xodetail.InvoiceMPSQuantity == null)
                                xodetail.InvoiceMPSQuantity = 0;
                            xodetail.InvoiceMPSQuantity = xodetail.InvoiceMPSQuantity- mPSdetails.MPSdetailssum;
                            if (xodetail.InvoiceMPSQuantity >= xodetail.InvoiceXODetailQuantity)
                            {
                                xodetail.DetailMPSState = 2;
                            }
                            else
                            {
                                if (xodetail.InvoiceMPSQuantity > 0)
                                {
                                    xodetail.DetailMPSState = 1;
                                }
                                else
                                {
                                    xodetail.DetailMPSState = 0;
                                }
                            }
                            //invoiceXODetail.InvoiceMPSQuantity = Convert.ToDouble(MPSdetailsAccessor.GetByInvoiceXODetailId(mPSdetails.InvoiceXODetailId));
                            invoiceManager.Update(xodetail);
                            UpdateInvoiceXOFlag(xodetail.Invoice);
                        }
                    }
                }

                accessor.Delete(mPSheader.MPSheaderId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        public Model.MPSheader GetDetails(string mPSheaderId)
        {
            Model.MPSheader mPSheader = accessor.Get(mPSheaderId);
            if (mPSheader != null)
                mPSheader.Details = MPSdetailsAccessor.Select(mPSheader);
            return mPSheader;
        }

        /// <summary>
        /// Insert a MPSheader.
        /// </summary>
        public void Insert(Model.MPSheader mPSheader)
        {
            //
            // todo:add other logic here
            //
            Validate(mPSheader);
            if (this.Exists(mPSheader.Id))
            {
                throw new Helper.InvalidValueException(Model.MPSheader.PRO_Id);
            }

            try
            {
                BL.V.BeginTransaction();
                mPSheader.InsertTime = DateTime.Now;
                mPSheader.UpdateTime = DateTime.Now;
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, mPSheader.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, mPSheader.InsertTime.Value.Year, mPSheader.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, mPSheader.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                if (mPSheader.Details!=null&&mPSheader.Details.Count > 0)
                mPSheader.InvoiceXOId = mPSheader.Details[0].InvoiceXOId;
                accessor.Insert(mPSheader);

                foreach (Model.MPSdetails mPSdetails in mPSheader.Details)
                {
                    if (mPSdetails.Product == null || string.IsNullOrEmpty(mPSdetails.Product.ProductId)) continue;
                    mPSdetails.MPSheaderId = mPSheader.MPSheaderId;
                    mPSdetails.MPSEndState = false;
                    invoiceXODetail = invoiceManager.Get(mPSdetails.InvoiceXODetailId);
                    double ss = MPSdetailsAccessor.GetByInvoiceXODetailId(mPSdetails.InvoiceXODetailId);
                    MPSdetailsAccessor.Insert(mPSdetails);

                    if (invoiceXODetail != null)
                    {
                        if (invoiceXODetail.InvoiceMPSQuantity == null)
                            invoiceXODetail.InvoiceMPSQuantity = 0;
                        invoiceXODetail.InvoiceMPSQuantity += mPSdetails.MPSdetailssum;
                        if (invoiceXODetail.InvoiceMPSQuantity >= invoiceXODetail.InvoiceXODetailQuantity)
                        {
                            invoiceXODetail.DetailMPSState = 2;
                        }
                        else
                        {
                            if (invoiceXODetail.InvoiceMPSQuantity > 0)
                            {
                                invoiceXODetail.DetailMPSState = 1;
                            }
                            else
                            {
                                invoiceXODetail.DetailMPSState = 0;
                            }
                        }
                        //invoiceXODetail.InvoiceMPSQuantity = Convert.ToDouble(MPSdetailsAccessor.GetByInvoiceXODetailId(mPSdetails.InvoiceXODetailId));
                        Model.Product product = productManager.Get(mPSdetails.ProductId);
                        product.MpsStockQuantity = mPSdetails.Product.StocksQuantity;
                        invoiceManager.Update(invoiceXODetail);
                        UpdateInvoiceXOFlag(invoiceXODetail.Invoice);
                    }
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        public void UpdateInvoiceXOFlag(Model.InvoiceXO invoice)
        {
            int flag = 0;
            IList<Model.InvoiceXODetail> list = invoiceManager.Select(invoice);
            foreach (Model.InvoiceXODetail detail in list)
            {
                flag += detail.DetailMPSState == null ? 0 : detail.DetailMPSState.Value;
            }
            if (flag == 0)
                invoice.InvoiceMPSState = 0;
            else if (flag < list.Count * 2)
                invoice.InvoiceMPSState = 1;
            else if (flag == list.Count * 2)
                invoice.InvoiceMPSState = 2;
            InvoiceXOAccessor.Update(invoice);
        }
        /// <summary>
        /// Update a MPSheader.
        /// </summary>
        public void Update(Model.MPSheader mPSheader)
        {
            //
            // todo: add other logic here.
            //
            Validate(mPSheader);
            if (this.ExistsExcept(mPSheader))
            {
                throw new Helper.InvalidValueException(Model.MPSheader.PRO_Id);
            }
            // mPSheader.UpdateTime = DateTime.Now;
            // accessor.Update(mPSheader);
            if (mPSheader != null)
            {
                this.Delete(mPSheader);
                mPSheader.UpdateTime = DateTime.Now;
                this.Insert(mPSheader);
            }

        }
        /// <summary>
        /// Update a MPSheader.
        /// </summary>
        public void UpdateMPS(Model.MPSheader mPSheader)
        {
            //
            // todo: add other logic here.
            //
            Validate(mPSheader);
            if (this.ExistsExcept(mPSheader))
            {
                throw new Helper.InvalidValueException(Model.MPSheader.PRO_Id);
            }
            mPSheader.UpdateTime = DateTime.Now;
            accessor.Update(mPSheader);

        }
        private void Validate(Model.MPSheader mPSheader)
        {
            if (string.IsNullOrEmpty(mPSheader.Id))
            {
                throw new Helper.RequireValueException(Model.MPSheader.PRO_Id);
            }
            if (mPSheader.MPSStartDate == null)
            {
                throw new Helper.RequireValueException(Model.MPSheader.PRO_MPSStartDate);
            }
            if (mPSheader.MPSEndDate == null)
            {
                throw new Helper.RequireValueException(Model.MPSheader.PRO_MPSEndDate);
            }
            bool IsNullOrZero = false;
            foreach (var item in mPSheader.Details)
            {
                if (item.MPSdetailssum != null && item.MPSdetailssum.Value > 0)
                    IsNullOrZero = true;
            }

            if (IsNullOrZero == false)
                throw new Helper.MessageValueException("數量不能為空或者零！");

        }
        public IList<Book.Model.MPSheader> SelectById(string mPSheaderId)
        {
            return accessor.SelectById(mPSheaderId);
        }
        public IList<Book.Model.MPSheader> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }
        protected override string GetSettingId()
        {
            return "mpsRule";
        }
        protected override string GetInvoiceKind()
        {
            return "mps";
        }
    }
}

