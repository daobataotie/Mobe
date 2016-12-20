//------------------------------------------------------------------------------
//
// file name：DepartmentManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Department.
    /// </summary>
    public partial class DepartmentManager : BaseManager
    {

        /// <summary>
        /// Delete Department by primary key.
        /// </summary>
        public void Delete(string departmentId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(departmentId);
        }
        public void Delete(Model.Department department)
        {
            accessor.Delete(department.DepartmentId);
        }
        /// <summary>
        /// Insert a Department.
        /// </summary>
        public void Insert(Model.Department department)
        {
            department.InsertTime = DateTime.Now;
            accessor.Insert(department);
        }
        /// <summary>
        /// Update a Department.
        /// </summary>
        public void Update(IList<Model.Department> detail)
        {
            foreach (Model.Department department in detail)
            {
                if (string.IsNullOrEmpty(department.DepartmentName))
                {
                    throw new Helper.RequireValueException(Model.Department.PROPERTY_DEPARTMENTNAME);
                }

                if (accessor.ExistsName(department.DepartmentName, department.DepartmentId))
                {
                    throw new Helper.InvalidValueException(Model.Department.PROPERTY_DEPARTMENTNAME);
                }
            }

            foreach (Model.Department department in detail)
            {
                if (accessor.ExistsPrimary(department.DepartmentId))
                {
                    department.UpdateTime = DateTime.Now;
                    accessor.Update(department);
                }
                else
                {
                    this.Insert(department);
                }
            }
        }
        public void SaveInfo(System.Data.DataTable Deport)
        {
            accessor.SaveInfo(Deport);
        }

        public System.Data.DataTable GetAll()
        {
            return accessor.GetAll();
        }
    }
}

