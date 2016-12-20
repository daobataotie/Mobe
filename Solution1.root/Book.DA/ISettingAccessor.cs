//------------------------------------------------------------------------------
//
// file name：ISettingAccessor.cs
// author: peidun
// create date：2008/6/24 16:47:26
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Setting
    /// </summary>
    public partial interface ISettingAccessor : IEntityAccessor
    {
        IList<Model.Setting> Select(string tag);
        Book.Model.Setting selectimage(string tag);
        void Update(string settingId, string currentValue);
        void UpdateImage(string settingId, byte[] images);

        IList<Book.Model.Setting> SelectTagOrderDefault(string tag);
        IList<Book.Model.Setting> SelectByName(string name);
        void DeleteByTag(string tag);
        IList<Book.Model.Setting> SelectByIdNo(string IdNo);
    }
}

