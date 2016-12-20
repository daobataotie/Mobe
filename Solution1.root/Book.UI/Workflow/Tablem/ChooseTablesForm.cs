using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Workflow.Tablem
{
    public partial class ChooseTablesForm : Settings.BasicData.BaseChooseForm
    {
        public ChooseTablesForm()
        {
            InitializeComponent();
            this.manager = new BL.TablesManager();
        }
   
        protected override Book.UI.Settings.BasicData.BaseEditForm  GetEditForm()

        {
            return new UI.Workflow.Tablem.EditForm();
        }
    }
}