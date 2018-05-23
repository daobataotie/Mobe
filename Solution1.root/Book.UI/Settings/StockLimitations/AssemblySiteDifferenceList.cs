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
    public partial class AssemblySiteDifferenceList : Settings.BasicData.BaseListForm
    {
        BL.AssemblySiteDifferenceDetaiManager detailManager = new BL.AssemblySiteDifferenceDetaiManager();
        int type = 0;   //0:搜索   1:选择订单
        public Model.AssemblySiteDifference SelectItem { get; set; }

        public AssemblySiteDifferenceList()
        {
            if (!AssemblySiteDifferenceForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
        }

        protected override void RefreshData()
        {
            IList<Model.AssemblySiteDifferenceDetai> list = detailManager.SelectByDateRage(DateTime.Now.AddMonths(-1), DateTime.Now, "");

            this.bindingSource1.DataSource = list;
            this.gridView1.GroupPanelText = "默認顯示一个月内的記錄";
        }

        private void barBtn_ChangeDateSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssemblySiteInventorySearchForm f = new AssemblySiteInventorySearchForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                IList<Model.AssemblySiteDifferenceDetai> list = detailManager.SelectByDateRage(f.StartDate, f.EndDate, f.ProductId);
                this.bindingSource1.DataSource = list;
            }
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new AssemblySiteDifferenceForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(AssemblySiteDifferenceForm);
            Model.AssemblySiteDifference model = (args[0] as Model.AssemblySiteDifferenceDetai).AssemblySiteDifference;

            args[0] = model;
            return (AssemblySiteDifferenceForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Settings.BasicData.BaseEditForm f1 = this.GetEditForm(new object[] { this.bindingSource1.Current });
            f1.ShowDialog();
        }
    }
}