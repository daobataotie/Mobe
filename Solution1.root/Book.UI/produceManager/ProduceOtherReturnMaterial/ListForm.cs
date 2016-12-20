using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;

namespace Book.UI.produceManager.ProduceOtherReturnMaterial
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.ProduceOtherReturnMaterialManager();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionAChooseForm condition = new ConditionAChooseForm();
            if (condition.ShowDialog(this) == DialogResult.OK)
            {
                ConditionA con = condition.Condition as ConditionA;
                this.bindingSource1.DataSource = (this.manager as BL.ProduceOtherReturnMaterialManager).Select(con.StartDate, con.EndDate);
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}