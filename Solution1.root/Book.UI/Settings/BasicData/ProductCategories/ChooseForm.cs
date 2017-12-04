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
   // Copyright (C) 2008 - 2010  ����w�Yܛ�����޹�˾
   //                     ������� �����ؾ�
   // ��������: 
   // �� �� ����ChooseForm
   // �� �� ��: ƈ����                   ���ʱ��:2009-10-30
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
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
        /// <param name="CategoryLevel">ֻ��2,3����</param>
        /// <param name="ProductCategoryParentId">��������</param>
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