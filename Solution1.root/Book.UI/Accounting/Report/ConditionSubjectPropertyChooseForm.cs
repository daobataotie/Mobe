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
    public partial class ConditionSubjectPropertyChooseForm :ConditionAChooseForm
    {
        private ConditionSubjectProperty condition;
        public ConditionSubjectPropertyChooseForm()
        {
            InitializeComponent();
            this.bindingSource1.DataSource = new BL.AtAccountSubjectManager().Select();
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }
        #region 重写父类方法
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionSubjectProperty();
            if (this.lookUpEditRespectiveSubject.EditValue != null)
            {
                this.condition.RespectiveSubject = this.lookUpEditRespectiveSubject.EditValue.ToString();
            }
        }
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionSubjectProperty;
            }
        }
        #endregion
    }
}