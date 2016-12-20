//------------------------------------------------------------------------------
//
// file name：AcInvoiceCOBillManager.cs
// author: mayanjun
// create date：2011-06-27 15:07:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcInvoiceCOBill.
    /// </summary>
    public partial class AcInvoiceCOBillManager : BaseManager
    {

        private static readonly DA.IAcInvoiceCOBillDetailAccessor accessorDetails = (DA.IAcInvoiceCOBillDetailAccessor)Accessors.Get("AcInvoiceCOBillDetailAccessor");
        private static readonly DA.IInvoiceCGDetailAccessor mInvoiceCGDetailAccessor = (DA.IInvoiceCGDetailAccessor)Accessors.Get("InvoiceCGDetailAccessor");
        public IList<Model.AcInvoiceCOBill> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }

        /// <summary>
        /// Delete AcInvoiceCOBill by primary key.
        /// </summary>
        public void Delete(string acInvoiceCOBillId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acInvoiceCOBillId);
        }

        public void Delete(Model.AcInvoiceCOBill acInvoiceCOBill)
        {
            try
            {
                BL.V.BeginTransaction();
                calEffect(acInvoiceCOBill.Details);
                accessorDetails.Delete(acInvoiceCOBill);
                this.Delete(acInvoiceCOBill.AcInvoiceCOBillId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a AcInvoiceCOBill.
        /// </summary>
        public void Insert(Model.AcInvoiceCOBill acInvoiceCOBill)
        {
            Validate(acInvoiceCOBill);

            try
            {
                acInvoiceCOBill.InsertTime = DateTime.Now;
                TiGuiExists(acInvoiceCOBill);


                acInvoiceCOBill.UpdateTime = DateTime.Now;

                BL.V.BeginTransaction();

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, acInvoiceCOBill.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, acInvoiceCOBill.InsertTime.Value.Year, acInvoiceCOBill.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, acInvoiceCOBill.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(acInvoiceCOBill);

                foreach (Model.AcInvoiceCOBillDetail item in acInvoiceCOBill.Details)
                {
                    item.AcInvoiceCOBillId = acInvoiceCOBill.AcInvoiceCOBillId;
                    accessorDetails.Insert(item);
                    //更新进货详细
                    Model.InvoiceCGDetail icgd = mInvoiceCGDetailAccessor.Get(item.InvoiceCGDetailId);
                    if (icgd != null)
                    {
                        icgd.InvoiceCGDetailFPQuantity = Convert.ToDouble(icgd.InvoiceCGDetailFPQuantity) + Convert.ToDouble(item.InvoiceCGDetaiInQuantity);
                    }
                    mInvoiceCGDetailAccessor.Update(icgd);
                }
                BL.V.CommitTransaction();
            }
            finally
            {
                BL.V.RollbackTransaction();
            }
        }

        public void _Update(Model.AcInvoiceCOBill acInvoiceCOBill)
        {
            acInvoiceCOBill.UpdateTime = DateTime.Now;
            accessor.Update(acInvoiceCOBill);
            IList<Model.AcInvoiceCOBillDetail> olddetail = accessorDetails.SelectByAcInvoiceCOBill(acInvoiceCOBill);
            calEffect(olddetail);
            accessorDetails.Delete(acInvoiceCOBill);
            addDetail(acInvoiceCOBill);          
        }
        public void Update(Model.AcInvoiceCOBill acInvoiceCOBill)
        {
            Validate(acInvoiceCOBill);
            try
            {
                BL.V.BeginTransaction();
                _Update(acInvoiceCOBill);               
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        private void Validate(Model.AcInvoiceCOBill acInvoiceCOBill)
        {
            if (string.IsNullOrEmpty(acInvoiceCOBill.Id))
            {
                throw new global::Helper.RequireValueException(Model.AcInvoiceCOBill.PRO_Id);
            }
            if (string.IsNullOrEmpty(acInvoiceCOBill.SupplierId))
            {
                throw new global::Helper.RequireValueException(Model.AcInvoiceCOBill.PRO_SupplierId);
            }
            if (acInvoiceCOBill.Details == null || acInvoiceCOBill.Details.Count == 0)
            {
                throw new global::Helper.RequireValueException("AcInvoiceCOBill.Details");
            }

        }

        public Model.AcInvoiceCOBill GetDetails(Model.AcInvoiceCOBill acInvoiceCoBill)
        {
            Model.AcInvoiceCOBill temp = accessor.Get(acInvoiceCoBill.AcInvoiceCOBillId);
            if (temp != null)
                temp.Details = accessorDetails.SelectByAcInvoiceCOBill(temp);
            return temp;
        }

        private void TiGuiExists(Model.AcInvoiceCOBill model)
        {
            if (this.ExistsPrimary(model.AcInvoiceCOBillId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.InsertTime.Value.Year, model.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.AcInvoiceCOBillId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }
        public DataSet SelectMayShou(Model.Supplier supplier1, Model.Supplier supplier2, Model.Employee employee1, Model.Employee employee2, DateTime startDate, DateTime endDate)
        {
            return accessor.SelectMayShou(supplier1, supplier2, employee1, employee2, startDate, endDate);

        }
        private void addDetail(Model.AcInvoiceCOBill acInvoiceCOBill)
        {
            foreach (Model.AcInvoiceCOBillDetail item in acInvoiceCOBill.Details)
            {
                item.AcInvoiceCOBillId = acInvoiceCOBill.AcInvoiceCOBillId;
                accessorDetails.Insert(item);
                //更新进货详细
                Model.InvoiceCGDetail icgd = mInvoiceCGDetailAccessor.Get(item.InvoiceCGDetailId);
                if (icgd != null)
                {
                    icgd.InvoiceCGDetailFPQuantity = Convert.ToDouble(icgd.InvoiceCGDetailFPQuantity) + Convert.ToDouble(item.InvoiceCGDetaiInQuantity);
                }
                mInvoiceCGDetailAccessor.Update(icgd);
            }
        }
        private void calEffect(IList<Model.AcInvoiceCOBillDetail> detail)
        {
            foreach (Model.AcInvoiceCOBillDetail olddetail in detail)
            {
                Model.InvoiceCGDetail icgd_old = mInvoiceCGDetailAccessor.Get(olddetail.InvoiceCGDetailId);
                if (icgd_old != null)
                {
                    icgd_old.InvoiceCGDetailFPQuantity = Convert.ToDouble(icgd_old.InvoiceCGDetailFPQuantity) - Convert.ToDouble(olddetail.InvoiceCGDetaiInQuantity);
                }
                mInvoiceCGDetailAccessor.Update(icgd_old);
            }
        }
    }
}

