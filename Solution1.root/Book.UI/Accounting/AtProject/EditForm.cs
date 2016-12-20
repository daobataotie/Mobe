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
namespace Book.UI.Accounting.AtProject
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.AtProject AtProject;
        BL.AtProjectManager AtProjectManager = new Book.BL.AtProjectManager();
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.AtProject.PRO_Id, new AA("請輸入專案編號", this.textEditProjectId));
            this.requireValueExceptions.Add(Model.AtProject.PRO_ProjectName, new AA("請輸入專案名稱", this.textEditProjectName));
        }
        #region Override
        protected override void AddNew()
        {
            this.AtProject = new Model.AtProject();
        }
        protected override void Save()
        {
            this.AtProject.Id = this.textEditProjectId.Text;
            this.AtProject.ProjectName = this.textEditProjectName.Text;
            this.AtProject.ProjectAddressd = this.textEditProjectAddressd.Text;
            this.AtProject.Mark = this.memoEditMark.Text;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
            {
                this.AtProject.StartDate = null;
            }
            else
            {
                this.AtProject.StartDate = this.dateEditStartDate.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
            {
                this.AtProject.EndDate = null;
            }
            else
            {
                this.AtProject.EndDate = this.dateEditEndDate.DateTime;
            }
            switch (this.action)
            {
                case "insert":
                    this.AtProjectManager.Insert(this.AtProject);
                    break;
                case "update":
                    this.AtProjectManager.Update(this.AtProject);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {

            if (this.AtProject == null)
            {
                this.AtProject = new Book.Model.AtProject();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = this.AtProjectManager.Select();

            this.textEditProjectId.Text = this.AtProject.Id;
              this.textEditProjectName.Text = this.AtProject.ProjectName;
             this.textEditProjectAddressd.Text = this.AtProject.ProjectAddressd;
             this.memoEditMark.Text = this.AtProject.Mark;
             if (global::Helper.DateTimeParse.DateTimeEquls(this.AtProject.StartDate, global::Helper.DateTimeParse.NullDate))
             {
                 this.dateEditStartDate.EditValue = null;
             }
             else
             {
                 this.dateEditStartDate.EditValue = this.AtProject.StartDate;
             }
             if (global::Helper.DateTimeParse.DateTimeEquls(this.AtProject.EndDate, global::Helper.DateTimeParse.NullDate))
             {
                 this.dateEditEndDate.EditValue = null;
             }
             else
             {
                 this.dateEditEndDate.EditValue = this.AtProject.EndDate;
             }
            switch (this.action)
            {
                case "insert":
                    this.textEditProjectId.Properties.ReadOnly = false;
                    this.textEditProjectName.Properties.ReadOnly = false;
                    this.textEditProjectAddressd.Properties.ReadOnly = false;
                    this.memoEditMark.Properties.ReadOnly = false;
                    this.dateEditStartDate.Properties.ReadOnly = false;
                    this.dateEditStartDate.Properties.Buttons[0].Visible = true;
                    this.dateEditEndDate.Properties.ReadOnly = false;
                    this.dateEditEndDate.Properties.Buttons[0].Visible = true;
                    break;

                case "update":
                    this.textEditProjectId.Properties.ReadOnly = false;
                    this.textEditProjectName.Properties.ReadOnly = false;
                    this.textEditProjectAddressd.Properties.ReadOnly = false;
                    this.memoEditMark.Properties.ReadOnly = false;
                    this.dateEditStartDate.Properties.ReadOnly = false;
                    this.dateEditStartDate.Properties.Buttons[0].Visible = true;
                    this.dateEditEndDate.Properties.ReadOnly = false;
                    this.dateEditEndDate.Properties.Buttons[0].Visible = true;
                    break;

                case "view":
                    this.textEditProjectId.Properties.ReadOnly = true;
                    this.textEditProjectName.Properties.ReadOnly = true;
                    this.textEditProjectAddressd.Properties.ReadOnly = true;
                    this.memoEditMark.Properties.ReadOnly = true;
                    this.dateEditStartDate.Properties.ReadOnly = true;
                    this.dateEditStartDate.Properties.Buttons[0].Visible = false;
                    this.dateEditEndDate.Properties.ReadOnly = true;
                    this.dateEditEndDate.Properties.Buttons[0].Visible = false;
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
            if (this.AtProject == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.AtProjectManager.Delete(this.AtProject.ProjectId);
                this.AtProject = this.AtProjectManager.GetNext(this.AtProject);
                if (this.AtProject == null)
                {
                    this.AtProject = this.AtProjectManager.GetLast();
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
            this.AtProject = this.AtProjectManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.AtProject AtProject = this.AtProjectManager.GetPrev(this.AtProject);
            if (AtProject == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtProject = AtProject;
        }
        protected override void MoveLast()
        {
            this.AtProject = this.AtProjectManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.AtProject AtProject = this.AtProjectManager.GetNext(this.AtProject);
            if (AtProject == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtProject = AtProject;
        }
        protected override bool HasRows()
        {
            return this.AtProjectManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.AtProjectManager.HasRowsAfter(this.AtProject);
        }
        protected override bool HasRowsPrev()
        {
            return this.AtProjectManager.HasRowsBefore(this.AtProject);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.textEditProjectId, this.textEditProjectName, this.textEditProjectAddressd, this.memoEditMark, this });
        }
        #endregion

        private void gridControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AtProject productEpiboly = this.bindingSource1.Current as Model.AtProject;
                if (productEpiboly != null)
                {
                    this.AtProject = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}