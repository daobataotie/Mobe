using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
/*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  够波涛             完成时间:2009-5-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class InvoicesListForm : BaseForm
    {
        protected BL.Invoice00Manager invoice00Manager = new Book.BL.Invoice00Manager();

        public InvoicesListForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch ((string)e.Item.Tag)
            {
                case "refresh":
                    this.invoiceBindingSource.DataSource = this.invoice00Manager.Select();
                    break;
                case "edit":
                    this.LuanchEdit((Model.Invoice00)this.invoiceBindingSource.Current);
                    break;
                case "delete":
                    break;
                case "detail":
                    this.View((Model.Invoice00)this.invoiceBindingSource.Current);
                    break;
                case "null":
                    if (MessageBox.Show(Properties.Resources.ConfirmNullInvoice, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Null((Model.Invoice00)this.invoiceBindingSource.Current);
                        this.invoiceBindingSource.DataSource = this.invoice00Manager.Select();    
                    }
                    //this.barButtonItem1
                    break;
                default:
                    break;
            }
        }
        private void View(Model.Invoice00 invoice) 
        {
            if (invoice == null) return;
            Form f = null;

            switch (invoice.Kind)
            {
                case "bs":
                    f = new Invoices.BS.ViewForm(invoice.InvoiceId);
                    break;

                case "by":
                    f = new Invoices.BY.ViewForm(invoice.InvoiceId);
                    break;

                case "cg":
                    f = new Invoices.CG.ViewForm(invoice.InvoiceId);
                    break;

                case "cj":
                    f = new Invoices.CJ.ViewForm(invoice.InvoiceId);
                    break;

                case "co":
                    f = new Invoices.CO.ViewForm(invoice.InvoiceId);
                    break;

                case "ct":
                    f = new Invoices.CT.ViewForm(invoice.InvoiceId);
                    break;

                case "fk":
                    f = new Invoices.FK.ViewForm(invoice.InvoiceId);
                    break;

                case "ft":
                    f = new Invoices.FT.ViewForm(invoice.InvoiceId);
                    break;

                case "hz":
                    f = new Invoices.HZ.ViewForm(invoice.InvoiceId);
                    break;

                case "pt":
                    f = new Invoices.PT.ViewForm(invoice.InvoiceId);
                    break;

                case "qi":
                    f = new Invoices.QI.ViewForm(invoice.InvoiceId);
                    break;

                case "qo":
                    f = new Invoices.QO.ViewForm(invoice.InvoiceId);
                    break;

                case "sk":
                    f = new Invoices.SK.ViewForm(invoice.InvoiceId);
                    break;

                case "xj":
                    f = new Invoices.XJ.ViewForm(invoice.InvoiceId);
                    break;

                case "xo":
                    f = new Invoices.XO.ViewForm(invoice.InvoiceId);
                    break;
                case "xs":
                    f = new Invoices.XS.ViewForm(invoice.InvoiceId);
                    break;

                case "xt":
                    f = new Invoices.XT.ViewForm(invoice.InvoiceId);
                    break;

                //case "zf":
                //    f = new Invoices.ZF.ViewForm(invoice.InvoiceId);
                //    break;

                case "zs":
                    f = new Invoices.ZS.ViewForm(invoice.InvoiceId);
                    break;

                case "zz":
                    f = new Invoices.ZZ.ViewForm(invoice.InvoiceId);
                    break;
            }
            if (f != null)
            {
                f.MdiParent = this.MdiParent;
                f.Show();
            }
        }
        private void Null(Model.Invoice00 invoice) 
        {
            switch (invoice.Kind)
            {
                case "bs":
                    new BL.InvoiceBSManager().TurnNull(invoice.InvoiceId);
                    break;

                case "by":
                    new BL.InvoiceBYManager().TurnNull(invoice.InvoiceId);
                    break;

                case "cg":
                    new BL.InvoiceCGManager().TurnNull(invoice.InvoiceId);
                    break;

                case "cj":
                    new BL.InvoiceCJManager().TurnNull(invoice.InvoiceId);
                    break;

                case "co":
                    new BL.InvoiceCOManager().TurnNull(invoice.InvoiceId);
                    break;

                case "ct":
                    new BL.InvoiceCTManager().TurnNull(invoice.InvoiceId);
                    break;

                case "fk":
                    new BL.InvoiceFKManager().TurnNull(invoice.InvoiceId);
                    break;

                case "ft":
                    new BL.InvoiceFTManager().TurnNull(invoice.InvoiceId);
                    break;

                case "hz":
                    new BL.InvoiceHZManager().TurnNull(invoice.InvoiceId);
                    break;

                case "pt":
                    new BL.InvoicePTManager().TurnNull(invoice.InvoiceId);
                    break;

                case "qi":
                    new BL.InvoiceQIManager().TurnNull(invoice.InvoiceId);
                    break;

                case "qo":
                    new BL.InvoiceQOManager().TurnNull(invoice.InvoiceId);
                    break;

                case "sk":
                    new BL.InvoiceSKManager().TurnNull(invoice.InvoiceId);
                    break;

                case "xj":
                    new BL.InvoiceXJManager().TurnNull(invoice.InvoiceId);
                    break;

                case "xo":
                    new BL.InvoiceXOManager().TurnNull(invoice.InvoiceId);
                    break;
                case "xs":
                    new BL.InvoiceXSManager().TurnNull(invoice.InvoiceId);
                    break;

                case "xt":
                    new BL.InvoiceXTManager().TurnNull(invoice.InvoiceId);
                    break;

                //case "zf":
                //    f = new Invoices.ZF.ViewForm(invoice.InvoiceId);
                //    break;

                case "zs":
                    new BL.InvoiceZSManager().TurnNull(invoice.InvoiceId);
                    break;

                case "zz":
                    new BL.InvoiceZZManager().TurnNull(invoice.InvoiceId);
                    break;
            }
        }
        private void LuanchEdit(Model.Invoice00 invoice)
        {
            if (invoice == null) return;
            Form f = null;
            switch (invoice.Kind)
            {
                case "bs":
                    f = new Invoices.BS.EditForm(invoice.InvoiceId);
                    break;

                case "by":
                    f = new Invoices.BY.EditForm(invoice.InvoiceId);
                    break;

                case "cg":
                    f = new Invoices.CG.EditForm(invoice.InvoiceId);
                    break;

                case "cj":
                    f = new Invoices.CJ.EditForm(invoice.InvoiceId);
                    break;

                case "co":
                    f = new Invoices.CO.EditForm(invoice.InvoiceId);
                    break;

                case "ct":
                    f = new Invoices.CT.EditForm(invoice.InvoiceId);
                    break;

                case "fk":
                    f = new Invoices.FK.EditForm(invoice.InvoiceId);
                    break;

                case "ft":
                    f = new Invoices.FT.EditForm(invoice.InvoiceId);
                    break;

                case "hz":
                    f = new Invoices.HZ.EditForm(invoice.InvoiceId);
                    break;

                case "pt":
                    f = new Invoices.PT.EditForm(invoice.InvoiceId);
                    break;

                case "qi":
                    f = new Invoices.QI.EditForm(invoice.InvoiceId);
                    break;

                case "qo":
                    f = new Invoices.QO.EditForm(invoice.InvoiceId);
                    break;

                case "sk":
                    f = new Invoices.SK.EditForm(invoice.InvoiceId);
                    break;

                case "xj":
                    f = new Invoices.XJ.EditForm(invoice.InvoiceId);
                    break;

                case "xo":
                    f = new Invoices.XO.EditForm(invoice.InvoiceId);
                    break;                    
                case "xs":
                    f = new Invoices.XS.EditForm(invoice.InvoiceId);
                    break;

                case "xt":
                    f = new Invoices.XT.EditForm(invoice.InvoiceId);
                    break;

                //case "zf":
                //    f = new Invoices.ZF.EditForm(invoice.InvoiceId);
                //    break;

                case "zs":
                    f = new Invoices.ZS.EditForm(invoice.InvoiceId);
                    break;

                case "zz":
                    f = new Invoices.ZZ.EditForm(invoice.InvoiceId);
                    break;
            }
            if (f != null)
            {
                f.MdiParent = this.MdiParent;
                f.Show();
            }
        }

        private void invoiceBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            Model.Invoice00 invoice = this.invoiceBindingSource.Current as Model.Invoice00;
            
            //if (invoice == null)
            //{
            //    this.barButtonItem4.Enabled = false;
            //    this.barButtonItem7.Enabled = false;    
            //}
            //else 
            //{
            //    this.barButtonItem4.Enabled = (invoice.InvoiceStatus != 2);
            //    this.barButtonItem7.Enabled = (invoice.InvoiceStatus == 1);
            //}
            
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
          return null;
        }

        protected override void DoQuery()
        {
            this.invoiceBindingSource.DataSource = this.invoice00Manager.Select();
        }
    }
}