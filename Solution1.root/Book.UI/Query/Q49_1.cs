using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-6-12
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q49_1 : DevExpress.XtraReports.UI.XtraReport
    {
     
        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q49_1()
        {
            InitializeComponent();
            this.xrTableProductID.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCount.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_PronoteQuantity);
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.PronotedetailsMaterial.PRO_ProductUnit);
        }

        private Model.PronoteHeader pronote;

        public Model.PronoteHeader Pronote
        {
            get { return pronote; }
            set { pronote = value; }
        }

        protected void Q49_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //IList<Model.PronotedetailsMaterial> list = materialManager.selectByHeaderIdAndPid(this.Pronote,this.con);
            //if (list != null || list.Count != 0)
            if (Pronote!=null)
            this.DataSource = Pronote.DetailsMaterial;


        }

    }
}
