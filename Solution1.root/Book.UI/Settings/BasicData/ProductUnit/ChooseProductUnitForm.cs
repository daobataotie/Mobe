using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

namespace Book.UI.Settings.BasicData.ProductUnit
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：ChooseProductUnitForm
   // 编 码 人: 茍波濤                   完成时间:2009-11-05
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChooseProductUnitForm : BaseChooseForm
    {
        private BL.UnitGroupManager groupManager = new Book.BL.UnitGroupManager();
        public ChooseProductUnitForm()
        {
            InitializeComponent();
            this.manager = new BL.ProductUnitManager();
        }
        public ChooseProductUnitForm(string groupId)
        {
            InitializeComponent();
            this._unitGroupId = groupId;
            this.manager = new BL.ProductUnitManager();
        }

        protected override BaseEditForm GetEditForm()
        {
            return new Settings.BasicData.UnitGroup.EditForm();
        }

        private string _unitGroupId;

        protected override void LoadData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.ProductUnitManager).Select(_unitGroupId);
            this.gridControl1.RefreshDataSource();
        }
    }
}