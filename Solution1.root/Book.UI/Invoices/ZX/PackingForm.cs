using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Book.UI.Invoices.ZX
{
    public partial class PackingForm : DevExpress.XtraEditors.XtraForm
    {
        Model.InvoiceZX invoicezx;
        IList<Model.InvoiceZX> LInvoiceZXList = new List<Model.InvoiceZX>();
        IList<Model.InvoiceZX> RInvoiceZXList = new List<Model.InvoiceZX>();
        BL.InvoiceZXManager invoicezxmenager = new Book.BL.InvoiceZXManager();
        IList<Model.InvoiceZX> saveinvoicezxlist = new List<Model.InvoiceZX>();
        protected IDictionary<string, AA> requireValueExceptions = new Dictionary<string, AA>();
        protected IDictionary<string, AA> invalidValueExceptions = new Dictionary<string, AA>();
        public IList<Model.InvoiceZX> checkedInvoicezx = new List<Model.InvoiceZX>();
        int HasPackingNum = 0;

        public PackingForm()
        {
            InitializeComponent();

            this.ncCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.ncXOCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.dateStart.DateTime = DateTime.Now.Date.AddDays(-15);
            this.dateEnd.DateTime = DateTime.Now;

            this.requireValueExceptions.Add(Model.InvoiceZX.PRO_PackingId, new AA(Properties.Resources.NewNumbers, this.gridControl5));
            this.invalidValueExceptions.Add(Model.InvoiceZX.PRO_PackingId, new AA(Properties.Resources.EntityExists, this.gridControl5));
            this.requireValueExceptions.Add(Model.InvoiceZX.PRO_InvoiceDate, new AA(Properties.Resources.DateNotNull, this.gridControl5));
            this.requireValueExceptions.Add(Model.InvoiceZX.PRO_EmployeeId, new AA(Properties.Resources.EmployeeNotNull, this.newChooseEmployee));
        }

        string sign;
        public PackingForm(string str)
            : this()
        {
            if (str == "Check")
            {
                this.xtraTabPage4.PageVisible = false;
                this.xtraTabPage3.PageVisible = true;
                this.bindingSourceRecord.DataSource = invoicezxmenager.SelectPackingRecord(this.dateStart.DateTime, this.dateEnd.DateTime, null, null);
                sign = str;

                this.checkAll.Visible = true;
                this.btnOk.Visible = true;
                this.btnNo.Visible = true;
            }
        }

        private void PackingForm_Load(object sender, EventArgs e)
        {
            if (sign != "Check")
                this.xtraTabPage3.PageVisible = false;
        }
        public Object SelectedItem
        {
            get
            {
                return this.bindingSourceRecord.Current;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            XS.SearcharInvoiceXSForm f = new Book.UI.Invoices.XS.SearcharInvoiceXSForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.key != null && f.key.Count > 0)
                {
                    foreach (Model.InvoiceXODetail detail in f.key)
                    {
                        invoicezx = new Book.Model.InvoiceZX();
                        invoicezx.InvoiceZXId = Guid.NewGuid().ToString();
                        invoicezx.ParentInvoceZXId = null;

                        invoicezx.Product = detail.Product;
                        invoicezx.ProductId = detail.ProductId;
                        invoicezx.Customer = detail.Invoice.Customer;
                        invoicezx.CustomerId = detail.Invoice.CustomerId;
                        invoicezx.XOcustomer = detail.Invoice.xocustomer;
                        invoicezx.XOcustomerId = detail.Invoice.xocustomerId;
                        invoicezx.UNITPRICE = detail.InvoiceXODetailPrice;

                        //this.HasPackingNum = this.invoicezxmenager.SelectHasPackingNum(detail.ProductId);
                        invoicezx.ProductNum = Convert.ToDouble(detail.InvoiceXODetailQuantity0);

                        LInvoiceZXList.Add(invoicezx);
                    }
                }
                this.bindingSourceXO.DataSource = LInvoiceZXList;
                this.gridControl6.RefreshDataSource();
                this.bindingSourceXO.Position = this.bindingSourceXO.IndexOf(invoicezx);
            }
            f.Dispose();
            GC.Collect();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (RInvoiceZXList.Count > 0)
            {
                try
                {
                    if (this.newChooseEmployee.EditValue != null)
                    {
                        foreach (Model.InvoiceZX invoicezx in RInvoiceZXList)
                        {
                            invoicezx.EmployeeId = (this.newChooseEmployee.EditValue as Model.Employee).EmployeeId;
                        }
                    }
                    invoicezxmenager.InsertList(RInvoiceZXList);
                }
                catch (Helper.RequireValueException ex)
                {
                    if (this.requireValueExceptions.ContainsKey(ex.Message))
                    {
                        AA aa = this.requireValueExceptions[ex.Message];
                        MessageBox.Show(aa.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        aa.Control.Focus();
                        return;
                    }
                    throw;
                }
                catch (Helper.InvalidValueException ex)
                {
                    if (this.invalidValueExceptions.ContainsKey(ex.Message.Split(',')[0]))
                    {
                        AA aa = this.invalidValueExceptions[ex.Message.Split(',')[0]];
                        MessageBox.Show(aa.Message + ex.Message.Split(',')[1], this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        aa.Control.Focus();
                        return;
                    }
                    throw;
                }
                MessageBox.Show(Properties.Resources.SaveSuccess, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.RInvoiceZXList.Clear();
                this.LInvoiceZXList.Clear();
                this.newChooseEmployee.EditValue = null;
                this.bindingSourceXO.DataSource = null;
                this.bindingSourceDetail.DataSource = null;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (bindingSourceXO.Current != null)
            {
                Model.InvoiceZX zx = this.bindingSourceXO.Current as Model.InvoiceZX;
                this.LInvoiceZXList.Remove(zx);
                while (this.RInvoiceZXList != null && this.RInvoiceZXList.Where(d => d.ParentInvoceZXId == zx.InvoiceZXId).ToList<Model.InvoiceZX>().Count > 0)
                {
                    this.RInvoiceZXList.Remove(this.RInvoiceZXList.Where(d => d.ParentInvoceZXId == zx.InvoiceZXId).ToList<Model.InvoiceZX>().Last<Model.InvoiceZX>());
                }

                if (this.bindingSourceXO.DataSource == null || this.bindingSourceXO.Count == 0)
                {
                    this.RInvoiceZXList = null;
                    this.bindingSourceDetail.DataSource = null;
                }
                this.gridControl5.RefreshDataSource();
                this.gridControl6.RefreshDataSource();
            }
        }

        private void repositoryItemSpinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Model.InvoiceZX invoicezx = bindingSourceXO.Current as Model.InvoiceZX;
            Model.CustomerProducts cp = null;
            //成箱数量
            double? PackingSpecification = 0.0;
            if (invoicezx.Product != null && !(string.IsNullOrEmpty(invoicezx.Product.CustomerProductName) && string.IsNullOrEmpty(invoicezx.Product.CustomerId)))
            {
                cp = new BL.CustomerProductsManager().SelectByCustomerProductProceId(invoicezx.ProductId);
                PackingSpecification = cp.PackingSpecification;
            }
            if (PackingSpecification == 0 || PackingSpecification == null)
            {
                MessageBox.Show("請設置成箱數量！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SpinEdit spe = sender as SpinEdit;

            if (spe != null)
                invoicezx.ProductNum = Convert.ToDouble(spe.Value.ToString());
            else
                invoicezx.ProductNum = ((sender as BindingSource).Current as Model.InvoiceZX).ProductNum;
            int BoxNum = (int)(invoicezx.ProductNum / PackingSpecification);
            int detailnum = 0;


            while (this.RInvoiceZXList != null && this.RInvoiceZXList.Where(d => d.ParentInvoceZXId == invoicezx.InvoiceZXId && string.IsNullOrEmpty(d.PackingId) && string.IsNullOrEmpty(d.InvoiceDate.ToString())).ToList<Model.InvoiceZX>().Count > 0)
            {
                this.RInvoiceZXList.Remove(this.RInvoiceZXList.Where(d => d.ParentInvoceZXId == invoicezx.InvoiceZXId && string.IsNullOrEmpty(d.PackingId) && string.IsNullOrEmpty(d.InvoiceDate.ToString())).ToList<Model.InvoiceZX>().Last<Model.InvoiceZX>());
            }

            foreach (Model.InvoiceZX detail in RInvoiceZXList)
            {
                if (detail.ParentInvoceZXId == invoicezx.InvoiceZXId && (!string.IsNullOrEmpty(detail.PackingId) || !string.IsNullOrEmpty(detail.InvoiceDate.ToString())))
                    detailnum += 1;
            }


            if (invoicezx.ProductNum % PackingSpecification == 0)
            {
                DetailAdd(invoicezx, BoxNum, detailnum, cp);
                invoicezx.BoxNum = BoxNum;
            }
            else
            {
                DetailAdd(invoicezx, BoxNum + 1, detailnum, cp);
                invoicezx.BoxNum = BoxNum + 1;
            }

            foreach (Model.InvoiceZX iz in RInvoiceZXList)
            {
                iz.Sequence = (RInvoiceZXList.IndexOf(iz) + 1).ToString();
            }
            this.bindingSourceDetail.DataSource = RInvoiceZXList;
            this.gridControl5.RefreshDataSource();
            this.gridControl6.RefreshDataSource();
        }

        /// <summary>
        /// 计算装箱记录
        /// </summary>
        /// <param name="invoicezx"></param>
        /// <param name="产品数量"></param>
        /// <param name="装箱编号或日期不为空的记录条数"></param>
        private void DetailAdd(Model.InvoiceZX invoicezx, int BoxNum, int detailnum, Model.CustomerProducts cp)
        {
            for (int i = 0; i < BoxNum - detailnum; i++)
            {
                Model.InvoiceZX Rinvoicezx = new Book.Model.InvoiceZX();
                Rinvoicezx.InvoiceZXId = Guid.NewGuid().ToString();
                Rinvoicezx.ParentInvoceZXId = invoicezx.InvoiceZXId;
                Rinvoicezx.Product = invoicezx.Product;
                Rinvoicezx.ProductId = invoicezx.ProductId;
                Rinvoicezx.Customer = invoicezx.Customer;
                Rinvoicezx.CustomerId = invoicezx.CustomerId;
                Rinvoicezx.XOcustomer = invoicezx.XOcustomer;
                Rinvoicezx.XOcustomerId = invoicezx.XOcustomerId;
                Rinvoicezx.PackingNum = cp.PackingSpecification;
                Rinvoicezx.UNITPRICE = invoicezx.UNITPRICE;
                //if (i == BoxNum - detailnum - 1 && invoicezx.ProductNum % cp.PackingSpecification != 0)
                //    Rinvoicezx.PackingNum = invoicezx.ProductNum % cp.PackingSpecification;

                if (cp != null)
                {
                    Rinvoicezx.BLong = cp.BLong;
                    Rinvoicezx.BWide = cp.BWide;
                    Rinvoicezx.BHigh = cp.BHigh;
                    Rinvoicezx.BWeight = cp.JWeight;
                    Rinvoicezx.AllWeight = cp.MWeight;
                    Rinvoicezx.Caiji = cp.Caiji;
                }

                RInvoiceZXList.Add(Rinvoicezx);
            }
            for (int n = 0; n < detailnum - BoxNum; n++)
            {
                this.RInvoiceZXList.Remove(this.RInvoiceZXList.Where(d => d.ParentInvoceZXId == invoicezx.InvoiceZXId).ToList<Model.InvoiceZX>().Last<Model.InvoiceZX>());
            }
            if (invoicezx.ProductNum % cp.PackingSpecification != 0)
                this.RInvoiceZXList.Where(d => d.ParentInvoceZXId == invoicezx.InvoiceZXId).ToList<Model.InvoiceZX>().Last<Model.InvoiceZX>().PackingNum = invoicezx.ProductNum % cp.PackingSpecification;
        }

        private void bindingSourceXO_CurrentChanged(object sender, EventArgs e)
        {
            if (LInvoiceZXList != null)
            {
                if (RInvoiceZXList != null && RInvoiceZXList.Count > 0)
                {
                    this.bindingSourceDetail.DataSource = this.RInvoiceZXList.Where(d => d.ParentInvoceZXId == (this.bindingSourceXO.Current as Model.InvoiceZX).InvoiceZXId).ToList<Model.InvoiceZX>();
                }
                if (this.bindingSourceDetail.DataSource == null || this.bindingSourceDetail.Count == 0)
                {
                    //this.bindingSourceDetail.DataSource = null;
                    this.repositoryItemSpinEdit1_EditValueChanged(sender, e);
                }
                foreach (Model.InvoiceZX invoicezx in this.bindingSourceDetail)
                {
                    invoicezx.Sequence = (bindingSourceDetail.IndexOf(invoicezx) + 1).ToString();
                }
            }
            else
            {
                this.LInvoiceZXList = null;
                this.RInvoiceZXList = null;
                this.gridControl5.RefreshDataSource();
                this.gridControl6.RefreshDataSource();
            }
        }

        private void gridView5_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.InvoiceZX invoicezx = bindingSourceDetail.Current as Model.InvoiceZX;
            if (invoicezx.PackingId != null)
            {
                foreach (Model.InvoiceZX detail in RInvoiceZXList)
                {
                    if (invoicezx.PackingId == detail.PackingId && invoicezx.InvoiceZXId != detail.InvoiceZXId)
                    {
                        invoicezx.PackingId = null;
                        MessageBox.Show(Properties.Resources.EntityExists, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.bindingSourceRecord.DataSource = invoicezxmenager.SelectPackingRecord(this.dateStart.EditValue == null ? DateTime.Now.Date.AddDays(-15) : this.dateStart.DateTime, this.dateEnd.EditValue == null ? DateTime.Now : this.dateEnd.DateTime, this.ncCustomer.EditValue as Model.Customer, this.ncXOCustomer.EditValue as Model.Customer);
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            this.xtraTabPage4.PageVisible = false;
            this.xtraTabPage3.PageVisible = true;
            this.bindingSourceRecord.DataSource = invoicezxmenager.SelectPackingRecord(this.dateStart.DateTime, this.dateEnd.DateTime, null, null);
            //装箱历史中的选择框隐藏
            this.check.Visible = false;
        }

        private void btnInvoicePacking_Click(object sender, EventArgs e)
        {
            this.xtraTabPage3.PageVisible = false;
            this.xtraTabPage4.PageVisible = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.checkedInvoicezx.Count < 1)
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridView4_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "check")
            {
                Model.InvoiceZX invoicezx = this.bindingSourceRecord.Current as Model.InvoiceZX;

                if ((bool)e.Value)
                    checkedInvoicezx.Add(invoicezx);
                if (!(bool)e.Value)
                {
                    foreach (Model.InvoiceZX zx in checkedInvoicezx)
                    {
                        if (zx.InvoiceZXId == invoicezx.InvoiceZXId)
                            checkedInvoicezx.Remove(invoicezx);
                        break;
                    }
                }
            }
        }

        private void checkAll_Click(object sender, EventArgs e)
        {
            if (!this.checkAll.Checked)
            {
                foreach (Model.InvoiceZX invoicezx in this.bindingSourceRecord)
                {
                    invoicezx.IsChecked = true;
                    if (checkedInvoicezx.Contains(invoicezx))
                        continue;
                    checkedInvoicezx.Add(invoicezx);
                }
            }
            else
            {
                foreach (Model.InvoiceZX invoicezx in this.bindingSourceRecord)
                {
                    invoicezx.IsChecked = false;
                    if (checkedInvoicezx.Contains(invoicezx))
                        checkedInvoicezx.Remove(invoicezx);
                }
            }
            this.gridControl4.RefreshDataSource();
        }

        private void gridView4_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            GridHitInfo info = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (info.InRow && !view.IsGroupRow(info.RowHandle))
                this.DialogResult = DialogResult.OK;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.ColumnLong.OptionsColumn.AllowEdit = true;
            this.ColumnWidth.OptionsColumn.AllowEdit = true;
            this.ColumnHeight.OptionsColumn.AllowEdit = true;
            this.ColumnJWeight.OptionsColumn.AllowEdit = true;
            this.ColumnMWeight.OptionsColumn.AllowEdit = true;
            this.ColumnCaiji.OptionsColumn.AllowEdit = true;
            this.ColumnNum.OptionsColumn.AllowEdit = true;
            this.InvoiceNote.OptionsColumn.AllowEdit = true;
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            this.invoicezxmenager.InsertList(this.bindingSourceRecord.DataSource as IList<Model.InvoiceZX>);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.bindingSourceRecord.DataSource = invoicezxmenager.SelectPackingRecord(this.dateStart.EditValue == null ? DateTime.Now.Date.AddDays(-15) : this.dateStart.DateTime, this.dateEnd.EditValue == null ? DateTime.Now : this.dateEnd.DateTime, this.ncCustomer.EditValue as Model.Customer, this.ncXOCustomer.EditValue as Model.Customer);
        }

        private void btnRollBack_Click(object sender, EventArgs e)
        {
            this.LInvoiceZXList = null;
            this.RInvoiceZXList = null;

            this.gridControl5.RefreshDataSource();
            this.gridControl6.RefreshDataSource();
        }

    }
}