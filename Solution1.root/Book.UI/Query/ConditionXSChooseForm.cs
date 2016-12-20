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

// 编 码 人:  够波涛             完成时间:2009-5-18
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionXSChooseForm : DevExpress.XtraEditors.XtraForm
    {
        public ConditionXSChooseForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ‘确定’
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            string customerStart = string.IsNullOrEmpty(this.buttonEditCustomerStart.Text) ? "" : this.buttonEditCustomerStart.Text;


            string customerEnd = string.IsNullOrEmpty(this.buttonEditCustomerEnd.Text) ? "zzzzzzzzz" : this.buttonEditCustomerEnd.Text;
            

            DateTime dateStart =this.dateEditStart.DateTime.Year < 1500 ? Convert.ToDateTime("1900-01-01") : this.dateEditStart.DateTime;
            DateTime dateEnd = this.dateEditEnd.DateTime.Year < 1500 ? Convert.ToDateTime("3000-01-01") : this.dateEditEnd.DateTime;
            string productStart = this.buttonEditProductStart.Text;
            string productEnd = this.buttonEditProductEnd.Text;



          //  ConditionXSProductForm f = new ConditionXSProductForm( customerStart,  customerEnd,  productStart,  productEnd,  dateStart,  dateEnd);

           IList<Model.InvoiceXS> xsdetail=  new BL.InvoiceXSManager().Select( customerStart,  customerEnd, productStart, productEnd, dateStart, dateEnd);

           if (xsdetail.Count == 0)
            {
                MessageBox.Show("在此區間無進貨記錄"); return;
            }
           Q20 f = new Q20(xsdetail, productStart, productEnd);
            //if (f.ShowDialog() != DialogResult.OK) return;
           f.ShowPreviewDialog();

        }

        /// <summary>
        /// 选择客户编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditCustomerEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseCustoms f = new Book.UI.Invoices.ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Customer customer = f.SelectedItem as Model.Customer;
                if (customer != null)
                {
                    this.buttonEditCustomerEnd.Text = customer.Id;

                }
            }

        }


        /// <summary>
        /// 货品编号开始区间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditProductStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string customerStart = string.IsNullOrEmpty(this.buttonEditCustomerStart.Text) ? "" : this.buttonEditCustomerStart.Text;
            string customerEnd = string.IsNullOrEmpty(this.buttonEditCustomerEnd.Text) ? "zzzzzzz" : this.buttonEditCustomerEnd.Text;

            Settings.BasicData.Customs.ChooseCustomerProductForm f = new Settings.BasicData.Customs.ChooseCustomerProductForm(customerStart, customerEnd);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.CustomerProducts product = f.SelectedItem as Model.CustomerProducts;
                if (product != null)
                {
                    this.buttonEditProductStart.Text = product.CustomerProductId;
                }
            }

        }

        /// <summary>
        /// 货品编号结束区间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditProductEnd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string customerStart = string.IsNullOrEmpty(this.buttonEditCustomerStart.Text) ? "" : this.buttonEditCustomerStart.Text;
            string customerEnd = string.IsNullOrEmpty(this.buttonEditCustomerEnd.Text) ? "C999999" : this.buttonEditCustomerEnd.Text;
            Settings.BasicData.Customs.ChooseCustomerProductForm f = new Settings.BasicData.Customs.ChooseCustomerProductForm(customerStart, customerEnd);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.CustomerProducts product = f.SelectedItem as Model.CustomerProducts;
                if (product != null)
                {
                    this.buttonEditProductEnd.Text = product.CustomerProductId;
                }
            }

        }

        /// <summary>
        /// 客户编号开始区间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditCustomerStart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Invoices.ChooseCustoms  f = new Book.UI.Invoices.ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Customer customer = f.SelectedItem as Model.Customer;
                if (customer != null)
                {
                    this.buttonEditCustomerStart.Text = customer.Id;
                  
                }
            }

        }


        //取消
        private void simpleButton3_Click(object sender, EventArgs e)
        {


            this.Close();
            //string customerStart = this.buttonEditCustomerStart.Text;
            //string customerEnd = this.buttonEditCustomerEnd.Text;      
            

            //  DateTime dateStart =  this.dateEditStart.DateTime;
            //  DateTime dateEnd =this.dateEditEnd.DateTime;
            //ConditionXSCustomerForm f = new ConditionXSCustomerForm(customerStart, customerEnd, dateStart, dateEnd);
            //if (f.ShowDialog(this) != DialogResult.OK) return;
        }
        //protected override void OnOK()
        //{
        //    //if (this.condition == null)
        //    //    this.condition = new ConditionH();
        //    //this.condition.Employee = this.buttonEditEmployee.EditValue as Model.Employee;
        //    //this.condition.EndDate = this.dateEditEndDate.DateTime;
        //    //this.condition.StartDate = this.dateEditStartDate.DateTime;
        //}

        //public override Condition Condition
        //{
        //    //get
        //    //{
        //    //    return this.condition;
        //    //}
        //    //set
        //    //{
        //    //    this.condition = value as ConditionH;
        //    //}
        //}

    }
}