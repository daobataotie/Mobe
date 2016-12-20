//------------------------------------------------------------------------------
//
// file name：RoleManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Role.
    /// </summary>
    public partial class RoleManager : BaseManager
    {

        /// <summary>
        /// Delete Role by primary key.
        /// </summary>
        public void Delete(string roleId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(roleId);
        }

        void Validate(Model.Role role)
        {
           // if (string.IsNullOrEmpty(role.Id))
              //  throw new Helper.RequireValueException(Model.Role.PROPERTY_ID);

            if (string.IsNullOrEmpty(role.RoleName))
                throw new Helper.RequireValueException(Model.Role.PRO_RoleName);

            
        }

        public void Delete(Model.Role role)
        {
            this.Delete(role.RoleId);
        }
        /// <summary>
        /// Insert a Role.
        /// </summary>
        public void Insert(Model.Role role)
        {
            //
            // todo:add other logic here
            //
            Validate(role);
            if (this.Exists(role.Id))
            {
                throw new Helper.InvalidValueException(Model.Role.PRO_Id);
            }
            role.RoleId = Guid.NewGuid().ToString();
            role.InsertTime = DateTime.Now;
            accessor.Insert(role);            
        }

        /// <summary>
        /// Update a Role.
        /// </summary>
        public void Update(Model.Role role)
        {
            //
            // todo: add other logic here.
            //
            Validate(role);
            //if (this.ExistsExcept(role))
            //{
            //    throw new Helper.InvalidValueException(Model.Role.PROPERTY_ID);
            //}
            role.UpdateTime = DateTime.Now;
            accessor.Update(role);
        }

        //protected override string GetInvoiceKind()
        //{
        //    return base.GetInvoiceKind();
        //}

        //protected override string GetSettingId()
        //{
        //    return base.GetSettingId();
        //}

        public IList<Book.Model.Role> Select(string opid)
        {
            return accessor.Select(opid);
        }
    }
}

