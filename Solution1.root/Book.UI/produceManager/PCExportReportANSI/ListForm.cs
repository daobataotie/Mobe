using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        int tag = 0;
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.PCExportReportANSIManager();
        }

        public ListForm(string invoiceCusId, string type)
            : this()
        {
            this.bindingSource1.DataSource = (this.manager as BL.PCExportReportANSIManager).SelectByInvoiceCusId(invoiceCusId, type);
            this.gridControl1.RefreshDataSource();
            this.tag = 1;

            this.Text = type + "外銷報告";
        }

        public ListForm(string FormText, bool isDelete)
            : this()
        {
            this.Text = FormText + "選擇";
            if (!isDelete)
                this.CancleDelete();
        }

        public string etype = string.Empty;
        public string Etype
        {
            set { etype = value; }
            get { return etype; }
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                tag = 0;
                return;
            }
            this.bindingSource1.DataSource = (this.manager as BL.PCExportReportANSIManager).SelectByDateRage(etype, DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, null, null, "");
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionPronoteHeader condition = f.Condition as Query.ConditionPronoteHeader;
                this.bindingSource1.DataSource = (this.manager as BL.PCExportReportANSIManager).SelectByDateRage(etype, condition.StartDate, condition.EndDate, condition.Product, condition.Customer, condition.CusXOId);
                this.gridControl1.RefreshDataSource();
            }
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            if (this.Text.Contains("AS") && !this.Text.Contains("ASTM"))
                return new ASEditForm();
            else if (this.Text.Contains("CEEN"))
                return new CEENEditsForm();
            else if (this.Text.Contains("CSA"))
                return new CSAEditForm();
            else if (this.Text.Contains("JIS"))
                return new JISEditForm();
            else if (this.Text.Contains("ANSI2015"))
                return new ANSI2015();
            else if (this.Text.Contains("ASTM"))
                return new XuejingASTMForm();
            else if (this.Text.Contains("EN174"))
                return new XuejingENForm();
            else
                return new EditForm();
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type;
            if (this.Text.Contains("AS") && !this.Text.Contains("ASTM"))
            {
                type = typeof(ASEditForm);
                return (ASEditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            }
            else if (this.Text.Contains("CEEN"))
            {
                type = typeof(CEENEditsForm);
                return (CEENEditsForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            }
            else if (this.Text.Contains("CSA"))
            {
                type = typeof(CSAEditForm);
                return (CSAEditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            }
            else if (this.Text.Contains("JIS"))
            {
                type = typeof(JISEditForm);
                return (JISEditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            }
            else if (this.Text.Contains("ANSI2015"))
            {
                type = typeof(ANSI2015);
                return (ANSI2015)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            }
            else if (this.Text.Contains("ASTM"))
            {
                type = typeof(XuejingASTMForm);
                return (XuejingASTMForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            }
            else if (this.Text.Contains("EN174"))
            {
                type = typeof(XuejingENForm);
                return (XuejingENForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            }
            else
            {
                type = typeof(EditForm);
                return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
            }
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.Show(this);
        }
    }
}