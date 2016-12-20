using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceMaterialExit
{
    public partial class ConditionForList : Query.ConditionChooseForm
    {
        private ConditionForListCls condition;

        public override Book.UI.Query.Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                base.Condition = value as ConditionForListCls;
            }
        }

        public ConditionForList()
        {
            InitializeComponent();
        }

        private void ConditionForList_Load(object sender, EventArgs e)
        {
            this.StartdateEdit.DateTime = DateTime.Now.AddDays(-3).Date;
            this.EnddateEdit.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionForListCls();

            if (this.StartdateEdit.EditValue == null)
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            else
                this.condition.StartDate = this.StartdateEdit.DateTime;

            if (this.EnddateEdit.EditValue == null)
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            else
                this.condition.EndDate = this.EnddateEdit.DateTime;


            this.condition.StartPMEid = this.txtStartPMEid.Text;
            this.condition.EndPMEid = this.txtEndPMEid.Text;
            this.condition.StartPronoteHeaderId = this.txtStartPronoteheadid.Text;
            this.condition.EndPronoteHeaderId = this.txtEndPronoteheadid.Text;

            this.condition.StartProduct = this.btnEditStartProduct.EditValue as Model.Product;
            this.condition.EndProduct = this.btnEditEndProduct.EditValue as Model.Product;
        }

        private void btnEditStartProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btnEditStartProduct.EditValue = f.SelectedItem as Model.Product;
                this.btnEditEndProduct.EditValue = f.SelectedItem as Model.Product;
            }
            f.Dispose();
            GC.Collect();
        }

        private void btnEditEndProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.btnEditEndProduct.EditValue = f.SelectedItem as Model.Product;
            }
            f.Dispose();
            GC.Collect();
        }


    }
}