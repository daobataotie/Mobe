using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

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

            this.ncc_Workhouse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();

            IList<string> handBookIds = new BL.BGHandbookManager().SelectAllId();
            foreach (var item in handBookIds)
            {
                this.cob_HandBook.Properties.Items.Add(item);
            }
        }

        private void ConditionForList_Load(object sender, EventArgs e)
        {
            this.StartdateEdit.DateTime = DateTime.Now.AddDays(-15).Date;
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


            this.condition.StartPMEid = this.btn_StartPMEId.Text;
            this.condition.EndPMEid = this.btn_EndPMEId.Text;
            this.condition.StartPronoteHeaderId = this.btn_StartPNTId.Text;
            this.condition.EndPronoteHeaderId = this.btn_EndPNTId.Text;

            this.condition.StartProduct = this.btnEditStartProduct.EditValue as Model.Product;
            this.condition.EndProduct = this.btnEditEndProduct.EditValue as Model.Product;

            this.condition.WorkhouseId = this.ncc_Workhouse.EditValue == null ? null : (this.ncc_Workhouse.EditValue as Model.WorkHouse).WorkHouseId;
            this.condition.InvocieXOCusId = this.txt_InvoiceXOCusId.Text;
            this.condition.HandBookId = this.cob_HandBook.Text;
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

        private void btn_StartPMEId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProduceMaterialExit form = new ChooseProduceMaterialExit();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (form.detailList != null)
                {
                    Model.ProduceMaterialExitDetail detail = form.detailList.FirstOrDefault(D => D.IsChecked == true);
                    if (detail != null)
                    {
                        this.btn_EndPMEId.EditValue = this.btn_StartPMEId.EditValue = detail.ProduceMaterialExitId;
                    }
                }
            }
        }

        private void btn_EndPMEId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProduceMaterialExit form = new ChooseProduceMaterialExit();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (form.detailList != null)
                {
                    Model.ProduceMaterialExitDetail detail = form.detailList.FirstOrDefault(D => D.IsChecked == true);
                    if (detail != null)
                    {
                        this.btn_EndPMEId.EditValue = detail.ProduceMaterialExitId;
                    }
                }
            }
        }

        private void btn_StartPNTId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm form = new produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.btn_EndPNTId.EditValue = this.btn_StartPNTId.EditValue = (form.SelectItem == null ? null : (form.SelectItem).PronoteHeaderID);
            }
            GC.Collect();
            form.Dispose();
        }

        private void btn_EndPNTId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm form = new produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                this.btn_EndPNTId.EditValue = (form.SelectItem == null ? null : (form.SelectItem).PronoteHeaderID);
            }
            GC.Collect();
            form.Dispose();
        }
    }
}