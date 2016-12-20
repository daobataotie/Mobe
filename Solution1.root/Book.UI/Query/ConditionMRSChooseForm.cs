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
    public partial class ConditionMRSChooseForm : ConditionAChooseForm
    {
        public ConditionMRSChooseForm()
        {
            InitializeComponent();
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddDays(-15);
            this.dateEditEndDate.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            //this.comboBoxEditMpsStart.Properties.Items.Clear();
            //this.comboBoxEditMpsEnd.Properties.Items.Clear();
            //BL.MPSheaderManager MPSheaderManager = new Book.BL.MPSheaderManager();
            //foreach (Model.MPSheader MPSheader in MPSheaderManager.Select())
            //{
            //    this.comboBoxEditMpsStart.Properties.Items.Add(MPSheader.Id);
            //    this.comboBoxEditMpsEnd.Properties.Items.Add(MPSheader.Id);
            //}
            this.newChooseProductCate.Choose = new Settings.BasicData.ProductCategories.ChooseProductCategories();
            this.comBox_OrderColumn.SelectedIndex = 0;
            this.comBox_OrderType.SelectedIndex = 0;
        }

        private ConditionMRS condition;

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionMRS();
            this.condition.StartDate = this.dateEditStartDate.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStartDate.DateTime.Date;
            this.condition.EndDate = this.dateEditEndDate.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.dateEditEndDate.DateTime.Date.AddDays(1).AddSeconds(-1);
            this.condition.CustomerStart = this.buttonEditCustomerStart.Text == "" ? null : this.buttonEditCustomerStart.Text;
            this.condition.CustomerEnd = this.buttonEditCustomerEnd.Text == "" ? null : this.buttonEditCustomerEnd.Text;
            this.condition.MrsStart = this.textEditMpsStart.Text == "" ? null : this.textEditMpsStart.Text;
            this.condition.MrsEnd = this.textEditMpsEnd.Text == "" ? null : this.textEditMpsEnd.Text;
            if (this.cboSourceType.SelectedIndex == 0 || this.cboSourceType.SelectedIndex == -1)
                this.condition.SourceType = -1;
            else if (this.cboSourceType.SelectedIndex <= 2)
                this.condition.SourceType = this.cboSourceType.SelectedIndex - 1;
            else
                this.condition.SourceType = this.cboSourceType.SelectedIndex;
            this.condition.Product = this.buttonEditPro.EditValue as Model.Product;
            this.condition.Id1 = this.textEditid1.Text;
            this.condition.Id2 = this.textEditid2.Text;
            this.condition.Cusxoid = this.textEditCusxoid.Text;

            this.condition.OrderColumn = this.comBox_OrderColumn.SelectedIndex == -1 ? 0 : this.comBox_OrderColumn.SelectedIndex;
            this.condition.OrderType = this.comBox_OrderType.SelectedIndex == -1 ? 0 : this.comBox_OrderType.SelectedIndex;
            this.condition.ProductCategory = this.newChooseProductCate.EditValue as Model.ProductCategory;
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionMRS;
            }
        }

        private void buttonEditCustomerStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseCustoms f = new Book.UI.Invoices.ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Customer customer = f.SelectedItem as Model.Customer;
                if (customer != null)
                {
                    this.buttonEditCustomerStart.Text = customer.Id;
                    this.buttonEditCustomerEnd.Text = customer.Id;
                }
            }
        }

        private void buttonEditCustomerEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseCustoms f = new Book.UI.Invoices.ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Customer customer = f.SelectedItem as Model.Customer;
                if (customer != null)
                {
                    this.buttonEditCustomerEnd.Text = customer.Id;

                }
            }
        }

        private void buttonEditPro_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
                this.labelCusPro.Text = (f.SelectedItem as Model.Product).CustomerProductName;
            }
            f.Dispose();
            GC.Collect();
        }
    }
}