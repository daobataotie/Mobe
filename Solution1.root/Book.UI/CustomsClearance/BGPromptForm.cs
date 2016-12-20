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
    public partial class BGPromptForm : DevExpress.XtraEditors.XtraForm
    {
        BL.BGHandbookDetail1Manager manager = new Book.BL.BGHandbookDetail1Manager();

        public DataTable dt;

        public BGPromptForm()
        {
            InitializeComponent();
            dt = manager.GetBGPrompt();
            this.bindingSource1.DataSource = dt;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            BGPromptRO ro = new BGPromptRO(dt);
            ro.ShowPreviewDialog();
        }

        private void BGPromptForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_SetBGHandbookEffect_Click(object sender, EventArgs e)
        {
            SetBGHandbookEffect f = new SetBGHandbookEffect();
            f.ShowDialog(this);
            dt = manager.GetBGPrompt();
            this.bindingSource1.DataSource = dt;
            this.gridControl1.RefreshDataSource();
        }
    }
}