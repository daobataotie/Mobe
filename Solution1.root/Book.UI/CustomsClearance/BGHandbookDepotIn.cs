using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.CustomsClearance
{
    public partial class BGHandbookDepotIn : DevExpress.XtraEditors.XtraForm
    {
        public BGHandbookDepotIn()
        {
            InitializeComponent();

            this.bindingSourceBGHandbookId.DataSource = new BL.BGHandbookIdSetManager().Select();
        }
    }
}