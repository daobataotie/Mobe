using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Settings.BasicData.ProductCategories
{
    public partial class ChooseMaterialForm : BaseChooseForm
    {
        BL.MaterialManager materialmanager = new BL.MaterialManager();
        IList<Model.Material> _key = new List<Model.Material>();

        public IList<Model.Material> Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public ChooseMaterialForm()
        {
            InitializeComponent();

            this.manager = this.materialmanager;
            this.bindingSource1.DataSource = materialmanager.SelectAll();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.gridView1.OptionsBehavior.Editable = true;
        }

        protected override BaseEditForm GetEditForm()
        {
            return new Material();
        }

        public override void simpleButtonOK_Click(object sender, EventArgs e)
        {
            foreach (Model.Material model in (List<Model.Material>)this.bindingSource1.DataSource)
            {
                if (model.Check)
                    this.Key.Add(model);
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}