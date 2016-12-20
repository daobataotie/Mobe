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
    public partial class CompactSelectForm : DevExpress.XtraEditors.XtraForm
    {
        public CompactSelectForm()
        {
            InitializeComponent();
        }

        BL.ProduceOtherCompactManager _produceOtherCompactManager = new BL.ProduceOtherCompactManager();
        Model.MRSHeader _mrsHeader;
        public CompactSelectForm(Model.MRSHeader mrsHeader)
            : this()
        {
            this._mrsHeader = mrsHeader;
        }

        public Model.ProduceOtherCompact SelectItem
        {
            get {
                return this.bindingSourceCopact.Current as Model.ProduceOtherCompact;
            }
        }


        private void CompactSelectForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceCopact.DataSource = this._produceOtherCompactManager.SelectByMRSHeaderId(this._mrsHeader.MRSHeaderId);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_look_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }


    }
}