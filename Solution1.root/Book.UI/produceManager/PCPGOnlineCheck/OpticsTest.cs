using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class OpticsTest : Settings.BasicData.BaseEditForm
    {
        Model.OpticsTest _OpticsTest;
        BL.OpticsTestManager _OpticsTestManager = new Book.BL.OpticsTestManager();
        private string _PCPGOnlineCheckDetailId = string.Empty;
        private string _PCFinishCheckId = string.Empty;
        int _FromPcFinishCheck = 0;

        public OpticsTest()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.OpticsTest.PRO_OpticsTestId, new AA(Properties.Resources.NumsIsNotNull, this.txtOpticsTestId));
            this.requireValueExceptions.Add(Model.OpticsTest.PRO_OptiscTestDate, new AA(Properties.Resources.DateIsNull, this.dateEditOpticsDate));
            this.requireValueExceptions.Add(Model.OpticsTest.PRO_MachineName, new AA("機型不能為空", this.comboxMachine));
            this.requireValueExceptions.Add(Model.OpticsTest.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.nccEmployee));
            this.requireValueExceptions.Add(Model.OpticsTest.PRO_ManualId, new AA("手動編號不能為空!", this.txtManualid));
            this.invalidValueExceptions.Add(Model.OpticsTest.PRO_ManualId, new AA("手動編號重複,請重新輸入!", this.txtManualid));

            this.invalidValueExceptions.Add(Model.OpticsTest.PRO_PCPGOnlineCheckDetailId, new AA("必須先保存本測試所依存的頭表", this.txtManualid));


            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();

            foreach (Model.Setting SET in new BL.SettingManager().SelectByName("JISgxzcJiXing"))
            {
                this.comboxMachine.Properties.Items.Add(SET.SettingCurrentValue);
            }

            foreach (Model.Setting SET in new BL.SettingManager().SelectByName("JISgxzcTiaoJian"))
            {
                this.comboBoxCondition.Properties.Items.Add(SET.SettingCurrentValue);
            }

            this.action = "view";
        }

        public OpticsTest(string PCPGOnlineCheckDetailId)
            : this()
        {
            this._PCPGOnlineCheckDetailId = PCPGOnlineCheckDetailId;
        }
        public OpticsTest(string PCFinishCheckId, int i)
            : this()
        {
            this._PCFinishCheckId = PCFinishCheckId;
            this._FromPcFinishCheck = i;
        }

        protected override void AddNew()
        {
            this._OpticsTest = new Book.Model.OpticsTest();
            this._OpticsTest.OpticsTestId = this._OpticsTestManager.GetId();
            this._OpticsTest.PCPGOnlineCheckDetailId = null;
            this._OpticsTest.PCFinishCheckId = null;
            this._OpticsTest.OptiscTestDate = DateTime.Now;
            this._OpticsTest.LattrA = 180;
            this._OpticsTest.RattrA = 180;
            this._OpticsTest.LattrC = 0;
            this._OpticsTest.RattrC = 0;

            this._OpticsTest.Employee = BL.V.ActiveOperator.Employee;
            this._OpticsTest.EmployeeId = BL.V.ActiveOperator.EmployeeId;
        }

        protected override void Delete()
        {
            if (this._OpticsTest == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            if (this._FromPcFinishCheck == 0)
            {
                this._OpticsTestManager.Delete(this._OpticsTest.OpticsTestId);
                this._OpticsTest = this._OpticsTestManager.mGetNext(this._OpticsTest.InsertTime.Value, this._PCPGOnlineCheckDetailId);
                if (this._OpticsTest == null)
                {
                    this._OpticsTest = this._OpticsTestManager.mGetLast(this._PCPGOnlineCheckDetailId);
                }
            }
            else
            {
                this._OpticsTestManager.Delete(this._OpticsTest.OpticsTestId);
                this._OpticsTest = this._OpticsTestManager.FGetNext(this._OpticsTest.InsertTime.Value, this._PCFinishCheckId);
                if (this._OpticsTest == null)
                {
                    this._OpticsTest = this._OpticsTestManager.FGetLast(this._PCFinishCheckId);
                }
            }

        }

        protected override void MoveLast()
        {
            if (this._FromPcFinishCheck == 0)
            {
                this._OpticsTest = this._OpticsTestManager.Get(this._OpticsTestManager.mGetLast(this._PCPGOnlineCheckDetailId) == null ? "" : this._OpticsTestManager.mGetLast(this._PCPGOnlineCheckDetailId).OpticsTestId);
            }
            else
            {
                this._OpticsTest = this._OpticsTestManager.Get(this._OpticsTestManager.FGetLast(this._PCFinishCheckId) == null ? "" : this._OpticsTestManager.FGetLast(this._PCFinishCheckId).OpticsTestId);
            }
        }

        protected override void MoveFirst()
        {
            if (this._FromPcFinishCheck == 0)
            {
                this._OpticsTest = this._OpticsTestManager.Get(this._OpticsTestManager.mGetFirst(this._PCPGOnlineCheckDetailId) == null ? "" : this._OpticsTestManager.mGetFirst(this._PCPGOnlineCheckDetailId).OpticsTestId);
            }
            else
            {
                this._OpticsTest = this._OpticsTestManager.Get(this._OpticsTestManager.FGetFirst(this._PCFinishCheckId) == null ? "" : this._OpticsTestManager.FGetFirst(this._PCFinishCheckId).OpticsTestId);
            }
        }

        protected override void MovePrev()
        {
            Model.OpticsTest mOpticsTest = null;
            if (this._FromPcFinishCheck == 0)
            {
                mOpticsTest = this._OpticsTestManager.mGetPrev(this._OpticsTest.InsertTime.Value, this._PCPGOnlineCheckDetailId);
            }
            else
            {
                mOpticsTest = this._OpticsTestManager.FGetPrev(this._OpticsTest.InsertTime.Value, this._PCFinishCheckId);
            }

            if (mOpticsTest == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._OpticsTest = this._OpticsTestManager.Get(mOpticsTest.OpticsTestId);
        }

        protected override void MoveNext()
        {
            Model.OpticsTest mOpticsTest = null;
            if (this._FromPcFinishCheck == 0)
            {
                mOpticsTest = this._OpticsTestManager.mGetNext(this._OpticsTest.InsertTime.Value, this._PCPGOnlineCheckDetailId);
            }
            else
            {
                mOpticsTest = this._OpticsTestManager.FGetNext(this._OpticsTest.InsertTime.Value, this._PCFinishCheckId);
            }
            if (mOpticsTest == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._OpticsTest = this._OpticsTestManager.Get(mOpticsTest.OpticsTestId);
        }

        protected override bool HasRows()
        {
            if (this._FromPcFinishCheck == 0)
            {
                return this._OpticsTestManager.mHasRows(this._PCPGOnlineCheckDetailId);
            }
            else
            {
                return this._OpticsTestManager.FHasRows(this._PCFinishCheckId);
            }
        }

        protected override bool HasRowsNext()
        {
            if (this._FromPcFinishCheck == 0)
            {
                return this._OpticsTestManager.mHasRowsAfter(this._OpticsTest, this._PCPGOnlineCheckDetailId);
            }
            else
            {
                return this._OpticsTestManager.FHasRowsAfter(this._OpticsTest, this._PCFinishCheckId);
            }
        }

        protected override bool HasRowsPrev()
        {
            if (this._FromPcFinishCheck == 0)
            {
                return this._OpticsTestManager.mHasRowsBefore(this._OpticsTest, this._PCPGOnlineCheckDetailId);
            }
            else
            {
                return this._OpticsTestManager.FHasRowsBefore(this._OpticsTest, this._PCFinishCheckId);
            }
        }

        protected override void Save()
        {
            this._OpticsTest.OpticsTestId = this.txtOpticsTestId.Text;
            this._OpticsTest.ManualId = this.txtManualid.Text;
            this._OpticsTest.OptiscTestDate = this.dateEditOpticsDate.DateTime;
            this._OpticsTest.MachineName = this.comboxMachine.SelectedItem == null ? "" : this.comboxMachine.SelectedItem.ToString();
            this._OpticsTest.Condition = this.comboBoxCondition.SelectedItem == null ? "" : this.comboBoxCondition.SelectedItem.ToString();
            this._OpticsTest.Employee = (this.nccEmployee.EditValue as Model.Employee);
            if (this._OpticsTest.Employee != null)
            {
                this._OpticsTest.EmployeeId = this._OpticsTest.Employee.EmployeeId;
            }

            if (this._FromPcFinishCheck == 0)
            {
                this._OpticsTest.PCPGOnlineCheckDetailId = this._PCPGOnlineCheckDetailId;
            }
            else
            {
                this._OpticsTest.PCFinishCheckId = this._PCFinishCheckId;
            }

            try
            {
                this._OpticsTest.LattrS = double.Parse(string.IsNullOrEmpty(this.NewKTxtLs.mTextValue) ? "0" : this.NewKTxtLs.mTextValue);
                this._OpticsTest.LattrC = double.Parse(string.IsNullOrEmpty(this.NewKTxtLc.mTextValue) ? "0" : this.NewKTxtLc.mTextValue);
                this._OpticsTest.LattrA = double.Parse(string.IsNullOrEmpty(this.NewKTxtLa.mTextValue) ? "0" : this.NewKTxtLa.mTextValue);
                this._OpticsTest.LinPSM = double.Parse(string.IsNullOrEmpty(this.NewKTxtLin.mTextValue) ? "0" : this.NewKTxtLin.mTextValue);
                //this._OpticsTest.LoutPSM = double.Parse(string.IsNullOrEmpty(this.NewKTxtLout.mTextValue) ? "0" : this.NewKTxtLout.mTextValue);
                this._OpticsTest.LupPSM = double.Parse(string.IsNullOrEmpty(this.NewKTxtLup.mTextValue) ? "0" : this.NewKTxtLup.mTextValue);
                //this._OpticsTest.LdownPSM = double.Parse(string.IsNullOrEmpty(this.NewKTxtLdown.mTextValue) ? "0" : this.NewKTxtLdown.mTextValue);
                this._OpticsTest.RattrS = double.Parse(string.IsNullOrEmpty(this.NewKTxtRs.mTextValue) ? "0" : this.NewKTxtRs.mTextValue);
                this._OpticsTest.RattrC = double.Parse(string.IsNullOrEmpty(this.NewKTxtRc.mTextValue) ? "0" : this.NewKTxtRc.mTextValue);
                this._OpticsTest.RattrA = double.Parse(string.IsNullOrEmpty(this.NewKTxtRa.mTextValue) ? "0" : this.NewKTxtRa.mTextValue);
                this._OpticsTest.RinPSM = double.Parse(string.IsNullOrEmpty(this.NewKTxtRin.mTextValue) ? "0" : this.NewKTxtRin.mTextValue);
                //this._OpticsTest.RoutPSM = double.Parse(string.IsNullOrEmpty(this.NewKTxtRout.mTextValue) ? "0" : this.NewKTxtRout.mTextValue);
                this._OpticsTest.RupPSM = double.Parse(string.IsNullOrEmpty(this.NewKTxtRup.mTextValue) ? "0" : this.NewKTxtRup.mTextValue);
                //this._OpticsTest.RdowmPSM = double.Parse(string.IsNullOrEmpty(this.NewKTxtRdown.mTextValue) ? "0" : this.NewKTxtRdown.mTextValue);

                this._OpticsTest.LeftLevelJudge = this.cobLLevelJudge.Text;
                this._OpticsTest.LeftVerticalJudge = this.cobLVerticalJudge.Text;
                this._OpticsTest.RightLevelJudge = this.cobRLevelJudge.Text;
                this._OpticsTest.RightVerticalJudge = this.cobRVerticalJudge.Text;
            }
            catch (Exception)
            {
                throw new Helper.MessageValueException("輸入數據中有誤,請查證");
            }

            switch (this.action)
            {
                case "insert":
                    this._OpticsTestManager.Insert(this._OpticsTest);
                    break;
                case "update":
                    this._OpticsTestManager.Update(this._OpticsTest);
                    break;
            }

            this.txtOpticsTestId.Enabled = false;
        }

        public override void Refresh()
        {
            if (this._OpticsTest == null)
            {
                this.AddNew();
                this.action = "insert";
            }

            this.txtOpticsTestId.Text = this._OpticsTest.OpticsTestId;
            this.txtManualid.Text = this._OpticsTest.ManualId;
            this.dateEditOpticsDate.EditValue = this._OpticsTest.OptiscTestDate.Value;
            this.comboxMachine.Text = this._OpticsTest.MachineName;
            this.comboBoxCondition.Text = this._OpticsTest.Condition;
            this.nccEmployee.EditValue = this._OpticsTest.Employee;

            this.NewKTxtLs.mTextValue = string.Format("{0:0.00}", this._OpticsTest.LattrS.HasValue ? this._OpticsTest.LattrS.Value : 0);
            this.NewKTxtLc.mTextValue = string.Format("{0:0.00}", this._OpticsTest.LattrC.HasValue ? this._OpticsTest.LattrC.Value : 0);
            this.NewKTxtLa.mTextValue = string.Format("{0:0.00}", this._OpticsTest.LattrA.HasValue ? this._OpticsTest.LattrA.Value : 0);
            this.NewKTxtLin.mTextValue = string.Format("{0:0.00}", this._OpticsTest.LinPSM.HasValue ? this._OpticsTest.LinPSM.Value : 0);
            //this.NewKTxtLout.mTextValue = string.Format("{0:0.00}", this._OpticsTest.LoutPSM.HasValue ? this._OpticsTest.LoutPSM.Value : 0);
            this.NewKTxtLup.mTextValue = string.Format("{0:0.00}", this._OpticsTest.LupPSM.HasValue ? this._OpticsTest.LupPSM.Value : 0);
            //this.NewKTxtLdown.mTextValue = string.Format("{0:0.00}", this._OpticsTest.LdownPSM.HasValue ? this._OpticsTest.LdownPSM.Value : 0);
            this.NewKTxtRs.mTextValue = string.Format("{0:0.00}", this._OpticsTest.RattrS.HasValue ? this._OpticsTest.RattrS.Value : 0);
            this.NewKTxtRc.mTextValue = string.Format("{0:0.00}", this._OpticsTest.RattrC.HasValue ? this._OpticsTest.RattrC.Value : 0);
            this.NewKTxtRa.mTextValue = string.Format("{0:0.00}", this._OpticsTest.RattrA.HasValue ? this._OpticsTest.RattrA.Value : 0);
            this.NewKTxtRin.mTextValue = string.Format("{0:0.00}", this._OpticsTest.RinPSM.HasValue ? this._OpticsTest.RinPSM.Value : 0);
            //this.NewKTxtRout.mTextValue = string.Format("{0:0.00}", this._OpticsTest.RoutPSM.HasValue ? this._OpticsTest.RoutPSM.Value : 0);
            this.NewKTxtRup.mTextValue = string.Format("{0:0.00}", this._OpticsTest.RupPSM.HasValue ? this._OpticsTest.RupPSM.Value : 0);
            //this.NewKTxtRdown.mTextValue = string.Format("{0:0.00}", this._OpticsTest.RdowmPSM.HasValue ? this._OpticsTest.RdowmPSM.Value : 0);
            //this.cobLLevelJudge.EditValue = string.IsNullOrEmpty(this._OpticsTest.LeftLevelJudge) ? this.cobLLevelJudge.Text : this._OpticsTest.LeftLevelJudge;
            //this.cobLVerticalJudge.EditValue = string.IsNullOrEmpty(this._OpticsTest.LeftVerticalJudge) ? this.cobLVerticalJudge.Text : this._OpticsTest.LeftVerticalJudge;
            //this.cobRLevelJudge.EditValue = string.IsNullOrEmpty(this._OpticsTest.RightLevelJudge) ? this.cobRLevelJudge.Text : this._OpticsTest.RightLevelJudge;
            //this.cobRVerticalJudge.EditValue = string.IsNullOrEmpty(this._OpticsTest.RightVerticalJudge) ? this.cobRVerticalJudge.Text : this._OpticsTest.RightVerticalJudge;

            this.cobLLevelJudge.EditValue = this._OpticsTest.LeftLevelJudge;
            this.cobLVerticalJudge.EditValue = this._OpticsTest.LeftVerticalJudge;
            this.cobRLevelJudge.EditValue = this._OpticsTest.RightLevelJudge;
            this.cobRVerticalJudge.EditValue = this._OpticsTest.RightVerticalJudge;

            base.Refresh();

            switch (this.action)
            {
                case "insert":
                    this.barBtn_Search.Enabled = false;
                    break;
                case "update":
                    this.barBtn_Search.Enabled = false;
                    break;
                case "view":
                    this.barBtn_Search.Enabled = true;
                    break;
            }

            this.txtOpticsTestId.Enabled = false;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new ROOpticsTest(_OpticsTest);
        }

        private void barBtn_Search_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpticsTestList f = null;
            if (this._FromPcFinishCheck == 0)
            {
                f = new OpticsTestList(this._PCPGOnlineCheckDetailId);
            }
            else
            {
                f = new OpticsTestList(this._PCFinishCheckId, this._FromPcFinishCheck);
            }
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.OpticsTest currentModel = f.SelectItem as Model.OpticsTest;
                if (currentModel != null)
                {
                    this._OpticsTest = currentModel;
                    this.Refresh();
                }
            }

            f.Dispose();
            GC.Collect();
        }

        private void mTextEditValiate(object sender, EventArgs e)
        {
            try
            {

                int.Parse((sender as DevExpress.XtraEditors.TextEdit).Text);
                (sender as DevExpress.XtraEditors.TextEdit).BackColor = Color.White;
            }
            catch
            {
                (sender as DevExpress.XtraEditors.TextEdit).BackColor = Color.PaleVioletRed;
            }
        }
    }
}