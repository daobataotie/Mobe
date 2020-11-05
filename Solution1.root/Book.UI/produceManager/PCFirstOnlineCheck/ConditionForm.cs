using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCFirstOnlineCheck
{
    public partial class ConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public ConditionForm()
        {
            InitializeComponent();
            this.dateEditStart.EditValue = DateTime.Now.AddMonths(-1);
            this.dateEditEnd.EditValue = DateTime.Now;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public Condition condition { get; set; }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (this.condition == null)
                this.condition = new Condition();
            this.condition.StartDate = this.dateEditStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStart.DateTime;
            this.condition.EndDate = this.dateEditEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.dateEditEnd.DateTime;
            this.condition.InvoiceCusId = this.txt_InvoiceCusId.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}