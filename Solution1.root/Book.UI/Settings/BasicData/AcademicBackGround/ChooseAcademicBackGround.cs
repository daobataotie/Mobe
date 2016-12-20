using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;
namespace Book.UI.Settings.BasicData.AcademicBackGround
{
    public  class ChooseAcademicBackGround:IChoose
    {
        private Model.Bank ojb;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {

            ChooseAcademicBackGroundForm  f = new ChooseAcademicBackGroundForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.AcademicBackGround academic = f.SelectedItem as Model.AcademicBackGround;
                item = new ChooseItem(academic , academic.AcademicBackGroundId,academic.AcademicBackGroundName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.AcademicBackGroundManager academicManager = new Book.BL.AcademicBackGroundManager();
            Model.AcademicBackGround  academic =academicManager.Get(item.ButtonText);
            if (academic != null)
            {
                item.EditValue = academic;
                item.LabelText = academic.AcademicBackGroundName;
                item.ButtonText =academic.AcademicBackGroundId;
            }
            else
            {
                item.ErrorMessage = Properties.Resources.ChooseEmployeeError;
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Bank)
                    .BankId;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Bank).BankName;
            }
        }

        public object EditValue
        {
            get
            {
                return ojb;
            }
            set
            {
                ojb = (Model.Bank)value;
            }
        }

        #endregion
    }
}
