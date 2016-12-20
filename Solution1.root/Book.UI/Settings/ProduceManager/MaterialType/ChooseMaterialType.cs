using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.ProduceManager.MaterialType
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    class ChooseMaterialType :Invoices.IChoose
    {
        /// <summary>
        /// 
        /// 物料类型
        /// </summary>
        private Model.MaterialType obj;

        #region IChoose 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void MyClick(ref ChooseItem item)
        {
            ChooseMaterialTypeForm f = new ChooseMaterialTypeForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.MaterialType MaterialType = f.SelectedItem as Model.MaterialType;
                item = new ChooseItem(MaterialType, MaterialType.Id, MaterialType.MaterialTypeName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.MaterialTypeManager manager = new Book.BL.MaterialTypeManager();
            Model.MaterialType MaterialType = manager.GetById(item.ButtonText);
            if (MaterialType != null)
            {
                item.EditValue = MaterialType;
                item.LabelText = MaterialType.MaterialTypeName;
                item.ButtonText = MaterialType.Id;
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
                return EditValue == null ? string.Empty : (EditValue as Model.MaterialType).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.MaterialType).MaterialTypeName;
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
                obj = (Model.MaterialType)value;
            }
        }

        #endregion
    }
}
