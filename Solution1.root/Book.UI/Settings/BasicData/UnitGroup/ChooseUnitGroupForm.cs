using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.UnitGroup
{
    public partial class ChooseUnitGroupForm : BaseChooseForm
    {
        public ChooseUnitGroupForm()
        {
            InitializeComponent();
            this.manager = new BL.UnitGroupManager();
        }

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }
    }
}