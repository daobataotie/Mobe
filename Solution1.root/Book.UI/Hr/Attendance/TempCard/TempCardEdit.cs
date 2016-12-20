using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using Book.UI.Settings.BasicData.Employees;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace Book.UI.Hr.Attendance.TempCard
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军              完成时间:2009-10-10
// 修改原因：
// 修 改 人: 刘永亮                   修改时间:2010-07-16
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class TempCardEdit : BaseEditForm
    {

        #region 參數對象的定義
        public static IList<Book.Model.TempCard> _tempCardList = null;
        /// <summary>
        /// 当前选择员工
        /// </summary>
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();


        /// <summary>
        /// 临时卡管理
        /// </summary>
        BL.TempCardManager tempcardmanage = new Book.BL.TempCardManager();


        /// <summary>
        /// 待处理临时卡
        /// </summary>
        Model.TempCard currentTempcard;
        #endregion

        #region 無參構造函數
        public TempCardEdit()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.TempCard.PROPERTY_CARDNO, new AA(Properties.Resources.CardNotNull, this.textEdit_CardNo));
            //this.newChooseContorl1.Choose = new ChooseEmployee(EmployeeParameters.ALL);
            this.newChooseEmployeeId.Choose = new ChooseEmployee(EmployeeParameters.ALL);
            this.action = "insert";
        }
        #endregion

        #region 加載事件
        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempCardEdit_Load(object sender, EventArgs e)
        {
            this.Visibles();
            this.dateEdit_DutyDate.DateTime = DateTime.Now.Date;
            this.TempCardSource1.DataSource = tempcardmanage.SelectbyDateTop();
        }
        #endregion


        #region 重載父類中的方法

        //新增
        protected override void AddNew()
        {
            this.currentTempcard = new Book.Model.TempCard();
            this.currentTempcard.TempCardId = Guid.NewGuid().ToString();
            this.currentTempcard.DutyDate = DateTime.Now.Date;
        }

        /// <summary>
        /// 添加、保存
        /// </summary>
        protected override void Save()
        {
            //if (lookUpEdit_IDNO.EditValue != null)
            //    this.currentTempcard.EmployeeId = lookUpEdit_IDNO.EditValue.ToString();
            if (this.textEdit_CardNo.EditValue != null)
                this.currentTempcard.CardNo = this.textEdit_CardNo.EditValue.ToString();
            this.currentTempcard.Employee = this.newChooseEmployeeId.EditValue as Model.Employee;
            if (this.currentTempcard.Employee != null)
            {
                this.currentTempcard.EmployeeId = this.currentTempcard.Employee.EmployeeId;
            }

            if (dateEdit_DutyDate.EditValue != null)
                this.currentTempcard.DutyDate = this.dateEdit_DutyDate.DateTime;
            else
                this.currentTempcard.DutyDate = DateTime.Now;
            switch (this.action)
            {
                case "insert":
                    this.tempcardmanage.Insert(this.currentTempcard);
                    break;
                case "update":
                    this.tempcardmanage.Update(this.currentTempcard);
                    break;
            }
            this.TempCardSource1.DataSource = tempcardmanage.SelectbyDateTop();
        }

        protected override bool HasRows()
        {
            return this.tempcardmanage.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.tempcardmanage.HasRowsAfter(this.currentTempcard);
        }

        protected override bool HasRowsPrev()
        {
            return this.tempcardmanage.HasRowsBefore(this.currentTempcard);
        }

        protected override void MoveFirst()
        {
            this.currentTempcard = tempcardmanage.GetFirst();
        }

        protected override void MoveNext()
        {
            this.currentTempcard = tempcardmanage.GetNext(this.currentTempcard);
        }

        protected override void MoveLast()
        {
            this.currentTempcard = tempcardmanage.GetLast();
        }

        protected override void MovePrev()
        {
            this.currentTempcard = tempcardmanage.GetPrev(this.currentTempcard);
        }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Refresh()
        {
            if (this.currentTempcard == null)
            {
                this.currentTempcard = new Book.Model.TempCard();
                this.action = "insert";
            }

            //this.lookUpEdit_IDNO.EditValue = this.currentTempcard.EmployeeId;
            //this.textEdit_CardNo.EditValue = this.currentTempcard.CardNo;

            this.newChooseEmployeeId.EditValue = this.currentTempcard.Employee;
            this.textEdit_CardNo.EditValue = this.currentTempcard.CardNo;
            this.dateEdit_DutyDate.EditValue = this.currentTempcard.DutyDate;

            switch (this.action)
            {
                case "insert":
                    this.barButtonItem2.Enabled = false;
                    this.textEdit_CardNo.Properties.ReadOnly = false;
                    this.dateEdit_DutyDate.Properties.ReadOnly = false;
                    break;
                case "update":
                    this.barButtonItem2.Enabled = false;
                    this.textEdit_CardNo.Properties.ReadOnly = false;
                    this.dateEdit_DutyDate.Properties.ReadOnly = false;
                    break;
                case "view":
                    this.barButtonItem2.Enabled = true;
                    this.textEdit_CardNo.Properties.ReadOnly = true;
                    this.dateEdit_DutyDate.Properties.ReadOnly = true;
                    break;
            }
            base.Refresh();
        }

        /// <summary>
        /// 删除
        /// </summary>
        protected override void Delete()
        {
            this.currentTempcard = this.TempCardSource1.Current as Model.TempCard;
            if (this.currentTempcard != null)
            {
                if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.tempcardmanage.Delete(this.currentTempcard.TempCardId);
                }
                this.TempCardSource1.DataSource = tempcardmanage.SelectbyDateTop();
            }
        }

        private void TempCardSource1_CurrentChanged(object sender, EventArgs e)
        {
            Model.TempCard tempcard = this.TempCardSource1.Current as Model.TempCard;
            if (tempcard != null)
            {
                this.currentTempcard = tempcard;
                this.action = "view";
                this.Refresh();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Book.UI.Hr.Attendance.TempCard.SelectTempCardForm f = new SelectTempCardForm();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            this.TempCardSource1.DataSource = _tempCardList;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

                default:
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
                default:
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
    }
}
