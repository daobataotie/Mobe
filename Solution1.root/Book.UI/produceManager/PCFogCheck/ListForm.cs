using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.produceManager.PCFogCheck
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        int tag = 0;
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.PCFogCheckManager();

            this.gridColumn10.Visible = false;
            this.btn_OK.Visible = false;
        }

        public ListForm(string invoiceCusId)
            : this()
        {
            this.tag = 1;
            this.bindingSource1.DataSource = (this.manager as BL.PCFogCheckManager).SelectByDateRage(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, invoiceCusId);
            gridControl1.RefreshDataSource();
        }

        /// <summary>
        /// 用于入料检验单选择雾都测试单
        /// </summary>
        /// <param name="ShowCheck"></param>
        public ListForm(bool ShowCheck)
            : this()
        {
            this.gridView1.OptionsBehavior.Editable = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.btn_OK.Visible = true;
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                this.tag = 0;
                return;
            }
            this.bindingSource1.DataSource = (this.manager as BL.PCFogCheckManager).SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, null, null, "");
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Query.ConditionPronoteHeaderChooseForm f = new Query.ConditionPronoteHeaderChooseForm();
            Query.ConditionAChooseForm f = new Query.ConditionAChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionA condition = f.Condition as Query.ConditionA;
                //this.bindingSource1.DataSource = (this.manager as BL.PCFogCheckManager).SelectByDateRage(condition.StartDate, condition.EndDate, condition.Product, condition.Customer, condition.CusXOId);
                this.bindingSource1.DataSource = (this.manager as BL.PCFogCheckManager).SelectByDate(condition.StartDate, condition.EndDate);
                this.gridControl1.RefreshDataSource();
            }
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.Show(this);
        }

        public string PCFogCheckId { get; set; }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            IList<Model.PCFogCheck> list = this.bindingSource1.DataSource as IList<Model.PCFogCheck>;
            if (list != null)
            {
                Model.PCFogCheck pcFogCheck = list.FirstOrDefault(P => P.IsChecked == true);
                if (pcFogCheck != null)
                    this.PCFogCheckId = pcFogCheck.PCFogCheckId;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
