using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.CG
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 採購單(採購的詳細信息的展示)
     * 窗體繼承了基類窗體,風格統一,介面比較美觀
   // 文 件 名：CGFormm
   // 编 码 人: 茍波濤                   完成时间:2009-05-09
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class CGForm : DevExpress.XtraEditors.XtraForm
    {

        #region 構造函數
        //無慘
        private BL.InvoiceCOManager invoiceCOManager = new BL.InvoiceCOManager();
        private BL.InvoiceCODetailManager invoicecoDetailManager = new Book.BL.InvoiceCODetailManager();
        private IList<Model.InvoiceCODetail> detailList;
        private IList<Model.InvoiceCODetail> _key = new List<Model.InvoiceCODetail>();
        public IList<Model.InvoiceCODetail> key
        {
            get { return _key; }
            set { _key = value; }
        }
        public CGForm()
        {
            InitializeComponent();
            this.newChooseSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.dateEdit1.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEdit2.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }
        //一個參數supplier是model對象
        public CGForm(Model.Supplier supplier)
            : this()
        {
            this.bindingSource1.DataSource = new BL.InvoiceCOManager().Select(supplier);
        }

        #endregion

        #region 變量定義
        private Model.InvoiceCO _invoice;
        //get訪問器
        public Model.InvoiceCO Invoice
        {
            get
            {
                return _invoice;
            }
        }

        private BL.InvoiceCODetailManager invoiceCODetailManager = new Book.BL.InvoiceCODetailManager();
        #endregion

        #region gridview的雙擊事件
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

            //_invoice = this.bindingSource1.Current as Model.InvoiceCO;
            //if (_invoice != null)
            //{
            //    _invoice.Details = this.invoiceCODetailManager.Select(_invoice);
            //    EditForm._invoice = _invoice;
            //    this.DialogResult = DialogResult.OK;
            //}
            //else
            //{
            //    MessageBox.Show(Properties.Resources.RequireDataForCJ, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

        }
        #endregion

        #region 無操作
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex == -1)
            //    return;
            //Model.InvoiceCO ico = (this.bindingSource1.Current as Model.InvoiceCO);
            //switch (e.Column.Name)
            //{       
            //    case"gridColumnEmployee0":
            //        e.DisplayText = string.IsNullOrEmpty( ico.Employee0.EmployeeName)?"":ico.Employee0.EmployeeName ;
            //        break;
            //    case "gridColumnSupplier":
            //        e.DisplayText =string.IsNullOrEmpty(ico.Supplier.SupplierShortName)?"":ico.Supplier.SupplierShortName;
            //        break;
            //    default:
            //        break;
            //}
        }
        #endregion

        private void CGForm_Load(object sender, EventArgs e)
        {
         //   this.newChooseContorlSupplier.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();
            this.bindingSource1.DataSource = invoiceCOManager.SelectDateRangAndWhere(null,null, null, null, this.dateEdit1.DateTime, this.dateEdit2.DateTime, null, null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, 0);//非2 显示全部
        }

        private void simpleButton_Search_Click(object sender, EventArgs e)
        {
            Query.ConditionCOChooseForm f = new Query.ConditionCOChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Query.ConditionCO con = f.Condition as Query.ConditionCO;
                this.dateEdit1.DateTime = con.StartDate;
                this.dateEdit2.DateTime = con.EndDate;
                this.bindingSource1.DataSource = invoiceCOManager.SelectDateRangAndWhere(con.COStartId,con.COEndId, con.SupplierStart, con.SupplierEnd, con.StartDate, con.EndDate, con.ProductStart, con.ProductEnd, con.CusXOId, con.StartJHDate, con.EndJHDate, con.InvoiceFlag.Value);
                bandDetail();
            }
            f.Dispose();
            GC.Collect();        
        }
        private void bandDetail()
        {
            if (this.bindingSource1.Current != null)
            {
                detailList = this.invoicecoDetailManager.Select(this.bindingSource1.Current as Model.InvoiceCO);
                if (detailList != null)
                {
                     int flag = 0;
                for (int i = 0; i < key.Count; i++)
                {
                    foreach (Model.InvoiceCODetail detail in detailList)
                    {
                        if (key[i].InvoiceCODetailId==detail.InvoiceCODetailId)
                        {
                            detail.Checked = true;
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 1)
                        break;
                }
                  
                }
                this.checkEditALL.Checked = false;
                this.bindingSourceDetail.DataSource = detailList;
            }
            else
            {
                detailList = null;
                this.bindingSourceDetail.DataSource = detailList;
            }
        }
        private void simpleButtonSearch_Click(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = invoiceCOManager.SelectDateRangAndWhere(null, null, this.newChooseSupplier.EditValue as Model.Supplier, this.newChooseSupplier.EditValue as Model.Supplier, this.dateEdit1.DateTime, this.dateEdit2.DateTime, null, null, null, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, this.checkEditJieAn.Checked == true ? 1 : 0);
            bandDetail();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bandDetail();
        }

        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            bandDetail();
        }

        private void gridViewDetail_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumnCheck")
            {
                Model.InvoiceCODetail detail = this.gridViewDetail.GetRow(e.RowHandle) as Model.InvoiceCODetail;

                if ((bool)e.Value)
                {
                    key.Add(detail);
                    //  MrsDetails.Add(this.mRSdetailsManager.Get(detail.MRSdetailsId));
                }
                if (!(bool)e.Value)
                {
                    for (int i = 0; i < key.Count; i++)
                    {
                        if (key[i].InvoiceCODetailId == detail.InvoiceCODetailId)
                        {
                            key.RemoveAt(i);
                            break;
                        }
                    }             
                    //  MrsDetails.Remove(this.mRSdetailsManager.Get(detail.MRSdetailsId));
                }
            }
        }

        private void checkEditALL_CheckedChanged(object sender, EventArgs e)
        {
            int flag = 0;
             if (checkEditALL.Checked == true)
            {      
        
                foreach (Model.InvoiceCODetail detail in detailList)
                {
                    flag = 0;
                    detail.Checked = true;                
                    for (int i = 0; i < key.Count; i++)
                    {
                        if (key[i].InvoiceCODetailId == detail.InvoiceCODetailId)
                        {
                            flag = 1;                                   
                            break;
                        }
                    }
                    if(flag==0)
                    key.Add(detail);     
                                    
                }
            } 
            if (checkEditALL.Checked == false)
            {             

                foreach (Model.InvoiceCODetail detail in detailList)
                {
                    detail.Checked = false ;                 
                    for (int i = 0; i < key.Count; i++)
                    {
                        if (key[i].InvoiceCODetailId == detail.InvoiceCODetailId)
                        {                          
                            key.RemoveAt(i);
                            break;
                        }
                    }          
                                      
                }
            }
           // this.gridViewDetail.UpdateCurrentRow();
           // this.gridViewDetail.PostEditor();
            this.gridControl2.RefreshDataSource();
        }

        private void simpleButtonCel_Click(object sender, EventArgs e)
        {
            key.Clear();
            detailList.Clear();
            this.Close();
            this.Dispose();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}