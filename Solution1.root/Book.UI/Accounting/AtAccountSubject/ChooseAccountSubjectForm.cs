using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtAccountSubject
{
    public partial class ChooseAccountSubjectForm : Settings.BasicData.BaseChooseForm
    {
        public ChooseAccountSubjectForm()
        {
            InitializeComponent();
            this.manager = new BL.AtAccountSubjectManager();
        }
        //重写方法
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Accounting.AtAccountSubject.EditForm();
        }
    }
}