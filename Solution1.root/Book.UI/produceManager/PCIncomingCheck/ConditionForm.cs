using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCIncomingCheck
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

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (condition == null)
                condition = new Condition();
            condition.StartDate = dateEditStart.EditValue == null ? global::Helper.DateTimeParse.NullDate : dateEditStart.DateTime;
            condition.EndDate = dateEditEnd.EditValue == null ? global::Helper.DateTimeParse.EndDate : dateEditEnd.DateTime;
          
            condition.LotNumber = this.txt_Pihao.Text;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}