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
    // 功能描述：加工单形成领料单（根据加工单料的仓库不同形成领料单据）
    // 编 码 人: 刘永亮             完成时间:2011-03-07
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class SelectProduceMaterial : DevExpress.XtraEditors.XtraForm
    {
        BL.ProduceMaterialdetailsManager produceMaterialdetailsManager = new Book.BL.ProduceMaterialdetailsManager();
        BL.ProduceMaterialManager produceMaterialManager = new Book.BL.ProduceMaterialManager();
        #region
        private Model.PronoteHeader _pronoteHeader;
        private Model.MRSHeader _MRSHeader;
        private BL.ProduceMaterialManager pm = new BL.ProduceMaterialManager();
        public object SelectItem
        {
            get { return this.bindingSourceProduceMaterial.Current; }
        }
        #endregion

        public SelectProduceMaterial()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public SelectProduceMaterial(Model.PronoteHeader pronoteHeader)
            : this()
        {
            this._pronoteHeader = pronoteHeader;
            this.bindingSourceProduceMaterial.DataSource = pm.SelectInvoiceId(this._pronoteHeader.PronoteHeaderID);
        }

        public SelectProduceMaterial(Model.MRSHeader mrsHeader)
            : this()
        {
            this._MRSHeader = mrsHeader;
            this.bindingSourceProduceMaterial.DataSource = pm.SelectInvoiceId(this._MRSHeader.MRSHeaderId);
        }
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
            IList<Model.ProduceMaterialdetails> details = this.bindingSource1.DataSource as IList<Model.ProduceMaterialdetails>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumn8":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
                case "gridColumnStock":
                    if (detail == null) return;
                    e.DisplayText = detail.StocksQuantity.HasValue ? detail.StocksQuantity.ToString() : "0";
                    break;

            }
        }

        private void bindingSourceProduceMaterial_CurrentChanged(object sender, EventArgs e)
        {
            if (this.bindingSourceProduceMaterial.Current == null) return;
            this.bindingSource1.DataSource = this.produceMaterialdetailsManager.Select(this.bindingSourceProduceMaterial.Current as Model.ProduceMaterial);


        }
    }
}