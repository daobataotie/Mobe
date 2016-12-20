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
    public partial class ConditionPropertyChooseForm : ConditionAChooseForm
    {
        private ConditionProperty condition;
        public ConditionPropertyChooseForm()
        {
            InitializeComponent();
            this.newChooseContorl1.Choose =new  Accounting.AtProperty.ChooseProperty();
            this.newChooseContorl2.Choose = new Accounting.AtProperty.ChooseProperty();
        } 
#region 重写父类方法
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionProperty();
            if (this.newChooseContorl1.EditValue != null)
            {
                this.condition.StartPropertyId = (this.newChooseContorl1.EditValue as Model.AtProperty).PropertyId;
            }
            if (this.newChooseContorl2.EditValue != null)
            {
                this.condition.EndPropertyId = (this.newChooseContorl2.EditValue as Model.AtProperty).PropertyId;
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
                this.condition = value as ConditionProperty;
            }
        }
        #endregion
    }
}