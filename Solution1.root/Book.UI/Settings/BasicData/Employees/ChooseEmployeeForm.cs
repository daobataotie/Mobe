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
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-18
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChooseEmployeeForm : Settings.BasicData.BaseChooseForm
    {
        //變量定義(角色Id)
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

        #region 重載父類的方法
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