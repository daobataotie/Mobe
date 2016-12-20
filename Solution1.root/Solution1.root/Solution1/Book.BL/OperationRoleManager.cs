//------------------------------------------------------------------------------
//
// file name：OperationRoleManager.cs
// author: peidun
// create date：2009-09-17 上午 11:49:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.OperationRole.
    /// </summary>
    public partial class OperationRoleManager
    {

        /// <summary>
        /// Delete OperationRole by primary key.
        /// </summary>
        public void Delete(string primaryKey)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(primaryKey);
        }
        public void Delete(Model.Operators operators)
        {

            accessor.Delete(operators);
        }

        /// <summary>
        /// Insert a OperationRole.
        /// </summary>
        public void Insert(Model.OperationRole operationRole)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(operationRole);
        }

        /// <summary>
        /// Update a OperationRole.
        /// </summary>
        public void Update(Model.OperationRole operationRole)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(operationRole);
        }
        public IList<Model.OperationRole> Select(Model.Operators operatoes)
        {
            return accessor.Select(operatoes);
        }
        public void Update(IList<Model.OperationRole> operationRoles)
        {
            foreach (Model.OperationRole or in operationRoles)
            {
                this.Update(or);
            }
        }
    }
}
