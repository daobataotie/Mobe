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
    public partial class ConditionPronoteHeaderChooseForm : ConditionAChooseForm
    {
        //Q51  生產加工單
        private ConditionPronoteHeader condition;
        private int FlagIsProcee;

        public ConditionPronoteHeaderChooseForm()
        {
            InitializeComponent();
            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.coBoxSourceType.SelectedIndex = 0;
        }

        public ConditionPronoteHeaderChooseForm(int flagIsProcee)
        {
            InitializeComponent();
            this.FlagIsProcee = flagIsProcee;
            if (flagIsProcee == 0)
                this.coBoxSourceType.SelectedIndex = 1;
            else if (flagIsProcee == 1)
                this.coBoxSourceType.SelectedIndex = 2;
            else if (flagIsProcee == 2)
                this.coBoxSourceType.SelectedIndex = 3;

        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionPronoteHeader;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionPronoteHeader();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.dateEditStartDate.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.dateEditEndDate.DateTime;
            }
            this.condition.Product = this.buttonEditPro.EditValue as Model.Product;
            this.condition.PronoteHeaderIdStart = this.buttonEditProHeader1.Text == "" ? null : this.buttonEditProHeader1.Text;
            this.condition.PronoteHeaderIdEnd = this.buttonEditProHeader2.Text == "" ? null : this.buttonEditProHeader2.Text;
            this.condition.Customer = this.newChooseCustomer.EditValue as Model.Customer;
            if (this.coBoxSourceType.SelectedIndex == 0 || this.coBoxSourceType.SelectedIndex == -1)
                this.condition.SourceTpye = -1;
            else if (this.coBoxSourceType.SelectedIndex == 1)
                this.condition.SourceTpye = 0;
            else if (this.coBoxSourceType.SelectedIndex == 2)
                this.condition.SourceTpye = 5;
            else if (this.coBoxSourceType.SelectedIndex == 3)
                this.condition.SourceTpye = 4;

            this.condition.ProNameKey = this.TXTproNameKey.Text;
            this.condition.ProCusNameKey = this.TXTproCusNameKey.Text;
            this.condition.PronoteHeaderIdKey = this.txtpronoteHeaderIdKey.Text;
            this.condition.CusXOId = this.textEditCusXOId.Text;
        }

        private void buttonEditPro_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPro.EditValue = form.SelectedItem as Model.Product;

            }
            form.Dispose();
            GC.Collect();
        }

        private void ConditionPronoteHeaderChooseForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonEditProHeader1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(0);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditProHeader1.Text = f.SelectItem.PronoteHeaderID;
                this.buttonEditProHeader2.Text = f.SelectItem.PronoteHeaderID;
            }
            f.Dispose();
            GC.Collect();

        }

        private void buttonEditProHeader2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(0);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditProHeader2.Text = f.SelectItem.PronoteHeaderID;
            }
            f.Dispose();
            GC.Collect();

        }
    }
}