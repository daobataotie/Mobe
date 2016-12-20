using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.BasicData;

namespace Book.UI.Hr.Attendance.TempCard
{
    public partial class SelectTempCardForm : DevExpress.XtraEditors.XtraForm
    {
        BL.TempCardManager tempCardManager = new Book.BL.TempCardManager();
        public SelectTempCardForm()
        {
            InitializeComponent();
            this.newChooseContorl1.Choose = new ChooseEmployee();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string cardNo = this.textEdit1.EditValue==null?null:this.textEdit1.Text;
            string employeeId = null;
            Book.Model.Employee emp = this.newChooseContorl1.EditValue as Book.Model.Employee;
            if (emp != null)
            {
                employeeId = emp.EmployeeId;
            }
            DateTime StartDate = global::Helper.DateTimeParse.NullDate;
            DateTime EndDate = global::Helper.DateTimeParse.EndDate;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit1.DateTime, new DateTime()))
            {
                StartDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                StartDate = this.dateEdit1.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit2.DateTime, new DateTime()))
            {
                 EndDate = global::Helper.DateTimeParse.EndDate;
            }
            else
            {
                 EndDate = this.dateEdit2.DateTime;
            }
            UI.Hr.Attendance.TempCard.TempCardEdit._tempCardList = tempCardManager.SelectByCardType(cardNo,employeeId,StartDate,EndDate);
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void SelectTempCardForm_Load(object sender, EventArgs e)
        {

        }
    }
}