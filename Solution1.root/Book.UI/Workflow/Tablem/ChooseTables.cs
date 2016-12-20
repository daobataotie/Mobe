using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;
namespace Book.UI.Workflow.Tablem
{
    class ChooseTables : Invoices.IChoose
    {
        private Model.Tables obj;

        #region IChoose 成员
        public void MyClick(ref ChooseItem item)
        {
            ChooseTablesForm f = new ChooseTablesForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Tables tables = f.SelectedItem as Model.Tables;
                item = new ChooseItem(tables,tables.Tablename,null);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.TablesManager manager = new Book.BL.TablesManager();
            Model.Tables tables = manager.Get(obj.TablesID);
            if (tables != null)
            {
                item.EditValue = tables;
                item.LabelText = null;
            }
            else
            {
                item.ErrorMessage = "表單出錯";
            }
        }
        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Tables).Tablename;
            }
        }

        public string LableText
        {
            get
            {
                return null;// EditValue == null ? string.Empty : (EditValue as Model.Tables).TablesID;
            }
        }

        public object EditValue
        {
            get
            {
                return obj;
            }
            set
            {
                obj = (Model.Tables)value;
            }
        }
        #endregion
    }
}
