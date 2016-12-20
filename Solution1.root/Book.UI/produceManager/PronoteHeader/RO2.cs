using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PronoteHeader  
{
    public partial class RO2 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        private BL.PronoteProceduresDetailManager pronoteProceduresDetailManager = new Book.BL.PronoteProceduresDetailManager();
        private IList<Model.PronoteMachine> machineList = new List<Model.PronoteMachine>();
        private BL.PronoteMachineManager pronoteMachineManager = new BL.PronoteMachineManager();

        private Model.PronoteHeader pronoteHeader;

        public Model.PronoteHeader PronoteHeader
        {
            get { return pronoteHeader; }
            set { pronoteHeader = value; }
        }
        public RO2()
        {
            InitializeComponent(); 

            //明细
            this.xrTableCellPronoteProceduresDate.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_PronoteProceduresDate, "{0:yyyy-MM-dd}");
            //this.xrTableCellProceduresNo.DataBindings.Add("Text", this.DataSource,Model.PronoteProceduresDetail.PRO_ProceduresNo);

            this.xrTableCellWorkHouseId.DataBindings.Add("Text", this.DataSource, "WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            this.xrTableCellSupplierId.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            //this.xrTableCellPronoteYingQuantity.DataBindings.Add("Text", this.DataSource,Model.PronoteProceduresDetail.PRO_PronoteYingQuantity);
            //this.xrTableCellFulfillQuantity.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_FulfillQuantity);
            //this.xrTableCellDeposeQuantity.DataBindings.Add("Text", this.DataSource,  Model.PronoteProceduresDetail.PRO_DeposeQuantity);
            //this.xrTableCellcheckQuantity.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_checkQuantity);
            //this.xrTableCellLossQuantity.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_LossQuantity);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Procedures." + Model.Procedures.PRO_Procedurename);
            this.xrTableCell12.DataBindings.Add("Text", this.DataSource, "Procedures." + Model.Procedures.PRO_Proceduredescription);
            //this.xrTableCellNO.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_ProceduresNo);
            this.xrTableCellMachine.DataBindings.Add("Text", this.DataSource, Model.PronoteProceduresDetail.PRO_Machine);
        }

        private void RO2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.pronoteHeader == null)
                return;

            this.pronoteHeader.DetailProcedures = this.pronoteProceduresDetailManager.GetPronotedetailsMaterialByHeaderId(this.pronoteHeader);

            if (this.pronoteHeader.DetailProcedures != null)
            {
                foreach (Model.PronoteProceduresDetail detail in this.pronoteHeader.DetailProcedures)
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
            }

            this.DataSource = this.pronoteHeader.DetailProcedures;
        }

    }
}
