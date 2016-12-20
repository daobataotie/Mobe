using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    public partial class ChooseAcInvoiceXOBill : Book.UI.Settings.BasicData.BaseChooseForm
    {
        public IList<Model.AcInvoiceXOBill> listAcinvoiceXOBill = new List<Model.AcInvoiceXOBill>();

        public ChooseAcInvoiceXOBill()
        {
            InitializeComponent();
            this.manager = new BL.AcInvoiceXOBillManager();
            this.dateEditStart.DateTime = DateTime.Now.AddMonths(-1);
            this.dateEditEnd.DateTime = DateTime.Now.AddDays(1).AddSeconds(-1);
        }

        protected override void LoadData()
        {
            this.listAcinvoiceXOBill = (this.manager as BL.AcInvoiceXOBillManager).SelectByDateRange(this.dateEditStart.DateTime, this.dateEditEnd.DateTime);
            this.bindingSource1.DataSource = this.listAcinvoiceXOBill;
        }

        public override void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.listAcinvoiceXOBill = (from i in this.listAcinvoiceXOBill
                                        where i.Checked == true
                                        select i).ToList<Model.AcInvoiceXOBill>();
            this.DialogResult = DialogResult.OK;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
    }
}
