using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
        public ChooseForm()
        {
            InitializeComponent();
            this.manager = new BL.ProductCategoryManager();
        }

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }
    }
}