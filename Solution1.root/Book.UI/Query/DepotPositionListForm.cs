using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-5-21
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class DepotPositionListForm : ConditionChooseForm
    {
        //货品管理
        private BL.ProductManager productManager = new Book.BL.ProductManager();

        private DepCondition condition;


        /// <summary>
        /// 无参构造
        /// </summary>
        public DepotPositionListForm()
        {

            InitializeComponent();
            this.editDept.Choose = new Book.UI.Invoices.ChooseDepot();
        }

        #region 重写父类方法
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as DepCondition;
            }
        }
        protected override void OnOK()
        {
            if (condition == null)
            {
                condition = new DepCondition();
            }
            condition.Depot = editDept.EditValue as Model.Depot;
        }
        #endregion


    }
}