using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;

namespace Book.UI.Accounting.Report
{
    public partial class ConditionGeneralAccountChooseForm : ConditionAChooseForm
    {
        private ConditionGeneralAccount condition;
        public ConditionGeneralAccountChooseForm()
        {
            InitializeComponent();
            this.bindingSource1.DataSource = new BL.AtAccountSubjectManager().Select();
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionGeneralAccount();
            this.condition.StartDate = this.dateEditStartDate.DateTime.Date;
            this.condition.EndDate = this.dateEditEndDate.DateTime.AddDays(1).AddSeconds(-1);
            this.condition.StartSubjectId = (this.lue_AtaccoutSubject.EditValue == null || string.IsNullOrEmpty(this.lue_AtaccoutSubject.EditValue.ToString())) ? null : this.lue_AtaccoutSubject.EditValue.ToString();
            this.condition.EndSubjectId = (this.lue_end_AtaccoutSubject.EditValue == null || string.IsNullOrEmpty(this.lue_end_AtaccoutSubject.EditValue.ToString())) ? null : this.lue_end_AtaccoutSubject.EditValue.ToString();
            //this.condition.Include_QiChuYuE = this.chkEdit_IncludeQiChuYuE.Checked;
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionGeneralAccount;
            }
        }

        private void lue_AtaccoutSubject_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lue_end_AtaccoutSubject.EditValue == null)
                this.lue_end_AtaccoutSubject.EditValue = this.lue_AtaccoutSubject.EditValue;
        }
    }
}