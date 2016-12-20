using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.AccountPayable.AcOtherShouldCollection
{
    public partial class ChooseAcOtherShouldCollection : Book.UI.Settings.BasicData.BaseChooseForm
    {
        public IList<Model.AcOtherShouldCollection> listAcOtherShouldCollection = new List<Model.AcOtherShouldCollection>();

        public ChooseAcOtherShouldCollection()
        {
            InitializeComponent();
            this.manager = new BL.AcOtherShouldCollectionManager();
            this.dateEditStart.DateTime = DateTime.Now.AddMonths(-1);
            this.dateEditEnd.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        protected override void LoadData()
        {
            this.listAcOtherShouldCollection = (this.manager as BL.AcOtherShouldCollectionManager).SelectByDateRange(this.dateEditStart.DateTime, this.dateEditEnd.DateTime);
            this.bindingSource1.DataSource = this.listAcOtherShouldCollection;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        public override void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.listAcOtherShouldCollection = (from i in this.listAcOtherShouldCollection
                                                where i.Checked == true
                                                select i).ToList<Model.AcOtherShouldCollection>();
            this.DialogResult = DialogResult.OK;
        }
    }
}
