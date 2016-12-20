using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Customs;
namespace Book.UI.Settings.BasicData.Supplier
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  ����w�Yܛ�����޹�˾
   //                     ������� �����ؾ�
   // ��������: 
   // �� �� ����ListForm
   // �� �� ��: ƈ����                   ���ʱ��:2009-11-06
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   //----------------------------------------------------------------*/
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        BL.SupplierCategoryManager supplierCategoryManager = new Book.BL.SupplierCategoryManager();
        BL.SupplierManager supplierManager = new Book.BL.SupplierManager();
        Model.SupplierCategory supplierCategory = new Book.Model.SupplierCategory();
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.SupplierManager();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceSupplierCategory.DataSource = supplierCategoryManager.Select();
            this.bindingSource1.DataSource = this.supplierManager.Select();
        }

        #region Override

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm(this.supplierCategory);
        }

        protected override BaseEditForm GetEditForm(object[] args)
        { 
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        #endregion

        private void bindingSourceSupplierCategory_CurrentChanged(object sender, EventArgs e)
        {
            this.supplierCategory = this.bindingSourceSupplierCategory.Current as Model.SupplierCategory;

            this.bindingSource1.DataSource = this.supplierManager.Select(supplierCategory);
            this.gridControl1.RefreshDataSource();
        }

    }
}