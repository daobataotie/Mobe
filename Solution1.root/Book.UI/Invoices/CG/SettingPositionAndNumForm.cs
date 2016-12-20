using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.CG
{
    public partial class SettingPositionAndNumForm : Form
    {
        private IList<Model.InvoiceCGDetail> InvoiceCGDetailList = new List<Model.InvoiceCGDetail>();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.InvoiceCGDetailManager _invoiceCGDetailManager = new Book.BL.InvoiceCGDetailManager();
        Model.InvoiceCGDetail _invoicecg;
        public SettingPositionAndNumForm(Model.InvoiceCGDetail invoice)
        {
            InitializeComponent();
            this._invoicecg = invoice;
        }

        private void SettingPositionAndNumForm_Load(object sender, EventArgs e)
        {
            IList<Model.DepotPosition> list = depotPositionManager.Select(this._invoicecg.Depot);
            if (list.Count == 0) return;
            foreach (Model.DepotPosition item in list)
            {
                Model.InvoiceCGDetail invoiceCGDetail = this._invoiceCGDetailManager.SelectByProductIdAndHeadIdAndPositionId(this._invoicecg.ProductId, this._invoicecg.InvoiceId, item.DepotPositionId);
                if (invoiceCGDetail == null)
                {
                    invoiceCGDetail = new Book.Model.InvoiceCGDetail();
                    invoiceCGDetail.InvoiceCGDetailId = Guid.NewGuid().ToString();
                    invoiceCGDetail.InvoiceId = this._invoicecg.InvoiceId;
                    invoiceCGDetail.ProductId = this._invoicecg.ProductId;
                    invoiceCGDetail.Product = this._invoicecg.Product;
                    invoiceCGDetail.DepotPositionId = item.DepotPositionId;
                    invoiceCGDetail.Donatetowards = this._invoicecg.Donatetowards;
                    invoiceCGDetail.InvoiceProductUnit = this._invoicecg.Product.DepotUnit.CnName;
                    invoiceCGDetail.InvoiceCODetailId = this._invoicecg.InvoiceCODetailId;
                    invoiceCGDetail.InvoiceCODetail = this._invoicecg.InvoiceCODetail;
                }
                if (EditForm.dic.ContainsKey(this._invoicecg.ProductId + item.DepotPositionId))
                {
                    Model.InvoiceCGDetail d = EditForm.dic[this._invoicecg.ProductId + item.DepotPositionId];
                    invoiceCGDetail.InvoiceCGDetailQuantity = d.InvoiceCGDetailQuantity;
                    invoiceCGDetail.InvoiceCODetailId = this._invoicecg.InvoiceCODetailId;
                }
                else
                {
                    invoiceCGDetail.InvoiceCGDetailId = Guid.NewGuid().ToString();
                    invoiceCGDetail.InvoiceCODetailId = this._invoicecg.InvoiceCODetailId;
                    invoiceCGDetail.InvoiceCGDetailQuantity = 0;
                }
                invoiceCGDetail.DepotPosition = item;
                invoiceCGDetail.DepotPositionId = item.DepotPositionId;
                this.InvoiceCGDetailList.Add(invoiceCGDetail);
            }

            this.bindingSourceDetail.DataSource = this.InvoiceCGDetailList;
            this.gridControl1.RefreshDataSource();
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            InvoiceCGDetailList = this.bindingSourceDetail.DataSource as IList<Model.InvoiceCGDetail>;
            double nums = 0;
            if (InvoiceCGDetailList != null)
            {
                foreach (Model.InvoiceCGDetail item in this.InvoiceCGDetailList)
                {
                    item.ORDERId = this._invoicecg.ORDERId;
                    if (EditForm.dic.ContainsKey(this._invoicecg.ProductId + item.DepotPositionId))
                    {
                        if (item.InvoiceCGDetailQuantity != 0)
                        {
                            EditForm.dic.Remove(this._invoicecg.ProductId + item.DepotPositionId);
                            EditForm.dic.Add(this._invoicecg.ProductId + item.DepotPositionId, item);
                        }
                        else
                            EditForm.dic.Remove(this._invoicecg.ProductId + item.DepotPositionId);
                    }
                    else
                    {
                        if (item.InvoiceCGDetailQuantity != 0)
                        {
                            EditForm.dic.Add(this._invoicecg.ProductId + item.DepotPositionId, item);
                        }
                    }
                    nums += item.InvoiceCGDetailQuantity.Value;
                }
            }
            EditForm.SetNums = nums;
            EditForm.invoicecg.PositionAndNumsSet.Clear();
            foreach (var item in EditForm.dic.Values)
            {
                EditForm.invoicecg.PositionAndNumsSet.Add(item);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
