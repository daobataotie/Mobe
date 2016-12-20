//------------------------------------------------------------------------------
//
// file name：PersonworkManager.cs
// author: peidun
// create date：2009-11-26 15:16:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Personwork.
    /// </summary>
    public partial class PersonworkManager
    {
		
		/// <summary>
		/// Delete Personwork by primary key.
		/// </summary>
		public void Delete(string personworkID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(personworkID);
		}

		/// <summary>
		/// Insert a Personwork.
		/// </summary>
        public void Insert(Model.Personwork personwork)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(personwork);
        }
		
		/// <summary>
		/// Update a Personwork.
		/// </summary>
        public void Update(Model.Personwork personwork)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(personwork);
        }

        //public Model.Personwork GetPersonwork(Model.wfrecord wfr)
        //{
        //   return  accessor.GetPersonwork(wfr);
        
        //}


    }
}

