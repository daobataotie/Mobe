using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.produceManager.ProduceAbility
{

    public partial class ProduceScheduleForm : DevExpress.XtraEditors.XtraForm
    {
        Model.MRSHeader _mrsHeader ;
        public ProduceScheduleForm()
        {
            InitializeComponent();
        }
        public ProduceScheduleForm( Model.MRSHeader mrsHeader ):this()
        {
            _mrsHeader = mrsHeader;           
        }

        private void ProduceScheduleForm_Load(object sender, EventArgs e)
        {
            this.dateEditStart.DateTime = this._mrsHeader.MRSstartdate.Value;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //PronoteHeader.PlanForm f = new Book.UI.produceManager.PronoteHeader.PlanForm(this._mrsHeader);
            //f.Show();
        }

    }
}           