//------------------------------------------------------------------------------
//
// file name：AtProjectManager.cs
// author: mayanjun
// create date：2010-11-15 10:11:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtProject.
    /// </summary>
    public partial class AtProjectManager
    {
		
		/// <summary>
		/// Delete AtProject by primary key.
		/// </summary>
		public void Delete(string projectId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(projectId);
		}

		/// <summary>
		/// Insert a AtProject.
		/// </summary>
        public void Insert(Model.AtProject atProject)
        {
			//
			// todo:add other logic here
			//
            Validate(atProject);
            atProject.InsertTime = DateTime.Now;
            atProject.ProjectId = Guid.NewGuid().ToString();
            accessor.Insert(atProject);
        }
		
		/// <summary>
		/// Update a AtProject.
		/// </summary>
        public void Update(Model.AtProject atProject)
        {
			//
			// todo: add other logic here.
			//
            Validate(atProject);
            atProject.UpdateTime = DateTime.Now;
            accessor.Update(atProject);
        }
        private void Validate(Model.AtProject atProject)
        {
            if (string.IsNullOrEmpty(atProject.Id))
            {
                throw new Helper.RequireValueException(Model.AtProject.PRO_Id);
            }
            if (string.IsNullOrEmpty(atProject.ProjectName))
            {
                throw new Helper.RequireValueException(Model.AtProject.PRO_ProjectName);
            }
        }
    }
}

