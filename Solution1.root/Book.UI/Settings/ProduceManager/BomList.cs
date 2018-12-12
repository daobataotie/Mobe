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
using Microsoft.Office.Interop.Excel;
using System.Reflection;

//物料清单
namespace Book.UI.Settings.ProduceManager
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-11-12
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    public partial class BomList : Settings.BasicData.BaseListForm
    {
        private int flag = 0;
        BL.BomParentPartInfoManager bomParentPartInfoManager = new Book.BL.BomParentPartInfoManager();
        BL.BomComponentInfoManager bomComponentInfoManager = new Book.BL.BomComponentInfoManager();

        public BomList()
        {
            InitializeComponent();
            this.manager = new BL.BomParentPartInfoManager();
        }

        /// <summary>
        /// 1:成品一览
        /// </summary>
        /// <param name="i"></param>
        public BomList(int i)
            : this()
        {
            flag = i;
        }

        protected override void RefreshData()
        {
            if (flag == 1)
            {
                this.bindingSource1.DataSource = (this.manager as BL.BomParentPartInfoManager).SelectNotContentDataSet();
            }
            else
            {
                this.bindingSource1.DataSource = (this.manager as BL.BomParentPartInfoManager).SelectDataSet();
            }

            this.gridView1.OptionsBehavior.Editable = true;
        }

        /// <summary>
        /// 重写父类方法
        /// </summary>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new BomEdit();
        }

        /// <summary>
        /// 重写父类方法 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(BomEdit);
            return (BomEdit)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                //BaseEditForm f1 = this.GetEditForm(new object[] { this.bindingSource1.Current, "view" });

                ////  f1.Show();
                //// 

                //if (f1.ShowDialog() == DialogResult.OK)
                //{
                //    f1.MdiParent = this.MdiParent;
                //    RefreshData();
                //}
                this.DialogResult = DialogResult.OK;
            }

        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.bindingSource1.Current != null)
            //{
            //    DataRow dr = gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            //    if (dr != null)
            //    {
            //        if ((sender as CheckEdit).Checked)
            //            this.BOMIds.Add(dr["BomId"].ToString());
            //        else
            //            this.BOMIds.Remove(dr["BomId"].ToString());

            //        this.gridView1.PostEditor();
            //        this.gridView1.UpdateCurrentRow();
            //    }
            //}
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            this.gridControl1.RefreshDataSource();
        }

        private void bar_ExportSelectProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Type objClassType = null;
            objClassType = Type.GetTypeFromProgID("Excel.Application");
            if (objClassType == null)
            {
                MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                return;
            }

            IList<Model.BomParentPartInfo> parentList = ((System.Windows.Forms.BindingSource)(this.gridControl1.DataSource)).DataSource as IList<Model.BomParentPartInfo>;
            if (parentList != null && parentList.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);
                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];

                #region SetHeader
                sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 9]).RowHeight = 20;
                sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 9]).Font.Size = 15;
                sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 9]).HorizontalAlignment = -4108;
                sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 9]).ColumnWidth = 12;
                sheet.get_Range(excel.Cells[1, 3], excel.Cells[1, 4]).ColumnWidth = 50;

                sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 9]).Interior.Color = 12566463;

                //excel.get_Range(excel.Cells[2, 1], excel.Cells[details.Count + 2, 20]).RowHeight = 20;
                //excel.get_Range(excel.Cells[2, 1], excel.Cells[details.Count + 2, 20]).Font.Size = 13;
                //excel.get_Range(excel.Cells[3, 1], excel.Cells[details.Count + 2, 20]).WrapText = true;
                //excel.get_Range(excel.Cells[3, 1], excel.Cells[details.Count + 2, 20]).EntireRow.AutoFit();

                sheet.Cells[1, 1] = "级别";
                sheet.Cells[1, 2] = "子件编号";
                sheet.Cells[1, 3] = "子件名称";
                sheet.Cells[1, 4] = "客户型号";
                sheet.Cells[1, 5] = "计量单位";
                sheet.Cells[1, 6] = "使用数量";
                sheet.Cells[1, 7] = "损耗率";
                sheet.Cells[1, 8] = "生效日期";
                sheet.Cells[1, 9] = "失效日期";

                #endregion

                int row = 2;

                foreach (var model in parentList)
                {
                    if (model.IsChecked)
                    {
                        try
                        {
                            #region 每个商品一个Sheet
                            //Model.BomParentPartInfo _bomParmentPartInfo = bomParentPartInfoManager.Get(model.BomId);
                            //List<Model.BomComponentInfo> list = GetBomComponetList(_bomParmentPartInfo);

                            //if (sheet.Name != "Sheet1")
                            //{
                            //    excel.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
                            //}
                            //sheet = ((Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[excel.Worksheets.Count]);

                            //#region SetHeader
                            //sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 7]).RowHeight = 20;
                            //sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 7]).Font.Size = 15;
                            //sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 7]).HorizontalAlignment = -4108;
                            //sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 7]).ColumnWidth = 12;
                            //sheet.get_Range(excel.Cells[1, 2], excel.Cells[1, 2]).ColumnWidth = 50;

                            //sheet.get_Range(excel.Cells[1, 1], excel.Cells[1, 7]).Interior.Color = 12566463;

                            ////excel.get_Range(excel.Cells[2, 1], excel.Cells[details.Count + 2, 20]).RowHeight = 20;
                            ////excel.get_Range(excel.Cells[2, 1], excel.Cells[details.Count + 2, 20]).Font.Size = 13;
                            ////excel.get_Range(excel.Cells[3, 1], excel.Cells[details.Count + 2, 20]).WrapText = true;
                            ////excel.get_Range(excel.Cells[3, 1], excel.Cells[details.Count + 2, 20]).EntireRow.AutoFit();

                            //sheet.Cells[1, 1] = "级别";
                            //sheet.Cells[1, 2] = "子件名称";
                            //sheet.Cells[1, 3] = "计量单位";
                            //sheet.Cells[1, 4] = "使用数量";
                            //sheet.Cells[1, 5] = "损耗率";
                            //sheet.Cells[1, 6] = "生效日期";
                            //sheet.Cells[1, 7] = "失效日期";
                            ////sheet.Cells[1, 8] = "备注";

                            //#endregion

                            //try
                            //{
                            //    sheet.Name = list[0].Product.Id;
                            //}
                            //catch
                            //{
                            //    sheet.Name = list[0].Product.Id + "-" + list[0].Product.ProductVersion;
                            //}

                            //int row = 2;
                            //foreach (var item in list)
                            //{
                            //    sheet.Cells[row, 1] = item.Jibie;
                            //    sheet.Cells[row, 2] = item.Product.ProductName;
                            //    sheet.Cells[row, 3] = item.Unit;
                            //    sheet.Cells[row, 4] = item.UseQuantity;
                            //    sheet.Cells[row, 5] = item.SubLoseRate == null ? 0 : item.SubLoseRate;
                            //    sheet.Cells[row, 6] = Convert.ToDateTime(item.EffectsDate).ToString("yyyy-MM-dd");
                            //    sheet.Cells[row, 7] = Convert.ToDateTime(item.ExpiringDate).ToString("yyyy-MM-dd");
                            //    //RichTextBox rt = new RichTextBox();
                            //    //rt.Rtf = item.ProductDesc;
                            //    //rt.SelectAll();
                            //    //sheet.Cells[row, 8] = rt.SelectedText;

                            //    //不同级别不同颜色
                            //    switch (item.Jibie)
                            //    {
                            //        case 1:
                            //            sheet.get_Range(sheet.Cells[row, 1], sheet.Cells[row, 7]).Interior.Color = 13311;     //红
                            //            break;
                            //        case 2:
                            //            sheet.get_Range(sheet.Cells[row, 1], sheet.Cells[row, 7]).Interior.Color = 6750105;   //浅绿
                            //            break;
                            //        case 3:
                            //            sheet.get_Range(sheet.Cells[row, 1], sheet.Cells[row, 7]).Interior.Color = 16763955;  //浅蓝
                            //            break;
                            //        case 4:
                            //            sheet.get_Range(sheet.Cells[row, 1], sheet.Cells[row, 7]).Interior.Color = 6750207;   //浅黄
                            //            break;
                            //        case 5:
                            //            sheet.get_Range(sheet.Cells[row, 1], sheet.Cells[row, 7]).Interior.Color = 16711935;  //浅紫
                            //            break;
                            //        case 6:
                            //            sheet.get_Range(sheet.Cells[row, 1], sheet.Cells[row, 7]).Interior.Color = 12566463;  //浅灰
                            //            break;
                            //    }

                            //    row++;
                            //} 
                            #endregion

                            #region 所有商品放一个Sheet

                            Model.BomParentPartInfo _bomParmentPartInfo = bomParentPartInfoManager.Get(model.BomId);
                            List<Model.BomComponentInfo> list = GetBomComponetList(_bomParmentPartInfo);

                            foreach (var item in list)
                            {
                                sheet.Cells[row, 1] = item.Jibie;
                                sheet.Cells[row, 2] = item.Product.Id;
                                sheet.Cells[row, 3] = item.Product.ProductName;
                                sheet.Cells[row, 4] = item.Product.CustomerProductName;
                                sheet.Cells[row, 5] = item.Unit;
                                sheet.Cells[row, 6] = item.UseQuantity;
                                sheet.Cells[row, 7] = item.SubLoseRate == null ? 0 : item.SubLoseRate;
                                sheet.Cells[row, 8] = Convert.ToDateTime(item.EffectsDate).ToString("yyyy-MM-dd");
                                sheet.Cells[row, 9] = Convert.ToDateTime(item.ExpiringDate).ToString("yyyy-MM-dd");

                                if(item.Jibie==0)
                                    sheet.get_Range(sheet.Cells[row, 1], sheet.Cells[row, 9]).Interior.Color = 13311;     //红

                                row++;
                            }
                            row++;
                            row++;

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }

                excel.Visible = true;
            }
        }

        private List<Model.BomComponentInfo> GetBomComponetList(Model.BomParentPartInfo _bomParmentPartInfo)
        {
            List<Model.BomComponentInfo> _comDetailss = new List<Book.Model.BomComponentInfo>();

            _comDetailss.Clear();

            Model.BomComponentInfo comm = new Model.BomComponentInfo();
            comm.Jibie = 0;
            comm.UseQuantity = 1;
            comm.Product = _bomParmentPartInfo.Product;


            comm.Product.ProductName = string.IsNullOrEmpty(_bomParmentPartInfo.Product.CustomerProductName) ? _bomParmentPartInfo.Product.ProductName : _bomParmentPartInfo.Product.ProductName + "{" + _bomParmentPartInfo.Product.CustomerProductName + "}";
            comm.ProductId = _bomParmentPartInfo.ProductId;
            comm.Customer = _bomParmentPartInfo.Customer;

            _comDetailss.Add(comm);
            foreach (Model.BomComponentInfo bomcon in this.bomComponentInfoManager.Select(_bomParmentPartInfo))
            {
                bomcon.Jibie = 1;
                bomcon.Product.ProductName = " ".PadLeft(bomcon.Jibie * 2, ' ') + (string.IsNullOrEmpty(bomcon.Product.CustomerProductName) ? bomcon.Product.ProductName : bomcon.Product.ProductName + "{" + bomcon.Product.CustomerProductName + "}");

                _comDetailss.Add(bomcon);

                GetBomComponetByParent(bomcon, _comDetailss);
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
                        item.Jibie = componet.Jibie + 1;
                        item.Product.ProductName = " ".PadLeft(item.Jibie * 2, ' ') + (string.IsNullOrEmpty(item.Product.CustomerProductName) ? item.Product.ProductName : item.Product.ProductName + "{" + item.Product.CustomerProductName + "}");

                        _comDetailss.Add(item);

                        //递归调用
                        GetBomComponetByParent(item, _comDetailss);
                    }
                }
            }
        }
    }
}