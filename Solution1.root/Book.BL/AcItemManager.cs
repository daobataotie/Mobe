//------------------------------------------------------------------------------
//
// file name：AcItemManager.cs
// author: mayanjun
// create date：2012-2-21 13:36:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcItem.
    /// </summary>
    public partial class AcItemManager : BaseManager
    {

        /// <summary>
        /// Delete AcItem by primary key.
        /// </summary>
        public void Delete(string acItemId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acItemId);
        }

        /// <summary>
        /// Insert a AcItem.
        /// </summary>
        public void Insert(Model.AcItem acItem)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(acItem);
        }

        /// <summary>
        /// Update a AcItem.
        /// </summary>
        public void Update(Model.AcItem acItem)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(acItem);
        }

        public void DeleteALL()
        {
            accessor.DeleteALL();
        }

        public void Insert(IList<Model.AcItem> list)
        {
            var qGroup = from Model.AcItem acitem in list
                         group acitem by acitem.ItemName;
            foreach (IGrouping<string, Model.AcItem> item in qGroup)
            {
                if (item.Count() > 1)
                    throw new Helper.InvalidValueException(Model.AcItem.PRO_ItemName);
            }

            if (list == null || list.Count == 0)
            {
                this.DeleteALL();
            }
            else
            {
                foreach (Model.AcItem ac in list)
                {
                    if (ac.mState)
                    {
                        this.Delete(ac.AcItemId);
                        this.Insert(ac);
                    }
                }
            }
        }

        public string SelectPriIdByName(string itemname)
        {
            return accessor.SelectPriIdByName(itemname);
        }
    }
}

