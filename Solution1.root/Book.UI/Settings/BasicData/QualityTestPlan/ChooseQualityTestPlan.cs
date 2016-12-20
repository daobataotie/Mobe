using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.QualityTestPlan
{
    public  class ChooseQualityTestPlan:IChoose
    {

        private Model.QualityTestPlan ojb;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseQualityTestPlanForm f = new ChooseQualityTestPlanForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.QualityTestPlan qualitytestplan = f.SelectedItem as Model.QualityTestPlan;
                item = new ChooseItem(qualitytestplan, qualitytestplan.Id,qualitytestplan.QualityTestPlanName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.QualityTestPlanManager manager = new Book.BL.QualityTestPlanManager();
            Model.QualityTestPlan qualitytestplan = manager.GetById(item.ButtonText);

            if (qualitytestplan != null)
            {
                item.EditValue = qualitytestplan;
                item.LabelText = qualitytestplan.QualityTestPlanName;
                item.ButtonText = qualitytestplan.Id;
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.QualityTestPlan).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.QualityTestPlan).QualityTestPlanName;
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
                ojb = (Model.QualityTestPlan)value;
            }
        }

        #endregion
    }
}
