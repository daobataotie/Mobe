//------------------------------------------------------------------------------
//
// file name：AcInvoiceXOBillManager.cs
// author: mayanjun
// create date：2011-09-28 08:45:15
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcInvoiceXOBill.
    /// </summary>
    public partial class AcInvoiceXOBillManager : BaseManager
    {
        private static readonly DA.IAcInvoiceXOBillDetailAccessor accessorDetails = (DA.IAcInvoiceXOBillDetailAccessor)Accessors.Get("AcInvoiceXOBillDetailAccessor");
        private static readonly DA.IInvoiceXSDetailAccessor mInvoiceXSDetailAccessor = (DA.IInvoiceXSDetailAccessor)Accessors.Get("InvoiceXSDetailAccessor");

        public IList<Model.AcInvoiceXOBill> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }

        /// <summary>
        /// Delete AcInvoiceXOBill by primary key.
        /// </summary>
        public void Delete(string acInvoiceXOBillId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acInvoiceXOBillId);
        }

        public void Delete(Model.AcInvoiceXOBill acInvoiceXOBill)
        {
            try
            {
                BL.V.BeginTransaction();

                calEffect(acInvoiceXOBill.Details);
                accessorDetails.Delete(acInvoiceXOBill);

                this.Delete(acInvoiceXOBill.AcInvoiceXOBillId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a AcInvoiceXOBill.
        /// </summary>
        public void Insert(Model.AcInvoiceXOBill acInvoiceXOBill)
        {
            //
            // todo:add other logic here
            //
            Validate(acInvoiceXOBill);
            try
            {
                BL.V.BeginTransaction();
                acInvoiceXOBill.InsertTime = DateTime.Now;
                TiGuiExists(acInvoiceXOBill);
                acInvoiceXOBill.UpdateTime = DateTime.Now;

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, acInvoiceXOBill.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, acInvoiceXOBill.InsertTime.Value.Year, acInvoiceXOBill.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, acInvoiceXOBill.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(acInvoiceXOBill);
                addDetail(acInvoiceXOBill);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a AcInvoiceXOBill.
        /// </summary>
        public void Update(Model.AcInvoiceXOBill acInvoiceXOBill)
        {
            Validate(acInvoiceXOBill);

            acInvoiceXOBill.UpdateTime = DateTime.Now;
            accessor.Update(acInvoiceXOBill);
            IList<Model.AcInvoiceXOBillDetail> olddetail = accessorDetails.SelectByAcInvoiceXOBill(acInvoiceXOBill);
            calEffect(olddetail);
            accessorDetails.Delete(acInvoiceXOBill);
            addDetail(acInvoiceXOBill);
        }

        public Model.AcInvoiceXOBill GetDetails(Model.AcInvoiceXOBill acinvoiceXoBill)
        {
            if (acinvoiceXoBill != null)
                acinvoiceXoBill.Details = accessorDetails.SelectByAcInvoiceXOBill(acinvoiceXoBill);
            return acinvoiceXoBill;
        }

        private void TiGuiExists(Model.AcInvoiceXOBill model)
        {
            if (this.ExistsPrimary(model.AcInvoiceXOBillId))
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
                model.AcInvoiceXOBillId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }
        }

        private void Validate(Model.AcInvoiceXOBill AcInvoiceXOBill)
        {
            if (string.IsNullOrEmpty(AcInvoiceXOBill.Id))
            {
                throw new Helper.RequireValueException(Model.AcInvoiceXOBill.PRO_Id);
            }
            if (string.IsNullOrEmpty(AcInvoiceXOBill.CustomerId))
            {
                throw new Helper.RequireValueException(Model.AcInvoiceXOBill.PRO_CustomerId);
            }
            if (AcInvoiceXOBill.Details == null || AcInvoiceXOBill.Details.Count == 0)
            {
                throw new Helper.RequireValueException("AcInvoiceXOBill.Details");
            }
        }

        protected override string GetSettingId()
        {
            return "AcInvoiceXOBillRule";
        }

        protected override string GetInvoiceKind()
        {
            return "AcInvoiceXOBill";
        }
        public DataSet SelectCuiShou(Model.Customer customer1, Model.Customer customer2, Model.Employee employee1, Model.Employee employee2, DateTime ysdate)
        {
            return accessor.SelectCuiShou(customer1, customer2, employee1, employee2, ysdate);
        }
        public DataSet SelectMayShou(Model.Customer customer1, Model.Customer customer2, Model.Employee employee1, Model.Employee employee2, DateTime startDate, DateTime endDate)
        {
            return accessor.SelectMayShou(customer1, customer2, employee1, employee2, startDate, endDate);

        }
        private void addDetail(Model.AcInvoiceXOBill acInvoiceXOBill)
        {
            foreach (Model.AcInvoiceXOBillDetail Detail in acInvoiceXOBill.Details)
            {
                if (string.IsNullOrEmpty(Detail.InvoiceId)) continue;
                Detail.AcInvoiceXOBillId = acInvoiceXOBill.AcInvoiceXOBillId;
                accessorDetails.Insert(Detail);
                Model.InvoiceXSDetail ixsd = mInvoiceXSDetailAccessor.Get(Detail.InvoiceXODetailId);
                if (ixsd != null)
                {
                    ixsd.InvoiceXSDetailFPQuantity = Convert.ToDouble(ixsd.InvoiceXSDetailFPQuantity) + Convert.ToDouble(Detail.InvoiceXODetaiInQuantity);
                }
                mInvoiceXSDetailAccessor.Update(ixsd);
            }
        }
        private void calEffect(IList<Model.AcInvoiceXOBillDetail> Detail)
        {
            foreach (Model.AcInvoiceXOBillDetail detail in Detail)
            {
                Model.InvoiceXSDetail ixsd = mInvoiceXSDetailAccessor.Get(detail.InvoiceXODetailId);
                if (ixsd != null)
                {
                    ixsd.InvoiceXSDetailFPQuantity = Convert.ToDouble(ixsd.InvoiceXSDetailFPQuantity) - Convert.ToDouble(detail.InvoiceXODetaiInQuantity);
                }
                mInvoiceXSDetailAccessor.Update(ixsd);
            }
        }
    }
}


#region 注释备用
//---from --public void Insert(Model.AcInvoiceXOBill acInvoiceXOBill)--//

//foreach (Model.AcInvoiceXOBillDetail acInvoiceXOBillDetail in acInvoiceXOBill.Details)
//{
//    if (produceInDepotDetail.Product == null || string.IsNullOrEmpty(produceInDepotDetail.Product.ProductId)) continue;
//    produceInDepotDetail.ProduceInDepotId = produceInDepot.ProduceInDepotId;
//    //if (produceInDepotDetail.ProductProce != null)
//    //    produceInDepotDetail.ProductProceId = produceInDepotDetail.ProductProce.ProductId;
//    double? proceduresSum = ProduceInDepotDetailAccessor.select_SumbyPronHeaderId(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId);
//    double? checkOutSum = ProduceInDepotDetailAccessor.select_CheckOutSumByPronHeaderId(produceInDepotDetail.PronoteHeaderId, produceInDepot.WorkHouseId);
//    produceInDepotDetail.HeJiProceduresSum = proceduresSum == null ? 0 : proceduresSum + produceInDepotDetail.ProceduresSum;
//    produceInDepotDetail.HeJiCheckOutSum = checkOutSum == null ? 0 : checkOutSum + produceInDepotDetail.CheckOutSum;
//    ProduceInDepotDetailAccessor.Insert(produceInDepotDetail);
//}
#endregion