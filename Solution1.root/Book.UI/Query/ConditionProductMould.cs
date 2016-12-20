using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    public partial class ConditionProductMould : DevExpress.XtraEditors.XtraForm
    {
        public ConditionProductMould()
        {
            InitializeComponent();

            this.date_End.EditValue = DateTime.Now;
            this.date_Start.EditValue = DateTime.Now.AddDays(-30);
            this.bindingSource1.DataSource = (new BL.MouldCategoryManager()).Select();
            this.StartPosition = FormStartPosition.CenterParent;
        }
        private ConditionPM _conditionPM;

        internal ConditionPM ConditionPM
        {
            get { return _conditionPM; }
            set { _conditionPM = value; }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (ConditionPM == null)
                ConditionPM = new ConditionPM();
            ConditionPM.StartDate = this.date_Start.EditValue == null ? DateTime.Now.AddDays(-30) : this.date_Start.DateTime;
            ConditionPM.EndDate = this.date_End.EditValue == null ? DateTime.Now : this.date_End.DateTime;
            ConditionPM.MouldId = this.txt_MouldId.Text;
            ConditionPM.MouldName = this.txt_MouldName.Text;
            ConditionPM.MouldCategory = this.lookUpEditMouldCategory.EditValue == null ? null : this.lookUpEditMouldCategory.EditValue as Model.MouldCategory;
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}