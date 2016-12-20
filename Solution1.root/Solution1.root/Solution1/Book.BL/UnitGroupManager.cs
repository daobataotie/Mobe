//------------------------------------------------------------------------------
//
// file name：UnitGroupManager.cs
// author: peidun
// create date：2009-08-03 9:37:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.UnitGroup.
    /// </summary>
    public partial class UnitGroupManager : BaseManager
    {
        private static readonly DA.IProductUnitAccessor ProductUnitAccessor = (DA.IProductUnitAccessor)Accessors.Get("ProductUnitAccessor");

        /// <summary>
        /// Delete UnitGroup by primary key.
        /// </summary>
        public void Delete(string unitGroupId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(unitGroupId);
        }
        public void Delete(Model.UnitGroup unitGroup)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(unitGroup.UnitGroupId);
        }
        public Model.UnitGroup GetDetails(string unitGroupId)
        {
            Model.UnitGroup unitGroup = accessor.Get(unitGroupId);
            unitGroup.Details = ProductUnitAccessor.SelectByGroup(unitGroup);
            return unitGroup;
        }
        /// <summary>
        /// Insert a UnitGroup.
        /// </summary>
        public void Insert(Model.UnitGroup unitGroup)
        {
            //
            // todo:add other logic here
            //            
            Validate(unitGroup);
            if (accessor.existsInsertName(unitGroup.UnitGroupName))
            {
                throw new Helper.InvalidValueException(Model.UnitGroup.PROPERTY_UNITGROUPNAME);     
             }
           // unitGroup.UnitGroupId = Guid.NewGuid().ToString();
            try
            {
                unitGroup.InsertTime = DateTime.Now;
                unitGroup.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, unitGroup.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, unitGroup.InsertTime.Value.Year, unitGroup.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, unitGroup.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(unitGroup);
                if (unitGroup.Details != null )
                {
                    foreach (Model.ProductUnit productUnit in unitGroup.Details)
                    {
                        if (string.IsNullOrEmpty(productUnit.CnName)) continue;
                        if (ProductUnitAccessor.existsInsertName(productUnit.CnName,unitGroup))
                        {
                            throw new Helper.InvalidValueException(Model.ProductUnit.PROPERTY_CNNAME);
                        }
                        productUnit.UnitGroupId = unitGroup.UnitGroupId;
                        ProductUnitAccessor.Insert(productUnit);
                    }
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
        /// Update a UnitGroup.
        /// </summary>
        public void Update(Model.UnitGroup unitGroup)
        {
            //
            // todo: add other logic here.
            //
            Validate(unitGroup);
            if(accessor.existsUpdateName(unitGroup.UnitGroupName,unitGroup.UnitGroupId))

            {
                throw new Helper.InvalidValueException(Model.UnitGroup.PROPERTY_UNITGROUPNAME);
            }

             unitGroup.UpdateTime = DateTime.Now;
             accessor.Update(unitGroup);
            if (unitGroup.Details != null)
            {
                foreach (Model.ProductUnit productUnit in unitGroup.Details)
                {
                    if (string.IsNullOrEmpty(productUnit.CnName)) continue;
  
                    if ( ProductUnitAccessor.ExistsPrimary(productUnit.ProductUnitId))
                    {

                        if (ProductUnitAccessor.existsUpdateName(productUnit.CnName, productUnit.ProductUnitId, unitGroup))
                        {
                        throw new Helper.InvalidValueException(Model.ProductUnit.PROPERTY_CNNAME);
                        }
                         productUnit.UnitGroupId = unitGroup.UnitGroupId;
                         ProductUnitAccessor.Update(productUnit);
                     }

                   else
                    {
                        productUnit.UnitGroupId = unitGroup.UnitGroupId;
                        ProductUnitAccessor.Insert(productUnit);
                           
                    }               
                    

                }
            }
        }

        private void Validate(Model.UnitGroup unitGroup)
        {
            //if (string.IsNullOrEmpty(unitGroup.Id))
            //{
            //    throw new Helper.RequireValueException(Model.UnitGroup.PROPERTY_ID);
            //}

            if (string.IsNullOrEmpty(unitGroup.UnitGroupName))
            {
                throw new Helper.RequireValueException(Model.UnitGroup.PROPERTY_UNITGROUPNAME);
            }

            if (string.IsNullOrEmpty(unitGroup.UnitGroupType))
            {
                throw new Helper.RequireValueException(Model.UnitGroup.PROPERTY_UNITGROUPTYPE);
            }            
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "UnitGroup";
        //}

        //protected override string GetSettingId()
        //{
        //    return "UnitGroupRule";
        //}
    }
}
