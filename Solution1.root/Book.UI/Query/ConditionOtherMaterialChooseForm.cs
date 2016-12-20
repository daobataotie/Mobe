using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.produceManager.ProduceOtherCompact;

namespace Book.UI.Query
{
    public partial class ConditionOtherMaterialChooseForm : ConditionAChooseForm
    {
        private ConditionOtherMaterial condition;
        public ConditionOtherMaterialChooseForm()
        {
            InitializeComponent();

            Book.UI.Settings.BasicData.Supplier.ChooseSupplier chooseSupplier = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseContorlSupplier1.Choose = chooseSupplier;
            this.newChooseContorlSupplier2.Choose = chooseSupplier;
            this.dateEditStartDate.DateTime = System.DateTime.Now.Date.AddMonths(-3);
            this.dateEditEndDate.DateTime = System.DateTime.Now.Date;
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionOtherMaterial;
            }
        }
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionOtherMaterial();
            this.condition.StartDate = this.dateEditStartDate.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEditStartDate.DateTime;
            this.condition.EndDate = this.dateEditEndDate.EditValue == null ? System.DateTime.Now : this.dateEditEndDate.DateTime;
            this.condition.SupplierId1 = this.newChooseContorlSupplier1.EditValue == null ? null : (this.newChooseContorlSupplier1.EditValue as Model.Supplier).SupplierId;
            this.condition.SupplierId2 = this.newChooseContorlSupplier2.EditValue == null ? null : (this.newChooseContorlSupplier2.EditValue as Model.Supplier).SupplierId;
            this.condition.ProduceOtherCompactId1 = this.buttonEditProduceOtherCompactId1.EditValue == null ? null : this.buttonEditProduceOtherCompactId1.Text;
            this.condition.ProduceOtherCompactId2 = this.buttonEditProduceOtherCompactId2.EditValue == null ? null : this.buttonEditProduceOtherCompactId2.Text;
            this.condition.ProductId1 = this.StartProductId.EditValue as Model.Product == null ? null : (this.StartProductId.EditValue as Model.Product).Id;
            this.condition.ProductId2 = this.EndProductId.EditValue as Model.Product == null ? null : (this.EndProductId.EditValue as Model.Product).Id;
        }

        private void buttonEditProduceOtherCompactId1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseOutContract form = new ChooseOutContract();
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.buttonEditProduceOtherCompactId1.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
                this.buttonEditProduceOtherCompactId2.EditValue = (form.SelectItem as Model.ProduceOtherCompact).ProduceOtherCompactId;
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseOutContract form = new ChooseOutContract();
            if (form.ShowDialog() == DialogResult.OK)
                this.buttonEditProduceOtherCompactId2.EditValue = (form.SelectItem as Model.ProduceOtherCompactDetail).ProduceOtherCompactId;
        }

        private void StartProductId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.StartProductId.EditValue = form.SelectedItem as Model.Product;
                this.EndProductId.EditValue = form.SelectedItem as Model.Product;
            }
        }

        private void EndProductId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm form = new Invoices.ChooseProductForm();
            if (form.ShowDialog(this) == DialogResult.OK)
                this.EndProductId.EditValue = form.SelectedItem as Model.Product;
        }
    }
}