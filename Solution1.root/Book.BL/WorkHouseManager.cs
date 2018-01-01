//------------------------------------------------------------------------------
//
// file name：WorkHouseManager.cs
// author: peidun
// create date：2009-11-18 15:33:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.WorkHouse.
    /// </summary>
    public partial class WorkHouseManager : BaseManager
    {
        public void Delete(string workhouseid)
        {
            accessor.Delete(workhouseid);
        }

        public void Insert(Model.WorkHouse workHouse)
        {
            Validate(workHouse);
            workHouse.InsertTime = DateTime.Now;
            workHouse.UpdateTime = DateTime.Now;
            workHouse.WorkHouseId = Guid.NewGuid().ToString();

            TiGuiExists(workHouse);

            string invoiceKind = this.GetInvoiceKind().ToLower();
            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, workHouse.InsertTime.Value.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, workHouse.InsertTime.Value.Year, workHouse.InsertTime.Value.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, workHouse.InsertTime.Value.ToString("yyyy-MM-dd"));
            string sequencekey = string.Format(invoiceKind);

            SequenceManager.Increment(sequencekey_y);
            SequenceManager.Increment(sequencekey_m);
            SequenceManager.Increment(sequencekey_d);
            SequenceManager.Increment(sequencekey);

            accessor.Insert(workHouse);
        }

        public void Update(Model.WorkHouse workHouse)
        {
            Validate(workHouse);
            workHouse.UpdateTime = DateTime.Now;
            accessor.Update(workHouse);
        }

        private void Validate(Model.WorkHouse workHouse)
        {
            if (string.IsNullOrEmpty(workHouse.Workhousename))
                throw new Helper.RequireValueException(Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            //if (string.IsNullOrEmpty(workHouse.WorkhouseCode))
            //    throw new Helper.RequireValueException(Model.WorkHouse.PROPERTY_WORKHOUSECODE);
        }

        private void TiGuiExists(Model.WorkHouse model)
        {
            if (this.ExistsWorkHouseCode(model.WorkhouseCode))
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
                model.WorkhouseCode = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        private bool ExistsWorkHouseCode(string WorkHouseCode)
        {
            return accessor.ExistsWorkHouseCode(WorkHouseCode);
        }

        protected override string GetSettingId()
        {
            return "whRule";
        }

        protected override string GetInvoiceKind()
        {
            return "WH";
        }

        public string SelectWorkHouseIdByName(string name)
        {
            return accessor.SelectWorkHouseIdByName(name);
        }
    }
}

