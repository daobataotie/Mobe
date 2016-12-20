//------------------------------------------------------------------------------
//
// file name：PCPGOnlineCheckManager.cs
// author: mayanjun
// create date：2011-12-6 14:19:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCPGOnlineCheck.
    /// </summary>
    public partial class PCPGOnlineCheckManager : BaseManager
    {
        private static readonly PCPGOnlineCheckDetailManager PCPGOCDManager = new PCPGOnlineCheckDetailManager();
        private static readonly OpticsTestManager opticsTestManager = new OpticsTestManager();
        private static readonly ThicknessTestManager thicknessManager = new ThicknessTestManager();

        /// <summary>
        /// Delete PCPGOnlineCheck by primary key.
        /// </summary>
        public void Delete(string pCPGOnlineCheckId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCPGOnlineCheckId);
        }

        public void Delete(Model.PCPGOnlineCheck pcpgoc)
        {
            try
            {
                BL.V.BeginTransaction();

                //删除光学测试,厚度测试
                foreach (Model.PCPGOnlineCheckDetail d in pcpgoc.Details)
                {
                    opticsTestManager.DeleteByPCPGOnlineCheckDetailId(d.PCPGOnlineCheckDetailId);
                    thicknessManager.DeleteByPCPGOnlineCheckDetailId(d.PCPGOnlineCheckDetailId);
                }

                PCPGOCDManager.DeleteByPCPGOnlineCheckId(pcpgoc.PCPGOnlineCheckId);

                this.Delete(pcpgoc.PCPGOnlineCheckId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Insert(Model.PCPGOnlineCheck pCPGOnlineCheck)
        {
            Validate(pCPGOnlineCheck);
            try
            {
                BL.V.BeginTransaction();
                pCPGOnlineCheck.InsertTime = DateTime.Now;
                pCPGOnlineCheck.UpdateTime = DateTime.Now;
                TiGuiExists(pCPGOnlineCheck);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCPGOnlineCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCPGOnlineCheck.InsertTime.Value.Year, pCPGOnlineCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCPGOnlineCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCPGOnlineCheck);

                foreach (Model.PCPGOnlineCheckDetail detail in pCPGOnlineCheck.Details)
                {
                    detail.PCPGOnlineCheckId = pCPGOnlineCheck.PCPGOnlineCheckId;
                    PCPGOCDManager.Insert(detail);
                }
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.PCPGOnlineCheck pCPGOnlineCheck)
        {
            Validate(pCPGOnlineCheck);
            if (pCPGOnlineCheck != null)
            {
                try
                {
                    BL.V.BeginTransaction();

                    // 修改详细
                    IList<Model.PCPGOnlineCheckDetail> DBDetails = PCPGOCDManager.Select(pCPGOnlineCheck.PCPGOnlineCheckId);
                    foreach (Model.PCPGOnlineCheckDetail d in DBDetails)
                    {
                        //if (pCPGOnlineCheck.Details.Any(ind => d.PCPGOnlineCheckDetailId == ind.PCPGOnlineCheckDetailId))
                        //{
                        //    PCPGOCDManager.Update(d);
                        //}
                        if (!pCPGOnlineCheck.Details.Any(id => id.PCPGOnlineCheckDetailId == d.PCPGOnlineCheckDetailId))
                        {
                            opticsTestManager.DeleteByPCPGOnlineCheckDetailId(d.PCPGOnlineCheckDetailId);
                            thicknessManager.DeleteByPCPGOnlineCheckDetailId(d.PCPGOnlineCheckDetailId);

                            PCPGOCDManager.Delete(d.PCPGOnlineCheckDetailId);
                        }
                    }

                    foreach (Model.PCPGOnlineCheckDetail d in pCPGOnlineCheck.Details)
                    {
                        if (!DBDetails.Any(ind => ind.PCPGOnlineCheckDetailId == d.PCPGOnlineCheckDetailId))
                        {
                            d.PCPGOnlineCheckId = pCPGOnlineCheck.PCPGOnlineCheckId;
                            PCPGOCDManager.Insert(d);
                        }
                        else
                            PCPGOCDManager.Update(d);
                    }

                    //修改头
                    pCPGOnlineCheck.UpdateTime = DateTime.Now;
                    accessor.Update(pCPGOnlineCheck);

                    //IList<Model.PCPGOnlineCheckDetail> DBDetails = PCPGOCDManager.Select(pCPGOnlineCheck.PCPGOnlineCheckId);
                    //foreach (Model.PCPGOnlineCheckDetail d in DBDetails)
                    //{
                    //    opticsTestManager.DeleteByPCPGOnlineCheckDetailId(d.PCPGOnlineCheckDetailId);
                    //    thicknessManager.DeleteByPCPGOnlineCheckDetailId(d.PCPGOnlineCheckDetailId);
                    //    PCPGOCDManager.Delete(d.PCPGOnlineCheckDetailId);
                    //}
                    //foreach (Model.PCPGOnlineCheckDetail detail in pCPGOnlineCheck.Details)
                    //{
                    //    foreach (Model.PCPGOnlineCheckDetail d in DBDetails)
                    //    {
                    //        if (d.PCPGOnlineCheckDetailId == detail.PCPGOnlineCheckDetailId)
                    //        {
                    //            PCPGOCDManager.Update(detail);
                    //        }
                    //    }
                    //}

                    BL.V.CommitTransaction();
                }
                catch
                {
                    BL.V.RollbackTransaction();
                    throw;
                }
            }
        }

        public Model.PCPGOnlineCheck GetDetail(string PCPGOCId)
        {
            Model.PCPGOnlineCheck pcpgoc = accessor.Get(PCPGOCId);
            pcpgoc.Details = PCPGOCDManager.Select(PCPGOCId);
            return pcpgoc;
        }

        protected override string GetSettingId()
        {
            return "pcpgocRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pcpgoc";
        }

        private void Validate(Model.PCPGOnlineCheck pcpgoc)
        {
            if (string.IsNullOrEmpty(pcpgoc.PCPGOnlineCheckId))
                throw new Helper.RequireValueException(Model.PCPGOnlineCheck.PRO_PCPGOnlineCheckId);
            if (string.IsNullOrEmpty(pcpgoc.PCPGOnlineCheckDate.Value.ToString()))
                throw new Helper.RequireValueException(Model.PCPGOnlineCheck.PRO_PCPGOnlineCheckDate);
            if (string.IsNullOrEmpty(pcpgoc.EmployeeId))
                throw new Helper.RequireValueException(Model.PCPGOnlineCheck.PRO_EmployeeId);
        }

        public IList<Model.PCPGOnlineCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Model.Product product, Model.Customer customer, string CusXOId, string StartPronoteHeader, string EndPronoteHeader)
        {
            return accessor.SelectByDateRage(StartDate, EndDate, product, customer, CusXOId, StartPronoteHeader, EndPronoteHeader);
        }

        private void TiGuiExists(Model.PCPGOnlineCheck model)
        {
            if (this.ExistsPrimary(model.PCPGOnlineCheckId))
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
                model.PCPGOnlineCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }
    }
}

