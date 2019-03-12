using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace Book.UI.produceManager.ProduceInDepot
{
    public partial class CountMaterialForm : DevExpress.XtraEditors.XtraForm
    {
        BL.ProduceInDepotDetailManager detailManager = new Book.BL.ProduceInDepotDetailManager();
        BL.BomParentPartInfoManager bomParentManager = new Book.BL.BomParentPartInfoManager();
        BL.BomComponentInfoManager bomComponentManager = new Book.BL.BomComponentInfoManager();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.MaterialManager materialManager = new Book.BL.MaterialManager();

        IList<Model.Product> list = new List<Model.Product>();
        List<string> Header = new List<string>();
        Dictionary<string, string> dicProduct = new Dictionary<string, string>();

        public CountMaterialForm()
        {
            InitializeComponent();

            dicProduct = productManager.SelectProductIdAndName().ToDictionary(P1 => P1.ProductId, P2 => P2.ProductName);
            IList<string> handBookIds = new BL.BGHandbookManager().SelectAllId();
            foreach (var item in handBookIds)
            {
                this.cob_HandBookId.Properties.Items.Add(item);
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (this.date_Start.EditValue == null || this.date_End.EditValue == null)
            {
                MessageBox.Show("日期区间不完整", "提示", MessageBoxButtons.OK);
                return;
            }
            DateTime dateStart = date_Start.DateTime.Date;
            DateTime dateEnd = date_End.DateTime.Date.AddDays(1).AddSeconds(-1);
            string handBookId = this.cob_HandBookId.Text;

            list = detailManager.SelectAllByDateRange(dateStart, dateEnd, handBookId);

            IList<Model.ProduceInDepotDetail> listShechu = detailManager.SelectShechuByDateRange(dateStart, dateEnd, handBookId);
            IList<Model.ProduceInDepotDetail> listYanpian = detailManager.SelectQianghuaByDateRange(dateStart, dateEnd, handBookId);
            //2018年3月8日00:42:22 验片生产数量改为强化防雾生产数量
            //IList<Model.ProduceInDepotDetail> listYanpian = detailManager.SelectYanpianByDateRange(dateStart, dateEnd);

            foreach (var item in list)
            {
                var shechu = listShechu.FirstOrDefault(L => L.ProductId == item.ProductId && L.HandbookId == item.HandbookId);
                var yanpian = listYanpian.FirstOrDefault(L => L.ProductId == item.ProductId && L.HandbookId == item.HandbookId);

                if (shechu != null)
                    item.ShechuHege = shechu.CheckOutSum.HasValue ? shechu.CheckOutSum.Value : 0;
                if (yanpian != null)
                    item.YanpianHege = yanpian.CheckOutSum.HasValue ? yanpian.CheckOutSum.Value : 0;
            }

            this.bindingSource1.DataSource = list;
        }

        private void btn_CountMaterial_Click(object sender, EventArgs e)
        {
            if (list == null || list.Count == 0)
            {
                MessageBox.Show("请先进行查询", "提示", MessageBoxButtons.OK);
                return;
            }

            Header.Clear();

            foreach (var item in list)
            {
                Dictionary<string, double> dic = new Dictionary<string, double> { { item.ProductId, 1 } };
                CountMaterialMaozhong(item.ProductId, dic);
                item.ZijianDic = new Dictionary<string, string>();

                //毛重
                foreach (var d in dic)
                {
                    item.ZijianDic.Add(dicProduct[d.Key], (d.Value * item.ShengChan).ToString("0.####"));

                    if (!Header.Contains(dicProduct[d.Key]))
                        Header.Add(dicProduct[d.Key]);
                }
            }

            //净重
            CountMaterialJingzhong();

            ExportExcel();
        }

        private void CountMaterialMaozhong(string productId, Dictionary<string, double> temp)
        {
            string bomId = bomParentManager.SelectBomIdByProductId(productId);
            if (!string.IsNullOrEmpty(bomId))
            {
                IList<Model.BomComponentInfo> listComponent = bomComponentManager.SelectProductIdAndUseQty(bomId);
                if (listComponent != null && listComponent.Count > 0)
                {
                    foreach (var item in listComponent)
                    {
                        if (!temp.Keys.Contains(item.ProductId))
                            temp.Add(item.ProductId, Convert.ToDouble(item.UseQuantity) * temp[productId]);
                        else
                            temp[item.ProductId] = temp[item.ProductId] + Convert.ToDouble(item.UseQuantity) * temp[productId];


                        CountMaterialMaozhong(item.ProductId, temp);
                    }

                    temp.Remove(productId);
                }
            }
        }

        private void CountMaterialJingzhong()
        {
            IList<string> str = materialManager.SelectMaterialCategory();
            Dictionary<string, string> dic = new Dictionary<string, string>();


            foreach (var pro in list)
            {
                pro.MaterialDic = new Dictionary<string, string>();
                foreach (var item in str)
                {
                    pro.MaterialDic.Add(item, "0");
                }


                if (!string.IsNullOrEmpty(pro.MaterialIds))
                {
                    string[] materialIds = pro.MaterialIds.Split(',');
                    string[] materialnums = pro.MaterialNum.Split(',');

                    for (int i = 0; i < materialIds.Length; i++)
                    {
                        Model.Material model = materialManager.Get(materialIds[i]);
                        if (model != null)
                        {
                            double value = Convert.ToDouble(materialnums[i]) * Convert.ToDouble(model.JWeight) * pro.TotalHege;

                            if (!pro.MaterialDic.Keys.Contains(model.MaterialCategoryName))
                            {
                                model.MaterialCategoryName = model.MaterialCategoryName.ToLower();
                            }
                            pro.MaterialDic[model.MaterialCategoryName] = (Convert.ToDouble(pro.MaterialDic[model.MaterialCategoryName]) + (value / 1000)).ToString("0.####");  //换算KG
                        }
                    }
                }
            }
        }

        private void ExportExcel()
        {
            try
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

                Microsoft.Office.Interop.Excel.Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 8]);
                r.MergeCells = true;//合并单元格

                excel.Cells.ColumnWidth = 10;
                excel.Cells[1, 1] = "生产所用原料毛重/净重(" + this.date_Start.DateTime.Date.ToString("yyyy-MM-dd") + " ~ " + this.date_End.DateTime.Date.ToString("yyyy-MM-dd") + ")";
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).RowHeight = 25;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, 1]).Font.Size = 20;
                //excel.Cells[1, productShipmentList.Count + 1] = DateTime.Now.ToString("yyyy.MM.dd");
                excel.get_Range(excel.Cells[1, 8], excel.Cells[1, 8]).HorizontalAlignment = -4108;

                excel.Cells[2, 1] = "商品编号";
                excel.Cells[2, 2] = "商品名称";
                excel.Cells[2, 3] = "手册号";
                excel.Cells[2, 4] = "生产数量";
                excel.Cells[2, 5] = "射出合格";
                excel.Cells[2, 6] = "片生产数量";
                excel.Cells[2, 7] = "总合格";
                excel.Cells[2, 9] = "毛重";
                excel.Cells[2, 9 + Header.Count + 2] = "净重";
                excel.get_Range(excel.Cells[2, 1], excel.Cells[2, 9 + Header.Count + 2 + list[0].MaterialDic.Keys.Count]).Interior.Color = "12566463";
                excel.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).ColumnWidth = 50;


                int col = 10;
                //毛重
                foreach (var item in Header)
                {
                    excel.Cells[2, col] = item;

                    SetExcelColumnWidth(excel, col, item);

                    col++;
                }

                col += 2;

                //净重
                foreach (var item in list[0].MaterialDic)
                {
                    excel.Cells[2, col] = item.Key;

                    SetExcelColumnWidth(excel, col, item.Key);

                    col++;
                }

                List<Model.Product> haveThreeCategoryPro = list.Where(P => !string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();
                List<Model.Product> haveTwoCategoryPro = list.Where(P => !string.IsNullOrEmpty(P.ProductCategoryName2) && string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();
                List<Model.Product> haveOneCategoryPro = list.Where(P => string.IsNullOrEmpty(P.ProductCategoryName2) && string.IsNullOrEmpty(P.ProductCategoryName3)).ToList();

                int row = 3;

                foreach (var item in haveThreeCategoryPro.GroupBy(P => P.ProductCategoryName3))
                {
                    SetExcelFormat(excel, ref col, ref row, item);
                }

                foreach (var item in haveTwoCategoryPro.GroupBy(P => P.ProductCategoryName2))
                {
                    SetExcelFormat(excel, ref col, ref row, item);
                }

                foreach (var item in haveOneCategoryPro.GroupBy(P => P.ProductCategoryName))
                {
                    SetExcelFormat(excel, ref col, ref row, item);
                }

                excel.Visible = true;//是否打开该Excel文件
                excel.WindowState = XlWindowState.xlMaximized;
            }
            catch
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }
        }

        private static void SetExcelColumnWidth(Microsoft.Office.Interop.Excel.Application excel, int col, string item)
        {
            if (item.Length > 20)
                excel.get_Range(excel.Cells[2, col], excel.Cells[2, col]).ColumnWidth = 40;
            else if (item.Length > 15)
                excel.get_Range(excel.Cells[2, col], excel.Cells[2, col]).ColumnWidth = 30;
            else if (item.Length > 10)
                excel.get_Range(excel.Cells[2, col], excel.Cells[2, col]).ColumnWidth = 20;

        }

        private void SetExcelFormat(Microsoft.Office.Interop.Excel.Application excel, ref int col, ref int row, IGrouping<string, Book.Model.Product> item)
        {
            excel.get_Range(excel.Cells[row, 1], excel.Cells[row, 9 + Header.Count + 2 + list[0].MaterialDic.Keys.Count]).Interior.Color = "6750207";    //红色 255; 浅黄 6750207； 浅浅黄 10092543
            excel.Cells[row, 2] = item.Key;
            excel.get_Range(excel.Cells[row, 4], excel.Cells[row, 4]).Formula = string.Format("=SUM(D{0}:D{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 5], excel.Cells[row, 5]).Formula = string.Format("=SUM(E{0}:E{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 6], excel.Cells[row, 6]).Formula = string.Format("=SUM(F{0}:F{1})", row + 1, row + item.Count());
            excel.get_Range(excel.Cells[row, 7], excel.Cells[row, 7]).Formula = string.Format("=SUM(G{0}:G{1})", row + 1, row + item.Count());

            col = 10;

            foreach (var h in Header)
            {
                string excelColumnName = CountExcelColumnName(col);
                excel.get_Range(excel.Cells[row, col], excel.Cells[row, col]).Formula = string.Format("=SUM({2}{0}:{2}{1})", row + 1, row + item.Count(), excelColumnName);
                col++;
            }

            col += 2;

            foreach (var ec in list[0].MaterialDic)
            {
                string excelColumnName = CountExcelColumnName(col);
                excel.get_Range(excel.Cells[row, col], excel.Cells[row, col]).Formula = string.Format("=SUM({2}{0}:{2}{1})", row + 1, row + item.Count(), excelColumnName);
                col++;
            }

            row++;

            foreach (var pro in item)
            {
                excel.Cells[row, 1] = pro.Id;
                excel.Cells[row, 2] = pro.ProductName;
                excel.Cells[row, 3] = pro.HandbookId;
                excel.Cells[row, 4] = pro.ShengChan;
                excel.Cells[row, 5] = pro.ShechuHege;
                excel.Cells[row, 6] = pro.YanpianHege;
                excel.Cells[row, 7] = pro.TotalHege;

                col = 10;
                //毛重
                foreach (var h in Header)
                {
                    if (pro.ZijianDic.Keys.Contains(h))
                        excel.Cells[row, col++] = pro.ZijianDic[h];
                    else
                        excel.Cells[row, col++] = 0;
                }

                col += 2;

                //净重
                foreach (var dic in pro.MaterialDic)
                {
                    excel.Cells[row, col++] = dic.Value;
                }

                row++;
            }
            row++;
        }

        private string CountExcelColumnName(int i)
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (i <= 26)
                return str.ToCharArray()[i - 1].ToString();
            else
            {
                int count = (int)Math.Floor(Convert.ToDecimal(i / 26));
                if (i % 26 == 0)
                {
                    return str.ToCharArray()[count - 2].ToString() + "Z";
                }
                else
                {
                    return str.ToCharArray()[count - 1].ToString() + str.ToCharArray()[i % 26 - 1].ToString();
                }
            }
        }
    }
}