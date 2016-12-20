//------------------------------------------------------------------------------
//
// file name：ConveyanceMethodManager.cs
// author: mayanjun
// create date：2010-8-9 15:00:23
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ConveyanceMethod.
    /// </summary>
    public partial class ConveyanceMethodManager
    {

        /// <summary>
        /// Delete ConveyanceMethod by primary key.
        /// </summary>
        public void Delete(string conveyanceMethodId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(conveyanceMethodId);
        }

        /// <summary>
        /// Insert a ConveyanceMethod.
        /// </summary>
        public void Insert(Model.ConveyanceMethod conveyanceMethod)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(conveyanceMethod);
        }

        /// <summary>
        /// Update a ConveyanceMethod.
        /// </summary>
        public void Update(Model.ConveyanceMethod conveyanceMethod)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(conveyanceMethod);
        }

        public void Update(IList<Model.ConveyanceMethod> detail)
        {
            foreach (Model.ConveyanceMethod convery in detail)
            {
                if (string.IsNullOrEmpty(convery.ConveyanceMethodName))
                {
                    throw new Helper.RequireValueException(Model.ConveyanceMethod.PROPERTY_CONVEYANCEMETHODNAME);
                }

                if (accessor.IsExists(convery))
                {
                    throw new Helper.InvalidValueException(Model.ConveyanceMethod.PROPERTY_CONVEYANCEMETHODNAME);
                }
            }

            foreach (Model.ConveyanceMethod convery in detail)
            {
                if (accessor.ExistsPrimary(convery.ConveyanceMethodId))
                {
                    accessor.Update(convery);
                }
                else
                {

                    this.Insert(convery);
                }
            }
        }
    }
}

