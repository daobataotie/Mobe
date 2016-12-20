using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.ZX
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        IList<Model.Setting> detail = new List<Model.Setting>();
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.SettingManager();
        }

        protected override void RefreshData()
        {
            this.detail = (this.manager as BL.SettingManager).SelectByName("Box");
            this.bindingSource1.DataSource = this.detail;
            this.gridView1.GroupPanelText = "可選擇的箱子  ";
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new ZXParameterSet();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(ZXParameterSet);
            return (ZXParameterSet)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }
    }
}