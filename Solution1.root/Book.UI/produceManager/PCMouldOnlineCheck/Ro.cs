using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCMouldOnlineCheck
{
    public partial class Ro : DevExpress.XtraReports.UI.XtraReport
    {
        public Ro()
        {
            InitializeComponent();
        }

        public Ro(Model.PCMouldOnlineCheck model)
            : this()
        {
            this.DataSource = model.Detail;

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblDate.Text = model.PCMouldOnlineCheckDate.Value.ToString("yyyy-MM-dd");
            this.lblEmployee.Text = model.Employee == null ? null : model.Employee.EmployeeName;

            this.TCProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCInvoiceCusId.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            this.TCOnlineDate.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_OnlineDate, "{0:MM-dd HH:mm}");
            this.TCCheckDate.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_CheckDate, "{0:MM-dd HH:mm}");
            this.TCBurr.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_Burr);
            this.TCBruise.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_Bruise);
            this.TCShrink.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_Shrink);
            this.TCForColor.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_ForColor);
            this.TCFlap.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_Flap);
            this.TCSandwichedConfirm.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_SandwichedConfirm);
            this.TCMark.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_Mark);
            this.TCAbnormal.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_Abnormal);
            this.TCEmployee.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);

            //2018年8月30日17:49:26
            this.TCAppearance.DataBindings.Add("Text", this.DataSource, Model.PCMouldOnlineCheckDetail.PRO_Appearance);
        }
    }
}
