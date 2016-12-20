using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
    public partial class ConditionChooseForm : DevExpress.XtraEditors.XtraForm
    {
        public ConditionChooseForm()
        {
            InitializeComponent();           
        }

        protected virtual void OnOK()
        {
        }

        public virtual Condition Condition
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.OnOK();
            this.DialogResult = DialogResult.OK;
        }
    }
}