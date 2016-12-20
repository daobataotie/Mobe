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
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���            ���ʱ��:2009-6-5
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q15Form : BaseForm
    {
        private BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();
       

        //����
        public Q15Form()
        {
            InitializeComponent();
        }


        /// <summary>
        ///  gridview����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null || e.Button != MouseButtons.Right)
                return;

            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                view.FocusedRowHandle = hitInfo.RowHandle;
                this.popupMenu1.ShowPopup(view.GridControl.PointToScreen(hitInfo.HitPoint));
            }
        }

        //��д
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R15();
        }

        private void barButtonItemGenerateInvoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.bindingSource1.Current == null)
                return;

            //DataRow r = (this.bindingSource1.Current as DataRowView).Row;
            //Model.Company c = this.companyManager.Get((string)r[Model.Company.PROPERTY_COMPANYID]);
            //Invoices.QK.EditForm f = new Book.UI.Invoices.QK.EditForm(c);
            //f.ShowDialog(this);
        }

        protected override void DoQuery()
        {
            this.bindingSource1.DataSource = this.miscDataManager.Q15(DateTime.Now.Day);
        }
    }
}