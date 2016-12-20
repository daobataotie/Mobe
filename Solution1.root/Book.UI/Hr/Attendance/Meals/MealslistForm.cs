using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.BasicData;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Columns;

namespace Book.UI.Hr.Attendance.Meals
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010 咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军          完成时间:2009-10-19
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class MealslistForm : DevExpress.XtraEditors.XtraForm
    {
        BL.LunchDetailManager LunchM = new Book.BL.LunchDetailManager();
        BL.DepartmentManager deptmanager = new Book.BL.DepartmentManager();
        private DataSet ds = new DataSet();
        
        public MealslistForm()
        {
            InitializeComponent();
        }

        private void MealslistForm_Load(object sender, EventArgs e)
        {
            //InitTreelistWhenDept();
            datelunch.DateTime = DateTime.Now.Date;
            loadlunch();
            repositoryItemLookUpEdit1.DataSource = deptmanager.Select();

            //for (int i = 0; i < System.DateTime.Now.Date.Month; i++)
            //{
            //    if (System.DateTime.Now.Date.Month - i > 0)
            //        this.comboBoxEditDate.Properties.Items.Add(DateTime.Now.AddMonths(-i).ToString("yyyy年MM月"));
            //}
            //this.comboBoxEditDate.SelectedIndex = 0;

        }

        private void loadlunch()
        {
            ds = LunchM.GetEmployeeInfo(datelunch.DateTime);
            bindingSourceLunch.DataSource = ds.Tables["employee"];
            if (ds.Tables["employee"].Rows.Count > 0)
                barStaticItem2.Caption = "共" + ds.Tables["employee"].Rows.Count + "項";
            addAllMoney();
        }

        // 餐费总计
        private void addAllMoney()
        {
            double total = 0;
            foreach (DataRow row in ds.Tables["employee"].Rows)
            {
                if (row["LunchFee"] is Nullable)
                    return;
                total += Convert.ToDouble(row["LunchFee"]);
            }
            calcEdit1.Text = total.ToString();
        }

        private void sbtn_search_Click(object sender, EventArgs e)
        {
            if (datelunch.Text == "")
                return;
            loadlunch();
        }

        private void btn_save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
            {
                return;
            }
          
            try
            {  if (this.ds.HasChanges())
                this.LunchM.UpdateLunchDetail(this.ds.GetChanges(), datelunch.DateTime.Date);
                this.ds.AcceptChanges();
            }
            catch
            {
                MessageBox.Show(Properties.Resources.SavaFailure, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadlunch();
        }   

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumn8)
            {
                decimal pay = decimal.Zero;
                decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn8).ToString(), out pay);
                if (pay - 20 > 0)
                {
                    pay = pay - 20;
                }
                else
                {
                    pay = 0;
                }
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumn9, pay);
                addAllMoney();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag != null)
            {
                string kind = e.Item.Tag.ToString();
                switch (kind)
                {
                    case "export":
                        this.Export(kind);
                        break;
                    case "xls":
                        this.Export(kind);
                        break;
                    case "pdf":
                        this.Export(kind);
                        break;
                    case "doc":
                        this.Export(kind);
                        break;
                }
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

                case "export":
                    this.saveFileDialog1.Filter = "Excel file|*.xls";
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
                case "export":
                    view.GridControl.ExportToXls(file);
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

        private void sbtn_yearAndMonth_Click(object sender, EventArgs e)
        {
        //    string date = this.comboBoxEditDate.Text;

        //    string year = date.Substring(0, date.IndexOf('年'));
        //    string month = date.Substring(date.IndexOf('年') + 1, 2);

        //    ds = LunchM.GetEmployeeInfo(year, month);
        //    bindingSourceLunch.DataSource = ds.Tables["employee"];
        //    if (ds.Tables["employee"].Rows.Count > 0)
        //        barStaticItem2.Caption = "共" + ds.Tables["employee"].Rows.Count + "項";
        //    addAllMoney();
        }
    }
}