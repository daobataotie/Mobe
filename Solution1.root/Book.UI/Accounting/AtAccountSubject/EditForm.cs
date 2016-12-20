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
namespace Book.UI.Accounting.AtAccountSubject
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.AtAccountSubject AtAccountSubject;
        BL.AtAccountSubjectManager AtAccountSubjectManager = new Book.BL.AtAccountSubjectManager();

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtAccountSubject.PRO_Id, new AA("請輸入科目編號", this.textEditSubjectId));
            this.requireValueExceptions.Add(Model.AtAccountSubject.PRO_SubjectName, new AA("請輸入科目名稱", this.textEditSubjectName));
            this.requireValueExceptions.Add(Model.AtAccountSubject.PRO_AccountingCategoryId, new AA("請類別編號", this.newChooseAccountingCategoryId));

            this.newChooseAccountingCategoryId.Choose = new Accounting.AccountingCategory.ChooseAccountingCategory();
            this.nccAtAccountSubject.Choose = new Accounting.AtAccountSubject.ChooseAccountSubject();

            this.action = "view";
        }

        protected override void AddNew()
        {
            this.AtAccountSubject = new Model.AtAccountSubject();
            this.AtAccountSubject.Id = this.AtAccountSubjectManager.GetId();
        }

        protected override void Save()
        {
            this.AtAccountSubject.AccountingCategory = this.newChooseAccountingCategoryId.EditValue as Model.AtAccountingCategory;
            if (this.AtAccountSubject.AccountingCategory != null)
            {
                this.AtAccountSubject.AccountingCategoryId = this.AtAccountSubject.AccountingCategory.AccountingCategoryId;
            }
            this.AtAccountSubject.Id = this.textEditSubjectId.Text;
            this.AtAccountSubject.SubjectName = this.textEditSubjectName.Text;
            this.AtAccountSubject.CommonSummary = this.textEditCommonSummary.Text;

            if (this.nccAtAccountSubject.EditValue != null)
            {
                this.AtAccountSubject.UnderSubject = (this.nccAtAccountSubject.EditValue as Model.AtAccountSubject).SubjectId;
            }

            if (this.radioGroupTheLending.SelectedIndex == 0)
            {
                this.AtAccountSubject.TheLending = "借";
            }
            else
            {
                this.AtAccountSubject.TheLending = "貸";
            }
            this.AtAccountSubject.TheBalance = this.spinEditTheBalance.EditValue == null ? 0 : decimal.Parse(this.spinEditTheBalance.EditValue.ToString());
            switch (this.action)
            {
                case "insert":
                    this.AtAccountSubjectManager.Insert(this.AtAccountSubject);
                    break;
                case "update":
                    this.AtAccountSubjectManager.Update(this.AtAccountSubject);
                    break;
                default:
                    break;
            }
        }

        public override void Refresh()
        {
            if (this.AtAccountSubject == null)
            {
                this.AddNew();
                this.action = "insert";
            }

            //排序
            //this.bindingSource1.DataSource = this.MXsrot(this.AtAccountSubjectManager.Select());
            this.bindingSource1.DataSource = this.AtAccountSubjectManager.Select();

            this.newChooseAccountingCategoryId.EditValue = this.AtAccountSubject.AccountingCategory;
            this.textEditSubjectId.Text = this.AtAccountSubject.Id;
            this.textEditSubjectName.Text = this.AtAccountSubject.SubjectName;
            this.textEditCommonSummary.Text = this.AtAccountSubject.CommonSummary;
            this.spinEditTheBalance.EditValue = this.AtAccountSubject.TheBalance;

            this.nccAtAccountSubject.EditValue = this.AtAccountSubjectManager.Get(this.AtAccountSubject.UnderSubject);

            if (this.AtAccountSubject.TheLending == "借")
            {
                this.radioGroupTheLending.SelectedIndex = 0;
            }
            else
            {
                this.radioGroupTheLending.SelectedIndex = 1;
            }
            switch (this.action)
            {
                case "insert":
                    this.newChooseAccountingCategoryId.ShowButton = true;
                    this.newChooseAccountingCategoryId.ButtonReadOnly = false;
                    this.textEditSubjectId.Properties.ReadOnly = false;
                    this.textEditSubjectName.Properties.ReadOnly = false;
                    this.textEditCommonSummary.Properties.ReadOnly = false;
                    this.spinEditTheBalance.Properties.ReadOnly = false;
                    this.radioGroupTheLending.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.newChooseAccountingCategoryId.ShowButton = true;
                    this.newChooseAccountingCategoryId.ButtonReadOnly = false;
                    this.textEditSubjectId.Properties.ReadOnly = false;
                    this.textEditSubjectName.Properties.ReadOnly = false;
                    this.textEditCommonSummary.Properties.ReadOnly = false;
                    this.spinEditTheBalance.Properties.ReadOnly = false;
                    this.radioGroupTheLending.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.newChooseAccountingCategoryId.ShowButton = false;
                    this.newChooseAccountingCategoryId.ButtonReadOnly = true;
                    this.textEditSubjectId.Properties.ReadOnly = true;
                    this.textEditSubjectName.Properties.ReadOnly = true;
                    this.textEditCommonSummary.Properties.ReadOnly = true;
                    this.spinEditTheBalance.Properties.ReadOnly = true;
                    this.radioGroupTheLending.Properties.ReadOnly = true;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }

        protected override void Delete()
        {
            if (this.AtAccountSubject == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.AtAccountSubjectManager.Delete(this.AtAccountSubject.SubjectId);
                this.AtAccountSubject = this.AtAccountSubjectManager.GetNext(this.AtAccountSubject);
                if (this.AtAccountSubject == null)
                {
                    this.AtAccountSubject = this.AtAccountSubjectManager.GetLast();
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
            this.AtAccountSubject = this.AtAccountSubjectManager.GetFirst();
        }

        protected override void MovePrev()
        {
            Model.AtAccountSubject AtAccountSubject = this.AtAccountSubjectManager.GetPrev(this.AtAccountSubject);
            if (AtAccountSubject == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtAccountSubject = AtAccountSubject;
        }

        protected override void MoveLast()
        {
            this.AtAccountSubject = this.AtAccountSubjectManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.AtAccountSubject AtAccountSubject = this.AtAccountSubjectManager.GetNext(this.AtAccountSubject);
            if (AtAccountSubject == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtAccountSubject = AtAccountSubject;
        }

        protected override bool HasRows()
        {
            return this.AtAccountSubjectManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.AtAccountSubjectManager.HasRowsAfter(this.AtAccountSubject);
        }

        protected override bool HasRowsPrev()
        {
            return this.AtAccountSubjectManager.HasRowsBefore(this.AtAccountSubject);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.textEditSubjectId, this.textEditSubjectName, this.textEditCommonSummary, this.newChooseAccountingCategoryId, this });
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AtAccountSubject productEpiboly = this.bindingSource1.Current as Model.AtAccountSubject;
                if (productEpiboly != null)
                {
                    this.AtAccountSubject = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }

        //数据粗排序
        private IList<Model.AtAccountSubject> MXsrot(IList<Model.AtAccountSubject> datas)
        {
            IList<Model.AtAccountSubject> temp = new List<Model.AtAccountSubject>();

            for (int i = 0; i < datas.Count; i++)
            {
                temp.Add(datas[i]);
                for (int j = i + 1; j < datas.Count; j++)
                {
                    if (datas[j].UnderSubject == datas[i].SubjectId)
                    {
                        temp.Add(datas[j]);
                    }
                }
            }
            return temp;
        }
    }
}