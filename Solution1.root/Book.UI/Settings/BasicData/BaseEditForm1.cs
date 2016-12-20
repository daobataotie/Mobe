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
    public partial class BaseEditForm1 : DevExpress.XtraEditors.XtraForm
    {
        protected IDictionary<string, AA> requireValueExceptions;
        protected IDictionary<string, AA> invalidValueExceptions;
        protected BL.BaseManager manager = null;
        protected string action;

        public BaseEditForm1()
        {
            InitializeComponent();
            this.requireValueExceptions = new Dictionary<string, AA>();
            this.invalidValueExceptions = new Dictionary<string, AA>();
            
        }

        protected virtual void Save()
        {


        }

        protected virtual void Delete()
        {

        }

        protected virtual void AddNew()
        {
        }

        /// <summary>
        /// CaoRui 2012年11月1日10:11:49
        /// 1.{Book.UI.produceManager.PCParameterSet.EditForm} 有使用
        /// </summary>
        protected virtual void Undo()
        { }

        bool f = false;

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //string tag = (string)e.Item.Tag;
            //if (tag == null) return;
            //MethodInfo methodInfo = null;

            //this.gridView1.OptionsView.ShowAutoFilterRow = false;
            //this.gridView1.ActiveFilter.Clear();
            string tag = (string)e.Item.Tag;
            string action = "";
            if (tag != null)
                action = tag.Contains("-") ? tag.Substring(0, tag.IndexOf("-")) : tag;
            switch (action)
            {
                case "save":
                    try
                    {
                        this.Save();
                        this.action = "view";
                        this.Refresh();
                    }
                    catch (Helper.RequireValueException ex)
                    {
                        if (this.requireValueExceptions.ContainsKey(ex.Message))
                        {
                            AA aa = this.requireValueExceptions[ex.Message];
                            MessageBox.Show(aa.Message, "Text", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            aa.Control.Focus();
                            return;
                        }
                        throw;
                    }

                    catch (Helper.InvalidValueException ex)
                    {
                        if (this.invalidValueExceptions.ContainsKey(ex.Message))
                        {
                            AA aa = this.invalidValueExceptions[ex.Message];
                            MessageBox.Show(aa.Message, "Text", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            aa.Control.Focus();
                            return;
                        }
                        throw;
                    }
                    catch (Helper.ViolateConstraintException ex)
                    {
                        MessageBox.Show(Properties.Resources.InvoiceExist, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "new":
                    this.AddNew();
                    this.action = "insert";
                    this.Refresh();
                    break;
                case "update":
                    this.action = "update";
                    this.Refresh();
                    break;
                case "delete":
                    try
                    {
                        this.Delete();
                        this.Refresh();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Helper.ViolateConstraintException exe)
                    {
                        MessageBox.Show(exe.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch
                    {
                        MessageBox.Show(Properties.Resources.DeleteError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "undo":
                    this.Undo();
                    this.action = "view";
                    this.Refresh();
                    break;
                case "export":

                    string kind = tag.Contains("-") ? tag.Substring(tag.IndexOf("-") + 1) : "pdf";
                    this.Export(kind);
                    break;
                case "query":
                    this.gridView1.OptionsView.ShowAutoFilterRow = !f;
                    f = !f;
                    break;

            }

        }

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

        protected virtual void RefreshData()
        {
            MethodInfo methodInfo = null;
            if (this.manager != null)
            {
                methodInfo = this.manager.GetType().GetMethod("Select", new Type[] { });
                if (methodInfo != null)
                {

                    this.bindingSource1.DataSource = methodInfo.Invoke(this.manager, null);
                }
            }
        }

        private void BaseEditForm1_Load(object sender, EventArgs e)
        {
            this.Refresh();
            //this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        public override void Refresh()
        {
            this.barButtonItemNew.Enabled = this.action == "view";
            this.barButtonItemUpdate.Enabled = this.action == "view";

            this.barButtonItemPrint.Enabled = this.action == "view";

            this.barButtonItemSave.Enabled = this.action != "view";
            this.barButtonItemDelete.Enabled = this.action == "view";
            this.barButtonItemUndo.Enabled = this.action != "view";
            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
            base.Refresh();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {
                    this.grid_keyDpwn();
                }

            }
            if (e.KeyData == Keys.Delete)
            {
                grid_KeyDelete();
            }
            this.gridControl1.RefreshDataSource();
        }

        protected virtual void grid_keyDpwn()
        {

        }

        protected virtual void grid_KeyDelete()
        {


        }

    }
}