using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.produceManager.PronoteHeader
{
    public partial class SelectPronoteProceduresDetail : DevExpress.XtraEditors.XtraForm
    {

        #region
        private BL.PronoteProceduresDetailManager pronoteprocedureDetailManger = new BL.PronoteProceduresDetailManager();
        private IList<Model.PronoteProceduresDetail> pronoteProcedureDetails;

        public IList<Model.PronoteProceduresDetail> _pronoteProcedureDetails
        {
            get { return pronoteProcedureDetails; }
            set { pronoteProcedureDetails = value; }
        }
        #endregion

        public SelectPronoteProceduresDetail()
        {
            InitializeComponent();
            this.dateEditstartdate.DateTime = System.DateTime.Now.AddMonths(-3);
            this.dateEditenddate.DateTime = System.DateTime.Now;
        }

        private void SelectPronoteProceduresDetail_Load(object sender, EventArgs e)
        {
            this.bindingSourcePronoteProceduresDetail.DataSource = this.pronoteprocedureDetailManger.SelectByDateRange(this.dateEditstartdate.Text == null ? global::Helper.DateTimeParse.NullDate : this.dateEditstartdate.DateTime, this.dateEditenddate.Text == null ? System.DateTime.Now.AddMonths(12) : this.dateEditenddate.DateTime);
            this.gridControl1.RefreshDataSource();
        }

        private void simpleButtonSearch_Click(object sender, EventArgs e)
        {
            this.bindingSourcePronoteProceduresDetail.DataSource = this.pronoteprocedureDetailManger.SelectByDateRange(this.dateEditstartdate.Text == null ? global::Helper.DateTimeParse.NullDate : this.dateEditstartdate.DateTime, this.dateEditenddate.Text == null ? System.DateTime.Now.AddMonths(12) : this.dateEditenddate.DateTime);
            this.gridControl1.RefreshDataSource();
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void simpleButtonSure_Click(object sender, EventArgs e)
        {
            if (this.bindingSourcePronoteProceduresDetail.DataSource == null) return;
            if (this._pronoteProcedureDetails == null)
                this._pronoteProcedureDetails = new List<Model.PronoteProceduresDetail>();
            IList<Model.PronoteProceduresDetail> list = this.bindingSourcePronoteProceduresDetail.DataSource as List<Model.PronoteProceduresDetail>;
            this._pronoteProcedureDetails = list.Where(p => p.IsChecked == true).ToList();
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.PronoteProceduresDetail> details = this.bindingSourcePronoteProceduresDetail.DataSource as IList<Model.PronoteProceduresDetail>;
            if (details == null || details.Count < 1) return;
            Model.PronoteHeader header = details[e.ListSourceRowIndex].PronoteHeader;
            Model.Supplier supplier = details[e.ListSourceRowIndex].Supplier;
            switch (e.Column.Name)
            {
                case "gridColumnPro":
                    if (header == null) return;
                    if (string.IsNullOrEmpty(header.Product.CustomerProductName))
                        e.DisplayText = header.Product.ProductName;
                    else
                        e.DisplayText = header.Product.ProductName+"{"+header.Product.CustomerProductName+"}";                       
                    break;
                case "gridColumnSupplier":
                    if (supplier == null) return;
                    e.DisplayText = supplier.SupplierShortName;
                    break;
                case "gridColumnQuanqity":                                    
                        e.DisplayText = header.DetailsSum.ToString();           
                      break;
                case "gridColumnUnit":
                      e.DisplayText = header.ProductUnit;
                      break;

            }
        }
    }
}