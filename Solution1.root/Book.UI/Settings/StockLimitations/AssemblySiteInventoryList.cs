using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.StockLimitations
{
    public partial class AssemblySiteInventoryList : Settings.BasicData.BaseListForm
    {
        BL.AssemblySiteInventoryDetailManager detailManager = new BL.AssemblySiteInventoryDetailManager();
        int type = 0;   //0:搜索   1:选择订单
        public Model.AssemblySiteInventory SelectItem { get; set; }

        public AssemblySiteInventoryList()
        {
            if (!AssemblySiteInventoryForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
        }

        public AssemblySiteInventoryList(int type)
            : this()
        {
            this.type = type;
        }

        protected override void RefreshData()
        {
            IList<Model.AssemblySiteInventoryDetail> list = detailManager.SelectByDateRage(DateTime.Now.AddMonths(-1), DateTime.Now, "", (this.type == 1 ? true : false));

            this.bindingSource1.DataSource = list;
            this.gridView1.GroupPanelText = "默認顯示一个月内的記錄";
        }

        private void barBtn_ChangeDateSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssemblySiteInventorySearchForm f = new AssemblySiteInventorySearchForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                IList<Model.AssemblySiteInventoryDetail> list = detailManager.SelectByDateRage(f.StartDate, f.EndDate, f.ProductId, (this.type == 1 ? true : false));
                this.bindingSource1.DataSource = list;
            }
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new AssemblySiteInventoryForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(AssemblySiteInventoryForm);
            Model.AssemblySiteInventory model = (args[0] as Model.AssemblySiteInventoryDetail).AssemblySiteInventory;

            args[0] = model;
            return (AssemblySiteInventoryForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (type == 0)
            {
                Settings.BasicData.BaseEditForm f1 = this.GetEditForm(new object[] { this.bindingSource1.Current });
                f1.ShowDialog();
            }
            else
            {
                this.SelectItem = new BL.AssemblySiteInventoryManager().GetDetail((this.bindingSource1.Current as Model.AssemblySiteInventoryDetail).AssemblySiteInventoryId);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}