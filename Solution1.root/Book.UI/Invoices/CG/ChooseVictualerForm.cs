using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.CG
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 此類提供了供應商選擇的一些功能
   // 文 件 名：ChooseSuppliers
   // 编 码 人: 茍波濤                   完成时间:2009-05-10
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChooseSuppliers : DevExpress.XtraEditors.XtraForm
    {

        #region 構造函數
        public ChooseSuppliers()
        {
            InitializeComponent();
            buttonEditSupplier.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();
        }
        #endregion

        #region 變量定義 
        private Model.Supplier _supplier;
        //Get 訪問器
        public Model.Supplier Supplier 
        {
            get 
            {
                return _supplier;
            }
        }
        #endregion

        #region 對選擇的供應商進行提交
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this._supplier = buttonEditSupplier.EditValue as Model.Supplier;
            if (_supplier != null)
            {
                this.DialogResult = DialogResult.OK;
            }
            else 
            {
                MessageBox.Show(Properties.Resources.ChooseSupplier, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        public CGForm GetCgForm() 
        {
            return new CGForm(this._supplier);
        }
    }
}