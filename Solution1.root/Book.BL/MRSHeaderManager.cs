//------------------------------------------------------------------------------
//
// file name：MRSHeaderManager.cs
// author: peidun
// create date：2009-12-18 11:12:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.MRSHeader.
    /// </summary>
    public partial class MRSHeaderManager : BaseManager
    {
        private static readonly DA.IMRSdetailsAccessor MRSdetailsAccessor = (DA.IMRSdetailsAccessor)Accessors.Get("MRSdetailsAccessor");
        private readonly DA.IProductAccessor ProductAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        byte[] pic = new byte[] { };
        /// <summary>
        /// Delete MRSHeader by primary key.
        /// </summary>
        public void Delete(string mRSHeaderID)
        {
            //
            // todo:add other logic here
            //

            accessor.Delete(mRSHeaderID);

        }
        public void _Delete(Model.MRSHeader mRSHeader)
        {
            this.Delete(mRSHeader.MRSHeaderId);
        }
        public void Delete(Model.MRSHeader mRSHeader)
        {
            //
            // todo:add other logic here
            //
            _ValidateForDelete(mRSHeader);
            try
            {
                BL.V.BeginTransaction();
                this._Delete(mRSHeader);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        public void _ValidateForDelete(Model.MRSHeader mRSHeader)
        {
            switch (mRSHeader.SourceType)
            {
                case "3":
                case "6":
                    if (this.JudgeValueExists(" SELECT  1 FROM ProduceOtherCompact where MRSHeaderId ='" + mRSHeader.MRSHeaderId + "' "))
                    {
                        throw new Helper.ViolateConstraintException("已形成委外單,不能刪除");
                    }
                    break;
                case "1":
                    if (this.JudgeValueExists(" SELECT  1 FROM InvoiceCO where MRSHeaderId ='" + mRSHeader.MRSHeaderId + "'"))
                    {
                        throw new Helper.ViolateConstraintException("已形成採購單,不能刪除");
                    }
                    break;
                case "0":
                case "4":
                case "5":
                    if (this.JudgeValueExists("SELECT 1 FROM PronoteHeader WHERE MRSHeaderId='" + mRSHeader.MRSHeaderId + "'"))
                    {
                        throw new Helper.ViolateConstraintException("已形成加工單,不能刪除");
                    }
                    break;






            }



        }

        public Model.MRSHeader GetDetails(string mRSheaderId)
        {
            Model.MRSHeader mRSheader = accessor.Get(mRSheaderId);
            if (mRSheader != null)
                mRSheader.Details = MRSdetailsAccessor.Select(mRSheader);
            return mRSheader;
        }

        public void Insert(Model.MRSHeader mRSHeader)
        {
            //
            // todo:add other logic here
            //
            Validate(mRSHeader);
            if (this.Exists(mRSHeader.Id))
            {
                throw new Helper.InvalidValueException(Model.MRSHeader.PRO_Id);
            }
            try
            {
                mRSHeader.InsertTime = DateTime.Now;

                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, mRSHeader.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, mRSHeader.InsertTime.Value.Year, mRSHeader.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, mRSHeader.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                if (mRSHeader.Employee0 != null)
                    mRSHeader.Employee0Id = mRSHeader.Employee0.EmployeeId;
                if (mRSHeader.Employee1 != null)
                    mRSHeader.Employee1Id = mRSHeader.Employee1.EmployeeId;
                mRSHeader.InvoiceStatus = 1;









                accessor.Insert(mRSHeader);

                if (mRSHeader.Details != null)
                {
                    foreach (Model.MRSdetails mRSdetails in mRSHeader.Details)
                    {
                        if (string.IsNullOrEmpty(mRSdetails.ProductId))
                            continue;
                        mRSdetails.MRSHeaderId = mRSHeader.MRSHeaderId;
                        MRSdetailsAccessor.Insert(mRSdetails);

                        //Model.Product product =ProductAccessor.Get(mRSdetails.ProductId);
                        //if (product.MRSStockQuantity != null )
                        //{
                        //    product.MRSStockQuantity += mRSdetails.Product.StocksQuantity;
                        //    if (product.ProductImage == null)
                        //        product.ProductImage = pic;
                        //    if (product.ProductImage1 == null)
                        //        product.ProductImage1 = pic;
                        //    if (product.ProductImage2 == null)
                        //        product.ProductImage2 = pic;
                        //    if (product.ProductImage3 == null)
                        //        product.ProductImage3 = pic;

                        //    ProductAccessor.Update(product);
                        //}
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

        /// <summary>
        /// Update a MRSHeader.
        /// </summary>
        public void Update(Model.MRSHeader mRSHeader)
        {
            //
            // todo: add other logic here.
            Validate(mRSHeader);
            //if (this.ExistsExcept(mRSHeader))
            //{
            //    throw new Helper.InvalidValueException(Model.MRSHeader.PRO_Id);
            //}
            // mPSheader.UpdateTime = DateTime.Now;
            // accessor.Update(mPSheader);
            if (mRSHeader != null)
            {
                try
                {
                    BL.V.BeginTransaction();
                    //this.Delete(mRSHeader);
                    mRSHeader.UpdateTime = DateTime.Now;
                    //修改头




                    this.UpdateHeader(mRSHeader);
                    //删除详细
                    MRSdetailsAccessor.DeleteByHeader(mRSHeader);
                    //添加详细
                    foreach (Model.MRSdetails item in mRSHeader.Details)
                    {
                        item.MRSHeaderId = mRSHeader.MRSHeaderId;

                        MRSdetailsAccessor.Insert(item);
                    }




                    BL.V.CommitTransaction();
                }
                catch
                {
                    BL.V.RollbackTransaction();
                    throw;
                }
            }
        }

        public void UpdateHeader(Model.MRSHeader mrsHeader)
        {
            accessor.Update(mrsHeader);
        }

        private void Validate(Model.MRSHeader mRSHeader)
        {
            if (mRSHeader.MRSstartdate == new DateTime() || mRSHeader.MRSstartdate == global::Helper.DateTimeParse.NullDate || mRSHeader.MRSstartdate == null)
                throw new Helper.RequireValueException(Model.MRSHeader.PRO_MRSstartdate);

            bool IsNullOrZero = false;
            if (mRSHeader.Details != null)
            {
                foreach (Model.MRSdetails item in mRSHeader.Details)
                {
                    if (item.MRSdetailssum != null)
                        IsNullOrZero = true;
                    if (mRSHeader.SourceType == "1" || mRSHeader.SourceType == "3")
                    {
                        if (string.IsNullOrEmpty(item.SupplierId))
                            throw new Helper.RequireValueException(Model.MRSdetails.PRO_SupplierId);
                    }
                }
                if (IsNullOrZero == false)
                    throw new Helper.MessageValueException("數量不能為空！");
            }
        }

        protected override string GetSettingId()
        {
            return "mrpRule";
        }

        protected override string GetInvoiceKind()
        {
            return "mrp";
        }

        public IList<Model.MRSHeader> SelectbySourceType(string type)
        {
            return accessor.SelectbySourceType(type);
        }

        public IList<Model.MRSHeader> SelectbyCondition(string mrsstartId, string mrsendId, string customerstartId, string customerendId, DateTime startdate, DateTime enddate, int? sourceType, string id1, string id2, string cusxoid, Model.Product product)
        {
            return accessor.SelectbyCondition(mrsstartId, mrsendId, customerstartId, customerendId, startdate, enddate, sourceType, id1, id2, cusxoid, product);
        }

        public bool SelectIsCloseed(string mrsid)
        {
            return accessor.SelectIsCloseed(mrsid);
        }

        public IList<string> SelectAllProductIdByMRSHeaderId(string MRSHerderId, string handBookProductId)
        {
            return accessor.SelectAllProductIdByMRSHeaderId(MRSHerderId, handBookProductId);
        }

        public IList<string> SelectAllProductIdByInvoiceXOId(string invoiceXOId)
        {
            return accessor.SelectAllProductIdByInvoiceXOId(invoiceXOId);
        }
    }
}

