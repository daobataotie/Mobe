using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.ProduceManager.Techonlogy
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾           完成时间:2009-11-2
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    class ChooseTechonlogy:Invoices.IChoose
    {
        /// <summary>
        /// 
        /// 工艺
        /// </summary>
        private Model.TechonlogyHeader obj;
        
        #region IChoose 成员
        public void MyClick(ref ChooseItem item)
        {
            ChooseTechonlogyForm f = new ChooseTechonlogyForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.TechonlogyHeader techonlogyHeader = f.SelectedItem as Model.TechonlogyHeader;
                item = new ChooseItem(techonlogyHeader, techonlogyHeader.Id, techonlogyHeader.TechonlogyHeadername);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.TechonlogyHeaderManager manager = new Book.BL.TechonlogyHeaderManager();
            Model.TechonlogyHeader techonlogyHeader = manager.GetById(item.ButtonText);
            if (techonlogyHeader != null)
            {
                item.EditValue = techonlogyHeader;
                item.LabelText = techonlogyHeader.TechonlogyHeadername;
                item.ButtonText = techonlogyHeader.Id;
            }
            else
            {
                item.ErrorMessage = "工藝路線頭出錯";
            }
        }
        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.TechonlogyHeader).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.TechonlogyHeader).TechonlogyHeadername;
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
                obj = (Model.TechonlogyHeader)value;
            }
        }
        #endregion
    }
}
