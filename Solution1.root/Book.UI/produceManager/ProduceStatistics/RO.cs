using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceStatistics
{
	public partial class RO : DevExpress.XtraReports.UI.XtraReport
	{
        public RO(IList<Model.ProduceStatisticsDetail> pstList)
		{
			InitializeComponent();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceLookTable;
            this.xrLabel2.Text = DateTime.Now.ToShortDateString();
            this.DataSource = pstList;

            //Ã÷Ï¸
            this.xrTableCellDate.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_DetailDate, "{0:yyyy-MM-dd}");
            this.xrTableCellType.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_BusinessHoursType);
            this.xrTableCellPId.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_PronoteHeaderID);
            this.xrTableCellEllo.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellElpple.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellPcount.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_ProduceQuantity);
            this.xrTableCellHege.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_HeGeQuantity);
            this.xrTableCellNoPcount.DataBindings.Add("Text", this.DataSource, Model.ProduceStatisticsDetail.PRO_NoProduceQuantity);           
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Procedures." + Model.Procedures.PRO_Procedurename);
		}

	}
}
