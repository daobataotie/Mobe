//------------------------------------------------------------------------------
//
// file name：PCInputCheckManager.cs
// author: mayanjun
// create date：2015/4/18 上午 11:58:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCInputCheck.
    /// </summary>
    public partial class PCInputCheckManager : BaseManager
    {

        /// <summary>
        /// Delete PCInputCheck by primary key.
        /// </summary>
        public void Delete(string pCInputCheckId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(pCInputCheckId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a PCInputCheck.
        /// </summary>
        public void Insert(Model.PCInputCheck pCInputCheck)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(pCInputCheck);
                this.TiGuiExists(pCInputCheck);
                if (this.ExistsLotNumberInsert(pCInputCheck.LotNumber, pCInputCheck.ProductId))
                    throw new Helper.MessageValueException("商品：" + pCInputCheck.Product + " 已有相同批號");

                pCInputCheck.InsertTime = DateTime.Now;
                pCInputCheck.UpdateTime = DateTime.Now;

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCInputCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCInputCheck.InsertTime.Value.Year, pCInputCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCInputCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                //有时保存后选择数据清空
                if (string.IsNullOrEmpty(pCInputCheck.Heidian))
                    pCInputCheck.Heidian = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Guohuo))
                    pCInputCheck.Guohuo = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Liaodian))
                    pCInputCheck.Liaodian = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Wasiqi))
                    pCInputCheck.Wasiqi = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Zazhi))
                    pCInputCheck.Zazhi = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Qipao))
                    pCInputCheck.Qipao = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Duise))
                    pCInputCheck.Duise = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Chongji))
                    pCInputCheck.Chongji = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Nairanceshi))
                    pCInputCheck.Nairanceshi = "0";
                if (string.IsNullOrEmpty(pCInputCheck.UVvalue))
                    pCInputCheck.UVvalue = "0";

                accessor.Insert(pCInputCheck);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a PCInputCheck.
        /// </summary>
        public void Update(Model.PCInputCheck pCInputCheck)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(pCInputCheck);
                if (this.ExistsLotNumberUpdate(pCInputCheck.LotNumber, pCInputCheck.PCInputCheckId, pCInputCheck.ProductId))
                    throw new Helper.MessageValueException("商品:" + pCInputCheck.Product + " 已有相同批號");
                pCInputCheck.UpdateTime = DateTime.Now;

                //有时保存后选择数据清空
                if (string.IsNullOrEmpty(pCInputCheck.Heidian))
                    pCInputCheck.Heidian = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Guohuo))
                    pCInputCheck.Guohuo = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Liaodian))
                    pCInputCheck.Liaodian = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Wasiqi))
                    pCInputCheck.Wasiqi = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Zazhi))
                    pCInputCheck.Zazhi = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Qipao))
                    pCInputCheck.Qipao = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Duise))
                    pCInputCheck.Duise = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Chongji))
                    pCInputCheck.Chongji = "0";
                if (string.IsNullOrEmpty(pCInputCheck.Nairanceshi))
                    pCInputCheck.Nairanceshi = "0";
                if (string.IsNullOrEmpty(pCInputCheck.UVvalue))
                    pCInputCheck.UVvalue = "0";

                accessor.Update(pCInputCheck);
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
            return "PIC";
        }

        protected override string GetSettingId()
        {
            return "PCInputCheck";
        }
        public void Validate(Model.PCInputCheck model)
        {
            if (model.PCInputCheckDate == null)
                throw new Helper.InvalidValueException(Model.PCInputCheck.PRO_PCInputCheckDate);

            if (string.IsNullOrEmpty(model.LotNumber))
                throw new Helper.MessageValueException("批號不能為空");
        }

        private void TiGuiExists(Model.PCInputCheck model)
        {
            if (this.ExistsPrimary(model.PCInputCheckId))
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
                model.PCInputCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public IList<Model.PCInputCheck> SelectByCondition(DateTime startdate, DateTime enddate, string productid, string testProductid, string supplierid, string lotnumber, bool IsClosed)
        {
            return accessor.SelectByCondition(startdate, enddate, productid, testProductid, supplierid, lotnumber, IsClosed);
        }

        public IList<Model.PCInputCheck> SelectByInvoiceCusId(string invoiceCusId)
        {
            return accessor.SelectByInvoiceCusId(invoiceCusId);
        }

        public bool ExistsLotNumberInsert(string lotNumber, string ProductId)
        {
            return accessor.ExistsLotNumberInsert(lotNumber, ProductId);
        }

        public bool ExistsLotNumberUpdate(string lotNumber, string PCInputCheckId, string ProductId)
        {
            return accessor.ExistsLotNumberUpdate(lotNumber, PCInputCheckId, ProductId);
        }

        public void UpdateIsClosed(Model.PCInputCheck model)
        {
            accessor.UpdateIsClosed(model);
        }
    }
}
