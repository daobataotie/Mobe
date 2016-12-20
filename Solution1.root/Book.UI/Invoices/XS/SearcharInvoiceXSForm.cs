using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Invoices.XS
{
    public partial class SearcharInvoiceXSForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.EmployeeManager employeeManager = new BL.EmployeeManager();
        private BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        private BL.InvoiceXODetailManager invoicexoDetailManager = new Book.BL.InvoiceXODetailManager();


        public IList<Model.InvoiceXODetail> detailList;
        private IList<Model.InvoiceXODetail> _key = new List<Model.InvoiceXODetail>();
        public IList<Model.InvoiceXODetail> key
        {
            get { return _key; }
            set { _key = value; }
        }
        int tag=0;

        /// <summary>
        /// 选中的订单
        /// </summary>
        public IList<Model.InvoiceXO> CheckedXos { get; set; }

        public SearcharInvoiceXSForm()
        {

            InitializeComponent();
            this.newChooseCustom.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseXOcustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            IList<Model.Employee> roles = employeeManager.Select(Book.UI.Settings.BasicData.Employees.EmployeeParameters.BUSINESS);
            this.dateEdit1.DateTime = DateTime.Now.Date.AddMonths(-4);
            this.dateEdit2.DateTime = DateTime.Now;
            foreach (Model.Employee role in roles)
            {
                this.cbo_bussiness.Properties.Items.Add(role);
            }
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public SearcharInvoiceXSForm(int i)
            : this()
        { 

        }

        private void spb_search_Click(object sender, EventArgs e)
        {
            Model.Customer customer = newChooseCustom.EditValue as Model.Customer;
            Model.Customer xocustomer = newChooseXOcustomer.EditValue as Model.Customer;
            Model.Employee emp = this.cbo_bussiness.EditValue as Model.Employee;
            IList<Model.InvoiceXO> invoicesXO = invoiceXOManager.SelectByYJRQCustomEmpCusXOId(customer, xocustomer, this.dateEdit1.DateTime, this.dateEdit2.DateTime, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, emp, emp, null, null, this.textEditCustomerXOInvoiceId.Text, null, null, this.ceIsClose.Checked, false, this.ceIsForeigntrade.Checked);
            this.bindingSource1.DataSource = invoicesXO;
            bandDetail();
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            //    Model.InvoiceXO xo = this.bindingSource1.Current as Model.InvoiceXO;
            //    xo.Details = this.invoicexoDetailManager.Select(xo);
            //    CheckedXos.Clear();
            //    CheckedXos.Add(xo);
            //    this.DialogResult = DialogResult.OK;
        }

        private void SearcharInvoiceXSForm_Load(object sender, EventArgs e)
        {
            IList<Model.InvoiceXO> invoicesXO = invoiceXOManager.SelectByYJRQCustomEmpCusXOId(null, null, this.dateEdit1.DateTime, this.dateEdit2.DateTime, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, null, null, null, null, null, null, false, false, false);
            this.bindingSource1.DataSource = invoicesXO;
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            // CheckedXos = (this.bindingSource1.DataSource as List<Model.InvoiceXO>).Where(xo => xo.IsChecked).ToList<Model.InvoiceXO>();
            if (key.Count == 0)
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bandDetail();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoiceXODetail> details = this.bindingSourceDetail.DataSource as IList<Model.InvoiceXODetail>;
            if (details == null || details.Count < 1) return;
            //Model.CustomerProducts detail = details[e.ListSourceRowIndex].PrimaryKey;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "gridColumnCusPro":
                    e.DisplayText = detail.CustomerProductName;
                    break;
                case "gridColumnStock":
                    if (detail.StocksQuantity != null)
                        e.DisplayText = detail.StocksQuantity.Value.ToString("0.####");
                    break;
            }
        }

        private void gridView2_ColumnFilterChanged(object sender, EventArgs e)
        {
            bandDetail();
        }
        private void gridViewDetail_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.Name == "gridColumnCheck")
            {
                Model.InvoiceXODetail detail = this.gridViewDetail.GetRow(e.RowHandle) as Model.InvoiceXODetail;

                if ((bool)e.Value)
                {
                    key.Add(detail);
                    //  MrsDetails.Add(this.mRSdetailsManager.Get(detail.MRSdetailsId));
                }
                if (!(bool)e.Value)
                {
                    for (int i = 0; i < key.Count; i++)
                    {
                        if (key[i].InvoiceXODetailId == detail.InvoiceXODetailId)
                        {
                            key.RemoveAt(i);
                            break;
                        }
                    }
                    //  MrsDetails.Remove(this.mRSdetailsManager.Get(detail.MRSdetailsId));
                }
            }
        }
        private void bandDetail()
        {
            if (this.bindingSource1.Current != null)
            {
                detailList = this.invoicexoDetailManager.Select(this.bindingSource1.Current as Model.InvoiceXO, this.cedetailisclose.Checked);
                if (detailList.Count > 0)
                {
                    int count = 0;
                    foreach (Model.InvoiceXODetail detail in detailList)
                    {
                        if (key.Contains(detail))
                        {
                            detail.Checked = true;
                            count++;
                            continue;
                        }
                    }
                    this.checkEditALL.Checked = false;
                    if (detailList.Count == count)
                        this.checkEditALL.Checked = true;
                }
            }
            else
            {
                detailList = null;
            }
            this.bindingSourceDetail.DataSource = detailList;
        }

        //全选按钮状态改变事件
        //private void checkEditALL_CheckedChanged(object sender, EventArgs e)
        //{
        //if (checkEditALL.Checked == true)
        //{

        //    foreach (Model.InvoiceXODetail detail in detailList)
        //    {
        //        detail.Checked = true;
        //        if (key.Contains(detail))
        //            continue;
        //        key.Add(detail);
        //    }
        //}
        //if (checkEditALL.Checked == false)
        //{

        //    foreach (Model.InvoiceXODetail detail in detailList)
        //    {
        //        detail.Checked = false;
        //        //for (int i = 0; i < key.Count; i++)
        //        //{
        //        //    if (key[i].InvoiceXODetailId == detail.InvoiceXODetailId)
        //        //    {
        //        //        key.RemoveAt(i);
        //        //        break;
        //        //    }
        //        //}
        //        if (key.Contains(detail))
        //            key.Remove(detail);
        //    }
        //}
        //// this.gridViewDetail.UpdateCurrentRow();
        //// this.gridViewDetail.PostEditor();
        //this.gridControl1.RefreshDataSource();
        //}

        private void checkEditALL_Click(object sender, EventArgs e)
        {
            if (checkEditALL.Checked == false)
            {
                foreach (Model.InvoiceXODetail detail in detailList)
                {
                    detail.Checked = true;
                    if (key.Contains(detail))
                        continue;
                    key.Add(detail);
                }
            }
            if (checkEditALL.Checked == true)
            {

                foreach (Model.InvoiceXODetail detail in detailList)
                {
                    detail.Checked = false;

                    if (key.Contains(detail))
                        key.Remove(detail);
                }
            }
            this.gridControl1.RefreshDataSource();
        }
    }
}