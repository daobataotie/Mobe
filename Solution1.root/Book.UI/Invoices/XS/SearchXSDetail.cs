using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Book.UI.Invoices.XS
{
    public partial class SearchXSDetail : Book.UI.Settings.BasicData.BaseChooseForm
    {
        BL.InvoiceXSManager _mInvoiceXSManager = new Book.BL.InvoiceXSManager();

        BL.InvoiceXSDetailManager _mInvoiceXSDetailManager = new Book.BL.InvoiceXSDetailManager();
        public List<Model.InvoiceXSDetail> selectItems = new List<Model.InvoiceXSDetail>();

        public SearchXSDetail()
        {
            InitializeComponent();
            this.gridView3.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView3_CellValueChanging);
            this.manager = new BL.InvoiceXSDetailManager();
            this.dateEditStart.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEditEnd.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            this.nccChuHuoCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
        }

        protected override void LoadData()
        {
            this.bsHeader.DataSource = this._mInvoiceXSManager.SelectDateRangAndWhere(null, null, this.dateEditStart.DateTime, this.dateEditEnd.DateTime, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, "", null, null, null);
            if (this.bsHeader.Current != null)
            {
                Model.InvoiceXS xs = this.bsHeader.Current as Model.InvoiceXS;
                IList<Model.InvoiceXSDetail> xsd = this._mInvoiceXSDetailManager.Select(xs);
                (this.bsHeader.Current as Model.InvoiceXS).Details = xsd;
                this.bindingSource1.DataSource = (this.bsHeader.Current as Model.InvoiceXS).Details;
            }
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoiceXSDetail> details = this.bindingSource1.DataSource as IList<Model.InvoiceXSDetail>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            Model.InvoiceXODetail codetail = details[e.ListSourceRowIndex].InvoiceXODetail;
            switch (e.Column.Name)
            {
                case "colProductId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
                case "gridColumn7":
                    if (codetail != null)
                        e.DisplayText = (codetail.InvoiceXODetailQuantity0 == null ? 0 : codetail.InvoiceXODetailQuantity0.Value).ToString("0.####");
                    break;
                case "gridColumn6":
                    if (codetail != null)
                        e.DisplayText = codetail.InvoiceXODetailQuantity.Value.ToString();
                    break;
                case "gridColumn27":
                    if (codetail != null)
                        e.DisplayText = codetail.Invoice.CustomerInvoiceXOId;
                    break;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.RecordSelected();
            this.bsHeader.DataSource = this._mInvoiceXSManager.SelectDateRangAndWhere(this.nccChuHuoCustomer.EditValue as Model.Customer, null, this.dateEditStart.DateTime, this.dateEditEnd.DateTime, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, "", null, null, null);
            if (this.bsHeader.Current != null)
            {
                Model.InvoiceXS xs = this.bsHeader.Current as Model.InvoiceXS;
                IList<Model.InvoiceXSDetail> xsd = this._mInvoiceXSDetailManager.Select(xs);
                (this.bsHeader.Current as Model.InvoiceXS).Details = xsd;
                this.bindingSource1.DataSource = (this.bsHeader.Current as Model.InvoiceXS).Details;
            }
            else
            {
                this.bindingSource1.DataSource = null;
            }
        }

        //更改查询条件
        private void btnChangeSearch_Click(object sender, EventArgs e)
        {
            this.RecordSelected();
            Query.ConditionXChooseForm f = new Query.ConditionXChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionX con = f.Condition as Query.ConditionX;
                this.bsHeader.DataSource = this._mInvoiceXSManager.SelectDateRangAndWhere(con.Customer1, con.Customer2, con.StartDate, con.EndDate, con.Yjri1, con.Yjri2, con.CusXOId, con.Product, null, null);
                if (this.bsHeader.Current != null)
                {
                    Model.InvoiceXS xs = this.bsHeader.Current as Model.InvoiceXS;
                    IList<Model.InvoiceXSDetail> xsd = this._mInvoiceXSDetailManager.Select(xs);
                    (this.bsHeader.Current as Model.InvoiceXS).Details = xsd;
                    this.bindingSource1.DataSource = (this.bsHeader.Current as Model.InvoiceXS).Details;
                }
            }
            f.Dispose();
            GC.Collect();
        }

        public override void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.RecordSelected();
            this.DialogResult = DialogResult.OK;
        }

        private void bsHeader_CurrentChanged(object sender, EventArgs e)
        {
            if (this.bsHeader.Current != null)
            {
                if ((this.bsHeader.Current as Model.InvoiceXS).Details.Count == 0)
                {
                    Model.InvoiceXS ixs = this.bsHeader.Current as Model.InvoiceXS;
                    IList<Model.InvoiceXSDetail> ixsd = this._mInvoiceXSDetailManager.Select(ixs);
                    (this.bsHeader.Current as Model.InvoiceXS).Details = ixsd;
                }
                //this.SelectAll((this.bsHeader.Current as Model.InvoiceXS).Details);
                this.bindingSource1.DataSource = (this.bsHeader.Current as Model.InvoiceXS).Details;
            }
            else
                this.bindingSource1.DataSource = null;
        }

        //记录选择保存
        private void RecordSelected()
        {
            foreach (Model.InvoiceXS h in (this.bsHeader.DataSource as IList<Model.InvoiceXS>))
            {
                foreach (Model.InvoiceXSDetail d in h.Details.Where(d => d.Checked == true))
                {
                    if (!this.selectItems.Any(ind => ind.InvoiceXSDetailId == d.InvoiceXSDetailId))
                    {
                        this.selectItems.Add(d);
                    }
                }
            }
        }

        //改变都信息值
        private void gridView3_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name != "gridColumn16") return;
            if (bool.Parse(e.Value.ToString()))
            {
                (this.bsHeader.Current as Model.InvoiceXS).Details.ToList().ForEach(d => d.Checked = true);
            }
            else
            {
                (this.bsHeader.Current as Model.InvoiceXS).Details.ToList().ForEach(d => d.Checked = false);
            }

            this.gridControl3.RefreshDataSource();
        }
    }
}