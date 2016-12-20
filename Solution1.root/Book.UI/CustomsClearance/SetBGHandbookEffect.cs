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
    public partial class SetBGHandbookEffect : DevExpress.XtraEditors.XtraForm
    {
        DataTable dt = new DataTable();
        DataRow dr;
        BL.BGHandbookManager manager = new Book.BL.BGHandbookManager();
        public SetBGHandbookEffect()
        {
            InitializeComponent();
            dr = dt.NewRow();
            dt = manager.SelectIdGroupById();
            this.bindingSource1.DataSource = dt;
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            dr = this.dt.Rows[this.bindingSource1.IndexOf(this.bindingSource1.Current)];
            if (dr != null)
                this.manager.UpdateIsEffect(dr[0].ToString(), (dr[1].ToString() == "0" ? "1" : "0"));
            dt = manager.SelectIdGroupById();
            this.gridControl1.RefreshDataSource();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "gridColumn2")
            {
                dr = this.dt.Rows[e.RowHandle];
                if (dr != null)
                {
                    if (dr[1].ToString() == "1")
                        e.DisplayText = "生效";
                    else
                        e.DisplayText = "失效";
                }
            }
            if (e.Column.Name == "gridColumn3")
            {
                dr = this.dt.Rows[e.RowHandle];
                if (dr != null)
                {
                    if (dr[1].ToString() == "0")
                        e.DisplayText = "使生效";
                    else
                        e.DisplayText = "使失效";
                }
            }
        }
    }
}