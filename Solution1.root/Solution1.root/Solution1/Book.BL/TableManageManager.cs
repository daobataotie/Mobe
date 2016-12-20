//------------------------------------------------------------------------------
//
// file name：TableManageManager.cs
// author: peidun
// create date：2009-11-23 10:26:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.TableManage.
    /// </summary>
    public partial class TableManageManager:BaseManager
    {
		
		/// <summary>
		/// Delete TableManage by primary key.
		/// </summary>
		public void Delete(string tableID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(tableID);
		}

		/// <summary>
		/// Insert a TableManage.
		/// </summary>
        public void Insert(Model.TableManage tableManage)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(tableManage);
        }
		
		/// <summary>
		/// Update a TableManage.
		/// </summary>
        public void Update(Model.TableManage tableManage)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(tableManage);
        }


        public Model.TableManage GetIDbyname(string tablename)
        {
           return   accessor.GetIDbyname(tablename);
        }

        public Model.TableManage GetIDbyDBname(string dbtablename)
        {
            return accessor.GetIDbyDBname(dbtablename);
        }
    }
}

