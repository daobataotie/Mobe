using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.CustomsClearance
{
    public partial class BGProductOutDetail : DevExpress.XtraEditors.XtraForm
    {
        BL.InvoiceXSDetailManager invoiceXSDetailManager = new Book.BL.InvoiceXSDetailManager();
        BL.BomParentPartInfoManager bomParentPartInfoManager = new Book.BL.BomParentPartInfoManager();
        BL.BomComponentInfoManager bomComponentInfoManager = new Book.BL.BomComponentInfoManager();


        public BGProductOutDetail()
        {
            InitializeComponent();

            this.date_Start.EditValue = DateTime.Now.AddMonths(-1);
            this.date_End.EditValue = DateTime.Now;

            IList<string> bgHandbookIds = new BL.BGHandbookManager().SelectAllId();
            foreach (var item in bgHandbookIds)
            {
                this.ccb_bgHandBook.Properties.Items.Add(item);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期区间不完整", this.Text, MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrEmpty(this.ccb_bgHandBook.Text))
            {
                MessageBox.Show("手册不能为空", this.Text, MessageBoxButtons.OK);
                return;
            }
            DateTime startDate = this.date_Start.DateTime;
            DateTime endDate = this.date_End.DateTime;

            string bgHandBookId = "";
            string[] bgHandBookIds = this.ccb_bgHandBook.Text.Split(',');
            foreach (var item in bgHandBookIds)
            {
                bgHandBookId += "'" + item.Trim() + "',";
            }
            bgHandBookId = bgHandBookId.TrimEnd(',');

            string bgProductId = this.txt_BGProductId.Text;
            string productId = (this.btn_Product.EditValue as Model.Product) == null ? null : (this.btn_Product.EditValue as Model.Product).ProductId;
            string cusXOId = this.txt_CusXOId.Text;

            IList<Model.InvoiceXSDetail> xsList = invoiceXSDetailManager.SelectByBGHandBook(startDate, endDate, bgHandBookId, bgProductId, productId, cusXOId);
            if (xsList == null || xsList.Count < 1)
            {
                MessageBox.Show("无数据", this.Text, MessageBoxButtons.OK);
                return;
            }
            Dictionary<Model.InvoiceXSDetail, List<Model.BomComponentInfo>> dic = new Dictionary<Book.Model.InvoiceXSDetail, List<Book.Model.BomComponentInfo>>();

            foreach (var xsDetail in xsList)
            {
                xsDetail.Product = new Book.Model.Product();
                xsDetail.Product.ProductId = xsDetail.ProductId;
                Model.BomParentPartInfo bomParent = this.bomParentPartInfoManager.Get(xsDetail.Product);
                if (bomParent != null)
                {
                    dic.Add(xsDetail, GetBomComponetList(bomParent, xsDetail.InvoiceXSDetailQuantity.Value));
                }
            }

            ExportExcel(dic);
        }

        private void ExportExcel(Dictionary<Book.Model.InvoiceXSDetail, List<Book.Model.BomComponentInfo>> dic)
        {
            Type objClassType = null;
            objClassType = Type.GetTypeFromProgID("Excel.Application");
            if (objClassType == null)
            {
                MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                return;
            }
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];

            #region SetHeader
            sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 12]).RowHeight = 20;
            sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 12]).Font.Size = 15;
            sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 12]).HorizontalAlignment = -4108;
            sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 12]).ColumnWidth = 12;
            sheet.get_Range(excel.Cells[1, 3], excel.Cells[1, 4]).ColumnWidth = 30;
            sheet.get_Range(excel.Cells[1, 10], excel.Cells[1, 10]).ColumnWidth = 30;
            sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 12]).Interior.Color = 12566463;

            sheet.Cells[1, 1] = "手册号";
            sheet.Cells[1, 2] = "成品项号";
            sheet.Cells[1, 3] = "成品编号";
            sheet.Cells[1, 4] = "成品名称";
            sheet.Cells[1, 5] = "规格型号";
            sheet.Cells[1, 6] = "装配单编号";
            sheet.Cells[1, 7] = "客户订单号";
            sheet.Cells[1, 8] = "订单量";
            sheet.Cells[1, 9] = "半成品编号";
            sheet.Cells[1, 10] = "使用半成品名称";
            sheet.Cells[1, 11] = "单位";
            sheet.Cells[1, 12] = "使用半成品数量";

            #endregion

            int row = 2;

            foreach (var item in dic)
            {
                int count = item.Value.Count;
                sheet.get_Range(excel.Cells[row, 1], excel.Cells[row + count - 1, 1]).MergeCells = true;
                sheet.get_Range(excel.Cells[row, 2], excel.Cells[row + count - 1, 2]).MergeCells = true;
                sheet.get_Range(excel.Cells[row, 3], excel.Cells[row + count - 1, 3]).MergeCells = true;
                sheet.get_Range(excel.Cells[row, 4], excel.Cells[row + count - 1, 4]).MergeCells = true;
                sheet.get_Range(excel.Cells[row, 5], excel.Cells[row + count - 1, 5]).MergeCells = true;
                sheet.get_Range(excel.Cells[row, 6], excel.Cells[row + count - 1, 6]).MergeCells = true;
                sheet.get_Range(excel.Cells[row, 7], excel.Cells[row + count - 1, 7]).MergeCells = true;
                sheet.get_Range(excel.Cells[row, 8], excel.Cells[row + count - 1, 8]).MergeCells = true;

                sheet.Cells[row, 1] = item.Key.HandbookId;
                sheet.Cells[row, 2] = item.Key.HandbookProductId;
                sheet.Cells[row, 3] = item.Key.PId;
                sheet.Cells[row, 4] = item.Key.ProductName;
                sheet.Cells[row, 5] = item.Key.CustomerProductName;
                sheet.Cells[row, 6] = item.Key.PronoteHeaderID;
                sheet.Cells[row, 7] = item.Key.CustomerInvoiceXOId;
                sheet.Cells[row, 8] = item.Key.InvoiceXSDetailQuantity;

                foreach (var detail in item.Value)
                {
                    sheet.Cells[row, 9] = detail.Product.Id;
                    sheet.Cells[row, 10] = detail.Product.ProductName;
                    sheet.Cells[row, 11] = detail.Unit;
                    sheet.Cells[row, 12] = detail.UseQuantity;

                    row++;
                }
            }

            excel.Visible = true;
        }

        private List<Model.BomComponentInfo> GetBomComponetList(Model.BomParentPartInfo _bomParmentPartInfo, double outQty)
        {
            List<Model.BomComponentInfo> _comDetailss = new List<Book.Model.BomComponentInfo>();

            foreach (Model.BomComponentInfo bomcon in this.bomComponentInfoManager.Select(_bomParmentPartInfo))   //第一层子件
            { //商品类型为：自制或者委外 才会带出，其他一律不需要
                if (bomcon.Product.IsProcee == false && bomcon.Product.TrustOut == false && (bomcon.Product.HomeMade == true || bomcon.Product.OutSourcing == true))
                {
                    //bomcon.UseQuantity = bomcon.UseQuantity * outQty * (1 + (bomcon.SubLoseRate.HasValue ? bomcon.SubLoseRate.Value * 0.01 : 0));
                    bomcon.UseQuantity = bomcon.UseQuantity * outQty;
                    _comDetailss.Add(bomcon);

                    GetBomComponetByParent(bomcon, _comDetailss);     //第一层以下子件
                }
            }

            return _comDetailss;
        }

        private void GetBomComponetByParent(Model.BomComponentInfo componet, List<Model.BomComponentInfo> _comDetailss)
        {
            Model.BomParentPartInfo _bomparent = bomParentPartInfoManager.Get(componet.Product);
            if (_bomparent != null)
            {
                IList<Model.BomComponentInfo> comList = this.bomComponentInfoManager.Select(_bomparent);
                if (comList != null && comList.Count > 0)
                {
                    foreach (var item in comList)
                    {
                        //商品类型为：自制或者委外 才会带出，其他一律不需要
                        if (item.Product.IsProcee == false && item.Product.TrustOut == false && (item.Product.HomeMade == true || item.Product.OutSourcing == true))
                        {
                            //item.UseQuantity = componet.UseQuantity * item.UseQuantity * (1 + (item.SubLoseRate.HasValue ? item.SubLoseRate.Value * 0.01 : 0));
                            item.UseQuantity = componet.UseQuantity * item.UseQuantity;
                            _comDetailss.Add(item);

                            //递归调用
                            GetBomComponetByParent(item, _comDetailss);
                        }
                    }
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Product_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.btn_Product.EditValue = f.SelectedItem;
            }
        }
    }
}