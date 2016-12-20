using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.BL;
namespace Book.UI.produceManager.PronoteHeader
{
    public partial class PlanForm : DevExpress.XtraEditors.XtraForm
    {
        Model.MRSHeader _mrsHeader;
        IList<Model.MRSdetails> detail;
        BL.MRSdetailsManager mRShetailsManager = new Book.BL.MRSdetailsManager();
        BL.MPSdetailsManager mPSdetailsManager = new Book.BL.MPSdetailsManager();
        Model.MPSdetails _mpsDetails;
        BL.PronoteHeaderManager pronoteHeaderManager = new Book.BL.PronoteHeaderManager();
        Model.PronoteHeader pronoteHeader = new Book.Model.PronoteHeader();
        BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.BomComponentInfoManager bomComponentinfoManager = new BomComponentInfoManager();
        ManProcedureManager manProcedureManager = new ManProcedureManager();
        TechonlogyHeaderManager techonlogyHeaderManager = new TechonlogyHeaderManager();
        public PlanForm()
        {
            InitializeComponent();
        }
        public PlanForm(Model.MRSHeader mrsHeader)
            : this()
        {
            _mrsHeader = mrsHeader;   
            
        }
        private void PlanForm_Load(object sender, EventArgs e)
        {
            //detail = this.mRShetailsManager.Select(this._mpsDetails.MPSheaderId, ((Int32)global::Helper.ProductType.HomeMade).ToString()); ;//this._mrsHeader参数
            this.bindingSourceMPS.DataSource = this.mPSdetailsManager.Select();
            this.bindingSource1.DataSource = detail;           
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.MRSdetails> details = this.bindingSource1.DataSource as IList<Model.MRSdetails>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
          //  Model.MPSheader mpsHeader = this.mPSheaderManager.Get(details[e.ListSourceRowIndex].MRSHeader.MPSheaderId);
            switch (e.Column.Name)
            {
                case "gridColumnProductId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
                case "gridColumnDate":
                    if (detail == null) return;
                    e.DisplayText =details[e.ListSourceRowIndex].MRSHeader.MRSstartdate==null?"": details[e.ListSourceRowIndex].MRSHeader.MRSstartdate.Value.ToShortDateString();
                    break;
                case "gridColumnGuiGe":
                    if (detail == null) return;
                    e.DisplayText =detail==null ? "" : detail.ProductSpecification;
                    break;

            }
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            Model.MRSdetails mrsdetails =this.bindingSource1.Current as Model.MRSdetails;
            if(mrsdetails==null) return;
            EditForm f =new EditForm();
            if(f.ShowDialog(this)!=DialogResult.OK)
                return ;
        }

        private void bindingSourceMPS_CurrentChanged(object sender, EventArgs e)
        {
            this._mpsDetails=this.bindingSourceMPS.Current as Model.MPSdetails;
            if (this._mpsDetails == null) return;
            detail = this.mRShetailsManager.Select(this._mpsDetails.MPSheaderId, ((Int32)global::Helper.ProductType.HomeMade).ToString(), ((Int32)global::Helper.ProductType.Package).ToString(), ((Int32)global::Helper.ProductType.HomeMadeProcee).ToString());
           this.bindingSource1.DataSource = detail;

           if (this._mpsDetails.IsPronoteHeader == null || !(bool)this._mpsDetails.IsPronoteHeader )
           this.barButtonItem1.Enabled = true ;
           else
               this.barButtonItem1.Enabled = false ;
           //this.gridControl1.RefreshDataSource();
        }
 
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            foreach (Model.MRSdetails _mrsdetail in detail)
            {
               //this.pronoteHeader.Details = new List<Model.Pronotedetails>();
                pronoteHeader.DetailsMaterial = new List<Model.PronotedetailsMaterial>();
                this.pronoteHeader.DetailProcedures = new List<Model.PronoteProceduresDetail>();
                pronoteHeader.PronoteHeaderID = pronoteHeaderManager.GetId();
                pronoteHeader.Employee0 = _mrsdetail.MRSHeader.Employee0;
                if (pronoteHeader.Employee0 != null)
                    pronoteHeader.Employee0Id = pronoteHeader.Employee0.EmployeeId;
                pronoteHeader.PronoteDate = DateTime.Now;
                pronoteHeader.MRSHeaderId = _mrsdetail.MRSHeaderId;
         
                this.pronoteHeader.Product = _mrsdetail.Product;
                this.pronoteHeader.ProductId = _mrsdetail.ProductId;
                this.pronoteHeader.DetailsSum = _mrsdetail.MRSdetailssum - (_mrsdetail.MRSHasSingleSum == null ? 0 : _mrsdetail.MRSHasSingleSum);
                this.pronoteHeader.ProductUnit = _mrsdetail.ProductUnit;
                this.pronoteHeader.Employee0 = BL.V.ActiveOperator.Employee;
                this.pronoteHeader.Employee1 = BL.V.ActiveOperator.Employee;
              
             //   Model.Pronotedetails pronotedetails = new Book.Model.Pronotedetails();
             //   pronotedetails.PronotedetailsID = Guid.NewGuid().ToString();                            
             ////   pronotedetails.MPSDetailId = _mrsdetail.MRSdetailsId;     

             //   pronotedetails.DetailsSum = _mrsdetail.MRSdetailssum - (_mrsdetail.MRSHasSingleSum == null ? 0 : _mrsdetail.MRSHasSingleSum);
             //   pronotedetails.QuantityTemp = pronotedetails.DetailsSum;
             //   pronotedetails.Product = _mrsdetail.Product;
             //   pronotedetails.ProductId = _mrsdetail.ProductId;
             //   pronotedetails.MPSQuantity = _mrsdetail.MRSdetailssum;
             //   pronotedetails.ProductStock = _mrsdetail.Product.StocksQuantity;
             //   pronotedetails.PronoteHeader = this.pronoteHeader;
             //   pronotedetails.MRSHeaderId = _mrsdetail.MRSHeaderId;
             //   pronotedetails.MRSdetailsId = _mrsdetail.MRSdetailsId;
             //   pronotedetails.ProductUnit = _mrsdetail.ProductUnit;
             //   pronotedetails.InvoiceXOId = this.mPSdetailsManager.Get(_mrsdetail.MPSdetailsId).InvoiceXOId;
             //   pronotedetails.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;
             //   pronotedetails.MPSheaderId = _mrsdetail.MPSheaderId; // l.MRSHeader.MPSheaderId;

             //   this.pronoteHeader.Details.Add(pronotedetails);
                //
              
                //if (pronotedetails.Product.IsCustomerProduct == true)
                //{
                //    bomP = new BL.BomParentPartInfoManager().Get(this.productManager.Get(pronotedetails.Product.CustomerBeforeProductId), pronotedetails.Product.Customer);
                //    if (bomP == null)
                //        bomP = new BL.BomParentPartInfoManager().Get(this.productManager.Get(pronotedetails.Product.CustomerBeforeProductId));
                //}

                //else
                Model.BomParentPartInfo bomP = new BL.BomParentPartInfoManager().Get(this.pronoteHeader.Product);

                //配料
                foreach (Model.BomComponentInfo component in bomComponentinfoManager.Select(bomP))
                {
                    Model.PronotedetailsMaterial materials = new Model.PronotedetailsMaterial();
                    materials.PronotedetailsMaterialId = Guid.NewGuid().ToString();
                    materials.Product = component.Product;
                    materials.ProductId = component.ProductId;
                    materials.PronoteHeader = this.pronoteHeader;
                    materials.PronoteHeaderID = this.pronoteHeader.PronoteHeaderID;
                    materials.PronoteQuantity = component.UseQuantity * this.pronoteHeader.DetailsSum;

                    materials.MPSQuantity = _mrsdetail.MRSdetailssum * component.UseQuantity;
                    //materials.Pronotedetails = pronotedetails;
                    //materials.PronotedetailsID = pronotedetails.PronotedetailsID;
                    materials.MPSheaderId = _mrsdetail.MPSheaderId;
                    materials.MRSHeaderId = _mrsdetail.MRSHeaderId;
                    materials.MRSdetailsId = _mrsdetail.MRSdetailsId;
                    pronoteHeader.DetailsMaterial.Add(materials);
                }

                //工序
                if (manProcedureManager.Select(bomP,_mrsdetail.Customer) != null)
                {
                    Model.TechonlogyHeader techonlogyHeader = techonlogyHeaderManager.Get(manProcedureManager.Select(bomP, _mrsdetail.Customer).TechonlogyHeaderId);
                    Model.PronoteProceduresDetail pronoteProceduresDetail = null;
                    foreach (Model.Technologydetails technologydetails in (new BL.TechnologydetailsManager().Select(techonlogyHeader)))
                    {
                        pronoteProceduresDetail = new Book.Model.PronoteProceduresDetail();
                        pronoteProceduresDetail.PronoteProceduresDetailId = Guid.NewGuid().ToString();
                        pronoteProceduresDetail.Procedures = technologydetails.Procedures;
                        //  pronoteProceduresDetail.PronoteMachine = technologydetails.Procedures.PronoteMachine;
                        pronoteProceduresDetail.ProceduresNo = technologydetails.TechnologydetailsNo;
                        if (technologydetails.Procedures != null)
                            pronoteProceduresDetail.ProceduresId = technologydetails.Procedures.ProceduresId;
                        pronoteProceduresDetail.WorkHouseId = technologydetails.Procedures.WorkHouseId;
                        this.pronoteHeader.DetailProcedures.Add(pronoteProceduresDetail);
                       
                    }

                }
                this.pronoteHeaderManager.Insert(this.pronoteHeader);



              
                //Model.
                //_mrsdetail.MRSHeader.MPSheaderId
                //this.mPSdetailsManager.Update();
               // this.gridControl2.RefreshDataSource();
                       
            }
            this._mpsDetails.IsPronoteHeader = true;
                this.mPSdetailsManager.Update(this._mpsDetails);
            this.barButtonItem1.Enabled = false;
            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.MPSdetails> details = this.bindingSourceMPS.DataSource as IList<Model.MPSdetails>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            //  Model.MPSheader mpsHeader = this.mPSheaderManager.Get(details[e.ListSourceRowIndex].MRSHeader.MPSheaderId);
            switch (e.Column.Name)
            {
                case "gridColumnProductIds":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
                case "gridColumnMpsDate": 
                    if (detail == null) return;
                    e.DisplayText = details[e.ListSourceRowIndex].MPSheader.MPSStartDate == null ? "" : details[e.ListSourceRowIndex].MPSheader.MPSStartDate.Value.ToShortDateString();
                    break;
                case "gridColumnMPSEndDate": 
                    if (detail == null) return;
                    e.DisplayText = details[e.ListSourceRowIndex].MPSheader.MPSEndDate == null ? "" : details[e.ListSourceRowIndex].MPSheader.MPSEndDate.Value.ToShortDateString();
                    break;
                //case "gridColumnGuiGe":
                //    if (detail == null) return;
                //    e.DisplayText = detail == null ? "" : detail.ProductSpecification;
                //    break;
                case "gridColumnMPS":
                    if (detail == null) return;
                    e.DisplayText = details[e.ListSourceRowIndex].MPSheaderId;
                    break;
                case "gridColumnCustomerPro":
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.CustomerProductName;
                    break;

            }
        }
    }
}