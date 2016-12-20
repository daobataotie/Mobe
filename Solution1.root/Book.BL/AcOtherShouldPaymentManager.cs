//------------------------------------------------------------------------------
//
// file name：AcOtherShouldPaymentManager.cs
// author: mayanjun
// create date：2011-6-10 10:11:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcOtherShouldPayment.
    /// </summary>
    public partial class AcOtherShouldPaymentManager : BaseManager
    {
        private static readonly DA.IAcOtherShouldPaymentDetailAccessor AcOtherShouldPaymentDetailAccessor = (DA.IAcOtherShouldPaymentDetailAccessor)Accessors.Get("AcOtherShouldPaymentDetailAccessor");
        private static readonly DA.IAcItemAccessor AcItemaccessor = (DA.IAcItemAccessor)Accessors.Get("AcItemAccessor");

        /// <summary>
        /// Delete AcOtherShouldPayment by primary key.
        /// </summary>
        public void Delete(string acOtherShouldPaymentId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acOtherShouldPaymentId);
        }

        public void Delete(Model.AcOtherShouldPayment AcOtherShouldPayment)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(AcOtherShouldPayment.AcOtherShouldPaymentId);
        }

        public Model.AcOtherShouldPayment GetDetails(string AcOtherShouldPaymentId)
        {
            Model.AcOtherShouldPayment AcOtherShouldPayment = accessor.Get(AcOtherShouldPaymentId);
            if (AcOtherShouldPayment != null)
                AcOtherShouldPayment.Details = AcOtherShouldPaymentDetailAccessor.Select(AcOtherShouldPayment);
            return AcOtherShouldPayment;
        }

        public void Insert(Model.AcOtherShouldPayment acOtherShouldPayment)
        {
            //
            // todo:add other logic here
            //
            Validate(acOtherShouldPayment);

            //获取数据库原有
            IList<Model.AcItem> dbAClist = AcItemaccessor.Select();

            try
            {
                acOtherShouldPayment.InsertTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, acOtherShouldPayment.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, acOtherShouldPayment.InsertTime.Value.Year, acOtherShouldPayment.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, acOtherShouldPayment.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(acOtherShouldPayment);

                var Qgroup = from Model.AcOtherShouldPaymentDetail acspd in acOtherShouldPayment.Details
                             group acspd by acspd.LoanName;

                foreach (IGrouping<string, Model.AcOtherShouldPaymentDetail> item in Qgroup)
                {
                    if (!string.IsNullOrEmpty(item.Last<Model.AcOtherShouldPaymentDetail>().AcItemId))
                    {
                        Model.AcItem acitem = (from Model.AcItem ac in dbAClist
                                               where ac.AcItemId == item.Last().AcItemId
                                               select ac).First<Model.AcItem>();
                        acitem.ItemPrice = item.Last<Model.AcOtherShouldPaymentDetail>().AcItemPrice;
                        AcItemaccessor.Update(acitem);
                        continue;
                    }

                    if (dbAClist == null || dbAClist.Count == 0)
                    {
                        Model.AcItem acitem = new Book.Model.AcItem();
                        acitem.AcItemId = Guid.NewGuid().ToString();
                        acitem.AcItemDate = DateTime.Now;
                        acitem.ItemName = item.Last().LoanName;
                        acitem.ItemPrice = item.Last().AcItemPrice;
                        AcItemaccessor.Insert(acitem);
                    }
                    else
                    {
                        IList<Model.AcItem> actQ = (from Model.AcItem ac in dbAClist
                                                    where ac.ItemName == item.Last().LoanName
                                                    select ac).ToList();

                        Model.AcItem act = null;
                        if (actQ != null && actQ.Count != 0)
                        {
                            act = actQ.First<Model.AcItem>();
                        }

                        if (act != null)
                        {
                            if (act.ItemPrice != item.Last().AcItemPrice)
                            {
                                act.ItemPrice = item.Last().AcItemPrice;
                                AcItemaccessor.Update(act);
                            }
                        }
                        else
                        {
                            Model.AcItem acitem = new Book.Model.AcItem();
                            acitem.AcItemId = Guid.NewGuid().ToString();
                            acitem.AcItemDate = DateTime.Now;
                            acitem.ItemName = item.Last().LoanName;
                            acitem.ItemPrice = item.Last().AcItemPrice;
                            AcItemaccessor.Insert(acitem);
                        }
                    }
                }

                foreach (Model.AcOtherShouldPaymentDetail acOtherShouldPaymentDetail in acOtherShouldPayment.Details)
                {
                    acOtherShouldPaymentDetail.AcOtherShouldPaymentDetailId = Guid.NewGuid().ToString();
                    acOtherShouldPaymentDetail.AcOtherShouldPaymentId = acOtherShouldPayment.AcOtherShouldPaymentId;

                    AcOtherShouldPaymentDetailAccessor.Insert(acOtherShouldPaymentDetail);
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.AcOtherShouldPayment acOtherShouldPayment)
        {
            //
            // todo: add other logic here.
            //
            Validate(acOtherShouldPayment);
            if (acOtherShouldPayment != null)
            {
                this.Delete(acOtherShouldPayment);
                acOtherShouldPayment.UpdateTime = DateTime.Now;
                this.Insert(acOtherShouldPayment);
            }
        }

        protected override string GetSettingId()
        {
            return "aospRule";
        }

        protected override string GetInvoiceKind()
        {
            return "aosp";
        }

        private void Validate(Model.AcOtherShouldPayment acOtherShouldPayment)
        {
            if (string.IsNullOrEmpty(acOtherShouldPayment.AcOtherShouldPaymentId))
            {
                throw new Helper.RequireValueException(Model.AcOtherShouldPayment.PRO_AcOtherShouldPaymentId);
            }
            if (acOtherShouldPayment.InvoiceTax + acOtherShouldPayment.InvoiceHeji != acOtherShouldPayment.HeJi)
            {
                throw new Helper.InvalidValueException(Model.AcOtherShouldPayment.PRO_InvoiceHeji);
            }
        }

        public IList<Model.AcOtherShouldPayment> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }

        public IList<Model.AcOtherShouldPayment> SelectByDateRangeAndSupCompany(DateTime startdate, DateTime enddate, Model.Supplier supplier, Model.Company company)
        {

            return accessor.SelectByDateRangeAndSupCompany(startdate, enddate, supplier, company);
        }

        public string UpdateAcOtherShouldPaymentList(IList<Model.AcOtherShouldPayment> list)
        {
            try
            {
                BL.V.BeginTransaction();

                foreach (Model.AcOtherShouldPayment item in list)
                {
                    accessor.Update(item);
                }

                BL.V.CommitTransaction();

                return "保存成功";
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                return ex.Message;
            }
        }
    }
}

