using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
//------------------------------------------------------------------------------
//
// 说明：该文件中的内容主要处理   我的申请
//
// author: 徐彦飞
// create date：2009-12-16 上午 11:45:01
//
//------------------------------------------------------------------------------
namespace Book.UI.Workflow.currentwork
{
    public partial class PersonworkForm : DevExpress.XtraEditors.XtraForm
    {
        private const string helpfile = "erp.chm";

        private BL.ProcessManager processmanage = new Book.BL.ProcessManager();

        private BL.wfrecordManager wfrecordmanage = new Book.BL.wfrecordManager();

        private BL.OperatorsManager operatorsManager = new Book.BL.OperatorsManager();

        IList<Model.wfrecord> listall;

        private string wfrecordid;

        Model.wfrecord wfr;

        public PersonworkForm()
        {
            InitializeComponent();
            timer1.Interval = 1800000;
            this.DeStart.DateTime = DateTime.Now.Date.AddDays(-14);
            this.DeEnd.DateTime = DateTime.Now;
        }

        private void PersonworkForm_Load(object sender, EventArgs e)
        {
            Loadoperato();     //登录用户信息
            loadexaming();     //加载待审信息
            //loadexam();      //我的申请
        } 

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadexaming();//待审核数量
            //loadexam(); //待审核列表
        }

        // 加载登录用户
        private void Loadoperato()
        {
            Model.Operators currentoper = BL.V.ActiveOperator;
            labelControl4.Text = currentoper.OperatorName;
        }

        // 加载待审信息
        private void loadexaming()
        {
            IList<Model.wfrecord> lis = wfrecordmanage.GetMyexaming(BL.V.ActiveOperator, this.DeStart.DateTime, this.DeEnd.DateTime.AddDays(1).AddSeconds(-1));
            this.bindingSource1.DataSource = lis;
            labelControl2.Text = lis.Count + " 條待審信息 ";
        }

        //加载我的申请
        private void loadexam()
        {
            //加载我的申请
            listall = wfrecordmanage.Select();
            List<Model.wfrecord> Mylist = new List<Book.Model.wfrecord>();
            foreach (Model.wfrecord wfr in listall)
            {
                if (wfr.applyuserid == BL.V.ActiveOperator.OperatorsId)
                {
                    Mylist.Add(wfr);
                }
            }
            this.bindingSource2.DataSource = Mylist;
            labelControl3.Text = Mylist.Count + " 申請結果信息 ";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            #region 注释
            //if (wfr != null)
            //{
            //    //bool percheck = true;
            //    Model.process noprocess = (new BL.ProcessManager()).GetProcessbyid(wfr.nowprocessid);
            //    if (noprocess != null)
            //    {
            //        // checkedListBoxControl1.
            //        if (radioButton1.Checked)
            //        {
            //            wfr.nowprocessid = noprocess.Processnex;
            //            Model.process proc = (new BL.ProcessManager()).GetProcessbyid(wfr.nowprocessid);
            //            if (proc.processType == "結束")
            //            {
            //                wfr.allstate = (int)global::Helper.InvoiceAudit.Audited;
            //                wfr.allovertime = DateTime.Today;
            //            }
            //            (new BL.wfrecordManager()).Update(wfr);
            //            MessageBox.Show("提交成功!");
            //        }
            //        else
            //        {
            //            // wfr.allstate = "未通過審批";
            //            wfr.allovertime = DateTime.Today;
            //            (new BL.wfrecordManager()).Update(wfr);
            //            #region MyRegion

            //            string logid = "";
            //            foreach (Model.wfrecordlog log in ((new BL.wfrecordlogManager()).Select()))
            //            {
            //                if (log.wfrecordId == wfr.wfrecordId)
            //                {
            //                    if (log.logtype == "添加")
            //                    {
            //                        wfrecordid = wfr.wfrecordId;
            //                        labelControl2.Text = wfr.wfrecordname;
            //                        logid = log.logid;
            //                    }
            //                }
            //            }
            //            if (!string.IsNullOrEmpty(logid))
            //            {
            //                foreach (Model.TechonlogyHeader h in (new BL.TechonlogyHeaderManager()).Select())
            //                {
            //                    if (h.TechonlogyHeaderId == logid)
            //                    {
            //                        (new BL.TechonlogyHeaderManager()).Delete(h.TechonlogyHeaderId);
            //                        MessageBox.Show(this.Text, Properties.Resources.SaveSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    }
            //                }
            //                if (!string.IsNullOrEmpty(logid))
            //                {
            //                    foreach (Model.InvoiceXJ h in (new BL.InvoiceXJManager()).Select())
            //                    {
            //                        if (h.InvoiceId == logid)
            //                        {
            //                            (new BL.InvoiceXJManager()).Delete(h.InvoiceId);
            //                            MessageBox.Show(Properties.Resources.SaveSuccess, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        }
            //                    }
            //                }
            //            }

            //            #endregion
            //        }
            //        IList<Model.wfrecord> lis = wfrecordmanage.GetMyexaming(BL.V.ActiveOperator);
            //        this.bindingSource1.DataSource = lis;
            //        labelControl6.Text = "";
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("請選擇");
            //}
            #endregion
            //GridView view = this.gridView1;
            //GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            //if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            //{
            if (this.bindingSource1.Current != null)
            {
                wfr = this.bindingSource1.Current as Model.wfrecord;
                // string formname = "Settings.ProduceManager.Techonlogy.EidtForm";//wfr.Workflow.Tables.TableCode;
                string formname = "produceManager.ProduceMaterial.EditForm";
                if (wfr != null)
                {
                    Form f = null;
                    foreach (Form form in this.MdiParent.MdiChildren)
                    {
                        if (form.GetType().FullName.EndsWith(formname))
                        {
                            f = form;
                            break;
                        }
                    }

                    if (f == null)
                    {
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                        f = (Form)assembly.CreateInstance(string.Format("{0}.{1}", assembly.GetName().Name, formname), false, System.Reflection.BindingFlags.CreateInstance, null, new object[] { wfr.wfrecordname }, null, null);
                    }
                    f.MdiParent = this.MdiParent;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //GridView view = sender as GridView;
            //GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            //if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            //{
            if (this.bindingSource1.Current != null)
            {
                wfr = this.bindingSource1.Current as Model.wfrecord;
                // string formname = "Settings.ProduceManager.Techonlogy.EidtForm";//wfr.Workflow.Tables.TableCode;
                string formname = "produceManager.ProduceMaterial.EditForm";
                if (wfr != null)
                {
                    Form f = null;
                    foreach (Form form in this.MdiParent.MdiChildren)
                    {
                        if (form.GetType().FullName.EndsWith(formname))
                        {
                            f = form;
                            break;
                        }
                    }

                    if (f == null)
                    {
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                        f = (Form)assembly.CreateInstance(string.Format("{0}.{1}", assembly.GetName().Name, formname), false, System.Reflection.BindingFlags.CreateInstance, null, new object[] { wfr.wfrecordname }, null, null);
                    }

                    f.MdiParent = this.MdiParent;
                    f.Show();
                    f.BringToFront();
                    #region Note
                    //wfr.Workflow.Tables.TableCode
                    //    string logid = "";
                    //    foreach (Model.wfrecordlog log in ((new BL.wfrecordlogManager()).Select()))
                    //    {
                    //        if (log.wfrecordId == wfr.wfrecordId)
                    //        {
                    //            if (log.logtype == "添加")
                    //            {
                    //                wfrecordid = wfr.wfrecordId;
                    //                labelControl6.Text = wfr.wfrecordname;
                    //                logid = log.logid;
                    //            }
                    //        }
                    //    }
                    //    if (!string.IsNullOrEmpty(logid))
                    //    {
                    //        foreach (Model.TechonlogyHeader h in (new BL.TechonlogyHeaderManager()).Select())
                    //        {
                    //            if (h.TechonlogyHeaderId == logid)
                    //            {
                    //                Settings.ProduceManager.Techonlogy.EidtForm f = new Book.UI.Settings.ProduceManager.Techonlogy.EidtForm(h);
                    //                if (f.ShowDialog() == DialogResult.OK)
                    //                {
                    //                    IList<Model.wfrecord> lis = wfrecordmanage.GetMyexaming(BL.V.ActiveOperator);
                    //                    this.bindingSource1.DataSource = lis;
                    //                }
                    //            }
                    //        }
                    //        foreach (Model.InvoiceXJ h in new BL.InvoiceXJManager().Select())
                    //        {
                    //            if (h.InvoiceId == logid)
                    //            {
                    //                Invoices.XJ.EditForm f = new Invoices.XJ.EditForm(h);
                    //                f.Show();
                    //                //if (f.ShowDialog() == DialogResult.OK)
                    //                //{
                    //                //    IList<Model.wfrecord> lis = wfrecordmanage.GetMyexaming(BL.V.ActiveOperator);
                    //                //    this.bindingSource1.DataSource = lis;

                    //                //}
                    //            }
                    //        }
                    //    }
                    #endregion
                }
            }
        }
        //}

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.wfrecord> details = this.bindingSource2.DataSource as IList<Model.wfrecord>;
            if (details == null || details.Count < 1) return;
            //Model.CustomerProducts detail = details[e.ListSourceRowIndex].PrimaryKey;
            Model.Operators opertors = this.operatorsManager.Get(details[e.ListSourceRowIndex].applyuserid);
            if (opertors == null) return;
            if (e.Column.Name == this.gridColumnapplyuser.Name)
            {
                e.DisplayText = opertors.OperatorName;
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            wfr = this.bindingSource1.Current as Model.wfrecord;
            if (wfr != null)
            {
                // string logid = "";
                foreach (Model.wfrecordlog log in ((new BL.wfrecordlogManager()).Select()))
                {
                    if (log.wfrecordId == wfr.wfrecordId)
                    {
                        if (log.logtype == "添加")
                        {
                            wfrecordid = wfr.wfrecordId;
                            labelControl6.Text = wfr.wfrecordname;
                            //logid = log.logid;
                        }
                    }
                }
            }
        }

        //切换页触发
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Name == "xtraTabPage2")
            {
                this.loadexam();
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.wfrecord> details = this.bindingSource1.DataSource as IList<Model.wfrecord>;
            if (details == null || details.Count < 1) return;
            Model.Operators opertors = this.operatorsManager.Get(details[e.ListSourceRowIndex].applyuserid);
            if (opertors == null) return;
            if (e.Column.Name == "g1ColApplyuser")
            {
                e.DisplayText = opertors.Employee.EmployeeName;
            }
        }

        private void Btn_SearchByDate_Click(object sender, EventArgs e)
        {
            IList<Model.wfrecord> lis = wfrecordmanage.GetMyexaming(BL.V.ActiveOperator, this.DeStart.DateTime, this.DeEnd.DateTime.AddDays(1).AddSeconds(-1));
            this.bindingSource1.DataSource = lis;
            labelControl2.Text = lis.Count + " 條待審信息 ";
        }
    }
}