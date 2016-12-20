//------------------------------------------------------------------------------
//
// file name：AcPaymentManager.cs
// author: mayanjun
// create date：2011-6-23 09:29:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcPayment.
    /// </summary>
    public partial class AcPaymentManager : BaseManager
    {
        private static readonly DA.IAcPaymentDetailAccessor acPaymentDetailaccessor = (DA.IAcPaymentDetailAccessor)Accessors.Get("AcPaymentDetailAccessor");
        private static readonly DA.IAcInvoiceCOBillAccessor acInvoiceCOBillaccessor = (DA.IAcInvoiceCOBillAccessor)Accessors.Get("AcInvoiceCOBillAccessor");
        private static readonly DA.IAcOtherShouldPaymentAccessor acOtherShouldPaymentAccessor = (DA.IAcOtherShouldPaymentAccessor)Accessors.Get("AcOtherShouldPaymentAccessor");
        public IList<Model.AcPayment> SelectByDateRange(DateTime starttime, DateTime endtime)
        {
            return accessor.SelectByDateRange(starttime, endtime);
        }

        /// <summary>
        /// Delete AcPayment by primary key.
        /// </summary>
        public void Delete(string acPaymentId)
        {
            accessor.Delete(acPaymentId);
        }

        public void Delete(Model.AcPayment acp)
        {
            try
            {
                BL.V.BeginTransaction();
                calEffect(acp.Detail);
                acPaymentDetailaccessor.DeleteByAcPaymentId(acp.AcPaymentId);
                this.Delete(acp.AcPaymentId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a AcPayment.
        /// </summary>
        public void Insert(Model.AcPayment acPayment)
        {
            Validate(acPayment);
            try
            {
                BL.V.BeginTransaction();
                acPayment.InsertTime = DateTime.Now;
                TiGuiExists(acPayment);
                acPayment.UpdateTime = DateTime.Now;
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, acPayment.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, acPayment.InsertTime.Value.Year, acPayment.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, acPayment.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(acPayment);
                addDetail(acPayment);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        private void Validate(Model.AcPayment acPayment)
        {
            if (string.IsNullOrEmpty(acPayment.SupplierId))
            {
                throw new global::Helper.RequireValueException(Model.AcPayment.PRO_SupplierId);
            }
            if (string.IsNullOrEmpty(acPayment.PayMethodId))
            {
                throw new global::Helper.RequireValueException(Model.AcPayment.PRO_PayMethodId);
            }
            foreach (Model.AcPaymentDetail item in acPayment.Detail)
            {
                if (item.DomesticNoPaymentMoney < item.DomesticThisChargeMoney)
                {
                    throw new global::Helper.InvalidValueException(Model.AcPaymentDetail.PRO_DomesticThisChargeMoney);
                }
            }
        }

        /// <summary>
        /// Update a AcPayment.
        /// </summary>
        public void Update(Model.AcPayment acPayment)
        {
            try
            {
                BL.V.BeginTransaction();
                Validate(acPayment);
                acPayment.UpdateTime = DateTime.Now;
                accessor.Update(acPayment);
                IList<Model.AcPaymentDetail> olddetail = acPaymentDetailaccessor.Select(acPayment);
                calEffect(olddetail);
                acPaymentDetailaccessor.DeleteByAcPaymentId(acPayment.AcPaymentId);
                addDetail(acPayment);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        private void addDetail(Model.AcPayment acPayment)
        {
            foreach (Model.AcPaymentDetail item in acPayment.Detail)
            {
                if (String.IsNullOrEmpty(item.AcInvoiceId)) continue;
                item.AcPaymentId = acPayment.AcPaymentId;
                if (item.AcInvoiceType == "採購發票單")
                {
                    Model.AcInvoiceCOBill cobill = acInvoiceCOBillaccessor.Get(item.AcInvoiceId);
                    if (cobill == null)
                        goto mustdo;
                    cobill.mHeXiaoJingE = cobill.mHeXiaoJingE == null ? 0 : cobill.mHeXiaoJingE + item.DomesticThisChargeMoney;
                    cobill.NoHeXiaoTotal = cobill.ZongMoney - cobill.mHeXiaoJingE;
                    acInvoiceCOBillaccessor.Update(cobill);
                }
                else if (item.AcInvoiceType == "其它應付款")
                {
                    Model.AcOtherShouldPayment acoSP = acOtherShouldPaymentAccessor.Get(item.AcInvoiceId);
                    if (acoSP == null)
                        goto mustdo;
                    acoSP.mHeXiaoJingE = acoSP.mHeXiaoJingE == null ? 0 : acoSP.mHeXiaoJingE + item.DomesticThisChargeMoney;
                    acoSP.NoHeXiaoTotal = acoSP.HeJi - acoSP.mHeXiaoJingE;
                    acOtherShouldPaymentAccessor.Update(acoSP);
                }
            mustdo:
                acPaymentDetailaccessor.Insert(item);
            }
        }

        private void calEffect(IList<Model.AcPaymentDetail> acPaymentDetail)
        {
            foreach (Model.AcPaymentDetail item in acPaymentDetail)
            {
                if (String.IsNullOrEmpty(item.AcInvoiceId)) continue;
                if (item.AcInvoiceType == "採購發票單")
                {
                    Model.AcInvoiceCOBill cobill = acInvoiceCOBillaccessor.Get(item.AcInvoiceId);
                    if (cobill == null)
                        continue;
                    cobill.mHeXiaoJingE = cobill.mHeXiaoJingE == null ? 0 : cobill.mHeXiaoJingE - item.DomesticThisChargeMoney;
                    cobill.NoHeXiaoTotal = cobill.ZongMoney - cobill.mHeXiaoJingE;
                    acInvoiceCOBillaccessor.Update(cobill);
                }
                else if (item.AcInvoiceType == "其它應付款")
                {
                    Model.AcOtherShouldPayment acoSP = acOtherShouldPaymentAccessor.Get(item.AcInvoiceId);
                    if (acoSP == null)
                        continue;
                    acoSP.mHeXiaoJingE = acoSP.mHeXiaoJingE == null ? 0 : acoSP.mHeXiaoJingE - item.DomesticThisChargeMoney;
                    acoSP.NoHeXiaoTotal = acoSP.HeJi - acoSP.mHeXiaoJingE;
                    acOtherShouldPaymentAccessor.Update(acoSP);
                }
            }

        }

        public Model.AcPayment GetDetails(Model.AcPayment acPayment)
        {
            Model.AcPayment temp = accessor.Get(acPayment.AcPaymentId);
            if (temp != null)
                temp.Detail = acPaymentDetailaccessor.Select(temp);
            return temp;
        }

        protected override string GetInvoiceKind()
        {
            return "AcPayment";
        }

        protected override string GetSettingId()
        {
            return "AcPaymentRule";
        }

        private void TiGuiExists(Model.AcPayment model)
        {
            if (this.ExistsPrimary(model.AcPaymentId))
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
                model.AcPaymentId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }
        }

        public void UpdateAcInvoiceXOHeXiao(IList<Model.AcPaymentDetail> detail)
        {
            System.Collections.Hashtable para = new System.Collections.Hashtable();
            foreach (Model.AcPaymentDetail item in detail)
            {
                para.Add(item.AcInvoiceId, item.DomesticThisChargeMoney);
            }
            acInvoiceCOBillaccessor.UpdateHeXiaobyAcinvoiceCOId(para);
        }
    }
}
