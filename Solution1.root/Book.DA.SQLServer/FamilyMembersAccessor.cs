//------------------------------------------------------------------------------
//
// file name：FamilyMembersAccessor.cs
// author: peidun
// create date：2009-09-02 上午 10:38:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of FamilyMembers
    /// </summary>
    public partial class FamilyMembersAccessor : EntityAccessor, IFamilyMembersAccessor
    {
        public IList<Book.Model.FamilyMembers> Select(Book.Model.Employee emp)
        {
            if (emp == null)
                return (IList<Model.FamilyMembers>)new List<Model.FamilyMembers>();
            return sqlmapper.QueryForList<Model.FamilyMembers>("FamilyMembers.select_emp_empid", emp.EmployeeId);
        }

        public void DeleteByEmployeeId(string Employeeid)
        {
            sqlmapper.Delete("FamilyMembers.DeleteByEmployeeId", Employeeid);
        }
    }
}
