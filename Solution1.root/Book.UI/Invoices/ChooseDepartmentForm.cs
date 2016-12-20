using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices
{
    public partial class ChooseDepartmentForm : Settings.BasicData.BaseChooseForm
    {
       #region Construcotrs

        public ChooseDepartmentForm()
        {
            InitializeComponent();
            this.manager = new BL.DepartmentManager();
        }

        #endregion

        protected override UI.Settings.BasicData.BaseEditForm1 GetEditForm1()
        {
            return new UI.Settings.BasicData.Department.EditForm();
        }
    }
}