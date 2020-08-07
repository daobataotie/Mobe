using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Query;
using System.Linq;
namespace Book.UI.produceManager.PronoteHeader
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        int flag = 0;
        int sourceType = 0;
        int tag = 0;
        Model.MRSHeader mrsheader;
        private IList<Model.PronoteHeader> listDetail = new List<Model.PronoteHeader>();
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.gridView1.OptionsBehavior.Editable = true;
            this.gridColumnCheck.OptionsColumn.AllowEdit = true;
            this.manager = new BL.PronoteHeaderManager();
        }

        public ListForm(string invoiceCusId)
            : this()
        {
            this.tag = 1;

            listDetail = (this.manager as BL.PronoteHeaderManager).GetByDateMa(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, invoiceCusId, null, null, null, -1, null, false, null, null, null, false, false, false, null);

            foreach (Model.PronoteHeader pronoteHeader in listDetail)
            {
                pronoteHeader.Checkeds = true;
            }
            this.bindingSource1.DataSource = listDetail;
        }

        public ListForm(int flagIsProcee)
            : this()
        {
            sourceType = flagIsProcee;
            switch (flagIsProcee)
            {
                case 0:
                    this.Text = "生產管理-生產加工單列表";
                    break;
                case 4:
                    this.Text = "生產管理-" + Properties.Resources.ZZJiaGong + "列表";
                    break;
                case 5:
                    this.Text = "生產管理-" + Properties.Resources.GZZhiShi + "列表";
                    break;
                default:
                    this.Text = "";
                    break;
            }
        }

        public ListForm(Model.MRSHeader mrsheader, int sourceType)
            : this()
        {
            // this.barButtonItem2.item = DevExpress.XtraBars.BarItemVisibility.Always;
            this.mrsheader = mrsheader;
            flag = 1;

            switch (sourceType)
            {
                case 0:
                    this.Text = "生產管理-生產加工單列表";
                    break;
                case 4:
                    this.Text = "生產管理-" + Properties.Resources.ZZJiaGong + "列表";
                    break;
                case 5:
                    this.Text = "生產管理-" + Properties.Resources.GZZhiShi + "列表";
                    break;
                default:
                    this.Text = "";
                    break;
            }
            this.sourceType = sourceType;
            //listDetail = (this.manager as BL.PronoteHeaderManager).Select(mrsheader);
            //foreach (Model.PronoteHeader pronoteHeader in listDetail)
            //{
            //    pronoteHeader.Checkeds = true;
            //}
            //this.bindingSource1.DataSource = listDetail;

        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override void RefreshData()
        {
            if (this.tag == 1)
            {
                return;
            }
            if (flag == 0)
            {
                listDetail = (this.manager as BL.PronoteHeaderManager).GetByDateMa(DateTime.Now.AddDays(-7).Date, global::Helper.DateTimeParse.EndDate, null, null, null, null, null, -1, null, false, null, null, null, false, false, false, null);
            }
            else
                listDetail = (this.manager as BL.PronoteHeaderManager).Select(mrsheader);
            foreach (Model.PronoteHeader pronoteHeader in listDetail)
            {
                pronoteHeader.Checkeds = true;
            }
            this.bindingSource1.DataSource = listDetail;
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {

            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, new object[] { args[0], sourceType }, null, null);
        }


        //多条件查询
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionPronoteHeaderChooseForm f = new ConditionPronoteHeaderChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                ConditionPronoteHeader condition = f.Condition as ConditionPronoteHeader;
                listDetail = (this.manager as BL.PronoteHeaderManager).GetByDateMa(condition.StartDate, condition.EndDate, condition.Customer, condition.CusXOId, condition.Product, condition.PronoteHeaderIdStart, condition.PronoteHeaderIdEnd, condition.SourceTpye, null, false, condition.ProNameKey, condition.ProCusNameKey, condition.PronoteHeaderIdKey, false, false, false, condition.HandbookId);
                foreach (Model.PronoteHeader pronoteHeader in listDetail)
                {
                    pronoteHeader.Checkeds = true;
                }
                this.bindingSource1.DataSource = listDetail;
                f.Dispose();
                GC.Collect();
            }
            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.UpdateCurrentRow() || !this.gridView1.PostEditor()) return;
            if (listDetail == null || listDetail.Count == 0) return;
            if (this.sourceType == 4)
            {
                //RODetail f = new RODetail(    (from l in listDetail
                //                           where l.Checkeds == true
                //                           select l).ToList<Model.PronoteHeader>(), this.sourceType);

                RODetail f = new RODetail((this.manager as BL.PronoteHeaderManager).Select((from l in listDetail
                                                                                            where l.Checkeds == true
                                                                                            select l.PronoteHeaderID).ToList<string>()), this.sourceType);
                f.ShowPreviewDialog();
            }
            else
            {
                //ROZZJiaGongDetail f = new ROZZJiaGongDetail((from l in listDetail
                //                                             where l.Checkeds == true
                //                                             select l).ToList<Model.PronoteHeader>(), this.sourceType);
                IList<string> a = (from l in listDetail
                                   where l.Checkeds == true
                                   select l.PronoteHeaderID).ToList<string>();
                ROZZJiaGongDetail f = new ROZZJiaGongDetail((this.manager as BL.PronoteHeaderManager).Select((from l in listDetail
                                                                                                              where l.Checkeds == true
                                                                                                              select l.PronoteHeaderID).ToList<string>()), this.sourceType);
                f.ShowPreviewDialog();
            }
        }

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                //  popupMenu1.ShowPopup(gridControl1, ((MouseEventArgs)e).X, ((MouseEventArgs)e).Y);
            }
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.Show(this);
        }

        //public override Model.PronoteHeader SelectItem
        //{
        //    get
        //    {
        //        if (this.bindingSource1.Current == null)
        //            return null;
        //        else
        //        return this.bindingSource1.Current as Model.PronoteHeader; }
        //}
    }
}