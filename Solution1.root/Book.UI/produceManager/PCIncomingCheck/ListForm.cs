using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.PCIncomingCheck
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        int tag = 0;
        public ListForm()
        {
            InitializeComponent();
            this.gridView1.OptionsBehavior.Editable = true;
        }
        BL.PCIncomingCheckManager pCIncomingCheckManager = new Book.BL.PCIncomingCheckManager();
        BL.PCIncomingCheckDetailManager detailManager = new Book.BL.PCIncomingCheckDetailManager();

        new public Model.PCIncomingCheck SelectItem { get; set; }

        public IList<Model.PCIncomingCheck> keys = new List<Model.PCIncomingCheck>();
        public ListForm(int i)
            : this()
        {
            tag = i;
        }
        //public ListForm(string invoiceCusId)
        //    : this()
        //{
        //    this.bindingSource1.DataSource = this.detailManager.SelectByCondition(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null);
        //    this.gridControl1.RefreshDataSource();
        //    this.tag = 1;
        //}

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        protected override void RefreshData()
        {
            if (tag == 1)
            {
                tag = 0;
                return;
            }
            this.bindingSource1.DataSource = this.detailManager.SelectByCondition(DateTime.Now.AddMonths(-1), DateTime.Now, null);
            this.gridControl1.RefreshDataSource();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            Model.PCIncomingCheck model = this.pCIncomingCheckManager.Get((args[0] as Model.PCIncomingCheckDetail) == null ? "" : (args[0] as Model.PCIncomingCheckDetail).PCIncomingCheckId);
            args[0] = model;
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Form f = this.GetEditForm(new object[] { this.bindingSource1.Current });
            if (f != null)
                f.ShowDialog(this);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForm f = new ConditionForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Condition c = f.condition;
                this.bindingSource1.DataSource = this.detailManager.SelectByCondition(c.StartDate, c.EndDate, c.LotNumber);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (tag == 2)
            {
                keys.Clear();
                IList<Model.PCIncomingCheckDetail> list = (this.bindingSource1.DataSource as List<Model.PCIncomingCheckDetail>);
                if (list != null && list.Count > 0)
                {
                    list = list.Where(d => d.IsCheck == true).ToList();
                    foreach (var item in list)
                    {
                        if (keys.Any(d => d.PCIncomingCheckId == item.PCIncomingCheckId))
                            continue;
                        keys.Add(item.PCIncomingCheck);
                    }
                }
            }
        }

    }
}