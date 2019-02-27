//------------------------------------------------------------------------------
//
// file name：ProductUnitManager.cs
// author: peidun
// create date：2009-4-25 13:55:04
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductUnit.
    /// </summary>
    public partial class ProductUnitManager : BaseManager
    {

        /// <summary>
        /// Delete ProductUnit by primary key.
        /// </summary>
        public void Delete(string unitId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(unitId);
        }
        public void Delete(Model.ProductUnit productUnit)
        {
            //
            // todo:add other logic here
            //
            this.Delete(productUnit.ProductUnitId);
        }

        /// <summary>
        /// Insert a ProductUnit.
        /// </summary>
        public void Insert(Model.ProductUnit productUnit)
        {
            //
            // todo:add other logic here
            //

            // this.Validate(productUnit);

            //if (this.Exists(productUnit.Id))
            //{
            //    throw new Helper.InvalidValueException(Model.ProductUnit.PROPERTY_ID);
            //}
            //if (this.GetByTw(productUnit.CnName) != null)
            //{
            //    throw new Helper.InvalidValueException(Model.ProductUnit.PROPERTY_CNNAME);
            //}

            //if (this.HasRows(productUnit.ProductUnitId))
            //{
            //    throw new Helper.InvalidValueException(Model.ProductUnit.PROPERTY_PRODUCTUNITID);
            //}

            //productUnit.ProductUnitId = Guid.NewGuid().ToString();
            //productUnit.UnitGroupId = productUnit.UnitGroup.UnitGroupId;
            productUnit.InsertTime = DateTime.Now;
            productUnit.UpdateTime = DateTime.Now;
            if (productUnit.IsMainUnit != null && productUnit.IsMainUnit.Value)
            {
                this.Updates(productUnit.UnitGroup);
            }
            accessor.Insert(productUnit);
        }

        private void Validate(Model.ProductUnit unit)
        {
            if (string.IsNullOrEmpty(unit.Id))
            {
                throw new Helper.RequireValueException(Model.ProductUnit.PROPERTY_ID);
            }

            if (string.IsNullOrEmpty(unit.CnName))
            {
                throw new Helper.RequireValueException(Model.ProductUnit.PROPERTY_CNNAME);
            }

            if (string.IsNullOrEmpty(unit.UnitBarCode))
            {
                throw new Helper.RequireValueException(Model.ProductUnit.PROPERTY_UNITBARCODE);
            }
            if (unit.UnitGroup == null)
            {
                throw new Helper.RequireValueException(Model.ProductUnit.PROPERTY_UNITGROUPID);
            }
        }

        private Model.ProductUnit GetByTw(string unit)
        {
            return accessor.GetByTw(unit);
        }
        /// <summary>
        /// Update a ProductUnit.
        /// </summary>
        public void Update(Model.ProductUnit productUnit)
        {
            //
            // todo: add other logic here.
            //
            this.Validate(productUnit);

            if (this.ExistsExcept(productUnit))
            {
                throw new Helper.InvalidValueException(Model.ProductUnit.PROPERTY_ID);
            }
            productUnit.UnitGroupId = productUnit.UnitGroup.UnitGroupId;
            productUnit.UpdateTime = DateTime.Now;

            if (productUnit.IsMainUnit != null && productUnit.IsMainUnit.Value)
            {
                this.Updates(productUnit.UnitGroup);
            }
            accessor.Update(productUnit);
        }

        /// <summary>
        /// 根据单位组，修改所有单位组成员主计量单位标志为false
        /// </summary>
        /// <param name="groupId"></param>
        public void Updates(Model.UnitGroup group)
        {
            //
            // todo: add other logic here.
            //
            accessor.updates(group);
        }


        /// <summary>
        /// 获取这个单位组编号的所有单位
        /// </summary>
        /// <param name="groupId">单位组编号</param>
        /// <returns>单位结合</returns>
        public IList<Model.ProductUnit> Select(string groupId)
        {
            return accessor.SelectByGroup(groupId);
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "ProductUnit";
        //}

        //protected override string GetSettingId()
        //{
        //    return "ProductUnitRule";
        //}
        /// <summary>
        /// 該函數為單位轉換-lyl
        /// </summary>
        /// <param name="groupunitId">單位組</param>
        /// <param name="unitid0">從起始單位</param>
        /// <param name="unitid1">到目標單位</param>
        /// <param name="unitid0Quantity">轉換數量</param>
        /// <returns></returns>
        public double? ConvertUnit(string groupunitId, string Fromunitid, string Tounitid, double FromQuantity)
        {
            if (Fromunitid == Tounitid)
                return FromQuantity;
            else
                return accessor.ConvertUnit(groupunitId, Fromunitid, Tounitid, FromQuantity);
        }

        public IList<string> SelectProductUnitGroup()
        {
            return accessor.SelectProductUnitGroup();
        }
    }
}

