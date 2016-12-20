using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.Options
{

    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-14
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (DevExpress.XtraTab.XtraTabPage p in this.xtraTabControl1.TabPages)
            {
                BaseOptionsPage page = null;
                switch ((string)p.Tag)
                {
                    case "invoicenumberrule":
                    case "entitynumberrule":
                        page = new Options01Page(p.Tag.ToString());
                        page.Dock = DockStyle.Fill;
                        break;
                    default:
                        break;
                }

                if (page != null)
                {
                    p.Controls.Add(page);
                    page.Bounds = p.ClientRectangle;
                    page.DoLoad();
                }

            }
        }

        private void DoSave()
        {
            BaseOptionsPage page = null;
            foreach (DevExpress.XtraTab.XtraTabPage p in this.xtraTabControl1.TabPages)
            {
                if (p.Controls.Count == 0) continue;

                page = p.Controls[0] as BaseOptionsPage;
                if (page != null)
                {
                    page.DoSave();
                }
            }
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch ((string)e.Item.Tag)
            {
                case "save":
                    this.DoSave();
                    break;

                default:
                    break;
            }
        }
    }
}