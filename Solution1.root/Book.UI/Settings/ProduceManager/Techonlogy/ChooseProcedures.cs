using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.ProduceManager.Techonlogy
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾           完成时间:2009-11-1
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    class ChooseProcedures : Invoices.IChoose
    {
        /// <summary>
        /// 
        /// 工序
        /// </summary>
        private Model.Procedures obj;

        #region IChoose 成员
        public void MyClick(ref ChooseItem item)
        {
            ChooseProceduresForm f = new ChooseProceduresForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Procedures procedures = f.SelectedItem as Model.Procedures;
                item = new ChooseItem(procedures, procedures.Id, procedures.Procedurename);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.ProceduresManager manager = new Book.BL.ProceduresManager();
            Model.Procedures procedures = manager.GetById(item.ButtonText);
            if (procedures != null)
            {
                item.EditValue = procedures;
                item.LabelText = procedures.Procedurename;
                item.ButtonText = procedures.Id;
            }
            else
            {
                item.ErrorMessage = "工序出錯";
            }
        }
        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Procedures).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Procedures).Procedurename;
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
                obj = (Model.Procedures)value;
            }
        }
        #endregion
    }
}
