using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Settings.ProduceManager
{
    public partial class ROBOMProcess : DevExpress.XtraReports.UI.XtraReport
    {
        public ROBOMProcess()
        {
            InitializeComponent();
        }

        public ROBOMProcess(IList<Model.Technologydetails> list)
            : this()
        {
            this.DataSource = list;

            TCInumber.DataBindings.Add("Text", this.DataSource, Model.Technologydetails.PROPERTY_TECHNOLOGYDETAILSNO);
            TCId.DataBindings.Add("Text", this.DataSource, "Procedures." + Model.Procedures.PRO_Id);
            xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Procedures." + Model.Procedures.PRO_Procedurename);
            TCWorkStations.DataBindings.Add("Text", this.DataSource, "Procedures.WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            xrCheckBox1.DataBindings.Add("Checked", this.DataSource, "Procedures." + Model.Procedures.PRO_IsOtherProduceOther);
            TCWeiwaiSupplier.DataBindings.Add("Text", this.DataSource, "Procedures.Supplier." + Model.Supplier.PROPERTY_SUPPLIERFULLNAME);
        }
    }
}
