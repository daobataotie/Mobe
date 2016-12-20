using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.produceManager.MouldCategory
{
    public partial class ChooseProductMould : DevExpress.XtraEditors.XtraForm
    {

        BL.ProductMouldManager _productMouldMannager = new Book.BL.ProductMouldManager();
        BL.ProductMouldTestDetailManager _productMouldTestDetail = new Book.BL.ProductMouldTestDetailManager();
        private string tsid;
        private IList<Model.ProductMould> prolist = new List<Model.ProductMould>();
        private IList<Model.ProductMould> plist = new List<Model.ProductMould>();

        public ChooseProductMould()
        {
            InitializeComponent();
            this.bindingSourceProductMould.DataSource = this._productMouldMannager.Select();
        }

        public ChooseProductMould(IList<Model.ProductMould> list, string tid)
        {
            InitializeComponent();
            this.tsid = tid;
            this.plist = list;

            prolist = this._productMouldMannager.Select();
            foreach (Model.ProductMould productMould in prolist)
            {
                foreach (Model.ProductMould item in plist)
                {
                    if (item.MouldId == productMould.MouldId)
                        productMould.IsChecked = true;
                }

            }
            this.bindingSourceProductMould.DataSource = prolist;
        }

        private IList<Model.ProductMould> selectItem;

        public IList<Model.ProductMould> SelectItem
        {
            get { return selectItem; }
            set { selectItem = value; }
        }

        private void sbtn_sure_Click(object sender, EventArgs e)
        {
            this._productMouldTestDetail.DeleteByProductMouldTestId(this.tsid);
            selectItem = new List<Model.ProductMould>();
            foreach (Model.ProductMould pro in prolist)
            {
                if (pro.IsChecked == true)
                    this.selectItem.Add(pro);
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
