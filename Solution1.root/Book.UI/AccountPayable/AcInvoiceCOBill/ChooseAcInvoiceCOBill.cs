using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.AccountPayable.AcInvoiceCOBill
{
    public partial class ChooseAcInvoiceCOBill : Book.UI.Settings.BasicData.BaseChooseForm
    {
        public IList<Model.AcInvoiceCOBill> listAcinvoiceCOBill = new List<Model.AcInvoiceCOBill>();

        public ChooseAcInvoiceCOBill()
        {
            InitializeComponent();
            this.manager = new BL.AcInvoiceCOBillManager();
            this.dateEditStart.DateTime = DateTime.Now.AddMonths(-1);
            this.dateEditEnd.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        protected override void LoadData()
        {
            this.listAcinvoiceCOBill = (this.manager as BL.AcInvoiceCOBillManager).SelectByDateRange(this.dateEditStart.DateTime, this.dateEditEnd.DateTime);
            this.bindingSource1.DataSource = this.listAcinvoiceCOBill;
        }

        public override void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.listAcinvoiceCOBill = (from i in this.listAcinvoiceCOBill
                                        where i.Checked == true
                                        select i).ToList<Model.AcInvoiceCOBill>();
            this.DialogResult = DialogResult.OK;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
    }
}
