//------------------------------------------------------------------------------
//
// file name：SettingManager.cs
// author: peidun
// create date：2008/6/24 16:47:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

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
            this.Validate(setting);
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
            this.Validate(setting);
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

        public IList<Book.Model.Setting> SelectTagOrderDefault(string tag)
        {
            return accessor.SelectTagOrderDefault(tag);
        }

        public IList<Book.Model.Setting> SelectByName(string name)
        {
            return accessor.SelectByName(name);
        }

        public void DeleteByTag(string tag)
        {
            accessor.DeleteByTag(tag);
        }

        public void Validate(Model.Setting setting)
        {
            if (setting.SettingTags == "BOX")
            {
                if (setting.IdNO == "")
                    throw new Helper.RequireValueException(Model.Setting.PRO_IdNO);
                if (setting.Blong <= 0)
                    throw new Helper.InvalidValueException(Model.Setting.PRO_Blong);
                if (setting.BWidth <= 0)
                    throw new Helper.InvalidValueException(Model.Setting.PRO_BWidth);
                if (setting.BHeight <= 0)
                    throw new Helper.InvalidValueException(Model.Setting.PRO_BHeight);
                IList<Model.Setting> list = accessor.SelectByIdNo(setting.IdNO);
                if (list != null && list.Count > 0 && list[0].IdNO == setting.IdNO && list[0].SettingId != setting.SettingId)
                    throw new Helper.InvalidValueException(Model.Setting.PRO_IdNO);
            }
        }
    }
}

