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
    public partial class ConditionPacking : DevExpress.XtraEditors.XtraForm
    {
        public ConditionPacking()
        {
            InitializeComponent();

            this.date_Start.EditValue = DateTime.Now.AddDays(-7);
            this.date_End.EditValue = DateTime.Now;
            this.bindingSourceCompany.DataSource = (new BL.CompanyManager()).Select();
            this.StartPosition = FormStartPosition.CenterParent;
        }
        private ConditionPK condition;

        public ConditionPK Condition
        {
            get { return condition; }
            set { condition = value; }
        }


        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.condition == null)
                this.condition = new ConditionPK();
            if (this.date_Start.EditValue == null)
            {
                MessageBox.Show("起始日期不能為空", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                condition.StartDate = this.date_Start.DateTime;
            if (this.date_End.EditValue == null)
            {
                MessageBox.Show("結束日期不能為空", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                condition.EndDate = this.date_End.DateTime;
            condition.NO = this.txt_NO.Text;
            condition.InvoiceOf = this.txt_InvoiceOf.Text;
            condition.ShippedById = this.lookUpEditSHIPPEDBY.EditValue == null ? null : this.lookUpEditSHIPPEDBY.EditValue.ToString();
            condition.ConsigneeId = this.newChooseContorlConsignee.EditValue == null ? null : (this.newChooseContorlConsignee.EditValue as Model.Customer).CustomerId;
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}