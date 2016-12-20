using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Book.UI.Invoices.XS
{
    public partial class HisXSInfomationForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.InvoiceXSManager invoiceXSmanager = new Book.BL.InvoiceXSManager();
        private BL.InvoiceXODetailManager invoiceXOmanger = new Book.BL.InvoiceXODetailManager();
        private BL.CustomerManager customermanager = new Book.BL.CustomerManager();

        public HisXSInfomationForm()
        {
            InitializeComponent();         
        }
        private void band()
        {
            this.treeList1.ClearNodes();
            foreach (Model.Customer customer in customermanager.selectCustomerInXS())
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { customer.CustomerShortName }, null, null);
                foreach (Model.InvoiceXS invoiceXS in invoiceXSmanager.SelectInvoice(customer))
                {
                    treeList1.AppendNode(new object[] { invoiceXS.InvoiceXOId }, treeNode, invoiceXS.InvoiceXOId);
                }
            }
        }           

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node.Tag == null) return;
            bindingSource1.DataSource = invoiceXSmanager.SelectCustomerInfo(e.Node.Tag.ToString());
        }

       

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {            
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                EditForm.xs = bindingSource1.Current as Model.InvoiceXS;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void HisXSInfomationForm_Load(object sender, EventArgs e)
        {
            band();
        }
    }
}