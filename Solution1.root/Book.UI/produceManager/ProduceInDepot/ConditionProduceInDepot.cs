using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.produceManager.PronoteHeader;
using Book.UI.Query;
using Book.UI.Invoices;

namespace Book.UI.produceManager.ProduceInDepot
{
    public partial class ConditionProduceInDepot : Book.UI.Query.ConditionAChooseForm
    {
        public ConditionProduceInDepot()
        {
            InitializeComponent();
            this.newChooseWorkhouse.Choose=new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.dateEditStartDate.DateTime = System.DateTime.Now.AddMonths(-1);
            this.dateEditEndDate.DateTime = DateTime.Now.AddDays(1).AddSeconds(-1);
        }

        private void buttonEditPronoteId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChoosePronoteHeaderDetailsForm form = new ChoosePronoteHeaderDetailsForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.PronoteHeader pronoteHeader = form.SelectItem as Model.PronoteHeader;
                if (pronoteHeader != null)
                    this.buttonEditPronoteId.Text = pronoteHeader.PronoteHeaderID;
                this.buttonEditPro.EditValue = null;
                this.textEditCusPro.EditValue = null;

            }
        }
        private ConditionProduceIn condition;

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionProduceIn;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionProduceIn();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            else
                this.condition.StartDate = this.dateEditStartDate.DateTime;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            else
                this.condition.EndDate = this.dateEditEndDate.DateTime;
            if (this.buttonEditPronoteId.Text == "")
                this.condition.PronoteHeaderId = null;
            else
                this.condition.PronoteHeaderId = this.buttonEditPronoteId.Text;
            if (this.newChooseWorkhouse.EditValue == null)
                this.condition.WorkHouseId = null;
            else
                this.condition.WorkHouseId = (this.newChooseWorkhouse.EditValue as Model.WorkHouse).WorkHouseId;
            this.condition.Product = buttonEditPro.EditValue as Model.Product;
            this.condition.CusXOId =this.textEditCusXOId.Text;
        }

        private void buttonEditPro_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm form = new ChooseProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditPro.EditValue = form.SelectedItem as Model.Product;
                this.textEditCusPro.Text = (form.SelectedItem as Model.Product).CustomerProductName;
            }
        }
    }
}