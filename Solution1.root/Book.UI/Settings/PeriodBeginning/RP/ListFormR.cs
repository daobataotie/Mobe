using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.PeriodBeginning.RP
{

    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���޾�            ���ʱ��:2009-10-14
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class ListFormR : DevExpress.XtraEditors.XtraForm
    {
       
        public ListFormR()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            DataTable dt = (DataTable)this.bindingSource1.DataSource;
            int rowCount = dt.Rows.Count;
            if (rowCount > 0)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr.RowState == DataRowState.Modified)
                    {
                        dr["CompanyR1"] = dr["CompanyR0"];
                        dr["CompanyP1"] = dr["CompanyP0"];
                    }
                }
                //this.companyManager.UpdateDataTable(dt);
                this.DataBind();
            }else
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        //����
        private void ListForm_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }
        private void DataBind() 
        {
            //this.bindingSource1.DataSource = this.companyManager.SelectDataTable(global::Helper.CompanyKind.Customer);
        }
    }
}