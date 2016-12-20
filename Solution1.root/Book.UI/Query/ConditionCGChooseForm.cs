using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 够波涛             完成时间:2009-4-12
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    public partial class ConditionCGChooseForm : DevExpress.XtraEditors.XtraForm
    {
        IList<Model.InvoiceCG>  detail=new List<Model.InvoiceCG>();

        //窗体构造函数
        public ConditionCGChooseForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// "确定"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //string SupplierStart = this.buttonEditSupplierStart.Text;
            //string SupplierEnd = this.buttonEditSupplierEnd.Text;
            //DateTime dateStart = this.dateEditStart.DateTime;
            //DateTime dateEnd = this.dateEditEnd.DateTime;
           string  SupplierStart= string.IsNullOrEmpty(this.buttonEditSupplierStart.Text) ? "" : this.buttonEditSupplierStart.Text;
          string  SupplierEnd= string.IsNullOrEmpty(this.buttonEditSupplierEnd.Text) ? "zzzzzzzzz" : this.buttonEditSupplierEnd.Text;
           DateTime dateEditStart= this.dateEditStart.DateTime.Year < 1500 ? Convert.ToDateTime("1900-01-01") : this.dateEditStart.DateTime;
          DateTime  dateEditEnd = this.dateEditEnd.DateTime.Year < 1500 ? Convert.ToDateTime("3000-01-01") : this.dateEditEnd.DateTime;
          // string  ProductStart= string.IsNullOrEmpty(this.buttonEditProductStart.Text) ? "" : this.buttonEditProductStart.Text;
          //string   ProductEnd= string.IsNullOrEmpty(this.buttonEditProductEnd.Text) ? "zzzzzzzzzzzzzz" : this.buttonEditProductEnd.Text;
          string productStart = this.buttonEditProductStart.Text;
          string productEnd = this.buttonEditProductEnd.Text;
          detail = new BL.InvoiceCGManager().Select(SupplierStart, SupplierEnd, dateEditStart, dateEditEnd, productStart, productEnd);
            if (detail.Count == 0)
            {
                MessageBox.Show(Properties.Resources.NoRecords); return;
            }
            //foreach (Model.Customer customer in details)
            //{
            //    if (customer.customerCheck == true)
            //        customerDetails.Add(customer);
            //}
            ////gridView1
            ////    if(this.gridView1.)
            ////  this.bindingSource1.DataSource = customerDetails;
            //if (customerDetails.Count == 0)
            //    MessageBox.Show("請選擇客戶");
            Q16 f = new Q16(detail,productStart ,productEnd);

            f.ShowPreviewDialog();
            //ConditionCGSupplierForm f = new ConditionCGSupplierForm(SupplierStart, SupplierEnd, dateStart, dateEnd);              

        }

        /// <summary>
        /// 选择供应商编号开始区间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditSupplierStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseSupplier f = new Book.UI.Invoices.ChooseSupplier();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Supplier supplier = f.SelectedItem as Model.Supplier;
                if (supplier != null)
                {
                    this.buttonEditSupplierStart.Text = supplier.Id;
                }
            }
        }

        /// <summary>
        /// 选择供应商编号结束区间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditSupplierEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseSupplier f = new Book.UI.Invoices.ChooseSupplier();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Supplier supplier = f.SelectedItem as Model.Supplier;
                if (supplier != null)
                {
                    this.buttonEditSupplierEnd.Text = supplier.Id;

                }
            }
        }


        //取消
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        /// <summary>
        /// 商品编号开始区间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditProductEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Product product = f.SelectedItem as Model.Product;
                if (product != null)
                {
                    this.buttonEditProductEnd.Text = product.Id;
                }
            }
        }

        /// <summary>
        /// 商品编号结束区间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditProductStartbuttonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Product product = f.SelectedItem as Model.Product;
                if (product != null)
                {
                    this.buttonEditProductStart.Text = product.Id;
                }
            
            }


        }
    }
}