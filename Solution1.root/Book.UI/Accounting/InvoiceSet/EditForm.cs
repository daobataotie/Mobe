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
namespace Book.UI.Accounting.InvoiceSet
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.AtInvoiceSet AtInvoiceSet;
        BL.AtInvoiceSetManager AtInvoiceSetManager = new Book.BL.AtInvoiceSetManager();
        public EditForm()
        {
            InitializeComponent();
        }
        #region Override
        protected override void AddNew()
        {
            this.AtInvoiceSet = new Model.AtInvoiceSet();
        }
        protected override void Save()
        {
            this.AtInvoiceSet.InvoiceYear = this.comboBoxEditInvoiceYear.EditValue==null?0:int.Parse(this.comboBoxEditInvoiceYear.EditValue.ToString());
            this.AtInvoiceSet.StartMaths = this.comboBoxEditStartMaths.EditValue == null ? 0 : int.Parse(this.comboBoxEditStartMaths.EditValue.ToString());
            this.AtInvoiceSet.EndMaths = this.comboBoxEditEndMaths.EditValue == null ? 0 : int.Parse(this.comboBoxEditEndMaths.EditValue.ToString());
            this.AtInvoiceSet.InvoiceForm = this.comboBoxEditInvoiceForm.EditValue == null ? null : this.comboBoxEditInvoiceForm.EditValue.ToString();
            this.AtInvoiceSet.StartInvoiceId = this.textEditStartInvoiceId.Text;
            this.AtInvoiceSet.EndInvoiceId = this.textEditEndInvoiceId.Text;
            this.AtInvoiceSet.TheId = this.textEditTheId.Text;
            this.AtInvoiceSet.InvoiceWord = this.textEditInvoiceWord.Text;
            this.AtInvoiceSet.InvoiceCount = this.spinEditInvoiceCount.EditValue == null ? 0 : int.Parse(this.spinEditInvoiceCount.EditValue.ToString());
            switch (this.action)
            {
                case "insert":
                    this.AtInvoiceSetManager.Insert(this.AtInvoiceSet);
                    break;
                case "update":
                    this.AtInvoiceSetManager.Update(this.AtInvoiceSet);
                    break;
                default:
                    break;
            }
        }
        public override void Refresh()
        {

            if (this.AtInvoiceSet == null)
            {
                this.AtInvoiceSet = new Book.Model.AtInvoiceSet();
                this.action = "insert";
            }
            this.bindingSource1.DataSource = this.AtInvoiceSetManager.Select();
            this.comboBoxEditInvoiceYear.EditValue=this.AtInvoiceSet.InvoiceYear;
            this.comboBoxEditStartMaths.EditValue =this.AtInvoiceSet.StartMaths;
            this.comboBoxEditEndMaths.EditValue=this.AtInvoiceSet.EndMaths;
            this.comboBoxEditInvoiceForm.EditValue=this.AtInvoiceSet.InvoiceForm;
            this.textEditStartInvoiceId.Text=this.AtInvoiceSet.StartInvoiceId;
            this.textEditEndInvoiceId.Text=this.AtInvoiceSet.EndInvoiceId;
            this.textEditTheId.Text=this.AtInvoiceSet.TheId;
            this.textEditInvoiceWord.Text=this.AtInvoiceSet.InvoiceWord;
            this.spinEditInvoiceCount.EditValue = this.AtInvoiceSet.InvoiceCount;
            switch (this.action)
            {
                case "insert":
                    this.comboBoxEditInvoiceYear.Properties.ReadOnly = false;
                    this.comboBoxEditStartMaths.Properties.ReadOnly = false;
                    this.comboBoxEditEndMaths.Properties.ReadOnly = false;
                    this.comboBoxEditInvoiceForm.Properties.ReadOnly = false;
                    this.textEditStartInvoiceId.Properties.ReadOnly = false;
                    this.textEditEndInvoiceId.Properties.ReadOnly = false;
                    this.textEditTheId.Properties.ReadOnly = false;
                    this.textEditInvoiceWord.Properties.ReadOnly = false;
                    this.spinEditInvoiceCount.Properties.ReadOnly = false;
                    break;

                case "update":
                    this.comboBoxEditInvoiceYear.Properties.ReadOnly = false;
                    this.comboBoxEditStartMaths.Properties.ReadOnly = false;
                    this.comboBoxEditEndMaths.Properties.ReadOnly = false;
                    this.comboBoxEditInvoiceForm.Properties.ReadOnly = false;
                    this.textEditStartInvoiceId.Properties.ReadOnly = false;
                    this.textEditEndInvoiceId.Properties.ReadOnly = false;
                    this.textEditTheId.Properties.ReadOnly = false;
                    this.textEditInvoiceWord.Properties.ReadOnly = false;
                    this.spinEditInvoiceCount.Properties.ReadOnly = false;
                    break;

                case "view":
                    this.comboBoxEditInvoiceYear.Properties.ReadOnly = true;
                    this.comboBoxEditStartMaths.Properties.ReadOnly = true;
                    this.comboBoxEditEndMaths.Properties.ReadOnly = true;
                    this.comboBoxEditInvoiceForm.Properties.ReadOnly = true;
                    this.textEditStartInvoiceId.Properties.ReadOnly = true;
                    this.textEditEndInvoiceId.Properties.ReadOnly = true;
                    this.textEditTheId.Properties.ReadOnly = true;
                    this.textEditInvoiceWord.Properties.ReadOnly = true;
                    this.spinEditInvoiceCount.Properties.ReadOnly = true;
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
            if (this.AtInvoiceSet == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.AtInvoiceSetManager.Delete(this.AtInvoiceSet.Id);
                this.AtInvoiceSet = this.AtInvoiceSetManager.GetNext(this.AtInvoiceSet);
                if (this.AtInvoiceSet == null)
                {
                    this.AtInvoiceSet = this.AtInvoiceSetManager.GetLast();
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
            this.AtInvoiceSet = this.AtInvoiceSetManager.GetFirst();
        }
        protected override void MovePrev()
        {
            Model.AtInvoiceSet AtInvoiceSet = this.AtInvoiceSetManager.GetPrev(this.AtInvoiceSet);
            if (AtInvoiceSet == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtInvoiceSet = AtInvoiceSet;
        }
        protected override void MoveLast()
        {
            this.AtInvoiceSet = this.AtInvoiceSetManager.GetLast();
        }
        protected override void MoveNext()
        {
            Model.AtInvoiceSet AtInvoiceSet = this.AtInvoiceSetManager.GetNext(this.AtInvoiceSet);
            if (AtInvoiceSet == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.AtInvoiceSet = AtInvoiceSet;
        }
        protected override bool HasRows()
        {
            return this.AtInvoiceSetManager.HasRows();
        }
        protected override bool HasRowsNext()
        {
            return this.AtInvoiceSetManager.HasRowsAfter(this.AtInvoiceSet);
        }
        protected override bool HasRowsPrev()
        {
            return this.AtInvoiceSetManager.HasRowsBefore(this.AtInvoiceSet);
        }
        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.textEditStartInvoiceId, this.textEditEndInvoiceId,this.textEditTheId, this.textEditInvoiceWord, this });
        }
        #endregion

        private void gridView1_Click(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Model.AtInvoiceSet productEpiboly = this.bindingSource1.Current as Model.AtInvoiceSet;
                if (productEpiboly != null)
                {
                    this.AtInvoiceSet = productEpiboly;
                    this.action = "view";
                    this.Refresh();
                }
            }
        }
    }
}