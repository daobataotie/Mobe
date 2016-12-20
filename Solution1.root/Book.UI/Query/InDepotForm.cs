using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    public partial class InDepotForm : ConditionChooseForm
    {
        public InDepotForm()
        {
            InitializeComponent();

            this.bindingSourceDepot.DataSource = (new BL.DepotManager()).Select();
            this.nccCustomerStart.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.nccCustomerEnd.Choose = new Settings.BasicData.Supplier.ChooseSupplier();

            this.date_Start.EditValue = DateTime.Now.Date.AddDays(-7);
            this.date_End.EditValue = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        private InDepot condition;

        public override Condition Condition
        {
            get
            {
                return condition;
            }
            set
            {
                condition = value as InDepot;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new InDepot();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_Start.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.date_Start.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.date_End.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.date_End.DateTime;
            }

            this.condition.InDepotIdEnd = this.txt_InDepotIdStart.Text;
            this.condition.InDepotIdStart = this.txt_InDepotIdEnd.Text;
            this.condition.DepotIdStart = this.lookUpEditDepotStart.EditValue == null ? null : this.lookUpEditDepotStart.EditValue.ToString();
            this.condition.DepotIdEnd = this.lookUpEditDepotEnd.EditValue == null ? null : this.lookUpEditDepotEnd.EditValue.ToString();
            this.condition.SupplierStart = this.nccCustomerStart.EditValue == null ? null : this.nccCustomerStart.EditValue as Model.Supplier;
            this.condition.SupplierEnd = this.nccCustomerEnd.EditValue == null ? null : this.nccCustomerEnd.EditValue as Model.Supplier;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}