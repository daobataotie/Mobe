//------------------------------------------------------------------------------
//
// file name：DepotManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Depot.
    /// </summary>
    public partial class DepotManager : BaseManager
    {
        private static readonly DA.IDepotPositionAccessor DepotPositionAccessor = (DA.IDepotPositionAccessor)Accessors.Get("DepotPositionAccessor");
        /// <summary>
        /// Delete Depot by primary key.
        /// </summary>
        public void Delete(string depotId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                accessor.Delete(depotId);
            }
            catch
            {
                throw new Helper.InvalidValueException("DeleteError");
            }
        }
        public void Delete(Model.Depot depot)
        {
            this.Delete(depot.DepotId);
        }
        /// <summary>
        /// Insert a Depot.
        /// </summary>
        public void Insert(Model.Depot depot)
        {
            //
            // todo:add other logic here
            //
            Validate(depot);
            if (this.Exists(depot.Id))
            {
                throw new Helper.InvalidValueException(Model.Depot.PROPERTY_ID);
            }
            try
            {
                BL.V.BeginTransaction();
                depot.InsertTime = DateTime.Now;
                accessor.Insert(depot);
                if (depot.Details != null)
                {
                    foreach (Model.DepotPosition dp in depot.Details)
                    {
                        if (string.IsNullOrEmpty(dp.Id))
                            throw new Helper.RequireValueException(Model.DepotPosition.PROPERTY_ID+"aa");
                        
                        if (DepotPositionAccessor.existsInsertName(dp.Id, depot))
                        {
                            throw new Helper.InvalidValueException(Model.DepotPosition.PROPERTY_ID+"aa");
                        }
                        dp.DepotPositionId = Guid.NewGuid().ToString();
                        dp.DepotId = depot.DepotId;
                        DepotPositionAccessor.Insert(dp);
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


        private void Validate(Model.Depot depot)
        {
            if (string.IsNullOrEmpty(depot.Id))
            {
                throw new Helper.RequireValueException(Model.Depot.PROPERTY_ID);
            }
            
            
            //if (string.IsNullOrEmpty(depot.))
            //{
            //    throw new Helper.RequireValueException(Model.Depot.PROPERTY_DEPOTNAME);
            //}
        }

        /// <summary>
        /// Update a Depot.
        /// </summary>
        public void Update(Model.Depot depot)
        {
            //
            // todo: add other logic here.
            //
            Validate(depot);
            if (this.ExistsExcept(depot))
            {
                throw new Helper.InvalidValueException(Model.Depot.PROPERTY_ID);
            }
            
            depot.UpdateTime = DateTime.Now;
            accessor.Update(depot);
            if (depot.Details != null)
            {
                List<string> str=new List<string>();
                foreach (Model.DepotPosition dp in depot.Details)
                {
                    if (string.IsNullOrEmpty(dp.Id))
                        throw new Helper.RequireValueException(Model.DepotPosition.PROPERTY_ID + "aa");
                    if (DepotPositionAccessor.existsInsertName(dp.Id,depot))
                    {
                        if (DepotPositionAccessor.ExistsExcept(dp))
                        {
                            throw new Helper.InvalidValueException(Model.DepotPosition.PROPERTY_ID + "aa");
                        }
                        dp.DepotId = depot.DepotId;
                        DepotPositionAccessor.Update(dp);
                    }
                    else
                    {
                      
                        dp.DepotId = depot.DepotId;
                        DepotPositionAccessor.Insert(dp);

                    }
                    str.Add(dp.DepotPositionId);
                }

                foreach(Model.DepotPosition DepotPosition in DepotPositionAccessor.Select(depot))
                {
                    if (!str.Contains(DepotPosition.DepotPositionId))
                        DepotPositionAccessor.Delete(DepotPosition.DepotPositionId);
                }
            }
        }
        public Model.Depot SelectByDepotPosition(string id)
        {
            return accessor.SelectByDepotPosition(id);
        }
        //protected override string GetInvoiceKind()
        //{
        //    return "Depot";
        //}

        //protected override string GetSettingId()
        //{
        //    return "DepotRule";
        //}

        public Model.Depot SelectByDepot(string id)
        {
            return accessor.SelectByDepot(id);
        }

        public Model.Depot GetDetails(string depotid)
        {
            Model.Depot depot = accessor.Get(depotid);
            depot.Details = DepotPositionAccessor.SelectByDepot(depot);
            return depot;
        }
    }
}

