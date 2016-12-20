using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
namespace Book.UI.produceManager.MRSHeader
{
    public partial class RO2 : DevExpress.XtraReports.UI.XtraReport
    {
        public System.Collections.Generic.IList<Model.BomComponentInfo> _bomcomDetail = new System.Collections.Generic.List<Model.BomComponentInfo>();
        public  Model.BomParentPartInfo _bomparent = new Book.Model.BomParentPartInfo();
        public Model.BomParentPartInfo _bomparents = new Book.Model.BomParentPartInfo();
        public BL.BomParentPartInfoManager BomparentManager = new Book.BL.BomParentPartInfoManager();
        public Model.BomComponentInfo b = new Book.Model.BomComponentInfo();
        public BL.BomComponentInfoManager BomcomManager = new Book.BL.BomComponentInfoManager();
        public RO2(System.Collections.Generic.IList<Model.MPSdetails> mpsdetail, DateTime  dateStart,DateTime dateEnd)
        {
            InitializeComponent();
            this.xrLabelDate.Text = "自" + dateStart.ToShortDateString() + "至" + dateEnd.ToShortDateString();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.MRSDetails;
            this.xrLabelNowDate.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (mpsdetail.Count == 0) return;

            foreach (Model.MPSdetails MPSdetails in mpsdetail)
            {
                this._bomparent = this.BomparentManager.Get(MPSdetails.Product);
                this._bomparents = this._bomparent;
                if (_bomparent != null)
                    this._bomcomDetail = this.BomcomManager.Select(_bomparent);
                IList<Model.BomComponentInfo> a = null;
                if (this._bomcomDetail.Count != 0)
                {
                    for (int i = 0; i < this._bomcomDetail.Count; i++)
                    // foreach(Model.BomComponentInfo com in this._bomcomDetail)
                    {    //在物料中查询 是否 存在此子件
                        this._bomparent = this.BomparentManager.Get(_bomcomDetail[i].Product);
                        _bomcomDetail[i].Customer = MPSdetails.Customer;
                        _bomcomDetail[i].MPSheader = MPSdetails.MPSheader;
                        _bomcomDetail[i].InvoiceXOId = MPSdetails.InvoiceXOId;
                        if (_bomcomDetail[i].MPSheader != null)
                            _bomcomDetail[i].MPSheaderId = _bomcomDetail[i].MPSheader.MPSheaderId;

                        if (this._bomparent != null)
                        {
                            a = this.BomcomManager.Select(this._bomparent);
                            foreach (Model.BomComponentInfo bom in a)
                            {
                                bom.Jibie = _bomcomDetail[i].Jibie + 1;

                                bom.UseQuantity = _bomcomDetail[i].UseQuantity * bom.UseQuantity;
                                bom.Customer = MPSdetails.Customer;

                                bom.MPSheader = MPSdetails.MPSheader;



                                if (MPSdetails.MPSheader != null)
                                    bom.MPSheaderId = MPSdetails.MPSheader.MPSheaderId;
                                bom.Product.Id = this._bomparent.Product.Id + "/" + bom.Product.Id;
                                this._bomcomDetail.Add(bom);

                            }

                            a.Clear();
                        }
                    }
                }
            }




            this.DataSource = this._bomcomDetail;

            this.xrTableProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellGuige.DataBindings.Add("Text", this.DataSource, "Product.ProductSpecification");
            this.xrTableCellMPSheadid.DataBindings.Add("Text", this.DataSource,"MPSheader.MPSheaderId");
            this.xrTableMRSstartdate.DataBindings.Add("Text", this.DataSource, "MPSheader.MPSStartDate", "{0:yyyy-MM-dd}");
            this.xrTableMRSdetailssum.DataBindings.Add("Text", this.DataSource, "UseQuantity");
              this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, "Unit");
           
            this.xrTableCustomer.DataBindings.Add("Text", this.DataSource,"Customer.CustomerShortName");

            this.xrTableXO.DataBindings.Add("Text", this.DataSource, "InvoiceXOId");  
        }

    }
}
