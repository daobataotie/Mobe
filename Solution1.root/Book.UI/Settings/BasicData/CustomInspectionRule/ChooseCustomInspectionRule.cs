using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.CustomInspectionRule
{
    public class ChooseCustomInspectionRule:IChoose
    {

        private Model.CustomInspectionRule  ojb;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseCustomInspectionRuleForm f = new ChooseCustomInspectionRuleForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.CustomInspectionRule CustomInspectionRule = f.SelectedItem as Model.CustomInspectionRule;
                item = new ChooseItem(CustomInspectionRule,CustomInspectionRule.Id, CustomInspectionRule.CustomInspectionRuleName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.CustomInspectionRuleManager manager = new Book.BL.CustomInspectionRuleManager();
            Model.CustomInspectionRule CustomInspectionRule = manager.GetById(item.ButtonText);

            if ( CustomInspectionRule != null)
            {
                item.EditValue = CustomInspectionRule;
                item.LabelText = CustomInspectionRule.CustomInspectionRuleName;
                item.ButtonText = CustomInspectionRule.Id;
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.CustomInspectionRule).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.CustomInspectionRule).CustomInspectionRuleName;
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
                ojb = (Model.CustomInspectionRule)value;
            }
        }

        #endregion
    }
}
