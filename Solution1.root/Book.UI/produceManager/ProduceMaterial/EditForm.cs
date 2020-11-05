using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;
using Book.UI.produceManager.MRSHeader;


namespace Book.UI.produceManager.ProduceMaterial
{


    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010   咸阳飛馳軟件有限公司
//                             版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2010-3-18
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        //public static IList<Model.Pronotedetails> _pronotedetails = new List<Model.Pronotedetails>();
        Model.ProduceMaterial _produceMaterial = new Book.Model.ProduceMaterial();
        Model.ProduceMaterialdetails _TempProduceMaterialdetails = new Book.Model.ProduceMaterialdetails();
        private bool _TempIsChuCangupdate = false;
        BL.ProduceMaterialManager produceMaterialManager = new Book.BL.ProduceMaterialManager();
        BL.BomPackageDetailsManager bomPackageDetailsManager = new Book.BL.BomPackageDetailsManager();

        BL.ProduceMaterialdetailsManager produceMaterialdetailsManager = new Book.BL.ProduceMaterialdetailsManager();

        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();

        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        BL.PronotedetailsMaterialManager _pronotedetailMaterialManager = new Book.BL.PronotedetailsMaterialManager();
        BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        BL.PronoteHeaderManager PronoteHeaderManager = new BL.PronoteHeaderManager();
        BL.MRSdetailsManager mRSdetailsManager = new Book.BL.MRSdetailsManager();
        string invoiceId = string.Empty;
        int LastFlag = 0;

        public EditForm()
        {
            InitializeComponent();
            this.bindingSourceDetails.CurrentChanged += new EventHandler(bindingSourceDetails_CurrentChanged);
            this.requireValueExceptions.Add(Model.ProduceMaterial.PRO_ProduceMaterialID, new AA(Properties.Resources.RequireDataForId, this.textEditProduceMaterialID));
            this.requireValueExceptions.Add(Model.ProduceMaterial.PRO_WorkHouseId, new AA(Properties.Resources.WorkHouse, this.newChooseWorkHorseId));
            this.invalidValueExceptions.Add(Model.ProduceMaterial.PRO_ProduceMaterialID, new AA(Properties.Resources.EntityExists, this.textEditProduceMaterialID));
            //this.invalidValueExceptions.Add(Model.ProduceMaterialdetails.PRO_Materialprocesedsum, new AA("保存失敗.不得對已有出倉詳細,進行修改.請重新選取本單,以恢復原樣數據", this.textEditProduceMaterialID));

            this.newChooseEmployee1.Choose = new ChooseEmployee();
            //this.newChooseEmployee1.Choose = new ChooseEmployee();
            this.newChooseWorkHorseId.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            // this.newChooseContorlDepot.Choose = new Invoices.ChooseDepot();
            this.EmpAudit.Choose = new ChooseEmployee();
            this.newChooseEmployee0.Choose = new ChooseEmployee();

            this.bindingSourceHandbookId.DataSource = (new BL.BGHandbookIdSetManager()).SelectHasUsing();
            this.action = "view";
        }

        public EditForm(Model.ProduceMaterial produceMaterial)
            : this()
        {

            this._produceMaterial = produceMaterial;
            this._produceMaterial.Details = this.produceMaterialdetailsManager.Select(produceMaterial);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(string id)
            : this()
        {
            this._produceMaterial = this.produceMaterialManager.GetDetails(id);
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        private Model.PronoteHeader _pronoteHeader;

        public EditForm(Model.PronoteHeader pronoteHeader)
            : this()
        {
            this._pronoteHeader = new Book.Model.PronoteHeader();
            this._pronoteHeader = pronoteHeader;
            this.action = "insert";
            if (this.action == "view")
                LastFlag = 1;
        }

        protected override bool SetColumnNumber()
        {
            return true;
        }

        public EditForm(Model.ProduceMaterial produceMaterial, string action)
            : this()
        {
            this._produceMaterial = produceMaterial;
            this._produceMaterial.Details = this.produceMaterialdetailsManager.Select(produceMaterial);
            this.action = action;
            if (this.action == "view")
                LastFlag = 1;
        }

        int tag = 0;
        public EditForm(IList<Model.MRSdetails> List)
            : this()
        {
            this.action = "insert";
            this.AddNew();
            this.tag = 1;

            string xoid = string.Empty;

            if (List[0].MPSheaderId != null)
            {
                xoid = new BL.MPSheaderManager().Get(List[0].MPSheaderId).InvoiceXOId;
            }
            else if (List[0].MRSHeader.MPSheaderId != null)
                xoid = new BL.MPSheaderManager().Get(List[0].MRSHeader.MPSheaderId) == null ? null : new BL.MPSheaderManager().Get(List[0].MRSHeader.MPSheaderId).InvoiceXOId;
            this.comboBoxEdit1.SelectedIndex = 1;
            this.textEditPronoteHeaderID.EditValue = List[0].MRSHeaderId;
            if (!string.IsNullOrEmpty(xoid))
            {
                Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(xoid);
                if (invoiceXO != null)
                {
                    this.textEditCustomerXOId.Text = invoiceXO.CustomerInvoiceXOId;
                    this.textEditPiHao.Text = invoiceXO.CustomerLotNumber;
                    //this.calcEditInvoiceSum.Text = invoiceXO.Details.Where(w => w.ProductId == List[0].MadeProductId).ToList().First().InvoiceXODetailQuantity.Value.ToString("f0");
                }
                this._produceMaterial.InvoiceXOId = xoid;
            }
            else
                this.textEditCustomerXOId.Text = string.Empty;
            //this._produceMaterial.WorkHouse = List[0].WorkHouseNext;
            //this.textEditProduct.Text = string.IsNullOrEmpty(List[0].Product.CustomerProductName) ? List[0].Product.ProductName : List[0].Product.ProductName + "{" + List[0].Product.CustomerProductName + "}";
            this._produceMaterial.Details.Clear();

            IList<Model.ProduceMaterialdetails> dtlist = new List<Model.ProduceMaterialdetails>();
            Model.ProduceMaterialdetails produceMaterialdetails;
            for (int i = 0; i < List.Count; i++)
            {
                Model.MRSdetails mRSdetails = List[i];

                produceMaterialdetails = new Book.Model.ProduceMaterialdetails();
                //produceMaterialdetails.Inumber = this._produceMaterial.Details.Count + 1;
                produceMaterialdetails.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                produceMaterialdetails.MRSHeaderId = mRSdetails.MRSHeaderId;
                produceMaterialdetails.MRSdetailsId = mRSdetails.MRSdetailsId;
                produceMaterialdetails.Product = mRSdetails.Product;
                produceMaterialdetails.ProductId = mRSdetails.Product.ProductId;
                produceMaterialdetails.ProductStock = mRSdetails.Product.StocksQuantity;
                produceMaterialdetails.ProductUnit = mRSdetails.ProductUnit;

                if (!mRSdetails.Product.ProduceMaterialDistributioned.HasValue)
                    mRSdetails.Product.ProduceMaterialDistributioned = 0;
                if (!mRSdetails.Product.OtherMaterialDistributioned.HasValue)
                    mRSdetails.Product.OtherMaterialDistributioned = 0;
                produceMaterialdetails.Distributioned = mRSdetails.Product.ProduceMaterialDistributioned + mRSdetails.Product.OtherMaterialDistributioned;
                produceMaterialdetails.ProductSpecification = mRSdetails.Product.ProductSpecification;
                //produceMaterialdetails.NextWorkHouse = mRSdetails.WorkHouseNext;
                //produceMaterialdetails.NextWorkHouseId = mRSdetails.WorkHouseNextId;
                //if (mRSdetails.MRSHeader.SourceType == "1")             //外购
                //    produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailsQuantity;
                //else
                //{
                //    if (mRSdetails.Product.StocksQuantity - mRSdetails.Product.ProduceMaterialDistributioned > mRSdetails.MRSdetailsQuantity)
                //    {
                //        if (string.IsNullOrEmpty(mRSdetails.Product.SunhaoRage))
                //            produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailsQuantity;
                //        else
                //        {
                //            string[] sunhaolist = mRSdetails.Product.SunhaoRage.Split(',');
                //            string[] numlist1;
                //            string[] numlist2;
                //            string[] numlist3;
                //            if (sunhaolist != null && sunhaolist.Length > 0)
                //            {
                //                numlist1 = sunhaolist[0].Split('/');
                //                numlist2 = sunhaolist[1].Split('/');
                //                numlist3 = sunhaolist[2].Split('/');
                //                if (mRSdetails.MRSdetailsQuantity >= Convert.ToDouble(numlist1[0]) && mRSdetails.MRSdetailsQuantity <= Convert.ToDouble(numlist1[1]))
                //                    produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailsQuantity * (1 + Convert.ToDouble(numlist1[2]) / 100);
                //                else if (mRSdetails.MRSdetailsQuantity >= Convert.ToDouble(numlist2[0]) && mRSdetails.MRSdetailsQuantity <= Convert.ToDouble(numlist2[1]))
                //                    produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailsQuantity * (1 + Convert.ToDouble(numlist2[2]) / 100);
                //                else if (mRSdetails.MRSdetailsQuantity >= Convert.ToDouble(numlist3[0]) && mRSdetails.MRSdetailsQuantity <= Convert.ToDouble(numlist3[1]))
                //                    produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailsQuantity * (1 + Convert.ToDouble(numlist3[2]) / 100);
                //            }
                //        }
                //    }
                //    else
                //        produceMaterialdetails.Materialprocessum = Convert.ToDouble(mRSdetails.Product.StocksQuantity) - Convert.ToDouble(mRSdetails.Product.ProduceMaterialDistributioned);
                //}
                produceMaterialdetails.NextWorkHouse = mRSdetails.WorkHouseNext;
                produceMaterialdetails.NextWorkHouseId = mRSdetails.WorkHouseNextId;

                if (mRSdetails.MRSHeader.SourceType == "1")
                    produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailsQuantity;
                else
                    produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailssum;

                //produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailssum;     //修改：领料数量=需求数量
                //produceMaterialdetails.Materialprocesedsum = PronoteMaterial.DetailsSum;                   
                produceMaterialdetails.ProduceMaterialID = this._produceMaterial.ProduceMaterialID;
                produceMaterialdetails.MPSDetailsSum = mRSdetails.MRSdetailsQuantity;
                produceMaterialdetails.HandbookProductId = mRSdetails.HandbookProductId;
                produceMaterialdetails.HandbookId = mRSdetails.HandbookId;
                //produceMaterialdetails.InvoiceXOId = this.produceMaterial.pro;
                //produceMaterialdetails.InvoiceXODetailId = Pronotedetails.InvoiceXODetailId;
                dtlist.Add(produceMaterialdetails);

            }
            int count = 1;
            this._produceMaterial.Details = (from p in dtlist
                                             group p by new { p.ProductId, p.ProductUnit } into pm
                                             select new Model.ProduceMaterialdetails()
                                             {
                                                 ProduceMaterialdetailsID = pm.First().ProduceMaterialdetailsID,
                                                 Inumber = count++,
                                                 MRSHeaderId = pm.First().MRSHeaderId,
                                                 MRSdetailsId = pm.First().MRSdetailsId,
                                                 Product = pm.First().Product,
                                                 ProductId = pm.First().ProductId,
                                                 ProductStock = pm.First().ProductStock,
                                                 ProductUnit = pm.First().ProductUnit,
                                                 Distributioned = pm.First().Distributioned,
                                                 ProductSpecification = pm.First().Product.ProductSpecification,
                                                 Materialprocessum = (from m in pm select m.Materialprocessum).Sum(),
                                                 ProduceMaterialID = this._produceMaterial.ProduceMaterialID,
                                                 MPSDetailsSum = pm.First().MPSDetailsSum,
                                                 NextWorkHouseId = pm.First().NextWorkHouseId,
                                                 NextWorkHouse = pm.First().NextWorkHouse,
                                                 HandbookProductId = pm.First().HandbookProductId,
                                                 HandbookId = pm.First().HandbookId
                                             }).ToList<Model.ProduceMaterialdetails>();
            this.bindingSourceDetails.DataSource = this._produceMaterial.Details;

            this._produceMaterial.SourceType = this.comboBoxEdit1.SelectedIndex;
            this._produceMaterial.InvoiceId = this.textEditPronoteHeaderID.Text;
        }

        public EditForm(IList<Model.PronotedetailsMaterial> list)
            : this()
        {
            this.action = "insert";
            this.AddNew();
            this.tag = 1;

            this.calcEditInvoiceSum.Value = 0;
            string xoid = list[0].PronoteHeader.InvoiceXOId;
            this.textEditPronoteHeaderID.Text = list[0].PronoteHeaderID;
            this.textEditProduct.Text = string.IsNullOrEmpty(list[0].Product.CustomerProductName) ? list[0].Product.ProductName : list[0].Product.ProductName + "{" + list[0].Product.CustomerProductName + "}";
            this._produceMaterial.InvoiceXOId = xoid;
            this.comboBoxEdit1.SelectedIndex = 0;
            //this.com
            if (!string.IsNullOrEmpty(xoid))
            {
                Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(xoid);
                if (invoiceXO != null)
                {
                    this.textEditCustomerXOId.Text = invoiceXO.CustomerInvoiceXOId;

                    Model.MRSdetails mrsdetail = new BL.MRSdetailsManager().Get(list[0].PronoteHeader.MRSdetailsId);
                    if (mrsdetail != null && invoiceXO.Details != null && invoiceXO.Details.Count > 0)
                    {
                        foreach (Model.InvoiceXODetail detail in invoiceXO.Details)
                        {
                            if (detail.ProductId == mrsdetail.MadeProductId)
                                this.calcEditInvoiceSum.Text = detail.InvoiceXODetailQuantity.Value.ToString("f0");
                        }
                    }
                }
            }
            else
                this.textEditCustomerXOId.Text = string.Empty;
            this._produceMaterial.Details.Clear();
            foreach (Model.PronotedetailsMaterial PronoteMaterial in list)
            {
                Model.ProduceMaterialdetails produceMaterialdetails = new Book.Model.ProduceMaterialdetails();
                produceMaterialdetails.Inumber = this._produceMaterial.Details.Count + 1;
                produceMaterialdetails.HandbookId = PronoteMaterial.PronoteHeader.HandbookId;
                produceMaterialdetails.HandbookProductId = PronoteMaterial.PronoteHeader.HandbookProductId;
                produceMaterialdetails.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                produceMaterialdetails.PronoteHeaderID = PronoteMaterial.PronoteHeaderID;
                produceMaterialdetails.PronotedetailsID = PronoteMaterial.PronotedetailsMaterialId;
                if (PronoteMaterial.Product != null)
                {
                    produceMaterialdetails.Product = PronoteMaterial.Product;
                    produceMaterialdetails.ProductId = PronoteMaterial.Product.ProductId;
                    produceMaterialdetails.ProductStock = PronoteMaterial.Product.StocksQuantity;

                    if (!PronoteMaterial.Product.ProduceMaterialDistributioned.HasValue)
                        PronoteMaterial.Product.ProduceMaterialDistributioned = 0;
                    if (!PronoteMaterial.Product.OtherMaterialDistributioned.HasValue)
                        PronoteMaterial.Product.OtherMaterialDistributioned = 0;
                    produceMaterialdetails.Distributioned = PronoteMaterial.Product.ProduceMaterialDistributioned + PronoteMaterial.Product.OtherMaterialDistributioned;
                    produceMaterialdetails.ProductSpecification = PronoteMaterial.Product.ProductSpecification;
                }
                produceMaterialdetails.ProductUnit = PronoteMaterial.ProductUnit;
                produceMaterialdetails.Materialprocessum = PronoteMaterial.PronoteQuantity;
                //produceMaterialdetails.Materialprocesedsum = PronoteMaterial.DetailsSum;

                produceMaterialdetails.HandbookProductId = PronoteMaterial.PronoteHeader.HandbookProductId;
                produceMaterialdetails.HandbookId = PronoteMaterial.PronoteHeader.HandbookId;

                produceMaterialdetails.ProduceMaterialID = this._produceMaterial.ProduceMaterialID;
                //produceMaterialdetails.InvoiceXOId = this.produceMaterial.pro;
                //produceMaterialdetails.InvoiceXODetailId = Pronotedetails.InvoiceXODetailId;
                this._produceMaterial.Details.Add(produceMaterialdetails);

            }
            this._produceMaterial.SourceType = this.comboBoxEdit1.SelectedIndex;
            this._produceMaterial.InvoiceId = this.textEditPronoteHeaderID.Text;
        }

        protected override string AuditKeyId()
        {
            return Model.ProduceMaterial.PRO_ProduceMaterialID;
        }

        protected override int AuditState()
        {
            return this._produceMaterial.AuditState.HasValue ? this._produceMaterial.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "ProduceMaterial" + "," + this._produceMaterial.ProduceMaterialID;
        }

        protected override void Save()
        {
            //if (this._TempIsChuCangupdate)
            //    throw new Helper.InvalidValueException(Model.ProduceMaterialdetails.PRO_Materialprocesedsum);
            this._produceMaterial.InvoiceId = this.textEditPronoteHeaderID.Text;
            this._produceMaterial.ProduceMaterialID = this.textEditProduceMaterialID.Text;
            this._produceMaterial.ProduceMaterialdesc = this.textEditProduceMaterialdesc.Text;
            this._produceMaterial.SourceType = this.comboBoxEdit1.SelectedIndex;
            this._produceMaterial.WorkHouse = this.newChooseWorkHorseId.EditValue as Model.WorkHouse;
            this._produceMaterial.AuditState = this.saveAuditState;
            if (this._produceMaterial.WorkHouse != null)
            {
                this._produceMaterial.WorkHouseId = this._produceMaterial.WorkHouse.WorkHouseId;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProduceMaterialDate.DateTime, new DateTime()))
            {
                this._produceMaterial.ProduceMaterialDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._produceMaterial.ProduceMaterialDate = this.dateEditProduceMaterialDate.DateTime;
            }
            this._produceMaterial.Employee1 = (this.newChooseEmployee1.EditValue as Model.Employee);
            if (this._produceMaterial.Employee1 != null)
            {
                this._produceMaterial.Employee1Id = this._produceMaterial.Employee1.EmployeeId;
            }
            //this.produceMaterial.Employee1 = (this.newChooseEmployee1.EditValue as Model.Employee);
            //if (this.produceMaterial.Employee1 != null)
            //{
            //    this.produceMaterial.Employee1Id = this.produceMaterial.Employee1.EmployeeId;
            //}

            if (this.EmpAudit.EditValue != null)
            {
                this._produceMaterial.Employee2 = this.EmpAudit.EditValue as Model.Employee;
                this._produceMaterial.Employee2Id = this._produceMaterial.Employee2.EmployeeId;
            }
            if (this.newChooseEmployee0.EditValue != null)
            {
                this._produceMaterial.Employee0 = this.newChooseEmployee0.EditValue as Model.Employee;
                this._produceMaterial.Employee0Id = this._produceMaterial.Employee0.EmployeeId;
            }
            //this._produceMaterial.InvoiceXO = textEditXOId.Text;


            //if (this.newChooseContorlDepot.EditValue != null)
            //{
            //    this.produceMaterial.Depot = this.newChooseContorlDepot.EditValue as Model.Depot;
            //    this.produceMaterial.DepotId = this.produceMaterial.Depot.DepotId;
            //}

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            //int flag = 0;
            //StringBuilder str = new StringBuilder();
            //Model.Product product = new Book.Model.Product();
            //foreach (Model.ProduceMaterialdetails detail in _produceMaterial.Details)
            //{
            //    if (string.IsNullOrEmpty(detail.ProductId)) continue;

            //    product = this.productManager.getStockYFPByProduct(detail.ProductId);
            //    if (detail.Materialprocessum + product.ProduceMaterialDistributioned > product.StocksQuantity)
            //    {
            //        flag = 1;
            //        str.Append(detail.Product.ProductName + " ,");
            //    }
            //}
            //this.flagSave = false;
            //if (flag == 1)
            //{
            //    if (MessageBox.Show(str.ToString() + "領料數量大於未分配數量,是否繼續", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            //    {
            //        this.flagSave = true;
            //        throw new Helper.MessageValueException("");
            //    }
            //}

            switch (this.action)
            {
                case "insert":
                    this.produceMaterialManager.Insert(this._produceMaterial);
                    break;
                case "update":
                    this.produceMaterialManager.Update(this._produceMaterial);
                    break;
            }

        }

        protected override void Delete()
        {
            if (this.produceMaterialManager.IsDepotOut(this._produceMaterial.ProduceMaterialID))
            {
                MessageBox.Show("已出倉，請勿删除！", this.Text, MessageBoxButtons.OK);
                return;
            }

            if (this._produceMaterial == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            // try
            //{
            this.produceMaterialManager.Delete(this._produceMaterial);
            this._produceMaterial = this.produceMaterialManager.GetNext(this._produceMaterial);
            if (this._produceMaterial == null)
                this._produceMaterial = this.produceMaterialManager.GetLast();


        }

        public override void Refresh()
        {

            if (this._produceMaterial == null)
            {
                this.action = "insert";
                this.AddNew();
            }
            else
            {
                if (this.action != "insert")
                {
                    this._produceMaterial = this.produceMaterialManager.GetDetails(_produceMaterial.ProduceMaterialID);
                }
            }

            if (this.produceMaterialManager.IsDepotOut(this._produceMaterial.ProduceMaterialID) && this.action == "update")
            {
                MessageBox.Show("已出仓，请勿修改！", this.Text, MessageBoxButtons.OK);
                this.action = "view";
            }

            this.textEditProduceMaterialID.Text = this._produceMaterial.ProduceMaterialID;
            this.textEditProduceMaterialdesc.Text = this._produceMaterial.ProduceMaterialdesc;
            this.newChooseWorkHorseId.EditValue = this._produceMaterial.WorkHouse;
            this.comboBoxEdit1.SelectedIndex = this._produceMaterial.SourceType.HasValue ? this._produceMaterial.SourceType.Value : -1;
            //if (this._produceMaterial.SourceType == null && !string.IsNullOrEmpty(this._produceMaterial.InvoiceId))
            //{
            //    this.comboBoxEdit1.SelectedIndex = 0;
            //}

            if (global::Helper.DateTimeParse.DateTimeEquls(this._produceMaterial.ProduceMaterialDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProduceMaterialDate.EditValue = null;
            }
            else
            {
                this.dateEditProduceMaterialDate.EditValue = this._produceMaterial.ProduceMaterialDate;
            }
            this.newChooseEmployee1.EditValue = this._produceMaterial.Employee1;
            //this.newChooseEmployee1.EditValue = this.produceMaterial.Employee1;
            this.EmpAudit.EditValue = this._produceMaterial.AuditEmp;
            this.textEditAuditState.Text = this.GetAuditName(this._produceMaterial.AuditState);
            //this.textEditInvoiceXO.Text = this.produceMaterial.InvoiceXO;
            //this.newChooseContorlDepot.EditValue = this.produceMaterial.Depot;
            this.textEditPronoteHeaderID.Text = this._produceMaterial.InvoiceId;
            this.newChooseEmployee0.EditValue = this._produceMaterial.Employee0;
            if (!string.IsNullOrEmpty(this._produceMaterial.InvoiceId))
            {
                Model.PronoteHeader pronoteHeader = this.PronoteHeaderManager.Get(this._produceMaterial.InvoiceId);
                if (pronoteHeader != null)
                {
                    if (pronoteHeader.Product != null)
                        this.textEditProduct.Text = string.IsNullOrEmpty(pronoteHeader.Product.CustomerProductName) ? pronoteHeader.Product.ProductName : pronoteHeader.Product.ProductName + "{" + pronoteHeader.Product.CustomerProductName + "}";

                    // this.textEditXOId.Text = pronoteHeader.InvoiceXOId;
                    this.calcEditInvoiceSum.EditValue = pronoteHeader.InvoiceXODetailQuantity;
                }
                else
                {
                    this.textEditProduct.EditValue = null;
                    this.calcEditInvoiceSum.EditValue = null;
                }
            }
            else
            {
                this.textEditProduct.EditValue = null;
                this.calcEditInvoiceSum.EditValue = null;
            }
            if (!string.IsNullOrEmpty(this._produceMaterial.InvoiceXOId))
            {
                Model.InvoiceXO invoiceXO = invoiceXOManager.Get(this._produceMaterial.InvoiceXOId);
                this.textEditCustomerXOId.Text = invoiceXO == null ? null : invoiceXO.CustomerInvoiceXOId;
                this.textEditPiHao.Text = invoiceXO == null ? null : invoiceXO.CustomerLotNumber;
            }
            else
            {
                this.textEditCustomerXOId.Text = string.Empty;
                this.textEditPiHao.Text = string.Empty;
                // this.textEditXOId.Text = string.Empty;
            }

            foreach (Model.ProduceMaterialdetails item in this._produceMaterial.Details)
            {
                Model.PronotedetailsMaterial pro = this._pronotedetailMaterialManager.GetByHeadIdAndDetailId(item.PronoteHeaderID, item.PronotedetailsID);
                if (pro != null)
                {
                    item.Materialprocessum = pro.PronoteQuantity;
                    item.Materialprocesedsum = pro.PronoteQuantity;
                }
            }
            this.bindingSourceDetails.DataSource = this._produceMaterial.Details;
            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.ButtonDepotOutState.Enabled = false;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.ButtonDepotOutState.Enabled = false;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    if (this._produceMaterial.DepotOutState != 2)
                        this.ButtonDepotOutState.Enabled = true;
                    else
                        this.ButtonDepotOutState.Enabled = false;
                    break;

            }
            //this.newChooseEmployee1.Enabled = false;

            this.EmpAudit.Enabled = false;
            this.textEditPronoteHeaderID.Properties.ReadOnly = true;
            this.comboBoxEdit1.Properties.ReadOnly = true;
            this.textEditProduceMaterialID.Properties.ReadOnly = true;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(_produceMaterial.ProduceMaterialID);
        }

        protected override void MoveNext()
        {
            Model.ProduceMaterial produceMaterial = this.produceMaterialManager.GetNext(this._produceMaterial);
            if (produceMaterial == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceMaterial = this.produceMaterialManager.Get(produceMaterial.ProduceMaterialID);
        }

        protected override void MovePrev()
        {
            Model.ProduceMaterial produceMaterial = this.produceMaterialManager.GetPrev(this._produceMaterial);
            if (produceMaterial == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceMaterial = this.produceMaterialManager.Get(produceMaterial.ProduceMaterialID);
        }

        protected override void MoveFirst()
        {
            this._produceMaterial = this.produceMaterialManager.Get(this.produceMaterialManager.GetFirst() == null ? "" : this.produceMaterialManager.GetFirst().ProduceMaterialID);
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1) { LastFlag = 0; return; }
            // if (produceMaterial == null)
            {
                this._produceMaterial = this.produceMaterialManager.Get(this.produceMaterialManager.GetLast() == null ? "" : this.produceMaterialManager.GetLast().ProduceMaterialID);
            }
        }

        protected override bool HasRows()
        {
            return this.produceMaterialManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.produceMaterialManager.HasRowsAfter(this._produceMaterial);
        }

        protected override bool HasRowsPrev()
        {
            return this.produceMaterialManager.HasRowsBefore(this._produceMaterial);
        }

        protected override void AddNew()
        {
            if (this.tag == 1)
            {
                this.tag = 0; return;
            }

            this.bindingSourceDepotPositionId.DataSource = null;
            if (this._pronoteHeader == null)
            {
                this._produceMaterial = new Model.ProduceMaterial();
                this._produceMaterial.ProduceMaterialID = this.produceMaterialManager.GetId();// Guid.NewGuid().ToString();
                this._produceMaterial.Employee0 = BL.V.ActiveOperator.Employee;
                this._produceMaterial.ProduceMaterialDate = DateTime.Now;
                this._produceMaterial.Details = new List<Model.ProduceMaterialdetails>();
                if (this.action == "insert")
                {
                    Model.ProduceMaterialdetails detail = new Model.ProduceMaterialdetails();
                    detail.Inumber = this._produceMaterial.Details.Count + 1;
                    detail.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                    detail.Materialprocessum = 0;
                    detail.Materialprocesedsum = 0;
                    detail.ProductStock = 0;
                    detail.ProductSpecification = "";
                    detail.Product = new Book.Model.Product();
                    this._produceMaterial.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
            }
            else
            {
                this._produceMaterial = new Model.ProduceMaterial();
                this._produceMaterial.ProduceMaterialID = this.produceMaterialManager.GetId();// Guid.NewGuid().ToString();

                this._produceMaterial.Details = new List<Model.ProduceMaterialdetails>();

                if (this.action == "insert")
                {
                    foreach (Model.PronotedetailsMaterial item in this._pronoteHeader.DetailsMaterial)
                    {
                        Model.ProduceMaterialdetails detail = new Model.ProduceMaterialdetails();
                        detail.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                        detail.PronotedetailsID = item.PronotedetailsID;
                        detail.PronoteHeaderID = this._pronoteHeader.PronoteHeaderID;
                        detail.Materialprocessum = 0;
                        detail.Materialprocesedsum = 0;
                        detail.ProductStock = 0;
                        detail.ProductSpecification = "";
                        detail.ProductId = item.ProductId;
                        detail.Product = item.Product;
                        this._produceMaterial.Details.Add(detail);
                        this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                    }
                }
            }
        }

        // “+”
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {

                if (this._produceMaterial.Details.Count > 0 && this._produceMaterial.Details[0] != null && string.IsNullOrEmpty(this._produceMaterial.Details[0].ProductId))
                    this._produceMaterial.Details.RemoveAt(0);
                Model.ProduceMaterialdetails detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.ProduceMaterialdetails();
                        detail.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                        detail.Inumber = this._produceMaterial.Details.Count + 1;
                        detail.Product = this.productManager.Get(product.ProductId);
                        detail.ProductId = detail.Product.ProductId;
                        detail.ProductSpecification = detail.Product.ProductSpecification;
                        detail.Materialprocessum = 0;
                        detail.Materialprocesedsum = 0;
                        detail.ProductStock = detail.Product.StocksQuantity;
                        detail.DepotStock = mRSdetailsManager.SumSpotStock(detail.ProductId);

                        if (this._produceMaterial.Details == null || this._produceMaterial.Details.Count == 0)
                            detail.HandbookId = "";
                        else
                            detail.HandbookId = (this.bindingSourceDetails[0] as Model.ProduceMaterialdetails).HandbookId;

                        if (!detail.Product.ProduceMaterialDistributioned.HasValue)
                            detail.Product.ProduceMaterialDistributioned = 0;
                        if (!detail.Product.OtherMaterialDistributioned.HasValue)
                            detail.Product.OtherMaterialDistributioned = 0;
                        detail.Distributioned = detail.Product.ProduceMaterialDistributioned + detail.Product.OtherMaterialDistributioned;
                        this._produceMaterial.Details.Add(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.ProduceMaterialdetails();
                    Model.Product product = this.productManager.Get((f.SelectedItem as Model.Product).ProductId); ;
                    detail.Inumber = this._produceMaterial.Details.Count + 1;
                    detail.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                    detail.Product = this.productManager.Get(product.ProductId);
                    detail.ProductId = detail.Product.ProductId;
                    detail.ProductSpecification = detail.Product.ProductSpecification;
                    detail.Materialprocessum = 0;
                    detail.Materialprocesedsum = 0;
                    detail.ProductStock = detail.Product.StocksQuantity;
                    detail.DepotStock = mRSdetailsManager.SumSpotStock(detail.ProductId);

                    if (this._produceMaterial.Details == null || this._produceMaterial.Details.Count == 0)
                        detail.HandbookId = "";
                    else
                        detail.HandbookId = (this.bindingSourceDetails[0] as Model.ProduceMaterialdetails).HandbookId;

                    if (!detail.Product.ProduceMaterialDistributioned.HasValue)
                        detail.Product.ProduceMaterialDistributioned = 0;
                    if (!detail.Product.OtherMaterialDistributioned.HasValue)
                        detail.Product.OtherMaterialDistributioned = 0;
                    detail.Distributioned = detail.Product.ProduceMaterialDistributioned + detail.Product.OtherMaterialDistributioned;
                    this._produceMaterial.Details.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                f.Dispose();
                GC.Collect();
            }
        }

        // "-"
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this._produceMaterial.Details.Remove(this.bindingSourceDetails.Current as Book.Model.ProduceMaterialdetails);

                if (this._produceMaterial.Details.Count == 0)
                {
                    Model.ProduceMaterialdetails detail = new Model.ProduceMaterialdetails();
                    detail.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                    detail.Materialprocessum = 0;
                    detail.Materialprocesedsum = 0;
                    detail.ProductStock = 0;
                    detail.ProductSpecification = "";
                    detail.Product = new Book.Model.Product();
                    this._produceMaterial.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        //选择加工单
        private void simpleButtonXO_Click(object sender, EventArgs e)
        {

            PronoteHeader.ChoosePronoteHeaderForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderForm();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList.Count != 0)
            {
                string xoid = PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].PronoteHeader.InvoiceXOId;
                if (!string.IsNullOrEmpty(xoid))
                {
                    Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(xoid);
                    this.textEditCustomerXOId.Text = invoiceXO.CustomerInvoiceXOId;
                }
                else
                    this.textEditCustomerXOId.Text = string.Empty;
                //this._produceMaterial.Details.Clear();

                //if (_pronotedetails != null)
                //{
                foreach (Model.PronotedetailsMaterial PronoteMaterial in PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList)
                {
                    Model.ProduceMaterialdetails produceMaterialdetails = new Book.Model.ProduceMaterialdetails();
                    produceMaterialdetails.Inumber = this._produceMaterial.Details.Count + 1;
                    produceMaterialdetails.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                    produceMaterialdetails.PronoteHeaderID = this._pronoteHeader.PronoteHeaderID;
                    produceMaterialdetails.PronotedetailsID = PronoteMaterial.PronotedetailsID;
                    if (PronoteMaterial.Product != null)
                    {
                        produceMaterialdetails.Product = PronoteMaterial.Product;
                        produceMaterialdetails.ProductId = PronoteMaterial.Product.ProductId;
                        produceMaterialdetails.ProductStock = PronoteMaterial.Product.StocksQuantity;
                        if (PronoteMaterial.Product.DepotUnit != null)
                            produceMaterialdetails.ProductUnit = PronoteMaterial.Product.DepotUnit.CnName;
                        produceMaterialdetails.Distributioned = produceMaterialdetails.Product.ProduceMaterialDistributioned;
                        produceMaterialdetails.ProductSpecification = PronoteMaterial.Product.ProductSpecification;
                    }
                    produceMaterialdetails.Materialprocessum = PronoteMaterial.PronoteQuantity;
                    //produceMaterialdetails.Materialprocesedsum = PronoteMaterial.DetailsSum;

                    produceMaterialdetails.ProduceMaterialID = this._produceMaterial.ProduceMaterialID;

                    produceMaterialdetails.PronoteHeaderID = PronoteMaterial.PronoteHeaderID;

                    //produceMaterialdetails.InvoiceXOId = this.produceMaterial.pro;
                    //produceMaterialdetails.InvoiceXODetailId = Pronotedetails.InvoiceXODetailId;
                    this._produceMaterial.Details.Add(produceMaterialdetails);

                }

                this.gridControl1.RefreshDataSource();

            }

        }

        //选择物料需求单
        private void sBMRP_Click(object sender, EventArgs e)
        {

            MRSHeader.SelectMrsHeaderAndDetails f = new MRSHeader.SelectMrsHeaderAndDetails();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (f.key != null && f.key.Count > 0)
            {
                string xoid = string.Empty;

                if (new BL.MRSdetailsManager().Get(f.key[0]).MRSHeader.MPSheaderId != null)
                {
                    xoid = new BL.MPSheaderManager().Get(new BL.MRSdetailsManager().Get(f.key[0]).MRSHeader.MPSheaderId).InvoiceXOId;
                }

                this.comboBoxEdit1.SelectedIndex = 1;
                this.textEditPronoteHeaderID.EditValue = new BL.MRSdetailsManager().Get(f.key[0]).MRSHeaderId;
                this.invoiceId = new BL.MRSdetailsManager().Get(f.key[0]).MRSHeaderId;
                if (!string.IsNullOrEmpty(xoid))
                {
                    Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(xoid);
                    if (invoiceXO != null)
                    {
                        this.textEditCustomerXOId.Text = invoiceXO.CustomerInvoiceXOId;
                        this._produceMaterial.InvoiceXOId = xoid;
                    }
                }
                else
                    this.textEditCustomerXOId.Text = string.Empty;
                //this._produceMaterial.Details.Clear();

                //IList<Model.ProduceMaterialdetails> dtlist = new List<Model.ProduceMaterialdetails>();
                IList<Model.ProduceMaterialdetails> dtlist = this._produceMaterial.Details;
                Model.ProduceMaterialdetails produceMaterialdetails;
                for (int i = 0; i < f.key.Count; i++)
                {

                    Model.MRSdetails mRSdetails = this.mRSdetailsManager.Get(f.key[i]);

                    produceMaterialdetails = new Book.Model.ProduceMaterialdetails();
                    produceMaterialdetails.Inumber = this._produceMaterial.Details.Count + 1;
                    produceMaterialdetails.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                    produceMaterialdetails.MRSHeaderId = mRSdetails.MRSHeaderId;
                    produceMaterialdetails.MRSdetailsId = mRSdetails.MRSdetailsId;
                    produceMaterialdetails.Product = mRSdetails.Product;
                    produceMaterialdetails.ProductId = mRSdetails.Product.ProductId;
                    produceMaterialdetails.ProductStock = mRSdetails.Product.StocksQuantity;
                    produceMaterialdetails.DepotStock = mRSdetailsManager.SumSpotStock(produceMaterialdetails.ProductId);
                    produceMaterialdetails.ProductUnit = mRSdetails.ProductUnit;

                    if (!mRSdetails.Product.ProduceMaterialDistributioned.HasValue)
                        mRSdetails.Product.ProduceMaterialDistributioned = 0;
                    if (!mRSdetails.Product.OtherMaterialDistributioned.HasValue)
                        mRSdetails.Product.OtherMaterialDistributioned = 0;
                    produceMaterialdetails.Distributioned = mRSdetails.Product.ProduceMaterialDistributioned + mRSdetails.Product.OtherMaterialDistributioned;
                    produceMaterialdetails.ProductSpecification = mRSdetails.Product.ProductSpecification;
                    produceMaterialdetails.NextWorkHouse = mRSdetails.WorkHouseNext;
                    produceMaterialdetails.NextWorkHouseId = mRSdetails.WorkHouseNextId;

                    if (mRSdetails.MRSHeader.SourceType == "1")
                        produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailsQuantity;
                    else
                        produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailssum;


                    //produceMaterialdetails.Materialprocesedsum = PronoteMaterial.DetailsSum;                   
                    produceMaterialdetails.ProduceMaterialID = this._produceMaterial.ProduceMaterialID;
                    produceMaterialdetails.MPSDetailsSum = mRSdetails.MRSdetailsQuantity;
                    produceMaterialdetails.HandbookProductId = mRSdetails.HandbookProductId;
                    produceMaterialdetails.HandbookId = mRSdetails.HandbookId;

                    produceMaterialdetails.MRSHeaderId = mRSdetails.MRSHeaderId;
                    xoid = produceMaterialdetails.MRSHeaderId;
                    //produceMaterialdetails.InvoiceXOId = this.produceMaterial.pro;
                    //produceMaterialdetails.InvoiceXODetailId = Pronotedetails.InvoiceXODetailId;
                    dtlist.Add(produceMaterialdetails);

                    #region 注释
                    //Model.MRSdetails mRSdetails = new BL.MRSdetailsManager().Get(f.key[i]);
                    //if (mRSdetails == null) continue;
                    //Model.BomParentPartInfo bom = new BL.BomParentPartInfoManager().Get(mRSdetails.Product);
                    //if (bom == null)
                    //    continue;
                    //foreach (Model.BomComponentInfo comm in new BL.BomComponentInfoManager().Select(bom))
                    //{
                    //    Model.ProduceMaterialdetails produceMaterialdetails = new Book.Model.ProduceMaterialdetails();
                    //    produceMaterialdetails.Inumber = this._produceMaterial.Details.Count + 1;
                    //    produceMaterialdetails.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                    //    produceMaterialdetails.MRSHeaderId = mRSdetails.MRSHeaderId;
                    //    produceMaterialdetails.MRSdetailsId = mRSdetails.MRSdetailsId;

                    //    produceMaterialdetails.Product = comm.Product;
                    //    produceMaterialdetails.ProductId = comm.Product.ProductId;
                    //    produceMaterialdetails.ProductStock = comm.Product.StocksQuantity;
                    //    produceMaterialdetails.ProductUnit = comm.Unit;

                    //    produceMaterialdetails.Distributioned = comm.Product.ProduceMaterialDistributioned;
                    //    produceMaterialdetails.ProductSpecification = comm.Product.ProductSpecification;

                    //    produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailssum * comm.UseQuantity * (1 + 0.01 * (comm.SubLoseRate == null ? 0 : comm.SubLoseRate));
                    //    //produceMaterialdetails.Materialprocesedsum = PronoteMaterial.DetailsSum;

                    //    produceMaterialdetails.ProduceMaterialID = this._produceMaterial.ProduceMaterialID;
                    //    //produceMaterialdetails.InvoiceXOId = this.produceMaterial.pro;
                    //    //produceMaterialdetails.InvoiceXODetailId = Pronotedetails.InvoiceXODetailId;
                    //    dtlist.Add(produceMaterialdetails);
                    //}
                    //if (!string.IsNullOrEmpty(mRSdetails.Product.CustomerProductName))
                    //{
                    //    IList<Model.BomPackageDetails> packageList = this.bomPackageDetailsManager.Select(bom.BomId);
                    //    if (packageList != null)
                    //    {
                    //        foreach (Model.BomPackageDetails item in packageList)
                    //        {
                    //            Model.ProduceMaterialdetails produceMaterialdetails = new Book.Model.ProduceMaterialdetails();
                    //            produceMaterialdetails.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                    //            produceMaterialdetails.Inumber = this._produceMaterial.Details.Count + 1;
                    //            produceMaterialdetails.MRSHeaderId = mRSdetails.MRSHeaderId;
                    //            produceMaterialdetails.MRSdetailsId = mRSdetails.MRSdetailsId;

                    //            produceMaterialdetails.Product = item.Product;
                    //            produceMaterialdetails.ProductId = item.Product.ProductId;
                    //            produceMaterialdetails.ProductStock = item.Product.StocksQuantity;
                    //            produceMaterialdetails.ProductUnit = item.PackageUnit;

                    //            produceMaterialdetails.Distributioned = item.Product.ProduceMaterialDistributioned;
                    //            produceMaterialdetails.ProductSpecification = item.Product.ProductSpecification;

                    //            produceMaterialdetails.Materialprocessum = mRSdetails.MRSdetailssum * item.Quantity;
                    //            //produceMaterialdetails.Materialprocesedsum = PronoteMaterial.DetailsSum;

                    //            produceMaterialdetails.ProduceMaterialID = this._produceMaterial.ProduceMaterialID;
                    //            //produceMaterialdetails.InvoiceXOId = this.produceMaterial.pro;
                    //            //produceMaterialdetails.InvoiceXODetailId = Pronotedetails.InvoiceXODetailId;
                    //            dtlist.Add(produceMaterialdetails);
                    //        }
                    //    }
                    //}

                    #endregion
                }
                int count = 1;
                this._produceMaterial.Details = (from p in dtlist
                                                 group p by new { p.ProductId, p.ProductUnit } into pm
                                                 select new Model.ProduceMaterialdetails()
                                                 {
                                                     ProduceMaterialdetailsID = pm.First().ProduceMaterialdetailsID,
                                                     Inumber = count++,
                                                     MRSHeaderId = pm.First().MRSHeaderId,
                                                     MRSdetailsId = pm.First().MRSdetailsId,
                                                     Product = pm.First().Product,
                                                     ProductId = pm.First().ProductId,
                                                     ProductStock = pm.First().ProductStock,
                                                     DepotStock = pm.First().DepotStock,
                                                     ProductUnit = pm.First().ProductUnit,
                                                     Distributioned = pm.First().Distributioned,
                                                     ProductSpecification = pm.First().Product.ProductSpecification,
                                                     Materialprocessum = (from m in pm select m.Materialprocessum).Sum(),
                                                     ProduceMaterialID = this._produceMaterial.ProduceMaterialID,
                                                     MPSDetailsSum = pm.First().MPSDetailsSum,
                                                     NextWorkHouseId = pm.First().NextWorkHouseId,
                                                     NextWorkHouse = pm.First().NextWorkHouse,
                                                     HandbookProductId = pm.First().HandbookProductId,
                                                     HandbookId = pm.First().HandbookId
                                                 }).ToList<Model.ProduceMaterialdetails>();
                this.bindingSourceDetails.DataSource = this._produceMaterial.Details;
                this.gridControl1.RefreshDataSource();

            }

            f.Dispose();
            GC.Collect();
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.ProduceMaterialdetails detail = this.gridView1.GetRow(e.RowHandle) as Model.ProduceMaterialdetails;

            if (e.Column == this.ColProductId)
            {
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                    detail.Materialprocessum = 0;
                    detail.Materialprocesedsum = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.ProductStock = p.StocksQuantity;
                    detail.ProductSpecification = p.ProductSpecification;

                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
            //if (e.Column == this.gridColumn6)
            //{
            //    detail.DepotPosition = null;
            //}
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Model.ProduceMaterialdetails detail = this.gridView1.GetRow(e.RowHandle) as Model.ProduceMaterialdetails;
            //if (detail != null)
            //{
            //    if (detail.Materialprocesedsum.HasValue && detail.Materialprocesedsum.Value > 0)
            //    {
            //        MessageBox.Show("本條記錄已有出倉,在此不能進行修改", "提示", MessageBoxButtons.OK);
            //        this._TempIsChuCangupdate = true;
            //        return;
            //    }
            //}
        }

        //详细项目选择
        void bindingSourceDetails_CurrentChanged(object sender, EventArgs e)
        {
            this._TempProduceMaterialdetails = this.bindingSourceDetails.Current as Model.ProduceMaterialdetails;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceMaterialdetails> details = this.bindingSourceDetails.DataSource as IList<Model.ProduceMaterialdetails>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            //IList<Model.MPSheader> mpsHeader = new BL.MPSheaderManager().Select(details[e.ListSourceRowIndex]);
            switch (e.Column.Name)
            {
                case "ColProductId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
                case "ColCusProductId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.CustomerProductName) ? "" : detail.CustomerProductName;
                    break;
                //case "gridColumn5":
                //    if (detail == null || mpsHeader == null) return;
                //    e.DisplayText = mpsHeader[e.ListSourceRowIndex].Id;
                //    break;

            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                //if (this.gridView1.FocusedColumn.Name == "gridColumn7")
                //{
                //    Model.ProduceMaterialdetails detail = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceMaterialdetails;
                //    this.repositoryItemComboBox1.Items.Clear();
                //    if (detail != null)
                //    {
                //        if (detail.DepotId != null)
                //        {
                //            IList<Model.DepotPosition> unitList = depotPositionManager.Select(detail.DepotId);
                //            foreach (Model.DepotPosition item in unitList)
                //            {
                //                this.repositoryItemComboBox1.Items.Add(item.Id);
                //            }

                //        }
                //        this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                //    }
                //}

                if (this.gridView1.FocusedColumn.Name == "gridColumnProductUnit")
                {

                    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {
                        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceMaterialdetails).Product;

                        this.repositoryItemComboBox2.Items.Clear();
                        if (p != null)
                        {
                            if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                            {
                                BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                                IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroupId);
                                foreach (Model.ProductUnit item in unitList)
                                {
                                    this.repositoryItemComboBox2.Items.Add(item.CnName);
                                }

                            }

                        }
                    }
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            //string sql = "SELECT productid,id,productname,CustomerProductName FROM product";
            //this.bindingSourceProductId.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            this.bindingSourcDepot.DataSource = depotManager.Select();
            this.bindingSourceDepotPositionId.DataSource = depotPositionManager.Select();
            this.bindingSourceWorkHouse.DataSource = new BL.WorkHouseManager().Select();

            //this.bindingSourceProductId.DataSource = productManager.GetProduct();
        }

        private void barButtonPronoteHeader_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PronoteHeader.ChoosePronoteHeaderForm f = new PronoteHeader.ChoosePronoteHeaderForm(1);
            //if (f.ShowDialog(this) != DialogResult.OK) return;
            //if (PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList.Count != 0)
            //{
            //    this._produceMaterial.Details.Clear();
            //    Model.PronoteHeader pronoteHeader = PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].PronoteHeader;

            //    this.textEditPronoteHeaderID.Text = PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].PronoteHeader.PronoteHeaderID;
            //    this.textEditProId.Text = pronoteHeader.Product.Id;
            //    this.textEditProName.Text = pronoteHeader.Product.ProductName;
            //    this.textEditCusProName.Text = pronoteHeader.Product.CustomerProductName;
            //    this.textEditCustomerXOId.Text = pronoteHeader.InvoiceXOId;
            //    foreach (Model.PronotedetailsMaterial pronoteMaterial in PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList)
            //    {
            //        Model.ProduceMaterialExitDetail produceMaterialExitDetail = new Book.Model.ProduceMaterialExitDetail();
            //        produceMaterialExitDetail.ProduceExitMaterialDetailId = Guid.NewGuid().ToString();
            //        produceMaterialExitDetail.Product = pronoteMaterial.Product;
            //        produceMaterialExitDetail.ProductId = pronoteMaterial.ProductId;
            //        produceMaterialExitDetail.PronotedetailsMaterialId = pronoteMaterial.PronotedetailsMaterialId;
            //        produceMaterialExitDetail.ProduceQuantity = 0;
            //        if (produceMaterialExitDetail.Product != null)
            //        {
            //            if (pronoteMaterial.Product.DepotUnit != null)
            //                produceMaterialExitDetail.ProductUnit = pronoteMaterial.Product.DepotUnit.CnName;
            //            // produceMaterialExitDetail. = pronotedetails.Product.ProductSpecification;                                  }
            //            //单位  pronotedetails. =  pronotedetails.ProductUnit;
            //            //pronotedetails.InDepotQuantity = Convert.ToDouble(mpsdetail.MPSdetailssum);                     
            //            //produceMaterialExitDetail.ProduceAllUserQuantity = pronotedetails.DetailsSum;
            //            //produceMaterialExitDetail. = this._produceMaterialExit;
            //            //produceMaterialExitDetail._produceMaterialExitID = this._produceMaterialExit._produceMaterialExitID
            //            //produceMaterialExitDetail.MPSheaderId = pronotedetails.MPSheaderId;
            //            //produceMaterialExitDetail.InvoiceXOId = pronotedetails.InvoiceXOId;
            //            //produceMaterialExitDetail.InvoiceXODetailId = pronotedetails.InvoiceXODetailId;                      
            //        }
            //        this._produceMaterialExit.Detail.Add(produceMaterialExitDetail);
            //    }
            //    this.gridControl1.RefreshDataSource();

            //}

        }

        //搜索
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm();
            form.IsEdit = true;
            if (form.ShowDialog() == DialogResult.OK)
            {
                this._produceMaterial = form.SelectItem as Model.ProduceMaterial;
                this.action = "view";
                this.Refresh();
            }
            GC.Collect();
            form.Dispose();
        }

        private void barButtonItem1_ItemClick(object sender, EventArgs e)
        {
            //ListForm form = new ListForm();
            //form.IsEdit = true;
            //if (form.ShowDialog() == DialogResult.OK)
            //{
            //    this._produceMaterial = form.SelectItem as Model.ProduceMaterial;
            //    this.action = "view";
            //    this.Refresh();
            //}
            //GC.Collect();
            //form.Dispose();
        }

        private void ButtonDepotOutState_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (MessageBox.Show(Properties.Resources.ConfirmToJieAn, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._produceMaterial.DepotOutState = 2;
            this.produceMaterialManager._Update(this._produceMaterial);
            MessageBox.Show(Properties.Resources.SaveSuccess, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Refresh();
        }

        //选择加工单
        private void sBPronoteHeader_Click(object sender, EventArgs e)
        {

            PronoteHeader.ChoosePronoteHeaderForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderForm();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList.Count != 0)
            {
                this.calcEditInvoiceSum.Value = 0;
                string xoid = PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].PronoteHeader.InvoiceXOId;
                this.textEditPronoteHeaderID.Text = PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].PronoteHeaderID;

                //只有数据来源于同一个加工单才显示主件商品
                //if (PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList.GroupBy(p => p.PronoteHeaderID).Count() == 1)
                this.textEditProduct.Text = string.IsNullOrEmpty(PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].Product.CustomerProductName) ? PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].Product.ProductName : PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].Product.ProductName + "{" + PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].Product.CustomerProductName + "}";

                this.invoiceId = PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].PronoteHeaderID;
                this._produceMaterial.InvoiceXOId = xoid;
                this.comboBoxEdit1.SelectedIndex = 0;
                //this.com
                if (!string.IsNullOrEmpty(xoid))
                {
                    Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(xoid);
                    if (invoiceXO != null)
                    {
                        this.textEditCustomerXOId.Text = invoiceXO.CustomerInvoiceXOId;

                        Model.MRSdetails mrsdetail = new BL.MRSdetailsManager().Get(PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].PronoteHeader.MRSdetailsId);
                        if (mrsdetail != null && invoiceXO.Details != null && invoiceXO.Details.Count > 0)
                        {
                            //foreach (Model.InvoiceXODetail detail in invoiceXO.Details)
                            //{
                            //    if (detail.ProductId == mrsdetail.MadeProductId)
                            //        this.calcEditInvoiceSum.Text = detail.InvoiceXODetailQuantity.Value.ToString("f0");
                            //}
                        }
                    }
                }
                else
                    this.textEditCustomerXOId.Text = string.Empty;
                //this._produceMaterial.Details.Clear();

                //if (_pronotedetails != null)
                //{


                foreach (Model.PronotedetailsMaterial PronoteMaterial in PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList)
                {
                    Model.ProduceMaterialdetails produceMaterialdetails = new Book.Model.ProduceMaterialdetails();
                    produceMaterialdetails.Inumber = this._produceMaterial.Details.Count + 1;
                    produceMaterialdetails.ProduceMaterialdetailsID = Guid.NewGuid().ToString();
                    produceMaterialdetails.PronoteHeaderID = PronoteMaterial.PronoteHeaderID;
                    produceMaterialdetails.PronotedetailsID = PronoteMaterial.PronotedetailsMaterialId;
                    if (PronoteMaterial.Product != null)
                    {
                        produceMaterialdetails.Product = PronoteMaterial.Product;
                        produceMaterialdetails.ProductId = PronoteMaterial.Product.ProductId;
                        produceMaterialdetails.ProductStock = PronoteMaterial.Product.StocksQuantity;
                        produceMaterialdetails.DepotStock = mRSdetailsManager.SumSpotStock(produceMaterialdetails.ProductId);

                        if (!PronoteMaterial.Product.ProduceMaterialDistributioned.HasValue)
                            PronoteMaterial.Product.ProduceMaterialDistributioned = 0;
                        if (!PronoteMaterial.Product.OtherMaterialDistributioned.HasValue)
                            PronoteMaterial.Product.OtherMaterialDistributioned = 0;
                        produceMaterialdetails.Distributioned = PronoteMaterial.Product.ProduceMaterialDistributioned + PronoteMaterial.Product.OtherMaterialDistributioned;
                        produceMaterialdetails.ProductSpecification = PronoteMaterial.Product.ProductSpecification;
                    }
                    produceMaterialdetails.ProductUnit = PronoteMaterial.ProductUnit;
                    produceMaterialdetails.Materialprocessum = PronoteMaterial.PronoteQuantity;


                    produceMaterialdetails.HandbookProductId = PronoteMaterial.PronoteHeader.HandbookProductId;
                    produceMaterialdetails.HandbookId = PronoteMaterial.PronoteHeader.HandbookId;

                    //produceMaterialdetails.Materialprocesedsum = PronoteMaterial.DetailsSum;

                    produceMaterialdetails.ProduceMaterialID = this._produceMaterial.ProduceMaterialID;
                    //produceMaterialdetails.InvoiceXOId = this.produceMaterial.pro;
                    //produceMaterialdetails.InvoiceXODetailId = Pronotedetails.InvoiceXODetailId;
                    this._produceMaterial.Details.Add(produceMaterialdetails);

                }
                this.gridControl1.RefreshDataSource();
                PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList.Clear();
            }
            f.Dispose();
            GC.Collect();
        }

        //条件查询
        private void barButtonItemQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query.ConditionMaterialChooseForm form = new Query.ConditionMaterialChooseForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Query.ConditionMaterial condition = form.Condition as Query.ConditionMaterial;
                try
                {
                    RODetail f = new RODetail(condition);
                    f.ShowPreviewDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void textEditProduceMaterialdesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.textEditProduceMaterialdesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }
    }
}