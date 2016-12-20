//------------------------------------------------------------------------------
//
// file name：TablesManager.cs
// author: peidun
// create date：2009-12-11 14:53:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Tables.
    /// </summary>
    public partial class TablesManager:BaseManager
    {
		
		/// <summary>
		/// Delete Tables by primary key.
		/// </summary>
		public void Delete(string tablesID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(tablesID);
		}
        public void Delete(Model.Tables   tables)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(tables.TablesID);
        }

		/// <summary>
		/// Insert a Tables.
		/// </summary>
        public void Insert(Model.Tables tables)
        {
			//
			// todo:add other logic here
            //
            if (string.IsNullOrEmpty(tables.Tablename))
            {
                throw new Helper.RequireValueException(Model.Tables.PROPERTY_TABLENAME);
            }
            tables.TablesID = Guid.NewGuid().ToString();
            tables.InsertTime = DateTime.Now;
            accessor.Insert(tables);
        }
		
		/// <summary>
		/// Update a Tables.
		/// </summary>
        public void Update(Model.Tables tables)
        {
			//
			// todo: add other logic here.
			//
            if (string.IsNullOrEmpty(tables.Tablename))
            {
                throw new Helper.RequireValueException(Model.Tables.PROPERTY_TABLENAME);
            }
            tables.UpdateTime = DateTime.Now;
            accessor.Update(tables);
        }


        public Model.Tables GetIDbyname(string tablename)
        {
           return  accessor.GetIDbyname(tablename);
        }
        //public Model.Tables GetIDbycode(string tablename)
        //{
        //    return accessor.GetIDbycode(tablename);
        //}

    }
}

