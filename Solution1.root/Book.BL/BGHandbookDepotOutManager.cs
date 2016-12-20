//------------------------------------------------------------------------------
//
// file name：BGHandbookDepotOutManager.cs
// author: mayanjun
// create date：2014/3/5 16:32:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbookDepotOut.
    /// </summary>
    public partial class BGHandbookDepotOutManager : BaseManager
    {
        private static readonly DA.IBGHandbookDepotOutDetailAccessor Detailaccessor = (DA.IBGHandbookDepotOutDetailAccessor)Accessors.Get("BGHandbookDepotOutDetailAccessor");
        BGHandbookDetail2Manager Detail2Manager = new BGHandbookDetail2Manager();

        /// <summary>
        /// Delete BGHandbookDepotOut by primary key.
        /// </summary>
        public void Delete(string bGHandbookDepotOutId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.Delete(this.Select(bGHandbookDepotOutId));
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.BGHandbookDepotOut model)
        {
            Model.BGHandbookDetail2 detail2;
            foreach (var item in model.Detail)
            {

                //改变手册料件
                detail2 = Detail2Manager.SelectByShouceAndId(model.BGHangbookId, (int)item.BGHandbookProductId);
                if (detail2 != null)
                {
                    detail2.ZhuanCeOutQuantity = Convert.ToDouble(detail2.ZhuanCeOutQuantity) - Convert.ToDouble(item.DepotOutQuantity);
                    detail2.LbejinQuantity = Convert.ToDouble(detail2.LbejinQuantity) + Convert.ToDouble(item.DepotOutQuantity);
                    detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.HaiKeJInQuantity) - Convert.ToDouble(item.DepotOutQuantity);
                    detail2.UpQuantity = Convert.ToDouble(detail2.UpQuantity) - Convert.ToDouble(item.DepotOutQuantity);
                    detail2.JinKouiMoney = Convert.ToDecimal(detail2.LbejinQuantity) * detail2.LPrice;

                    Detail2Manager.Update(detail2);
                }
                Detailaccessor.Delete(item.BGHandbookDepotOutDetailId);
            }
            accessor.Delete(model.BGHandbookDepotOutId);

        }

        /// <summary>
        /// Insert a BGHandbookDepotOut.
        /// </summary>
        public void Insert(Model.BGHandbookDepotOut bGHandbookDepotOut)
        {
            //
            // todo:add other logic here
            //
            this.Validate(bGHandbookDepotOut);
            try
            {
                BL.V.BeginTransaction();
                this.TiGuiExists(bGHandbookDepotOut);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, DateTime.Now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, DateTime.Now.Year, DateTime.Now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, DateTime.Now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                bGHandbookDepotOut.InsertTime = DateTime.Now;
                bGHandbookDepotOut.UpdateTime = DateTime.Now;
                accessor.Insert(bGHandbookDepotOut);

                Model.BGHandbookDetail2 detail2;
                foreach (var item in bGHandbookDepotOut.Detail)
                {
                    detail2 = Detail2Manager.SelectByShouceAndId(bGHandbookDepotOut.BGHangbookId, (int)item.BGHandbookProductId);
                    if (detail2 != null)
                    {
                        detail2.ZhuanCeOutQuantity = Convert.ToDouble(detail2.ZhuanCeOutQuantity) + Convert.ToDouble(item.DepotOutQuantity);
                        detail2.LbejinQuantity = Convert.ToDouble(detail2.LbejinQuantity) - Convert.ToDouble(item.DepotOutQuantity);
                        detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.HaiKeJInQuantity) + Convert.ToDouble(item.DepotOutQuantity);
                        detail2.UpQuantity = Convert.ToDouble(detail2.UpQuantity) + Convert.ToDouble(item.DepotOutQuantity);
                        detail2.JinKouiMoney = Convert.ToDecimal(detail2.LbejinQuantity) * detail2.LPrice;

                        Detail2Manager.Update(detail2);
                    }
                    item.BGHandbookDepotOutId = bGHandbookDepotOut.BGHandbookDepotOutId;
                    Detailaccessor.Insert(item);
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
        /// Update a BGHandbookDepotOut.
        /// </summary>
        public void Update(Model.BGHandbookDepotOut bGHandbookDepotOut)
        {
            //
            // todo: add other logic here.
            //
            this.Validate(bGHandbookDepotOut);
            try
            {
                BL.V.BeginTransaction();
                bGHandbookDepotOut.UpdateTime = DateTime.Now;
                accessor.Update(bGHandbookDepotOut);
                Model.BGHandbookDetail2 detail2;
                //删除详细
                IList<Model.BGHandbookDepotOutDetail> DetailList = Detailaccessor.SelectByBGHandbookDepotOutId(bGHandbookDepotOut.BGHandbookDepotOutId);
                foreach (var item in DetailList)
                {
                    detail2 = Detail2Manager.SelectByShouceAndId(bGHandbookDepotOut.BGHangbookId, (int)item.BGHandbookProductId);
                    if (detail2 != null)
                    {
                        detail2.ZhuanCeOutQuantity = Convert.ToDouble(detail2.ZhuanCeOutQuantity) - Convert.ToDouble(item.DepotOutQuantity);
                        detail2.LbejinQuantity = Convert.ToDouble(detail2.LbejinQuantity) + Convert.ToDouble(item.DepotOutQuantity);
                        detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.HaiKeJInQuantity) - Convert.ToDouble(item.DepotOutQuantity);
                        detail2.UpQuantity = Convert.ToDouble(detail2.UpQuantity) - Convert.ToDouble(item.DepotOutQuantity);
                        detail2.JinKouiMoney = Convert.ToDecimal(detail2.LbejinQuantity) * detail2.LPrice;
                        Detail2Manager.Update(detail2);
                    }
                    Detailaccessor.Delete(item.BGHandbookDepotOutDetailId);
                }

                //添加详细
                foreach (var item in bGHandbookDepotOut.Detail)
                {
                    detail2 = Detail2Manager.SelectByShouceAndId(bGHandbookDepotOut.BGHangbookId, (int)item.BGHandbookProductId);
                    if (detail2 != null)
                    {
                        detail2.ZhuanCeOutQuantity = Convert.ToDouble(detail2.ZhuanCeOutQuantity) + Convert.ToDouble(item.DepotOutQuantity);
                        detail2.LbejinQuantity = Convert.ToDouble(detail2.LbejinQuantity) - Convert.ToDouble(item.DepotOutQuantity);
                        detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.HaiKeJInQuantity) + Convert.ToDouble(item.DepotOutQuantity);
                        detail2.UpQuantity = Convert.ToDouble(detail2.UpQuantity) + Convert.ToDouble(item.DepotOutQuantity);
                        detail2.JinKouiMoney = Convert.ToDecimal(detail2.LbejinQuantity) * detail2.LPrice;
                        Detail2Manager.Update(detail2);
                    }
                    item.BGHandbookDepotOutId = bGHandbookDepotOut.BGHandbookDepotOutId;
                    Detailaccessor.Insert(item);
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
            accessor.Update(bGHandbookDepotOut);
        }
        protected override string GetInvoiceKind()
        {
            return "BGO";
        }

        protected override string GetSettingId()
        {
            return "BGHandbookDepotOutRule";
        }

        public void TiGuiExists(Model.BGHandbookDepotOut model)
        {
            if (this.ExistsPrimary(model.BGHandbookDepotOutId))
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
                model.BGHandbookDepotOutId = this.GetId(DateTime.Now);
                TiGuiExists(model);
            }
        }

        public void Validate(Model.BGHandbookDepotOut model)
        {
            if (string.IsNullOrEmpty(model.BGHangbookId))
                throw new Helper.InvalidValueException(Model.BGHandbookDepotIn.PRO_BGHangbookId);
            foreach (var item in model.Detail)
            {
                if (item.BGHandbookProductId == null)
                    throw new Helper.MessageValueException("第 " + (model.Detail.IndexOf(item) + 1).ToString() + " 项,手册料件号不能为空！");
            }
        }

        public Model.BGHandbookDepotOut Select(string bGHandbookDepotOutId)
        {
            Model.BGHandbookDepotOut model = this.Get(bGHandbookDepotOutId);
            if (model != null)
                model.Detail = Detailaccessor.SelectByBGHandbookDepotOutId(bGHandbookDepotOutId);
            return model;
        }
    }
}

