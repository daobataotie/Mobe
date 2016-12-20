using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
//н�Y���A�O����ӡ
namespace Book.UI.Hr.Salary.Salaryset
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�
// ��������: �͑��aƷ�O��
// �� �� ����CustomerProductForm
// �� �� ��: ���޾�  ���             ���ʱ��:2009-10-10
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class MonthSalaryReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MonthSalaryReport()
        {
            InitializeComponent();
            this.xrLabel2.Text = BL.Settings.CompanyChineseName+"�T��н�Y���A���";
            this.xrLabelPrintDate.Text = string.Format(Properties.Resources.Book_UI_Query_BaseReport_InvoiceDate, DateTime.Now.ToString("yyyy-MM-dd"));
           // this.employeeManager.SelectOnActive();

            this.DataSource = (new BL.EmployeeManager()).SelectOnActive();
            #region ��ϸ��Ϣ
     

            this.xrTableCellname.DataBindings.Add("Text", this.DataSource,Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCelldep.DataBindings.Add("Text", this.DataSource, "Department." + Model.Department.PROPERTY_DEPARTMENTNAME);
            this.xrTableCellpost.DataBindings.Add("Text", this.DataSource, "Duty." + Model.Duty.PROPERTY_DUTYNAME);
            //�ɲ�
            this.xrTableCellCadre.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_ISCADRE);
            this.xrTableCellMonthlyPay.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_MONTHLYPAY, "{0:0}");
            this.xrTableCell1FieldPay.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_FIELDPAY, "{0:0}");
            this.xrTableCell1PostPay.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_POSTPAY, "{0:0}");
            this.xrTableCellInsurance.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_INSURANCE, "{0:0}");
            this.xrTableCellTax.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_TAX, "{0:0}");
            this.xrTableDailyPay.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_DAILYPAY, "{0:0}");
            this.xrTableDutyPay.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_DUTYPAY, "{0:0}");
            #endregion
            
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
