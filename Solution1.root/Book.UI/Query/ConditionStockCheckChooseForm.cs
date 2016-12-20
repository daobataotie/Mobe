using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
    public partial class ConditionStockCheckChooseForm : ConditionAChooseForm
    {

        private ConditionStockCheck _conditionStockcheck;
        private global::Helper.CompanyKind kind;
        

        public ConditionStockCheckChooseForm()
        {
            InitializeComponent();
        }

        public override Condition Condition
        {
            get
            {
                return this._conditionStockcheck;
            }
            set
            {
                this._conditionStockcheck = value as ConditionStockCheck;
            }
        }

        protected override void OnOK()
        {
            if (this._conditionStockcheck == null)
                _conditionStockcheck = new ConditionStockCheck();
            _conditionStockcheck.StartDate = this.dateEditStartDate.DateTime;
            _conditionStockcheck.EndDate = this.dateEditEndDate.DateTime;
           
        }
    }
}
