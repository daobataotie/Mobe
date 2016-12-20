//------------------------------------------------------------------------------
//
// file name：BGHandbookDepotInManager.cs
// author: mayanjun
// create date：2013/12/19 18:37:43
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbookDepotIn.
    /// </summary>
    public partial class BGHandbookDepotInManager : BaseManager
    {
        private static readonly DA.IBGHandbookDepotInDetailAccessor Detailaccessor = (DA.IBGHandbookDepotInDetailAccessor)Accessors.Get("BGHandbookDepotInDetailAccessor");
        BGHandbookDetail2Manager Detail2Manager = new BGHandbookDetail2Manager();
        /// <summary>
        /// Delete BGHandbookDepotIn by primary key.
        /// </summary>
        public void Delete(string bGHandbookDepotInId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                //Detailaccessor.DeleteByBGHandbookDepotInId(bGHandbookDepotInId);
                this.Delete(this.Select(bGHandbookDepotInId));
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.BGHandbookDepotIn bGHandbookDepotIn)
        {
            Model.BGHandbookDetail2 detail2;
            foreach (var item in bGHandbookDepotIn.Detail)
            {

                //改变手册料件
                detail2 = Detail2Manager.SelectByShouceAndId(bGHandbookDepotIn.BGHangbookId, (int)item.BGHandbookProductId);
                if (detail2 != null)
                {
                    if (Convert.ToBoolean(item.IsInto))
                    {
                        detail2.ZhuanCeInQuantity = Convert.ToDouble(detail2.ZhuanCeInQuantity) - Convert.ToDouble(item.DepotInQuantity);
                    }
                    detail2.LbejinQuantity = Convert.ToDouble(detail2.LbejinQuantity) - Convert.ToDouble(item.DepotInQuantity);
                    detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.HaiKeJInQuantity) + Convert.ToDouble(item.DepotInQuantity);
                    detail2.UpQuantity = Convert.ToDouble(detail2.UpQuantity) + Convert.ToDouble(item.DepotInQuantity);
                    detail2.JinKouiMoney = Convert.ToDecimal(detail2.LbejinQuantity) * detail2.LPrice;

                    Detail2Manager.Update(detail2);
                }
                Detailaccessor.Delete(item.BGHandbookDepotInDetailId);
            }
            accessor.Delete(bGHandbookDepotIn.BGHandbookDepotInId);
        }

        /// <summary>
        /// Insert a BGHandbookDepotIn.
        /// </summary>
        public void Insert(Model.BGHandbookDepotIn bGHandbookDepotIn)
        {
            //
            // todo:add other logic here
            //
            this.Validate(bGHandbookDepotIn);
            try
            {
                BL.V.BeginTransaction();

                this.TiGuiExists(bGHandbookDepotIn);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, DateTime.Now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, DateTime.Now.Year, DateTime.Now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, DateTime.Now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                bGHandbookDepotIn.InsertTime = DateTime.Now;
                bGHandbookDepotIn.UpdateTime = DateTime.Now;
                accessor.Insert(bGHandbookDepotIn);
                Model.BGHandbookDetail2 detail2;
                foreach (var item in bGHandbookDepotIn.Detail)
                {
                    //改变手册料件
                    detail2 = Detail2Manager.SelectByShouceAndId(bGHandbookDepotIn.BGHangbookId, (int)item.BGHandbookProductId);
                    if (detail2 != null)
                    {
                        if (Convert.ToBoolean(item.IsInto))
                        {
                            detail2.ZhuanCeInQuantity = Convert.ToDouble(detail2.ZhuanCeInQuantity) + Convert.ToDouble(item.DepotInQuantity);
                        }
                        detail2.LbejinQuantity = Convert.ToDouble(detail2.LbejinQuantity) + Convert.ToDouble(item.DepotInQuantity);
                        detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.HaiKeJInQuantity) - Convert.ToDouble(item.DepotInQuantity);
                        detail2.UpQuantity = Convert.ToDouble(detail2.UpQuantity) - Convert.ToDouble(item.DepotInQuantity);
                        detail2.JinKouiMoney = Convert.ToDecimal(detail2.LbejinQuantity) * detail2.LPrice;

                        Detail2Manager.Update(detail2);
                    }
                    item.BGHandbookDepotInId = bGHandbookDepotIn.BGHandbookDepotInId;
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
        /// Update a BGHandbookDepotIn.
        /// </summary>
        public void Update(Model.BGHandbookDepotIn bGHandbookDepotIn)
        {
            //
            // todo: add other logic here.
            //
            this.Validate(bGHandbookDepotIn);
            try
            {
                BL.V.BeginTransaction();

                bGHandbookDepotIn.UpdateTime = DateTime.Now;
                accessor.Update(bGHandbookDepotIn);
                Model.BGHandbookDetail2 detail2;

                //删除详细

                IList<Model.BGHandbookDepotInDetail> DetailList = Detailaccessor.SelectByBGHandbookDepotInId(bGHandbookDepotIn.BGHandbookDepotInId);
                foreach (var item in DetailList)
                {
                    //改变手册料件
                    detail2 = Detail2Manager.SelectByShouceAndId(bGHandbookDepotIn.BGHangbookId, (int)item.BGHandbookProductId);
                    if (detail2 != null)
                    {
                        if (Convert.ToBoolean(item.IsInto))
                        {
                            detail2.ZhuanCeInQuantity = Convert.ToDouble(detail2.ZhuanCeInQuantity) - Convert.ToDouble(item.DepotInQuantity);
                        }
                        detail2.LbejinQuantity = Convert.ToDouble(detail2.LbejinQuantity) - Convert.ToDouble(item.DepotInQuantity);
                        detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.HaiKeJInQuantity) + Convert.ToDouble(item.DepotInQuantity);
                        detail2.UpQuantity = Convert.ToDouble(detail2.UpQuantity) + Convert.ToDouble(item.DepotInQuantity);
                        detail2.JinKouiMoney = Convert.ToDecimal(detail2.LbejinQuantity) * detail2.LPrice;

                        Detail2Manager.Update(detail2);
                    }
                    Detailaccessor.Delete(item.BGHandbookDepotInDetailId);
                }

                //添加详细
                foreach (var item in bGHandbookDepotIn.Detail)
                {
                    //改变手册料件
                    detail2 = Detail2Manager.SelectByShouceAndId(bGHandbookDepotIn.BGHangbookId, (int)item.BGHandbookProductId);
                    if (detail2 != null)
                    {
                        if (Convert.ToBoolean(item.IsInto))
                        {
                            detail2.ZhuanCeInQuantity = Convert.ToDouble(detail2.ZhuanCeInQuantity) + Convert.ToDouble(item.DepotInQuantity);
                        }
                        detail2.LbejinQuantity = Convert.ToDouble(detail2.LbejinQuantity) + Convert.ToDouble(item.DepotInQuantity);
                        detail2.HaiKeJInQuantity = Convert.ToDouble(detail2.HaiKeJInQuantity) - Convert.ToDouble(item.DepotInQuantity);
                        detail2.UpQuantity = Convert.ToDouble(detail2.UpQuantity) - Convert.ToDouble(item.DepotInQuantity);
                        detail2.JinKouiMoney = Convert.ToDecimal(detail2.LbejinQuantity) * detail2.LPrice;

                        Detail2Manager.Update(detail2);
                    }
                    item.BGHandbookDepotInId = bGHandbookDepotIn.BGHandbookDepotInId;
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

        protected override string GetInvoiceKind()
        {
            return "BGI";
        }

        protected override string GetSettingId()
        {
            return "BGHandbookDepotInRule";
        }

        public void TiGuiExists(Model.BGHandbookDepotIn model)
        {
            if (this.ExistsPrimary(model.BGHandbookDepotInId))
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
                model.BGHandbookDepotInId = this.GetId(DateTime.Now);
                TiGuiExists(model);
            }
        }

        public void Validate(Model.BGHandbookDepotIn model)
        {
            if (string.IsNullOrEmpty(model.BGHangbookId))
                throw new Helper.InvalidValueException(Model.BGHandbookDepotIn.PRO_BGHangbookId);
            foreach (var item in model.Detail)
            {
                if (item.BGHandbookProductId == null)
                    throw new Helper.MessageValueException("第 " + (model.Detail.IndexOf(item) + 1).ToString() + " 项,手册料件号不能为空！");
            }
        }

        public Model.BGHandbookDepotIn Select(string bGHandbookDepotInId)
        {
            Model.BGHandbookDepotIn model = this.Get(bGHandbookDepotInId);
            if (model != null)
                model.Detail = Detailaccessor.SelectByBGHandbookDepotInId(bGHandbookDepotInId);
            return model;
        }
    }
}

