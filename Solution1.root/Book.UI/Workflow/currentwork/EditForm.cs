using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//------------------------------------------------------------------------------
//
// ˵�������ļ��е�������Ҫ����   ��������
//
// author: �����
// create date��2009-12-16 ���� 11:45:01
//
//------------------------------------------------------------------------------
namespace Book.UI.Workflow.currentwork
{
    public partial class EditForm : DevExpress.XtraEditors.XtraForm
    {

        /// <summary>
        /// �������
        /// </summary>
        /// 
        private string wfrecordid;
        Model.wfrecord wfr;
        int n = 0;
        private BL.ProcessManager processmanage = new Book.BL.ProcessManager();

        private BL.wfrecordManager wfrecordmanage = new Book.BL.wfrecordManager();
        public EditForm()
        {
            InitializeComponent();
            IList<Model.wfrecord> lis = wfrecordmanage.GetMyexaming(BL.V.ActiveOperator, DateTime.Now.Date.AddDays(-14), DateTime.Now.AddDays(1).AddSeconds(-1));
            this.WindowState = FormWindowState.Maximized;
            n = lis.Count;
            this.bindingSource1.DataSource = lis;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IList<Model.wfrecord> lis = wfrecordmanage.GetMyexaming(BL.V.ActiveOperator, DateTime.Now.Date.AddDays(-14), DateTime.Now.AddDays(1).AddSeconds(-1));
            this.bindingSource1.DataSource = lis;
            if (n < lis.Count)
            {
                n = lis.Count;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            //Settings.ProduceManager.Techonlogy.EidtForm f = new Book.UI.Settings.ProduceManager.Techonlogy.EidtForm();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    IList<Model.wfrecord> lis = wfrecordmanage.GetMyexaming(BL.V.ActiveOperator);
            //    this.bindingSource1.DataSource = lis;
            //}
            wfr = this.bindingSource1.Current as Model.wfrecord;
            string logid = "";
            foreach (Model.wfrecordlog log in ((new BL.wfrecordlogManager()).Select()))
            {
                if (log.wfrecordId == wfr.wfrecordId)
                {
                    if (log.logtype == "���")
                    {
                        wfrecordid = wfr.wfrecordId;
                        labelControl2.Text = wfr.wfrecordname;
                        logid = log.logid;
                    }
                }
            }

            if (!string.IsNullOrEmpty(logid))
            {
                foreach (Model.TechonlogyHeader h in (new BL.TechonlogyHeaderManager()).Select())
                {
                    if (h.TechonlogyHeaderId == logid)
                    {
                        Settings.ProduceManager.Techonlogy.EidtForm f = new Book.UI.Settings.ProduceManager.Techonlogy.EidtForm(h);
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            IList<Model.wfrecord> lis = wfrecordmanage.GetMyexaming(BL.V.ActiveOperator, DateTime.Now.Date.AddDays(-14), DateTime.Now.AddDays(1).AddSeconds(-1));
                            this.bindingSource1.DataSource = lis;
                        }
                    }
                }
            }
        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (wfr != null)
            {
                Model.process noprocess = (new BL.ProcessManager()).GetProcessbyid(wfr.nowprocessid);
                if (noprocess != null)
                {
                    if (checkEdit1.Checked)
                    {
                        wfr.nowprocessid = noprocess.Processnex;
                        Model.process proc = (new BL.ProcessManager()).GetProcessbyid(wfr.nowprocessid);
                        if (proc.processType == "����")
                        {
                            wfr.allstate = (int)global::Helper.InvoiceAudit.Audited;
                            wfr.allovertime = DateTime.Today;

                        }
                        (new BL.wfrecordManager()).Update(wfr);
                        MessageBox.Show("�ύ�ɹ�!");
                    }
                    else
                    {
                        // wfr.allstate = "δͨ������";
                        wfr.allovertime = DateTime.Today;
                        (new BL.wfrecordManager()).Update(wfr);
                        #region MyRegion

                        string logid = "";
                        foreach (Model.wfrecordlog log in ((new BL.wfrecordlogManager()).Select()))
                        {
                            if (log.wfrecordId == wfr.wfrecordId)
                            {
                                if (log.logtype == "���")
                                {
                                    wfrecordid = wfr.wfrecordId;
                                    labelControl2.Text = wfr.wfrecordname;
                                    logid = log.logid;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(logid))
                        {
                            foreach (Model.TechonlogyHeader h in (new BL.TechonlogyHeaderManager()).Select())
                            {
                                if (h.TechonlogyHeaderId == logid)
                                {
                                    (new BL.TechonlogyHeaderManager()).Delete(h.TechonlogyHeaderId);
                                    MessageBox.Show("�ύ�ɹ�!");
                                }
                            }
                        }

                        #endregion
                    }
                    IList<Model.wfrecord> lis = wfrecordmanage.GetMyexaming(BL.V.ActiveOperator, DateTime.Now.Date.AddDays(-14), DateTime.Now.AddDays(1).AddSeconds(-1));
                    this.bindingSource1.DataSource = lis;
                    n = lis.Count;
                }
            }
        }

        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }
    }
}