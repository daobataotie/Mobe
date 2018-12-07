//------------------------------------------------------------------------------
//
// file name：PronoteHeaderManager.cs
// author: peidun
// create date：2009-12-29 11:58:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PronoteHeader.
    /// </summary>
    public partial class PronoteHeaderManager : BaseManager
    {
        //  private static readonly DA.IPronotedetailsAccessor PronotedetailsAccessor = (DA.IPronotedetailsAccessor)Accessors.Get("PronotedetailsAccessor");
        private static readonly DA.IPronotedetailsMaterialAccessor PronotedetailsMaterialAccessor = (DA.IPronotedetailsMaterialAccessor)Accessors.Get("PronotedetailsMaterialAccessor");
        private static readonly DA.IPronoteProceduresDetailAccessor PronoteProceduresDetailAccessor = (DA.IPronoteProceduresDetailAccessor)Accessors.Get("PronoteProceduresDetailAccessor");
        private static readonly DA.IPronoteMachineAccessor PronoteMachineAccessor = (DA.IPronoteMachineAccessor)Accessors.Get("PronoteMachineAccessor");
        private static readonly DA.IProceduresMachineAccessor ProceduresMachineAccessor = (DA.IProceduresMachineAccessor)Accessors.Get("ProceduresMachineAccessor");
        private MRSdetailsManager mRSdetailsManager = new MRSdetailsManager();

        public void Delete(string pronoteHeaderID)
        {
            try
            {
                // BL.V.BeginTransaction();
                Model.PronoteHeader pronoteHeader = accessor.Get(pronoteHeaderID);
                if (pronoteHeader != null)
                {
                    Model.MRSdetails mrsDal = new BL.MRSdetailsManager().Get(pronoteHeader.MRSdetailsId);
                    if (mrsDal != null)
                    {
                        mrsDal.MRSHasSingleSum = mrsDal.MRSHasSingleSum - pronoteHeader.DetailsSum;
                        if (mrsDal.MRSHasSingleSum >= mrsDal.MRSdetailssum)
                        {
                            mrsDal.DetailsFlag = 2;
                        }
                        else
                        {
                            if (mrsDal.MRSHasSingleSum > 0)
                                mrsDal.DetailsFlag = 1;
                            else
                                mrsDal.DetailsFlag = 0;
                        }
                        //修改排单描述
                        mrsDal.ArrangeDesc = string.Empty;
                        new BL.MRSdetailsManager().Update(mrsDal);
                        UpdateMRSHeaderFlag(mrsDal.MRSHeader);
                    }
                }
                //
                // todo:add other logic here
                //
                accessor.Delete(pronoteHeaderID);

                // BL.V.CommitTransaction();
            }
            catch
            {
                // BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.PronoteHeader pronoteHeader)
        {
            //
            // todo:add other logic here
            //  
            this.Delete(pronoteHeader.PronoteHeaderID);
        }

        public Model.PronoteHeader GetDetails(string pronoteHeaderID)
        {
            Model.PronoteHeader pronoteHeader = accessor.Get(pronoteHeaderID);
            if (pronoteHeader == null) return null;
            pronoteHeader.DetailsMaterial = PronotedetailsMaterialAccessor.GetByHeader(pronoteHeader);
            pronoteHeader.DetailProcedures = PronoteProceduresDetailAccessor.GetPronotedetailsMaterialByHeaderId(pronoteHeader);
            return pronoteHeader;
        }

        public void Insert(Model.PronoteHeader pronoteHeader)
        {
            //
            // todo:add other logic here
            //
            Validate(pronoteHeader);
            try
            {
                BL.V.BeginTransaction();

                this.InsertWithOutTrans(pronoteHeader);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void InsertWithOutTrans(Model.PronoteHeader pronoteHeader)
        {


            pronoteHeader.InsertTime = DateTime.Now;
            pronoteHeader.UpdateTime = DateTime.Now;
            TiGuiExists(pronoteHeader);
            if (pronoteHeader.Employee0 != null)
                pronoteHeader.Employee0Id = pronoteHeader.Employee0.EmployeeId;
            if (pronoteHeader.Employee1 != null)
                pronoteHeader.Employee1Id = pronoteHeader.Employee1.EmployeeId;
            if (pronoteHeader.Employee2 != null)
                pronoteHeader.Employee2Id = pronoteHeader.Employee2.EmployeeId;
            string invoiceKind = this.GetInvoiceKind().ToLower();
            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pronoteHeader.InsertTime.Value.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pronoteHeader.InsertTime.Value.Year, pronoteHeader.InsertTime.Value.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pronoteHeader.InsertTime.Value.ToString("yyyy-MM-dd"));
            string sequencekey = string.Format(invoiceKind);
            SequenceManager.Increment(sequencekey_y);
            SequenceManager.Increment(sequencekey_m);
            SequenceManager.Increment(sequencekey_d);
            SequenceManager.Increment(sequencekey);
            pronoteHeader.InvoiceStatus = 1;
            pronoteHeader.InDepotQuantity = 0;
            pronoteHeader.IsClose = false;
            accessor.Insert(pronoteHeader);
            Model.MRSdetails mrsDal = mRSdetailsManager.Get(pronoteHeader.MRSdetailsId);
            if (mrsDal != null)
            {
                if (mrsDal.MRSHasSingleSum == null)
                    mrsDal.MRSHasSingleSum = 0;
                mrsDal.MRSHasSingleSum += pronoteHeader.DetailsSum;
                if (mrsDal.MRSHasSingleSum >= mrsDal.MRSdetailssum)
                {
                    mrsDal.DetailsFlag = 2;
                }
                else
                {
                    if (mrsDal.MRSHasSingleSum > 0)
                    {
                        mrsDal.DetailsFlag = 1;
                    }
                    else
                    {
                        mrsDal.DetailsFlag = 0;
                    }
                }
                mrsDal.ArrangeDesc = "已經排單";
                mRSdetailsManager.Update(mrsDal);
                UpdateMRSHeaderFlag(mrsDal.MRSHeader);
            }

            foreach (Model.PronotedetailsMaterial proMaterial in pronoteHeader.DetailsMaterial)
            {
                if (string.IsNullOrEmpty(proMaterial.ProductId))
                    continue;
                proMaterial.PronoteHeaderID = pronoteHeader.PronoteHeaderID;
                PronotedetailsMaterialAccessor.Insert(proMaterial);
            }
            foreach (Model.PronoteProceduresDetail detailProcedures in pronoteHeader.DetailProcedures)
            {
                if (detailProcedures.PronoteProceduresDetailId == null)
                    continue;
                detailProcedures.PronoteHeader = pronoteHeader;
                detailProcedures.PronoteHeaderID = pronoteHeader.PronoteHeaderID;
                if (PronoteProceduresDetailAccessor.ExistsPrimary(detailProcedures.PronoteProceduresDetailId))
                    PronoteProceduresDetailAccessor.Update(detailProcedures);
                else
                    PronoteProceduresDetailAccessor.Insert(detailProcedures);
            }
            if (pronoteHeader.ProceduresMachineDetail != null)
            {
                foreach (Model.ProceduresMachine item in pronoteHeader.ProceduresMachineDetail)
                {
                    if (ProceduresMachineAccessor.ExistsPrimary(item.ProceduresMachineId))
                        ProceduresMachineAccessor.Update(item);
                    else
                        ProceduresMachineAccessor.Insert(item);
                }
            }
        }

        public void UpdateMRSHeaderFlag(Model.MRSHeader mRSHeader)
        {
            int flag = 0;
            //IList<Model.MRSdetails> list = mRSdetailsManager.Select(mRSHeader);
            IList<Model.MRSdetails> list = mRSdetailsManager.SelectBySqlMap(mRSHeader);


            foreach (Model.MRSdetails detail in list)
            {
                flag += detail.DetailsFlag == null ? 0 : detail.DetailsFlag.Value;
            }
            if (flag == 0)
                mRSHeader.InvoiceFlag = 0;
            else if (flag < list.Count * 2)
                mRSHeader.InvoiceFlag = 1;
            else if (flag == list.Count * 2)
                mRSHeader.InvoiceFlag = 2;
            new BL.MRSHeaderManager().UpdateHeader(mRSHeader);
        }

        public void Update(Model.PronoteHeader pronoteHeader)
        {
            //
            // todo: add other logic here.
            //
            Validate(pronoteHeader);
            // mPSheader.UpdateTime = DateTime.Now;
            // accessor.Update(mPSheader);
            if (pronoteHeader != null)
            {
                try
                {
                    BL.V.BeginTransaction();
                    //this.Delete(pronoteHeader);
                    pronoteHeader.UpdateTime = DateTime.Now;
                    //if (pronoteHeader.Employee0 != null)
                    //    pronoteHeader.Employee0Id = pronoteHeader.Employee0.EmployeeId;
                    if (pronoteHeader.Employee1 != null)
                        pronoteHeader.Employee1Id = pronoteHeader.Employee1.EmployeeId;
                    if (pronoteHeader.Employee2 != null)
                        pronoteHeader.Employee2Id = pronoteHeader.Employee2.EmployeeId;


                    pronoteHeader.InvoiceStatus = 1;
                    if (pronoteHeader.IsClose == null || !pronoteHeader.IsClose.Value)
                    {
                        pronoteHeader.InDepotQuantity = pronoteHeader.InDepotQuantity.HasValue ? pronoteHeader.InDepotQuantity : 0;
                        if (pronoteHeader.InDepotQuantity >= pronoteHeader.DetailsSum)
                        {
                            pronoteHeader.IsClose = true;
                            pronoteHeader.JieAnDate = DateTime.Now;
                        }
                        else
                            pronoteHeader.IsClose = false;
                    }
                    accessor.Update(pronoteHeader);


                    Model.MRSdetails mrsDal = mRSdetailsManager.Get(pronoteHeader.MRSdetailsId);
                    if (mrsDal != null)
                    {
                        if (mrsDal.MRSHasSingleSum == null)
                            mrsDal.MRSHasSingleSum = 0;
                        mrsDal.MRSHasSingleSum += pronoteHeader.DetailsSum;
                        if (mrsDal.MRSHasSingleSum >= mrsDal.MRSdetailssum)
                        {
                            mrsDal.DetailsFlag = 2;
                        }
                        else
                        {
                            if (mrsDal.MRSHasSingleSum > 0)
                            {
                                mrsDal.DetailsFlag = 1;
                            }
                            else
                            {
                                mrsDal.DetailsFlag = 0;
                            }
                        }
                        //修改派单描述
                        if (mrsDal.DetailsFlag == 2)
                            mrsDal.ArrangeDesc = "已經排單";
                        mRSdetailsManager.Update(mrsDal);
                        UpdateMRSHeaderFlag(mrsDal.MRSHeader);
                    }
                    //foreach (Model.Pronotedetails pronotedetails in pronoteHeader.Details)
                    //{
                    //    if (pronotedetails.Product == null || string.IsNullOrEmpty(pronotedetails.Product.ProductId))
                    //        throw new Exception("貨品不為空");
                    //    pronotedetails.PronoteHeaderID = pronoteHeader.PronoteHeaderID;


                    //    Model.MRSdetails mrsDal = new BL.MRSdetailsManager().Get(pronotedetails.MRSdetailsId);
                    // //   double Sum = new BL.ProduceOtherCompactDetailManager().GetByMPSdetail(pronotedetails.MPSdetailId);
                    //    PronotedetailsAccessor.Insert(pronotedetails);
                    //    if (mrsDal != null)
                    //    {
                    //        if (mrsDal.MRSHasSingleSum == null)
                    //            mrsDal.MRSHasSingleSum = 0;
                    //        mrsDal.MRSHasSingleSum += pronotedetails.DetailsSum;
                    //        //if (mrsDal.MPSHasSingleSum == mpsDal.MPSdetailssum)
                    //        //{
                    //        //    mrsDal.MPSEndState = true;
                    //        //}
                    //        new BL.MRSdetailsManager().Update(mrsDal);
                    //    }
                    //}

                    PronotedetailsMaterialAccessor.DeleteByHeaderId(pronoteHeader.PronoteHeaderID);
                    foreach (Model.PronotedetailsMaterial proMaterial in pronoteHeader.DetailsMaterial)
                    {
                        if (string.IsNullOrEmpty(proMaterial.ProductId))
                            continue;
                        proMaterial.PronoteHeaderID = pronoteHeader.PronoteHeaderID;
                        if (PronotedetailsMaterialAccessor.ExistsPrimary(proMaterial.PronotedetailsMaterialId))
                            PronotedetailsMaterialAccessor.Update(proMaterial);
                        else
                            PronotedetailsMaterialAccessor.Insert(proMaterial);
                    }
                    foreach (Model.PronoteProceduresDetail detailProcedures in pronoteHeader.DetailProcedures)
                    {
                        if (string.IsNullOrEmpty(detailProcedures.PronoteProceduresDetailId))
                            continue;
                        detailProcedures.PronoteHeader = pronoteHeader;
                        detailProcedures.PronoteHeaderID = pronoteHeader.PronoteHeaderID;
                        if (PronoteProceduresDetailAccessor.ExistsPrimary(detailProcedures.PronoteProceduresDetailId))
                            PronoteProceduresDetailAccessor.Update(detailProcedures);
                        else
                            PronoteProceduresDetailAccessor.Insert(detailProcedures);
                    }
                    foreach (Model.ProceduresMachine item in pronoteHeader.ProceduresMachineDetail)
                    {
                        if (ProceduresMachineAccessor.ExistsPrimary(item.ProceduresMachineId))
                            ProceduresMachineAccessor.Update(item);
                        else
                            ProceduresMachineAccessor.Insert(item);

                    }
                    BL.V.CommitTransaction();
                }
                catch
                {
                    BL.V.RollbackTransaction();
                    throw;
                }
            }
        }

        private void Validate(Model.PronoteHeader pronoteHeader)
        {
            if (string.IsNullOrEmpty(pronoteHeader.PronoteHeaderID))
            {
                throw new Helper.RequireValueException(Model.PronoteHeader.PRO_PronoteHeaderID);
            }
            //if (string.IsNullOrEmpty(pronoteHeader.WorkHouseId))
            //{
            //    throw new Helper.RequireValueException(Model.PronoteHeader.PROPERTY_WORKHOUSEID);
            //}
        }

        protected override string GetSettingId()
        {
            return "pntRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pnt";
        }

        public IList<Book.Model.PronoteHeader> GetByDate(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourceType, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5)
        {
            return accessor.GetByDate(startDate, endDate, customer, cusxoid, product, PronoteHeaderIdStart, PronoteHeaderIdEnd, sourceType, workhouseIndepot, jiean, proNameKey, proCusNameKey, pronoteHeaderIdKey, sourcetype0, sourcetype4, sourcetype5);
        }

        public IList<Book.Model.PronoteHeader> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd, string CusXOId)
        {
            return accessor.Select(customerStart, customerEnd, dateStart, dateEnd, CusXOId);
        }

        private void TiGuiExists(Model.PronoteHeader model)
        {
            if (this.ExistsPrimary(model.PronoteHeaderID))
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
                model.PronoteHeaderID = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }

        public IList<Book.Model.PronoteHeader> Select(Model.MRSHeader mrsheader)
        {
            return accessor.Select(mrsheader);
        }

        public void UpdateHeaderIsClse(string pronoteheaderid, bool isclose)
        {
            accessor.UpdateHeaderIsClse(pronoteheaderid, isclose);
        }

        public IList<Book.Model.PronoteHeader> GetByDateMa(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourcetype, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5)
        {
            return accessor.GetByDateMa(startDate, endDate, customer, cusxoid, product, PronoteHeaderIdStart, PronoteHeaderIdEnd, sourcetype, workhouseIndepot, jiean, proNameKey, proCusNameKey, pronoteHeaderIdKey, sourcetype0, sourcetype4, sourcetype5);
        }

        public IList<Book.Model.PronoteHeader> GetByDateZJ(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourcetype, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5)
        {
            return accessor.GetByDateZJ(startDate, endDate, customer, cusxoid, product, PronoteHeaderIdStart, PronoteHeaderIdEnd, sourcetype, workhouseIndepot, jiean, proNameKey, proCusNameKey, pronoteHeaderIdKey, sourcetype0, sourcetype4, sourcetype5);
        }
        public void UpdateHeaderIsClseByXOId(string InvoiceXOId, bool isclose)
        {
            accessor.UpdateHeaderIsClseByXOId(InvoiceXOId, isclose);
        }


        public IList<Book.Model.PronoteHeader> Select(IList<string> ids)
        {
            return accessor.Select(ids);
        }

        public IList<Model.PronoteHeader> SelectByProductId(DateTime startDate, DateTime endDate, string productid)
        {
            return accessor.SelectByProductId(startDate, endDate, productid);
        }

        /// <summary>
        /// 用于在查询中通过“手册号”筛选
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="productid"></param>
        /// <param name="handBookId"></param>
        /// <returns></returns>
        public IList<Model.PronoteHeader> SelectByProductId(DateTime startDate, DateTime endDate, string productid, string handBookId)
        {
            return accessor.SelectByProductId(startDate, endDate, productid, handBookId);
        }

        public IList<Model.PronoteHeader> SelectByProductIdAll(string productid)
        {
            return accessor.SelectByProductIdAll(productid);
        }
    }
}

/// <summary>
/// 修改加工单 结案 参数 加工单编号 入库合格数量
/// </summary>
/// <param name="pronoteheaderid"></param>
/// <param name="indepotquantity"></param>
//public void UpdatePronoteIsClose(string pronoteheaderid, double? indepotquantity)
//{
//    accessor.UpdatePronoteIsClose( pronoteheaderid , indepotquantity) ;
//}

