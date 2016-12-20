using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Query
{
    public partial class Q15JiShiForm : BaseForm
    {
        private BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();
        private BL.StockManager stockManager = new Book.BL.StockManager();
        private BL.DepotPositionManager depotPositionMananger = new BL.DepotPositionManager();
        private BL.DepotManager depotManager = new BL.DepotManager();
        private DataTable dt = new DataTable();
        public Q15JiShiForm()
        {
            InitializeComponent();
            this.bindingSourceDepot.DataSource = (new BL.DepotManager()).Select();
            this.bindingSourceProductCategory.DataSource = (new BL.ProductCategoryManager()).Select();
            this.dateEdit1.DateTime = DateTime.Now.Date;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.dateEdit1.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.DateNotNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime date = this.dateEdit1.DateTime.Date.AddDays(1);
            IList<Model.StockSeach> stockList = new List<Model.StockSeach>();
            dt = this.miscDataManager.SelectByCondition("Q15", this.lookUpEditDepotStar.EditValue == null ? null : this.lookUpEditDepotStar.EditValue.ToString(), lookUpEditDepotPosition.EditValue == null ? null : lookUpEditDepotPosition.EditValue.ToString(), null, this.textProductNameOrId.Text, this.btn_ProductNameStart.Text, this.btn_ProductNameEnd.Text, this.LookUpProductCategoryStart.EditValue == null ? null : this.LookUpProductCategoryStart.EditValue.ToString(), this.lookUpProductCategoryEnd.EditValue == null ? null : this.lookUpProductCategoryEnd.EditValue.ToString(), true);//this.checkEditShowZeroProduct.Checked 改为true，因为即时库存查的是当天库存，当天库存不为0，现在为0的情况就会查不出，故此，不显示为0库存要在查出以后做判断。

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0 || dt.Rows[i]["productid"].ToString() != dt.Rows[i - 1]["productid"].ToString())
                {
                    stockList = this.stockManager.SelectJiShi(dt.Rows[i]["productid"].ToString(), date, DateTime.Now);
                    dt.Rows[i]["yifenpeiquantity"] = this.stockManager.SelectJiShidistributioned(dt.Rows[i]["productid"].ToString(), date, DateTime.Now).ToString("0.####");
                }


                if (stockList != null && stockList.Count > 0)
                {

                    var list = from s in stockList
                               where s.PositionName == dt.Rows[i]["posoid"].ToString() //调拨单为进
                               orderby s.InvoiceDate.Value.Date descending
                               select s;

                    if (list.Where(l => l.InvoiceTypeIndex == 3).Count() > 0)
                    {

                        Model.StockSeach seach = list.Where(l => l.InvoiceTypeIndex == 3)
                            .OrderBy(o => o.InvoiceDate.Value.Date).ThenBy(d => d.InsertTime.Value).FirstOrDefault();


                        list = list.Where(l => l.InvoiceDate.Value.Date <= seach.InvoiceDate.Value.Date && l.InsertTime.Value < seach.InsertTime.Value)
                             .OrderByDescending(o => o.InvoiceDate.Value.Date);

                        dt.Rows[i]["Quantity"] = seach.StockCheckBookQuantity;


                    }




                    if (list != null && list.Count() > 0)
                    {
                        foreach (Model.StockSeach stock in list.ToList<Model.StockSeach>())
                        {

                            if (stock.InvoiceTypeIndex == 0)
                            {
                                dt.Rows[i]["Quantity"] = Convert.ToDouble(dt.Rows[i]["Quantity"].ToString()) + stock.InvoiceQuantity.Value;

                            }
                            if (stock.InvoiceTypeIndex == 1)
                            {
                                dt.Rows[i]["Quantity"] = Convert.ToDouble(dt.Rows[i]["Quantity"].ToString()) - stock.InvoiceQuantity.Value;

                            }
                            if (stock.InvoiceTypeIndex == 2)
                            {
                                dt.Rows[i]["Quantity"] = Convert.ToDouble(dt.Rows[i]["Quantity"].ToString()) - stock.InvoiceQuantity.Value;

                            }

                            //已定未入
                            if (stock.InvoiceType == "採購入庫單")
                            {
                                dt.Rows[i]["OrderOnWayQuantity"] = Convert.ToDouble(dt.Rows[i]["OrderOnWayQuantity"]) + stock.InvoiceQuantity.Value;
                            }
                        }

                        var list1 = from s in stockList
                                    where s.OutPositionName == dt.Rows[i]["posoid"].ToString() //挑拨单为出
                                    orderby s.InvoiceDate.Value.Date descending
                                    select s;

                        foreach (Model.StockSeach stock in list1.ToList<Model.StockSeach>())
                        {

                            if (stock.InvoiceTypeIndex == 2)
                            {
                                dt.Rows[i]["Quantity"] = Convert.ToDouble(dt.Rows[i]["Quantity"].ToString()) + stock.InvoiceQuantity.Value;

                            }

                        }


                    }
                }

            }

            dt.AcceptChanges();
            if (dt.Rows.Count < 1)
                MessageBox.Show("无数据", this.Text);
            dt.DefaultView.Sort = "spid,Quantity asc";
            dt = dt.DefaultView.ToTable();
            DataRow[] dr = dt.Select("Quantity<>'0'");
            if (!this.checkEditShowZeroProduct.Checked && dr.Count()>0)
                dt = dr.CopyToDataTable();
            this.gridControl1.DataSource = dt;
            this.labelControl1.Text = dt.Rows.Count.ToString() + " 项";

            //this.condition.DepotEnd = this.lookUpEditDepotEnd.EditValue == null ? null : this.lookUpEditDepotEnd.EditValue.ToString();
            //this.condition.DepotStart = this.lookUpEditDepotStar.EditValue == null ? null : this.lookUpEditDepotStar.EditValue.ToString();
            //this.condition.ProductNameStart = this.btn_ProductNameStart.Text;
            //this.condition.ProductNameEnd = this.btn_ProductNameEnd.Text;
            //this.condition.ProduceCategoryStart = this.LookUpProductCategoryStart.EditValue == null ? null : this.LookUpProductCategoryStart.EditValue.ToString();
            //this.condition.ProductCategoryEnd = this.lookUpProductCategoryEnd.EditValue == null ? null : this.lookUpProductCategoryEnd.EditValue.ToString();
        }

        private void btn_ProductNameStart_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_ProductNameStart.Text = (f.SelectedItem as Model.Product).ProductName;
        }

        private void btn_ProductNameEnd_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
                this.btn_ProductNameEnd.Text = (f.SelectedItem as Model.Product).ProductName;
        }
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R14(dt);
        }

        private void lookUpEditDepotPosition_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void lookUpEditDepotStar_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEditDepotStar.EditValue != null)
            {
                //if (this.barEditItem3.EditValue.ToString() == "True")
                this.bindingSourceDepotPosition.DataSource = this.depotPositionMananger.Select(new Model.Depot() { DepotId = this.lookUpEditDepotStar.EditValue.ToString() });
                this.lookUpEditDepotPosition.EditValue = null;
            }
        }

        private void btn_MaterialCount_Click(object sender, EventArgs e)
        {
            if (this.dt.Rows.Count < 1)
            {
                MessageBox.Show("无数据", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                Settings.BasicData.Products.ProductConvertMaterial f = new Book.UI.Settings.BasicData.Products.ProductConvertMaterial(dt);
                f.ShowDialog(this);
            }
        }

        private void btn_ProductNameStart_EditValueChanged(object sender, EventArgs e)
        {
            this.btn_ProductNameEnd.EditValue = this.btn_ProductNameStart.EditValue;
        }

        private void LookUpProductCategoryStart_EditValueChanged(object sender, EventArgs e)
        {
            this.lookUpProductCategoryEnd.EditValue = this.LookUpProductCategoryStart.EditValue;
        }
    }
}