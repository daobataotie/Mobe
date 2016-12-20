using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.ProduceOtherInDepot
{

    /*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究
    // 编 码 人: 刘永亮                   完成时间:2010-12-02
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class ChooseProduceOtherCompactForm : Form
    {

        #region
        private BL.ProduceOtherCompactManager _compactManager = new Book.BL.ProduceOtherCompactManager();
        #endregion

        private string sign = string.Empty;
        public ChooseProduceOtherCompactForm(string isproductOrMaterial)
        {
            InitializeComponent();
            this.sign = isproductOrMaterial;
        }


        private void ChooseProduceOtherCompactForm_Load(object sender, EventArgs e)
        {
            if (this.sign == "product")
                this.bindingSourceProduceOtherCompay.DataSource = this._compactManager.SelectIsInDepot();
            else if (this.sign == "material")
                this.bindingSourceProduceOtherCompay.DataSource = this._compactManager.SelectIsInDepotMaterial();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Model.ProduceOtherCompact temp = this.bindingSourceProduceOtherCompay.Current as Model.ProduceOtherCompact;
            if (temp == null) return;
            if (this.sign == "product")
                Editform._compact = temp;
            else if (this.sign == "material")
                ProduceOtherMaterial.EditForm._compact = temp;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            Model.ProduceOtherCompact temp = this.bindingSourceProduceOtherCompay.Current as Model.ProduceOtherCompact;
            if (temp == null) return;
            if (this.sign == "product")
                Editform._compact = temp;
            else if (this.sign == "material")
                ProduceOtherMaterial.EditForm._compact = temp;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
