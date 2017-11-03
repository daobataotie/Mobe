using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCInputCheck
{
    public partial class BackUp_RO : DevExpress.XtraReports.UI.XtraReport
    {
        public BackUp_RO()
        {
            InitializeComponent();
        }

        public BackUp_RO(Model.PCInputCheck PCInputCheck)
            : this()
        {
            foreach (var item in PCInputCheck.GetType().GetProperties())
            {
                if (item.Name == "Heidian" || item.Name == "Guohuo" || item.Name == "Liaodian" || item.Name == "Wasiqi" || item.Name == "Zazhi" || item.Name == "Qipao" || item.Name == "Guangxue" || item.Name == "Duise" || item.Name == "Chongji" || item.Name == "Nairanceshi" || item.Name == "UVvalue")
                {
                    if (item.GetValue(PCInputCheck, null) != null)
                        switch (item.GetValue(PCInputCheck, null).ToString())
                        {
                            case "0":
                                item.SetValue(PCInputCheck, "PASS", null);
                                break;
                            case "1":
                                item.SetValue(PCInputCheck, "FALL", null);
                                break;
                            case "2":
                                item.SetValue(PCInputCheck, "無此項", null);
                                break;
                        }
                }
            }

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            //this.lblPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.lblPCInputCheckId.Text = PCInputCheck.PCInputCheckId;
            this.lblPCInputCheckDate.Text = PCInputCheck.PCInputCheckDate.Value.ToString("yyyy-MM-dd");
            this.lblSupplier.Text = PCInputCheck.Supplier == null ? null : PCInputCheck.Supplier.SupplierShortName;
            this.lblMadeEmp.Text = PCInputCheck.CheckEmployee == null ? null : PCInputCheck.CheckEmployee.EmployeeName;
            this.lblProduct.Text = PCInputCheck.Product == null ? null : PCInputCheck.Product.ProductName;

            string[] coid = string.IsNullOrEmpty(PCInputCheck.InvoiceCOId) ? null : PCInputCheck.InvoiceCOId.Split(',');
            string[] cgid = string.IsNullOrEmpty(PCInputCheck.InvoiceCGId) ? null : PCInputCheck.InvoiceCGId.Split(',');
            if (coid != null)
                this.lblInvoiceCOId.Text = coid[0] + (string.IsNullOrEmpty(coid[1]) ? "" : "," + coid[1] + (string.IsNullOrEmpty(coid[2]) ? "" : "," + coid[2]));
            if (cgid != null)
                this.lblInvoiceCGId.Text = cgid[0] + (string.IsNullOrEmpty(cgid[1]) ? "" : "," + cgid[1] + (string.IsNullOrEmpty(cgid[2]) ? "" : "," + cgid[2]));
            this.lblQuantity.Text = PCInputCheck.Quantity.Value.ToString();
            this.lblProductUnit.Text = PCInputCheck.ProductUnit;
            this.lblChouliaoDate.Text = PCInputCheck.ChouliaoDate == null ? null : PCInputCheck.ChouliaoDate.Value.ToString("yyyy-MM-dd");
            this.lblTestDate.Text = PCInputCheck.TestDate == null ? null : PCInputCheck.TestDate.Value.ToString("yyyy-MM-dd");
            this.lblLotNukber.Text = PCInputCheck.LotNumber;
            this.lblTestProduct.Text = PCInputCheck.TestProductId;
            this.lblTestEmployee.Text = PCInputCheck.TestEmployee == null ? null : PCInputCheck.TestEmployee.EmployeeName;

            this.TCHeidian.Text = PCInputCheck.Heidian;
            this.TCGuohuo.Text = PCInputCheck.Guohuo;
            this.TCLiaodian.Text = PCInputCheck.Liaodian;
            //this.TCWasiqi.Text = PCInputCheck.Wasiqi;
            this.TCGuangxue.Text = PCInputCheck.Guangxue;
            this.TCZazhi.Text = PCInputCheck.Zazhi;
            this.TCQipao.Text = PCInputCheck.Qipao;
            this.TCDuise.Text = PCInputCheck.Duise;
            this.TCChongji.Text = PCInputCheck.Chongji;
            this.TCNairan.Text = PCInputCheck.Nairanceshi;
            this.TCUV.Text = PCInputCheck.UVvalue;
            this.TCANSI.Text = PCInputCheck.ANSICSAToushilv;
            this.TCEN.Text = PCInputCheck.ENToushilv;
            this.TCAS.Text = PCInputCheck.ASToushilv;
            this.TCJIS.Text = PCInputCheck.JISToushilv;
            this.TC_Wudu.Text = PCInputCheck.Wudu;

            this.TCDuiseEmp.Text = PCInputCheck.DuiseEmployee == null ? null : PCInputCheck.DuiseEmployee.EmployeeName;
            this.TCChongjiEmp.Text = PCInputCheck.ChongjiEmployee == null ? null : PCInputCheck.ChongjiEmployee.EmployeeName;
            this.TCNairanEmp.Text = PCInputCheck.NairanEmployee == null ? null : PCInputCheck.NairanEmployee.EmployeeName;
            this.TCUVEmp.Text = PCInputCheck.UVEmployee == null ? null : PCInputCheck.UVEmployee.EmployeeName;
            this.TCToushiEmp.Text = PCInputCheck.ToushiEmployee == null ? null : PCInputCheck.ToushiEmployee.EmployeeName;

            this.lblConfirmor.Text = PCInputCheck.Confirmor == null ? null : PCInputCheck.Confirmor.EmployeeName;
        }
    }
}
