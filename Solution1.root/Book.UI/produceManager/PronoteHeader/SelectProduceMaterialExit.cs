using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PronoteHeader
{

    //*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010   咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究
    // 功能描述：加工单形成退料单（根据加工单料的仓库不同形成退料单据）
    // 编 码 人: 刘永亮             完成时间:2011-03-07
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class SelectProduceMaterialExit : DevExpress.XtraEditors.XtraForm
    {
        BL.ProduceMaterialExitManager _producematerialexitmanager = new Book.BL.ProduceMaterialExitManager();
        BL.ProduceMaterialExitDetailManager _producematerialexitdetailmanager = new Book.BL.ProduceMaterialExitDetailManager();

        private Model.PronoteHeader _pronoteHeader;

        //private Model.MRSHeader _MRSHeader;

        private BL.ProduceMaterialManager pm = new BL.ProduceMaterialManager();

        public Model.ProduceMaterialExit SelectItem
        {
            get { return (this.bindingSourceProduceMaterialExit.Current as Model.ProduceMaterialExit); }
        }

        public SelectProduceMaterialExit()
        {
            InitializeComponent();
        }

        public SelectProduceMaterialExit(Model.PronoteHeader pronoteHeader)
            : this()
        {
            this._pronoteHeader = pronoteHeader;
            this.bindingSourceProduceMaterialExit.DataSource = this._producematerialexitmanager.SelectByProduceHeaderId(pronoteHeader.PronoteHeaderID);
        }

        //public SelectProduceMaterialExit(Model.MRSHeader mrsHeader)
        //    : this()
        //{
        //    this._MRSHeader = mrsHeader;
        //    this.bindingSourceProduceMaterialExit.DataSource = pm.SelectInvoiceId(this._MRSHeader.MRSHeaderId);
        //}

        private void simpleButton_Info_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //private void simpleButton_Save_Click(object sender, EventArgs e)
        //{
        //    this.pm.UpdateProduceMaterial(this.bindingSourceProduceMaterial.DataSource as DataTable);
        //    MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

        //}

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceMaterialExitDetail> details = this.bindingSource1.DataSource as IList<Model.ProduceMaterialExitDetail>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "colpId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
                case "colStock":
                    if (detail == null) return;
                    e.DisplayText = detail.StocksQuantity.HasValue ? detail.StocksQuantity.ToString() : "0";
                    break;
            }
        }

        private void bindingSourceProduceMaterial_CurrentChanged(object sender, EventArgs e)
        {
            if (this.bindingSourceProduceMaterialExit.Current == null) return;
            this.bindingSource1.DataSource = this._producematerialexitdetailmanager.Select(this.bindingSourceProduceMaterialExit.Current as Model.ProduceMaterialExit);
        }
    }
}