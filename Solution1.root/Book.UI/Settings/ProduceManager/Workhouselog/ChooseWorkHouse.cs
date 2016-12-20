using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.ProduceManager.Workhouselog
{

    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-11-15
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    class ChooseWorkHouse: Invoices.IChoose
    {
        private Model.WorkHouse obj;

        #region IChoose 成员
        public void MyClick(ref ChooseItem item)
        {
            ChooseWorkHouseFrom f = new ChooseWorkHouseFrom();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.WorkHouse workHouse = f.SelectedItem as Model.WorkHouse;
                item = new ChooseItem(workHouse, workHouse.Workhousename, null);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.WorkHouseManager manager = new Book.BL.WorkHouseManager();
            Model.WorkHouse WorkHouse = manager.Get(obj.WorkHouseId);
            if (WorkHouse != null)
            {
                item.EditValue = WorkHouse;
                item.LabelText = null;
            }
            else
            {
                item.ErrorMessage = "生產站出错";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.WorkHouse).Workhousename;
            }
        }

        public string LableText
        {
            get
            {
                return null;//EditValue == null ? string.Empty : (EditValue as Model.Workhouselog).Workhouselogcontent;
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
                obj = (Model.WorkHouse)value;
            }
        }
        #endregion
    }
}
