using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.XS
{
    public partial class SettingDepotPositionNums : Form
    {

        private BL.DepotPositionManager depotpositionManager = new Book.BL.DepotPositionManager();
        private BL.InvoiceXSDetailManager detailManager = new Book.BL.InvoiceXSDetailManager();
        private Model.InvoiceXS invoicexs = new Book.Model.InvoiceXS();
        private Model.InvoiceXSDetail invoiceXSdetail;
        public SettingDepotPositionNums(Model.InvoiceXSDetail temp)
        {
            InitializeComponent();
            this.invoiceXSdetail = temp;
            IList<Model.DepotPosition> positions = depotpositionManager.GetDepotPositionsByDepotAndProduct(invoiceXSdetail.ProductId, invoiceXSdetail.Invoice.Depot.DepotId);
            this.invoicexs.Details = new List<Model.InvoiceXSDetail>();
            foreach (var item in positions)
            {
                Model.InvoiceXSDetail detail = new Book.Model.InvoiceXSDetail();
                detail.DepotPosition = item;
                detail.DepotPositionId = item.DepotPositionDescription;
                detail.ProductId = this.invoiceXSdetail.ProductId;
                if (EditForm.dic.ContainsKey(detail.ProductId + item.DepotPositionId))
                {
                    detail.InvoiceXSDetailQuantity = EditForm.dic[detail.ProductId + item.DepotPositionId].InvoiceXSDetailQuantity;
                }
                else
                {
                    Model.InvoiceXSDetail d = detailManager.GetByProIdPosIdInvoiceId(detail.ProductId, item.DepotPositionId, invoiceXSdetail.InvoiceId);
                    if (d != null)
                        detail.InvoiceXSDetailQuantity = d.InvoiceXSDetailQuantity;
                    else
                        detail.InvoiceXSDetailQuantity = 0;
                }
                this.invoicexs.Details.Add(detail);
            }
            this.bindingSourceDetail.DataSource = this.invoicexs.Details;
        }

        private void simpleButton_Sure_Click(object sender, EventArgs e)
        {
            EditForm.sum = 0;
            IList<Model.InvoiceXSDetail> list = this.bindingSourceDetail.DataSource as IList<Model.InvoiceXSDetail>;
            foreach (var item in list)
            {
                if (item.InvoiceXSDetailQuantity != 0)
                {
                    Model.InvoiceXSDetail tem = new Book.Model.InvoiceXSDetail();
                    if (EditForm.dic.ContainsKey(item.ProductId + item.DepotPosition.DepotPositionId))
                    {
                        EditForm.dic.Remove(item.ProductId + item.DepotPosition.DepotPositionId);
                    }
                    tem.Product = invoiceXSdetail.Product;
                    tem.ProductId = invoiceXSdetail.ProductId;
                    tem.InvoiceXSDetailId = Guid.NewGuid().ToString();
                    tem.InvoiceXSDetailQuantity = item.InvoiceXSDetailQuantity;
                    tem.DepotPositionId = item.DepotPosition.DepotPositionId;
                    tem.InvoiceProductUnit = invoiceXSdetail.InvoiceProductUnit;
                    tem.InvoiceXODetailId = invoiceXSdetail.InvoiceXODetailId;
                    EditForm.sum += item.InvoiceXSDetailQuantity.Value;
                    EditForm.dic.Add(item.ProductId + item.DepotPosition.DepotPositionId, tem);
                }
            }
            EditForm.invoice.Setdetails.Clear();
            foreach (Model.InvoiceXSDetail item in EditForm.dic.Values)
            {
                EditForm.invoice.Setdetails.Add(item);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
