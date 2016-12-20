//------------------------------------------------------------------------------
//
// file name:SettingAccessor.cs
// author: peidun
// create date:2008/6/24 16:47:35
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
    /// Data accessor of Setting
    /// </summary>
    public partial class SettingAccessor : EntityAccessor, ISettingAccessor
    {
        #region ISettingAccessor Members

        public Book.Model.Setting selectimage(string tag)
        {
            return sqlmapper.QueryForObject<Model.Setting>("Setting.select_by_piclogo", tag);
        }

        public void Update(string settingId, string currentValue)
        {
            Hashtable paras = new Hashtable();
            paras.Add("Id", settingId);
            paras.Add("Value", currentValue);
            sqlmapper.Update("Setting.update_by_id_value", paras);
        }

        public void UpdateImage(string settingId, byte[] images)
        {
            Hashtable paras = new Hashtable();
            paras.Add("Id", settingId);
            paras.Add("Value", images);
            sqlmapper.Update("Setting.update_by_piclogo_value", paras);
        }

        public IList<Book.Model.Setting> Select(string tag)
        {
            return sqlmapper.QueryForList<Model.Setting>("Setting.select_by_tag", tag);
        }

        public IList<Book.Model.Setting> SelectTagOrderDefault(string tag)
        {
            return sqlmapper.QueryForList<Model.Setting>("Setting.select_by_tagOrderDefault", tag);
        }

        public IList<Book.Model.Setting> SelectByName(string name)
        {
            return sqlmapper.QueryForList<Model.Setting>("Setting.select_by_Name", name);
        }

        public void DeleteByTag(string tag)
        {
            sqlmapper.Delete("Setting.deleteByTag", tag);
        }

        public IList<Book.Model.Setting> SelectByIdNo(string IdNo)
        {
            return sqlmapper.QueryForList<Model.Setting>("Setting.select_by_IdNo", IdNo);
        }
        #endregion
    }
}
