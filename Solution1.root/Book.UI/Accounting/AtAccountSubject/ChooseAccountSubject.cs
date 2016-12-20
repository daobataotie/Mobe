using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Invoices;
namespace Book.UI.Accounting.AtAccountSubject
{
    class ChooseAccountSubject : Invoices.IChoose
    { /// <summary>
        /// 
        /// 物料类型
        /// </summary>
        private Model.AtAccountSubject obj;

        #region IChoose 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void MyClick(ref ChooseItem item)
        {
            ChooseAccountSubjectForm f = new ChooseAccountSubjectForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.AtAccountSubject AtAccountSubject = f.SelectedItem as Model.AtAccountSubject;
                item = new ChooseItem(AtAccountSubject, AtAccountSubject.Id, AtAccountSubject.SubjectName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.AtAccountSubjectManager manager = new Book.BL.AtAccountSubjectManager();
            Model.AtAccountSubject AtAccountSubject = manager.GetById(item.ButtonText);
            if (AtAccountSubject != null)
            {
                item.EditValue = AtAccountSubject;
                item.LabelText = AtAccountSubject.SubjectName;
                item.ButtonText = AtAccountSubject.Id;
            }
            else
            {
                item.ErrorMessage = "物料類型錯誤";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.AtAccountSubject).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.AtAccountSubject).SubjectName;
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
                obj = (Model.AtAccountSubject)value;
            }
        }

        #endregion
    }
}
