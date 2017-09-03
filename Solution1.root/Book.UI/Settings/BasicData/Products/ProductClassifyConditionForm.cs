using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Products
{
    public partial class ProductClassifyConditionForm : DevExpress.XtraEditors.XtraForm
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string KeyWord { get; set; }

        public ProductClassifyConditionForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.dateEdit1.EditValue = DateTime.Now.AddMonths(-1);
            this.dateEdit2.EditValue = DateTime.Now;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.StartDate = (this.dateEdit1.EditValue == null ? global::Helper.DateTimeParse.NullDate : this.dateEdit1.DateTime);
            this.EndDate = (this.dateEdit2.EditValue == null ? global::Helper.DateTimeParse.EndDate : this.dateEdit2.DateTime);
            this.KeyWord = this.textEdit1.Text;

            this.DialogResult = DialogResult.OK;
        }
    }
}