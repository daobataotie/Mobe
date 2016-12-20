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


        public IList<Book.Model.Setting> Select(string tag)
        {
            return sqlmapper.QueryForList<Model.Setting>("Setting.select_by_tag", tag);
        }

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
        #endregion
    }
}
