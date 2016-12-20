//------------------------------------------------------------------------------
//
// file name：BGHandbookManager.cs
// author: mayanjun
// create date：2013-4-16 11:58:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbook.
    /// </summary>
    public partial class BGHandbookManager : BaseManager
    {
        private static readonly DA.IBGHandbookDetail1Accessor Detail1Accessor = (DA.IBGHandbookDetail1Accessor)Accessors.Get("BGHandbookDetail1Accessor");

        private static readonly DA.IBGHandbookDetail2Accessor Detail2Accessor = (DA.IBGHandbookDetail2Accessor)Accessors.Get("BGHandbookDetail2Accessor");
        /// <summary>
        /// Delete BGHandbook by primary key.
        /// </summary>
        public void Delete(string bGHandbookId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(bGHandbookId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        public void Delete(Model.BGHandbook model)
        {
            //
            // todo:add other logic here
            //
            this.Delete(model.BGHandbookId);
        }

        /// <summary>
        /// Insert a BGHandbook.
        /// </summary>
        public void Insert(Model.BGHandbook bGHandbook)
        {
            //
            // todo:add other logic here
            //

            bGHandbook.InsertTime = DateTime.Now;

            TiGuiExists(bGHandbook);

            string invoiceKind = this.GetInvoiceKind().ToLower();
            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, bGHandbook.BGHandbookDate.Value.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, bGHandbook.BGHandbookDate.Value.Year, bGHandbook.BGHandbookDate.Value.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, bGHandbook.BGHandbookDate.Value.ToString("yyyy-MM-dd"));
            string sequencekey = string.Format(invoiceKind);
            SequenceManager.Increment(sequencekey_y);
            SequenceManager.Increment(sequencekey_m);
            SequenceManager.Increment(sequencekey_d);
            SequenceManager.Increment(sequencekey);
            accessor.Insert(bGHandbook);
            addDetail(bGHandbook);





        }

        public void _insert(Model.BGHandbook bGHandbook)
        {

            try
            {
                BL.V.BeginTransaction();
                Insert(bGHandbook);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }




        private void TiGuiExists(Model.BGHandbook model)
        {
            if (this.ExistsPrimary(model.BGHandbookId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.BGHandbookDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.BGHandbookDate.Value.Year, model.BGHandbookDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.BGHandbookDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.BGHandbookId = this.GetId(model.BGHandbookDate.Value);
                TiGuiExists(model);
            }

        }
        private void addDetail(Model.BGHandbook model)
        {

            if (model.Detail1 != null)
                foreach (Model.BGHandbookDetail1 detail1 in model.Detail1)
                {

                    detail1.BGHandbookId = model.BGHandbookId;
                    Detail1Accessor.Insert(detail1);
                }
            if (model.Detail2 != null)
                foreach (Model.BGHandbookDetail2 detail2 in model.Detail2)
                {

                    detail2.BGHandbookId = model.BGHandbookId;
                    Detail2Accessor.Insert(detail2);
                }
        }

        public Model.BGHandbook GetDetails(string id)
        {
            Model.BGHandbook produceInDepot = accessor.Get(id);
            produceInDepot.Detail1 = Detail1Accessor.Select(id);
            produceInDepot.Detail2 = Detail2Accessor.Select(id);
            return produceInDepot;
        }
        /// <summary>
        /// Update a BGHandbook.
        /// </summary>
        public void Update(Model.BGHandbook bGHandbook)
        {
            //
            // todo: add other logic here.
            //
            //accessor.Update(bGHandbook);


            try
            {
                BL.V.BeginTransaction();
                bGHandbook.UpdateTime = DateTime.Now;
                accessor.Delete(bGHandbook.BGHandbookId);
                this.Insert(bGHandbook);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        protected override string GetSettingId()
        {
            return "bghandbookRule";
        }

        protected override string GetInvoiceKind()
        {
            return "BGH";
        }
        public IList<Book.Model.BGHandbook> Select(string id)
        {
            return accessor.Select(id);
        }

        public DataTable SelectIdGroupById()
        {
            return accessor.SelectIdGroupById();
        }

        public void UpdateIsEffect(string id, string effect)
        {
            accessor.UpdateIsEffect(id, effect);
        }

        public bool HasEffect(string bGHandBookId, string id)
        {
            return accessor.HasEffect(bGHandBookId, id);
        }
    }
}

