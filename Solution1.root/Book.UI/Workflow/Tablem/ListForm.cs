using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using Book.UI.Workflow.Tablem;
namespace Book.UI.Workflow.Tablem
{
    /// <summary>
    /// ±íµ¥
    /// </summary>
    public partial class ListForm : BaseListForm
    {
        public ListForm()
        {
            InitializeComponent();
            this.manager = new Book.BL.TablesManager();
        }

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }
    }
}