using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.Customs
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  ����w�Yܛ�����޹�˾
   //                     ������� �����ؾ�
   // ��������: �͑�һ�[�O��
   // �� �� ����ListForm
   // �� �� ��: ���޾�                   ���ʱ��:2009-10-12
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   //----------------------------------------------------------------*/
    public partial class ListForm : Form
    {
        #region ����������
        private BL.CustomerManager _customerManager = new Book.BL.CustomerManager();
        #endregion

        #region ���캯��
        public ListForm()
        {
            InitializeComponent();
        }
        #endregion 

        private void ListForm_Load(object sender, EventArgs e)
        {
            this.bindingSourcecustomer.DataSource = _customerManager.Select();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            EditForm.cstomer = bindingSourcecustomer.Current as Model.Customer;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}