using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q49_2 : DevExpress.XtraReports.UI.XtraReport
    {

        BL.PronoteProceduresDetailManager detailManager = new BL.PronoteProceduresDetailManager();
        private System.Collections.Generic.IList<Model.PronoteMachine> machineList = new System.Collections.Generic.List<Model.PronoteMachine>();
        private BL.PronoteMachineManager pronoteMachineManager = new BL.PronoteMachineManager();

        public Q49_2()
        {
            InitializeComponent();

            this.xrTableProceduresNo.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_ProceduresNo);
            this.xrRichProcedures.DataBindings.Add("Rtf", this.DataSource, "Procedures." + Model.Procedures.PRO_Procedurename);
            //this.xrTableIsOtherProduceOther.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_IsOtherProduceOther);
            this.xrCheckIsOtherProduceOther.DataBindings.Add("Checked", this.DataSource, Model.PronoteProceduresDetail.PRO_IsOtherProduceOther);
            this.xrTablePronoteProceduresDate.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_PronoteProceduresDate, "{0:yyyy-MM-dd}");
            this.xrTableSupplierId.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.xrTableWorkHouseId.DataBindings.Add("Text", this.DataSource, "WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
        }

        private Model.PronoteHeader pronote;

        public Model.PronoteHeader Pronote
        {
            get { return pronote; }
            set { pronote = value; }
        }

        private void Q49_2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            System.Collections.Generic.IList<Model.PronoteProceduresDetail> list = detailManager.GetPronotedetailsMaterialByHeaderId(this.Pronote);

            if (list != null || list.Count != 0)
            {
                foreach (Model.PronoteProceduresDetail detail in list)
                {
                    this.machineList = this.pronoteMachineManager.GetPronoteMachineByPronoteProceduresDetailId(detail.PronoteProceduresDetailId);
                    {
                        foreach (Model.PronoteMachine machine in machineList)
                        {
                            if (machineList.IndexOf(machine) == machineList.Count - 1)
                                detail.Machine += machine.PronoteMachineName;
                            else
                                detail.Machine += machine.PronoteMachineName + ",";
                        }
                    }
                }
                this.DataSource = list;

            }
        }
    }
}
