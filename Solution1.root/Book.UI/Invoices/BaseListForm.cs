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

namespace Book.UI.Invoices
{
    public partial class BaseListForm : DevExpress.XtraEditors.XtraForm
    {
        protected BL.InvoiceManager invoiceManager;

        public BaseListForm()
        {
            InitializeComponent();
            this.datetimeBase1 = DateTime.Now.Date.AddMonths(-1);
            this.datetimeBase2 = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }

        protected virtual void ShowSearchForm()
        {
            ChooseQueryPeriodForm f = new ChooseQueryPeriodForm(this.datetimeBase1, this.datetimeBase2);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.datetimeBase1 = f.DateTime1;
                this.datetimeBase2 = f.DateTime2;
                this.LoadInvoices(this.datetimeBase1, this.datetimeBase2);
            }
        }

        protected virtual void LoadInvoices(DateTime datetime1, DateTime datetime2)
        {
            if (this.invoiceManager != null)
            {
                MethodInfo methodInfo = invoiceManager.GetType().GetMethod("Select", new Type[] { typeof(DateTime), typeof(DateTime) });
                this.bindingSource1.DataSource = methodInfo.Invoke(invoiceManager, new object[] { datetime1, datetime2 });
            }
        }

        protected virtual void ExportInvoices(string kind)
        {
            GridView view = this.MainView;
            if (view == null)
                return;

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
            if (this.MainView.Columns.Count > 0)
            {
                tmpHead = this.MainView.Columns[0].AppearanceHeader.Font;
                tmpContent = this.MainView.Columns[0].AppearanceCell.Font;
            }
            foreach (GridColumn column in this.MainView.Columns)
            {
                column.AppearanceHeader.Font = fhead;
                column.AppearanceCell.Font = fcontent;
            }

            PrintingSystem system = new PrintingSystem();
            PrintableComponentLink link = new PrintableComponentLink(system);
            try
            {
                link.Component = this.MainView.GridControl;
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
                foreach (GridColumn column in this.MainView.Columns)
                {
                    column.AppearanceHeader.Font = tmpHead;
                    column.AppearanceCell.Font = tmpContent;
                }
            }
        }

        protected virtual Form GetViewForm()
        {
            return null;
        }

        protected virtual DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = (string)e.Item.Tag;
            if (tag == null) return;
            string action = tag.Contains("-") ? tag.Substring(0, tag.IndexOf("-")) : tag;
            switch (action)
            {
                case "exportinvoices":
                    string kind = tag.Contains("-") ? tag.Substring(tag.IndexOf("-") + 1) : "pdf";
                    this.ExportInvoices(kind);
                    break;

                case "loadinvoices":
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        this.LoadInvoices(this.datetimeBase1, this.datetimeBase2);
                        this.ShowStatistics();
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                    break;

                case "changequeryperiod":
                    ShowSearchForm();
                    break;

                case "viewdetail":
                    Form viewform = this.GetViewForm();
                    if (viewform != null)
                    {
                        viewform.ShowDialog(this);
                    }
                    break;

                case "print":
                    DevExpress.XtraReports.UI.XtraReport r = this.GetReport();
                    if (r != null)
                    {
                        // DevExpress.XtraReports.UI.ReportPrintContext;
                        r.ShowPreview();
                    }
                    break;

                case "turnnormal":
                    break;
                case "del":
                    try
                    {
                        if (MessageBox.Show("確定要刪除？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            //this.invoiceManager.Delete((this.bindingSource1.Current as Model.Invoice).InvoiceId);
                            this.invoiceManager.TurnNull((this.bindingSource1.Current as Model.Invoice).InvoiceId);
                            MessageBox.Show("刪除成功!");
                            Cursor.Current = Cursors.WaitCursor;
                            this.LoadInvoices(this.datetimeBase1, this.datetimeBase2);
                            this.ShowStatistics();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("由於當前記錄被引用，所以刪除失敗。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                default:
                    break;
            }
        }

        private void ShowStatistics()
        {
            this.barStaticItem1.Caption = string.Format("{0}项", this.bindingSource1.Count);
        }

        protected DateTime datetimeBase1;
        protected DateTime datetimeBase2;

        private void BaseListForm_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.LoadInvoices(this.datetimeBase1, this.datetimeBase2);
                this.ShowStatistics();

                if (this.MainView != null)
                {
                    this.MainView.MouseUp += new MouseEventHandler(MainView_MouseUp);
                    this.MainView.DoubleClick += new EventHandler(gridView1_DoubleClick);
                }
            }
        }

        void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow)
            {
                if (e.Button == MouseButtons.Right && this.popupMenu2.ItemLinks.Count > 0)
                {
                    this.popupMenu2.ShowPopup(view.GridControl.PointToScreen(e.Location));
                }
            }
        }

        public virtual Model.Invoice SelectedItem
        {
            get
            {
                return this.bindingSource1.Current as Model.Invoice;
            }
        }

        public virtual string InvoiceKind
        {
            get
            {
                return null;
            }
        }

        protected virtual DevExpress.XtraGrid.Views.Grid.GridView MainView
        {
            get
            {
                return null;
            }
        }

        protected virtual void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                Form viewform = this.GetViewForm();
                if (viewform != null)
                {
                    viewform.ShowDialog(this);
                }
            }
        }

        protected void CancleDelete()
        {
            this.barButtonItem11.Enabled = false;
        }
        protected void CanDelete()
        {
            this.barButtonItem11.Enabled = true;
        }
    }
}