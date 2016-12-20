//------------------------------------------------------------------------------
//
// file name：FamilyMembersManager.cs
// author: peidun
// create date：2009-09-02 上午 10:38:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.FamilyMembers.
    /// </summary>
    public partial class FamilyMembersManager
    {
		
		/// <summary>
		/// Delete FamilyMembers by primary key.
		/// </summary>
		public void Delete(string familyMembersId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(familyMembersId);
		}

		/// <summary>
		/// Insert a FamilyMembers.
		/// </summary>
        public void Insert(Model.FamilyMembers familyMembers)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(familyMembers);
        }
		
		/// <summary>
		/// Update a FamilyMembers.
		/// </summary>
        public void Update(Model.FamilyMembers familyMembers)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(familyMembers);
        }

        public void DeleteByEmployeeId(string Employeeid)
        {
            accessor.DeleteByEmployeeId(Employeeid);
        }
    }
}

