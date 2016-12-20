using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.ProductUnit
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 计量单位一览表
   // 文 件 名：ListForm
   // 编 码 人: 茍波濤                   完成时间:2009-11-05
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ListForm : BaseListForm
    {
        protected BL.ProductUnitManager unitManager = new Book.BL.ProductUnitManager();
        protected BL.UnitGroupManager groupMamager = new Book.BL.UnitGroupManager();
        public Model.UnitGroup unitGroup = new Book.Model.UnitGroup();

        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.ProductUnitManager();
        }

        protected override BaseEditForm GetEditForm()
        {
            this.unitGroup = this.bindingSourceUnitGroup.Current as Model.UnitGroup;
            return new EditForm(unitGroup);
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        private void bindingSourceUnitGroup_CurrentChanged(object sender, EventArgs e)
        {
            Model.UnitGroup unitGroup = this.bindingSourceUnitGroup.Current as Model.UnitGroup;
            if (unitGroup == null)
                return;
            this.bindingSource1.DataSource = this.unitManager.Select(unitGroup.UnitGroupId);
        }

        protected override void RefreshData()
        {
            bindingSourceUnitGroup.DataSource = this.groupMamager.Select();
            this.bindingSource1.DataSource = this.unitManager.Select((this.bindingSourceUnitGroup.Current as Model.UnitGroup) == null ? "" : (this.bindingSourceUnitGroup.Current as Model.UnitGroup).UnitGroupId);
        }
    }
}