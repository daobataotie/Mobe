//------------------------------------------------------------------------------
//
// file name：SettingManager.cs
// author: peidun
// create date：2008/6/24 16:47:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Setting.
    /// </summary>
    public partial class SettingManager : BaseManager
    {
		
		/// <summary>
		/// Delete Setting by primary key.
		/// </summary>
		public void Delete(string settingId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(settingId);
		}

		/// <summary>
		/// Insert a Setting.
		/// </summary>
        public void Insert(Model.Setting setting)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(setting);
        }
		
		/// <summary>
		/// Update a Setting.
		/// </summary>
        public void Update(Model.Setting setting)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(setting);
        }

        public void Update(string settingId, string currentValue)
        {
            accessor.Update(settingId, currentValue);
        }

        public void UpdateImage(string settingId, byte[] images)
        {
            accessor.UpdateImage(settingId, images);
        }

        public IList<Model.Setting> Select(string tag)
        {
            return accessor.Select(tag);
        }

        public Book.Model.Setting selectimage(string tag)
        {
            return accessor.selectimage(tag);
        }
    }
}

