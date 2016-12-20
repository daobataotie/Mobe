using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.PeriodBeginning.AccountsBalance
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���            ���ʱ��:2009-10-10
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class ListForm : DevExpress.XtraEditors.XtraForm
    {
        protected BL.AccountManager accountManager = new Book.BL.AccountManager();

        public ListForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListForm_Load(object sender, EventArgs e)
        {
            this.accountBindingSource.DataSource = this.accountManager.SelectDataTable();
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            DataTable dt = (DataTable)this.accountBindingSource.DataSource;
            int rowCount = dt.Rows.Count;
            if (rowCount > 0)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr.RowState == DataRowState.Modified)
                    {
                        dr["AccountBalance1"] = dr["AccountBalance0"];
                    }
                }

                this.accountManager.UpdateDataTable(dt);
            }
            else
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            
        }
    }
}