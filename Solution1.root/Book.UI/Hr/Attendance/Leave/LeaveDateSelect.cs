using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Hr.Attendance.Leave
{
    public partial class LeaveDateSelect : DevExpress.XtraEditors.XtraForm
    {
        private BL.LeaveManager _leaveManager = new Book.BL.LeaveManager();

        public LeaveDateSelect()
        {
            InitializeComponent();
        }

        private void LeaveDateSelect_Load(object sender, EventArgs e)
        {
            int mYear = DateTime.Now.Year;
            for (int i = mYear; i >= mYear - 4; i--)
            {
                this.comboBox_Year.Properties.Items.Add(i.ToString());
            }

            this.comboBox_Year.SelectedIndex = 0;
            this.comboBox_Month.SelectedIndex = 0;
        }

        private void butYes_Click(object sender, EventArgs e)
        {
            string mYear = this.comboBox_Year.SelectedItem.ToString();
            string mMonth = this.comboBox_Month.SelectedIndex == 0 ? "" : this.comboBox_Month.SelectedItem.ToString();
            IList<Model.Leave> mLeaveList = new List<Model.Leave>();
            IList<LeaveListModel> _mllModelList = new List<LeaveListModel>();

            if (string.IsNullOrEmpty(mMonth))
            {
                DateTime sd = DateTime.Parse(mYear + "/01/01").Date;
                DateTime ed = DateTime.Parse(mYear + "/12/31").Date;
                mLeaveList = this._leaveManager.SelectForMonthListPrint(sd, ed);
            }
            else
            {
                DateTime sd = DateTime.Parse(mYear + "/" + mMonth + "/01").Date;
                DateTime ed = sd.AddMonths(1).AddDays(-1).Date;
                mLeaveList = this._leaveManager.SelectForMonthListPrint(sd, ed);
            }

            var query = from Model.Leave l in mLeaveList
                        group l by l.EmployeeId;
            foreach (IGrouping<string, Model.Leave> item in query)
            {
                LeaveListModel llm = new LeaveListModel();

                StringBuilder sbDateList = new StringBuilder();
                StringBuilder sbLeaveQuantity = new StringBuilder();

                foreach (Model.Leave le in item)
                {
                    sbDateList.Append(le.LeaveDate.Value.Date.ToShortDateString() + "-" + le.LeaveType.LeaveTypeName + "-" + le.GetLeaveRangeName + ",");
                }

                var que = from Model.Leave ll in item
                          group ll by ll.LeaveTypeId;
                double aa = 0;
                foreach (IGrouping<string, Model.Leave> it in que)
                {
                    foreach (Model.Leave l_l in it)
                    {
                        aa += l_l.LeaveRange.Value == 0 ? 1 : 0.5;
                    }
                    sbLeaveQuantity.Append(it.First<Model.Leave>().LeaveType.LeaveTypeName + ":" + aa.ToString());
                    aa = 0;
                }
                llm.EmpId = item.First<Model.Leave>().Employee.IDNo;
                llm.EmpName = item.First<Model.Leave>().Employee.EmployeeName;
                llm.LeaveDateList = sbDateList.ToString();
                llm.LeaveQuantity = sbLeaveQuantity.ToString();
                _mllModelList.Add(llm);
            }

            if (_mllModelList == null || _mllModelList.Count == 0)
            {
                MessageBox.Show("該時間區段內沒有任何員工休假記錄");
                return;
            }

            string strprintdate = string.IsNullOrEmpty(mMonth) ? mYear + "年" : mYear + "年" + mMonth + "月";
            ROLeaveList rof = new ROLeaveList(_mllModelList, strprintdate);
            rof.ShowPreviewDialog();
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}