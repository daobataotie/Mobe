using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{

    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾             完成时间:2009-4-3
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class BaseForm : DevExpress.XtraEditors.XtraForm
    {
        protected Condition condition;  

        /// <summary>
        /// 无参构造
        /// </summary>
        public BaseForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 带一个参数构造
        /// </summary>
        /// <param name="condition"></param>
        public BaseForm(Condition condition)
            : this()
        {
            this.condition = condition;
        }

        protected virtual DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch ((string)e.Item.Tag)
            {
                case "print":
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        DevExpress.XtraReports.UI.XtraReport r = this.GetReport();
                        if (r != null)
                        {
                            r.ShowPreview();
                        }
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                    break;

                default:
                    break;
            }
        }

        protected virtual void DoQuery()
        {
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Load(object sender, EventArgs e)
        {
            this.DoQuery();
        }
    }
}