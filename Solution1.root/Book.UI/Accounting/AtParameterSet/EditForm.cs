using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtParameterSet
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.AtParameterSet _atParameterSet = new Book.Model.AtParameterSet();
        BL.AtParameterSetManager _atParameterManager = new Book.BL.AtParameterSetManager();

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.AtParameterSet.PRO_AtParameterSetId, new AA(Properties.Resources.NumsIsNotNull, this.spinKuaiJiNianDu));
            this.requireValueExceptions.Add(Model.AtParameterSet.PRO_ACMoneySubjectId, new AA("現金科目不能為空", this.nccACMoneySubject));
            this.requireValueExceptions.Add(Model.AtParameterSet.PRO_AtOldSunYiSubjectId, new AA("前期損益科目不能為空", this.nccAtOldSunYiSubject));
            this.requireValueExceptions.Add(Model.AtParameterSet.PRO_AtSunYiSubjectId, new AA("本期損益科目不能為空", this.nccAtSunYiSubject));
            this.requireValueExceptions.Add(Model.AtParameterSet.PRO_AtBeginDate, new AA("會計起始日期不能為空", this.DateEditAtBeginDate));
            this.requireValueExceptions.Add(Model.AtParameterSet.PRO_AtEndDate, new AA("會計關張日期不能為空", this.DateEditAtEndDate));
            this.requireValueExceptions.Add(Model.AtParameterSet.PRO_AtCurrentlyYear, new AA("目前會計年度不能為空", this.spinKuaiJiNianDu));


            this.invalidValueExceptions.Add(Model.AtParameterSet.PRO_AtCurrentlyYear + "_error", new AA("會計年度輸入有誤", this.spinKuaiJiNianDu));
            this.invalidValueExceptions.Add(Model.AtParameterSet.PRO_AtCurrentlyYear + "_Exists", new AA("會計年度重複", this.spinKuaiJiNianDu));
            this.nccACMoneySubject.Choose = new Accounting.AtAccountSubject.ChooseAccountSubject();
            this.nccAtOldSunYiSubject.Choose = new Accounting.AtAccountSubject.ChooseAccountSubject();
            this.nccAtSunYiSubject.Choose = new Accounting.AtAccountSubject.ChooseAccountSubject();

            this.action = "view";
        }

        protected override void AddNew()
        {
            this._atParameterSet = new Book.Model.AtParameterSet();
            this._atParameterSet.AtParameterSetId = this._atParameterManager.GetId();
        }

        public override void Refresh()
        {
            if (this._atParameterSet == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._atParameterSet = this._atParameterManager.Get(this._atParameterSet.AtParameterSetId);
                }
            }

            this.spinKuaiJiNianDu.EditValue = this._atParameterSet.AtCurrentlyYear.HasValue ? this._atParameterSet.AtCurrentlyYear.Value : DateTime.Now.Year;
            this.DateEditAtBeginDate.EditValue = this._atParameterSet.AtBeginDate;
            this.DateEditAtEndDate.EditValue = this._atParameterSet.AtEndDate;

            this.nccACMoneySubject.EditValue = this._atParameterSet.ACMoneySubject;
            this.nccAtOldSunYiSubject.EditValue = this._atParameterSet.AtOldSunYiSubject;
            this.nccAtSunYiSubject.EditValue = this._atParameterSet.AtSunYiSubject;

            this.chkIsThisYear.Checked = this._atParameterSet.IsThisYear.HasValue ? this._atParameterSet.IsThisYear.Value : false;

            base.Refresh();
        }

        protected override void MoveNext()
        {
            Model.AtParameterSet atPara = this._atParameterManager.GetNext(this._atParameterSet);
            if (atPara == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._atParameterSet = this._atParameterManager.Get(atPara.AtParameterSetId);
        }

        protected override void MovePrev()
        {
            Model.AtParameterSet atPara = this._atParameterManager.GetPrev(this._atParameterSet);
            if (atPara == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._atParameterSet = this._atParameterManager.Get(atPara.AtParameterSetId);
        }

        protected override void MoveFirst()
        {
            this._atParameterSet = this._atParameterManager.Get(this._atParameterManager.GetFirst() == null ? "" : this._atParameterManager.GetFirst().AtParameterSetId);
        }

        protected override void MoveLast()
        {
            this._atParameterSet = this._atParameterManager.Get(this._atParameterManager.GetLast() == null ? "" : this._atParameterManager.GetLast().AtParameterSetId);
        }

        protected override bool HasRows()
        {
            return this._atParameterManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._atParameterManager.HasRowsAfter(this._atParameterSet);
        }

        protected override bool HasRowsPrev()
        {
            return this._atParameterManager.HasRowsBefore(this._atParameterSet);
        }

        protected override void Delete()
        {
            if (this._atParameterSet == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._atParameterManager.Delete(this._atParameterSet.AtParameterSetId);
            this._atParameterSet = this._atParameterManager.GetNext(this._atParameterSet);
            if (this._atParameterSet == null)
            {
                this._atParameterSet = this._atParameterManager.GetLast();
            }
        }

        protected override void Save()
        {
            int mYear;
            if (int.TryParse(this.spinKuaiJiNianDu.EditValue.ToString(), out  mYear))
            {
                this._atParameterSet.AtCurrentlyYear = mYear;
            }
            else
            {
                throw new Helper.InvalidValueException(Model.AtParameterSet.PRO_AtCurrentlyYear + "_error");
            }

            this._atParameterSet.AtBeginDate = this.DateEditAtBeginDate.DateTime;
            this._atParameterSet.AtEndDate = this.DateEditAtEndDate.DateTime;
            if (this.nccACMoneySubject.EditValue != null)
            {
                this._atParameterSet.ACMoneySubjectId = (this.nccACMoneySubject.EditValue as Model.AtAccountSubject).SubjectId;
            }
            if (this.nccAtOldSunYiSubject.EditValue != null)
            {
                this._atParameterSet.AtOldSunYiSubjectId = (this.nccAtOldSunYiSubject.EditValue as Model.AtAccountSubject).SubjectId;
            }
            if (this.nccAtSunYiSubject.EditValue != null)
            {
                this._atParameterSet.AtSunYiSubjectId = (this.nccAtSunYiSubject.EditValue as Model.AtAccountSubject).SubjectId;
            }

            this._atParameterSet.IsThisYear = this.chkIsThisYear.Checked;

            switch (this.action)
            {
                case "insert":
                    this._atParameterManager.Insert(this._atParameterSet);
                    break;
                case "update":
                    this._atParameterManager.Update(this._atParameterSet);
                    break;
            }
        }
    }
}