//------------------------------------------------------------------------------
//
// file name：BGProductDepotOutManager.cs
// author: mayanjun
// create date：2014/3/25 18:18:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGProductDepotOut.
    /// </summary>
    public partial class BGProductDepotOutManager : BaseManager
    {
        private static readonly DA.IBGProductDepotOutDetailAccessor detailaccessor = (DA.IBGProductDepotOutDetailAccessor)Accessors.Get("BGProductDepotOutDetailAccessor");
        BL.BGHandbookDetail1Manager bGHandbookDetail1Manager = new BGHandbookDetail1Manager();

        /// <summary>
        /// Delete BGProductDepotOut by primary key.
        /// </summary>
        public void Delete(string bGProductDepotOutId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.Delete(this.Select(bGProductDepotOutId));
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.BGProductDepotOut model)
        {
            Model.BGHandbookDetail1 detail = new Book.Model.BGHandbookDetail1();
            foreach (var item in model.Detail)
            {
                detail = this.bGHandbookDetail1Manager.SelectBGProduct(item.BGHandbookId, item.BGHandbookProductId);
                if (detail != null)
                {
                    if (detail.BeeQuantity == null)
                        detail.BeeQuantity = 0;
                    detail.BeeQuantity -= Convert.ToDouble(item.Quantity);
                    this.bGHandbookDetail1Manager.Update(detail);

                    this.bGHandbookDetail1Manager.UpdateUpQuantity(detail);
                }
                detailaccessor.Delete(item.BGProductDepotOutDetailId);
            }
            accessor.Delete(model.BGProductDepotOutId);
        }

        /// <summary>
        /// Insert a BGProductDepotOut.
        /// </summary>
        public void Insert(Model.BGProductDepotOut bGProductDepotOut)
        {
            //
            // todo:add other logic here
            //
            Validate(bGProductDepotOut);
            if (this.IsExistsDeclareCustomsIdInsert(bGProductDepotOut.DeclareCustomsId))
                throw new Helper.MessageValueException("已存在相同报关单号！");
            try
            {
                BL.V.BeginTransaction();
                this.TiGuiExists(bGProductDepotOut);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, DateTime.Now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, DateTime.Now.Year, DateTime.Now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, DateTime.Now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                bGProductDepotOut.InsertTime = DateTime.Now;
                bGProductDepotOut.UpdateTime = DateTime.Now;
                accessor.Insert(bGProductDepotOut);

                //修改手册成品
                Model.BGHandbookDetail1 detail;
                foreach (var item in bGProductDepotOut.Detail)
                {
                    detail = this.bGHandbookDetail1Manager.SelectBGProduct(item.BGHandbookId, item.BGHandbookProductId);
                    if (detail != null)
                    {
                        if (detail.BeeQuantity == null)
                            detail.BeeQuantity = 0;
                        detail.BeeQuantity += Convert.ToDouble(item.Quantity);
                        this.bGHandbookDetail1Manager.Update(detail);

                        this.bGHandbookDetail1Manager.UpdateUpQuantity(detail);
                    }
                    item.BGProductDepotOutId = bGProductDepotOut.BGProductDepotOutId;
                    detailaccessor.Insert(item);
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
        /// Update a BGProductDepotOut.
        /// </summary>
        public void Update(Model.BGProductDepotOut bGProductDepotOut)
        {
            //
            // todo: add other logic here.
            //
            this.Validate(bGProductDepotOut);
            if (this.IsExistsDeclareCustomsIdUpdate(bGProductDepotOut.DeclareCustomsId, bGProductDepotOut.BGProductDepotOutId))
                throw new Helper.MessageValueException("已存在相同报关单号！");
            try
            {
                BL.V.BeginTransaction();

                bGProductDepotOut.UpdateTime = DateTime.Now;
                accessor.Update(bGProductDepotOut);
                Model.BGHandbookDetail1 detail;

                //删除详细
                IList<Model.BGProductDepotOutDetail> detailList = detailaccessor.SelectByBGProductDepotOutId(bGProductDepotOut.BGProductDepotOutId);
                foreach (var item in detailList)
                {
                    detail = this.bGHandbookDetail1Manager.SelectBGProduct(item.BGHandbookId, item.BGHandbookProductId);
                    if (detail != null)
                    {
                        if (detail.BeeQuantity == null)
                            detail.BeeQuantity = 0;
                        detail.BeeQuantity -= Convert.ToDouble(item.Quantity);
                        this.bGHandbookDetail1Manager.Update(detail);

                        this.bGHandbookDetail1Manager.UpdateUpQuantity(detail);
                    }
                    detailaccessor.Delete(item.BGProductDepotOutDetailId);
                }

                //添加详细
                foreach (var item in bGProductDepotOut.Detail)
                {
                    detail = this.bGHandbookDetail1Manager.SelectBGProduct(item.BGHandbookId, item.BGHandbookProductId);
                    if (detail != null)
                    {
                        if (detail.BeeQuantity == null)
                            detail.BeeQuantity = 0;
                        detail.BeeQuantity += Convert.ToDouble(item.Quantity);
                        this.bGHandbookDetail1Manager.Update(detail);

                        this.bGHandbookDetail1Manager.UpdateUpQuantity(detail);
                    }
                    item.BGProductDepotOutId = bGProductDepotOut.BGProductDepotOutId;
                    detailaccessor.Insert(item);
                }

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
            return "BPO";
        }

        protected override string GetSettingId()
        {
            return "BGProductDepotOutRule";
        }

        public void TiGuiExists(Model.BGProductDepotOut model)
        {
            if (this.ExistsPrimary(model.BGProductDepotOutId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, DateTime.Now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, DateTime.Now.Year, DateTime.Now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, DateTime.Now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.BGProductDepotOutId = this.GetId(DateTime.Now);
                TiGuiExists(model);
            }
        }

        public void Validate(Model.BGProductDepotOut model)
        {
            //if (string.IsNullOrEmpty(model.BGHangbookId))
            //    throw new Helper.InvalidValueException(Model.BGHandbookDepotIn.PRO_BGHangbookId);
            //foreach (var item in model.Detail)
            //{
            //    if (item.BGHandbookProductId == null)
            //        throw new Helper.MessageValueException("第 " + (model.Detail.IndexOf(item) + 1).ToString() + " 项,手册料件号不能为空！");
            //}
            if (string.IsNullOrEmpty(model.DeclareCustomsId))
                throw new Helper.InvalidValueException(Model.BGProductDepotOut.PRO_DeclareCustomsId);
        }

        public Model.BGProductDepotOut Select(string bGProductDepotOutId)
        {
            Model.BGProductDepotOut model = this.Get(bGProductDepotOutId);
            if (model != null)
                model.Detail = detailaccessor.SelectByBGProductDepotOutId(bGProductDepotOutId);
            return model;
        }

        public bool IsExistsDeclareCustomsIdInsert(string DeclareCustomsId)
        {
            return accessor.IsExistsDeclareCustomsIdInsert(DeclareCustomsId);
        }

        public bool IsExistsDeclareCustomsIdUpdate(string DeclareCustomsId, string Id)
        {
            return accessor.IsExistsDeclareCustomsIdUpdate(DeclareCustomsId, Id);
        }
    }
}

