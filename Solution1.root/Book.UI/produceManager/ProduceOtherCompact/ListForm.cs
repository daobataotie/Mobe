using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace Book.UI.produceManager.ProduceOtherCompact
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2010-3-20
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        private IList<Model.ProduceOtherCompact> listDetail = new List<Model.ProduceOtherCompact>();
        int flag = 0;
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.manager = new BL.ProduceOtherCompactManager();
            this.gridView1.OptionsBehavior.Editable = true;
        }

        Model.MRSHeader mrsheader;
        public ListForm(Model.MRSHeader mrsheader)
            : this()
        {
            flag = 1;
            //listDetail = (this.manager as BL.ProduceOtherCompactManager).SelectByMRSHeaderId(mrsheader.MRSHeaderId);
            //foreach (Model.ProduceOtherCompact item in listDetail)
            //{
            //    item.Checkeds = true;
            //}
            this.mrsheader=mrsheader;

        }
        public ListForm(string InvoiceCusId)
            : this()
        {
            this.flag = 1;
            listDetail = (this.manager as BL.ProduceOtherCompactManager).GetByDate(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, InvoiceCusId, null, null, null);
            this.bindingSource1.DataSource = listDetail;
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Condition1ChooseForm f = new Condition1ChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Condition1 condition = f.Condition as Condition1;
                listDetail = (this.manager as BL.ProduceOtherCompactManager).selectByConditionRang(condition.StartDate, condition.EndDate, condition.Product, condition.mCustomerId, condition.mSupplierId, condition.ProduceOtherCompactId, condition.InvoiceCusXOId);
                foreach (Model.ProduceOtherCompact item in listDetail)
                {
                    item.Checkeds = true;
                }
                this.bindingSource1.DataSource = listDetail;
                this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
            }
        }

        protected override void RefreshData()
        {
            if (flag == 0)
            {
                listDetail = (this.manager as BL.ProduceOtherCompactManager).selectByConditionRang(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, null, null, null, null, null);
            }
            else if (flag == 1)
                listDetail = (this.manager as BL.ProduceOtherCompactManager).SelectByMRSHeaderId(mrsheader.MRSHeaderId);
            this.bindingSource1.DataSource = listDetail;
        }

        private void barBtnCondContinuousPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();
            if (this.bindingSource1.DataSource == null) return;
            ROContinuous f = new ROContinuous((from l in listDetail
                                               where l.Checkeds == true
                                               select l).ToList<Model.ProduceOtherCompact>());
            f.ShowPreviewDialog();
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.ShowDialog();
        }
    }
}