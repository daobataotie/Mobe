//------------------------------------------------------------------------------
//
// file name：TechonlogyHeaderManager.cs
// author: peidun
// create date：2009-12-7 14:57:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.TechonlogyHeader.
    /// </summary>
    public partial class TechonlogyHeaderManager : BaseManager
    {
        ///<summary>
        /// Data accessor of dbo.TechonlogyHeader
        ///</summary>
        private static readonly DA.ITechnologydetailsAccessor accessorDetails = (DA.ITechnologydetailsAccessor)Accessors.Get("TechnologydetailsAccessor");


        /// <summary>
        /// Delete TechonlogyHeader by primary key.
        /// </summary>
        public void Delete(string techonlogyHeaderid)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(techonlogyHeaderid);
        }
        public void Delete(Model.TechonlogyHeader techonlogyHeader)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(techonlogyHeader.TechonlogyHeaderId);
        }
        /// <summary>
        /// Insert a TechonlogyHeader.
        /// </summary>
        public void Insert(Model.TechonlogyHeader techonlogyHeader)
        {
            //
            // todo:add other logic here
            //
            Validate(techonlogyHeader);

            if (this.Exists(techonlogyHeader.Id))
            {
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);
                techonlogyHeader.Id = this.GetId(DateTime.Now);
            }
            //if (this.Exists(techonlogyHeader.Id))
            //{
            //    throw new Helper.InvalidValueException(Model.TechonlogyHeader.PROPERTY_ID);
            //}
            try
            {
                techonlogyHeader.InsertTime = DateTime.Now;
                techonlogyHeader.UpdateTime = DateTime.Now;
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, techonlogyHeader.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, techonlogyHeader.InsertTime.Value.Year, techonlogyHeader.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, techonlogyHeader.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                IList<string> str = new List<string>();
                foreach (Model.Technologydetails detail in techonlogyHeader.detail)
                {
                    if (detail.TechnologydetailsNo == null) continue;
                    if (str.Contains(detail.TechnologydetailsNo))
                        throw new Helper.InvalidValueException(Model.Technologydetails.PROPERTY_TECHNOLOGYDETAILSNO);
                    str.Add(detail.TechnologydetailsNo);
                }
                BL.V.BeginTransaction();
               
                //techonlogyHeader.TechonlogyHeaderId = GetNewId();
                accessor.Insert(techonlogyHeader);
                foreach (Model.Technologydetails detail in techonlogyHeader.detail)
                {
                    if (string.IsNullOrEmpty(detail.ProceduresId)) continue;

                    if (string.IsNullOrEmpty(detail.TechnologydetailsNo))
                        throw new Helper.RequireValueException(Model.Technologydetails.PROPERTY_TECHNOLOGYDETAILSNO);
                    //if (accessorDetails.IsExists_TechnologydetailsNo(detail))
                    //    throw new Helper.InvalidValueException(Model.Technologydetails.PROPERTY_TECHNOLOGYDETAILSNO);
                    detail.TechonlogyHeaderId = techonlogyHeader.TechonlogyHeaderId;
                    accessorDetails.Insert(detail);
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
        /// Update a TechonlogyHeader.
        /// </summary>
        public void Update(Model.TechonlogyHeader techonlogyHeader)
        {
            //
            // todo: add other logic here.
            //
            Validate(techonlogyHeader);
            if (this.ExistsExcept(techonlogyHeader))
            {
                throw new Helper.InvalidValueException(Model.TechonlogyHeader.PROPERTY_ID);
            }

            //foreach (Model.Technologydetails detail in techonlogyHeader.detail)
            //{
            accessorDetails.Delete(techonlogyHeader);
            // }

            _update(techonlogyHeader);
        }

        void _update(Model.TechonlogyHeader header)
        {

            IList<string> str = new List<string>();
            foreach (Model.Technologydetails tecdetail in header.detail)
            {
                if (tecdetail.TechnologydetailsNo == null) continue;
                if (str.Contains(tecdetail.TechnologydetailsNo))
                    throw new Helper.RequireValueException(Model.Technologydetails.PROPERTY_TECHNOLOGYDETAILSNO);
                str.Add(tecdetail.TechnologydetailsNo);
            }
            header.UpdateTime = DateTime.Now;
            accessor.Update(header);



            foreach (Model.Technologydetails detail in header.detail)
            {
                if (string.IsNullOrEmpty(detail.ProceduresId)) continue;

                //if (accessorDetails.IsExists_TechnologydetailsNo(detail))
                //    throw new Helper.InvalidValueException(Model.Technologydetails.PROPERTY_TECHNOLOGYDETAILSNO);

                detail.TechonlogyHeaderId = header.TechonlogyHeaderId;
                accessorDetails.Insert(detail);
            }
        }

        private void Validate(Model.TechonlogyHeader techonlogyHeader)
        {
            if (string.IsNullOrEmpty(techonlogyHeader.Id))
            {
                throw new Helper.RequireValueException(Model.TechonlogyHeader.PROPERTY_ID);
            }
            if (string.IsNullOrEmpty(techonlogyHeader.TechonlogyHeadername))
            {
                throw new Helper.RequireValueException(Model.TechonlogyHeader.PROPERTY_TECHONLOGYHEADERNAME);
            }
        }
        public Model.TechonlogyHeader GetDetail(string TechonlogyHeaderId)
        {
            Model.TechonlogyHeader TechonlogyHeader = accessor.Get(TechonlogyHeaderId);
            if (TechonlogyHeader!=null)
            TechonlogyHeader.detail = accessorDetails.Select(TechonlogyHeader);
            return TechonlogyHeader;
        }

        protected override string GetSettingId()
        {
            return "TechonlogyRule";
        }

        protected override string GetInvoiceKind()
        {
            return "Techonlogy";
        }

        //public string GetNewId()
        //{
        //    string invoiceKind = this.GetInvoiceKind().ToLower();
        //    string sequencekey = string.Format(invoiceKind);
        //    string rule = Settings.Get(GetSettingId());
        //    if (string.IsNullOrEmpty(rule))
        //        return string.Empty;
        //    int sequenceval = SequenceManager.GetCurrentVal(invoiceKind);
        //    sequenceval++;
        //    string n1 = string.Format("{0:d1}", sequenceval);
        //    string n2 = string.Format("{0:d2}", sequenceval);
        //    string n3 = string.Format("{0:d3}", sequenceval);
        //    string n4 = string.Format("{0:d4}", sequenceval);
        //    string n5 = string.Format("{0:d5}", sequenceval);
        //    string n6 = string.Format("{0:d6}", sequenceval);
        //    string n7 = string.Format("{0:d7}", sequenceval);
        //    string n8 = string.Format("{0:d8}", sequenceval);
        //    string n9 = string.Format("{0:d9}", sequenceval);
        //    string n10 = string.Format("{0:d10}", sequenceval);

        //    return rule.Replace("{N1}", n1).Replace("{N2}", n2).Replace("{N3}", n3).Replace("{N4}", n4).Replace("{N5}", n5).Replace("{N6}", n6).Replace("{N7}", n7).Replace("{N8}", n8).Replace("{N9}", n9).Replace("{N10}", n10);
        //}

    }
}

