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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;

namespace Book.UI.Hr.Attendance.LendRecord
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���޾�             ���ʱ��:2009-10-18
// �޸�ԭ��
// �� �� ��: ������                   �޸�ʱ��:2010-07-17
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class LendRecordForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.LoanDetailManager manager = new Book.BL.LoanDetailManager();
        private DataSet ds = new DataSet();
        private bool IsCopy = false;

        public LendRecordForm()
        {
            InitializeComponent();
        }

        private void LendRecordForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                DateTime.Now.AddMonths(-i);
                this.comboBoxEdit1.Properties.Items.Add(DateTime.Now.AddMonths(-i).ToString("yyyy��MM��"));
                if (i > 0)
                    this.comboxDataFrom.Properties.Items.Add(DateTime.Now.AddMonths(-i).ToString("yyyy��MM��"));
            }

            this.comboBoxEdit1.SelectedIndex = 0;
            NewMethod();
        }

        private void NewMethod()
        {
            if (string.IsNullOrEmpty(this.comboBoxEdit1.Text))
            {
                MessageBox.Show("Ո�x�����ڣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //��ȡcomboBox��������
                int year = Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4));
                int month = Int32.Parse(this.comboBoxEdit1.Text.Substring(5, 2));

                //������Դ
                ds = manager.SelestLoanList(year, month);

                bindingSource1.DataSource = ds.Tables["LoanListInfo"];
                this.Refresh();
                CostTotal();
                if (ds.Tables["LoanListInfo"].Rows.Count > 0)
                    barStaticItem1.Caption = "��" + ds.Tables["LoanListInfo"].Rows.Count + "�";
            }

            double sum = 0;
            for (int i = 0; i < ds.Tables["LoanListInfo"].Rows.Count; i++)
            {
                sum += Convert.ToDouble(ds.Tables["LoanListInfo"].Rows[i]["LoanFee"]);
            }

            this.txtLoanFee.Text = sum.ToString();
        }

        //�ܼ�
        private void CostTotal()
        {
            decimal total = decimal.Zero;
            foreach (DataRow row in ds.Tables["LoanListInfo"].Rows)
            {
                if (row["LoanFee"] is Nullable)
                    return;
                total += Convert.ToDecimal(row["LoanFee"]);
            }
            this.txtLoanFee.Text = total.ToString();
        }

        //��������ѯ���¼�
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            NewMethod();
        }

        //�޸ı���
        private void bar_btn_save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
            {
                return;
            }
            if (this.ds.HasChanges())
            {
                if (comboBoxEdit1.Text == "") return;
                this.manager.UpdateLoanDetail(ds.GetChanges(), Convert.ToDateTime(comboBoxEdit1.Text));
            }
            try
            {
                this.ds.AcceptChanges();
            }
            catch
            {
                MessageBox.Show(Properties.Resources.SavaFailure, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            NewMethod();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void btnDataCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.comboxDataFrom.Text))
            {
                MessageBox.Show("Ո�x�񔵓�Դ���ڣ�(�҂�����ѡ��)", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //��ȡcomboBox��������
                int year = Int32.Parse(this.comboxDataFrom.Text.Substring(0, 4));
                int month = Int32.Parse(this.comboxDataFrom.Text.Substring(5, 2));

                //������Դ
                DataSet dsCopyFrom = manager.SelestLoanList(year, month);

                foreach (DataRow row in dsCopyFrom.Tables[0].Rows)
                {
                    foreach (DataRow inRow in ds.Tables["LoanListInfo"].Rows)
                    {
                        if (inRow["EmployeeId"].ToString() == row["EmployeeId"].ToString())
                        {
                            inRow["LoanFee"] = row["LoanFee"];
                            break;
                        }
                    }
                }
                //ds = manager.SelestLoanList(year, month);

                //bindingSource1.DataSource = ds.Tables["LoanListInfo"];
                this.Refresh();
                CostTotal();
                if (ds.Tables["LoanListInfo"].Rows.Count > 0)
                    barStaticItem1.Caption = "��" + ds.Tables["LoanListInfo"].Rows.Count + "�";


            }
        }

        private void barBtnCancelCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.NewMethod();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.CostTotal();
        }
    }
}