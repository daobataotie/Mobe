using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices
{
    public class ChooseCompany : IChoose
    {
        private object obj;
        private Helper.CompanyKind _kind;
        public ChooseCompany(Helper.CompanyKind kind)
        {
            _kind = kind;
        }

        #region IChoose 成员

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : "  ";
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : "   ";
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
                obj = value;
            }
        }

        public void MyClick(ref ChooseItem item)
        {
            switch (this._kind)
            {
                case Helper.CompanyKind.Supplier:
                    ChooseSupplier f = new ChooseSupplier();
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        //Model.Company emp1 = f.SelectedItem as Model.Company;
                        //item = new ChooseItem(emp1, emp1.CompanyId, emp1.CompanyName0);
                    }                    
                    break;
                case Helper.CompanyKind.Customer:
                    ChooseCustoms f1 = new ChooseCustoms();
                    if (f1.ShowDialog() == DialogResult.OK)
                    {
                        //Model.Company emp = f1.SelectedItem as Model.Company;
                        //item = new ChooseItem(emp, emp.CompanyId, emp.CompanyName0);
                    }
                    break;
                default:
                    break;
            }
        }

        public void MyLeave(ref ChooseItem item)
        {

            //BL.CompanyManager manager = new Book.BL.CompanyManager();
            //Model.Company emp = manager.Get(item.ButtonText);
            //if (emp != null)
            //{
            //    if ((int)_kind == emp.CompanyKind)
            //    {
            //        item.EditValue = emp;
            //        item.LabelText = emp.CompanyName0;
            //        item.ButtonText = emp.CompanyId;
            //    }
            //    else 
            //    {
            //        switch (this._kind)
            //        {
            //            case Helper.CompanyKind.Supplier:
            //                item.ErrorMessage = Properties.Resources.ChooseSupplierError;
            //                break;
            //            case Helper.CompanyKind.Customer:
            //                item.ErrorMessage = Properties.Resources.ChooseCustomerError;
            //                break;
            //            default:
            //                break;
            //        }                    
            //    }
            //}
            //else
            //{
            //    switch (this._kind)
            //    {
            //        case Helper.CompanyKind.Supplier:
            //            item.ErrorMessage = Properties.Resources.ChooseSupplierError;
            //            break;
            //        case Helper.CompanyKind.Customer:
            //            item.ErrorMessage = Properties.Resources.Cho00oseCustomerError;
            //            break;
            //        default:
            //            break;
            //    }       
            //}           
        }

        #endregion
    }
}