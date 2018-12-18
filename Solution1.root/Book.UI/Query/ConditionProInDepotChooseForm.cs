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

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究

    // 编 码 人: 裴盾              完成时间:2009-5-9
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/
    public partial class ConditionProInDepotChooseForm : ConditionChooseForm
    {
        //Q51  生產領料單
        private ConditionProInDepotChoose condition;
        BL.PronoteHeaderManager pronoteManager = new BL.PronoteHeaderManager();
        BL.ProduceInDepotManager produceInDepotManager = new Book.BL.ProduceInDepotManager();

        public ConditionProInDepotChooseForm()
        {
            InitializeComponent();
            this.newChooseWorkHouse.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.newChooseContorlDepot.Choose = new Book.UI.Invoices.ChooseDepot();
            this.newChooseCustomer1.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseCustomer2.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.dateEdit1.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEdit2.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            IList<string> listKeyWord = new BL.ProductClassifyManager().SelectAllKeyWord();
            foreach (var item in listKeyWord)
            {
                this.checkedComboBoxEdit1.Properties.Items.Add(item);
            }


            IList<string> bgHandbookIds = new BL.BGHandbookManager().SelectAllId();
            foreach (var item in bgHandbookIds)
            {
                this.ccb_BGHandBookIds.Properties.Items.Add(item);
            }
        }

        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionProInDepotChoose;
            }
        }

        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionProInDepotChoose();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit1.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.dateEdit1.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit2.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.dateEdit2.DateTime;
            }
            this.condition.Product = this.buttonEditPro.EditValue as Model.Product;
            this.condition.StartPronoteHeader = this.buttonEditPronote1.Text;
            this.condition.EndPronoteHeader = this.buttonEditPronote2.Text;
            this.condition.WorkHouse = this.newChooseWorkHouse.EditValue as Model.WorkHouse;
            this.condition.MDepot = this.newChooseContorlDepot.EditValue as Model.Depot;
            this.condition.MDepotPosition = this.newChooseContorlDepotPosition.EditValue as Model.DepotPosition;
            this.condition.Id1 = this.buttonEditId1.EditValue == null ? null : this.buttonEditId1.Text;
            this.condition.Id2 = this.buttonEditid2.EditValue == null ? null : this.buttonEditid2.Text;
            this.condition.Cusxoid = this.textEditCusXOId.EditValue == null ? null : this.textEditCusXOId.Text; ;
            this.condition.Customer1 = this.newChooseCustomer1.EditValue as Model.Customer;
            this.condition.Customer2 = this.newChooseCustomer2.EditValue as Model.Customer;
            this.condition.ProductState = this.comBoxProductState.SelectedIndex;

            if (!string.IsNullOrEmpty(this.ccb_BGHandBookIds.Text))
            {
                string bgHandBookId = "";
                string[] bgHandBookIds = this.ccb_BGHandBookIds.Text.Split(',');
                foreach (var item in bgHandBookIds)
                {
                    bgHandBookId += "'" + item.Trim() + "',";
                }
                bgHandBookId = bgHandBookId.TrimEnd(',');
                this.condition.HandBookId = bgHandBookId;
            }
        }

        private void ConditionProInDepotChooseForm_Load(object sender, EventArgs e)
        {
            this.labelCusPro.Enabled = false;
        }

        private void buttonEditPro_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
                this.labelCusPro.Text = (f.SelectedItem as Model.Product).CustomerProductName;
            }
            f.Dispose();
            GC.Collect();
        }

        private void buttonEditPronote1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(1);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPronote1.EditValue = f.SelectItem;
            }
            f.Dispose();
            GC.Collect();
        }

        private void buttonEditPronote2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderDetailsForm(1);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditPronote2.EditValue = f.SelectItem;
            }
            f.Dispose();
            GC.Collect();
        }

        private void newChooseContorlDepot_EditValueChanged(object sender, EventArgs e)
        {
            Model.Depot depot = this.newChooseContorlDepot.EditValue as Model.Depot;
            if (depot != null)
            {
                this.newChooseContorlDepotPosition.Choose = new Book.UI.Invoices.ChooseDepotPosition(depot);
                this.newChooseContorlDepotPosition.EditValue = null;
            }
            else
            {
                this.newChooseContorlDepotPosition.Choose = null;
                this.newChooseContorlDepotPosition.EditValue = null;
            }
        }

        private void buttonEditId1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceInDepot.SelectInDepotForm f = new Book.UI.produceManager.ProduceInDepot.SelectInDepotForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditId1.EditValue = f.SelectItem == null ? null : f.SelectItem.ProduceInDepotId;
                this.buttonEditid2.EditValue = f.SelectItem == null ? null : f.SelectItem.ProduceInDepotId;
            }


        }

        private void buttonEditid2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            produceManager.ProduceInDepot.SelectInDepotForm f = new Book.UI.produceManager.ProduceInDepot.SelectInDepotForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditid2.EditValue = f.SelectItem == null ? null : f.SelectItem.ProduceInDepotId;
            }

        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            if (this.dateEdit1.EditValue == null || this.dateEdit2.EditValue == null)
            {
                MessageBox.Show("请选择日期区间", "提示", MessageBoxButtons.OK);
                return;
            }
            if (this.newChooseWorkHouse.EditValue == null)
            {
                MessageBox.Show("请选择部门", "提示", MessageBoxButtons.OK);
                return;
            }
            string selectedItem = this.checkedComboBoxEdit1.Text;
            if (string.IsNullOrEmpty(selectedItem))
            {
                MessageBox.Show("请选择商品关键字", "提示", MessageBoxButtons.OK);
                return;
            }
            string[] tems = selectedItem.Split(',');
            List<Model.ProduceInDepot> listProduceInDepot = new List<Book.Model.ProduceInDepot>();
            List<HelpExcel> listHelpExcel = new List<HelpExcel>();
            foreach (var keyword in tems)
            {
                if (!string.IsNullOrEmpty(keyword.Trim()))
                {
                    var list = this.produceInDepotManager.SelectExcel(this.dateEdit1.DateTime, this.dateEdit2.DateTime, (this.newChooseWorkHouse.EditValue as Model.WorkHouse).WorkHouseId, keyword);
                    if (list != null && list.Count > 0)
                    {
                        HelpExcel he = new HelpExcel();
                        he.ProceduresSum = list.Sum(P => P.ProceduresSum);
                        he.CheckOutSum = list.Sum(P => P.CheckOutSum);
                        he.ProduceTransferQuantity = list.Sum(P => P.ProduceTransferQuantity);
                        he.ProduceQuantity = list.Sum(P => P.ProduceQuantity);
                        he.ProductName = keyword;
                        listHelpExcel.Add(he);

                        listProduceInDepot.AddRange(list);
                    }
                }
            }
            if (listProduceInDepot.Count == 0)
            {
                MessageBox.Show("无数据", "提示", MessageBoxButtons.OK);
                return;
            }

            #region Generate Excel

            Type objClassType = null;
            objClassType = Type.GetTypeFromProgID("Excel.Application");
            if (objClassType == null)
            {
                MessageBox.Show("本機沒有安裝Excel", "提示！", MessageBoxButtons.OK);
                return;
            }

            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);
                excel.Cells.ColumnWidth = 12;
                excel.Rows.RowHeight = 20;

                #region 表頭
                //excel.get_Range(excel.Cells[1, 1], excel.Cells[1 + dt.Rows.Count, 10]).Borders.LineStyle = XlLineStyle.xlContinuous;
                //excel.get_Range(excel.Cells[1, 1], excel.Cells[1 + dt.Rows.Count, 10]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                excel.get_Range(excel.Cells[1, 3], excel.Cells[1, 3]).ColumnWidth = 40;
                excel.Cells[1, 1] = "部門";
                excel.Cells[1, 2] = "日期";
                excel.Cells[1, 3] = "商品名稱";
                excel.Cells[1, 4] = "生產數量";
                excel.Cells[1, 5] = "良品數量";
                excel.Cells[1, 6] = "轉生產數量";
                excel.Cells[1, 7] = "入庫數量";
                #endregion

                string workHouseName = (this.newChooseWorkHouse.EditValue as Model.WorkHouse).Workhousename;
                for (int i = 0; i < listHelpExcel.Count; i++)
                {
                    excel.Cells[i + 2, 1] = workHouseName;
                    //excel.Cells[i + 2, 2] = "";
                    excel.Cells[i + 2, 3] = listHelpExcel[i].ProductName;
                    excel.Cells[i + 2, 4] = listHelpExcel[i].ProceduresSum;
                    excel.Cells[i + 2, 5] = listHelpExcel[i].CheckOutSum;
                    excel.Cells[i + 2, 6] = listHelpExcel[i].ProduceTransferQuantity;
                    excel.Cells[i + 2, 7] = listHelpExcel[i].ProduceQuantity;

                }

                excel.Cells[listHelpExcel.Count + 7, 1] = "详细数据：";
                for (int i = 0; i < listProduceInDepot.Count; i++)
                {
                    excel.Cells[listHelpExcel.Count + i + 8, 1] = workHouseName;
                    excel.Cells[listHelpExcel.Count + i + 8, 2] = listProduceInDepot[i].ProduceInDepotDate.HasValue ? listProduceInDepot[i].ProduceInDepotDate.Value.ToString("yyyy-MM-dd") : "";
                    excel.Cells[listHelpExcel.Count + i + 8, 3] = listProduceInDepot[i].ProductName;
                    excel.Cells[listHelpExcel.Count + i + 8, 4] = listProduceInDepot[i].ProceduresSum;
                    excel.Cells[listHelpExcel.Count + i + 8, 5] = listProduceInDepot[i].CheckOutSum;
                    excel.Cells[listHelpExcel.Count + i + 8, 6] = listProduceInDepot[i].ProduceTransferQuantity;
                    excel.Cells[listHelpExcel.Count + i + 8, 7] = listProduceInDepot[i].ProduceQuantity;
                }

                excel.Visible = true;
            }
            catch
            {
                MessageBox.Show("Excel未生成完畢，請勿操作，并重新點擊按鈕生成數據！", "提示！", MessageBoxButtons.OK);
                return;
            }

            #endregion
        }

        private void newChooseCustomer1_EditValueChanged(object sender, EventArgs e)
        {
            this.newChooseCustomer2.EditValue = this.newChooseCustomer1.EditValue;
        }
    }

    internal class HelpExcel
    {
        public string Workhousename { get; set; }

        public string ProductName { get; set; }

        public double ProceduresSum { get; set; }

        public double CheckOutSum { get; set; }

        public double ProduceTransferQuantity { get; set; }

        public double ProduceQuantity { get; set; }
    }
}