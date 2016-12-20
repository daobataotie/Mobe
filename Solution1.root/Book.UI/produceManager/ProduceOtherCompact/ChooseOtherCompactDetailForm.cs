using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceOtherCompact
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2010-3-20
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ChooseOtherCompactDetailForm : DevExpress.XtraEditors.XtraForm
    {
        BL.ProduceOtherCompactDetailManager ProduceOtherCompactDetailManager = new Book.BL.ProduceOtherCompactDetailManager();
        Model.ProduceOtherCompact produceOtherCompact = new Book.Model.ProduceOtherCompact();
        public ChooseOtherCompactDetailForm()
        {
            InitializeComponent();
            this.produceOtherCompact.Details = ProduceOtherCompactDetailManager.Select();

        }

        private void ChooseOtherCompactDetailForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = produceOtherCompact.Details;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (Model.ProduceOtherCompactDetail ProduceOtherCompactDetail in produceOtherCompact.Details)
            {
                if (ProduceOtherCompactDetail.Checkeds == true)
                {
                    produceManager.ProduceOtherInDepot.Editform._produceOtherCompactDetail.Add(ProduceOtherCompactDetail);

                }
            }

            this.DialogResult = DialogResult.OK;
            
        }

        //取消
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 自定义列显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherCompactDetail> details = this.bindingSource1.DataSource as IList<Model.ProduceOtherCompactDetail>;
            if (details == null || details.Count < 1) return;
            Model.ProduceOtherCompact detail = details[e.ListSourceRowIndex].ProduceOtherCompact;
            Model.Product products = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumn2":
                    if (detail == null) return;
                    e.DisplayText = detail.ProduceOtherCompactDate.Value.ToString("yyyy-MM-dd");
                    break;
                case "gridColumn5":
                    if (products == null) return;
                    e.DisplayText = string.IsNullOrEmpty(products.Id) ? "" : products.Id; ;
                    break;
            }
        }
    }
}