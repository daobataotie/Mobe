using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.MouldCategory
{
    public partial class ChooseProductMouldEditForm : Settings.BasicData.BaseChooseForm
    {

        public ChooseProductMouldEditForm()
        {
            InitializeComponent();
            this.manager = new BL.ProductMouldManager();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new ProductMouldEditForm();
        }
    }
}
