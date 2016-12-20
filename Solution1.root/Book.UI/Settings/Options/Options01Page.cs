using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.Options
{

    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010   咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-14
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Options01Page : BaseOptionsPage
    {
        protected BL.SettingManager settingManager = new Book.BL.SettingManager();
        protected string _tag;

        public Options01Page(string tag)
        {
            InitializeComponent();
            this._tag = tag;
        }
        public Options01Page()
        {
            InitializeComponent();
        }

        #region 重写方法
        public override void DoSave()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            IList<Model.Setting> settings = this.settingBindingSource.DataSource as IList<Model.Setting>;
            foreach (Model.Setting s in settings)
            {
                byte[] pic = new byte[] { };
                s.PictrueLogo = pic;
                this.settingManager.Update(s);
            }
        }

        public override void DoLoad()
        {
            this.settingBindingSource.DataSource = this.settingManager.Select(this._tag);
            //this.settingBindingSource.DataSource = this.settingManager.Select();
            this.gridControl1.RefreshDataSource();
        }
        #endregion
    }
}
