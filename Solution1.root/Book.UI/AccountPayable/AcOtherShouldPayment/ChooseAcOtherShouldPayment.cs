using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.AccountPayable.AcOtherShouldPayment
{
    public partial class ChooseAcOtherShouldPayment : Book.UI.Settings.BasicData.BaseChooseForm
    {
        public IList<Model.AcOtherShouldPayment> listAcOtherShouldPayment = new List<Model.AcOtherShouldPayment>();

        public ChooseAcOtherShouldPayment()
        {
            InitializeComponent();
            this.manager = new BL.AcOtherShouldPaymentManager();
            this.dateEditStart.DateTime = DateTime.Now.AddMonths(-1);
            this.dateEditEnd.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        protected override void LoadData()
        {
            this.listAcOtherShouldPayment = (this.manager as BL.AcOtherShouldPaymentManager).SelectByDateRange(this.dateEditStart.DateTime, this.dateEditEnd.DateTime);
            this.bindingSource1.DataSource = this.listAcOtherShouldPayment;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        public override void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.listAcOtherShouldPayment = (from i in this.listAcOtherShouldPayment
                                             where i.Checked == true
                                             select i).ToList<Model.AcOtherShouldPayment>();
            this.DialogResult = DialogResult.OK;
        }
    }
}
