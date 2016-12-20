using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.ProduceManager
{
    public partial class ChooseBomParentPartInfoForm : Settings.BasicData.BaseChooseForm
    {
        public ChooseBomParentPartInfoForm()
        {
            InitializeComponent();
            this.manager = new BL.BomParentPartInfoManager();
        }
        //重写方法
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.ProduceManager.BomEdit();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.BomParentPartInfo> details = this.bindingSource1.DataSource as IList<Model.BomParentPartInfo>;
            if (details == null || details.Count < 1) return;
            Model.Product product = details[e.ListSourceRowIndex].Product;
            if (product == null) return;
            switch (e.Column.Name)
            {
                case "gridColumn2":
                    e.DisplayText = product.Id;
                    break;
                case "gridColumn3":
                    e.DisplayText = string.IsNullOrEmpty(product.CustomerProductName) ? product.ProductName : product.ProductName + "{" + product.CustomerProductName + "}";
                    break;
                case "gridColumn5":
                    e.DisplayText = product.Customer == null ? "" : product.Customer.CustomerShortName;
                    break;


            }
        }

        private void ChooseBomParentPartInfoForm_Load(object sender, EventArgs e)
        {
            //string sql = "SELECT productid,id,productname FROM product WHERE (IsProcee IS null OR IsProcee=0) AND (IsCustomerProduct IS NULL OR IsCustomerProduct =0)";
            //this.bindingSource1.DataSource = new BL.BomParentPartInfoManager().DataReaderBind<Model.BomParentPartInfo>(sql);

        }
    }
}