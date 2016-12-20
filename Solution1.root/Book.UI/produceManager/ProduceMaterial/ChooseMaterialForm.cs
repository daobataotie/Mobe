using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceMaterial
{
    public partial class ChooseMaterialForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.ProduceMaterialManager produceMaterialManager = new BL.ProduceMaterialManager();
        public ChooseMaterialForm()
        {
            InitializeComponent();
        }

        private void ChooseMaterialForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = this.produceMaterialManager.SelectByDateRage(DateTime.Now.AddMonths(-1), global::Helper.DateTimeParse.EndDate, null, false, null);
        }

        private void simpleButtonNodo_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            GC.Collect();
        }
        public Model.ProduceMaterial SelectItem
        {
            get { return this.bindingSource1.Current as Model.ProduceMaterial; }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

        }

        private void simpleButtonSearch_Click(object sender, EventArgs e)
        {
            DateTime startTime = global::Helper.DateTimeParse.NullDate;
            DateTime endTime = global::Helper.DateTimeParse.EndDate;
            if (this.dateEditStart.EditValue != null)
            {
                startTime = this.dateEditStart.DateTime;
            }
            if (this.dateEditEnd.EditValue != null)
            {
                endTime = this.dateEditEnd.DateTime.Date.AddDays(1).AddSeconds(-1);
            }
            this.bindingSource1.DataSource = this.produceMaterialManager.SelectByDateRage(startTime, endTime, null, false, null); ;
        }
    }
}