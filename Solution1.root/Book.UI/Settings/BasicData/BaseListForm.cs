using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Columns;
namespace Book.UI.Settings.BasicData
{
    public partial class BaseListForm : DevExpress.XtraEditors.XtraForm
    {
        #region Data
        protected BL.BaseManager manager = null;

        #endregion

        #region Constructors

        public BaseListForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
        }

        private bool isEdit = false;

        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; }
        }


        #endregion

        #region Form Events Handing

        public object SelectItem
        {
            get
            {
                return this.bindingSource1.Current;
            }
        }

        protected virtual void RefreshData()
        {
            MethodInfo methodInfo = null;
            if (this.manager != null)
            {
                methodInfo = this.manager.GetType().GetMethod("Select", new Type[] { });
                if (methodInfo != null)
                {
                    this.bindingSource1.DataSource = methodInfo.Invoke(this.manager, null);
                    //this.gridControl1.RefreshDataSource();
                }
            }
        }

        private void BaseListForm_Load(object sender, EventArgs e)
        {
            RefreshData();
            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        #endregion

        #region BarManager1 Events Handing

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = (string)e.Item.Tag;

            if (tag == null) return;
            MethodInfo methodInfo = null;
            string action = tag.Contains("-") ? tag.Substring(0, tag.IndexOf("-")) : tag;
            //  this.gridView1.OptionsView.ShowAutoFilterRow = false;
            //this.gridView1.ActiveFilter.Clear();
            switch (action)
            {
                case "insert":
                    BaseEditForm f1 = this.GetEditForm();
                    if (f1 != null)
                    {
                        if (f1.ShowDialog() == DialogResult.OK)
                        {
                            RefreshData();
                            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
                        }
                        this.bindingSource1.Position = this.bindingSource1.IndexOf(f1.EditedItem);
                    }
                    break;

                case "update":
                    if (this.bindingSource1.Current == null)
                    {
                        updates();
                    }
                    else
                    {
                        BaseEditForm f2 = this.GetEditForm(new object[] { this.bindingSource1.Current });
                        if (f2 != null)
                        {
                            if (f2.ShowDialog() == DialogResult.OK)
                            {
                                RefreshData();
                                this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
                            }
                            //f2.MdiParent = this.MdiParent;
                            //f2.Show();
                            this.bindingSource1.Position = this.bindingSource1.IndexOf(f2.EditedItem);
                        }
                    }
                    break;

                case "delete":
                    if (this.bindingSource1.Current == null)
                    {
                        MessageBox.Show(Properties.Resources.ErrorNothingSelected, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    {
                        return;
                    }
                    methodInfo = this.manager.GetType().GetMethod("Delete", new Type[] { this.bindingSource1.Current.GetType() });
                    if (methodInfo != null)
                    {
                        try
                        {
                            methodInfo.Invoke(this.manager, new object[] { this.bindingSource1.Current });
                        }
                        catch (Helper.ViolateConstraintException ex)
                        {
                            MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch
                        {
                            MessageBox.Show(Properties.Resources.DeleteError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        RefreshData();
                        this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
                    }
                    break;

                case "export":
                    string kind = tag.Contains("-") ? tag.Substring(tag.IndexOf("-") + 1) : "pdf";
                    this.Export(kind);
                    break;
                case "query":
                    this.gridView1.OptionsView.ShowAutoFilterRow = !f;
                    f = !f;
                    break;
                case "refresh":
                    RefreshData();
                    this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
                    break;
                default:
                    break;
            }

            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        bool f = false;

        #endregion

        #region Overrideable

        protected virtual BaseEditForm GetEditForm()
        {
            return null;
        }

        protected virtual BaseEditForm GetEditForm(object[] args)
        {
            return null;
        }

        #endregion

        #region Helpers

        private void Export(string kind)
        {
            GridView view = this.gridView1;

            switch (kind)
            {
                case "pdf":
                    this.saveFileDialog1.Filter = "PDF file|*.pdf";
                    break;

                case "xls":
                    this.saveFileDialog1.Filter = "Excel file|*.xls";
                    break;

                case "doc":
                    this.saveFileDialog1.Filter = "Word file|*.doc";
                    break;

                default:
                    break;
            }
            if (this.saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                return;

            string file = this.saveFileDialog1.FileName;
            switch (kind)
            {
                case "pdf":
                    //view.GridControl.ExportToPdf(file);
                    this.ExportToPdf(file);
                    break;
                case "xls":
                    view.GridControl.ExportToXls(file);
                    break;
                case "doc":
                    view.GridControl.ExportToRtf(file);
                    break;
                default:
                    break;
            }
        }

        private void ExportToPdf(string file)
        {
            System.Drawing.Font fhead = new System.Drawing.Font("DFKai-SB", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            System.Drawing.Font fcontent = new System.Drawing.Font("DFKai-SB", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            System.Drawing.Font tmpHead = null;
            System.Drawing.Font tmpContent = null;
            if (this.gridView1.Columns.Count > 0)
            {
                tmpHead = this.gridView1.Columns[0].AppearanceHeader.Font;
                tmpContent = this.gridView1.Columns[0].AppearanceCell.Font;
            }
            foreach (GridColumn column in this.gridView1.Columns)
            {
                column.AppearanceHeader.Font = fhead;
                column.AppearanceCell.Font = fcontent;
            }

            PrintingSystem system = new PrintingSystem();
            PrintableComponentLink link = new PrintableComponentLink(system);
            try
            {
                link.Component = this.gridView1.GridControl;
                link.Landscape = true;
                link.PaperKind = System.Drawing.Printing.PaperKind.A4;
                link.Margins = new System.Drawing.Printing.Margins(30, 30, 50, 50);
                link.CreateDocument();
                //link.PrintingSystem.Document.PrintingSystem.PageMargins.Bottom = 10;
                //link.PrintingSystem.Document.PrintingSystem.PageMargins.Left = 1000;
                //link.PrintingSystem.Document.PrintingSystem.PageMargins.Right = 10;
                //link.PrintingSystem.Document.PrintingSystem.PageMargins.Top = 30;
                //PdfDocumentOptions pdo = new PdfDocumentOptions();
                //pdo.Author = "author";
                //pdo.Keywords = "keywords";
                //pdo.Subject = "subject";
                //pdo.Title = "title";
                //pdo.Application = "application";
                PdfExportOptions op = new PdfExportOptions();
                op.DocumentOptions.Author = "author";
                op.DocumentOptions.Keywords = "keywords";
                op.DocumentOptions.Subject = "subject";
                op.DocumentOptions.Title = "title";
                op.DocumentOptions.Application = "application";
                op.ImageQuality = PdfJpegImageQuality.Highest;

                link.PrintingSystem.ExportToPdf(file, op);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                system = null;
                link = null;
                foreach (GridColumn column in this.gridView1.Columns)
                {
                    column.AppearanceHeader.Font = tmpHead;
                    column.AppearanceCell.Font = tmpContent;
                }
            }
        }

        #endregion

        public virtual void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            //if (this.isEdit) return;
            //GridView view = sender as GridView;
            //GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            //if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            //{
            //    BaseEditForm f1 = this.GetEditForm(new object[] { this.bindingSource1.Current, "view" });
            //    if (f1 != null)
            //        f1.Show();
            //    // 

            //    //if (f1.ShowDialog() == DialogResult.OK)
            //    //{
            //    //    f1.MdiParent = this.MdiParent;
            //    //    RefreshData();
            //    //}
            // }
        }

        public virtual void updates()
        {
            MessageBox.Show(Properties.Resources.ErrorNothingSelected, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;

        }

        protected void CancleDelete()
        {
            this.barButtonItem3.Enabled = false;
        }
    }
}