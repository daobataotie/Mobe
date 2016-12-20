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
        private static readonly DA.IPronotedetailsAccessor PronotedetailsAccessor = (DA.IPronotedetailsAccessor)Accessors.Get("PronotedetailsAccessor");
        private static readonly DA.IPronotedetailsMaterialAccessor PronotedetailsMaterialAccessor = (DA.IPronotedetailsMaterialAccessor)Accessors.Get("PronotedetailsMaterialAccessor");
        private static readonly DA.IPronoteProceduresDetailAccessor PronoteProceduresDetailAccessor = (DA.IPronoteProceduresDetailAccessor)Accessors.Get("PronoteProceduresDetailAccessor");
        private static readonly DA.IPronoteMachineAccessor PronoteMachineAccessor = (DA.IPronoteMachineAccessor)Accessors.Get("PronoteMachineAccessor");
        private static readonly DA.IProceduresMachineAccessor ProceduresMachineAccessor = (DA.IProceduresMachineAccessor)Accessors.Get("ProceduresMachineAccessor");

        /// <summary>
        /// Delete PronoteHeader by primary key.
        /// </summary>
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
                            {
                                mrsDal.DetailsFlag = 1;
                            }
                            else
                            {
                                mrsDal.DetailsFlag = 0;
                            }
                        }
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
            //pronoteHeader.Details = PronotedetailsAccessor.Select(pronoteHeader);
            pronoteHeader.DetailsMaterial = PronotedetailsMaterialAccessor.GetByHeader(pronoteHeader);
            pronoteHeader.DetailProcedures = PronoteProceduresDetailAccessor.GetPronotedetailsMaterialByHeaderId(pronoteHeader);
            return pronoteHeader;
        }
        /// <summary>
        /// Insert a PronoteHeader.
        /// </summary>
        public void Insert(Model.PronoteHeader pronoteHeader)
        {
            //
            // todo:add other logic here
            //
            Validate(pronoteHeader);
            try
            {
                pronoteHeader.InsertTime = DateTime.Now;
                pronoteHeader.UpdateTime = DateTime.Now;
                if (pronoteHeader.Employee0 != null)
                    pronoteHeader.Employee0Id = pronoteHeader.Employee0.EmployeeId;
                if (pronoteHeader.Employee1 != null)
                    pronoteHeader.Employee1Id = pronoteHeader.Employee1.EmployeeId;
                if (pronoteHeader.Employee2 != null)
                    pronoteHeader.Employee2Id = pronoteHeader.Employee2.EmployeeId;

                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pronoteHeader.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pronoteHeader.InsertTime.Value.Year, pronoteHeader.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pronoteHeader.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pronoteHeader);
                Model.MRSdetails mrsDal = new BL.MRSdetailsManager().Get(pronoteHeader.MRSdetailsId);
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
                    new BL.MRSdetailsManager().Update(mrsDal);
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

                foreach (Model.PronotedetailsMaterial proMaterial in pronoteHeader.DetailsMaterial)
                {
                    if (proMaterial.Product == null || string.IsNullOrEmpty(proMaterial.Product.ProductId))
                        continue;
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
        public void UpdateMRSHeaderFlag(Model.MRSHeader mRSHeader)
        {
            int flag = 0;
            IList<Model.MRSdetails> list = new BL.MRSdetailsManager().Select(mRSHeader);
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

        /// <summary>
        /// Update a PronoteHeader.
        /// </summary>
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
                    this.Delete(pronoteHeader);                  
                    pronoteHeader.UpdateTime = DateTime.Now;
                    //if (pronoteHeader.Employee0 != null)
                    //    pronoteHeader.Employee0Id = pronoteHeader.Employee0.EmployeeId;
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

                    accessor.Insert(pronoteHeader);
                    Model.MRSdetails mrsDal = new BL.MRSdetailsManager().Get(pronoteHeader.MRSdetailsId);
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
                        new BL.MRSdetailsManager().Update(mrsDal);
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

                    foreach (Model.PronotedetailsMaterial proMaterial in pronoteHeader.DetailsMaterial)
                    {
                        if (proMaterial.Product == null || string.IsNullOrEmpty(proMaterial.Product.ProductId))
                            continue;
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
        public IList<Book.Model.PronoteHeader> GetByDate(DateTime startDate, DateTime endDate)
        {
            return accessor.GetByDate(startDate,endDate);
        }
        public IList<Book.Model.PronoteHeader> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd)
        {
            return accessor.Select(customerStart, customerEnd, dateStart, dateEnd);
        }
    }
}

