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

// 编 码 人: 马艳军            完成时间:2009-6-13
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q50Form :BaseForm
    {
        Model.Product product = new Book.Model.Product();
        BL.DepotManager depotMamager = new Book.BL.DepotManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();

        #region 构造函数，初始化
        public Q50Form()
        {
            InitializeComponent();
        }

        public Q50Form(DepCondition condition)
            : base()
        {
            this.condition = condition;
        }
        #endregion 


        #region 重写父类方法
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
        }

        protected override void DoQuery()
        {
            DepCondition condition =this.condition as DepCondition;

            this.bindingSourceProduct.DataSource = this.productManager.Select();

        }
        #endregion 

        public static ConditionChooseForm GetConditionChooseForm()
        {
            return new DepotPositionListForm();
        }
    }
}