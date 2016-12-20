//------------------------------------------------------------------------------
//
// file name：PronotePackageManager.cs
// author: mayanjun
// create date：2011-07-20 16:57:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PronotePackage.
    /// </summary>
    public partial class PronotePackageManager
    {
		
		/// <summary>
		/// Delete PronotePackage by primary key.
		/// </summary>
		public void Delete(string pronotePackageId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pronotePackageId);
		}

		/// <summary>
		/// Insert a PronotePackage.
		/// </summary>
        public void Insert(Model.PronotePackage pronotePackage)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pronotePackage);
        }
		
		/// <summary>
		/// Update a PronotePackage.
		/// </summary>
        public void Update(Model.PronotePackage pronotePackage)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(pronotePackage);
        }

        public void UpdateData(DataTable pronotePackage)
        {
            accessor.UpdateData(pronotePackage);
        }

        public DataSet GetDataTable(DateTime date)
        {
            return accessor.GetDataTable(date);
        }


        public IList<Model.PronotePackage> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }
    }
}

