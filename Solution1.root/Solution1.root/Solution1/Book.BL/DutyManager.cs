//------------------------------------------------------------------------------
//
// file name：DutyManager.cs
// author: peidun
// create date：2008-11-24 11:10:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Duty.
    /// </summary>
    public partial class DutyManager : BaseManager
    {
		
		/// <summary>
		/// Delete Duty by primary key.
		/// </summary>
		public void Delete(string dutyId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(dutyId);
		}
        public void Delete(Model.Duty duty)
        {
            //
            // todo:add other logic here
            //
            this.Delete(duty.DutyId);
        }
		/// <summary>
		/// Insert a Duty.
		/// </summary>
        public void Insert(Model.Duty duty)
        {
            Validate(duty);
            duty.InsertTime = DateTime.Now; 
            duty.DutyId = Guid.NewGuid().ToString();
            accessor.Insert(duty);
        }
        private void Validate(Model.Duty duty)
        {
            if (string.IsNullOrEmpty(duty.DutyName))
            {
                throw new Helper.RequireValueException(Model.Duty.PROPERTY_DUTYNAME);
            }

            if (accessor.IsExistsName(duty.DutyId, duty.DutyName))
            {
                throw new Helper.InvalidValueException(Model.Duty.PROPERTY_DUTYNAME);
            }
        }
		/// <summary>
		/// Update a Duty.
		/// </summary>
        public void Update(Model.Duty duty)
        {
            Validate(duty);
            duty.UpdateTime = DateTime.Now; ;
            accessor.Update(duty);
        }

        public void Update(IList<Model.Duty> detail)
        {
            foreach (Model.Duty duty in detail)
            {
                if (string.IsNullOrEmpty(duty.DutyName))
                {
                    throw new Helper.RequireValueException(Model.Duty.PROPERTY_DUTYNAME);
                }

                if (IsExistsName(duty.DutyId, duty.DutyName))
                {
                    throw new Helper.InvalidValueException(Model.Duty.PROPERTY_DUTYNAME);
                }
            }

            foreach (Model.Duty duty in detail)
            {
                if (accessor.ExistsPrimary(duty.Id))
                {
                    duty.UpdateTime = DateTime.Now;
                    accessor.Update(duty);
                }
                else
                {
                    this.Insert(duty);
                }
            }
        }

        public DataSet  SelectNoModel()
        {
            return accessor.SelectNoModel();
        }
        public void UpdateDataTable(DataTable accounts)
        {
            accessor.UpdateDataTable(accounts);
        }

        private bool IsExistsName(string id, string name)
        {
            return accessor.IsExistsName(id, name);
        }
    }
}

