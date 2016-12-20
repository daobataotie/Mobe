using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.PCBoxFootCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();


        }

        public RO(Model.PCBoxFootCheck model)
            : this()
        {
            this.DataSource = model.Details.OrderBy(d => d.CheckDate).ToList();

            foreach (var detail in model.Details)
            {
                foreach (var item in detail.GetType().GetProperties())
                {
                    if (item.Name == "Flap" || item.Name == "Exterior" || item.Name == "OfColor" || item.Name == "HeightFootL" || item.Name == "HeightFootR" || item.Name == "FootElasticL" || item.Name == "FootElasticR" || item.Name == "ImpactTest" || item.Name == "AceticacidTest" || item.Name == "Houdu" || item.Name == "Guangxue" || item.Name == "Jihao")
                    {
                        if (item.GetValue(detail, null) != null)
                        {
                            switch (item.GetValue(detail, null).ToString())
                            {
                                case "0":
                                    item.SetValue(detail, "√", null);
                                    break;
                                case "1":
                                    item.SetValue(detail, "X", null);
                                    break;
                                case "2":
                                    item.SetValue(detail, "△", null);
                                    break;
                            }
                        }
                    }
                }
            }
            if (model != null)
            {
                this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
                //this.lblReportName.Text = Properties.Resources.PCBoxFootCheck;
                this.lblReportName.Text = "品管线上检查表";
                //this.lblPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

                this.lblId.Text = model.PCBoxFootCheckId;
                this.lblCheckDate.Text = model.CheckDate == null ? null : model.CheckDate.Value.ToString("yyyy-MM-dd");
                this.lblProductName.Text = model.Product == null ? null : model.Product.ProductName;
                if (model.InvoiceXO != null)
                {
                    this.lblInvoiceXO.Text = model.InvoiceXO.CustomerInvoiceXOId;
                }
                else if (model.PronoteHeader != null)
                {
                    this.lblInvoiceXO.Text = model.PronoteHeader.InvoiceXO == null ? "" : model.PronoteHeader.InvoiceXO.CustomerInvoiceXOId;
                }
                this.lblPronoteHeader.Text = model.PronoteHeaderId;
                this.lblEmployee.Text = model.Employee == null ? null : model.Employee.EmployeeName;
                this.xrRichTextNote.Rtf = model.Note;
                this.lblGetNum.Text = model.GetNum.ToString();
                this.lblCheckNum.Text = model.CheckNum.ToString();
                this.lblPassNum.Text = model.PassNum.ToString();
                this.lblProductUnit.Text = model.ProductUnit;

                //this.DataSource = model;
                //this.TCFlap.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheck.PRO_Flap);
                //this.TCExterior.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheck.PRO_Exterior);
                this.TCCheckDate.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_CheckDate, "{0:yyyy-MM-dd HH:mm:ss}");
                this.TCFlap.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_Flap);
                this.TCExterior.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_Exterior);
                this.TCOfColor.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_OfColor);
                this.TCHeightFootL.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_HeightFootL);
                //this.TCHeightFootR.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_HeightFootR);
                this.TCFootElasticL.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_FootElasticL);
                //this.TCFootElasticR.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_FootElasticR);
                this.TCImpactTest.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_ImpactTest);
                this.TCAceticacidTest.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_AceticacidTest);

                this.xrTBusinessHours.DataBindings.Add("Text", this.DataSource, "BusinessHours." + Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);
                this.TCAbnormal.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_Abnormal);

                this.TCHoudu.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_Houdu);
                this.TCGuangxue.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_Guangxue);
                this.TCJihao.DataBindings.Add("Text", this.DataSource, Model.PCBoxFootCheckDetail.PRO_Jihao);
            }

        }
    }
}
