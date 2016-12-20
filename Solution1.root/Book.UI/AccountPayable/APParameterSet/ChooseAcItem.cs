using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.AccountPayable.APParameterSet
{
    public partial class ChooseAcItem : Settings.BasicData.BaseChooseForm
    {
        public ChooseAcItem()
        {
            InitializeComponent();
            this.manager = new BL.AcItemManager();
        }
    }
}