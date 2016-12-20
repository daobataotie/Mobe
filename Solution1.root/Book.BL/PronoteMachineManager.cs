//------------------------------------------------------------------------------
//
// file name：PronoteMachineManager.cs
// author: mayanjun
// create date：2010-9-16 9:27:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PronoteMachine.
    /// </summary>
    public partial class PronoteMachineManager
    {

        /// <summary>
        /// Delete PronoteMachine by primary key.
        /// </summary>
        public void Delete(string pronoteMachine)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pronoteMachine);
        }

        /// <summary>
        /// Insert a PronoteMachine.
        /// </summary>
        public void Insert(Model.PronoteMachine pronoteMachine)
        {
            pronoteMachine.PronoteMachineId = Guid.NewGuid().ToString();
            validate(pronoteMachine);
            accessor.Insert(pronoteMachine);
        }

        private void validate(Model.PronoteMachine pro)
        {
            if (string.IsNullOrEmpty(pro.Id))
                throw new Helper.RequireValueException(Model.PronoteMachine.PROPERTY_ID);
            if (string.IsNullOrEmpty(pro.PronoteMachineName))
                throw new Helper.RequireValueException(Model.PronoteMachine.PROPERTY_PRONOTEMACHINENAME);
            if (accessor.ExistsId(pro.Id, pro.PronoteMachineId))
                throw new Helper.InvalidValueException(Model.PronoteMachine.PROPERTY_ID);
            if (accessor.ExistsName(pro.PronoteMachineName, pro.PronoteMachineId))
                throw new Helper.InvalidValueException(Model.PronoteMachine.PROPERTY_PRONOTEMACHINENAME);
            if (string.IsNullOrEmpty(pro.WorkHouseId))
                throw new Helper.InvalidValueException(Model.PronoteMachine.PROPERTY_WORKHOUSEID);
        }

        /// <summary>
        /// Update a PronoteMachine.
        /// </summary>
        public void Update(Model.PronoteMachine pronoteMachine)
        {
            validate(pronoteMachine);
            accessor.Update(pronoteMachine);
        }

        public DataTable GetAll()
        {
            return accessor.GetAll();
        }

        public void Update(IList<Model.PronoteMachine> detail)
        {
            foreach (Model.PronoteMachine pronoteMachine in detail)
            {
                if (string.IsNullOrEmpty(pronoteMachine.PronoteMachineName))
                {
                    throw new Helper.RequireValueException(Model.PronoteMachine.PROPERTY_PRONOTEMACHINENAME);
                }

                if (accessor.ExistsName(pronoteMachine.PronoteMachineName, pronoteMachine.PronoteMachineId))
                {
                    throw new Helper.InvalidValueException(Model.PronoteMachine.PROPERTY_PRONOTEMACHINENAME);
                }
            }

            foreach (Model.PronoteMachine pronoteMachine in detail)
            {
                if (accessor.ExistsPrimary(pronoteMachine.PronoteMachineId))
                {
                    accessor.Update(pronoteMachine);
                }
                else
                {
                    this.Insert(pronoteMachine);
                }
            }
        }

        public void SaveInfo(System.Data.DataTable pronoteMachine)
        {
            accessor.SaveInfo(pronoteMachine);
        }

        public IList<Model.PronoteMachine> SelectMachineByProduresId(string ProduresId)
        {
            return accessor.SelectMachineByProduresId(ProduresId);
        }

        public IList<Model.PronoteMachine> GetPronoteMachineByPronoteProceduresDetailId(string PronoteProceduresDetailId)
        {
            return accessor.GetPronoteMachineByPronoteProceduresDetailId(PronoteProceduresDetailId);
        }
    }
}

