//------------------------------------------------------------------------------
//
// file name：LeaveTypeManager.cs
// author: peidun
// create date：2010-2-6 10:33:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.LeaveType.
    /// </summary>
    public partial class LeaveTypeManager
    {
		
		/// <summary>
		/// Delete LeaveType by primary key.
		/// </summary>
		public void Delete(string leaveTypeId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(leaveTypeId);
		}

		/// <summary>
		/// Insert a LeaveType.
		/// </summary>
        public void Insert(Model.LeaveType leaveType)
        {
			//
			// todo:add other logic here
			//
            Validate(leaveType);
            accessor.Insert(leaveType);
        }
		
		/// <summary>
		/// Update a LeaveType.
		/// </summary>
        public void Update(Model.LeaveType leaveType)
        {
			//
			// todo: add other logic here.
			//
            Validate(leaveType);
            accessor.Update(leaveType);
        }
        public void Validate(Model.LeaveType leavetype)
        {
            if (string.IsNullOrEmpty(leavetype.LeaveTypeName))
            {
                throw new Helper.RequireValueException(Model.LeaveType.PROPERTY_LEAVETYPENAME);
            }
            
        }
        public  System.Data.DataSet SelectLeaveTypeInfo()
        {
            return accessor.SelectLeaveTypeInfo();
        }
        public  void SaveLeaveTypeInfo(System.Data.DataTable table)
        {
            accessor.SaveLeaveTypeInfo(table);
        }

        public bool IsExistsName(string id, string name)
        {
            return accessor.IsExitsName(id, name);
        }
    }
}

