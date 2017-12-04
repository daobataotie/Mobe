using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Book.UI.Settings.BasicData.ProductCategories
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：ChooseForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-30
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChooseForm : BaseChooseForm
    {
        string CategoryLevel = "1";
        string ProductCategoryParentId = null;

        public ChooseForm()
        {
            InitializeComponent();
            this.manager = new BL.ProductCategoryManager();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryLevel">只有2,3两种</param>
        /// <param name="ProductCategoryParentId">父级类型</param>
        public ChooseForm(string CategoryLevel, string ProductCategoryParentId)
            : this()
        {
            this.CategoryLevel = CategoryLevel;
            this.ProductCategoryParentId = ProductCategoryParentId;
        }

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override void LoadData()
        {
            if (this.manager != null)
            {
                MethodInfo methodInfo = null;
                methodInfo = this.manager.GetType().GetMethod("SelectListByFilter", new Type[] { typeof(string), typeof(string) });
                if (methodInfo != null)
                {
                    this.bindingSource1.DataSource = methodInfo.Invoke(this.manager, new object[] { this.CategoryLevel, this.ProductCategoryParentId });
                }
            }
        }
    }
}