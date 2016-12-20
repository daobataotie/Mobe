using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;
namespace Book.UI.Settings.BasicData.Company
{
    public   class ChooseCompany:IChoose
    {
        private Model.Company ojb;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {

            ChooseCompanyForm f = new ChooseCompanyForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Company company = f.SelectedItem as Model.Company;
                item = new ChooseItem(company, company.CompanyId, company.CompanyName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.CompanyManager companyManager = new Book.BL.CompanyManager();
            Model.Company company = companyManager.Get(item.ButtonText);
            if (company != null)
            {
                item.EditValue = company;
                item.LabelText =company.CompanyName ;
                item.ButtonText=company.CompanyId;
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
                return EditValue == null ? string.Empty : (EditValue as Model.Company).CompanyId;
                  
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Company).CompanyName;
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
                ojb = (Model.Company)value;
            }
        }

        #endregion
    }
}
