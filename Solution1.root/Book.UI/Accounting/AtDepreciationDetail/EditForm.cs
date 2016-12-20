using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
namespace Book.UI.Accounting.AtDepreciationDetail
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.AtDepreciationDetail AtDepreciationDetail;
        BL.AtDepreciationDetailManager AtDepreciationDetailManager = new Book.BL.AtDepreciationDetailManager();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtDepreciationDetail.PRO_PropertyId, new AA("請選擇財產編號", this.newChooseContorlPropertyId));
            this.newChooseContorlPropertyId.Choose = new Accounting.AtProperty.ChooseProperty();
            this.bindingSource1.DataSource = new BL.AtAccountSubjectManager().Select();
        }
        #region Override
        protected override void AddNew()
        {
            this.AtDepreciationDetail = new Model.AtDepreciationDetail();
        }
        protected override void Save()
        {
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditDepreciationDate.DateTime, new DateTime()))
            {
                this.AtDepreciationDetail.DepreciationDate = null;
            }
            else
            {
                this.AtDepreciationDetail.DepreciationDate = this.dateEditDepreciationDate.DateTime;
            }
            AtDepreciationDetail.Property = this.newChooseContorlPropertyId.EditValue as Model.AtProperty;
            if (AtDepreciationDetail.Property != null)
            {
                AtDepreciationDetail.PropertyId = AtDepreciationDetail.Property.PropertyId;
            }
            this.AtDepreciationDetail.DepreciationMoney = this.spinEditDepreciationMoney.EditValue == null ? 0 : decimal.Parse(this.spinEditDepreciationMoney.EditValue.ToString());
            //this.AtDepreciationDetail.RespectiveSubject = this.lookUpEditRespectiveSubject.EditValue == null ? null : this.lookUpEditRespectiveSubject.EditValue.ToString();
            this.AtDepreciationDetail.CostSubject = this.lookUpEditCostSubject.EditValue == null ? null : this.lookUpEditCostSubject.EditValue.ToString();
            this.AtDepreciationDetail.Mark = this.memoEditMark.Text;
           
            switch (this.action)
            {
                case "insert":
                    this.AtDepreciationDetailManager.Insert(this.AtDepreciationDetail);
                    break;
                case "update":
                    this.AtDepreciationDetailManager.Update(this.AtDepreciationDetail);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {

            if (this.AtDepreciationDetail == null)
            {
                this.AtDepreciationDetail = new Book.Model.AtDepreciationDetail();
                this.action = "insert";
            }
            this.bindingSource2.DataSource = this.AtDepreciationDetailManager.Select();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.AtDepreciationDetail.DepreciationDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditDepreciationDate.EditValue = null;
            }
            else
            {
                this.dateEditDepreciationDate.EditValue = this.AtDepreciationDetail.DepreciationDate;
            }
            this.newChooseContorlPropertyId.EditValue = AtDepreciationDetail.Property;
            this.spinEditDepreciationMoney.EditValue = this.AtDepreciationDetail.DepreciationMoney;
            this.lookUpEditDepreciationSubject.EditValue = this.AtDepreciationDetail.DepreciationSubject;
            this.lookUpEditCostSubject.EditValue = this.AtDepreciationDetail.CostSubject;
            this.memoEditMark.Text = this.AtDepreciationDetail.Mark;
            switch (this.action)
            {
                case "insert":
                    this.dateEditDepreciationDate.Properties.ReadOnly = false;
                    this.dateEditDepreciationDate.Properties.Buttons[0].Visible = true;
                    this.spinEditDepreciationMoney.Properties.ReadOnly = false;
                    this.lookUpEditDepreciationSubject.Properties.ReadOnly = false;
                    this.lookUpEditCostSubject.Properties.ReadOnly = false;
                    this.memoEditMark.Properties.ReadOnly = false;
                    this.newChooseContorlPropertyId.ShowButton = true;
                    this.newChooseContorlPropertyId.ButtonReadOnly = false;
                    break;

                case "update":
                    this.dateEditDepreciationDate.Properties.ReadOnly = false;
                    this.dateEditDepreciationDate.Properties.Buttons[0].Visible = true;
                    this.spinEditDepreciationMoney.Properties.ReadOnly = false;
                    this.lookUpEditDepreciationSubject.Properties.ReadOnly = false;
                    this.lookUpEditCostSubject.Properties.ReadOnly = false;
                    this.memoEditMark.Properties.ReadOnly = false;
                    this.newChooseContorlPropertyId.ShowButton = true;
                    this.newChooseContorlPropertyId.ButtonReadOnly = false;
                    break;

                case "view":
                    this.dateEditDepreciationDate.Properties.ReadOnly = true;
                    this.dateEditDepreciationDate.Properties.Buttons[0].Visible = false;
                    this.spinEditDepreciationMoney.Properties.ReadOnly = true;
                    this.lookUpEditDepreciationSubject.Properties.ReadOnly = true;
                    this.lookUpEditCostSubject.Properties.ReadOnly = true;
                    this.memoEditMark.Properties.ReadOnly = true;
                    this.newChooseContorlPropertyId.ShowButton = false;
                    this.newChooseContorlPropertyId.ButtonReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        /// <summary>
        /// 删除
        /// </summary>
        protected override void Delete()
        {
            if (this.AtDepreciationDetail == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.AtDepreciationDetailManager.Delete(this.AtDepreciationDetail.DepreciationId);
                this.AtDepreciationDetail = this.AtDepreciationDetailManager.GetNext(this.AtDepreciationDetail);
                if (this.AtDepreciationDetail == null)
                {
                    this.AtDepreciationDetail = this.AtDepreciationDetailManager.GetLast();
                }
            }
            catch
            {
                throw new Exception("");
            }

            return;

        }
        protected override void MoveFirst()
        {
            this.AtDepreciationDetail = this.AtDepreciationDetailManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.AtDepreciationDetail AtDepreciationDetail = this.AtDepreciationDetailManager.GetPrev(this.AtDepreciationDetail);
            if (AtDepreciationDetail == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtDepreciationDetail = AtDepreciationDetail;
        }
        protected override void MoveLast()
        {
            this.AtDepreciationDetail = this.AtDepreciationDetailManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.AtDepreciationDetail AtDepreciationDetail = this.AtDepreciationDetailManager.GetNext(this.AtDepreciationDetail);
            if (AtDepreciationDetail == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtDepreciationDetail = AtDepreciationDetail;
        }
        protected override bool HasRows()
        {
            return this.AtDepreciationDetailManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.AtDepreciationDetailManager.HasRowsAfter(this.AtDepreciationDetail);
        }
        protected override bool HasRowsPrev()
        {
            return this.AtDepreciationDetailManager.HasRowsBefore(this.AtDepreciationDetail);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.dateEditDepreciationDate, this.newChooseContorlPropertyId,this });
        }
        #endregion


        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AtDepreciationDetail productEpiboly = this.bindingSource2.Current as Model.AtDepreciationDetail;
                if (productEpiboly != null)
                {
                    this.AtDepreciationDetail = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}