using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.PCParameterSet
{
    public partial class ChooseParameter : Book.UI.Settings.BasicData.BaseChooseForm
    {
        IList<Model.Setting> _detail = new List<Model.Setting>();
        private BL.SettingManager settingManager = new Book.BL.SettingManager();

        public ChooseParameter()
        {
            InitializeComponent();
        }

        public ChooseParameter(string paramFlag)
            : this()
        {
            this._detail = this.settingManager.SelectByName(paramFlag);
            if (_detail == null)
                this._detail = new List<Model.Setting>();
            this.bindingSource1.DataSource = this._detail;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
