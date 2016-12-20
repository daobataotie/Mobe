using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.Privileges.Operators
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-10-21
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class RoleToOperators : DevExpress.XtraEditors.XtraForm
    {

        private BL.OperationRoleManager operationRoleManager = new BL.OperationRoleManager();

        private Model.Operators _operators;

        private RoleToOperators()
        {
            InitializeComponent();
        }

        //构造函数
        public RoleToOperators(Model.Operators op)
            : this()
        {
            this._operators = op;
            this.textEdit1.Text = op.Id;
            this.textEdit2.Text = op.OperatorName;
            this.bindingSource1.DataSource = operationRoleManager.Select(op);
        }

        /// <summary>
        /// “确定”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            this.operationRoleManager.Update(this.bindingSource1.DataSource as IList<Model.OperationRole>);
        }
    }
}