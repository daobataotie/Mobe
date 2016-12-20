//------------------------------------------------------------------------------
//
// file name：OperationManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Operation.
    /// </summary>
    public partial class OperationManager 
    {
		
		/// <summary>
		/// Delete Operation by primary key.
		/// </summary>
		public void Delete(string operationId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(operationId);
		}

        private void Validate(Model.Operation operation) 
        {
            if (string.IsNullOrEmpty(operation.OperationId)) 
            {
                throw new Helper.RequireValueException(Model.Operation.PRO_OperationId);
            }
            if(string.IsNullOrEmpty(operation.OperationName))
            {
                throw new Helper.RequireValueException(Model.Operation.PRO_OperationName);
            }
        }

		/// <summary>
		/// Insert a Operation.
		/// </summary>
        public void Insert(Model.Operation operation)
        {
			//
			// todo:add other logic here
			//
            Validate(operation);

            if (this.HasRows(operation.OperationId)) 
            {
                throw new Helper.InvalidValueException(Model.Operation.PRO_OperationId);
            }

            accessor.Insert(operation);
        }
		
		/// <summary>
		/// Update a Operation.
		/// </summary>
        public void Update(Model.Operation operation)
        {
			//
			// todo: add other logic here.
			//
            Validate(operation);
            accessor.Update(operation);
        }
        public IList<Model.Operation> Select_KeyTag0()
        {
            return accessor.Select_KeyTag0();
        }
        public IList<Model.Operation> Select_ByParent(string ParentId)
        {
            return accessor.Select_ByParent(ParentId);        
        }
        public string GetOperationNamebyTabel(string tableName)
        {
            return accessor.GetOperationNamebyTabel(tableName);
        }
      
    }
}

