using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Linq;
namespace Book.UI.Accounting.AtSummon
{
    public partial class EditForm : BaseEditForm
    {
        Model.AtSummon atSummon = new Book.Model.AtSummon();
        BL.AtSummonManager atSummonManager = new Book.BL.AtSummonManager();

        BL.AtSummonDetailManager atSummonDetailManager = new Book.BL.AtSummonDetailManager();

        public EditForm()
        {
            InitializeComponent();

            //添加事件
            this.requireValueExceptions.Add(Model.AtSummon.PRO_Id, new AA(Properties.Resources.RequireDataForId, this.textEditSummonId));
            this.requireValueExceptions.Add(Model.AtSummon.PRO_SummonCategory, new AA("請選擇傳票類別..", this.comboBoxEditSummonCategory));
            //this.requireValueExceptions.Add(Model.AtSummon.PRO_BIllCode, new AA("請輸入傳票詳細資料！", this.gridControl1 as Control));
            //this.invalidValueExceptions.Add(Model.AtSummon.PRO_SummonId, new AA(Properties.Resources.EntityExists, this.textEditSummonId));

            this.bindingSource2.DataSource = new BL.AtAccountSubjectManager().Select();
            //this.comboBoxEditSummonCategory.SelectedIndex = 0;  //默认选择第一项

            this.action = "view";
        }

        public EditForm(Model.AtSummon atSummon)
            : this()
        {
            this.atSummon = atSummon;
            this.atSummon.Details = this.atSummonDetailManager.Select(atSummon);
            this.action = "update";
        }

        public EditForm(Model.AtSummon atSummon, string action)
            : this()
        {
            this.atSummon = atSummon;
            this.atSummon.Details = this.atSummonDetailManager.Select(atSummon);
            this.action = action;
        }

        protected override void AddNew()
        {
            this.atSummon = new Model.AtSummon();
            this.atSummon.SummonDate = DateTime.Now;
            this.atSummon.Id = this.atSummonManager.GetId();
            this.atSummon.SummonId = Guid.NewGuid().ToString();
            this.atSummon.Details = new List<Model.AtSummonDetail>();
            this.atSummon.CreditTotal = 0;
            this.atSummon.TotalDebits = 0;
            //if (this.action == "insert")
            //{
            Model.AtSummonDetail detail = new Model.AtSummonDetail();
            detail.SummonDetailId = Guid.NewGuid().ToString();
            detail.AMoney = 0;
            detail.Subject = new Book.Model.AtAccountSubject();
            switch (this.comboBoxEditSummonCategory.SelectedIndex)
            {
                case 0:
                    detail.Lending = "貸";

                    break;
                case 1:
                    detail.Lending = "借";
                    break;
                case 2:
                    detail.Lending = "";
                    break;
                default:
                    detail.Lending = "";
                    break;
            }

            this.atSummon.Details.Add(detail);
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            this.atSummon.Id = this.textEditSummonId.Text;
            this.atSummon.SummonCategory = this.comboBoxEditSummonCategory.EditValue == null ? null : this.comboBoxEditSummonCategory.EditValue.ToString();
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditSummonDate.DateTime, new DateTime()))
            {
                this.atSummon.SummonDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.atSummon.SummonDate = this.dateEditSummonDate.DateTime;
            }
            this.atSummon.TotalDebits = this.spinEditTotalDebits.EditValue == null ? 0 : decimal.Parse(this.spinEditTotalDebits.EditValue.ToString());
            this.atSummon.CreditTotal = this.spinEditCreditTotal.EditValue == null ? 0 : decimal.Parse(this.spinEditCreditTotal.EditValue.ToString());


            //借贷平衡
            if (this.comboBoxEditSummonCategory.SelectedIndex == 2)
            {
                if (this.spinEditTotalDebits.EditValue != null && this.spinEditCreditTotal.EditValue != null)
                {
                    if (decimal.Parse(this.spinEditTotalDebits.EditValue.ToString()) != decimal.Parse(this.spinEditCreditTotal.EditValue.ToString()))
                    {
                        throw new global::Helper.MessageValueException("借貸雙方不平衡,請檢查輸入是否有誤！");
                    }
                    else
                    {
                        this.atSummon.TotalDebits = decimal.Parse(this.spinEditTotalDebits.EditValue.ToString());
                        this.atSummon.CreditTotal = decimal.Parse(this.spinEditCreditTotal.EditValue.ToString());
                    }
                }
                else
                {
                    throw new global::Helper.MessageValueException("轉帳傳票,借貸金額須相等,借貸數據不完整");
                }

                //删除自增平衡记录
                for (int i = 0; i < this.atSummon.Details.Count; i++)
                {
                    if (this.atSummon.Details[i].Summon != null && this.atSummon.Details[i].Lending.Contains(this.atSummon.Details[i].Summon.Id))
                    {
                        this.atSummon.Details.Remove(this.atSummon.Details[i]);
                    }
                }
            }
            //else
            //{
            //    Model.AtSummonDetail _md;
            //    if (this.atSummon.Details != null)
            //    {
            //        if (!this.atSummon.Details.Any(d => !string.IsNullOrEmpty(d.Lending) && d.Lending.Contains(this.atSummon.Id)))
            //        {
            //            //新增一条附加记录
            //            _md = new Book.Model.AtSummonDetail();

            //            _md.SummonDetailId = Guid.NewGuid().ToString();

            //            _md.SummonId = this.atSummon.SummonId;

            //            //默认现金科目
            //            Model.AtParameterSet atparSet = new BL.AtParameterSetManager().SelectByAtCurrentlyYear(this.atSummon.SummonDate.Value.Year);
            //            if (atparSet != null)
            //            {
            //                _md.Subject = atparSet.ACMoneySubject;
            //                if (_md.Subject != null)
            //                {
            //                    _md.SubjectId = _md.Subject.SubjectId;
            //                }
            //            }
            //            else
            //            {
            //                throw new global::Helper.MessageValueException("請先設置會計參數,<現金科目>參數");
            //            }
            //            //默认增加一条记录
            //            this.atSummon.Details.Add(_md);
            //        }
            //        else
            //        {
            //            _md = this.atSummon.Details.Where(d => d.Lending.Contains(this.atSummon.Id)).First<Model.AtSummonDetail>();
            //        }


            //        if (this.comboBoxEditSummonCategory.SelectedIndex == 0)
            //        {
            //            _md.Lending = "借" + this.atSummon.Id;
            //            _md.SummonCatetory = "現金收入傳票";
            //            _md.AMoney = decimal.Parse(this.spinEditCreditTotal.EditValue.ToString());
            //        }
            //        else
            //        {
            //            _md.Lending = "貸" + this.atSummon.Id;
            //            _md.SummonCatetory = "現在支出傳票";
            //            _md.AMoney = decimal.Parse(this.spinEditTotalDebits.EditValue.ToString());
            //        }
            //    }
            //}

            switch (this.action)
            {
                case "insert":
                    this.atSummonManager.Insert(this.atSummon);
                    break;

                case "update":
                    this.atSummonManager.Update(this.atSummon);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this.atSummon == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.atSummonManager.Delete(this.atSummon);
                MessageBox.Show("刪除成功!");
            }
            this.MoveLast();
        }

        public override void Refresh()
        {
            if (this.atSummon == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this.atSummon = this.atSummonManager.GetDetails(atSummon.SummonId);
                }
            }
            this.textEditSummonId.Text = this.atSummon.Id;

            if (global::Helper.DateTimeParse.DateTimeEquls(this.atSummon.SummonDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditSummonDate.EditValue = null;
            }
            else
            {
                this.dateEditSummonDate.EditValue = this.atSummon.SummonDate;
            }

            this.spinEditCreditTotal.EditValue = this.atSummon.CreditTotal;
            this.spinEditTotalDebits.EditValue = this.atSummon.TotalDebits;

            //详细处理
            this.bindingSource1.DataSource = this.atSummon.Details.Where(d => (!d.Lending.Contains(this.atSummon.Id))).ToList<Model.AtSummonDetail>();

            this.comboBoxEditSummonCategory.EditValue = this.atSummon.SummonCategory;

            switch (this.action)
            {
                case "insert":
                    this.textEditSummonId.Properties.ReadOnly = false;
                    this.comboBoxEditSummonCategory.Properties.ReadOnly = false;
                    this.dateEditSummonDate.Properties.ReadOnly = false;
                    this.simpleButton1.Enabled = true;
                    this.simpleButton2.Enabled = true;
                    this.spinEditTotalDebits.Properties.ReadOnly = false;
                    this.spinEditCreditTotal.Properties.ReadOnly = false;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.textEditSummonId.Properties.ReadOnly = false;
                    this.comboBoxEditSummonCategory.Properties.ReadOnly = false;
                    this.dateEditSummonDate.Properties.ReadOnly = false;
                    this.simpleButton1.Enabled = true;
                    this.simpleButton2.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.spinEditTotalDebits.Properties.ReadOnly = false;
                    this.spinEditCreditTotal.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.textEditSummonId.Properties.ReadOnly = true;
                    this.comboBoxEditSummonCategory.Properties.ReadOnly = true;
                    this.dateEditSummonDate.Properties.ReadOnly = true;
                    this.simpleButton1.Enabled = false;
                    this.simpleButton2.Enabled = false;
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.spinEditTotalDebits.Properties.ReadOnly = true;
                    this.spinEditCreditTotal.Properties.ReadOnly = true;
                    break;

                default:
                    break;
            }

            base.Refresh();

            this.spinEditCreditTotal.Properties.ReadOnly = true;
            this.spinEditTotalDebits.Properties.ReadOnly = true;

            if (this.action != "view")
            {
                switch (this.comboBoxEditSummonCategory.SelectedIndex)
                {
                    case 0:
                        this.colJieorDai.OptionsColumn.AllowEdit = false;
                        foreach (Model.AtSummonDetail d in this.bindingSource1.DataSource as IList<Model.AtSummonDetail>)
                        {
                            d.Lending = "貸";
                        }
                        break;
                    case 1:
                        this.colJieorDai.OptionsColumn.AllowEdit = false;
                        foreach (Model.AtSummonDetail d in this.bindingSource1.DataSource as IList<Model.AtSummonDetail>)
                        {
                            d.Lending = "借";
                        }
                        break;
                    case 2:
                        this.colJieorDai.OptionsColumn.AllowEdit = true;
                        break;
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        protected override void MoveNext()
        {
            Model.AtSummon atSummon = this.atSummonManager.GetNext(this.atSummon);
            if (atSummon == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.atSummon = this.atSummonManager.Get(atSummon.SummonId);
        }

        protected override void MovePrev()
        {
            Model.AtSummon atSummon = this.atSummonManager.GetPrev(this.atSummon);
            if (atSummon == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.atSummon = this.atSummonManager.Get(atSummon.SummonId);
        }

        protected override void MoveFirst()
        {
            this.atSummon = this.atSummonManager.Get(this.atSummonManager.GetFirst() == null ? "" : this.atSummonManager.GetFirst().SummonId);
        }

        protected override void MoveLast()
        {
            this.atSummon = this.atSummonManager.Get(this.atSummonManager.GetLast() == null ? "" : this.atSummonManager.GetLast().SummonId);
        }

        protected override bool HasRows()
        {
            return this.atSummonManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.atSummonManager.HasRowsAfter(this.atSummon);
        }

        protected override bool HasRowsPrev()
        {
            return this.atSummonManager.HasRowsBefore(this.atSummon);
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.AtSummonDetail> details = this.bindingSource1.DataSource as IList<Model.AtSummonDetail>;
            //if (details == null || details.Count < 1) return;
            //Model.AtAccountSubject detail = details[e.ListSourceRowIndex].Subject;
            //switch (e.Column.Name)
            //{

            //    case "gridColumn2":
            //        if (detail == null) return;
            //        e.DisplayText = string.IsNullOrEmpty(detail.SubjectId) ? "" : detail.Id;
            //        break;
            //}
        }

        // '+'
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.AddDataRows();
        }

        // '-'
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.atSummon.Details.Remove(this.bindingSource1.Current as Book.Model.AtSummonDetail);

                (this.bindingSource1.DataSource as IList<Model.AtSummonDetail>).Remove(this.bindingSource1.Current as Model.AtSummonDetail);

                if ((this.bindingSource1.DataSource as IList<Model.AtSummonDetail>).Count == 0)
                {
                    this.AddDataRows();
                }

                this.gridControl1.RefreshDataSource();

                this.bindingSource1.Position = this.atSummon.Details.Count - 1;
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.AtSummonDetail _detail = this.gridView1.GetRow(e.RowHandle) as Model.AtSummonDetail;
            IList<Model.AtSummonDetail> _detailList = this.bindingSource1.DataSource as IList<Model.AtSummonDetail>;

            if (e.Column == this.colJinE || e.Column == this.colJieorDai)
            {
                this.spinEditTotalDebits.EditValue = _detailList.Where(d => d.Lending == "借").ToList().Sum(d => d.AMoney);
                this.spinEditCreditTotal.EditValue = _detailList.Where(d => d.Lending == "貸").ToList().Sum(d => d.AMoney);
            }

            //this.bindingSource1.Position = this.bindingSource1.IndexOf(_detail);
            this.gridControl1.RefreshDataSource();
        }

        //更改传票类别
        private void comboBoxEditSummonCategory_EditValueChanged(object sender, EventArgs e)
        {
            IList<Model.AtSummonDetail> _details = this.bindingSource1.DataSource as IList<Model.AtSummonDetail>;

            if (_details != null && _details.Count != 0)
            {
                if (this.action != "view")
                {
                    switch (this.comboBoxEditSummonCategory.SelectedIndex)
                    {
                        case 0:
                            this.colJieorDai.OptionsColumn.AllowEdit = false;
                            foreach (Model.AtSummonDetail d in _details)
                            {
                                d.Lending = "貸";
                            }
                            break;
                        case 1:
                            this.colJieorDai.OptionsColumn.AllowEdit = false;
                            foreach (Model.AtSummonDetail d in _details)
                            {
                                d.Lending = "借";
                            }
                            break;
                        case 2:
                            this.colJieorDai.OptionsColumn.AllowEdit = true;
                            break;
                    }

                    this.spinEditCreditTotal.EditValue = _details.Where(d => d.Lending == "貸") == null ? 0 : _details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);
                    this.spinEditTotalDebits.EditValue = _details.Where(d => d.Lending == "借") == null ? 0 : _details.Where(d => d.Lending == "借").Sum(d => d.AMoney);

                    this.gridControl1.RefreshDataSource();
                }
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action != "view")
            {
                switch (e.KeyValue)
                {
                    case 13:
                        if ((sender as GridView).FocusedColumn == this.colJinE)
                        {
                            this.AddDataRows();
                            (sender as GridView).FocusedColumn = this.colJieorDai;
                        }
                        break;
                    case 46:
                        if (this.bindingSource1.Current != null)
                        {
                            this.atSummon.Details.Remove(this.bindingSource1.Current as Model.AtSummonDetail);

                            (this.bindingSource1.DataSource as IList<Model.AtSummonDetail>).Remove(this.bindingSource1.Current as Model.AtSummonDetail);

                            if ((this.bindingSource1.DataSource as IList<Model.AtSummonDetail>).Count == 0)
                            {
                                this.AddDataRows();
                            }
                        }
                        this.gridControl1.RefreshDataSource();
                        this.bindingSource1.Position = this.atSummon.Details.Count - 1;
                        break;
                }
            }
        }

        //添加行
        private void AddDataRows()
        {
            Model.AtSummonDetail mdetail = new Book.Model.AtSummonDetail();
            mdetail.SummonDetailId = Guid.NewGuid().ToString();
            mdetail.Subject = new Book.Model.AtAccountSubject();
            mdetail.SummonId = this.atSummon.SummonId;
            mdetail.InsertTime = DateTime.Now;
            mdetail.UpdateTime = DateTime.Now;
            mdetail.AMoney = 0;

            switch (this.comboBoxEditSummonCategory.SelectedIndex)
            {
                case 0:
                    mdetail.Lending = "貸";
                    break;
                case 1:
                    mdetail.Lending = "借";
                    break;
                case 3:
                    mdetail.Lending = "";
                    break;
            }

            this.atSummon.Details.Add(mdetail);

            (this.bindingSource1.DataSource as IList<Model.AtSummonDetail>).Add(mdetail);

            this.gridControl1.RefreshDataSource();

            this.bindingSource1.Position = this.bindingSource1.IndexOf(mdetail);
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.AtSummon currentModel = f.SelectItem as Model.AtSummon;
                if (currentModel != null)
                {
                    this.atSummon = currentModel;
                    this.atSummon = this.atSummonManager.GetDetails(this.atSummon.SummonId);
                    this.Refresh();
                }
            }
            f.Dispose();
            GC.Collect();
        }

        private void dateEditSummonDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action == "insert")
            {
                this.textEditSummonId.Text = this.atSummonManager.GetId(this.dateEditSummonDate.DateTime);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List f = new List();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.AtSummon model = f.SelectItem as Model.AtSummon;
                if (model != null)
                {
                    this.atSummon = model;
                    this.Refresh();
                }
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(this.atSummon);
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConditionForm f = new ConditionForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                IList<Model.AtSummon> list = this.atSummonManager.SelectByCondition(f.condition.StartDate, f.condition.EndDate, f.condition.StartId, f.condition.EndId);
                if (list.Count == 0)
                {
                    MessageBox.Show("無資料！", this.Text);
                    return;
                }

                RO1 r = new RO1(list);
                r.ShowPreviewDialog();
            }
        }
    }
}