using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.Employees
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  ����w�Yܛ�����޹�˾
   //                     ������� �����ؾ�
   // ��������: 
   // �� �� ����EditForm
   // �� �� ��: ƈ����                   ���ʱ��:2009-10-18
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   //----------------------------------------------------------------*/
    public partial class ChooseEmployeeForm : Settings.BasicData.BaseChooseForm
    {
        //׃�����x(��ɫId)
        private string _roleId;

        #region Construcotrs

        public ChooseEmployeeForm()
        {
            InitializeComponent();
            _roleId = EmployeeParameters.BUSINESS;
            this.manager = new Book.BL.EmployeeManager();
        }

        public ChooseEmployeeForm(string roleId)
            : this()
        {
            _roleId = roleId;
        }

        #endregion

        #region ���d��ķ���
        protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.Employees.EditForm();
        }

        protected override void LoadData()
        {
            if (string.IsNullOrEmpty(_roleId))
            {
                _roleId = EmployeeParameters.BUSINESS;
            }

            if (_roleId == EmployeeParameters.ALL)
                this.bindingSource1.DataSource = (this.manager as BL.EmployeeManager).SelectOnActive();                
            else
                this.bindingSource1.DataSource = (this.manager as BL.EmployeeManager).Select(_roleId);

        }
        #endregion
    }
}