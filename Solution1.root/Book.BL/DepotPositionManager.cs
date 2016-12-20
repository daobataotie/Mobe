//------------------------------------------------------------------------------
//
// file name：DepotPositionManager.cs
// author: peidun
// create date：2009-07-24 11:18:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.DepotPosition.
    /// </summary>
    public partial class DepotPositionManager:BaseManager
    {
		
		/// <summary>
		/// Delete DepotPosition by primary key.
		/// </summary>
		public void Delete(string depotPositionID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(depotPositionID);
		}
        /// <summary>
        /// Delete DepotPosition by primary key.
        /// </summary>
        public void Delete(Model.DepotPosition depotpositon)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(depotpositon.DepotPositionId);
        } 

		/// <summary>
		/// Insert a DepotPosition.
		/// </summary>
        public void Insert(Model.DepotPosition depotPosition)
        {
			//
			// todo:add other logic here
			//

            this.Validate(depotPosition);
            if (this.Exists(depotPosition.Id))
            {
                throw new Helper.InvalidValueException(Model.DepotPosition.PROPERTY_ID);
            }
            depotPosition.DepotPositionId = Guid.NewGuid().ToString();
            depotPosition.DepotId = depotPosition.DepotId;          
            depotPosition.InsertTime = DateTime.Now;
            accessor.Insert(depotPosition);
        }
		
		/// <summary>
		/// Update a DepotPosition.
		/// </summary>
        public void Update(Model.DepotPosition depotPosition)
        {
			//
			// todo: add other logic here.
			//
            this.Validate(depotPosition);
            if (this.ExistsExcept(depotPosition))
            {
                throw new Helper.InvalidValueException(Model.DepotPosition.PROPERTY_ID);
            }
            depotPosition.DepotId = depotPosition.Depot.DepotId;
            depotPosition.UpdateTime = DateTime.Now;
            accessor.Update(depotPosition);
        }

        private void Validate(Model.DepotPosition depotPosition)
        {
            if (string.IsNullOrEmpty(depotPosition.Id))
            {
                throw new Helper.RequireValueException(Model.DepotPosition.PROPERTY_ID);
            }
            if (depotPosition.DepotId == null)
            {
                throw new Helper.RequireValueException(Model.DepotPosition.PROPERTY_DEPOTID);
            }       
        }

        public IList<Model.DepotPosition> Select(Book.Model.Depot depot)
        {
            return accessor.Select(depot);
        }
        public IList<Book.Model.DepotPosition> Select(string depotId)
        {
            return accessor.Select(depotId);
        }
        /// <summary>
        /// 根据货位查询库房编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
      

        //protected override string GetInvoiceKind()
        //{
        //    return "DepotPosition";
        //}

        //protected override string GetSettingId()
        //{
        //    return "DepotPositionRule";
        //}

        public bool existsInsertName(string name, Model.Depot depot)
        {
            return accessor.existsInsertName(name, depot);
        }
        public IList<Model.DepotPosition> GetDepotPositionsByDepotAndProduct(string ProductId, string DepotId)
        {
            return accessor.GetDepotPositionsByDepotAndProduct(ProductId, DepotId);
        }
        /// <summary>
        /// 获取库房下货位 及单个商品库存
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="DepotId"></param>
        /// <returns></returns>
        public IList<Model.DepotPosition> GetStockByDepotAndProduct(string ProductId, string DepotId)
        {
            return accessor.GetStockByDepotAndProduct(ProductId, DepotId);
        }
    }
}

