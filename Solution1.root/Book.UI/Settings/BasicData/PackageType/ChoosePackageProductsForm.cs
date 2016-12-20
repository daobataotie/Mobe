using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.PackageType
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：ChoosePackageProductsForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-27
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChoosePackageProductsForm : Settings.BasicData.BaseChooseForm
    {
        //变量定义
        protected BL.PackageDetailsManager productManager = new Book.BL.PackageDetailsManager();
        Model.Customer customer = new Book.Model.Customer();
        public ChoosePackageProductsForm()
        {
            InitializeComponent();
            this.manager = new Book.BL.PackageDetailsManager();
        }
        protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.PackageType.PackageEditForm();
        }
    }
}