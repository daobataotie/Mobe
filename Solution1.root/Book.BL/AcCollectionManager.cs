//------------------------------------------------------------------------------
//
// file name：AcCollectionManager.cs
// author: mayanjun
// create date：2011-6-23 09:29:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcCollection.
    /// </summary>
    public partial class AcCollectionManager : BaseManager
    {

        private static readonly DA.IAcCollectionDetailAccessor acCollectionDetailaccessor = (DA.IAcCollectionDetailAccessor)Accessors.Get("AcCollectionDetailAccessor");
        private static readonly DA.IAcInvoiceXOBillAccessor acInvoiceXOBillaccessor = (DA.IAcInvoiceXOBillAccessor)Accessors.Get("AcInvoiceXOBillAccessor");
        private static readonly DA.IAcOtherShouldCollectionAccessor acOtherShouldCollectionAccessor = (DA.IAcOtherShouldCollectionAccessor)Accessors.Get("AcOtherShouldCollectionAccessor");
        /// <summary>
        /// Delete AcCollection by primary key.
        /// </summary>
        public void Delete(string acCollectionId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acCollectionId);
        }

        public void Delete(Model.AcCollection acc)
        {
            try
            {
                BL.V.BeginTransaction();
                calEffect(acc.Detail);
                acCollectionDetailaccessor.DeleteByAccid(acc.AcCollectionId);
                this.Delete(acc.AcCollectionId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        private void addDetail(Model.AcCollection acCollection)
        {
            foreach (Model.AcCollectionDetail item in acCollection.Detail)
            {
                if (string.IsNullOrEmpty(item.AcInvoiceId)) continue;
                item.AcCollectionId = acCollection.AcCollectionId;
                if (item.AcInvoiceType == "銷售發票單")
                {
                    Model.AcInvoiceXOBill xobill = acInvoiceXOBillaccessor.Get(item.AcInvoiceId);
                    if (xobill == null)
                        goto mustdo;
                    xobill.mHeXiaoJingE = xobill.mHeXiaoJingE == null ? 0 : xobill.mHeXiaoJingE + item.DomesticThisChargeMoney;
                    xobill.NoHeXiaoTotal = xobill.ZongMoney - xobill.mHeXiaoJingE;
                    acInvoiceXOBillaccessor.Update(xobill);
                }
                else if (item.AcInvoiceType == "其它應收款")
                {
                    Model.AcOtherShouldCollection acoSc = acOtherShouldCollectionAccessor.Get(item.AcInvoiceId);
                    if (acoSc == null)
                        goto mustdo;
                    acoSc.mHeXiaoJingE = acoSc.mHeXiaoJingE == null ? 0 : acoSc.mHeXiaoJingE + item.DomesticThisChargeMoney;
                    acoSc.NoHeXiaoTotal = acoSc.HeJi - acoSc.mHeXiaoJingE;
                    acOtherShouldCollectionAccessor.Update(acoSc);
                }
            mustdo:
                acCollectionDetailaccessor.Insert(item);
            }
        }
        private void calEffect(IList<Model.AcCollectionDetail> detail)
        {
            foreach (Model.AcCollectionDetail item in detail)
            {
                if (string.IsNullOrEmpty(item.AcInvoiceId)) continue;
                if (item.AcInvoiceType == "銷售發票單")
                {
                    Model.AcInvoiceXOBill xobill = acInvoiceXOBillaccessor.Get(item.AcInvoiceId);
                    if (xobill == null)
                        continue;
                    xobill.mHeXiaoJingE = xobill.mHeXiaoJingE == null ? 0 : xobill.mHeXiaoJingE - item.DomesticThisChargeMoney;
                    xobill.NoHeXiaoTotal = xobill.ZongMoney - xobill.mHeXiaoJingE;
                    acInvoiceXOBillaccessor.Update(xobill);
                }
                else if (item.AcInvoiceType == "其它應收款")
                {
                    Model.AcOtherShouldCollection acoSc = acOtherShouldCollectionAccessor.Get(item.AcInvoiceId);
                    if (acoSc == null)
                        continue;
                    acoSc.mHeXiaoJingE = acoSc.mHeXiaoJingE == null ? 0 : acoSc.mHeXiaoJingE - item.DomesticThisChargeMoney;
                    acoSc.NoHeXiaoTotal = acoSc.HeJi - acoSc.mHeXiaoJingE;
                    acOtherShouldCollectionAccessor.Update(acoSc);
                }
            }
        }
        public Model.AcCollection GetDetails(Model.AcCollection acCollection)
        {
            Model.AcCollection temp = accessor.Get(acCollection.AcCollectionId);
            if (temp != null)
                temp.Detail = acCollectionDetailaccessor.Select(temp);
            return temp;
        }

        /// <summary>
        /// Insert a AcCollection.
        /// </summary>
        public void Insert(Model.AcCollection acCollection)
        {
            Validate(acCollection);
            try
            {
                BL.V.BeginTransaction();
                acCollection.InsertTime = DateTime.Now;
                TiGuiExists(acCollection);


                acCollection.UpdateTime = DateTime.Now;

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, acCollection.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, acCollection.InsertTime.Value.Year, acCollection.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, acCollection.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(acCollection);
                addDetail(acCollection);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a AcCollection.
        /// </summary>
        public void Update(Model.AcCollection acCollection)
        {
            try
            {
                BL.V.BeginTransaction();
                Validate(acCollection);
                acCollection.UpdateTime = DateTime.Now;
                accessor.Update(acCollection);
                IList<Model.AcCollectionDetail> olddetail = acCollectionDetailaccessor.Select(acCollection);
                calEffect(olddetail);
                acCollectionDetailaccessor.DeleteByAccid(acCollection.AcCollectionId);
                addDetail(acCollection);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        protected override string GetInvoiceKind()
        {
            return "AcCollection";
        }

        protected override string GetSettingId()
        {
            return "AcCollectionRule";
        }

        private void TiGuiExists(Model.AcCollection model)
        {
            if (this.ExistsPrimary(model.AcCollectionId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.AcPaymentDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.AcPaymentDate.Value.Year, model.AcPaymentDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.AcPaymentDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.AcCollectionId = this.GetId(model.AcPaymentDate.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }
        }

        private void Validate(Model.AcCollection acc)
        {
            //if (string.IsNullOrEmpty(acc.PayMethodId))
            //{
            //    throw new global::Helper.RequireValueException(Model.AcCollection.PRO_PayMethodId);
            //}
            if (string.IsNullOrEmpty(acc.CustomerId))
            {
                throw new global::Helper.RequireValueException(Model.AcCollection.PRO_CustomerId);
            }
            if (string.IsNullOrEmpty(acc.Employee1Id))
            {
                throw new global::Helper.RequireValueException(Model.AcCollection.PRO_Employee1Id);
            }
            foreach (Model.AcCollectionDetail item in acc.Detail)
            {
                if (item.DomesticNoPaymentMoney < item.DomesticThisChargeMoney)
                {
                    throw new global::Helper.InvalidValueException(Model.AcCollectionDetail.PRO_DomesticThisChargeMoney);
                }
            }
        }

        //修改销售发票中核销数据
        public void UpdateAcInvoiceXOHeXiao(IList<Model.AcCollectionDetail> accDetailList)
        {
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            foreach (Model.AcCollectionDetail item in accDetailList)
            {
                ht.Add(item.AcInvoiceId, item.DomesticThisChargeMoney);
            }
            acInvoiceXOBillaccessor.UpdateHeXiaoByAcInvoiceXOBillId(ht);
        }
    }
}

