using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.Settings.BasicData.Supplier
{
    public partial class ROSupplierProcesscategory : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.SupplierProcesscategoryManager _SupplierProcesscategoryManager = new Book.BL.SupplierProcesscategoryManager();
        IList<Model.SupplierProcesscategory> _data;

        public ROSupplierProcesscategory()
        {
            InitializeComponent();
        }

        public ROSupplierProcesscategory(Model.Supplier sup)
            : this()
        {
            this._data = this._SupplierProcesscategoryManager.mSelect(sup.SupplierId);
            if (_data == null | this._data.Count == 0)
                return;

            var _gData = (from item in _data
                          group item by item.ProcessCategoryId into g
                          select g.Key).ToList<string>();

            this.DataSource = _gData;

            //Control
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = "供應商加工對應表";
            this.lblSupplier.Text += " " + sup.ToString();
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.xrSubreport1.ReportSource = new ROSupplierProcesscategoryDetails();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ROSupplierProcesscategoryDetails DetailReport = this.xrSubreport1.ReportSource as ROSupplierProcesscategoryDetails;
            DetailReport.data = this._data.Where(d => d.ProcessCategoryId == (this.GetCurrentRow() as string)).ToList();

        }
    }
}
