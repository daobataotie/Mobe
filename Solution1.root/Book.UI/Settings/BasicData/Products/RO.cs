using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.Settings.BasicData.Products
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();

        }
        public RO(DataTable dt)
            : this()
        {
            this.lblCompany.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.MaterialCount;
            this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            this.DataSource = dt;
            XRTableCell cell1;
            XRTableCell cell2;
            for (int i = 3; i < dt.Columns.Count; i++)
            {
                cell1 = new XRTableCell();
                cell2 = new XRTableCell();
                cell1.Name = dt.Columns[i].ToString();
                cell1.Text = cell1.Name;
                cell2.Name = dt.Columns[i].ToString();
                cell2.Text = cell2.Name;
                this.xrTable1.WidthF += cell1.WidthF;
                this.xrTable2.WidthF += cell2.WidthF;
                xrTableRow1.Cells.Add(cell1);
                xrTableRow2.Cells.Add(cell2);
            }
            for (int i = 0; i < xrTableRow1.Cells.Count; i++)
            {
                if (i == 0)
                {
                    xrTableRow1.Cells[i].WidthF = 500;
                    xrTableRow2.Cells[i].WidthF = 500;
                }
                else if (i == 1)
                {
                    xrTableRow1.Cells[i].WidthF = 500;
                    xrTableRow2.Cells[i].WidthF = 500;
                }
                else
                {
                    xrTableRow1.Cells[i].WidthF = 200;
                    xrTableRow2.Cells[i].WidthF = 200;
                }

                xrTableRow1.Cells[i].DataBindings.Add("Text", this.DataSource, dt.Columns[i].ToString());
            }
        }
    }
}
