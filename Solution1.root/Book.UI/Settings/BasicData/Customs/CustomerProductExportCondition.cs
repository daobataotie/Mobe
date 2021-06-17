using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Customs
{
    public partial class CustomerProductExportCondition : DevExpress.XtraEditors.XtraForm
    {
        BL.CustomerProductsManager customerProductsManager = new Book.BL.CustomerProductsManager();
        public CustomerProductExportCondition()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.ncc_StartCustomer.Choose = new BasicData.Customs.ChooseCustoms();
            this.ncc_EndCustomer.Choose = new BasicData.Customs.ChooseCustoms();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string startCustoemrId = ncc_StartCustomer.EditValue == null ? null : (ncc_StartCustomer.EditValue as Model.Customer).Id;
            string endCustomerId = ncc_EndCustomer.EditValue == null ? null : (ncc_EndCustomer.EditValue as Model.Customer).Id;
            DataTable dt = customerProductsManager.SelectByCustomer(startCustoemrId, endCustomerId);
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("提示", "无数据", MessageBoxButtons.OK);
                return;
            }

            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);
                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];

                sheet.Cells.EntireRow.AutoFit();
                sheet.Cells.WrapText = true;  //自动换行
                sheet.Cells.ColumnWidth = 30;
                sheet.get_Range(sheet.Cells[1, 2], sheet.Cells[1, 3]).ColumnWidth = 50;
                sheet.get_Range(sheet.Cells[1, 5], sheet.Cells[1, 5]).ColumnWidth = 10;
                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, 5]).HorizontalAlignment = -4108;
                sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, 5]).Interior.ColorIndex = 15;   //浅灰色

                sheet.Cells[1, 1] = "客户";
                sheet.Cells[1, 2] = "商品编号";
                sheet.Cells[1, 3] = "商品名称";
                sheet.Cells[1, 4] = "客户型号";
                sheet.Cells[1, 5] = "版本";

                //内容，一次性填充，速度快
                object[,] data = new object[dt.Rows.Count, 5];
                int row = 0;
                foreach (DataRow item in dt.Rows)
                {
                    data[row, 0] = item["CustomerFullName"];
                    data[row, 1] = item["Id"];
                    data[row, 2] = item["ProductName"];
                    data[row, 3] = item["CustomerProductId"];
                    data[row, 4] = item["ProductVersion"];

                    row++;
                }

                Microsoft.Office.Interop.Excel.Range r = sheet.get_Range(sheet.Cells[2, 1], sheet.Cells[1 + dt.Rows.Count, 5]);
                r.Value2 = data;

                excel.Visible = true;//是否打开该Excel文件
                excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void ncc_StartCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (this.ncc_StartCustomer.EditValue == null)
                this.ncc_EndCustomer.EditValue = this.ncc_StartCustomer.EditValue;
        }
    }
}