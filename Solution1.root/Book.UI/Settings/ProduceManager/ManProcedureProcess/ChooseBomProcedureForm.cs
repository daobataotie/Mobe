using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.ProduceManager.ManProcedureProcess
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-21
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ChooseBomProcedureForm : Settings.BasicData.BaseChooseForm
    {
        protected BL.ProceduresManager proceduresManager = new Book.BL.ProceduresManager();

        #region Construcotrs

       public ChooseBomProcedureForm()
        {
            InitializeComponent();            
            this.manager = new Book.BL.TechonlogyHeaderManager();
        }

        #endregion

       #region 重写方法
       protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.Products.EditForm();
        }

        //bool isFirst = true;

        protected override void LoadData()
        {
            BL.TechonlogyHeaderManager manager = this.manager as BL.TechonlogyHeaderManager;

            this.BomBomTechonlogyHeaderSource.DataSource = manager.Select();
        }
       #endregion 


        /// <summary>
        /// 数据源改变触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BomProcedurebindingSource_CurrentChanged(object sender, EventArgs e)
        {
            Model.TechonlogyHeader technology = this.BomBomTechonlogyHeaderSource.Current as Model.TechonlogyHeader;
            if (technology == null)
                return;
            this.bindingSource1.DataSource = this.proceduresManager.Select(technology);
        }
    }
}