using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.CustomsClearance
{
    public partial class BGProductDepotOutForm : Settings.BasicData.BaseEditForm
    {
        BL.BGProductDepotOutManager manager = new Book.BL.BGProductDepotOutManager();
        BL.BGProductDepotOutDetailManager detailManager = new Book.BL.BGProductDepotOutDetailManager();
        Model.BGProductDepotOut _bGProductDepotOut = new Book.Model.BGProductDepotOut();
        BL.BGHandbookDetail1Manager bGHandbookDetail1Manager = new Book.BL.BGHandbookDetail1Manager();

        int flag = 0;
        public BGProductDepotOutForm()
        {
            InitializeComponent();
            this.action = "view";
            this.invalidValueExceptions.Add(Model.BGProductDepotOut.PRO_DeclareCustomsId,new AA("报关单号不能为空！",this.txt_DeclareCustomsId));
        }

        protected override void AddNew()
        {
            this._bGProductDepotOut = new Book.Model.BGProductDepotOut();
            this._bGProductDepotOut.BGProductDepotOutId = this.manager.GetId();
            this._bGProductDepotOut.BGProductDepotOutDate = DateTime.Now;

            this.action = "insert";
        }

        protected override bool HasRows()
        {
            return manager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return manager.HasRowsAfter(this._bGProductDepotOut);
        }

        protected override bool HasRowsPrev()
        {
            return manager.HasRowsBefore(this._bGProductDepotOut);
        }

        protected override void MoveFirst()
        {
            this._bGProductDepotOut = this.manager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (flag == 1)
            {
                flag = 0;
                return;
            }
            this._bGProductDepotOut = this.manager.GetLast();
        }

        protected override void MoveNext()
        {

            Model.BGProductDepotOut model = this.manager.GetNext(this._bGProductDepotOut);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGProductDepotOut = model;
        }

        protected override void MovePrev()
        {
            Model.BGProductDepotOut model = this.manager.GetPrev(this._bGProductDepotOut);
            if (model == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._bGProductDepotOut = model;
        }

        public override void Refresh()
        {
            if (this._bGProductDepotOut == null)
                this.AddNew();
            else
            {
                if (this.action == "view")
                    this._bGProductDepotOut = this.manager.Select(this._bGProductDepotOut.BGProductDepotOutId);
            }
            this.txt_BGProductDepotOutId.EditValue = this._bGProductDepotOut.BGProductDepotOutId;
            this.Date_BGProductDepotOutDate.EditValue = this._bGProductDepotOut.BGProductDepotOutDate;
            this.txt_DeclareCustomsId.EditValue = this._bGProductDepotOut.DeclareCustomsId;
            this.txt_InvoiceXSId.EditValue = this._bGProductDepotOut.InvoiceXSId;
            this.bindingSource1.DataSource = this._bGProductDepotOut.Detail;

            base.Refresh();


            //switch (this.action)
            //{
            //    case "view":
            //        this.gridView1.OptionsBehavior.Editable = false;
            //        break;
            //    default:
            //        this.gridView1.OptionsBehavior.Editable = true;
            //        break;
            //}
            this.gridView1.OptionsBehavior.Editable = false;
        }

        protected override void Save()
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();
            this._bGProductDepotOut.BGProductDepotOutId = this.txt_BGProductDepotOutId.Text;
            this._bGProductDepotOut.BGProductDepotOutDate = this.Date_BGProductDepotOutDate.EditValue == null ? DateTime.Now : this.Date_BGProductDepotOutDate.DateTime;
            this._bGProductDepotOut.DeclareCustomsId = this.txt_DeclareCustomsId.Text;
            this._bGProductDepotOut.InvoiceXSId = this.txt_InvoiceXSId.Text;

            switch (this.action)
            {
                case "insert":
                    this.manager.Insert(this._bGProductDepotOut);
                    break;
                case "update":
                    this.manager.Update(this._bGProductDepotOut);
                    break;
            }
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.manager.Delete(this._bGProductDepotOut.BGProductDepotOutId);
                this._bGProductDepotOut = this.manager.GetNext(this._bGProductDepotOut);
                if (this._bGProductDepotOut == null)
                    this._bGProductDepotOut = this.manager.GetLast();

                MessageBox.Show(Properties.Resources.DeleteSuccess);
            }
        }

        private void BarInvoiceXS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Invoices.XS.SearchXSDetail f = new Book.UI.Invoices.XS.SearchXSDetail();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.selectItems.Count > 0)
                    this.txt_InvoiceXSId.EditValue = f.selectItems[0].InvoiceId;
                Model.BGProductDepotOutDetail detail;
                foreach (var item in f.selectItems)
                {
                    detail = new Book.Model.BGProductDepotOutDetail();
                    detail.BGProductDepotOutDetailId = Guid.NewGuid().ToString();
                    detail.BGHandbookId = item.HandbookId;
                    detail.BGHandbookProductId = item.HandbookProductId;
                    detail.BGHandbookProductName = this.bGHandbookDetail1Manager.SelectProName(detail.BGHandbookId, detail.BGHandbookProductId);
                    detail.CustomerProductName = item.Product == null ? null : item.Product.CustomerProductName;
                    detail.InvoiceCGProductName = item.Product == null ? null : item.Product.ToString();
                    detail.ProductUnit = item.InvoiceProductUnit;
                    detail.Quantity = item.InvoiceXSDetailQuantity;
                    this._bGProductDepotOut.Detail.Add(detail);
                }
            }
            this.gridControl1.RefreshDataSource();
            f.Dispose();
            GC.Collect();
        }
    }
}