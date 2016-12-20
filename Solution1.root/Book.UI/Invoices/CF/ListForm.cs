using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.CF
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  ����w�Yܛ�����޹�˾
   //                     ������� �����ؾ�
   // ��������: ��ֆΓ�һ�[��(����������Ԕ����Ϣ��չʾ)
     * �^���˻���w,�L��yһ,������^���^
   // �� �� ����ListForm
   // �� �� ��: ƈ����                   ���ʱ��:2009-05-09
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   //----------------------------------------------------------------*/
    public partial class ListForm : BaseListForm
    {

        #region ���캯��
        public ListForm()
        {
            if (!EditForm.isDelete)
                this.CancleDelete();
            InitializeComponent();
            this.invoiceManager = new BL.InvoiceZZManager();
        }
        #endregion

        private void ListForm_Load(object sender, EventArgs e)
        {

        }

        #region ���d��ķ���
        protected override Form GetViewForm()
        {
            Model.InvoiceZZ invoice = this.SelectedItem as Model.InvoiceZZ;
            if (invoice != null)
                return new ViewForm(invoice.InvoiceId);

            return null;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
            //return new R02(this.bindingSource1.DataSource as IList<Model.InvoiceZZ>);
        }

        protected override DevExpress.XtraGrid.Views.Grid.GridView MainView
        {
            get
            {
                return this.gridView1;
            }
        }
        #endregion

    }
}