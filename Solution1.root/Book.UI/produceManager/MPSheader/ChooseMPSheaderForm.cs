using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.MPSheader
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

/// 编 码 人: 马艳军             完成时间:2010-3-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    public partial class ChooseMPSheaderForm : DevExpress.XtraEditors.XtraForm
    {
        //计划管理
        BL.MPSheaderManager mpsheaderManager = new Book.BL.MPSheaderManager();
        //计划
        Model.MPSheader mpsheader = new Book.Model.MPSheader();
        //计划详细管理
        BL.MPSdetailsManager MPSdetailsManager = new Book.BL.MPSdetailsManager();
        //计划详细
        IList<Model.MPSheader> details = new List<Model.MPSheader>();   

        public ChooseMPSheaderForm()
        {
            InitializeComponent();
            this.details = this.mpsheaderManager.Select();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseMPSheaderForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = this.details;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (Model.MPSheader MPSheader in this.details)
            {
                if (MPSheader.Checkeds == true)
                {
                    MPSheader.Details = this.MPSdetailsManager.Select(MPSheader);
                    if (MPSheader.Details.Count != 0)
                    {
                        produceManager.MRSHeader.EditForm._mpsheader.Add(MPSheader);
                        //produceManager.PronoteHeader.EditForm._mpsheader.Add(MPSheader);
                        //produceManager.ProduceOtherCompact.EditForm._mpsheader.Add(MPSheader);
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
        }


        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
         //   Model.MPSheader mPSheader = new Book.Model.MPSheader();
         //   produceManager.MRSHeader.EditForm f = new Book.UI.produceManager.MRSHeader.EditForm();
         //// produceManager.ProductionNotice.EditForm f1 = new Book.UI.produceManager.ProductionNotice.EditForm();
         //   if (f.ShowDialog(this) != DialogResult.OK) return;
         //  // if (f1.ShowDialog(this) != DialogResult.OK) return;
            this.Close();
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DateTime start = global::Helper.DateTimeParse.NullDate;
            DateTime end = global::Helper.DateTimeParse.NullDate;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit1.DateTime, new DateTime()))
            {
                 start = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                 start = this.dateEdit1.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit2.DateTime, new DateTime()))
            {
                 end = global::Helper.DateTimeParse.EndDate;
            }
            else
            {
                 end = this.dateEdit2.DateTime;
            }
            this.bindingSource1.DataSource = this.mpsheaderManager.Select(start,end);
        }
    }
}