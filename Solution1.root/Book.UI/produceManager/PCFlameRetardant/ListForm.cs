using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.PCFlameRetardant
{
    public partial class ListForm : Book.UI.Settings.BasicData.BaseListForm
    {
        BL.PCFlameRetardantDetailManager _pCFlameRetardantDetailManager = new Book.BL.PCFlameRetardantDetailManager();

        public ListForm()
        {
            InitializeComponent();

            this.gridColumn7.Visible = false;
            this.btn_OK.Visible = false;
        }

        /// <summary>
        /// 用于入料检验单选择阻燃性测试表
        /// </summary>
        /// <param name="ShowCheck"></param>
        public ListForm(bool ShowCheck)
            : this()
        {
            this.gridView1.OptionsBehavior.Editable = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.btn_OK.Visible = true;
        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = _pCFlameRetardantDetailManager.SelectByDateRage(DateTime.Now.AddDays(-15), global::Helper.DateTimeParse.EndDate, null, null);
            this.gridView1.GroupPanelText = "默認顯示半个月内的記錄";
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForm f = new ConditionForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.bindingSource1.DataSource = _pCFlameRetardantDetailManager.SelectByDateRage(f.StartDate, f.EndDate, f.ProductId, f.CusXOId);
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
            Model.PCFlameRetardantDetail model = args[0] as Model.PCFlameRetardantDetail;
            if (model != null)
                args[0] = model.PCFlameRetardantId;

            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.ShowDialog();
        }

        public string PCFlameRetardantId { get; set; }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            IList<Model.PCFlameRetardantDetail> list = this.bindingSource1.DataSource as IList<Model.PCFlameRetardantDetail>;
            if (list != null)
            {
                Model.PCFlameRetardantDetail detail = list.FirstOrDefault(P => P.IsChecked == true);
                if (detail != null)
                    this.PCFlameRetardantId = detail.PCFlameRetardantId;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}