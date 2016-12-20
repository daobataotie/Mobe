using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.ProduceManager
{
    public partial class ChooseBomIdRangeForm : Form
    {
        public string StartBomId { get; set; }
        public string EndBomId { get; set; }
        public string CustomerId { get; set; }

        BL.ProductManager productManager = new Book.BL.ProductManager();
        public ChooseBomIdRangeForm()
        {
            InitializeComponent();
            this.bindingSourceBomIds.DataSource = this.productManager.DataReaderBind<Model.Product>("select BomId,Id from BomParentPartInfo", null, CommandType.Text);
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            this.StartBomId = this.lookUpEditStartBomId.Text;
            this.EndBomId = this.lookUpEditEndBomId.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
