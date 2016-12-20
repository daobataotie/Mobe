using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.CustomsClearance.PassNoteBook
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.PassNoteBook _passNoteBook;
        BL.PassNoteBookManager _manager = new Book.BL.PassNoteBookManager();
        BL.PassNoteBookDetailManager _detailManager = new Book.BL.PassNoteBookDetailManager();
        public EditForm()
        {
            InitializeComponent();
            this.bindingSourceAtCurrency.DataSource = new BL.AtCurrencyCategoryManager().Select();
            this.bindingSourceImportExportShore.DataSource = new BL.ImportExportShoreManager().Select();
            this.nccEmployeeId.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "view";
        }

        protected override bool HasRows()
        {
            return this._manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._manager.HasRowsAfter(this._passNoteBook);
        }

        protected override bool HasRowsPrev()
        {
            return this._manager.HasRowsBefore(this._passNoteBook);
        }

        protected override void MoveFirst()
        {
            this._passNoteBook = this._manager.GetFirst();
        }

        protected override void MoveLast()
        {
            this._passNoteBook = this._manager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.PassNoteBook model = this._manager.GetNext(this._passNoteBook);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._passNoteBook = model;
        }

        protected override void MovePrev()
        {
            Model.PassNoteBook model = this._manager.GetPrev(this._passNoteBook);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._passNoteBook = model;
        }

        protected override void AddNew()
        {
            this._passNoteBook = new Book.Model.PassNoteBook();
            this._passNoteBook.PassNoteBookId = Guid.NewGuid().ToString();
            this.action = "insert";
        }

        public override void Refresh()
        {
            if (this._passNoteBook == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._passNoteBook = this._manager.Get(this._passNoteBook.PassNoteBookId);
            }

            this.txt_CompanyInnerId.EditValue = this._passNoteBook.CompanyInnerId;
            this.txt_NoteBookId.EditValue = this._passNoteBook.NoteBookId;
            this.cobBackType.EditValue = this._passNoteBook.BackType;
            this.txt_JingYingDanWeiId.EditValue = this._passNoteBook.JingYingDanWeiId;
            this.txt_JingYingDanWeiName.EditValue = this._passNoteBook.JingYingDanWeiName;
            this.cobNoteBookType.EditValue = this._passNoteBook.NoteBookType;
            this.txt_JiaGongDanWeiId.EditValue = this._passNoteBook.JiaGongDanWeiId;
            this.txt_JiaGongDanWeiName.EditValue = this._passNoteBook.JiaGongDanWeiName;
            this.txt_ReceivingArea.EditValue = this._passNoteBook.ReceivingArea;
            this.txt_ReportingUnitId.EditValue = this._passNoteBook.ReportingUnitId;
            this.txt_ReportingUnitName.EditValue = this._passNoteBook.ReportingUnitName;
            this.date_ReportingDate.EditValue = this._passNoteBook.ReportingDate;
            this.txt_ZhengMianXingZhi.EditValue = this._passNoteBook.ZhengMianXingZhi;
            this.txt_AgreeNum.EditValue = this._passNoteBook.AgreeNum;
            this.date_EnteringDate.EditValue = this._passNoteBook.EnteringDate;
            this.txt_ImportContract.EditValue = this._passNoteBook.ImportContract;
            this.txt_ExportContract.EditValue = this._passNoteBook.ExportContract;
            this.txt_ZhuGuanHaiGuan.EditValue = this._passNoteBook.ZhuGuanHaiGuan;
            this.txt_BackImportTotalMoney.EditValue = this._passNoteBook.BackImportTotalMoney;
            this.txt_BackExportTotalMoney.EditValue = this._passNoteBook.BackExportTotalMoney;
            this.txt_ZhuGuanWaiJingMaoWei.EditValue = this._passNoteBook.ZhuGuanWaiJingMaoWei;
            this.lueImportAtCurrencyCategoryId.EditValue = this._passNoteBook.ImportAtCurrencyCategoryId;
            this.lueExportAtCurrencyCategoryId.EditValue = this._passNoteBook.ExportAtCurrencyCategoryId;
            this.cobXianZhiLeiMark.EditValue = this._passNoteBook.XianZhiLeiMark;
            this.txt_ProcessType.EditValue = this._passNoteBook.ProcessType;
            this.txt_BGProductId.EditValue = this._passNoteBook.BGProductId;
            this.cobDanSunReportingLink.EditValue = this._passNoteBook.DanSunReportingLink;
            this.date_EffectiveData.EditValue = this._passNoteBook.EffectiveData;
            this.txt_TradeType.EditValue = this._passNoteBook.TradeType;
            this.speImportProductNum.EditValue = this._passNoteBook.ImportProductNum;
            this.speImportMoney.EditValue = this._passNoteBook.ImportMoney;
            this.speExportProductNum.EditValue = this._passNoteBook.ExportProductNum;
            this.speExportMoney.EditValue = this._passNoteBook.ExportMoney;
            this.txt_ForeignCompany.EditValue = this._passNoteBook.ForeignCompany;
            this.nccEmployeeId.EditValue = this._passNoteBook.EmployeeId;
            this.lueImportExportShore.EditValue = this._passNoteBook.ImportExportShoreId;
            this.speNeiXiaoBiLi.EditValue = this._passNoteBook.NeiXiaoBiLi;
            this.cobManageObject.EditValue = this._passNoteBook.ManageObject;
            this.cobTaiZhangBank.EditValue = this._passNoteBook.TaiZhangBank;
            this.txt_Note.EditValue = this._passNoteBook.Note;

            this.newChooseContorlAuditEmp.EditValue = this._passNoteBook.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._passNoteBook.AuditState);

            base.Refresh();
            switch (this.action)
            {
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.gridView2.OptionsBehavior.Editable = false;
                    break;
                default:
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView2.OptionsBehavior.Editable = true;
                    break;
            }
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow() || !this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;
            this._passNoteBook.CompanyInnerId = this.txt_CompanyInnerId.Text;
            this._passNoteBook.NoteBookId = this.txt_NoteBookId.Text;
            this._passNoteBook.BackType = this.cobBackType.EditValue == null ? null : this.cobBackType.EditValue.ToString();
            this._passNoteBook.JingYingDanWeiId = this.txt_JingYingDanWeiId.Text;
            this._passNoteBook.JingYingDanWeiName = this.txt_JingYingDanWeiName.Text;
            this._passNoteBook.NoteBookType = this.cobNoteBookType.EditValue == null ? null : this.cobNoteBookType.EditValue.ToString();
            this._passNoteBook.JiaGongDanWeiId = this.txt_JiaGongDanWeiId.Text;
            this._passNoteBook.JiaGongDanWeiName = this.txt_JiaGongDanWeiName.Text;
            this._passNoteBook.ReceivingArea = this.txt_ReceivingArea.Text;
            this._passNoteBook.ReportingUnitId = this.txt_ReportingUnitId.Text;
            this.txt_ReportingUnitName.EditValue = this._passNoteBook.ReportingUnitName;
            this.date_ReportingDate.EditValue = this._passNoteBook.ReportingDate;
            this.txt_ZhengMianXingZhi.EditValue = this._passNoteBook.ZhengMianXingZhi;
            this.txt_AgreeNum.EditValue = this._passNoteBook.AgreeNum;
            this.date_EnteringDate.EditValue = this._passNoteBook.EnteringDate;
            this.txt_ImportContract.EditValue = this._passNoteBook.ImportContract;
            this.txt_ExportContract.EditValue = this._passNoteBook.ExportContract;
            this.txt_ZhuGuanHaiGuan.EditValue = this._passNoteBook.ZhuGuanHaiGuan;
            this.txt_BackImportTotalMoney.EditValue = this._passNoteBook.BackImportTotalMoney;
            this.txt_BackExportTotalMoney.EditValue = this._passNoteBook.BackExportTotalMoney;
            this.txt_ZhuGuanWaiJingMaoWei.EditValue = this._passNoteBook.ZhuGuanWaiJingMaoWei;
            this.lueImportAtCurrencyCategoryId.EditValue = this._passNoteBook.ImportAtCurrencyCategoryId;
            this.lueExportAtCurrencyCategoryId.EditValue = this._passNoteBook.ExportAtCurrencyCategoryId;
            this.cobXianZhiLeiMark.EditValue = this._passNoteBook.XianZhiLeiMark;
            this.txt_ProcessType.EditValue = this._passNoteBook.ProcessType;
            this.txt_BGProductId.EditValue = this._passNoteBook.BGProductId;
            this.cobDanSunReportingLink.EditValue = this._passNoteBook.DanSunReportingLink;
            this.date_EffectiveData.EditValue = this._passNoteBook.EffectiveData;
            this.txt_TradeType.EditValue = this._passNoteBook.TradeType;
            this.speImportProductNum.EditValue = this._passNoteBook.ImportProductNum;
            this.speImportMoney.EditValue = this._passNoteBook.ImportMoney;
            this.speExportProductNum.EditValue = this._passNoteBook.ExportProductNum;
            this.speExportMoney.EditValue = this._passNoteBook.ExportMoney;
            this.txt_ForeignCompany.EditValue = this._passNoteBook.ForeignCompany;
            this.nccEmployeeId.EditValue = this._passNoteBook.EmployeeId;
            this.lueImportExportShore.EditValue = this._passNoteBook.ImportExportShoreId;
            this.speNeiXiaoBiLi.EditValue = this._passNoteBook.NeiXiaoBiLi;
            this.cobManageObject.EditValue = this._passNoteBook.ManageObject;
            this.cobTaiZhangBank.EditValue = this._passNoteBook.TaiZhangBank;
            this.txt_Note.EditValue = this._passNoteBook.Note;
        }

        protected override void Delete()
        {
            if (this._passNoteBook == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.PassNoteBook model = this._manager.GetNext(this._passNoteBook);
                this._manager.Delete(this._passNoteBook.PassNoteBookId);
                if (model == null)
                    this._passNoteBook = this._manager.GetLast();
                else
                    this._passNoteBook = model;
                MessageBox.Show(Properties.Resources.DeleteSuccess);
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PassNoteBook.PRO_PassNoteBookId;
        }

        protected override int AuditState()
        {
            return this._passNoteBook.AuditState.HasValue ? this._passNoteBook.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "PassNoteBook" + "," + this._passNoteBook.PassNoteBookId;
        }

        #endregion
    }
}