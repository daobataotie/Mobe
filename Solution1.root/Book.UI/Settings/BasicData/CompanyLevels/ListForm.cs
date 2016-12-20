using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.CompanyLevels
{

    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  ����w�Yܛ�����޹�˾
   //                     ������� �����ؾ�
   // ��������: �͑����e�O��
   // �� �� ����ListForm
   // �� �� ��: ���޾�                   ���ʱ��:2009-09-27
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   // �޸�ԭ��
   // �� �� ��:                          �޸�ʱ��:
   //----------------------------------------------------------------*/
    public partial class ListForm : DevExpress.XtraEditors.XtraForm
    {
        #region myj---�I�Ռ����x
        protected BL.CompanyLevelManager companyLevelManager = new Book.BL.CompanyLevelManager();
        #endregion

        #region myj---�o�K���캯��
        public ListForm()
        {
            InitializeComponent();
        }
        #endregion

        #region myj---���w���d�¼�(����Ĭ�J����)
        private void ListForm_Load(object sender, EventArgs e)
        {
            this.companyLevelBindingSource.DataSource = this.companyLevelManager.SelectDateTable();
        }
        #endregion

        #region myj---barButtonItem�c���¼�
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            // ����������
            System.Data.DataTable table = (DataTable)this.companyLevelBindingSource.DataSource;
            this.companyLevelManager.UpdateDataTable(table);
        }
        #endregion

    }
}